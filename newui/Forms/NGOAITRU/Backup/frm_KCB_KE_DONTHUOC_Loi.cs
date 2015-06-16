using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using SortOrder = System.Windows.Forms.SortOrder;
using Microsoft.VisualBasic;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using System.Drawing.Printing;
using VNS.HIS.NGHIEPVU.THUOC;
namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_KCB_KE_DONTHUOC_Loi : Form
    {
        bool Giathuoc_quanhe = false;
        KCB_KEDONTHUOC _KEDONTHUOC = new KCB_KEDONTHUOC();
        public DataTable dt_ICD_PHU = new DataTable();
        public DataTable dt_ICD = new DataTable();
        public DataTable m_dtDrugDataSource = new DataTable();
        public KcbChandoanKetluan _KcbChandoanKetluan ;
        public action em_Action = action.Insert;
        public KcbLuotkham objLuotkham { get; set; }
        public bool b_Cancel, isLoaded = false;
       
        private bool blnHasLoaded = false;
        private int IdDonthuoc = -1;
        //public int TrongGoi = 0;
        private bool Selected;
        private string TEN_BENHPHU = "";
        private bool hasMorethanOne = true;
        private bool isLike = true;
        //public int IdDonthuoc = -1
        public byte Noi_tru { get; set; }
        public int PreType { get; set; }
        public int TrongGoi { get; set; }
        private int ID_Goi_Dvu { get; set; }
        public string MaDoiTuong { get; set; }
        public KcbDangkyKcb objRegExam { get; set; }
        private string rowFilter = "1=2";
        public short ObjectType_Id = -1;
        public DataTable m_dtDonthuocChitiet = new DataTable();
        public DataTable m_dtDonthuocChitiet_View = new DataTable();
        public string v_PatientCode = "";
        public int v_Patient_ID = -1;
        public int id_kham = -1;
        public CallActionKieuKeDon CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
        private ActionResult _actionResult = ActionResult.Error;
        //public KcbLuotkham objLuotkham;
        public CallAction em_CallAction = CallAction.FromMenu;
        public frm_KCB_KE_DONTHUOC_Loi()
        {
            InitializeComponent();
            Utility.loadIconToForm(this);
            //txtCachDung.LostFocus+=new EventHandler(txtCachDung_LostFocus);
            this.KeyPreview = true;
            dtpCreatedDate.Value = dtNgayIn.Value = dtNgayKhamLai.Value = globalVariables.SysDate;
           
            InitEvents();
            CauHinh();
        }
        void InitEvents()
        {
            this.Load += new System.EventHandler(this.frm_KE_DONTHUOC_BN_NEW_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KE_DONTHUOC_BN_NEW_KeyDown);
            this.FormClosing += new FormClosingEventHandler(frm_KE_DONTHUOC_BN_NEW_FormClosing);
            grdPresDetail.KeyDown += new KeyEventHandler(grdPresDetail_KeyDown);
            grdPresDetail.UpdatingCell += new UpdatingCellEventHandler(grdPresDetail_UpdatingCell);
            grdPresDetail.CellEdited += new ColumnActionEventHandler(grdPresDetail_CellEdited);
            grdPresDetail.CellUpdated += new ColumnActionEventHandler(grdPresDetail_CellUpdated);
            grdPresDetail.SelectionChanged += new EventHandler(grdPresDetail_SelectionChanged);
            txtPres_ID.TextChanged += new System.EventHandler(this.txtPres_ID_TextChanged);
            txtQuantity.TextChanged += new EventHandler(txtQuantity_TextChanged);
            txtDrugID.TextChanged+=new EventHandler(txtDrugID_TextChanged);
            txtSolan.TextChanged+=new EventHandler(txtSolan_TextChanged);
            txtSoLuongDung.TextChanged+=new EventHandler(txtSoLuongDung_TextChanged);
            txtCachDung._OnSelectionChanged +=new UCs.AutoCompleteTextbox_Danhmucchung.OnSelectionChanged(txtCachDung__OnSelectionChanged);
            txtCachDung.TextChanged += new EventHandler(txtCachDung_TextChanged);
            chkSaveAndPrint.CheckedChanged += new EventHandler(chkSaveAndPrint_CheckedChanged);
            chkNgayTaiKham.CheckedChanged += new System.EventHandler(this.chkNgayTaiKham_CheckedChanged);
            mnuDelele.Click += new System.EventHandler(this.mnuDelele_Click);
            cmdSavePres.Click += new EventHandler(cmdSavePres_Click);
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            cmdDelete.Click+=new EventHandler(cmdDelete_Click);
            cmdDonThuocDaKe.Click+=new EventHandler(cmdDonThuocDaKe_Click);
            cmdLuuSotayBS.Click+=new EventHandler(cmdLuuSotayBS_Click);
            cmdUpdaeChiDan.Click += new EventHandler(cmdUpdateChiDan_Click);
           
            cmdPrintPres.Click+=new EventHandler(cmdPrintPres_Click);
            
            cmdAddDetail.Click+=new EventHandler(cmdAddDetail_Click);
            
            cmdCauHinh.Click += new System.EventHandler(this.cmdCauHinh_Click);
            
            cboStock.SelectedIndexChanged+=new EventHandler(cboStock_SelectedIndexChanged);
            txtdrug._OnGridSelectionChanged += new UCs.AutoCompleteTextbox_Thuoc.OnGridSelectionChanged(txtdrug__OnGridSelectionChanged);
            cboPrintPreview.SelectedIndexChanged+=new EventHandler(cboPrintPreview_SelectedIndexChanged);
            cboA4.SelectedIndexChanged+=new EventHandler(cboA4_SelectedIndexChanged);
            cboLaserPrinters.SelectedIndexChanged+=new EventHandler(cboLaserPrinters_SelectedIndexChanged);

            chkHienthithuoctheonhom.CheckedChanged += new EventHandler(chkHienthithuoctheonhom_CheckedChanged);
            chkAskbeforeDeletedrug.CheckedChanged += new EventHandler(chkAskbeforeDeletedrug_CheckedChanged);

            txtMaBenhChinh.KeyDown += new KeyEventHandler(txtMaBenhChinh_KeyDown);
            txtMaBenhChinh.TextChanged += new EventHandler(txtMaBenhChinh_TextChanged);

            txtMaBenhphu.GotFocus += new EventHandler(txtMaBenhphu_GotFocus);
            txtMaBenhphu.KeyDown += new KeyEventHandler(txtMaBenhphu_KeyDown);
            txtMaBenhphu.TextChanged += new EventHandler(txtMaBenhphu_TextChanged);

            mnuThuoctutuc.Click += new EventHandler(mnuThuoctutuc_Click);
            txtCachDung._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtCachDung__OnShowData);
            txtCachDung._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtCachDung__OnSaveAs);
            txtdrug._OnChangedView += new UCs.AutoCompleteTextbox_Thuoc.OnChangedView(txtdrug__OnChangedView);
                
        }

        void txtdrug__OnChangedView(bool gridview)
        {
            PropertyLib._AppProperties.GridView = gridview;
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }

      

        void txtCachDung__OnSaveAs()
        {
            
        }

        void txtCachDung__OnShowData()
        {
            
        }

        void mnuThuoctutuc_Click(object sender, EventArgs e)
        {

            Updatethuoctutuc();
        }
        void Updatethuoctutuc()
        {
            try
            {
                if (!Utility.isValidGrid(grdPresDetail)) return;
                int IdChitietdonthuoc = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdChitietdonthuoc));
                decimal don_gia = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.DonGia));
                int IdThuoc = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdThuoc));
                foreach (DataRow dr in m_dtDonthuocChitiet.Rows)
                {
                    if (Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.DonGia], -1) == don_gia
                        && Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], -1) == IdThuoc)
                    {
                        dr[KcbDonthuocChitiet.Columns.TuTuc] = 1;
                       
                    }
                }
                foreach (DataRow dr in m_dtDonthuocChitiet_View.Rows)
                {
                    if (Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1) == IdChitietdonthuoc)
                    {
                        dr[KcbDonthuocChitiet.Columns.TuTuc] = 1;
                        break;
                    }
                }
            }
            catch
            {
            }
        }
        #region ICD

        public string _MabenhChinh
        {
            get { return txtMaBenhChinh.Text; }
            set { txtMaBenhChinh.Text = value; }
        }
        public string _Chandoan
        {
            get { return txtChanDoan.Text; }
            set { txtChanDoan._Text = value; }
        }
        private void txtMaBenhphu_GotFocus(object sender, EventArgs e)
        {
            txtMaBenhphu_TextChanged(txtMaBenhphu, e);
        }
        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (hasMorethanOne)
                    {
                        DSACH_ICD(txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        AddBenhphu();
                        txtMaBenhphu.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        void AddBenhphu()
        {
            try
            {
                if (txtMaBenhphu.Text.TrimStart().TrimEnd() == "" || txtTenBenhPhu.Text.TrimStart().TrimEnd() == "")
                    return;
                //int record = dt_ICD.Select(string.Format(DmucBenh.Columns.MaBenh+ " ='{0}'", txtMaBenhphu.Text)).GetLength(0);
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == txtMaBenhphu.Text
                                                         select benh;


                if (!query.Any())
                {
                    AddMaBenh(txtMaBenhphu.Text, TEN_BENHPHU);
                    txtMaBenhphu.ResetText();
                    txtTenBenhPhu.ResetText();
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                    Selected = false;
                }
                else
                {
                    txtMaBenhphu.ResetText();
                    txtTenBenhPhu.ResetText();
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
            }
            finally
            {
            }
        }
        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            //DataRow[] arrDr = dt_ICD_PHU.Select(string.Format("MA_ICD='{0}'", MaBenh));
            EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                     where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh
                                                     select benh;
            if (!query.Any())
            {
                DataRow drv = dt_ICD_PHU.NewRow();
                drv[DmucBenh.Columns.MaBenh] = MaBenh;
                EnumerableRowCollection<string> query1 = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             MaBenh
                                                         select Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]);
                if (query1.Any())
                {
                    drv[DmucBenh.Columns.TenBenh] = Utility.sDbnull(query1.FirstOrDefault());
                }

                dt_ICD_PHU.Rows.Add(drv);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }
        private void txtTenBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBenhChinh.TextLength <= 0)
                {
                    txtMaBenhChinh.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }
        private void txtTenBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //    if (InValiMaBenh(txtMaBenhphu.Text))
            //        cmdAddMaBenhPhu.PerformClick();
            //    else
            //    {
            //        DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
            //    }
            //}
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
                    txtTenBenhPhu.Focus();
                }
                else
                    txtTenBenhPhu.Focus();
            }
        }
        private void txtTenBenhPhu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTenBenhPhu.TextLength <= 0)
                {
                    Selected = false;
                    txtMaBenhphu.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }
        private void txtTenBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!InValiMaBenh(txtMaBenhChinh.Text))
                {
                    DSACH_ICD(txtTenBenhChinh, DmucChung.Columns.Ten, 0);
                    txtMaBenhphu.Focus();
                    //hasMorethanOne = false;
                }
                else
                    txtMaBenhphu.Focus();
            }
        }
        private bool InValiMaBenh(string mabenh)
        {
            EnumerableRowCollection<DataRow> query = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                     where
                                                         Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                         Utility.sDbnull(mabenh)
                                                     select benh;
            if (query.Any()) return true;
            else return false;
        }
        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                hasMorethanOne = true;
                DataRow[] arrDr;
                if (isLike)
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                               Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                               "%'");
                else
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                               Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                               "'");
                if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                {
                    if (arrDr.GetLength(0) == 1)
                    {
                        hasMorethanOne = false;
                        txtMaBenhChinh.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                        txtTenBenhChinh.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                    }
                    else
                    {
                        txtTenBenhChinh.Text = "";
                    }
                }
                else
                {
                    txtTenBenhChinh.Text = "";
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc mã bệnh phụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
            
            hasMorethanOne = true;
            DataRow[] arrDr;
            if (isLike)
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                           Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                           "%'");
            else
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                           Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                           "'");
            if (!string.IsNullOrEmpty(txtMaBenhphu.Text))
            {
                if (arrDr.GetLength(0) == 1)
                {
                    hasMorethanOne = false;
                    txtMaBenhphu.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                    txtTenBenhPhu.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                    TEN_BENHPHU = txtTenBenhPhu.Text;
                }
                else
                {
                    txtTenBenhPhu.Text = "";
                    TEN_BENHPHU = "";
                }
            }
            else
            {
              

                txtMaBenhphu.Text = "";
                TEN_BENHPHU = "";
                
            }
        }
        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                    hasMorethanOne = false;
                  
                }
               
            }
        }
        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                Selected = false;
                string sFillter = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    sFillter = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text +
                               "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    sFillter = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] dataRows;
                dataRows = dt_ICD.Select(sFillter);
                if (dataRows.Length == 1)
                {
                    if (CP == 0)
                    {
                        txtMaBenhChinh.Text = "";
                        txtMaBenhChinh.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                        txtMaBenhChinh.Focus();
                    }
                    else if (CP == 1)
                    {
                        txtMaBenhphu.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                        txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                        Selected = false;
                    }
                }
                else if (dataRows.Length > 1)
                {
                    var frmDanhSachIcd = new frm_DanhSach_ICD(CP);
                    frmDanhSachIcd.dt_ICD = dataRows.CopyToDataTable();
                    frmDanhSachIcd.ShowDialog();
                    if (!frmDanhSachIcd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = frmDanhSachIcd.lstSelectedRows;
                        if (CP == 0)
                        {
                            isLike = false;
                            txtMaBenhChinh.Text = "";
                            txtMaBenhChinh.Text =
                                Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            hasMorethanOne = false;
                            txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                            txtMaBenhChinh_KeyDown(txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                            Selected = false;
                        }
                        else if (CP == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                isLike = false;
                                txtMaBenhphu.Text = "";
                                txtMaBenhphu.Text =
                                    Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                hasMorethanOne = false;
                                txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                Selected = false;
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    isLike = false;
                                    txtMaBenhphu.Text = "";
                                    txtMaBenhphu.Text =
                                        Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    hasMorethanOne = false;
                                    txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                    txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                    Selected = false;
                                }
                                hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                isLike = true;
            }
        }
        #endregion
        void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyButton();
        }

        
       
        void chkAskbeforeDeletedrug_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc = chkAskbeforeDeletedrug.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties); 
        }

       

        void chkHienthithuoctheonhom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._ThamKhamProperties.Hienthinhomthuoc = chkHienthithuoctheonhom.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                grdPresDetail.RootTable.Groups.Clear();
                if (chkHienthithuoctheonhom.Checked)
                {
                    GridEXColumn gridExColumn = grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    gridExGroup.GroupPrefix = "Loại thuốc: ";
                    // if(grdList.RootTable.Groups)
                    grdPresDetail.RootTable.Groups.Add(gridExGroup);
                }
            }
            catch
            {
            }
        }

        void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void LoadLaserPrinters()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    //khoi tao may in
                    String pkInstalledPrinters;
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(pkInstalledPrinters);
                    }
                }
                catch
                {
                }
                finally
                {
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
            }
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }
        void txtdrug__OnGridSelectionChanged(string ID,int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            txtDrugID.Text = ID;
            txtPrice.Text = Dongia;
            txtSurcharge.Text = phuthu;
        }

        void txtCachDung_TextChanged(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

        void txtCachDung__OnSelectionChanged()
        {
            ChiDanThuoc();
        }
        bool hasChanged = false;
        void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
           
        }
        void grdPresDetail_CellEdited(object sender, ColumnActionEventArgs e)
        {
            CreateViewTable();
        }
        Dictionary<long, string> lstChangeData = new Dictionary<long, string>();
        void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KcbDonthuocChitiet.Columns.TuTuc)
                {

                    return;
                }
                if (e.Column.Key == KcbDonthuocChitiet.Columns.SoLuong)
                {
                    GridEXRow _row = grdPresDetail.CurrentRow;
                    int id_thuoc= Utility.Int32Dbnull(_row.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,0);
                    int id_thuockho = Utility.Int32Dbnull(_row.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, 0);
                    decimal don_gia = Utility.DecimaltoDbnull(_row.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                    hasChanged = true;
                    int oldQuantity =Utility.Int32Dbnull( e.InitialValue,0);
                    int newQuantity = Utility.Int32Dbnull(e.Value,0);
                    int chenhlech = newQuantity-oldQuantity ;
                    if (newQuantity == oldQuantity)
                        return;
                    else if (newQuantity > oldQuantity)
                    {
                        AddQuantity(id_thuoc,id_thuockho, newQuantity - oldQuantity);
                    }
                    else
                    {
                        //Lấy tất cả các thuốc có chung ID_THUOC và DON_GIA
                        var p = (from q in m_dtDonthuocChitiet.Select("1=1").AsEnumerable()
                                 where Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdThuoc], 0) == id_thuoc
                                 && Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.DonGia], 0) == don_gia
                                 orderby q[KcbDonthuocChitiet.Columns.SttIn] descending
                                 select q).ToArray<DataRow>();
                        int v_intRemain = oldQuantity - newQuantity;
                        Dictionary<int, int> lstIdChitietDonthuoc = new Dictionary<int, int>();
                        List<int> lstDeleteId = new List<int>();
                        int lastdetail = -1;
                        string s = "";
                        for (int i = 0; i <= p.Length - 1; i++)
                        {
                            if (v_intRemain > 0)
                            {
                                int v_intCurrentQuantity = Utility.Int32Dbnull(p[i][KcbDonthuocChitiet.Columns.SoLuong], 0);
                                if (v_intCurrentQuantity >= v_intRemain)
                                {
                                    p[i][KcbDonthuocChitiet.Columns.SoLuong] = v_intCurrentQuantity - v_intRemain;
                                    v_intRemain = v_intCurrentQuantity - v_intRemain;
                                    lstIdChitietDonthuoc.Add(Utility.Int32Dbnull(p[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]), v_intRemain);
                                    lastdetail = Utility.Int32Dbnull(p[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]);
                                    if (v_intRemain <= 0) s += Utility.sDbnull(p[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc], "-1") + ",";
                                    break;
                                }
                                else
                                {
                                    p[i][KcbDonthuocChitiet.Columns.SoLuong] = 0;
                                    s += Utility.sDbnull(p[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc], "-1") + ",";
                                    lstDeleteId.Add(Utility.Int32Dbnull(p[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                                    v_intRemain -= v_intCurrentQuantity;
                                }
                            }
                        }
                        //Xóa các thuốc có số lượng=0 và cập nhật thuốc còn lại
           �QUUũ�������Z-eee�\�"�����Rc�����y[(����o����jƎ�و#^n�"�(�`@k�gh(���h-�̀���ߗ�)^�V��
�R��~$2H�����x=��K$rB��9��� +'Y�y���CF���EF�i�b��� �HÍBQ6�&�!�J['23N
J%��ܢ��ґ���%��uīT�߇-�F�h@A��3�����v��:�����u��YHr�f�EEE�g@j��<e�3!��i�z�˕�|�es^Ns^Fs��4�ٜ�����|���\��9���)|�g#%%��L��h7���2���/u<�+�B�]��oulv���}-�,���,j��5��vh��7'��1�b$�J0����,n�O �h|�˱=�$��*���"g(��U�G��M��hm�,��Ҳ)��|�wH�PP��CB4^�W��Kိ����������-��Z�4j@zz�M�PH*��7|����ԟ�!��P�BAaukJ��J��S�͓#W*#
ij�ԠR(ɧr�GHy�cdМg�����Kal�����V�D��|��W�`�vq�!P��̘G0#�̋�7j@FF>-<��
d�H6|����-\�huzJfz���hѢEii9JJ˨��A�)�dV��b
T�,��QTT�R�k�\��L� �����@�tdgK�C�(Z�K%>�Q�="ʃpO{w��Dµ�+;�
r����̋��qZ��{󊮾U�Ӳ}�&�v�OIͤf�h�!�Nh��_�	>m" ���p���S~��f`�b�7���F,%%��9�IYo���K� W���8ǈ�+���r?x��I�N�hV�R�d�-+wT�rX{��0���ܱ֖�:����ooS���	C��DFb*���nD7`/�]>�v�׬@��0�֏���j�!� <��Ƣ1��_h
�#�F�6=Ƞt��g�.�H�{���K\pU{��+�}�����r/n�&�X�/b��)� �
Hv�a���~%�pI�͹��B��!�ᩱ��a4ɀ�LV*맠 ���kC%��:�:��uu�=��%�u�=ez���q�WR���=��<�ʨ\l��ɕ���">AD��,x�,��jl�[����n�y�N��n�g��!��U �� �RS?T������'e�r��e8U���/@Pd>�Z\>2)Hr�{�OdS���u^J-���3��9��u�5:��Y<�%�d�gDi4����G�3Lؐ�	8T��u;8gu�I���n#����0��$�RU����P�/�0���Ψ1��
�GT����(��S��v��k��њz6
�:6��s��=��qh�II)FD�(����҉8X�A�-&6��&�6�n�����a���D"8\*%�� SU���J���,����
Vcđ"X��B�s[!�sJ��yU���Ĥd��tx�[Զ�Q>	�H���[G��I�j�����0��2�j����\��$�Q��C�dE�����'
�Hh��4ڻex��n����w�b�킔�x��*uc�K2�:V���Ʉ�qBj}��I2���7#��H30��}����8�_��5��~��\M|���$�<�+x ��2dUc�%
l��p:I�`��G�Y"���EO2�m�	����6���3�|Ca�F�A�^<	
E��.Ŷ��إ���z'7�4�J��8jX�#x�g��0�m@M����@ ���<�CR��a@hfB��\���2*0?D���M��=��g�� �`7`"v��~�	^n��$�߰~�%أ���a4ˀ:u^&CUR��� ��8�l@H�?�p��E�!��J��.�S2d�T�D"A\�c��M��a�Ũ�3�p��q�P�xU���0�4^Y+��ǖ�
����1����̾u�������
P#��LF��h`쵕ae��@�+�J�J�q{�����6�E4�l�8�1�DDќ��J�}*���FBoDP�H�p�y={*ɀ�9$x!��6�������h��Po�G��E ��7�0�8����$=N	�0��$~-��w��ĦPe��ϙ�,��ț��M�R(���;����j|�f�@�c-��j.����Uv�=�&�`{�:����]��J06@�x=�$d3�q�`ϗ_/�G�U�����
n@���|�z@�xT�p��'C�dD�8	
�I�/��|���d�NPNb(2�� �ؼ�gbo�,쫞S��𩶧���u3N�&L��t)ԴY�VQ�J8E��*�UFF��i�@2��cu8��/Q�C�܅��}���"�%|3#�:@��tT,����yg�&[���_ f>�G3��nL�:��P��;�iU(�g�����硝	{���������F>5� ���R�x
�g�13 �(t�)��Gհ!��u���'V�}��K����aEQ����c�s�
tz � �pi�;��-��X��{�`F�mg$������<X`����&�T�a���������|����]2`��(
v���#J�=я���P-���(�pM^��*��~�0 �6�c,x
�Џ�Xo��p��&
�	��C|�%�8Z�� Kl�1��8	>��
Ԟ����g�� �J�$l\p~��c���EF0v��/	�b��R|������<��ρp_`�K�97��6�2E�ō@�Z�,���L2EA����\:OCȗ�lK�Ux2�D9|7��C%��j��Џia�x�_�*��se�|O��d��H��7���z,�a��R�5\��J�i���7 8��t.�ί&#�I�Jc>�k����p�8��c)��b:��-����}j���c)|�ʑ66����a�r��0`�=6��{�XtC��A���X	�_�լ�d�CoGs�g<��3���`,N-N��Υ�� �·�d2f�j�� ����O&�<�S�{K�EG�>*�S.j���kɄu$x=��tϞ;Q؜�ip��<^��Ȁ��7��t�9a��g~3�C��#@9@y�:bb��9!�l��`>�T;�%�
0H2�H���:��E�����K:��1��XM��W�
L��Ǆ�t�6�h [ϳE
3@7�F~�e|�i$x*pp���;�>�ƀ�Q����ZL����R�%S )))��l/JήٽMKg���	q"$�r�_{�\،�z��+I�*�-��̨�۽J,�U��.8#ΔaY`-g�x{�����H���'�� o���3�vRI�L=�)	2�W/�W��xM�
xڏuF�tͼ��S�`w݀�w*��w%ጕĊ���5`�5#l���l}����Ά�3q�������qԷQ���o`��
�t15�3Zl@BBBPC��&^��Pk[��$~��J,�S��L4���џw��t͌�y-7��g�8Dݝ� �W���I0�y ��cO��p�K*�Z߀'^c�ْւZ�Y$p	��
K�VaiDӽCx��ks���ŚK��g��:8�֒ص}�5}��MMп ��t�e�n@s�3���n�H�H���p�S
'��~�]��&]�s�+��c���(pn`%U����n$�*���P:�7����Ν;'[p����IpԨQ]�����O�y��73�����.��=A��=����a�b�E��/��q1��<6�����+[8����ό*�n�M��$�ƍK"�T�b�-u��gKZ�}*.vI�C8G=�Rb#ڈK-��i��L<[�rl^f��dJG���������
0���f��7߼IG���@r4�8GgmC���E��+t��*݇��
kk���>|����5>��ˉ���C8��03�~�C�
���+��/�im)���dJǼ��=���{y�^��L�o��l=��=z�h�5b��D_l�_|1sȐ!�
�8p��p�ׯ�r++�Ut}M߾}7XXXl�:��bW2 7����$�e&�c�DdJ�]g����c��{��	��_D�����}��찲m۶�ڴi���^j�tL��6$�	|�O�>��ٳ�[ݻw�J��;w��S�N�;v��Q��o�~p�v�|��W�}�WF~|��fB��
!U�'`�9R%�8�?��_ve��A哺��e�&Rb�6�uym}����SB{�� vT����M�@�/�G���m�W����D'�3ѕx�C/E�&�+"�[���b'�+�V�O���ϼ�C����賣�Q�]ڻ�N�N�C�u��q� EûbG�����wd�k�-��e�G©Q<��y	�R:Jo����0���ş	�G̘6̜7�g�R�L�H�c��Z�����j�򐗼�oF?��;�N/��=wP�������Z�F�- �U������)7,z/}j8\�@�y*��.��}\��
�4c�)f��ǟ��\��]���~*qm>��f��)�ooɿ��K�Ǧ/ό�Zv�,��bhw�ʺ���1rĻb�p#����u9t�[������<7;�Ás�>��(e�xiƐzqN�L</�c�}l_ǟ+�9��/�j�W����o����3�x��3�9�́���ߊ;� s�j    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �&  �PNG

   
IHDR   �   �   �>a�   sRGB ���   gAMA  ���a  &;IDATx^�}xW��ox��}k?��g�}����{�u\�����D4���F$���@�P�9�QF9�r�92�`׫j�玆�ni$K�}���=}Ω�O��S�~ Y�ҕ���xX&�2~1ǲ6<L<��AGx ���ğ�Dq�����k�x��,o=�Q�D������޵C��w�߳<XpR!T�*���ҹ�ڡ�O`w�����u?T�����h	���޾Q�G�ꙟ�慯A7�[��%�����⏌U`	��k��7�[t���q�ťxR�V�V0L�f�G�:N���!0�	� M�d�]�i
������`	���??"
T��FY�?���g���d3�����l�m�k7�����>o�᧟����G��l�GX�����x���`��i�^�1��o][r��"��%@h��#T�ǈ1i�kgl�sI'��^�Y
��O5V^/�{�.L\g00���ͿJO��h	0��K+^3�+8��1E���[���*���O����i�큪�r�P��y!��$PP�#�
}�C�o�'T|���g}
F� (�	D�p�z���I���Y���K�E1�� 4ޗI*�z�C���$��d?�CS.����7��!�.x��ے�o��GX@TNТ%�����%�o��������@o��=1�o�bL| �v��%�dG�
7��� ����,^Q ��Y�7�> 	���1������Fݽ��u�x�丌x$ڃM�1Dd->И?���9
���;w��]T:a��(�p��eH��-��BNCX��X^$����mhJ��t�$�u�	ȪJ���>qCH��]u�� nq�ː!�Xk8d�(s'V�E�����^�s���Y�c�92p�>�z��-�
�U���$�=� F��q6�����߾}.�%�C��e(��]�9��iCP�� ���3|�r��dQ8��7���i �OOOCbaPÖ�X��`�����,������$OX�+�vV��[7ŕ�P��a��2x��:��xk��ۂ' -�����zPܘ��L2��P�TV�g�!@�B��S|�\�r�5}*�
�@�|q����.֦�}����#�@l�-���냾�q�IrY�8$���D���UŘ�[�n���ט��"Hoe`���G�;���%��Zٛ�`, ���<��A�R�����q�4@{s����)�r=���	 d��{�V'�������n޼�`pl �t���B�sH��{��=F�����#x�ڋez��
�^� \J�^6��o��p@����ŽP�Z�S���aR���$/�6�c��|(�=<1Ƞ��	�J	���hH*���V�30Þŷ��v����t �c�/H�)��������nܸ����}�y�w�KL���r��m�fH��� U��P�\	�B2��-��F~��%�
ʾ���8�p�n1�b�^�q*�pb	����q�ax|�_�� 3{��x�4��8��i���P��5��dU$���zp��f@u��eݧ�u���k��X��TW��=�!�@�G�9���$K �}L�TõkW�������a����&Z�s.��,�M����04u�Cmk%�6@JQ,C�'p��������%']58f�\��P��H�X<�V	�-H�kRp	���!�����B�oܺ�����Q������i
c�T/�>�t�̴��#�w�S�9�l�b'+��}� 	p���9_@Cg���իW�
��<_��f.=>�:��?>9�cC���0>>CCC�_i�23p(�ǽ���� fc�ʣ�=�?�[�ċ=��ԡ���\��(������Sny�c��
��Z:�w�\�r����ׇ�������===A���:ö������_DGG���P�3�����;���|�+����`d�5]��[8������.�O����_��j���FEE�#�������!+�� �)z���p�6
���q6DW������z1�#v�C�Q�U� q%nPԔ���P�U���Za���C�b(��$�qA����Z�:8��K���SČShv`����N:��
�4���1}L¶�"�$!4���	�O���p�u-�9��>MϯqKٕŠ��Vǁ��9@�kᢏv�𙘜�K�.Asgl?�������BVe�B��&����k��ϖw��{�a��\�16��,�/wuuAXX؞��%�N�p�ǯ��(<���|Bq�E�k�	�l����n^h�k�%��
o�$� D��߃t^ϗ���'r��Y��p��jOH�����Ijpf�u���p}���}�_jii//��P�A��$8	�=�:��;��B�`LMM1H��^jv���N8h�/�9�4�K��]ڭ��KC|���6RAE��)_�|�&�ܼ-��}�-0z��!���QAuT��
�������B�7	�w(���x���:���F08����p�qGT<O��j���~��r	`�U`x��i��C&?�%F)�'rd5���Ӝ�ʲ0��Rk���Ŗ��2��
bKs��
E��^VV���(���_���;N�c	�#:;� �Q�P���y�'�^p�K[R`�ٻW �,.�ϧ�K�Ph��>ʲ2l�	Uni�K�\/((�_|��fH@��("�M� AU�ahd���"X�A�O���cyUT���l1	�X "�v��pKZ����Gj�f��^�Tخ�7�fi�	�8P��|�!++�*�ğ��xlf(�{y@<Te�#C�%��[��oy����WE�o���,�[�|	���m��V�:��I&�_��4E�c�����>��r�	@���ILL�D���x�4����A� /V����1z�jp�n+Z�oy����BCm�
����h��L�O
(�HA?����O��� |,U4<<��M9����xr�!�t\�p`�;�TC�ü��uc��W06���AJ3�4�w��
nK�P��@Q������]1C ��A��G�`phFGG��M �$EuTx����̙@v�� �h @���;��?o�T8�9|��Κw�\*W�))Pګ��斍�~����"~˛ "[����4�o����"�<F����@���������`��k��fü���C����pS�|�"J988DJ�/�&���a8b�A{��(�Sڒ&� �h� ����I@!^�͛�'��_p�|BR�B�|�b[ZZzsX ~> Y���~��3}�#��� -��
q��%>��}�o�ɬd�"@�D�5pJ���Х�-^eD3�|*��\�����SSS��X��E6���ǬR!�.�� �x��@a3�|D��a�q6�֨���| ~�F
��R�7n_�S^��),S����������� C)'��� �
�����A�=������9(~�v�*)�ցj8ⲖI�%�G^�cn[�*��#UP������E�|�����x�zcc#:t��}&���MI��:�����E�(���q�;��-� ��^��c�)%��,t��)���WTF���4^������۷�Fe�)��l�3Ars�@�
k���ZX@0�>�K��q�Q�h�@e�|��8�U�2�\�{� ��Ћ�^�M��1+�B�K��Sv=�y��G�T1���W���|����U���bX�v�3`'��qz�?��[CwO7�C�8>`�s̂��b
����9X9�ĉ꭬�ܳt��f=��y 
�	$�� "����=��2��i
Ǔ�n)�~|�yc�
�e�j�$��H����2DI�r�E�6\������\x��'�Ce�T0�MSZxn0��#�#p.L�v��x�ܹ7
E�2c��`���9�rUJݔ�>�g�]��[ ��w�3rH�`�>e���ܞ	� /!���NPJX�z �@�������ŝ�fA'�&R��^x�)
;?\B3+rA�ỏS��� D�5G߆�ˣ��|ۧ�����P"�f_C��x� ��YAgW'��s��z��[챗s!�&w�ޙ�G�!��|q���}��n�}S�\C ��ö��U����A|?00�A��3��a�, ���p��`�	��8cN�����tL�p���/���sш7�'���r������:�Ux�w��*%WW�2	P:�6��o-�l�N���I朘�<�s�ӂA��
W��;#��
&���_f^>1��.��\�~3f�*�|A��F�R+h�h���nv�F�gީ�2��<.ȸ�x�B�2T��9x枙<rN���׉��|,�K��l��7n]TWE���@S׆��LEeJ_�6D�D�g@���Y �RKhkoZ_Np�4�D3�CE�BrY(6���p�5�)�Q��]�)���&̗ /l�Z
�of��)t<�֝뾁�vq[��- ���<_�����C+�ʛ�:8��d�c`��Vs&��L '��t�2e���E�8�_��Rgp�6TC�&��H�ӡ?Ϊ�G�D��0Ȭ����(�Ȁ��"܍tIn�ZE���sь)+�қ)��5�)��58�	$+>2e���lkT4�Z��� ��E��*��(��ֶV���d�c���Bs\d�����\W��!�zp�2P::|.��e۟��/�}��ܺ�#)�ފ�'��Qz�X9e7F+,����\'N�П! ��< �b^?��ɳ �/@<�]rZZ[�2K�8K�`�=eb_Cf��nMAVC8��dT�T��h�̙@6�(�| � �ny6|�Y'Ｓ�7�*�ml�s�Tֶ��T^u��*���� ��J	"@s��������������!��y�Kݺ}E&F/�@p�8\�W9�"�`���M�# ��'�L��_P�=�`"��vI�?��_e�u��GqI!lٲE:Dy Z���m$	�ha9�@�f[����`����oN���M"��� �2 I�ΙF`���T��lL��w=�q'���.��+��P^���Q{���y
�e�꒛�
��Χ�g��� �0� ���LSs�|�CT�'���@L�X����k`׹��j�!�Ԝq=�f��+��H�	��r,�G��m�� ؤ�P���d�"@V`���L9DP"��6�e�	���v��1}<d���*��< %�h{�D�ƦFhmme�l��`���Z�,W��[���0�K��ǚ�����2 �:��ϷkT�,�D���W���|@$0
ҝS;���ઋ2�O!&����M͙ ZN{a��۰�/�v� �ߜxh��ݣ�8��CaK:$W�Bh��L��w>u��28��I:*���|d:�J��b� �	�	����}��6�����^��f����e����=`!������`���0�g	iO�@�M�V	���x���۫M� ��D���������݌������Y0
�_9���5Ԉ��CX�8G[�9�'�/a��j&���3��Tޑ�	Z*�C�	��PG�����z	�$�Y I�ma{�V��f�'��
<����S��9��V����_�~��F�nWvC"�ƝR	<�mx�ct��� \� &�� ��P�PMMM�!�Z��E��g![���ǃ��8�*��8VS%,p�%߾�6���+�'�b��͋ ���>���G�g���}��%�|8��D���o{g�9s�-��=��)��� ����s���V\��>�����n���p��RGti 8_�B�
�Qxd������#��FmaA���ŉ �p��-�u5���������E�#1LC����>N�:/�rlG�(�̺x(hN�f�
�Ø^����I<��Y�]y�)�F02p�:���@?� ����DTUG���b6�� �8�'p���>s�H�S��r!�,�"Np�6�\2�!���+#!�!�{K�����_õ��hꯀ��<��q����-��")���QX�{��Q��7�$@���VC}}=
��p��ܩ�V���P<뷇Nhu0O0WX$�]��z�)�!	j{��)�>��Z��}�08�ʫ�9y�iӦ�H vG�"�"��!�k� O�f��
���x`��b4t�2�n��U/�����[ʔ�[�2����5{�`�i�ۯ��i���[oQ"H憐y@�ev�Nz�@�g=�2��/w�DS9�
=�t��q��P6�<��hNqk�=�I���1�f�0C������q�Q��O�;��
!�A��@�|S���:q�@8�4}�1��܍�tvp"�.4C�+9�I�ez�8.�*�����/HE@fm<��0ӎ�D^�����9 vG����xG�0 E�S����[���1��<�إvNx�F�1��3�\�M�ͣJ.�,���sr�*O򚟟���w�� �,@e�255�ݶ���G���A&z��Sm���u2��R��ep�If�Cͨ��J-���q��ܶ�幁k��{@JUd�%@mw!tU���<��嚓�c�$7�(� '<ׂN��Դ�`�Zebp�Q%ʰ�7�,���X%�'8�Yq�K20�ї[�Y�i�N6g�F�)o�fH�%C���`mm�&�$7�<��'�x[ ׋& �����j�����Z�
������D�1/���O���v�,�|U�I���s�,wx�e�d׋��$S��r���A�pn�����k8˖�}�`=�Rg,�� q`��q߁�3|zA�*f7V��)8��
e��W�Q%*)��W�Yn������3C
��ʚ�G+�����Ha"H*D�PUUŀ���1�o���30<��	�\g��W�p��q^%e�"hL�*��0�� Dzy�f��5����t"H|2̝ ;� �O� �SH/����6q��<U�u���Lc��|�Sў�YnN}�J���7�y�f����hC���40��&� y&P^Qt�A����C?�ʦL�j��n<�z>����^�)�2{�UE T���
βc˂U�^V��i�eK꠨4~��	ܷ!D�<��tVsV��5S%�O��,����t��S��z6r���]eeS���y��s1v��I� Y'��A�� �<c(+/�H�@�s;c,cw*�P�7%��^
�8�FS�����vr�¢�pY��ڸ9?WqLb
嶗f=c>�[p��j����.��g�}�04od�W���1����9n�٦X 	Z��(4�zfX��ʔ@s �`Fee�<��x�;!)
V�X�7E� ��5�Ҳ����5B���Z�ǫB|X+�z��a������ ���LU�B"���h�i�� �ᚼe�>���H&����;�.�h���Rs%��������]r��Y3U�Kϥh��,���D�d�DМ-�I�o�3�@G?�p�<���tpY[c=Bq�g�z̧�fc���.U:�D "6_�zx��H%�f�&�8�AIi1���3�e�	l�z�w��V�A�G�")����,ɴSd�h�B�>��x�������W>���6q$�f�x[ i0D�ߊ/���pg2k=ȩd�G���a�+#TJ J�� :��(���/4/���1>���CQ�*-@� ��̺��) ���d0�C�;^D��DqY�U�Bck���>��5�ӌ�7�t1��d�V&��A���!H*�-�#���2��חL�zIԜ� ��z�V���/�ܼ�����C�!19
�
Ӡ�*�2|*�T���c|J�TE2��-X��%E� ����
�ԩSn3��+�'{2جA����em Y8��~������ ~��v�U���]��9n�w�]��`�G���Q�H��E� ���7w,�CZ�d=��d��o�c="�� d��=j�'$����5���lr}��;��
���:���#�`��_�}���b��*�v�o��x33; 6���	H���PY�-���<�d�ݓ������K�5��h=%oh�^S�#�8�/�����=	/�~^�%/�~����ٱհMc#��
�.�H�)�9����[ī',5ų���I�W�p%��s�x9��|�7N=��
G�������JO|~�)�m�}ڿ����wY�|���g>{<��O��ӆ���g?2շ�Vn�ᓦ��7w�i�w���w/)�r�4B{��zP�LN�����oG%K�vߎ �> ���!m&�S��1iО3�n�shV!� �#>G|��
�5b���O�VĶPe��|�ʕ+��n��9�

�\w���Ƞ[)i����m*�ξ�%a9b���W^��;H�	�KNI:[Nt� y�����Ѵ#����OzC��V���D"mT�=�D��o#ޙ�����D&�l�3�z�)��?p��ɓ'�

Ҽ��z"���S/�B���b���A�PU� ���o�A2� |	 ���~�1����֛A�tYz=-E�#�ɤ9�5�Q�V�Л��(�n��B�+�*D��4,qȬyX��g�}v-��w����iii��{��wGǆ�Nˈc���Z�5���~��A�Y�XMr�*O�2�I�曩0:��,H@ǒ<���1hL�wؓ� r�[,�FH���ׇB��~�����Hss�L��1qa��3���r��-8r8�8�t
�42O��'�/H�-�h��2B��]�d)�!�S.ih!�!MfdYn�嘓��(�]_7nܸ�ȡ��dq�<����sFv�e2���(䰲��"E�� D.� Y
"
%�jN�r��d9hH���� ���OC�<r(mH!r����~����x<K����|"G|b$PxV\�Ő�T5�皘��J�gma;�' bȲ䄲C�49��!r�����O��,9�� ��P���on���̐#��ƪ��׽�ȑ��%�9�-:U�@v`�chh�&�V`[e��*��X yց9��d�Nb4���!E�r�#�oа�:�D�}`�O���X�G��rDڞ�.Egn .1�/&c�-�!�"4��K!�r�%���X48�PD�ErF�ae��`#�i�p)=R�a����
g��9tuu��΋����9�)LҍO䠙R��&��s���&�)>n�Y��X
�(;��7RYPa,�c��՛w��q�ȁ[��m�*���Gy r|ɒ�����8k? K���U�s�
@^�Ch�� �Xl;�i���ޗP� i^��|�X[��q<�0����4�FՏ|�e(����!+�e�
e������u����)�%ǖ&ب�S}d���@I��yK�Ʋ�
)�@�撥�P�f\)*��L���L �@��$����ا�@�9XP��z=y���Y�S�i��	�����		c����a�fM�/!�O�>)��L���k��P���(2�ʹH*�S��@�>�$���yWtN� �2���f��w�����bk�r}�G�eH�M{�j� ���	Yj=�����̉%%>�nM    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      *  �PNG

   
IHDR         �w=�   gAMA  ����    cHRM  z%  ��  �%  ��  m_  �l  <�  X��x   	pHYs    ?@"�  �IDATHK��k,�gǟ1;��5n�m�A�v�h��U�P���KZ,�A*�R�d�%���>PA>,A\>�l$�KPuO[N�yb&31#M�$��z�y��y���󲪪*F����FFF�ull�mll���y����~cc�g茼���eff���z��cbbXxx�R��� ��vw~~?���ذ���wS���.//���
�nnn��(((�zbbb�GTb555����'�"��JQjj����������`{{���ෞ����k PY___���Xp�okkk���X�嵵�U*�0�@@�����/U���M����%������'���(,���Z`vvv
��� //���� 0�"�A[[�����㡸�JJJd R����C�uqq�^^^�->::���l����W�7���p���0H$���7<�!L$�k�����"�Ѯ�b1`m��utt���BCC�6X]]���[�~rr***�=�T�4-��;#�[ZZ�������AOO������,,,����k�����������j���8 t�
e�P�455�*++7��jnn������ʁ����H�b#huu5�~���OQ�ͨ��o#���!$$�����	���U3Ɓ���..�p�|CG;;;Sx�
�����'&&A���`jjJ����I]]��
5�4�ttt���(ϭ�����R��z{{ann��ˡ���������A\\���X__�!���z��<�Z[[���p<+�xxx����8�����Cccc!**��BBBWV@@����
 CCCOl�뺺:hkkj.y�����)E\Q>>>��� ��Ӳ���������[�e'�����K����<�vqq��'''�Bc�aqq��mww��}	��p�/����Oѽ�`�P.�Q������)����mPZd���6:����.`ee�� ';�󔔔���|������:���<f(�vL�W�<��---�<���#մ�������Iaa�kkk�(�ߕ�ħ���� �.�024�����A'��8^QQ�p�]���Uዦ �}��s���R���_633�C ��=�n����nnn��h������{ A�prr�����MZZگ�gk0% �
@��O K��!�����CJy�1i��B5�~    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR         �w=�   gAMA  ����    cHRM  z%  ��  �%  ��  m_  �l  <�  X��x   	pHYs    ?@"�  	IDATHK�Uk(�i>{��H�qI5
�%������=�!���k�㒻�J���dL�r
I��a�8�a���ke����fV�}{�w��Y�g��
�7�Y��N:@�%���濷�����Z ��g	���LT���d������D'���t�Gpp���&���X}X3���V�*��~/**BPPP�.bunnn>� /}���������<<<�{oo��$%����	xddt<<<``` CCC-���	������czzZ'G^^���ptt###������666Z����7�CXX��������Ơ���P(���F`` |}}A�r+--�L��q~~��������,���q�T������@4��222�HD_s�%��������ʂ��1���@OO������CLL������TTTT`||;;;����?��������C���30�������NO:������Aww7VWW����R���S&�@SRR&%���R���؉���"22���3))	��՘�����.Hb���������[S�J5�H��ˍ�L����gp�Q__G������ATVV"??4p�����N888��	���1::���35���#����L���`ffu{{;���A�qtt�:;;S�?�(Q�~���qyy�$www�.`cc���HCC���PUUE�28	���
����ֆ:���N�Ɲ����%բ�i5ФhMM
<==akk���&�&ONNbee��u-�@H���������ő�M�p����quu���gGJ"�.--18���etHT$t}B& j$�@����iii�*�CL3D�)��(�[������%��5�6���<O/I�JI2�HU�ꇆ��899Uk�@c��DcOM_XX���
NNNx�� ���}��Jw*ཽ=H�������y|���UG�����H�d�������Xz���iH9��/�pd�o�T������B1_�녏��O!6��L��"}�5�3�'����QΈx�g]f��f�}��ك�    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      u  �PNG

   
IHDR         �w=�   gAMA  �ܲ�  ,IDATHK�TKLSYv��,f�bB�j4�`�Dv.�L:ѕ
F�0N		c����P��@	�Z(��"/a�RA�(����T(΅Rz[��O[]��wn��{����'�<&�P��4�3����,6Z[=:��G��^8j/��h�1Z����|�͘%��(��fڇ��u�����QYi	&f�����o��6��>���[�ð�ð�i�G���^�|�5���8�"MM�֪�9�;}!�&�>O=���u��ĺ�ض0�\�J�w�d��QE�ZKRC��Q-�ѻ�h x�� �w裳�
Vw�����a�B�͈*P[ۮ+�}�?��� �C����!y�c�ΆH̾ĳ�X�B~~�S��[��C������|�0�
���8�;	,�)��!�,1 +���cE��PO��_������<��?�0����E"�%����oi�n���m�O�
T�t����h�G�f��y�G�� ^��J�t�V�_%��{�ւ�7�(*�y�'�B]��ΏǞ �� �)�_�e�6n�&a� �_��4��)����5��hY
�y�2O��s_8�L�y��w"��L�kӃ����E��}xA��D#
V�F &z�Ĉ?,��e��=e
^�V߃k��eG`R�fH�6�V6H=���	��e9g�������(�E�������)<���Z�tL��~x��h�Dƺe������ʅ�P�
l�~s����Q��1j$�酧n�P8�-0L.�)���L��(�nCj�=N�i����ӌيD̗|�Mǋ���;';y�,%%%Ϟ�k!�{n���=.]�qJ$9�Iz�TZ���VU��<��\�[���bg���D"�IN�%'g2|sԻ��	N�lx����M	�,qC��!r\ȶ�x�D�Y.�R�%�M��8v5]B���Ŏb&�&��"��"��	�jc�/~�8��_E2C���8=�E%b�O��ə�*��L|�� CF�G���l�Sc�ֻg8��
��6X�P�ȟ�P��˱���3�9�<�**��]���`;�/q�\N���Ei�7��(�fv�cv    IEND�B`��p<GridEXLayoutData><RootTable><GroupTotals>Always</GroupTotals><Columns Collection="true" ElementName="Column"><Column0 ID="STT"><Caption>stt</Caption><DataMember>STT</DataMember><Key>STT</Key><Position>0</Position><Width>41</Width></Column0><Column1 ID="ma_luotkham"><Caption>ma_luotkham</Caption><DataMember>ma_luotkham</DataMember><Key>ma_luotkham</Key><Position>1</Position></Column1><Column2 ID="ten_benhnhan"><Caption>hoten</Caption><DataMember>ten_benhnhan</DataMember><Key>ten_benhnhan</Key><Position>2</Position><Width>137</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column2><Column3 ID="nam_sinh"><Caption>namsinh</Caption><DataMember>nam_sinh</DataMember><Key>nam_sinh</Key><Position>3</Position><Width>59</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column3><Column4 ID="id_gioitinh"><Caption>gioitinh</Caption><DataMember>id_gioitinh</DataMember><Key>id_gioitinh</Key><Position>4</Position><Visible>False</Visible><Width>33</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column4><Column5 ID="BHDT"><Caption>DTTT</Caption><DataMember>BHDT</DataMember><Key>BHDT</Key><Position>5</Position></Column5><Column6 ID="gioi_tinh"><Caption>GT</Caption><DataMember>gioi_tinh</DataMember><Key>gioi_tinh</Key><Position>6</Position><Width>35</Width></Column6><Column7 ID="mathe_bhyt"><Caption>mathe</Caption><DataMember>mathe_bhyt</DataMember><Key>mathe_bhyt</Key><Position>7</Position><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column7><Column8 ID="KCB"><Caption>ma_dkbd</Caption><DataMember>KCB</DataMember><Key>KCB</Key><Position>8</Position><Width>66</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column8><Column9 ID="mabenh_chinh"><Caption>mabenh</Caption><DataMember>mabenh_chinh</DataMember><Key>mabenh_chinh</Key><Position>9</Position><Width>58</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column9><Column10 ID="ngay_tiepdon"><Caption>Ngày Vào</Caption><DataMember>ngay_tiepdon</DataMember><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngay_tiepdon</Key><Position>10</Position><Width>72</Width><FormatMode>UseStringFormat</FormatMode><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column10><Column11 ID="ngay_ketthuc"><Caption>Ngày Ra</Caption><DataMember>ngay_ketthuc</DataMember><DefaultGroupFormatMode>UseStringFormat</DefaultGroupFormatMode><DefaultGroupFormatString>{0:dd/MM/yyyy}</DefaultGroupFormatString><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngay_ketthuc</Key><Position>11</Position><Width>61</Width><FormatMode>UseStringFormat</FormatMode></Column11><Column12 ID="songay_dieutri"><Caption>ngaydtr</Caption><DataMember>songay_dieutri</DataMember><Key>songay_dieutri</Key><Position>12</Position><Width>66</Width></Column12><Column13 ID="TONGCONG"><AggregateFunction>Sum</AggregateFunction><Caption>t_tongchi</Caption><DataMember>TONGCONG</DataMember><FormatString>{0:N2}</FormatString><Key>TONGCONG</Key><NullText>0</NullText><Position>13</Position><Width>75</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><DefaultValue>0</DefaultValue><DefaultValueType>System.Int32</DefaultValueType><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column13><Column14 ID="XN"><AggregateFunction>Sum</AggregateFunction><Caption>t_xn</Caption><DataMember>XN</DataMember><FormatString>{0:N2}</FormatString><Key>XN</Key><NullText>0</NullText><Position>14</Position><Width>68</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column14><Column15 ID="CDHA"><AggregateFunction>Sum</AggregateFunction><Caption>t_cdha</Caption><DataMember>CDHA</DataMember><FormatString>{0:N2}</FormatString><Key>CDHA</Key><NullText>0</NullText><Position>15</Position><Width>60</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column15><Column16 ID="THUOC"><AggregateFunction>Sum</AggregateFunction><Caption>t_thuoc</Caption><DataMember>THUOC</DataMember><FormatString>{0:N2}</FormatString><Key>THUOC</Key><NullText>0</NullText><Position>16</Position><Width>54</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column16><Column17 ID="MAU"><AggregateFunction>Sum</AggregateFunction><Caption>t_mau</Caption><DataMember>MAU</DataMember><FormatString>{0:N2}</FormatString><Key>MAU</Key><NullText>0</NullText><Position>17</Position><Width>43</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column17><Column18 ID="PTTT"><AggregateFunction>Sum</AggregateFunction><Caption>t_pttt</Caption><DataMember>PTTT</DataMember><FormatString>{0:N2}</FormatString><Key>PTTT</Key><NullText>0</NullText><Position>18</Position><Width>66</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column18><Column19 ID="VTYT"><AggregateFunction>Sum</AggregateFunction><Caption>t_vtytth</Caption><DataMember>VTYT</DataMember><FormatString>{0:N2}</FormatString><Key>VTYT</Key><NullText>0</NullText><Position>19</Position><Width>42</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column19><Column20 ID="VTYT_TT"><AggregateFunction>Sum</AggregateFunction><Caption>t_vtyttt</Caption><DataMember>VTYT_TT</DataMember><EditType>NoEdit</EditType><Key>VTYT_TT</Key><NullText>0</NullText><Position>20</Position><Width>44</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString></Column20><Column21 ID="THUOC_K"><AggregateFunction>Sum</AggregateFunction><Caption>t_ktg</Caption><DataMember>THUOC_K</DataMember><FormatString>{0:N2}</FormatString><Key>THUOC_K</Key><NullText>0</NullText><Position>21</Position><Width>42</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column21><Column22 ID="TienCong"><AggregateFunction>Sum</AggregateFunction><Caption>t_kham</Caption><DataMember>TienCong</DataMember><FormatString>{0:N2}</FormatString><Key>TienCong</Key><NullText>0</NullText><Position>22</Position><Width>79</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column22><Column23 ID="DVKT"><AggregateFunction>Sum</AggregateFunction><Caption>DVKT cao</Caption><DataMember>DVKT</DataMember><FormatString>{0:N2}</FormatString><Key>DVKT</Key><NullText>0</NullText><Position>23</Position><Visible>False</Visible><Width>65</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column23><Column24 ID="VanChuyen"><AggregateFunction>Sum</AggregateFunction><Caption>t_vchuyen</Caption><DataMember>VanChuyen</DataMember><FormatString>{0:N2}</FormatString><Key>VanChuyen</Key><NullText>0</NullText><Position>24</Position><Width>65</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column24><Column25 ID="BNCT"><AggregateFunction>Sum</AggregateFunction><Caption>t_bnct</Caption><DataMember>BNCT</DataMember><FormatString>{0:N2}</FormatString><Key>BNCT</Key><NullText>0</NullText><Position>25</Position><Width>103</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column25><Column26 ID="BHCT"><AggregateFunction>Sum</AggregateFunction><Caption>t_bhtt</Caption><DataMember>BHCT</DataMember><FormatString>{0:N2}</FormatString><Key>BHCT</Key><NullText>0</NullText><Position>26</Position><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column26><Column27 ID="QDS"><AggregateFunction>Sum</AggregateFunction><Caption>t_ngoaids</Caption><DataMember>QDS</DataMember><FormatString>{0:N2}</FormatString><Key>QDS</Key><NullText>0</NullText><Position>27</Position><Width>83</Width><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column27><Column28 ID="LyDoXuatToan"><Caption>lydo_vv</Caption><DataMember>LyDoXuatToan</DataMember><Key>LyDoXuatToan</Key><Position>28</Position><Width>85</Width></Column28><Column29 ID="ten_kcbbd"><Caption>noikcb</Caption><DataMember>ten_kcbbd</DataMember><Key>ten_kcbbd</Key><Position>29</Position><Width>60</Width></Column29><Column30 ID="KHOA"><Caption>khoa</Caption><DataMember>KHOA</DataMember><Key>KHOA</Key><Position>30</Position></Column30><Column31 ID="NamQT"><Caption>nam_qt</Caption><DataMember>NamQT</DataMember><Key>NamQT</Key><Position>31</Position><Width>53</Width></Column31><Column32 ID="ThangQT"><Caption>thang_qt</Caption><DataMember>ThangQT</DataMember><Key>ThangQT</Key><Position>32</Position><Width>65</Width></Column32><Column33 ID="id_benhnhan"><Caption>id_benhnhan</Caption><DataMember>id_benhnhan</DataMember><Key>id_benhnhan</Key><Position>33</Position><Visible>False</Visible></Column33><Column34 ID="ngaybatdau_bhyt"><Caption>gt_tu</Caption><DataMember>ngaybatdau_bhyt</DataMember><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngaybatdau_bhyt</Key><Position>34</Position><Width>57</Width><FormatMode>UseStringFormat</FormatMode></Column34><Column35 ID="ngayketthuc_bhyt"><Caption>gt_den</Caption><DataMember>ngayketthuc_bhyt</DataMember><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngayketthuc_bhyt</Key><Position>35</Position><Width>59</Width><FormatMode>UseStringFormat</FormatMode></Column35><Column36 ID="dia_chi"><Caption>diachi</Caption><DataMember>dia_chi</DataMember><Key>dia_chi</Key><Position>36</Position><Width>49</Width></Column36><Column37 ID="Tuyen"><Caption>tuyen</Caption><DataMember>Tuyen</DataMember><Key>Tuyen</Key><Position>37</Position></Column37><Column38 ID="TUYEN_XA"><Caption>tuyen_xa</Caption><DataMember>TUYEN_XA</DataMember><Key>TUYEN_XA</Key><Position>38</Position></Column38><Column39 ID="ma_noicap_bhyt"><Caption>matinh</Caption><DataMember>ma_noicap_bhyt</DataMember><Key>ma_noicap_bhyt</Key><Position>39</Position></Column39><Column40 ID="MADTCU"><Caption>madtcu</Caption><DataMember>MADTCU</DataMember><Key>MADTCU</Key><Position>40</Position></Column40><Column41 ID="QUY_QT"><Caption>quy_qt</Caption><DataMember>QUY_QT</DataMember><Key>QUY_QT</Key><Position>41</Position></Column41><Column42 ID="GiamDinh"><Caption>Giám định</Caption><DataMember>GiamDinh</DataMember><Key>GiamDinh</Key><Position>42</Position><Visible>False</Visible><Width>73</Width></Column42><Column43 ID="TienXuatToan"><Caption>Tiền Xuất Toán</Caption><DataMember>TienXuatToan</DataMember><Key>TienXuatToan</Key><Position>43</Position><Visible>False</Visible><Width>93</Width></Column43><Column44 ID="TVuotTran"><Caption>T-Vượt Trần</Caption><DataMember>TVuotTran</DataMember><Key>TVuotTran</Key><Position>44</Position><Visible>False</Visible></Column44><Column45 ID="LoaiKCB"><Caption>Loại KCB</Caption><DataMember>LoaiKCB</DataMember><Key>LoaiKCB</Key><Position>45</Position><Visible>False</Visible></Column45><Column46 ID="NoiTT"><Caption>Nơi Thanh toán</Caption><DataMember>NoiTT</DataMember><Key>NoiTT</Key><Position>46</Position><Visible>False</Visible></Column46><Column47 ID="BenhKhac"><Caption>Bệnh khác</Caption><DataMember>BenhKhac</DataMember><Key>BenhKhac</Key><Position>47</Position><Visible>False</Visible><Width>67</Width></Column47><Column48 ID="DoiTuong"><Caption>Đối Tượng</Caption><DataMember>DoiTuong</DataMember><Key>DoiTuong</Key><Position>48</Position><Visible>False</Visible></Column48><Column49 ID="ten_nhombhyt"><Caption>Nhóm</Caption><DataMember>ten_nhombhyt</DataMember><Key>ten_nhombhyt</Key><Position>49</Position><Visible>False</Visible></Column49><Column50 ID="TdaTuyen"><Caption>T - Đa Tuyến</Caption><DataMember>TdaTuyen</DataMember><Key>TdaTuyen</Key><Position>50</Position><Visible>False</Visible><Width>75</Width></Column50></Columns><SortKeys Collection="true" ElementName="SortKey"><SortKey0 ID="SortKey0"><ColIndex>11</ColIndex></SortKey0></SortKeys><ColumnHeaders>True</ColumnHeaders><ColumnSetHeaders>True</ColumnSetHeaders><ColumnSetRowCount>1</ColumnSetRowCount><GroupCondition /><GroupMode>Collapsed</GroupMode><SelfReferencingSettings ID="SelfReferencingSettings"><AutoSizeColumnOnExpand>True</AutoSizeColumnOnExpand><ExpandColumn>48</ExpandColumn></SelfReferencingSettings></RootTable></GridEXLayoutData>�d<GridEXLayoutData><RootTable><GroupTotals>Always</GroupTotals><Columns Collection="true" ElementName="Column"><Column0 ID="ma_luotkham"><Caption>Mã lượt khám</Caption><DataMember>ma_luotkham</DataMember><Key>ma_luotkham</Key><Position>0</Position></Column0><Column1 ID="ten_benhnhan"><Caption>Họ và tên</Caption><DataMember>ten_benhnhan</DataMember><Key>ten_benhnhan</Key><Position>1</Position><Width>134</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column1><Column2 ID="nam_sinh"><Caption>Năm sinh</Caption><DataMember>nam_sinh</DataMember><Key>nam_sinh</Key><Position>2</Position><Width>72</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column2><Column3 ID="gioi_tinh"><Caption>GT</Caption><DataMember>gioi_tinh</DataMember><Key>gioi_tinh</Key><Position>3</Position><Visible>False</Visible><Width>33</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column3><Column4 ID="id_gioitinh"><Caption>GT</Caption><DataMember>id_gioitinh</DataMember><Key>id_gioitinh</Key><Position>4</Position><Visible>False</Visible><Width>35</Width></Column4><Column5 ID="BHDT"><Caption>DTTT</Caption><DataMember>BHDT</DataMember><Key>BHDT</Key><Position>5</Position><Width>102</Width></Column5><Column6 ID="ma_thebhyt"><Caption>Mã thẻ</Caption><DataMember>ma_thebhyt</DataMember><Key>ma_thebhyt</Key><Position>6</Position><Width>116</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column6><Column7 ID="KCB"><Caption>ĐKKCBBĐ</Caption><DataMember>KCB</DataMember><Key>KCB</Key><Position>7</Position><Width>84</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column7><Column8 ID="ngay_tiepdon"><Caption>Ngày khám</Caption><DataMember>ngay_tiepdon</DataMember><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngay_tiepdon</Key><Position>8</Position><Width>116</Width><FormatMode>UseStringFormat</FormatMode><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column8><Column9 ID="mabenh_chinh"><Caption>Mã bệnh</Caption><DataMember>mabenh_chinh</DataMember><Key>mabenh_chinh</Key><Position>9</Position><Width>68</Width><CellStyle><TextAlignment>Center</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column9><Column10 ID="XN"><AggregateFunction>Sum</AggregateFunction><Caption>Xét nghiệm</Caption><DataMember>XN</DataMember><FormatString>{0:#,#.##}</FormatString><Key>XN</Key><Position>10</Position><Width>99</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column10><Column11 ID="CDHA"><AggregateFunction>Sum</AggregateFunction><Caption>CDHA</Caption><DataMember>CDHA</DataMember><FormatString>{0:#,#.##}</FormatString><Key>CDHA</Key><Position>11</Position><Width>92</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column11><Column12 ID="THUOC"><AggregateFunction>Sum</AggregateFunction><Caption>Thuốc</Caption><DataMember>THUOC</DataMember><FormatString>{0:#,#.##}</FormatString><Key>THUOC</Key><Position>12</Position><Width>104</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column12><Column13 ID="MAU"><AggregateFunction>Sum</AggregateFunction><Caption>Máu</Caption><DataMember>MAU</DataMember><FormatString>{0:#,#.##}</FormatString><Key>MAU</Key><Position>13</Position><Width>86</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column13><Column14 ID="PTTT"><AggregateFunction>Sum</AggregateFunction><Caption>PT-TT</Caption><DataMember>PTTT</DataMember><FormatString>{0:#,#.##}</FormatString><Key>PTTT</Key><Position>14</Position><Width>79</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column14><Column15 ID="VTYT"><AggregateFunction>Sum</AggregateFunction><Caption>VTYT</Caption><DataMember>VTYT</DataMember><FormatString>{0:#,#.##}</FormatString><Key>VTYT</Key><Position>15</Position><Width>83</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column15><Column16 ID="VTYT_TT"><AggregateFunction>Sum</AggregateFunction><Caption>VTYT_thay thế</Caption><DataMember>VTYT_TT</DataMember><EditType>NoEdit</EditType><FormatString>{0:#,#.##}</FormatString><Key>VTYT_TT</Key><Position>16</Position><Width>111</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString></Column16><Column17 ID="DVKT"><AggregateFunction>Sum</AggregateFunction><Caption>DVKT cao</Caption><DataMember>DVKT</DataMember><FormatString>{0:#,#.##}</FormatString><Key>DVKT</Key><Position>17</Position><Width>65</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column17><Column18 ID="THUOC_K"><AggregateFunction>Sum</AggregateFunction><Caption>Thuốc K thải ghép</Caption><DataMember>THUOC_K</DataMember><FormatString>{0:#,#.##}</FormatString><Key>THUOC_K</Key><Position>18</Position><Width>124</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column18><Column19 ID="TienCong"><AggregateFunction>Sum</AggregateFunction><Caption>Tiền công</Caption><DataMember>TienCong</DataMember><FormatString>{0:#,#.##}</FormatString><Key>TienCong</Key><Position>19</Position><Width>70</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column19><Column20 ID="VanChuyen"><AggregateFunction>Sum</AggregateFunction><Caption>Vận chuyển</Caption><DataMember>VanChuyen</DataMember><FormatString>{0:#,#.##}</FormatString><Key>VanChuyen</Key><Position>20</Position><Width>88</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column20><Column21 ID="TONGCONG"><AggregateFunction>Sum</AggregateFunction><Caption>Tổng cộng</Caption><DataMember>TONGCONG</DataMember><FormatString>{0:#,#.##}</FormatString><Key>TONGCONG</Key><Position>21</Position><Width>85</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column21><Column22 ID="BNCT"><AggregateFunction>Sum</AggregateFunction><Caption>Bệnh nhân cùng chi trả</Caption><DataMember>BNCT</DataMember><FormatString>{0:#,#.##}</FormatString><Key>BNCT</Key><Position>22</Position><Width>103</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column22><Column23 ID="BHCT"><AggregateFunction>Sum</AggregateFunction><Caption>Bảo Hiểm TT</Caption><DataMember>BHCT</DataMember><FormatString>{0:#,#.##}</FormatString><Key>BHCT</Key><Position>23</Position><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column23><Column24 ID="QDS"><AggregateFunction>Sum</AggregateFunction><Caption>Ngoài QĐS</Caption><DataMember>QDS</DataMember><FormatString>{0:#,#.##}</FormatString><Key>QDS</Key><Position>24</Position><Width>83</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><TextAlignment>Far</TextAlignment></CellStyle><HeaderStyle><TextAlignment>Center</TextAlignment></HeaderStyle></Column24><Column25 ID="id_benhnhan"><Caption>id_benhnhan</Caption><DataMember>id_benhnhan</DataMember><Key>id_benhnhan</Key><Position>25</Position><Visible>False</Visible></Column25><Column26 ID="DoiTuong"><Caption>Đối Tượng</Caption><DataMember>DoiTuong</DataMember><DefaultGroupPrefix /><Key>DoiTuong</Key><Position>26</Position><Visible>False</Visible></Column26><Column27 ID="ten_kcbbd"><Caption>Nơi KCBBĐ</Caption><DataMember>ten_kcbbd</DataMember><Key>ten_kcbbd</Key><Position>27</Position><Width>60</Width></Column27><Column28 ID="NamQT"><Caption>Năm QT</Caption><DataMember>NamQT</DataMember><Key>NamQT</Key><Position>28</Position><Width>53</Width></Column28><Column29 ID="ThangQT"><Caption>Tháng QT</Caption><DataMember>ThangQT</DataMember><Key>ThangQT</Key><Position>29</Position><Width>65</Width></Column29><Column30 ID="ngaybatdau_bhyt"><Caption>Ngày bắt đầu BHYT</Caption><DataMember>ngaybatdau_bhyt</DataMember><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngaybatdau_bhyt</Key><Position>30</Position><Width>57</Width><FormatMode>UseStringFormat</FormatMode></Column30><Column31 ID="ngayketthuc_bhyt"><Caption>Ngày kết thúc BHYT</Caption><DataMember>ngayketthuc_bhyt</DataMember><FormatString>{0:dd/MM/yyyy}</FormatString><Key>ngayketthuc_bhyt</Key><Position>31</Position><Width>59</Width><FormatMode>UseStringFormat</FormatMode></Column31><Column32 ID="dia_chi"><Caption>Địa chỉ</Caption><DataMember>dia_chi</DataMember><Key>dia_chi</Key><Position>32</Position><Width>49</Width></Column32><Column33 ID="diachi_bhyt"><Caption>Địa chỉ BHYT</Caption><DataMember>diachi_bhyt</DataMember><EditType>NoEdit</EditType><Key>diachi_bhyt</Key><Position>33</Position></Column33><Column34 ID="ten_dung_tuyen"><Caption>Tuyến</Caption><DataMember>ten_dung_tuyen</DataMember><DefaultGroupPrefix /><Key>ten_dung_tuyen</Key><Position>34</Position><Visible>False</Visible><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0,00}</TotalFormatString></Column34></Columns><SortKeys Collection="true" ElementName="SortKey"><SortKey0 ID="SortKey0"><ColIndex>1</ColIndex></SortKey0></SortKeys><ColumnHeaders>True</ColumnHeaders><ColumnSetHeaders>True</ColumnSetHeaders><ColumnSetRowCount>1</ColumnSetRowCount><GroupCondition /><GroupMode>Collapsed</GroupMode><Groups Collection="true" ElementName="Group"><Group0 ID="Group0"><ColIndex>26</ColIndex></Group0><Group1 ID="Group1"><ColIndex>34</ColIndex></Group1></Groups><GroupHeaderTotals Collection="true" ElementName="GroupHeaderTotal"><GroupHeaderTotal0 ID="GroupHeaderTotal1"><ColIndex>26</ColIndex><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString><Key>GroupHeaderTotal1</Key></GroupHeaderTotal0></GroupHeaderTotals><SelfReferencingSettings ID="SelfReferencingSettings"><AutoSizeColumnOnExpand>True</AutoSizeColumnOnExpand><ExpandColumn>26</ExpandColumn></SelfReferencingSettings></RootTable></GridEXLayoutData>�Lưu ý: Điều kiện để dữ liệu vào báo cáo 79A là Bệnh nhân đã được in phôi BHYT. Do vậy, khi gặp các tình huống chưa thấy dữ liệu bệnh nhân hiển thị thì bạn cần kiểm tra lại xem đã in phôi cho Bệnh nhân đó hay chưa?       �J  ���   �   lSystem.Resources.ResourceReader, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#System.Resources.RuntimeResourceSet         hSystem.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aPADPADN���u��
�
fz����t��yN   �       /      �   j  *b a o c a O _ T I E U D E 1 . P i c I m g     c m d E x i t . I m a g e +  ,c m d E x p o r t T o E x c e l . I m a g e �  $c m d I n P h i e u X N . I m a g e N:  Jg r d L i s t _ D e s i g n T i m e L a y o u t . L a y o u t S t r i n g �=  "u i G r o u p B o x 2 . I m a g e uC  @    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR   @   @   �iq�   gAMA  �ܲ�  ?IDATx^�T�W�ǳ�l����&�5fM�k41э(!j�nLb���c`EѠ`��� 6;"X�\E�����C��0C�BQ�{ߛ� HN��9�s��ft��w߽��y<�ܳ�3�9@����
��������M�6��F��v���2
�Ӎ�2eJ��'�$�N�4����>~��Oi ;ZZZ2�~3f�������=z��p���QUUũ�������Z-eee�\�"�����Rc�����y[(����o����jƎ�و#^n�"�(�`@k�gh(���h-�̀���ߗ�)^�V��
�R��~$2H�����x=��K$rB��9��� +'Y�y���CF���EF�i�b��� �HÍBQ6�&�!�J['23N
J%��ܢ��ґ���%��uīT�߇-�F�h@A��3�����v��:�����u��YHr�f�EEE�g@j��<e�3!��i�z�˕�|�es^Ns^Fs��4�ٜ�����|���\��9���)|�g#%%��L��h7���2���/u<�+�B�]��oulv���}-�,���,j��5��vh��7'��1�b$�J0����,n�O �h|�˱=�$��*���"g(��U�G��M��hm�,��Ҳ)��|�wH�PP��CB4^�W��Kိ����������-��Z�4j@zz�M�PH*��7|����ԟ�!��P�BAaukJ��J��S�͓#W*#
ij�ԠR(ɧr�GHy�cdМg�����Kal�����V�D��|��W�`�vq�!P��̘G0#�̋�7j@FF>-<��
d�H6|����-\�huzJfz���hѢEii9JJ˨��A�)�dV��b
T�,��QTT�R�k�\��L� �����@�tdgK�C�(Z�K%>�Q�="ʃpO{w��Dµ�+;�
r����̋��qZ��{󊮾U�Ӳ}�&�v�OIͤf�h�!�Nh��_�	>m" ���p���S~��f`�b�7���F,%%��9�IYo���K� W���8ǈ�+���r?x��I�N�hV�R�d�-+wT�rX{��0���ܱ֖�:����ooS���	C��DFb*���nD7`/�]>�v�׬@��0�֏���j�!� <��Ƣ1��_h
�#�F�6=Ƞt��g�.�H�{���K\pU{��+�}�����r/n�&�X�/b��)� �
Hv�a���~%�pI�͹��B��!�ᩱ��a4ɀ�LV*맠 ���kC%��:�:��uu�=��%�u�=ez���q�WR���=��<�ʨ\l��ɕ���">AD��,x�,��jl�[����n�y�N��n�g��!��U �� �RS?T������'e�r��e8U���/@Pd>�Z\>2)Hr�{�OdS���u^J-���3��9��u�5:��Y<�%�d�gDi4����G�3Lؐ�	8T��u;8gu�I���n#����0��$�RU����P�/�0���Ψ1��
�GT����(��S��v��k��њz6
�:6��s��=��qh�II)FD�(����҉8X�A�-&6��&�6�n�����a���D"8\*%�� SU���J���,����
Vcđ"X��B�s[!�sJ��yU���Ĥd��tx�[Զ�Q>	�H���[G��I�j�����0��2�j����\��$�Q��C�dE�����'
�Hh��4ڻex��n����w�b�킔�x��*uc�K2�:V���Ʉ�qBj}��I2���7#��H30��}����8�_��5��~��\M|���$�<�+x ��2dUc�%
l��p:I�`��G�Y"���EO2�m�	����6���3�|Ca�F�A�^<	
E��.Ŷ��إ���z'7�4�J��8jX�#x�g��0�m@M����@ ���<�CR��a@hfB��\���2*0?D���M��=��g�� �`7`"v��~�	^n��$�߰~�%أ���a4ˀ:u^&CUR��� ��8�l@H�?�p��E�!��J��.�S2d�T�D"A\�c��M��a�Ũ�3�p��q�P�xU���0�4^Y+��ǖ�
����1����̾u�������
P#��LF��h`쵕ae��@�+�J�J�q{�����6�E4�l�8�1�DDќ��J�}*���FBoDP�H�p�y={*ɀ�9$x!��6�������h��Po�G��E ��7�0�8����$=N	�0��$~-��w��ĦPe��ϙ�,��ț��M�R(���;����j|�f�@�c-��j.����Uv�=�&�`{�:����]��J06@�x=�$d3�q�`ϗ_/�G�U�����
n@���|�z@�xT�p��'C�dD�8	
�I�/��|���d�NPNb(2�� �ؼ�gbo�,쫞S��𩶧���u3N�&L��t)ԴY�VQ�J8E��*�UFF��i�@2��cu8��/Q�C�܅��}���"�%|3#�:@��tT,����yg�&[���_ f>�G3��nL�:��P��;�iU(�g�����硝	{���������F>5� ���R�x
�g�13 �(t�)��Gհ!��u���'V�}��K����aEQ����c�s�
tz � �pi�;��-��X��{�`F�mg$������<X`����&�T�a���������|����]2`��(
v���#J�=я���P-���(�pM^��*��~�0 �6�c,x
�Џ�Xo��p��&
�	��C|�%�8Z�� Kl�1��8	>��
Ԟ����g�� �J�$l\p~��c���EF0v��/	�b��R|������<��ρp_`�K�97��6�2E�ō@�Z�,���L2EA����\:OCȗ�lK�Ux2�D9|7��C%��j��Џia�x�_�*��se�|O��d��H��7���z,�a��R�5\��J�i���7 8��t.�ί&#�I�Jc>�k����p�8��c)��b:��-����}j���c)|�ʑ66����a�r��0`�=6��{�XtC��A���X	�_�լ�d�CoGs�g<��3���`,N-N��Υ�� �·�d2f�j�� ����O&�<�S�{K�EG�>*�S.j���kɄu$x=��tϞ;Q؜�ip��<^��Ȁ��7��t�9a��g~3�C��#@9@y�:bb��9!�l��`>�T;�%�
0H2�H���:��E�����K:��1��XM��W�
L��Ǆ�t�6�h [ϳE
3@7�F~�e|�i$x*pp���;�>�ƀ�Q����ZL����R�%S )))��l/JήٽMKg���	q"$�r�_{�\،�z��+I�*�-��̨�۽J,�U��.8#ΔaY`-g�x{�����H���'�� o���3�vRI�L=�)	2�W/�W��xM�
xڏuF�tͼ��S�`w݀�w*��w%ጕĊ���5`�5#l���l}����Ά�3q�������qԷQ���o`��
�t15�3Zl@BBBPC��&^��Pk[��$~��J,�S��L4���џw��t͌�y-7��g�8Dݝ� �W���I0�y ��cO��p�K*�Z߀'^c�ْւZ�Y$p	��
K�VaiDӽCx��ks���ŚK��g��:8�֒ص}�5}��MMп ��t�e�n@s�3���n�H�H���p�S
'��~�]��&]�s�+��c���(pn`%U����n$�*���P:�7����Ν;'[p����IpԨQ]�����O�y��73�����.��=A��=����a�b�E��/��q1��<6�����+[8����ό*�n�M��$�ƍK"�T�b�-u��gKZ�}*.vI�C8G=�Rb#ڈK-��i��L<[�rl^f��dJG���������
0���f��7߼IG���@r4�8GgmC���E��+t��*݇��
kk���>|����5>��ˉ���C8��03�~�C�
���+��/�im)���dJǼ��=���{y�^��L�o��l=��=z�h�5b��D_l�_|1sȐ!�
�8p��p�ׯ�r++�Ut}M߾}7XXXl�:��bW2 7����$�e&�c�DdJ�]g����c��{��	��_D�����}��찲m۶�ڴi���^j�tL��6$�	|�O�>��ٳ�[ݻw�J��;w��S�N�;v��Q��o�~p�v�|��W�}�WF~|��fB��
!U�'`�9R%�8�?��_ve��A哺��e�&Rb�6�uym}����SB{�� vT����M�@�/�G���m�W����D'�3ѕx�C/E�&�+"�[���b'�+�V�O���ϼ�C����賣�Q�]ڻ�N�N�C�u��q� EûbG�����wd�k�-��e�G©Q<��y	�R:Jo����0���ş	�G̘6̜7�g�R�L�H�c��Z�����j�򐗼�oF?��;�N/��=wP�������Z�F�- �U������)7,z/}j8\�@�y*��.��}\��
�4c�)f��ǟ��\��]���~*qm>��f��)�ooɿ��K�Ǧ/ό�Zv�,��bhw�ʺ���1rĻb�p#����u9t�[������<7;�Ás�>��(e�xiƐzqN�L</�c�}l_ǟ+�9��/�j�W����o����3�x��3�9�́���ߊ;� s�j    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	        �PNG

   
IHDR         �w=�   gAMA  �ܲ�  �IDATHK�V[SRQ�����Nȃ��M/p�2Q��M�Q�)/��(X�#`Z���4i���w�2�Kc�X���������[�^k�⮹��-A��$K9s����PHK�;ޓdw'\�?�s9gJ���L׍�n5|��v;h�{P�Q����� ��T�N97�3c�x��\��O���S'�ϝ��~����x��Fd�L�Rq����+��̔(S[��-?�~�h ��'�MyA�{Rˬ6�Ҕ�������Ae�z�\�9 [<����'T?���|8�kR�![�Ldjs}Q3)���@p�������\��܂*�S��^a�G1+�L��	f�X��,�2k�|l�
<.
xͦQ�K!0��O6���
M+Pi���F�N"�{��$Ei
��E���z{lܯZ�-�����p̆_D�N�� ���!dAuN�x���B�Eѹ�Q/���]B�P����B�UL�5@�a�C�lZ2\�2������8�м�5(7�	j
�"�h?�H��V<���v@����[�Ac�%����zI�3���s̀:��	
$P������X&���BYϮ��Wރu(�qB�p�j�%����7D��6�"+Zt~�D�1�w�aza�7�þ}�Z$��nȩ�9g�`�ve��uB��	�0P�:����Z�!"�N{UO�!�b�_�tQ�)Z����\"�"5��,����2,���]�1�o4�h�gTO�
{~^IRջ)d@�Z����+хL��)�G4�s�
ZxgP\�Z�>�X�rC��%��m\B���aGL�4��_�$��-�b�:!����Q@i�qp��eǵ�&�Dy��˂�yn����Oʖ��	<�'�eB�<}T��UW4pe�����kɖ��#��0�$g�H�Ω��W�����됍��UK�����0λh*��֧��&��o�_�,t{ҦF
    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �   �PNG

   
IHDR   �   �   �>a�   sBIT|d�   sRGB ���   gAMA  ���a   	pHYs  
�  
�   tEXtSoftware www.inkscape.org��<   'IDATx^�]	�TՕu�`wF�t�;�h�ݝ��t�v4QEAA����IP���$�M"���$��" �L��D"�"T4 Ȭ�Bp�����{޿u�zCU����^U�_�~����{ξ�=SJv�98D�A �p�a�����!��9�8�i�xb�I��ǀ�|���z�@�;9 ���BH$ C>��>
����~v�=�y�'䂜��D"�- ��O������/_�|8�Z��\��ܓrAN�M"$ SU�?�C����M��-�ۇ�*����0��	�!GG�]
b	 oȂ��b������t :� ],���P�s �����rBn�Q�,�D ,6��0�|
8����}q�p�Y��fu*q��o��78��f�q]�:�z�ͪ�F��<�q���&u��Fu��UGg��Qi�"�Y75��G{�$��Au��^u��[��Ѡ���p�x��j0���*�
ω�{�󉉽U��u���&թ�ɵ�x�qgzN�U=�֨^��Q�ݙ���FU�
fԨjbf��	<rAN�
9"W�j�$`�ɢ��wЋ��<�^}gXou�+'|�׽�����{�o<�r
Q�=^����<����:��:uڈ:u��7�A��D��0�V�A��Ù��kU��ר��<�u��h<7ת��1:����ZW!X��֨s�	D�:�6]�WC�D�J���n�Z�ϻO�� �)���05=�U*��*U/��J���B�ҠjF������pAN�
9"WE �P� ��:F�M�ɑ���� [H��Ax�b��&פ���&��p��pM�Cx�t�ǪPU��^���*
��pAN�
9*� X���:���<w��u �@G�G8�\"=n��(�p!�f�t;�Ixg�����(O�E ��W��P���`� '�������xfJ"d_\_��{�}�����ݭ~0��)�}�<)��ݥF��]u�� L�$�"�'=��H�:�3�'&܋rI�v��(�6���j�%z��y�@#@.J# mz-GZ?�0Q.����4��\��#�^�u}���|��7?8яp�a��q���:�ل�u�&�Yǃ�p/�� ܐ.����4�zh�C�	@< :{l7���kb�N�5����(���(�NG�v:
6����-�[*|�zرo�&\�*ܲG�6]��mig'��M
�<��$�u ��}����Z M�rBn�Ql/ n* �fل��j@:�j�9�J}ҾM2`W�`ȧ �.�ԃ�Zl����*�BSzFZ�&��{�����(� ؇�~C�]
�}��Ӭ��^�wmg��lȬ_�G��W$�X�q�n�t��5|Ӟ-����M����֬HI���K�&�[�:M8��&��k̫P�@��
,� `�tY�qp���W\�F���Zt�:�պۋە�_�ըη c�j����H��F��^�g����tJ��nm�ɻO�\ãz���*S�g>i��k "�z�CEoP��z�K�}`�o���*E�[PSz�q;�w50a�����=�'M��F��Qu4�D8�\W�&\����[�ѓW�x�������v�ċ 2̗��G�/҇{)�T�.��V�@�B8�'�$�AËh��&<� ��c����a��j�Q���S��oR����i='�$�D��p�E���p��]�u5������%h͂ܶ ����Iv�I���Bx�Ą�t���kK��w���+�@6qvV��*7Q�y\�m���U��[�
k��#<H �v�����Ȅ���H�� `ࢺ��(�rM:�9�u�_Ý�O�&���dl������`�evJ�k��/:���n��B�t����B����B�A��K/ �u2�����)]�u��#�]<����wT���h�Ӗ��%H ҇�6_�mˇp��f�Z��~�:�_�\T�VigN�5\�Z�ш� �O��pC:����mٛ�۫v/P����A��[���[F�{�8�;*�]�I� ���xɢ��g cU��]���=`F��q�G��Yxy���dl�t{�Mj�fAȬ��� ��i�H�[���~9R���3��&�����[0xqc��A����m�->��K��:�E��p��f@�����Ӌ?�wKV�|!�V/.��j��Dx!	�xa=P�.1�tqS���;�l�Ƭ���Ԏ��!C 0p6��#�=0A$1_�ۗ- n��t��%�a�xh�R�%�p����`���k4�,i.`�I"��f
P�ޤ��q�8{ �z�J�;=�0V�f��Z�|���3͗ �m����b�d�/�I�EVJ#�#�Q�R?\Z��ގp�p!���
��F ���i<�pu�L�$ �1�w͌�Ez2����$�X�=4�-�S���F;�]�{L��v)pW�;�l��ؿ#k�)�|�!27Pv��#�h�@�~�始���^�K�݋�m,�X�en�k�%&��iݎp���t����/�[zp���K�ۄk���QA`�����V�ur'_��ć?tU�|?�-��?�t�Z̗b~��-�{��k��>�/�WzTb\Y"�#�#]�I7�J �w��|'2Sz�
�|�~~�(�2_�^�m��*=N�'!����.~���P=s���h��0�~��fU�\9����|�s�p�/���|
 �|i�@��|	"��t�Jϕ�C	gTcM����<����� �?�pE9���:ʧ{<u�?0lٕ�t=a��ݯ�*� �$��ן����m�Ӌ��'���I�l?.s���D#�5��[�	�?��_���l���g Φ3ʫfz�zv�0vՍ�*�!��OL(� �⑘�a��X�7P����N/�f�j�Q��F�����2�3���_Xz�ͩ�%J=������}�(5u�x��T�d����l������'XQO��$���I3��d�j���p�t�P�p;�#�'�#��kҗ]�q%����K/���p����r�9=���:3�5�&�0M�s'�
���o�7�D������m�"�U��pT�Q���̴F8ӺDy�$���}���Ae  �TK�
H�/ӫ�0�m��"��s���g>�v[�qs���-+��>�?��/D�U+.*� 1�^7�.S��2���#&Q�ۄ��`z�s��'ܳXs�/~�LxP�,�3Ӻ�.�D����+..� 8�^�u^��e���'�p�p���^��4�R�-Ң�mV���I8��&���/R����������(��}�|��g��?�Z�l����Kj�_���B���c?���U���k�i�&x�u����x�x�
�7l���zx��[o�������� ��z'�}W�����,>x�K~]�+�%K��>�����o�"�'>�AĿY@�ˉt[Xe'�}�k֬�x��#��gE}��/�H�&e% ��t�(޼y�ڹs�Nߒ��~�#S>"� QA|k����l �S �X����H�9��KHk��|`�o
�cz	��5�؄&�ںy?[�	 �ڷ*��j?*�M~k�ѹ�-k�5?��iӦ�C�ڌ $�_z�e�x���ۑO�7lؠ�~�i��3Ϩ?���j�ƍjӦM��g���-A�u�Vu�}Jݵ6=x<�/-�y����9�Ƕ	ة��|��?� ��S*-( �*-�y����9���r�\�u��`��J�����i��q����5���� |.���|i0w�RiAp�I~޴��|���[ʦ��V��~�R��� Xk�?o�cy\>ǋ �f&�O f�'W��F �6(� δ��M{,����6#�\6����F ˞Q*-(���z*5�yKu|��]���{֖�w�!�@�E[�r�̗� {�$�������+�޾���d.y�K��Rj�^��*�ch��z���(5e�G�[���g�zb�^s�5�{�z�J<�QQ��}��-K�>�;БF �	ȧr	 �E�
 .��(H�w} ;�9ɓ� ��K q��kD I���\ː�;���&�U?I�cZ�Oj� ?L q��%��E�x� d�� �:R	 %�� ��Q��V���J n�璟V �fX��/5@�%��Ȗ��ypd�����O�l$!_2@\򃈎*���F q��5A���Q\�E?�m���+{��> �Ș� �:�|=3@��g�3�m�e�$�XTĻ���>��#���?s߻l� ;�]���5m��3�gVO��%�8k��$%__���AD���������{]���?	�v�F��m�D IH�_�������
- 뒭4 � \���ܸ�Ep�,���\�h;�~ύ|���G Q��Ƨ�r���q��$٠M	 t�۾`!� Ґ�.I"?� ��|�-����s���Y���"_*�\ �I��8��� �F?k��؋��3�	D_T.����u�z`�r��~0��Yre�(�/��cf���ǉ��^�d�oۯ��)/�7�>�O<�	��x���"?��U�K��|d/a+ly
��e' V���+{�Y Q����S4������W>�n��u~3.�ڵk��=�{�~�2@��ϕ�/��~��pɋ���� �>����g �}�@�	�OxT
�������s�	�����������s���[ n����I�����Ad]�.����A���)�џq�.[ f�7_$���% J Q;r�����ٟэb!�r	�k���S�p�>�a�|�R��m�o��N�eY
�����r� �l\k�?��+��Z����H/�����4�h��e�
i6��K�q,sDk��~=ߧ�`<��K���	�P($M�v��|�#_>�K����B�Mj�}mK�)�����F ��Yd�[�W���Q��xn�aG � >|8�w�䇰�|~�a�>�����u,mH�q�����n��3�x~g��I��f�Ҙ�*���U��J~澖�S���Q���\֕F i�?� �z|i�/�kČ��|!^����/��/��s�m�w�q�# ���:�^�Uh�r֢:��w�<i��>(����uIR��u,��-Z����k��)���_D��X.�"_�|�rs�/Qo��x/i�%�&z/i��cݟۂ0h�R|�?��KS8C~p����2@� �{9>.�n�g��4.LmRy�^aM�ؠ��x��@H?8�g�3�^�����`.�j��x)?�K��
��nݺg@v%��/��Ok�.�6�L�����_�'����RD��vڗ�g�g��{K�K�H&�B���
ċ/�����_P��S���/���{���SD���q�w1�>8�7��� G�L �Ж`���� =�|qȗT/i\��I�G��c"x������5�L����ء���晏?��z衇>8��
�+�3M���y��Q���� >��\�N��� q��� q�}�~�̡ ��ψ'�bF�&\��NRI.�޺u�&��3�{��M'N�#����裏�U�V}ԭ[�k@r
��?�_��O����G)Tj�C$\| �@�M�Bt��]��f�|����>#��3�I:�x'�ᓽ�'�7���6�nbO>��⿰�"\0c��n��ߑ|D�4hЭ�7@#�����L�E�k�R�%�\S�q����c�S ����/)��;#�QN'��n�7��D#��h�rq\���իՃ>�x�F8^�	���:t��̶���) ?�~L�G%Y��l���K@�跗�(�N�m�ڛ9b�H�o|l͘�I>������=Z=��#:zI�ʕT��X�7�/^��y�.�W�^����m۶,�ǌ�����|�-��/v�og��/Y�����Kr�p`[�������"�N��|��?�פ_{�j�
�y�Խs�ٳ�U3f�TL�,���p� �\�sf���ɓ7u�Q}Ar�-_;���l�����+� ��_���쩋��Y�q�g�6v�X5r�H5|�p�#F�[n�E��QO�Hg�Z�#���ۏ>�� �����~�R�&�p5���] H i3@��;�+U�B�,Yr��D�����K���ߴ�Y��w� �Öf5��� ����+)�g]���{���l�:܅�����,�cm���Y,�>@[���K�Ǭ!k�m���s��Hܡ���8f�ܹ��C>��'�pA.�>Z�'4{���ˊ?����\ �����&W�+�e��n���^���o�n,������������t��⚷ڷo�K\
t��
��+�^��e	���� ր�;�.�� aK@!�F?�+@X��=��ݻ�a�)���gΜ��[ ��&����]����\�i�p��$�� {}�j�Z-�M��4lZ�Y��j ����~{�'h������G׎�=[<oj-�@�!����{3H�8�E��Fϗ���S�{E@�@�$��iueOP��מ����d���4n��-B�O��(�g�gg�@v}�}^NG��.����+.��9�xI~�^?��[��� ��s��K��B�g�� b͚��|;l�Q���W�ɴx���q��h��T�~�	 ` D�@{	(f?^��&�0kԄ	ԨQ�t�O/��SH�|��Y��3 �
F�ʕ+�< ��Z��� H��������#E�h�	 �z���Oq�������_Z?{��n����Ob���@*�9k��)���#�<��~7�=�߀X���y=Q�}�]��| ;�� ���ş�Q,�/�~��c��m�v�&�:��KQ��敁��ȷȧ�O�h����d�@��ѣl�� ,�- �2�����~" Y�iٲ��֏ѿs'5�����ߝ�?:C�;������aÆqgOҾD�W�3N���>��v����(`���.c��b��C�0�?	f�O#gժ��}��Ӗn�ǇC��j��|F�M~�o�" ��v�K��-���>\��?{�ݦ����a��� B>���z���>�}���A6���L��m]�}��V#�� �5��d{��D ��?��u��t�z9�
.�p��ѣ�oA4�]�<���@>��mU�J����įd W a�L�0�׮]���͛��܎�E���С�/@t�9>:|4y����b����P�. �P�^�S��}abG�?$��_L���7oޞv��
�����w ڻt�h���d�  �͞�.ͧpw�f za@
@;$�Qo�?k֬m�ݿ$�����]=����-���,x�t�x��|����c�9�?��'y8���|n�ʮ7vJF~Q2@�p ������q�ƭ�_3��p��nr������nAw��D{>A��Ӓ�g�¹4V��L���(��_q�d�/�c�^�n��ק���Ǹ
6ɓ��E`lD�i��[�@j�꟰6�N��~g����?�����G����s;�6x8�Yv�'Y��i�7	@���	�Q �
�V�A� |+�8������={����6��%��/�W��?� x%
�g��( ����dhk�0���.�x���Nb�Ǳmi���y���+m^ّ�F Lcla�_}�4p���^���?~ �Cws>���敼Ǐ�)ⶁ\X�r����$�,�;*�cL���K�&<?� ߍ�x�d�s����J_�<���ϊ�����$ S�*��5� 'V��p9�`g `!t��ߍߓv./��6.�ں��a�'c�%���#� �Z�r�8�H��E�qe�fQ�l
iv������������"�{fȂ\���B�&� �0�R�\��Y���(t�{���{��!�z�|z%eY�����r"n[r9��Y����y  ��g~����y�
����3�#~Q*�_��'x�w���yhs��|�U��������nS��
N+�C�E߭����{®_Kg;�    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR           szz�   gAMA  �ܲ�  wIDATXG����QƷ�#��;� ��"�/A4DAA�P�Oa!� "��#"�"� l+D�5"��|�3xw��Ό>��x~�w�^8o��⫠N��<�Nt>�5	����v;���t8>$�qk ����5�V+MC��B��)���M���%�f3M,�����n�R���&�c��X,���dB�^O�� �1��N�R�`0�v��I`�Ř���}�����1��]��3�o��"�{��*�����z�Q#�p84T��/@�բz�N�f�P�^\�F�A�R�.�@*
w�� �Z�r��]/.@�Z�L&s��P�T(�N�E����eJ&��H$<��(��F)�*x�����)S(2T� ��R  ��O���� ��������J7�n���j�E`�	6<Psp<��n�&X,�k(L�ၚ�J���r���$�f��6��4Tb����X`�
�\�x<N�C���d����|�I`��̇ ��R6�MF��	���_����e4Q���$0�@#s�'</�X}����u ��E�pņ</������C��:	,%����z�T�'���v������SQ�Bm�j�jC����FV���
    IEND�B`��<GridEXLayoutData><RootTable><Columns Collection="true" ElementName="Column"><Column0 ID="STT"><Caption>STT</Caption><DataMember>STT</DataMember><Key>STT</Key><Position>0</Position><Width>65</Width></Column0><Column1 ID="TEN_THUOC_full"><Caption>Tên thuốc</Caption><DataMember>TEN_THUOC_full</DataMember><Key>TEN_THUOC_full</Key><Position>1</Position><Width>269</Width></Column1><Column2 ID="ten_donvitinh"><Caption>ĐVT</Caption><DataMember>ten_donvitinh</DataMember><Key>ten_donvitinh</Key><Position>2</Position><Width>72</Width></Column2><Column3 ID="SO_LUONG"><AggregateFunction>Sum</AggregateFunction><Caption>Số lượng</Caption><DataMember>SO_LUONG</DataMember><Key>SO_LUONG</Key><Position>3</Position><Width>84</Width></Column3><Column4 ID="DON_GIA"><Caption>Đơn giá</Caption><DataMember>DON_GIA</DataMember><FormatString>{0:0,0}</FormatString><Key>DON_GIA</Key><Position>4</Position><TextAlignment>Far</TextAlignment><Width>131</Width><FormatMode>UseStringFormat</FormatMode></Column4><Column5 ID="THANH_TIEN"><AggregateFunction>Sum</AggregateFunction><Caption>Thành tiền</Caption><DataMember>THANH_TIEN</DataMember><FormatString>{0:0,0}</FormatString><Key>THANH_TIEN</Key><Position>5</Position><TextAlignment>Far</TextAlignment><Width>131</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:0,0}</TotalFormatString></Column5></Columns><GroupCondition /></RootTable></GridEXLayoutData>@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      u  �PNG

   
IHDR         �w=�   gAMA  �ܲ�  ,IDATHK�TKLSYv��,f�bB�j4�`�Dv.�L:ѕ
F�0N		c����P��@	�Z(��"/a�RA�(����T(΅Rz[��O[]��wn��{����'�<&�P��4�3����,6Z[=:��G��^8j/��h�1Z����|�͘%��(��fڇ��u�����QYi	&f�����o��6��>���[�ð�ð�i�G���^�|�5���8�"MM�֪�9�;}!�&�>O=���u��ĺ�ض0�\�J�w�d��QE�ZKRC��Q-�ѻ�h x�� �w裳�
Vw�����a�B�͈*P[ۮ+�}�?��� �C����!y�c�ΆH̾ĳ�X�B~~�S��[��C������|�0�
���8�;	,�)��!�,1 +���cE��PO��_������<��?�0����E"�%����oi�n���m�O�
T�t����h�G�f��y�G�� ^��J�t�V�_%��{�ւ�7�(*�y�'�B]��ΏǞ �� �)�_�e�6n�&a� �_��4��)����5��hY
�y�2O��s_8�L�y��w"��L�kӃ����E��}xA��D#
V�F &z�Ĉ?,��e��=e
^�V߃k��eG`R�fH�6�V6H=���	��e9g�������(�E�������)<���Z�tL��~x��h�Dƺe������ʅ�P�
l�~s����Q��1j$�酧n�P8�-0L.�)���L��(�nCj�=N�i����ӌيD̗|�Mǋ���;';y�,%%%Ϟ�k!�{n���=.]�qJ$9�Iz�TZ���VU��<��\�[���bg���D"�IN�%'g2|sԻ��	N�lx����M	�,qC��!r\ȶ�x�D�Y.�R�%�M��8v5]B���Ŏb&�&��"��"��	�jc�/~�8��_E2C���8=�E%b�O��ə�*��L|�� CF�G���l�Sc�ֻg8��
��6X�P�ȟ�P��˱���3�9�<�**��]���`;�/q�\N���Ei�7��(�fv�cv    IEND�B`�     �5  ���   �   lSystem.Resources.ResourceReader, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#System.Resources.RuntimeResourceSet         hSystem.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a�System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089N���u��
�
fz�F�%2CT<G7Erq   �       R   �   4  /   �  *b a o c a O _ T I E U D E 1 . P i c I m g     c m d B a o C a o . I m a g e +  c m d E x i t . I m a g e �  ,c m d E x p o r t T o E x c e l . I m a g e R   Jg r d L i s t _ D e s i g n T i m e L a y o u t . L a y o u t S t r i n g ,(  >t x t L y d o t h a n h l y . A u t o C o m p l e t e L i s t �0  2t x t t h u o c . A u t o C o m p l e t e L i s t Z1  @    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR   @   @   �iq�   gAMA  �ܲ�  ?IDATx^�T�W�ǳ�l����&�5fM�k41э(!j�nLb���c`EѠ`��� 6;"X�\E�����C��0C�BQ�{ߛ� HN��9�s��ft��w߽��y<�ܳ�3�9@����
��������M�6��F��v���2
�Ӎ�2eJ��'�$�N�4����>~��Oi ;ZZZ2�~3f�������=z��p���QUUũ�������Z-eee�\�"�����Rc�����y[(����o����jƎ�و#^n�"�(�`@k�gh(���h-�̀���ߗ�)^�V��
�R��~$2H�����x=��K$rB��9��� +'Y�y���CF���EF�i�b��� �HÍBQ6�&�!�J['23N
J%��ܢ��ґ���%��uīT�߇-�F�h@A��3�����v��:�����u��YHr�f�EEE�g@j��<e�3!��i�z�˕�|�es^Ns^Fs��4�ٜ�����|���\��9���)|�g#%%��L��h7���2���/u<�+�B�]��oulv���}-�,���,j��5��vh��7'��1�b$�J0����,n�O �h|�˱=�$��*���"g(��U�G��M��hm�,��Ҳ)��|�wH�PP��CB4^�W��Kိ����������-��Z�4j@zz�M�PH*��7|����ԟ�!��P�BAaukJ��J��S�͓#W*#
ij�ԠR(ɧr�GHy�cdМg�����Kal�����V�D��|��W�`�vq�!P��̘G0#�̋�7j@FF>-<��
d�H6|����-\�huzJfz���hѢEii9JJ˨��A�)�dV��b
T�,��QTT�R�k�\��L� �����@�tdgK�C�(Z�K%>�Q�="ʃpO{w��Dµ�+;�
r����̋��qZ��{󊮾U�Ӳ}�&�v�OIͤf�h�!�Nh��_�	>m" ���p���S~��f`�b�7���F,%%��9�IYo���K� W���8ǈ�+���r?x��I�N�hV�R�d�-+wT�rX{��0���ܱ֖�:����ooS���	C��DFb*���nD7`/�]>�v�׬@��0�֏���j�!� <��Ƣ1��_h
�#�F�6=Ƞt��g�.�H�{���K\pU{��+�}�����r/n�&�X�/b��)� �
Hv�a���~%�pI�͹��B��!�ᩱ��a4ɀ�LV*맠 ���kC%��:�:��uu�=��%�u�=ez���q�WR���=��<�ʨ\l��ɕ���">AD��,x�,��jl�[����n�y�N��n�g��!��U �� �RS?T������'e�r��e8U���/@Pd>�Z\>2)Hr�{�OdS���u^J-���3��9��u�5:��Y<�%�d�gDi4����G�3Lؐ�	8T��u;8gu�I���n#����0��$�RU����P�/�0���Ψ1��
�GT����(��S��v��k��њz6
�:6��s��=��qh�II)FD�(����҉8X�A�-&6��&�6�n�����a���D"8\*%�� SU���J���,����
Vcđ"X��B�s[!�sJ��yU���Ĥd��tx�[Զ�Q>	�H���[G��I�j�����0��2�j����\��$�Q��C�dE�����'
�Hh��4ڻex��n����w�b�킔�x��*uc�K2�:V���Ʉ�qBj}��I2���7#��H30��}����8�_��5��~��\M|���$�<�+x ��2dUc�%
l��p:I�`��G�Y"���EO2�m�	����6���3�|Ca�F�A�^<	
E��.Ŷ��إ���z'7�4�J��8jX�#x�g��0�m@M����@ ���<�CR��a@hfB��\���2*0?D���M��=��g�� �`7`"v��~�	^n��$�߰~�%أ���a4ˀ:u^&CUR��� ��8�l@H�?�p��E�!��J��.�S2d�T�D"A\�c��M��a�Ũ�3�p��q�P�xU���0�4^Y+��ǖ�
����1����̾u�������
P#��LF��h`쵕ae��@�+�J�J�q{�����6�E4�l�8�1�DDќ��J�}*���FBoDP�H�p�y={*ɀ�9$x!��6�������h��Po�G��E ��7�0�8����$=N	�0��$~-��w��ĦPe��ϙ�,��ț��M�R(���;����j|�f�@�c-��j.����Uv�=�&�`{�:����]��J06@�x=�$d3�q�`ϗ_/�G�U�����
n@���|�z@�xT�p��'C�dD�8	
�I�/��|���d�NPNb(2�� �ؼ�gbo�,쫞S��𩶧���u3N�&L��t)ԴY�VQ�J8E��*�UFF��i�@2��cu8��/Q�C�܅��}���"�%|3#�:@��tT,����yg�&[���_ f>�G3��nL�:��P��;�iU(�g�����硝	{���������F>5� ���R�x
�g�13 �(t�)��Gհ!��u���'V�}��K����aEQ����c�s�
tz � �pi�;��-��X��{�`F�mg$������<X`����&�T�a���������|����]2`��(
v���#J�=я���P-���(�pM^��*��~�0 �6�c,x
�Џ�Xo��p��&
�	��C|�%�8Z�� Kl�1��8	>��
Ԟ����g�� �J�$l\p~��c���EF0v��/	�b��R|������<��ρp_`�K�97��6�2E�ō@�Z�,���L2EA����\:OCȗ�lK�Ux2�D9|7��C%��j��Џia�x�_�*��se�|O��d��H��7���z,�a��R�5\��J�i���7 8��t.�ί&#�I�Jc>�k����p�8��c)��b:��-����}j���c)|�ʑ66����a�r��0`�=6��{�XtC��A���X	�_�լ�d�CoGs�g<��3���`,N-N��Υ�� �·�d2f�j�� ����O&�<�S�{K�EG�>*�S.j���kɄu$x=��tϞ;Q؜�ip��<^��Ȁ��7��t�9a��g~3�C��#@9@y�:bb��9!�l��`>�T;�%�
0H2�H���:��E�����K:��1��XM��W�
L��Ǆ�t�6�h [ϳE
3@7�F~�e|�i$x*pp���;�>�ƀ�Q����ZL����R�%S )))��l/JήٽMKg���	q"$�r�_{�\،�z��+I�*�-��̨�۽J,�U��.8#ΔaY`-g�x{�����H���'�� o���3�vRI�L=�)	2�W/�W��xM�
xڏuF�tͼ��S�`w݀�w*��w%ጕĊ���5`�5#l���l}����Ά�3q�������qԷQ���o`��
�t15�3Zl@BBBPC��&^��Pk[��$~��J,�S��L4���џw��t͌�y-7��g�8Dݝ� �W���I0�y ��cO��p�K*�Z߀'^c�ْւZ�Y$p	��
K�VaiDӽCx��ks���ŚK��g��:8�֒ص}�5}��MMп ��t�e�n@s�3���n�H�H���p�S
'��~�]��&]�s�+��c���(pn`%U����n$�*���P:�7����Ν;'[p����IpԨQ]�����O�y��73�����.��=A��=����a�b�E��/��q1��<6�����+[8����ό*�n�M��$�ƍK"�T�b�-u��gKZ�}*.vI�C8G=�Rb#ڈK-��i��L<[�rl^f��dJG���������
0���f��7߼IG���@r4�8GgmC���E��+t��*݇��
kk���>|����5>��ˉ���C8��03�~�C�
���+��/�im)���dJǼ��=���{y�^��L�o��l=��=z�h�5b��D_l�_|1sȐ!�
�8p��p�ׯ�r++�Ut}M߾}7XXXl�:��bW2 7����$�e&�c�DdJ�]g����c��{��	��_D�����}��찲m۶�ڴi���^j�tL��6$�	|�O�>��ٳ�[ݻw�J��;w��S�N�;v��Q��o�~p�v�|��W�}�WF~|��fB��
!U�'`�9R%�8�?��_ve��A哺��e�&Rb�6�uym}����SB{�� vT����M�@�/�G���m�W����D'�3ѕx�C/E�&�+"�[���b'�+�V�O���ϼ�C����賣�Q�]ڻ�N�N�C�u��q� EûbG�����wd�k�-��e�G©Q<��y	�R:Jo����0���ş	�G̘6̜7�g�R�L�H�c��Z�����j�򐗼�oF?��;�N/��=wP�������Z�F�- �U������)7,z/}j8\�@�y*��.��}\��
�4c�)f��ǟ��\��]���~*qm>��f��)�ooɿ��K�Ǧ/ό�Zv�,��bhw�ʺ���1rĻb�p#����u9t�[������<7;�Ás�>��(e�xiƐzqN�L</�c�}l_ǟ+�9��/�j�W����o����3�x��3�9�́���ߊ;� s�j    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR           szz�   gAMA  �ܲ�  wIDATXG����QƷ�#��;� ��"�/A4DAA�P�Oa!� "��#"�"� l+D�5"��|�3xw��Ό>��x~�w�^8o��⫠N��<�Nt>�5	����v;���t8>$�qk ����5�V+MC��B��)���M���%�f3M,�����n�R���&�c��X,���dB�^O�� �1��N�R�`0�v��I`�Ř���}�����1��]��3�o��"�{��*�����z�Q#�p84T��/@�բz�N�f�P�^\�F�A�R�.�@*
w�� �Z�r��]/.@�Z�L&s��P�T(�N�E����eJ&��H$<��(��F)�*x�����)S(2T� ��R  ��O���� ��������J7�n���j�E`�	6<Psp<��n�&X,�k(L�ၚ�J���r���$�f��6��4Tb����X`�
�\�x<N�C���d����|�I`��̇ ��R6�MF��	���_����e4Q���$0�@#s�'</�X}����u ��E�pņ</������C��:	,%����z�T�'���v������SQ�Bm�j�jC����FV���
    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      !  �PNG

   
IHDR           szz�   gAMA  �ܲ�  �IDATXGՖ	TT��_m
Z	��Ȏ�@0�̰o�JedG6EP�IܪƠ(hb�Cl�鉸�jm�
&�����3,*��� P�T��{�b،����s���{���}�w���0�̓a���T�ׯ(�񮌑>ݏ�����j���g-���;�TZ[k�?9�3�=u
��OV��s�(DO�քa�?	�5����v���K�q��gN����{��}ח_����(��}v���\���Gh�}��a�4��;���M.zw�h1�;�ֽ�~6���T�z�-�z�v��(�z�6�w���a��R�x��״��)Z�Lo��3����k�����"(��C�����H�P�A�m3ԯ���^U����CE}Y��,@��p{k�t� !`�+s��Mo����; ߐyv:�Y��^�ޫPd�B�)��9c��]uA.U
hNyF���P�砹h?�2SQ6C�րahnNҖ0���lCM��hK.��	��%@���ʿb�uq	�x��S�ژ�I����ʥ��=B�oO@�Ef2Y)h��cP:�*=�1�`���z��8�2�!���$!R������!.���H���A�uv��	�:�J!K�$���%�B����P�6�|�scw���/O�A=uǄAq�J��<9a`�Aϥs�G�@��AI݄>C���{wA�
�p���!�[���(��V��c���x�d��4<ua+ ^ID�Q!���	M�79��3���w��'$9٨
p�"~5䱡�E�
)�!<E��5d�;���<���@ԅ��>4��A��/�<r�Yb�c�B�Ț�d�C��U�J(��i�W ���k��!4 E��/� ��X�:R�+� �|�P+�E��*�<(i9��6����U�<H=m �^i�3d!����t�?B�ph< ��ႉ���"�B�,6G��	D�P��' SH\Lq��y(J�q=/�ZS/kH�������e����\� 0w��M2�����y��4��P�j�
&q����^8;i��青�P�̓��ro�D�a���.f�{y>�.�8<Y.�0Pko��	j�9��Y@�n��)
h����O��~S����(�l��Y0B��)��/��l	��7��ts�D�R��2��6�����!Zs���Z���<M���*�J_[.#�x�r�@o�$d.�
H�GΚ�<�IP��Nj���*�sq�����EK���ׂj��@r�`!�IG��'|iA� �I����6%E>����R�Wi�����.�Q�I��'�1��^n�R���myg�D ǯ��BH�-!���j(��׿�.F'��l��ގ��|tźA�kBsH�4�
~��l�Za��`��m�����]�hA0�f���fW+n�)|pikZ�UNsQ}���C�ڳu��'�ݵ�8!��\Q�C������`�*��A�c��_-��P'�6I�b�`��%�I;��Vc�s�ы�[�l�܎j�M�Ak�K�v��H1�����ړ �&�]���*��D�	���2\c�� +h(C��θ�2�H͞�������I?�#�D�ƳE�D�[+_Ջ��޴�.@O�qw�zR�Л�=�t�O��
Znѓ��Xڣ\q7�-�����=E�HӞtXew���Ƣ�N�y��-�]��n����3^�m�/���������g�BK@�@���C�X<�;�~���2Qd���:����v�&^�EΚk���>��o��FV�ܕ�g���~��W]�V_��#pD����/�
�4ٺF�)���������� |�ewe�����	.� 
X7k�l>���B�Ai\'�&�&\:՛�c#&��-Aܺ����/E_n �r���D���|,����c� -k�h��ۣ=�/�M5����Q��0ї��CϽ�F�
����G���{ǝ�������<�q��3ߢ���2�6!Gg�=�OQt�f~1������G}	2��q�    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      7  �PNG

   
IHDR           szz�   sRGB ���   gAMA  ���a   	pHYs  
�  
�l=   tIME�(۝�   tEXtAuthor ���H   tEXtDescription 	!#   
tEXtCopyright ��:   tEXtCreation time 5�	   	tEXtSoftware ]p�:   tEXtDisclaimer ����   tEXtWarning ��   tEXtSource ����   tEXtComment �̖�   tEXtTitle ���'  �IDATXG�WYlTUn	4V����/&��F�"D�٧S�.@��)�i���+ em�J�Lk�t֦(�!��}����;�\.�N/�����3�����?�97@�96l�+1�El$�N���1oJ�����C,M� `��F������c\�<���c�w~$���č~|�c"&�����ar����Ô�>L��ôm^|�Ë;=��˃Y{ܘ�����]��s����'�:Ab
E`�L&|�=A}�m���I{V���߈��ZL�����L��P7f���\����ܽN��;���	�A��:�9l������$� FE#`����4Q�E�������k�<;
��m��n���p� ����0�!��?��|��	'�t���C`�f�����[�J���A���[]H�IÀ�� ]J�t�p �	�n$�x�8ǋ%�>k���u��b�SH�
���AE@Y�m������?��ain"�����Nͳ&0G
g�^��ѡl-ق�yk��|*��oV�,t�B̀_��� .?=+N��L ��,�&@K�J�k�U��hh��"�+
}J�\F@[y&	��.���zn�璑vn�5��±��Qy<��B)��T8�)��)�0��㛼%X~)���,¶�v�B!�<x���hmkk��@Gg':%�����ݭ������*����x��!"�)�$�.`�֭[���F��۸-QSS���Z�J��թ���W���0l����Jyqq1*++q��M��8T�4.��PUUeDiii�1��Z�t�Ch����%%%(//��B�`���
U(�:���9�ْ sL��x$�+*��|3�tE��
��X���	H��N�, ���! t�7n�P�*�u\�vM�755����x��ѣG`X`��V��XhT�U��Ii��W���;w���diQ}�a�t
\�zըr*Պ�j�br̫���1���
�Z7;�e���ιαV�A����\��K��9�:�:�T�{>C�t��r�Ѩ�	Hg+�D�望sL�S1]bwd�b*Y�lh��U�	��y5;`�9��<s�ib�d���jհY������---B`�pG2�� �f"�9�S9�&�6����W�[9	�H �.ވv(���#���d�9�s*�|��#�ggg���H��CJJ�qy�;cǎ�3�ͧbE���5�s��z:��೴],�lR��J`
\�~���bcc�=zt|��5��Z9�+�yg�	v��%��W�6�������%�+���y��~��X��=��ݻj��($�iӦp||�d��P�Q	ht�3w��^�B�c���x�a�322.��	�j4�$�k�����\;�����f�I��SSS�I���_<� ?��T�̍�\���c�ݻwo ))i���m�
��FG�_'	v+����C����eE�Os�`����1��d��9�/��찝�.    IEND�B`��<GridEXLayoutData><RootTable><Columns Collection="true" ElementName="Column"><Column0 ID="TEN_THUOC"><Caption>Tên thuốc(biệt dược)</Caption><DataMember>TEN_THUOC</DataMember><Key>TEN_THUOC</Key><Position>0</Position><Width>164</Width></Column0><Column1 ID="ten_donvitinh"><Caption>ĐVT</Caption><DataMember>ten_donvitinh</DataMember><Key>ten_donvitinh</Key><Position>1</Position><Width>62</Width></Column1><Column2 ID="SO_LUONG"><Caption>Số lượng</Caption><DataMember>SO_LUONG</DataMember><FormatString>{0:#,#.##}</FormatString><Key>SO_LUONG</Key><Position>2</Position><Width>81</Width><FormatMode>UseStringFormat</FormatMode><CellStyle><LineAlignment>Center</LineAlignment><TextAlignment>Center</TextAlignment></CellStyle></Column2><Column3 ID="mota_them"><Caption>Lý do thanh lý</Caption><DataMember>mota_them</DataMember><EditType>NoEdit</EditType><Key>mota_them</Key><Position>3</Position><Width>117</Width></Column3><Column4 ID="don_gia"><Caption>Đơn giá</Caption><DataMember>don_gia</DataMember><FormatString>{0:#,#.##}</FormatString><InputMask>{0:#,#.##}</InputMask><Key>don_gia</Key><Position>4</Position><TextAlignment>Far</TextAlignment><Width>94</Width><FormatMode>UseStringFormat</FormatMode></Column4><Column5 ID="THANH_TIEN"><AggregateFunction>Sum</AggregateFunction><Caption>Thành tiền</Caption><DataMember>THANH_TIEN</DataMember><FormatString>{0:#,#.##}</FormatString><Key>THANH_TIEN</Key><Position>5</Position><TextAlignment>Far</TextAlignment><Width>101</Width><FormatMode>UseStringFormat</FormatMode><TotalFormatMode>UseStringFormat</TotalFormatMode><TotalFormatString>{0:#,#.##}</TotalFormatString><CellStyle><FontBold>True</FontBold><FontItalic>True</FontItalic><LineAlignment>Far</LineAlignment><TextAlignment>Far</TextAlignment></CellStyle></Column5><Column6 ID="TEN_NHA_CCAP"><Caption>Nhà  cung cấp</Caption><DataMember>TEN_NHA_CCAP</DataMember><Key>TEN_NHA_CCAP</Key><Position>6</Position><Width>86</Width></Column6></Columns><SortKeys Collection="true" ElementName="SortKey"><SortKey0 ID="SortKey0"><ColIndex>2</ColIndex></SortKey0></SortKeys><GroupCondition /></RootTable></GridEXLayoutData>A    ����          System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   _items_size_version  	                  A    ����          System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   _items_size_version  	                        A  ���   �   lSystem.Resources.ResourceReader, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#System.Resources.RuntimeResourceSet         hSystem.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aPADPADf�5C    U  ,p a n e l 2 . B a c k g r o u n d I m a g e     @    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      I  �PNG

   
IHDR   @   @   �iq�   gAMA  �ܲ�   IDATx^�[tT�M�N�^�6�d�7�Iqb'��ٸ%�qc�
b;�;8.��czX�7�{CHB
���PA�#!�$!@����4�¹��'�b4�@�v���9��A�f޻��_����<����^�����---�*++3�;V����?""bޗ�8���t:�HUU��ʐ���={������拿�"��CCC�]���Vttt��������c�֭�_:��͟~������F���[;^�xQ����3�m�333����K%���%�s�����ե�okk�ٳgGGG�����aggww�/� B~Ε+W*zzz4�@
N�>��� ��X^�>]��兀� ݗ�B�~!�L�z����hjj�T|������Z#oaa��`` ���!����M ǐ��NAm�w�j{�<�z֍ވ��;22bhoo�r�Ǻ�:hQ&i�����*?S!44QQQ��{�ξ����}v!�s삚߲j�sn-���?�]��[X��1�m�L?T�/�J?�<g�籦�㕞9OP�ݻwk�066			�fz��oP�=� n���R��Cpl;��v 5W����	M耜�4݋���Z����HL�q:�������������5�����ӽδ�s�j�m�߼XH�h)2��ӆ��$d葖�5����։��ĥw����>�:��6?q���|__�HCC�#+}dd�Jooo???�F<..���/n��5����W�nXl���g�\n���]�K�XJ��sFA�!{�ණ	�X�p��s��.����פ��;Vۀ��k]\�}�T����Z��׃���b-��+=[�Ty�<��EEBF�����]��d'���O�bk�o.��m˶�;�aq�O�#9K D2:52B�£w��:���1	;$`���3
ۼҰ�6k��K�'�a�۬37ϻy�{��v�T�Nt
EEEZ��+��������!=<?�]O�ũ�1m򊰥��*��i��+<C[�Clj'3�����"�wx3l|*���$68`�s�8�`�����BH�1��@u}�֮���S��.�^H��Q�a��8��F��/�L�<W��̄���x�g�g��ڵ111���Fǟ^F���F��Ｆ��b���[�(�M��m�{H+�l/�%�����pX���`�V�����=��2��Ch�I䟸��1��EH�֠&��YX�x�R/��Q��-v����y~���gΜ��s�4[��������V��G�w�����0�����o�5u�%�^��^Mph�_�{�*�������8]�'^e��u�=s��3G�
��BDf����"�ڴIL��ㄥpM��Qi%���2-���>M�J��W^�]/���9�?~||�W��3=�ZVz
s��=��������k�}�k[�«>�ʳ��[�M�� Q�k�ǞjX���6ߣ��[0�|8�9���8Tz	uM��&l��5�����N���Q���NXy7I������g7n�Z�q6??_kq�mә�i�b�׏@���8{��q��=y�yo��x��W3l�`LJ'�#�`\��C0 ��sq��p�L
�'FX Sxҿ����S�[��
�Y��R�՗�A-��vּ��+*dz;p�fk�	����ޒ�,vIII(����F�mw�v���?ͽ6y*�ݣ�#+���|��`�W��A��Y���r?[��݄ϔ�R��4�3���Ð��]�%X'��]Z��Ԗ��F�ܹ8�
0f�+=]�9��.%%YYY8��S(���h}��3ݯ�nz�n�G�fk�f�}�����(AK[�F�E�F	��"��Pŏ�	~6��{ѩj�qɁ�wN��i����^�Z��,�4lst��������F�_���ށ��8�����v�3�h�g!p�kEP�����L9��(��<L-N⊴)qA�������zu�~��w_Cƺ5j�F��ls\ͱ��P����([]f�&m|�,�o6s�78�J���K�eX�)EG�~Ƒ7����1���^�Jq��ӄ��8p�S�L�}9gP*�v=]5�=�<�⹏��ް�q�c�#i�!> .x��ȫ7mr�������,Z���2��j�,{�ǵ�-+�	6����&I��N��X�Ҩ��F�=I�r�ȷ��nT�r;�^}s�ͱ�q #�,��ß�<߼ɹf	[�[�N��Nq�9$�h�9�c��fm淲8�I;�ҥ+]�b�t
p��y����K���3�|��w��-w#��H�N�|?�����(I��}f����;o��^?���ޯ�Kt5<�+����q��fmE����Z�ʽ�=Z��s�zm5GK����v���j��zN�f�����Å#�W]��7�qA�S@�b:����r�4vhi`��
�i�qu���:�֫�i�Y�ldt��oWsז/���P��P��Bt�X����3t/~�ϝ���7��
�'��z�DV!��y|����]��"�ހ
�:l��a���ƐW�=�ck�]�M.e9�$���`DN��Tu����f��p�'�2���4�׽�ؕY�ٗ7��t��LߤNl���؁-A��8�Ӻ
}���h��j5��}~_|f��������W�w��r鐅PZaS
]GτJn�ۦVW��ye
a�]��m!�ؾ[���t���lW38��|bR%�:�f�(��o��%7G�6��;e ��6�'+:4W��f� ����O���0�OJ7z���3>ݱ��
p���
���&�ڑ��g�x���4�ܳQ�@'2{ຯqE�� �hj�E��.HW�4¦(:�;N�"��UT7��TԍU֙ڈ�і��maa!�k�d!vv�u��L_e{��
;ӱ���;V��:eǧ�ɲ2�o���� tB��;�G�+;��͞�?ҍv}�&D�a�������M����h�
`�t��7��-�(*:�C�=Xl	��˰���Y�a���k��L��t�Yis1�U�݈����&T���� ɕWF�	��,n��9��b�l��E��^-�$mk���!ހ����ʥZ��$��I+�<������C;e߰Z��{���5�s>����J��!r� ��6d�'��rC�����Q�R�����6���ےx���CBRO�k�-F�`!�WY��9{��dN��sh��5J
qo�r��+�6�鞸̼���;kF�#���Ӌ��z�e�O�m0Z��U�Y�Y�T�����F�;	q��>��JZ�ڛ"�8��8�+���+m��k��؄��W\�؆��&�N�ӌ�[aYU�#Ħ��G�)j����C
����n ��W�G�FݞQ#�2 W!_X>��Z*�����7JKKq��)���h�>�unǥ(�k"���8�W8cR3y�2��d�$,���}pNԣ�\�Fz24��v�.��y��>�L�����
�?g}��W�"jW���t �+�)�G�y
6�����68^�R�?�	�:w�y�=f��2t#�`�S{q�0���z@������c��A�q��!xf���*q��v� �A9��ɓXn��U�Y0��Ǌ-�xuE���L�-/V�� %�O�yl���1.Z�=�껂��a�?E�I�tb�S ��7N��\|`�3�|l�<�����}�¥�+��ww�y/2��5K��Mڲ� ��6���7Q���7��i@�;���CxwG:6��b������_�;��3�Ԧ�P��yG�&��F�%m��@qX�UP)pRR��Xf����9�n��e���7��󶯚��}�7q*��k���U8�Ȝ~��N04��M7Ҍ��L�����0�ʕ�-�c��,�;j���?��/BMݷ������BRv?Ҋd���~�exB3����#m*w�T��I�ĉp�(|b�/"��奱�;��Ϳ�=~]�5�M� �b��ƶ���gް�kFtJ
O�U�@v���S��LI���)�.P��8pT�=��j�0���'���Չx�%��v\!R��Dc.�B��*Rͯ�{'��U��G"d�+86��,�������H_+�Oq��
��G�٭�[��C�\X�̇�[�����[S��B�r��|[��11n1�� �k��ȫ����a������x��R4 �N/ںF�	?�	�I�L>��,ee�-�:�u*�6�8���� ����!�7�������^���,�mc���$�1����H[񃾽p�A+�)�p�xH�{��a�nc½������v��!>��"vJ��!������ ,�C�pY�;��|����;�~`$ķ��RCՇIP�'y��P�;z��ɍ�u�O8�#���'7��?mH��j���*�	62dy$&)�T0��#C�n�p�� R�� �7&�_��;a�~��r���T��t����+���ෂ�~9&����X)=e*��+��'���.�O�l*�����7�xp �{{��;y
LE�NV��ɽ�̖e��Ղ�CZa�?6���A$Dl��2Eh" ���ë�5x}é�'������1����rO/
�<)�W�
��+���i�D`���&�Ӊ% �ϓ�s�������e���#=oGK�e���i���]A��
�U!��Nq��%H�G�G����pX��p�0�NK��G��9(Q��
{�i�z�,�X:���I~;�!}�/^�馯����@��ςE�&"�	*%��#c@Yϔ��*1��]����T��G�;}�G�䆋ύ G�g{��"�/csl� ��c-�1�&��02�d����H��"��lu�N4��T6��[�U�`�}׽/~��_�!��.�*�(X#X!X*xC�����	�<&xH�\���}��,���(��������[���;f���2�ĺ#8%"-�tD\���X��̈���GP(QΑ(�ϕܗ��Ua	˦�:!�dc��3o&T=��8a���>�#�M�E�A�Z�|L�%r���i@�p�H��=�\��z6-P�[��r�#~?�W����-낄,�ݓ#(=/BJG%ʹG��V0,�kH�"�e/�&�?o(|�.��?�0aE��H����4�����p�_����������Ac��b����� ���Ӏu�.� £/{�X�P-IAʁa����z�ߜ�	o<3��������_��WrX�QAU��0#�I����q������$��@Y�_�OF����W#�u[�6���Ef��p��P��f�����.��n��H��J�_�pf���r�>�Ǡػx���o|�v�4F�9LB����I�׈Ղ�|���7��j�Ƴ ���ȳ�����)��#�<�����׻~�j����e����hޒx���0[�[����F02�MV���Wږ՛Q��I�����
��%���u�m\��*�ƽ��iy5
�5[��H<� |A�@!�D���
��Ȣ��l5�?�J��c7Ǿ�����W3j$AaH菂�c�s�%���Xظ�aqcug�Y���4k�<�\
=��,���nV��f���*%�"A;��+%�
�g
¹�7�bĖ��|@��ȑ2����,b|�D)��Z��Rd�9f��I�fp$��j�i����?LW�te>���J�BQx��Na�(I0r�|M���$���|&YZ��8K�V~j��Q�g�4&<�sc!T��E� �0o�Q�0�7Jqx�t	��"����H����6&�{�=!%�q�(Q�JR	C�(q�k�P�Aj�n�wu�����Q��4��a����T���3=����T��]]�z�5�����ά+Rx    IEND�B`�   ^	  ���   �   lSystem.Resources.ResourceReader, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#System.Resources.RuntimeResourceSet         hSystem.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aPADPADb��    [  2c m d C o n f i g . B a c k g r o u n d I m a g e     @    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      `  �PNG

   
IHDR           szz�   gAMA  �ܲ�  IDATXG��yLUW�1Ȣ,Mj�Rk�*ZQl�jL��h�m�6
��&��*���よ1Q���(�*�"�P�@6Y�@D�t~�w�W*�kғL޽�3���7sγ��~��"c�޽Y"fɌ��OU�֫�r����GyyyUUU�����Ç���Zݼy�����s+��������߀\��TTTT��
�"�Ft[�0u�ԯd��j`����c���I�����
+��(4����[Z�f�Z���Ĭ�
 �=z�**^�ٷo_�(�;v�wDDĶ���v�;;;U{{�z��*//W���q��}��իWG�&??_=~���A,_�|!ȫ��z�j�S�Z%����A~���jjjR���lNN�:q�ċ�'O����_	Pu��m����jkkպu�z�Ą	���{��u��jkkS�T��њ�MB��>�1HI�*((�Ҫ3g�hӧO�1vvv�+W�̵�N�	1��ڪ�߿��_��.\���*==]:tH?��w�R�d2��+W����L
dӦM%����{ャ|�mDGG�sKK�F�'Ǐ�F����#G����D�~�z�v�ܩC.)S/^T�ϟW�/_�xi�~���No#�������{��z�����ٳ�Q��;w�h��`�ΝS6lPaaajŊ:�̝>}Z%%%5��?��q
������
���xDn�4�T�


U���jɒ%j۶m�[�y�"��Vjj�5jԤ�<�57;����ܸqC��v�ޭ=�u떒���̙�j̘1�nnnq�����
��s�c\ޕtlu��A�љ`�
6_iqB�J1V���[$${���ʴ�ÇkF�H��<xp�l�d�aÆ�.X��s����<�e˖����k�.��*�
����򄄄43�T9��QZ����&
�X�^�ieS�LI7�cR�?�6m�	 �ٳ�ƣV�Z���\ZZ�n�(�!V){���x�bF��@2�����YYY:�C�
�)�Ç�
0�c�������q[evĦ���D]v��v��=��.]���رc���ݿ' ���?�.�"x�/-K� �	��iL4� �yGx' ��̘1#�' �g�^hBM��*0f�a�wA�������3Y(����պ�=b�"oT��uÐ!C �?���x�R3 !+��D��%�ځ�)�BQ�1Jlmm'I�
�"��L
�a6�'"@c�#JDjg͚��H#F��
		�C���u�d�F	At
Yc�!b'D�
a����� 0���Ej=zt�	����N+DJy�b�Z:�f�I��;!G8-9��=���I����7f�s�֭�� @�-#T@/]���w<� \}}�>�DW]O����̙3c����Q.�ʠ<9�F$�c{�$�-Z�,�,oVo��BB�WpA^����'O�h �7 ,�e���_"�N�7\�������d����f���<Sn0�z
`c-�c/�.�%__�_{K��D�$ eF9nܸ�M�r$�
��_�.9[(e��tY99������|��d��ɧ��2.��-��v��FX��K��x{{o���r$籆���Ν�/�E�����~�����χ/�wn2��:���Q��s�/���,�i.�a,   G�ƿ�5��Pb����VGG�͵k,������C$''���z��}'�/s߉pݳj��Et����o�^]ZZ���@�����7:�vk���e#Wz�s9z��7�I�,�iR�a�����v���� ��"��E���>8;�h��R��-靀�<D��u�{n    IEND�B`�      ��  ���   �   lSystem.Resources.ResourceReader, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#System.Resources.RuntimeResourceSet         hSystem.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a�System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089U�U���נ�%�e3��kǷ9SN��N$�iJ��` Ʌ���Rz9�z��`���7��+�v�,m�-2	Yq��& D��*Ah0��GO.!J�7�P�F\8C/fn�Bv�o�v
ߑzD  �  
  m  a  W  4  c  �  �   U  z   �    �  �  W     �          �  �   �  �  A  �  �    ^	  Rc b o K i e u K h a m _ D e s i g n T i m e L a y o u t . L a y o u t S t r i n g     c m d C o n f i g . I m a g e   8c m d G o i S o K h a m . B a c k g r o u n d I m a g e U  $c m d I n B i e n l a i . I m a g e I,  (c m d I n P h i e u K h a m . I m a g e �3  "c m d I n h o a d o n . I m a g e Y;  (c m d Q M S P r o p e r t y . I m a g e �>  c m d S a v e . I m a g e UJ  0c m d S t a r t . B a c k g r o u n d I m a g e iP  .c m d S t o p . B a c k g r o u n d I m a g e []  ,c m d T h a n h T o a n K h a m . I m a g e j  &c m d T h e m D a n t o c . I m a g e Ks  $c m d T h e m M o i B N . I m a g e �v  .c m d T h e m N g h e n g h i e p . I m a g e k{  :c m d T h e m t r i e u c h u n g b a n d a u . I m a g e �~   c m d X o a K h a m . I m a g e 5�  8c m d X o a S o K h a m . B a c k g r o u n d I m a g e ߆  Rg r d L i s t K h o a _ D e s i g n T i m e L a y o u t . L a y o u t S t r i n g 1�  Pg r d R e g E x a m _ D e s i g n T i m e L a y o u t . L a y o u t S t r i n g ��  ,p a n e l 4 . B a c k g r o u n d I m a g e [�  4t x t D a n t o c . A u t o C o m p l e t e L i s t s�  4t x t D i a c h i . A u t o C o m p l e t e L i s t @�  >t x t D i a c h i _ b h y t . A u t o C o m p l e t e L i s t 
�  @t x t E x a m t y p e C o d e . A u t o C o m p l e t e L i s t ��  8t x t K i e u K h a m . A u t o C o m p l e t e L i s t ��  Dt x t M a D t u o n g _ B H Y T 2 . A u t o C o m p l e t e L i s t t�  <t x t N g h e N g h i e p . A u t o C o m p l e t e L i s t A�  :t x t P h o n g k h a m . A u t o C o m p l e t e L i s t �  @t x t T r i e u C h u n g B D . A u t o C o m p l e t e L i s t ��  "u i G r o u p B o x 3 . I m a g e ��  �<GridEXLayoutData><RootTable><Columns Collection="true" ElementName="Column"><Column0 ID="id_dichvukcb"><Caption>ID</Caption><DataMember>id_dichvukcb</DataMember><Key>id_dichvukcb</Key><Position>0</Position><Visible>False</Visible><OwnerDrawnMode>Cells</OwnerDrawnMode></Column0><Column1 ID="ma_dichvukcb"><Caption>Mã</Caption><DataMember>ma_dichvukcb</DataMember><Key>ma_dichvukcb</Key><Position>1</Position></Column1><Column2 ID="ten_dichvukcb"><Caption>Tên dịch vụ KCB</Caption><DataMember>ten_dichvukcb</DataMember><DefaultGroupPrefix>Tên dịch vụ KCB:</DefaultGroupPrefix><Key>ten_dichvukcb</Key><Position>2</Position><Width>250</Width></Column2><Column3 ID="don_gia"><Caption>Đơn giá</Caption><DataMember>don_gia</DataMember><FormatString>{0:0,0}</FormatString><Key>don_gia</Key><Position>3</Position><TextAlignment>Far</TextAlignment><FormatMode>UseStringFormat</FormatMode><CellStyle><FontBold>True</FontBold><LineAlignment>Far</LineAlignment><TextAlignment>Far</TextAlignment></CellStyle></Column3><Column4 ID="phuthu_dungtuyen"><Caption>Phụ thu ĐT</Caption><DataMember>phuthu_dungtuyen</DataMember><FormatString>{0:0,0}</FormatString><Key>phuthu_dungtuyen</Key><Position>4</Position><TextAlignment>Far</TextAlignment><FormatMode>UseStringFormat</FormatMode><CellStyle><FontBold>True</FontBold><LineAlignment>Far</LineAlignment><TextAlignment>Far</TextAlignment></CellStyle></Column4><Column5 ID="phuthu_traituyen"><Caption>Phụ thu TT</Caption><DataMember>phuthu_traituyen</DataMember><Key>phuthu_traituyen</Key><Position>5</Position></Column5><Column6 ID="ten_phongkham"><Caption>Phòng khám</Caption><DataMember>ten_phongkham</DataMember><Key>ten_phongkham</Key><Position>6</Position></Column6><Column7 ID="id_phongkham"><Caption>id phòng khám</Caption><DataMember>id_phongkham</DataMember><Key>id_phongkham</Key><Position>7</Position><Visible>False</Visible></Column7></Columns><SortKeys Collection="true" ElementName="SortKey"><SortKey0 ID="SortKey0"><ColIndex>2</ColIndex></SortKey0></SortKeys><GroupCondition /></RootTable><RowWithErrorsFormatStyle><PredefinedStyle>RowWithErrorsFormatStyle</PredefinedStyle></RowWithErrorsFormatStyle><LinkFormatStyle><PredefinedStyle>LinkFormatStyle</PredefinedStyle></LinkFormatStyle><CardCaptionFormatStyle><PredefinedStyle>CardCaptionFormatStyle</PredefinedStyle></CardCaptionFormatStyle><GroupByBoxFormatStyle><PredefinedStyle>GroupByBoxFormatStyle</PredefinedStyle></GroupByBoxFormatStyle><GroupByBoxInfoFormatStyle><PredefinedStyle>GroupByBoxInfoFormatStyle</PredefinedStyle></GroupByBoxInfoFormatStyle><GroupRowFormatStyle><PredefinedStyle>GroupRowFormatStyle</PredefinedStyle></GroupRowFormatStyle><HeaderFormatStyle><PredefinedStyle>HeaderFormatStyle</PredefinedStyle></HeaderFormatStyle><PreviewRowFormatStyle><PredefinedStyle>PreviewRowFormatStyle</PredefinedStyle></PreviewRowFormatStyle><RowFormatStyle><PredefinedStyle>RowFormatStyle</PredefinedStyle></RowFormatStyle><SelectedFormatStyle><PredefinedStyle>SelectedFormatStyle</PredefinedStyle></SelectedFormatStyle><SelectedInactiveFormatStyle><PredefinedStyle>SelectedInactiveFormatStyle</PredefinedStyle></SelectedInactiveFormatStyle><WatermarkImage /><AlternatingColors>True</AlternatingColors><BorderStyle>Raised</BorderStyle><BackColor>Control</BackColor><VisualStyle>Office2003</VisualStyle><AllowEdit>False</AllowEdit><ExpandableGroups>False</ExpandableGroups><GroupByBoxVisible>False</GroupByBoxVisible><HideSelection>Highlight</HideSelection><RowHeaders>True</RowHeaders><DynamicFiltering>True</DynamicFiltering><ColumnAutoSizeMode>DiaplayedCells</ColumnAutoSizeMode><FilterRowButtonStyle>ConditionOperatorDropDown</FilterRowButtonStyle><DefaultFilterRowComparison>Contains</DefaultFilterRowComparison><DisplayMember>_name</DisplayMember><ValueMember>ID</ValueMember><VisibleRows>15</VisibleRows></GridEXLayoutData>@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR   0   0   W��   gAMA  �ܲ�  SIDAThC�YT�U%�Ʃ4�{�\��Աl��)m�6�SYc�i�+E�(*j��#@!G�oR�|���R��
��<�/{�w�����p����s�=g����������xxx���8���.�\�`A�������g�?��݇www�1>>���ٳHOOGFF�����O?!22���������;����_�p%%%�~�:


PXX���2H��e�29��wh�iK�,��˗Q\\���<�����ٳg?q?���;w����ӷ������`������N��>�����?�Ȟ��QTTt[�|tt4H<�������D�����H%�u�Fb�g��mk"?|��ZӮM�0=F�<y�q���o@DD��ޚ�Ο�)�5m۴
I���7++K�.3��Z��]���@\�uߖڷY���C��[�^���Gnn�-b%��!!!2/�m@.n���`��������,Lk3�-u4iҤG�xS�Os�&h/�>�~���d�±c�P^^���$���q۲�Cll왆���(�@2	�q;7���-Z�}����lz���`YD[l��֯_F���Rv��A9> (((�V��x��1118z��/Ο?�	ii�6�����Evv6d�6!��ܟ;w�`����!�XL��/i�k@����M	��#/�����p�!�gB�*kD�(�D��(***i֬Y6-�y��(�{�s޲e��)�@����B���&B�TSaM�����(kʽ������Zl��n�:E��T�'�����CG�������~KDsy�Ή'�g�Snnnn5V�7�ln��q�wS��q�/�ŕP;$�ށ}1�����i!�;ɚh�^�[K:tL���'ۉ�fl��^UHZd�·'����72��o��4��o�	�J555�2��X�����#6�Ž�"S���!�S��gRB�NT|3�!�P��`�h��j?+�Ǿ��"� w������B2���"JP]]mxB����`j]I���!�ge_A�i$���_�Ƶi��uQ!k�%J��!j�����Y�8�	��{�w��.'��Y�j�v�{�=˗/_G�̠����S�G�D���/؁�G��M��l����_�E�V(���_��B�g��������M�>>�v�jk:��ь�[C�Y$_����Q4[
}hO��^�X��]��%�}Q0w 2]l����ӭ'�f�viS饲�����N��@u�=ʼ-�����IZ�U�^��A=P�a��6�0A�ԉ'H��^�.D��J/�����%����e�(��U�!���j�+"��4B���B1�����~�B��*9�̬�Ɉ�@���1�1�M�b�d;j�pq���
�WG��Z"���k���?�C�k\t�� \tVǚ�<mSm$_���7QhɈ��#i!�5�ƈc���g�E�j[dL��g'5~vT�$9��7��rڦf-��J��f�(����"$ؔ�����Dld&��$p�TD>��ϭpy��D^����t/��i�_���9����FBԕ���#-ą�"V�{ 	O�#Q=�3�p�s
R>S�'��5Y���#zT!�ǃ�m<ۼ�����=`�p`�n�,��6���.���uCP쮅n�)��9�7��9��ի�k$_GbvL��#sP�g&�}���8	Ǔ��5p�mR?@���(��"�.O2�mJ�Uc�V0�$�O��orgՠa�K(��)p�<���|{�ڗxX{�^�,�"�C��OQ�a*���>���|��	#_覲���B:�|�VF��Y2Ku��>k��u��7�b�L�z82Hw�͍���G�b/��Lۘ�|��zTŗ<�.㑘�*񅖙���_v��>U��"��zG��?N�i���*�}�(�f�-u�>1a���`֩,�<��k�`�I���e�P�[�������O�|���hH�W6�8�=_��_|�P�m�kiZ�HD����V�j�p: ���ǁ>ܠ��eC�+��E�O8�*�5�����c�mc��)�+�L������偪
|�'��]�xIK�%�K������%w s6mㄺ�!�+�ɝn�;���ۤ�o(����
k�2�.L��G{7f��;��Dw�#,O����(�Т��e��8�������''���𲼆5|���-À�$�H" ��:��A�*W�&e�bI���sFYwl�S��-A�����t�1��*��Uv�_�p���w��=Ç�a(�o8��"�Q�{M�ЫdԑP���H�ԛ݈.Dg�QB^���x��?/$䨭�U��[���4ty�*�n� �y7���6�)�`ԗ>��i�Q����^�t����J�$Fc�����@�\�i7�f��̜�L寥��!B��u����;	yQ.Q��<<����-rK��!wN_�\mpu��<g�`v��n��t��Ŷ�L�CxR.��
�Z �R?�p!��#��!�F/"�i�7!��2k��O��a=�.��i���HXS�;�>1��w�B��lN��#q�{���v�0�m�o!_B��5C ����^����}�k��M�����"Kރʬ�K��$'R!~��	�R�>������>���ͯG�u��O�����˷DU���	9>-�)�7�S��LL">!�I��� �^C����'� ��B�t�H��Pl$B���,HkUG�!�Nf�?�%d�7	��x�cb1�HH�1�	�^�J�E�D[��E�F4���O܃7t%��8D���9ZY��H�&j凒%D��PfHD�,��d�~�xV^�ɴ?KȆ�@�|�����Ȋ5�	����q%+5_��;�6��8(�AbA���!�
�{�����*)�y���D��=�s��;�?�O~��#�    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      Q  ���� JFIF       �� C 		

	


 "" $(4,$&1'-=-157:::#+?D?8C49:7�� C




7%%77777777777777777777777777777777777777777777777777��  � �" ��             �� @      !13A�"Qar��Rq���#2BTb�$C���Sc�����            �� "          !1"A#2BQ��   ? ��          2g`#����g`#�ؖ�`#�ؖ�`#�ؗ4lv1�=�lA��              2	$"I�I� EDʉ�@���h�:1с_�9���t`W��y� ��aİ�A�

h�(��(��L�         gc;;H��H���M���MЁ(VX�d�Y�TX�G��o�t�eߴ���M[��/���3:�yV:#�h�u�׭�~n\�ƲĬ���b��
���Ѽ����S�E�/tc�<�G�0�/�G�yD�>���\p������[������X����5N���5}:��g�-�;oS�_�X��u(��H�d
ӈ�"͒DD@    �H"I��(��M���I�����5F�Vl�e���Y��p�bVNW+,��"����I/AWS���p.�ͱB�����ԗ���u��"�'���k^�5o�\}_���œ���Mi�K�k���������0a�F�׭k���a�/3�XzQ��{�{���ܣϗ�R�@��^'��йT�ɺN��˫��b������u8��D�}Mn�Q�N%ҭ�u����&�%�����s�x�<F��ʪfT��A�3��o�>g+�"��jթ��q_2ݼko�~l��3�'�߭ש��rI��yiKњvӻ��%���3����zVu�n���K��O�^+�z+"��:���d��i��ɧ�ۏ��ڭ�*�����9�S�54o�4�k`�0   �0e$��Db��@J17B���!Y�aZ�7F1^(
p���gu�Cu�D9G)�_�'�i���l�b� }���^'��#�>�ǖ��Y�:��s��`� ���[ݝ�|?�^��� ;N����=#��(�u=�=��zG�G/ڱu��ŕm�X�M�L�]�;m����f'h����q�<��R��dׂ�c/k:uZ����z�/��S��9�3��2�u/?���=Y�΃{W?M/>�/��j�U�,�mN���[��K��V=��6�޹��7	�SOfv~K�G�ÕU9of+t��K�?�_#��^�������K03�0�[YE��[��_�S�gGr�x�u� ol���gN�,�-��R��/E�#o$�΍k#��}\佱�f�� &��uuۢP�Jθ�Q�/%^H�j+M��$Ȕ  	"$��#}h�Y�o�%�a�W�j[ 8�����7���_I(�\,q�b�˩7���rޗ�i����ߙ���X���k���rޗ�>���w'��~�P]@����s�r���)=���   	����^g~�gt8WR�6�*ş5���p{�{���c<g�(�l��h��N����o���uwS'+��$��G�8sS���b��o�֥$����p�'�>���pR�+��=�����
A٦da���n�^�u��k�jE��q���T�VJ�Gj�+V� rꗒ�$yΝ[+
�F��������ܲ�4�Ժ���d����>K��>{�gF��&�|��yb�Whxy�zT_�o�d�T�u��_C�}:��N�	}��3�q�?��/P[n�Z�}�i�+�NEV��椾rq�ئ�^��X>u(�z7`Ϥƌ�i2�Ӆ�����˖���4H�9(  �qtU"�e��.R���EJ��y�#���~f�fGg���g�{�     'O{yy�����y�����=#��(���V.�\�᪵�<�����͓���Ѣ�$�.�~�w궍�1��Y��i�<-.�m���?���'p��e��b� ��
GێG�t�R�Y��뫙b�I~M�OL��Աm�B_)#�q�zN�S�ś�-���~���L��J��p�u?o�� Şt���������U?#�-m�N'�Q�~���p��~u�.^|~��
��� �_��o8���ꅥK��,�"�#[(  �q}e���-T�/�\]��K.Ǳy�#���~f�fGg���g�{�     'O{yy�����y�����=#��(���V.�-_Wi��w�_(� ��_'qr��]�#c� �?E�>R���b'�]2�K��-��|�K�vq�۪�%��m/��z��]K�e���U��&�h��}}�w�
�Gj{9�0_%�ð��q�_�lc�hqz��O���t�{*~G�����=�b��s-�ex�?�Y�_�8�IGy���=�^l�w�7�j�tL(z����9-ݥ��Fҥ��J�dkf��@  �H�@n�f�U�7��/���=��*�v������}��ϧ�:VN����dW$��p��S��M|�͗��Z&&7v�l�Lse�e   :{�{����ܣʹ������-F�q�oP���XcY(5�����F���g��5��NvD%Ϊ���=���g$x[S���yb�/���9�ݵ���N��:_պ6.<�ӌ9���[�5Ș�8�-��>G,j���Oҿ!=��M��s^��Oi��W)?�=��^W���х	oJ}%�R���^K��}v̆��=��=��->�6GUt�.������p_�����;,�q�Q_�򳔱�Z�d�,�����NO�����q���};n�|[��[�'�e+��U��a�Q�_$J�OmF��U�ᇚ���ab�V�F�'"  (��ȳt]3dX�j���2V�]�9$ߵ��C�����ZA���� JU��#��Z���$��ܤps�'�i���o�TWj�k�������-�8�(�$�L�����֗�R~s���|��^��������a�/3�Q��$����>Ə7����^g�t��|�j�ߌ���7WY�;eTgυۛ��i��Ձ�nFD�k�s��In�g��]�l�������fՏ�#�k������h��;�r�_>�ST�η���oR}��:%Zk�Hy3��ʱ�{��~g-��,�Σ��K��������1�Ӱ!�(����F+���[U�Cw��漰�
�k�8�󤿪}~I|ϟɎ�8�W��q�o�.��n~_�e�kY��� >�(�#��H���iέ.̹-��f�ݏR�w5����g��\ڒ*\�V������V�����2��F�2�   	&I2Sldl���R�fm��%#dfA~�+��|��� ���f��:3�-�ʮ6Ud\e��Z��p>0���sS�2�O��E�~��?j>�#�qu�2�<���	��K���k:^N��[��m����$��3�`��O�Z�j�������?�G���a�/3�:r�._�K����.�
*�a�����=5ַ��#����Y�[Mgp�N����G+O̖n�R��8U^�����#�r���'��%R�#7�I'֡�O�����QR�.�p^;��qE�o��M�S�����'.O�����5�Ȯ���Y%��g�x~�F=kѦ�����\�iJե�dw�z>پϒ����_�RE�_v�� ���]"��7�2��9�i��$͓f�Q`0      w3��2�k3��#l&VL�d��,�a�#t, �P�����\C�oZQ̥7L������ج-fk;�'N�hٙ�xs���KΎ�ğ^�~��](]�]�Ro�Z�J����o.Y�1���^��K�����y2��I��8��z��~$1���f�T��W_��z_i[*���z��8�����pv�;O�=n+�9}�>�� ޣ�ӟQZ�B�s`bs<�fgr��2��Jr4Ɂ3[2�Q�           D� e�26FF��)eL����K�Ak�![�9�X�HW�p:@�+s�<
�f�L��E�	JD0��
�fL              �;��=��Cq��s�n7{��n7{��n7[���7;�1�                                    ��@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR           szz�   gAMA  �ܲ�  �IDATXG�V	LUWUJYY+"X�41�Ԧ
FE��
u!.Q1�%UWЈ
���1��JP	HX"n
�UZĂ�Q�"�9�~����Mn����{��̙3wذA�3fX޼y3{Ϟ=���I��DDD�@Dox��ׯ�211�ⓜ��!���󨭭��a�q������黅���O<zV��v�ĉC2��f�]�v����>�˧���7A���1d0���uuuU�V��nݺ���9�rĈ�7�ؿ������233��n�q�mi�7PLL���������+y���b���` @����o
 �_�x�,X@���j�;v����bҤI��PSSC�W�&===>|8�~���W{ę3gJ�I� ����srr�����@IIIt��!Z�v-�F�E��TJD�
NQ.�����:;;�p��m:q�]�t�-Z$�7N@������͜9�(66V�[�l9�+ }}}�9s戇=�ÇiժU�u�V1l`` ����҄	h֬Y���I���& ���Ԃ`IW�nw,��ږ^^^ͧN��1::��ϟO;w�$��LK������L����d��
���,�V�\I%%%* �_�^Ζ?W��������ٳT]]MEEE���J:::ĪGVVVdhh(������˗�ҥKi������C~~~Lܰ�
t�ja����+.* lll|Q�7nܠM�6I�1�_KKK�7��+(  ����e�c�<�r�
���I���E�Çdmm�@KK�����oA���
�a���܉�0�l�21���r����dу��J1�h���Qcc������ϟ�G^=���˗/���ɓ'wE�Ra�F�^�����T^^N,̈́>�D�W($$��!g��gϞ!��u��~ljj"�Gy�����B��t_�~=�=Aʲ�������KHH��� ������� ����lܰ +�=��ѣG=�� $J-((H>}��xÈ\ff&����޽{e���JyB���ɓ'�Q��
��{&���}xr��i,���a�=R�y�f������t��>=~���J�?�o�>�08e�?~<͞=��o�m�UNU	yaJ RRR$�(�hmm�G�<7,!c�իW�B�t@+ f(۩S�J��9B��� �� �U�?v�X�
7:��D�UH�Ǐ���0&�έW�Øk֬�\Β�ݻw��7�,��˗/���=A�q�˰*�_4%�((�^���=*���(U�7��댌�IAA��
�:S�bֽ32�w��цH�H ��Q��	d���*�o>doo/%�T �iiiR��
��w�J�?��V�� VM9��(@H�r��i�D#�m�&�077W<o�ݻGO�>�,��p��d6l�[G4� 
���j;<E#Bޑ���ax�^��}NI-� ���R�����9l�x���%  P�޽��0���而׮]oaƑk�gI���{�m�W2�.�Pt3�
Ȅ���x��=<mqrrJd��h80��%�;y��cƌ	���*-��L�L�Ѧ���7�_����˭�[NA ?WI'7��l7`��,��N�+��8�dj��k͕�3�o8��x\������P�+�~5}��s��Rn��ܮ=��g�櫙���K,s��SV>C����<�?��J��S��    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR           szz�   gAMA  �ܲ�  �IDATXG�V	LUWUJYY+"X�41�Ԧ
FE��
u!.Q1�%UWЈ
���1��JP	HX"n
�UZĂ�Q�"�9�~����Mn����{��̙3wذA�3fX޼y3{Ϟ=���I��DDD�@Dox��ׯ�211�ⓜ��!���󨭭��a�q������黅���O<zV��v�ĉC2��f�]�v����>�˧���7A���1d0���uuuU�V��nݺ���9�rĈ�7�ؿ������233��n�q�mi�7PLL���������+y���b���` @����o
 �_�x�,X@���j�;v����bҤI��PSSC�W�&===>|8�~���W{ę3gJ�I� ����srr�����@IIIt��!Z�v-�F�E��TJD�
NQ.�����:;;�p��m:q�]�t�-Z$�7N@������͜9�(66V�[�l9�+ }}}�9s戇=�ÇiժU�u�V1l`` ����҄	h֬Y���I���& ���Ԃ`IW�nw,��ږ^^^ͧN��1::��ϟO;w�$��LK������L����d��
���,�V�\I%%%* �_�^Ζ?W��������ٳT]]MEEE���J:::ĪGVVVdhh(������˗�ҥKi������C~~~Lܰ�
t�ja����+.* lll|Q�7nܠM�6I�1�_KKK�7��+(  ����e�c�<�r�
���I���E�Çdmm�@KK�����oA���
�a���܉�0�l�21���r����dу��J1�h���Qcc������ϟ�G^=���˗/���ɓ'wE�Ra�F�^�����T^^N,̈́>�D�W($$��!g��gϞ!��u��~ljj"�Gy�����B��t_�~=�=Aʲ�������KHH��� ������� ����lܰ +�=��ѣG=�� $J-((H>}��xÈ\ff&����޽{e���JyB���ɓ'�Q��
��{&���}xr��i,���a�=R�y�f������t��>=~���J�?�o�>�08e�?~<͞=��o�m�UNU	yaJ RRR$�(�hmm�G�<7,!c�իW�B�t@+ f(۩S�J��9B��� �� �U�?v�X�
7:��D�UH�Ǐ���0&�έW�Øk֬�\Β�ݻw��7�,��˗/���=A�q�˰*�_4%�((�^���=*���(U�7��댌�IAA��
�:S�bֽ32�w��цH�H ��Q��	d���*�o>doo/%�T �iiiR��
��w�J�?��V�� VM9��(@H�r��i�D#�m�&�077W<o�ݻGO�>�,��p��d6l�[G4� 
���j;<E#Bޑ���ax�^��}NI-� ���R�����9l�x���%  P�޽��0���而׮]oaƑk�gI���{�m�W2�.�Pt3�
Ȅ���x��=<mqrrJd��h80��%�;y��cƌ	���*-��L�L�Ѧ���7�_����˭�[NA ?WI'7��l7`��,��N�+��8�dj��k͕�3�o8��x\������P�+�~5}��s��Rn��ܮ=��g�櫙���K,s��SV>C����<�?��J��S��    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR           szz�   gAMA  �ܲ�  wIDATXG����QƷ�#��;� ��"�/A4DAA�P�Oa!� "��#"�"� l+D�5"��|�3xw��Ό>��x~�w�^8o��⫠N��<�Nt>�5	����v;���t8>$�qk ����5�V+MC��B��)���M���%�f3M,�����n�R���&�c��X,���dB�^O�� �1��N�R�`0�v��I`�Ř���}�����1��]��3�o��"�{��*�����z�Q#�p84T��/@�բz�N�f�P�^\�F�A�R�.�@*
w�� �Z�r��]/.@�Z�L&s��P�T(�N�E����eJ&��H$<��(��F)�*x�����)S(2T� ��R  ��O���� ��������J7�n���j�E`�	6<Psp<��n�&X,�k(L�ၚ�J���r���$�f��6��4Tb����X`�
�\�x<N�C���d����|�I`��̇ ��R6�MF��	���_����e4Q���$0�@#s�'</�X}����u ��E�pņ</������C��:	,%����z�T�'���v������SQ�Bm�j�jC����FV���
    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �
  �PNG

   
IHDR   0   0   W��   gAMA  �ܲ�  
�IDAThC�Y	P��F"��b�)QT5�Q4&�&i�j4�ĥi��Z��fˌ�m�w�wdqAAQ\P�EY]@\@QP<�����x�!δ��7������9�;�.�����t���֭ڸqc��ի}����tss3�
xzz���x��c-Y��-[�ħ���:q�D���K�o�~(((�x֫��"��jժ ��F47��􈋋K�u떔��Iee����˥K�d׮]�·���7o>u��ٽ{w�����F���ξ{��h�Ç���{���:ܹ��-[6{�Ν��߫ ۷:5������a�^�*��֝ /;::>�f͚���$E�Jbb�~�پ�� �F�TTT4�������A�n�KҔ
?�o�bt��ِ���`//�Q-����4��f>�_G�-Y�pa��EDDĬ��9w��߿_П��m�nF�Pͽx� i�]]]Gzq���N�<)W�\id@~~����=�^C��2�-�:�pႠ�嶻T{fee	%[�����>!��>p�5˃��\A�&�� ����ӧ._�,��ժ_�?�9�޾LPo�����N�]���v��)((hF���Ni���������x�M�s�mۆT[ҽ���|��� $�����~Ҋ۴i��={�y�ༀY�V3֘��y/�4\�0Ϙw[l/Xoذ�����I�:,gΜ1H�7i�P<��1H�T�7�<��9����(�l4aݹsG8q5�x�%��ի�g�zrn[P6�^��G�,K���{F?�GB��nܸѢ����kkk���J%=sD%f��|z�,���V�!yT���y}I��U&���9ͥ��ׯ�(0gP�����M=�\R�Y�(�~|�1��!w4f�:z��9���:uJ(��7o*�w������'����\?i�j��'����dӚ���o�:,�Ƕ ��A&%u.t��YRR�&5�W��<�C�ׯ��dh��}P�z��dACH�������<�=�}�Z�q=��{��� �lj�ߵg�����������g$--]R�W`��"C�!'Xf�P���:2���뛻wE��I�HB�^�*33S�_�v�@z\�=�����e'���>1Q����K$##]��Zbbbd߾}\.D͟?��e����_�&gJ��"I"q��}8N\��;v(9!��*Y�xq>*ԛO��?���|�!�������3����}%vw��ݻWP��ą�*�FU�����I��Lx>�O��ǊĎ��~R�j)�zˎϭ$n]�D|�V�g(#�	wZ�6�X�"l���v�!�|��L��.ȏ�lFI�'Ȼ�۫�I�}d���O%�>~((��Rij��%99YU+,[�!7w�J6ٓ2����|%�'N��P��kӭ�}����~���Ijj��&r7�
U\\,)))*"QQQ�0��g?�Z�����<4��w��#@~Z_Y5�L�l��N��gs��qX����>��r���j�WZZ�&>��`O�'c�����6�l��|�H������޶f��X�6̶�޾��b%b9�'�`D(%�ɎFp3��vj7��^P�ɚ�!	�G�F�P��4"�C\_3���1kv��Il��$�)[3#���r�`��jD��'eJ�#�5 �F��J��>������frg�B=M/;vLE���&��`b�.���"r�k[�m��#�{^G>qj_	f&.����clxn�Z�Ո�
}9��h=���$�R�G�ƾ>a��˦Z�*	��H<oym�Lb&/�`('�H�z�5=�1� Um� �}$��<dS�#�:��|��ii �ƎLVF�`��7�r��e4�D:�L�"�F�����_O��ב/��u�U��6OXc��7s�'4B[/!:���A�
�̇��n=���פA6I���s��-�|�0�3�%%/�ʶ4�ƀݰ������"�y�߃�`�W���+UXپ�j���I_ə�'?���N�YH%�6e�v __mP�k�>��`�1�Kz�@ʋ���x�h� ��<�	��
D͹����3V�mD�G*ِ|�,��<gX���T�����Lq�h�y��3g:�2�Q�Ms�9�\ p�}�����[b��� ��1�^O�N��}���L]'��'���E��K@=�7��3����/ &%�x���3�Xf���Lo3'�n�)�������h8����E�����h�������^CI��o:"6����� ��? >� �ct�~����_<��
z<x4H��`����%�C>���ಛ�j3��Fϸ�~68��yY;�iz�><�H7 I�� �d~0*�� f5���|
P�S�d�X�@��8888�\�2{��vpQ���}�&K��z�>�<Cː[ �gYu[9�G�}oE��v x�O�_��rW5��`;�6��.�� &��� ��Ch����8�ѥ�z ��Ax�
"j� X!,��-@�S4�S��#��7 =LR
"H��~�#���1
�� ���_� #a
0o�g�������/>`�#r�Dc�	��NY��[�ڧ�>~�%�Q^��������
0Hx(��T�#�`����$NyS%F�W����2Cǎ�++
�Fm�,u��Ic��{<'�#H�&IV/�"1�?�@:�c7��u�ү�Z���o
�F��9�S���rW{����~��˶    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      q  �PNG

   
IHDR         �w=�   gAMA  �ܲ�  (IDATHK��kLSgǙ�1fɒeY�s�eɖm�O..&f�	��ΡC]��t�U.�VJ��
v�c
��P�Җrkˡ��D�"P�{[�	"�O�$����\��9�g��gaȊv�E�3�w|^�3�֌+���:��V�X��:,���x�B(B��@�Tjw��}����Y��ڵ�۶mC{�������z��FGE��9�ٳYtx��ˠO��I����,V�B:�L<x�޷O@��ç/ӻw_�w���~~j�k�[�Z6�����H>Ŭ� �X陛�M�g=�t��n|��C3g��[��gu}�꒑���4����_V�z�6=L����$Ƨ��ko��C>�3��CU��
��>��?�ۼo��p��$^�O<��Oh@R� X����|�Y��0�_��#����s��{�_	>:	p�p9�!ǖ�P�&�	:�d�/H6`��ܓ�Ep�ړ��d���!� hG؏��%���s�G
Fh�N�a��w^R�
�n�L��:�ޞ�
�����Xp�uoq��q��?��l#�rs�30eIڂd��br�>?ԅ�]�{�CN 6Qv�GB��ó�
I��C(.3yO��H̷�b�#) �h�cZ��Q�|[��3�0�����#����Dp����_��T6�;yZ���arj
O�I%�%�b3F�:^�;� 6W��x�s���T����H�T�Rډ�*�+;PUӍژ��\Ƣ7�m"��/�ܨA%m�DfCy�
�VD^�Ĺ�.�5@��QViAN�����_kF�5.%6 �C�2�k�dV���07�q��ET|£��p�����f��#K'NܤJ�zH�Q�+@���d&�6�
�&�7v��}
vi��4a�`��w��b��?E/#
�N�PQ3�������G����[PԂ{r�T����k�[A��1���kGR�g���e,V
�W��X:�;E�H��
Y��\�6���GT�
*�d��Tj�*oF�4L~"�r�W[�×��K��kC��1����iA
݇8��6r�:Ѥ�#��==}��{�ڪG�X���Hp��+N���hPX6�۹��𰶗Q
�
���hhb`�?���a0��e����'P�,
JZv���|+nf; �Π�ڄ�8%�f��ѡ�yY�Vk��xvwP˱��i"�T�*#Ԛ<jd�p���R��JՍ�Ħ�+X�8�m	a���ip#���{PWׅZ��5�Ju�Tj�\
qY+JK�(,V������7N��  0�����M��\�Ν\��_�����΀ԭ� �ygX�.!    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      O  �PNG

   
IHDR   0   0   W��   gAMA  �ܲ�  IDAThC�Z	X��6���a�Z�fay��%Sڌk�^��h�Kz�V�Z]��{,-3rWD�f�RQA@@@fXf�ufX�}�y���02�֝������r���wh��~����'L�0hҤI�'N|��o�0%((( 00�/
��{8�݋0`��ƍG]���'�+�rhs�V�Hn�b�
�r����R��!N�8�#x��	=z��|�<��xٲ��&��*
:�H�����GJ���z�ɍ�)M��%ܔ�p=W��\#RmȯpB$+/8r,b˘1c|=���n
#S�E���L�X��N �5��p�ŏ��D��"('�!�ā�j�:�X�>>>����=:�8����aM�3!U�C���"�;5`e
�M����)"UTK��Z�����l��zzV�H�D\��,�q�ѝ�aw��fw6�c	�H��βR[Q�W�t(P9��n&j�ܤw�f�Ǐ&S��.:�`�a"b�D�V�f�f;����3L��BַS	r)��Z��F~%p��������R��=�R���O��?< ��	�vh�&�s�!�j������`�3����V#��}>:.��ew�С}ą�y�b�$F��l5P��Q��Pà����+˞�P3�3A�!%8q����x�㥸��R��r�ph�@*����P�6Tl�S�~�'+Z��歟o�)yZ�*�~^��k,��*I���4�{y�<8D�ݕL
QS=tԨ�n)1b������*-mU�L�R���@Y�2�]9
����jߜLA���[j@b�?_�9K
�עa�'�(tr�!TJ*�(U�e��~8��l9Tzg���>���2�i�-�bF�[���رc�d�����+��(�0�
:`��=i2�U��:Q�J
�,�M��!Q��c����R�c�J�9�'YfEm��&��(�)�{P���.��o~����w��o��Q��5ZZo��*mLM*еkW�k�����	�6,��0���
�>��7� 1CE
�	��~�ح=n����}��T`�Ӄ3K�H��GF�$
7р+�p�1<�}&����|��3�By�v%ܖ_ǃ7҄\.��H�����_��r��*d�"�O��00+X�&���
�,���7c��p��OL	�S�����-�' WQ��r{���]��4��]J>e�E:g�K/�.�:�<��^h�e���h`��ؘDe�I�ċ�B|���t��az}�7��b�~<�C�}��Qy�!5
���O�Ԟrh��1]����ϐ鑭�#xޢu�<н{��bo��i2�pqA� ��Is��a�z�F��@"=�;F�̀y����<5��o��>S�b��=8uYJI�Q���O�{���89%���K����vl۵'�Q����wL���̂�����OR�#��<G�CF��}�Oݏ��7F�a�v��Ed�Y���.�5��.�	���sbŲ���M
<P��}�����BVQ��:�7Ƚl�Tz���b#}zON�3s�7	�G0b�Q��q)�����22Ȁ"� ���9J~<t*��w�W�����T�
�D�㑵@�[Z����<�A�N6��g�c��8tA�L2�Ǩ��'y�N�p(2����W�Ƿ��tI�� � 7�y
3�}~O������`����X�-F�v�hki�[t��6R*��L'�=)�F�)5q�)�ef��v�Fb�⟛�������H��6�v|�Y����;�$�P�������]b�()����Sp���X���/��w��,�v��AJ�������
�Ɠ��+;m���e�/q7K
+�^�������w�c�_0rET#[vC�9���(��
�#�aV�������܅ЖÈ��s��d�(p3qk*�RP��w׉��^]���/a��K� CY��J)f��B�WX�@`��Mę����t����� �������쾁g�]�1��{/�g����H�g��O��J�i��_�gf�
wc�!#G����	����	��1#ލA�{�0be,&~����J:�y.�ťL�o���#�o�@�6��?q�Z1J�t��Lr��L<�&#W'`�i��h�ja�H^C|�R�W�=B�{�&�K	���o/��Q5�s��Τ�E
�7aȻ	x�lJIPS���r�j_�u )Ga���oq}���]߻���+�J��Pm�l�Z"��^V��@N�h&���(4UTWۼ=$��ru�Q�����s�OeF\���Fk�U���.f�Ӛ�wj�6:��������8��lu�ao�3#�o���Z%\�3w�:*�pq���-�cY`�-QDܿn�l*��w셧����\����v��c箕?��ܒ�]Z=D_�����P2Z�T����`OR��>
��gGoe��/�4�	�H>�ڵ��廰�*u����?\���A��O	�es$��ٸi��';w�����V���?&*��tp1��Wp9�ЩO�U�s��v�����{ť5T5&���4<_j�K�\ᢄb>9���u�Ɵ@x���m�+Ѧm۹s����AW'3�Ș���
����.�����p[W?.��H���U��J�~�=4���	�Rw�߰{�a��s/�y���r%=Ml#��d���w����{2��"���`�F��I��ࠔC���w���G��8]�k�.ExN�5^�p�k&�~'���D�T���6;��қ`0ZR��U���Mf&k��P�V��@E�}*��g���CF�J�y��'���1ߒwX`W�@B���C^�:��/�P^N�8�K������jڅ�to,�\�4A�1��~S�LȔ�p�b�eͦ9��
�]��!�"p�����#�!��K}|�}3��~]���Y���R����EF%[�/&[#�]��9U�����K�&?;60��.o������',�'�'�"|��G�u���E�_޶C�u�;y}� �m��=�}!a*�e�?��Ā3�?������^��B�52I� B�{�I�Z�2�Ĵwч	�\�?�a�.�yE���i�̍�     IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	        �PNG

   
IHDR   0   0   W��   gAMA  �ܲ�  �IDAThC�Z	XSW��XE�u����آ����
�Tʠ���*��ֵj�����e�mm�UG�ZD��jUE@� ;$,��@���əs��y����ϋ��w��{��]�'����t���>{�왳f�z���x+  �w�̙P������jȐ!���F߉�*�((U�4�R��_aeki��:%S����u�ҥ��{������Çk�N���x�H�hR�d���u�R��l�
*�P,5@BP�@F���k 1_�"Vف_,+9s.b����Gi��5�q)���"ؓ�&$�e�j������l�C���*��Bf�
�J��������l=#�ot:qqo��fN,0@�PJ��)��2%2��!)�(��m��7�8����p?='�/�C|�-�q�����`���b�7�#��P���2c[~���h�Dn��	ݍ��s��r3??���U�Kf�H}��
Q��z�
�
tF+�M���քַ��6�ȡ�\c��lV\�y�d�����iϫX����(�AIH�'�Z�Ԍ�IИh���i�}Rք��F�
��UAz�zL|D���Q�F��dqg=kq�	T�,��!��6ߧ~�,y�B͈�D������Kߵ��ߊO��+2�S �	�	���Oj��#d���*�����o�������H)аV!���'�1��iAV'���(ɹ��F#���Ə�I��c'y	ʵ�8�)p���R�0�T��@��X�|1I�F��f�T�9�J��<O��_���R^m��ك�JFր��jȭ0�{�K淨����wN9c�ù^�4Ci��E�U�@�����VC���Lmk�.r�
�� �yz�s/x�Y%
9�+6C.�eU(��|�����Df��B����S7���[��7��!�F��L�ʨ��g� �֬2����I�w�Y"����Mg ��ϕ�ǔ��:l=�ym�Q���+�`��C���*В<z�U��%8x�ĉ&�5o~`��<�y�Q��e����2+a��G���W�/�w��zM�	�7~er3��:�Gib>�S�\]]_zD�/9�Qff�D!*P�
4V>���d���cH��?�0w�y�L�
K���*�nI&��)����j�S�>F]����.2㴅�Q�F����Å���1p9IC���s�����y��}�~��$d��:����wx�at?�X�+._���z��5(.��"�X�nD`=PG�b07+����2���%0bQ�<�|�$w�?~��E��Ъ:H�>���'��+�̀�\�����
+|y�xh�l����w��ҘYR���%z�vR�"'�Y�!��5�&K�k�Y�|7^]p�;���1�l;�%���>2H^z��%��q"�r+lp�dx*Х����#�P9�NzAPG��P�Z%�#-u=��|�.<C���?�+v˃/���V�h�$����cز���Γ��X��T$ߣ^��R�r�-�x��&�Y���!����*�*
�.9#�Ep��0Cn�g?C���}F���/'2;|���{�+��1`V�P�*@1H��A�Q
�]���G�׊��c��0��0X��U$e@�;���J�",
�LG�})���ٸも
�͒�φk����2w��C���}�8Uٔ��LjS"\�/P�+�\w��I��P�I��h�I�Q����0f�5��:w��#�G�G_�aW��2�W�����4�{9�SW�n���g����^����}ʅ�J��I,�]&l�in��.�ƃ�ٹ��\!�L	���}�_�p�0�?�N�ȫP�4��=�Еa������`¦X���
ġ�n@�7�P.7q�)B��6�0wr�
?� }z�	�~J�
P
Mi�3�!�@
~����Lޒ�
$�����'GR���R�ӎ�B�u���sǆ����q��1=�(-l���CK�������%�n�
6%��cYPYc1yħRp�r���Fth�@�v:L���PV��*� ��j�Ƴ?τi;�`���V�g���
w}�iq&�6XRT`�ƭg�x_� v(A��oٺ���`#"'�=iK���b�������:�č�3Y�Zc�1'����r}���߻���706�H�d� ��P�m�\��-q�#Wc9�����d�:�(�
������/Ub1*o�?@��/_j�<�c�HX�Չ�nn�D�<�k��J8*g�ו
]UX4ذcO4���>�� �ya�����0& �~/P�*��%Ⱥ��:��C���nK1��&;�-��Z,ja���1��a͗T������۷gϷa�
u͵��g*�����-��H��8�[�q��
�W\�v[�U�O"%�3vH�ʧ
v�n\x���TN|�.t����P��Z�]Wv0$O\�Uct�MCB�P�I��eJ0�yU�?I��G�	����wqqqY�x��xt��D�H�|R���wxv@�\@m�Q)݀��?����)�&G�E�C4J�Z�a;׺4�k�Ԑ��kJ1/���֣��+{��8�?'�#(���I"�7`�P16̫�e�aA�>/�IA��c�w(Bc��1�c�.As��9s%�)©V���V�
���M,�Z��*F��#�5�Զ�j�0�+y*�˾#�#�O	��i��BІ��1��;�aw�0D��ۋ;f�]���"��S��B1�^�`9\e�ύY�P�
��g����p�f�)h��y������eA?E �?�y㏈��7k�=~5�ͅ��������(
���FF�̄��<sĵ����kv����33��n=wị��O;�6ֶhL��ɺ'��iRf!z~�K�N�:tr��v�t���@�ELCx!�'�Y>S�+K�;!(�h�#�� (k$����
��&8?2j�i�"Dc�~RC��ӌ���� ��>�$��    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR           szz�   gAMA  �ܲ�  ^IDATXG��{T���휵vj�ڮZ��:K;����tg;�����ͺ���mg��͎��<�	�*�Hn�DC�P."��� (!�R�X�[������|��b��x�c��|��&���}����=��Θ��ñdpppkccc��{���|�
�����EEE���=�P ���z�v�ccc��刎�F||���n�����J�͆��!��bDDD���B(^|`a��Y�ٺ�\�ئ�"C!��>'>K��v��BZ����V����8�R�
���{
-�'z))W�A�H_(,5�W���J�
8�4�@1�M����dL�j���z=:::������Hd����Q��<Yt[q&M��ݦT�H~���U(7����G�_Al�{y���DTz+dF$f�czB�li�j��hjjBbbb ��!�����h��p�a *���c�t��rG�i�]y��n��#��8|I���E��	�8����F1i�D�g-���"�`��M8��;�`4�/_����j��� ��A�L��d�/_�8;c��7�9�ï'g�b��1�]CJa�|*i��X;5��6������|\�r��6p���e,�H���?�D���(��qH!�:!��~���kn
�>�`I�Ȁ,Y��(Qv��BF��t9o��?]�a��J���?��~/싳�|�s.��݋��d������ĵ����7�|K�c����=ҋ{`���� ������"|��<�����U�Z���������ʌ�{mcT��8��֫+n��"��?N/0���	�m
0a�b�f��M�p"�m<�uY�?��Z^Ϫ7Q�˷�"Ȇ��8N*��꺺�����4���1W�t ���7L��	S#�������@�Ƅ��:|�2��Uhii������/�s�t�ކ�̤�r�O�Y�4��p�@���� ��3[-e��Xu9�0�'6���e�8תBj�~DR���C�7������܏x`̮Ծ���&�wt&�-f&�q3�tm�{�8vn��,0S�����(��<�x����=CCCg޷8�I��g�q���s�/	;I�o�
퀕ĩ��N����QhBQ�%Ǻ�kw��6[���2
ֿ�_�mk���8���e
)�0�f�v�#³ǰ� U���6(����Lb��;bs.���V�$$�[9��|=�&^���;;S��$H�&(�Nʵ�R#�Q�Å&�#�R+�Km�R�ޠu�\��@?X��ZL8~n\�)���[xz`G����Q�$��f�u6����
	ev�j\Л��&4i6�<u=�F����8^׊���~հ����2ƹ�TjިQVkG�%f��)j6�n�WF<8�q�����f4(���~����Q����[���
X��p�`���tU�|v�I^���^����h �T�#2��J���3`������'5c���l`��	�*;�GQ��Zi�'��~%z]���d�hp�[���f�H8���j����&a�枆�>�@jv�s�B���N��N/
y�֎BRތ���p�'�O<���$�z�c�7�o�����ٲ��m�;��<�݄�+TR�D'��8�Y< .}7�rT�Ω��Kߡ9�+���I��<J<A�]���q�p��_��p��~}P��Qq�	�Ҏ��v�~�h��לd����v:��!S��+�h��������oM�	��}�o�g�\��&i��j�I��gA�9�BM��*@�����?g��y��U�b���m��y��h��Ļ����u"�X�I�f`��<y�IdjϘ�8��R5���n'�H�tӼ8�7�Y�/J�����.b+��'b�b%��`��� K�,b�]6h�m��J�L	(q��~e��D�s/�Κ�(���&�;�����?�߈Pb-���qP����y6q�f��	���^�l�ƥ+������*X.��'�'�B�5�F:�O����Eg��#�ĳ늬$�^�c%�H��,�.���,�3�����љ��p_	�{��l��H2QU�^6'���{Apд�+Of��b����<������zg��t��_��vLO� �{Ly��� �j���c+    IEND�B`�@    ����          QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   System.Drawing.Bitmap   Data   	      �  �PNG

   
IHDR         ��a   sRGB ���   gAMA  ���a    cHRM  z&  ��  �   ��  u0  �`  :�  p��Q<  @IDAT8O��]H�Q�ߛ�P'�͐2���^dH
q���.�bYaaR��]*�ebk&�a�7[��sM���H�O}�>��ךڇ��mƜ���N�b#ց��<�����E�V��(���G��3�GßA~��^��A�wly�՗j/�V7���3,�Y<]䡛���ȰYY{�'K��Z�ڝ�V8�~�0�8tMG0�)н�!H�in�ԫ'X�&� ��,��<^9��v�J��W������^8�I��	���^ # ���+Qӭ{|��7��x�.�����f�EB�DQhT�[�q�mF��#����J*�o���~�}%�Qyy�!.�̩���Sa�-�ГG{�n��BD<Ѹ�$�C��&�q�E�n �L~o	�AB1�?AM�]�D����ܙ.�Q�w��;���:�1.(�5���9����?�J�����?�T��j�