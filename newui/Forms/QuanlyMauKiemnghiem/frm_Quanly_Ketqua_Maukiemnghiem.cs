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
using VNS.HIS.DAL;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.CanLamSang;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_Quanly_Ketqua_Maukiemnghiem : Form
    {
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private DataTable m_dtDichvuKn=new DataTable();
        private bool m_blnHasloaded = false;

        public frm_Quanly_Ketqua_Maukiemnghiem()
        {
            InitializeComponent();
            //this.InitTrace();
            this.KeyPreview = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;
            
            InitEvents();
        }
        void InitEvents()
        {
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            this.txtPatient_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_ID_KeyDown);
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);

            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);

            this.Load += new System.EventHandler(this.frm_Quanly_Ketqua_Maukiemnghiem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanly_Ketqua_Maukiemnghiem_KeyDown);
            grdDetail.SelectionChanged += grdDetail_SelectionChanged;
            cmdNhanmau.Click += cmdNhanmau_Click;
            cmdNhapKQ.Click += cmdNhapKQ_Click;
            cmdXacnhanKQ.Click += cmdXacnhanKQ_Click;
        }

        void cmdXacnhanKQ_Click(object sender, EventArgs e)
        {
            XacnhanKQ(cmdXacnhanKQ.Tag.ToString() == "0");
        }
        void XacnhanKQ(bool xacnhan)
        {
            bool _isValid = Utility.isValidGrid(grdDetail);
            bool _autoChecked = false;
            try
            {
                if (grdDetail.GetCheckedRows().Count() <= 0)
                {
                    if (!_isValid)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một mẫu kiểm nghiệm để xác nhận/hủy xác nhận kết quả");
                        return;
                    }
                    else
                    {
                        _autoChecked = true;
                        grdDetail.CurrentRow.BeginEdit();
                        grdDetail.CurrentRow.IsChecked = true;
                        grdDetail.CurrentRow.EndEdit();
                    }
                }
                List<string> lstIdchidinhchitiet = (from chidinh in grdDetail.GetCheckedRows().AsEnumerable()
                                                    let x = Utility.sDbnull(chidinh.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value)
                                                    select x).ToList<string>();
                string _IdChitietchidinh = string.Join(",", lstIdchidinhchitiet.ToArray());
                string Question = string.Format("Bạn có muốn xác nhận kết quả các mẫu đang chọn hay không?\nChú ý: Chỉ các mẫu có kết quả và chưa xác nhận mới được xác nhận");
                byte trangthaicu = 3;
                byte trangthaimoi = 4;
                if (!xacnhan)
                {
                    trangthaicu = 4;
                    trangthaimoi = 3;
                    Question = string.Format("Bạn có muốn hủy xác nhận kết quả các mẫu đang chọn hay không?\nChú ý: Chỉ các mẫu có kết quả và chưa in mới được hủy xác nhận");
                }
                if (Utility.AcceptQuestion(Question, "Thông báo", true))
                {
                    cmdXacnhanKQ.Text = xacnhan ? "Hủy xác nhận kết quả" : "Xác nhận kết quả";
                    cmdXacnhanKQ.Tag = xacnhan ? 1 : 0;
                    _KCB_CHIDINH_CANLAMSANG.MaukiemnghiemCapnhattrangthai(_IdChitietchidinh, trangthaicu, trangthaimoi);
                    m_dtDichvuKn.AsEnumerable()
                                    .Where(c => Utility.ByteDbnull(c[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan], 0) == 1 && Utility.sDbnull(c["trangthai_chuyencls"], "") == trangthaicu.ToString() && lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trangthai_chuyencls"] = trangthaimoi; c1["ten_trangthai_chuyencls"] = trangthaimoi == 4 ? "Đã xác nhận kết quả" : "Đã có kết quả"; });
                    m_dtDichvuKn.AcceptChanges();
                    ModifyCommand();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            if (_autoChecked)
            {
                grdDetail.CurrentRow.BeginEdit();
                grdDetail.CurrentRow.IsChecked = false;
                grdDetail.CurrentRow.EndEdit();
            }
        }
        void cmdNhapKQ_Click(object sender, EventArgs e)
        {
            frm_nhapketquaKN _nhapketquaKN = new frm_nhapketquaKN();
            string mahoa_mau = Utility.GetValueFromGridColumn(grdDetail, "mahoa_mau");
            _nhapketquaKN._OnResult += _nhapketquaKN__OnResult;
            _nhapketquaKN.AutoSearch(mahoa_mau);
            _nhapketquaKN.ShowDialog(); 
        }

        void _nhapketquaKN__OnResult(long id_chitiet, byte tthai_cls)
        {
            try
            {
                 m_dtDichvuKn.AsEnumerable()
                                    .Where(c =>Utility.Int64Dbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))==id_chitiet)
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trangthai_chuyencls"] = tthai_cls; c1["ten_trangthai_chuyencls"] = (tthai_cls == 4 ? "Đã xác nhận kết quả" : (tthai_cls == 2?"Đang nhập kết quả":"Đã có kết quả")); });
                    m_dtDichvuKn.AcceptChanges();
            }
            catch (Exception ex)
            {
                
                
            }
        }

      
        void Nhanmau(bool nhanmau)
        {
             bool _isValid = Utility.isValidGrid(grdDetail);
                bool _autoChecked = false;
            try
            {
                if (grdDetail.GetCheckedRows().Count() <= 0)
                {
                    if (!_isValid)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một mẫu kiểm nghiệm để bàn giao");
                        return;
                    }
                    else
                    {
                        _autoChecked = true;
                        grdDetail.CurrentRow.BeginEdit();
                        grdDetail.CurrentRow.IsChecked = true;
                        grdDetail.CurrentRow.EndEdit();
                    }
                }
                List<string> lstIdchidinhchitiet = (from chidinh in grdDetail.GetCheckedRows().AsEnumerable()
                                                    let x = Utility.sDbnull(chidinh.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value)
                                                    select x).ToList<string>();
                string _IdChitietchidinh = string.Join(",", lstIdchidinhchitiet.ToArray());
                string Question = string.Format("Bạn có muốn nhận các mẫu đang chọn hay không?");
                byte trangthaicu = 1;
                byte trangthaimoi = 2;
                if (!nhanmau)
                {
                    trangthaicu = 2;
                    trangthaimoi = 1;
                    Question = string.Format("Bạn có muốn hủy nhận các mẫu đang chọn hay không?");
                }
                if (Utility.AcceptQuestion(Question, "Thông báo", true))
                {
                    cmdNhanmau.Text = nhanmau ? "Hủy nhận mẫu" : "Nhận mẫu";
                    cmdNhanmau.Tag = nhanmau ? 1 : 0;
                    _KCB_CHIDINH_CANLAMSANG.MaukiemnghiemCapnhattrangthai(_IdChitietchidinh, trangthaicu, trangthaimoi);
                    m_dtDichvuKn.AsEnumerable()
                                    .Where(c => Utility.ByteDbnull(c[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan], 0) == 1 && Utility.sDbnull(c["trangthai_chuyencls"], "") == trangthaicu.ToString() && lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trangthai_chuyencls"] = trangthaimoi; c1["ten_trangthai_chuyencls"] = trangthaimoi == 1 ? "Đã bàn giao" : "Đang nhập kết quả"; });
                    m_dtDichvuKn.AcceptChanges();
                    ModifyCommand();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                if (_autoChecked)
                {
                    grdDetail.CurrentRow.BeginEdit();
                    grdDetail.CurrentRow.IsChecked = false;
                    grdDetail.CurrentRow.EndEdit();
                }
            }
        }
        void cmdNhanmau_Click(object sender, EventArgs e)
        {
            Nhanmau(cmdNhanmau.Tag.ToString() == "0");
        }

        void grdDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }
        void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string  mayin="";
                int v_AssignId = Utility.Int32Dbnull(grdDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";

                KCB_INPHIEU.InphieuDangkyKiemnghiem(v_AssignId);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       
      
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                byte trang_thai = 10;
                if (optChuacoKQ.Checked) trang_thai = 1;
                if (optDanhapKQ.Checked) trang_thai = 2;
                if (optDaXacnhanKQ.Checked) trang_thai = 4;
                m_dtDichvuKn = _KCB_CHIDINH_CANLAMSANG.MaukiemnghiemLayChitietDangkyKiemnghiem(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text), Utility.Int32Dbnull(txtDichvuKn.MyID, -1), Utility.Int32Dbnull(txtMauKn.MyID, -1),trang_thai);
                Utility.SetDataSourceForDataGridEx(grdDetail, m_dtDichvuKn, true, true, "1=1","");
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }
       
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
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
        private bool AllowTextChanged;
        
      
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Quanly_Ketqua_Maukiemnghiem_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            AllowTextChanged = true;
            TimKiemThongTin(true);
            m_blnHasloaded = true;
        }
        private DataTable m_dataDataRegExam=new DataTable();
       
       
        /// <summary>
        /// hàm thực hiện việc cáu hình trạng thái của nút
        /// 0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
        /// </summary>
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdDetail);
            int trang_thai = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdDetail, "trangthai_chuyencls"), 0);
            cmdNhapKQ.Enabled = isValid && (trang_thai == 2 || trang_thai == 3);
            cmdXacnhanKQ.Enabled = isValid && (trang_thai == 3 || trang_thai == 4);
            cmdXacnhanKQ.Text = trang_thai == 3 ? "Xác nhận kết quả" : "Hủy xác nhận kết quả";
            cmdXacnhanKQ.Tag = trang_thai == 3 ? 0 : 1;
            cmdNhanmau.Enabled = isValid && (trang_thai == 1 || trang_thai == 2);
            cmdNhanmau.Text = trang_thai == 1 ? "Nhận mẫu" : "Hủy nhận mẫu";
            cmdNhanmau.Tag = trang_thai == 1 ? 0 : 1;
        }
        

      
       
        private void frm_Quanly_Ketqua_Maukiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);      
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
           
            if (e.KeyCode == Keys.N && e.Control) cmdNhapKQ.PerformClick();
            if (e.KeyCode == Keys.X && e.Control) cmdXacnhanKQ.PerformClick();
        }
        
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "") 
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string _Code ="KN"+ Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientCode.Text = _Code;
                    TimKiemThongTin(false);
                    if (grdDetail.RowCount == 1) grdDetail_SelectionChanged(grdDetail, new EventArgs());
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khách hàng");
            }
        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatient_ID.Text.Trim())!="") 
                {
                    string _code = txtPatientCode.Text.Trim();
                    txtPatientCode.Clear();
                    TimKiemThongTin(false);
                    if ( grdDetail.RowCount == 1) grdDetail_SelectionChanged(grdDetail, new EventArgs());
                    txtPatientCode.Text = _code;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khách hàng");
            }
        }

    }
}
