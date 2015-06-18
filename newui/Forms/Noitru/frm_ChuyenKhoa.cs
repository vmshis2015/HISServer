using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using System.Collections.Generic;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_ChuyenKhoa : Form
    {
        public int IDBuonggiuong = -1;
        public bool b_CallParent;
        public bool b_Cancel=true;
        public bool b_HasValue;
        private bool m_blnHasLoaded;
        public GridEX grdList;
        private DataTable m_dtGiuong = new DataTable();
        private DataTable m_dtKhoaNoItru = new DataTable();
        private DataTable m_dtPhong = new DataTable();
        private NoitruPhanbuonggiuong objPatientDept;
        private KcbLuotkham objPatientExam;
        public DataTable p_DanhSachPhanBuongGiuong = new DataTable();

        public frm_ChuyenKhoa()
        {
            InitializeComponent();
            dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = DateTime.Now.Hour;
            txtPhut.Value = DateTime.Now.Minute;
            ClearControl();
            dtNgayvao.ValueChanged += new EventHandler(dtNgayvao_ValueChanged);
            CauHinh();
            txtDepartment_ID.Visible = globalVariables.IsAdmin;
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);
            txtHour2Cal.Text = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            
        }

        void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLanKham.Text)!="")
            {
                string _patient_Code = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                ClearControl();
                txtMaLanKham.Text = _patient_Code;
                LoadData();
            }
        }

        void chkAutoCal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCal.Checked)
                txtSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
            else
                txtSoluong.Focus();
        }

        void dtNgayvao_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGiovao.Text = Utility.sDbnull(dtNgayvao.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhutvao.Text = Utility.sDbnull(dtNgayvao.Value.Minute);
            }
          
        }

        private string maluotkham { get; set; }

        private void CauHinh()
        {
            //_hinhPhanBuongGiuongProperties = Utility.GetPhanBuongGiuongPropertiesConfig();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiên việc load thông tin của khoa phòng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoa_Load(object sender, EventArgs e)
        {
            LoadData();
            m_blnHasLoaded = true;
        }
        void LoadData()
        {
            maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
            txtMaLanKham.Text = maluotkham;
            BindData();
            m_dtKhoaNoItru =
               THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0, Utility.Int32Dbnull(txtDepartment_ID.Text));
            txtKhoanoitru.Init(m_dtKhoaNoItru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtKhoanoitru._TextAlign = HorizontalAlignment.Left;
            ModifyCommand();
            chkAutoCal_CheckedChanged(chkAutoCal, new EventArgs());
            txtTotalHour.Text = Math.Ceiling((dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            //dtNgayChuyen.Focus();
        }
     
        private void ShowPatientList()
        {
            var frm = new frm_TimKiem_BN();
            frm.SearchByDate = false;
            frm.txtPatientCode.Text = txtMaLanKham.Text;
            frm.ShowDialog();
            if (!frm.m_blnCancel)
            {
                txtMaLanKham.Text = Utility.sDbnull(frm.MaLuotkham);
                BindData();
            }
        }

        

        private void ClearControl()
        {
            foreach (Control ctrl in grpThongTinBN.Controls)
            {
                if (ctrl is EditBox)
                {
                    ((EditBox)ctrl).Clear();
                }
               
            }
        }

        private void BindData()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLanKham.Text);
            if (sqlQuery.GetRecordCount() > 0)
            {
                objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objPatientExam != null)
                {
                    txtMaLanKham.Text = Utility.sDbnull(objPatientExam.MaLuotkham);
                    txtSoBHYT.Text = Utility.sDbnull(objPatientExam.MatheBhyt);
                    DmucKhoaphong objDmucKhoaphong = DmucKhoaphong.FetchByID(objPatientExam.IdKhoanoitru);
                    if (objDmucKhoaphong != null)
                    {
                        txtDepartmentName.Tag = Utility.sDbnull(objDmucKhoaphong.IdKhoaphong);
                        txtDepartment_ID.Text = Utility.sDbnull(objDmucKhoaphong.IdKhoaphong);
                        txtDepartmentName.Text = Utility.sDbnull(objDmucKhoaphong.TenKhoaphong);
                    }

                    KcbDanhsachBenhnhan objPatientInfo = KcbDanhsachBenhnhan.FetchByID(objPatientExam.IdBenhnhan);
                    if (objPatientInfo != null)
                    {
                        txtPatient_Name.Text = Utility.sDbnull(objPatientInfo.TenBenhnhan);
                        txtPatient_ID.Text = Utility.sDbnull(objPatientExam.IdBenhnhan);
                        txtNamSinh.Text = Utility.sDbnull(objPatientInfo.NamSinh);
                        txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - objPatientInfo.NamSinh);
                        txtPatientSex.Text = objPatientInfo.GioiTinh;// Utility.Int32Dbnull(objPatientInfo.PatientSex) == 0 ? "Nam" : "Nữ";
                    }
                    objPatientDept = NoitruPhanbuonggiuong.FetchByID(objPatientExam.IdRavien);
                    dtNgayvao.Value = objPatientDept.NgayVaokhoa;
                    if (objPatientDept != null)
                    {
                        txtPatientDept_ID.Text = Utility.sDbnull(objPatientDept.Id);
                        NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(objPatientDept.IdBuong);
                        if (objRoom != null)
                        {
                            txtSoPhong.Text = Utility.sDbnull(objRoom.TenBuong);
                            txtSoPhong.Tag = Utility.sDbnull(objPatientDept.IdBuong);
                        }
                        NoitruDmucGiuongbenh objNoitruDmucGiuongbenh = NoitruDmucGiuongbenh.FetchByID(objPatientDept.IdGiuong);
                        if (objNoitruDmucGiuongbenh != null)
                        {
                            txtSoGiuong.Text = Utility.sDbnull(objNoitruDmucGiuongbenh.TenGiuong);
                            txtSoGiuong.Tag = Utility.sDbnull(objPatientDept.IdGiuong);
                        }
                    }
                }
               
            }
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin chuyển viện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            //if (!InValBenhNhan.ExistBN(txtMaLanKham.Text)) return;
            if (!KiemTraThongtin()) return;
            PerformAction();
            ModifyCommand();
        }

        private bool KiemTraThongtin()
        {
            
            if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn khoa nội trú chuyển đến", "Thông báo", MessageBoxIcon.Warning);
                txtKhoanoitru.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluong.Text)) != THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value))
            {
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn số ngày nằm viện trước khi chuyển là {0} thay vì {1}(Hệ thống tự động tính toán) hay không?", txtSoluong.Text, THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString()), "Cảnh báo", true))
                    txtSoluong.Focus();
                txtSoluong.SelectAll();
                return false;
            }
            

            return true;
        }

        private void PerformAction()
        {
            NoitruPhanbuonggiuong objPatientDept = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(txtPatientDept_ID.Text));

            if (objPatientDept != null)
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển khoa điều trị từ {0} đến {1} hay không?",txtDepartmentName.Text,txtKhoanoitru.Text), "Thông báo", true))
                {
                    var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                      dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                      Utility.Int32Dbnull(txtPhut.Text), 00);
                    objPatientDept.SoLuong = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluong.Text));
                    objPatientDept.SoluongGio = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtTotalHour.Text)); 
                    objPatientDept.CachtinhSoluong = (byte)(chkAutoCal.Checked ? 0 : 1);
                    ActionResult actionResult = new noitru_nhapvien().ChuyenKhoaDieuTri(objPatientDept,
                                                                                            objPatientExam,
                                                                                            ngaychuyenkhoa,
                                                                                            Utility.Int16Dbnull(txtKhoanoitru.MyID, -1), -1,
                                                                                            -1);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            txtPatientDept_ID.Text = Utility.sDbnull(objPatientDept.Id);
                            Utility.SetMsg(lblMsg, "Bạn chuyển phòng thành công", true);
                            if (b_CallParent)
                            {
                                b_Cancel = false;
                                Close();
                            }

                          

                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// hàm thực hiện việc load thông tin của phòng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKhoaChuyenDen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_blnHasLoaded)
            {
                m_dtPhong =
                    THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));
            }
        }

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F2)
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
            if (e.KeyCode == Keys.F3)
            {
                ShowPatientList();
            }
        }

        private void ModifyCommand()
        {
            cmdSave.Enabled = !string.IsNullOrEmpty(txtMaLanKham.Text);
            
        }
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
            }
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            if (chkAutoCal.Checked)
                txtSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
        }

        private void dtNgayChuyen_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
            }
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            if (chkAutoCal.Checked)
                txtSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            //var frm = new frm_CauHinh_BuongGiuong();
            //frm.HisclsProperties = _hinhPhanBuongGiuongProperties;
            //frm.ShowDialog();
            //CauHinh();
        }

       
     

       
    }
}