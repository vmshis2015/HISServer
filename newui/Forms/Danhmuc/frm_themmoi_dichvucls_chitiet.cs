using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using SubSonic;
using System.Data;
using VNS.Libs;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_dichvucls_chitiet : Form
    {
        
        #region "THUOC TINH"
        private Query query = DmucDichvuclsChitiet.CreateQuery();
      
        public action m_enAction = action.Insert;
        public  DataTable dsServiceDetail=new DataTable();
        public Janus.Windows.GridEX.GridEX grdlist;
        public DataRow drServiceDetail;
        bool m_blnLoaded = false;
        private  DataTable m_dtDataService=new DataTable();
        // private Query query = DmucDichvuclsChitiet.CreateQuery();
        #endregion
        #region "KHOI TAO "
        public frm_themmoi_dichvucls_chitiet()
        {
            InitializeComponent();
            txtServiceDetailCode.CharacterCasing = CharacterCasing.Upper;
            
            this.KeyPreview = true;
           // sysColor.BackColor = globalVariables.SystemColor;
           
            cmdExit.Click += new EventHandler(cmdExit_Click);
            btnNew.Click+=new EventHandler(btnNew_Click);
            txtServiceDetailName.LostFocus +=new EventHandler(txtServiceDetailName_LostFocus);
            cboDepartment.SelectedIndexChanged += new EventHandler(cboDepartment_SelectedIndexChanged);
            chkHienThi.Checked = true;
        }

        void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DataTable dtPhong = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), 1);
            DataBinding.BindDataCombobox(cboPhongthuchien, dtPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa phòng", true);
        }
        #endregion
        #region "SU KIEN CUA FORM"
        private void frmServiceDetail_Load(object sender, EventArgs e)
        {
            BindService();
            txtDonvitinh.Init();
            m_blnLoaded = true;
            SetControlStatus();
        }

        void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
                m_enAction = action.Insert;
                txtServiceDetailCode.Focus();
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN THOÁT FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// THỰC HIỆN GHI THÔNG TIN CỦA DỊCH VỤ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if(!IsValidData())return;
            switch (m_enAction)
            {
                case action.Insert:
                    Insert();
                    break;
                case  action.Update:
                    Update();
                    break;

            }
        }

       
        /// <summary>
        /// HÀM THỰC HIỆN PHÍM TẮT CỦA FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) ClearControl();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&e.KeyCode==Keys.S)btnNew.PerformClick();
        }
       
       
        /// <summary>
        /// HÀM THỰC HIỆN CHỈ CHO NHẬP SỐ VÀO Ô TIỀN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
        #endregion
        #region "HAM THUC HIEN HAM CHUNG"
        private void SetControlStatus()
        {
            if (m_enAction == action.Update)
            {
                getData();
            }
            if(m_enAction==action.Insert)
            {
                try
                {
                    txtIntOrder.Value = Utility.DecimaltoDbnull(query.GetMax(DmucDichvuclsChitiet.Columns.SttHthi), 0)+1;
                    txtID.Text = Utility.sDbnull(Utility.DecimaltoDbnull(query.GetMax(DmucDichvuclsChitiet.Columns.IdChitietdichvu), 0) + 1);
                }catch(Exception ex)
                {}
            }
        }

        public int Service_ID = -1;
        private DataTable m_dtKhoaChucNang=new DataTable();
        void BindService()
        {
            m_dtDataService = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboService, m_dtDataService, DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu, "---Chọn---", true);
            DataTable dtnhombaocao = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAOCLS", true);
            DataBinding.BindDataCombox(cbonhombaocao, dtnhombaocao, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            m_dtKhoaChucNang = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 1);
            DataBinding.BindDataCombobox(cboDepartment, m_dtKhoaChucNang, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn---", true);
            LoadServicesDetails();
        }
        void LoadServicesDetails()
        {
            DataTable dtdata = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.CoChitiet).IsEqualTo(1).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboParent, dtdata, DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu, "---Chọn---", true);
        }
        /// <summary>
        /// ham thuc hien viec laythong tin cua du lieu
        /// </summary>
        void getData()
        {
            DmucDichvuclsChitiet objServiceDetail = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if(objServiceDetail!=null)
            {
                txtServiceDetailName.Text = Utility.sDbnull(objServiceDetail.TenChitietdichvu, "");
                txtTenBHYT.Text = Utility.sDbnull(objServiceDetail.TenBhyt, "");
                txtServiceDetailCode.Text = Utility.sDbnull(objServiceDetail.MaChitietdichvu, "");
                txtDongia.Text = Utility.sDbnull(objServiceDetail.DonGia, "0");
                txtGiaBHYT.Text = Utility.sDbnull(objServiceDetail.GiaBhyt, "0");
                txtPTDT.Text = Utility.sDbnull(objServiceDetail.PhuthuDungtuyen, "0");
                txtPTTT.Text = Utility.sDbnull(objServiceDetail.PhuthuTraituyen, "0");
                txtchidan.Text = objServiceDetail.ChiDan;
                txtMotathem.Text = Utility.sDbnull(objServiceDetail.MotaThem, "");
                txtBTNam.Text = Utility.sDbnull(drServiceDetail[DmucDichvuclsChitiet.Columns.BinhthuongNam], "");
                txtBTNu.Text = Utility.sDbnull(drServiceDetail[DmucDichvuclsChitiet.Columns.BinhthuongNu], "");
                txtIntOrder.Value = Utility.DecimaltoDbnull(objServiceDetail.SttHthi, 1);
                chkTutuc.Checked = Utility.Byte2Bool(objServiceDetail.TuTuc);
                chkLachiphithem.Checked = Utility.Byte2Bool(objServiceDetail.LaChiphithem);
                txtDonvitinh.SetCode(Utility.sDbnull(objServiceDetail.MaDonvitinh));
                cboService.SelectedValue = Utility.sDbnull(objServiceDetail.IdDichvu, "-1");
                chkHienThi.Checked = Utility.sDbnull(objServiceDetail.HienThi, "0").ToString() == "1";
                chkCochitiet.Checked = Utility.Byte2Bool(objServiceDetail.CoChitiet);
                chkTrangthai.Checked = Utility.sDbnull(objServiceDetail.TrangThai, "0").ToString() == "1";
                cboService.SelectedIndex = Utility.GetSelectedIndex(cboService, Utility.sDbnull(objServiceDetail.IdDichvu, "-1"));
                cboParent.SelectedIndex = Utility.GetSelectedIndex(cboParent, Utility.sDbnull(objServiceDetail.IdCha, "-1"));
                if (Utility.Int32Dbnull(objServiceDetail.IdKhoaThuchien, -1) > 0)
                {
                    cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, objServiceDetail.IdKhoaThuchien.Value.ToString());
                }
                if (Utility.Int32Dbnull(objServiceDetail.IdPhongThuchien, -1) > 0)
                {
                    cboPhongthuchien.SelectedIndex = Utility.GetSelectedIndex(cboPhongthuchien, objServiceDetail.IdPhongThuchien.Value.ToString());
                }
                cbonhombaocao.SelectedIndex = Utility.GetSelectedIndex(cbonhombaocao, objServiceDetail.NhomBaocao);
            }
           
        }
        /// <summary>
        /// HAM THUC HIEN KIEM TRA XEM CO DU TIEU CHUAN DE EP THEM CSDL
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtServiceDetailName.Text))
            {
                Utility.SetMsg(lblMsg, "Tên chi tiết không bỏ trống", true);
                txtServiceDetailName.Focus();
                return false;
            }
            
            if (cboService.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn dịch vụ", true);
                cboService.Focus();
                return false;
            }
            //if (chkCochitiet.Checked && Utility.Int32Dbnull(cboParent.SelectedValue,-1)<=0)
            //{
            //    Utility.SetMsg(lblMsg, "Nếu dịch vụ này có chi tiết cấp dưới thì bạn phải chọn chi tiết cấp trên của nó. Mời chọn lại", true);
            //    cboParent.Focus();
            //    return false;
            //}
            SqlQuery sqlQuery = new Select().From(DmucDichvuclsChitiet.Schema)
                .Where(DmucDichvuclsChitiet.Columns.MaChitietdichvu).IsEqualTo(txtServiceDetailCode.Text);
            if (m_enAction == action.Insert)
            {
                
               
                DmucDichvuclsChitietCollection detailCollection = new DmucDichvuclsChitietController()
                    .FetchByQuery(DmucDichvuclsChitiet.CreateQuery().AddWhere(DmucDichvuclsChitiet.Columns.TenChitietdichvu,
                                                                        Comparison.Equals,
                                                                        txtServiceDetailName.Text.Trim())
                                                                        .AND(DmucDichvuclsChitiet.Columns.IdDichvu,Comparison.Equals,cboService.SelectedValue));
                if (detailCollection.Count()>0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại tên chi tiết dịch vụ", true);
                    txtServiceDetailName.Focus();
                    return false;
                }
               
            }
            else if (m_enAction == action.Update)
            {
               
                DmucDichvuclsChitietCollection detailCollection = new DmucDichvuclsChitietController()
                    .FetchByQuery(DmucDichvuclsChitiet.CreateQuery()
                        .AddWhere(DmucDichvuclsChitiet.Columns.TenChitietdichvu, Comparison.Equals, txtServiceDetailName.Text.Trim())
                            .AND(DmucDichvuclsChitiet.Columns.IdDichvu, Comparison.Equals, cboService.SelectedValue)
                            .AND(DmucDichvuclsChitiet.Columns.IdChitietdichvu, Comparison.NotEquals, txtID.Text));
                if (detailCollection.Count() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại tên chi tiết dịch vụ", true);
                    txtServiceDetailName.Focus();
                    return false;
                }
            }
           
            return true;
        }

        void Insert()
        {
          int actionResult=  CreateServiceDetail();
          if (actionResult>-1)
          {
              Utility.SetMsg(lblMsg, "Thêm thông tin thành công", false);
              try
              {
                   ProcessData(actionResult);
              }
              catch (Exception)
              {
                  
                  //throw;
              }
             
          }
          else
          {
              Utility.SetMsg(lblMsg, "Bạn thực hiện không thành công", true);
              return;

          }
        }
        private int CreateServiceDetail()
        {
            try
            {
                DmucDichvuclsChitiet objServiceDetail = new DmucDichvuclsChitiet();
                objServiceDetail.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                objServiceDetail.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objServiceDetail.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objServiceDetail.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);

                objServiceDetail.IdChitietdichvu = Utility.Int32Dbnull(txtID.Text, -1);
                objServiceDetail.IdDichvu = Utility.Int16Dbnull(cboService.SelectedValue, -1);
                objServiceDetail.TenChitietdichvu = Utility.DoTrim(txtServiceDetailName.Text);
                objServiceDetail.TenBhyt = Utility.DoTrim(txtTenBHYT.Text);
                objServiceDetail.MaChitietdichvu = Utility.sDbnull(txtServiceDetailCode.Text, "");
                objServiceDetail.SttHthi = Utility.Int32Dbnull(txtIntOrder.Value);
                objServiceDetail.MaDonvitinh = txtDonvitinh.myCode;
                objServiceDetail.NgayTao = globalVariables.SysDate;
                objServiceDetail.NguoiTao = globalVariables.UserName;
                objServiceDetail.MotaThem = Utility.DoTrim(txtMotathem.Text);
                objServiceDetail.BinhthuongNam = Utility.DoTrim(txtBTNam.Text);
                objServiceDetail.BinhthuongNu = Utility.DoTrim(txtBTNu.Text);
                objServiceDetail.ChiDan = Utility.DoTrim(txtchidan.Text);
                objServiceDetail.TrangThai =(byte)( chkTrangthai.Checked ? 1 : 0);
                objServiceDetail.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                objServiceDetail.LaChiphithem = Utility.Bool2byte(chkLachiphithem.Checked);
                objServiceDetail.IdCha = Utility.Int32Dbnull(cboParent.SelectedValue,-1);
                objServiceDetail.CoChitiet = Utility.Bool2byte(chkCochitiet.Checked);
                objServiceDetail.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objServiceDetail.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objServiceDetail.IdPhongThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objServiceDetail.IsNew = true;
                objServiceDetail.Save();
                return objServiceDetail.IdChitietdichvu;
            }
            catch
            {
                return -1;
            }
        }
        private void ProcessData(int ServiceDetailId)
        {
           
            DataRow dr = dsServiceDetail.NewRow();
            dr[DmucDichvuclsChitiet.Columns.TenBhyt] = Utility.DoTrim(txtTenBHYT.Text);
            dr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(txtServiceDetailName.Text, "");
            dr[DmucDichvuclsChitiet.Columns.MaChitietdichvu] = Utility.sDbnull(txtServiceDetailCode.Text, "");
            dr[DmucDichvuclsChitiet.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucDichvuclsChitiet.Columns.NguoiTao] = globalVariables.UserName;

            dr[DmucDichvuclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text,0);
            dr[DmucDichvuclsChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
            dr[DmucDichvuclsChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            dr[DmucDichvuclsChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);

            dr[DmucDichvuclsChitiet.Columns.SttHthi] = txtIntOrder.Value;
            dr[DmucDichvuclsChitiet.Columns.IdDichvu] = Utility.Int16Dbnull(cboService.SelectedValue, -1);
            dr[DmucDichvuclsChitiet.Columns.NgayTao] =globalVariables.SysDate;
            dr[DmucDichvuclsChitiet.Columns.NguoiTao] = globalVariables.UserName;
            dr[DmucDichvuclsChitiet.Columns.IdChitietdichvu] = ServiceDetailId;
            dr[DmucDichvuclsChitiet.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
            dr[DmucDichvuclsChitiet.Columns.LaChiphithem] = Utility.Bool2byte(chkLachiphithem.Checked);
            dr[DmucDichvuclsChitiet.Columns.CoChitiet] = Utility.Bool2byte(chkCochitiet.Checked);
            dr[DmucDichvuclsChitiet.Columns.IdCha] = Utility.Int32Dbnull(cboParent.SelectedValue,-1);
            dr[DmucDichvuclsChitiet.Columns.HienThi] = chkHienThi.Checked ? 1 : 0;
            dr[DmucDichvuclsChitiet.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
            dr[ DmucDichvucl.Columns.TenDichvu] = cboService.Text;
            dr[DmucDichvuclsChitiet.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
            dr[DmucDichvuclsChitiet.Columns.MaDonvitinh] = txtDonvitinh.myCode;
            dr["ten_donvitinh"] = txtDonvitinh.Text;
            dr[DmucDichvuclsChitiet.Columns.BinhthuongNam] =Utility.DoTrim( txtBTNam.Text);
            dr[DmucDichvuclsChitiet.Columns.BinhthuongNu] = Utility.DoTrim(txtBTNu.Text);
            dr[DmucDichvucl.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
            dr[DmucDichvucl.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
            if (cboDepartment.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
            else
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = "";
            if (cboPhongthuchien.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
            else
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = "";
            dr[VDmucDichvucl.Columns.MaDichvu] = ((DataTable)cboService.DataSource).Select("id_dichvu=" + cboService.SelectedValue.ToString())[0][VDmucDichvucl.Columns.MaDichvu];
            dr[DmucDichvuclsChitiet.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
            dr["ten_nhombaocao_chitiet"] = Utility.sDbnull(cbonhombaocao.Text, "");
            dsServiceDetail.Rows.Add(dr);
            dsServiceDetail.AcceptChanges();
            Utility.GotoNewRowJanus(grdlist,DmucDichvuclsChitiet.Columns.IdChitietdichvu, ServiceDetailId.ToString());
            if (!chkThemmoilientuc.Checked) this.Close();
            else
            {
                m_enAction = action.Insert;
                if (chkCochitiet.Checked) LoadServicesDetails();
                chkCochitiet.Checked = false;
                chkTutuc.Checked = false;
                chkHienThi.Checked = true;
                chkTrangthai.Checked = true;
                ClearControl();
                SetControlStatus();
            }
        }

        void Update()
        {
            try
            {
                new Update(DmucDichvuclsChitiet.Schema)
                    .Set(DmucDichvuclsChitiet.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(txtDongia.Text,0))
                    .Set(DmucDichvuclsChitiet.Columns.GiaBhyt).EqualTo(Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0))
                    .Set(DmucDichvuclsChitiet.Columns.PhuthuDungtuyen).EqualTo(Utility.DecimaltoDbnull(txtPTDT.Text, 0))
                    .Set(DmucDichvuclsChitiet.Columns.PhuthuTraituyen).EqualTo(Utility.DecimaltoDbnull(txtPTTT.Text, 0))
                    .Set(DmucDichvuclsChitiet.Columns.MotaThem).EqualTo(Utility.sDbnull(txtMotathem.Text, ""))
                    .Set(DmucDichvuclsChitiet.Columns.MaChitietdichvu).EqualTo(Utility.sDbnull(txtServiceDetailCode.Text, ""))
                    .Set(DmucDichvuclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Set(DmucDichvuclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(DmucDichvuclsChitiet.Columns.TenChitietdichvu).EqualTo(Utility.sDbnull(txtServiceDetailName.Text, ""))
                    .Set(DmucDichvuclsChitiet.Columns.TenBhyt).EqualTo(Utility.DoTrim(txtTenBHYT.Text))
                     .Set(DmucDichvuclsChitiet.Columns.IdKhoaThuchien).EqualTo(Utility.Int16Dbnull(cboDepartment.SelectedValue, -1))
                    .Set(DmucDichvuclsChitiet.Columns.IdPhongThuchien).EqualTo(Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1))
                    .Set(DmucDichvuclsChitiet.Columns.ChiDan).EqualTo(Utility.DoTrim(txtchidan.Text))
                    .Set(DmucDichvuclsChitiet.Columns.NhomBaocao).EqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue, "-1"))
                    .Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(txtIntOrder.Text, -1))
                    .Set(DmucDichvuclsChitiet.Columns.IdCha).EqualTo(Utility.Int32Dbnull(cboParent.SelectedValue, -1))
                    .Set(DmucDichvuclsChitiet.Columns.HienThi).EqualTo(chkHienThi.Checked ? 1 : 0)
                    .Set(DmucDichvuclsChitiet.Columns.TrangThai).EqualTo(chkTrangthai.Checked ? 1 : 0)
                    .Set(DmucDichvuclsChitiet.Columns.MaDonvitinh).EqualTo(txtDonvitinh.myCode)
                    .Set(DmucDichvuclsChitiet.Columns.TuTuc).EqualTo(Utility.Bool2byte(chkTutuc.Checked))
                    .Set(DmucDichvuclsChitiet.Columns.CoChitiet).EqualTo(Utility.Bool2byte(chkCochitiet.Checked))
                    .Set(DmucDichvuclsChitiet.Columns.BinhthuongNam).EqualTo(Utility.DoTrim(txtBTNam.Text))
                    .Set(DmucDichvuclsChitiet.Columns.BinhthuongNu).EqualTo(Utility.DoTrim(txtBTNu.Text))
                    .Set(DmucDichvuclsChitiet.Columns.IdDichvu).EqualTo(Utility.Int16Dbnull(cboService.SelectedValue, -1))
                   .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))

                    .Execute();
                new Update(KcbChidinhclsChitiet.Schema)
                   .Set(KcbChidinhclsChitiet.Columns.IdKhoaThuchien).EqualTo(Utility.Int16Dbnull(cboDepartment.SelectedValue, -1))
                   .Set(KcbChidinhclsChitiet.Columns.IdPhongThuchien).EqualTo(Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1))
                   .Set(KcbChidinhclsChitiet.Columns.IdDichvu).EqualTo(Utility.Int16Dbnull(cboService.SelectedValue, -1))
                   .Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))
                   .Execute();

                new Update(KcbThanhtoanChitiet.Schema)
                    .Set(KcbThanhtoanChitiet.Columns.IdDichvu).EqualTo(Utility.Int16Dbnull(cboService.SelectedValue, -1))
                    .Set(KcbThanhtoanChitiet.Columns.DonviTinh).EqualTo(txtDonvitinh.Text)
                    .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(Utility.sDbnull(txtServiceDetailName.Text, ""))
                    .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(2)
                    .And(KcbThanhtoanChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))
                    .Execute();


                ProcessData1();
            }
            catch
            {
            }
        }

        private void ProcessData1()
        {
            try
            {
                foreach (DataRow dr in dsServiceDetail.Rows)
                {
                    if (Utility.Int32Dbnull(dr[DmucDichvuclsChitiet.Columns.IdChitietdichvu], -1) == Utility.Int32Dbnull(txtID.Text, 0))
                    {
                        dr[DmucDichvuclsChitiet.Columns.TenBhyt] = Utility.DoTrim(txtTenBHYT.Text);
                        dr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(txtServiceDetailName.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.MaChitietdichvu] = Utility.sDbnull(txtServiceDetailCode.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.NgaySua] = globalVariables.SysDate;
                        dr[DmucDichvuclsChitiet.Columns.NguoiSua] =globalVariables.UserName;
                        dr[DmucDichvuclsChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                        dr[DmucDichvuclsChitiet.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
                        dr[DmucDichvuclsChitiet.Columns.LaChiphithem] = Utility.Bool2byte(chkLachiphithem.Checked);
                        dr[DmucDichvuclsChitiet.Columns.CoChitiet] = Utility.Bool2byte(chkCochitiet.Checked);
                        dr[DmucDichvuclsChitiet.Columns.IdCha] = Utility.Int32Dbnull(cboParent.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.SttHthi] = txtIntOrder.Value;
                        dr[DmucDichvuclsChitiet.Columns.MotaThem] =Utility.DoTrim(txtMotathem.Text);
                        dr[DmucDichvuclsChitiet.Columns.IdDichvu] = Utility.Int16Dbnull(cboService.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.BinhthuongNam] = Utility.DoTrim(txtBTNam.Text);
                        dr[DmucDichvuclsChitiet.Columns.BinhthuongNu] = Utility.DoTrim(txtBTNu.Text);
                        dr[DmucDichvuclsChitiet.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
                        dr[DmucDichvuclsChitiet.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                        dr[VDmucDichvucl.Columns.MaDichvu] = ((DataTable)cboService.DataSource).Select("id_dichvu=" + cboService.SelectedValue.ToString())[0][VDmucDichvucl.Columns.MaDichvu];
                        if (cboDepartment.SelectedIndex > 0)
                            dr[VDmucDichvuclsChitiet.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
                        else
                            dr[VDmucDichvuclsChitiet.Columns.TenKhoaThuchien] = "";
                        if (cboPhongthuchien.SelectedIndex > 0)
                            dr[VDmucDichvuclsChitiet.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
                        else
                            dr[VDmucDichvucl.Columns.TenPhongThuchien] = "";
                        dr[DmucDichvuclsChitiet.Columns.HienThi] = chkHienThi.Checked ? 1 : 0;
                        dr[DmucDichvuclsChitiet.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
                        dr[DmucDichvucl.Columns.TenDichvu] = cboService.Text;
                        dr[DmucDichvuclsChitiet.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                        dr["ten_nhombaocao_chitiet"] = Utility.sDbnull(cbonhombaocao.Text, "");
                        dr[DmucDichvuclsChitiet.Columns.MaDonvitinh] = txtDonvitinh.myCode;
                        dr["ten_donvitinh"] = txtDonvitinh.Text;
                        break;
                    }

                }
                dsServiceDetail.AcceptChanges();
                //Utility.GotoNewRow(grdlist, "coDmucDichvuclsChitiet_ID", txtID.Text);
                Utility.GotoNewRowJanus(grdlist, DmucDichvuclsChitiet.Columns.IdChitietdichvu, Utility.Int32Dbnull(txtID.Text, 0).ToString());
            }
            catch (Exception)
            {
                
               // throw;
            }
           
            this.Close();
        }
        #endregion
        private void txtServiceDetailName_LostFocus(object sender, System.EventArgs e)
        {
            //txtServiceDetailName.Text = Utility.chuanhoachuoi(txtServiceDetailName.Text);
        }
        

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearControl();
        }
    }
}
