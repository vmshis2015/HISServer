using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.THUOC
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frm_PhieuNhapKho : Form
    {
        #region "khai báo biến"
        THUOC_NHAPKHO _NHAPKHO = new THUOC_NHAPKHO();
        private int Distance = 488;
        private bool b_Hasloaded = false;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuNhapKho.txt"));
        private DataTable m_dtDataNhapKho=new DataTable();
        string KIEU_THUOC_VT = "THUOC";
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        #endregion

        #region "Khởi tạo Form"
        public frm_PhieuNhapKho(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
            //txtTieuDe.Text = BusinessHelper.GetTieuDeBaoCao(this.Name, txtTieuDe.Text);
            dtFromdate.Value = globalVariables.SysDate.AddMonths(-1);
            dtToDate.Value = globalVariables.SysDate;
            CauHinh();
        }
        private void txtTieuDe_LostFocus(object sender, EventArgs eventArgs)
        {
            //BusinessHelper.UpdateTieuDe(this.Name, txtTieuDe.Text);
        }
        private void CauHinh()
        {
          
            cmdConfig.Visible = globalVariables.IsAdmin;
            
          
        }
        #endregion

     
       
        /// <summary>
        /// hàm thực hiện viecj load thong tin của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuNhapKho_Load(object sender, EventArgs e)
        {
            InitData();
            TIMKIEM_THONGTIN();
            b_Hasloaded = true;
            ModifyCommand();
           
        }
        private DataTable m_dtKhoThuoc=new DataTable();
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của Form
        /// </summary>
        private void InitData()
        {
            txtNhacungcap.Init();

            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoThuoc = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
            }
            else
            {
                m_dtKhoThuoc = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
            }
            if (m_dtKhoThuoc.Rows.Count > 1)
                DataBinding.BindDataCombobox(cboKhoThuoc, m_dtKhoThuoc,
                                           TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                           "---Chọn kho---",false);
            else
                DataBinding.BindDataCombobox(cboKhoThuoc, m_dtKhoThuoc,
                                       TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn---", true);

        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc trạng thái của phần từ ngay tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }

        private void frm_PhieuNhapKho_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.N && e.Control)cmdThemPhieuNhap.PerformClick();
            if(e.KeyCode==Keys.E && e.Control)cmdUpdatePhieuNhap.PerformClick();
            if(e.KeyCode==Keys.D && e.Control)cmdXoaPhieuNhap.PerformClick();
            if(e.KeyCode==Keys.P && e.Control)cmdInPhieuNhapKho.PerformClick();

            if (e.KeyCode == Keys.X && e.Control) cmdNhapKho.PerformClick();
            if (e.KeyCode == Keys.Z && e.Control) cmdHuyXacnhan.PerformClick();

            if (e.KeyCode == Keys.F3 || (e.KeyCode == Keys.F && e.Control)) cmdSearch.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc tim kiem thong tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TIMKIEM_THONGTIN();
            
        }
        /// <summary>
        /// HÀM THỰC HIỆN TÌM KIẾM THÔNG TIN 
        /// </summary>
        private void TIMKIEM_THONGTIN()
        {
            int TRANG_THAI = -1;
            if(radDaNhap.Checked) TRANG_THAI = 1;
            if(radChuaNhapKho.Checked) TRANG_THAI = 0;
            string manhacungcap="ALL";
            if(Utility.DoTrim(txtNhacungcap.myCode)!="")
                manhacungcap=Utility.DoTrim(txtNhacungcap.myCode);
            string MaKho = "-1";
            
            m_dtDataNhapKho =
                _NHAPKHO.Laydanhsachphieunhapkho(chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                             chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",-1, -1,
                                             Utility.Int32Dbnull(cboKhoThuoc.SelectedValue, -1), -1,
                                             Utility.Int32Dbnull(cboNhanVien.SelectedValue, -1),
                                             -1, manhacungcap, Utility.sDbnull(txtSoPhieu.Text), TRANG_THAI, (int)LoaiPhieu.PhieuNhapKho, MaKho, 2, KIEU_THUOC_VT);

            Utility.SetDataSourceForDataGridEx_Basic(grdList,m_dtDataNhapKho,true,true,"1=1","");
            if (!Utility.isValidGrid(grdList)) if (m_dtDataPhieuChiTiet != null) m_dtDataPhieuChiTiet.Clear();
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                using (frm_Themmoi_Phieunhapkho frm = new frm_Themmoi_Phieunhapkho())
                {
                    frm.em_Action = action.Insert;
                    frm._OnActionSuccess += frm__OnActionSuccess;
                    frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                    frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                    frm.grdList = grdList;
                    frm.txtIDPhieuNhapKho.Text = "-1";
                    frm.ShowDialog();
                    if (!frm.b_Cancel)
                    {
                        grdList_SelectionChanged(grdList, new EventArgs());
                    }
                }
            }
            catch (Exception exception)
            {
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
              
            }
            finally
            {
                ModifyCommand();
            }
            
        }

        void frm__OnActionSuccess()
        {
            grdList_SelectionChanged(grdList, new EventArgs());
        }
        private bool InValiUpdateXoa()
        {
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuoc.Schema)
                .Where(TPhieuNhapxuatthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IdPhieu);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Phiếu nhập kho đang chọn đã được xác nhận. Bạn không thể sửa hoặc xóa thông tin","Thông báo",MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện cập nhâp thôn tin phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if(!InValiUpdateXoa())return;
                int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                using (frm_Themmoi_Phieunhapkho frm = new frm_Themmoi_Phieunhapkho())
                {
                    frm._OnActionSuccess += frm__OnActionSuccess;
                    frm.em_Action = action.Update;
                    frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                    frm.grdList = grdList;
                    frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                    frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(ITPhieuNhapxuatthuoc);

                    frm.ShowDialog();
                    if (!frm.b_Cancel)
                    {
                        grdList_SelectionChanged(grdList, new EventArgs());
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }

            }
            finally
            {
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin chi tiết của phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaPhieuNhap_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            if (!InValiUpdateXoa())return;
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa thông tin phiếu nhập kho với mã phiếu {0} hay không?", ITPhieuNhapxuatthuoc),"Thông báo",true))
            {
                ActionResult actionResult = _NHAPKHO.XoaPhieuNhapKho(ITPhieuNhapxuatthuoc);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        m_dtDataNhapKho.AcceptChanges();
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn xóa thông tin phiếu nhập kho thành công", false);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình xóa thông tin của phiếu nhập kho", "Thông báo lỗi", MessageBoxIcon.Error);
                        break;
                }

            }
            ModifyCommand();
        }
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if(grdList.CurrentRow!=null&&grdList.CurrentRow.RowType==RowType.Record)
            {
                int IDPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                m_dtDataPhieuChiTiet =
                       _NHAPKHO.LaythongtinChitietPhieunhapKho(IDPhieu);
                Utility.SetDataSourceForDataGridEx(grdPhieuNhapChiTiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
            }
            else
            {
                grdPhieuNhapChiTiet.DataSource = null;
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện viêc in phiếu nhập kho thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
                if (objPhieuNhap != null)
                {
                    VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuNhapkho(IdPhieu, "PHIẾU NHẬP", globalVariables.SysDate);
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi in phiếu nhập kho:\n" + ex.Message);
            }
          

        }
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC XÁC NHẬN NHẬP KHO THUỐC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                cmdNhapKho.Enabled = false;
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "0")
                    if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xác nhận phiếu nhập kho đang chọn hay không?\nSau khi xác nhận, thuốc sẽ được cộng vào trong kho nhập", "Thông báo", true))
                    {
                        return;
                    }

                int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(ITPhieuNhapxuatthuoc);
                if (objTPhieuNhapxuatthuoc != null)
                {
                    DateTime _ngayxacnhan = globalVariables.SysDate;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "1")
                    {
                        frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                        _ChonngayXacnhan.pdt_InputDate = objTPhieuNhapxuatthuoc.NgayHoadon;
                        _ChonngayXacnhan.ShowDialog();
                        if (_ChonngayXacnhan.b_Cancel)
                            return;
                        else
                            _ngayxacnhan = _ChonngayXacnhan.pdt_InputDate;
                    }
                    ActionResult actionResult =
                        _NHAPKHO.XacNhanPhieuNhapKho(objTPhieuNhapxuatthuoc, _ngayxacnhan);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Xác nhận phiếu nhập kho thành công", false);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = _ngayxacnhan;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = globalVariables.UserName;
                            grdList.CurrentRow.EndEdit();
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi khi xác nhận phiếu nhập kho thuốc", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }



            }
            catch (Exception)
            {


            }
            finally
            {
                ModifyCommand();
            }
            
        }
        private void ModifyCommand()
        {
            cmdThemPhieuNhap.Enabled = true;
            bool isvalidGrid = Utility.isValidGrid(grdList);
            cmdInPhieuNhapKho.Enabled = isvalidGrid;
            int Trang_thai = 0;
            if (isvalidGrid) 
                Trang_thai= Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.TrangThai), 0);
            cmdXoaPhieuNhap.Enabled = isvalidGrid && Trang_thai == 0;
            cmdUpdatePhieuNhap.Enabled = cmdXoaPhieuNhap.Enabled;
            cmdNhapKho.Enabled = isvalidGrid && Trang_thai == 0;
            cmdHuyXacnhan.Enabled = isvalidGrid && !cmdNhapKho.Enabled;

        }
        /// <summary>
        /// hàm thực hiện việc cho phép lọc thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void cmdHuyXacnhan_Click(object sender, EventArgs e)
        {
            try
            {
                cmdHuyXacnhan.Enabled = false;
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
                if (Utility.AcceptQuestion("Bạn có muốn hủy xác nhận phiếu nhập kho đang chọn hay không?\nSau khi hủy, thuốc sẽ được trừ ra khỏi kho nhập", "Thông báo", true))
                {
                    int ITPhieuNhapxuatthuoc = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                    TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(ITPhieuNhapxuatthuoc);
                    if (objTPhieuNhapxuatthuoc != null)
                    {
                        string errMsg = "";
                        ActionResult actionResult =
                            _NHAPKHO.HuyXacNhanPhieuNhapKho(objTPhieuNhapxuatthuoc, ref errMsg);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện hủy nhập kho thành công", false);
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 0;
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = DBNull.Value;
                                grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = DBNull.Value;
                                grdList.CurrentRow.EndEdit();
                                break;
                            case ActionResult.Exceed:
                                Utility.ShowMsg("Thuốc nhập đã được sử dụng hết nên bạn không thể hủy phiếu nhập", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.NotEnoughDrugInStock:
                                Utility.ShowMsg("Thuốc nhập đã được sử dụng nên bạn không thể hủy phiếu nhập", "Thông báo lỗi", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Error:
                                break;
                        }
                    }

                }
               
            }
            catch (Exception)
            {
                ModifyCommand();
            }
            
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._NhapkhoProperties);
                frm.ShowDialog();
                CauHinh();

            }
            catch (Exception exception)
            {

            }
        }
    }
}
