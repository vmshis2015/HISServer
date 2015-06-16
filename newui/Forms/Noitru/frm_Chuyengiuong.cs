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
    public partial class frm_Chuyengiuong : Form
    {
        public int IDBuonggiuong = -1;
        public bool b_CallParent;
        public bool b_Cancel=true;
        public bool b_HasValue;
        public GridEX grdList;
        private DataTable m_dtDataRoom = new DataTable();
        private DataTable m_dtDatabed = new DataTable();
        private KcbLuotkham objLuotkham;
        public DataTable p_DanhSachPhanBuongGiuong = new DataTable();
        private string rowFilter = "1=1";
        private readonly Logger log;

        public frm_Chuyengiuong()
        {
            InitializeComponent();
            InitEvents();
            dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = DateTime.Now.Hour;
            txtPhut.Value = DateTime.Now.Minute;
            
            //cmdConfig.Visible = globalVariables.IsAdmin;
           
            txtDepartment_ID.Visible = globalVariables.IsAdmin;
            txtRoom_ID.Visible = globalVariables.IsAdmin;
            txtBed_ID.Visible = globalVariables.IsAdmin;
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;

            txtHour2Cal.Text =THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            CauHinh();
        }
        void InitEvents()
        {
            this.Load += new System.EventHandler(this.frm_ChuyenKhoaDT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChuyenKhoaDT_KeyDown);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.txtDepartment_ID.TextChanged += new System.EventHandler(this.txtDepartment_ID_TextChanged);
            this.cmdLamSach.Click += new System.EventHandler(this.cmdLamSach_Click);
            this.txtMaLanKham.TextChanged += new System.EventHandler(this.txtMaLanKham_TextChanged);
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            this.txtSoHSBA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoHSBA_KeyDown);
            this.txtBedCode.TextChanged += new System.EventHandler(this.txtBedCode_TextChanged);
            this.txtBedCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBedCode_KeyDown);
            this.txtRoom_code.TextChanged += new System.EventHandler(this.txtRoom_code_TextChanged);
            this.txtRoom_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoom_code_KeyDown);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            grdBuong.SelectionChanged += new EventHandler(grdBuong_SelectionChanged);
            grdGiuong.MouseDoubleClick += grdGiuong_MouseDoubleClick;
            dtNgayChuyen.ValueChanged += new EventHandler(dtNgayChuyen_ValueChanged);
            dtNgayChuyen.TextChanged += new EventHandler(dtNgayChuyen_TextChanged);
            dtNgayvao.ValueChanged += new EventHandler(dtNgayvao_ValueChanged);
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);
            txtSoHSBA.LostFocus += txtSoHSBA_LostFocus;
        }

        void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }
        void chkAutoCal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCal.Checked)
                txtSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
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

        void dtNgayChuyen_TextChanged(object sender, EventArgs e)
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

        void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
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
            {
                txtSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
            }
        }

        private string maluotkham { get; set; }

        private void CauHinh()
        {
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiên việc load thông tin của khoa Buồng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoaDT_Load(object sender, EventArgs e)
        {
            if (b_CallParent)
            {
                SearchThongTin();
                chkAutoCal_CheckedChanged(chkAutoCal, e);
                txtRoom_code.Focus();
            }
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
        }

        private void InitData()
        {

            m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtDepartment_ID.Text));
            Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtDataRoom, true, true, "1=1", "sluong_giuong_trong desc,ten_buong");
            if (grdBuong.DataSource != null)
            {
                grdBuong.MoveFirst();
            }
            
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
                txtRoom_code.Focus();
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

            foreach (Control control in grpThongTinChuyenKhoa.Controls)
            {
                if (control is EditBox)
                {
                    var txtFormantTongTien = new EditBox();

                    txtFormantTongTien = ((EditBox) (control));
                    txtFormantTongTien.Clear();
                }
                if (control is UIComboBox)
                {
                    var txtFormantTongTien = new UIComboBox();
                    txtFormantTongTien = ((UIComboBox) (control));
                    if (txtFormantTongTien.Items.Count > 0)
                        txtFormantTongTien.SelectedIndex = 0;
                }
            }
            txtSoHSBA.Focus();
            txtSoHSBA.SelectAll();
        }

        private void BindData()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtSoHSBA.Text);
            if (sqlQuery.GetRecordCount() > 0)
            {
                objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {
                    txtMaLanKham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtSoBHYT.Text = Utility.sDbnull(objLuotkham.MatheBhyt);
                    DmucKhoaphong objLDepartment = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                    if (objLDepartment != null)
                    {
                        txtDepartment_ID.Text = Utility.sDbnull(objLDepartment.IdKhoaphong);
                        txtDepartmentName.Tag = Utility.sDbnull(objLDepartment.IdKhoaphong);
                        txtDepartmentName.Text = Utility.sDbnull(objLDepartment.TenKhoaphong);
                    }
                    KcbDanhsachBenhnhan objPatientInfo = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                    if (objPatientInfo != null)
                    {
                        txtPatient_Name.Text = Utility.sDbnull(objPatientInfo.TenBenhnhan);
                        txtPatient_ID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        txtNamSinh.Text = Utility.sDbnull(objPatientInfo.NamSinh);
                        txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - objPatientInfo.NamSinh);
                        txtPatientSex.Text =objPatientInfo.GioiTinh;// Utility.Int32Dbnull(objPatientInfo.) == 0 ? "Nam" : "Nữ";
                    }
                    NoitruPhanbuonggiuong objPatientDept = noitru_nhapvien.LaythongtinBuonggiuongHtai(objLuotkham);
                    if (objPatientDept != null)
                    {
                        dtNgayvao.Value = objPatientDept.NgayVaokhoa;
                        txtPatientDept_ID.Text = Utility.sDbnull(objPatientDept.Id);
                        NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(objPatientDept.IdBuong);
                        if (objRoom != null)
                        {
                            txtSoPhong.Text = Utility.sDbnull(objRoom.TenBuong);
                            txtSoPhong.Tag = Utility.sDbnull(objPatientDept.IdBuong);
                            txtRoom_ID.Text = Utility.sDbnull(objPatientDept.IdBuong);
                        }
                        NoitruDmucGiuongbenh objNoitruDmucGiuongbenh = NoitruDmucGiuongbenh.FetchByID(objPatientDept.IdGiuong);
                        if (objNoitruDmucGiuongbenh != null)
                        {
                            txtBed_ID.Text = Utility.sDbnull(objPatientDept.IdGiuong);
                            txtSoGiuong.Text = Utility.sDbnull(objNoitruDmucGiuongbenh.TenGiuong);
                            txtSoGiuong.Tag = Utility.sDbnull(objPatientDept.IdGiuong);
                        }
                    }
                }
                InitData();
            }
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
                txtRoom_code.Focus();

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

        private void txtSoHSBA_LostFocus(object sender, EventArgs eventArgs)
        {
           
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            CallFormSearch();
        }

       

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin chuyển viện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            //if (!InValBenhNhan.ExistBN(txtMaLanKham.Text)) return;
            if (!IsValidData()) return;
            PerformAction();
        }

        private bool IsValidData()
        {
            KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), txtSoHSBA.Text);
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru < 1)
            {
                Utility.ShowMsg("Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường", "Thông báo",
                                MessageBoxIcon.Warning);
                txtSoHSBA.Focus();
                txtSoHSBA.SelectAll();
                return false;
            }

            if (!Utility.isValidGrid(grdBuong))
            {
                Utility.ShowMsg("Bạn phải chọn Buồng cần chuyển", "Thông báo", MessageBoxIcon.Warning);
                // cboPhong.Focus();
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }


            if (!Utility.isValidGrid(grdGiuong))
            {
                Utility.ShowMsg("Bạn phải chọn giường cần chuyển", "Thông báo", MessageBoxIcon.Warning);
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                // cboGiuong.Focus();
                return false;
            }
            DataTable dt = new noitru_nhapvien().NoitruKiemtraBuongGiuong((int)objLuotkham.IdKhoanoitru, Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucGiuongbenh.Columns.IdBuong)), Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));
            if (dt != null && dt.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, string.Format("Giường này đang được nằm bởi bệnh nhân\n{0}\nMời bạn chọn giường khác", Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan])), true);
                txtBedCode.Focus();
                txtBedCode.SelectAll();
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

        /// <summary>
        /// hàm thực hiện việc hoạt động lưu lại thông tin 
        /// </summary>
        private void PerformAction()
        {
            NoitruPhanbuonggiuong objPatientDept = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(txtPatientDept_ID.Text));
            if (objPatientDept != null)
            {
                if (Utility.AcceptQuestion("Bạn có muốn chuyển Buồng và giường không", "Thông báo", true))
                {
                    var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                      dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                      Utility.Int32Dbnull(txtPhut.Text), 00);
                    objPatientDept.SoLuong = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluong.Text));
                    objPatientDept.SoluongGio = Utility.ByteDbnull(txtTotalHour.Text);
                    objPatientDept.CachTinh = (byte)(chkAutoCal.Checked ? 0 : 1);
                    ActionResult actionResult = new noitru_nhapvien().ChuyenGiuongDieuTri(objPatientDept,
                                                                                              objLuotkham,
                                                                                              ngaychuyenkhoa,
                                                                                              Utility.Int16Dbnull(
                                                                                                  grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)),
                                                                                              Utility.Int16Dbnull(
                                                                                                  grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            txtPatientDept_ID.Text = Utility.sDbnull(objPatientDept.Id);
                            Utility.SetMsg(lblMsg, "Bạn chuyển Buồng thành công", true);
                            // Utility.ShowMsg("Bạn chuyển Buồng thành công", "Thông báo", MessageBoxIcon.Information);
                            if (b_CallParent)
                            {
                                ProcessChuyenKhoa();
                                b_Cancel = false;
                                Close();
                            }
                            else
                            {
                                ClearControl();
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }

        private void ProcessChuyenKhoa()
        {
            DataRow query = (from khoa in p_DanhSachPhanBuongGiuong.AsEnumerable()
                             where
                                 Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                                 Utility.Int32Dbnull(IDBuonggiuong)
                             select khoa).FirstOrDefault();
            if (query != null)
            {
                NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(Utility.Int32Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong))));
                if (objRoom != null)
                {
                    query[NoitruDmucBuong.Columns.IdBuong] = Utility.Int32Dbnull(objRoom.IdBuong, -1);
                    query[NoitruDmucBuong.Columns.TenBuong] = Utility.sDbnull(objRoom.TenBuong);
                }
                NoitruDmucGiuongbenh objBed = NoitruDmucGiuongbenh.FetchByID(Utility.Int32Dbnull(Utility.Int32Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong))));
                if (objBed != null)
                {
                    query[NoitruDmucGiuongbenh.Columns.IdGiuong] = Utility.Int32Dbnull(objBed.IdGiuong, -1);
                    query[NoitruDmucGiuongbenh.Columns.TenGiuong] = Utility.sDbnull(objBed.TenGiuong);
                }
                query[NoitruPhanbuonggiuong.Columns.Id] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
            }
        }

        //private void cboPhong_ValueChanged(object sender, EventArgs e)
        //{
        //    m_dtDatabed =
        //        CommonBusiness.LayThongTinGiuongKhacGiuongHienTai(
        //            Utility.Int32Dbnull(txtDepartment_ID.Text), Utility.Int32Dbnull(cboPhong.Value),
        //            Utility.Int32Dbnull(txtBed_ID.Text));
        //    cboGiuong.DataSource = m_dtDatabed;
        //    cboGiuong.ValueMember = NoitruDmucGiuongbenh.Columns.IdGiuong;
        //    cboGiuong.DisplayMember = NoitruDmucGiuongbenh.Columns.BedName;
        //    if (_hinhPhanBuongGiuongProperties != null)
        //    {
        //        cboGiuong.DropDownList.SortKeys.Add("IsFull", SortOrder.Ascending);
        //    }
        //}

        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoaDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            //if (e.KeyCode == Keys.F11)cmdXemGiuong.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtSoHSBA.Focus();
                txtSoHSBA.SelectAll();
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                grdBuong_SelectionChanged(grdBuong, new EventArgs());
                return;
            }
        }

        private void ModifyCommand()
        {
            cmdSave.Enabled = !string.IsNullOrEmpty(txtMaLanKham.Text);
            grpThongTinChuyenKhoa.Enabled = !string.IsNullOrEmpty(txtMaLanKham.Text);
        }

        /// <summary>
        /// hàm thực hiện việc làm sạch thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //private void cmdXemGiuong_Click(object sender, EventArgs e)
        //{
        //    var frm = new frm_XemGiuong();
        //    frm.Department_ID = Utility.Int32Dbnull(txtDepartmentName.Tag);
        //    // frm.Room_ID = Utility.Int32Dbnull(cboPhong.Value);
        //    // frm.Bed_ID = Utility.Int32Dbnull(cboGiuong.Value);
        //    frm.ShowDialog();
        //    if (frm.b_Cancel)
        //    {
        //        //   cboPhong.Text = Utility.sDbnull(frm.RoomName);
        //        // cboGiuong.Text = Utility.sDbnull(frm.BedName);
        //    }
        //}

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            //var frm = new frm_CauHinh_BuongGiuong();
            //frm.HisclsProperties = _hinhPhanBuongGiuongProperties;
            //frm.ShowDialog();
            //CauHinh();
        }

        private void txtRoom_code_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                rowFilter = "ma_buong like '%" + Utility.DoTrim(txtRoom_code.Text.ToUpper()) + "%' OR ten_buong like '%" + Utility.DoTrim(txtRoom_code.Text.ToUpper()) + "%'";
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=2";
            }
            finally
            {
                m_dtDataRoom.DefaultView.RowFilter = "1=1";
                m_dtDataRoom.DefaultView.RowFilter = rowFilter;
                m_dtDataRoom.AcceptChanges();
                grdBuong.MoveFirst();
            }
            
        }

        private void txtBedCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "ma_giuong like '%" + Utility.DoTrim(txtBedCode.Text) + "%' OR ten_giuong like '%" + Utility.DoTrim(txtBedCode.Text) + "%'";
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=2";
            }
            finally
            {
                m_dtDatabed.DefaultView.RowFilter = "1=1";
                m_dtDatabed.DefaultView.RowFilter = rowFilter;
                m_dtDatabed.AcceptChanges();
                grdGiuong.MoveFirst();
            }
            
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
        }

        private void txtDepartment_ID_TextChanged(object sender, EventArgs e)
        {
        }

        private void grdBuong_SelectionChanged(object sender, EventArgs e)
        {
            string IdBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            m_dtDatabed = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtDepartment_ID.Text),
                                                           Utility.Int32Dbnull(IdBuong));
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtDatabed, true, true, "1=1", "isFull asc,dang_nam ASC,ten_giuong");
            if (grdGiuong.DataSource != null)
            {
                grdGiuong.MoveFirst();
            }
        }

        private void txtRoom_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_dtDataRoom.Rows.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtBedCode.Focus();
                    txtRoom_code.Text = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.TenBuong));
                }
                if (e.KeyCode == Keys.Down)
                {
                    grdBuong.Focus();
                    grdBuong.MoveFirst();
                }
            }
            else
            {
                Utility.ShowMsg("Không có Buồng cho bạn chọn", "Thông báo", MessageBoxIcon.Warning);
            }
        }

        private void grdBuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBedCode.Focus();
                txtRoom_code.Text = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.TenBuong));
            }
        }

        private void grdGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSave_Click(cmdSave, new EventArgs());
                //cmdSave.Focus();
                //txtBedCode.Text = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.TenGiuong));
            }
        }

        private void txtBedCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_dtDatabed.Rows.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdSave.Focus();
                    txtBedCode.Text = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.TenGiuong));
                }
                if (e.KeyCode == Keys.Down)
                {
                    grdGiuong.Focus();
                    grdGiuong.MoveFirst();
                }
            }
            else
            {
                Utility.ShowMsg("Không có giường cho bạn chọn", "Thông báo", MessageBoxIcon.Warning);
            }
        }
    }
}