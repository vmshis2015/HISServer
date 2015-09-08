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
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_chuyenvien : Form
    {
        bool AllowTextChanged = false;
        KcbPhieuchuyenvien objPhieuchuyenvien = null;
        KcbLuotkham objLuotkham = null;
        action m_enAct = action.Insert;
        public byte noitru = 0;
        public frm_chuyenvien()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            txtMaluotkham.KeyDown += new KeyEventHandler(txtMaluotkham_KeyDown);
            this.KeyDown += new KeyEventHandler(frm_chuyenvien_KeyDown);
            this.Load += new EventHandler(frm_chuyenvien_Load);

            txtphuongtienvc._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtphuongtienvc__OnSaveAs);
            txtphuongtienvc._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtphuongtienvc__OnShowData);

            txtTrangthainguoibenh._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtTrangthainguoibenh__OnSaveAs);
            txtTrangthainguoibenh._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTrangthainguoibenh__OnShowData);

            txtdauhieucls._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtdauhieucls__OnSaveAs);
            txtdauhieucls._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtdauhieucls__OnShowData);

            txtHuongdieutri._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtHuongdieutri__OnSaveAs);
            txtHuongdieutri._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtHuongdieutri__OnShowData);

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdChuyen.Click += new EventHandler(cmdChuyen_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);

            cmdgetPatient.Click += new EventHandler(cmdgetPatient_Click);
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            cmdPrint.Click += new EventHandler(cmdPrint_Click);
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData =
                                 SPs.KcbThamkhamPhieuchuyenvien(Utility.DoTrim(txtMaluotkham.Text)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "thamkham_phieuchuyenvien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("thamkham_phieuchuyenvien", ref tieude, ref reportname);
                if (crpt == null) return;
                frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtData);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkham_phieuchuyenvien";
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
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chuyển viện cho bệnh nhân {0} hay không?", txtTenBN.Text), "Xác nhận hủy chuyển viện", true))
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
                            objLuotkham.NgayRavien = null;
                            objLuotkham.IsNew=false;
                            objLuotkham.MarkOld();
                            objLuotkham.Save();
                            new Delete().From(KcbPhieuchuyenvien.Schema).Where(KcbPhieuchuyenvien.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1)).Execute();
                           
                        }
                        scope.Complete();
                        Utility.SetMsg(lblMsg,string.Format( "Hủy chuyển viện cho bệnh nhân {0} thành công",txtTenBN.Text), true);
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
            if (txtNoichuyenden.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin nơi chuyển đến", true);
                txtNoichuyenden.Focus();
                return;
            }
            if (Utility.DoTrim(txtdauhieucls.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin dấu hiệu Cận lâm sàng", true);
                txtdauhieucls.Focus();
                return;
            }
            if (Utility.DoTrim(txtketquaCls.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin kết quả xét nghiệm, cận lâm sàng", true);
                txtketquaCls.Focus();
                return;
            }
            if (Utility.DoTrim(txtChandoan.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin chẩn đoán", true);
                txtChandoan.Focus();
                return;
            }
            if (Utility.DoTrim(txtThuocsudung.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị:", true);
                txtThuocsudung.Focus();
                return;
            }
            if (Utility.DoTrim(txtTrangthainguoibenh.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin trạng thái người bệnh", true);
                txtTrangthainguoibenh.Focus();
                return;
            }
            if (Utility.DoTrim(txtHuongdieutri.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin hướng điều trị", true);
                txtHuongdieutri.Focus();
                return;
            }
            if (Utility.DoTrim(txtphuongtienvc.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin phương tiện vận chuyển", true);
                txtphuongtienvc.Focus();
                return;
            }
            if (Utility.DoTrim(txtNguoivanchuyen.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin người vận chuyển", true);
                txtNguoivanchuyen.Focus();
                return;
            }

            try
            {
                KcbPhieuchuyenvien _phieuchuyenvien = null;
                if (m_enAct == action.Insert)
                {
                    _phieuchuyenvien = new KcbPhieuchuyenvien();
                    _phieuchuyenvien.IsNew = true;
                }
                else
                {
                    _phieuchuyenvien = KcbPhieuchuyenvien.FetchByID(Utility.Int32Dbnull(txtId.Text));
                    _phieuchuyenvien.IsNew = false;
                    _phieuchuyenvien.MarkOld();
                }
                _phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                _phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                _phieuchuyenvien.IdBenhvienChuyenden =Utility.Int16Dbnull( txtNoichuyenden.MyID,-1);
                _phieuchuyenvien.DauhieuCls = Utility.DoTrim(txtdauhieucls.Text);
                _phieuchuyenvien.KetquaXnCls = Utility.DoTrim(txtketquaCls.Text);
                _phieuchuyenvien.ChanDoan = Utility.DoTrim(txtChandoan.Text);
                _phieuchuyenvien.ThuocSudung = Utility.DoTrim(txtThuocsudung.Text);
                _phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtTrangthainguoibenh.Text);
                _phieuchuyenvien.HuongDieutri = Utility.DoTrim(txtHuongdieutri.Text);
                _phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                _phieuchuyenvien.NgayChuyenvien = dtNgaychuyenvien.Value;
                _phieuchuyenvien.IdBacsiChuyenvien = Utility.Int16Dbnull(cboDoctorAssign.SelectedValue, -1);
                _phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);
                _phieuchuyenvien.NoiTru = noitru;
                _phieuchuyenvien.IdRavien = Utility.Int32Dbnull(txtIdravien.Text,-1);
                _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(txtIdkhoanoitru.Text, -1);
                _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(txtidBuong.Text, -1);
                _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(txtidgiuong.Text, -1);
                
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        _phieuchuyenvien.Save();
                        objLuotkham.TthaiChuyendi = 1;
                        objLuotkham.IdBacsiChuyenvien = _phieuchuyenvien.IdBacsiChuyenvien;
                        objLuotkham.NgayRavien = _phieuchuyenvien.NgayChuyenvien;
                        objLuotkham.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID,-1);
                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();
                    }
                    scope.Complete();
                }
                Utility.SetMsg(lblMsg, "Cập nhật phiếu chuyển viện thành công", false);
                if (m_enAct == action.Insert)
                    cmdPrint.Enabled = true;
                m_enAct = action.Update;
                txtId.Text = _phieuchuyenvien.IdPhieu.ToString();
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

        void txtHuongdieutri__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }    
        }

        void txtHuongdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtphuongtienvc.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }   
        }

        void txtdauhieucls__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtdauhieucls.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtdauhieucls.Text;
                txtdauhieucls.Init();
                txtdauhieucls._Text = oldCode;
                txtdauhieucls.Focus();
            }   
        }

        void txtdauhieucls__OnSaveAs()
        {
            if (Utility.DoTrim(txtdauhieucls.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtdauhieucls.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtdauhieucls.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtdauhieucls.Text;
                txtdauhieucls.Init();
                txtdauhieucls._Text = oldCode;
                txtdauhieucls.Focus();
            }    
        }

        void txtTrangthainguoibenh__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTrangthainguoibenh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTrangthainguoibenh.Text;
                txtTrangthainguoibenh.Init();
                txtTrangthainguoibenh._Text = oldCode;
                txtTrangthainguoibenh.Focus();
            }   
        }

        void txtTrangthainguoibenh__OnSaveAs()
        {
            if (Utility.DoTrim(txtTrangthainguoibenh.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTrangthainguoibenh.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtTrangthainguoibenh.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTrangthainguoibenh.Text;
                txtTrangthainguoibenh.Init();
                txtTrangthainguoibenh._Text = oldCode;
                txtTrangthainguoibenh.Focus();
            }   
        }

        void txtphuongtienvc__OnShowData()
        {

            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
          
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }    
        }

        void txtphuongtienvc__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtphuongtienvc.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }    
        }

        void frm_chuyenvien_Load(object sender, EventArgs e)
        {
            try
            {
                LaydanhsachBacsi();
                baocaO_TIEUDE1.Init("thamkham_phieuchuyenvien");
                AutocompleteBenhvien();
                txtphuongtienvc.Init();
                txtTrangthainguoibenh.Init();
                txtHuongdieutri.Init();
                txtdauhieucls.Init();
                
            }
            catch (Exception ex)
            {
            }
        }
        private void LaydanhsachBacsi()
        {
            try
            {
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
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
        void frm_chuyenvien_KeyDown(object sender, KeyEventArgs e)
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
                    var dtPatient = new DataTable();
                   
                        objLuotkham = null;
                        string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                        ClearControls();

                        dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text,
                                                       -1,0, 0);

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
                        DataTable dt_Patient = new KCB_THAMKHAM().TimkiemThongtinBenhnhansaukhigoMaBN(txtMaluotkham.Text, -1, globalVariables.MA_KHOA_THIEN);
                        if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                        {

                            txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                            objLuotkham = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(txtIdBn.Text)
                                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                                .ExecuteSingle<KcbLuotkham>();
                            txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                            txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
                            txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
                            txtDiaChi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                            txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");

                            txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoaphong_noitru"], "");
                            txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
                            txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");

                            txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                            txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                            txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                            txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");

                            objPhieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                               .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(txtIdBn.Text)
                               .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                               .ExecuteSingle<KcbPhieuchuyenvien>();
                            if (objPhieuchuyenvien != null)
                            {
                                txtId.Text = objPhieuchuyenvien.IdPhieu.ToString();
                                txtNoichuyenden.SetId(objPhieuchuyenvien.IdBenhvienChuyenden);
                                txtdauhieucls._Text = objPhieuchuyenvien.DauhieuCls;
                                txtketquaCls.Text = objPhieuchuyenvien.KetquaXnCls;
                                txtChandoan.Text = objPhieuchuyenvien.ChanDoan;
                                txtThuocsudung.Text = objPhieuchuyenvien.ThuocSudung;
                                txtTrangthainguoibenh._Text = objPhieuchuyenvien.TrangthaiBenhnhan;
                                txtHuongdieutri._Text = objPhieuchuyenvien.HuongDieutri;
                                txtphuongtienvc._Text = objPhieuchuyenvien.PhuongtienChuyen;
                                txtNguoivanchuyen.Text = objPhieuchuyenvien.TenNguoichuyen;
                                cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign,Utility.sDbnull( objPhieuchuyenvien.IdBacsiChuyenvien,"-1"));
                                cmdPrint.Enabled = true;
                                cmdHuy.Enabled = true;
                            }
                            else
                            {
                                cmdPrint.Enabled = false;
                                cmdHuy.Enabled = false;
                            }
                            m_enAct = objPhieuchuyenvien == null ? action.Insert : action.Update;
                            if (m_enAct == action.Insert)
                                cmdPrint.Enabled = false;
                            else
                                cmdPrint.Enabled = true;
                            dtNgaychuyenvien.Focus();
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
