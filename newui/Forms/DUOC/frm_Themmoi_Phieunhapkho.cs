using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VNS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.THUOC;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.DANHMUC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_Themmoi_Phieunhapkho : Form
    {
       
        #region"khai báo biến"
        public DataTable p_mDataPhieuNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet=new DataTable();
        private DataTable m_dtDataKhoNhap=new DataTable();
        private DataTable m_dtDataNhaCungCap=new DataTable();
       
       // private DataTable m_dtDataNhaCungCap=new DataTable();
        public action em_Action = action.Insert;
        public bool b_Cancel = false;
        public Janus.Windows.GridEX.GridEX grdList;
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        public string KIEU_THUOC_VT = "THUOC";
        #endregion

        #region "khai báo khởi tạo Form"
        public frm_Themmoi_Phieunhapkho()
        {
            InitializeComponent();
           
            
            
            dtNgayHetHan.Value = dtNgayNhap.Value =globalVariables.SysDate;
            InitEvents();
            CauHinh();
        }
        void InitEvents()
        {
            txtChietkhau.TextChanged += new EventHandler(txtChietkhau_TextChanged);
            nmrThangDu.ValueChanged += new EventHandler(nmrThangDu_ValueChanged);
            txtTongTien.KeyDown += new KeyEventHandler(txtTongTien_KeyDown);
            
            txtDongia.TextChanged += new EventHandler(txtDongia_TextChanged);
            txtSoluong.TextChanged += new EventHandler(txtSoluong_TextChanged);
            this.txtTongTien.TextChanged += new System.EventHandler(this.txtTongTien_TextChanged);
            this.txtTongTien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTongTien_KeyPress);
            this.cboKhoNhap.SelectedIndexChanged += new System.EventHandler(this.cboKhoNhap_SelectedIndexChanged);
            this.cboKhoNhap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboKhoNhap_KeyPress);
            this.txtVAT.TextChanged += new System.EventHandler(this.txtVAT_TextChanged);
            this.txtSoHoaDon.TextChanged += new System.EventHandler(this.txtSoHoaDon_TextChanged);
            this.cmdXoaThongTin.Click += new System.EventHandler(this.cmdXoaThongTin_Click);
            this.cmdInPhieuNhap.Click += new System.EventHandler(this.cmdInPhieuNhap_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.txtDrug_ID.TextChanged += new System.EventHandler(this.txtDrug_ID_TextChanged);
            this.grdPhieuNhapChiTiet.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdPhieuNhapChiTiet_CellUpdated);
            this.grdPhieuNhapChiTiet.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.grdPhieuNhapChiTiet_UpdatingCell);
            
            this.cmdAddDetail.Click += new System.EventHandler(this.cmdAddDetail_Click);
            this.cmdHuyThongTin.Click += new System.EventHandler(this.cmdHuyThongTin_Click);
            this.txtThanhTien.TextChanged += new System.EventHandler(this.txtThanhTien_TextChanged);
            this.txtThanhTien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThanhTien_KeyPress);
            this.nmrThangDu.Click += new System.EventHandler(this.nmrThangDu_Click);
            this.Load += new System.EventHandler(this.frm_Themmoi_Phieunhapkho_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Themmoi_Phieunhapkho_KeyDown);
            this.cmdCauHinh.Click += new System.EventHandler(this.cmdCauHinh_Click);
            this.cmdThemPhieuNhap.Click += new System.EventHandler(this.cmdThemPhieuNhap_Click);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            //chkGiaBHYT.CheckedChanged += new EventHandler(chkGiaBHYT_CheckedChanged);
            txtGiaban.TextChanged += new EventHandler(txtGiaban_TextChanged);
            txtGiaBHYT.TextChanged += new EventHandler(txtGiaBHYT_TextChanged);
            txtGiaBHYT_cu.TextChanged += new EventHandler(txtGiaBHYT_cu_TextChanged);
            txtPhuthuDT.TextChanged += new EventHandler(txtPhuthuDT_TextChanged);
            txtPhuthuTT.TextChanged += new EventHandler(txtPhuthuTT_TextChanged);
            txtSoluong.GotFocus += new EventHandler(txtSoluong_GotFocus);
            txtDongia.GotFocus += new EventHandler(txtDongia_GotFocus);
            chkCloseAfterSaving.CheckedChanged += new EventHandler(chkCloseAfterSaving_CheckedChanged);
            txtDrugName._OnEnterMe += new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtDrugName__OnEnterMe);
            grdPhieuNhapChiTiet.EditingCell += new EditingCellEventHandler(grdPhieuNhapChiTiet_EditingCell);
            grdPhieuNhapChiTiet.CurrentCellChanged += new EventHandler(grdPhieuNhapChiTiet_CurrentCellChanged);
            txtLyDoNhap._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLyDoNhap__OnShowData);
            txtNhacungcap._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNhacungcap__OnShowData);
            txtNguoiGiao._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoiGiao__OnShowData);
            txtNguoinhan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNguoinhan__OnShowData);
        }

        void txtNguoinhan__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtNguoinhan);
            
        }
       
        void txtNguoiGiao__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtNguoiGiao);
        }

        void txtNhacungcap__OnShowData()
        {
            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtNhacungcap);
        }

        void txtLyDoNhap__OnShowData()
        {

            VNS.HIS.UI.Classess.dmucchunghelper.ShowMe(txtLyDoNhap);
        }

        void grdPhieuNhapChiTiet_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdPhieuNhapChiTiet.CurrentColumn != null) grdPhieuNhapChiTiet.CurrentColumn.InputMask = "";
        }
        string oldInpustMask = "";
        void grdPhieuNhapChiTiet_CurrentCellChanged(object sender, EventArgs e)
        {

         
            
        }

      

        void txtDrugName__OnEnterMe()
        {
            int _idthuoc = Utility.Int32Dbnull(txtDrugName.MyID, -1);
            txtDrug_ID.Text = _idthuoc.ToString();
        }

        void chkCloseAfterSaving_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NhapkhoProperties.Themmoilientuc = !chkCloseAfterSaving.Checked;
            PropertyLib.SaveProperty(PropertyLib._NhapkhoProperties);
        }

        void txtDongia_GotFocus(object sender, EventArgs e)
        {
        }

        void txtSoluong_GotFocus(object sender, EventArgs e)
        {
            
        }

        void txtPhuthuTT_TextChanged(object sender, EventArgs e)
        {
        }

        void txtPhuthuDT_TextChanged(object sender, EventArgs e)
        {
        }

        void txtGiaBHYT_cu_TextChanged(object sender, EventArgs e)
        {
        }

        void txtGiaBHYT_TextChanged(object sender, EventArgs e)
        {
        }

        void txtGiaban_TextChanged(object sender, EventArgs e)
        {
            if (BHYT_GIABHYT_BANG_GIABAN) txtGiaBHYT.Text = txtGiaban.Text;
        }

        void chkGiaBHYT_CheckedChanged(object sender, EventArgs e)
        {
            //txtGiaBHYT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
            //txtGiaBHYT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ;

            //txtPhuthuDT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIAPHUTHU;
            //txtPhuthuDT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIAPHUTHU ? txtSoluong.BackColor : txtThanhTien.BackColor;

            //txtPhuthuTT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIAPHUTHU;
            //txtPhuthuTT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIAPHUTHU ? txtSoluong.BackColor : txtThanhTien.BackColor;
        }
        
        private void CauHinh()
        {

            nmrThangDu.Enabled = PropertyLib._NhapkhoProperties.CoThangDu;
            chkCloseAfterSaving.Checked = !PropertyLib._NhapkhoProperties.Themmoilientuc;
            if (!PropertyLib._NhapkhoProperties.CoThangDu) nmrThangDu.Value = 0;
            txtGiaban.Enabled = PropertyLib._NhapkhoProperties.Nhapgiaban;
            txtGiaban.BackColor = PropertyLib._NhapkhoProperties.Nhapgiaban ? txtSoluong.BackColor : txtThanhTien.BackColor;

            //chkGiaBHYT.Enabled = BHYT_CHOPHEPNHAPGIA;
            //if (!BHYT_CHOPHEPNHAPGIA) chkGiaBHYT.Checked = false;
            //txtGiaBHYT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;
            //txtGiaBHYT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;

            //txtPhuthuDT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;
            //txtPhuthuDT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;

            //txtPhuthuTT.Enabled = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;
            //txtPhuthuTT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
        }
        void txtTongTien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDrugName.Focus();
        }

        void nmrThangDu_ValueChanged(object sender, EventArgs e)
        {

            TinhGiaBan();
        }

        void txtChietkhau_TextChanged(object sender, EventArgs e)
        {
            ThanhTien();
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Themmoi_Phieunhapkho_Load(object sender, EventArgs e)
        {

            LoadData();  
        }
        void LoadData()
        {
            try
            {
                lblChietkhau.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_HIENTHI_CHIETKHAUCHITIET", "0", false) == "1";
                txtChietkhau.Visible = lblChietkhau.Visible;
                txtPhuthuDT.TabStop = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_TABSTOP_PHUTHU", "0", false) == "1";
                txtPhuthuTT.TabStop = txtPhuthuDT.TabStop;
                lblTongtien.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_KIEMTRATONGTIEN", "0", false) == "1";
                txtTongHoaDon.Visible = lblTongtien.Visible;
                DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtNhacungcap.LOAI_DANHMUC, txtNguoiGiao.LOAI_DANHMUC, txtLyDoNhap.LOAI_DANHMUC }, true);
                txtNhacungcap.Init(dtData);
                txtNguoiGiao.Init(dtData);
                txtNguoinhan.Init();
                txtLyDoNhap.Init(dtData);

                if (KIEU_THUOC_VT == "THUOC")
                {
                    m_dtDataKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
                }
                else
                {
                    m_dtDataKhoNhap = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                }

                if (m_dtDataKhoNhap.Rows.Count > 1)
                    DataBinding.BindDataCombobox(cboKhoNhap, m_dtDataKhoNhap,
                                               TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                               "---Chọn kho nhập---", false);
                else
                    DataBinding.BindDataCombobox(cboKhoNhap, m_dtDataKhoNhap,
                                           TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn---", true);
                DataBinding.BindDataCombobox(cboNhanVien, CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                                       DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Nhân viên---", false);
                cboNhanVien.Enabled = false;

                cboNhanVien.SelectedIndex = Utility.GetSelectedIndex(cboNhanVien, globalVariables.gv_intIDNhanvien.ToString());

                LoadAuCompleteThuoc();
                getData();
                SetStatusControl();
                ModifyCommand();
                CauHinh();
                ConfigBHYT();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        bool BHYT_CHOPHEPNHAPGIA = false;
        bool BHYT_HIENTHIGIA = false;
        bool BHYT_CHOPHEPNHAPGIAPHUTHU = false;
        bool BHYT_LUACHON_APDUNG = false;
        bool BHYT_GIABHYT_BANG_GIABAN = false;
        bool BHYT_CANHBAO_GIAMOIKHACGIACU = true;
        bool NHAPKHOTHUOC_CHOPHEP_NHAPCHIETKHAU = true;
        void ConfigBHYT()
        {
            BHYT_CANHBAO_GIAMOIKHACGIACU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CANHBAO_GIAMOIKHACGIACU", "0", true), 0) == 1;
            BHYT_GIABHYT_BANG_GIABAN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_GIABHYT_BANG_GIABAN", "0", true), 0) == 1;
            BHYT_CHOPHEPNHAPGIA = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CHOPHEPNHAPGIA", "0", true), 0) == 1;
             BHYT_LUACHON_APDUNG = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_LUACHON_APDUNG", "0", true), 0) == 1;
             BHYT_CHOPHEPNHAPGIAPHUTHU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CHOPHEPNHAPGIAPHUTHU", "0", true), 0) == 1;
             BHYT_HIENTHIGIA = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_HIENTHIGIA", "0", true), 0) == 1;
             NHAPKHOTHUOC_CHOPHEP_NHAPCHIETKHAU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPKHOTHUOC_CHOPHEP_NHAPCHIETKHAU", "0", true), 0) == 1;

             //chkGiaBHYT.Visible = BHYT_LUACHON_APDUNG;
             //chkGiaBHYT.Enabled = BHYT_LUACHON_APDUNG;
             txtGiaBHYT.Visible = BHYT_LUACHON_APDUNG;
             txtGiaBHYT_cu.Visible = BHYT_LUACHON_APDUNG;
             txtPhuthuDT.Visible = BHYT_LUACHON_APDUNG;
             txtPhuthuTT.Visible = BHYT_LUACHON_APDUNG;

             lblPhuthuDt.Visible = BHYT_LUACHON_APDUNG;
             lblPhuthuTT.Visible = BHYT_LUACHON_APDUNG;
             lblBHYTcu.Visible = BHYT_LUACHON_APDUNG;


             if (!BHYT_LUACHON_APDUNG) return;//Nếu cho lựa chọn áp dụng BHYT thì kiểm tra tiếp
             txtGiaBHYT.Visible = BHYT_HIENTHIGIA;
             txtGiaBHYT_cu.Visible = BHYT_HIENTHIGIA ;
             txtPhuthuDT.Visible = BHYT_HIENTHIGIA ;
             txtPhuthuTT.Visible = BHYT_HIENTHIGIA ;

             lblPhuthuDt.Visible = BHYT_HIENTHIGIA;
             lblPhuthuTT.Visible = BHYT_HIENTHIGIA;
             lblBHYTcu.Visible = BHYT_HIENTHIGIA;

             txtGiaBHYT.Enabled = BHYT_CHOPHEPNHAPGIA;// chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA;Từ các dòng sau bỏ chkGiaBHYT.Checked &&
             txtGiaBHYT.BackColor = BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;

             txtPhuthuDT.Enabled = BHYT_CHOPHEPNHAPGIAPHUTHU;
             txtPhuthuDT.BackColor =  BHYT_CHOPHEPNHAPGIAPHUTHU ? txtSoluong.BackColor : txtThanhTien.BackColor;

             txtPhuthuTT.Enabled = BHYT_CHOPHEPNHAPGIAPHUTHU;
             txtPhuthuTT.BackColor =  BHYT_CHOPHEPNHAPGIAPHUTHU ? txtSoluong.BackColor : txtThanhTien.BackColor;


        }
        private void ClearControlPhieu()
        {
            foreach (Control control in grpControl.Controls)
            {
                if(control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
                if (control is Janus.Windows.EditControls.UIComboBox)
                {
                    if (((Janus.Windows.EditControls.UIComboBox)(control)).Name!=cboNhanVien.Name)
                    {
                        ((Janus.Windows.EditControls.UIComboBox)(control)).SelectedIndex = 0;
                    }
                   
                }
            }
            txtNhacungcap.SetCode("-1");
            txtTongHoaDon.Clear();
            dtNgayNhap.Value =globalVariables.SysDate;
            txtSoHoaDon.Focus();
        }
        private void getData()
        {
            if(em_Action==action.Update)
            {
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                if(objPhieuNhap!=null)
                {
                    txtSoHoaDon.Text = Utility.sDbnull(objPhieuNhap.SoHoadon);
                    dtNgayNhap.Value = objPhieuNhap.NgayHoadon;
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    txtLyDoNhap._Text = Utility.sDbnull(objPhieuNhap.MotaThem);
                    txtNguoiGiao._Text = Utility.sDbnull(objPhieuNhap.NguoiGiao);
                    txtNguoinhan._Text = Utility.sDbnull(objPhieuNhap.NguoiNhan);
                    dtNgayNhap.Value = Convert.ToDateTime(objPhieuNhap.NgayHoadon);
                    txtChietkhau.Text = Utility.sDbnull(objPhieuNhap.Vat);
                    txtTongTien.Text = Utility.sDbnull(objPhieuNhap.TongTien);
                    cboKhoNhap.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhonhap);
                    txtNo.Text = objPhieuNhap.TkNo;
                    txtCo.Text = objPhieuNhap.TkCo;
                    txtSoCTkemtheo.Text = objPhieuNhap.SoChungtuKemtheo;
                    chkPhieuvay.Checked = Utility.Byte2Bool(objPhieuNhap.PhieuVay);
                    //if (Utility.Int32Dbnull(objPhieuNhap.IdKhonhap) > 0)
                        txtNhacungcap.SetCode(objPhieuNhap.MaNhacungcap);
                    //cboNhaCungCap.SelectedValue = Utility.sDbnull(objPhieuNhap.IdNhaCcap);
                    if(Utility.Int32Dbnull(objPhieuNhap.IdNhanvien)>0)
                    cboNhanVien.SelectedValue = Utility.sDbnull(objPhieuNhap.IdNhanvien);

                    m_dtDataPhieuChiTiet =
                         new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetDataSourceForDataGridEx(grdPhieuNhapChiTiet,m_dtDataPhieuChiTiet,false,true,"1=1","");
                }
            }
            if(em_Action==action.Insert)
            {
                m_dtDataPhieuChiTiet =
                       new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(-100);
                Utility.SetDataSourceForDataGridEx(grdPhieuNhapChiTiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
            }
          
        }
        private void LoadAuCompleteThuoc()
        {
            txtDrugName.dtData=CommonLoadDuoc.LayThongTinThuoc(KIEU_THUOC_VT);
            txtDrugName.ChangeDataSource();
           

           
        }

        private void frm_Themmoi_Phieunhapkho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F4) cmdInPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtDrugName.Focus();
                txtDrugName.SelectAll();
            }
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.A && e.Control)cmdAddDetail.PerformClick();
            if(e.KeyCode==Keys.S && e.Control)cmdSave.PerformClick();
            if (e.KeyCode == Keys.F1 && e.Control) ConfigBHYT();
        }

        private void txtDrug_ID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDrug_ID.Text))
                {
                    DmucThuoc objDrug = DmucThuoc.FetchByID(Utility.Int32Dbnull(txtDrug_ID.Text));
                    if (objDrug != null)
                    {
                        DmucChung objMeasureUnit = THU_VIEN_CHUNG.LaydoituongDmucChung("DONVITINH", Utility.sDbnull(objDrug.MaDonvitinh));
                        if (objMeasureUnit != null)
                        {
                            txtDonViTinh.Text = Utility.sDbnull(objMeasureUnit.Ten);
                            txtMaDonvitinh.Text = Utility.sDbnull(objMeasureUnit.Ma);
                        }

                        txtDongia.Text = Utility.sDbnull(objDrug.DonGia, "0");

                        QheDoituongThuoc _objQhe = new Select().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(objDrug.IdThuoc)
                     .And(QheDoituongThuoc.Columns.IdLoaidoituongKcb).IsEqualTo(0).ExecuteSingle<QheDoituongThuoc>();
                        if (_objQhe != null)
                        {
                           // chkGiaBHYT.Checked = true;
                            if (!BHYT_GIABHYT_BANG_GIABAN) txtGiaBHYT.Text = _objQhe.DonGia.ToString();
                            txtGiaBHYT_cu.Text = _objQhe.DonGia.ToString();
                            txtPhuthuDT.Text = Utility.DecimaltoDbnull(_objQhe.PhuthuDungtuyen, 0).ToString();
                            txtPhuthuTT.Text = Utility.DecimaltoDbnull(_objQhe.PhuthuTraituyen, 0).ToString();
                        }
                        else
                        {
                            DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuoc + "=" + txtDrug_ID.Text);
                            if (arrDr.Length > 0)
                            {
                                //chkGiaBHYT.Checked = Utility.Byte2Bool(Utility.ByteDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.CoBhyt]));
                                txtGiaBHYT.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt], 0).ToString();
                                txtGiaBHYT_cu.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaBhytCu], 0).ToString();

                                txtPhuthuDT.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen], 0).ToString();
                                txtPhuthuTT.Text = Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen], 0).ToString();

                            }
                            else
                            {
                                //chkGiaBHYT.Checked = false;
                                txtGiaBHYT.Text = "0";
                                txtGiaBHYT_cu.Text = "0";
                                txtPhuthuDT.Text = "0";
                                txtPhuthuTT.Text = "0";
                            }
                        }

                        //Bỏ chkGiaBHYT.Checked &&
                        txtGiaBHYT.BackColor = BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;

                        txtPhuthuDT.BackColor =  BHYT_CHOPHEPNHAPGIAPHUTHU ? txtSoluong.BackColor : txtThanhTien.BackColor;

                        txtPhuthuTT.BackColor =  BHYT_CHOPHEPNHAPGIAPHUTHU ? txtSoluong.BackColor : txtThanhTien.BackColor;

                        //txtGiaBHYT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
                        //txtPhuthuDT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
                        //txtPhuthuTT.BackColor = chkGiaBHYT.Checked && BHYT_CHOPHEPNHAPGIA ? txtSoluong.BackColor : txtThanhTien.BackColor;
                        //txtGiaBHYT.Enabled = chkGiaBHYT.Checked;
                    }

                }
                else
                {

                    txtDonViTinh.Clear();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới phiếu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            em_Action = action.Insert;
            ClearControlPhieu();
            ClearControl();
            txtSoHoaDon.Focus();
            m_dtDataPhieuChiTiet.Clear();
            m_dtDataPhieuChiTiet.AcceptChanges();
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            if (!IsValidChiTiet()) return;
            AddDetailNhapKho();
            ClearControl();
            TinhSumThanhTien();
            ModifyCommand();
        }
        private bool IsValidChiTiet()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema)
                .Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(Utility.Int32Dbnull(txtDrug_ID.Text));
            if (sqlQuery.GetRecordCount() <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn cần chọn thuốc để nhập", true);
                txtDrugName.Focus();
                return false;
            }
            if (Utility.DoTrim(txtSoLo.Text) == "")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập thông tin số lô", true);
                txtSoLo.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoluong.Text) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Số lượng >0", true);
                txtSoluong.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtDongia.Text) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Đơn giá phải >0", true);
                txtDongia.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtGiaban.Text) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Giá bán phải >0", true);
                txtGiaban.Focus();
                return false;
            }
            if (BHYT_LUACHON_APDUNG && BHYT_HIENTHIGIA && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_CANHBAO_KHACGIA", "1", false) == "1")
                if (Utility.DecimaltoDbnull(txtGiaBHYT_cu.Text,-1) <=0 && Utility.DecimaltoDbnull(txtGiaBHYT.Text) != Utility.DecimaltoDbnull(txtGiaBHYT_cu.Text))
                {
                    if (!Utility.AcceptQuestion("Giá BHYT cũ khác giá BHYT mới. Bạn có chắc chắn điều chỉnh giá BHYT thành giá mới hay không?", "Cảnh báo", true))
                    {
                        txtGiaBHYT.Focus();
                        return false;
                    }
                }
            if ( BHYT_CHOPHEPNHAPGIA)
                if (Utility.DecimaltoDbnull(txtPhuthuDT.Text) > Utility.DecimaltoDbnull(txtGiaBHYT.Text))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Giá phụ thu đúng tuyến phải < giá BHYT mới", true);
                    txtPhuthuDT.Focus();
                    return false;
                }
            if ( BHYT_CHOPHEPNHAPGIA)
                if (Utility.DecimaltoDbnull(txtPhuthuTT.Text) > Utility.DecimaltoDbnull(txtGiaBHYT.Text))
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Giá phụ thu trái tuyến phải < giá BHYT mới", true);
                    txtPhuthuTT.Focus();
                    return false;
                }
            return true;
        }
        private void AddDetailNhapKho()
        {
            try
            {
                DataRow[] arrDr = m_dtDataPhieuChiTiet
                    .Select
                    (
                    TPhieuNhapxuatthuocChitiet.Columns.IdThuoc + "=" + txtDrug_ID.Text + " AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.GiaNhap + "=" + Utility.DecimaltoDbnull(txtDongia.Text) + " AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.SoLo + "='" + Utility.DoTrim(txtSoLo.Text) + "' AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.Vat + "=" + Utility.Int32Dbnull(txtVAT.Text, 0) + " AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.NgayHethan + "='" + dtNgayHetHan.Text + "' AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt + "='" + Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0) + "' AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen + "=" +Utility.DecimaltoDbnull(txtPhuthuDT.Text, 0) + " AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen + "=" +Utility.DecimaltoDbnull(txtPhuthuTT.Text, 0) + " AND "
                    + TPhieuNhapxuatthuocChitiet.Columns.GiaBan + "=" + Utility.DecimaltoDbnull(txtGiaban.Text, 0)
                    );
                if (arrDr.Length > 0)
                {
                    int newquantity =(int)( Utility.DecimaltoDbnull(arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.SoLuong], 0) + Utility.DecimaltoDbnull(txtSoluong.Text));
                    arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = newquantity;
                    m_dtDataPhieuChiTiet.AcceptChanges();
                }
                else
                {
                    DataRow drv = m_dtDataPhieuChiTiet.NewRow();
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = txtDrug_ID.Text;
                    drv["ten_donvitinh"] = Utility.sDbnull(txtDonViTinh.Text);
                    DmucThuoc objLDrug = DmucThuoc.FetchByID(Utility.Int32Dbnull(txtDrug_ID.Text));
                    if (objLDrug != null)
                    {
                        drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                    }

                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPhuthuDT.Text) ;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen] = Utility.DecimaltoDbnull(txtPhuthuTT.Text);

                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt] =  Utility.DecimaltoDbnull(txtGiaBHYT.Text) ;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.CoBhyt] =  BHYT_LUACHON_APDUNG;

                    drv[TPhieuNhapxuatthuocChitiet.Columns.MaNhacungcap] = Utility.DoTrim(txtNhacungcap.myCode);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoLo] = Utility.DoTrim(txtSoLo.Text);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap] = Utility.DecimaltoDbnull(txtDongia.Text);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = Utility.DecimaltoDbnull(txtSoluong.Text);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = Utility.DecimaltoDbnull(txtThanhTien.Text);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtChietkhau.Text));
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ThangDu] = Utility.Int32Dbnull(nmrThangDu.Value, 0);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.Vat] = Utility.Int32Dbnull(txtVAT.Text, 0);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.GiaBan] = Utility.DecimaltoDbnull(txtGiaban.Text, 0);
                    drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = dtNgayHetHan.Text;
                    drv["NGAY_HET_HAN"] = dtNgayHetHan.Value.Date;
                    m_dtDataPhieuChiTiet.Rows.Add(drv);
                }
                UpdateWhenChanged();
            }
            catch
            {
            }
        }
        private void SetStatusControl()
        {
             
             
        }
        /// <summary>
        /// hàm thực hiện việc Invalinhap khoa thuốc
        /// </summary>
        /// <returns></returns>
        private bool IsValidNhapKho()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            if (string.IsNullOrEmpty(txtSoHoaDon.Text))
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập số hóa đơn", true);
                txtSoHoaDon.Focus();
                return false;

            }
            if ( txtNhacungcap.myCode== "-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn nhà cung cấp", true);
                txtNhacungcap.Focus();
                return false;
            }
            if (cboKhoNhap.SelectedValue.ToString() == "-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn kho để nhập thuốc", true);
                cboKhoNhap.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtTongHoaDon.Text) != Utility.DecimaltoDbnull(txtTongTien.Text) && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NHAPKHO_KIEMTRATONGTIEN", "0", false) == "1")
            {
                txtTongTien.Focus();
                txtTongTien.SelectAll();
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Tổng tiền hóa đơn phải bằng tổng tiền nhập",true);
                return false;
            }
           
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc nhạp số hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoHoaDon_TextChanged(object sender, EventArgs e)
        {
            SetStatusControl();
        }

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStatusControl();
        }

        private void cboKhoNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStatusControl();
        }

        private void cmdHuyThongTin_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        /// <summary>
        /// hàm thực hiện việc làm sạch thông tin 
        /// </summary>
        private void ClearControl()
        {
            txtDrugName.Clear();
            txtDrugName.Focus();
            txtSoLo.Clear();
            txtSoluong.Clear();
            txtDongia.Clear();
            txtGiaban.Clear();
            txtThanhTien.Clear();
            nmrThangDu.Value = 0;
            txtChietkhau.Clear();
            
        }
        /// <summary>
        /// hàm thực hiện việc cho phép 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaThongTin_Click(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuNhapChiTiet.GetCheckedRows())
            {
                int ID = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet].Value);
                TPhieuNhapxuatthuocChitiet.Delete(ID);
                gridExRow.Delete();
                grdPhieuNhapChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            ModifyCommand();
            TinhSumThanhTien();
        }
        private void ModifyCommand()
        {
            cmdSave.Enabled = grdPhieuNhapChiTiet.RowCount > 0;
            cmdInPhieuNhap.Enabled = grdPhieuNhapChiTiet.RowCount > 0;
            cmdXoaThongTin.Enabled = grdPhieuNhapChiTiet.RowCount > 0;
            TinhSumThanhTien();
        }
        private void TinhSumThanhTien()
        {
            var query = from thuoc in grdPhieuNhapChiTiet.GetDataRows()
                        let y = Utility.DecimaltoDbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value)
                        select y;
            txtTongTien.Text = Utility.sDbnull(query.Sum());
        }
        private void ThanhTien()
        {
            decimal thanhtien = Utility.DecimaltoDbnull(txtDongia.Text)*Utility.DecimaltoDbnull(txtSoluong.Text);
            thanhtien = thanhtien + thanhtien * Utility.DecimaltoDbnull(txtVAT.Text) / 100;
               decimal ChietKhau= Utility.DecimaltoDbnull(txtChietkhau.Text);
            if (thanhtien < ChietKhau)
            {
                txtChietkhau.Text = thanhtien.ToString();
            }
            else
                thanhtien=thanhtien-ChietKhau;
            txtThanhTien.Text = Utility.sDbnull(thanhtien);
            TinhSumThanhTien();
        }
        private void txtDongia_Click(object sender, EventArgs e)
        {
            ThanhTien();
        }

        

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            ThanhTien();
            
        }
        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của phiếu nhập kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if(!IsValidNhapKho())return;
            PerformAction();
        }
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    ThemPhieuNhapKho();
                    break;
                case action.Update:
                    UpdatePhieuNhapKho();
                    break;
            }
        }
        #region "khai báo các đối tượng để thực hiện việc "
        private TPhieuNhapxuatthuoc CreatePhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc=new TPhieuNhapxuatthuoc();
            if(em_Action==action.Update)
            {
                objTPhieuNhapxuatthuoc.MarkOld();
                objTPhieuNhapxuatthuoc.IsLoaded = true;
                objTPhieuNhapxuatthuoc.IdPhieu = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text,-1);
            }
            objTPhieuNhapxuatthuoc.SoHoadon = Utility.sDbnull(txtSoHoaDon.Text);
            objTPhieuNhapxuatthuoc.IdKhonhap = Utility.Int16Dbnull(cboKhoNhap.SelectedValue, -1);
            objTPhieuNhapxuatthuoc.MaNhacungcap = txtNhacungcap.myCode;
            objTPhieuNhapxuatthuoc.MotaThem = Utility.DoTrim(txtLyDoNhap.Text);
            objTPhieuNhapxuatthuoc.TrangThai = 0;
            objTPhieuNhapxuatthuoc.PhieuVay = Utility.Bool2byte(chkPhieuvay.Checked);
            objTPhieuNhapxuatthuoc.IdNhanvien = Utility.Int16Dbnull(cboNhanVien.SelectedValue, -1);
            if (Utility.Int32Dbnull(objTPhieuNhapxuatthuoc.IdNhanvien, -1) <= 0)
                objTPhieuNhapxuatthuoc.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objTPhieuNhapxuatthuoc.NgayHoadon =dtNgayNhap.Value;
            objTPhieuNhapxuatthuoc.NgayTao = globalVariables.SysDate;
            objTPhieuNhapxuatthuoc.NguoiTao = globalVariables.UserName;
            objTPhieuNhapxuatthuoc.NguoiGiao = Utility.DoTrim(txtNguoiGiao.Text);
            objTPhieuNhapxuatthuoc.NguoiNhan = Utility.DoTrim(txtNguoinhan.Text);
            objTPhieuNhapxuatthuoc.TkNo = Utility.DoTrim(txtNo.Text);
            objTPhieuNhapxuatthuoc.TkCo = Utility.DoTrim(txtCo.Text);
            objTPhieuNhapxuatthuoc.SoChungtuKemtheo = Utility.DoTrim(txtSoCTkemtheo.Text);
            objTPhieuNhapxuatthuoc.Vat = Utility.DecimaltoDbnull(txtVAT.Text);
            objTPhieuNhapxuatthuoc.LoaiPhieu =(byte) LoaiPhieu.PhieuNhapKho;
            objTPhieuNhapxuatthuoc.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapKho);
            objTPhieuNhapxuatthuoc.KieuThuocvattu = KIEU_THUOC_VT;
            return objTPhieuNhapxuatthuoc;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin chi tiết
        /// </summary>
        /// <returns></returns>
        private List< TPhieuNhapxuatthuocChitiet> GetReceiptDetails()
        {
            List<TPhieuNhapxuatthuocChitiet> lstDetails = new List<TPhieuNhapxuatthuocChitiet>();
            try
            {
               
               
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuNhapChiTiet.GetDataRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        TPhieuNhapxuatthuocChitiet _newItem = new TPhieuNhapxuatthuocChitiet();
                        _newItem.MaNhacungcap = txtNhacungcap.myCode;
                        _newItem.IdThuoc =
                            Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc].Value);
                        _newItem.NgayHethan =
                            Convert.ToDateTime(gridExRow.Cells["NGAY_HET_HAN"].Value).Date;
                        _newItem.GiaNhap = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap].Value);
                        _newItem.SoLo = Utility.sDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLo].Value);
                        _newItem.SoLuong = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLuong].Value);
                        _newItem.ThanhTien = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value);
                        _newItem.ChietKhau = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau].Value);
                        _newItem.Vat = Utility.DecimaltoDbnull(txtVAT.Text);
                        _newItem.GiaBan = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value, 0);
                        _newItem.SluongChia = 1;
                        _newItem.GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt].Value);
                        _newItem.GiaBhytCu = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBhytCu].Value);
                        _newItem.CoBhyt = Utility.ByteDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.CoBhyt].Value);
                        _newItem.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen].Value);
                        _newItem.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen].Value);

                        _newItem.ThangDu = Utility.Int16Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThangDu].Value, 0);
                        _newItem.KieuThuocvattu = KIEU_THUOC_VT;
                        lstDetails.Add(_newItem);
                    }
                }
                return lstDetails;
            }
            catch(Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo dữ liệu phiếu chi tiết. Đề nghị kiểm tra lại\n",ex);
                return lstDetails;
            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        private void ThemPhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = CreatePhieuNhapKho();
            List< TPhieuNhapxuatthuocChitiet> lstDetail=GetReceiptDetails();
            if (lstDetail.Count <= 0)
            {
               
                return;
            }
            ActionResult actionResult = new THUOC_NHAPKHO().ThemPhieuNhapKho(objPhieuNhap, lstDetail);
            switch (actionResult)
            {
                case ActionResult.Success:
                    txtIDPhieuNhapKho.Text = Utility.sDbnull(objPhieuNhap.IdPhieu);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();
                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc,ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList,TPhieuNhapxuatthuoc.Columns.IdPhieu,Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"] ,"Thêm mới phiếu thành công",false);
                    em_Action = action.Update;
                    ClearControlPhieu();
                    ClearControl();
                    txtSoHoaDon.Focus();
                    b_Cancel = true;
                    if (PropertyLib._NhapkhoProperties.Themmoilientuc) cmdThemPhieuNhap.PerformClick();
                    else
                        this.Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm phiếu", "Thông báo lỗi",MessageBoxIcon.Error);
                    break;
            }
        }
        private void UpdatePhieuNhapKho()
        {
            TPhieuNhapxuatthuoc objPhieuNhap = CreatePhieuNhapKho();
            List<TPhieuNhapxuatthuocChitiet> lstDetail = GetReceiptDetails();
            if (lstDetail.Count <= 0)
            {

                return;
            }
            ActionResult actionResult = new THUOC_NHAPKHO().UpdatePhieuNhapKho(objPhieuNhap, lstDetail);
            switch (actionResult)
            {
                case ActionResult.Success:
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow[] arrDr =
                        p_mDataPhieuNhapKho.Select(string.Format("{0}={1}", TPhieuNhapxuatthuoc.Columns.IdPhieu,
                                                                 Utility.Int32Dbnull(txtIDPhieuNhapKho.Text)));
                    if(arrDr.GetLength(0)>0)
                    {
                        arrDr[0].Delete();
                    }
                    DataRow newDr = p_mDataPhieuNhapKho.NewRow();

                    Utility.FromObjectToDatarow(objTPhieuNhapxuatthuoc, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoNhap.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    p_mDataPhieuNhapKho.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList, TPhieuNhapxuatthuoc.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"] ,"Bạn sửa  phiếu thành công", false);
                    em_Action = action.Insert;
                    ClearControlPhieu();
                    ClearControl();
                    b_Cancel = true;
                    //if (PropertyLib._NhapkhoProperties.Themmoilientuc) cmdThemPhieuNhap.PerformClick();
                    //else
                        this.Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        #endregion
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC IN PHIẾU ĐƠN THUỐC NHẬP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
            int IdPhieunhap = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);
            VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuNhapkho(IdPhieunhap, "PHIẾU NHẬP", globalVariables.SysDate);
        }
        /// <summary>
        /// hàm thực hiện việc cho phép nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTongHoaDon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtTongHoaDon_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
          
        }

      
        /// <summary>
        /// hàm thực hiện việc nhập kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKhoNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string sKeyChar = Utility.sDbnull(e.KeyChar);
                if (!string.IsNullOrEmpty(sKeyChar))
                {
                    var query = from khonhap in m_dtDataKhoNhap.AsEnumerable()
                                let y = khonhap[TDmucKho.Columns.IdKho]
                                where Utility.sDbnull(khonhap[TDmucKho.Columns.MaKho]) == sKeyChar
                                select y;
                    if (query.Count() > 0)
                    {
                        cboKhoNhap.SelectedValue = Utility.sDbnull(query.FirstOrDefault());
                    }
                }
            }
            catch (Exception)
            {


            }
           
        }
       
        /// <summary>
        /// hàm thực hiện việc nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPhieuNhapChiTiet_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {

            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.SoLuong.ToUpper())
            {
                int soluong = Utility.Int32Dbnull(e.Value);
                decimal GiaNhap = Utility.DecimaltoDbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.GiaNhap));
                decimal chietkhau = Utility.DecimaltoDbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.ChietKhau));
               
                grdPhieuNhapChiTiet.CurrentRow.BeginEdit();
                decimal thanhtien = ThanhTienTrenLuoi(GiaNhap, soluong, chietkhau);
                grdPhieuNhapChiTiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                
                grdPhieuNhapChiTiet.CurrentRow.EndEdit();
                grdPhieuNhapChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaNhap.ToUpper())
            {
                int soluong = Utility.Int32Dbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.SoLuong));
                decimal GiaNhap = Utility.DecimaltoDbnull(e.Value);
                decimal chietkhau = Utility.DecimaltoDbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.ChietKhau));
                int THANG_DU = TinhThangDutheoQuyetDinhBYT(GiaNhap);
                if (!PropertyLib._NhapkhoProperties.CoThangDu) THANG_DU = 0;
                decimal Gia_ban = TinhGiaBan(GiaNhap, Utility.Int32Dbnull(txtVAT.Text, 0), THANG_DU);
                grdPhieuNhapChiTiet.CurrentRow.BeginEdit();
                decimal thanhtien = ThanhTienTrenLuoi(GiaNhap, soluong, chietkhau);
                grdPhieuNhapChiTiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                grdPhieuNhapChiTiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value = Gia_ban;
                grdPhieuNhapChiTiet.CurrentRow.EndEdit();
                grdPhieuNhapChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ChietKhau.ToUpper())
            {
                int soluong = Utility.Int32Dbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.SoLuong));
                decimal chietkhau = Utility.DecimaltoDbnull(e.Value);
                decimal  GiaNhap= Utility.DecimaltoDbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.GiaNhap));
                grdPhieuNhapChiTiet.CurrentRow.BeginEdit();
                decimal thanhtien = ThanhTienTrenLuoi(GiaNhap, soluong, chietkhau);
                grdPhieuNhapChiTiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                grdPhieuNhapChiTiet.CurrentRow.EndEdit();
                grdPhieuNhapChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ThangDu.ToUpper())
            {
                int  THANG_DU = Utility.Int32Dbnull(e.Value,0);
                if (!PropertyLib._NhapkhoProperties.CoThangDu) THANG_DU = 0;
                decimal GiaNhap = Utility.Int32Dbnull(grdPhieuNhapChiTiet.GetValue(TPhieuNhapxuatthuocChitiet.Columns.GiaNhap), 0);
                decimal Gia_ban = TinhGiaBan(GiaNhap, Utility.Int32Dbnull(txtVAT.Text, 0), THANG_DU);
                grdPhieuNhapChiTiet.CurrentRow.BeginEdit();
                grdPhieuNhapChiTiet.CurrentRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value = Gia_ban;
                grdPhieuNhapChiTiet.CurrentRow.EndEdit();
                grdPhieuNhapChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.NgayHethan.ToUpper())
            {
                if(string.IsNullOrEmpty(Utility.sDbnull(e.Value)))
                {
                    Utility.ShowMsg("Ngày hết hạn không thể bỏ trống \n Mời bạn xem lại","Thông báo",MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    DateTime NgayHethancu = Convert.ToDateTime(e.InitialValue);
                    DateTime NgayHethanmoi = Convert.ToDateTime(e.Value);
                    if(!SubSonic.Sugar.Dates.IsDate(NgayHethanmoi))
                    {
                        Utility.ShowMsg("Ngày hết hạn không đúng định dạng \n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
               

            }
            TinhSumThanhTien();
        }
        private decimal ThanhTienTrenLuoi(decimal  GiaNhap, int soluong,decimal  chietkhau)
        {
            decimal thanhtien = GiaNhap * soluong;
            thanhtien = thanhtien + thanhtien * Utility.DecimaltoDbnull(txtVAT.Text) / 100 - chietkhau;
            return thanhtien;
        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của vat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateWhenChanged();
                ThanhTien();
                TinhGiaBan();
            }
            catch
            {
            }
        }
        private void UpdateWhenChanged()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuNhapChiTiet.GetDataRows())
                {
                    Int32 soluong = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.SoLuong].Value);
                    int THANG_DU = Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThangDu].Value, 0);
                    decimal GiaNhap = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaNhap].Value);
                    decimal chitietkhau = Utility.DecimaltoDbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau].Value);
                    decimal thanhtien = soluong * GiaNhap + (soluong * GiaNhap) * Utility.Int32Dbnull(txtVAT.Text, 0) / 100 - chitietkhau;
                    decimal Gia_ban = TinhGiaBan(GiaNhap, Utility.Int32Dbnull(txtVAT.Text, 0), THANG_DU);
                    gridExRow.BeginEdit();
                    gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien].Value = thanhtien;
                    gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.GiaBan].Value = Gia_ban;
                    gridExRow.EndEdit();
                }
                grdPhieuNhapChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
                TinhSumThanhTien();
            }
            catch
            {
            }
        }

        private void txtDongia_TextChanged(object sender, EventArgs e)
        {
            if ( PropertyLib._NhapkhoProperties.CoThangDu)
                nmrThangDu.Value = TinhThangDutheoQuyetDinhBYT(Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtDongia.Text, 0), 0));
            else
                nmrThangDu.Value = 0;
            TinhGiaBan();
            ThanhTien();
            
        }
        void TinhGiaBan()
        {
            try
            {
                if (!Utility.IsNumeric(txtDongia.Text))
                {
                    txtGiaban.Text = "0";
                    return;
                }
                //Giá VAT
                decimal GiaVAT = Utility.DecimaltoDbnull(txtDongia.Text, 0)+(decimal)(Utility.DecimaltoDbnull(txtDongia.Text, 0) * Utility.Int32Dbnull(txtVAT.Text) / 100);
                //Thặng dư so với giá VAT
                decimal ThangDu = (decimal)(GiaVAT * nmrThangDu.Value / 100);
                decimal GIA_BAn = ThangDu + GiaVAT;
                txtGiaban.Text = GIA_BAn.ToString();

            }
            catch
            {
            }
        }
        decimal TinhGiaBan(decimal GiaNhap,int VAT,int ThangDu)
        {
            try
            {
                //Giá VAT
                decimal GiaVAT = GiaNhap + (decimal)(GiaNhap * VAT / 100);
                //Thặng dư so với giá VAT
                decimal GiaThangDu = (decimal)(GiaVAT * ThangDu / 100);
                return  GiaThangDu + GiaVAT;
            }
            catch
            {
                return GiaNhap;
            }
        }
       
        int TinhThangDutheoQuyetDinhBYT(decimal GiaMua)
        {
            if (KIEU_THUOC_VT == "VT") return 0;
            if (GiaMua <= 1000) return 15;
            if (GiaMua > 1000 && GiaMua <= 5000) return 10;
            if (GiaMua > 5000 && GiaMua <= 100000) return 7;
            if (GiaMua > 100000 && GiaMua <= 1000000) return 5;
            if (GiaMua > 1000000) return 2;
            return 0;
        }
        private void txtDongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtThanhTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void grdPhieuNhapChiTiet_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.SoLuong.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaNhap.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaBan.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ChietKhau.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuDungtuyen.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.GiaPhuthuTraituyen.ToUpper()
                || e.Column.Key.ToUpper() == TPhieuNhapxuatthuocChitiet.Columns.ThangDu.ToUpper()

                )
            {
                e.Column.InputMask = "{0:#,#.##}";
            }
          //  UpdateWhenChanged();
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._NhapkhoProperties);
                
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception exception)
            {
            }
        }

        private void nmrThangDu_Click(object sender, EventArgs e)
        {

        }

      
    }
}
