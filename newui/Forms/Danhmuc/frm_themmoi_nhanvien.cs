using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
using VNS.Libs;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_nhanvien : Form
    {
        #region "Public Variables(Class Level)

        public string UserName = "";
        public action em_Action = action.Insert;

        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về đối tượng khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;

        public bool m_blnCancel = true;
        private DataTable m_dtDepartmentList = new DataTable();
        private DataTable m_dtDepartmentListUp = new DataTable();

        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public DmucNhanvien m_objObjectReturn = null;

        public DataTable p_dtStaffList = new DataTable();

        #endregion

        private DataTable m_dtKhoThuoc = new DataTable();
        private DataTable m_dtKhoangoaitru = new DataTable();
        private DataTable m_dtKhoanoitru = new DataTable();
        private DataTable m_dtPhongkham = new DataTable();

        public frm_themmoi_nhanvien()
        {
            InitializeComponent();

            InitEvents();
            txtName.LostFocus += txtName_LostFocus;
        }

        private void InitEvents()
        {
            grdKhoa.SelectionChanged += grdKhoa_SelectionChanged;
        }

        private void grdKhoa_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdKhoa))
                {
                    m_dtPhongkham.DefaultView.RowFilter = "1=1";
                    return;
                }
                m_dtPhongkham.DefaultView.RowFilter = "1=1";
                m_dtPhongkham.DefaultView.RowFilter = "ma_cha=" +
                                                      Utility.sDbnull(grdKhoa.GetValue(DmucKhoaphong.Columns.MaCha),
                                                                      "-1");
            }
            catch (Exception ex)
            {
            }
        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {
        }

        private void txtName_LostFocus(object sender, EventArgs e)
        {
            txtName.Text = Utility.chuanhoachuoi(txtName.Text);
        }

        private void txtSpecMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitialData()
        {
            try
            {
                //Khởi tạo danh mục loại nhân viên
                DataTable v_dtStaffTypeList = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAINHANVIEN", true);
                //  v_dtStaffTypeList =
                //   DataBinding.BindData(cboUserName,v_dtStaffTypeList.Select(""));
                cboStaffType.DataSource = v_dtStaffTypeList.DefaultView;
                v_dtStaffTypeList.DefaultView.Sort = DmucChung.Columns.SttHthi;
                cboStaffType.ValueMember = DmucChung.Columns.Ma;
                cboStaffType.DisplayMember = DmucChung.Columns.Ten;
                //Khởi tạo danh mục chức vụ

                //Khởi tạo danh mục phòng ban
                m_dtDepartmentList = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", -1);
                DataBinding.BindData(cboUpLevel, m_dtDepartmentList, DmucKhoaphong.Columns.IdKhoaphong,
                                     DmucKhoaphong.Columns.TenKhoaphong);
                cboUpLevel_SelectedIndexChanged(cboUpLevel, new EventArgs());
                //Khởi tạo danh mục User
                var v_dtUserList = new DataTable();
                if (em_Action == action.Insert)
                {
                    v_dtUserList =
                        new Select().From(SysUser.Schema).Where(SysUser.Columns.PkSuid).NotIn(
                            new Select(DmucNhanvien.Columns.UserName).From(DmucNhanvien.Schema)).ExecuteDataSet().Tables
                            [0];
                    DataBinding.BindData(cboUserName, v_dtUserList, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid);
                }
                else
                {
                    v_dtUserList =
                        new Select().From(SysUser.Schema).ExecuteDataSet().Tables[0];
                    DataBinding.BindData(cboUserName, v_dtUserList, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid);
                }

                //Utility.AddColumnAlltoUserDataTable(ref v_dtUserList, SysUser.Columns.PkSuid, "");
                //v_dtUserList.DefaultView.Sort = SysUser.Columns.PkSuid+" ASC";
                txtUID.Init(v_dtUserList,
                            new List<string> {SysUser.Columns.PkSuid, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid});


                m_dtKhoThuoc = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOCVaTuThuoc();
                Utility.SetDataSourceForDataGridEx(grdKhoThuoc, m_dtKhoThuoc, false, true, "1=1",
                                                   TDmucKho.Columns.SttHthi);
                m_dtPhongkham = THU_VIEN_CHUNG.LaydanhmucPhong(0);
                Utility.SetDataSourceForDataGridEx(grdPhongkham, m_dtPhongkham, false, true, "1=1",
                                                   VDmucKhoaphong.Columns.SttHthi);

                m_dtKhoangoaitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                Utility.SetDataSourceForDataGridEx(grdKhoa, m_dtKhoangoaitru, false, true, "1=1",
                                                   VDmucKhoaphong.Columns.SttHthi);

                m_dtKhoanoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                Utility.SetDataSourceForDataGridEx(grdKhoanoitru, m_dtKhoanoitru, false, true, "1=1",
                                                   VDmucKhoaphong.Columns.SttHthi);

                DataTable dtQuyen =
                    new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("QUYENNHANVIEN").
                        ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdQuyen, dtQuyen, false, true, "1=1",
                                                         DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);

                Utility.SetDataSourceForDataGridEx_Basic(grdLoaiThuoc,
                                                         SPs.DmucLaydanhsachLoaithuoc("-1").GetDataSet().Tables[0],
                                                         false, true, "1=1",
                                                         "stt_nhomthuoc," + DmucLoaithuoc.Columns.SttHthi + "," +
                                                         DmucLoaithuoc.Columns.TenLoaithuoc);
                Utility.SetDataSourceForDataGridEx_Basic(grdDichvuCls,
                                                         new Select().From(DmucChung.Schema).Where(
                                                             DmucChung.Columns.Loai).IsEqualTo("LOAIDICHVUCLS").
                                                             ExecuteDataSet().Tables[0], false, true, "1=1",
                                                         DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện load thông tin của Form khi load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_nhanvien_Load(object sender, EventArgs e)
        {
            InitialData();
            if (em_Action == action.Update) GetData();
            SetStatusAlter();
        }

        private void frm_themmoi_nhanvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        private void cboUpLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataTable = new DataTable();
            dataTable = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1), -1);
            DataBinding.BindData(cboDepart, dataTable, DmucKhoaphong.Columns.IdKhoaphong,
                                 DmucKhoaphong.Columns.TenKhoaphong);
        }

        private void cboUserName_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable objNhanvien =
                new Select("*").From(SysUser.Schema).Where(SysUser.Columns.PkSuid).IsEqualTo(
                    Utility.sDbnull(cboUserName.SelectedValue)).ExecuteDataSet().Tables[0];
            if (objNhanvien != null && em_Action == action.Insert)
            {
                foreach (DataRow row in objNhanvien.AsEnumerable())
                {
                    txtName.Text = row["sFullName"].ToString();
                    txtStaffCode.Text = row["PK_sUID"].ToString();
                    txtKhoa.Text = row["sDepart"].ToString();
                }
            }
        }

        #region "Method of common"

        private readonly Query _Query = DmucNhanvien.CreateQuery();

        /// <summary>
        /// hàm thực hiện validate trạng thái cần nhập
        /// </summary>
        private void SetStatusAlter()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtStaffCode.Text.Trim()))
            {
                Utility.SetMsg(lblMsg, "Bạn nhập phải nhập mã nhân viên", true);
                txtStaffCode.Focus();
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn nhập tên nhân viên", true);
                txtName.Focus();
            }
            if (cboStaffType.SelectedIndex <= -1)
            {
                Utility.SetMsg(lblMsg, "Bạn nhập loại nhân viên", true);
                cboStaffType.Focus();
            }
        }

        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!isValidData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!isValidData()) return;
                    PerformActionUpdate();
                    break;
            }
        }

        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtStaffCode.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập mã nhân viên", true);
                txtStaffCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên nhân viên", true);
                txtName.Focus();
                return false;
            }

            SqlQuery q = new Select().From(DmucNhanvien.Schema)
                .Where(DmucNhanvien.Columns.MaNhanvien).IsEqualTo(txtStaffCode.Text);
            if (em_Action == action.Update)
                q.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
            if (q.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Tồn tại mã nhân viên", true);
                txtStaffCode.Focus();
                return false;
            }

            if (txtUID.MyID != "-1")
            {
                SqlQuery q2 = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.UserName).IsEqualTo(txtUID.MyID);
                if (em_Action == action.Update)
                    q2.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
                if (q2.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Tên đăng nhập đã gán cho một nhân viên khác", true);
                    txtUID.Focus();
                    return false;
                }
            }
            if (cboUserName.SelectedValue != "-1")
            {
                SqlQuery q2 = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.UserName).IsEqualTo(cboUserName.SelectedValue);
                if (em_Action == action.Update)
                    q2.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
                if (q2.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Tên đăng nhập đã gán cho một nhân viên khác", true);
                    cboUserName.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// hamdf thực hiện thêm thông tin 
        /// </summary>
        private void PerformActionInsert()
        {
            DmucNhanvien objDmucNhanvien = TaoDoituongNhanvien();
            QheNhanvienDanhmucCollection lstQheDmuc = GetQheNhanvienDanhmuc(objDmucNhanvien);
            QheNhanvienKhoCollection lstQhekho = GetQuanheNhanVienKho(objDmucNhanvien);
            QheBacsiKhoaphongCollection lstQhekhoa = GetQuanheBsi_khoaphong(objDmucNhanvien);
            QheNhanvienQuyensudungCollection lstQheQuyensudung = GetQuanheNhanVienQuyen(objDmucNhanvien);
            string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung,
                                                        lstQheDmuc);
            if (ErrMsg == string.Empty)
            {
                DataRow dr = p_dtStaffList.NewRow();
                dr[DmucNhanvien.Columns.NguoiTao] = globalVariables.UserName;
                dr[DmucNhanvien.Columns.NgayTao] = globalVariables.SysDate;
                dr[DmucNhanvien.Columns.IdNhanvien] = Utility.Int32Dbnull(
                    _Query.GetMax(DmucNhanvien.Columns.IdNhanvien), -1);
                dr[DmucNhanvien.Columns.MaNhanvien] = txtStaffCode.Text;
                dr[DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                dr[DmucNhanvien.Columns.TenNhanvien] = Utility.sDbnull(txtName.Text, "");

                dr[DmucNhanvien.Columns.MotaThem] = Utility.DoTrim(txtmotathem.Text);
                dr[DmucNhanvien.Columns.IdKhoa] = Utility.Int16Dbnull(cboUpLevel.SelectedValue, 1);
                dr[DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                dr["ten_khoa"] = Utility.sDbnull(cboUpLevel.Text, "");
                dr[DmucNhanvien.Columns.IdPhong] = Utility.Int16Dbnull(cboDepart.SelectedValue, 1);
                dr["ten_phong"] = Utility.sDbnull(cboDepart.Text, "");
                dr["ten_loainhanvien"] = Utility.sDbnull(cboStaffType.Text, "");
                dr[DmucNhanvien.Columns.UserName] = Utility.sDbnull(cboUserName.SelectedValue, "");
                dr[DmucNhanvien.Columns.MaLoainhanvien] = Utility.sDbnull(cboStaffType.SelectedValue, "");

                p_dtStaffList.Rows.InsertAt(dr, 0);
                Close();
            }
            else
            {
                Utility.ShowMsg(ErrMsg);
            }
        }

        private QheNhanvienKhoCollection GetQuanheNhanVienKho(DmucNhanvien objDmucNhanvien)
        {
            var lst = new QheNhanvienKhoCollection();

            foreach (GridEXRow gridExRow in grdKhoThuoc.GetCheckedRows())
            {
                var objDNhanvienKho = new QheNhanvienKho();
                objDNhanvienKho.IdKho = Utility.Int16Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value);
                objDNhanvienKho.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objDNhanvienKho.IsNew = true;
                lst.Add(objDNhanvienKho);
            }
            return lst;
        }

        private QheNhanvienQuyensudungCollection GetQuanheNhanVienQuyen(DmucNhanvien objDmucNhanvien)
        {
            var lst = new QheNhanvienQuyensudungCollection();

            foreach (GridEXRow gridExRow in grdQuyen.GetCheckedRows())
            {
                var objQheNhanvienQuyensudung = new QheNhanvienQuyensudung();
                objQheNhanvienQuyensudung.Ma = Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Ma].Value);
                objQheNhanvienQuyensudung.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienQuyensudung.Loai =
                    Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Loai].Value);
                objQheNhanvienQuyensudung.IsNew = true;
                lst.Add(objQheNhanvienQuyensudung);
            }
            return lst;
        }

        private QheBacsiKhoaphongCollection GetQuanheBsi_khoaphong(DmucNhanvien objDmucNhanvien)
        {
            var lst = new QheBacsiKhoaphongCollection();

            foreach (GridEXRow gridExRow in grdKhoanoitru.GetCheckedRows())
            {
                var objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa =
                    Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 1;
                objQheBacsiKhoaphong.IdPhong = -1;
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
            foreach(GridEXRow gridExRow in grdKhoa.GetCheckedRows())
            {
                var objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa =
                    Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 0;
                objQheBacsiKhoaphong.IdPhong = -1;
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong); 
            }
            foreach (GridEXRow gridExRow in grdPhongkham.GetCheckedRows())
            {
                var objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.MaCha].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 0;
                objQheBacsiKhoaphong.IdPhong =
                    Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
            return lst;
        }

        private QheNhanvienDanhmucCollection GetQheNhanvienDanhmuc(DmucNhanvien objDmucNhanvien)
        {
            var lst = new QheNhanvienDanhmucCollection();

            foreach (GridEXRow gridExRow in grdLoaiThuoc.GetCheckedRows())
            {
                var objQheNhanvienDanhmuc = new QheNhanvienDanhmuc();
                objQheNhanvienDanhmuc.IdDichvu =
                    Utility.sDbnull(gridExRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value);
                objQheNhanvienDanhmuc.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDanhmuc.Loai = 1;
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }

            foreach (GridEXRow gridExRow in grdDichvuCls.GetCheckedRows())
            {
                var objQheNhanvienDanhmuc = new QheNhanvienDanhmuc();
                objQheNhanvienDanhmuc.IdDichvu = Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value);
                objQheNhanvienDanhmuc.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDanhmuc.Loai = 0;
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }
            return lst;
        }


        private DmucNhanvien TaoDoituongNhanvien()
        {
            DmucNhanvien objDmucNhanvien = null;
            if (em_Action == action.Update)
            {
                objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int16Dbnull(txtID.Text, -1));
                objDmucNhanvien.MarkOld();
                objDmucNhanvien.IsNew = false;
                objDmucNhanvien.IsLoaded = true;
            }
            else
            {
                objDmucNhanvien = new DmucNhanvien();
                objDmucNhanvien.IsNew = true;
            }
            objDmucNhanvien.MaNhanvien = Utility.sDbnull(txtStaffCode.Text);
            objDmucNhanvien.TenNhanvien = Utility.sDbnull(txtName.Text);
            objDmucNhanvien.IdPhong = Utility.Int16Dbnull(cboDepart.SelectedValue, -1);
            objDmucNhanvien.IdKhoa = Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1);
            objDmucNhanvien.MaLoainhanvien = Utility.sDbnull(cboStaffType.SelectedValue, "-1");
            objDmucNhanvien.UserName = Utility.sDbnull(cboUserName.SelectedValue, "");
            objDmucNhanvien.TrangThai = Convert.ToByte(chkHienThi.Checked ? 1 : 0);

            objDmucNhanvien.MotaThem = Utility.DoTrim(txtmotathem.Text);
            objDmucNhanvien.NgayTao = globalVariables.SysDate;
            objDmucNhanvien.NguoiTao = globalVariables.UserName;

            return objDmucNhanvien;
        }

        private void PerformActionUpdate()
        {
            try
            {
                DmucNhanvien objDmucNhanvien = TaoDoituongNhanvien();
                QheNhanvienDanhmucCollection lstQheDmuc = GetQheNhanvienDanhmuc(objDmucNhanvien);
                QheNhanvienKhoCollection lstQhekho = GetQuanheNhanVienKho(objDmucNhanvien);
                QheBacsiKhoaphongCollection lstQhekhoa = GetQuanheBsi_khoaphong(objDmucNhanvien);
                QheNhanvienQuyensudungCollection lstQheQuyensudung = GetQuanheNhanVienQuyen(objDmucNhanvien);
                string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung,
                                                            lstQheDmuc);
                if (ErrMsg == string.Empty)
                {
                    DataRow[] dr =
                        p_dtStaffList.Select(DmucNhanvien.Columns.IdNhanvien + "=" + Utility.Int32Dbnull(txtID.Text, -1));
                    if (dr.GetLength(0) > 0)
                    {
                        dr[0][DmucNhanvien.Columns.UserName] = Utility.sDbnull(cboUserName.SelectedValue);
                        dr[0][DmucNhanvien.Columns.MaLoainhanvien] = Utility.sDbnull(cboStaffType.SelectedValue, -1);
                        dr[0]["ten_loainhanvien"] = Utility.sDbnull(cboStaffType.Text, "");
                        dr[0]["ten_phong"] = Utility.sDbnull(cboDepart.Text, -1);
                        dr[0][DmucNhanvien.Columns.IdPhong] = Utility.Int32Dbnull(cboDepart.SelectedValue, -1);
                        dr[0]["ten_khoa"] = Utility.sDbnull(cboUpLevel.Text, -1);
                        dr[0][DmucNhanvien.Columns.IdKhoa] = Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1);
                        dr[0][DmucNhanvien.Columns.MaNhanvien] = txtStaffCode.Text;
                        dr[0][DmucNhanvien.Columns.TenNhanvien] = Utility.sDbnull(txtName.Text, "");
                        dr[0][DmucNhanvien.Columns.MotaThem] = Utility.DoTrim(txtmotathem.Text);
                        dr[0][DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                        dr[0][DmucNhanvien.Columns.NguoiSua] = globalVariables.UserName;
                    }
                    p_dtStaffList.AcceptChanges();
                    Close();
                }
                else
                {
                    Utility.ShowMsg(ErrMsg);
                }
            }
            catch
            {
            }
        }

        private void AutocheckPhongkham(string danhmucphongkham)
        {
            try
            {
                if (!string.IsNullOrEmpty(danhmucphongkham))
                {
                    string[] rows = danhmucphongkham.Split(',');
                    foreach (GridEXRow gridExRow in grdPhongkham.GetDataRows())
                    {
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                if (Utility.sDbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value, "-1") ==
                                    Utility.sDbnull(row))
                                {
                                    gridExRow.IsChecked = true;
                                    break;
                                }
                                else
                                {
                                    gridExRow.IsChecked = false;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void GetData()
        {
            DmucNhanvien objStaffList = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objStaffList != null)
            {
                txtName.Text = Utility.sDbnull(objStaffList.TenNhanvien);
                txtStaffCode.Text = Utility.sDbnull(objStaffList.MaNhanvien);

                cboStaffType.SelectedIndex = Utility.GetSelectedIndex(cboStaffType,
                                                                      objStaffList.MaLoainhanvien);
                txtUID.SetCode(objStaffList.UserName);
                cboUserName.SelectedIndex = Utility.GetSelectedIndex(cboUserName, objStaffList.UserName);

                cboUpLevel.SelectedIndex = Utility.GetSelectedIndex(cboUpLevel,
                                                                    objStaffList.IdKhoa.ToString());
                cboDepart.SelectedIndex = Utility.GetSelectedIndex(cboDepart,
                                                                   objStaffList.IdPhong.ToString());
                chkHienThi.Checked = Utility.Int32Dbnull(objStaffList.TrangThai, 0) == 1;


                LoadQuanHeNhanVienKho();
                LoadQuanHeNhanVienQuyen();
                LoadQheBS_khoanoitru();
                LoadQheBS_khoangoaitru();
                LoadQheLoaithuoc();
                LoadQheDichvuCLS();
            }
        }

        private void LoadQheLoaithuoc()
        {
            var lstQhenhanviendanhmucthuoc = new Select().From(QheNhanvienDanhmuc.Schema)
                .Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheNhanvienDanhmuc.Columns.Loai).IsEqualTo(1)
                .ExecuteAsCollection<QheNhanvienDanhmucCollection>();
            foreach (GridEXRow gridExRow in grdLoaiThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheNhanvienDanhmuc> query = from kho in lstQhenhanviendanhmucthuoc.AsEnumerable()
                                                        where
                                                            kho.IdDichvu ==
                                                            Utility.sDbnull(
                                                                gridExRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value)
                                                        select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }

        private void LoadQheDichvuCLS()
        {
            var lstQhenhanviendanhmucdichvucls = new Select().From(QheNhanvienDanhmuc.Schema)
                .Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheNhanvienDanhmuc.Columns.Loai).IsEqualTo(0)
                .ExecuteAsCollection<QheNhanvienDanhmucCollection>();
            foreach (GridEXRow gridExRow in grdDichvuCls.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheNhanvienDanhmuc> query = from kho in lstQhenhanviendanhmucdichvucls.AsEnumerable()
                                                        where
                                                            kho.IdDichvu ==
                                                            Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value)
                                                        select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }

        private void LoadQheBS_khoanoitru()
        {
            var lstQheBacsiKhoaphong = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(1)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (GridEXRow gridExRow in grdKhoanoitru.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheBacsiKhoaphong> query = from kho in lstQheBacsiKhoaphong.AsEnumerable()
                                                       where
                                                           kho.IdKhoa ==
                                                           Utility.Int32Dbnull(
                                                               gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                                                       select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }

        private void LoadQheBS_khoangoaitru()
        {
            var lstQheBacsiKhoaphongCollection = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(0)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (GridEXRow gridExRow in grdKhoa.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheBacsiKhoaphong> query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                                                       where
                                                           kho.IdKhoa ==
                                                           Utility.Int32Dbnull(
                                                               gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                                                       select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
            foreach (GridEXRow gridExRow in grdPhongkham.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheBacsiKhoaphong> query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                                                       where
                                                           kho.IdPhong ==
                                                           Utility.Int32Dbnull(
                                                               gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                                                       select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }

        private void LoadQuanHeNhanVienQuyen()
        {
            var LstQheNhanvienQuyensudung = new Select().From(QheNhanvienQuyensudung.Schema)
                .Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienQuyensudungCollection>();
            foreach (GridEXRow gridExRow in grdQuyen.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheNhanvienQuyensudung> query = from kho in LstQheNhanvienQuyensudung.AsEnumerable()
                                                            where
                                                                kho.Ma ==
                                                                Utility.sDbnull(
                                                                    gridExRow.Cells[QheNhanvienQuyensudung.Columns.Ma].
                                                                        Value)
                                                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }

        private void LoadQuanHeNhanVienKho()
        {
            var objNhanvienKhoCollection = new Select().From(QheNhanvienKho.Schema)
                .Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienKhoCollection>();
            foreach (GridEXRow gridExRow in grdKhoThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                IEnumerable<QheNhanvienKho> query = from kho in objNhanvienKhoCollection.AsEnumerable()
                                                    where
                                                        kho.IdKho ==
                                                        Utility.Int32Dbnull(
                                                            gridExRow.Cells[TDmucKho.Columns.IdKho].Value)
                                                    select kho;
                if (query.Count() > 0)
                {
                    gridExRow.Cells["IsChon"].Value = 1;
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.Cells["IsChon"].Value = 0;
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
            }
        }

        #endregion
    }
}