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
using VNS.HIS.NGHIEPVU;
using System.Collections.Generic;


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
        private bool AllowGridSelecttionChanged = true;
        public frm_Chuyengiuong()
        {
            InitializeComponent();
            InitEvents();
            dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = globalVariables.SysDate.Hour;
            txtPhut.Value = globalVariables.SysDate.Minute;
            
            //cmdConfig.Visible = globalVariables.IsAdmin;
           
            txtDepartment_ID.Visible = globalVariables.IsAdmin;
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            lblGiaBG.Visible = txtGia.Visible = cboGia.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_APGIABUONGGIUONG_THEODANHMUCGIA", "0", false) == "0";
            txtHour2Cal.Text =THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            CauHinh();
        }
        void InitEvents()
        {
            this.Load += new System.EventHandler(this.frm_Chuyengiuong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Chuyengiuong_KeyDown);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.txtDepartment_ID.TextChanged += new System.EventHandler(this.txtDepartment_ID_TextChanged);
            txtMaLanKham.KeyDown+=txtMaLanKham_KeyDown;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            dtNgayChuyen.ValueChanged += new EventHandler(dtNgayChuyen_ValueChanged);
            dtNgayChuyen.TextChanged += new EventHandler(dtNgayChuyen_TextChanged);
            dtNgayvao.ValueChanged += new EventHandler(dtNgayvao_ValueChanged);
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);

            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;

            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            grdBuong.SelectionChanged += grdBuong_SelectionChanged;
            grdBuong.KeyDown += grdBuong_KeyDown;
            grdGiuong.KeyDown += grdGiuong_KeyDown;
            grdGiuong.SelectionChanged += grdGiuong_SelectionChanged;
            grdGiuong.MouseDoubleClick += grdGiuong_MouseDoubleClick;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
        }
        void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdGiuong)) return;
            ChonGiuong();
        }
        void ChonGiuong()
        {
            string IdGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            txtBedCode.SetId(IdGiuong);
        }
        void txtBedCode__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdGiuong, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtBedCode.MyID, ""));
        }

        void txtRoom_code__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdBuong, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtRoom_code.MyID, ""));
        }
        void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }
        private void grdGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdGiuong))
            {
                cmdSave_Click(cmdSave, new EventArgs());
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
        private void grdBuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdBuong)) return;
            ChonBuong();
        }
        void ChonBuong()
        {
            string IdBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(IdBuong);
            m_dtDatabed = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtDepartment_ID.Text),
                                                           Utility.Int32Dbnull(IdBuong));
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtDatabed, true, true, "1=1", "isFull asc,dang_nam ASC,ten_giuong");
            string oldBed = txtBedCode.MyCode;
            txtBedCode.Init(m_dtDatabed, new List<string>() { NoitruDmucGiuongbenh.Columns.IdGiuong, NoitruDmucGiuongbenh.Columns.MaGiuong, NoitruDmucGiuongbenh.Columns.TenGiuong });
            txtBedCode.SetCode(oldBed);
            if (grdGiuong.DataSource != null)
            {
                grdGiuong.MoveFirst();

            }
            if (txtBedCode.MyCode == "-1")
            {
                string IdGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
                txtBedCode.SetId(IdGiuong);
            }
        }
        void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.MyText;
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
        private void frm_Chuyengiuong_Load(object sender, EventArgs e)
        {
            if (b_CallParent)
            {
                LoadData();
                chkAutoCal_CheckedChanged(chkAutoCal, e);
                txtRoom_code.SelectAll();
                txtRoom_code.Focus();
            }
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
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
                        txtPatientSex.Text = objPatientInfo.GioiTinh;// Utility.Int32Dbnull(objPatientInfo.) == 0 ? "Nam" : "Nữ";
                    }
                    NoitruPhanbuonggiuong objPhanbuonggiuong = noitru_nhapvien.LaythongtinBuonggiuongHtai(objLuotkham);
                    if (objPhanbuonggiuong != null)
                    {
                        dtNgayvao.Value = objPhanbuonggiuong.NgayVaokhoa;
                        txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                        NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(objPhanbuonggiuong.IdBuong);
                        if (objRoom != null)
                        {
                            txtSoPhong.Text = Utility.sDbnull(objRoom.TenBuong);
                            txtSoPhong.Tag = Utility.sDbnull(objPhanbuonggiuong.IdBuong);
                        }
                        NoitruDmucGiuongbenh objNoitruDmucGiuongbenh = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
                        if (objNoitruDmucGiuongbenh != null)
                        {
                            txtSoGiuong.Text = Utility.sDbnull(objNoitruDmucGiuongbenh.TenGiuong);
                            txtSoGiuong.Tag = Utility.sDbnull(objPhanbuonggiuong.IdGiuong);
                        }
                    }

                    DataTable dtGia = new dmucgiagiuong_busrule().dsGetList("-1").Tables[0];
                    dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia;
                    txtGia.Init(dtGia, new System.Collections.Generic.List<string>() { NoitruGiabuonggiuong.Columns.IdGia, NoitruGiabuonggiuong.Columns.MaGia, NoitruGiabuonggiuong.Columns.TenGia });
                    cboGia.DataSource = dtGia;
                    cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
                    cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
                    cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;

                    m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtDepartment_ID.Text));
                    Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtDataRoom, true, true, "1=1", "sluong_giuong_trong desc,ten_buong");
                    txtRoom_code.Init(m_dtDataRoom, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
                    if (grdBuong.DataSource != null)
                    {
                        grdBuong.MoveFirst();
                    }
                }
                else
                {
                    string tempt = txtMaLanKham.Text;
                    ClearControl();
                    if (m_dtDataRoom != null) m_dtDataRoom.Clear();
                    if (m_dtDatabed != null) m_dtDataRoom.Clear();
                    txtMaLanKham.Text = tempt;
                    txtMaLanKham.SelectAll();
                    txtMaLanKham.Focus();
                }
            }
        }

        
        void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLanKham.Text) != "")
            {
                string _patient_Code = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                ClearControl();
                txtMaLanKham.Text = _patient_Code;
                BindData();

                //string _patient_Code = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                //ClearControl();
                //txtMaLanKham.Text = _patient_Code;
                //LoadData();
            }
        }

        void LoadData()
        {
            maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
            txtMaLanKham.Text = maluotkham;
            BindData();
            ModifyCommand();
            chkAutoCal_CheckedChanged(chkAutoCal, new EventArgs());
            txtTotalHour.Text = Math.Ceiling((dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
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
            KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text), txtMaLanKham.Text);
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru < 1)
            {
                Utility.ShowMsg("Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường", "Thông báo",
                                MessageBoxIcon.Warning);
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
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
            if (lblGiaBG.Visible && Utility.Int32Dbnull(txtGia.MyID, -1) == -1)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giá buồng giường", true);
                txtGia.Focus();
                txtGia.SelectAll();
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
            NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(txtPatientDept_ID.Text));
            if (objPhanbuonggiuong != null)
            {
                if (Utility.AcceptQuestion("Bạn có muốn chuyển Buồng và giường không", "Thông báo", true))
                {
                    var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                      dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                      Utility.Int32Dbnull(txtPhut.Text), 00);
                    objPhanbuonggiuong.SoLuong = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluong.Text));
                    objPhanbuonggiuong.SoluongGio = Utility.Int32Dbnull(txtTotalHour.Text);
                    objPhanbuonggiuong.CachtinhSoluong = (byte)(chkAutoCal.Checked ? 0 : 1);
                    ActionResult actionResult = new noitru_nhapvien().ChuyenGiuongDieuTri(objPhanbuonggiuong,
                                                                                              objLuotkham,
                                                                                              ngaychuyenkhoa,
                                                                                              Utility.Int16Dbnull(
                                                                                                  grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)),
                                                                                              Utility.Int16Dbnull(
                                                                                                  grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)), Utility.Int32Dbnull(txtGia.MyID, -1));
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
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

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Chuyengiuong_KeyDown(object sender, KeyEventArgs e)
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

       

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            //var frm = new frm_CauHinh_BuongGiuong();
            //frm.HisclsProperties = _hinhPhanBuongGiuongProperties;
            //frm.ShowDialog();
            //CauHinh();
        }

       

        private void lblMessage_Click(object sender, EventArgs e)
        {
        }

        private void txtDepartment_ID_TextChanged(object sender, EventArgs e)
        {
        }

       

       
    }
}