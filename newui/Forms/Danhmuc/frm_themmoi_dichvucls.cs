using System;
using System.Data;
using System.Windows.Forms;
using VNS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using Janus.Windows.GridEX;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_dichvucls : Form
    {
        #region "THUOC TINH"
        public GridEX grdService;
        private DataTable m_dtDataServiceType=new DataTable();
        public DataRow drServiceInfo;
        public  DataTable dsService=new DataTable();
        public action em_Action = action.Insert;
        bool m_blnLoaded = false;
        #endregion
        public frm_themmoi_dichvucls()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
            cmdThoat.Click+=new EventHandler(cmdThoat_Click);
            cmdGhi.Click+=new EventHandler(cmdGhi_Click);
            txtServiceName.LostFocus+=new EventHandler(txtServiceName_LostFocus);
            txtServiceCode.CharacterCasing = CharacterCasing.Upper;
            cboDepartment.SelectedIndexChanged += new EventHandler(cboDepartment_SelectedIndexChanged);
        }

        void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            DataTable dtPhong = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), 1);
            DataBinding.BindDataCombobox(cboPhongthuchien, dtPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa phòng", true);
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtServicePrice_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtServicePrice);
        }

        private DataTable m_dtNhomDichVu,m_dtKhoaChucNang=new DataTable();
        void InitData()
        {
            m_dtDataServiceType = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAIDICHVUCLS", true);
            DataBinding.BindDataCombox(cboServiceType, m_dtDataServiceType, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            m_dtNhomDichVu = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBAOCAOCLS",true);
            DataBinding.BindDataCombox(cbonhombaocao, m_dtNhomDichVu, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
           DataTable  dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
           DataBinding.BindDataCombox(cboNhomin, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            m_dtKhoaChucNang = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL",1);
            DataBinding.BindDataCombobox(cboDepartment, m_dtKhoaChucNang, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn---", true);
            
            
        }
        void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
                em_Action = action.Insert;
                txtServiceCode.Focus();
            }
        }
        private void frm_themmoi_dichvucls_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                m_blnLoaded = true;
                if (em_Action == action.Update) GetData();
                if (em_Action == action.Insert)
                {
                    txtServiceOrder.Value = Utility.DecimaltoDbnull(query.GetMax(DmucDichvucl.Columns.SttHthi), 0);
                    txtID.Text = (Utility.DecimaltoDbnull(query.GetMax(DmucDichvucl.Columns.SttHthi), 0) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                m_blnLoaded = true;
            }
        }
        void BindServiceCode()
        {
            
        }
        private void frm_themmoi_dichvucls_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frm_themmoi_dichvucls_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdThoat.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if(e.Control&&e.KeyCode==Keys.S)cmdGhi.PerformClick();
            if (e.KeyCode == Keys.F5) ClearControl();
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {

            if (!IsValidData())return;
            switch (em_Action)
            {
                case action.Insert:
                    Insert();
                    break;
                case action.Update:
                    Update();
                    break;
                    

            }
        }
     
        private bool  IsValidData()
        {
            SqlQuery q;
            Utility.SetMsg(lblMsg, "", true);
            if (em_Action == action.Insert)
            {
                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.MaDichvu).IsEqualTo(Utility.DoTrim( txtServiceCode.Text));
                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có mã như vậy", true);
                    txtServiceCode.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtServiceName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên dịch vụ", true);
                txtServiceName.Focus();
                return false;
            }
            if (cboServiceType.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn loại dịch vụ",true);
                cboServiceType.Focus();
                return false;
            }
           
            if(em_Action==action.Insert)
            {
                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.TenDichvu).IsEqualTo(txtServiceName.Text);
                if(q.GetRecordCount()>0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có tên như vậy", true);
                    txtServiceName.Focus();
                    return false;
                }
            }
            if (em_Action == action.Update)
            {
                q = new Select().From(DmucDichvucl.Schema)
                    .Where(DmucDichvucl.Columns.TenDichvu).IsEqualTo(txtServiceName.Text).And(DmucDichvucl.Columns.IdDichvu).
                    IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

                if (q.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Đã tồn tại dịch vụ có tên như vậy", true);
                    txtServiceName.Focus();
                    return false;
                }
            }
            if (Utility.sDbnull(cboNhomin.SelectedValue, "-1") == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm in phiếu", true);
                cboNhomin.Focus();
                return false;
            }
            if (Utility.sDbnull(cbonhombaocao.SelectedValue, "-1") == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn nhóm báo cáo", true);
                cbonhombaocao.Focus();
                return false;
            }
            return true;
        }
        void GetData()
        {
            DmucDichvucl objService = DmucDichvucl.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objService != null)
            {
                txtID.Text = objService.IdDichvu.ToString();
                txtServiceCode.Text = Utility.sDbnull(objService.MaDichvu, "");
                txtServiceName.Text = Utility.sDbnull(objService.TenDichvu, "");
                nmrDongia.Value = Utility.DecimaltoDbnull(objService.DonGia, 0);
                if (Utility.Int32Dbnull(objService.IdKhoaThuchien, -1) > 0)
                {
                    cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, objService.IdKhoaThuchien.Value.ToString());
                }
                if (Utility.Int32Dbnull(objService.IdPhongThuchien, -1) > 0)
                {
                    cboPhongthuchien.SelectedIndex = Utility.GetSelectedIndex(cboPhongthuchien, objService.IdPhongThuchien.Value.ToString());
                }
                txtDesc.Text = Utility.sDbnull(objService.MotaThem, "");
                txtchidan.Text = objService.ChiDan;
                txtServiceOrder.Value = Utility.DecimaltoDbnull(objService.SttHthi, "1");
                cbonhombaocao.SelectedIndex = Utility.GetSelectedIndex(cbonhombaocao, objService.NhomBaocao);
                cboNhomin.SelectedIndex = Utility.GetSelectedIndex(cboNhomin, objService.NhomInCls);
                chkTrangthai.Checked = Utility.Int16Dbnull(objService.TrangThai, 0) == 1;
                chkHaveDetail.Checked = objService.HienthiChitiet == 1 ? true : false;
                chkHighTech.Checked = objService.DichvuKtc == 1 ? true : false;
                cboServiceType.SelectedIndex = Utility.GetSelectedIndex(cboServiceType, Utility.sDbnull(objService.IdLoaidichvu, "-1"));
            }
        }

        private Query query = DmucDichvucl.CreateQuery();
        void Insert()
        {
            try
            {
                DmucDichvucl objDichVu = new DmucDichvucl();
                objDichVu.MotaThem = txtDesc.Text;
                objDichVu.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVu.IdPhongThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVu.MaDichvu = Utility.sDbnull(txtServiceCode.Text, "");
                objDichVu.TenDichvu = Utility.sDbnull(txtServiceName.Text, "");
                objDichVu.IdLoaidichvu = Utility.sDbnull(cboServiceType.SelectedValue, "-1");
                objDichVu.SttHthi = Utility.Int16Dbnull(txtServiceOrder.Value, 0);
                objDichVu.DichvuKtc = chkHighTech.Checked ? Convert.ToInt16(1) : Convert.ToInt16(0);
                objDichVu.HienthiChitiet = chkHaveDetail.Checked ? Convert.ToByte(1) : Convert.ToByte(0);
                objDichVu.NgayTao = globalVariables.SysDate;
                objDichVu.NguoiTao = globalVariables.UserName;
                objDichVu.ChiDan = Utility.DoTrim(txtchidan.Text);
                objDichVu.TrangThai = (byte)(chkTrangthai.Checked ? 1 : 0);
                objDichVu.NhomInCls = Utility.sDbnull(cboNhomin.SelectedValue, "ALL");// getNhominCLS(cboNhomin.SelectedIndex);
                objDichVu.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objDichVu.DonGia = nmrDongia.Value;
                objDichVu.IsNew = true;
                objDichVu.Save();
                ProcessData(objDichVu.IdDichvu);
            }
            catch
            {
            }
        }

        private void ProcessData(int id)
        {
            
            DataRow dr = dsService.NewRow();
            dr[VDmucDichvucl.Columns.IdDichvu] = id;
            dr[VDmucDichvucl.Columns.TenLoaidichvu] = Utility.sDbnull(cboServiceType.Text, "");
            dr[DmucDichvucl.Columns.NguoiTao] =globalVariables.UserName;
            dr[DmucDichvucl.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucDichvucl.Columns.IdDichvu] =Utility.Int32Dbnull(query.GetMax(DmucDichvucl.Columns.IdDichvu),-1);
            dr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(txtServiceName.Text, "");
            dr[DmucDichvucl.Columns.MaDichvu] = Utility.sDbnull(txtServiceCode.Text, "");
            dr[DmucDichvucl.Columns.HienthiChitiet] = chkHaveDetail.Checked ? Convert.ToByte(1) : Convert.ToByte(0);
            dr[DmucDichvucl.Columns.DichvuKtc] = chkHighTech.Checked ? Convert.ToInt16(1) : Convert.ToInt16(0);
            dr[DmucDichvucl.Columns.IdLoaidichvu] = Utility.sDbnull(cboServiceType.SelectedValue, "-1");
            dr[DmucDichvucl.Columns.SttHthi] = Utility.Int32Dbnull(txtServiceOrder.Text,1);
            dr[DmucDichvucl.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue,-1);
            dr[DmucDichvucl.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
            if (cboDepartment.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
            else
                dr[VDmucDichvucl.Columns.TenKhoaThuchien] = "";
            if (cboPhongthuchien.SelectedIndex > 0)
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
            else
                dr[VDmucDichvucl.Columns.TenPhongThuchien] = "";
            dr[DmucDichvucl.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;
            dr[DmucDichvucl.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue,"-1");
            dr[VDmucDichvucl.Columns.TenNhombaocaoDichvu] = Utility.sDbnull(cbonhombaocao.Text, "");
            dr[VDmucDichvucl.Columns.TenNhominphieucls] = Utility.sDbnull(cbonhombaocao.Text, "");
            dr[DmucDichvucl.Columns.NhomInCls] = Utility.sDbnull(cboNhomin.SelectedValue, "ALL");// getNhominCLS(cboNhomin.SelectedIndex);
            dr[DmucDichvucl.Columns.DonGia] = nmrDongia.Value;
            dsService.Rows.Add(dr);
            dsService.AcceptChanges();
            Utility.GotoNewRowJanus(grdService, DmucDichvucl.Columns.IdDichvu, id.ToString());
            this.Close();
        }
        /// <summary>
        //Phiếu xét nghiệm
        //Phiếu X Quang
        //Phiếu siêu âm
        //Phiếu điện tim
        //Phiếu nội soi
        //Phiếu điện não đồ
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string getNhominCLS(int index)
        {

            switch(index)
            {
                case 0:
                    return "XN";
                case 1:
                    return "XQ";
                case 2:
                    return "SA";
                case 3:
                    return "DIEN_TIM";
                case 4:
                    return "NS";
                case 5:
                    return "DIEN_NAODO";
                default:
                    return "XN";
            }
            return "XN";
                
        }
        int getselectedIndex(string strNhomin)
        {
            //Phiếu xét nghiệm
            //Phiếu X Quang
            //Phiếu siêu âm
            //Phiếu điện tim
            //Phiếu nội soi
            //Phiếu điện não đồ
            switch (strNhomin)
            {
                case "XN":
                    return 0;
                case "XQ":
                    return 1;
                case "SA":
                    return 2;
                case "DIEN_TIM":
                    return 3;
                case "NS":
                    return 4;
                case "DIEN_NAODO":
                    return 5;
                default:
                    return 0;
            }
            return 0;

        }
        void Update()
        {
            new Update(DmucDichvucl.Schema)
                .Set(DmucDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                .Set(DmucDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                .Set(DmucDichvucl.Columns.TenDichvu).EqualTo(Utility.sDbnull(txtServiceName.Text, ""))
                .Set(DmucDichvucl.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(nmrDongia.Value,0))
                .Set(DmucDichvucl.Columns.MaDichvu).EqualTo(Utility.sDbnull(txtServiceCode.Text, ""))
                .Set(DmucDichvucl.Columns.MotaThem).EqualTo(Utility.sDbnull(txtDesc.Text, ""))
                .Set(DmucDichvucl.Columns.IdLoaidichvu).EqualTo(Utility.sDbnull(cboServiceType.SelectedValue, "-1"))
                .Set(DmucDichvucl.Columns.TrangThai).EqualTo(chkTrangthai.Checked?1:0)
                .Set(DmucDichvucl.Columns.HienthiChitiet).EqualTo(chkHaveDetail.Checked ? 1 : 0)
                .Set(DmucDichvucl.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(txtServiceOrder.Text, 0))
                .Set(DmucDichvucl.Columns.IdKhoaThuchien).EqualTo(Utility.Int16Dbnull(cboDepartment.SelectedValue,-1))
                .Set(DmucDichvucl.Columns.IdPhongThuchien).EqualTo(Utility.Int16Dbnull(cboPhongthuchien.SelectedValue,-1))
                .Set(DmucDichvucl.Columns.DichvuKtc).EqualTo(chkHighTech.Checked ? 1 : 0)
                .Set(DmucDichvucl.Columns.ChiDan).EqualTo(Utility.DoTrim(txtchidan.Text))
                .Set(DmucDichvucl.Columns.NhomBaocao).EqualTo(Utility.sDbnull(cbonhombaocao.SelectedValue,"-1"))
                //.Set(DmucDichvucl.Columns.NhomInCls).EqualTo(getNhominCLS(cboNhomin.SelectedIndex))
                .Set(DmucDichvucl.Columns.NhomInCls).EqualTo(Utility.sDbnull(cboNhomin.SelectedValue,"ALL"))
                .Where(DmucDichvucl.Columns.IdDichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text,-1))
                .Execute();
            
            ProcessData1();
        }

        private void ProcessData1()
        {
            DataRow[] arrDr=dsService.Select(DmucDichvucl.Columns.IdDichvu+"="+txtID.Text);
            if(arrDr.Length>0)
            {
                if (arrDr[0][DmucDichvucl.Columns.IdDichvu].ToString().Equals(txtID.Text))
                {
                    arrDr[0][DmucDichvucl.Columns.NguoiSua] = globalVariables.UserName;
                    arrDr[0][DmucDichvucl.Columns.NgaySua] = globalVariables.SysDate;

                    arrDr[0][DmucDichvucl.Columns.TrangThai] = chkTrangthai.Checked ? 1 : 0;

                    arrDr[0][DmucDichvucl.Columns.NhomBaocao] = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                    arrDr[0][VDmucDichvucl.Columns.TenNhombaocaoDichvu] = Utility.sDbnull(cbonhombaocao.Text, "");
                    arrDr[0][VDmucDichvucl.Columns.TenNhominphieucls] = Utility.sDbnull(cboNhomin.Text, "");
                    arrDr[0][DmucDichvucl.Columns.NhomInCls] = Utility.sDbnull(cboNhomin.SelectedValue, "ALL");// getNhominCLS(cboNhomin.SelectedIndex);
                    arrDr[0][DmucDichvucl.Columns.DonGia] = Utility.DecimaltoDbnull(nmrDongia.Value, 0);
                    arrDr[0][DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(txtServiceName.Text, "");
                    arrDr[0][DmucDichvucl.Columns.MaDichvu] = Utility.sDbnull(txtServiceCode.Text, "");
                    arrDr[0][DmucDichvucl.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text, "");
                    arrDr[0][DmucDichvucl.Columns.IdLoaidichvu] = Utility.sDbnull(cboServiceType.SelectedValue, "-1");
                    arrDr[0][DmucDichvucl.Columns.HienthiChitiet] = chkHaveDetail.Checked ? 1 : 0;
                    arrDr[0][DmucDichvucl.Columns.SttHthi] = Utility.Int16Dbnull(txtServiceOrder.Text, 0);
                    arrDr[0][DmucDichvucl.Columns.DichvuKtc] = chkHighTech.Checked ? 1 : 0;
                    arrDr[0][VDmucDichvucl.Columns.TenLoaidichvu] = Utility.sDbnull(cboServiceType.Text, "");
                    arrDr[0][DmucDichvucl.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                    arrDr[0][DmucDichvucl.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                    arrDr[0][DmucDichvucl.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
                    if (cboDepartment.SelectedIndex > 0)
                        arrDr[0][VDmucDichvucl.Columns.TenKhoaThuchien] = Utility.sDbnull(cboDepartment.Text);
                    else
                        arrDr[0][VDmucDichvucl.Columns.TenKhoaThuchien] = "";
                    if (cboDepartment.SelectedIndex > 0)
                        arrDr[0][VDmucDichvucl.Columns.TenPhongThuchien] = Utility.sDbnull(cboPhongthuchien.Text);
                    else
                        arrDr[0][VDmucDichvucl.Columns.TenPhongThuchien] = "";
                }
            }
            dsService.AcceptChanges();
            this.Close();
        }
        private void txtServiceName_LostFocus(object sender, System.EventArgs e)
        {
            //txtServiceName.Text = Utility.chuanhoachuoi(txtServiceName.Text);
        }
        private void txtServicePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
           // Utility.OnlyDigit(e);
        }

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearControl();
        }

        private void cmdGhi_Click_1(object sender, EventArgs e)
        {

        }
    }
}
