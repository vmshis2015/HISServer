using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.IO;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UCs.Noitru
{
    public partial class ucTamung : UserControl
    {
        public delegate void OnChangedData();

        public event OnChangedData _OnChangedData;

        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_maskedEdit = false;
        NoitruTamung objTamung = null;
        private DataTable m_dtTimKiembenhNhan = new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;
        public ucTamung()
        {
            InitializeComponent();
            InitEvents();
        }
        public void ChangePatients(KcbLuotkham objLuotkham,string maloailydothu)
        {
            AllowedChanged = false;
            this.objLuotkham = objLuotkham;
            if (maloailydothu != string.Empty)
                txtLydo.LOAI_DANHMUC = maloailydothu;
            Init();
        }
       
        void InitEvents()
        {
            grdTamung.SelectionChanged += new EventHandler(grdTamung_SelectionChanged);
            grdTamung.KeyDown += new KeyEventHandler(grdTamung_KeyDown);
            txtSotien._OnTextChanged += new MaskedTextBox.MaskedTextBox.OnTextChanged(txtSotien__OnTextChanged);
            
            txtLydo._OnShowData+=txtLydo__OnShowData;
            txtLydo._OnSaveAs+=txtLydo__OnSaveAs;

            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            cmdIn.Click += new EventHandler(cmdIn_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);
            cmdConfig.Click += cmdConfig_Click;
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkPrintPreview.CheckedChanged += chkPrintPreview_CheckedChanged;
            foreach(Control ctrl in panel2.Controls)
                ctrl.KeyDown += ctrl_KeyDown;
        }

        void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            Control _ctrl = sender as Control;
            if (e.KeyCode == Keys.Enter)
                SelectNextControl(_ctrl, true, true, true, true);
        }

       

        void chkPrintPreview_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewPhieuTamung = chkPrintPreview.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.InsaukhiLuu = chkSaveAndPrint.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }

        void txtLydo__OnSaveAs()
        {
            if (Utility.DoTrim(txtLydo.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtLydo.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        void txtLydo__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        void grdTamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdTamung) && e.KeyCode == Keys.Delete) cmdxoa.PerformClick();
        }

        void txtSotien__OnTextChanged(string text)
        {
            if (AllowedChanged_maskedEdit)
                Utility.SetMsg(lblMsg, text, false);
        }
        void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            try
            {
                if (m_enAct == action.Insert)
                {
                    objTamung = new NoitruTamung();
                    objTamung.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objTamung.MaLuotkham =objLuotkham.MaLuotkham;
                    objTamung.IdKhoanoitru =objLuotkham.IdKhoanoitru;
                    objTamung.IdBuonggiuong = objLuotkham.IdRavien;
                    objTamung.IdBuong = objLuotkham.IdBuong;
                    objTamung.IdGiuong =objLuotkham.IdGiuong;
                    objTamung.Noitru = objLuotkham.TrangthaiNoitru;
                    objTamung.KieuTamung = 0;
                    objTamung.MotaThem = txtLydo.Text;
                    objTamung.IdTnv = Utility.Int32Dbnull(txtNguoithu.MyID, -1);
                    objTamung.SoTien = Utility.DecimaltoDbnull(txtSotien.Text);
                    objTamung.NgayTamung = dtpNgaythu.Value;
                    objTamung.TrangThai = 0;
                    objTamung.IsNew = true;
                    if (noitru_TamungHoanung.NoptienTamung(objTamung))
                    {
                        DataRow newDr = m_dtTamung.NewRow();
                        Utility.FromObjectToDatarow(objTamung, ref newDr);
                        newDr["sngay_tamung"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                        newDr["ten_khoanoitru"] = "";
                        newDr["ten_nhanvien"] = txtNguoithu.Text;
                        m_dtTamung.Rows.Add(newDr);
                        m_dtTamung.AcceptChanges();
                        Utility.GotoNewRowJanus(grdTamung, NoitruTamung.Columns.Id, objTamung.Id.ToString());
                        if (chkSaveAndPrint.Checked)
                            cmdIn_Click(cmdIn, e);
                        m_enAct = action.FirstOrFinished;
                    }
                }
                else
                {
                    objTamung.SoTien = Utility.DecimaltoDbnull(txtSotien.Text);
                    objTamung.NgayTamung = dtpNgaythu.Value;
                    objTamung.MotaThem = txtLydo.Text;
                    objTamung.IdTnv = Utility.Int32Dbnull(txtNguoithu.MyID, -1);
                    objTamung.IsNew = false;
                    objTamung.MarkOld();
                    if (noitru_TamungHoanung.NoptienTamung(objTamung))
                    {

                        DataRow _myDr = ((DataRowView)grdTamung.CurrentRow.DataRow).Row;
                        _myDr[NoitruTamung.Columns.SoTien] = Utility.DecimaltoDbnull(txtSotien.Text);
                        _myDr[NoitruTamung.Columns.NgayTamung] = dtpNgaythu.Value;
                        _myDr[NoitruTamung.Columns.MotaThem] = txtLydo.Text;
                        _myDr[NoitruTamung.Columns.IdTnv] = Utility.Int32Dbnull(txtNguoithu.MyID, -1);

                        _myDr["sngay_tamung"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                        _myDr["ten_khoanoitru"] = "";
                        _myDr["ten_nhanvien"] = txtNguoithu.Text;

                        m_dtTamung.AcceptChanges();
                        m_enAct = action.FirstOrFinished;
                    }
                }
                setTongtienStatus();
                SetControlStatus();
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần tạm ứng", true);
                return false;
            }
            //Kiểm tra tạm ứng ngoại trú
            if (objLuotkham.TrangthaiNoitru<=0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU",true)=="1")
            {
                return true;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền tạm ứng");
                return false;
            }
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú", true);
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSotien.Text) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập số tiền tạm ứng >0 ", true);
                txtSotien.SelectAll();
                txtSotien.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLydo.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập lý do thu tiền tạm ứng ", true);
                txtLydo.SelectAll();
                txtLydo.Focus();
                return false;
            }
            if (txtNguoithu.MyID.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập tên người thu tiền tạm ứng(Có thể xóa trắng và nhập phím cách để ra tất cả các nhân viên trong hệ thống)", true);
                txtNguoithu.SelectAll();
                txtNguoithu.Focus();
                return false;
            }

            return true;
        }
        void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }

        void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham==null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân in phiếu tạm ứng");
                    return;
                }
                if (grdTamung.GetDataRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu tạm ứng muốn in");
                    return;
                }
                if (!Utility.isValidGrid(grdTamung))
                {

                    grdTamung.MoveFirst();

                }
                DataTable m_dtReport = noitru_TamungHoanung.NoitruInphieutamung(Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdTamung, NoitruTamung.Columns.Id), -1));
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "noitru_phieutamung.xml");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string tieude = "", reportname = "";
                var crpt = Utility.GetReport("noitru_phieutamung", ref tieude, ref reportname);
                if (crpt == null) return;

                MoneyByLetter _moneyByLetter = new MoneyByLetter();
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref m_dtReport);

                crpt.SetDataSource(m_dtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieutamung";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "TelePhone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(Utility.Int32Dbnull(m_dtReport.Compute("SUM(so_tien)", "1=1"), 0).ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;

                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewPhieuTamung))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(objForm.getPrintNumber, false, 0, 0);
                }


            }
            catch (Exception)
            {
            }
        }

        void cmdxoa_Click(object sender, EventArgs e)
        {
            Xoatamung();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        void Xoatamung()
        {
            if (!isValidXoatamung()) return;
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa dòng tạm ứng đang chọn này hay không?", "Xác nhận", true))
                {
                    if (objTamung != null)
                    {
                        frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung("LYDOHUYTAMUNG", "Hủy tiền tạm ứng", "Chọn lý do hủy tiền tạm ứng...", "Lý do hủy");
                        _Chondanhmucdungchung.ShowDialog();
                        if (!_Chondanhmucdungchung.m_blnCancel)
                        {
                            if (noitru_TamungHoanung.XoaTienTamung(objTamung, grdTamung.GetDataRows().Length - 1 > 0, _Chondanhmucdungchung.ten))
                            {
                                Utility.SetMsg(lblMsg, string.Format("Xóa tạm ứng {0} thành công", txtSotien.Text), false);
                                DataRow drDelete = Utility.getCurrentDataRow(grdTamung);
                                if (drDelete != null)
                                {
                                    m_dtTamung.Rows.Remove(drDelete);
                                    m_dtTamung.AcceptChanges();
                                }
                            }
                        }
                        SetControlStatus();
                    }
                    else
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng tạm ứng cần xóa"), true);
                    }
                }
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
        }
        bool isValidXoatamung()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần tạm ứng", true);
                return false;
            }
            if (objTamung.TrangThai == 1)
            {
                Utility.SetMsg(lblMsg, "Đã hoàn ứng cho phần tạm ứng đang chọn nên bạn không thể hủy tạm ứng", true);
                return false;
            }
            if (objLuotkham.TrangthaiNoitru<=0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", true) == "1")
            {
                return true;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã xuất viện nên bạn không thể hủy tiền tạm ứng");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được tài chính duyệt nên bạn không thể hủy tiền tạm ứng");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được tổng hợp xuất viện nên bạn không thể hủy tiền tạm ứng");
                return false;
            }
            return true;
        }
        void cmdSua_Click(object sender, EventArgs e)
        {
            if (objTamung != null && objTamung.TrangThai == 1)
            {
                Utility.ShowMsg("Đã hoàn ứng cho phần tạm ứng đang chọn nên bạn không thể sửa tạm ứng");
                return;
            }
            m_enAct = action.Update;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
        public void Themmoi()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần nộp tiền tạm ứng");
                return;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền tạm ứng");
                return;
            }
            m_enAct = action.Insert;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
         void cmdthemmoi_Click(object sender, EventArgs e)
        {
            Themmoi();
        }
        private void SetControlStatus()
        {
            try
            {
                grdTamung.Enabled = false;
                AllowedChanged = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        //Cho phép nhập liệu mã loại đối tượng,vị trí, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = true;
                        txtSotien.Enabled = true;
                        txtLydo.Enabled = true;
                        txtNguoithu.Enabled = true;
                        objTamung = null;
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtSotien.Text = "0";
                        txtLydo.SetCode("-1");
                        txtNguoithu.SetCode("-1");


                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();

                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục ID để người dùng nhập liệu
                        txtID.Text = "Tự sinh";
                        cmdHuy.Text = "Hủy";
                        txtSotien.Focus();
                        break;
                    case action.Update:
                        //Không cho phép cập nhật lại mã loại đối tượng
                        Utility.DisabledTextBox(txtID);
                        //Cho phép cập nhật lại vị trí, tên loại đối tượng và mô tả thêm
                        dtpNgaythu.Enabled = true;
                        txtLydo.Enabled = true;
                        txtNguoithu.Enabled = true;
                        txtSotien.Enabled = true;
                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();
                        cmdHuy.Text = "Hủy";
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục Position để người dùng nhập liệu
                        txtSotien.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                        AllowedChanged_maskedEdit = false;
                        grdTamung.Enabled = true;
                        AllowedChanged = true;
                        //Không cho phép nhập liệu mã loại đối tượng, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = false;
                        txtLydo.Enabled = false;
                        txtNguoithu.Enabled = false;
                        txtSotien.Enabled = false;

                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                        //Cho phép thêm mới
                        cmdGhi.Enabled = false;
                        cmdHuy.Enabled = false;
                        cmdGhi.SendToBack();
                        cmdHuy.SendToBack();
                        //Nút Hủy biến thành nút thoát
                        cmdHuy.Text = "Thoát";
                        //--------------------------------------------------------------
                        //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = true;
                        //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                        grdTamung_SelectionChanged(grdTamung, new EventArgs());
                        //Tự động Focus đến nút thêm mới? 
                        cmdthemmoi.Focus();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                grdTamung.Enabled = true;
                setTongtienStatus();
            }

        }
        void grdTamung_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged) return;
            FillData();
        }
        void FillData()
        {
            try
            {
                if (!Utility.isValidGrid(grdTamung))
                {

                    objTamung = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtSotien.Text = "0";
                    txtLydo.SetCode("-1");
                    txtNguoithu.SetCode("-1");
                }
                else
                {

                    objTamung = NoitruTamung.FetchByID(Utility.Int32Dbnull(grdTamung.GetValue(NoitruTamung.Columns.Id)));

                    if (objTamung == null)
                    {
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtSotien.Text = "0";
                        txtLydo.SetCode("-1");
                        txtNguoithu.SetCode("-1");
                    }
                    else
                    {
                        objTamung.IsNew = false;
                        objTamung.MarkOld();
                        dtpNgaythu.Value = objTamung.NgayTamung.Value;
                        txtSotien.Text = objTamung.SoTien.ToString();
                        txtLydo._Text = objTamung.MotaThem;
                        txtNguoithu.SetId(objTamung.IdTnv);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ModifyCommand();
                //SetControlStatus();
            }
        }
        

        public void Init()
        {
            LoadConfig();
            LayLichsuTamung();
            AutoCompleteTextBox();
            
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();

        }
        
        private DataTable m_dtKhoaNoiTru = new DataTable();
      
        void AutoCompleteTextBox()
        {
            txtLydo.Init();
            AutoCompleteTextBox_nguoithu();
            txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);

        }
        void AutoCompleteTextBox_nguoithu()
        {
            DataTable m_dtNhanvien = SPs.DmucLaydanhsachTnv().GetDataSet().Tables[0];
            txtNguoithu.Init(m_dtNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
        }
       
        private void ModifyCommand()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdTamung);
            cmdSua.Enabled = isValid && isValid2;
            cmdxoa.Enabled = isValid && isValid2;
            cmdIn.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;

        }

       
        public DataTable m_dtTamung = null;
        void setTongtienStatus()
        {
            try
            {
                if (m_dtTamung == null) return;
                lblTongtien.Text = "Tổng tiền: " + new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(m_dtTamung.Compute("SUM(so_tien)", "1=1"), "0"));
            }
            catch (Exception ex)
            {

            }

        }
        void LayLichsuTamung()
        {
            try
            {
                m_dtTamung = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham,
                  Utility.Int32Dbnull(objLuotkham.IdBenhnhan, 0),
                  0, objLuotkham.TrangthaiNoitru > 0 ? Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, 0) : -1,(byte)(objLuotkham.TrangthaiNoitru > 0 ?1:0));
                Utility.SetDataSourceForDataGridEx_Basic(grdTamung, m_dtTamung, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                grdTamung.MoveFirst();
                if (grdTamung.GetDataRows().Length <= 0)
                {
                    objTamung = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtSotien.Text = "0";
                    txtLydo.SetCode("-1");
                    txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                AllowedChanged = true;
                setTongtienStatus();
                ShowLSuTamung();
            }
        }
        void ShowLSuTamung()
        {
            if (objLuotkham != null)
            {
                grdTamung.Width = 0;
            }
            else
            {
                grdTamung.Width = 425;
            }
        }

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlytamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }

            if (e.KeyCode == Keys.N && e.Control)
            {
                cmdthemmoi.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                cmdGhi.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.U && e.Control)
            {
                cmdSua.PerformClick();
                return;
            }
        }

        void LoadConfig()
        {
            chkPrintPreview.Checked = PropertyLib._MayInProperties.PreviewPhieuTamung;
            chkSaveAndPrint.Checked = PropertyLib._NoitruProperties.InsaukhiLuu;
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
            LoadConfig();
        }
    }
}
