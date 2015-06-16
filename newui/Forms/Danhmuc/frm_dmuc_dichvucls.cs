using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_dichvucls : Form
    {
        private  DataTable m_dtDataServiceType=new DataTable();
        private  DataTable dtDataCLS=new DataTable();
        private string RowFilter = "1=1";
        bool m_blnLoaded = false;
        public frm_dmuc_dichvucls()
        {
            InitializeComponent();
            this.KeyPreview = true;
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //grdList.ApplyingFilter+=new CancelEventHandler(grdList_ApplyingFilter);
            grdList.SelectionChanged +=new EventHandler(grdList_SelectionChanged);
           grdList.FilterApplied+=new EventHandler(grdList_FilterApplied);
           grdList.CellUpdated += new ColumnActionEventHandler(grdList_CellUpdated);
           cboDepartment.SelectedIndexChanged += new EventHandler(cboDepartment_SelectedIndexChanged);
        }

        void grdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                int ServiceDetail_ID = Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.IdDichvu));

                if (e.Column.Key == DmucDichvucl.Columns.SttHthi)
                {
                    new Update(DmucDichvucl.Schema)
                        .Set(DmucDichvucl.Columns.SttHthi).EqualTo(
                            Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.SttHthi)))
                        .Set(DmucDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        .Set(DmucDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(
                            Utility.Int32Dbnull(grdList.GetValue(DmucDichvucl.Columns.IdDichvu))).Execute();

                }
                Utility.GotoNewRowJanus(grdList, DmucDichvucl.Columns.IdDichvu, Utility.sDbnull(ServiceDetail_ID));
            }
            catch
            { }
        }

        void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DataTable dtPhong = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), 1);
            DataBinding.BindDataCombobox(cboPhongthuchien, dtPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa phòng", true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
            ModifyCommand();
        }
        void InitData()
        {
            try
            {
                m_dtDataServiceType = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAIDICHVUCLS", true);
                DataBinding.BindDataCombox(cboServiceType, m_dtDataServiceType, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataTable m_dtNhomDichVu = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAOCLS", true);
                DataBinding.BindDataCombox(cbonhombaocao, m_dtNhomDichVu, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                DataTable m_dtKhoaChucNang = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL",1);
                DataBinding.BindDataCombobox(cboDepartment, m_dtKhoaChucNang, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn---", true);
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("InitData()-->\n" + ex.Message);
            }
        }
        void Search()
        {
            SqlQuery _sqlquery = new Select().From(VDmucDichvucl.Schema);
            if (Utility.Int32Dbnull(cboServiceType.SelectedValue, -1) != -1)
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdLoaidichvu).IsEqualTo(Utility.Int32Dbnull(cboServiceType.SelectedValue, -1));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdLoaidichvu).IsEqualTo(Utility.Int32Dbnull(cboServiceType.SelectedValue, -1));

            if (Utility.sDbnull(cbonhombaocao.SelectedValue, "-1") != "-1")
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.NhomBaocao).IsEqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue, "-1"));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.NhomBaocao).IsEqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue, "-1"));

            if (Utility.Int32Dbnull(cboDepartment.SelectedValue, -1) != -1)
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdKhoaThuchien).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdKhoaThuchien).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));

            if (Utility.Int32Dbnull(cboPhongthuchien.SelectedValue, -1) != -1)
                if (_sqlquery.HasWhere)
                    _sqlquery.Where(VDmucDichvucl.Columns.IdPhongThuchien).IsEqualTo(Utility.Int32Dbnull(cboPhongthuchien.SelectedValue, -1));
                else
                    _sqlquery.And(VDmucDichvucl.Columns.IdPhongThuchien).IsEqualTo(Utility.Int32Dbnull(cboPhongthuchien.SelectedValue, -1));

            dtDataCLS = _sqlquery.ExecuteDataSet().Tables[0];
            dtDataCLS.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdList, dtDataCLS, true, true, "1=1", "stt_hthi_loaidvu,ten_loaidichvu,stt_hthi,ten_dichvu");
        }

        private void frm_dmuc_dichvucls_Load(object sender, EventArgs e)
        {
            InitData();
            m_blnLoaded = true;
            Search();
            ModifyCommand();


        }
        void ModifyCommand()
        {

            if (!Utility.isValidGrid(grdList))
            {
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdDeleteALL.Enabled = grdList.RowCount > 0;
                cmdPrint.Enabled = grdList.RowCount > 0;
                cmdSaveAll.Enabled = grdList.RowCount > 0;
            }
            else
            {
                cmdEdit.Enabled = true;
                cmdDelete.Enabled = true;
                cmdDeleteALL.Enabled = grdList.RowCount > 0;
                cmdPrint.Enabled = grdList.RowCount > 0;
                cmdSaveAll.Enabled = grdList.RowCount > 0;
            }
           
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
           
        }

      
     
        private int v_Service_ID = -1;
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtDataCLS.DefaultView.Count <= 0)
                {
                    Utility.ShowMsg("Hiện thời không có bản ghi nào để thao tác", "Thông báo");
                    grdList.Focus();
                    return;

                }
                if (Utility.AcceptQuestion("Bạn có muốn xoá 1 bản ghi đang chọn không ", "Thông báo", true))
                {
                    if (grdList.CurrentRow != null)
                    {
                        new Delete()
                            .From(DmucDichvucl.Schema)
                            .Where(DmucDichvucl.Columns.IdDichvu)
                            .IsEqualTo(v_Service_ID).Execute();
                        DataRow[] arrDr = dtDataCLS.Select(DmucDichvucl.Columns.IdDichvu + "=" + v_Service_ID);
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0].Delete();
                        }
                        dtDataCLS.AcceptChanges();

                    }
                }
               
            }
            catch (Exception)
            {
            }
            finally
            {
                ModifyCommand();
            }
          
        }

        private void cmdDeleteALL_Click(object sender, EventArgs e)
        {

            try
            {
                Janus.Windows.GridEX.GridEXRow[] checkedRows;
                checkedRows = grdList.GetCheckedRows();

                if (checkedRows.Length == 0)
                {

                    Utility.ShowMsg("Bạn phải chọn một bản ghi thao tác", "Thông báo");
                    grdList.Focus();
                    return;
                }
                if (grdList.CurrentRow != null)
                {
                    string message = string.Format("Bạn có muốn xoá {0} bản ghi đang chọn không", checkedRows.Length);
                    if (Utility.AcceptQuestion(message, "Thông báo", true))
                    {
                        foreach (Janus.Windows.GridEX.GridEXRow row in checkedRows)
                        {
                            ((DataRowView)row.DataRow).Delete();

                            new Delete()
                                .From(DmucDichvucl.Schema)
                                .Where(DmucDichvucl.Columns.IdDichvu)
                                .IsEqualTo(row.Cells[DmucDichvucl.Columns.IdDichvu].Value)
                                .Execute();

                        }



                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_dichvucls frm = new frm_themmoi_dichvucls();
                frm.em_Action = action.Insert;
                frm.grdService = grdList;
                frm.dsService = dtDataCLS;
                frm.ShowDialog();
                ModifyCommand();
            }catch(Exception exception)
            {
                ModifyCommand();
            }
           
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_dichvucls frm = new frm_themmoi_dichvucls();
                frm.em_Action = action.Update;
                if (grdList.CurrentRow != null)
                {
                    frm.drServiceInfo = Utility.FetchOnebyCondition(dtDataCLS,DmucDichvucl.Columns.IdDichvu+ "=" + v_Service_ID);
                    frm.txtID.Text = v_Service_ID.ToString();
                    frm.dsService = dtDataCLS;
                    frm.grdService = grdList;
                    frm.ShowDialog();
                    ModifyCommand();
                }
            }
            catch (Exception)
            {

                ModifyCommand();
            }
           
        }

       
        private void frm_dmuc_dichvucls_KeyDown(object sender, KeyEventArgs e)
        {
            //Add event handeler for Ctrl + E and Ctrl + D
            if(e.KeyCode==Keys.F4)cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.Control && (e.KeyCode == Keys.N)) cmdNew.PerformClick();//Call edit command
            if (e.Control && (e.KeyCode == Keys.E)) cmdEdit.PerformClick();//Call edit command
            if (e.Control && (e.KeyCode == Keys.D)) cmdDelete.PerformClick();//Call delete command
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
        
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null&&grdList.CurrentRow.RowType==RowType.Record)
            {
                v_Service_ID = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvucl.Columns.IdDichvu].Value, -1);
            }
            ModifyCommand();
        }

        private void grdList_FilterApplied(object sender, EventArgs e)
        {
           
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridEXRow gridExRow in grdList.GetRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        new Update(DmucDichvucl.Schema)
                            .Set(DmucDichvucl.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[DmucDichvucl.Columns.DonGia].Value, 0))
                            .Set(DmucDichvucl.Columns.SttHthi).EqualTo(Utility.Int32Dbnull(gridExRow.Cells[DmucDichvucl.Columns.SttHthi].Value,
                                                                                        -1))
                            .Set(DmucDichvucl.Columns.TenDichvu).EqualTo(Utility.sDbnull(
                                gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, ""))
                                  .Set(DmucDichvucl.Columns.MotaThem).EqualTo(Utility.sDbnull(
                                gridExRow.Cells[DmucDichvucl.Columns.MotaThem].Value, ""))
                            .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[DmucDichvucl.Columns.IdDichvu].Value, -1)).Execute();

                    }
                }
                grdList.UpdateData();
                Utility.ShowMsg("Cập nhập thông tin thành công","Thông báo");
            }catch(Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ","Thông báo",MessageBoxIcon.Error);
                return;
            }
            

        }


    }
}
