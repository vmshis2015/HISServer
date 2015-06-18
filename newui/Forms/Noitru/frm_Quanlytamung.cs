using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlytamung : Form
    {
        public delegate void OnChangedData();
        
        public event OnChangedData _OnChangedData;

        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_maskedEdit = false;
        NoitruTamung objTamung = null;
        private DataTable m_dtTimKiembenhNhan=new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;

        public frm_Quanlytamung()
        {
            InitializeComponent();
            LoadConfig();
            InitEvents();
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
        }
        void InitEvents()
        {
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
            grdTamung.SelectionChanged += new EventHandler(grdTamung_SelectionChanged);
            grdTamung.KeyDown += new KeyEventHandler(grdTamung_KeyDown);
            txtSotien._OnTextChanged += new MaskedTextBox.MaskedTextBox.OnTextChanged(txtSotien__OnTextChanged);

            txtLydo._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtLydo__OnShowData);
            txtLydo._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtLydo__OnSaveAs);

            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            cmdIn.Click += new EventHandler(cmdIn_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);
            cmdTimKiem.Click+=cmdTimKiem_Click;
            cmdConfig.Click+=cmdConfig_Click;
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkPrintPreview.CheckedChanged += chkPrintPreview_CheckedChanged;
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
                    objTamung.IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                    objTamung.MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    objTamung.IdKhoanoitru = Utility.Int16Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdKhoanoitru));
                    objTamung.IdBuonggiuong = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdRavien));
                    objTamung.IdBuong = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBuong));
                    objTamung.IdGiuong = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdGiuong));
                    
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
                        newDr["ten_khoanoitru"] = Utility.sDbnull(grdList.GetValue("ten_khoanoitru"));
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
                        _myDr["ten_khoanoitru"] = Utility.sDbnull(grdList.GetValue("ten_khoanoitru"));
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
            objLuotkham = Utility.getKcbLuotkhamFromGrid(grdList);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần tạm ứng", true);
                return false;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền tạm ứng");
                return false;
            }
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham))
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú", true);
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSotien.Text) <= 0)
            {
                Utility.SetMsg(lblMsg,"Bạn cần nhập số tiền tạm ứng >0 ",true);
                txtSotien.SelectAll();
                txtSotien.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLydo.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập lý do thu tiền tạm ứng ", true);
                txtLydo.SelectAll();
                txtLydo.Focus();
                return false;
            }
            if (txtNguoithu.MyID.ToString()=="-1")
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
                if (grdList.GetDataRows().Count() <= 0)
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
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(Utility.Int32Dbnull(m_dtReport.Compute("SUM(so_tien)", "1=1"),0).ToString()));
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
              if(_OnChangedData!=null)  _OnChangedData();
            }
            catch (Exception ex)
            {
                
            }
        }
        bool isValidXoatamung()
        {
            return true;
        }
        void cmdSua_Click(object sender, EventArgs e)
        {
            m_enAct = action.Update;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();  
        }

        void cmdthemmoi_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần nộp tiền tạm ứng");
                return;
            }
            if (Utility.Byte2Bool( objLuotkham.TthaiThopNoitru ) && objLuotkham.TrangthaiNoitru==5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền tạm ứng");
                return;
            }
            m_enAct = action.Insert;
            AllowedChanged_maskedEdit = true;
            SetControlStatus(); 
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
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Quanlytamung_Load(object sender, EventArgs e)
        {
            AutoCompleteTextBox();
            InitData();
            fillSearchData();
            TimKiemThongTin();
            SetControlStatus();
            ModifyCommand();
            
        }
        void fillSearchData()
        {
            try
            {
                if (objLuotkham != null)
                {
                    txtPatientCode.Text = objLuotkham.MaLuotkham;
                    txtPatientName.Clear();
                    chkByDate.Checked = false;
                    cboKhoaChuyenDen.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }
        private DataTable m_dtKhoaNoiTru=new DataTable();
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
           
            
            m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            DataBinding.BindDataCombobox(cboKhoaChuyenDen, m_dtKhoaNoiTru,
                                       DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,"",true);
            string _rowFilter = "1=1";
            
                //if (PropertyLib._NoitruProperties.HienthiKhoatheonguoidung)
                //{
                //    if (!globalVariables.IsAdmin)
                //    {
                //        _rowFilter = string.Format("{0}={1}", DmucKhoaphong.Columns.IdKhoaphong, globalVariables.IdKhoaNhanvien);
                //    }
                //}
               
           
           
            m_dtKhoaNoiTru.DefaultView.RowFilter = _rowFilter;
            m_dtKhoaNoiTru.AcceptChanges();

        }
        void AutoCompleteTextBox()
        {
            txtLydo.Init();
            AutoCompleteTextBox_nguoithu();
            txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);

        }
        void AutoCompleteTextBox_nguoithu()
        {
            DataTable m_dtNhanvien = SPs.DmucLaydanhsachTnv().GetDataSet().Tables[0];
            try
            {
                if (m_dtNhanvien == null) return;
                if (!m_dtNhanvien.Columns.Contains("ShortCut"))
                    m_dtNhanvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtNhanvien.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    shortcut = dr[DmucNhanvien.Columns.MaNhanvien].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
                var source = new List<string>();
                var query = from p in m_dtNhanvien.AsEnumerable()
                            select p[DmucNhanvien.Columns.IdNhanvien].ToString() + "#" + p[DmucNhanvien.Columns.MaNhanvien].ToString() + "@" + p[DmucNhanvien.Columns.TenNhanvien].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtNguoithu.AutoCompleteList = source;
                this.txtNguoithu.TextAlign = HorizontalAlignment.Left;
                this.txtNguoithu.CaseSensitive = false;
                this.txtNguoithu.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {


            }

        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            bool isValid2 = Utility.isValidGrid(grdTamung);
            cmdSua.Enabled = isValid && isValid2;
            cmdxoa.Enabled = isValid && isValid2;
            cmdIn.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
           
        }

        private string _rowFilter = "1=1";
        private void TimKiemThongTin()
        {
            m_dtTimKiembenhNhan =SPs.NoitruTimkiembenhnhan(Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue),
                                                txtPatientCode.Text, 1,
                                                chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                string.Empty, (int?) _TrangthaiNoitru,-1,0).
                    GetDataSet().Tables[0];
            _rowFilter = "1=1";
                //if (PropertyLib._NoitruProperties.HienthiKhoatheonguoidung)
                //{
                //    _rowFilter = string.Format("{0}={1}", NoitruPhanbuonggiuong.Columns.IdKhoanoitru,
                //        Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue));
                //}
            Utility.SetDataSourceForDataGridEx(grdList, m_dtTimKiembenhNhan, true, true, _rowFilter, "");
            AllowedChanged = true;
            grdList.MoveFirst();
            if (grdList.GetDataRows().Length <= 0)
            {
                Utility.SetMsg(lblMsg, "", false);
                lblTongtien.Text = "Tổng tiền:";
            }
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        
       
       
      
      
        
        
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            LayLichsuTamung();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();
        }
        DataTable m_dtTamung = null;
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
                objLuotkham = Utility.getKcbLuotkhamFromGrid(grdList);
                m_dtTamung = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)),
                    Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)),
                    0,
                    Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdKhoanoitru))
                    );
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
                setTongtienStatus();
                ShowLSuTamung();
            }
        }
        void ShowLSuTamung()
        {
            if (!Utility.isValidGrid(grdList) || grdTamung.GetDataRows().Length <= 1)
            {
                grdTamung.Width = 0;
            }
            else
            {
                grdTamung.Width = 425;
            }
        }
       
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtPatientCode.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtPatientCode.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtPatientCode.Text);
                txtPatientCode.Text = MaLuotkham;
                txtPatientCode.Select(txtPatientCode.Text.Length, txtPatientCode.Text.Length);
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
            if (e.KeyCode == Keys.F3)
            {
                cmdTimKiem.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (m_enAct == action.FirstOrFinished)
                {
                    this.Close();
                }
                else
                {
                    cmdHuy.PerformClick();
                }
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                txtPatientCode.Focus();
                txtPatientCode.SelectAll();
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
