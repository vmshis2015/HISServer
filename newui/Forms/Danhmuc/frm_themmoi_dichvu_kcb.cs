using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;



namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_dichvu_kcb : Form
    {
        #region "Declare Variable Form"
        public DataTable m_dtDataRelation=new DataTable();
        public action m_enAction = action.Insert;
        private DataTable m_dtGroupInsurance = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        #endregion

        #region "Contructor"
        public frm_themmoi_dichvu_kcb()
        {
            InitializeComponent();
            cboRoomDept.SelectedIndexChanged+=new EventHandler(cboRoomDept_SelectedIndexChanged);
            //cboRoomDept_ValueChanged
        }
        public TextBox _txtInsObject_ID
        {
            get { return txtInsObject_ID; }
        }
        #endregion
        #region "Method of Event Form"
        /// <summary>
        /// hàm thực hiện laod thông tin cảu Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_dichvu_kcb_Load(object sender, EventArgs e)
        {
            InitData();
            if (m_enAction == action.Update) GetData();
            else
            {
                nmrSTT.Value = Utility.DecimaltoDbnull(DmucDichvucl.CreateQuery().GetMax(DmucDichvukcb.Columns.SttHthi), 0);
            }
            m_blnLoaded = true;
            cboDepartment1_SelectedIndexChanged(cboDepartment1, e);
            

        }
        /// <summary>
        /// hàm thực hiện dùng phím tắt trên Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_dichvu_kcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
        #endregion
        bool m_blnLoaded = false;
        #region "Method of common"
        private void PerformAction()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    if (!IsValidData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!IsValidData()) return;
                    PerformActionUpdate();
                    break;
            }

        }
       
       
        private  DataTable dtDept = new DataTable();
         DataTable v_ObjectTypeList=new DataTable();
        /// <summary>
        /// hàm thực hiện khởi tạo thông tin 
        /// </summary>
        private void InitData()
        {
            try
            {
                dtDept = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI",0);
                //Khởi tạo danh mục Loại khám
                DataTable v_ExamTypeList = new DmucKieukhamCollection().Load().ToDataTable();
               
                cboLoaiKham.DataSource = v_ExamTypeList.DefaultView;
                cboLoaiKham.ValueMember = DmucDichvukcb.Columns.IdKieukham;
                cboLoaiKham.DisplayMember = DmucKieukham.Columns.TenKieukham;
                //Khởi tạo danh mục Loại khám
                v_ObjectTypeList = new Select().From(DmucDoituongkcb.Schema).ExecuteDataSet().Tables[0];
                //Utility.AddColumnAlltoDataTable(ref v_ObjectTypeList, DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "====Chọn====");
                //v_ObjectTypeList.DefaultView.Sort = DmucDoituongkcb.Columns.SttHthi;
                DataBinding.BindDataCombox(cboDoituong, v_ObjectTypeList, DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
                //cboDoituong.DataSource = v_ObjectTypeList.DefaultView;
                //cboDoituong.ValueMember = DmucDoituongkcb.Columns.IdDoituongKcb;
                //cboDoituong.DisplayMember = DmucDoituongkcb.Columns.TenDoituongKcb;
                //Phòng ban
                BindDepartment();
               
            }
            catch
            {
            }
        }
        private void BindStaffList(int departmentID)
        {
            try
            {
               
                DataTable _dsStaffList = THU_VIEN_CHUNG.Laydanhsachnhanvienthuockhoa(departmentID);
                Utility.AddColumnAlltoDataTable(ref _dsStaffList, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "====Chọn====");
                _dsStaffList.DefaultView.Sort =DmucNhanvien.Columns.IdNhanvien+ " ASC";
                cboBacSy.DataSource = _dsStaffList.DefaultView;
                cboBacSy.ValueMember = DmucNhanvien.Columns.IdNhanvien;
                cboBacSy.DisplayMember = DmucNhanvien.Columns.TenNhanvien;
                
            }
            catch
            {
            }

        }
        private void BindDepartment()
        {


            //Utility.AddColumnAlltoDataTable(ref dtDept, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "====Chọn====");
            DataBinding.BindDataCombox(cboDepartment1, dtDept, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
            //dtDept.DefaultView.Sort = DmucKhoaphong.Columns.SttHthi;
            //cboDepartment1.DataSource = dtDept.DefaultView;
            //cboDepartment1.ValueMember = DmucDichvukcb.Columns.IdKhoaphong;
            //cboDepartment1.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
            ////dtDept.DefaultView.RowFilter = DmucKhoaphong.Columns.PhongChucnang + "=0 and " + DmucKhoaphong.Columns.KieuKhoaphong + " =KHOA and ma_cha=0";
            //dtDept.DefaultView.Sort = DmucKhoaphong.Columns.SttHthi+ " ASC";
            if (cboDepartment1.Items.Count >= 0)
                BindRoomDept(Utility.Int32Dbnull(cboDepartment1.SelectedValue, -1));

        }
        private void BindRoomDept(int Dept_Id)
        {

           
            DataTable dataTable = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Dept_Id,0);
            Utility.AddColumnAlltoDataTable(ref dataTable, DmucDichvukcb.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "====Chọn====");
             cmdSave.Enabled = dataTable.Rows.Count > 0;
             dataTable.DefaultView.Sort =DmucKhoaphong.Columns.TenKhoaphong+ " ASC";
             cboRoomDept.DataSource = dataTable.DefaultView;
             cboRoomDept.ValueMember = DmucKhoaphong.Columns.IdKhoaphong;
             cboRoomDept.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
           
        }
        private Query _Query = DmucDichvukcb.CreateQuery();
        private void PerformActionInsert()
        {
            DmucDichvukcb objDmucDichvukcb=new DmucDichvukcb();
            objDmucDichvukcb.MaDichvukcb = Utility.sDbnull(txtCode.Text, "");
            objDmucDichvukcb.TenDichvukcb = Utility.sDbnull(txtName.Text, "");
            objDmucDichvukcb.IdKieukham = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
            objDmucDichvukcb.IdKhoaphong = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
            objDmucDichvukcb.IdBacsy = Convert.ToInt16(cboBacSy.Items.Count > 0
                                                                   ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                                   : -1);
            objDmucDichvukcb.MotaThem = Utility.DoTrim(txtDesc.Text);
            objDmucDichvukcb.SttHthi = Utility.Int16Dbnull(nmrSTT.Value);
            objDmucDichvukcb.IdDoituongKcb = Utility.Int16Dbnull(cboDoituong.SelectedValue,-1);
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objDmucDichvukcb.IdDoituongKcb);
            if (objectType != null)
                objDmucDichvukcb.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
            else
                objDmucDichvukcb.MaDoituongKcb = "ALL";

            DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1));
            if (objKieukham != null)
                objDmucDichvukcb.NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");
            else
                objDmucDichvukcb.NhomBaocao = "-1";

            objDmucDichvukcb.IdPhongkham = Utility.Int16Dbnull(cboRoomDept.SelectedValue);
            objDmucDichvukcb.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            objDmucDichvukcb.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
            objDmucDichvukcb.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
            objDmucDichvukcb.DongiaNgoaigio = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
            objDmucDichvukcb.PhuthuNgoaigio = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);
            objDmucDichvukcb.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
            objDmucDichvukcb.IsNew = true;
            objDmucDichvukcb.Save();
            DataRow dr = m_dtDataRelation.NewRow();
            dr[DmucDichvukcb.Columns.IdDichvukcb] = Utility.Int32Dbnull(_Query.GetMax(DmucDichvukcb.Columns.IdDichvukcb), -1);
            dr[DmucDichvukcb.Columns.IdDoituongKcb] = Utility.DecimaltoDbnull(cboDoituong.SelectedValue);
            dr[DmucDichvukcb.Columns.IdKhoaphong] = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
            dr[DmucDichvukcb.Columns.IdPhongkham] = Utility.Int16Dbnull(cboRoomDept.SelectedValue, -1);
            dr[DmucDichvukcb.Columns.IdBacsy] = cboBacSy.Items.Count > 0 ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1) : -1;
            dr[DmucDichvukcb.Columns.DonGia] =Utility.DecimaltoDbnull(txtDongia.Text,0);
            dr[DmucDichvukcb.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            dr[DmucDichvukcb.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
            dr[DmucDichvukcb.Columns.DongiaNgoaigio] = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
            dr[DmucDichvukcb.Columns.PhuthuNgoaigio] = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);

            dr[DmucDichvukcb.Columns.MotaThem] = Utility.DoTrim(txtDesc.Text);
            dr[DmucDichvukcb.Columns.SttHthi] = Utility.Int16Dbnull(nmrSTT.Value, 1);


            dr[DmucDichvukcb.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
             dr["ten_phong"] = cboRoomDept.Text;
             dr[DmucDichvukcb.Columns.IdKieukham] = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
            dr[DmucKieukham.Columns.TenKieukham] = cboLoaiKham.Text;
            dr[DmucDichvukcb.Columns.MaDichvukcb] = txtCode.Text.Trim();
            dr[DmucDichvukcb.Columns.TenDichvukcb] = txtName.Text.Trim();
            dr["ten_khoa"] = cboDepartment1.SelectedIndex <= -1 ? "Tất cả các khoa KCB" : cboDepartment1.Text;
            dr["ten_bacsi"] = cboBacSy.SelectedIndex > 0 ? cboBacSy.Text : "";
            dr[DmucDoituongkcb.Columns.TenDoituongKcb] =objectType != null? cboDoituong.Text:"Tất cả các đối tượng";

            m_dtDataRelation.Rows.Add(dr);
            //this.Close();
        }
        /// <summary>
        /// /hàm thực heien thông tin update thông tin lại
        /// </summary>
        private void PerformActionUpdate()
        {

            string MaDoituongKcb = "ALL";
            string NhomBaocao = "-1";
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(Utility.Int16Dbnull(cboDoituong.SelectedValue, -1));
            if (objectType != null)
            {
                MaDoituongKcb = objectType.MaDoituongKcb;
            }
            else
                MaDoituongKcb = "ALL";
            DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1));
            if (objKieukham != null)
                NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");
            else
                NhomBaocao = "-1";
            int record = new Update(DmucDichvukcb.Schema)
                  .Set(DmucDichvukcb.Columns.IdKhoaphong).EqualTo(Utility.Int16Dbnull(cboDepartment1.SelectedValue, 1))
                   .Set(DmucDichvukcb.Columns.MaDichvukcb).EqualTo(txtCode.Text.Trim())
                   .Set(DmucDichvukcb.Columns.TenDichvukcb).EqualTo(txtName.Text.Trim())
                   .Set(DmucDichvukcb.Columns.MotaThem).EqualTo(txtDesc.Text.Trim())
                   .Set(DmucDichvukcb.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(nmrSTT.Value))
                    .Set(DmucDichvukcb.Columns.NhomBaocao).EqualTo(NhomBaocao)
                     .Set(DmucDichvukcb.Columns.MaDoituongKcb).EqualTo(MaDoituongKcb)
                  .Set(DmucDichvukcb.Columns.IdPhongkham).EqualTo(Utility.Int16Dbnull(cboRoomDept.SelectedValue, 1))
                  .Set(DmucDichvukcb.Columns.IdBacsy).EqualTo(Convert.ToInt16(cboBacSy.Items.Count > 0 ? Utility.Int16Dbnull(cboBacSy.SelectedValue, 1) : -1))
                  .Set(DmucDichvukcb.Columns.IdKieukham).EqualTo(Utility.Int16Dbnull(cboLoaiKham.SelectedValue, 1))
                  .Set(DmucDichvukcb.Columns.IdDoituongKcb).EqualTo(Utility.Int16Dbnull(cboDoituong.SelectedValue, -1))
                  .Set(DmucDichvukcb.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(txtDongia.Text, 0))
                  .Set(DmucDichvukcb.Columns.DongiaNgoaigio).EqualTo(Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0))
                  .Set(DmucDichvukcb.Columns.PhuthuNgoaigio).EqualTo(Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0))
                  .Set(DmucDichvukcb.Columns.TuTuc).EqualTo(Utility.Bool2byte(chkTutuc.Checked))
                  .Set(DmucDichvukcb.Columns.PhuthuDungtuyen).EqualTo(Utility.DecimaltoDbnull(txtPTDT.Text, 0))
                  .Set(DmucDichvukcb.Columns.PhuthuTraituyen).EqualTo(Utility.DecimaltoDbnull(txtPTTT.Text, 0))
                  .Where(DmucDichvukcb.Columns.IdDichvukcb).IsEqualTo(Utility.DecimaltoDbnull(txtInsObject_ID.Text, -1)).Execute();
            if (record > 0)
            {
                DataRow[] dr = m_dtDataRelation.Select(DmucDichvukcb.Columns.IdDichvukcb+"=" + Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
                if (dr.GetLength(0) > 0)
                {
                    
                    dr[0][DmucDichvukcb.Columns.IdDoituongKcb] = Utility.DecimaltoDbnull(cboDoituong.SelectedValue);
                    dr[0][DmucDichvukcb.Columns.IdKhoaphong] = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                    dr[0][DmucDichvukcb.Columns.IdPhongkham] = Utility.Int16Dbnull(cboRoomDept.SelectedValue, -1);
                    dr[0][DmucDichvukcb.Columns.IdBacsy] = cboBacSy.Items.Count>0? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1):-1;
                    dr[0][DmucDichvukcb.Columns.IdKieukham] = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
                    dr[0][DmucKieukham.Columns.TenKieukham] = cboLoaiKham.Text;
                    dr[0]["ten_khoa"] = cboDepartment1.SelectedIndex <= -1 ? "Tất cả các khoa KCB" : cboDepartment1.Text;
                    dr[0]["ten_phong"] = cboRoomDept.Text;
                    dr[0][DmucDichvukcb.Columns.MaDichvukcb] = txtCode.Text.Trim();
                    dr[0][DmucDichvukcb.Columns.TenDichvukcb] = txtName.Text.Trim();
                    dr[0]["ten_bacsi"] = cboBacSy.SelectedIndex>0? cboBacSy.Text:"";
                    dr[0][DmucDichvukcb.Columns.MaDoituongKcb] = objectType != null ? objectType.MaDoituongKcb : "ALL";
                    dr[0][DmucDoituongkcb.Columns.TenDoituongKcb] = objectType != null ? cboDoituong.Text : "Tất cả các đối tượng";
                    dr[0][DmucDichvukcb.Columns.MotaThem] = Utility.DoTrim(txtDesc.Text);
                    dr[0][DmucDichvukcb.Columns.SttHthi] = Utility.Int16Dbnull(nmrSTT.Value, 1);

                    dr[0][DmucDichvukcb.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                    dr[0][DmucDichvukcb.Columns.DongiaNgoaigio] = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
                    dr[0][DmucDichvukcb.Columns.PhuthuNgoaigio] = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);
                    dr[0][DmucDichvukcb.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
                    dr[0][DmucDichvukcb.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                    dr[0][DmucDichvukcb.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                }
                m_dtDataRelation.AcceptChanges();
                this.Close();
            }
            else
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                return;
            }


        }
        /// <summary>
        /// hàm thực hiện kiểm tra thông tin của phần mã tham gia đối tượng bảo hiểm
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (cboLoaiKham.Items.Count<=0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục loại khám trước khi thực hiện chức năng này",true);
                cboLoaiKham.Focus();
                return false;

            }
            if (cboDoituong.Items.Count <= 0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục Đối tượng trước khi thực hiện chức năng này", true);
                cboDoituong.Focus();
                return false;

            }
            if (cboDepartment1.Items.Count <= 0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục Khoa trước khi thực hiện chức năng này", true);
                cboDepartment1.Focus();
                return false;

            }
            if (cboRoomDept.Items.Count<=0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục Phòng ban thuộc Khoa " + cboDepartment1.Text + " trước khi thực hiện chức năng này", true);
                cboRoomDept.Focus();
                return false;

            }
            SqlQuery q = new Select().From(DmucDichvukcb.Schema).Where(DmucDichvukcb.Columns.MaDichvukcb).IsEqualTo(Utility.DoTrim(txtCode.Text));
            if (m_enAction == action.Update)
                q.And(DmucDichvukcb.Columns.IdDichvukcb).IsNotEqualTo(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ KCB có mã " + Utility.DoTrim(txtCode.Text) + ". Mời bạn nhập mã khác",true);
                txtCode.Focus();
                return false;
            }
           //Kiểm tra xem quan hệ này đã có hay chưa
            if (blnIsExisted(ID,txtCode.Text.Trim(), Convert.ToInt32(cboDoituong.SelectedValue), Convert.ToInt32(cboRoomDept.SelectedValue), Convert.ToInt32(cboLoaiKham.SelectedValue), Convert.ToInt32(cboBacSy.SelectedValue)))
            {
                Utility.SetMsg(lblMsg, "Đã tồn tại quan hệ này. Đề nghị bạn chọn các cặp đối tượng khác! " + cboDepartment1.Text + " trước khi thực hiện chức năng này", true);
                cboRoomDept.Focus();
                return false;
            }
            return true;
        }
        public bool blnIsExisted(int ID,string code, int objectType_Id, int Department_Id, int ExamType_ID, int Doctor_ID)
        {
            try
            {
                DmucDichvukcbCollection v_DmucDichvukcb =
               new DmucDichvukcbController().FetchByQuery(
                   DmucDichvukcb.CreateQuery().AddWhere(DmucDichvukcb.Columns.MaDichvukcb,
                                                                Comparison.Equals, txtCode.Text.Trim()).AND(DmucDichvukcb.Columns.IdPhongkham,
                                                                Comparison.Equals, Department_Id).AND(
                                                                    DmucDichvukcb.Columns.IdDoituongKcb,
                                                                    Comparison.Equals, objectType_Id).AND(
                                                                    DmucDichvukcb.Columns.IdKieukham,
                                                                    Comparison.Equals, ExamType_ID).AND(
                                                                    DmucDichvukcb.Columns.IdBacsy,
                                                                    Comparison.Equals, Doctor_ID).AND(
                                                                    DmucDichvukcb.Columns.IdDichvukcb,
                                                                    Comparison.NotEquals, ID));
                return v_DmucDichvukcb.Count > 0;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        int ID = -1;
        /// <summary>
        /// lấy thông tin của khi load sửa thông tin của Form
        /// </summary>
        private void GetData()
        {
            DmucDichvukcb Obj = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
            if (Obj != null)
            {
                ID = Obj.IdDichvukcb;
                cboDepartment1.SelectedIndex = Utility.GetSelectedIndex(cboDepartment1,
                                                                         Obj.IdKhoaphong.ToString());
                cboDepartment1_SelectedIndexChanged(cboDepartment1, new EventArgs());
                cboRoomDept.SelectedIndex = Utility.GetSelectedIndex(cboRoomDept,
                                                                           Obj.IdPhongkham.
                                                                               ToString());
                cboRoomDept_SelectedIndexChanged(cboRoomDept, new EventArgs());
                cboBacSy.SelectedIndex = Utility.GetSelectedIndex(cboBacSy,
                                                                             Obj.IdBacsy.
                                                                                 ToString());
               
                cboLoaiKham.SelectedIndex = Utility.GetSelectedIndex(cboLoaiKham,
                                                                            Obj.IdKieukham.
                                                                                ToString());
                cboDoituong.SelectedIndex = Utility.GetSelectedIndex(cboDoituong,
                                                                            Obj.IdDoituongKcb.
                                                                                ToString());
               txtDongia.Text   = Utility.DecimaltoDbnull(Obj.DonGia, 0).ToString();
               txtGiangoaigio.Text = Utility.DecimaltoDbnull(Obj.DongiaNgoaigio, 0).ToString();
               txtPhuthungoaigio.Text = Utility.DecimaltoDbnull(Obj.PhuthuNgoaigio, 0).ToString();
               txtPTDT.Text = Utility.DecimaltoDbnull(Obj.PhuthuDungtuyen, 0).ToString();
               txtPTTT.Text = Utility.DecimaltoDbnull(Obj.PhuthuTraituyen, 0).ToString();
               chkTutuc.Checked = Utility.Byte2Bool(Obj.TuTuc);
               txtDesc.Text = Obj.MotaThem;
               nmrSTT.Value = Utility.DecimaltoDbnull(Obj.SttHthi, 1);
                txtCode.Text = Obj.MaDichvukcb;
                txtName.Text = Obj.TenDichvukcb;
            }
        }
        #endregion
        private decimal DeptFee;
       

        private void cboDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            BindRoomDept(Utility.Int32Dbnull(cboDepartment1.SelectedValue, -1));
        }

        private void cboRoomDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                BindStaffList(Utility.Int32Dbnull(cboRoomDept.SelectedValue));

            }
            catch (Exception)
            {

            }
        }

        private void cboLoaiKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                string _RowFilter = "1=1";
                DmucKieukham objExamTypeList = DmucKieukham.FetchByID(Utility.Int32Dbnull(cboLoaiKham.SelectedValue));
                if(objExamTypeList!=null)
                {
                    if(objExamTypeList.MaDoituongkcb == "ALL")
                    {
                        _RowFilter = string.Format("{0}",  "1=1");
                    }else
                    {
                        _RowFilter = string.Format("{0}='{1}'", DmucDoituongkcb.Columns.MaDoituongKcb, objExamTypeList.MaDoituongkcb);
                    }
                }
                v_ObjectTypeList.DefaultView.RowFilter = _RowFilter;
                v_ObjectTypeList.AcceptChanges();
            }
            catch (Exception)
            {
                
               // throw;
            }
        }
      
    }
}
