using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.THUOC;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhieuHuythuoc : Form
    {
      
        private bool b_Hasloaded = false;
        private DataTable m_dtDataNhapKho = new DataTable();
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        
        private int id_PhieuNhap;
        
        public string KIEU_THUOC_VT = "THUOC";
        public frm_PhieuHuythuoc(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            InitEvents();
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
            dtFromdate.Value = globalVariables.SysDate.AddDays(-7);
            dtToDate.Value = globalVariables.SysDate;
            CauHinh();
        }
        void InitEvents()
        {
            this.cmdThemPhieuNhap.Click += new System.EventHandler(this.cmdThemPhieuNhap_Click);
            this.cmdUpdatePhieuNhap.Click += new System.EventHandler(this.cmdUpdatePhieuNhap_Click);
            this.cmdXoaPhieuNhap.Click += new System.EventHandler(this.cmdXoaPhieuNhap_Click);
            this.cmdNhapKho.Click += new System.EventHandler(this.cmdNhapKho_Click);
            this.cmdHuychuyenkho.Click += new System.EventHandler(this.cmdHuychuyenkho_Click);
            this.cmdInPhieuNhapKho.Click += new System.EventHandler(this.cmdInPhieuNhapKho_Click);
            this.cmdCauhinh.Click += new System.EventHandler(this.cmdCauhinh_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            this.txtSoPhieu.TextChanged += new System.EventHandler(this.txtSoPhieu_TextChanged);
            this.txtSoPhieu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoPhieu_KeyDown);
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            this.Load += new System.EventHandler(this.frm_PhieuHuythuoc_Load);
            this.KeyDown += new KeyEventHandler(frm_PhieuHuythuoc_KeyDown);
            
            grdList.ApplyingFilter += new CancelEventHandler(grdList_ApplyingFilter);
            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieuHuythuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Control) cmdThemPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdatePhieuNhap.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaPhieuNhap.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuNhapKho.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
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
            if (radDaNhap.Checked) TRANG_THAI = 1;
            if (radChuaNhapKho.Checked) TRANG_THAI = 0;
            if (radTatCa.Checked) TRANG_THAI = -1;
            string MaKho = "-1";
           

            m_dtDataNhapKho =
                new THUOC_NHAPKHO().Laydanhsachphieunhapkho(chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") :"01/01/1900",
                                             chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",Utility.Int32Dbnull(txtthuoc.MyID,-1), -1,
                                            -1, Utility.Int32Dbnull(cboKhohuy.SelectedValue, -1),
                                             Utility.Int32Dbnull(cboNhanVien.SelectedValue, -1),
                                             -1, "-1", Utility.sDbnull(txtSoPhieu.Text), TRANG_THAI,(int) LoaiPhieu.PhieuHuy, MaKho, KIEU_THUOC_VT);

            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDataNhapKho, true, true, "1=1", "");
            if (!Utility.isValidGrid(grdList)) if(m_dtDataPhieuChiTiet!=null) m_dtDataPhieuChiTiet.Clear();
            Utility.SetGridEXSortKey(grdList, TPhieuNhapxuatthuoc.Columns.NgayHoadon,
                                    Janus.Windows.GridEX.SortOrder.Ascending);

            ModifyCommand();
        }
        private void ModifyCommand()
        {
            bool isValidGid = grdList.RowCount > 0 && grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record;
            bool CHUAXACNHAN = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.TrangThai), 0) == 0;
            cmdUpdatePhieuNhap.Enabled = cmdXoaPhieuNhap.Enabled = CHUAXACNHAN && isValidGid;
            cmdNhapKho.Enabled = CHUAXACNHAN && isValidGid;
            cmdHuychuyenkho.Enabled = !cmdNhapKho.Enabled;
            cmdInPhieuNhapKho.Enabled = isValidGid;
            if (!Utility.isValidGrid(grdList))
            {
                if (Utility.isValidDatatable(m_dtDataPhieuChiTiet, false)) m_dtDataPhieuChiTiet.Rows.Clear();
            }
        }
        /// <summary>
        /// hàm thực hiện việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// trạng thái của khi chọn toàn bọ hoặc bỏ ngày tháng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        private bool IsValid4UpdateDelete()
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuoc.Schema)
                .Where(TPhieuNhapxuatthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IdPhieu);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Phiếu hủy thuốc đang chọn đã xác nhận, Bạn không thể sửa hoặc xóa thông tin", false);
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện xóa thông tin của hiếu nhập chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaPhieuNhap_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
            if (!IsValid4UpdateDelete()) return;
            if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa thông tin phiếu hủy thuốc với mã phiếu {0}\n hay không?", IdPhieu), "Thông báo", true))
            {
                ActionResult actionResult = new THUOC_NHAPKHO().XoaPhieuNhapKho(IdPhieu);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.Delete();
                        grdList.UpdateData();
                        m_dtDataNhapKho.AcceptChanges();
                        Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Bạn xóa thông tin phiếu hủy thuốc thành công", false);
                        break;
                    case ActionResult.Error:
                        break;
                }

            }
            ModifyCommand();
        }

        private void frm_PhieuHuythuoc_Load(object sender, EventArgs e)
        {
            IntialData();
            AutocompleteThuoc();
            TIMKIEM_THONGTIN();
            ModifyCommand();
            cmdCauhinh.Visible = globalVariables.IsAdmin;
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
        private DataTable  m_dtKhoXuat = new DataTable();
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của Form
        /// </summary>
        private void IntialData()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                m_dtKhoXuat = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA();
            }
            else
            {
                m_dtKhoXuat = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA();
            }
            DataBinding.BindDataCombobox(cboKhohuy, m_dtKhoXuat,
                                   TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,
                                   "---Kho xuất---", true);
        }
        /// <summary>
        /// hà thực hiện việc in phiêu xuat kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhapKho_Click(object sender, EventArgs e)
        {
            id_PhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);

            TPhieuNhapxuatthuoc objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(id_PhieuNhap);
            if (objPhieuNhap != null)
            {
                VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuHuythuoc(id_PhieuNhap, "BIÊN BẢN HỦY THUỐC", globalVariables.SysDate);
            }
           

        }
        /// <summary>
        /// hàm thực hiện việc cho phép chuyển thông tin xác nhận vào kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNhapKho_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
                if (objTPhieuNhapxuatthuoc != null)
                {
                    string errMsg = "";
                    DateTime _ngayxacnhan = globalVariables.SysDate;
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_HIENTHI_NGAYXACNHAN", "0", false) == "1")
                    {
                        frm_ChonngayXacnhan _ChonngayXacnhan = new frm_ChonngayXacnhan();
                        _ChonngayXacnhan.pdt_InputDate = objTPhieuNhapxuatthuoc.NgayHoadon;
                        _ChonngayXacnhan.ShowDialog();
                        _ngayxacnhan = _ChonngayXacnhan.pdt_InputDate;
                    }
                    ActionResult actionResult =
                        new XuatThuoc().XacNhanPhieuHuy_thanhly_thuoc(objTPhieuNhapxuatthuoc, _ngayxacnhan, ref errMsg);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Xác nhận phiếu hủy thuốc thành công", false);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = _ngayxacnhan;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = globalVariables.UserName;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 1;
                            grdList.CurrentRow.EndEdit();
                            break;
                        case ActionResult.Exceed:
                            Utility.ShowMsg("Không có thuốc trong kho xuất nên không thể xác nhận phiếu hủy\n" + errMsg, "Thông báo lỗi", MessageBoxIcon.Warning);
                            break;
                        case ActionResult.NotEnoughDrugInStock:
                            Utility.ShowMsg("Thuốc trong kho xuất không còn đủ số lượng nên không thể xác nhận phiếu hủy\n" + errMsg, "Thông báo lỗi", MessageBoxIcon.Warning);
                            break;
                        case ActionResult.Error:
                            break;
                    }
                }
                
                ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện việc cho phép lọc thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc di chuyển thông tin của trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                int IDPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                m_dtDataPhieuChiTiet =
                       new THUOC_NHAPKHO().LaythongtinChitietPhieunhapKho(IDPhieu);
                Utility.SetDataSourceForDataGridEx(grdPhieuNhapChiTiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
            }
            else
            {
                grdPhieuNhapChiTiet.DataSource = null;
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc thêm phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_phieuhuythuoc frm = new frm_themmoi_phieuhuythuoc();
                frm.m_enAction = action.Insert;
                frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                frm.grdList = grdList;
                frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                frm.txtIDPhieuNhapKho.Text = "-1";
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, new EventArgs());
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
        /// hàm thực hiện việc cho phép thông tin của phiếu nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid4UpdateDelete()) return;
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                frm_themmoi_phieuhuythuoc frm = new frm_themmoi_phieuhuythuoc();
                frm.m_enAction = action.Update;
                frm.KIEU_THUOC_VT = KIEU_THUOC_VT;
                frm.grdList = grdList;
                frm.p_mDataPhieuNhapKho = m_dtDataNhapKho;
                frm.txtIDPhieuNhapKho.Text = Utility.sDbnull(IdPhieu);

                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, new EventArgs());
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
        /// hàm thực hiện việc tì kiếm thông tin sucar phiếu xuất kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSoPhieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoPhieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSearch.PerformClick();
            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
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
            cmdNhapKho.Visible = PropertyLib._ChuyenkhoProperties.QuyenXacNhanPhieu;
            cmdHuychuyenkho.Visible = PropertyLib._ChuyenkhoProperties.QuyenHuyXacNhanPhieu;
            cmdInPhieuNhapKho.Visible = PropertyLib._ChuyenkhoProperties.QuyenInphieu;
        }

        private void cmdHuychuyenkho_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "", false);
            if (Utility.AcceptQuestion("Bạn có muốn hủy xác nhận phiếu Hủy thuốc đang chọn này không?", "Thông báo", true))
            {
                int IdPhieu = Utility.Int32Dbnull(grdList.GetValue(TPhieuNhapxuatthuoc.Columns.IdPhieu), -1);
                TPhieuNhapxuatthuoc objTPhieuNhapxuatthuoc = TPhieuNhapxuatthuoc.FetchByID(IdPhieu);
                if (objTPhieuNhapxuatthuoc != null)
                {
                    string errMsg = "";
                    ActionResult actionResult =
                        new XuatThuoc().HuyXacNhanPhieuHuy_thanhly_Thuoc(objTPhieuNhapxuatthuoc, ref errMsg);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.SetMsg(uiStatusBar2.Panels["MSG"], "Bạn thực hiện hủy xác nhận phiếu hủy thuốc thành công", false);
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.TrangThai].Value = 0;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NgayXacnhan].Value = DBNull.Value;
                            grdList.CurrentRow.Cells[TPhieuNhapxuatthuoc.Columns.NguoiXacnhan].Value = DBNull.Value;
                            grdList.CurrentRow.EndEdit();
                            break;
                        
                        case ActionResult.Error:
                            break;
                    }
                }

            }
            ModifyCommand();
        }

        private void cmdCauhinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties(PropertyLib._ChuyenkhoProperties);
                frm.ShowDialog();
                CauHinh();

                //grdPresDetail.SaveLayoutFile();
            }
            catch (Exception exception)
            {

            }
        }
    }
}
