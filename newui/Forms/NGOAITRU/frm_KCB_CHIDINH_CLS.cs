using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using SubSonic.Sugar;
using VNS.Libs;
using VNS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using Strings = Microsoft.VisualBasic.Strings;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_CHIDINH_CLS : Form
    {
        private readonly Logger log;
        private int CurrentRowIndex = -1;
        public int Exam_ID = -1;
        decimal BHYT_PTRAM_TRAITUYENNOITRU=0;
        private string Help =
            "Lấy thông tin dịch vụ CLS vào lưới chọn dịch vụ=NOITIET_GetDataServiceDetail\n.Lấy thông tin chi tiết CLS đã đăng ký=YHHQ_LAYTHONGTIN_DICHVU_THUOC_VATTU";
        KCB_CHIDINH_CANLAMSANG CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        KCB_THAMKHAM __THAMKHAM = new KCB_THAMKHAM();

        private int ServiceDetail_Id;
        bool m_blnAllowSelectionChanged = false;
        private ActionResult actionResult = ActionResult.Error;
        public bool m_blnCancel=true;


        private bool isSaved;
        private int lastIndex;
        private char lastKey;
        private DataTable m_dtAssignDetail = new DataTable();
        private DataTable m_dtReport = new DataTable();
        public DataTable m_dtServiceDetail = new DataTable();
        public action m_eAction = action.Insert;
        private bool neverFound = true;
        public KcbLuotkham objLuotkham;
        public KcbDanhsachBenhnhan objBenhnhan;
        public DataTable p_AssignInfo;
        private string rowFilter = "1=1";
        private string strQuestion = "";
        private string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfigKedonthuoc.txt";
        private int v_AssignId = -1;
        string nhomchidinh = "";
        byte chidinhgoi = 0;
        byte chidinhchiphithem = 0;
        #region "khai báo khởi tạo ban đầu"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nhomchidinh">Mô tả thêm của nhóm chỉ định CLS dùng cho việc tách form kê chỉ định CLS và kê gói dịch vụ</param>
        public frm_KCB_CHIDINH_CLS(string nhomchidinh, byte chidinhgoi, byte chidinhchiphithem)
        {
            InitializeComponent();
            InitEvents();
            this.nhomchidinh = nhomchidinh;
            this.chidinhchiphithem = chidinhchiphithem;
            this.chidinhgoi = chidinhgoi;
            
            log = LogManager.GetCurrentClassLogger();
            dtRegDate.Value = globalVariables.SysDate;
            chkChiDinhNhanh.Visible = globalVariables.IsAdmin;
            if (globalVariables.gv_UserAcceptDeleted) FormatUserNhapChiDinh();
            CauHinh();
        }

        void InitEvents()
        {
            Load += new EventHandler(frm_KCB_CHIDINH_CLS_Load);
            KeyDown += new KeyEventHandler(frm_KCB_CHIDINH_CLS_KeyDown);
            FormClosing += new FormClosingEventHandler(frm_KCB_CHIDINH_CLS_FormClosing);

            grdServiceDetail.CellValueChanged += new ColumnActionEventHandler(grdServiceDetail_CellValueChanged);
            grdServiceDetail.Enter += new EventHandler(grdServiceDetail_Enter);
            grdServiceDetail.KeyDown += new KeyEventHandler(grdServiceDetail_KeyDown);
            grdServiceDetail.KeyPress += new KeyPressEventHandler(grdServiceDetail_KeyPress);
            grdServiceDetail.SelectionChanged += new EventHandler(grdServiceDetail_SelectionChanged);
            grdServiceDetail.UpdatingCell += new UpdatingCellEventHandler(grdServiceDetail_UpdatingCell);

            grdAssignDetail.CellUpdated += new ColumnActionEventHandler(grdAssignDetail_CellUpdated);
            grdAssignDetail.CellValueChanged += new ColumnActionEventHandler(grdAssignDetail_CellValueChanged);
            grdAssignDetail.ColumnHeaderClick += new ColumnActionEventHandler(grdAssignDetail_ColumnHeaderClick);
            grdAssignDetail.FormattingRow += new RowLoadEventHandler(grdAssignDetail_FormattingRow);
            grdAssignDetail.UpdatingCell += new UpdatingCellEventHandler(grdAssignDetail_UpdatingCell);
            grdAssignDetail.KeyDown += new KeyEventHandler(grdAssignDetail_KeyDown);
            cboDichVu.SelectedIndexChanged += new EventHandler(cboDichVu_SelectedIndexChanged);
            txtFilterName.KeyDown += new KeyEventHandler(txtFilterName_KeyDown);
            txtFilterName.TextChanged += new EventHandler(txtFilterName_TextChanged);
            cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);

            chkChiDinhNhanh.CheckedChanged += new EventHandler(chkChiDinhNhanh_CheckedChanged);
            chkSaveAndPrint.CheckedChanged += new EventHandler(chkSaveAndPrint_CheckedChanged);

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdInPhieuCLS.Click += new EventHandler(cmdInPhieuCLS_Click);
            cmdDelete.Click += new EventHandler(cmdDelete_Click);
            mnuDelete.Click += new EventHandler(mnuDelete_Click);
            cmdTaonhom.Click += cmdTaonhom_Click;
            cmdAccept.Click += cmdAccept_Click;
            txtNhomDichvuCLS._OnEnterMe += txtNhomDichvuCLS__OnEnterMe;
        }

        void txtNhomDichvuCLS__OnEnterMe()
        {
            if (PropertyLib._HISCLSProperties.InsertAfterSelectGroup)
                AddDetailbySelectedGroup();
        }
        void AddDetailbySelectedGroup()
        {
            if (Utility.Int32Dbnull(txtNhomDichvuCLS.MyID, -1) > 0)
            {
                DataTable dtChitietnhom = CHIDINH_CANLAMSANG.DmucLaychitietNhomchidinhCls(Utility.Int32Dbnull(txtNhomDichvuCLS.MyID, -1));
                uncheckItems();
                foreach (GridEXRow row in grdServiceDetail.GetDataRows())
                {
                    if (dtChitietnhom.Select(DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu + "=" + Utility.sDbnull(row.Cells[DmucNhomcanlamsangChitiet.Columns.IdChitietdichvu].Value,"-1")).Length > 0)
                        row.IsChecked = true;
                }
                cmdAddDetail_Click(cmdAddDetail, new EventArgs());
            }
        }
        void cmdAccept_Click(object sender, EventArgs e)
        {
            AddDetailbySelectedGroup();
            txtNhomDichvuCLS.SelectAll();
            txtNhomDichvuCLS.Focus();
        }

        void cmdTaonhom_Click(object sender, EventArgs e)
        {
            frm_quanlynhomchidinh_cls _quanlynhomchidinh_cls = new frm_quanlynhomchidinh_cls();
            _quanlynhomchidinh_cls.ShowDialog();
            txtNhomDichvuCLS.Init(new Select().From(DmucNhomcanlamsang.Schema).ExecuteDataSet().Tables[0], new List<string>() { DmucNhomcanlamsang.Columns.Id, DmucNhomcanlamsang.Columns.MaNhom, DmucNhomcanlamsang.Columns.TenNhom });
        }

        void grdAssignDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.Delete) || e.KeyCode == Keys.Delete)
                mnuDelete_Click(mnuDelete, new EventArgs());
        }
        #endregion

        public int HosStatus { get; set; }
        public KcbDangkyKcb ObjRegExam { get; set; }
        public NoitruPhieudieutri objPhieudieutriNoitru { get; set; }
        // public KcbLuotkham objLuotkham;

        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                chkChiDinhNhanh.Checked = PropertyLib._ThamKhamProperties.Chidinhnhanh;
                chkSaveAndPrint.Checked = PropertyLib._MayInProperties.InCLSsaukhiluu;
            }
        }
        private void frm_KCB_CHIDINH_CLS_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!isSaved) cmdSave_Click(cmdSave, new EventArgs());
            if (grdAssignDetail.RowCount > 0)
            {
                if (!isSaved)
                {
                    if (m_dtAssignDetail.Select("NoSave=1").Length > 0)
                    {
                        if (
                            Utility.AcceptQuestion(
                                "Bạn đã thêm mới một số chỉ định chi tiết mà chưa nhấn Ghi.\nBạn nhấn yes để hệ thống tự động lưu thông tin.\nNhấn No để hủy bỏ các chỉ định vừa thêm mới.", "Thông báo",
                                true))
                        {
                            cmdSave.PerformClick();
                        }
                    }
                }
            }
            SaveCauHinh();
        }

        private void SaveCauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                PropertyLib._MayInProperties.InCLSsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib._ThamKhamProperties.Chidinhnhanh = chkChiDinhNhanh.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
        }
        private void FormatUserNhapChiDinh()
        {
            try
            {
                GridEXColumn gridExColumn =
                    grdAssignDetail.RootTable.Columns[KcbChidinhclsChitiet.Columns.NguoiTao];
                GridEXColumn gridExColumnTarget =
                    grdAssignDetail.RootTable.Columns[DmucDichvuclsChitiet.Columns.TenChitietdichvu];
                var gridExFormatCondition = new GridEXFormatCondition(gridExColumn, ConditionOperator.NotEqual,
                                                                      globalVariables.UserName);
                gridExFormatCondition.FormatStyle.BackColor = Color.Red;
                gridExFormatCondition.TargetColumn = gridExColumnTarget;
                grdAssignDetail.RootTable.FormatConditions.Add(gridExFormatCondition);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// hàm hự hiện việc load form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_CHIDINH_CLS_Load(object sender, EventArgs e)
        {
            try
            {
               
                LaydanhsachbacsiChidinh();
                BHYT_PTRAM_TRAITUYENNOITRU =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false),0m);
                DataBinding.BindDataCombobox(cboDichVu, THU_VIEN_CHUNG.LayThongTinDichVuCLS(nhomchidinh),
                                           DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu,
                                           "Lọc thông tin theo loại dịch vụ", false);

                txtNhomDichvuCLS.Init(new Select().From(DmucNhomcanlamsang.Schema).ExecuteDataSet().Tables[0], new List<string>() { DmucNhomcanlamsang.Columns.Id, DmucNhomcanlamsang.Columns.MaNhom, DmucNhomcanlamsang.Columns.TenNhom });
                InitData();
                GetData();
                if (m_eAction == action.Update)
                {
                    LoadNhomin();
                }
                else
                {

                }
            }
            catch
            {
            }
            finally
            {
                v_AssignId = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
            }
        }
        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, HosStatus);
                VNS.Libs.DataBinding.BindDataCombox(this.cbobacSyChiDinh, data, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (this.cbobacSyChiDinh.Items.Count > 0)
                    {
                        this.cbobacSyChiDinh.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.cbobacSyChiDinh.SelectedIndex = Utility.GetSelectedIndex(this.cbobacSyChiDinh, globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS()) return;
                foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
                {
                    long AssignDetail = Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail);
                    gridExRow.Delete();
                    grdAssignDetail.UpdateData();
                    grdAssignDetail.Refresh();
                    m_dtAssignDetail.AcceptChanges();
                }
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(Utility.Int32Dbnull(txtAssign_ID.Text, -1));
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    if (
                        Utility.AcceptQuestion(
                            "Phiếu điều trị hiện tại không còn chi tiết nữa, Bạn có muốn xóa phiếu điều trị đi không?",
                            "Thông báo", true))
                    {
                        new Delete().From(KcbChidinhcl.Schema)
                            .Where(KcbChidinhcl.Columns.IdChidinh).IsEqualTo(Utility.Int32Dbnull(txtAssign_ID.Text, -1)).
                            Execute();
                        m_eAction = action.Insert;
                        txtAssign_ID.Text = "(Tự sinh)";
                        txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                    }
                }
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// hàm thực hiện kiểm tra thông tin chỉ dịnh cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private bool IsValidDataXoaCLS()
        {
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ cận lâm sàng",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }

            if (!globalVariables.IsAdmin)
            {

                foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
                {
                    int AssignDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                        .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                        return false;
                       
                    }
                }
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                    return false;

                }
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                    return false;

                }
            }
            return true;
        }

        private bool IsValidDataXoaCLS_Selected()
        {
            if (grdAssignDetail.RowCount <= 0 || grdAssignDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ cận lâm sàng",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            int AssignDetail_ID = -1;
            SqlQuery sqlQuery = null;
            if (!globalVariables.IsAdmin)
            {

                AssignDetail_ID =
                    Utility.Int32Dbnull(
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            AssignDetail_ID =
                   Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                       -1);
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }


            AssignDetail_ID =
                Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của dịch vụ cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            isSaved = true;
            SetSaveStatus();
            PerformAction();
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra lại thông tin 
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (Utility.Int32Dbnull(cbobacSyChiDinh.SelectedValue, -1) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện lưu chỉ định", true);
                cbobacSyChiDinh.Focus();
                return false;
            }
            if (objPhieudieutriNoitru != null)
            {
                if (dtRegDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                {
                    Utility.ShowMsg("Ngày kê đơn phải <= " + objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                    dtRegDate.Focus();
                    return false;
                }
            }
            if (grdAssignDetail.RowCount <= 0)
            {
                Utility.ShowMsg("Hiện không có bản ghi nào thực hiện việc lưu lại thông tin", "Thông báo",
                                MessageBoxIcon.Warning);
                cmdSave.Focus();
                return false;
            }

            return true;
        }

        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdAssignDetail.RowCount > 0;
                cmdDelete.Enabled = grdAssignDetail.RowCount > 0;
                cmdInPhieuCLS.Enabled = grdAssignDetail.RowCount > 0;
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh trang thai cua nut {0}", ex);
            }
        }

        /// <summary>
        /// hàm thục hiện việc trạng thái của hoạt động của 
        /// 
        /// </summary>
        private void PerformAction()
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                switch (m_eAction)
                {
                    case action.Insert:
                        InsertDataCLS();
                        break;
                    case action.Update:
                        UpdateDataCLS();
                        break;
                }
               
            }
            catch (Exception)
            {

            }
            finally
            {
                m_blnCancel = false; ;
                ModifyCommand();
                Utility.EnableButton(cmdSave, true);
            }
        }

       

        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin cận lâm sàng
        /// </summary>
        private void InsertDataCLS()
        {
            KcbChidinhcl objKcbChidinhcls = CreateAssignInfo();
            actionResult =
               CHIDINH_CANLAMSANG.InsertDataChiDinhCLS(
                    objKcbChidinhcls, objLuotkham, CreateArrAssignDetail());
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (objKcbChidinhcls != null)
                    {
                        txtAssign_ID.Text = Utility.sDbnull(objKcbChidinhcls.IdChidinh);
                        txtAssignCode.Text = Utility.sDbnull(objKcbChidinhcls.MaChidinh);
                        barcode1.Data = txtAssignCode.Text;
                    }
                    m_eAction = action.Update;
                    m_blnCancel = false;
                    //LayThongTin_Chitiet_CLS();
                    if (chkSaveAndPrint.Checked)
                    {
                        Utility.EnableButton(cmdSave, false);
                        GetData();
                        LoadNhomin();
                        modifyRegions();
                        Utility.EnableButton(cmdSave, true);
                    }
                    else
                        Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
            ModifyCommand();
        }
        bool hasLoadPrintType = false;
        void LoadNhomin()
        {
            if (hasLoadPrintType) return;
            hasLoadPrintType = true;
            chkIntach.Visible = true;
            cboServicePrint.Visible = true;
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
        }
        void modifyRegions()
        {
            pnlQuickSearch.Height = 0;
            pnlLeft.Width = 0;
            cmdSave.Visible = false;
            cmdDelete.Visible = false;
        }
        /// <summary>
        /// hmf thực hiện cập n hập thông tin cận lâm sàng
        /// </summary>
        private void UpdateDataCLS()
        {
            KcbChidinhcl objKcbChidinhcls = CreateAssignInfo();
            actionResult =
                CHIDINH_CANLAMSANG.UpdateDataChiDinhCLS(
                    objKcbChidinhcls, objLuotkham, CreateArrAssignDetail());
            switch (actionResult)
            {
                case ActionResult.Success:
                    m_blnCancel = false;
                    if (chkSaveAndPrint.Checked)
                    {
                      
                    }
                    //Utility.ShowMsg("Bạn thực hiện sửa chỉ định thành công", "Thông báo");
                    Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
        }

        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của phiếu chỉ định cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private KcbChidinhcl CreateAssignInfo()
        {
            var objKcbChidinhcls = new KcbChidinhcl();
            objKcbChidinhcls.MaChidinh = string.Empty;
            objKcbChidinhcls.MaLuotkham = objLuotkham.MaLuotkham;
            objKcbChidinhcls.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan, -1);
            objKcbChidinhcls.IdBacsiChidinh = Utility.Int16Dbnull(cbobacSyChiDinh.SelectedValue,globalVariables.gv_intIDNhanvien);
            objKcbChidinhcls.IdKhoaChidinh = (Int16)globalVariables.idKhoatheoMay;
            objKcbChidinhcls.NguoiTao = globalVariables.UserName;
            objKcbChidinhcls.NgayTao = globalVariables.SysDate;
            objKcbChidinhcls.NgayChidinh = dtRegDate.Value.Date;
            objKcbChidinhcls.ChidinhGoidichvu = chidinhgoi;
            objKcbChidinhcls.ChidinhChiphithem = chidinhchiphithem;
            objKcbChidinhcls.IdKham = Exam_ID;
            if (HosStatus == 0)
            {
                if (ObjRegExam != null)
                {
                    objKcbChidinhcls.IdPhongChidinh = Utility.Int16Dbnull(ObjRegExam.IdPhongkham);
                }
                else
                {
                    objKcbChidinhcls.IdPhongChidinh = (Int16)globalVariables.idKhoatheoMay;
                }
            }
            else
            {

            }
            if (objPhieudieutriNoitru != null)
            {
                objKcbChidinhcls.IdDieutri = objPhieudieutriNoitru.IdPhieudieutri;
                objKcbChidinhcls.IdKhoadieutri = objPhieudieutriNoitru.IdKhoanoitru;
                objKcbChidinhcls.IdBuongGiuong = objPhieudieutriNoitru.IdBuongGiuong;
                objKcbChidinhcls.IdPhongChidinh = objPhieudieutriNoitru.IdKhoanoitru;
            }

            objKcbChidinhcls.Noitru = (byte?)HosStatus;
            if (m_eAction == action.Update)
            {
                objKcbChidinhcls.IdChidinh = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
                objKcbChidinhcls.NgaySua = globalVariables.SysDate;
                objKcbChidinhcls.NguoiSua = globalVariables.UserName;
                objKcbChidinhcls.IpMaysua = globalVariables.gv_strIPAddress;
                objKcbChidinhcls.TenMaysua = globalVariables.gv_strComputerName;
            }
            else
            {
                 objKcbChidinhcls.IpMaytao = globalVariables.gv_strIPAddress;
                objKcbChidinhcls.TenMaytao = globalVariables.gv_strComputerName;
            }


            return objKcbChidinhcls;
        }

        /// <summary>
        /// hàm thực hiện việc tạo mảng thông tin của chỉ định chi tiết cận lâm sàng
        /// </summary>
        /// <returns></returns>
        private KcbChidinhclsChitiet[] CreateArrAssignDetail()
        {
            int i = 0;
            foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record) i++;
            }
            int idx = 0;
            var arrAssignDetail = new KcbChidinhclsChitiet[i];
            foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    arrAssignDetail[idx] = new KcbChidinhclsChitiet();
                    if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1) > 0)
                    {
                        arrAssignDetail[idx].IsLoaded = true;
                        arrAssignDetail[idx].IsNew = false;
                        arrAssignDetail[idx].MarkOld();
                        arrAssignDetail[idx].IdChitietchidinh = Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    }
                    else
                    {
                        arrAssignDetail[idx].IsNew = true;
                    }
                    arrAssignDetail[idx].IdChidinh = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
                    arrAssignDetail[idx].NguoiTao = globalVariables.UserName;
                    arrAssignDetail[idx].IdDichvu = Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdDichvu].Value, -1);
                    arrAssignDetail[idx].IdChitietdichvu = Utility.Int16Dbnull(
                        gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                   
                    arrAssignDetail[idx].SoLuong = Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                    arrAssignDetail[idx].DonGia = Utility.DecimaltoDbnull(
                        gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    arrAssignDetail[idx].PhuThu =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                    arrAssignDetail[idx].HienthiBaocao = 0;
                    
                    if (m_eAction == action.Insert)
                    {
                        arrAssignDetail[idx].TrangthaiBhyt = 0;
                        arrAssignDetail[idx].TrangthaiHuy = 0;
                        arrAssignDetail[idx].TrangThai = 0;
                        
                    }
                    arrAssignDetail[idx].TrangthaiThanhtoan = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan].Value, 0);
                    //arrAssignDetail[idx].MotaThem = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MotaThem].Value, "");

                    arrAssignDetail[idx].NgayTao = globalVariables.SysDate;
                    arrAssignDetail[idx].TuTuc = Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0);
                    arrAssignDetail[idx].MadoituongGia = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value, objLuotkham.MaDoituongKcb);

                    arrAssignDetail[idx].IdThanhtoan = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdThanhtoan].Value, -1);
                    if (Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0) == 0)
                    {
                        decimal BHCT = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                        }
                        decimal BNCT =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) -
                            BHCT;
                        arrAssignDetail[idx].BhytChitra = BHCT;
                        arrAssignDetail[idx].BnhanChitra = BNCT;
                        arrAssignDetail[idx].PtramBhyt = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                        arrAssignDetail[idx].PtramBhytGoc = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    }
                    else
                    {
                        arrAssignDetail[idx].PtramBhyt = 0;
                        arrAssignDetail[idx].PtramBhytGoc = 0;
                        arrAssignDetail[idx].BhytChitra = 0;
                        arrAssignDetail[idx].BnhanChitra =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    }
                    arrAssignDetail[idx].IpMaysua = globalVariables.gv_strIPAddress;
                    arrAssignDetail[idx].TenMaysua = globalVariables.gv_strComputerName;

                    arrAssignDetail[idx].IpMaytao = globalVariables.gv_strIPAddress;
                    arrAssignDetail[idx].TenMaytao = globalVariables.gv_strComputerName;
                    idx++;
                }
            }
            return arrAssignDetail;
        }

        /// <summary>
        /// hàm thực hiẹn eviệc thây đổi thông tin trên lưới
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_CHIDINH_CLS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) Utility.ShowMsg(Help);
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode == Keys.P)) cmdInPhieuCLS_Click(cmdInPhieuCLS, new EventArgs());
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.A && e.Control) cmdAddDetail.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtFilterName.Focus();
                txtFilterName.SelectAll();
            }
            if ((e.Control && e.KeyCode == Keys.F3) || e.KeyCode == Keys.F3)
            {
                txtNhomDichvuCLS.Focus();
                txtNhomDichvuCLS.SelectAll();
            }
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.Alt && e.KeyCode == Keys.M) grdServiceDetail.Select();
        }

        /// <summary>
        /// hàm thực hiện việc lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            FilterCLS();
        }

        private void txtFilterName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                Utility.focusCell(grdServiceDetail, DmucDichvucl.Columns.TenDichvu);
            }
        }
       
        private void FilterCLS()
        {
            try
            {
                m_blnAllowSelectionChanged = false;
                
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtFilterName.Text))
                {
                    string _value = Utility.DoTrim(txtFilterName.Text.ToUpper());
                    int rowcount = 0;
                    rowcount =
                        m_dtServiceDetail.Select(DmucDichvucl.Columns.MaDichvu + " ='" + _value +
                                                 "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = DmucDichvucl.Columns.MaDichvu + " ='" + _value + "'";
                    }
                    else
                    {
                        rowFilter = DmucDichvuclsChitiet.Columns.TenChitietdichvu + " like '%" + _value +
                                    "%'  OR  " + DmucDichvuclsChitiet.Columns.MaChitietdichvu + " like '" + _value +
                                    "%'";

                    }
                }
                m_dtServiceDetail.DefaultView.RowFilter = "1=1";
                m_dtServiceDetail.DefaultView.RowFilter = rowFilter;
                m_dtServiceDetail.AcceptChanges();
                grdServiceDetail.Refresh();
                Application.DoEvents();
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=2";
            }
            finally
            {
                m_blnAllowSelectionChanged = true;
            }
        }

        /// <summary>
        /// hamf thuc hien viec them thong tin cua can lam sang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            resetNewItem();
            AddDetail();
            uncheckItems();
        }
        void resetNewItem()
        {
            if (m_dtAssignDetail == null || !m_dtAssignDetail.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtAssignDetail.Rows)
                dr["isnew"] = 0;
            m_dtAssignDetail.AcceptChanges();
        }
        void SetSaveStatus()
        {
            if (m_dtAssignDetail == null || !m_dtAssignDetail.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtAssignDetail.Rows)
                dr["NoSave"] = 0;
            m_dtAssignDetail.AcceptChanges();
        }
        private void AddDetail()
        {
            try
            {
                
                isSaved = false;
                bool selectnew = false;
                GridEXRow[] ArrCheckList = grdServiceDetail.GetCheckedRows();
                foreach (GridEXRow gridExRow in ArrCheckList)
                {
                    Int32 ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    EnumerableRowCollection<DataRow> query = from loz in m_dtAssignDetail.AsEnumerable().Cast<DataRow>()
                                                             where
                                                                 Utility.Int32Dbnull(
                                                                     loz[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) ==
                                                                 ServiceDetail_Id
                                                             select loz;
                    if (query.Count() <= 0)
                    {
                        DataRow newDr = m_dtAssignDetail.NewRow();
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;

                        newDr["stt_hthi_dichvu"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                        newDr["stt_hthi_chitiet"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);


                        newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
                        newDr[KcbChidinhclsChitiet.Columns.IdDichvu] =
                            Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] =
                            Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.PtramBhyt] = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                        //Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PtramBhyt].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.DonGia] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.GiaDanhmuc] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.LoaiChietkhau] =
                            Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.LoaiChietkhau].Value);
                        newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                        newDr["IsNew"] = 1;
                        newDr["NoSave"] = 1;
                        newDr["IsLocked"] = 0;
                        newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                        newDr[KcbChidinhclsChitiet.Columns.HienthiBaocao] = 1;
                        newDr[KcbChidinhclsChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                        newDr[KcbChidinhclsChitiet.Columns.TuTuc] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value);
                        newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.MadoituongGia] = Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.PhuThu] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);

                        //Không tự túc thì tính theo giá BHYT nếu là BN BHYT
                        if (Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0) == 0)
                        {
                            decimal BHCT = 0m;
                            if (objLuotkham.DungTuyen == 1)
                            {
                                BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            }
                            else
                            {
                                if (objLuotkham.TrangthaiNoitru <= 0)
                                    BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                    BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                            }
                            decimal BNCT =
                                Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) -
                                BHCT;
                            newDr[KcbChidinhclsChitiet.Columns.BhytChitra] = BHCT;
                            newDr[KcbChidinhclsChitiet.Columns.BnhanChitra] = BNCT;
                        }
                        else//Tự túc
                        {
                            newDr[KcbChidinhclsChitiet.Columns.BhytChitra] = 0;
                            newDr[KcbChidinhclsChitiet.Columns.BnhanChitra] =
                                Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                        }
                        newDr["TT_BHYT"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.BhytChitra], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        newDr["TT_BN"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) + Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        newDr["TT"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.DonGia], 0) + Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        newDr["TT_PHUTHU"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        newDr["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                        newDr["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);

                        newDr[KcbChidinhclsChitiet.Columns.NguoiTao] = globalVariables.UserName;
                        newDr[KcbChidinhclsChitiet.Columns.NgayTao] = globalVariables.SysDate;
                        m_dtAssignDetail.Rows.Add(newDr);
                        if (!selectnew)
                        {
                            Utility.GonewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "0"));
                            selectnew = true;
                        }
                    }
                }

                m_dtAssignDetail.AcceptChanges();
                //UpdateDataWhenChanged();
                m_dtServiceDetail.AcceptChanges();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //AutoWarning();
            }
        }
        void AutoWarning()
        {
            try
            {
                string Canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham);
               Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], Canhbaotamung, true);

            }
            catch (Exception ex)
            {
                
            }
        }
        private void ModifyButtonCommand()
        {
            cmdDelete.Enabled = grdAssignDetail.GetCheckedRows().Length > 0;
            cmdSave.Enabled = grdAssignDetail.RowCount > 0;
        }

        private void cmdSearchKhoa_Click(object sender, EventArgs e)
        {
            //frm_YHHQ_KHOANOITRU frm = new frm_YHHQ_KHOANOITRU();
            //frm.ShowDialog();
            //if (frm.m_blnCancel)
            //{
            //    cboKhoaNoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoitru, frm.Department_ID.ToString());
            //}
        }

        private void uncheckItems()
        {
            if (grdServiceDetail.RowCount <= 0) return;
            try
            {
                foreach (GridEXRow _item in grdServiceDetail.GetCheckedRows())
                {
                    _item.IsChecked = false;
                }
            }
            catch
            {
            }
        }

        private void grdServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdServiceDetail.Focused)
                if (e.KeyCode == Keys.Enter)
                {
                    cmdAddDetail.PerformClick();
                    txtFilterName.SelectAll();
                    txtFilterName.Focus();
                }
                else if (e.KeyCode == Keys.Space && Utility.sDbnull(grdServiceDetail.CurrentColumn.Key, "") != "colCHON")
                {
                    grdServiceDetail.CurrentRow.IsChecked = !grdServiceDetail.CurrentRow.IsChecked;
                    if (chkChiDinhNhanh.Checked)
                    {
                        if (grdServiceDetail.CurrentRow.IsChecked)
                        {
                            AddOneRow_ServiceDetail();
                            //txtFilterName.SelectAll();
                            //txtFilterName.Focus();
                        }
                    }
                }
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra số lượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KcbChidinhclsChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    int quanlity = Utility.Int32Dbnull(e.InitialValue, 1);
                    int quanlitynew = Utility.Int32Dbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                    GridEXRow _row = grdAssignDetail.CurrentRow;

                    _row.Cells["TT_BHYT"].Value = (Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BhytChitra].Value, 0)) * quanlitynew;
                    _row.Cells["TT_BN"].Value = (Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) + Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    _row.Cells["TT"].Value = (Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) + Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    _row.Cells["TT_PHUTHU"].Value = (Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0)) * quanlitynew;
                    _row.Cells["TT_KHONG_PHUTHU"].Value = Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * quanlitynew;
                    _row.Cells["TT_BN_KHONG_PHUTHU"].Value = Utility.Int32Dbnull(_row.Cells[KcbChidinhclsChitiet.Columns.BnhanChitra].Value, 0) * quanlitynew;
                    grdAssignDetail.UpdateData();

                }
            }
            catch (Exception exception)
            {
            }
            ModifyButtonCommand();
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC CHO PHÉ THƯC HIỆN CIỆC CHỌN GÓI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTheoDoiTuong_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// HÀM THƯC HIỆN VIỆC LOAD NHỮNG DANH MỤC CLS TRONG GÓI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTrongGoi_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        private void rad_TuTuc_CheckedChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// hàm thưc hiện việc nhóm cận lâm sàng thong tin 
        /// khi chọn danh sách cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhom_CLS_Click(object sender, EventArgs e)
        {
            try
            {
                //var frm = new frm_Nhom_DVuCLS();
                //frm.MaDoiTuong = Utility.sDbnull(objLuotkham.MaDoiTuong);
                //frm.ShowDialog();
                //if (frm.m_blnCancel)
                //{
                //    GridEXRow[] gridExRows = frm.gridExRows;
                //    foreach (GridEXRow gridExRow in gridExRows)
                //    {
                //        int ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.IdDvu].Value);
                //        EnumerableRowCollection<DataRow> query =
                //            from nhom in m_dtAssignDetail.AsEnumerable().Cast<DataRow>()
                //            where
                //                Utility.Int32Dbnull(nhom[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) == ServiceDetail_Id
                //            select nhom;

                //        if (query.Count() <= 0)
                //        {
                //            AddNhomCLS(gridExRow);
                //        }
                //    }
                //}
                //ModifyCommand();
                //ModifyButtonCommand();
            }
            catch (Exception exception)
            {
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiệnv iệc add nhóm cls vào cls đang dùng
        /// </summary>
        /// <param name="gridExRow"></param>
        private void AddNhomCLS(GridEXRow gridExRow)
        {
            //DataRow newDr = m_dtAssignDetail.NewRow();
            //newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;

            //newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
            //newDr[KcbChidinhclsChitiet.Columns.IdDichvu] =
            //    Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.IdLoaiDvu].Value, -1);
            //ServiceDetail_Id = Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.IdDvu].Value, -1);
            //newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] = ServiceDetail_Id;
            ////newDr[KcbChidinhclsChitiet.Columns.OriginPrice] = Utility.DecimaltoDbnull(gridExRow.Cells["Price"].Value, 0);
            //newDr[KcbChidinhclsChitiet.Columns.DiscountType] = 1;
            //newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells["TEN_DVU"].Value, "");
            //newDr["IsNew"] = 1;
            //newDr["IsLocked"] = 0;
            //newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.ObjectTypeId;
            //newDr[KcbChidinhclsChitiet.Columns.DisplayOnReport] = 1;
            //newDr[KcbChidinhclsChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[LNhomDvuCl.Columns.SoLuong].Value, 1);
            //newDr[KcbChidinhclsChitiet.Columns.TuTuc] = 0;
            //newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells["TEN_LOAI_DVU"].Value, "");
            //newDr[KcbChidinhclsChitiet.Columns.IdBacsiChidinh] = globalVariables.gv_StaffID;
            //IEnumerable<GridEXRow> query = from dichvu in grdServiceDetail.GetDataRows().AsEnumerable()
            //                               where
            //                                   Utility.Int32Dbnull(
            //                                       dichvu.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value) ==
            //                                   ServiceDetail_Id
            //                               select dichvu;
            //if (query.Count() > 0)
            //{
            //    GridEXRow exRow = query.FirstOrDefault();
            //    newDr[KcbChidinhclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(exRow.Cells[.IdDichvu].Value, 0);
            //    newDr[KcbChidinhclsChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(exRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
            //    newDr[KcbChidinhclsChitiet.Columns.OriginPrice] = Utility.DecimaltoDbnull(exRow.Cells["Price"].Value, 0);

            //    newDr[KcbChidinhclsChitiet.Columns.IdGoiDvu] = Utility.Int32Dbnull(exRow.Cells["ID_GOI_DVU"].Value, -1);
            //    newDr[KcbChidinhclsChitiet.Columns.TrongGoi] = Utility.Int32Dbnull(exRow.Cells["TRONG_GOI"].Value, 0);
            //}
            //else
            //{
            //    SqlQuery sqlQuery = new Select().From(LObjectTypeService.Schema)
            //        .Where(LObjectTypeService.Columns.MaDtuong).IsEqualTo(objLuotkham.MaDoiTuong)
            //        .And(LNhomDvuCl.Columns.IdDvu).IsEqualTo(ServiceDetail_Id);
            //    var objectTypeService = sqlQuery.ExecuteSingle<LObjectTypeService>();
            //    if (objectTypeService != null)
            //    {
            //        newDr[KcbChidinhclsChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(objectTypeService.Surcharge, 0);
            //        newDr[KcbChidinhclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(objectTypeService.Surcharge, 0);
            //        newDr[KcbChidinhclsChitiet.Columns.IdGoiDvu] = -1;
            //        newDr[KcbChidinhclsChitiet.Columns.TrongGoi] = 0;
            //    }
            //}

            ////  newDr["TT"] = Utility.DecimaltoDbnull(drv[KcbChidinhclsChitiet.Columns.PhuThu], 0) + Utility.DecimaltoDbnull(drv["Price"], 0);

            //m_dtAssignDetail.Rows.Add(newDr);
        }

        private void grdServiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnAllowSelectionChanged) return;
            if (!Utility.isValidGrid(grdServiceDetail)) return;
            if (!grdServiceDetail.Focused && grdServiceDetail.CurrentColumn == null)
            {
                Utility.focusCell(grdServiceDetail, DmucDichvucl.Columns.TenDichvu);
            }
        }

        private void clearGrid(int i)
        {
            grdServiceDetail.MoveFirst();
        }

        private void grdServiceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    neverFound = false;
                    if (grdServiceDetail.CurrentRow == null) grdServiceDetail.MoveFirst();
                    int oldIdex = grdServiceDetail.CurrentRow.Position;
                    CurrentRowIndex = grdServiceDetail.CurrentRow.Position;
                    bool hasFound = false;
                    bool lastIdx = false;
                    string _Keyvalue = e.KeyChar.ToString().ToUpper();
                    object value;
                    if (CurrentRowIndex + 1 > grdServiceDetail.RowCount - 1)
                    {
                        grdServiceDetail.MoveFirst();
                        grdServiceDetail.Focus();
                        for (int j = 0; j <= oldIdex; j++)
                        {
                            value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;
                            if (hasFound) break;
                            if (!hasFound && j <= grdServiceDetail.RowCount - 1)
                            {
                                grdServiceDetail.MoveNext();
                                grdServiceDetail.Focus();
                            }
                        }
                    }
                    else
                    {
                        for (int i = CurrentRowIndex + 1; i <= grdServiceDetail.RowCount - 1; i++)
                        {
                            grdServiceDetail.MoveNext();
                            grdServiceDetail.Focus();
                            value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;

                            if (hasFound) break;
                        }
                        if (!hasFound && oldIdex > 0)
                        {
                            grdServiceDetail.MoveFirst();
                            grdServiceDetail.Focus();

                            for (int j = 0; j <= oldIdex; j++)
                            {
                                value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                                if (value != null &&
                                    value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                    _Keyvalue)
                                    hasFound = true;
                                if (hasFound) break;
                                if (!hasFound && j <= grdServiceDetail.RowCount - 1)
                                {
                                    grdServiceDetail.MoveNext();
                                    grdServiceDetail.Focus();
                                }
                            }
                        }
                    }
                    if (!hasFound) grdServiceDetail.MoveToRowIndex(oldIdex - 1);
                    CurrentRowIndex = grdServiceDetail.CurrentRow.Position;
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }

        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }

        private void cmdNhom_CLS_Click_1(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của dịch vụ khi lọc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            try
            {
                if (cboDichVu.SelectedIndex > 0)
                {
                    _rowFilter = string.Format("{0}={1}", DmucDichvuclsChitiet.Columns.IdDichvu,
                                               Utility.Int32Dbnull(cboDichVu.SelectedValue, -1));
                }
            }

            catch (Exception)
            {
                _rowFilter = "1=1";
            }
            m_dtServiceDetail.DefaultView.RowFilter = _rowFilter;
            m_dtServiceDetail.AcceptChanges();
            
            if (grdServiceDetail.RowCount > 0)
            {
                grdServiceDetail.Focus();
                grdServiceDetail.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdServiceDetail.RootTable.Columns[DmucDichvucl.Columns.TenDichvu];
                grdServiceDetail.Col = gridExColumn.Position;
            }
        }


        private void grdServiceDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (chkChiDinhNhanh.Checked)
            {
                if (grdServiceDetail.CurrentRow.IsChecked)
                {
                    AddOneRow_ServiceDetail();
                }
            }
        }

        private void AddOneRow_ServiceDetail()
        {
            try
            {
                GridEXRow gridExRow = grdServiceDetail.CurrentRow;
                resetNewItem();
                Int32 IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                EnumerableRowCollection<DataRow> query = from loz in m_dtAssignDetail.AsEnumerable().Cast<DataRow>()
                                                         where
                                                             Utility.Int32Dbnull(
                                                                 loz[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) ==
                                                             IdChitietdichvu
                                                         select loz;
                if (query.Count() <= 0)
                {
                    DataRow newDr = m_dtAssignDetail.NewRow();
                    newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;
                    newDr["stt_hthi_dichvu"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                    newDr["stt_hthi_chitiet"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                    newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
                    newDr[KcbChidinhclsChitiet.Columns.IdDichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                    newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    newDr[KcbChidinhclsChitiet.Columns.PtramBhyt] = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    //Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PtramBhyt].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.DonGia] =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.GiaDanhmuc] = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.GiaDanhmuc].Value, 0);
                    newDr[KcbChidinhclsChitiet.Columns.LoaiChietkhau] =
                        Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.LoaiChietkhau].Value);
                    newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                    newDr["IsNew"] = 1;
                    newDr["IsLocked"] = 0;
                    newDr["NoSave"] = 1;
                    newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = objLuotkham.IdDoituongKcb;
                    newDr[KcbChidinhclsChitiet.Columns.HienthiBaocao] = 1;
                    newDr[KcbChidinhclsChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                    newDr[KcbChidinhclsChitiet.Columns.TuTuc] = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value);
                    newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                    newDr[KcbChidinhclsChitiet.Columns.PhuThu] =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                    //Không tự túc thì tính theo giá BHYT nếu là BN BHYT
                    if (Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0) == 0)
                    {
                        decimal BHCT = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                BHCT = Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                        }
                        decimal BNCT =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) -
                            BHCT;
                        newDr[KcbChidinhclsChitiet.Columns.BhytChitra] = BHCT;
                        newDr[KcbChidinhclsChitiet.Columns.BnhanChitra] = BNCT;
                    }
                    else//Tự túc
                    {
                        newDr[KcbChidinhclsChitiet.Columns.BhytChitra] = 0;
                        newDr[KcbChidinhclsChitiet.Columns.BnhanChitra] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    }
                    newDr["TT_BHYT"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.BhytChitra], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    newDr["TT_BN"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) + Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    newDr["TT"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.DonGia], 0) + Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    newDr["TT_PHUTHU"] = (Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.PhuThu], 0)) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    newDr["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.DonGia], 0) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    newDr["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.BnhanChitra], 0) * Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.SoLuong], 0);
                    m_dtAssignDetail.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "0"));
                    m_dtServiceDetail.AcceptChanges();
                }
            }
            catch
            {
            }
            finally
            {
                ModifyButtonCommand();
            }
        }

        private void chkChiDinhNhanh_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void grdServiceDetail_Enter(object sender, EventArgs e)
        {
           // grdServiceDetail.Col = 0;
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS_Selected()) return;

                long AssignDetail =
                    Utility.Int64Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);

                CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail);
                grdAssignDetail.CurrentRow.Delete();
                grdAssignDetail.UpdateData();
                grdAssignDetail.Refresh();
                m_dtAssignDetail.AcceptChanges();


                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc cấu hình của cls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._HISCLSProperties);
            frm.ShowDialog();
            CauHinh();
            //InitData();
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._MayInProperties.InCLSsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuCLS_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");

                }
                KCB_INPHIEU.InphieuChidinhCLS((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId, v_AssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked, ref mayin);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void grdServiceDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KcbChidinhclsChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    int quanlity = Utility.Int32Dbnull(e.InitialValue, 1);
                    int quanlitynew = Utility.Int32Dbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }



        #region "Event Common"

        private int ID_Goi_Dvu = -1;

        /// <summary>
        /// hàm thực hiện việc lấy thông tin 
        /// </summary>
        private void GetData()
        {
            KcbChidinhcl objKcbChidinhcls = KcbChidinhcl.FetchByID(Utility.Int32Dbnull(txtAssign_ID.Text, -1));
            if (objKcbChidinhcls != null)
            {
                txtAssignCode.Text = objKcbChidinhcls.MaChidinh;
                dtRegDate.Value = objKcbChidinhcls.NgayTao.Value;
                cbobacSyChiDinh.SelectedIndex = Utility.GetSelectedIndex(cbobacSyChiDinh, objKcbChidinhcls.IdBacsiChidinh.ToString());
            }
            else
            {
                if (objPhieudieutriNoitru != null)
                    dtRegDate.Value = objPhieudieutriNoitru.NgayDieutri.Value;
                else
                    dtRegDate.Value = globalVariables.SysDate;
                txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                barcode1.Data = Utility.sDbnull(txtAssignCode.Text);
            }
            if (objLuotkham != null)
            {
                if (objBenhnhan != null)
                {
                    this.Text = "Chỉ định dịch vụ Cận lâm sàng cho Bệnh nhân:" + objBenhnhan.TenBenhnhan
                        + ", " + (Utility.Int32Dbnull(objBenhnhan.GioiTinh) == 0 ? "Nam" : "Nữ") + ", " + (globalVariables.SysDate.Year + 1 - objBenhnhan.NamSinh.Value) + " tuổi";

                }
            }
            LayThongTin_Chitiet_CLS();
        }

        /// <summary>
        /// ham thc hien viecj lay thông tin chi tiết của cls
        /// </summary>
        private void LayThongTin_Chitiet_CLS()
        {
            try
            {
                m_dtAssignDetail = CHIDINH_CANLAMSANG.LaythongtinCLS_Thuoc(Utility.Int32Dbnull(txtAssign_ID.Text, -1), "DICHVU");
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "1=1",
                                                   "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                grdAssignDetail.MoveFirst();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu CLS chi tiết\n" + ex.Message);
            }
        }
        /// <summary>
        /// Đã được xử lý phía Client
        /// </summary>
        private void ProcessData()
        {
            //Utility.AddColumToDataTable(ref m_dtServiceDetail, KcbChidinhclsChitiet.Columns.TuTuc, typeof(int));
            //foreach (DataRow dr in m_dtServiceDetail.Rows)
            //{
            //    if (objLuotkham.MaDoituongKcb == "BHYT" && objLuotkham.DungTuyen == 0)
            //    {
            //        dr[KcbChidinhclsChitiet.Columns.PhuThu] = dr["Phuthu_traituyen"];
            //    }
            //}
            //m_dtServiceDetail.AcceptChanges();
        }

        /// <summary>
        /// khởi tạo thông tin của dữ liệu
        /// </summary>
        private void InitData()
        {
            try
            {
                string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
                if (Utility.Int32Dbnull(objLuotkham.Noitru, 0) <= 0)
                {
                    if (ObjRegExam != null)
                    {
                        if (ObjRegExam.KhamNgoaigio == 1)
                            MA_KHOA_THIEN = "KYC";
                    }
                    else
                    {
                        if (THU_VIEN_CHUNG.IsNgoaiGio())
                        {
                            MA_KHOA_THIEN = "KYC";
                        }
                    }
                }
                else
                {
                    MA_KHOA_THIEN = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_GIACLS", false) ?? MA_KHOA_THIEN;
                }

                m_dtServiceDetail = CHIDINH_CANLAMSANG.LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb, objLuotkham.TrangthaiNoitru,Utility.ByteDbnull( objLuotkham.GiayBhyt,0), - 1, Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0), MA_KHOA_THIEN, nhomchidinh);
                //Xử lý phụ thu đúng tuyến-trái tuyến
                ProcessData();


                if (!m_dtServiceDetail.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
                    m_dtServiceDetail.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof(int));
                if (!m_dtServiceDetail.Columns.Contains("ten_donvitinh"))
                    m_dtServiceDetail.Columns.Add("ten_donvitinh", typeof(string));
                m_dtServiceDetail.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, m_dtServiceDetail, false, true, "", "");
                GridEXColumn gridExColumnGroupIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_dichvu"];
                GridEXColumn gridExColumnIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_chitiet"];
                Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnGroupIntOrder, SortOrder.Ascending);
                Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnIntOrder, SortOrder.Ascending);
                
                txtFilterName.Focus();
                txtFilterName.SelectAll();

            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình load thông tin :" + exception);
            }
        }
        #endregion
    }
}