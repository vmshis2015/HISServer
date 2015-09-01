using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.Forms.NGOAITRU;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_HuyThanhtoan : Form
    {
        public int v_Payment_Id = -1;
        public bool m_blnCancel = true;
        public KcbLuotkham objLuotkham;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        bool m_blnLoaded = false;
        public frm_HuyThanhtoan()
        {
            InitializeComponent();
            setProperties();
            this.KeyDown+=new KeyEventHandler(frm_HuyThanhtoan_KeyDown);
            chkNoview.CheckedChanged += new EventHandler(chkNoview_CheckedChanged);
            chkNoview.Checked = PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan;

            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            cmdInBienlai.Click += new EventHandler(cmdInBienlai_Click);
            cmdInBienlaiTonghop.Click += new EventHandler(cmdInBienlaiTonghop_Click);
            cmdInphieuDCT.Click += new EventHandler(cmdInphieuDCT_Click);
            cmdInphoiBHYT.Click += new EventHandler(cmdInphoiBHYT_Click);
            cmdClose1.Click += new EventHandler(cmdClose1_Click);
        }

        void cmdClose1_Click(object sender, EventArgs e)
        {
            cmdExit_Click(cmdExit, e);
        }

        void cmdInphoiBHYT_Click(object sender, EventArgs e)
        {
           DataTable m_dtPayment =
                   new KCB_THANHTOAN().LaythongtinCacLanthanhtoan(objLuotkham.MaLuotkham,
                      Utility.Int32Dbnull( objLuotkham.IdBenhnhan), 0,objLuotkham.TrangthaiNoitru,0,
                       globalVariables.MA_KHOA_THIEN);
            if(objLuotkham.MabenhChinh != null)
            {
                new INPHIEU_THANHTOAN_NGOAITRU().InPhoiBHYT(objLuotkham, m_dtPayment, dtPaymentDate.Value);
            }
            else
            {
                Utility.ShowMsg("Bệnh nhân chưa có mã ICD10 để in phôi BHYT.\n Quay lại phòng bác sỹ để nhập mã ICD10");
                return;
            }
        
        }

        void cmdInphieuDCT_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InphieuDCT_Benhnhan(objLuotkham, m_dtPaymentDetail);
        }

        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, v_Payment_Id, objLuotkham);
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_Id, objLuotkham);
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPaymentDetail))
                return;
            new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(v_Payment_Id); 
        }
      
        public bool ShowCancel
        {
            set {
                if (value) pnlHuyThanhtoan.BringToFront();
                else pnlHuyThanhtoan.SendToBack();
            }
        }
        void chkNoview_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan = chkNoview.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
        }
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        /// <summary>
        /// /hàm thực hiện load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_HuyThanhtoan_Load(object sender, EventArgs e)
        {
           List<Control> lstCtrls=new List<Control>();
            GetData();
            pnlInfor.Height = 209;
            if (objLuotkham.IdLoaidoituongKcb == 1)
            {
                pnlInfor.Height = 91;
                lstCtrls.AddRange(new Control[] {pnlActions, lblNgaythanhtoan, dtPaymentDate, lblTongtien, txtTongChiPhi });
            }
            Visible(lstCtrls);
            ModifyComamd();
            m_blnLoaded = true;
        }
        private  DataTable m_dtPaymentDetail=new DataTable();
        /// <summary>
        /// lấy thông tin dữ liệu
        /// </summary>
        private void GetData()
        {
            m_dtPaymentDetail = _THANHTOAN.Laychitietthanhtoan(v_Payment_Id);
            m_dtPaymentDetail.AcceptChanges();
            grdPaymentDetail.DataSource = m_dtPaymentDetail;
            SetSumTotalProperties();
        }
       public decimal Chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {
                GridEXColumn gridExColumntrangthaithanhtoan = getGridExColumn(grdPaymentDetail, "trangthai_thanhtoan");
                GridEXColumn gridExColumn = getGridExColumn(grdPaymentDetail, "TT_KHONG_PHUTHU");
                GridEXColumn gridExColumn_tutuc = getGridExColumn(grdPaymentDetail, "TT_BN_KHONG_TUTUC");
                GridEXColumn gridExColumnTT = getGridExColumn(grdPaymentDetail, "TT");
                GridEXColumn gridExColumnTT_chietkhau = getGridExColumn(grdPaymentDetail, KcbThanhtoanChitiet.Columns.TienChietkhau);
                GridEXColumn gridExColumnBHYT = getGridExColumn(grdPaymentDetail, "TT_BHYT");
                GridEXColumn gridExColumnTTBN = getGridExColumn(grdPaymentDetail, "TT_BN");
                GridEXColumn gridExColumntutuc = getGridExColumn(grdPaymentDetail, "tu_tuc");
                GridEXColumn gridExColumntrangthai_huy = getGridExColumn(grdPaymentDetail, "trangthai_huy");
                GridEXColumn gridExColumnPhuThu = getGridExColumn(grdPaymentDetail,
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
                GridEXColumn gridExColumnBNCT = getGridExColumn(grdPaymentDetail,
                    "bnhan_chitra");
                // Janus.Windows.GridEX.GridEXColumn gridExColumnTuTuc = getGridExColumn(grdPaymentDetail, "bnhan_chitra");
                decimal BN_KHONGTUTUC =
                  Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumn_tutuc, AggregateFunction.Sum),
                      gridExFilterCondition_khong_Tutuc);
                decimal TT =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTT, AggregateFunction.Sum),
                        gridExFilterConditiontrangthai_huy);
                decimal TT_Chietkhau =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTT_chietkhau, AggregateFunction.Sum),
                       gridExFilterConditiontrangthai_huy);

                decimal TT_KHONG_PHUTHU =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumn, AggregateFunction.Sum),
                       gridExFilterConditiontrangthai_huy);
                decimal TT_BHYT =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnBHYT, AggregateFunction.Sum,
                        gridExFilterConditiontrangthai_huy));
                decimal TT_BN =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                        gridExFilterConditiontrangthai_huy));
               
                Chuathanhtoan =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                       gridExFilterChuathanhtoan));
                //Tạm bỏ
                //decimal PtramBHYT = 0;
                //_THANHTOAN.LayThongPtramBHYT(TongChiphiBHYT, objLuotkham, ref PtramBHYT);
                decimal PhuThu =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));
                decimal TuTuc =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnBNCT, AggregateFunction.Sum,
                        gridExFilterConditionTutuc));
                //txtPtramBHChiTra.Text = Utility.sDbnull(PtramBHYT);
                txtTongChiPhi.Text = Utility.sDbnull(TT);
                TT_KHONG_PHUTHU -= TuTuc;
                txtTongtienDCT.Text = objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_BHYT + BN_KHONGTUTUC);// objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_KHONG_PHUTHU);
                txtPhuThu.Text = Utility.sDbnull(PhuThu);
                txtTuTuc.Text = Utility.sDbnull(TuTuc);
                //decimal BHCT = TongChiphiBHYT*PtramBHYT/100;
                txtBHCT.Text = Utility.sDbnull(TT_BHYT, "0");
                decimal BNCT = BN_KHONGTUTUC;
                txtBNCT.Text = Utility.sDbnull(BNCT);
                decimal BNPhaiTra = BNCT + Utility.DecimaltoDbnull(txtTuTuc.Text, 0) +
                                    Utility.DecimaltoDbnull(txtPhuThu.Text);
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);

                KcbThanhtoanCollection _item = new KcbThanhtoanController().FetchByID(v_Payment_Id);
                if (!_item.Any())
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                }
                else
                {
                    txtDachietkhau.Text = _item.FirstOrDefault().TongtienChietkhau.ToString();
                    txtsotiendathu.Text = (_item.FirstOrDefault().BnhanChitra - Utility.DecimaltoDbnull(txtDachietkhau.Text, 0)).ToString();
                }
                ModifyCommand();
            }
            catch
            { }
        }
        private void setProperties()
        {
            try
            {
                foreach (Control control in pnlInfor.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox)(control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
               
            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txt = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txt);
        }
        private void ModifyCommand()
        {
            try
            {
                cmdInhoadon.Enabled = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objLuotkham != null;
                cmdInBienlai.Visible = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objLuotkham != null;
                cmdInphoiBHYT.Visible = Chuathanhtoan <= 0 && Utility.DecimaltoDbnull(txtsotiendathu.Text) > 0 && objLuotkham.MaDoituongKcb == "BHYT" && grdPaymentDetail.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInphieuDCT.Visible = objLuotkham.MaDoituongKcb == "BHYT" && grdPaymentDetail.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInBienlaiTonghop.Visible = TotalPayment > 1 && objLuotkham != null;
            }
            catch (Exception)
            {
                //throw;
            }
        }
        void Visible(List<Control> lstCtrls)
        {
            bool _visible = lstCtrls.Count <= 0;
            foreach (Control ctrl in pnlInfor.Controls)
            {
                ctrl.Visible =_visible || lstCtrls.Contains(ctrl);
            }
        }
        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }
        /// <summary>
        /// hàm thực hiện thoát khỏi form hienj tại
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thự hiện việc hủy thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_HuyThanhtoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F5) cmdPrint.PerformClick();
           // if (e.KeyCode == Keys.F4) cmdInPhieu.PerformClick();
        }
        private void ModifyComamd()
        {
            cmdPrint.Enabled = m_dtPaymentDetail.DefaultView.Count > 0;
        }
        string ma_lydohuy = "";
        string lydo_huy = "";
        private ActionResult actionResult = ActionResult.Error;
        /// <summary>
        /// hàm thực hiện việc hủy thanh toán 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
            {
                Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy phiếu chi ngoại trú nữa");
                return;
            }
            KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_Id);
            //Kiểm tra ngày hủy
            int KCB_THANHTOAN_SONGAY_HUYTHANHTOAN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
            int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
            if (Chenhlech > KCB_THANHTOAN_SONGAY_HUYTHANHTOAN)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán {0} - Ngày hủy thanh toán {1}. Hệ thống không cho phép bạn hủy thanh toán đã quá {2} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.ToString("dd/MM/yyyy"), globalVariables.SysDate.ToString("dd/MM/yyyy"), KCB_THANHTOAN_SONGAY_HUYTHANHTOAN.ToString()));
                return;
            }
             if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                 if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_Payment_Id.ToString()), "Thông báo", true))
                 {
                     return;
                 }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
            {
                frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán");
                _Nhaplydohuythanhtoan.ShowDialog();
                m_blnCancel = _Nhaplydohuythanhtoan.m_blnCancel;
                if(m_blnCancel) return;
                ma_lydohuy = _Nhaplydohuythanhtoan.ma;
            }
            bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
            actionResult = _THANHTOAN.HuyThanhtoan(objPayment, objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdPaymentDetail.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
            int record = -1;
            switch (actionResult)
            {
                case ActionResult.Success:
                    ModifyComamd();
                    Utility.ShowMsg("Bạn hủy thông tin thanh toán thành công", "Thông báo");
                    m_blnCancel = false;
                    cmdExit.PerformClick();
                    break;
                case ActionResult.ExistedRecord:
                    Utility.ShowMsg("Thuốc đã cấp phát cho Bệnh nhân nên cần trả lại thuốc bên Dược mới có thể thực hiện hủy thanh toán", "Thông báo", MessageBoxIcon.Warning);
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                    break;
            }

        }
       
    }
}
