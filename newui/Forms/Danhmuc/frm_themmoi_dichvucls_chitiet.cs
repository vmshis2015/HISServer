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
        public  DataTable dtDataServiceDetail=new DataTable();
        public Janus.Windows.GridEX.GridEX grdlist;
        public DataRow drServiceDetail;
        bool m_blnLoaded = false;
        private  DataTable m_dtDichvuCLS=new DataTable();
        DataTable m_dtqheCamchidinhCLSChungphieu = new DataTable();
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
            txtDonvitinh._OnShowData += txtDonvitinh__OnShowData;
            txtPhuongphapthu._OnShowData += txtPhuongphapthu__OnShowData;
            txtDichvu._OnEnterMe += txtDichvu__OnEnterMe;
        }

        void txtDichvu__OnEnterMe()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDanhsachCamChidinhChungphieu.GetDataRows())
            {
                gridExRow.BeginEdit();
                if (Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu].Value) ==Utility.Int32Dbnull( txtDichvu.MyID))
                {
                    gridExRow.Cells["CHON"].Value = 1;
                    gridExRow.IsChecked = true;
                    break;
                }
                gridExRow.BeginEdit();
            }
        }

        void txtPhuongphapthu__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtPhuongphapthu.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtPhuongphapthu.myCode;
                txtPhuongphapthu.Init();
                txtPhuongphapthu.SetCode(oldCode);
                txtPhuongphapthu.Focus();
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
                txtDonvitinh.Focus();
            } 
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
            txtPhuongphapthu.Init();
            m_dtqheCamchidinhCLSChungphieu = new Select().From(QheCamchidinhCLSChungphieu.Schema).ExecuteDataSet().Tables[0];
            DataTable dtChitiet = SPs.DmucLaydanhsachChidinhclsChitiet(1, -1).GetDataSet().Tables[0];
            Utility.AddColumToDataTable(ref dtChitiet, "CHON", typeof(int));
            txtDichvu.Init(dtChitiet, new List<string>() { DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.MaChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu });
            Utility.SetDataSourceForDataGridEx_Basic(grdDanhsachCamChidinhChungphieu, dtChitiet, true, true, "1=1", "CHON DESC," + VDmucDichvuclsChitiet.Columns.SttHthiLoaidvu + "," + VDmucDichvuclsChitiet.Columns.SttHthiDichvu + "," + VDmucDichvuclsChitiet.Columns.SttHthi + "," + VDmucDichvuclsChitiet.Columns.TenChitietdichvu);
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
            m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
            DataTable m_dtDichvuCLS_new = m_dtDichvuCLS.Clone();
            if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 0").Length <= 0)
                m_dtDichvuCLS_new = m_dtDichvuCLS.Copy();
            else
            {
                foreach (DataRow dr in m_dtDichvuCLS.Rows)
                {
                    if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucDichvucl.Columns.IdLoaidichvu]), "0"))
                    {
                        m_dtDichvuCLS_new.ImportRow(dr);
                    }
                }
            }

            DataBinding.BindDataCombobox(cboService, m_dtDichvuCLS_new, DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.TenDichvu, "---Chọn---", false);
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
            DmucDichvuclsChitiet objDichVuChitiet = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if(objDichVuChitiet!=null)
            {
                txtServiceDetailName.Text = Utility.sDbnull(objDichVuChitiet.TenChitietdichvu, "");
                txtTenBHYT.Text = Utility.sDbnull(objDichVuChitiet.TenBhyt, "");
                txtServiceDetailCode.Text = Utility.sDbnull(objDichVuChitiet.MaChitietdichvu, "");
                txtDongia.Text = Utility.sDbnull(objDichVuChitiet.DonGia, "0");
                txtGiaBHYT.Text = Utility.sDbnull(objDichVuChitiet.GiaBhyt, "0");
                txtPTDT.Text = Utility.sDbnull(objDichVuChitiet.PhuthuDungtuyen, "0");
                txtPTTT.Text = Utility.sDbnull(objDichVuChitiet.PhuthuTraituyen, "0");
                txtchidan.Text = objDichVuChitiet.ChiDan;
                txtMotathem.Text = Utility.sDbnull(objDichVuChitiet.MotaThem, "");
                txtBTNam.Text = Utility.sDbnull(drServiceDetail[DmucDichvuclsChitiet.Columns.BinhthuongNam], "");
                txtBTNu.Text = Utility.sDbnull(drServiceDetail[DmucDichvuclsChitiet.Columns.BinhthuongNu], "");
                txtIntOrder.Value = Utility.DecimaltoDbnull(objDichVuChitiet.SttHthi, 1);
                chkTutuc.Checked = Utility.Byte2Bool(objDichVuChitiet.TuTuc);
                chkLachiphithem.Checked = Utility.Byte2Bool(objDichVuChitiet.LaChiphithem);
                txtDonvitinh.SetCode(Utility.sDbnull(objDichVuChitiet.MaDonvitinh));
                cboService.SelectedValue = Utility.sDbnull(objDichVuChitiet.IdDichvu, "-1");
                chkHienThi.Checked = Utility.sDbnull(objDichVuChitiet.HienThi, "0").ToString() == "1";
                chkCochitiet.Checked = Utility.Byte2Bool(objDichVuChitiet.CoChitiet);
                chkSingle.Checked = Utility.Byte2Bool(objDichVuChitiet.SingleService);

                chkTrangthai.Checked = Utility.sDbnull(objDichVuChitiet.TrangThai, "0").ToString() == "1";
                cboService.SelectedIndex = Utility.GetSelectedIndex(cboService, Utility.sDbnull(objDichVuChitiet.IdDichvu, "-1"));
                cboParent.SelectedIndex = Utility.GetSelectedIndex(cboParent, Utility.sDbnull(objDichVuChitiet.IdCha, "-1"));
                txtsoluongchitieu.Text = Utility.sDbnull(objDichVuChitiet.SoluongChitieu, "0");
                txtPhuongphapthu.SetCode(objDichVuChitiet.MaPhuongphapthu);
                txtKihieuDat.Text = objDichVuChitiet.KihieuDinhtinhDat;
                if (Utility.Int32Dbnull(objDichVuChitiet.IdKhoaThuchien, -1) > 0)
                {
                    cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, objDichVuChitiet.IdKhoaThuchien.Value.ToString());
                }
                if (Utility.Int32Dbnull(objDichVuChitiet.IdPhongThuchien, -1) > 0)
                {
                    cboPhongthuchien.SelectedIndex = Utility.GetSelectedIndex(cboPhongthuchien, objDichVuChitiet.IdPhongThuchien.Value.ToString());
                }
                cbonhombaocao.SelectedIndex = Utility.GetSelectedIndex(cbonhombaocao, objDichVuChitiet.NhomBaocao);
                LoadQheCamchidinhchung(objDichVuChitiet.IdChitietdichvu);
            }
           
        }
        private void LoadQheCamchidinhchung(int id_chitiet)
        {
            DataRow[] arrDr = m_dtqheCamchidinhCLSChungphieu.Select(QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu + "=" + id_chitiet + " OR " + QheCamchidinhCLSChungphieu.Columns.IdChitietdichvuCamchidinhcung + "=" + id_chitiet);
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDanhsachCamChidinhChungphieu.GetDataRows())
            {
                gridExRow.BeginEdit();
                if (Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu].Value) != id_chitiet)
                {
                    var query = from kho in arrDr.AsEnumerable()
                                where
                                Utility.Int32Dbnull(kho[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu], 0)
                                == Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu].Value)
                                || Utility.Int32Dbnull(kho[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvuCamchidinhcung], 0)
                                == Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu].Value)
                                select kho;
                    if (query.Count() > 0)
                    {
                        gridExRow.Cells["CHON"].Value = 1;
                        gridExRow.IsChecked = true;
                    }
                    else
                    {
                        gridExRow.Cells["CHON"].Value = 0;
                        gridExRow.IsChecked = false;
                    }
                }
                gridExRow.EndEdit();

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
            
            if (Utility.Int32Dbnull( cboService.SelectedValue,"-1") <= 0)
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
            int actionResult = CreateServiceDetail();
            if (actionResult > -1)
            {
                Utility.SetMsg(lblMsg, "Thêm thông tin thành công", false);
                try
                {
                    ProcessData(actionResult);
                }
                catch (Exception)
                {
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

                DmucDichvuclsChitiet objDichVuChitiet = new DmucDichvuclsChitiet();
                objDichVuChitiet.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                objDichVuChitiet.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objDichVuChitiet.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objDichVuChitiet.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);

                objDichVuChitiet.IdChitietdichvu = Utility.Int32Dbnull(txtID.Text, -1);
                objDichVuChitiet.IdDichvu = Utility.Int16Dbnull(cboService.SelectedValue, -1);
                objDichVuChitiet.TenChitietdichvu = Utility.DoTrim(txtServiceDetailName.Text);
                objDichVuChitiet.TenBhyt = Utility.DoTrim(txtTenBHYT.Text);
                objDichVuChitiet.MaChitietdichvu = Utility.sDbnull(txtServiceDetailCode.Text, "");
                objDichVuChitiet.SttHthi = Utility.Int32Dbnull(txtIntOrder.Value);
                objDichVuChitiet.MaDonvitinh = txtDonvitinh.myCode;
                objDichVuChitiet.NgayTao = globalVariables.SysDate;
                objDichVuChitiet.NguoiTao = globalVariables.UserName;
                objDichVuChitiet.MotaThem = Utility.DoTrim(txtMotathem.Text);
                objDichVuChitiet.BinhthuongNam = Utility.DoTrim(txtBTNam.Text);
                objDichVuChitiet.BinhthuongNu = Utility.DoTrim(txtBTNu.Text);
                objDichVuChitiet.ChiDan = Utility.DoTrim(txtchidan.Text);
                objDichVuChitiet.TrangThai = (byte)(chkTrangthai.Checked ? 1 : 0);
                objDichVuChitiet.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                objDichVuChitiet.LaChiphithem = Utility.Bool2byte(chkLachiphithem.Checked);
                objDichVuChitiet.IdCha = Utility.Int32Dbnull(cboParent.SelectedValue, -1);
                objDichVuChitiet.CoChitiet = Utility.Bool2byte(chkCochitiet.Checked);
                objDichVuChitiet.SingleService = Utility.Bool2byte(chkSingle.Checked);
                objDichVuChitiet.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objDichVuChitiet.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVuChitiet.IdPhongThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVuChitiet.KihieuDinhtinhDat = Utility.sDbnull(txtKihieuDat.Text);
                objDichVuChitiet.MaPhuongphapthu = txtPhuongphapthu.myCode;
                objDichVuChitiet.SoluongChitieu = (int)Utility.DecimaltoDbnull(txtsoluongchitieu, 0);
                objDichVuChitiet.IsNew = true;
                dmucDichvuCLS_busrule.Insert(objDichVuChitiet, GetQheCamchidinhCLSChungphieuCollection());
                return objDichVuChitiet.IdChitietdichvu;
            }
            catch
            {
                return -1;
            }
        }
        private QheCamchidinhCLSChungphieuCollection GetQheCamchidinhCLSChungphieuCollection()
        {
            QheCamchidinhCLSChungphieuCollection lst = new QheCamchidinhCLSChungphieuCollection();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDanhsachCamChidinhChungphieu.GetCheckedRows())
            {
                QheCamchidinhCLSChungphieu objQheNhanvienDanhmuc = new QheCamchidinhCLSChungphieu();
                objQheNhanvienDanhmuc.IdChitietdichvu = -1;
                objQheNhanvienDanhmuc.IdChitietdichvuCamchidinhcung = Utility.Int32Dbnull(gridExRow.Cells[QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu].Value);
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }
            return lst;
        }
        private void ProcessData(int ServiceDetailId)
        {
           
            DataRow dr = dtDataServiceDetail.NewRow();
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
            dr[DmucDichvuclsChitiet.Columns.SingleService] = Utility.Bool2byte(chkSingle.Checked);
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

            dr[DmucDichvuclsChitiet.Columns.KihieuDinhtinhDat] = Utility.sDbnull(txtKihieuDat.Text);
            dr[DmucDichvuclsChitiet.Columns.MaPhuongphapthu] = txtPhuongphapthu.myCode;
            dr[DmucDichvuclsChitiet.Columns.SoluongChitieu] = (int)Utility.DecimaltoDbnull(txtsoluongchitieu, 0);

          
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
            dtDataServiceDetail.Rows.Add(dr);
            dtDataServiceDetail.AcceptChanges();
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

                DmucDichvuclsChitiet objDichVuChitiet = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtID.Text, 0));
                objDichVuChitiet.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                objDichVuChitiet.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                objDichVuChitiet.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objDichVuChitiet.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);

                objDichVuChitiet.IdChitietdichvu = Utility.Int32Dbnull(txtID.Text, -1);
                objDichVuChitiet.IdDichvu = Utility.Int16Dbnull(cboService.SelectedValue, -1);
                objDichVuChitiet.TenChitietdichvu = Utility.DoTrim(txtServiceDetailName.Text);
                objDichVuChitiet.TenBhyt = Utility.DoTrim(txtTenBHYT.Text);
                objDichVuChitiet.MaChitietdichvu = Utility.sDbnull(txtServiceDetailCode.Text, "");
                objDichVuChitiet.SttHthi = Utility.Int32Dbnull(txtIntOrder.Value);
                objDichVuChitiet.MaDonvitinh = txtDonvitinh.myCode;
                objDichVuChitiet.NgayTao = globalVariables.SysDate;
                objDichVuChitiet.NguoiTao = globalVariables.UserName;
                objDichVuChitiet.MotaThem = Utility.DoTrim(txtMotathem.Text);
                objDichVuChitiet.BinhthuongNam = Utility.DoTrim(txtBTNam.Text);
                objDichVuChitiet.BinhthuongNu = Utility.DoTrim(txtBTNu.Text);
                objDichVuChitiet.ChiDan = Utility.DoTrim(txtchidan.Text);
                objDichVuChitiet.TrangThai = (byte)(chkTrangthai.Checked ? 1 : 0);
                objDichVuChitiet.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                objDichVuChitiet.LaChiphithem = Utility.Bool2byte(chkLachiphithem.Checked);
                objDichVuChitiet.IdCha = Utility.Int32Dbnull(cboParent.SelectedValue, -1);
                objDichVuChitiet.CoChitiet = Utility.Bool2byte(chkCochitiet.Checked);
                objDichVuChitiet.SingleService = Utility.Bool2byte(chkSingle.Checked);
                objDichVuChitiet.NhomBaocao = Utility.sDbnull(cbonhombaocao.SelectedValue, "-1");
                objDichVuChitiet.IdKhoaThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVuChitiet.IdPhongThuchien = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                objDichVuChitiet.KihieuDinhtinhDat = Utility.sDbnull(txtKihieuDat.Text);
                objDichVuChitiet.MaPhuongphapthu = txtPhuongphapthu.myCode;
                objDichVuChitiet.SoluongChitieu = (int)Utility.DecimaltoDbnull(txtsoluongchitieu, 0);
                objDichVuChitiet.IsNew = false;
                objDichVuChitiet.MarkOld();

                dmucDichvuCLS_busrule.Insert(objDichVuChitiet, GetQheCamchidinhCLSChungphieuCollection());
               
                //Tạm bỏ 29/08/2015
                //new Update(KcbThanhtoanChitiet.Schema)
                //    .Set(KcbThanhtoanChitiet.Columns.IdDichvu).EqualTo(Utility.Int16Dbnull(cboService.SelectedValue, -1))
                //    .Set(KcbThanhtoanChitiet.Columns.DonviTinh).EqualTo(txtDonvitinh.Text)
                //    .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(Utility.sDbnull(txtServiceDetailName.Text, ""))
                //    .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(2)
                //    .And(KcbThanhtoanChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))
                //    .Execute();


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
                foreach (DataRow dr in dtDataServiceDetail.Rows)
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
                        dr[DmucDichvuclsChitiet.Columns.SingleService] = Utility.Bool2byte(chkSingle.Checked);
                        dr[DmucDichvuclsChitiet.Columns.IdCha] = Utility.Int32Dbnull(cboParent.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.SttHthi] = txtIntOrder.Value;
                        dr[DmucDichvuclsChitiet.Columns.MotaThem] =Utility.DoTrim(txtMotathem.Text);
                        dr[DmucDichvuclsChitiet.Columns.IdDichvu] = Utility.Int16Dbnull(cboService.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.BinhthuongNam] = Utility.DoTrim(txtBTNam.Text);
                        dr[DmucDichvuclsChitiet.Columns.BinhthuongNu] = Utility.DoTrim(txtBTNu.Text);
                        dr[DmucDichvuclsChitiet.Columns.ChiDan] = Utility.DoTrim(txtchidan.Text);
                        dr[DmucDichvuclsChitiet.Columns.IdKhoaThuchien] = Utility.Int16Dbnull(cboDepartment.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.IdPhongThuchien] = Utility.Int16Dbnull(cboPhongthuchien.SelectedValue, -1);
                        dr[DmucDichvuclsChitiet.Columns.KihieuDinhtinhDat] = Utility.sDbnull(txtKihieuDat.Text);
                        dr[DmucDichvuclsChitiet.Columns.MaPhuongphapthu] = txtPhuongphapthu.myCode;
                        dr[DmucDichvuclsChitiet.Columns.SoluongChitieu] = (int)Utility.DecimaltoDbnull(txtsoluongchitieu, 0);

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
                dtDataServiceDetail.AcceptChanges();
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
