using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlyphanbuonggiuong : Form
    {
        private DataTable m_dtTimKiembenhNhan=new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;

        public frm_Quanlyphanbuonggiuong()
        {
            InitializeComponent();
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Quanlyphanbuonggiuong_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            ModifyCommand();
            
        }
        private DataTable m_dtKhoaNoiTru=new DataTable();
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            DataBinding.BindDataCombobox(cboKhoaChuyenDen, m_dtKhoaNoiTru,
                                       DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,"",true);
            string _rowFilter = "1=1";
            
                if (PropertyLib._NoitruProperties.HienthiKhoatheonguoidung)
                {
                    if (!globalVariables.IsAdmin)
                    {
                        _rowFilter = string.Format("{0}={1}", DmucKhoaphong.Columns.IdKhoaphong, globalVariables.IdKhoaNhanvien);
                    }
                }
               
           
           
            m_dtKhoaNoiTru.DefaultView.RowFilter = _rowFilter;
            m_dtKhoaNoiTru.AcceptChanges();

        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdSuaThongTinBN.Enabled = isValid;
            cmdNhapvien.Enabled =isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdKhoanoitru)) <= 0;
            cmdPhanGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdHuyphangiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdChuyenKhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 ;//&& Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdHuychuyenkhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdChuyen)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdChuyenGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
        }

        private string _rowFilter = "1=1";
        private void TimKiemThongTin()
        {
            m_dtTimKiembenhNhan =SPs.NoitruTimkiembenhnhan(Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue),
                                                txtPatientCode.Text, 1,
                                                chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                string.Empty, (int?) _TrangthaiNoitru,-1,0).
                    GetDataSet().Tables[0];
           
                if (PropertyLib._NoitruProperties.HienthiKhoatheonguoidung)
                {
                    _rowFilter = string.Format("{0}={1}", NoitruPhanbuonggiuong.Columns.IdKhoanoitru,
                        Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue));
                }
            Utility.SetDataSourceForDataGridEx(grdList, m_dtTimKiembenhNhan, true, true, _rowFilter, "");
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới nhập vện cấp cứu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            //frm_Add_TIEPDON_CapCuu_BN frm = new frm_Add_TIEPDON_CapCuu_BN();
            //frm.p_dtDatathongTinBN = m_dtTimKiembenhNhan;
            //frm.m_enAction = action.Insert;
            //frm.txtPatientCode.Text = "Tự sinh";
            //frm.txtPatientID.Text = "Tự sinh";
            //frm.MyGetData=new timkiem(TimKiemThongTin);
            //frm.grdList = grdList;
            //frm.ShowDialog();
            //ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc sửa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            //frm_Add_TIEPDON_CapCuu_BN frm = new frm_Add_TIEPDON_CapCuu_BN();
            //frm.p_dtDatathongTinBN = m_dtTimKiembenhNhan;
            //frm.m_enAction = action.Update;
            //frm.txtPatientCode.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            //frm.txtPatientID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            //frm.txtPatientCode.Enabled = false;
            //frm.txtPatientCode.Enabled = false;
            //frm.MyGetData = new timkiem(TimKiemThongTin);
            //frm.grdList = grdList;
            //frm.ShowDialog();
            //ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc ký quĩ thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKiQui_Click(object sender, EventArgs e)
        {
            

        }

        private bool inValiChuyenKhoa()
        {
           string  MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
           int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
           KcbLuotkham _KcbLuotkham = new Select().From<KcbLuotkham>()
                .Where(KcbLuotkham.Columns.MaLuotkham)
                .IsEqualTo(MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan).ExecuteSingle<KcbLuotkham>();
            if (_KcbLuotkham!=null && Utility.Int32Dbnull(_KcbLuotkham.IdKhoanoitru,-1)<0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện, Bạn không thể thực hiện chức năng chuyển khoa","Thông báo",MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        private bool inValiChuyenPhongGiuong()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
             KcbLuotkham _KcbLuotkham = new Select().From<KcbLuotkham>()
                .Where(KcbLuotkham.Columns.MaLuotkham)
                .IsEqualTo(MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan).ExecuteSingle<KcbLuotkham>();
              if (_KcbLuotkham!=null && Utility.Int32Dbnull(_KcbLuotkham.IdKhoanoitru,-1)<0)
            {
                Utility.ShowMsg("Bệnh nhân chưa phân buồng giường, Bạn không thể thực hiện chức năng chuyển khoa", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
              int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
              NoitruPhanbuonggiuong _NoitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                  .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (_NoitruPhanbuonggiuong!=null && Utility.Int32Dbnull(_NoitruPhanbuonggiuong.IdKhoanoitru,-1)<0)
            {
                Utility.ShowMsg("Bệnh nhân chưa phân buồng giường, Bạn không thể thực hiện phân buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
       
        private bool IsValiPhanPhongGiuong()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham _KcbLuotkham = new Select().From<KcbLuotkham>()
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan).ExecuteSingle<KcbLuotkham>();
            if (_KcbLuotkham != null && Utility.Int32Dbnull(_KcbLuotkham.IdKhoanoitru, -1) < 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa nhập viện, Bạn không thể thực hiện phân buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
              NoitruPhanbuonggiuong _NoitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                  .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
              if (_NoitruPhanbuonggiuong != null && Utility.Int32Dbnull(_NoitruPhanbuonggiuong.TrangThai, -1)==1)
            {
                Utility.ShowMsg("Bạn không được phép phân buồng giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// hàm thực hiện việc chuyển khoa cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(!inValiChuyenKhoa())return;
                frm_ChuyenKhoa frm = new frm_ChuyenKhoa();
                frm.IDBuonggiuong = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                frm.p_DanhSachPhanBuongGiuong = m_dtTimKiembenhNhan;
               // frm.m_enAction = action.Insert;
                frm.b_CallParent = true;
                
                frm.txtMaLanKham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.b_Cancel)
                {
                    int newid=Utility.Int32Dbnull(frm.txtPatientDept_ID.Text);
                    if (newid > 0)
                    {
                        DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(newid).GetDataSet().Tables[0];
                        if (dtTemp.Rows.Count > 0)
                        {
                            DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                            Utility.CopyData(dtTemp.Rows[0], ref dr);
                            m_dtTimKiembenhNhan.AcceptChanges();
                        }
                    }
                    else//Xóa dòng hiện tại
                    {
                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                        m_dtTimKiembenhNhan.Rows.Remove(dr);
                        m_dtTimKiembenhNhan.AcceptChanges();
                    }
                }
                ModifyCommand();
            }
            catch (Exception exception)
            {
                
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
        }
        /// <summary>
        /// hàm thực hên việc chuyển giường bệnh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenGiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!inValiChuyenPhongGiuong()) return;
                frm_Chuyengiuong frm = new frm_Chuyengiuong();
                frm.p_DanhSachPhanBuongGiuong = m_dtTimKiembenhNhan;
                frm.b_CallParent = true;
                // frm.m_enAction = action.Insert;
                frm.IDBuonggiuong = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                frm.txtMaLanKham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, e);
                    ModifyCommand();
                }
                ModifyCommand();
            }
            catch (Exception exception)
            {

                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc phân giường cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPhanGiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValiPhanPhongGiuong()) return;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    frm_phanbuonggiuong frm = new frm_phanbuonggiuong();
                    frm.p_DanhSachPhanBuongGiuong = m_dtTimKiembenhNhan;
                    frm.txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                    frm.objPhanbuonggiuong = objPhanbuonggiuong;
                    frm.ShowDialog();
                    if (!frm.b_Cancel)
                    {
                        grdList_SelectionChanged(grdList, e);
                        ModifyCommand();
                    }
                }
               
            }
            catch (Exception exception)
            {
                ModifyCommand();
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
        }
        private bool IsValidHuyGiuong()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            NoitruPhieudieutri _NoitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (_NoitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại buồng-giường đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            NoitruPhanbuonggiuong _NoitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (_NoitruPhanbuonggiuong != null && Utility.Int32Dbnull(_NoitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép phân buồng giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin phần buồng giường
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyphangiuong_Click(object sender, EventArgs e)
        {
            if (!IsValidHuyGiuong()) return;
            if (Utility.AcceptQuestion("Bạn có muốn hủy phần buồng giường cho bệnh nhân đang chọn không\n Nếu bạn chọn bệnh nhân sẽ ra khỏi giường","Thông báo", true))
            {
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    objPhanbuonggiuong.IdBuong = -1;
                    int IdChuyen = -1;
                    objPhanbuonggiuong.IdGiuong = -1;
                    ActionResult actionResult = new noitru_nhapvien().HuyBenhNhanVaoBuongGuong(objPhanbuonggiuong, ref IdChuyen);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            if (IdChuyen > 0)
                            {
                                DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(IdChuyen).GetDataSet().Tables[0];
                                if (dtTemp.Rows.Count > 0)
                                {
                                    DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                    Utility.CopyData(dtTemp.Rows[0], ref dr);
                                    m_dtTimKiembenhNhan.AcceptChanges();
                                }
                            }
                            else//Xóa dòng hiện tại
                            {
                                ProcessChuyenKhoa(id);
                            }


                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            ModifyCommand();
        }
        private void ProcessChuyenKhoa(int id)
        {
            var query = (from khoa in m_dtTimKiembenhNhan.AsEnumerable()
                         where
                             Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                             Utility.Int32Dbnull(Utility.Int32Dbnull(id))
                         select khoa).FirstOrDefault();
            if (query != null)
            {
                    query["id_buong"] = -1;
                    query["ten_buong"] =string.Empty;
                    query[NoitruDmucGiuongbenh.Columns.IdGiuong] = -1;
                    query["ten_giuong"] =string.Empty;
              
              //  query["IDBuonggiuong"] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
            }

        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            LayLichsuBuongGiuong();
            ModifyCommand();
        }
        DataTable m_dtBuongGiuong = null;
        void LayLichsuBuongGiuong()
        {
            try
            {

                m_dtBuongGiuong = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)), Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBuongGiuong, false, true, "1=1", NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ShowLSuBuongGiuong();
            }
        }
        void ShowLSuBuongGiuong()
        {
            if (!Utility.isValidGrid(grdList) || grdBuongGiuong.GetDataRows().Length <= 1)
            {
                grdBuongGiuong.Width = 0;
            }
            else
            {
                grdBuongGiuong.Width = 425;
            }
        }
        /// <summary>
        /// hàm thực hiện việc lịch sử thông tin của phân buồng giường
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLichSu_Click(object sender, EventArgs e)
        {
            // string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            //int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            //SqlQuery sqlQuery = new Select().From<KcbLuotkham>()
            //    .Where(KcbLuotkham.Columns.MaLuotkham)
            //    .IsEqualTo(MaLuotkham)
            //    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan);

            //KcbLuotkham objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
            //if (objPatientExam != null)
            //{
            //    frm_LichSuBuongGiuong frm = new frm_LichSuBuongGiuong();
            //    frm.objPatientExam = objPatientExam;
            //    frm.ShowDialog();
            //}
           
        }
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtPatientCode.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtPatientCode.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtPatientCode.Text);
                txtPatientCode.Text = MaLuotkham;
                txtPatientCode.Select(txtPatientCode.Text.Length, txtPatientCode.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlyphanbuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtPatientCode.Focus();
                txtPatientCode.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdThemMoiBN.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdSuaThongTinBN.PerformClick();
        }
        private bool IsValidHuyKhoanoitru()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            NoitruPhieudieutri _NoitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (_NoitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại khoa nội trú đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            NoitruPhanbuonggiuong _NoitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (_NoitruPhanbuonggiuong != null && Utility.Int32Dbnull(_NoitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép phân buồng giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        private void cmdHuychuyenkhoa_Click(object sender, EventArgs e)
        {
            if (!IsValidHuyKhoanoitru()) return;
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy chuyển khoa nội trú. Sau khi hủy, Bệnh nhân sẽ quay về trạng thái khoa-buồng-giường trước đó", "Thông báo", true))
            {
                int IdChuyen = -1;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    objPhanbuonggiuong.IdBuong = -1;
                    objPhanbuonggiuong.IdGiuong = -1;
                    ActionResult actionResult = new noitru_nhapvien().HuyKhoanoitru(objPhanbuonggiuong, ref IdChuyen);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            if(IdChuyen>0)
                            {
                                DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(IdChuyen).GetDataSet().Tables[0];
                                if (dtTemp.Rows.Count > 0)
                                {
                                    DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                    Utility.CopyData(dtTemp.Rows[0], ref dr);
                                    m_dtTimKiembenhNhan.AcceptChanges();
                                }
                            }
                            else//Xóa dòng hiện tại
                            {
                                DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                m_dtTimKiembenhNhan.Rows.Remove(dr);
                                m_dtTimKiembenhNhan.AcceptChanges();
                            }

                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            ModifyCommand();
        }

        private void cmdNhapvien_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    KcbLuotkham _KcbLuotkham = new Select().From(KcbLuotkham.Schema)
            //                        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan),-1))
            //                        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),"-1")).ExecuteSingle<KcbLuotkham>();
            //    if (_KcbLuotkham == null)
            //    {
            //        Utility.ShowMsg("Bạn phải chọn một bệnh nhân cần nhập viện");
            //        return;
            //    }
            //    frm_Nhapvien frm = new frm_Nhapvien();
            //    frm.id_kham = -1;//Nhập viện do y tá phân buồng giường// Utility.Int32Dbnull(txtExam_ID.Text, -1);

            //    frm.objLuotkham = _KcbLuotkham;
            //    frm.ShowDialog();
            //    if (frm.b_Cancel)
            //    {
            //        _KcbLuotkham.IdKhoanoitru = Utility.Int16Dbnull(frm.objLuotkham.IdKhoanoitru);
            //        _KcbLuotkham.SoBenhAn = Utility.sDbnull(frm.objLuotkham.SoBenhAn);
            //        _KcbLuotkham.TrangthaiNoitru = frm.objLuotkham.TrangthaiNoitru;
            //        _KcbLuotkham.NgayNhapvien = frm.objLuotkham.NgayNhapvien;
            //        _KcbLuotkham.MotaNhapvien = frm.objLuotkham.MotaNhapvien;
            //        DataRow dr =((DataRowView) grdList.CurrentRow.DataRow).Row;
            //        if (dr != null)
            //        {
            //            dr[NoitruPhanbuonggiuong.Columns.IdKhoanoitru] = _KcbLuotkham.IdKhoanoitru;
            //            dr[NoitruPhanbuonggiuong.Columns.NgayVaokhoa] = _KcbLuotkham.IdKhoanoitru;
            //            dr[NoitruPhanbuonggiuong.Columns.trang] = _KcbLuotkham.IdKhoanoitru;
            //            dr[NoitruPhanbuonggiuong.Columns.IdKhoanoitru] = _KcbLuotkham.IdKhoanoitru;
            //            dr[NoitruPhanbuonggiuong.Columns.IdKhoanoitru] = _KcbLuotkham.IdKhoanoitru;
            //        }
                   

            //    }
            //    ModifyCommmand();
            //}
            //catch (Exception)
            //{
            //    // throw;
            //}
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
        }
     
    }
}
