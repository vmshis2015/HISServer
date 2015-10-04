using System;
using System.Data;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_phongban : Form
    {
        private readonly Query m_Query = DmucKhoaphong.CreateQuery();

        #region "THUOC TINH"

        public DataRow drDepartment;
        public DataTable dsDepartment = new DataTable();
        public DataGridView grdList = new DataGridView();
        public action m_enAction = action.Insert;

        #endregion

        #region "THUOC TINH KHAI BAO PRIVATE"

        private DataTable dtDepartment = new DataTable();

        #endregion

        #region "Contructor"

        public frm_themmoi_phongban()
        {
            InitializeComponent();

            txtDepartment_Name.LostFocus += txtDepartment_Name_LostFocus;
            KeyPreview = true;
            //cboDeptType.SelectedIndex = 0;
            KeyDown += frm_themmoi_phongban_KeyDown;
            //sysColor.BackColor = globalVariables.SystemColor;
            txtDepartment_Code.CharacterCasing = CharacterCasing.Upper;
            txtDonvitinh._OnShowData += txtDonvitinh__OnShowData;
        }

        private void txtDonvitinh__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG("DONVITINH");
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);
                txtDonvitinh.Focus();
            }
        }

        #endregion

        /// <summary>
        /// hàm thực hiện thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện dùng phím tắt của Form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_phongban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdGhi.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện load thông tin khi load Form lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_phongban_Load(object sender, EventArgs e)
        {
            BindParent_ID();
            txtDonvitinh.Init();
            cboParent_ID.SelectedIndex = 0;
            if (m_enAction == action.Update) GetData();
            if (string.IsNullOrEmpty(txtID.Text))
                txtintOrder.Text =
                    Utility.sDbnull(
                        new Select(Aggregate.Max(DmucKhoaphong.Columns.SttHthi)).From(DmucKhoaphong.Schema).
                            ExecuteScalar<int>() + 1);
        }

        /// <summary>
        /// hàm thực hiện Ghi thông tin khoa phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            switch (m_enAction)
            {
                case action.Insert:
                    Insert();
                    break;
                case action.Update:
                    Update();
                    break;
            }
            Close();
        }

        /// <summary>
        /// hàm thực hiện thêm thông tin 
        /// </summary>
        private void Insert()
        {
            try
            {
                Int16 result = 0;
                result = CreateDepartment();
                if (result > 0)
                {
                    ProcessData(chkParent.Checked ? Utility.Int32Dbnull(cboParent_ID.SelectedValue, 0) : 0);
                    //Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        /// hàm thực hiện xử lý thông tin khoa phòng
        /// </summary>
        /// <param name="Parent_ID"></param>
        private void ProcessData(int Parent_ID)
        {
            int departId = Utility.Int32Dbnull(m_Query.GetMax(DmucKhoaphong.Columns.IdKhoaphong));

            DataRow dr = dsDepartment.NewRow();
            dr[DmucKhoaphong.Columns.IdKhoaphong] = departId;
            dr[DmucKhoaphong.Columns.MaPhongStt] = Utility.sDbnull(txtMaphongXepStt.Text);
            dr[DmucKhoaphong.Columns.ChiDan] = Utility.sDbnull(txtPhong_Thien.Text);
            dr["ten_khoaphong_captren"] = chkParent.Checked ? cboParent_ID.Text : "CÁC KHOA";
            // dr["DeptType_Name"] = chkNoitru.Checked?"Nội trú":"Ngoại trú";
            dr[DmucKhoaphong.Columns.TenKhoaphong] = txtDepartment_Name.Text;
            dr[DmucKhoaphong.Columns.MaKhoaphong] = txtDepartment_Code.Text;
            dr[DmucKhoaphong.Columns.NoitruNgoaitru] = optNgoaitru.Checked
                                                           ? "NGOAI"
                                                           : (optNoitru.Checked ? "NOI" : "CAHAI");
            dr[DmucKhoaphong.Columns.KieuKhoaphong] = optPhong.Checked ? "PHONG" : "KHOA";
            dr["ten_kieukhoaphong"] = optPhong.Checked ? "Phòng" : "Khoa";
            dr[DmucKhoaphong.Columns.MotaThem] = txtDesc.Text;
            dr[DmucKhoaphong.Columns.SttHthi] = txtintOrder.Value;
            dr[DmucKhoaphong.Columns.MaCha] = Parent_ID;
            dr[DmucKhoaphong.Columns.DonGia] = Utility.DecimaltoDbnull(txtDeptFee.Text, 0);
            dr[DmucKhoaphong.Columns.TamUng] = Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0);
            dr[DmucKhoaphong.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucKhoaphong.Columns.NguoiTao] = globalVariables.UserName;
            dr[DmucKhoaphong.Columns.LaKhoacapcuu] = chkKhoaCapCuu.Checked ? 1 : 0;
            dr[DmucKhoaphong.Columns.MaDonvitinh] = txtDonvitinh.myCode;
            dr[DmucKhoaphong.Columns.PhongChucnang] = (byte) (optChucnang.Checked ? 1 : 0);
            dr["ten_donvitinh"] = txtDonvitinh.Text;
            dsDepartment.Rows.Add(dr);
            dsDepartment.AcceptChanges();
        }

        /// <summary>
        /// Hàm thực hiện khởi tạo thôngtin của khoa phòng
        /// </summary>
        /// <returns></returns>
        private Int16 CreateDepartment()
        {
            try
            {
                short Parent_ID = 0;

                var department = new DmucKhoaphong();
                department.MaKhoaphong = txtDepartment_Code.Text;
                department.SttHthi = Utility.Int16Dbnull(txtintOrder.Value, 0);
                department.TenKhoaphong = txtDepartment_Name.Text;
                department.DonGia = Utility.DecimaltoDbnull(txtDeptFee.Text, 0);
                department.MotaThem = Utility.sDbnull(txtDesc.Text, "");
                department.NoitruNgoaitru = optNgoaitru.Checked ? "NGOAI" : (optNoitru.Checked ? "NOI" : "CAHAI");
                department.MaCha =
                    (short?) (chkParent.Checked ? Utility.Int16Dbnull(cboParent_ID.SelectedValue, -1) : 0);
                department.TamUng = Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0);
                department.MaDonvitinh = txtDonvitinh.myCode;
                department.NgayTao = globalVariables.SysDate;
                department.NguoiTao = globalVariables.UserName;
                department.MaPhongStt = Utility.sDbnull(txtMaphongXepStt.Text);
                department.ChiDan = Utility.sDbnull(txtPhong_Thien.Text);

                department.KieuKhoaphong = optPhong.Checked ? "PHONG" : "KHOA";

                department.PhongChucnang = (byte) (optChucnang.Checked ? 1 : 0);

                department.LaKhoacapcuu = (byte) (chkKhoaCapCuu.Checked ? 1 : 0);
                department.IsNew = true;
                department.Save();
                return department.IdKhoaphong;
            }
            catch
            {
                return -1;
            }
        }


        /// <summary>
        /// hàm thực hiện Update thông tin
        /// </summary>
        private void Update()
        {
            try
            {
                int Parent_ID = 0;

                int record = new Update(DmucKhoaphong.Schema)
                    .Set(DmucKhoaphong.Columns.MaPhongStt).EqualTo(Utility.sDbnull(txtMaphongXepStt.Text))
                    .Set(DmucKhoaphong.Columns.ChiDan).EqualTo(txtPhong_Thien.Text)
                    .Set(DmucKhoaphong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Set(DmucKhoaphong.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(DmucKhoaphong.Columns.TenKhoaphong).EqualTo(txtDepartment_Name.Text)
                    .Set(DmucKhoaphong.Columns.LaKhoacapcuu).EqualTo(chkKhoaCapCuu.Checked ? 1 : 0)
                    .Set(DmucKhoaphong.Columns.MaKhoaphong).EqualTo(txtDepartment_Code.Text)
                    .Set(DmucKhoaphong.Columns.PhongChucnang).EqualTo(optChucnang.Checked ? 1 : 0)
                    .Set(DmucKhoaphong.Columns.KieuKhoaphong).EqualTo(optPhong.Checked ? "PHONG" : "KHOA")
                    .Set(DmucKhoaphong.Columns.NoitruNgoaitru).EqualTo(optNgoaitru.Checked
                                                                           ? "NGOAI"
                                                                           : (optNoitru.Checked ? "NOI" : "CAHAI"))
                    .Set(DmucKhoaphong.Columns.SttHthi).EqualTo(txtintOrder.Value)
                    .Set(DmucKhoaphong.Columns.MotaThem).EqualTo(txtDesc.Text)
                    .Set(DmucKhoaphong.Columns.MaCha).EqualTo(chkParent.Checked
                                                                  ? Utility.Int32Dbnull(cboParent_ID.SelectedValue, -1)
                                                                  : 0)
                    .Set(DmucKhoaphong.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(txtDeptFee.Text, 0))
                    .Set(DmucKhoaphong.Columns.TamUng).EqualTo(Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0))
                    .Set(DmucKhoaphong.Columns.MaDonvitinh).EqualTo(txtDonvitinh.myCode)
                    .Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))
                    .Execute();
                if (record > 0)
                {
                    //Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                    foreach (DataRow dr in dsDepartment.Rows)
                    {
                        if (dr[DmucKhoaphong.Columns.IdKhoaphong].ToString() == Utility.sDbnull(txtID.Text, -1))
                        {
                            dr[DmucKhoaphong.Columns.MaPhongStt] = Utility.sDbnull(txtMaphongXepStt.Text);
                            dr[DmucKhoaphong.Columns.ChiDan] = Utility.sDbnull(txtPhong_Thien.Text);
                            dr[DmucKhoaphong.Columns.NgaySua] = globalVariables.SysDate;
                            dr[DmucKhoaphong.Columns.NguoiSua] = globalVariables.UserName;
                            dr[DmucKhoaphong.Columns.TenKhoaphong] = Utility.sDbnull(txtDepartment_Name.Text, "");
                            dr[DmucKhoaphong.Columns.MaKhoaphong] = Utility.sDbnull(txtDepartment_Code.Text, "");
                            dr[DmucKhoaphong.Columns.LaKhoacapcuu] = chkKhoaCapCuu.Checked ? 1 : 0;
                            dr[DmucKhoaphong.Columns.NoitruNgoaitru] = optNgoaitru.Checked
                                                                           ? "NGOAI"
                                                                           : (optNoitru.Checked ? "NOI" : "CAHAI");
                            dr[DmucKhoaphong.Columns.SttHthi] = Utility.Int32Dbnull(txtintOrder.Text, 0);
                            dr[DmucKhoaphong.Columns.MaCha] = chkParent.Checked
                                                                  ? Utility.Int32Dbnull(cboParent_ID.SelectedValue, -1)
                                                                  : 0;
                            dr[DmucKhoaphong.Columns.DonGia] = Utility.DecimaltoDbnull(txtDeptFee.Text, 0);
                            dr[DmucKhoaphong.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text, "");
                            dr[DmucKhoaphong.Columns.TamUng] = Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0);
                            dr[DmucKhoaphong.Columns.KieuKhoaphong] = optPhong.Checked ? "PHONG" : "KHOA";
                            dr[DmucKhoaphong.Columns.MaDonvitinh] = txtDonvitinh.myCode;
                            dr[DmucKhoaphong.Columns.PhongChucnang] = (byte) (optChucnang.Checked ? 1 : 0);
                            dr["ten_donvitinh"] = txtDonvitinh.Text;
                            dr["ten_kieukhoaphong"] = optPhong.Checked ? "Phòng" : "Khoa";
                            dr["ten_khoaphong_captren"] = chkParent.Checked ? cboParent_ID.Text : "CÁC KHOA";
                            break;
                        }
                    }
                    dsDepartment.AcceptChanges();
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            //Utility.GotoNewRow(grdList,"coDmucKhoaphong_ID",txtID.Text);
        }

        /// <summary>
        /// chỉ cho  nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDeptFee_TextChanged(object sender, EventArgs e)
        {
            // Utility.FormatCurrencyHIS(txtDeptFee);
        }

        private void txtDeptFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Utility.OnlyDigit(e);
        }

        private void chkParent_CheckedChanged(object sender, EventArgs e)
        {
            cboParent_ID.Enabled = chkParent.Checked;
        }

        private void txtDepartment_Name_LostFocus(object sender, EventArgs e)
        {
            txtDepartment_Name.Text = Utility.chuanhoachuoi(txtDepartment_Name.Text);
        }

        /// <summary>
        /// chỉ cho nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTIEN_TAM_UNG_TextChanged(object sender, EventArgs e)
        {
            // Utility.FormatCurrencyHIS(txtTIEN_TAM_UNG);
        }

        private void txtTIEN_TAM_UNG_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Utility.OnlyDigit(e);
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC CHO PHÉP BẠN THỰC HIỆN VIỆC KIỂM TRA THÔNG TIN CỦA PHẦN NỘI TRÚ HOẶC NGOẠI TRÚ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboParent_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #region "HAM DUNG CHUNG"

        /// <summary>
        /// hàm thực hiện set validate các trường phải nhập chỉ số
        /// </summary>
        /// <summary>
        /// hàm thực hiện lấy thông tin đơn vị cấp trên
        /// </summary>
        private void BindParent_ID()
        {
            dtDepartment =
                new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaCha).IsEqualTo(0)
                    .OrderDesc(DmucKhoaphong.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombox(cboParent_ID, dtDepartment, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong);
        }

        /// <summary>
        /// hàm thực hiện validate thông tin
        /// </summary>
        /// <returns></returns>
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);


            if (string.IsNullOrEmpty(txtDepartment_Code.Text.Trim()))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập mã khoa phòng", true);
                txtDepartment_Code.Focus();
                return false;
            }
            SqlQuery q =
                new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(
                    Utility.DoTrim(txtDepartment_Code.Text));
            if (m_enAction == action.Update)
                q.And(DmucKhoaphong.Columns.IdKhoaphong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg,
                               "Đã tồn tại khoa phòng có mã " + Utility.DoTrim(txtDepartment_Code.Text) +
                               ". Mời bạn nhập mã khác", true);
                txtDepartment_Code.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDepartment_Name.Text.Trim()))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên khoa phòng", true);
                txtDepartment_Name.Focus();
                return false;
            }
            if (chkParent.Checked)
            {
                if (cboParent_ID.SelectedIndex <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải chọn khoa/phòng cấp trên", true);
                    cboParent_ID.Focus();
                    return false;
                }
            }
            if (txtDonvitinh.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn đơn vị tính", true);
                txtDonvitinh.Focus();
                return false;
            }
            return true;
        }

        private void GetData()
        {
            DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objDepartment != null)
            {
                txtDepartment_Code.Text = objDepartment.MaKhoaphong;
                txtDepartment_Name.Text = objDepartment.TenKhoaphong;
                txtintOrder.Text = Utility.sDbnull(objDepartment.SttHthi);

                chkParent.Checked = objDepartment.MaCha.ToString() == "0" ? false : true;

                cboParent_ID.SelectedIndex =
                    Utility.GetSelectedIndex(cboParent_ID, Utility.sDbnull(objDepartment.MaCha, "-1"));
                txtDesc.Text = Utility.sDbnull(objDepartment.MotaThem, "");
                optPhong.Checked = objDepartment.KieuKhoaphong == "PHONG";
                if (objDepartment.NoitruNgoaitru == "NOI") optNoitru.Checked = true;
                else if (objDepartment.NoitruNgoaitru == "NGOAI") optNgoaitru.Checked = true;
                else optKhac.Checked = true;
                if (objDepartment.PhongChucnang == 1) optChucnang.Checked = true;
                else optChuyenmon.Checked = true;
                // cboParent_ID.Enabled=Utility.sDbnull(drDepartment[DmucKhoaphong.Columns.MaCha], "-1") == "0" ? true : false;
                // cboParent_ID.SelectedIndex = Utility.GetSelectedIndex(cboParent_ID,Utility.sDbnull(objDepartment.MaCha,-1));
                txtTIEN_TAM_UNG.Text = Utility.sDbnull(objDepartment.TamUng, 0);
                txtDeptFee.Text = Utility.sDbnull(objDepartment.DonGia, "0");
                chkKhoaCapCuu.Checked = Utility.Int32Dbnull(objDepartment.LaKhoacapcuu, 0) == 1;
                txtDonvitinh.SetCode(objDepartment.MaDonvitinh);
                txtMaphongXepStt.Text = Utility.sDbnull(objDepartment.MaPhongStt);
                txtPhong_Thien.Text = Utility.sDbnull(objDepartment.ChiDan);
            }
        }

        #endregion
    }
}