using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using SubSonic;
using Janus.Windows.GridEX;
using VNS.Libs;
using System.Transactions;
using VNS.Properties;

namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_DynamicSetup : Form
    {
        public DmucDichvuclsChitiet objDichvuchitiet = null;
        
        public long ImageID = -1;
        public long Id_chidinhchitiet = -1;
        bool hasDeleted = false;
        public frm_DynamicSetup()
        {
            InitializeComponent();
            Config();
            this.Load += frm_DynamicSetup_Load;
            this.KeyDown += frm_DynamicSetup_KeyDown;
            this.FormClosing += frm_DynamicSetup_FormClosing;
            cmdExit.Click+=cmdExit_Click;
            grdList.UpdatingCell += grdList_UpdatingCell;
            grdList.DeletingRecords += grdList_DeletingRecords;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            cmdConfig.Click += cmdConfig_Click;
        }


        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            _Properties.ShowDialog();
            Config();
        }
        void Config()
        {
           
        }
        void frm_DynamicSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasDeleted)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void grdList_DeletingRecords(object sender, CancelEventArgs e)
        {
            try
            {
                int _Id=Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdList,DynamicField.Columns.Id), -1);
                if (_Id > 0)
                {
                     DynamicField.Delete(_Id);
                     hasDeleted = true;
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        void frm_DynamicSetup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
             if (e.Control && e.KeyCode == Keys.S)
            {
                cmdSave.PerformClick();
                return;
            }
        }
        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                var q = from p in grdList.GetDataRows().AsEnumerable()
                        where p != grdList.CurrentRow
                        && Utility.sDbnull(p.Cells[DynamicField.Columns.Ma], "") == e.Value
                        select p;
                if (q.Count() > 0)
                {
                    Utility.ShowMsg("Mã này đã tồn tại, bạn cần nhập mã khác!");
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
            }
        }

        void frm_DynamicSetup_Load(object sender, EventArgs e)
        {
            LoadData();
            Utility.focusCell(grdList, DynamicField.Columns.Ma);
        }
        void LoadData()
        {
            try
            {
                if(objDichvuchitiet==null)
                {
                    Utility.ShowMsg("Không xác định được danh mục dịch vụ CĐHA. Bạn cần kiểm tra lại danh mục hệ thống");
                    cmdSave.Enabled = false;
                }
                Utility.SetDataSourceForDataGridEx(grdList, GetDynamicFieldsValues(), true, true, "1=1", "Stt_hthi");
                
            }
            catch (Exception ex)
            {
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<DynamicField> lstFields = new List<DynamicField>();
                foreach (GridEXRow _row in grdList.GetDataRows())
                {
                    DynamicField obj = null;

                    if (Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1) > 0)
                    {
                        obj = DynamicField.FetchByID(Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1));
                        obj.IsNew = false;
                        obj.MarkOld();
                    }
                    else
                    {
                        obj = new DynamicField();
                        obj.IsNew = true;
                    }

                   

                    obj.Id = Utility.Int32Dbnull(_row.Cells[DynamicField.Columns.Id].Value, -1);
                    obj.IdDichvuchitiet = objDichvuchitiet.IdChitietdichvu;
                    obj.Ma = Utility.sDbnull(_row.Cells[DynamicField.Columns.Ma].Value, "-1");
                    obj.Mota = Utility.sDbnull(_row.Cells[DynamicField.Columns.Mota].Value, "-1");
                    obj.Stt = Utility.Int16Dbnull(_row.Cells[DynamicField.Columns.Stt].Value, 0);
                    obj.Rtxt = Utility.ByteDbnull(_row.Cells[DynamicField.Columns.Rtxt].Value, 0);
                    obj.TopLabel = Utility.ByteDbnull(_row.Cells[DynamicField.Columns.TopLabel].Value, 0);

                    obj.Bodypart = objDichvuchitiet.Bodypart;
                    obj.Viewposition = objDichvuchitiet.ViewPosition;

                  
                    lstFields.Add(obj);
                   
                }
                ActionResult _actionResult = UpdateDynamicFields(lstFields);
                if (_actionResult == ActionResult.Success)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    LoadData();
                    Utility.focusCell(grdList, DynamicField.Columns.Ma);
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public DataTable GetDynamicFieldsValues()
        {
            try
            {
                return SPs.HinhanhGetDynamicFieldsValues(objDichvuchitiet.IdChitietdichvu, objDichvuchitiet.Bodypart, objDichvuchitiet.ViewPosition, ImageID, Id_chidinhchitiet).GetDataSet().Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        public ActionResult UpdateDynamicFields(List<DynamicField> lstFields)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        foreach (DynamicField _object in lstFields)
                        {
                            if (_object.Id > 0)
                            {
                                _object.MarkOld();
                                _object.IsNew = false;
                                _object.Save();
                            }
                            else//Insert
                            {
                                _object.IsNew = true;

                                _object.Save();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                return ActionResult.Error;
            }

        }
    }
}
