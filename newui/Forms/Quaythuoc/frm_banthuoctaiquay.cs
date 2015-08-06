using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using BorderStyle = Janus.Windows.GridEX.BorderStyle;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using VNS.Properties;
using VNS.HIS.UI.NGOAITRU;
using VNS.HIS.Classes;
using VNS.HIS.BusRule.Classes;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;

namespace  VNS.HIS.UI.THANHTOAN
{
    public partial class frm_banthuoctaiquay : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath, "SplitterDistance.txt");
        private int Distance = 400;
        private int HOADON_CAPPHAT_ID = -1;
        private bool INPHIEU_CLICK;
        private bool m_blnHasloaded;
        private bool blnJustPayment;
        private DataTable dtCapPhat;
        private DataTable dtPatientPayment;
        private DataTable m_dtChiPhiDaThanhToan = new DataTable();
        private DataTable m_dtChitietthanhtoan;
        /// <summary>
        ///     05-11-2013
        /// </summary>
        #region "khai báo biến "
        private DataTable m_dtSearchdata = new DataTable();
        private DataTable m_dtPayment, m_dtPhieuChi = new DataTable();
        private DataTable m_dtReportPhieuThu;
      
        private string sFileName = "RedInvoicePrinterConfig.txt";
        private int v_Payment_ID = -1;
        #endregion
        public frm_banthuoctaiquay()
        {
            InitializeComponent();
            KeyPreview = true;
            
            dtFromDate.Value =
                dtPaymentDate.Value = dtInput_Date.Value = dtToDate.Value = globalVariables.SysDate;
            //cmdHuyThanhToan.Visible = (globalVariables.IsAdmin || globalVariables.quyenh);
            Utility.grdExVisiableColName(grdPayment, "cmdHUY_PHIEUTHU", globalVariables.IsAdmin);
            if (grdPayment.RootTable.Columns.Contains(KcbThanhtoan.Columns.NgayThanhtoan))
                grdPayment.RootTable.Columns[KcbThanhtoan.Columns.NgayThanhtoan].Selectable =
                    globalVariables.QUYEN_SUANGAY_THANHTOAN || globalVariables.IsAdmin;
            cmdCauHinh.Visible = globalVariables.IsAdmin;
            LoadLaserPrinters();
            CauHinh();
            InitEvents();
        }
        void InitEvents()
        {
            Load += new EventHandler(frm_THANHTOAN_NGOAITRU_Load);
            FormClosing += new FormClosingEventHandler(frm_THANHTOAN_NGOAITRU_FormClosing);
            KeyDown += new KeyEventHandler(frm_THANHTOAN_NGOAITRU_KeyDown);

           
            txtTenBenhNhan.KeyDown += new KeyEventHandler(txtTenBenhNhan_KeyDown);

            cmdSearch.Click += new EventHandler(cmdSearch_Click);

            cmdThanhToan.Click += cmdThanhToan_Click;
            cmdHuyThanhToan.Click += cmdHuyThanhToan_Click;
           
            grdPayment.CellUpdated += grdPayment_CellUpdated;
            grdPayment.ColumnButtonClick += grdPayment_ColumnButtonClick;
            grdPayment.SelectionChanged += grdPayment_SelectionChanged;
           

            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            
            chkCreateDate.CheckedChanged += new EventHandler(chkCreateDate_CheckedChanged);
            
            tsmSuaSoBienLai.Click += new EventHandler(tsmSuaSoBienLai_Click);
            tsmInLaiBienLai.Click += new EventHandler(tsmInLaiBienLai_Click);
            tmsHuyHoaDon.Click += new EventHandler(tmsHuyHoaDon_Click);
            
          
            grdList.DoubleClick += new EventHandler(grdList_DoubleClick);
            grdList.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdList_ColumnButtonClick);
            grdList.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(grdList_FormattingRow);
            grdList.KeyDown += new KeyEventHandler(grdList_KeyDown);

         
            

          
            cmdPrintProperties.Click += new EventHandler(cmdPrintProperties_Click);
           
            cmdCauHinh.Click += new EventHandler(this.cmdCauHinh_Click);
            dtFromDate.Enabled = dtToDate.Enabled = chkCreateDate.Checked;

            chkHoixacnhanhuythanhtoan.CheckedChanged += new EventHandler(chkHoixacnhanhuythanhtoan_CheckedChanged);
            chkHoixacnhanthanhtoan.CheckedChanged += new EventHandler(chkHoixacnhanthanhtoan_CheckedChanged);
            chkPreviewHoadon.CheckedChanged += new EventHandler(chkPreviewHoadon_CheckedChanged);
            chkPreviewInBienlai.CheckedChanged += new EventHandler(chkPreviewInBienlai_CheckedChanged);
            chkTudonginhoadonsauthanhtoan.CheckedChanged += new EventHandler(chkTudonginhoadonsauthanhtoan_CheckedChanged);
            chkViewtruockhihuythanhtoan.CheckedChanged += new EventHandler(chkViewtruockhihuythanhtoan_CheckedChanged);
            cbomayinphoiBHYT.SelectedIndexChanged += new EventHandler(cbomayinphoiBHYT_SelectedIndexChanged);
            chkHienthiDichvusaukhinhannutthanhtoan.CheckedChanged += new EventHandler(chkHienthichuathanhtoan_CheckedChanged);
            chkAutoPayment.CheckedChanged += new EventHandler(chkAutoPayment_CheckedChanged);

            cmdSaveforNext.Click += new EventHandler(cmdSaveforNext_Click);
           


            cmdInBienlai.Click += new EventHandler(cmdInBienlai_Click);
          
           
            mnuHuyChietkhau.Click += new EventHandler(mnuHuyChietkhau_Click);

            cmdNew.Click += new EventHandler(cmdNew_Click);
            cmdUpdate.Click += new EventHandler(cmdUpdate_Click);
            cmdPrint.Click += new EventHandler(cmdPrint_Click);
            cmdDelete.Click += new EventHandler(cmdDelete_Click);

            grdChitiet.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(grdChitiet_UpdatingCell);
            grdChitiet.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(grdChitiet_CellValueChanged);
            grdChitiet.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(grdChitiet_CellUpdated);
            grdChitiet.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(grdChitiet_ColumnHeaderClick);
            grdChitiet.RowCheckStateChanged += new RowCheckStateChangeEventHandler(grdChitiet_RowCheckStateChanged);
            grdChitiet.GroupsChanged += new Janus.Windows.GridEX.GroupsChangedEventHandler(grdChitiet_GroupsChanged);
            grdChitiet.EditingCell += new EditingCellEventHandler(grdChitiet_EditingCell);

        }

        void chkAutoPayment_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._QuaythuocProperties.Tudongthanhtoan = chkAutoPayment.Checked;
            PropertyLib.SaveProperty(PropertyLib._QuaythuocProperties);
        }

        void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một Bệnh nhân để thực hiện xóa đơn thuốc");
                    return;
                }
                if (!isValidPres(Utility.Int32Dbnull(grdList.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1))) return;
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa đơn thuốc ra khỏi hệ thống\n.Lưu ý: Toàn bộ đơn thuốc cũng bị xóa theo", "Cảnh báo", true))
                {
                    ActionResult _result = new KCB_KEDONTHUOC().XoaDonthuoctaiquay(Utility.Int64Dbnull(grdList.GetValue(KcbDonthuoc.Columns.IdBenhnhan), -1), Utility.Int64Dbnull(grdList.GetValue(KcbDonthuoc.Columns.IdDonthuoc), -1));
                    if (_result == ActionResult.Success)
                    {
                        DataRow[] arrDr = m_dtSearchdata.Select(KcbDonthuoc.Columns.IdBenhnhan + "=" + Utility.sDbnull(grdList.GetValue(KcbDonthuoc.Columns.IdBenhnhan), "-1"));
                        if (arrDr.Length > 0)
                        {
                            m_dtSearchdata.Rows.Remove(arrDr[0]);
                            m_dtSearchdata.AcceptChanges();
                            m_dtChitietthanhtoan.Rows.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xóa đơn thuốc tại quầy", ex);
            }
            finally
            {
                ClearControl();
                ModifyPresCommand();
                ModifyCommand();
            }
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một Bệnh nhân để thực hiện xóa đơn thuốc");
                return;
            }
            int Pres_ID = Utility.Int32Dbnull(grdChitiet.GetValue("id_phieu"));
            PrintPres(Pres_ID);
        }
        private void PrintPres(int PresID)
        {
            DataTable v_dtData =new  KCB_KEDONTHUOC().LaythongtinDonthuoc_In(PresID);
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof(byte[]));
            //barcode.Data = Utility.sDbnull(Pres_ID);
            byte[] Barcode = Utility.GenerateBarCode(barcode);
            string ICD_Name = "";
            string ICD_Code = "";
           

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] ="";
                drv["ma_icd"] = "";
            }
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            ReportDocument reportDocument = new ReportDocument();
            string tieude = "", reportname = "",reportCode="";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "quaythuoc_InDonthuocA5";
                    reportDocument = Utility.GetReport("quaythuoc_InDonthuocA5", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "quaythuoc_InDonthuocA4";
                    reportDocument = Utility.GetReport("quaythuoc_InDonthuocA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "quaythuoc_InDonthuocA4";
                    reportDocument = Utility.GetReport("quaythuoc_InDonthuocA4", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            //v_dtData.AcceptChanges();
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }
        bool isValidPres(int Pres_ID)
        {
            try
            {
                if (Donthuoc_DangXacnhan(Pres_ID))
                {
                    Utility.ShowMsg(
                        "Đơn thuốc này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị hủy thanh toán để hủy xác nhận trước khi thực hiện sửa");
                     return false;;
                }
                var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                    .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                if (v_collect.Count > 0)
                {
                    Utility.ShowMsg(
                        "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc bạn phải hủy thanh toán");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                
                Utility.CatchException(ex);
                return false;
            }
        }
          KcbDanhsachBenhnhan objBenhnhan=null;
        void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một Bệnh nhân để thực hiện xóa đơn thuốc");
                    return;
                }
                
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int32Dbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.IdBenhnhan)));
                if (objBenhnhan != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdChitiet.GetValue("id_phieu"));
                      if( !isValidPres( Pres_ID)) return;
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            frm_kedonthuoctaiquay frm = new frm_kedonthuoctaiquay();
                            frm.m_dtPatients = m_dtSearchdata;
                            frm.m_enAct = action.Update;
                            frm._OnPayment += new frm_kedonthuoctaiquay.OnPayment(frm__OnPayment);
                            frm.Noi_tru = 0;
                            frm.objBenhnhan = objBenhnhan;
                            frm.id_kham = -1;
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                Utility.GotoNewRowJanus(grdList, KcbDanhsachBenhnhan.Columns.IdBenhnhan,
                                           Utility.sDbnull(frm.objBenhnhan.IdBenhnhan));
                                getData();
                                cmdThanhToan.Focus();
                                ModifyPresCommand();
                            }
                        }
                    }
                
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void frm__OnPayment(long idBn)
        {
            Utility.GotoNewRowJanus(grdList, KcbDanhsachBenhnhan.Columns.IdBenhnhan,
                                          idBn.ToString());
            getData();
            cmdThanhToan.PerformClick();
        }
        void ModifyPresCommand()
        {
            cmdUpdate.Enabled = Utility.isValidGrid(grdChitiet);
            cmdDelete.Enabled = Utility.isValidGrid(grdChitiet);
            cmdPrint.Enabled = Utility.isValidGrid(grdChitiet);
        }
        void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_kedonthuoctaiquay frm = new frm_kedonthuoctaiquay();
                frm.m_enAct = action.Insert;
                frm.m_dtPatients = m_dtSearchdata;
                frm._OnPayment+=new frm_kedonthuoctaiquay.OnPayment(frm__OnPayment);
                frm.objBenhnhan = null;
                frm.id_kham = -1;
                frm.txtPres_ID.Text = "-1";
                frm.Noi_tru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    Utility.GotoNewRowJanus(grdList, KcbDanhsachBenhnhan.Columns.IdBenhnhan,
                                            Utility.sDbnull(frm.objBenhnhan.IdBenhnhan));
                    getData();
                    cmdThanhToan.Focus();
                    ModifyPresCommand();
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.CatchException(exception);
                }
            }
            finally
            {
              
            }
        }

        void cmdPrintProperties_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._MayInProperties);
            frm.ShowDialog();
            CauHinh();
        }

        void chkPreviewInBienlai_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewInBienlai = chkPreviewInBienlai.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void grdChitiet_EditingCell(object sender, EditingCellEventArgs e)
        {
            if (grdChitiet.CurrentColumn != null) grdChitiet.CurrentColumn.InputMask = "";
        }

       
        void grdChitiet_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            TinhToanSoTienPhaithu();
            ModifyCommand();
        }

        void mnuHuyChietkhau_Click(object sender, EventArgs e)
        {
            try
            {
                
                foreach (GridEXRow _row in grdChitiet.GetDataRows())
                {
                    if (Utility.Int64Dbnull(_row.Cells["trangthai_thanhtoan"].Value, 1) == 0)//Chỉ reset các mục chưa thanh toán
                    {
                        _row.BeginEdit();
                        _row.Cells["tile_chietkhau"].Value = 0;
                        _row.Cells["tien_chietkhau"].Value = 0;
                        _row.EndEdit();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                TinhToanSoTienPhaithu();
            }
        }

        

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdList) && e.KeyCode == Keys.Enter)
            {
                getData();
            }
        }

       

        
        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiQuaythuoc(true, _Payment_ID);
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiQuaythuoc(false, _Payment_ID);
            cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
        }

      

        void chkHienthichuathanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (m_blnHasloaded && m_dtChitietthanhtoan != null && m_dtChitietthanhtoan.Columns.Count > 0 && m_dtChitietthanhtoan.Rows.Count > 0)
                m_dtChitietthanhtoan.DefaultView.RowFilter = "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : "");
            
            PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan = chkHienthiDichvusaukhinhannutthanhtoan.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
        }

        void cmdSaveforNext_Click(object sender, EventArgs e)
        {
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cbomayinphoiBHYT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TenMayInBienlai = cbomayinphoiBHYT.Text;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

       

        void chkViewtruockhihuythanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan = chkViewtruockhihuythanhtoan.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
        }

        void chkTudonginhoadonsauthanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan = chkTudonginhoadonsauthanhtoan.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        

        void chkPreviewHoadon_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewInHoadon = chkPreviewHoadon.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void chkHoixacnhanthanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan = chkHoixacnhanthanhtoan.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
        }

        void chkHoixacnhanhuythanhtoan_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan = chkHoixacnhanhuythanhtoan.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        /// <summary>
        ///     hàm thực hiện việc lấy thông tin
        /// </summary>
        private GridEXRow gridExRow { set; get; }

        private Color getColorMessage { get; set; }
        private string Maluotkham { get; set; }


        private void CauHinh()
        {
            try
            {
                dtPaymentDate.Enabled = globalVariables.IsAdmin || (globalVariablesPrivate.objNhanvien != null && Utility.Byte2Bool(globalVariablesPrivate.objNhanvien.QuyenSuangayThanhtoan));
                chkHoixacnhanhuythanhtoan.Checked = PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan;
                chkHienthiDichvusaukhinhannutthanhtoan.Checked = PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan;
                chkHoixacnhanthanhtoan.Checked = PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan;
                chkPreviewHoadon.Checked = PropertyLib._MayInProperties.PreviewInHoadon;
                chkPreviewInBienlai.Checked = PropertyLib._MayInProperties.PreviewInBienlai;
                chkTudonginhoadonsauthanhtoan.Checked = PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan;
                chkViewtruockhihuythanhtoan.Checked = PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan;
                cbomayinphoiBHYT.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                grdPayment.RootTable.Columns[KcbThanhtoan.Columns.Serie].Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1";
               // if (!hasLoadedRedInvoice && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1") LoadInvoiceInfo();
                uiStatusBar1.Visible = !PropertyLib._ThanhtoanProperties.HideStatusBar;
                
                grdPayment.ContextMenuStrip = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1" ? ctxBienlai : null;
                
                grdChitiet.RootTable.Columns[KcbThanhtoanChitiet.Columns.TileChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdChitiet.RootTable.Columns[KcbThanhtoanChitiet.Columns.TienChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;
                grdChitiet.RootTable.Columns[KcbThanhtoanChitiet.Columns.KieuChietkhau].Visible = PropertyLib._ThanhtoanProperties.HienthiChietkhauChitiet;

                grdChitiet.RootTable.Columns[KcbThanhtoanChitiet.Columns.BnhanChitra].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdChitiet.RootTable.Columns[KcbThanhtoanChitiet.Columns.BhytChitra].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdChitiet.RootTable.Columns["TT_BN"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdChitiet.RootTable.Columns["TT_BHYT"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdChitiet.RootTable.Columns["TT_BN_KHONG_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;
                grdChitiet.RootTable.Columns["TT_KHONG_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;

                //----------------------------------------------------------------------------------------
                grdChitiet.RootTable.Columns[KcbThanhtoanChitiet.Columns.PhuThu].Visible = PropertyLib._ThanhtoanProperties.Hienthiphuthu;
                grdChitiet.RootTable.Columns["TT_PHUTHU"].Visible = PropertyLib._ThanhtoanProperties.HienthiChiTra;

              
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
            }
        }

        private void LoadLaserPrinters()
        {
            try
            {
                //khoi tao may in
                String pkInstalledPrinters;
                cbomayinphoiBHYT.Items.Clear();
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    cbomayinphoiBHYT.Items.Add(pkInstalledPrinters);
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi kho nạp danh sách máy in \n" + ex.Message);
            }
            finally
            {
                if (cbomayinphoiBHYT.Items.Count <= 0)
                    Utility.ShowMsg("Không tìm thấy máy in cài đặt trong máy tính của bạn", "Cảnh báo");

               
            }
        }

        private void setProperties()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox) (control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        //if (txtFormantTongTien.Font.Size < 9)
                        //    txtFormantTongTien.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold,
                        //        GraphicsUnit.Point, 0);
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.KeyPress += txtEventTongTien_KeyPress;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
                foreach (Control control in pnlThongtinBN.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtControl = new EditBox();
                        if (txtControl.Tag != "NO")//Đánh dấu một số Control cho phép chỉnh sửa. Ví dụ Hạn thẻ BHYT 
                            //để người dùng có thể sửa nếu phía Tiếp đón gõ sai
                        {
                            txtControl = ((EditBox) (control));
                            txtControl.ReadOnly = true;
                            txtControl.BackColor = Color.White;
                        }
                        txtControl.ForeColor = Color.Black;
                    }

                    if (control is UICheckBox)
                    {
                        var chkControl = new UICheckBox();
                        if (chkControl.Tag != "NO")
                        {
                            chkControl = (UICheckBox) control;
                            chkControl.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void ModifyCommand()
        {
            try
            {
                cmdHuyThanhToan.Enabled = grdPayment.GetDataRows().Length > 0 && objBenhnhan != null;
                cmdThanhToan.Enabled = grdChitiet.GetCheckedRows().Length > 0 && objBenhnhan != null;

                cmdInhoadon.Enabled = grdPayment.GetDataRows().Length > 0 && grdPayment.CurrentRow != null && grdPayment.CurrentRow.RowType == RowType.Record && objBenhnhan != null;
                cmdInBienlai.Visible = grdPayment.GetDataRows().Length > 0 && grdPayment.CurrentRow != null && grdPayment.CurrentRow.RowType == RowType.Record && objBenhnhan != null;
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((EditBox) (sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }

       

       

        private void chkCreateDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Enabled = dtToDate.Enabled = chkCreateDate.Checked;
        }

        /// <summary>
        ///     hàm thực hiện việc tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            TimKiemBenhNhan();
        }
        private void FilterThanhToan()
        {
            try
            {
                string _rowFilter = "1=1";
                //if (radChuaTT.Checked) _rowFilter = string.Format("{0}={1}", "TT_ThanhToan", 0);
                //if (radDaTT.Checked) _rowFilter = string.Format("{0}={1}", "TT_ThanhToan", 1);
                m_dtSearchdata.DefaultView.RowFilter = _rowFilter;
                m_dtSearchdata.AcceptChanges();
            }
            catch (Exception)
            {
                Utility.ShowMsgBox("Lỗi trong quá trình Defaultview");
            }

        }
        private void TimKiemBenhNhan()
        {
            try
            {
                string KieuTimKiem = "DANGKY";
                //if (radDangKyCLS.Checked) KieuTimKiem = "CLS";
                //if (radDangKyThuoc.Checked) KieuTimKiem = "THUOC";
                m_dtSearchdata =
                    Quaythuoc.QuaythuocLaydanhsachkhachhang(Utility.Int32Dbnull(txtIdBN.Text,-1),
                        Utility.sDbnull(txtTenBenhNhan.Text),
                        chkCreateDate.Checked
                            ? dtFromDate.Value
                            : Convert.ToDateTime("01/01/1900"),
                        chkCreateDate.Checked
                            ? dtToDate.Value
                            : globalVariables.SysDate);
                Utility.AddColumToDataTable(ref m_dtSearchdata, "CHON", typeof(Int32));
                Utility.SetDataSourceForDataGridEx(grdList, m_dtSearchdata, true, true, "1=1", "");
                ClearControl();
               
                grdList.MoveFirst();
                UpdateGroup();
                grdList.MoveFirst();
                ModifyCommand();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     hàm thực hiện việc lấy thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "CHONBN")
            {
                getData();
               
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtSearchdata.AsEnumerable().GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch
            {
            }
        }
        private void ClearControl()
        {
            try
            {
                foreach (Control control in pnlThongtintien.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                }
                foreach (Control control in pnlThongtinBN.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                }
                dtPaymentDate.Value =  globalVariables.SysDate;
            }
            catch (Exception)
            {
            }
        }
        
        private void getData()
        {
            try
            {
                ClearControl();
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                }
                
               
                txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
               
              
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int32Dbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.IdBenhnhan)));
                gridExRow = grdList.GetRow();
               
                if (objBenhnhan!=null)
                {

                    dtInput_Date.Value = objBenhnhan.NgayTiepdon;

                    txtPatient_ID.Text = objBenhnhan.IdBenhnhan.ToString();
                    txtYear_Of_Birth.Text =Utility.sDbnull( objBenhnhan.NamSinh,"");
                    txtPatientName.Text = objBenhnhan.TenBenhnhan;
                    txtDiaChi.Text = objBenhnhan.DiaChi;
                    GetDataChiTiet();
                    LaydanhsachLichsuthanhtoan();
                }
            }
            catch
            {
            }
            finally
            {
                ModifyPresCommand();
                ModifyCommand();
               
                tabThongTinCanThanhToan.SelectedIndex = 0;
            }
        }


        private void LaydanhsachLichsuthanhtoan()
        {
            try
            {
                m_dtPayment =
                    _THANHTOAN.LaythongtinCacLanthanhtoan("-1",
                        Utility.Int32Dbnull(txtPatient_ID.Text, -1), 0,0,0,
                        "-1");

                Utility.SetDataSourceForDataGridEx(grdPayment, m_dtPayment, false, true, "1=1", "");
                if (m_dtPayment.Rows.Count <= 0)
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                }
                else
                {
                    txtDachietkhau.Text = m_dtPayment.Compute("SUM(tongtien_chietkhau)", "1=1").ToString();
                    txtsotiendathu.Text = (Utility.DecimaltoDbnull(m_dtPayment.Compute("SUM(BN_CT)", "1=1"), 0) - Utility.DecimaltoDbnull(txtDachietkhau.Text, 0)).ToString();
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử thanh toán của bệnh nhân", exception);
                // throw;
            }
        }
   

        private void GetDataChiTiet()
        {
            try
            {
                m_dtChitietthanhtoan =
                    Quaythuoc.QuaythuocLaydonthuockhachhang( Utility.Int32Dbnull(txtPatient_ID.Text,-1));
                m_dtChitietthanhtoan.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdChitiet, m_dtChitietthanhtoan, false, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
               
                UpdateTuCheckKhiChuaThanhToan();
                SetSumTotalProperties();
            }
            catch
            {
            }
        }

       
      
        private void UpdateTuCheckKhiChuaThanhToan()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdChitiet.GetDataRows())
                {
                    if (gridExRow.RowType == RowType.Record)
                    {
                        gridExRow.IsChecked = Utility.Int32Dbnull(gridExRow.Cells["trangthai_thanhtoan"].Value, 0) == 0
                                              && Utility.Int32Dbnull(gridExRow.Cells["trangthai_huy"].Value, 0) == 0;
                    }
                }
                grdChitiet.UpdateData();
            }
            catch (Exception)
            {
            }
        }
        decimal Chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {
               
                GridEXColumn gridExColumntrangthaithanhtoan = getGridExColumn(grdChitiet, "trangthai_thanhtoan");
                GridEXColumn gridExColumn = getGridExColumn(grdChitiet, "TT_KHONG_PHUTHU");
                GridEXColumn gridExColumn_tutuc = getGridExColumn(grdChitiet, "TT_BN_KHONG_TUTUC");
                GridEXColumn gridExColumnTT = getGridExColumn(grdChitiet, "TT");
                GridEXColumn gridExColumnTT_chietkhau = getGridExColumn(grdChitiet, KcbThanhtoanChitiet.Columns.TienChietkhau);
                GridEXColumn gridExColumnBHYT = getGridExColumn(grdChitiet, "TT_BHYT");
                GridEXColumn gridExColumnTTBN = getGridExColumn(grdChitiet, "TT_BN");
                GridEXColumn gridExColumntutuc = getGridExColumn(grdChitiet, "tu_tuc");
                GridEXColumn gridExColumntrangthai_huy = getGridExColumn(grdChitiet, "trangthai_huy");
                GridEXColumn gridExColumnPhuThu = getGridExColumn(grdChitiet,
                    "TT_PHUTHU");
                var gridExFilterCondition_khong_Tutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditionTutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);
                var gridExFilterChuathanhtoan =
                    new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 0);
                var gridExFilterDathanhtoan =
                  new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 1);
                var gridExFilterCondition_TuTuc =
                   new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);

                var gridExFilterConditionKhongTuTuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthai_huy =
                    new GridEXFilterCondition(gridExColumntrangthai_huy, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthai_huy_va_khongtutuc =
                   new GridEXFilterCondition(gridExColumntrangthai_huy, ConditionOperator.Equal, 0);
                gridExFilterConditiontrangthai_huy_va_khongtutuc.AddCondition(gridExFilterConditionKhongTuTuc);
                GridEXColumn gridExColumnBNCT = getGridExColumn(grdChitiet,
                    "bnhan_chitra");
                // Janus.Windows.GridEX.GridEXColumn gridExColumnTuTuc = getGridExColumn(grdChitiet, "bnhan_chitra");
                decimal BN_KHONGTUTUC =
                  Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumn_tutuc, AggregateFunction.Sum),
                      gridExFilterCondition_khong_Tutuc);
                decimal TT =
                    Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnTT, AggregateFunction.Sum),
                        gridExFilterConditiontrangthai_huy);
                decimal TT_Chietkhau =
                   Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnTT_chietkhau, AggregateFunction.Sum),
                       gridExFilterConditiontrangthai_huy);

                decimal TT_KHONG_PHUTHU =
                   Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumn, AggregateFunction.Sum),
                       gridExFilterConditiontrangthai_huy);
                decimal TT_BHYT =
                    Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnBHYT, AggregateFunction.Sum,
                        gridExFilterConditiontrangthai_huy));
                decimal TT_BN =
                    Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                        gridExFilterConditiontrangthai_huy));
              
                Chuathanhtoan =
                   Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                       gridExFilterChuathanhtoan));
                //Tạm bỏ
                //decimal PtramBHYT = 0;
                //_THANHTOAN.LayThongPtramBHYT(TongChiphiBHYT, objLuotkham, ref PtramBHYT);
                decimal PhuThu =
                    Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));
                decimal TuTuc =
                    Utility.DecimaltoDbnull(grdChitiet.GetTotal(gridExColumnBNCT, AggregateFunction.Sum,
                        gridExFilterConditionTutuc));
                //txtPtramBHChiTra.Text = Utility.sDbnull(PtramBHYT);
              
                TT_KHONG_PHUTHU -= TuTuc;
               
               
                decimal BNCT = BN_KHONGTUTUC;
              
                decimal BNPhaiTra = BNCT ;
              
                txtSoTienCanNop.Text = Utility.sDbnull(Chuathanhtoan);
                //Tạm bỏ
                TinhToanSoTienPhaithu();
                ModifyCommand();
            }
            catch
            { }
        }

        private void TinhToanSoTienPhaithu()
        {
            try
            {
                List<GridEXRow> query = (from thanhtoan in grdChitiet.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                               && Utility.Int32Dbnull(thanhtoan.Cells["trangthai_thanhtoan"].Value) == 0
                                               //&& Utility.Int32Dbnull(thanhtoan.Cells["trang_thai"].Value) == 0
                                         select thanhtoan).ToList();

                List<GridEXRow> query1 = (from thanhtoan in grdChitiet.GetCheckedRows()
                                         where Utility.Int32Dbnull(thanhtoan.Cells["trangthai_huy"].Value) == 0
                                         select thanhtoan).ToList();
               
                decimal thanhtien = query.Sum(c => Utility.DecimaltoDbnull(c.Cells["TT_BN"].Value));
                decimal Chietkhauchitiet = query1.Sum(c => Utility.DecimaltoDbnull(c.Cells["tien_chietkhau"].Value));
                txtSoTienCanNop.Text = Utility.sDbnull(thanhtien - Chietkhauchitiet);
                txtTienChietkhau.Text = Utility.sDbnull( Chietkhauchitiet);
                ModifyCommand();
            }
            catch (Exception)
            {
            }
            //decimal thanhtien =
        }

        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_THANHTOAN_NGOAITRU_Load(object sender, EventArgs e)
        {
            setProperties();
            ClearControl();
            if (PropertyLib._ThanhtoanProperties.SearchWhenStart) cmdSearch_Click(cmdSearch, e);
            m_blnHasloaded = true;
            ModifyPresCommand();
            ModifyCommand();
        }
        bool hasLoadedRedInvoice = false;
        /// <summary>
        ///     hàm thực hiện việc thanh toán bản ghi đang chọn trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdThanhToan, false);
                if (blnJustPayment) return;
                blnJustPayment = true;
                if (!IsValidata()) return;
                PayCheckDate(dtInput_Date.Value);
                PerformAction();
                blnJustPayment = false;
            }
            catch
            {
                blnJustPayment = false;
            }
            finally
            {
                Utility.EnableButton(cmdThanhToan, true);
                ModifyCommand();
                blnJustPayment = false;
            }
        }

       

        public decimal TongtienCK = 0m;
        public decimal TongtienCK_Hoadon = 0m;
        public decimal TongtienCK_chitiet = 0m;
        public string ma_ldoCk = "";
        private void PerformAction()
        {
            try
            {
                
                if (objBenhnhan != null)
                {
                    if (INPHIEU_CLICK)
                    {
                        goto INPHIEU;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan)
                        if (!Utility.AcceptQuestion("Bạn có muốn thanh toán cho bệnh nhân này không", "Thông báo thanh toán", true))
                        {
                            return;
                        }

                    //Nếu thanh toán khi in phôi thì không cần hỏi
                INPHIEU:
                    bool IN_HOADON = false;
                    string ErrMsg = "";
                    long IdHdonLog = -1;
                    IN_HOADON = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0) > 0;
                    if (IN_HOADON)
                    {
                      
                    }

                    List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                    if (lstItems == null)
                    {
                        Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                        return;
                    }
                    TongtienCK_chitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                    if (chkChietkhauthem.Checked || TongtienCK_chitiet > 0)
                    {
                        frm_ChietkhauTrenHoadon _ChietkhauTrenHoadon = new frm_ChietkhauTrenHoadon();
                        _ChietkhauTrenHoadon.TongCKChitiet = Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        _ChietkhauTrenHoadon.TongtienBN = Utility.DecimaltoDbnull(txtSoTienCanNop.Text) + Utility.DecimaltoDbnull(txtTienChietkhau.Text);
                        _ChietkhauTrenHoadon.ShowDialog();
                        if (!_ChietkhauTrenHoadon.m_blnCancel)
                        {
                            TongtienCK = _ChietkhauTrenHoadon.TongtienCK;
                            TongtienCK_Hoadon = _ChietkhauTrenHoadon.TongCKHoadon;
                            ma_ldoCk = _ChietkhauTrenHoadon.ma_ldoCk;
                        }
                        else
                        {
                            if (TongtienCK_chitiet > 0)
                            {
                                Utility.ShowMsg("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Yêu cầu bạn nhập lý do chiết khấu(Do tiền chiết khấu >0). Mời bạn bấm lại nút thanh toán để bắt đầu lại");
                                return;
                            }
                            else
                            {
                                if (!Utility.AcceptQuestion("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Bạn có muốn tiếp tục thanh toán không cần chiết khấu hay không?"))
                                {
                                    return;
                                }
                            }
                        }
                    }
                    ActionResult actionResult = _THANHTOAN.ThanhtoanDonthuoctaiquay(CreatePayment(), objBenhnhan, lstItems, ref v_Payment_ID, -1, false);

                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            LaydanhsachLichsuthanhtoan();
                            GetDataChiTiet();
                            Utility.GotoNewRowJanus(grdPayment, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grdPayment.MoveFirst();
                            }
                            //Tạm rem phần hóa đơn đỏ lại
                            if (IN_HOADON && PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan)
                            {
                                int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KIEUINHOADON", "1", false));
                                if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                    InHoadon();
                                if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                    new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiQuaythuoc(false, v_Payment_ID);
                            }
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)=="1")
                            {
                               
                            }
                            if (PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan)
                            {
                                ShowPaymentDetail(v_Payment_ID);
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                    IN_HOADON = false;
                    INPHIEU_CLICK = false;
                }
            }
            catch (Exception exception)
            {
               
            }
            finally
            {
                TongtienCK = 0m;
                TongtienCK_chitiet = 0m;
                TongtienCK_Hoadon = 0m;
                ma_ldoCk = "";
                ModifyPresCommand();
                ModifyCommand();
            }
        }
        void ShowPaymentDetail(int v_Payment_ID)
        {

            if (objBenhnhan != null)
            {
                frm_HuyThanhtoan_Quaythuoc frm = new frm_HuyThanhtoan_Quaythuoc();
                frm.v_Payment_Id = v_Payment_ID;
                frm.Chuathanhtoan = Chuathanhtoan;
                frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                frm.TotalPayment = grdPayment.GetDataRows().Length;
                frm.ShowCancel = false;
                frm.ShowDialog();
            }
        }
        /// <summary>
        ///     hàm thực hiện mảng của chi tiết thanh toán chi tiết
        /// </summary>
        /// <returns></returns>
        private List< KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (GridEXRow gridExRow in grdChitiet.GetCheckedRows())
                {
                   KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.PtramBhyt = 0;
                    newItem.SoLuong = Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.SoLuong].Value, 0);
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = Utility.DecimaltoDbnull(gridExRow.Cells["don_gia"].Value, 0);
                    newItem.BhytChitra = 0;
                    newItem.DonGia =Utility.DecimaltoDbnull(gridExRow.Cells["don_gia"].Value, 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(gridExRow.Cells["PHU_THU"].Value, 0);
                    newItem.TuTuc = Utility.ByteDbnull(gridExRow.Cells["tu_tuc"].Value, 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(gridExRow.Cells["id_phieu"].Value);
                    newItem.IdKham = Utility.Int32Dbnull(gridExRow.Cells["Id_Kham"].Value);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(gridExRow.Cells["Id_Phieu_Chitiet"].Value, -1);
                    newItem.IdDichvu = Utility.Int16Dbnull(gridExRow.Cells["Id_dichvu"].Value, -1);
                    newItem.IdChitietdichvu = Utility.Int16Dbnull(gridExRow.Cells["Id_Chitietdichvu"].Value, -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(gridExRow.Cells["Ten_Chitietdichvu"].Value, "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(gridExRow.Cells["ten_bhyt"].Value, "Không xác định").Trim();
                    newItem.DonviTinh =Utility.chuanhoachuoi(Utility.sDbnull(gridExRow.Cells["Ten_donvitinh"].Value, "Lượt"));
                    newItem.SttIn = Utility.Int16Dbnull(gridExRow.Cells["stt_in"].Value, 0);
                    newItem.IdPhongkham = Utility.Int16Dbnull(gridExRow.Cells["id_phongkham"].Value, -1);
                    newItem.IdBacsiChidinh = Utility.Int16Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdBacsiChidinh].Value, -1);
                    newItem.IdLoaithanhtoan = Utility.ByteDbnull(gridExRow.Cells["Id_Loaithanhtoan"].Value, -1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(gridExRow.Cells["Id_Loaithanhtoan"].Value, -1));
                    newItem.TienChietkhau = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.TienChietkhau].Value, 0m);
                    newItem.TileChietkhau = Utility.DecimaltoDbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.TileChietkhau].Value, 0m);
                    newItem.MaDoituongKcb ="DV";
                    newItem.KieuChietkhau = "%";
                    newItem.NguoiHuy = "";
                    newItem.NgayHuy = null ;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 0;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;
                    lstItems.Add(newItem);
                }
                return lstItems;
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        private void PayCheckDate(DateTime InputDate)
        {
            if (globalVariables.SysDate.Date != InputDate.Date)
            {
                frm_ChonngayThanhtoan frm = new frm_ChonngayThanhtoan();
                frm.pdt_InputDate = dtInput_Date.Value;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    dtPaymentDate.Value = frm.pdt_InputDate;
                }
            }
        }

        private KcbThanhtoan CreatePayment()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = "";
            objPayment.IdBenhnhan = Utility.Int32Dbnull(txtPatient_ID.Text, -1);
            objPayment.NgayThanhtoan = dtPaymentDate.Value;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.TrangThai = 0;
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.KieuThanhtoan = 0;//0=Ngoại trú;1=nội trú
            objPayment.TenKieuThanhtoan = objPayment.KieuThanhtoan == 0 ? "NGOAI" : "NOI";
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtSoTienCanNop.Text, 0);
            //2 mục này được tính lại ở Business
            objPayment.BnhanChitra = -1;
            objPayment.BhytChitra = -1;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "T";
            objPayment.TongtienChietkhau = TongtienCK;
            objPayment.TongtienChietkhauChitiet = TongtienCK_chitiet;
            objPayment.TongtienChietkhauHoadon = TongtienCK_Hoadon;

            objPayment.MauHoadon = "";
            objPayment.KiHieu = "";
            objPayment.IdCapphat = -1;
            objPayment.MaQuyen = "";
            objPayment.Serie = "";
           

            objPayment.MaLydoChietkhau = ma_ldoCk;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            return objPayment;
        }

        private bool IsValidata()
        {
            SqlQuery sqlQuery;
           
            bool b_CheckPayment = false;
            GridEXRow[] checkList = grdChitiet.GetCheckedRows();
            if (grdChitiet.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một dịch vụ chưa thanh toán để thực hiện thanh toán", "Thông báo", MessageBoxIcon.Warning);
                grdChitiet.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdChitiet.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_thanhtoan"].Value.ToString() == "1")
                {
                    b_CheckPayment = true;
                    break;
                }
            }
            if (b_CheckPayment)
            {
                Utility.ShowMsg("Bạn phải chọn các bản ghi chưa thực hiện thanh toán mới thanh toán được", "Thông báo", MessageBoxIcon.Warning);
                grdChitiet.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdChitiet.GetCheckedRows())
            {
                if (gridExRow.Cells["trangthai_huy"].Value.ToString() == "1")
                {
                    b_CheckPayment = true;
                    break;
                }
            }
            if (b_CheckPayment)
            {
                Utility.ShowMsg("Bạn phải chọn bản ghi chưa được hủy mới thanh toán", "Thông báo",
                    MessageBoxIcon.Warning);
                grdChitiet.Focus();
                return false;
            }
           

            bool b_CancelVali = false;
            foreach (GridEXRow gridExRow in grdChitiet.GetCheckedRows())
            {
                switch (Utility.Int32Dbnull(gridExRow.Cells["Id_Loaithanhtoan"].Value))
                {
                    case 1:
                        int IdKham = Utility.Int32Dbnull(gridExRow.Cells["id_phieu"].Value);
                        sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                            .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(IdKham);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            //b_CancelVali = true;
                            Utility.ShowMsg(
                                "Bản ghi liên quan đến tiền khám đã bị người khác xóa. Đề nghị bạn chọn lại Bệnh nhân(hoặc nhấn F5) để hệ thống lấy lại dữ liệu cho bạn xem\n.Cần liên hệ lại với các nhân viên phòng ban khác để xem ai xóa",
                                "Thông báo", MessageBoxIcon.Warning);
                            return false;
                            //   break;
                        }
                        break;
                    case 2:
                        int AssignDetail = Utility.Int32Dbnull(gridExRow.Cells["Id_Phieu_Chitiet"].Value);
                        sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi liên quan đến dịch vụ Cận lâm sàng đã bị người khác xóa. Đề nghị bạn chọn lại Bệnh nhân(hoặc nhấn F5) để hệ thống lấy lại dữ liệu cho bạn xem\n.Cần liên hệ lại với các nhân viên phòng ban khác để xem ai xóa",
                                "Thông báo", MessageBoxIcon.Warning);
                            return false;
                        }
                        break;
                    case 3:
                        int Pres_ID = Utility.Int32Dbnull(gridExRow.Cells["id_phieu"].Value);
                        int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbThanhtoanChitiet.Columns.IdDichvu].Value);
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID)
                            .And(KcbDonthuocChitiet.Columns.IdThuoc).IsEqualTo(Drug_ID);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            Utility.ShowMsg(
                                "Bản ghi liên quan đến đơn thuốc đã bị người khác xóa. Đề nghị bạn chọn lại Bệnh nhân(hoặc nhấn F5) để hệ thống lấy lại dữ liệu cho bạn xem\n.Cần liên hệ lại với các nhân viên phòng ban khác để xem ai xóa",
                                "Thông báo", MessageBoxIcon.Warning);
                            return false;
                        }
                        break;
                }
            }

            return true;
        }

        private void txtLuongCoBan_TextChanged(object sender, EventArgs e)
        {
        }

       
        /// <summary>
        ///     hàm thực hiện việc khởi tọa sự kiện của lưới thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPayment_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdPHIEU_THU")
            {
                CallPhieuThu();
            }
            if (e.Column.Key == "cmdHUY_PHIEUTHU")
            {
                HuyThanhtoan();
            }
        }
        string ma_lydohuy = "";
        
        private void HuyThanhtoan()
        {
            ma_lydohuy = "";
            if (!Utility.isValidGrid(grdPayment)) return;
            if (grdPayment.CurrentRow != null)
            {
                
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                if (objPayment != null)
                {
                    //Kiểm tra ngày hủy
                    int SONGAY_HUYTHANHTOAN =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("SONGAY_HUYTHANHTOAN", "0", true),0);
                    int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                    if (Chenhlech > SONGAY_HUYTHANHTOAN)
                    {
                        Utility.ShowMsg("Hệ thống không cho phép bạn hủy thanh toán đã quá ngày. Cần liên hệ quản trị hệ thống để được trợ giúp");
                        return;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                    {
                        frm_HuyThanhtoan_Quaythuoc frm = new frm_HuyThanhtoan_Quaythuoc();
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = Chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            getData();
                        }
                    }
                    else
                    {
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán");
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                        }
                       int IdHdonLog = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                       bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                       ActionResult actionResult = _THANHTOAN.HuyThanhtoanDonthuoctaiquay(Utility.Int32Dbnull(objPayment.IdThanhtoan, -1), null, ma_lydohuy, IdHdonLog, HUYTHANHTOAN_HUYBIENLAI);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                getData();
                                break;
                            case ActionResult.ExistedRecord:
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.UNKNOW:
                                Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Cancel:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        ///     hàm thực hiện việc gọi phiếu thu
        /// </summary>
        private void CallPhieuThu()
        {
            if (grdPayment.CurrentRow != null)
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
               

                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
               
                if (objPayment != null)
                {
                    
                        frm_HuyThanhtoan_Quaythuoc frm = new frm_HuyThanhtoan_Quaythuoc();
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = Chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtSoTienCanNop.Text;
                        frm.ShowCancel = false;
                        frm.ShowDialog();
                    
                }
            }
        }
       
        private void cmdHuyThanhToan_Click(object sender, EventArgs e)
        {
            HuyThanhtoan();
        }

        private void grdPayment_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                UpdatePaymentDate();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc update thông tin ngày thanh toán
        /// </summary>
        private void UpdatePaymentDate()
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có muốn thay đổi thông tin chỉnh ngày thanh toán,Nếu bạn chỉnh ngày thanh toán, sẽ ảnh hưởng tới báo cáo",
                    "Thông báo", true))
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objPayment != null)
                {
                    objPayment.NgayThanhtoan = Convert.ToDateTime(grdPayment.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
        private void frm_THANHTOAN_NGOAITRU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F3 ||(e.Control && e.KeyCode==Keys.F))
            {
                cmdSearch.PerformClick();
            }
            if (e.KeyCode == Keys.F5) getData();
            if (e.KeyCode == Keys.T && e.Control) cmdThanhToan.PerformClick();
            if (e.KeyCode == Keys.F1) tabThongTinCanThanhToan.SelectedIndex = 0;
            if (e.KeyCode == Keys.F2) tabThongTinCanThanhToan.SelectedIndex = 1;
        }

        private void cmdLaylaiThongTin_Click(object sender, EventArgs e)
        {
            getData();
        }

        /// <summary>
        ///     hàm thực hiện viecj  check trạng thái nút thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdChitiet_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            
        }

        /// <summary>
        ///     hàm thực hiện viecj trạng thái của grid header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdChitiet_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                grdChitiet.RowCheckStateChanged -= grdChitiet_RowCheckStateChanged;
                TinhToanSoTienPhaithu();
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
                grdChitiet.RowCheckStateChanged += grdChitiet_RowCheckStateChanged;
            }
        }

       
       

        private void grdList_FormattingRow(object sender, RowLoadEventArgs e)
        {
        }

        /// <summary>
        ///     hàm thực hiện việc dổi trạng thái của chưa thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdChitiet_GroupsChanged(object sender, GroupsChangedEventArgs e)
        {
            ModifyCommand();
        }

        private void grdChitiet_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "tile_chietkhau" || e.Column.Key == "tien_chietkhau")
            {
                if (Utility.isValidGrid(grdChitiet) && Utility.Int64Dbnull(grdChitiet.CurrentRow.Cells["trangthai_thanhtoan"].Value, 1) == 1)
                {
                    Utility.ShowMsg("Chi tiết bạn chọn đã được thanh toán nên bạn không thể chiết khấu được nữa. Mời bạn kiểm tra lại");
                    //e.Cancel = true;
                    return;
                }
                else
                {
                    if (e.Column.Key == "tile_chietkhau")
                    {
                        //Tính lại tiền chiết khấu theo tỉ lệ %
                        if (Utility.DecimaltoDbnull(e.Value, 0) > 100)
                        {
                            Utility.ShowMsg("Tỉ lệ chiết khấu không được phép vượt quá 100 %. Mời bạn kiểm tra lại");
                            e.Cancel = true;
                            return;
                        }
                        grdChitiet.CurrentRow.Cells["tien_chietkhau"].Value = Utility.DecimaltoDbnull(grdChitiet.CurrentRow.Cells["TT_BN"].Value, 0) * Utility.DecimaltoDbnull(e.Value, 0) / 100;

                    }
                    else
                    {

                        if (Utility.DecimaltoDbnull(e.Value, 0) > Utility.DecimaltoDbnull(grdChitiet.CurrentRow.Cells["TT_BN"].Value, 0))
                        {
                            Utility.ShowMsg("Tiền chiết khấu không được lớn hơn(>) tiền Bệnh nhân chi trả("+ Utility.DecimaltoDbnull(grdChitiet.CurrentRow.Cells["TT_BN"].Value, 0).ToString()+"). Mời bạn kiểm tra lại");
                            e.Cancel = true;
                            return;
                        }
                        grdChitiet.CurrentRow.Cells["tile_chietkhau"].Value = (Utility.DecimaltoDbnull(e.Value, 0) / Utility.DecimaltoDbnull(grdChitiet.CurrentRow.Cells["TT_BN"].Value, 0)) * 100;
                    }
                }
            }
            ModifyCommand();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                getData();
            }
            catch (Exception)
            {
                //throw;
            }
            //grdList_ColumnButtonClick(grdList,col);
        }

        /// <summary>
        ///     hàm thực hiện việc format thông tin số tiền cần nợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdChitiet_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            TinhToanSoTienPhaithu();
            e.Column.InputMask = "{0:#,#.##}";
        }

        /// <summary>
        ///     hàm thực hiện việc hiển thị tên bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTenBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSearch_Click(cmdSearch, new EventArgs());
                if (grdList.RowCount == 1)
                {
                    grdList.MoveFirst();
                    grdList_DoubleClick(grdList, new EventArgs());
                }
            }
        }

        /// <summary>
        ///     hàm thực hiện sửa số biên lai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSuaSoBienLai_Click(object sender, EventArgs e)
        {
            SuaSoBienLai();
        }

        /// <summary>
        ///     hàm thực hiện việc sửa số biên lai của viện
        /// </summary>
        private void SuaSoBienLai()
        {
            try
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                var tHoadonLog =
                    new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.IdThanhtoan).IsEqualTo(v_Payment_ID).
                        And(HoadonLog.Columns.TrangThai).IsEqualTo(0).
                        ExecuteSingle<HoadonLog>();
                if (tHoadonLog != null)
                {
                    var frm = new frm_SUA_SOBIENLAI();
                    frm._HoadonLog = tHoadonLog;
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        grdPayment.CurrentRow.BeginEdit();
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MauHoadon].Value = frm._HoadonLog.MauHoadon;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.KiHieu].Value = frm._HoadonLog.KiHieu;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.MaQuyen].Value = frm._HoadonLog.MaQuyen;
                        grdPayment.CurrentRow.Cells[HoadonLog.Columns.Serie].Value = frm._HoadonLog.Serie;
                        grdPayment.CurrentRow.EndEdit();
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình sửa số biên lai");
            }
        }

        private void tsmInLaiBienLai_Click(object sender, EventArgs e)
        {
            try
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                DataTable dtPatientPayment = _THANHTOAN.Laythongtinhoadondo(v_Payment_ID);
                 string tieude="", reportname = "";
                ReportDocument report = Utility.GetReport("thanhtoan_RedInvoice",ref tieude,ref reportname);
                if (report == null) return;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    report.PrintOptions.PrinterName = Utility.sDbnull(printDialog1.PrinterSettings.PrinterName);
                    report.SetDataSource(dtPatientPayment);
                    report.PrintToPrinter(0, true, 0, 0);
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình in phiếu");
                throw;
            }
        }

        private void tmsHuyHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                int IdHdonLog = -1;
                if (!Utility.isValidGrid(grdPayment)) return;

                if (
                       !Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy hóa đơn này không. Chú ý: Hủy hóa đơn đồng nghĩa với việc bạn hủy thanh toán?", "Xác nhận hủy hóa đơn", true))
                    return;
                IdHdonLog = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                if (IdHdonLog > 0)
                {
                    int v_Payment_Id = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                    DataTable dtKTra = _THANHTOAN.KiemtraTrangthaidonthuocTruockhihuythanhtoan(v_Payment_Id);
                    if (dtKTra.Rows.Count > 0)
                    {
                        Utility.ShowMsg("Đã có thuốc được duyệt cấp phát. Bạn không thể hủy thanh toán.");
                        return;
                    }
                    bool HUYHOADON_XOABIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("HUYHOADON_XOABIENLAI", "0", true) == "1";
                    ActionResult actionResult = _THANHTOAN.HuyThongTinLanThanhToan(v_Payment_Id, null, "", IdHdonLog, HUYHOADON_XOABIENLAI);
                    //nếu hủy hóa đơn và hủy lần thanh toán thành công thì thông báo
                    if (actionResult == ActionResult.Success)
                    {
                        HuyThanhtoan();
                        Utility.ShowMsg("Đã hủy hóa đơn thành công");
                        getData();
                    }
                    else if (actionResult == ActionResult.Error) // hủy lần thanh toán bị lỗi
                    {
                        Utility.ShowMsg("Có lỗi trong quá trình hủy thanh toán");
                    }
                    else // Hủy hóa đơn và thanh toán bị lỗi
                    {
                        Utility.ShowMsg("Có lỗi trong quá trình hủy hóa đơn thanh toán.");
                    }
                }

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình hủy hóa đơn");
                throw;
            }
        }

        void ModifyContextMenu()
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false)!="1") return;
                int IdHdonLog = Utility.Int32Dbnull(grdPayment.GetValue(HoadonLog.Columns.IdHdonLog), -1);
                int TrangthaiChot = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.TrangthaiChot), 0);
                tsmSuaSoBienLai.Visible = IdHdonLog > 0;
                tmsHuyHoaDon.Visible = IdHdonLog > 0 && TrangthaiChot == 0;
                tsmInLaiBienLai.Visible = IdHdonLog > 0;
                if (TrangthaiChot == 0)
                    if (IdHdonLog > 0)
                    {
                        mnuLayhoadondo.Text = "Bỏ hóa đơn đỏ cho thanh toán đang chọn";
                        mnuLayhoadondo.Tag = 0;
                    }
                    else
                    {
                        mnuLayhoadondo.Text = "Lấy hóa đơn đỏ cho thanh toán đang chọn";
                        mnuLayhoadondo.Tag = 1;
                    }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tùy biến menu biên lai", ex);
            }
        }
        private void grdPayment_SelectionChanged(object sender, EventArgs e)
        {
            ModifyContextMenu();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        

        /// <summary>
        ///     hàm thực hiện việc cấu hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._QuaythuocProperties);
            frm.ShowDialog();
            CauHinh();
        }

        
        void RefreshData(KcbLuotkham objNew, string ten_kcbbd)
        {
            try
            {
                DataRow[] arrDr = m_dtSearchdata.Select(KcbLuotkham.Columns.IdBenhnhan + "=" + objNew.IdBenhnhan.ToString() + " AND " + KcbLuotkham.Columns.MaLuotkham + "='" + objNew.MaLuotkham + "'");
                foreach (DataRow dr in arrDr)
                {
                    dr[KcbLuotkham.Columns.MatheBhyt] = objNew.MatheBhyt;
                    dr[KcbLuotkham.Columns.NgaybatdauBhyt] =Utility.null2DBNull( objNew.NgaybatdauBhyt,DBNull.Value);
                    dr[KcbLuotkham.Columns.NgayketthucBhyt] = Utility.null2DBNull( objNew.NgayketthucBhyt,DBNull.Value);
                    dr[KcbLuotkham.Columns.DiachiBhyt] = objNew.DiachiBhyt;
                    dr[KcbLuotkham.Columns.MaDoituongKcb] = objNew.MaDoituongKcb;
                    dr[KcbLuotkham.Columns.PtramBhyt] = objNew.PtramBhyt;
                    dr[KcbLuotkham.Columns.DungTuyen] = objNew.DungTuyen;
                    dr[KcbLuotkham.Columns.MaKcbbd] = objNew.MaKcbbd;
                    dr["ten_kcbbd"] = objNew.IdLoaidoituongKcb == 1 ? "" : ten_kcbbd;
                }
                m_dtSearchdata.AcceptChanges();
            }
            catch
            {
            }
        }
        
        private void cmdInhoadon_Click(object sender, EventArgs e)
        {
            //Tạm rem đoạn xem phiếu
            //CallPhieuThu();
            if (!Utility.isValidGrid(grdPayment))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một phiếu thanh toán để in hóa đơn trong lưới bên dưới","thông báo");
                return;
            }
            InHoadon();
            
        }
      
        void InHoadon()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
            }
        }
       
       
      
       
        private KcbPhieuthu CreatePhieuThu(int _Payment_ID, decimal TONG_TIEN)
        {
            var objPhieuThu = new KcbPhieuthu();
            objPhieuThu.IdThanhtoan = _Payment_ID;
            objPhieuThu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
            objPhieuThu.SoluongChungtugoc = 1;
            objPhieuThu.LoaiPhieuthu = Convert.ToByte(0);
            objPhieuThu.NgayThuchien = globalVariables.SysDate;
            objPhieuThu.SoTien = TONG_TIEN;
            objPhieuThu.SotienGoc = TONG_TIEN;
            objPhieuThu.NguoiNop = globalVariables.UserName;
            objPhieuThu.TaikhoanCo = "";
            objPhieuThu.TaikhoanNo = "";
            objPhieuThu.LydoNop = "Thu tiền bệnh nhân";
            return objPhieuThu;
        }
       
       

        /// <summary>
        ///     hàmh tực hiện việc dóng form lưu lại thông tin cấu hình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_THANHTOAN_NGOAITRU_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

       

        private void cmdCapnhatngayinphoiBHYT_Click_1(object sender, EventArgs e)
        {
        }

       

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin bệnh nhân lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLuuLai_Click(object sender, EventArgs e)
        {
        }

       

       

    }
}