using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.DAL;

using VNS.Libs;
using Microsoft.VisualBasic;
using SortOrder = System.Windows.Forms.SortOrder;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VNS.HIS.UI.THUOC
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frm_PhieuTrathuoc_KhoLeVeKhoChan : Form
    {
        #region "khai báo biến của phần trả lại thuốc"

        public string KIEU_THUOC_VT = "THUOC";

        private DataTable m_dtDataDonThuoc, p_dtPhieuNhapTra = new DataTable();
        private int Distance = 488;
        private bool b_Hasloaded = false;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuXuatBN.txt"));
      //  private DataTable m_dtDataDonThuoc = new DataTable();
        private DataTable m_dtDataTraLaiDetail = new DataTable();

        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        #endregion
        #region "khởi tạo thông tin của kho thuốc"
        public frm_PhieuTrathuoc_KhoLeVeKhoChan(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            dtToDate.Value  = dtFromdate.Value = globalVariables.SysDate;
            
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            grdList.SelectionChanged+=new EventHandler(grdPres_SelectionChanged);
            txtMaPhieuTraKho.Click+=new EventHandler(txtPres_ID_Click);
            txtMaPhieuTraKho.KeyDown+=new KeyEventHandler(txtPres_ID_KeyDown);
        }

        #endregion
        /// <summary>
        /// hàm thực hiện việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemThongTinDonThuoc();
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        private void ModifyCommand()
        {
            bool isValidGid = grdList.RowCount > 0 && grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record;
            bool CHUAXACNHAN = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.TrangThai), 0) == 0;
            cmdUpdatePhieuNhap.Enabled = cmdXoaPhieuNhap.Enabled = CHUAXACNHAN && isValidGid;
            cmdXacNhan.Enabled = CHUAXACNHAN && isValidGid;
            cmdHuyTralai.Enabled = !cmdXacNhan.Enabled  && isValidGid;
            cmdInDonThuoc.Enabled = isValidGid;
        }
        private void TimKiemThongTinDonThuoc()
        {
            int TRANG_THAI = -1;
            if (radDaXacNhan.Checked) TRANG_THAI = 1;
            if (radChuaXacNhan.Checked) TRANG_THAI = 0;
            if (radTatCa.Checked) TRANG_THAI = -1;

            m_dtDataDonThuoc =
                SPs.ThuocLaydanhsachphieutrathuockholevekhochan(
                                          chkByDate.Checked
                                              ? dtFromdate.Value.ToString("dd/MM/yyyy")
                                              : "01/01/1900",
                                          chkByDate.Checked
                                              ? dtToDate.Value.ToString("dd/MM/yyyy")
                                              : "01/01/1900", TRANG_THAI,
                                               Utility.Int32Dbnull(cboKhoTra.SelectedValue, -1),
                                          Utility.Int32Dbnull(cboKhoLinh.SelectedValue, -1),
                                          -1,
                                          Utility.sDbnull(txtMaPhieuTraKho.Text, -1),(int?) LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan
                                          ).GetDataSet().Tables[0];

            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDataDonThuoc, true, true, "1=1", "");
            Utility.SetGridEXSortKey(grdList, TPhieutrathuocKholeVekhochan.Columns.IdPhieu,
                                     Janus.Windows.GridEX.SortOrder.Ascending);
          
         
            if (grdList.RowCount > 0)
            {
                grdList.MoveFirst();
            }
           
            ModifyCommand();
        }
        private void WriteSlipterContaiter()
        {
           // SplitterDistance = splitContainer1.SplitterDistance;
            System.IO.File.WriteAllText(FileName, SplitterDistance.ToString());
        }

        private void ReadSliper()
        {
            if (System.IO.File.Exists(FileName))
            {
                SplitterDistance = Utility.Int32Dbnull(System.IO.File.ReadAllLines(FileName)[0]);
            }
            else
            {
                WriteSlipterContaiter();
            }
            //splitContainer1.SplitterDistance = SplitterDistance;
        }
        private DataTable m_dtKhoTraLai, m_dtKhoLinh,m_dtKhoaNoiTru = new DataTable();
        private void frm_PhieuTraLai_Kho_Load(object sender, EventArgs e)
        {
            CauHinh();
            cmdCauHinh.Visible = globalVariables.IsAdmin;
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoLinh = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
                m_dtKhoTraLai = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
            }
            else
            {
                m_dtKhoLinh = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                m_dtKhoTraLai = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" });
            }
            DataBinding.BindDataCombobox(cboKhoTra, m_dtKhoTraLai,
                                  TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,"---Kho trả lại---",false);
            DataBinding.BindDataCombobox(cboKhoLinh, m_dtKhoLinh,
                                 TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,"----Kho lĩnh",false);
          
            TimKiemThongTinDonThuoc();
            b_Hasloaded = true;
        }

        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPres_ID_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc dichuyeen thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu));
                // dtNgayPhatThuoc.Value = Convert.ToDateTime(grdPres.GetValue(TPrescription.Columns.PresDate));
                GetDataPresDetail();
            }
            else
            {
                grdPhieuNhapChiTiet.DataSource = null;
            }
            ModifyCommand();
        }
        private void txtPres_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.Int32Dbnull(txtMaPhieuTraKho.Text) > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdSearch.PerformClick();
                }
            }

        }
        private int IdPhieu = -1;
        private void GetDataPresDetail()
        {
            //int stock_id = Utility.Int32Dbnull(grdList.GetValue(TPrescription.Columns.StockId));
            IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu));
            m_dtDataTraLaiDetail = SPs.ThuocLaychitietphieunhaptrakholevekhochan(IdPhieu).GetDataSet().Tables[0];
            if (!m_dtDataTraLaiDetail.Columns.Contains("CHON")) m_dtDataTraLaiDetail.Columns.Add("CHON", typeof(int));
            foreach (DataRow dr in m_dtDataTraLaiDetail.Rows)
            {
                dr["CHON"] = 0;
            }
            m_dtDataTraLaiDetail.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdPhieuNhapChiTiet, m_dtDataTraLaiDetail, false, true, "1=1",
                                                 "");
          

            ///thực hiện việc thay đổi khi load thông tin của thành tiền
          //  UpdateDataWhenChanged();
        }

        private void cmdInDonThuoc_Click(object sender, EventArgs e)
        {
            //IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPrescription.Columns.PresId));
            int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
            TPhieutrathuocKholeVekhochan objPhieuNhap = TPhieutrathuocKholeVekhochan.FetchByID(IDPhieuNhap);
            if (objPhieuNhap != null)
            {
                VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuTraKholeVeKhochan(IDPhieuNhap, "PHIẾU NHẬP TRẢ", globalVariables.SysDate);
            }
        }
        

      
        
     
       
        private bool IsValid4UpdateDeleteXoa()
        {
            int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
            SqlQuery sqlQuery = new Select().From(TPhieutrathuocKholeVekhochan.Schema)
                .Where(TPhieutrathuocKholeVekhochan.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieutrathuocKholeVekhochan.Columns.IdPhieu).IsEqualTo(IDPhieuNhap);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Phiếu trả thuốc đang chọn đã xác nhận. Bạn không thể sửa hoặc xóa thông tin",false);
                return false;
            }
            return true;
        }
        private DataTable m_dtDataNhapTraKho=new DataTable();
        private void cmdXoaPhieuNhap_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "",false);
            if (!IsValid4UpdateDeleteXoa()) return;
            int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
            if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa phiếu trả thuốc đang chọn hay không?", IDPhieuNhap), "Thông báo", true))
            {
               
                ActionResult actionResult = new PhieuTraLai().XoaPhieuNhapTraKho(IDPhieuNhap);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        m_dtDataNhapTraKho.AcceptChanges();
                        Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Xóa phiếu trả thuốc thành công", false);
                        break;
                    case ActionResult.Error:
                        break;
                }

            }
            ModifyCommand();
        }

        private void cmdExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phiếu nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            frm_themmoi_NhaptraKholeVeKhochan frm = new frm_themmoi_NhaptraKholeVeKhochan(KIEU_THUOC_VT);
            frm.txtIDPhieuNhapKho.Text = "-1";
            frm.em_Action = action.Insert;
            frm.grdList = grdList;
            frm.p_dtPhieuNhapTra = m_dtDataDonThuoc;
            frm.ShowDialog();
            if(frm.b_Cancel)
            {
                grdPres_SelectionChanged(grdList,new EventArgs());
            }
        }
        private bool IsValid4UpdateDelete()
        {
            int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
            SqlQuery sqlQuery = new Select().From(TPhieutrathuocKholeVekhochan.Schema)
                .Where(TPhieutrathuocKholeVekhochan.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieutrathuocKholeVekhochan.Columns.IdPhieu).IsEqualTo(IDPhieuNhap);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu trả đã được xác nhận, Bạn không thể sửa hoặc xóa thông tin", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hhienj iệc cập nhập thông tin của phiếu nhập kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePhieuNhap_Click(object sender, EventArgs e)
        {
            if(!IsValid4UpdateDelete())return;
            int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
            frm_themmoi_NhaptraKholeVeKhochan frm = new frm_themmoi_NhaptraKholeVeKhochan(KIEU_THUOC_VT);
            frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(IDPhieuNhap);
            frm.em_Action = action.Update;
            frm.grdList = grdList;
            frm.p_dtPhieuNhapTra = m_dtDataDonThuoc;
            frm.ShowDialog();
            if (frm.b_Cancel)
            {
                grdPres_SelectionChanged(grdList, new EventArgs());
            }
        }

        private void frm_PhieuTraLai_Kho_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdSearch.PerformClick();
            if(e.KeyCode==Keys.F4)cmdInDonThuoc.PerformClick();
            if(e.KeyCode==Keys.N&&e.Control)cmdThemPhieuNhap.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdatePhieuNhap.PerformClick();
            if(e.KeyCode==Keys.D&&e.Control)cmdXoaPhieuNhap.PerformClick();
            //if(e.KeyCode==Keys.F3)cmdSearch.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc xác nhạn thông tin 
        /// trừ vào kho khi xác nhận
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXacNhan_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            if (Utility.AcceptQuestion("Bạn có muốn xác nhận phieus trả thuốc từ kho lẻ về kho chẵn?\nSau khi trả, thuốc sẽ bị trừ khỏi kho lẻ(kho xuất) và cộng vào kho chẵn(kho nhập)", "Thông báo", true))
            {
                int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
                TPhieutrathuocKholeVekhochan objDPhieuNhap = TPhieutrathuocKholeVekhochan.FetchByID(IDPhieuNhap);
                if (objDPhieuNhap != null)
                {
                    DateTime _ngayxacnhan = globalVariables.SysDate;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "1")
                    {
                        frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                        _ChonngayXacnhan.pdt_InputDate = objDPhieuNhap.NgayTra.Value;
                        _ChonngayXacnhan.ShowDialog();
                        if (_ChonngayXacnhan.b_Cancel)
                            return;
                        else
                        _ngayxacnhan = _ChonngayXacnhan.pdt_InputDate;
                    }
                    ActionResult actionResult =
                        new PhieuTraLai().XacNhanTraLaiKhoLeVeKhoChan(objDPhieuNhap, _ngayxacnhan);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Trả thuốc từ kho lẻ về kho chẵn thành công", false);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieutrathuocKholeVekhochan.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.Cells[TPhieutrathuocKholeVekhochan.Columns.NgayXacnhan].Value = _ngayxacnhan;
                            grdList.CurrentRow.Cells[TPhieutrathuocKholeVekhochan.Columns.NguoiXacnhan].Value = globalVariables.UserName;
                            grdList.CurrentRow.EndEdit();
                            break;
                        case ActionResult.Error:
                            break;
                    }
                }
            }
            ModifyCommand();
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._ChuyenkhoProperties);
                
                frm.ShowDialog();
                CauHinh();

                //grdPresDetail.SaveLayoutFile();
            }
            catch (Exception exception)
            {

            }
        }

        private void CauHinh()
        {

            cmdThemPhieuNhap.Visible = PropertyLib._ChuyenkhoProperties.QuyenThemPhieu;
            cmdUpdatePhieuNhap.Visible = PropertyLib._ChuyenkhoProperties.QuyenSuaPhieu;
            cmdXoaPhieuNhap.Visible = PropertyLib._ChuyenkhoProperties.QuyenXoaPhieu;
            cmdXacNhan.Visible = PropertyLib._ChuyenkhoProperties.QuyenXacNhanPhieu;
        }

        private void cmdHuyTralai_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            if (Utility.AcceptQuestion("Bạn có muốn hủy trả thuốc từ kho lẻ về kho chẵn?\nSau khi hủy, thuốc được cộng lại kho lẻ(kho xuất) và trừ khỏi kho chẵn(kho nhập)?", "Thông báo", true))
            {
                int IDPhieuNhaptra = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
                TPhieutrathuocKholeVekhochan objTPhieutrathuocKholeVekhochan = TPhieutrathuocKholeVekhochan.FetchByID(IDPhieuNhaptra);
                if (objTPhieutrathuocKholeVekhochan != null)
                {
                    ActionResult actionResult =
                        new PhieuTraLai().HuyXacNhanPhieuTralaiKho(objTPhieutrathuocKholeVekhochan);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Hủy trả thuốc từ kho lẻ về kho chẵn thành công", false);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieutrathuocKholeVekhochan.Columns.TrangThai].Value = 0;
                            grdList.CurrentRow.Cells[TPhieutrathuocKholeVekhochan.Columns.NgayXacnhan].Value = DBNull.Value;
                            grdList.CurrentRow.Cells[TPhieutrathuocKholeVekhochan.Columns.NguoiXacnhan].Value = DBNull.Value;
                            grdList.CurrentRow.EndEdit();
                            break;
                        case ActionResult.Exceed:
                            Utility.ShowMsg("Thuốc trong kho nhận đã được sử dụng hết nên bạn không thể hủy phiếu trả", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                        case ActionResult.NotEnoughDrugInStock:
                            Utility.ShowMsg("Thuốc trong kho nhận(chẵn) đã được sử dụng nên không đủ số lượng để hoàn trả lại kho xuất(lẻ)", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                        case ActionResult.Error:
                            break;
                    }
                }

            }
            ModifyCommand();
        }
    }
}

