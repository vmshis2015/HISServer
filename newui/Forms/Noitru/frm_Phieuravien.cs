using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_Phieuravien : Form
    {
        bool AllowTextChanged = false;
        NoitruPhieuravien objRavien = null;
        public bool mv_blnCancel = true;
        public KcbLuotkham objLuotkham = null;
        action m_enAct = action.Insert;
        public bool AutoLoad = false;
        public frm_Phieuravien()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            txtMaluotkham.KeyDown += new KeyEventHandler(txtMaluotkham_KeyDown);
            this.KeyDown += new KeyEventHandler(frm_Phieuravien_KeyDown);
            this.Load += new EventHandler(frm_Phieuravien_Load);

            txtKieuchuyenvien._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtKieuchuyenvien__OnSaveAs);
            txtKieuchuyenvien._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtKieuchuyenvien__OnShowData);

            txtKqdieutri._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtKqdieutri__OnSaveAs);
            txtKqdieutri._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtKqdieutri__OnShowData);

            txtTinhtrangravien._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtTinhtrangravien__OnSaveAs);
            txtTinhtrangravien._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTinhtrangravien__OnShowData);

            txtPhuongphapdieutri._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtPhuongphapdieutri__OnSaveAs);
            txtPhuongphapdieutri._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtPhuongphapdieutri__OnShowData);

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdChuyen.Click += new EventHandler(cmdChuyen_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);

            cmdgetPatient.Click += new EventHandler(cmdgetPatient_Click);
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            cmdPrint.Click += new EventHandler(cmdPrint_Click);

            chkChuyenvien.CheckedChanged += chkChuyenvien_CheckedChanged;
            dtpNgayravien.ValueChanged += dtpNgayravien_ValueChanged;
        }

        void dtpNgayravien_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowTextChanged) return;
            txtTongSoNgayDtri.Text = THU_VIEN_CHUNG.Songay(objLuotkham.NgayNhapvien.Value, new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day, Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhuRaVien.Text, 0), 0)).ToString();
        }

        void chkChuyenvien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled = cmdGetBV.Enabled = txtNguoivanchuyen.Enabled = cboDoctorAssign.Enabled = txtphuongtienvc.Enabled = chkChuyenvien.Checked;
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData =
                                 SPs.NoitruInphieuravien(Utility.DoTrim(txtMaluotkham.Text)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_phieuravien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("noitru_phieuravien", ref tieude, ref reportname);
                if (crpt == null) return;
                frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtData);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieuravien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", baocaO_TIEUDE1.TIEUDE);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtpNgayin.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }
        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
            
        }

        void cmdgetPatient_Click(object sender, EventArgs e)
        {
            frm_DSACH_BN_TKIEM _DSACH_BN_TKIEM = new frm_DSACH_BN_TKIEM();
            if (_DSACH_BN_TKIEM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMaluotkham.Text = _DSACH_BN_TKIEM.MaLuotkham;
                txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
            }
        }

        void cmdHuy_Click(object sender, EventArgs e)
        {
            if(objLuotkham==null)
            {
                Utility.SetMsg(lblMsg,"Bạn cần chọn bệnh nhân trước khi thực hiện hủy chuyển viện",true);
                return;
            }
            if (objLuotkham.TrangthaiNoitru==4)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể hủy ra viện", true);
                return;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể hủy ra viện", true);
                return;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể hủy ra viện", true);
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy ra viện cho bệnh nhân {0} hay không?", txtTenBN.Text), "Xác nhận hủy ra viện", true))
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            objLuotkham.TthaiChuyendi=0;
                            objLuotkham.IdBenhvienDi=-1;
                            objLuotkham.IdBacsiChuyenvien = -1;
                            objLuotkham.TrangthaiNoitru = 2;
                            objLuotkham.NgayRavien = null;
                            objLuotkham.SoRavien = 0;
                            objLuotkham.IsNew=false;
                            objLuotkham.MarkOld();
                            objLuotkham.Save();
                            new Delete().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdRavien).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1)).Execute();
                            NoitruPhanbuonggiuong objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien.Value);
                            if (objNoitruPhanbuonggiuong != null)
                            {
                                objNoitruPhanbuonggiuong.MarkOld();
                                objNoitruPhanbuonggiuong.IsNew = false;
                                objNoitruPhanbuonggiuong.SoLuong = 0;
                                objNoitruPhanbuonggiuong.SoluongGio = 0;
                                objNoitruPhanbuonggiuong.NgayKetthuc = null;
                                objNoitruPhanbuonggiuong.Save();
                            }
                        }
                        scope.Complete();
                        mv_blnCancel = false;
                        Utility.SetMsg(lblMsg,string.Format( "Hủy chuyển viện cho bệnh nhân {0} thành công",txtTenBN.Text), true);
                        cmdHuy.Enabled = false;
                        cmdPrint.Enabled = false;
                        cmdChuyen.Enabled = objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3;
                    }
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
        }

        void cmdChuyen_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", false);
            if (Utility.DoTrim(txtGioRaVien.Text)=="")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin giờ ra viện", true);
                txtGioRaVien.Focus();
                return;
            }
            if (Utility.Int32Dbnull(txtGioRaVien.Text,0) >=24)
            {
                Utility.SetMsg(lblMsg, "Giờ ra viện nằm trong khoảng giá trị từ 0 đến 23", true);
                txtGioRaVien.Focus();
                return;
            }
            if (Utility.DoTrim(txtPhuRaVien.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin phút ra viện", true);
                txtPhuRaVien.Focus();
                return;
            }
            if (Utility.Int32Dbnull(txtPhuRaVien.Text, 0) >= 60)
            {
                Utility.SetMsg(lblMsg, "Phút ra viện nằm trong khoảng giá trị từ 0 đến 59", true);
                txtPhuRaVien.Focus();
                return;
            }
            if (Utility.DoTrim(txtSoRaVien.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin số phiếu ra viện", true);
                txtSoRaVien.Focus();
                return;
            }
            if(chkChuyenvien.Checked)
                if (txtNoichuyenden.MyCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin nơi chuyển đến", true);
                    txtNoichuyenden.Focus();
                    return;
                }
            if (txtKqdieutri.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin kết quả điều trị", true);
                txtKqdieutri.Focus();
                return;
            }
            if (txtTinhtrangravien.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin tình trạng ra viện", true);
                txtTinhtrangravien.Focus();
                return;
            }
            if (Utility.DoTrim(txtLoidanBS.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin lời dặn bác sĩ", true);
                txtLoidanBS.Focus();
                return;
            }
            

            try
            {
                if (m_enAct == action.Insert)
                {
                    objRavien = new NoitruPhieuravien();
                    objRavien.IsNew = true;
                }
                else
                {
                    objRavien = NoitruPhieuravien.FetchByID(Utility.Int32Dbnull(txtId.Text));
                    objRavien.IsNew = false;
                    objRavien.MarkOld();
                }
               objRavien.NgayRavien = new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day, Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhuRaVien.Text, 0), 0);
               objRavien.SophieuRavien=Utility.DoTrim( txtSoRaVien.Text ) ;
               objRavien.TongsongayDieutri = Utility.Int32Dbnull(txtTongSoNgayDtri.Text);
               objRavien.MabenhChinh=txtBenhchinh.MyCode;
               
                objRavien.IdBenhnhan = objLuotkham.IdBenhnhan;
                objRavien.MaLuotkham = objLuotkham.MaLuotkham;
                objRavien.SoBenhAn = Utility.Int32Dbnull(objLuotkham.SoBenhAn, -1);
                objRavien.IdKhoaravien = globalVariables.idKhoatheoMay;
                objRavien.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                objRavien.TrangThai = 0;

                objRavien.MabenhGiaiphau = txtBenhgiaiphau.MyCode;
               objRavien.MabenhBienchung = txtBenhbienchung.MyCode;
               objRavien.MabenhNguyennhan = txtBenhnguyennhan.MyCode;
               objRavien.MaKquaDieutri = txtKqdieutri.MyCode;
               objRavien.MaKieuchuyenvien = txtKieuchuyenvien.MyCode;
               objRavien.MaTinhtrangravien = txtTinhtrangravien.MyCode;
               objRavien.IdBacsiChuyenvien = Utility.Int16Dbnull(cboDoctorAssign.SelectedValue,-1);
               objRavien.PhuongphapDieutri = Utility.DoTrim(txtPhuongphapdieutri.Text);
               objRavien.TrangthaiChuyenvien = Utility.Bool2byte( chkChuyenvien.Checked );
               objRavien.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                objRavien.LoidanBacsi= Utility.DoTrim(txtLoidanBS.Text)  ;
                objRavien.YkienDexuat = Utility.DoTrim(txtYkien.Text);
               objRavien.PhuhopChandoanlamsang=  Utility.Bool2byte(chkPhuHopChanDoanCLS.Checked);
               objRavien.NgayCapgiayravien = dtNGAY_CAP_GIAY_RVIEN.Value;
                KcbPhieuchuyenvien _phieuchuyenvien = null;
                if (chkChuyenvien.Checked)
                {
                    _phieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                              .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(txtIdBn.Text)
                              .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                              .And(KcbPhieuchuyenvien.Columns.NoiTru).IsEqualTo(1)
                              .ExecuteSingle<KcbPhieuchuyenvien>();

                    if (_phieuchuyenvien == null)
                    {

                        _phieuchuyenvien = new KcbPhieuchuyenvien();
                        _phieuchuyenvien.IsNew = true;
                    }
                    else
                    {
                        _phieuchuyenvien.IsNew = false;
                        _phieuchuyenvien.MarkOld();
                    }
                    _phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                    _phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                    _phieuchuyenvien.IdBenhvienChuyenden = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                    _phieuchuyenvien.DauhieuCls = Utility.DoTrim(txtTinhtrangravien.Text);
                    _phieuchuyenvien.KetquaXnCls = "";
                    _phieuchuyenvien.ChanDoan ="";
                    _phieuchuyenvien.NgayChuyenvien = objRavien.NgayRavien;
                    _phieuchuyenvien.IdBacsiChuyenvien = objRavien.IdBacsiChuyenvien;
                    _phieuchuyenvien.ThuocSudung = "";
                    _phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtKqdieutri.Text);
                    _phieuchuyenvien.HuongDieutri = Utility.DoTrim(txtPhuongphapdieutri.Text);
                    _phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                    _phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);

                    _phieuchuyenvien.IdRavien = Utility.Int32Dbnull(txtIdravien.Text, -1);
                    _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(txtIdkhoanoitru.Text, -1);
                    _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(txtidBuong.Text, -1);
                    _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(txtidgiuong.Text, -1);
                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objRavien.Save();
                        if (_phieuchuyenvien != null)
                        {
                            _phieuchuyenvien.Save();
                            objLuotkham.TthaiChuyendi = 1;
                            
                            objLuotkham.IdBacsiChuyenvien = _phieuchuyenvien.IdBacsiChuyenvien;
                            objLuotkham.NgayRavien = objRavien.NgayRavien;
                            objLuotkham.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
                        }
                        objLuotkham.NgayRavien = objRavien.NgayRavien;
                        objLuotkham.SoRavien = Utility.Int32Dbnull(objRavien.SophieuRavien);
                        objLuotkham.TrangthaiNoitru = 3;
                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();

                        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien.Value);
                        if (objNoitruPhanbuonggiuong != null)
                        {
                            objNoitruPhanbuonggiuong.MarkOld();
                            objNoitruPhanbuonggiuong.IsNew = false;
                            objNoitruPhanbuonggiuong.NgayKetthuc = objRavien.NgayRavien;
                            objNoitruPhanbuonggiuong.CachtinhSoluong = 0;
                            objNoitruPhanbuonggiuong.SoluongGio =(int) Math.Ceiling((objNoitruPhanbuonggiuong.NgayKetthuc.Value - objNoitruPhanbuonggiuong.NgayVaokhoa).TotalHours);
                            objNoitruPhanbuonggiuong.SoLuong = THU_VIEN_CHUNG.Songay(objNoitruPhanbuonggiuong.NgayKetthuc.Value, objNoitruPhanbuonggiuong.NgayVaokhoa);
                            objNoitruPhanbuonggiuong.Save();
                        }
                    }
                    scope.Complete();
                }
                mv_blnCancel = false;
                Utility.SetMsg(lblMsg, m_enAct == action.Insert?"Thêm mới phiếu chuyển viện thành công":"Cập nhật phiếu chuyển viện thành công", false);
                if (m_enAct == action.Insert)
                    cmdPrint.Enabled = true;
                cmdHuy.Enabled = objRavien != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3;
                m_enAct = action.Update;
                txtId.Text = objRavien.IdRavien.ToString();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);   
            }
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void txtPhuongphapdieutri__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuchuyenvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuchuyenvien.Text;
                txtKieuchuyenvien.Init();
                txtKieuchuyenvien._Text = oldCode;
                txtKieuchuyenvien.Focus();
            }    
        }

        void txtPhuongphapdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtPhuongphapdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuchuyenvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKieuchuyenvien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuchuyenvien.Text;
                txtKieuchuyenvien.Init();
                txtKieuchuyenvien._Text = oldCode;
                txtKieuchuyenvien.Focus();
            }   
        }

        void txtTinhtrangravien__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTinhtrangravien.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTinhtrangravien.Text;
                txtTinhtrangravien.Init();
                txtTinhtrangravien._Text = oldCode;
                txtTinhtrangravien.Focus();
            }   
        }

        void txtTinhtrangravien__OnSaveAs()
        {
            if (Utility.DoTrim(txtTinhtrangravien.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTinhtrangravien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtTinhtrangravien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTinhtrangravien.Text;
                txtTinhtrangravien.Init();
                txtTinhtrangravien._Text = oldCode;
                txtTinhtrangravien.Focus();
            }    
        }

        void txtKqdieutri__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKqdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKqdieutri.Text;
                txtKqdieutri.Init();
                txtKqdieutri._Text = oldCode;
                txtKqdieutri.Focus();
            }   
        }

        void txtKqdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtKqdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKqdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKqdieutri.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKqdieutri.Text;
                txtKqdieutri.Init();
                txtKqdieutri._Text = oldCode;
                txtKqdieutri.Focus();
            }   
        }

        void txtKieuchuyenvien__OnShowData()
        {

            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuchuyenvien.LOAI_DANHMUC);
          
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuchuyenvien.Text;
                txtKieuchuyenvien.Init();
                txtKieuchuyenvien._Text = oldCode;
                txtKieuchuyenvien.Focus();
            }    
        }

        void txtKieuchuyenvien__OnSaveAs()
        {
            if (Utility.DoTrim(txtPhuongphapdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieuchuyenvien.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKieuchuyenvien.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieuchuyenvien.Text;
                txtKieuchuyenvien.Init();
                txtKieuchuyenvien._Text = oldCode;
                txtKieuchuyenvien.Focus();
            }    
        }

        void frm_Phieuravien_Load(object sender, EventArgs e)
        {
            try
            {
                LaydanhsachBacsi();
                baocaO_TIEUDE1.Init("noitru_phieuravien");
                AutocompleteBenhvien();
                AutocompleteICD();
                txtKieuchuyenvien.Init();
                txtKqdieutri.Init();
                txtPhuongphapdieutri.Init();
                txtTinhtrangravien.Init();
                txtphuongtienvc.Init();
                if (objLuotkham != null)
                    txtMaluotkham.Text = objLuotkham.MaLuotkham;
                if (AutoLoad)
                    txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
                
            }
            catch (Exception ex)
            {
            }
        }
        private void LaydanhsachBacsi()
        {
            try
            {
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
                DataBinding.BindDataCombox(cboDoctorAssign, dtBacsi, DmucNhanvien.Columns.IdNhanvien,
                                           DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = 0;
                }
                else
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception exception)
            {
                // throw;
            }

        }
        void AutocompleteICD()
        {
            try
            {
                if (globalVariables.gv_dtDmucBenh == null) return;
                if (!globalVariables.gv_dtDmucBenh.Columns.Contains("ShortCut"))
                    globalVariables.gv_dtDmucBenh.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in globalVariables.gv_dtDmucBenh.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucBenh.Columns.TenBenh].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucBenh.Columns.TenBenh].ToString().Trim());
                    shortcut = dr[DmucBenh.Columns.MaBenh].ToString().Trim();
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
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                var source = new List<string>();
                var query = from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                            select Utility.sDbnull(p[DmucBenh.Columns.IdBenh]) + "#" + Utility.sDbnull(p[DmucBenh.Columns.MaBenh]) + "@" + Utility.sDbnull(p[DmucBenh.Columns.TenBenh]) + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtBenhchinh.AutoCompleteList = source;
                this.txtBenhchinh.TextAlign = HorizontalAlignment.Center;
                this.txtBenhchinh.CaseSensitive = false;
                this.txtBenhchinh.MinTypedCharacters = 1;

                this.txtBenhbienchung.AutoCompleteList = source;
                this.txtBenhbienchung.TextAlign = HorizontalAlignment.Center;
                this.txtBenhbienchung.CaseSensitive = false;
                this.txtBenhbienchung.MinTypedCharacters = 1;

                this.txtBenhgiaiphau.AutoCompleteList = source;
                this.txtBenhgiaiphau.TextAlign = HorizontalAlignment.Center;
                this.txtBenhgiaiphau.CaseSensitive = false;
                this.txtBenhgiaiphau.MinTypedCharacters = 1;

                this.txtBenhnguyennhan.AutoCompleteList = source;
                this.txtBenhnguyennhan.TextAlign = HorizontalAlignment.Center;
                this.txtBenhnguyennhan.CaseSensitive = false;
                this.txtBenhnguyennhan.MinTypedCharacters = 1;

                this.txtBenhphu.AutoCompleteList = source;
                this.txtBenhphu.TextAlign = HorizontalAlignment.Center;
                this.txtBenhphu.CaseSensitive = false;
                this.txtBenhphu.MinTypedCharacters = 1;

            }
        }
        void frm_Phieuravien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdChuyen.PerformClick();
            if (e.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
        }

        public void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaluotkham.Text) != "")
                {
                    AllowTextChanged = false;
                    var dtPatient = new DataTable();
                   
                        objLuotkham = null;
                        string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                        ClearControls();

                        dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text,
                                                       -1,(byte)1, 0);

                        DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                        if (arrPatients.GetLength(0) <= 0)
                        {
                            if (dtPatient.Rows.Count > 1)
                            {
                                var frm = new frm_DSACH_BN_TKIEM();
                                frm.MaLuotkham = txtMaluotkham.Text;
                                frm.dtPatient = dtPatient;
                                frm.ShowDialog();
                                if (!frm.has_Cancel)
                                {
                                    txtMaluotkham.Text = frm.MaLuotkham;
                                }
                            }
                        }
                        else
                        {
                            txtMaluotkham.Text = _patient_Code;
                        }
                        DataTable dt_Patient = new KCB_THAMKHAM().TimkiemThongtinBenhnhansaukhigoMaBN(txtMaluotkham.Text, -1,"ALL");
                        if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                        {

                            txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                            objLuotkham = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(txtIdBn.Text)
                                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                                .ExecuteSingle<KcbLuotkham>();
                            dtpNgaynhapvien.Value = objLuotkham.NgayNhapvien.Value;
                            txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                            txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
                            txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
                            txtDiachi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                            txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");

                            txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoaphong_noitru"], "");
                            txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
                            txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");

                            txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                            txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                            txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                            txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
                            cmdChuyen.Enabled = true;
                            if (objLuotkham.TrangthaiNoitru == 0)
                            {
                                Utility.ShowMsg("Bệnh nhân chưa vào nội trú nên không thể lập phiếu ra viện. Đề nghị bạn kiểm tra lại");
                                cmdChuyen.Enabled = false;
                                return;
                            }
                            cmdChuyen.Enabled = objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3;
                            LoadData();
                            dtpNgayravien.Focus();
                        }
                   
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                
                AllowTextChanged = true;
            }
        }
        void LoadData()
        {
            //Tự động tính tổng số ngày điều trị
            txtTongSoNgayDtri.Text = THU_VIEN_CHUNG.Songay(objLuotkham.NgayNhapvien.Value, new DateTime(dtpNgayravien.Value.Year, dtpNgayravien.Value.Month, dtpNgayravien.Value.Day, Utility.Int32Dbnull(txtGioRaVien.Text, 0), Utility.Int32Dbnull(txtPhuRaVien.Text, 0), 0)).ToString();

            objRavien = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
            m_enAct = objRavien != null ? action.Update : action.Insert;
            cmdPrint.Enabled= objRavien != null;
            cmdHuy.Enabled =objRavien != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3;
            if (objRavien != null)
            {
                txtId.Text = objRavien.IdRavien.ToString();
                dtpNgayravien.Text = objRavien.NgayRavien.ToString("dd/MM/yyyy");
                txtGioRaVien.Text = objRavien.NgayRavien.ToString("HH");
                txtPhuRaVien.Text = objRavien.NgayRavien.ToString("mm");
                txtSoRaVien.Text = objRavien.SophieuRavien;
                txtTongSoNgayDtri.Text = objRavien.TongsongayDieutri.ToString();
                txtBenhchinh.SetCode(objRavien.MabenhChinh);
                txtBenhgiaiphau.SetCode(objRavien.MabenhGiaiphau);
                txtBenhbienchung.SetCode(objRavien.MabenhBienchung);
                txtBenhnguyennhan.SetCode(objRavien.MabenhNguyennhan);
                txtKqdieutri.SetCode(objRavien.MaKquaDieutri);
                txtKieuchuyenvien.SetCode(objRavien.MaKieuchuyenvien);
                txtTinhtrangravien.SetCode(objRavien.MaTinhtrangravien);
                txtPhuongphapdieutri.Text = objRavien.PhuongphapDieutri;
                chkChuyenvien.Checked = Utility.Byte2Bool(objRavien.TrangthaiChuyenvien);
                txtNoichuyenden.SetId(objRavien.IdBenhvienDi);
                txtLoidanBS.Text = objRavien.LoidanBacsi;
                txtYkien.Text = objRavien.YkienDexuat;
                chkPhuHopChanDoanCLS.Checked = Utility.Byte2Bool(objRavien.PhuhopChandoanlamsang);
                dtNGAY_CAP_GIAY_RVIEN.Text = objRavien.NgayCapgiayravien.Value.ToString("dd/MM/yyyy");
                cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign, Utility.sDbnull(objRavien.IdBacsiChuyenvien, "-1"));
            }
            else
            {
                dtpNgayravien.Value = globalVariables.SysDate;
                txtGioRaVien.Text = dtpNgayravien.Value.ToString("HH");
                txtPhuRaVien.Text = dtpNgayravien.Value.ToString("mm");
                txtSoRaVien.Text = THU_VIEN_CHUNG.Laysoravien();
                cmdPrint.Enabled = false;
                cmdHuy.Enabled = false;
            }
            chkChuyenvien_CheckedChanged(chkChuyenvien, new EventArgs());
        }
        private void AutocompleteBenhvien()
        {
            DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
            try
            {
                if (m_dtBenhvien == null) return;
                if (!m_dtBenhvien.Columns.Contains("ShortCut"))
                    m_dtBenhvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtBenhvien.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucBenhvien.Columns.TenBenhvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucBenhvien.Columns.TenBenhvien].ToString().Trim());
                    shortcut = dr[DmucBenhvien.Columns.MaBenhvien].ToString().Trim();
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
                var query = from p in m_dtBenhvien.AsEnumerable()
                            select p[DmucBenhvien.Columns.IdBenhvien].ToString() + "#" + p[DmucBenhvien.Columns.MaBenhvien].ToString() + "@" + p[DmucBenhvien.Columns.TenBenhvien].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtNoichuyenden.AutoCompleteList = source;
                this.txtNoichuyenden.TextAlign = HorizontalAlignment.Left;
                this.txtNoichuyenden.CaseSensitive = false;
                this.txtNoichuyenden.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
              

            }
        }
        private void ClearControls()
        {
            try
            {
                cmdPrint.Enabled = false;
                cmdHuy.Enabled = false;

                foreach (Control control in pnlTop.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
                foreach (Control control in pnlFill.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
