using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using System.Linq;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using System.Collections.Generic;
using WPF.UCs;
using VNS.Properties;
using VNS.UCs;
using Aspose.Words;
using System.Diagnostics;
using System.Drawing.Printing;
using Aspose.Words.Tables;
using Aspose.Words.Drawing;
using VNS.HIS.UI.HinhAnh;


namespace VNS.HIS.UI.Forms.HinhAnh
{
    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
    public partial class frm_quanlycacphongchucnang : Form
    {
        #region "biến thực hiện việc xử lý ảnh"

        private readonly string m_strMaDichvu = "SA";
        //private  UNCAccessWithCredentials unc = new UNCAccessWithCredentials();
        private short ObjectType_ID = -1;
        private string _rowFilter = "1=1";
        private bool b_PathImage1;
        private bool b_PathImage2;
        private Logger log;
        private DataTable m_dKcbChidinhclsChitiet = new DataTable();
        private DataTable m_dtDataForm = new DataTable();
        private DataTable m_dtFormListBookmark = new DataTable();
        private DataTable m_dtFormServiceDetail = new DataTable();

        /// <summary>
        ///     hàm thuc chiện
        /// </summary>
        /// <param name="dataTable"></param>
        private DataTable m_dtReportHinhanh = new DataTable();

        private DataTable m_dtRoleUser = new DataTable();

        private KcbLuotkham objPatientExam;
        private KcbDanhsachBenhnhan objPatientInfo;
        private string sPathServer = new KCB_HinhAnh().GetImageServerPath();
        public string sPatient_Code = "";
        private int v_id_chitietchidinh = -1;
        private int v_FormRadio_Id = -1;
        public int v_Patient_ID = -1;

        #endregion

        private readonly string sTitleReport = "";

        #region "Khởi tạo form thực hiện "

        public FTPclient FtpClient;
        public string _FtpClientCurrentDirectory;

        public string mabaocao = "SA";
        public string docChuan = "SA";
        /// <summary>
        ///     khởi tạo dữ liệu
        /// </summary>
        /// <param name="ServiceCode"></param>
        public frm_quanlycacphongchucnang(string ServiceCode)
        {
            InitializeComponent();
            log = LogManager.GetCurrentClassLogger();
            
            sTitleReport = ServiceCode;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = dtmFrom.Value;
            cboPatientSex.SelectedIndex = 0;
            m_strMaDichvu = ServiceCode;
           
            cmdExit.Click += cmdExit_Click;
            cmdPrintRadio.Click += cmdPrint_Click;
          
            cmdSave.Click += cmdSave_Click;

            LoadDataSysConfigRadio();
            cmdConfig.Visible=globalVariables.IsAdmin;

            InitEvents();
            CauHinh();
        }
        void InitEvents()
        {
            mnuBrowseImage.Click += new EventHandler(mnuBrowseImage_Click);
            mnuDeleteImage.Click += new EventHandler(mnuDeleteImage_Click);
            //txtVKS._OnEnterMe += new UCs.AutoCompleteTextbox.OnEnterMe(txtVKS__OnEnterMe);
            imgBox1._OnBrowseImage += new ImgBox.OnBrowseImage(imgBox1__OnBrowseImage);
            imgBox2._OnBrowseImage += new ImgBox.OnBrowseImage(imgBox2__OnBrowseImage);
            imgBox3._OnBrowseImage += new ImgBox.OnBrowseImage(imgBox3__OnBrowseImage);
            imgBox4._OnBrowseImage += new ImgBox.OnBrowseImage(imgBox4__OnBrowseImage);

            imgBox1._OnDeleteImage += new ImgBox.OnDeleteImage(imgBox1__OnDeleteImage);
            imgBox2._OnDeleteImage += new ImgBox.OnDeleteImage(imgBox2__OnDeleteImage);
            imgBox3._OnDeleteImage += new ImgBox.OnDeleteImage(imgBox3__OnDeleteImage);
            imgBox4._OnDeleteImage += new ImgBox.OnDeleteImage(imgBox4__OnDeleteImage);

            imgBox1._OnViewImage += imgBox__OnViewImage;
            imgBox2._OnViewImage += imgBox__OnViewImage;
            imgBox3._OnViewImage += imgBox__OnViewImage;
            imgBox4._OnViewImage += imgBox__OnViewImage;

          
            cmdBrowseMauChuan.Click += cmdBrowseMauChuan_Click;
            mnuView.Click += mnuView_Click;
            lnkMore.Click += lnkMore_Click;
            lnkSize.Click += lnkSize_Click;
            chkPreview.CheckedChanged += chkPreview_CheckedChanged;
            chkInsaukhiluu.CheckedChanged += chkInsaukhiluu_CheckedChanged;
            cmdConfig.Click += cmdConfig_Click;

            cmdDelFTPImages.Click+=cmdDelFTPImages_Click;
            
        }

        void imgBox__OnViewImage(ImgBox imgBox)
        {
            if (File.Exists(Utility.sDbnull(imgBox.Tag, "")))
                Utility.OpenProcess(Utility.sDbnull(imgBox.Tag, ""));
        }
        
        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._FTPProperties);
            _Properties.ShowDialog();
            SaveConfig();
            CauHinh();
        }

        void chkInsaukhiluu_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._FTPProperties.PrintAfterSave = chkInsaukhiluu.Checked;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }

        void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._FTPProperties.PrintPreview = chkPreview.Checked;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }

        void mnuView_Click(object sender, EventArgs e)
        {
            
        }

        void lnkSize_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            if (_Properties.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                FillDynamicValues();
            }
        }

        void lnkMore_Click(object sender, EventArgs e)
        {
            try
            {
                
                int IdChitietdichvu = Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1);

                DmucDichvuclsChitiet objDichvuchitiet = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                try
                {
                    if (objDichvuchitiet == null)
                    {
                        Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                        return;
                    }
                    frm_DynamicSetup _DynamicSetup = new frm_DynamicSetup();
                    _DynamicSetup.objDichvuchitiet = objDichvuchitiet;
                    _DynamicSetup.ImageID = -1;
                    _DynamicSetup.Id_chidinhchitiet = -1;
                    if (_DynamicSetup.ShowDialog() == DialogResult.OK)
                    {
                        FillDynamicValues();
                    }
                }
                catch (Exception)
                {

                }

            }
            catch (Exception)
            {


            }
        }

        void cmdBrowseMauChuan_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            _OpenFileDialog.Multiselect = false;
            if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMauchuan.Text = Path.GetFileName(_OpenFileDialog.FileName);
                docChuan = txtMauchuan.Text;
                new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.Columns.MauChuan).EqualTo(txtMauchuan.Text)
                    .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1)).Execute();
            }
        }

       

        void imgBox1__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic1.Tag = "";
        }
        void imgBox2__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic2.Tag = "";
        }
        void imgBox3__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic3.Tag = "";
        }
        void imgBox4__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic4.Tag = "";
        }
        void imgBox1__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic1.Tag = imgBox1.Tag;
        }
        void imgBox2__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic2.Tag = imgBox2.Tag;
        }
        void imgBox3__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic3.Tag = imgBox3.Tag;

        }
        void imgBox4__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic4.Tag = imgBox4.Tag;
        }
        void txtVKS__OnEnterMe()
        {
            try
            {
                //DmucVungkhaosat _DmucVungkhaosat = DmucVungkhaosat.FetchByID(txtVKS.MyID.ToString());
                //if (_DmucVungkhaosat != null)
                //{
                //    txtVKS.SetCode(_DmucVungkhaosat.MaKhaosat);
                //    txtsDesc.Text = Utility.sDbnull(_DmucVungkhaosat.MoTa, "");
                //    txtKetluan.Text = Utility.sDbnull(_DmucVungkhaosat.KetLuan, "");
                //    txtDenghi.Text = Utility.sDbnull(_DmucVungkhaosat.DeNghi, "");
                //}
            }
            catch
            {
            }
            
        }


        private void CauHinh()
        {
            try
            {
                chkPreview.Checked = PropertyLib._FTPProperties.PrintPreview;
                chkInsaukhiluu.Checked = PropertyLib._FTPProperties.PrintAfterSave;
                cmdGetImages.Enabled = PropertyLib._FTPProperties.Push2FTP;
                cmdDelFTPImages.Enabled = PropertyLib._FTPProperties.Push2FTP;
                FtpClient = new FTPclient(PropertyLib._FTPProperties.IPAddress, PropertyLib._FTPProperties.UID, PropertyLib._FTPProperties.PWD);
                _FtpClientCurrentDirectory = FtpClient.CurrentDirectory;
                _baseDirectory = Utility.DoTrim(PropertyLib._FTPProperties.ImageFolder);
                if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region "hàm thực hiện sự kiện của form"

        private DataTable m_dtServiceDetail = new DataTable();

        /// <summary>
        ///     tìm kiếm thông tin tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            // KcbChidinhclsChitiet.Columns.g
        }

        /// <summary>
        ///     tìm kiếm nhanh thông tin của form
        /// </summary>
        private void SearchData()
        {
            try
            {
                int id_benhnhan = -1;
                string ma_luotkham =Utility.DoTrim( txtMaluotkham_tk.Text);
                if(ma_luotkham=="") ma_luotkham="NULL";
                string ten_benhnhan = Utility.DoTrim(txtTenbenhnhan_tk.Text);
                if (ten_benhnhan == "") ten_benhnhan = "NULL";
                byte trangthai_xacnhan = 0;
                if (radChuaXacNhan.Checked) trangthai_xacnhan = (byte)0;
                if (radDaXacNhan.Checked) trangthai_xacnhan = (byte)4;
                if (radChoXacNhan.Checked) trangthai_xacnhan = (byte)3;
                m_dKcbChidinhclsChitiet =
                    SPs.HinhanhTimkiembnhan(id_benhnhan, ma_luotkham, ten_benhnhan, trangthai_xacnhan,
                    chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900", chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900"
                    , Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Utility.Int32Dbnull(cboPatientSex.SelectedValue, -1)).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(m_dKcbChidinhclsChitiet,"HINHANH.xml");
                Utility.SetDataSourceForDataGridEx(grdList, m_dKcbChidinhclsChitiet, true, true, m_strMaDichvu=="ALL"?"1=1":VKcbChidinhcl.Columns.MaDichvu+"='"+m_strMaDichvu+"'", "ten_benhnhan");
                if (grdList.RowCount > 0)
                {
                    Utility.SetMsg(lblMsg, "Mời bạn tiếp tục thực hiện công việc", false);
                    grdList.MoveFirst();
                }
                else
                {
                    Utility.SetMsg(lblMsg, "Không có dữ liệu theo điều kiện bạn chọn", true);
                }
                ModifyButtonCommand();
            }
            catch
            {
            }
        }
        public static void InphieuHA(DataTable m_dtReport,DateTime NgayIn,int coHA)
        {
            if (m_dtReport==null || m_dtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            string tieude = "", reportname = "";
            var crpt = Utility.GetReport("thamkham_InphieuHinhAnh_A4", ref tieude, ref reportname);

            // VNS.HIS.UI.BaoCao.PhieuNhapKho.CRPT_PHIEU_NHAPKHO crpt =new CRPT_PHIEU_NHAPKHO();
            frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            int id_dichvu = Utility.Int32Dbnull(Utility.GetValueOfDataRowFields(m_dtReport.Rows[0], "id_dichvu", -1),-1);
            DmucDichvucl _DmucDichvucl = DmucDichvucl.FetchByID(id_dichvu);
            if (_DmucDichvucl != null) tieude = _DmucDichvucl.MotaThem;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {

                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thanhtoan_Hoadondo";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);

                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "coHA", coHA );
                //Utility.SetParameterValue(crpt, "ketqua", m_dtReport.Rows[0][VKcbChidinhcl.Columns.KetQua].ToString());
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }
        private void chkBydate_CheckedChanged(object sender, EventArgs e)
        {
            dtmFrom.Enabled = chkByDate.Checked;
            dtmTo.Enabled = chkByDate.Checked;
        }

      
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_quanlycacphongchucnang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrintRadio.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F5) BeginExam();
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl) Utility.ShowMsg(this.ActiveControl.Name);
            
        }


        /// <summary>
        ///     hàm thực hiện việc hiên thị các nút thông tin của trạng thía
        /// </summary>
        private void ModifyButtonCommand()
        {
            try
            {

                toolChooseBN.Enabled = Utility.isValidGrid(grdList);
                toolPrintRadio.Enabled = Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value)>=3;

                toolAccept.Enabled = Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) ==3 ;
                toolUnAccept.Enabled =  Utility.isValidGrid(grdList) && Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) == 4 ;
                toolUnAccept.Visible = globalVariables.IsAdmin;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     thực hienj việc load thông tin của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_quanlycacphongchucnang_Load(object sender, EventArgs e)
        {
            //SearchFormRadio();
            //RoleConfigUserRadio();
            InitData();
           
            ModifyCommand();
            log = LogManager.GetCurrentClassLogger();

            log.Trace("Form Load OK");
            TabInfo.SelectedTab = tabDanhsach;
            txtMaluotkham.Focus();
            txtMaluotkham.SelectAll();

            //  cmdSearchListRadio.PerformClick();
        }

        /// <summary>
        ///     hàm thực hiện việc load thông tin của phần config hình ảnh
        /// </summary>
        private void LoadDataSysConfigRadio()
        {
            SysConfigRadio objConfigRadio = SysConfigRadio.FetchByID(1);
            if (objConfigRadio != null)
            {
               PropertyLib._FTPProperties.UNCPath = Utility.sDbnull(objConfigRadio.PathUNC, "");
               PropertyLib._FTPProperties.PWD = Utility.sDbnull(objConfigRadio.PassWord, "");
               PropertyLib._FTPProperties.IPAddress = Utility.sDbnull(objConfigRadio.Domain, "");
               PropertyLib._FTPProperties.UID = Utility.sDbnull(objConfigRadio.UserName, "");
            }
        }

      
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_TabIndexChanged(object sender, EventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_ChangingSelectedTab(object sender, TabCancelEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {
            ModifyButtonCommand();
        }

       

       

        /// <summary>
        ///     hàm thực hiện chọn thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            if (e.Column.Key == "colChooseBN")
            {
                BeginExam();
            }
            else
            {
                txtMaluotkham.Text = "";
            }

        }

        private void BeginExam()
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                TabInfo.SelectedTab = TabInfo.TabPages[0];
                Application.DoEvents();
                DataRowView dr = grdList.CurrentRow.DataRow as DataRowView;
                //Fill Infor
                Utility.SetMsg(lblMsg, "Đang nạp thông tin bệnh nhân...", false);
                txtMaluotkham.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MaLuotkham], "");
                txtTenBN.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtIdBenhnhan.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.IdBenhnhan], "");
                txtGioitinh.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.GioiTinh], "");
                txtTuoi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.Tuoi], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
                txtObjectType_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenDoituongKcb], "");

                txtDiachiBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiachiBhyt], "");
                txtSoBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MatheBhyt], "");
                txtPtram.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.PtramBhyt], "");
                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.NgayketthucBhyt], "");
                ResetImages();
                //Fill Detail
                txtIdKham.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.IdKham], "");
                txtidchidinhchitiet.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.IdChitietchidinh], "");
                txtIdDichvuChitiet.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.IdChitietdichvu], "");
                txtTendichvu.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.TenChitietdichvu], "");
                txtPhongchidinh.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.TenPhongchidinh], "");
                txtBSChidinh.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.TenBacsiChidinh], "");
                txtChanDoan.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.ChanDoan], "");
                //txtGhiChu.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.MotaThem], "");

                chkXacnhan.Checked = Utility.ByteDbnull(dr[VKcbChidinhcl.Columns.TrangThai], 0) == 4;
                //try2DelImageOnFTPFolder();
                FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
                CheckImages(dr.Row);
                Utility.SetMsg(lblMsg, "Đang download ảnh từ FTP...", false);
                LoadImage(imgBox1, Utility.sDbnull(dr["Local1"], ""), Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath1], ""));
                LoadImage(imgBox2, Utility.sDbnull(dr["Local2"], ""), Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath2], ""));
                LoadImage(imgBox3, Utility.sDbnull(dr["Local3"], ""), Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath3], ""));
                LoadImage(imgBox4, Utility.sDbnull(dr["Local4"], ""), Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath4], ""));
                pic1.Tag = imgBox1.Tag;
                pic2.Tag = imgBox2.Tag;
                pic3.Tag = imgBox3.Tag;
                pic4.Tag = imgBox4.Tag;
                DmucDichvuclsChitiet objDichvuchitiet = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1));
                if (objDichvuchitiet != null)
                {
                    txtMauchuan.Text = objDichvuchitiet.MauChuan;
                    docChuan = txtMauchuan.Text;
                }
                else
                {
                    txtMauchuan.Clear();
                    docChuan = txtMauchuan.Text;
                }
                FillDynamicValues();
                new KCB_HinhAnh().UpdateXacNhanDaThucHien(v_id_chitietchidinh, 2);
                ModifyButtonAssignDetail_Status();
                FocusMe(flowDynamics);
            }
            catch (Exception exception)
            {
            }
            finally
            {
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                this.Cursor = Cursors.Default;
            }
        }
        void FillDynamicValues()
        {
            try
            {
                flowDynamics.Controls.Clear();

                DataTable dtData = clsHinhanh.GetDynamicFieldsValues(Utility.Int32Dbnull(txtIdDichvuChitiet.Text), "", "", -1, Utility.Int32Dbnull(txtidchidinhchitiet.Text));
                
                foreach (DataRow dr in dtData.Select("1=1","Stt_hthi"))
                {
                    dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(txtidchidinhchitiet.Text);
                    ucDynamicParam _ucTextSysparam = new ucDynamicParam(dr,true);

                    _ucTextSysparam.TabStop = true;
                    _ucTextSysparam._OnEnterKey += _ucTextSysparam__OnEnterKey;
                    _ucTextSysparam.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt],0);
                    
                    _ucTextSysparam.Init();
                    if (Utility.Byte2Bool(dr[DynamicField.Columns.Rtxt]))
                    {
                        _ucTextSysparam.Size = PropertyLib._DynamicInputProperties.RtfDynamicSize;
                        _ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.RtfTextSize;
                        _ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.RtfLabelSize;
                    }
                    else
                    {
                        _ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
                        _ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
                        _ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
                    }
                    flowDynamics.Controls.Add(_ucTextSysparam);
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }

        void _ucTextSysparam__OnEnterKey(ucDynamicParam obj)
        {
            try
            {
                if (!obj._AcceptTab)
                {
                    int _idx = -1;
                    var q = (from p in flowDynamics.Controls.Cast<ucDynamicParam>().AsEnumerable()
                             where p.TabIndex > obj.TabIndex
                             select p.TabIndex);
                    if (q.Count() > 0)
                        _idx = q.Min();
                    if (_idx > 0)
                    {
                        foreach (ucDynamicParam ucs in flowDynamics.Controls)
                        {
                            if (ucs.TabIndex == _idx)
                            {
                                ucs.FocusMe();
                                return;
                            }
                        }
                    }
                    else//Last Controls
                        cmdSave.Focus();
                }
            }
            catch (Exception)
            {
                
            }
        }
        void CheckImages(DataRow dr)
        {
            try
            {
                if (Utility.Byte2Bool(dr[KcbChidinhclsChitiet.Columns.FTPImage]))
                {
                    List<string> lstImgFiles = new List<string>();

                    string PACS_SHAREDFOLDER = VNS.Libs.THU_VIEN_CHUNG.Laygiatrithamsohethong("PACS_SHAREDFOLDER", "", true);
                    string PACS_IMAGEREPLACEPATH = VNS.Libs.THU_VIEN_CHUNG.Laygiatrithamsohethong("PACS_IMAGEREPLACEPATH", "", true);
                    FtpClient.CurrentDirectory = string.Format("{0}{1}", _FtpClientCurrentDirectory,
                               txtidchidinhchitiet.Text);

                    string _strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], "");
                    string serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    string localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text, Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], ""));// Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), _baseDirectory.ToUpper());

                    if (_strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], ""));
                        dr["Local1"] = localfile;
                    }
                    _strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], "");
                    serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text, Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], ""));

                    if (_strfile != "")
                    {
                        // if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], ""));
                        dr["Local2"] = localfile;
                    }
                    _strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], "");
                    serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text, Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], ""));

                    if (_strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], ""));
                        dr["Local3"] = localfile;
                    }
                    _strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], "");
                    serverFile = Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), PACS_SHAREDFOLDER.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text, Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], ""));

                    if (_strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], ""));
                        dr["Local4"] = localfile;
                    }
                }
                else
                {
                    dr["Local1"] = dr[KcbChidinhclsChitiet.Columns.ImgPath1];
                    dr["Local2"] = dr[KcbChidinhclsChitiet.Columns.ImgPath2];
                    dr["Local3"] = dr[KcbChidinhclsChitiet.Columns.ImgPath3];
                    dr["Local4"] = dr[KcbChidinhclsChitiet.Columns.ImgPath4];
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
      
       
        /// <summary>
        ///     hàm thực hiện việc thông tin status
        /// </summary>
        private void ModifyButtonAssignDetail_Status()
        {
            try
            {
                if (globalVariables.UserName != "ADMIN")
                {
                    cmdSaveAndAccept.Enabled =
                        Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value, 0) <= 0;
                    cmdSave.Enabled =
                        Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value, 0) <= 0;
                    //chkXacnhan.Checked = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value, 0) >= 1;
                    
                }
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        ///     hàm thực hiện làm sách đường dẫn
        /// </summary>
        private void ResetImages()
        {
            imgBox1._img.Source = null;
            imgBox1.Tag = "";

            imgBox2._img.Source = null;
            imgBox2.Tag = "";

            imgBox3._img.Source = null;
            imgBox3.Tag = "";

            imgBox4._img.Source = null;
            imgBox4.Tag = "";
        }

        /// <summary>
        ///     hàm thực hiện việc load thông tin của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetPatient_Click(object sender, EventArgs e)
        {
            TabInfo.SelectedTab = TabInfo.TabPages[1];
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
        }

        private void toolAccept_Click(object sender, EventArgs e)
        {
            try
            {

                v_id_chitietchidinh = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);

                ActionResult actionResult =
                    new KCB_HinhAnh().UpdateXacNhanDaThucHien(
                        v_id_chitietchidinh, 4);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value = 4;
                        grdList.CurrentRow.Cells["ten_trangthai"].Value = GetAsssignDetailStatus(4);
                        grdList.CurrentRow.EndEdit();
                        grdList.UpdateData();
                        grdList.Refresh();
                        m_dKcbChidinhclsChitiet.AcceptChanges();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xác nhận", "Thông báo", MessageBoxIcon.Error);
                        break;
                }


                ModifyButtonAssignDetail_Status();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc in phiếu kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolPrintRadio_Click(object sender, EventArgs e)
        {
            //v_id_chitietchidinh = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
            //switch (BusinessHelper.GetAccountName())
            //{
            //    case "YHOCHAIQUAN":
            //        YHHQ_PrintDataRadio(v_id_chitietchidinh);
            //        break;
            //    case "KYDONG":
            //        KYDONG_PrintDataRadio(v_id_chitietchidinh);
            //        break;
            //}
        }
            
        /// <summary>
        ///     hàm thực hiện viêc hủy bỏ kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolUnAccept_Click(object sender, EventArgs e)
        {
            if (globalVariables.UserName == "ADMIN" || Utility.sDbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiThuchien].Value, "") == globalVariables.UserName)
            {

                ActionResult actionResult =
                    new KCB_HinhAnh().UpdateXacNhanDaThucHien(
                        v_id_chitietchidinh, 3);//Trạng thái đang nhập kết quả

                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value = 3;
                        grdList.CurrentRow.Cells["ten_trangthai"].Value = GetAsssignDetailStatus(3);
                        grdList.CurrentRow.EndEdit();
                        grdList.UpdateData();
                        grdList.Refresh();
                        m_dKcbChidinhclsChitiet.AcceptChanges();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xác nhận", "Thông báo", MessageBoxIcon.Error);
                        break;
                }

            }
            else
            {
                Utility.ShowMsg("Kết quả này được xác nhận bởi bác sĩ khác nên bạn không được phép hủy hoặc thay đổi. Muốn thay đổi bạn cần đăng nhập là Admin hoặc liên hệ bác sĩ xác nhận kết quả này");
                return;
            }

        }

        /// <summary>
        ///     nhan chuột phải thực hiện việc xử lý thông tin của phần chuẩn đoán đưa bệnh nhân vào chẩn đoán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolChooseBN_Click(object sender, EventArgs e)
        {
            // if (!InValiRadio()) return;
            BeginExam();
        }

        #endregion

      

        #region "Khu vực xử lý thông tin ảnh"

        private  string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
            "Radio\\");

        private readonly string path = Application.StartupPath;
        private ActionResult _actionResult = ActionResult.Error;
        private DataTable m_dtStaff = new DataTable();
        private KcbChidinhclsChitiet objAssignDetail = new KcbChidinhclsChitiet();
        private string sFilter = "BMP|*.BMP|JPG|*jpg|PNG|*.PNG|GIF|*.Gif";

        // private Logger log;
        string UploadFile(string sourcePath, string sFileName, int IntOrder)
        {


            try
            {
                if (Utility.DoTrim(sourcePath) == "" || !File.Exists(sourcePath)) return "";
                string FileName = "";
                string NewDirName = txtidchidinhchitiet.Text;
                string FtpCurrentDirectory = _FtpClientCurrentDirectory + NewDirName;
                if (!FtpClient.FtpDirectoryExists(FtpCurrentDirectory))
                    FtpClient.FtpCreateDirectory(FtpCurrentDirectory);
                FileName = Guid.NewGuid() + Path.GetExtension(sFileName);
                string UploadDirectory = string.Format("{0}/{1}", FtpCurrentDirectory, FileName);
                FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
                FtpClient.Upload(sourcePath, UploadDirectory);
                return FileName;
            }
            catch
            {
                return "";
            }


        }
        void try2DelImageOnFTPFolder()
        {
            if (!Directory.Exists(_baseDirectory)) return;
            foreach (string file in Directory.GetFiles(_baseDirectory))
            {
                try
                {
                    File.Delete(file);

                }
                catch
                {
                }
            }
        }
        /// <summary>
        ///     khởi tạo thông tin đối tượng chi tiết
        /// </summary>
        /// <returns></returns>
        private KcbChidinhclsChitiet CreateAssignDetail()
        {
            try
            {
                Utility.SetMsg(lblMsg, "",false);
                KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                objKcbChidinhclsChitiet.FTPImage = Utility.Bool2byte(PropertyLib._FTPProperties.Push2FTP);
                if (chkSaveImg.Checked )
                {
                    if (PropertyLib._FTPProperties.Push2FTP)
                    {
                        Utility.SetMsg(lblMsg, "Đang xóa ảnh khỏi FTP...", false);
                        try2DelImageOnFTP(objKcbChidinhclsChitiet.ImgPath1);
                        try2DelImageOnFTP(objKcbChidinhclsChitiet.ImgPath2);
                        try2DelImageOnFTP(objKcbChidinhclsChitiet.ImgPath3);
                        try2DelImageOnFTP(objKcbChidinhclsChitiet.ImgPath4);

                        string local1 = objKcbChidinhclsChitiet.ImgPath1;
                        string local2 = objKcbChidinhclsChitiet.ImgPath2;
                        string local3 = objKcbChidinhclsChitiet.ImgPath3;
                        string local4 = objKcbChidinhclsChitiet.ImgPath4;

                        Utility.SetMsg(lblMsg, "Đang cập nhật lại ảnh trên FTP...", false);
                        objKcbChidinhclsChitiet.ImgPath1 = UploadFile(Utility.sDbnull(imgBox1.Tag, ""), Utility.sDbnull(imgBox1.Tag, ""), 1);
                        objKcbChidinhclsChitiet.ImgPath2 = UploadFile(Utility.sDbnull(imgBox2.Tag, ""), Utility.sDbnull(imgBox2.Tag, ""), 2);
                        objKcbChidinhclsChitiet.ImgPath3 = UploadFile(Utility.sDbnull(imgBox3.Tag, ""), Utility.sDbnull(imgBox3.Tag, ""), 3);
                        objKcbChidinhclsChitiet.ImgPath4 = UploadFile(Utility.sDbnull(imgBox4.Tag, ""), Utility.sDbnull(imgBox4.Tag, ""), 4);

                        Utility.SetMsg(lblMsg, "Đang cập nhật ảnh trên Local...", false);
                        try2CopyImages2Local(Utility.sDbnull(imgBox1.Tag, ""), objKcbChidinhclsChitiet.ImgPath1, ref localpath1);
                        try2CopyImages2Local(Utility.sDbnull(imgBox2.Tag, ""), objKcbChidinhclsChitiet.ImgPath2, ref localpath2);
                        try2CopyImages2Local(Utility.sDbnull(imgBox3.Tag, ""), objKcbChidinhclsChitiet.ImgPath3, ref localpath3);
                        try2CopyImages2Local(Utility.sDbnull(imgBox4.Tag, ""), objKcbChidinhclsChitiet.ImgPath4, ref localpath4);

                        imgBox1.Tag = localpath1;
                        imgBox2.Tag = localpath2;
                        imgBox3.Tag = localpath3;
                        imgBox4.Tag = localpath4;
                        Utility.SetMsg(lblMsg, "Đang xóa ảnh khỏi Local...", false);
                        try2DelImageOnLocal(local1);
                        try2DelImageOnLocal(local2);
                        try2DelImageOnLocal(local3);
                        try2DelImageOnLocal(local4);
                    }
                    else
                    {
                        localpath1 = Utility.sDbnull(imgBox1.Tag, "");
                        localpath2 = Utility.sDbnull(imgBox2.Tag, "");
                        localpath3 = Utility.sDbnull(imgBox3.Tag, "");
                        localpath4 = Utility.sDbnull(imgBox4.Tag, "");

                        objKcbChidinhclsChitiet.ImgPath1 = Utility.sDbnull(imgBox1.Tag, "");
                        objKcbChidinhclsChitiet.ImgPath2 = Utility.sDbnull(imgBox2.Tag, "");
                        objKcbChidinhclsChitiet.ImgPath3 = Utility.sDbnull(imgBox3.Tag, "");
                        objKcbChidinhclsChitiet.ImgPath4 = Utility.sDbnull(imgBox4.Tag, "");
                    }
                }
                else
                {
                    //objKcbChidinhclsChitiet.ImgPath1 = Utility.sDbnull(imgBox1.Tag, "");
                    //objKcbChidinhclsChitiet.ImgPath2 = Utility.sDbnull(imgBox2.Tag, "");
                    //objKcbChidinhclsChitiet.ImgPath3 = Utility.sDbnull(imgBox3.Tag, "");
                    //objKcbChidinhclsChitiet.ImgPath4 = Utility.sDbnull(imgBox4.Tag, "");
                }

                pic1.Tag = imgBox1.Tag;
                pic2.Tag = imgBox2.Tag;
                pic3.Tag = imgBox3.Tag;
                pic4.Tag = imgBox4.Tag;
                objKcbChidinhclsChitiet.TrangThai = (byte?)(chkXacnhan.Checked ? 4 : 3);
                objKcbChidinhclsChitiet.NguoiThuchien = globalVariables.UserName;
                objKcbChidinhclsChitiet.NgayThuchien = globalVariables.SysDate;
                return objKcbChidinhclsChitiet;
            }
            catch(Exception ex)
            {
                return null;
                Utility.CatchException("Lỗi khi lưu kết quả hình ảnh",ex);
            }
        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (!HasValue(flowDynamics))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập ít nhất một kết quả trước khi Lưu", true);
                FocusMe(flowDynamics);
                return false;
            }
            //if (Utility.DoTrim(txtsDesc.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Mô tả không được bỏ trống", true);
            //    txtsDesc.Focus();
            //    return false;
            //}
            //if (Utility.DoTrim(txtKetluan.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Kết luận không được bỏ trống", true);
            //    txtKetluan.Focus();
            //    return false;
            //}
           
            return true;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
           
            if (!isValidData()) return;
            if (SaveResult())
                if (chkInsaukhiluu.Checked)
                    cmdPrintRadio_Click(cmdPrintRadio, e);
        }
        string localpath1 = "";
        string localpath2 = "";
        string localpath3 = "";
        string localpath4 = "";
        bool SaveResult()
        {
            try
            {
                localpath1 = "";
                localpath2 = "";
                localpath3 = "";
                localpath4 = "";
                KcbChidinhclsChitiet objAssignDetail = CreateAssignDetail();
                if (objAssignDetail == null) return false;
                SaveNow(flowDynamics);
                _actionResult = new KCB_HinhAnh().PerformActionUpdate(objAssignDetail);
                if (_actionResult == ActionResult.Success)
                    UpdateDataTable(objAssignDetail);
                else
                    return false;
                return true;
                //SetStatusMessage();
            }
            catch
            {
                return false;
            }
            finally
            {
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                chkSaveImg.Checked = false;
            }
        }
        void InitNow(FlowLayoutPanel pnlParent)
        {
            foreach (ucDynamicParam ctrl in pnlParent.Controls)
            {
                ctrl.Init();
            }
        }
        void FocusMe(FlowLayoutPanel pnlParent)
        {
            try
            {
                if (pnlParent.Controls.Count > 0)
                {
                    ((ucDynamicParam)pnlParent.Controls[0]).FocusMe();
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }
        bool HasValue(FlowLayoutPanel pnlParent)
        {
            foreach (ucDynamicParam ctrl in pnlParent.Controls)
            {
                if (Utility.DoTrim(ctrl._Giatri) != "")
                    return true;
            }
            return false;
        }
        void SaveNow(FlowLayoutPanel pnlParent)
        {
            foreach (ucDynamicParam ctrl in pnlParent.Controls)
            {
                if (!ctrl.isSaved)
                    ctrl.Save();
            }
        }
        void UpdateDataTable(KcbChidinhclsChitiet objAssignDetail)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;

                DataRow dr = m_dKcbChidinhclsChitiet.Select(VKcbChidinhcl.Columns.IdChitietchidinh + "=" + txtidchidinhchitiet.Text)[0];
                dr[VKcbChidinhcl.Columns.ImgPath1] = objAssignDetail.ImgPath1;
                dr[VKcbChidinhcl.Columns.ImgPath2] = objAssignDetail.ImgPath2;
                dr[VKcbChidinhcl.Columns.ImgPath3] = objAssignDetail.ImgPath3;
                dr[VKcbChidinhcl.Columns.ImgPath4] = objAssignDetail.ImgPath4;
                if (PropertyLib._FTPProperties.Push2FTP)
                {
                    dr["Local1"] = localpath1;
                    dr["Local2"] = localpath2;
                    dr["Local3"] = localpath3;
                    dr["Local4"] = localpath4;
                }
                else
                {
                    dr["Local1"] = objAssignDetail.ImgPath1;
                    dr["Local2"] = objAssignDetail.ImgPath2;
                    dr["Local3"] = objAssignDetail.ImgPath3;
                    dr["Local4"] = objAssignDetail.ImgPath4;
                }
             

                dr[KcbChidinhclsChitiet.Columns.TrangThai] = chkXacnhan.Checked ? 4 : 3;
                dr["ten_trangthai"] = GetAsssignDetailStatus(chkXacnhan.Checked ? 4 : 3);
                m_dKcbChidinhclsChitiet.AcceptChanges();
            }
            catch
            {
            }
        }
        private void InitData()
        {
            try
            {
                //DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi();
                //VNS.Libs.DataBinding.BindDataCombobox(this.cboDoctorAssign, data, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---",true);
                //if (globalVariables.gv_intIDNhanvien <= 0)
                //{
                //    if (this.cboDoctorAssign.Items.Count > 0)
                //    {
                //        this.cboDoctorAssign.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    this.cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(this.cboDoctorAssign, globalVariables.gv_intIDNhanvien.ToString());
                //}
                DataBinding.BindDataCombobox(this.cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Đối tượng KCB---",true);
            }
            catch
            {
            }
        }

        private void SetStatusMessage()
        {
            switch (_actionResult)
            {
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ", "Thông báo", MessageBoxIcon.Error);
                    break;
                case ActionResult.Success:

                    DataRow[] arrDr =
                        m_dKcbChidinhclsChitiet.Select("id_chitietchidinh=" +
                                                Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                    if (chkXacnhan.Checked)
                    {
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0][KcbChidinhclsChitiet.Columns.TrangThai] = chkXacnhan.Checked ? 4 : 3;
                            arrDr[0]["ten_trangthai"] = GetAsssignDetailStatus(chkXacnhan.Checked ? 4 : 3);
                        }
                    }
                    arrDr[0][KcbChidinhclsChitiet.Columns.NguoiThuchien] = globalVariables.UserName;
                    arrDr[0][KcbChidinhclsChitiet.Columns.NgayThuchien] = globalVariables.SysDate;
                    m_dKcbChidinhclsChitiet.AcceptChanges();

                    Utility.SetMsg(lblMsg, "Cập nhập thông tin thành công", true);
                    break;
            }
        }

        private void SetStatusMessageAndPrint()
        {
            switch (_actionResult)
            {
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ", "Thông báo");
                    break;
                case ActionResult.Success:
                    if (chkXacnhan.Checked)
                    {
                        DataRow[] arrDr =
                            m_dKcbChidinhclsChitiet.Select("id_chitietchidinh=" +
                                                    Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0][KcbChidinhclsChitiet.Columns.TrangThai] = chkXacnhan.Checked ? 4 : 3;
                            arrDr[0]["ten_trangthai"] = GetAsssignDetailStatus(chkXacnhan.Checked ? 4 : 3);
                        }
                        m_dKcbChidinhclsChitiet.AcceptChanges();
                    }
                    Utility.SetMsg(lblMsg, "Cập nhập thông tin thành công", true);
                    cmdPrintRadio.PerformClick();
                    break;
            }
        }

        private string GetAsssignDetailStatus(int AssginDetail_Status)
        {
            string AssginDetailStatus_Name = "Chưa thực hiện";
            switch (AssginDetail_Status)
            {
                case 0:
                    AssginDetailStatus_Name = "Chưa thực hiện";
                    break;
                case 1:
                    AssginDetailStatus_Name = "Đã chuyển CLS";
                    break;
                case 2:
                    AssginDetailStatus_Name = "Đang thực hiện";
                    break;
                case 3:
                    AssginDetailStatus_Name = "Đã có kết quả";
                    break;
                case 4:
                    AssginDetailStatus_Name = "Đã xác nhận";
                    break;
            }
            return AssginDetailStatus_Name;
        }

        

        void LoadImage(WPF.UCs.ImgBox pImage,string mylocalImage,string imgPath)
        {
            try
            {
                if (File.Exists(mylocalImage) && !m_blnForced2GetImagesFromFTP)
                {
                    pImage.fullName = mylocalImage;
                    pImage.LoadIMg();
                    pImage.Tag = mylocalImage;
                    return;
                }
                if (!string.IsNullOrEmpty(imgPath))
                {
                    if (!PropertyLib._FTPProperties.IamLocal || m_blnForced2GetImagesFromFTP)
                    {
                        FtpClient.CurrentDirectory = string.Format("{0}{1}", _FtpClientCurrentDirectory,
                            txtidchidinhchitiet.Text);
                        if (FtpClient.FtpFileExists(FtpClient.CurrentDirectory + imgPath))
                        {
                            string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                            txtidchidinhchitiet.Text, imgPath);
                            Utility.CreateFolder(sPath1);
                            FtpClient.Download(imgPath, sPath1, true);
                            pImage.fullName = sPath1;
                            pImage.LoadIMg();
                            pImage.Tag = sPath1;
                            log.Trace("load anh thu 1");
                        }
                        else
                        {
                            pImage.fullName = path + @"\Path\noimage.jpg";
                            pImage.LoadIMg();
                            pImage.Tag = "";
                        }
                    }
                    else//Ảnh trên chính máy tính này
                    {
                        pImage.fullName = imgPath;
                        pImage.LoadIMg();
                        pImage.Tag = imgPath;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (pImage._img.Source == null)
                {
                    pImage.fullName = path + @"\Path\noimage.jpg";
                    pImage.LoadIMg();
                    pImage.Tag = "";
                }
            }
        }
        /// <summary>
        ///     hàm thực hiện in phiếu ra ngoài
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            InPhieuHinhAnh(Utility.Int32Dbnull(txtidchidinhchitiet.Text));
        }
        void mnuDeleteImage_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    imgBoxtureBox imgBox = ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl as imgBoxtureBox;

            //    imgBox.Image = null;
            //    imgBox.Tag = "";
           

            //}
            //catch
            //{
            //}
        }
        void try2DelImageOnFTP(string imgPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    FtpClient.CurrentDirectory = string.Format("{0}{1}", _FtpClientCurrentDirectory,
                        txtidchidinhchitiet.Text);
                    if (FtpClient.FtpFileExists(FtpClient.CurrentDirectory + imgPath))
                    {
                        FtpClient.FtpDelete(imgPath);
                        
                    }
                    else
                    {
                       
                    }
                   
                }

            }
            catch
            {
            }
        }
        void try2DelImageOnLocal(string imgPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    
                    string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                           txtidchidinhchitiet.Text, imgPath);
                    Utility.CreateFolder(sPath1);
                    if (File.Exists(sPath1))
                    {
                        File.Delete(sPath1);

                    }
                }

            }
            catch
            {
            }
        }
        void try2CopyImages2Local(string _source, string des, ref string localpath)
        {
            try
            {
                if (!string.IsNullOrEmpty(_source) && !string.IsNullOrEmpty(des))
                {

                    string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                           txtidchidinhchitiet.Text, des);
                    localpath = sPath1;
                    Utility.CreateFolder(sPath1);
                    if (File.Exists(_source))
                    {
                        File.Copy(_source, sPath1);

                    }
                }

            }
            catch
            {
            }
        }
        void mnuBrowseImage_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ImgBox imgBox=null;// =(ImgBox) ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl ;
            //    OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            //    _OpenFileDialog.Multiselect = false;
            //    if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        imgBox.fullName = _OpenFileDialog.FileName;
            //        imgBox.LoadIMg();
            //        imgBox.Tag = _OpenFileDialog.FileName; //Guid.NewGuid() + "." + Path.GetExtension(_OpenFileDialog.FileName) + "|" + _OpenFileDialog.FileName;
            //    }
            //}
            //catch
            //{
            //}
        }
     

        private Image GetThumbnail(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (size.Width/(float) sourceWidth);
            nPercentH = (size.Height/(float) sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            var destWidth = (int) (sourceWidth*nPercent);
            var destHeight = (int) (sourceHeight*nPercent);

            var b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }


        /// <summary>
        ///     hàm thực hiện trạng thái của các nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
                cmdSaveAndAccept.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
                cmdPrintRadio.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
                cmdSaveBookMark.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
               
            }
            catch (Exception exception)
            {
                log.Error("loi trang thai cua cac nut :" + exception);
            }
        }

        #region "hàm thực hiện in phieus của phần y học hải quân"

        /// <summary>
        ///     hàm thwucj hiện in phiếu y học hải quân
        /// </summary>
        /// <param name=KcbChidinhclsChitiet.Columns.IdChitietchidinh></param>
        private void YHHQ_PrintDataRadio(int id_chitietchidinh)
        {
            //m_dtReportHinhanh =
            //    SPs.YhhqPhieuHinhAnh(id_chitietchidinh).GetDataSet().Tables[0];
            //if (m_dtReportHinhanh.Rows.Count <= 0)
            //{
            //    Utility.ShowMsg("Không tìm thấy thông tin của bản ghi", "Thông báo");
            //    return;
            //}
            //// string sTitle = "PHIẾU KẾT QUẢ SIÊU ÂM";
            //switch (sTitleReport)
            //{
            //    case "SA":
            //        YHHQ_ProcessData_COHINHANH(ref m_dtReportHinhanh);
            //        YHHQ_Inphieu_SIEUAM("PHIẾU KẾT QUẢ SIÊU ÂM");
            //        break;
            //    case "XQ":
            //        YHHQ_ProcessData_KHONGHINHANH(ref m_dtReportHinhanh);
            //        YHHQ_Inphieu_XQUANG("PHIẾU KẾT QUẢ XQUANG");
            //        break;
            //    case "NS":
            //        YHHQ_ProcessData_COHINHANH(ref m_dtReportHinhanh);
            //        YHHQ_Inphieu_NOISOI("PHIẾU KẾT QUẢ NỘI SOI");
            //        break;
            //    case "DT":
            //        YHHQ_ProcessData_KHONGHINHANH(ref m_dtReportHinhanh);
            //        YHHQ_Inphieu_DIENTIM("PHIẾU KẾT QUẢ ĐIỆN TIM");
            //        break;
            //    default:
            //        YHHQ_ProcessData_COHINHANH(ref m_dtReportHinhanh);
            //        Inphieu_XQUANG("PHIẾU KẾT QUẢ");
            //        break;
            //}
        }

      

      

        /// <summary>
        ///     hàm thực hiện viec xem có in hình ảnh không
        /// </summary>
        /// <param name="dataTable"></param>
        private void YHHQ_ProcessData_COHINHANH(ref DataTable dataTable)
        {
            //if (!m_dtReportHinhanh.Columns.Contains("VungKS")) m_dtReportHinhanh.Columns.Add("VungKS", typeof (string));
            //if (!m_dtReportHinhanh.Columns.Contains("ChanDoan"))
            //    m_dtReportHinhanh.Columns.Add("ChanDoan", typeof (string));
            //if (m_dtReportHinhanh.Columns.Contains("sPathImage1")) m_dtReportHinhanh.Columns.Remove("sPathImage1");
            //if (m_dtReportHinhanh.Columns.Contains("sPathImage2")) m_dtReportHinhanh.Columns.Remove("sPathImage2");
            //if (!m_dtReportHinhanh.Columns.Contains("sPathImage1"))
            //    m_dtReportHinhanh.Columns.Add("sPathImage1", typeof (byte[]));
            //if (!m_dtReportHinhanh.Columns.Contains("sPathImage2"))
            //    m_dtReportHinhanh.Columns.Add("sPathImage2", typeof (byte[]));
            //if (BusinessHelper.IsHinhHanh())
            //{
            //    using (var unc = new UNCAccessWithCredentials())
            //    {
            //        if (unc.NetUseWithCredentials(txtUNCPath.Text, txtUserName.Text, txtIP.Text, txtPassword.Text))
            //        {
            //            foreach (DataRow drv in dataTable.Rows)
            //            {
            //                drv["ChanDoan"] = Utility.sDbnull(txtChanDoan.Text, "");
            //                drv["VungKS"] = Utility.sDbnull(txtVungKS.Text, "");

            //                string sPath1 =
            //                    new KCB_HinhAnh().GetPathImageRadio(sPatient_Code, v_Patient_ID.ToString()) +
            //                    "\\" +
            //                    Utility.sDbnull(drv["Path1"], "");
            //                if (File.Exists(sPath1))
            //                {
            //                    //Image loadedImage =
            //                    //    Image.FromFile(
            //                    //       );
            //                    Bitmap loadedImage = Utility.LoadBitmap(sPath1);
            //                    drv["sPathImage1"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //                }
            //                else
            //                {
            //                    Bitmap loadedImage = Utility.LoadBitmap(path + @"\Path\imgWhite.jpg");
            //                    drv["sPathImage1"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //                }
            //                string sPath2 =
            //                    new KCB_HinhAnh().GetPathImageRadio(sPatient_Code, v_Patient_ID.ToString()) +
            //                    "\\" +
            //                    Utility.sDbnull(drv["Path2"], "");
            //                if (File.Exists(sPath2))
            //                {
            //                    Bitmap loadedImage = Utility.LoadBitmap(sPath2);
            //                    drv["sPathImage2"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //                }
            //                else
            //                {
            //                    Bitmap loadedImage = Utility.LoadBitmap(path + @"\Path\imgWhite.jpg");
            //                    drv["sPathImage2"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //                }
            //            }
            //            dataTable.AcceptChanges();
            //        }
            //        else
            //        {
            //            Utility.ShowMsg("Lỗi trong quá trình", "Thông báo", MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void YHHQ_ProcessData_KHONGHINHANH(ref DataTable dataTable)
        {
            //if (!m_dtReportHinhanh.Columns.Contains("VungKS")) m_dtReportHinhanh.Columns.Add("VungKS", typeof (string));
            //if (!m_dtReportHinhanh.Columns.Contains("ChanDoan"))
            //    m_dtReportHinhanh.Columns.Add("ChanDoan", typeof (string));
            //if (m_dtReportHinhanh.Columns.Contains("sPathImage1")) m_dtReportHinhanh.Columns.Remove("sPathImage1");
            //if (m_dtReportHinhanh.Columns.Contains("sPathImage2")) m_dtReportHinhanh.Columns.Remove("sPathImage2");
            //if (!m_dtReportHinhanh.Columns.Contains("sPathImage1"))
            //    m_dtReportHinhanh.Columns.Add("sPathImage1", typeof (byte[]));
            //if (!m_dtReportHinhanh.Columns.Contains("sPathImage2"))
            //    m_dtReportHinhanh.Columns.Add("sPathImage2", typeof (byte[]));
            //foreach (DataRow drv in dataTable.Rows)
            //{
            //    drv["ChanDoan"] = Utility.sDbnull(txtChanDoan.Text, "");
            //    drv["VungKS"] = Utility.sDbnull(txtVungKS.Text, "");
            //}
            //dataTable.AcceptChanges();
            //using (var unc = new UNCAccessWithCredentials())
            //{
            //    if (unc.NetUseWithCredentials(txtUNCPath.Text, txtUserName.Text, txtIP.Text, txtPassword.Text))
            //    {

            //            string sPath1 =
            //                new KCB_HinhAnh().GetPathImageRadio(sPatient_Code, v_Patient_ID.ToString()) +
            //                "\\" +
            //                Utility.sDbnull(drv["Path1"], "");
            //            if (File.Exists(sPath1))
            //            {

            //                //Image loadedImage =
            //                //    Image.FromFile(
            //                //       );
            //                Bitmap loadedImage = Utility.LoadBitmap(sPath1);
            //                drv["sPathImage1"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //            }
            //            else
            //            {

            //                Bitmap loadedImage = Utility.LoadBitmap(path + @"\Path\imgWhite.jpg");
            //                drv["sPathImage1"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //            }
            //            string sPath2 =
            //                new KCB_HinhAnh().GetPathImageRadio(sPatient_Code, v_Patient_ID.ToString()) +
            //                "\\" +
            //                Utility.sDbnull(drv["Path2"], "");
            //            if (File.Exists(sPath2))
            //            {
            //                Bitmap loadedImage = Utility.LoadBitmap(sPath2);
            //                drv["sPathImage2"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);

            //            }
            //            else
            //            {

            //                Bitmap loadedImage = Utility.LoadBitmap(path + @"\Path\imgWhite.jpg");
            //                drv["sPathImage2"] = Utility.ConvertImageToByteArray(loadedImage, ImageFormat.Jpeg);
            //            }
            //        }
            //        dataTable.AcceptChanges();
            //}
            //else
            //{
            //    Utility.ShowMsg("Lỗi trong quá trình", "Thông báo", MessageBoxIcon.Error);
            //}
        }

        #endregion

        #region "hàn thực hiện in phiếu của phần đơn ị ky đồng"

        /// <summary>
        ///     hàm thực hiện việc in thông tin dữ liệu
        /// </summary>
        private void KYDONG_PrintDataRadio(int id_chitietchidinh)
        {
            //m_dtReportHinhanh =
            //    SPs.YhhqPhieuHinhAnh(id_chitietchidinh).GetDataSet().Tables[0];
            //if (m_dtReportHinhanh.Rows.Count <= 0)
            //{
            //    Utility.ShowMsg("Không tìm thấy thông tin của bản ghi", "Thông báo");
            //    return;
            //}
            //// string sTitle = "PHIẾU KẾT QUẢ SIÊU ÂM";
            //switch (sTitleReport)
            //{
            //    case "SA":
            //        ProcessData_SIEUAM(ref m_dtReportHinhanh);
            //        Inphieu_SIEUAM(txtTieuDe.Text);
            //        break;
            //    case "XQ":
            //        ProcessData_SIEUAM(ref m_dtReportHinhanh);
            //        Inphieu_XQUANG(txtTieuDe.Text);
            //        break;
            //    case "NSTIEUHOA":
            //        ProcessData_NOISOI(ref m_dtReportHinhanh);
            //        Inphieu_NOISOI(txtTieuDe.Text);
            //        break;
            //    case "NSTMH":
            //        ProcessData_NOISOI(ref m_dtReportHinhanh);
            //        Inphieu_NOISOI(txtTieuDe.Text);
            //        break;
            //    case "NS":
            //        ProcessData_NOISOI(ref m_dtReportHinhanh);
            //        Inphieu_NOISOI(txtTieuDe.Text);
            //        break;
            //    case "DT":
            //        ProcessData_SIEUAM(ref m_dtReportHinhanh);
            //        Inphieu_DIENTIM(txtTieuDe.Text);
            //        break;
            //    default:
            //        ProcessData_SIEUAM(ref m_dtReportHinhanh);
            //        Inphieu_XQUANG(txtTieuDe.Text);
            //        break;
            //}
        }

        /// <summary>
        ///     hàm thực hiện viecj in cho lão khoa
        /// </summary>
        /// <param name=KcbChidinhclsChitiet.Columns.IdChitietchidinh></param>
        private void InPhieuHinhAnh(int id_chitietchidinh)
        {
            //m_dtReportHinhanh =
            //    SPs.YhhqPhieuHinhAnh(id_chitietchidinh).GetDataSet().Tables[0];
            //if (m_dtReportHinhanh.Rows.Count <= 0)
            //{
            //    Utility.ShowMsg("Không tìm thấy thông tin của bản ghi", "Thông báo");
            //    return;
            //}
            //Utility.UpdateLogotoDatatable(ref m_dtReportHinhanh);
            //ProcessData_SIEUAM(ref m_dtReportHinhanh);
            //BC_HinhAnh.HA_PhieuKetQua(mabaocao, m_dtReportHinhanh,
            //    txtTieuDe.Text, globalVariables.SysDate);
          
        }

     

       

        #endregion

        #endregion

        private void grdListForm_SelectionChanged_1(object sender, EventArgs e)
        {
        }

       
        /// <summary>
        ///     hàm thực hiện search thông tin của vùng khảo sát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearchForm_Click(object sender, EventArgs e)
        {
            
        }

        private void uiGroupBox9_Click(object sender, EventArgs e)
        {
        }

       

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin và in phiếu ngay khi cần
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSaveAndAccept_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            chkXacnhan.Checked = true;
            if (SaveResult())
                if (chkXacnhan.Checked)
                    cmdPrintRadio_Click(cmdPrintRadio, e);
        }

        private void linkClean_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

     

        private void chkHasHinhAnh_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._FTPProperties.IamLocal = PropertyLib._FTPProperties.IamLocal;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     hàm thực hiện việc phóng to giao diện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdRoomWord_Click(object sender, EventArgs e)
        {
           
        }

        private void chkBHYTtraiTuyen_CheckedChanged(object sender, EventArgs e)
        {
           
        }

       



        private void frm_quanlycacphongchucnang_FormClosing(object sender, FormClosingEventArgs e)
        {
           // SaveConfigCls();
        }


        private void cmdDeleteImgPath2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radChuaXacNhan.Checked) SearchData();
        }

        private void radChoXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radChoXacNhan.Checked) SearchData();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radDaXacNhan.Checked) SearchData();
        }

       

      

        #region "Hàm thực hiện việc kiểm tra config"

        /// <summary>
        ///     hàm thực hiện việc conect thông tin của Unc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConnect_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //Set FTP
            //    FtpClient = new FTPclient(PropertyLib._FTPProperties.IPAddress, PropertyLib._FTPProperties.UID, PropertyLib._FTPProperties.PWD);

            //    foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail("/"))
            //    {
            //        var item = new ListViewItem();
            //        item.Text = folder.Filename;
            //        if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
            //            item.SubItems.Add("Folder");
            //        else
            //            item.SubItems.Add("File");

            //        item.SubItems.Add(folder.FullName);
            //        item.SubItems.Add(folder.Permission);
            //        item.SubItems.Add(folder.FileDateTime.ToShortTimeString() + folder.FileDateTime.ToShortDateString());
            //        item.SubItems.Add(GetFileSize(folder.Size));
            //        lstRemoteSiteFiles.Items.Add(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }

        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount/1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount/1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount/1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount + " Bytes";

            return size;
        }

        /// <summary>
        ///     hàm thực hện luưu lại thông tin của hàm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConfig()
        {
            _baseDirectory = Utility.DoTrim(PropertyLib._FTPProperties.ImageFolder);
            if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0,_baseDirectory.Length - 1);
            _actionResult = new KCB_HinhAnh().UpdateSysConfigRadio(CreateNewSysRadio());
            switch (_actionResult)
            {
                case ActionResult.Success:
                    Utility.ShowMsg("Bạn thực hiện lưu thông tin thành công", "Thông báo thành công");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình lưu thông tin", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        ///     khởi tạo thông tin của đối tượng cấu hình
        /// </summary>
        /// <returns></returns>
        private SysConfigRadio CreateNewSysRadio()
        {
            var objConfigRadio = new SysConfigRadio();
            objConfigRadio.PassWord = PropertyLib._FTPProperties.PWD;
            objConfigRadio.UserName = PropertyLib._FTPProperties.UID;
            objConfigRadio.Domain = PropertyLib._FTPProperties.IPAddress;
            objConfigRadio.PathUNC = PropertyLib._FTPProperties.UNCPath;
            return objConfigRadio;
        }

        #endregion

     
        /// <summary>
        /// ham thực heienj việc cọn 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_DoubleClick(object sender, EventArgs e)
        {
           if(!Utility.isValidGrid(grdList)) return;
                BeginExam();
        }
        /// <summary>
        /// hàm thựcie ệnvi ệc enter khi search thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = _patient_Code;
                cmdSearch.PerformClick();
            }
        }
        DataTable dtData = null;
        private void cmdPrintRadio_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] arrDr =
                        m_dKcbChidinhclsChitiet.Select("id_chitietchidinh=" +
                                                Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                
                dtData = m_dKcbChidinhclsChitiet.Clone();
                if (arrDr.Length > 0)
                    dtData = arrDr.CopyToDataTable();
                if (!dtData.Columns.Contains("img1")) dtData.Columns.Add("img1", typeof(byte[]));
                if (!dtData.Columns.Contains("img2")) dtData.Columns.Add("img2", typeof(byte[]));
                if (!dtData.Columns.Contains("img3")) dtData.Columns.Add("img3", typeof(byte[]));
                if (!dtData.Columns.Contains("img4")) dtData.Columns.Add("img4", typeof(byte[]));

                dtData.Rows[0]["img1"] = Utility.bytGetImage(Utility.sDbnull(dtData.Rows[0]["Local1"], ""));
                dtData.Rows[0]["img2"] = Utility.bytGetImage(Utility.sDbnull(dtData.Rows[0]["Local2"], ""));
                dtData.Rows[0]["img3"] = Utility.bytGetImage(Utility.sDbnull(dtData.Rows[0]["Local3"], ""));
                dtData.Rows[0]["img4"] = Utility.bytGetImage(Utility.sDbnull(dtData.Rows[0]["Local4"], ""));
                SwapImage(dtData.Rows[0]);

                int coHA = 1;
                coHA = dtData.Rows[0]["img1"] != DBNull.Value ? 1 : 0;
                if (coHA == 0) coHA = dtData.Rows[0]["img2"] != DBNull.Value ? 1 : 0;
                if (coHA == 0) coHA = dtData.Rows[0]["img3"] != DBNull.Value ? 1 : 0;
                if (coHA == 0) coHA = dtData.Rows[0]["img4"] != DBNull.Value ? 1 : 0;
              // InphieuHA(dtData, dtpPrintDate.Value, coHA);
                PrintMau(dtData.Rows[0]);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private Document doc;
        void PrintMau(DataRow drData)
        {
            try
            {
                List<string> fieldNames = new List<string>() {"TEN_SO_YTE", "TEN_BENHVIEN",	"DIACHI_BENHVIEN",
                    "DIENTHOAI_BENHVIEN",		"MA_LUOTKHAM",	"ID_BENHNHAN",	"TEN_BENHNHAN",	"DIA_CHI",	"DOITUONG_KCB",
                    "NOI_CHIDINH",	"CHANDOAN",	"ID_PHIEU","ten_chitietdichvu",	"NAMSINH",	"TUOI",	"GIOI_TINH",
                    "MATHE_BHYT",	"Ket_qua",	"KET_LUAN",	"DE_NGHI",	"NGAYTHANGNAM","imgPath1","imgPath2","imgPath3","imgPath4"};
                List<string> Values = new List<string>() {  globalVariables.ParentBranch_Name,  globalVariables.Branch_Name,  globalVariables.Branch_Address,
                 globalVariables.Branch_Phone,Utility.sDbnull( drData["MA_LUOTKHAM"],""),Utility.sDbnull( drData["ID_BENHNHAN"],""),Utility.sDbnull( drData["TEN_BENHNHAN"],""),
                Utility.sDbnull( drData["dia_chi"],""),Utility.sDbnull( drData["ten_doituong_kcb"],""),Utility.sDbnull( drData["ten_phongchidinh"],""),Utility.sDbnull( drData["Chan_doan"],""),
                Utility.sDbnull( drData["id_chidinh"],""),Utility.sDbnull( drData["ten_chitietdichvu"],""),Utility.sDbnull( drData["nam_sinh"],""),Utility.sDbnull( drData["Tuoi"],""),Utility.sDbnull( drData["gioi_tinh"],""),
                Utility.sDbnull( drData["mathe_bhyt"],""),"","",
                "",Utility.FormatDateTime(globalVariables.SysDate),Utility.sDbnull( drData["Local1"],""),Utility.sDbnull( drData["Local2"],""),
                Utility.sDbnull( drData["Local3"],""),
                Utility.sDbnull( drData["Local4"],"")};

                DmucDichvuclsChitiet objDichvuchitiet = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1));
                if (objDichvuchitiet == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin dịch vụ CĐHA từ chi tiết chỉ định");
                    return;
                }
                DataTable dtDynamicValues = clsHinhanh.GetDynamicFieldsValues(objDichvuchitiet.IdChitietdichvu, objDichvuchitiet.Bodypart, objDichvuchitiet.ViewPosition, -1, Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                foreach (DataRow dr in dtDynamicValues.Rows)
                {
                    string SCode = Utility.sDbnull(dr[DynamicValue.Columns.Ma], "");
                    //string SName = Utility.sDbnull(dr[DynamicValue.Columns.mota], "");
                    string SValue = Utility.sDbnull(dr[DynamicValue.Columns.Giatri], "");
                    fieldNames.Add(SCode);
                    Values.Add(string.Format("{0}", SValue));
                    //Values.Add(string.Format("{0}{1}", SName, SValue));
                }
                string maubaocao = Application.StartupPath + @"\MauCDHA\" + docChuan;
                if (!File.Exists(maubaocao))
                {
                    Utility.ShowMsg("Chưa có mẫu báo cáo cho dịch vụ đang chọn. Bạn cần tạo mẫu và nhấn nút chọn mẫu để cập nhật cho dịch vụ đang thực hiện");
                    cmdBrowseMauChuan.Focus();
                    return;
                }
                string fileKetqua = string.Format("{0}{1}{2}{3}{4}", Path.GetDirectoryName(maubaocao), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(maubaocao), "_ketqua", Path.GetExtension(maubaocao));
                if ((drData != null) && File.Exists(maubaocao))
                {
                    doc = new Document(maubaocao);

                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.");
                        return;
                    }
                    doc.MailMerge.MergeImageField += MailMerge_MergeImageField;
                    doc.MailMerge.MergeField += MailMerge_MergeField;
                    doc.MailMerge.Execute(fieldNames.ToArray(), Values.ToArray());
                    
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;
                    if (chkPreview.Checked)
                    {
                        if (File.Exists(path))
                        {
                            Process process = new Process();
                            try
                            {
                                process.StartInfo.FileName = path;
                                process.Start();
                                process.WaitForInputIdle();
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        PrinterSettings printerSettings = new PrinterSettings();
                        printerSettings.DefaultPageSettings.Margins.Top = 0;
                        printerSettings.Copies = 1;

                        doc.Print(printerSettings);
                    }

                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
            }
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName.Contains("imgPath"))
            {
                DocumentBuilder builder = new DocumentBuilder(e.Document);

                // The code below should be adapted to your application specifics.
                byte[] imageData = getImgFile(e.FieldName);

                InsertImage(e.FieldName, imageData, builder);
            }
        }

        void MailMerge_MergeImageField(object sender, Aspose.Words.Reporting.MergeImageFieldEventArgs e)
        {
            DocumentBuilder builder = new DocumentBuilder(e.Document);

            // The code below should be adapted to your application specifics.
            byte[] imageData = getImgFile( e.FieldName);

            InsertImage(e.FieldName, imageData, builder);
        }
        //string getImgFile(string fieldName)
        //{
        //    return "";
        //}
        byte[] getImgFile(string fieldName)
        {
            try
            {
                if (dtData.Rows[0][fieldName.ToLower().Replace("imgpath", "img")] == null || dtData.Rows[0][fieldName.ToLower().Replace("imgpath", "img")].Equals(DBNull.Value))
                    return null;
                return (byte[])dtData.Rows[0][fieldName.ToLower().Replace("imgpath", "img")];
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }
/// <summary>
/// Replace merge field of the specified name with image.
/// </summary>
/// <param name=”mergefieldName”>Name of the merge field, where the image should be inserted.
/// The merge field itself is destroyed in the process.
/// If the merge field name is prefixed (e.g. “Image:Image1″),
/// then the name should be specified without prefix (e.g. “Image1″).</param>
/// <param name=”imageFileName”>fully qualified name image file.
/// <param name=”builder”>DocumentBuilder.</param>
/// <returns>Returns true, if the merge field with the specified name was found in the document,
/// false – if no such filed exists in the document.</returns>
        private bool InsertImage(string mergeFieldName, string imageFileName, DocumentBuilder builder)
        {
            if (File.Exists(imageFileName))
            {
                // Move builder to merge field (merge field is automatically removed).
                if (builder.MoveToMergeField(mergeFieldName))
                {
                    // No image resize by default.
                    // (Setting size to negative values makes image to be inserted without resizing.)
                    double width = -1;
                    double height = -1;

                    // Check if the image is inserted into table cell.
                    Cell cell = (Cell)builder.CurrentParagraph.GetAncestor(typeof(Aspose.Words.Tables.Cell));

                    if (cell != null)
                    {
                        // Set the cell properties so that the inserted image could occupy the cell space exactly.
                        cell.CellFormat.LeftPadding = 0;
                        cell.CellFormat.RightPadding = 0;
                        cell.CellFormat.TopPadding = 0;
                        cell.CellFormat.BottomPadding = 0;
                        cell.CellFormat.WrapText = false;
                        cell.CellFormat.FitText = true;

                        // Get cell dimensions.
                        width = cell.CellFormat.Width;
                        height = cell.ParentRow.RowFormat.Height;
                    }

                    // Check if the image is inserted into a textbox.
                    Shape shape = (Shape)builder.CurrentParagraph.GetAncestor(typeof(Aspose.Words.Drawing.Shape));

                    if ((shape != null) && (shape.ShapeType == ShapeType.TextBox))
                    {
                        // Set the textbox properties so that the inserted image could occupy the textbox space exactly.
                        shape.TextBox.InternalMarginTop = 0;
                        shape.TextBox.InternalMarginLeft = 0;
                        shape.TextBox.InternalMarginBottom = 0;
                        shape.TextBox.InternalMarginRight = 0;

                        // Get cell dimensions.
                        width = shape.Width;
                        height = shape.Height;
                    }

                    // Insert image with or without rescaling, based on the previously done analysis.
                    builder.InsertImage(imageFileName, width, height);

                    // Signal the caller that the image was succesfully inserted at merge field position.
                    return true;
                }
                else
                {
                    // Signal the caller that no merge field with the specified name could be found in the document.
                    return false;
                }
            }
            return true;
        }
        private bool InsertImage(string mergeFieldName, byte[] imagedata, DocumentBuilder builder)
        {
            if (imagedata != null)
            {
                // Move builder to merge field (merge field is automatically removed).
                if (builder.MoveToMergeField(mergeFieldName))
                {
                    // No image resize by default.
                    // (Setting size to negative values makes image to be inserted without resizing.)
                    double width = -1;
                    double height = -1;

                    // Check if the image is inserted into table cell.
                    Cell cell = (Cell)builder.CurrentParagraph.GetAncestor(typeof(Aspose.Words.Tables.Cell));

                    if (cell != null)
                    {
                        // Set the cell properties so that the inserted image could occupy the cell space exactly.
                        cell.CellFormat.LeftPadding = 0;
                        cell.CellFormat.RightPadding = 0;
                        cell.CellFormat.TopPadding = 0;
                        cell.CellFormat.BottomPadding = 0;
                        cell.CellFormat.WrapText = false;
                        cell.CellFormat.FitText = true;

                        // Get cell dimensions.
                        width = cell.CellFormat.Width;
                        height = cell.ParentRow.RowFormat.Height;
                    }

                    // Check if the image is inserted into a textbox.
                    Shape shape = (Shape)builder.CurrentParagraph.GetAncestor(typeof(Aspose.Words.Drawing.Shape));

                    if ((shape != null) && (shape.ShapeType == ShapeType.TextBox))
                    {
                        // Set the textbox properties so that the inserted image could occupy the textbox space exactly.
                        shape.TextBox.InternalMarginTop = 0;
                        shape.TextBox.InternalMarginLeft = 0;
                        shape.TextBox.InternalMarginBottom = 0;
                        shape.TextBox.InternalMarginRight = 0;

                        // Get cell dimensions.
                        //width = shape.Width;
                        //height = shape.Height;
                    }

                    // Insert image with or without rescaling, based on the previously done analysis.
                    builder.InsertImage(imagedata, width, height);

                    // Signal the caller that the image was succesfully inserted at merge field position.
                    return true;
                }
                else
                {
                    // Signal the caller that no merge field with the specified name could be found in the document.
                    return false;
                }
            }
            return true;
        }
        void SwapImage(DataRow dr)
        {
            try
            {
                if (dr["img1"] != DBNull.Value && dr["img2"] != DBNull.Value && dr["img3"] != DBNull.Value && dr["img4"] != DBNull.Value) return;
                if (dr["img1"] == DBNull.Value) dr["img1"] = FindNextNotNull(new List<string>() { "img2", "img3", "img4" }, dr);
                if (dr["img2"] == DBNull.Value) dr["img2"] = FindNextNotNull(new List<string>() { "img3", "img4" }, dr);
                if (dr["img3"] == DBNull.Value) dr["img3"] = FindNextNotNull(new List<string>() { "img4" }, dr);
                if (dr["img4"] != DBNull.Value)
                {
                    if (dr["img1"] == DBNull.Value)
                    {
                        dr["img1"] = dr["img4"]; dr["img4"] = DBNull.Value; ;
                    }
                    if (dr["img2"] == DBNull.Value)
                    {
                        dr["img2"] = dr["img4"]; dr["img4"] = DBNull.Value; ;
                    }
                    if (dr["img3"] == DBNull.Value)
                    {
                        dr["img3"] = dr["img4"]; dr["img4"] = DBNull.Value; ;
                    }
                }

                if (dr["Local1"] != DBNull.Value && dr["Local2"] != DBNull.Value && dr["Local3"] != DBNull.Value && dr["Local4"] != DBNull.Value) return;
                if (dr["Local1"] == DBNull.Value) dr["Local1"] = FindNextNotNull(new List<string>() { "Local2", "Local3", "Local4" }, dr);
                if (dr["Local2"] == DBNull.Value) dr["Local2"] = FindNextNotNull(new List<string>() { "Local3", "Local4" }, dr);
                if (dr["Local3"] == DBNull.Value) dr["Local3"] = FindNextNotNull(new List<string>() { "Local4" }, dr);
                if (dr["Local4"] != DBNull.Value)
                {
                    if (dr["Local1"] == DBNull.Value)
                    { dr["Local1"] = dr["Local4"]; dr["Local4"] = DBNull.Value; ; }
                    if (dr["Local2"] == DBNull.Value) { dr["Local2"] = dr["Local4"]; dr["Local4"] = DBNull.Value; ; }
                    if (dr["Local3"] == DBNull.Value) { dr["Local3"] = dr["Local4"]; dr["Local4"] = DBNull.Value; ; }
                }
            }
            catch
            {
            }
        }
        byte[] FindNextNotNull(List<string>lstFields ,DataRow dr)
        {
            try
            {
                foreach(string field in lstFields)
                {
                    if (dr[field] != DBNull.Value)
                    {
                        byte[] byt= dr[field] as byte[];
                        dr[field] = DBNull.Value;
                        return byt;

                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        bool m_blnForced2GetImagesFromFTP = false;
        private void cmdGetImages_Click(object sender, EventArgs e)
        {
            m_blnForced2GetImagesFromFTP = true;
            BeginExam();
            m_blnForced2GetImagesFromFTP = false;
        }

        private void cmdDelFTPImages_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_baseDirectory)) return;
            List<string> lstFiles = Directory.GetFiles(_baseDirectory).ToList<string>();
            if (lstFiles.Count > 0)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa bớt các ảnh đã download từ FTP về không", "Xác nhận", true))
                {
                    this.Cursor = Cursors.WaitCursor;
                    try2DelImageOnFTPFolder();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void chkPush2FTP_CheckedChanged(object sender, EventArgs e)
        {
            cmdGetImages.Enabled = PropertyLib._FTPProperties.Push2FTP;
            cmdDelFTPImages.Enabled = PropertyLib._FTPProperties.Push2FTP;
            PropertyLib._FTPProperties.Push2FTP = PropertyLib._FTPProperties.Push2FTP;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }
    }
}