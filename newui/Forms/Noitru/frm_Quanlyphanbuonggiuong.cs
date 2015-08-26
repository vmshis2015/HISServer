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
        DataTable m_dtKhoanoitru = null;
        public frm_Quanlyphanbuonggiuong()
        {
            InitializeComponent();
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
            this.cmdPhanGiuong.Click += new System.EventHandler(this.cmdPhanGiuong_Click);
            this.cmdHuyphangiuong.Click += new System.EventHandler(this.cmdHuyphangiuong_Click);
            this.cmdChuyenKhoa.Click += new System.EventHandler(this.cmdChuyenKhoa_Click);
            this.cmdHuychuyenkhoa.Click += new System.EventHandler(this.cmdHuychuyenkhoa_Click);
            this.cmdChuyenGiuong.Click += new System.EventHandler(this.cmdChuyenGiuong_Click);
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            this.Load += new System.EventHandler(this.frm_Quanlyphanbuonggiuong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanlyphanbuonggiuong_KeyDown);
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
            cmdThemMoiBN.Click+=cmdThemMoiBN_Click;
            cmdSuaThongTinBN.Click+=cmdSuaThongTinBN_Click;
            cmdXoaBN.Click+=cmdXoaBN_Click;
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
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            m_dtKhoanoitru= THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);
            DataBinding.BindDataCombobox(cboKhoaChuyenDen, m_dtKhoanoitru,
                                                 DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                 "---Chọn khoa nội trú---", false,false);

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
            cmdSuaThongTinBN.Enabled = isValid &&  Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.TrangthaiCapcuu)) > 0;
            cmdXoaBN.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.TrangthaiCapcuu)) > 0;

            cmdNhapvien.Enabled = cmdNhapvien.Visible=isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdKhoanoitru)) <= 0;
            cmdPhanGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdHuyphangiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdChuyenKhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 ;//&& Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdHuychuyenkhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdChuyen)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdChuyenGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
        }

        private string _rowFilter = "1=1";
        private void TimKiemThongTin()
        {
            if (cboKhoaChuyenDen.Items.Count <= 0)
            {
                Utility.ShowMsg("Người dùng đang sử dụng chưa được gắn với khoa nội trú nào nên không thể tìm kiếm. Đề nghị kiểm tra lại");
                return;
            }
            m_dtTimKiembenhNhan =SPs.NoitruTimkiembenhnhan(Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue,-1),
                                                txtPatientCode.Text, 1,
                                                chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                string.Empty, (Int16) (chkCapcuu.Checked?1:-1),-1,0).
                    GetDataSet().Tables[0];
                if (m_dtBuongGiuong != null) m_dtBuongGiuong.Clear();
            Utility.SetDataSourceForDataGridEx(grdList, m_dtTimKiembenhNhan, true, true, "1=1", "");
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
            frm_Taobenhnhancapcuu _Taobenhnhancapcuu = new frm_Taobenhnhancapcuu();
            _Taobenhnhancapcuu.m_enAction = action.Insert;
            _Taobenhnhancapcuu.m_dtPatient = m_dtTimKiembenhNhan;
            _Taobenhnhancapcuu.grdList = grdList;
            _Taobenhnhancapcuu._OnActionSuccess += _Taobenhnhancapcuu__OnActionSuccess;
            _Taobenhnhancapcuu.ShowDialog();
        }

        void _Taobenhnhancapcuu__OnActionSuccess()
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc sửa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            frm_Taobenhnhancapcuu _Taobenhnhancapcuu = new frm_Taobenhnhancapcuu();
            _Taobenhnhancapcuu.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            _Taobenhnhancapcuu.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            _Taobenhnhancapcuu.m_enAction = action.Update;
            _Taobenhnhancapcuu.m_dtPatient = m_dtTimKiembenhNhan;
            _Taobenhnhancapcuu.grdList = grdList;
            _Taobenhnhancapcuu._OnActionSuccess += _Taobenhnhancapcuu__OnActionSuccess;
            _Taobenhnhancapcuu.ShowDialog();
        }
        /// <summary>
        /// hàm thực hiện việc ký quĩ thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKiQui_Click(object sender, EventArgs e)
        {
            

        }

        private bool isValidData_ChuyenKhoa()
        {
           string  MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
           int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
           KcbLuotkham _KcbLuotkham = Utility.getKcbLuotkham(IdBenhnhan, MaLuotkham);
           if (_KcbLuotkham ==null)
           {
               Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
               grdList.Focus();
               return false;
           }
           if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) <= 0)
           {
               Utility.ShowMsg("Bệnh nhân chưa vào viện, Bạn không thể thực hiện chức năng chuyển khoa", "Thông báo", MessageBoxIcon.Warning);
               grdList.Focus();
               return false;
           }
           if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) == 4)
           {
               Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
               grdList.Focus();
               return false;
           }
           if (_KcbLuotkham.TrangthaiNoitru == 5)
           {
               Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
               return false;
           }
           if (Utility.Byte2Bool(_KcbLuotkham.TthaiThanhtoannoitru) || _KcbLuotkham.TrangthaiNoitru == 6)
           {
               Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
               return false;
           }
            return true;
        }
        private bool isValidData_ChuyenGiuong()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham _KcbLuotkham = Utility.getKcbLuotkham(IdBenhnhan, MaLuotkham);
            if (_KcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) <= 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện nên không thể chuyển giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (_KcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(_KcbLuotkham.TthaiThanhtoannoitru) || _KcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            NoitruPhanbuonggiuong _NoitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (_NoitruPhanbuonggiuong != null && Utility.Int32Dbnull(_NoitruPhanbuonggiuong.IdBuong, -1) < 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa phân buồng giường nên bạn không thể chuyển giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
       
        private bool isValidData_Phanbuonggiuong()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            int _IdKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            KcbLuotkham _KcbLuotkham = Utility.getKcbLuotkham(IdBenhnhan, MaLuotkham);
            if (_KcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) <= 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện nên không thể phân buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (_KcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(_KcbLuotkham.TthaiThanhtoannoitru) || _KcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (m_dtKhoanoitru == null || m_dtKhoanoitru.Rows.Count <= 0 || m_dtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + _IdKhoanoitru.ToString()).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được phân buồng giường cho Bệnh nhân của khoa khác. Đề nghị bạn kiểm tra lại");
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
                if(!isValidData_ChuyenKhoa())return;
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
                if (!isValidData_ChuyenGiuong()) return;
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
                if (!isValidData_Phanbuonggiuong()) return;
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
        private bool isValidData_Huygiuong()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            int _IdKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham _KcbLuotkham = Utility.getKcbLuotkham(IdBenhnhan, MaLuotkham);
            if (_KcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (_KcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(_KcbLuotkham.TthaiThanhtoannoitru) || _KcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (m_dtKhoanoitru == null || m_dtKhoanoitru.Rows.Count<=0 || m_dtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + _IdKhoanoitru.ToString()).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được quyền hủy giường của khoa này. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
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
                Utility.ShowMsg("Bạn không được phép hủy giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
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
            if (!isValidData_Huygiuong()) return;
            if (Utility.AcceptQuestion("Bạn có muốn hủy phần buồng giường cho bệnh nhân đang chọn không?","Thông báo", true))
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
                //Lấy tất cả lịch sử buồng giường
                m_dtBuongGiuong = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)), Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)),"-1");
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
        private bool isValidData_HuyKhoa()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham _KcbLuotkham = Utility.getKcbLuotkham(IdBenhnhan, MaLuotkham);
            if (_KcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(_KcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (_KcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(_KcbLuotkham.TthaiThanhtoannoitru) || _KcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
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
                Utility.ShowMsg("Bạn không được phép hủy chuyển khoa cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        private void cmdHuychuyenkhoa_Click(object sender, EventArgs e)
        {
            if (!isValidData_HuyKhoa()) return;
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
        bool isValidDeleteData()
        {
            try
            {
                string v_MaLuotkham =
              Utility.sDbnull(
                grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                  "");
                int v_Patient_ID =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                NoitruPhanbuonggiuongCollection LstNoitruPhanbuonggiuong=new Select().From(NoitruPhanbuonggiuong.Schema)
                    .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                    .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                    .ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
               
                if (LstNoitruPhanbuonggiuong != null && LstNoitruPhanbuonggiuong.Count > 1)
                {
                    Utility.ShowMsg( "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể xóa thông tin");
                    return false;
                }

                NoitruTamung objNoitruTamung = new Select().From(NoitruTamung.Schema)
                   .Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                   .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                   .ExecuteSingle<NoitruTamung>();

                if (objNoitruTamung != null )
                {
                    Utility.ShowMsg("Bệnh nhân đã nộp tiền tạm ứng nên bạn không thể xóa thông tin");
                    return false;
                }
                NoitruPhieudieutri objNoitruPhieudieutri = new Select().From(NoitruPhieudieutri.Schema)
                  .Where(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                  .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                  .ExecuteSingle<NoitruPhieudieutri>();

                if (objNoitruPhieudieutri != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã Lập phiếu điều trị nên bạn không thể xóa thông tin");
                    return false;
                }
                KcbDonthuoc objKcbDonthuoc = new Select().From(KcbDonthuoc.Schema)
                  .Where(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                  .And(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                  .ExecuteSingle<KcbDonthuoc>();

                if (objKcbDonthuoc != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã được kê đơn thuốc nên bạn không thể xóa thông tin");
                    return false;
                }
                KcbChidinhcl objKcbChidinhcl = new Select().From(KcbChidinhcl.Schema)
                  .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                  .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                  .ExecuteSingle<KcbChidinhcl>();

                if (objKcbChidinhcl != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã được lập phiếu chỉ định nên bạn không thể xóa thông tin");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân", ex);
                return false;
            }
        }
        private void cmdXoaBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân cấp cứu để xóa");
                    return;
                }
                string ErrMgs = "";
                string v_MaLuotkham =
                   Utility.sDbnull(
                     grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                       "");
                int v_Patient_ID =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);

                if (!isValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa Bệnh nhân cấp cứu này không", "Thông báo", true))
                {
                    ActionResult actionResult = new KCB_DANGKY().PerformActionDeletePatientExam(v_MaLuotkham,
                                                                                                       v_Patient_ID, ref ErrMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            try
                            {
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Delete();
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                grdList_SelectionChanged(grdList, e);

                            }
                            catch
                            {

                            }
                            m_dtTimKiembenhNhan.AcceptChanges();
                            Utility.ShowMsg("Xóa Bệnh nhân cấp cứu thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (ErrMgs != "")
                                Utility.ShowMsg(ErrMgs);
                            else
                                Utility.ShowMsg("Bệnh nhân đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần khám này", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }
                ModifyCommand();
            }
            catch
            {
            }
            finally
            {

            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
        }
     
    }
}
