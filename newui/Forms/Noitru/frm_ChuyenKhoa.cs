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


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_ChuyenKhoa : Form
    {
        public int IDBuonggiuong = -1;
        public bool b_CallParent;
        public bool b_Cancel=true;
        public bool b_HasValue;
        private bool b_hasloaed;
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
            cboKhoaChuyenDen.SelectedIndexChanged += cboKhoaChuyenDen_SelectedIndexChanged;
            ///cboPhong.SelectedIndexChanged+=new EventHandler(cboPhong_SelectedIndexChanged);
            dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = DateTime.Now.Hour;
            txtPhut.Value = DateTime.Now.Minute;
            // txtSoHSBA.LostFocus+=new EventHandler(txtSoHSBA_LostFocus);
            ClearControl();
            dtNgayvao.ValueChanged += new EventHandler(dtNgayvao_ValueChanged);
            CauHinh();
            txtDepartment_ID.Visible = globalVariables.IsAdmin;
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);
            txtHour2Cal.Text = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            // errorProvider1.BindToDataAndErrors();
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
            SetStatusControl();
            ModifyCommand();
            chkAutoCal_CheckedChanged(chkAutoCal, e);
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
        }

        private void InitalData()
        {
            m_dtKhoaNoItru =
                THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0,Utility.Int32Dbnull(txtDepartment_ID.Text));
            DataBinding.BindDataCombobox(cboKhoaChuyenDen, m_dtKhoaNoItru, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong, "",true);
            b_hasloaed = true;
            cboKhoaChuyenDen_SelectedIndexChanged(cboKhoaChuyenDen, new EventArgs());
        }

        private void CallFormSearch()
        {
            var frm = new frm_TimKiem_BN();
            frm.SoHSBA = txtSoHSBA.Text;
            frm.ShowDialog();
            if (frm.b_Cancel)
            {
                txtSoHSBA.Text = Utility.sDbnull(frm.MaLuotkham);
                BindData();
            }
        }

        private void getdata()
        {
            //if (!BusinessHelper.KiemTraTonTaiBenhNhan(txtSoHSBA.Text))
            //{
            //    CallFormSearch();
            //}
            //else
            //{
                BindData();
            //}
        }

        private void ClearControl()
        {
            foreach (Control control in grpThongTinBN.Controls)
            {
                if (control is EditBox)
                {
                    var txtFormantTongTien = new EditBox();

                    txtFormantTongTien = ((EditBox) (control));

                    if (txtFormantTongTien.Name != txtSoHSBA.Name)
                    {
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.Enabled = false;
                    }
                }
                if (control is UIComboBox)
                {
                    var txtFormantTongTien = new UIComboBox();
                    txtFormantTongTien = ((UIComboBox) (control));
                    txtFormantTongTien.Enabled = false;
                    if (txtFormantTongTien.Items.Count > 0)
                        txtFormantTongTien.SelectedIndex = 0;
                }
            }

          
            txtSoHSBA.Focus();
            txtSoHSBA.SelectAll();
        }

        private void BindData()
        {
            ClearControl();
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtSoHSBA.Text);
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
                InitalData();
            }
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của khoa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoHSBA_TextChanged(object sender, EventArgs e)
        {
            if (b_CallParent)
            {
                SearchThongTin();
            }
        }

        /// <summary>
        /// hàm thực hiện việc nhán phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoHSBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchThongTin();
            }
        }

        private void SearchThongTin()
        {
            maluotkham = Utility.sDbnull(txtSoHSBA.Text.Trim());
            if (!string.IsNullOrEmpty(maluotkham) && txtSoHSBA.Text.Length < 8)
            {
                maluotkham = Utility.GetYY(DateTime.Now) +
                           Utility.FormatNumberToString(Utility.Int32Dbnull(txtSoHSBA.Text, 0), "000000");
                txtSoHSBA.Text = maluotkham;
                txtSoHSBA.Select(txtSoHSBA.Text.Length, txtSoHSBA.Text.Length);
            }
            if (!string.IsNullOrEmpty(txtSoHSBA.Text))
            {
                getdata();
            }
        }


        private void cmdSearch_Click(object sender, EventArgs e)
        {
            CallFormSearch();
        }

        private void SetStatusControl()
        {
            Utility.ResetMessageError(errorProvider1);
            Utility.ResetMessageError(errorProvider2);
            Utility.ResetMessageError(errorProvider3);
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
            if (cboKhoaChuyenDen.SelectedIndex <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn khoa cần chuyển", "Thông báo", MessageBoxIcon.Warning);
                cboKhoaChuyenDen.Focus();
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
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển khoa điều trị từ {0} đến {1} hay không?",txtDepartmentName.Text,cboKhoaChuyenDen.Text), "Thông báo", true))
                {
                    var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                      dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                      Utility.Int32Dbnull(txtPhut.Text), 00);
                    objPatientDept.SoLuong = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluong.Text));
                    objPatientDept.SoluongGio = Utility.ByteDbnull(txtTotalHour.Text);
                    objPatientDept.CachTinh = (byte)(chkAutoCal.Checked ? 0 : 1);
                    ActionResult actionResult = new noitru_nhapvien().ChuyenKhoaDieuTri(objPatientDept,
                                                                                            objPatientExam,
                                                                                            ngaychuyenkhoa,
                                                                                            Utility.Int16Dbnull(
                                                                                                cboKhoaChuyenDen.
                                                                                                    SelectedValue), -1,
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
        /// hàm thực hiện việc xử lý thông tin chuyển khoa
        /// </summary>
        private void ProcessChuyenKhoa()
        {
            DataRow query = (from khoa in p_DanhSachPhanBuongGiuong.AsEnumerable()
                             where
                                 Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                                 Utility.Int32Dbnull(IDBuonggiuong)
                             select khoa).FirstOrDefault();
            if (query != null)
            {
                query[NoitruDmucBuong.Columns.TenBuong] = string.Empty;
                query["ten_giuong"] = string.Empty;
                query[NoitruDmucBuong.Columns.IdBuong] = -1;
                query["id_giuong"] = -1;
                query["ten_khoanoitru"] = Utility.sDbnull(cboKhoaChuyenDen.Text);
                query[NoitruPhanbuonggiuong.Columns.IdKhoanoitru] = Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue);
                query[NoitruPhanbuonggiuong.Columns.Id] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
            }
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của phòng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKhoaChuyenDen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b_hasloaed)
            {
                m_dtPhong =
                    THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue));
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
                txtSoHSBA.Focus();
                txtSoHSBA.SelectAll();
            }
            if (e.KeyCode == Keys.F3)
            {
                cmdTimKiemKhoaNoITru.PerformClick();
            }
        }

        private void ModifyCommand()
        {
            cmdSave.Enabled = !string.IsNullOrEmpty(txtMaLanKham.Text);
            
        }

        private void cmdLamSach_Click(object sender, EventArgs e)
        {
            txtSoHSBA.Clear();
            txtSoHSBA.Focus();
            txtSoHSBA.SelectAll();
            ClearControl();
            ModifyCommand();
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

        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiemKhoaNoITru_Click(object sender, EventArgs e)
        {
            TimKiemKhoaNoiTru();
        }

        private void TimKiemKhoaNoiTru()
        {
            var frm = new frm_TimKiemKhoaPhongNoiTru();
            frm.idkhoaloaibo = Utility.Int32Dbnull(txtDepartment_ID.Text,-1);
           
            frm.ShowDialog();
            if (frm.b_Cancel)
            {
                cboKhoaChuyenDen.SelectedValue = Utility.sDbnull(frm.IdKhoaphong);
            }
        }

        private void cboKhoaChuyenDen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F3)
            {
                cmdTimKiemKhoaNoITru.PerformClick();
            }
        }

        /// <summary>
        /// hàm thực hiện việc lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDepartmentCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                EnumerableRowCollection<string> query = from phong in m_dtKhoaNoItru.AsEnumerable()
                                                        let y = Utility.sDbnull(phong[DmucKhoaphong.Columns.IdKhoaphong])
                                                        where
                                                            Utility.sDbnull(phong[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                            Utility.sDbnull(txtDepartmentCode.Text.Trim())
                                                        select y;
                if (query.Any())
                {
                    cboKhoaChuyenDen.SelectedValue = Utility.sDbnull(query.FirstOrDefault());
                    cmdSave.Focus();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

       
    }
}