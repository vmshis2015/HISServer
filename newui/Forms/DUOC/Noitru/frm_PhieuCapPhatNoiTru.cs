using System;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;

using CrystalDecisions.Shared;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Baocao;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhieucapphatNoitru : Form
    {
       
        private int _ID_CAPPHAT = -1;
        private DataTable dtDrugList = new DataTable();
        private DataTable dtList = new DataTable();
        DataTable dtStaff = new DataTable();
        DataTable m_dtDataDepartment = new DataTable();
        //private Nlog.Logger log;
        string Noisudung = "KHOA";
        string KIEU_THUOC_VT = "THUOC";
        int loaiphieu = 0;
        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của form
        /// </summary>
        /// <param name="TYPE"></param>
        public frm_PhieucapphatNoitru(string param)
        {
            InitializeComponent();
            if (param.Split('-').Length > 1)
            {
                Noisudung = param.Split('-')[0];
                KIEU_THUOC_VT = param.Split('-')[1];
            }
            InitEvents();
            grdDrugList.CellValueChanged += new ColumnActionEventHandler(grdDrugList_CellValueChanged);
            grdList.SelectionChanged += grdList_SelectionChanged;
            cboStatus.SelectedIndex = 0;

            dtpToDate.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
        }

        void InitEvents()
        {
            cmdConfig.Click+=new EventHandler(cmdConfig_Click);
            cmdPhatThuoc.Click += cmdPhatThuoc_Click;
            cmdHuyDonThuoc.Click += cmdHuyDonThuoc_Click;
            cmdInsotamtra.Click += cmdInsotamtra_Click;
            cmdBenhnhanLinhthuoc.Click += cmdBenhnhanLinhthuoc_Click;
        }

        void cmdBenhnhanLinhthuoc_Click(object sender, EventArgs e)
        {
            frm_PhatThuocBN_Noitru _PhatThuocBN_Noitru = new frm_PhatThuocBN_Noitru(KIEU_THUOC_VT);
            _PhatThuocBN_Noitru.Startup(Utility.sDbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdCapphat].Value, "-1"));
            _PhatThuocBN_Noitru.ShowDialog();
        }

        void cmdInsotamtra_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu lĩnh thuốc nội trú trên lưới trước khi thực hiện lệnh in");
                    grdList.MoveFirst();
                    return;
                }
                TPhieuCapphatNoitru _item = TPhieuCapphatNoitru.FetchByID(_ID_CAPPHAT);
                if (_item == null)
                {
                    Utility.ShowMsg("Phiếu bạn chọn đã bị người khác tác động xóa mất. Đề nghị nhấn tìm kiếm để thử kiểm tra lại");
                    return;
                }
                DataTable dtData=SPs.ThuocNoitruInsotamtra(Utility.Int32Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdCapphat].Value, -1)).GetDataSet().Tables[0];
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu để in.");
                    return;
                }
                Utility.AddColumToDataTable(ref dtData, "STT", typeof(Int32));
                int idx = 1;
                foreach (DataRow dr in dtData.Rows)
                {
                    dr["STT"] = idx;
                    idx++;
                }
                string ten_khoa =Utility.sDbnull( Utility.getValueOfGridCell(grdList, "ten_khoaphong"),"Unknown");
                string loai_phieu = Utility.sDbnull(Utility.getValueOfGridCell(grdList, "loai_phieu"), "0")=="0"?"Phiếu lĩnh thường":"Phiếu lĩnh bổ sung";
                string ngay_linh = Utility.sDbnull(Utility.getValueOfGridCell(grdList, "sngay_xacnhan"), "Unknown");
                string sfileName = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\sotamtra.xls";
                string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\" + string.Format("{0}_{1}",Utility.Bodau( ten_khoa),Utility.GetYYMMDDHHMMSS(globalVariables.SysDate)) + ".xls";
                new ExcelUtlity().WriteDataTableToExcel_SoTamTra(dtData, "sotamtra", sfileNameSave, loai_phieu, ten_khoa, ngay_linh);
            }
            catch (Exception ex)
            {
                
            }
        }

        void cmdHuyDonThuoc_Click(object sender, EventArgs e)
        {
            cmdCancelConfirm.PerformClick();
        }

        void cmdPhatThuoc_Click(object sender, EventArgs e)
        {
            cmdConfirm.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdDrugList_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            grdDrugList.UpdateData();
        }
        #region "khởi tạo thông tin của Form hiện tại"
        /// <summary>
        /// hàm thực hiện load form hiện tại
        /// </summary>
        private void InitData()
        {
            try
            {
                DataTable m_dtKhoXuat = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_TUTRUC_NOITRU();
                DataBinding.BindDataCombobox(cboKhoxuat, m_dtKhoXuat,
                                              TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn kho xuất thuốc---", true);


                if (globalVariables.IsAdmin || Noisudung=="KHO" || Noisudung=="ALL")
                {
                    m_dtDataDepartment = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                    //log.Info("Lay thong tin khoa noi tru cua admin");
                }
                else
                {
                    m_dtDataDepartment = THU_VIEN_CHUNG.Laydanhmuckhoa(globalVariablesPrivate.objKhoaphong.IdKhoaphong);
                    //log.Info("Lay thong tin khoa noi tru cua theo nguoi su dung voi Department_ID=" + globalVariables.DepartmentID + " and nguoi dang nhap ten=" + globalVariables.gv_sStaffName);
                }
                DataBinding.BindDataCombobox(cboDepartment, m_dtDataDepartment, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,"Chọn khoa phòng",true);
                LoadThongTinNhanVienTheoKhoa();
                //log.Info("lay thong tin cua nhan vien theo khoa");
               
            }
            catch
            {
            }
            finally
            {
                bln_hasloaded = true;
            }
        }
        /// <summary>
        /// hàm thực hiện việc load thông tni của nhân viên khoa
        /// </summary>
        private void LoadThongTinNhanVienTheoKhoa()
        {
            if (globalVariables.IsAdmin)
            {
                dtStaff = THU_VIEN_CHUNG.Laydanhsachnhanvien("ALL") ;

            }
            else
            {
                dtStaff = THU_VIEN_CHUNG.Laydanhsachnhanvienthuockhoa(globalVariablesPrivate.objKhoaphong.IdKhoaphong);
            }
            DataBinding.BindDataCombobox(cboStaff, dtStaff, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "Chọn nhân viên",true);
        }

        #endregion
        /// <summary>
        /// hàm thực hiện việc load form hiện tịa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhieucapphatNoitruQ_Load(object sender, EventArgs e)
        {
            cmdConfig.Visible = globalVariables.IsAdmin;
            InitData();
            CauHinh();
            cmdTimKiem_Click(cmdTimKiem, e);
        }

        private void CauHinh()
        {
           cmdInsotamtra.Visible= cmdInsert.Visible = cmdUpdate.Visible = cmdDelete.Visible =cmdCheck.Visible= Noisudung == "KHOA" || Noisudung == "ALL";
            cmdPhatThuoc.Visible = cmdHuyDonThuoc.Visible = cmdConfirm.Visible = cmdCheck.Visible = cmdCancelConfirm.Visible = Noisudung == "KHO" || Noisudung == "ALL";
            dtpToDate.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate.AddDays(-1 * PropertyLib._DuocNoitruProperties.Songayluitimphieu);
            pnlPhatthuoc.Height = Noisudung == "KHOA" || Noisudung == "ALL" ? 0 : 66;

        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin dữ liệu
        /// </summary>
        private void TimKiemDuLieu()
        {
            try
            {
                Int16 StockID =  Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1);
                if (dtList != null) dtList.Rows.Clear();
                if (dtDrugList != null) dtDrugList.Rows.Clear();
                dtList = SPs.ThuocNoitruTimkiemphieutonghopthuocnoitru(Utility.Int32Dbnull(txtID_CAPPHAT.Text, -1), (Int16)(optLinhThuong.Checked ? 0 : 1), chkByDate.Checked ? dtpFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", chkByDate.Checked ? dtpToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboStaff.SelectedValue, -1),
                                                     Utility.Int32Dbnull(cboDepartment.SelectedValue, -1), StockID,
                                                     Utility.Int32Dbnull(cboStatus.SelectedValue, -1)).GetDataSet().Tables[0];

                ProcessStatus();
                //Bỏ dòng dưới để chỉ cho phép nhận thuốc từ một kho duy nhất
                grdList.DataSource = dtList.DefaultView;
                if (grdList.RowCount > 0) grdList.Row = 0;
                grdList_SelectionChanged(grdList, new EventArgs());
                bool bhasLoad = true;
            }
            catch
            {
            }
            finally
            {
                modifyActButtons();
            }
        }

        void modifyActButtons()
        {
            try
            {
                int DA_LINH = 0;
                if (Utility.isValidGrid(grdList))DA_LINH= Utility.Int32Dbnull(grdList.CurrentRow.Cells["DA_LINH"].Value, 0);
                string THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false);
                if (THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC == "1") DA_LINH=0;//Ko quan tâm trạng thái đã lĩnh

                cmdInsert.Enabled = true;
                cmdCheck.Enabled = Utility.isValidGrid(grdList) && DA_LINH == 0 && grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.TrangThai].Value.ToString() == "0";
                cmdUpdate.Enabled = Utility.isValidGrid(grdList) && DA_LINH == 0 && grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.TrangThai].Value.ToString() == "0";
                cmdDelete.Enabled = Utility.isValidGrid(grdList) && DA_LINH == 0 && grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.TrangThai].Value.ToString() == "0";
                cmdConfirm.Enabled = cmdUpdate.Enabled;
                cmdCancelConfirm.Enabled = Utility.isValidGrid(grdList) && DA_LINH == 0 && !cmdConfirm.Enabled;
                cmdBenhnhanLinhthuoc.Enabled = Utility.isValidGrid(grdList) && grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.TrangThai].Value.ToString() == "1";
                cmdPhatThuoc.Enabled = cmdConfirm.Enabled;
                cmdHuyDonThuoc.Enabled = cmdCancelConfirm.Enabled;
                if (cmdConfirm.Enabled) cmdPhatThuoc.BringToFront();
                else cmdPhatThuoc.SendToBack();
                cmdPrint.Enabled = Utility.isValidGrid(grdList);
                cmdInsotamtra.Enabled = cmdHuyDonThuoc.Enabled;//Chỉ in sổ tam tra sau khi đã nhận thuốc từ khoa dược

            }
            catch
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemDuLieu();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_AddCapPhatThuocNoiTru frm = new frm_AddCapPhatThuocNoiTru(KIEU_THUOC_VT, loaiphieu);
            frm.m_Action = action.Insert;
            frm._OnInsertCompleted += frm__OnInsertCompleted;
            frm.dtList = dtList;
            frm.ShowDialog();
            grdList_SelectionChanged(grdList, new EventArgs());
        }

        void frm__OnInsertCompleted(long idcapphat)
        {
            Utility.GonewRowJanus(grdList, TPhieuCapphatNoitru.Columns.IdCapphat, idcapphat.ToString());
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu để sửa");
                    return;
                }
                TPhieuCapphatNoitru _item = TPhieuCapphatNoitru.FetchByID(_ID_CAPPHAT);
                if (_item == null)
                {
                    Utility.ShowMsg("Phiếu bạn chọn đã bị người khác tác động xóa mất. Đề nghị nhấn tìm kiếm để thử kiểm tra lại");
                    return;
                }
                if (DAPHATTHUOC_BENHNHAN())
                {
                    Utility.ShowMsg("Đã phát thuốc cho Bệnh nhân nên bạn không thể sửa phiếu");
                    return;
                }
                frm_AddCapPhatThuocNoiTru frm = new frm_AddCapPhatThuocNoiTru(KIEU_THUOC_VT, loaiphieu);
                frm.m_Action = action.Update;
                frm.dtList = dtList;
                frm.StaffId = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdNhanvien].Value, -1);
                frm.DepartmentId = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdKhoaLinh].Value, -1);
                frm._IDCAPPHAT = _ID_CAPPHAT;
                frm.ShowDialog();
                grdList_SelectionChanged(grdList, new EventArgs());
            }
            catch (Exception ex)
            {


            }
            finally
            {
                modifyActButtons();
            }
            

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        bool DAPHATTHUOC_BENHNHAN()
        {
            try
            {
                string THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false);
                if (THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC == "1") return false;//Ko quan tâm trạng thái đã lĩnh
                return new Select()
                    .From(TPhieuCapphatChitiet.Schema)
                    .Where(TPhieuCapphatChitiet.IdCapphatColumn).IsEqualTo(_ID_CAPPHAT)
                    .And(TPhieuCapphatChitiet.DaLinhColumn).IsEqualTo(1)
                    .ExecuteSingle<TPhieuCapphatChitiet>() != null;
            }
            catch
            {
                return true;
            }
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                _ID_CAPPHAT = -1;
                if (grdList.CurrentRow != null && grdList.RecordCount > 0)
                {
                    if (grdList.CurrentRow.RowType == RowType.Record)
                    {
                        _ID_CAPPHAT = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdCapphat].Value, -1);
                        dtDrugList = SPs.ThuocNoitruLaychitietPhieutonghopThuocnoitru(_ID_CAPPHAT, Utility.Int32Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdKhoXuat].Value, -1)).GetDataSet().Tables[0];
                        grdDrugList.DataSource = dtDrugList.DefaultView;
                        //if (PropertyLib._DuocNoitruProperties.Doituong==Doituongdung.Nhanvienkho)//Tự động load kho nếu đã được chọn sẵn
                        //{
                            int _ID_kho = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdKhoXuat].Value, -1);
                            if (_ID_kho > 0) cboKhoxuat.SelectedIndex = Utility.GetSelectedIndex(cboKhoxuat, _ID_kho.ToString());
                        //}
                    }
                    
                }
                else
                    if (dtDrugList != null) dtDrugList.Rows.Clear();
            }
            catch
            {
            }
            finally
            {
                modifyActButtons();
            }
        }

        DataTable m_dtStockList = new DataTable();
        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu để xác nhận");
                    return;
                }
                TPhieuCapphatNoitru _item = TPhieuCapphatNoitru.FetchByID(_ID_CAPPHAT);
                if (_item == null)
                {
                    Utility.ShowMsg("Phiếu bạn chọn đã bị người khác tác động xóa mất. Đề nghị nhấn tìm kiếm để thử kiểm tra lại");
                    return;
                }
                if (DAPHATTHUOC_BENHNHAN())
                {
                    Utility.ShowMsg("Đã phát thuốc cho Bệnh nhân nên bạn không thể xác nhận phiếu");
                    return;
                }
                Int16 StockID = 0;
                if (cboKhoxuat.Items.Count <= 0 || cboKhoxuat.SelectedValue.ToString() == "-1" ||cboKhoxuat.SelectedIndex<=-1)
                {
                    Utility.ShowMsg("Bạn cần chọn kho xuất");
                    cboKhoxuat.Focus();
                    return;
                }
                StockID = Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1);
                if (
                    Utility.AcceptQuestion(string.Format(
                        "Bạn có muốn lấy thuốc từ kho {0} để cấp cho phiếu đang chọn hay không?", cboKhoxuat.Text),
                        "Xác nhận ", true))
                {
                    ActionResult ActionResult = new CapphatThuocKhoa().XacnhanphieuCapphatNoitru(_ID_CAPPHAT, StockID, dtNgayPhatThuoc.Value);
                    switch (ActionResult)
                    {
                        case ActionResult.Success:

                            Utility.ShowMsg("Xác nhận cấp phát thuốc thành công");
                            DataRow dr = Utility.FetchOnebyCondition(dtList, "ID_CAPPHAT=" + _ID_CAPPHAT);
                            if (dr != null)
                            {
                                dr[TPhieuCapphatNoitru.Columns.IdKhoXuat] = -1;
                                dr["TRANG_THAI"] = 1;
                                dr["ten_trangthai"] = "Đã cấp phát";
                                dtList.AcceptChanges();
                            }
                            ProcessStatus();
                            break;
                        case ActionResult.DataChanged:
                            Utility.ShowMsg("Phiếu này đã bị người khác vừa thay đổi. Bạn cần nhấn lại nút tìm kiếm để kiểm tra lại tình trạng tồn tại của phiếu");
                            break;
                        case ActionResult.NotEnoughDrugInStock:
                            Utility.ShowMsg("Số thuốc trong kho không đủ để cấp phát\nVui lòng bấm vào nút Kiểm tra thuốc trong kho bên cạnh để xem chi tiết thuốc còn thiếu");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình cấp phát thuốc");
                            break;
                        case ActionResult.Exceed:
                            Utility.ShowMsg("Có lỗi trong quá trình cập nhật trạng thái đơn thuốc");
                            break;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                modifyActButtons();
            }
        }
        

        

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {

            if (dtpToDate.Value < dtpFromDate.Value)
            {
                dtpFromDate.Value = dtpToDate.Value;
            }


        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {

            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu để xóa");
                    return;
                }
                TPhieuCapphatNoitru _item = TPhieuCapphatNoitru.FetchByID(_ID_CAPPHAT);
                if (_item == null)
                {
                    Utility.ShowMsg("Phiếu bạn chọn đã bị người khác tác động xóa mất. Đề nghị nhấn tìm kiếm để thử kiểm tra lại");
                    return;
                }
                if (DAPHATTHUOC_BENHNHAN())
                {
                    Utility.ShowMsg("Đã phát thuốc cho Bệnh nhân nên bạn không thể xóa phiếu");
                    return;
                }
                
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa phiếu lĩnh thuốc nội trú đang chọn hay không?", "Xác nhận xóa", true))
                    return;
                ActionResult actionResult = new CapphatThuocKhoa().XoaPhieuCapPhatNoiTru(_ID_CAPPHAT);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        if (dtDrugList != null) dtDrugList.Rows.Clear();
                        DataRow[] drDetele = dtList.Select(TPhieuCapphatNoitru.Columns.IdCapphat + " = " + _ID_CAPPHAT);
                        dtList.Rows.Remove(drDetele[0]);
                        dtList.AcceptChanges();

                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xóa dữ liệu");
                        break;
                }

            }
            catch (Exception ex)
            {

                Utility.ShowMsg("Có lỗi trong quá trình xóa dữ liệu /n" + ex);
            }
            finally
            {
                modifyActButtons();
            }
        }

        private void frm_PhieucapphatNoitruQ_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu để in");
                    return;
                }
                int idcapphat = Utility.Int32Dbnull(grdList.GetValue(TPhieuCapphatNoitru.Columns.IdCapphat));
                DataTable dataTable = SPs.ThuocNoitruLaydulieuinphieulinhthuocnoitru(idcapphat).GetDataSet().Tables[0];
                TPhieuCapphatNoitru objPhieuCapphatNoitru = TPhieuCapphatNoitru.FetchByID(idcapphat);
                if (objPhieuCapphatNoitru != null)
                {
                    thuoc_baocao.Inphieutonghoplinhthuocnoitru(objPhieuCapphatNoitru, dataTable);
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy dữ liệu");
                return;
            }
        }
        private void cmdCancelConfirm_Click(object sender, EventArgs e)
        {
            PerformCancelConfirmAction();
        }

        private void PerformCancelConfirmAction()
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu để hủy xác nhận");
                    return;
                }
                
                if (_ID_CAPPHAT == -1) return;
                TPhieuCapphatNoitru _item = TPhieuCapphatNoitru.FetchByID(_ID_CAPPHAT);
                if (_item == null)
                {
                    Utility.ShowMsg("Phiếu bạn chọn đã bị người khác tác động xóa mất. Đề nghị nhấn tìm kiếm để thử kiểm tra lại");
                    return;
                }
                if (DAPHATTHUOC_BENHNHAN())
                {
                    Utility.ShowMsg("Đã phát thuốc cho Bệnh nhân nên bạn không thể hủy xác nhận phiếu");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có muốn hủy xác nhận cấp phát thuốc cho phiếu đang chọn không?", "Xác nhận hủy cấp phát theo phiếu ", true))
                {
                    short ID_KHO_XUAT = Utility.Int16Dbnull(grdList.CurrentRow.Cells[TPhieuCapphatNoitru.Columns.IdKhoXuat].Value, -1);
                    string errmsg = "";
                    ActionResult action = new CapphatThuocKhoa().HuyXacnhanphieuCapphatNoitru(_ID_CAPPHAT, ID_KHO_XUAT, ref errmsg);
                    switch (action)
                    {
                        case ActionResult.Success:
                            DataRow dr = Utility.FetchOnebyCondition(dtList, "ID_CAPPHAT=" + _ID_CAPPHAT);
                            if (dr != null)
                            {
                                dr[TPhieuCapphatNoitru.Columns.IdKhoXuat] = -1;
                                dr["TRANG_THAI"] = 0;
                                dr["ten_trangthai"] = "Chưa cấp phát";
                                dtList.AcceptChanges();
                            }
                            ProcessStatus();
                            Utility.ShowMsg("Đã thực hiện hủy cấp phát thuốc theo phiếu thành công!");
                            break;
                        case ActionResult.DataChanged:
                            Utility.ShowMsg("Phiếu này đã bị người khác vừa thay đổi. Bạn cần nhấn lại nút tìm kiếm để kiểm tra lại tình trạng tồn tại của phiếu");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Không tìm thấy chi tiết trong bảng đơn thuốc chi tiết. Đề nghị bug lại dữ liệu");
                            break;
                        case ActionResult.Exceed:
                            Utility.ShowMsg("Không xóa hết dữ liệu chi tiết của lần cấp phát bệnh nhân. Bug lại code");
                            break;
                        case ActionResult.Exception:
                            Utility.ShowMsg("Lỗi exception:\n" + errmsg);
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString().Trim());
                return;
            }
            finally
            {
                modifyActButtons();
            }
        }
        private void ProcessStatus()
        {
            try
            {
                foreach (DataRow dr in dtList.Rows)
                    if (Convert.ToInt32(dr["TRANG_THAI"]) == 0)
                    {
                        dr["ten_trangthai"] = "Chưa duyệt";
                        dr["MOTA_THEM"] = "Chưa được kho dược phát thuốc";
                    }
                    else
                    {
                        dr["ten_trangthai"] = "Đã duyệt";
                        if (Convert.ToInt32(dr["DA_LINH"]) == 0)
                        {
                            dr["MOTA_THEM"] = "Chưa phát thuốc cho Bệnh nhân";
                        }
                        else
                        {
                            if (Convert.ToInt32(dr["DA_LINH"]) == Convert.ToInt32(dr["TOTAL"]))
                                dr["MOTA_THEM"] = "Đã phát thuốc cho tất cả Bệnh nhân";
                            else
                                dr["MOTA_THEM"] = "Đang phát thuốc cho các Bệnh nhân";
                        }
                    }
                //Kiểm tra nếu là các thủ kho đang thao tác thì cần ẩn các phiếu cấp phát không liên quan đến kho đó

            }
            catch
            {
            }
        }




        /// <summary>
        /// hàm thực hiện việc chọn trạng thái của ngày tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpToDate.Enabled = dtpFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của nhân viên thuôc khoa đó
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bln_hasloaded) return;
            LoadThongTinNhanVienTheoKhoa();
        }

        private void frm_PhieucapphatNoitruQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                cmdInsert.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                cmdUpdate.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                cmdConfirm.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                cmdCancelConfirm.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                cmdDelete.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                cmdPrint.PerformClick();
            }
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDrugList_SelectionChanged(object sender, EventArgs e)
        {



        }
        /// <summary>
        /// hàm thực hiện việc di chuyển thông tin thay đổi dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void grdList_SelectionChanged_1(object sender, EventArgs e)
        {

        }

        private void cmdXacNhan_Click(object sender, EventArgs e)
        {

        }

        private void cmdSelectStock_Click_1(object sender, EventArgs e)
        {

        }
        bool bln_hasloaded = false;
        private void cboKhoxuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bln_hasloaded) return;
                //if (cboKhoxuat.Items.Count > 0 && Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1)>0 && cmdUpdate.Enabled && PropertyLib._DuocNoitruProperties.Doituong==Doituongdung.Yta)
                //{
                //        new Update(TPhieuCapphatNoitru.Schema).Set(TPhieuCapphatNoitru.IdKhoXuatColumn).EqualTo(Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1)).Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(_ID_CAPPHAT).Execute();
                //        DataRow[] arrDR = dtList.Select("ID_CAPPHAT=" + _ID_CAPPHAT);
                //        if (arrDR.Length > 0)
                //        {
                //            arrDR[0][TPhieuCapphatNoitru.IdKhoXuatColumn.ColumnName] = Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1);
                //        }
                //}
            }
            catch
            {
            }
        }

        private void cmdCheck_Click(object sender, EventArgs e)
        {
            if (cboKhoxuat.SelectedValue.ToString() == "-1")
            {
                Utility.ShowMsg("Bạn cần chọn kho xuất");
                cboKhoxuat.Focus();
                return;
            }
            TPhieuCapphatNoitru _item = TPhieuCapphatNoitru.FetchByID(_ID_CAPPHAT);
            if (_item == null)
            {
                Utility.ShowMsg("Phiếu bạn chọn đã bị người khác tác động xóa mất. Đề nghị nhấn tìm kiếm để thử kiểm tra lại");
                return;
            }
            ActionResult ActionResult = new CapphatThuocKhoa().Kiemtratonthuoc(_ID_CAPPHAT, Utility.Int16Dbnull(cboKhoxuat.SelectedValue, -1));
            switch (ActionResult)
            {
                case ActionResult.Success:
                    Utility.ShowMsg("Thuốc trong kho còn đủ để cấp phát");
                    break;
                case ActionResult.NotEnoughDrugInStock:
                    //Đã thông báo
                    break;
                case ActionResult.UNKNOW:
                    Utility.ShowMsg("Tồn tại thuốc trong đơn cấp phát đã bị xóa khỏi bảng danh mục thuốc. Mời bạn kiểm tra lại!");
                    break;

                    break;
            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties _Properties = new frm_Properties(PropertyLib._DuocNoitruProperties);
                _Properties.ShowDialog();
                CauHinh();
            }
            catch (Exception exception)
            {

            }
        }

       
    }
}