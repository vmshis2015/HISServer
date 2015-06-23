using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.NGHIEPVU;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_phanbuonggiuong : Form
    {
        private readonly Logger log;
        public bool b_Cancel=true;
        private DataTable m_dtDataRoom = new DataTable();
        private DataTable m_dtDatabed = new DataTable();
        public NoitruPhanbuonggiuong objPhanbuonggiuong;
        private KcbLuotkham objPatientExam;
        public DataTable p_DanhSachPhanBuongGiuong = new DataTable();
        private string rowFilter = "1=1";

        public frm_phanbuonggiuong()
        {
            InitializeComponent();
            InitEvents();
            dtNgayChuyen.Value =globalVariables.SysDate;
        }
        void InitEvents()
        {
            this.txtRoom_code.TextChanged += new System.EventHandler(this.txtRoom_code_TextChanged_1);
            this.txtRoom_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoom_code_KeyDown_1);
            this.Load += new System.EventHandler(this.frm_phanbuonggiuong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_phanbuonggiuong_KeyDown);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            dtNgayChuyen.TextChanged += new EventHandler(dtNgayChuyen_TextChanged);
            dtNgayChuyen.ValueChanged += new EventHandler(dtNgayChuyen_ValueChanged);
            grdBuong.KeyDown += new KeyEventHandler(grdBuong_KeyDown);
            grdBuong.SelectionChanged += new EventHandler(grdBuong_SelectionChanged);
            grdGiuong.KeyDown += new KeyEventHandler(grdGiuong_KeyDown);
            grdGiuong.MouseDoubleClick += grdGiuong_MouseDoubleClick;
            txtBedCode.TextChanged+=txtBedCode_TextChanged;
            txtBedCode.KeyDown+=txtBedCode_KeyDown;
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            cmdChonBNmoi.Click += cmdChonBNmoi_Click;
            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;
        }

        void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.MyText;
        }

        void cmdChonBNmoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            txtMaLanKham.Focus();
        }

        void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLanKham.Text) != "")
            {
                objPhanbuonggiuong = null;
                string _patient_Code = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                ClearControl();
                txtMaLanKham.Text = _patient_Code;
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
        void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
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
            
        }

        /// <summary>
        /// hàm thực hiện thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //if (!InValBenhNhan.ExistBN(objPhanbuonggiuong.MaLuotkham)) return;
            if (!IsValidData()) return;
            PerformAction();
        }

        private void PerformAction()
        {
            //if (Utility.AcceptQuestion("Bạn có muốn chuyển bệnh nhân vào Buồng và giường đang chọn không", "Thông báo",
            //    true))
            //{
            var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                              dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                              Utility.Int32Dbnull(txtPhut.Text), 00);
            objPatientExam =
                new Select().From<KcbLuotkham>()
                    .Where(KcbLuotkham.Columns.MaLuotkham)
                    .IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                    .And(KcbLuotkham.Columns.IdBenhnhan)
                    .IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                    .ExecuteSingle<KcbLuotkham>();
            if (objPatientExam != null)
            {
                objPhanbuonggiuong.Id = Utility.Int32Dbnull(txtPatientDept_ID.Text);
                
                objPhanbuonggiuong.NgayVaokhoa = ngaychuyenkhoa;
                objPhanbuonggiuong.IdBuong = Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong));
                objPhanbuonggiuong.IdGiuong = Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong));
                objPhanbuonggiuong.IdGia = Utility.Int32Dbnull(txtGia.MyID, -1);
                ActionResult actionResult = new noitru_nhapvien().PhanGiuongDieuTri(objPhanbuonggiuong,
                                                                                        objPatientExam,
                                                                                        ngaychuyenkhoa,
                                                                                        Utility.Int16Dbnull(
                                                                                            grdBuong.GetValue(
                                                                                                NoitruDmucBuong.Columns.IdBuong)),
                                                                                        Utility.Int16Dbnull(
                                                                                            grdGiuong.GetValue(
                                                                                                NoitruDmucGiuongbenh.Columns.IdGiuong)));
                switch (actionResult)
                {
                    case ActionResult.Success:
                        txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                        Utility.SetMsg(lblMsg, "Bạn chuyển bênh nhân vào giường thành công", true);
                        ProcessChuyenKhoa();
                        b_Cancel = false;
                        Close();

                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình phân buồng giường", "Thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            //  }
        }

        private bool IsValidData()
        {
            KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(objPhanbuonggiuong.IdBenhnhan,objPhanbuonggiuong.MaLuotkham);
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru<1)
            {
                Utility.ShowMsg("Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }


           if(!Utility.isValidGrid(grdBuong))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Buồng cần chuyển", true);
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }


           if (!Utility.isValidGrid(grdGiuong))
           {
               Utility.SetMsg(lblMsg, "Bạn phải chọn giường cần chuyển", true);
               txtBedCode.Focus();
               txtBedCode.SelectAll();
               return false;
           }
           if (Utility.Int32Dbnull(txtGia.MyID, -1) == -1)
           {
               Utility.SetMsg(lblMsg, "Bạn phải chọn giá buồng giường", true);
               txtGia.Focus();
               txtGia.SelectAll();
               return false;
           }
           DataTable dt = new noitru_nhapvien().NoitruKiemtraBuongGiuong(objPhanbuonggiuong.IdKhoanoitru, Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucGiuongbenh.Columns.IdBuong)), Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));
           if (dt != null && dt.Rows.Count > 0)
           {
               Utility.SetMsg(lblMsg, string.Format("Giường này đang được nằm bởi bệnh nhân: {0}. Mời bạn chọn giường khác", Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan])), true);
               txtBedCode.Focus();
               txtBedCode.SelectAll();
               return false;
           }
            return true;
        }

        /// <summary>
        ///     hàm thực hiện việc xử lý thông tin chuyển khoa
        /// </summary>
        private void ProcessChuyenKhoa()
        {
            DataRow query = (from khoa in p_DanhSachPhanBuongGiuong.AsEnumerable()
                             where
                                 Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                                 Utility.Int32Dbnull(Utility.Int32Dbnull(txtPatientDept_ID.Text))
                             select khoa).FirstOrDefault();
            if (query != null)
            {
                NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)));
                if (objRoom != null)
                {
                    query[NoitruDmucBuong.Columns.IdBuong] = Utility.Int32Dbnull(objRoom.IdBuong, -1);
                    query[NoitruDmucBuong.Columns.TenBuong] = Utility.sDbnull(objRoom.TenBuong);
                }
                NoitruDmucGiuongbenh objBed = NoitruDmucGiuongbenh.FetchByID(Utility.Int32Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));
                if (objBed != null)
                {
                    query[NoitruDmucGiuongbenh.Columns.IdGiuong] = Utility.Int32Dbnull(objBed.IdGiuong, -1);
                    query[NoitruDmucGiuongbenh.Columns.TenGiuong] = Utility.sDbnull(objBed.TenGiuong);
                }
                query[NoitruPhanbuonggiuong.Columns.Id] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
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
                    if (objPhanbuonggiuong == null)
                        objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(objPatientExam.IdRavien, 0));
                    txtMaLanKham.Text = Utility.sDbnull(objPatientExam.MaLuotkham);
                    txtSoBHYT.Text = Utility.sDbnull(objPatientExam.MatheBhyt);
                    DmucKhoaphong objLDepartment = DmucKhoaphong.FetchByID(objPatientExam.IdKhoanoitru);
                    if (objLDepartment != null)
                    {
                        txtDepartment_ID.Text = Utility.sDbnull(objLDepartment.IdKhoaphong);
                        txtDepartmentName.Tag = Utility.sDbnull(objLDepartment.IdKhoaphong);
                        txtDepartmentName.Text = Utility.sDbnull(objLDepartment.TenKhoaphong);
                        txtKhoadieutri.Text = txtDepartmentName.Text;
                    }
                    KcbDanhsachBenhnhan objPatientInfo = KcbDanhsachBenhnhan.FetchByID(objPatientExam.IdBenhnhan);
                    if (objPatientInfo != null)
                    {
                        txtPatient_Name.Text = Utility.sDbnull(objPatientInfo.TenBenhnhan);
                        txtPatient_ID.Text = Utility.sDbnull(objPatientExam.IdBenhnhan);
                        txtNamSinh.Text = Utility.sDbnull(objPatientInfo.NamSinh);
                        txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - objPatientInfo.NamSinh);
                        txtPatientSex.Text = objPatientInfo.GioiTinh;// Utility.Int32Dbnull(objPatientInfo.) == 0 ? "Nam" : "Nữ";
                    }
                    if (objPhanbuonggiuong != null)
                    {
                        txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                        
                    }
                    DataTable dtGia= new dmucgiagiuong_busrule().dsGetList("-1").Tables[0];
                    dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia;
                    txtGia.Init(dtGia, new System.Collections.Generic.List<string>() { NoitruGiabuonggiuong.Columns.IdGia, NoitruGiabuonggiuong.Columns.MaGia, NoitruGiabuonggiuong.Columns.TenGia });
                    cboGia.DataSource = dtGia;
                    cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
                    cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
                    cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;
                    m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtDepartment_ID.Text));

                    Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtDataRoom, true, true, "1=1", "sluong_giuong_trong desc,ten_buong");
                    if (grdBuong.DataSource != null)
                    {
                        grdBuong.MoveFirst();
                    }
                }
            }
        }
        /// <summary>
        ///     hàm thực hiện việc chuyển thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyCommnad()
        {
        }

       
        private void frm_phanbuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2) txtRoom_code.Focus();
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
        private void ShowPatientList()
        {
            var frm = new frm_TimKiem_BN();
            frm.SearchByDate = false;
            frm.txtPatientCode.Text = txtMaLanKham.Text;
            frm.ShowDialog();
            if (!frm.m_blnCancel)
            {
                txtMaLanKham.Text = Utility.sDbnull(frm.MaLuotkham);
                txtMaLanKham_KeyDown(txtMaLanKham, new KeyEventArgs(Keys.Enter));
            }
        }


        /// <summary>
        /// Lọc thông tin giường bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBedCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";

                rowFilter = "ma_giuong like '%" + Utility.DoTrim(txtBedCode.Text) + "%' OR ten_giuong like '%" +Utility.DoTrim( txtBedCode.Text) + "%'";
                   
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

        private void txtBedCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_dtDatabed.Rows.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (Utility.isValidGrid(grdGiuong))
                        cmdSave_Click(cmdSave, new EventArgs());
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
            if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdGiuong))
            {
                cmdSave_Click(cmdSave, new EventArgs());
            }
        }

        private void frm_phanbuonggiuong_Load(object sender, EventArgs e)
        {
            if (objPhanbuonggiuong != null)
            {
                txtMaLanKham.Text = objPhanbuonggiuong.MaLuotkham;
                txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                txtDepartment_ID.Text = Utility.sDbnull(objPhanbuonggiuong.IdKhoanoitru);
                DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(txtDepartment_ID.Text));
                if (objDepartment != null)
                {
                    txtDepartmentName.Text = Utility.sDbnull(objDepartment.TenKhoaphong);
                    txtDepartmentName.Tag = Utility.sDbnull(objDepartment.IdKhoaphong);
                }
                dtNgayChuyen.Value = globalVariables.SysDate;
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
                BindData();
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
            }
        }

        private void txtRoom_code_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (m_dtDataRoom.Rows.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                   // txtBedCode.Focus();
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

        private void txtRoom_code_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                rowFilter = "ma_buong like '%" + Utility.DoTrim(txtRoom_code.Text.ToUpper()) + "%' OR ten_buong like '%" + Utility.DoTrim(txtRoom_code.Text.ToUpper()) + "%'";
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=1";
            }
            finally
            {
                m_dtDataRoom.DefaultView.RowFilter = "1=1";
                m_dtDataRoom.DefaultView.RowFilter = rowFilter;
                m_dtDataRoom.AcceptChanges();
                grdBuong.MoveFirst();
                txtBedCode.Focus();
            }
        }
    }
}