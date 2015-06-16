using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using SubSonic;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_thuoc : Form
    {
        #region "Public Variables(Class Level)
        public bool m_blnCancel = true;
        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về đối tượng khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public DmucThuoc objThuoc;
        public Janus.Windows.GridEX.GridEX grdList;
        public DataTable m_dtDrugDataSource;
        public DataTable m_dtSameCodeDataSource = new DataTable();
        public DataTable m_dtObjectDataSource = new DataTable();
        public int Drug_ID = -1;
        /// <summary>
        /// Thao tác đang thực hiện là gì: Insert, Delete, Update hay Select...
        /// </summary>
        public action m_enAction;
        #endregion

        #region "Private Variables(Class Level)"

        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";

        private int m_intOldMaxDrugID = 0;

        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool m_blnLoaded = false;
        private SubSonic.Query m_Query;
        private SubSonic.QueryCommand m_QueryCmd = null;
        #endregion

        //Các phương thức khởi tạo của Class

        #region "Constructors"
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của thuốc
        /// </summary>
        /// <param name="p_strDrugOnly"></param>
        public frm_themmoi_thuoc(string p_strDrugOnly)
        {
            InitializeComponent();
            
            InitializeEvents();
            m_Query = DmucThuoc.CreateQuery();
            m_QueryCmd = m_Query.BuildSelectCommand();
            //Khởi tạo các giá trị mặc định. Có thể đặt ở Form load và có thể sử dụng Thread để Load trong 
            //một số trường hợp cần cải thiện Performance
            if (p_strDrugOnly.ToUpper() == "DRUGONLY")
            {

            }
            txtCode.Enabled = false;
            cboDrugNature.SelectedIndex = 0;

            cboKIEU_THUOC_VT.SelectedIndex = 0;


        }



        #endregion

        //Vùng này chứa các thuộc tính để thao tác với các đối tượng khác 
        //Hiện tại ko dùng

        #region "Public Properties"
        #endregion

        #region "Private Methods"

        #region "Private Methods including Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        private void InitializeEvents()
        {
             txtDongia.KeyPress += new KeyPressEventHandler( txtDongia_KeyPress);
            this.KeyDown += new KeyEventHandler(frm_themmoi_thuoc_KeyDown);
            txtName.LostFocus += new EventHandler(txtName_LostFocus);
            txtActice.LostFocus += new EventHandler(txtActice_LostFocus);
            txtLoaithuoc._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtLoaithuoc__OnEnterMe);
            cboKIEU_THUOC_VT.SelectedIndexChanged += new EventHandler(cboKIEU_THUOC_VT_SelectedIndexChanged);
            txtDonvitinh._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtDonvitinh__OnShowData);
            txtDonvichia._OnShowData += txtDonvichia__OnShowData;
            chkChiathuoc.CheckedChanged += chkChiathuoc_CheckedChanged;
            
        }

        void chkChiathuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtDonvichia.Enabled = txtDonvichia.Enabled = txtSoluongchia.Enabled = chkChiathuoc.Checked;
        }

        void txtDonvichia__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDonvitinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);

                oldCode = txtDonvichia.myCode;
                txtDonvichia.Init();
                txtDonvichia.SetCode(oldCode);

                txtDonvichia.Focus();
            }   
        }

       

        void txtDonvitinh__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDonvitinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);

                oldCode = txtDonvichia.myCode;
                txtDonvichia.Init();
                txtDonvichia.SetCode(oldCode);

                txtDonvitinh.Focus();
            } 
        }

        void cmdThemdonvitinh_Click(object sender, EventArgs e)
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG("DONVITINH");
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);
                txtDonvitinh.Focus();
            }
        }

        void cboKIEU_THUOC_VT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Utility.Int32Dbnull(txtLoaithuoc.MyID, -1);
            AutocompleteLoaithuoc();
            txtLoaithuoc.SetId(id);
        }
        void txtLoaithuoc__OnEnterMe()
        {

        }
        void frm_themmoi_thuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdClose.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }


        /// <summary>
        /// Load các danh mục chủng loại thuốc và đơn vị tính vào ComboBox
        /// </summary>
        private void BindingDataSource()
        {


        }
        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (String.IsNullOrEmpty(txtID.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập mã thuốc.",true);
                txtCode.Focus();
                return false;
            }
            if (!globalVariables.IsAdmin)
            {
                if (String.IsNullOrEmpty(txtCode.Text))
                {
                    Utility.SetMsg(lblMsg, "Mã thuốc không được để trống và phải là duy nhất trong hệ thống",true);
                    txtCode.Focus();
                    return false;
                }
            }

            if (txtLoaithuoc.MyID == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập thông tin loại thuốc",true);
                txtLoaithuoc.Focus();
                txtLoaithuoc.SelectAll();
                return false;
            }
            if (String.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Tên thuốc không được để trống.",true);
                txtName.Focus();
                return false;
            }
            if (txtDonvitinh.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập đơn vị tính", true);
                txtDonvitinh.Focus();
                txtDonvitinh.SelectAll();
                return false;
            }
            if (String.IsNullOrEmpty( txtDongia.Text))
            {
                Utility.SetMsg(lblMsg, "Đơn giá không được để trống.",true);
                 txtDongia.Focus();
                return false;
            }
            if (chkChiathuoc.Checked)
            {
                if (Utility.DecimaltoDbnull(txtSoluongchia.Text,0)<=0)
                {
                    Utility.SetMsg(lblMsg, "Số lượng chia phải >0.", true);
                    txtSoluongchia.Focus();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtDongiachia.Text, 0) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Đơn giá chia phải >0.", true);
                    txtDongiachia.Focus();
                    return false;
                }
                if (txtDonvichia.myCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập đơn vị chia thuốc", true);
                    txtDonvichia.Focus();
                    return false;
                }
            }
            

            //if (BusinessHelper.GetAccountName() == "DETMAY")
            //{


            //    if (string.IsNullOrEmpty(txtNumber_Register.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số đăng ký của thuốc");
            //        txtNumber_Register.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtNuocSX.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số nhà sản xuất của thuốc");
            //        txtNuocSX.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtHangSX.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số nước sản xuất của thuốc");
            //        txtHangSX.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(txtListBHYT.Text))
            //    {
            //        Utility.ShowMsg("Bạn phải nhập số  danh mục BHYT của thuốc");
            //        txtListBHYT.Focus();
            //        return false;
            //    }
            //}
            return true;
        }
        private void CreateDefaultTableStructure()
        {

        }
        /// <summary>
        /// Lấy về danh mục thuốc tương đương 
        /// </summary>
        private void GetSameCodeDrugList()
        {

        }
        /// <summary>
        /// Lấy về danh mục đối tượng thuốc
        /// </summary>
        private void GetObjectList()
        {

        }
        /// <summary>
        /// Điền dữ liệu của đối tượng cần cập nhật vào các Controls trên Form
        /// </summary>
        private void FillDataIntoControlWhenUpdate()
        {
            try
            {
                objThuoc = DmucThuoc.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
                if (objThuoc == null)
                {
                    Utility.ShowMsg("Lỗi không xác định được đối tượng cần cập nhật. Liên hệ với ViSoft để được giải đáp.");
                    return;
                }

                if (objThuoc != null)
                {
                    Drug_ID = objThuoc.IdThuoc;
                    txtID.Text = Utility.sDbnull(objThuoc.IdThuoc).ToString().Trim();
                    txtActice.Text = Utility.sDbnull(objThuoc.HoatChat).ToString().Trim();
                    txtCode.Text = Utility.sDbnull(objThuoc.MaThuoc);
                     txtDongia.Text = Utility.sDbnull(objThuoc.DonGia);
                     txtGiaBHYT.Text = Utility.sDbnull(objThuoc.GiaBhyt);
                     txtPTDT.Text = Utility.sDbnull(objThuoc.PhuthuDungtuyen);
                     txtPTTT.Text = Utility.sDbnull(objThuoc.PhuthuTraituyen);
                    txtName.Text = Utility.sDbnull(objThuoc.TenThuoc);
                    txtDesc.Text = Utility.sDbnull(objThuoc.MotaThem);
                    txtLoaithuoc.SetId(Utility.sDbnull(objThuoc.IdLoaithuoc));
                    txtDonvitinh.SetCode(objThuoc.MaDonvitinh);
                    cboDrugNature.SelectedIndex = Convert.ToInt32(objThuoc.TinhChat);
                    txtNumber_Register.Text = Utility.sDbnull(objThuoc.SoDangky);
                    txtNuocSX.Text = Utility.sDbnull(objThuoc.NuocSanxuat);
                    txtHangSX.Text = Utility.sDbnull(objThuoc.HangSanxuat);
                    txtContent.Text = Utility.sDbnull(objThuoc.HamLuong);
                    txtQD31.Text = objThuoc.QD31;
                    chkTutuc.Checked = Utility.Byte2Bool(objThuoc.TuTuc.Value);
                    txtDangBaoChe._Text= Utility.sDbnull(objThuoc.DangBaoche, "");
                    txtTEN_BHYT.Text = Utility.sDbnull(objThuoc.TenBhyt);
                    chkHieuLuc.Checked = Utility.Int32Dbnull(objThuoc.TrangThai, 0) == 1 ? true : false;
                    optAll.Checked = objThuoc.NoitruNgoaitru=="ALL";
                    optNgoai.Checked = objThuoc.NoitruNgoaitru == "NGOAI";
                    optNoitru.Checked = objThuoc.NoitruNgoaitru == "NOI";
                    txtSoluong.Text = Utility.sDbnull(objThuoc.GioihanKedon,"");
                    txtBut.Text = Utility.sDbnull(objThuoc.DonviBut, "");

                    txtSoluongchia.Text = Utility.DecimaltoDbnull(objThuoc.SluongChia, 0).ToString();
                    txtDongiachia.Text = Utility.DecimaltoDbnull(objThuoc.DongiaChia, 0).ToString();
                    txtDonvichia.SetCode(objThuoc.MaDvichia);
                    chkChiathuoc.Checked = Utility.Byte2Bool(objThuoc.CoChiathuoc);
                }
                GetSameCodeDrugList();
                GetObjectList();
            }
            catch
            {
            }
        }
        private DataTable m_dtQuanHe = new DataTable();
        /// <summary>
        /// Thiết lập trạng thái các Control trên Form theo thao tác nghiệp vụ cần thực hiện
        /// Insert, Update hoặc Delete...
        /// </summary>
        private void SetControlStatus()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    CreateDefaultTableStructure();
                    //Cho phép nhập liệu mã kho,vị trí, tên kho và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtCode);
                    txtLoaithuoc.Enabled = true;
                    txtDonvitinh.Enabled = true;
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledComboBox(cboDrugNature);
                    //  Utility.EnabledTextBox( txtDongia);
                    txtDongia.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    txtCode.Clear();
                    txtContent.Clear();
                    txtNumber_Register.Clear();
                    txtHangSX.Clear();
                    txtNuocSX.Clear();
                    chkHieuLuc.Checked = true;
                    txtName.Clear();
                    txtTEN_BHYT.Clear();
                    txtLoaithuoc.SetId(-1);
                    txtActice.Clear();
                    txtDangBaoChe.Clear();
                    txtDonvitinh.SetCode("-1");
                    txtDongia.Clear();
                    txtGiaBHYT.Clear();
                    txtPTDT.Clear();
                    txtPTTT.Clear();
                    optAll.Checked = true;
                    chkTutuc.Checked = false;
                    txtDesc.Clear();
                    //--------------------------------------------------------------
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Tự động Focus đến mục Code để người dùng nhập liệu
                    txtID.Text = "Tự sinh";
                    txtCode.Text = "";// BusinessHelper.GenerateDrugCode();
                    txtName.Focus();
                    //txtCode.Text = BusinessHelper.MaThuoc();
                    break;
                case action.Update:
                    //Cho phép nhập liệu mã kho,vị trí, tên kho và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtCode);
                    txtLoaithuoc.Enabled = true;
                    txtDonvitinh.Enabled = true;
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledComboBox(cboDrugNature);
                    //Utility.EnabledTextBox( txtDongia);
                     txtDongia.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    FillDataIntoControlWhenUpdate();
                    //--------------------------------------------------------------
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Tự động Focus đến mục Code để người dùng nhập liệu
                    txtName.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtCode);
                    txtLoaithuoc.Enabled = true;
                    txtDonvitinh.Enabled = true;
                    Utility.EnabledComboBox(cboDrugNature);
                    Utility.EnabledTextBox(txtName);
                    // Utility.EnabledTextBox( txtDongia);
                     txtDongia.Enabled = true;
                    Utility.EnabledTextBox(txtDesc);
                    txtLoaithuoc.SetId("-1");
                    txtDonvitinh.SetCode("-1");
                    txtID.Text = "Tự sinh";
                    txtCode.Clear();
                    txtCode.Text = "";// BusinessHelper.GenerateDrugCode();
                    txtName.Clear();
                    txtDongia.Clear();
                    txtGiaBHYT.Clear();
                    txtPTDT.Clear();
                    txtPTTT.Clear();
                    chkTutuc.Checked = false;
                    txtDesc.Clear();
                    //Cho phép nhập mới liên tiếp
                    m_enAction = action.Insert;
                    m_dtObjectDataSource.Clear();
                    m_dtSameCodeDataSource.Clear();
                    //txtCode.Text = BusinessHelper.MaThuoc();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region "Insert, Delete, Update,Select,..."
        /// <summary>
        /// Thực hiện nghiệp vụ Insert dữ liệu
        /// </summary>
        private void PerformInsertAction()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema)
                    .Where(DmucThuoc.Columns.MaThuoc).IsEqualTo(txtCode.Text);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại thuốc có mã(Drug Code)=" + txtCode.Text.Trim() + ".Đề nghị bạn nhập mã khác",true);
                    m_Query = DmucThuoc.CreateQuery();
                    return;
                }
                //Bước 0: Thêm mới thuốc
                DmucThuoc objnewItems = new DmucThuoc();
                objnewItems.TenThuoc = Utility.sDbnull(txtName.Text);
                objnewItems.TenBhyt = Utility.sDbnull(txtTEN_BHYT.Text);
                objnewItems.MaThuoc = Utility.sDbnull(txtCode.Text);
                objnewItems.IdLoaithuoc = Utility.Int16Dbnull(txtLoaithuoc.MyID);
                objnewItems.DonGia = Utility.DecimaltoDbnull( txtDongia.Text, 0);
                objnewItems.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objnewItems.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objnewItems.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                objnewItems.MotaThem = Utility.sDbnull(txtDesc.Text);
                objnewItems.DangBaoche = Utility.DoTrim(txtDangBaoChe.Text);
                objnewItems.HamLuong = Utility.sDbnull(txtContent.Text);
                objnewItems.HangSanxuat = Utility.sDbnull(txtHangSX.Text);
                objnewItems.TrangThai = chkHieuLuc.Checked ? (byte)1 : (byte)0;
                objnewItems.TuTuc =Utility.Bool2byte( chkTutuc.Checked );
                objnewItems.NgayTao = DateTime.Now;
                objnewItems.QD31 = Utility.DoTrim(txtQD31.Text);
                objnewItems.NguoiTao = globalVariables.UserName;
                objnewItems.NuocSanxuat = Utility.sDbnull(txtNuocSX.Text);
                objnewItems.GioihanKedon = (Int16)Utility.DecimaltoDbnull(txtSoluong.Text,-1);
                objnewItems.DonviBut = (int)Utility.DecimaltoDbnull(txtBut.Text, -1);
                objnewItems.MaDonvitinh = txtDonvitinh.myCode;

                objnewItems.CoChiathuoc = Utility.Bool2byte(chkChiathuoc.Checked);
                objnewItems.MaDvichia = txtDonvichia.myCode;
                objnewItems.SluongChia =(int) Utility.DecimaltoDbnull(txtSoluongchia.Text, 0);
                objnewItems.DongiaChia = Utility.DecimaltoDbnull(txtDongiachia.Text, 0);

                objnewItems.TinhChat = Convert.ToByte(cboDrugNature.SelectedIndex);
                objnewItems.HoatChat = Utility.sDbnull(txtActice.Text);
                objnewItems.KieuThuocvattu = Utility.sDbnull(cboKIEU_THUOC_VT.SelectedValue);
                objnewItems.NoitruNgoaitru = optAll.Checked ? "ALL" : (optNoitru.Checked ? "NOI" : "NGOAI");
                objnewItems.IsNew = true;
                objnewItems.Save();
                int v_intNewDrugID = objnewItems.IdThuoc;
                txtID.Text = Utility.sDbnull(objnewItems.IdThuoc);
                DataRow dr = m_dtDrugDataSource.NewRow();
                Utility.FromObjectToDatarow(objnewItems, ref dr);
                dr[DmucThuoc.Columns.TinhChat] = Convert.ToByte(cboDrugNature.SelectedIndex);
                dr["ten_loaithuoc"] = txtLoaithuoc.Text;
                dr["ten_donvitinh"] = txtDonvitinh.Text;
                dr["ten_donvichia"] = txtDonvichia.Text;
                dr[DmucThuoc.Columns.NguoiTao] = globalVariables.UserName;
                dr[DmucThuoc.Columns.NgayTao] = DateTime.Now;
                m_dtDrugDataSource.Rows.Add(dr);
                m_dtDrugDataSource.AcceptChanges();
                m_enAction = action.Insert;
                //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                try
                {
                    Utility.GotoNewRowJanus(grdList, "id_thuoc", v_intNewDrugID.ToString().Trim());
                    if (chkThemlientuc.Checked)
                        SetControlStatus();
                    else
                        this.Close();
                }
                catch (Exception exception)
                {
                }
                Utility.SetMsg(lblMsg, "Thêm mới dữ liệu thành công!",false);
                SetControlStatus();
                txtCode.Focus();
            }
            catch
            {
            }
        }

        private void SaveSameCodeDrug(int p_intNewDrugID)
        {
            foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
            {
                QheThuoctuongduong.Insert(p_intNewDrugID, Convert.ToInt16(dr["id_thuoctuongduong"]));
            }
        }
        private void SaveDrug_ObjectType(int p_intNewDrugID)
        {
            foreach (DataRow dr in m_dtObjectDataSource.Rows)
            {

            }
        }
        /// <summary>
        /// Thực hiện nghiệp vụ Update dữ liệu
        /// </summary>
        private void PerformUpdateAction()
        {
            Utility.SetMsg(lblMsg, "", false);
            //Gọi Business cập nhật dữ liệu
            Int16 v_intUpdateDrugID = Convert.ToInt16(txtID.Text);
            SqlQuery sqlQuery = new Select().From(DmucThuoc.Schema)
                  .Where(DmucThuoc.Columns.MaThuoc).IsEqualTo(txtCode.Text)
                  .And(DmucThuoc.Columns.IdThuoc).IsNotEqualTo(v_intUpdateDrugID);

            //Kiểm tra nếu trùng Mã Drug Code thì bắt nhập mã khác
            //DmucThuocCollection v_arrSameCodeObject = new DmucThuocController().FetchByQuery(m_Query.AddWhere("Drug_Code", txtCode.Text.Trim().ToUpper()).AND("id_thuoc", Comparison.NotEquals, v_intUpdateDrugID));
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Đã tồn tại thuốc có mã =" + txtCode.Text.Trim() +
                                ".Đề nghị bạn nhập mã khác",true);
                m_Query = DmucThuoc.CreateQuery();
                return;
            }
            //Create Again to ignore Where Clause
            m_Query = DmucThuoc.CreateQuery();
            //Tạo giá trị mới cho đối tượng đang cần Update
            DmucThuoc objnewItems = DmucThuoc.FetchByID(objThuoc.IdThuoc);
            
            objnewItems.IdThuoc = v_intUpdateDrugID;
            objnewItems.TenThuoc = Utility.GetValue(txtName.Text, false);
            objnewItems.TenBhyt = Utility.GetValue(txtTEN_BHYT.Text, false);
            objnewItems.DonGia = Utility.DecimaltoDbnull( txtDongia.Text,0);
            objnewItems.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
            objnewItems.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            objnewItems.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
            objnewItems.MaThuoc = Utility.GetValue(txtCode.Text, false);
            objnewItems.IdLoaithuoc = Convert.ToInt16(txtLoaithuoc.MyID);
            objnewItems.TinhChat = Convert.ToByte(cboDrugNature.SelectedIndex);
            objnewItems.MaDonvitinh = txtDonvitinh.myCode;
            objnewItems.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
            objnewItems.MotaThem = Utility.GetValue(txtDesc.Text, false);
            objnewItems.HoatChat = Utility.GetValue(txtActice.Text, false);
            objnewItems.NuocSanxuat = txtNuocSX.Text;
            objnewItems.HangSanxuat = txtHangSX.Text;
            objnewItems.HamLuong = txtContent.Text;
            objnewItems.QD31 = Utility.DoTrim(txtQD31.Text);
            objnewItems.SoDangky = txtNumber_Register.Text;
            objnewItems.TrangThai = chkHieuLuc.Checked ? (byte)1 : (byte)0;
            objnewItems.DangBaoche = Utility.DoTrim(txtDangBaoChe.Text);
            objnewItems.NguoiSua = globalVariables.UserName;
            objnewItems.NgaySua = DateTime.Now;
            objnewItems.GioihanKedon = (Int16)Utility.DecimaltoDbnull(txtSoluong.Text, -1);
            objnewItems.DonviBut = (int)Utility.DecimaltoDbnull(txtBut.Text, -1);

            objnewItems.CoChiathuoc = Utility.Bool2byte(chkChiathuoc.Checked);
            objnewItems.MaDvichia = txtDonvichia.myCode;
            objnewItems.SluongChia = (int)Utility.DecimaltoDbnull(txtSoluongchia.Text, 0);
            objnewItems.DongiaChia = Utility.DecimaltoDbnull(txtDongiachia.Text, 0);

            objnewItems.NoitruNgoaitru=optAll.Checked?"ALL":(optNoitru.Checked?"NOI":"NGOAI");
            objnewItems.KieuThuocvattu = Utility.sDbnull(cboKIEU_THUOC_VT.SelectedValue);
            objnewItems.IsNew = false;
            objnewItems.MarkOld();
            objnewItems.Save();
            //Update to Datasource to reflect on DataGridView
            new Update(KcbThanhtoanChitiet.Schema)
             .Set(KcbThanhtoanChitiet.Columns.DonviTinh).EqualTo(Utility.sDbnull(txtDonvitinh.Text, "Lần"))
             .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(Utility.sDbnull(txtName.Text, ""))
             .Set(KcbThanhtoanChitiet.Columns.TenBhyt).EqualTo(Utility.sDbnull(txtTEN_BHYT.Text, ""))
             .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(3)
             .And(KcbThanhtoanChitiet.Columns.IdChitietdichvu).IsEqualTo(objThuoc.IdThuoc)
             .Execute();

            DataRow dr = Utility.FetchOnebyCondition(m_dtDrugDataSource, "id_thuoc=" + v_intUpdateDrugID.ToString().Trim());
            if (dr != null)
            {
                Utility.FromObjectToDatarow(objnewItems, ref dr);
                dr[DmucThuoc.Columns.TinhChat] = Convert.ToByte(cboDrugNature.SelectedIndex);
                dr["ten_loaithuoc"] = txtLoaithuoc.Text;
                dr["ten_donvitinh"] = txtDonvitinh.Text;
                dr["ten_donvichia"] = txtDonvichia.Text;


                dr[DmucThuoc.Columns.HamLuong] = txtContent.Text;
                dr[DmucThuoc.Columns.SoDangky] = txtNumber_Register.Text;
                dr[DmucThuoc.Columns.NuocSanxuat] = txtNuocSX.Text;
                dr[DmucThuoc.Columns.HangSanxuat] = txtHangSX.Text;

                m_dtDrugDataSource.AcceptChanges();
            }
            //Return to the InitialStatus
            m_enAction = action.FirstOrFinished;

            //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
            if (grdList != null) Utility.GotoNewRowJanus(grdList, "id_thuoc", txtID.Text.Trim());
            SetControlStatus();
            Utility.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.", false);
            this.Close();
        }

        public CallAction CallFrom = CallAction.FromMenu;
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                if (!IsValidData())
                {
                    return;
                }
                if (string.IsNullOrEmpty(txtTEN_BHYT.Text)) txtTEN_BHYT.Text = Utility.sDbnull(txtName.Text);
                switch (m_enAction)
                {
                    case action.Insert:
                        PerformInsertAction();
                        break;
                    case action.Update:
                        PerformUpdateAction();
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }

        }
        #endregion
        #endregion

        #region "Event Handlers: Form Events,GridView Events, Button Events"
        /// <summary>
        /// Sự kiện Load của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDrug_Load(object sender, EventArgs e)
        {
            cboKIEU_THUOC_VT.SelectedIndex = 0;
            AutocompleteLoaithuoc();
            txtDonvitinh.Init();
            txtDonvichia.Init();
            txtDangBaoChe.Init(globalVariables.gv_dtDangbaoche);
            SetControlStatus();
        }
        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc = null;
            try
            {
                dtLoaithuoc = new Select().From(DmucLoaithuoc.Schema)
     .Where(DmucLoaithuoc.KieuThuocvattuColumn).IsEqualTo(cboKIEU_THUOC_VT.SelectedValue.ToString())
     .ExecuteDataSet().Tables[0];
                if (dtLoaithuoc == null) return;
                if (!dtLoaithuoc.Columns.Contains("ShortCut"))
                    dtLoaithuoc.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtLoaithuoc.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucLoaithuoc.MaLoaithuocColumn.ColumnName].ToString().Trim();
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
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in dtLoaithuoc.AsEnumerable()
                            select p[DmucLoaithuoc.IdLoaithuocColumn.ColumnName].ToString() + "#" + p[DmucLoaithuoc.MaLoaithuocColumn.ColumnName].ToString()
                            + "@" + p[DmucLoaithuoc.TenLoaithuocColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtLoaithuoc.AutoCompleteList = source;
                this.txtLoaithuoc.TextAlign = HorizontalAlignment.Left;
                this.txtLoaithuoc.CaseSensitive = false;
                this.txtLoaithuoc.MinTypedCharacters = 1;

            }
        }
        private void cmdRemoveObject_Click(object sender, EventArgs e)
        {
            try
            {
                //Besure that you have selected a row on GridView to remove
                int ItemChecked = Utility.GetItemsChecked(m_dtObjectDataSource, "CHON");
                int TotalItemChecked = ItemChecked;//to Compare to ignore infinite loop
                if (ItemChecked <= 0) return;
            _Continue:
                foreach (DataRow dr in m_dtObjectDataSource.Rows)
                {
                    if (Convert.ToInt16(dr["CHON"]) == 1 || Convert.ToBoolean(dr["CHON"]) == true)
                    {
                        m_dtObjectDataSource.Rows.Remove(dr);
                        m_dtObjectDataSource.AcceptChanges();
                        ItemChecked -= 1;

                    }
                    if (ItemChecked > 0 && ItemChecked != TotalItemChecked) { TotalItemChecked = ItemChecked; goto _Continue; }
                }
            }
            catch
            {
            }
        }

        private void cmdRemoveSamecode_Click(object sender, EventArgs e)
        {
            try
            {
                //Besure that you have selected a row on GridView to remove
                int ItemChecked = Utility.GetItemsChecked(m_dtSameCodeDataSource, "CHON");
                int TotalItemChecked = ItemChecked;//to Compare to ignore infinite loop
                if (ItemChecked <= 0) return;
            _Continue:
                foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
                {
                    if (Convert.ToInt16(dr["CHON"]) == 1 || Convert.ToBoolean(dr["CHON"]) == true)
                    {
                        m_dtSameCodeDataSource.Rows.Remove(dr);
                        m_dtSameCodeDataSource.AcceptChanges();
                        ItemChecked -= 1;

                    }
                    if (ItemChecked > 0 && ItemChecked != TotalItemChecked) { TotalItemChecked = ItemChecked; goto _Continue; }
                }
            }
            catch
            {
            }
        }

        private void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                //Remove All object whether they are checked for removing or not
                if (Utility.AcceptQuestion("Bạn đã chắc chắn muốn xóa bỏ tất cả các đối tượng ứng với loại thuốc này không?", "Xác nhận", true))
                    m_dtObjectDataSource.Rows.Clear();
            }
            catch
            {
            }
        }
        private void cmdRemoveAllSameCode_Click(object sender, EventArgs e)
        {
            try
            {
                //Remove All object whether they are checked for removing or not
                if (Utility.AcceptQuestion("Bạn đã chắc chắn muốn xóa bỏ tất cả các thuốc tương đương ứng với loại thuốc này không?", "Xác nhận", true))
                    m_dtSameCodeDataSource.Rows.Clear();
            }
            catch
            {
            }
        }

        private void chkCheckAllorNone_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtObjectDataSource.Rows)
            {
                // dr["CHON"] = chkCheckAllorNone.Checked ? 1 : 0;
            }
            m_dtObjectDataSource.AcceptChanges();
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtObjectDataSource.Rows)
            {
                dr["CHON"] = Convert.ToInt16(dr["CHON"]) == 1 ? 0 : 1;
            }
            m_dtObjectDataSource.AcceptChanges();
        }

        private void chkcheckallSameCode_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
            {
                // dr["CHON"] = chkcheckallSameCode.Checked ? 1 : 0;
            }
            m_dtSameCodeDataSource.AcceptChanges();
        }

        private void chkReverseSameCode_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtSameCodeDataSource.Rows)
            {
                dr["CHON"] = Convert.ToInt16(dr["CHON"]) == 1 ? 0 : 1;
            }
            m_dtSameCodeDataSource.AcceptChanges();
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Sự kiện nhấn nút Thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sự kiện nhấn nút Ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
        private void cmdAddObject_Click(object sender, EventArgs e)
        {
            //frmSimpleObjectTypeList frm = new frmSimpleObjectTypeList();
            //frm.m_dtObjectDataSource = m_dtObjectDataSource;
            //frm.Drug_ID = Drug_ID;
            //frm.ShowDialog();
            //cmdRemoveAllObject.Enabled = m_dtObjectDataSource.Rows.Count > 0;
            //cmdRemoveObject.Enabled = cmdRemoveAllObject.Enabled;
            //if(m_enAction==action.Insert)
            //    Utility.SetDataSourceForDataGridView(grdObjectRelationList, m_dtObjectDataSource, false, true, "", "ObjectType_Name");
        }
        private void cmdAddSamecode_Click(object sender, EventArgs e)
        {
            //frmSimpleSameCodeDrugList frm = new frmSimpleSameCodeDrugList();
            //frm.m_dtSameCodeDrugDataSource = m_dtSameCodeDataSource;
            //frm.Drug_ID = Drug_ID;
            //frm.ShowDialog();
            //cmdRemoveAllSameCode.Enabled = m_dtSameCodeDataSource.Rows.Count > 0;
            //cmdRemoveSamecode.Enabled = cmdRemoveAllSameCode.Enabled;
            //if (m_enAction == action.Insert)
            //    Utility.SetDataSourceForDataGridView(grdSameCodeDrugList, m_dtSameCodeDataSource, false, true, "", "Drug_Name");
        }
        void  txtDongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = Utility.NumbersOnly(e.KeyChar,  txtDongia);
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc định dạng giá của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void  txtDongia_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(  txtDongia.Enabled = true);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc địn dạng tên thuốc theo định dạng chuẩn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTEN_BHYT.Text))
                txtTEN_BHYT.Text = Utility.sDbnull(txtName.Text);
        }
        /// <summary>
        /// hàm thực hiện việc định dạng hàm lượng thuốc theo định dạng chuẩn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActice_LostFocus(object sender, System.EventArgs e)
        {
            //txtActice.Text = Utility.chuanhoachuoi(txtActice.Text);
        }
        private void cmdCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTEN_BHYT.Text = Utility.sDbnull(txtName.Text);
        }




    }
}
