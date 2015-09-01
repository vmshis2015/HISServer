using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using System.Collections.Generic;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_themphieudieutri : Form
    {
        public NoitruPhanbuonggiuong objBuongGiuong;
        public KcbLuotkham objLuotkham;
        public bool b_Cancel;
        public action em_Action = action.Insert;
        public GridEX grdList;
        private DataTable m_dtNhanVienBsChidinh = new DataTable();
        public NoitruPhieudieutri objPhieudieutri;
        public DataTable p_TreatMent = new DataTable();
        DateTime MinDate = DateTime.Now;
        DateTime MaxDate = DateTime.Now;
        public List<int> lstPresID = new List<int>();
        public frm_themphieudieutri()
        {
            InitializeComponent();
            KeyPreview = true;
           
            cboBacSy.Enabled = true;
            CauHinh();
        }
        public void SetDate()
        {
            try
            {
                MinDate = objBuongGiuong.NgayVaokhoa;
                MaxDate = objBuongGiuong.NgayKetthuc != null ? objBuongGiuong.NgayKetthuc.Value : new DateTime(2099, 12,31);
                if (Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtNgayLapPhieu.MaxDate), 0) <= Utility.Int32Dbnull(Utility.GetYYYYMMDD(globalVariables.SysDate), 0))
                    dtNgayLapPhieu.Value = dtGioLapPhieu.Value = MaxDate;
                else
                {
                    dtNgayLapPhieu.Value = dtGioLapPhieu.Value = globalVariables.SysDate;
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void CauHinh()
        {
            cboKhoaNoiTru.Enabled = globalVariables.IsAdmin;
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

        /// <summary>
        /// hàm thực hiện việc khởi taojt hông tin 
        /// </summary>
        private void InitData()
        {
            try
            {
                
                DataTable dataTable=new DataTable();

                dataTable = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);
                    DataBinding.BindDataCombobox(cboKhoaNoiTru, dataTable,
                                               DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "", true);

                    if (objBuongGiuong != null)
                        cboKhoaNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoiTru,
                                                                               objBuongGiuong.IdKhoanoitru.ToString());
                    else if (objLuotkham != null)
                        cboKhoaNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoiTru,
                                                                               objLuotkham.IdKhoanoitru.ToString());
                    else
                    {
                        cboKhoaNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoiTru,
                                                                               globalVariables.idKhoatheoMay.ToString());
                    }
                    if (cboKhoaNoiTru.SelectedIndex != 0 && cboKhoaNoiTru.Items.Count == 1) cboKhoaNoiTru.SelectedIndex = 0;
                    DataTable m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 1);
                    DataBinding.BindDataCombox(cboBacSy, m_dtDoctorAssign, DmucNhanvien.Columns.IdNhanvien,
                                               DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ điều trị---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboBacSy.Items.Count > 0)
                        cboBacSy.SelectedIndex = 0;
                }
                else
                {
                    if (cboBacSy.Items.Count > 0)
                        cboBacSy.SelectedIndex = Utility.GetSelectedIndex(cboBacSy,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception)
            {
                
            }

            
        }

        private void frm_themphieudieutri_Load(object sender, EventArgs e)
        {
            InitData();
           
            getData();
            SetDate();
            ModifyCommands();
            cboKhoaNoiTru.Enabled = globalVariables.IsAdmin || cboKhoaNoiTru.Items.Count>1;
            cboBacSy.Enabled = globalVariables.IsAdmin;
           
        }

       

        private void ModifyCommands()
        {
            if (em_Action == action.Update)
            {
                cmdSave.Enabled = Utility.sDbnull(objPhieudieutri.NguoiTao) == globalVariables.UserName ||
                                  globalVariables.IsAdmin;
            }
            if (objPhieudieutri != null)
                Utility.SetMsg(lblMsg,
                                   string.Format("Phiếu điều trị của người dùng :{0}", objPhieudieutri.NguoiTao), true);
            else
            {
                Utility.SetMsg(lblMsg,
                                   string.Format("Phiếu điều trị của người dùng :{0}", globalVariables.UserName), true);
            }
        }

        /// <summary>
        /// hàm thực hiện việc lấy thông tin của dữ liệu
        /// </summary>
        private void getData()
        {
            if (objPhieudieutri != null && objPhieudieutri.IdPhieudieutri>0)
            {
               
                if (objPhieudieutri.NgayDieutri == null) objPhieudieutri.NgayDieutri = DateTime.Now;
                txtTreat_ID.Text = objPhieudieutri.IdPhieudieutri.ToString();
                dtNgayLapPhieu.Value = objPhieudieutri.NgayDieutri.Value;
                dtGioLapPhieu.Text = objPhieudieutri.GioDieutri;
                txtBstheodoi.Text = objPhieudieutri.ThongtinDieutri;
                txtDieuduongtheodoi.Text = objPhieudieutri.ThongtinTheodoi;
                cboKhoaNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoiTru, objPhieudieutri.IdKhoanoitru.ToString());
                chkPhieuBoSung.Checked = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                cboBacSy.SelectedIndex = Utility.GetSelectedIndex(cboBacSy, objPhieudieutri.IdBacsi.ToString());
                if (objBuongGiuong == null)
                    objBuongGiuong = NoitruPhanbuonggiuong.FetchByID(objPhieudieutri.IdBuongGiuong);
            }
            else
            {
                dtNgayLapPhieu.Value = dtNgayLapPhieu.MaxDate;
                dtGioLapPhieu.Value = dtNgayLapPhieu.MaxDate;
                

            }
        }

        private bool IsValidData()
        {
            if (!chkPhieuBoSung.Checked)
            {
                if (em_Action == action.Insert)
                {
                    DateTime _temp = new DateTime(dtNgayLapPhieu.Value.Year, dtNgayLapPhieu.Value.Month, dtNgayLapPhieu.Value.Day, dtGioLapPhieu.Value.Hour, dtGioLapPhieu.Value.Minute, 0);
                    if (_temp < MinDate || _temp > MaxDate)
                    {
                        Utility.ShowMsg(string.Format("Ngày lập phiếu phải nằm trong khoảng {0} và {1}", MinDate.ToString("dd/MM/yyyy HH:mm:ss"), MaxDate.ToString("dd/MM/yyyy HH:mm:ss")));
                        return false;
                    }
                    NoitruPhieudieutri item = new Select().From(NoitruPhieudieutri.Schema)
                     .Where(NoitruPhieudieutri.NgayDieutriColumn).IsEqualTo(dtNgayLapPhieu.Value.Date)
                     .And(NoitruPhieudieutri.Columns.TthaiBosung).IsEqualTo(0)
                     .And(NoitruPhieudieutri.Columns.IdKhoanoitru).IsEqualTo(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue,-1))
                     .And(NoitruPhieudieutri.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham, ""))
                     .ExecuteSingle<NoitruPhieudieutri>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Đã tồn tại phiếu điều trị chính cho tại khoa " + cboKhoaNoiTru.Text + " cho ngày: " + dtNgayLapPhieu.Value.ToString("dd/MM/yyyy") + ".\n Nếu bạn muốn nhập thêm phiếu bổ sung có thể check vào phiếu bổ sung \n Mời bạn nhập ngày khác");
                        dtNgayLapPhieu.Focus();
                        return false;
                    }
                }
                else
                {
                    //Kiểm tra xem khi update họ bỏ check phiếu bổ sung từ 1 phiếu bổ sung đi
                    NoitruPhieudieutri item = new Select().From(NoitruPhieudieutri.Schema)
                                     .Where(NoitruPhieudieutri.NgayDieutriColumn).IsEqualTo(dtNgayLapPhieu.Value.Date)
                                     .And(NoitruPhieudieutri.IdPhieudieutriColumn).IsNotEqualTo(objPhieudieutri.IdPhieudieutri)
                                     .And(NoitruPhieudieutri.Columns.IdKhoanoitru).IsEqualTo(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, -1))
                                     .And(NoitruPhieudieutri.Columns.TthaiBosung).IsEqualTo(0)
                                     .And(NoitruPhieudieutri.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham, ""))
                                     .ExecuteSingle<NoitruPhieudieutri>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Đã tồn tại phiếu điều trị chính tại khoa "+cboKhoaNoiTru.Text+" cho ngày: " + dtNgayLapPhieu.Value.ToString("dd/MM/yyyy") + ".\nChú ý: Bạn không thể biến đổi từ một phiếu bổ sung thành một phiếu điều trị chính nếu phiếu chính đã tồn tại");
                        dtNgayLapPhieu.Focus();
                        return false;
                    }
                }
            }

            if (cboKhoaNoiTru.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn khoa nội trú", true);
                cboKhoaNoiTru.Focus();
                return false;
            }

            if (cboBacSy.SelectedIndex <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn bác sỹ điều trị mời bạn xem lại", true);
                cboBacSy.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện viecj lưu lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            //GetDuplicatePres();
            switch (em_Action)
            {
                case action.Insert:
                    ThemPhieuDieuTri();
                    break;
                case action.Update:
                    UpdatePhieuDieuTri();
                    break;
            }
        }
        private void ThemPhieuDieuTri()
        {
            if (objPhieudieutri == null) objPhieudieutri = new NoitruPhieudieutri();
            if (em_Action == action.Update)
            {
                objPhieudieutri.MarkOld();
                objPhieudieutri.IsLoaded = true;
                objPhieudieutri.IsNew = false;
                objPhieudieutri.IdPhieudieutri = Utility.Int32Dbnull(txtTreat_ID.Text, -1);
                objPhieudieutri.NguoiSua = globalVariables.UserName;
                objPhieudieutri.NgaySua =globalVariables.SysDate;
                objPhieudieutri.TenMaysua = globalVariables.gv_strComputerName;
                objPhieudieutri.IpMaysua = globalVariables.gv_strIPAddress;
            }
            else
            {
                objPhieudieutri.TrangThai = 0;
                objPhieudieutri.IsNew = true;
                objPhieudieutri.TenMaytao = globalVariables.gv_strComputerName;
                objPhieudieutri.IpMaytao = globalVariables.gv_strIPAddress;
                objPhieudieutri.NguoiTao = globalVariables.UserName;
                objPhieudieutri.NgayTao = globalVariables.SysDate;
                objPhieudieutri.TthaiIn = 0;
            }
            objPhieudieutri.TthaiBosung =Utility.Bool2byte( chkPhieuBoSung.Checked);
            objPhieudieutri.IdPhieudieutri = Utility.Int32Dbnull(txtTreat_ID.Text, -1);
            objPhieudieutri.ThongtinDieutri = Utility.DoTrim(txtBstheodoi.Text);
            objPhieudieutri.ThongtinTheodoi = Utility.DoTrim(txtDieuduongtheodoi.Text);
            objPhieudieutri.NgayDieutri = dtNgayLapPhieu.Value.Date;
            objPhieudieutri.GioDieutri = dtGioLapPhieu.Text;
            objPhieudieutri.Thu = Utility.ConvertDayVietnamese(dtNgayLapPhieu.Value.DayOfWeek.ToString());
            objPhieudieutri.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
            objPhieudieutri.IdKhoanoitru = objBuongGiuong.IdKhoanoitru;
            objPhieudieutri.IdBuongGiuong =objBuongGiuong!=null? Utility.Int32Dbnull(objBuongGiuong.Id):-1;
            objPhieudieutri.IdBuong = objBuongGiuong != null ? Utility.Int32Dbnull(objBuongGiuong.IdBuong) : -1;
            objPhieudieutri.IdGiuong = objBuongGiuong != null ? Utility.Int32Dbnull(objBuongGiuong.IdGiuong) : -1;
            objPhieudieutri.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objPhieudieutri.IdBuong = objLuotkham.IdBuong;
            objPhieudieutri.IdGiuong = objLuotkham.IdGiuong;
            if (cboBacSy.SelectedIndex > 0)
                objPhieudieutri.IdBacsi = Utility.Int16Dbnull(cboBacSy.SelectedValue);
            else
            {
                objPhieudieutri.IdBacsi = globalVariables.gv_intIDNhanvien;
            }
            ActionResult actionResult = new noitru_phieudieutri().ThemPhieudieutri(objPhieudieutri);
            switch (actionResult)
            {
                case ActionResult.Success:
                    txtTreat_ID.Text = Utility.sDbnull(objPhieudieutri.IdPhieudieutri, -1);
                    DataRow drv = p_TreatMent.NewRow();
                    Utility.FromObjectToDatarow(objPhieudieutri, ref drv);
                    drv["sngay_dieutri"] = dtNgayLapPhieu.Value.ToString("dd/MM/yyyy");
                    if (cboBacSy.SelectedIndex > 0)
                    {
                        drv["ten_bacsidieutri"] = cboBacSy.Text;
                    }
                    
                    p_TreatMent.Rows.Add(drv);
                    Utility.GotoNewRowJanus(grdList, NoitruPhieudieutri.Columns.IdPhieudieutri, objPhieudieutri.IdPhieudieutri.ToString());
                    b_Cancel = true;
                    Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm phiếu điều trị", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        #region Sao chep don thuoc
        //private KcbDonthuocChitiet[] CreatePresDetail()
        //{
        //    var query = from thuoc in grdPresDetail.GetCheckedRows()

        //                select thuoc;
        //    var gridExRows = query as GridEXRow[] ?? query.ToArray();
        //    var arrDetail = new KcbDonthuocChitiet[gridExRows.Count()];
        //    int idx = 0;
        //    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in gridExRows.AsEnumerable())
        //    {
        //        arrDetail[idx] = new KcbDonthuocChitiet();
        //        arrDetail[idx].SoLuong = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
        //        arrDetail[idx].SoluongDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoluongDung].Value);
        //        arrDetail[idx].DonviTinh = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonviTinh].Value);
        //        arrDetail[idx].CachDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.CachDung].Value);
        //        arrDetail[idx].ChidanThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.ChidanThem].Value);
        //        arrDetail[idx].MotaThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
        //        arrDetail[idx].SolanDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SolanDung].Value);
        //        arrDetail[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value);
        //        arrDetail[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
        //        arrDetail[idx].PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.PhuThu].Value);
        //        arrDetail[idx].SoLuong = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
        //        arrDetail[idx].TuTuc = Utility.ByteDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value);
        //        arrDetail[idx].IdKho = Utility.Int16Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdKho].Value);
        //        arrDetail[idx].PtramBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.PtramBhyt].Value);
        //        arrDetail[idx].PtramBhytGoc = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.PtramBhytGoc].Value);
        //        arrDetail[idx].IdThuockho = Utility.Int64Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value);

        //        arrDetail[idx].BhytChitra = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.BhytChitra].Value);
        //        arrDetail[idx].BnhanChitra = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.BnhanChitra].Value);
        //        idx++;
        //    }
        //    return arrDetail;
        //}

        //private bool KiemTraTrungThuoc()
        //{
        //    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
        //    {
        //        SqlQuery sqlQuery = new Select(KcbDonthuocChitiet.Columns.IdThuoc, KcbDonthuocChitiet.Columns.IdChitietdonthuoc).From<KcbDonthuocChitiet>()
        //            .InnerJoin(KcbDonthuoc.IdDonthuocColumn, KcbDonthuocChitiet.IdDonthuocColumn)
        //            .Where(KcbDonthuoc.Columns.IdPhieudieutri).IsEqualTo(Utility.Int32Dbnull(txtTreat_ID.Text))
        //            .And(KcbDonthuocChitiet.Columns.IdThuoc)
        //            .IsEqualTo(Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value));
        //        if (sqlQuery.GetRecordCount() > 0)
        //        {
        //            KcbDonthuocChitiet objPrescriptionDetail = sqlQuery.ExecuteSingle<KcbDonthuocChitiet>();
        //            string tenthuoc = "";
        //            if (objPrescriptionDetail != null)
        //            {
        //                DmucThuoc objDrug = DmucThuoc.FetchByID(objPrescriptionDetail.IdThuoc);
        //                if (objDrug != null) tenthuoc = Utility.sDbnull(objDrug.TenThuoc);
        //            }
        //            Utility.ShowMsg(string.Format("Phiếu điều trị của bạn lập đã có thuốc :{0} \n Mời bạn xem lại", tenthuoc), "Thông báo trùng thuốc", MessageBoxIcon.Warning);
        //            return true;
        //            break;
        //        }
        //    }
        //    return false;
        //}
        //private void SaoChepDonThuoc()
        //{
        //    if (chkNgayDieuTri.Checked)
        //    {
        //        bool kiemtratrungthuoc = KiemTraTrungThuoc();
        //        if (kiemtratrungthuoc)
        //        {
        //            if (Utility.AcceptQuestion("Bạn có muốn sao chép thêm thuốc đã trùng không", "Thông báo", true))
        //            {
        //                SaoChepDonThuocDieuTri();
        //            }
        //        }
        //        else
        //        {
        //            SaoChepDonThuocDieuTri();
        //        }
        //    }

        //}

        //private void SaoChepDonThuocDieuTri()
        //{
        //    KcbDonthuocChitiet[] arrPresDetail = CreatePresDetail();
        //    if (arrPresDetail.Count() > 0)
        //    {
        //        int pres_id = Utility.Int32Dbnull(grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.IdDonthuoc].Value);
        //        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(pres_id);
        //        if (objPrescription != null)
        //        {
        //            objPhieudieutri = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtTreat_ID.Text));
        //            if (objPhieudieutri != null)
        //            {
        //                ActionResult actionResult = new noitru_phieudieutri().SaoChepDonThuocTheoPhieuDieuTri(objPrescription,
        //                    objPhieudieutri, arrPresDetail);
        //                switch (actionResult)
        //                {
        //                    case ActionResult.Error:
        //                        Utility.ShowMsg("Lỗi trong quát trình sao chép", "Thông báo lỗi", MessageBoxIcon.Error);
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
      

        private void UpdateNgayPhieuDieuTri()
        {
            new Update(KcbChidinhcl.Schema)
                .Set(KcbChidinhcl.Columns.NgayChidinh).EqualTo(dtNgayLapPhieu.Value)
                .Set(KcbChidinhcl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                .Set(KcbChidinhcl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                .Where(KcbChidinhcl.Columns.IdDieutri).IsEqualTo(Utility.Int32Dbnull(txtTreat_ID.Text)).Execute();

            new Update(KcbDonthuoc.Schema)
                .Set(KcbDonthuoc.Columns.NgayKedon).EqualTo(dtNgayLapPhieu.Value)
                .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                .Where(KcbDonthuoc.Columns.IdPhieudieutri).IsEqualTo(Utility.Int32Dbnull(txtTreat_ID.Text)).Execute();
        }
        private void UpdatePhieuDieuTri()
        {
            objPhieudieutri.IdPhieudieutri = Utility.Int32Dbnull(txtTreat_ID.Text, -1);
            if (objPhieudieutri.IdPhieudieutri > 0)
            {
                objPhieudieutri.MarkOld();
                objPhieudieutri.IsLoaded = true;
                objPhieudieutri.IsNew = false;
                objPhieudieutri.IdPhieudieutri = Utility.Int32Dbnull(txtTreat_ID.Text, -1);
                objPhieudieutri.NguoiSua = globalVariables.UserName;
                objPhieudieutri.NgaySua = globalVariables.SysDate;
                objPhieudieutri.TenMaysua = globalVariables.gv_strComputerName;
                objPhieudieutri.IpMaysua = globalVariables.gv_strIPAddress;
            }
            else
            {
                objPhieudieutri.TthaiIn = 0;
                objPhieudieutri.TrangThai = 0;
                objPhieudieutri.TenMaytao = globalVariables.gv_strComputerName;
                objPhieudieutri.IpMaytao =globalVariables.gv_strIPAddress;
                objPhieudieutri.NguoiTao = globalVariables.UserName;
                objPhieudieutri.NgayTao = globalVariables.SysDate;
            }
            objPhieudieutri.ThongtinDieutri = Utility.DoTrim(txtBstheodoi.Text);
            objPhieudieutri.ThongtinTheodoi = Utility.DoTrim(txtDieuduongtheodoi.Text);
            objPhieudieutri.NgayDieutri = dtNgayLapPhieu.Value.Date;
            objPhieudieutri.GioDieutri = dtGioLapPhieu.Text;
            objPhieudieutri.Thu = Utility.ConvertDayVietnamese(dtNgayLapPhieu.Value.DayOfWeek.ToString());
            objPhieudieutri.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
            objPhieudieutri.IdBuongGiuong = objBuongGiuong != null ? Utility.Int32Dbnull(objBuongGiuong.Id) : -1;
            objPhieudieutri.IdBuong = objBuongGiuong != null ? Utility.Int32Dbnull(objBuongGiuong.IdBuong) : -1;
            objPhieudieutri.IdGiuong = objBuongGiuong != null ? Utility.Int32Dbnull(objBuongGiuong.IdGiuong) : -1;
            objPhieudieutri.IdKhoanoitru = objBuongGiuong.IdKhoanoitru;
            objPhieudieutri.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objPhieudieutri.TthaiBosung =Utility.Bool2byte( chkPhieuBoSung.Checked);
            if (cboBacSy.SelectedIndex > 0)
                objPhieudieutri.IdBacsi = Utility.Int16Dbnull(cboBacSy.SelectedValue);
            else
            {
                objPhieudieutri.IdBacsi = globalVariables.gv_intIDNhanvien;
            }
            ActionResult actionResult = new noitru_phieudieutri().ThemPhieudieutri(objPhieudieutri);
            switch (actionResult)
            {
                case ActionResult.Success:
                    DataRow drv = p_TreatMent.NewRow();

                    Utility.FromObjectToDatarow(objPhieudieutri, ref drv);
                    var query = from dt in p_TreatMent.AsEnumerable()
                        where
                            Utility.Int32Dbnull(dt[NoitruPhieudieutri.Columns.IdPhieudieutri]) ==
                            Utility.Int32Dbnull(txtTreat_ID.Text, -1)
                        select dt;
                    
                    if (query.Any())
                    {
                        var firstrow = query.FirstOrDefault();
                        if(firstrow!=null)
                            firstrow.Delete();
                        p_TreatMent.AcceptChanges();
                    }
                    if (cboBacSy.SelectedIndex > 0)
                    {
                        if (p_TreatMent.Columns.Contains("ten_bacsidieutri"))drv["ten_bacsidieutri"] = cboBacSy.Text;
                    }
                  
                    drv["sngay_dieutri"] = dtNgayLapPhieu.Value.ToString("dd/MM/yyyy");
                    p_TreatMent.Rows.Add(drv);
                    Utility.GotoNewRowJanus(grdList, NoitruPhieudieutri.Columns.IdPhieudieutri, objPhieudieutri.IdPhieudieutri.ToString());
                    //Utility.ShowMsg("Bạn sửa thông tin thành công", "Thông báo thành công");
                    //SaoChepDonThuoc();
                    b_Cancel = true;
                    Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu điều trị", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }

        private void frm_themphieudieutri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
        }

        private void cboBacSy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //setStatusControl();
        }

       
        /// <summary>
        /// hàm thực hiện việc lấy thông tin đơn thuốc của phiếu điều trị
        /// </summary>
        /// <param name="treat_id"></param>
        /// <param name="KieuThuocVT"></param>

        private void label8_Click(object sender, EventArgs e)
        {

        }

      

       
    }
}