using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using VNS.Libs;
using SubSonic;
using VNS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using Microsoft.VisualBasic;
using System.IO;
using TriState = Janus.Windows.GridEX.TriState;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_phatthuoc_ngoaitru : Form
    {
        private int Distance = 488;
        private bool b_Hasloaded = false;
        private string FileName = string.Format("{0}/{1}", Application.StartupPath, string.Format("SplitterDistancefrm_PhieuXuatBN.txt"));
        private DataTable m_dtDataDonThuoc = new DataTable();
        private DataTable m_dtDataPresDetail = new DataTable();
        string kieuthuoc_vt = "THUOC";
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }
        public frm_phatthuoc_ngoaitru(string kieuthuoc_vt)
        {
            InitializeComponent();
            this.kieuthuoc_vt = kieuthuoc_vt;
            InitEvents();
            
            dtFromdate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
           
           
            dtNgayPhatThuoc.Value = globalVariables.SysDate;
            CauHinh();
            cmdConfig.Visible = globalVariables.IsAdmin;
        }
        void InitEvents()
        {
            
            this.Load += new System.EventHandler(this.frm_phatthuoc_ngoaitru_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_phatthuoc_ngoaitru_KeyDown);
            this.txtPres_ID.Click += new System.EventHandler(this.txtPres_ID_Click);
            this.txtPres_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPres_ID_KeyDown);
            this.txtPID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPID_KeyDown_1);
            
            this.radTatCa.CheckedChanged += new System.EventHandler(this.radTatCa_CheckedChanged);
            this.radChuaXacNhan.CheckedChanged += new System.EventHandler(this.radChuaXacNhan_CheckedChanged);
            this.radDaXacNhan.CheckedChanged += new System.EventHandler(this.radDaXacNhan_CheckedChanged);
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
           

            grdPresDetail.FormattingRow += new RowLoadEventHandler(grdPresDetail_FormattingRow);
            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdPhatThuoc.Click+=new EventHandler(cmdPhatThuoc_Click);
            cmdHuyDonThuoc.Click+=new EventHandler(cmdHuyDonThuoc_Click);
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            cmdKiemTraSoLuong.Click+=new EventHandler(cmdKiemTraSoLuong_Click);
            grdPresDetail.FormattingRow+=new RowLoadEventHandler(grdPresDetail_FormattingRow);
            grdPres.ApplyingFilter+=new CancelEventHandler(grdPres_ApplyingFilter);
            grdPres.SelectionChanged+=new EventHandler(grdPres_SelectionChanged);
        }

       
        private void CauHinh()
        {

          
            cmdHuyDonThuoc.Visible = PropertyLib._HisDuocProperties.HuyXacNhan;
          
           
            
            dtNgayPhatThuoc.Enabled = PropertyLib._HisDuocProperties.ChoPhepChinhNgayDuyet;
            

            grdPresDetail.RootTable.Groups.Clear();
            if (PropertyLib._ThamKhamProperties.Hienthinhomthuoc)
            {
                GridEXColumn gridExColumn = grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                var gridExGroup = new GridEXGroup(gridExColumn);
                gridExGroup.GroupPrefix = "Loại thuốc: ";
                grdPresDetail.RootTable.Groups.Add(gridExGroup);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private DataTable m_dtKhothuoc=new DataTable();
        private void frm_phatthuoc_ngoaitru_Load(object sender, EventArgs e)
        {
            if (kieuthuoc_vt == "THUOC")
                m_dtKhothuoc = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
            else
                m_dtKhothuoc = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU" });
            DataBinding.BindData(cboKho, m_dtKhothuoc,
                                     TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
            b_Hasloaded = true;
            dtNgayPhatThuoc.Value = globalVariables.SysDate;
            TimKiemThongTinDonThuoc();
            ModifyCommand();
            txtPID.Focus();
            txtPID.SelectAll();

        }
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin của đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemThongTinDonThuoc();
        }
        private void TimKiemThongTinDonThuoc()
        {
            try
            {
                int Status = -1;
                if (radChuaXacNhan.Checked) Status = 0;
                if (radDaXacNhan.Checked) Status = 1;
                int NoiTru = 0;
                m_dtDataDonThuoc =
                    SPs.ThuocTimkiemdonthuocCapphatngoaitru(Utility.Int32Dbnull(txtPres_ID.Text, -1), txtPID.Text,
                                                           Utility.sDbnull(txtTenBN.Text),"ALL",
                                                           chkByDate.Checked ? dtFromdate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                           chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", Status,
                                                            Utility.Int32Dbnull(cboKho.SelectedValue),kieuthuoc_vt).
                        GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx(grdPres, m_dtDataDonThuoc, true, true, "1=1", "ten_benhnhan");
                RowFilterView();
                if (grdPres.GetDataRows().Length <= 0)
               {
                   m_dtDataPresDetail.Rows.Clear();
               }
                ModifyCommand();
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
        /// <summary>
        /// hàm thực hiện việc lọc filter của dược
        /// </summary>
        private void RowFilterView()
        {
            if (PropertyLib._HisDuocProperties.LocDonThuocKhiDuyet)
            {
                string rowFilter = "1=1";

                if (radChuaXacNhan.Checked) rowFilter = string.Format("{0}={1}", KcbDonthuoc.Columns.TrangThai, 0);
                if (radDaXacNhan.Checked) rowFilter = string.Format("{0}={1}", KcbDonthuoc.Columns.TrangThai, 1);
                try
                {
                    m_dtDataDonThuoc.DefaultView.RowFilter = rowFilter;
                    m_dtDataDonThuoc.AcceptChanges();
                }
                catch (Exception)
                {

                }
            }
            
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPres_ID_Click(object sender, EventArgs e)
        {

        }

        private void txtPres_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if(Utility.Int32Dbnull(txtPres_ID.Text)>0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdSearch.PerformClick();
                }
            }
           
        }
        /// <summary>
        /// hàm thực hiện việc dichuyeen thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (Utility.isValidGrid(grdPres))
                {
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    GetDataPresDetail();
                }
                else
                {
                    grdPresDetail.DataSource = null;
                }
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
                
            }
            ModifyCommand();
        }
        private void ModifyCommand()
        {
            try
            {
                if (!Utility.isValidGrid(grdPres) || !Utility.isValidGrid(grdPresDetail))
                {
                    cmdPhatThuoc.Enabled = false;
                    cmdHuyDonThuoc.Enabled = false;
                    cmdKiemTraSoLuong.Enabled = false;
                }
                else
                {
                    int _daphat = m_dtDataPresDetail.Select(KcbDonthuocChitiet.Columns.TrangThai + "=1").Length;// Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.TrangThai));
                    cmdPhatThuoc.Enabled = _daphat <= 0;
                    cmdHuyDonThuoc.Enabled = !cmdPhatThuoc.Enabled;
                    cmdHuyDonThuoc.Enabled = Utility.Coquyen("quyen_huycapphatthuoc_ngoaitru");
                    cmdKiemTraSoLuong.Enabled = _daphat <= 0;
                }
            }
            catch (Exception)
            {
            }
        }
        private int Pres_ID=-1;
        private void GetDataPresDetail()
        {

            int stock_id = -1;
           
            m_dtDataPresDetail = SPs.DonthuocLaychitietCapphat(Pres_ID,Utility.Int32Dbnull(cboKho.SelectedValue,-1),kieuthuoc_vt).GetDataSet().Tables[0];
            if (!m_dtDataPresDetail.Columns.Contains("CHON")) m_dtDataPresDetail.Columns.Add("CHON", typeof(int));
            foreach (DataRow dr in m_dtDataPresDetail.Rows)
            {
                dr["CHON"] = 0;
            }
            m_dtDataPresDetail.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDataPresDetail, false, true, "1=1",
                                                 KcbDonthuocChitiet.Columns.SttIn);
            //Add2View
          //  Add2View();
            ModifyCommand();
        }
        void Add2View()
        {
            try
            {
                DataTable dtView = m_dtDataPresDetail.Clone();
                foreach (DataRow dr in m_dtDataPresDetail.Rows)
                {
                    int id_thuoc = Utility.Int32Dbnull(dr["id_thuoc"], -1);
                    DataRow[] arrDr = dtView.Select("id_thuoc=" + id_thuoc.ToString()+" ");
                    if (arrDr.Length <= 0)
                        dtView.ImportRow(dr);
                    else
                    {
                        arrDr[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(arrDr[0][KcbDonthuocChitiet.Columns.SoLuong]) + Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong]);
                    }
                }
                dtView.AcceptChanges();
                string _rowFilter = string.Format("id_donthuoc={0}", Pres_ID);
                Utility.SetDataSourceForDataGridEx(grdPresDetail, dtView, false, true, "1=1",
                                                     KcbDonthuocChitiet.Columns.SttIn);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phím tắt thực hiện tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_phatthuoc_ngoaitru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F2) 
            {
                txtPID.Clear();
                txtPID.Focus();
            }
            if (e.KeyCode == Keys.F5)
            {
                grdPres_SelectionChanged(grdPres, new EventArgs());
            }
            if (e.Control && e.KeyCode == Keys.S) cmdPhatThuoc_Click(cmdPhatThuoc, new EventArgs());
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin theo đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPhatThuoc_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(uiStatusBar2.Panels[1], "", false);
           
            if (!InValiKiemTraDonThuoc()) return;
            //if(Utility.AcceptQuestion("Bạn có muốn thực hiện việc xác nhận lĩnh thuốc cho bệnh nhân không\n Nếu bạn xác nhận thuốc sẽ trừ vào kho mà bạn đang chọn","thông báo",true))
            //{
                long Pres_ID = Utility.Int64Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
                Int16 stockID = Utility.Int16Dbnull(m_dtDataPresDetail.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                ActionResult actionResult =
                    new XuatThuoc().LinhThuocBenhNhan(Pres_ID, stockID, dtNgayPhatThuoc.Value);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        UpdateHasConfirm();
                        Utility.SetMsg(uiStatusBar2.Panels[1], "Bạn thực hiện việc phát thuốc thành công",false);
                        break;
                    case ActionResult.NotEnoughDrugInStock:
                        Utility.SetMsg(uiStatusBar2.Panels[1], "Thuốc không đủ cấp phát. Mời bạn kiểm tra lại", true);
                        break;
                    case ActionResult.Error:
                        Utility.SetMsg(uiStatusBar2.Panels[1], "Lỗi trong quá trình phát thuốc cho bệnh nhân", true);
                        break;
                }
            //}
        }

        private void UpdateHasConfirm()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value = 1;
                    gridExRow.EndEdit();
                }
                grdPresDetail.UpdateData();
                m_dtDataPresDetail.AcceptChanges();
                var query = from donthuoc in grdPresDetail.GetDataRows().AsEnumerable()
                            where
                                Utility.Int32Dbnull(donthuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                        .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (PropertyLib._HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                }

            }
            catch (Exception)
            {
                
                //throw;
            }

        }
        /// <summary>
        /// hàm thực hiện việc update thông tin hủy
        /// </summary>
        private void UpdateHuyHasConfirm()
        {
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDonthuocChitiet.Columns.TrangThai].Value = 0;
                    gridExRow.EndEdit();
                }
                grdPresDetail.UpdateData();
                m_dtDataPresDetail.AcceptChanges();
                var query = from donthuoc in grdPresDetail.GetDataRows().AsEnumerable()
                            where
                                Utility.Int32Dbnull(donthuoc.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value) == Pres_ID
                            select donthuoc;
                if (query.Any())
                {
                    Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                        .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    if (PropertyLib._HisDuocProperties.KieuDuyetDonThuoc == "DONTHUOC")
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                    else
                    {
                        grdPres.CurrentRow.BeginEdit();
                        grdPres.CurrentRow.Cells[KcbDonthuoc.Columns.TrangThai].Value = status;
                        grdPres.CurrentRow.EndEdit();
                        grdPres.UpdateData();
                        m_dtDataDonThuoc.AcceptChanges();
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }
        private bool InValiDonthuoc()
        {
            
            int Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
            SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                .Where(KcbDonthuoc.Columns.TrangThai).IsEqualTo(1)
                .And(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(Pres_ID);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Đơn thuốc đã phát thuốc,Mời bạn xem lại thông tin ","Thông báo",MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool InValiHuyDonthuoc()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Không tìm thấy chi tiết đơn thuốc. Bạn cần chọn ít nhất 1 đơn thuốc có chi tiết để thao tác", "Thông báo", MessageBoxIcon.Error);

                return false;
            }
            int tthai_chot = Utility.Int32Dbnull(grdPres.GetValue("tthai_chot"), -1);
            if (tthai_chot == 1)
            {
                Utility.ShowMsg("Đơn thuốc đã được chốt nên không thể hủy. Đề nghị bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            if (m_dtDataPresDetail.Select(KcbDonthuocChitiet.Columns.TrangThai + "=1").Length<=0)
            {
                Utility.ShowMsg("Đơn thuốc chưa phát thuốc nên không thể hủy. Đề nghị bạn kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// /hàm thực hiện việc khởi tạo thông tin của phiếu xuất cho bệnh nhân
        /// </summary>
        /// <param name="objPrescription"></param>
        /// <returns></returns>
       
        private TPhieuXuatthuocBenhnhanChitiet []CreatePhieuXuaChiTiet()
        {
            int length = 0;
            int idx = 0;
            var arrPhieuXuatCT = new TPhieuXuatthuocBenhnhanChitiet[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    arrPhieuXuatCT[idx]=new TPhieuXuatthuocBenhnhanChitiet();
                    arrPhieuXuatCT[idx].ChiDan =Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
                    arrPhieuXuatCT[idx].SoLuong = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                    arrPhieuXuatCT[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,-1);
                    arrPhieuXuatCT[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
                 
                    
                    idx++;
                }
            }
            return arrPhieuXuatCT;
        }

        private void cmdInDonThuoc_Click(object sender, EventArgs e)
        {
           
        }
       
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC SỐ LƯỢNG TỒN THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPresDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
           

        }
        /// <summary>
        /// hàm thưc hiện việc kiểm tra thông tin của kho có đủ thuốc không 
        /// Nếu không đủ không cho phát thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdKiemTraSoLuong_Click(object sender, EventArgs e)
        {
            if(!InValiKiemTraDonThuoc())return;
            else
            {
                Utility.ShowMsg("Bạn có thể xác nhận phiếu lĩnh thuốc của bệnh nhân\n Mời bạn phát thuốc","Thông báo",MessageBoxIcon.Information);
            }
        }

        private bool InValiKiemTraDonThuoc()
        {
            try
            {
                string id_loaidoituong_kcb = Utility.GetValueFromGridColumn(grdPres, "id_loaidoituong_kcb");
                string inphieu_dct = Utility.GetValueFromGridColumn(grdPres, "inphieu_dct");
                string dathanhtoan = Utility.GetValueFromGridColumn(grdPres, "dathanhtoan");
                if (id_loaidoituong_kcb == "1")
                {
                    if (dathanhtoan == "0")
                    {
                        Utility.ShowMsg(string.Format("Đối tượng bệnh nhân Dịch vụ đang chọn chưa thanh toán đơn thuốc nên bạn không thể thực hiện cấp phát.\nĐề nghị bệnh nhân đi nộp tiền thanh toán trước khi quay lại lĩnh thuốc"));
                        return false;
                    }
                }
                else
                {
                    if (inphieu_dct == "0")
                    {
                        Utility.ShowMsg(string.Format("Đối tượng bệnh nhân BHYT đang chọn chưa in phôi BHYT nên bạn không thể thực hiện cấp phát thuốc.\nĐề nghị bệnh nhân đến quầy thanh toán in phôi BHYT trước khi quay lại lĩnh thuốc"));
                        return false;
                    }
                }
                foreach (DataRow dr in m_dtDataPresDetail.Rows)
                {
                    long id_donthuoc = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc]);
                    int id_thuoc = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc]);
                    string Drug_name = Utility.sDbnull(dr["tenthuoc_bietduoc"]);
                    int id_kho = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdKho]);
                    int id_thuockho = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.IdThuockho]);
                    int so_luong = Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong]);
                    int SoLuongTon = CommonLoadDuoc.SoLuongTonTrongKho(id_donthuoc, id_kho, id_thuoc, id_thuockho, 0, (byte)0);//Ko cần kiểm tra chờ xác nhận
                    if (SoLuongTon < so_luong)
                    {
                        Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc, Vì thuốc :{0} số lượng tồn hiện tại trong kho không đủ\n Mời bạn xem lại số lượng", Drug_name));
                        Utility.GonewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, id_thuoc.ToString());
                        return false;
                    }
                }
                //if (grdPresDetail.GetDataRows().Length <= 0)
                //{
                //    Utility.ShowMsg("Bạn phải chọn một bản ghi để xác nhận phát thuốc cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
                //    return false;
                //}
                //foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
                //{
                //    if (gridExRow.RowType == RowType.Record)
                //    {
                //        long id_donthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                //        int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value);
                //        string Drug_name = Utility.sDbnull(gridExRow.Cells["tenthuoc_bietduoc"].Value);
                //        int id_kho = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdKho].Value);
                //        int id_thuockho = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value);
                //        int so_luong = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                //        int SoLuongTon = CommonLoadDuoc.SoLuongTonTrongKho(id_donthuoc,id_kho, id_thuoc,id_thuockho, 0);//Ko cần kiểm tra chờ xác nhận
                //        if (SoLuongTon < so_luong)
                //        {
                //            Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc, Vì thuốc :{0} số lượng tồn hiện tại trong kho không đủ\n Mời bạn xem lại số lượng", Drug_name));
                //            Utility.GonewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, id_thuoc.ToString());
                //            return false;
                //        }
                //    }
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// hàm thực hiện việc di chuyển thôn gtin trên đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPres_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void cmdExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện viecj cấu hình 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._HisDuocProperties);
                
                frm.ShowDialog();
                CauHinh();
                
                //grdPresDetail.SaveLayoutFile();
            }catch(Exception exception)
            {
                
            }
          
        }

        private void LoadLayout()
        {

            string layoutDir = GetLayoutDirectory() + @"\GridEXLayout.gxl";

            if (File.Exists(layoutDir))
            {

                FileStream layoutStream;

                layoutStream = new FileStream(layoutDir, FileMode.Open);

                grdPresDetail.LoadLayoutFile(layoutStream);

                layoutStream.Close();

            }

        }
        private string GetLayoutDirectory()
        {
            DirectoryInfo dInfo;
            dInfo = new DirectoryInfo(Application.ExecutablePath).Parent;

            dInfo = new DirectoryInfo(dInfo.FullName + @"\LayoutData");
            if (!dInfo.Exists) dInfo.Create();
            return dInfo.FullName;
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPres_SelectionChanged(grdPres, e);
        }
        /// <summary>
        /// hàm thực hiện việc hủy thông tin đơn thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyDonThuoc_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có muốn thực hiện hủy phát thuốc cho bệnh nhân \n Dữ liệu hủy sẽ được trả lại kho phát thuốc", "Thông báo", true))
            {

                if (!InValiHuyDonthuoc()) return;

                frm_NhaplydoHuy _NhaplydoHuy = new frm_NhaplydoHuy("LYDOHUYXACNHAN", "HỦY XÁC NHẬN ĐƠN THUỐC", "Chọn lý do hủy xác nhận trước khi thực hiện...", "Lý do hủy","Ngày hủy");
                _NhaplydoHuy.ShowDialog();
                if (!_NhaplydoHuy.m_blnCancel)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdPres.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1);
                    Int16 stockID = Utility.Int16Dbnull(m_dtDataPresDetail.Rows[0][KcbDonthuocChitiet.Columns.IdKho]);
                    dtNgayPhatThuoc.Value = globalVariables.SysDate;
                    ActionResult actionResult =
                        new XuatThuoc().HuyXacNhanDonThuocBN(Pres_ID, stockID, _NhaplydoHuy.ngay_thuchien, _NhaplydoHuy.ten);
                    switch (actionResult)
                    {
                        case ActionResult.DataUsed:
                            Utility.ShowMsg("Một trong các thuốc bạn chọn đã được sử dụng nên bạn không thể thực hiện hủy xác nhận", "thông báo", MessageBoxIcon.Information);
                            break;
                        case ActionResult.Success:
                            UpdateHuyHasConfirm();
                            THU_VIEN_CHUNG.Log(this.Name, globalVariables.UserName,
                                               string.Format(
                                                   "Hủy phát thuốc của bệnh nhân có mã lần khám {0} và mã bệnh nhân là: {1}. Đơn thuốc {2} bởi {3}",
                                                   Utility.sDbnull(grdPres.CurrentRow.Cells["ma_luotkham"].Value),
                                                   Utility.sDbnull(grdPres.CurrentRow.Cells["id_benhnhan"].Value),
                                                   Utility.sDbnull(grdPres.CurrentRow.Cells["id_donthuoc"].Value),
                                                   globalVariables.UserName), action.Delete);
                            Utility.ShowMsg("Bạn thực hiện việc hủy phát thuốc thành công", "thông báo", MessageBoxIcon.Information);
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình hủy phát thuốc cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
        private KcbDonthuocChitiet []CreatePresDetail()
        {
            int idx = 0;
            int length = 0;
            var query = from chitiet in grdPresDetail.GetDataRows()
                        let y = chitiet.RowType == RowType.Record
                        select y;
            length = query.Count();
            var arrDetail = new KcbDonthuocChitiet[length];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetDataRows())
            {
                arrDetail[idx]=new KcbDonthuocChitiet();
              
                arrDetail[idx].IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value);
                arrDetail[idx].IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value);
                arrDetail[idx].IdChitietdonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value);
                arrDetail[idx].IdKho = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdKho].Value);
                arrDetail[idx].DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value);
                arrDetail[idx].PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.PhuThu].Value);
                arrDetail[idx].SoLuong = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoLuong].Value);
                arrDetail[idx].MotaThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value);
                arrDetail[idx].ChidanThem = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.ChidanThem].Value);
                arrDetail[idx].CachDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.CachDung].Value);
                arrDetail[idx].DonviTinh = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonviTinh].Value);
                arrDetail[idx].SoluongDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SoluongDung].Value);
                arrDetail[idx].SolanDung = Utility.sDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.SolanDung].Value);
                arrDetail[idx].IdThanhtoan = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThanhtoan].Value);
                arrDetail[idx].TuTuc = Utility.ByteDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value);
                idx++;
            }
            return arrDetail;
        }
        private bool InValiXoaThongTin()
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi để thực hiện việc xóa thông tin đơn thuốc", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi đã thanh toán, bạn không thể xóa thông tin ", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bạn phải chọn những bản ghi chưa xác nhận", "Thông báo", MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
       
       

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            RowFilterView();
        }
        private void txtPID_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (txtPID.Text.Trim() != "" && Utility.Int32Dbnull(txtPID.Text) > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        string patient_ID = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPID.Text, 0), "000000");
                        txtPID.Text = patient_ID;
                        int Status = -1;
                        int NoiTru = 0;
                        m_dtDataDonThuoc =
                            SPs.ThuocTimkiemdonthuocCapphatngoaitru(-1, txtPID.Text,"","ALL",
                                                                   "01/01/1900","01/01/1900", Status,
                                                                    Utility.Int32Dbnull(cboKho.SelectedValue), kieuthuoc_vt).
                                GetDataSet().Tables[0];

                        Utility.SetDataSourceForDataGridEx(grdPres, m_dtDataDonThuoc, true, true, "1=1", "ten_benhnhan asc");
                        RowFilterView();
                        ModifyCommand();
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
            }
        }

        private void radTatCa_CheckedChanged_1(object sender, EventArgs e)
        {

        }

       
       

    }
}
