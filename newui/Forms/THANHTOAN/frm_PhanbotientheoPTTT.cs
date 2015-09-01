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
    public partial class frm_PhanbotientheoPTTT : Form
    {
        public int v_Payment_Id = -1;
        public bool m_blnCancel = true;
        public KcbLuotkham objLuotkham;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        bool m_blnLoaded = false;
        private DataTable m_dtData = new DataTable();
        public frm_PhanbotientheoPTTT(int v_Payment_Id)
        {
            InitializeComponent();
            this.v_Payment_Id = v_Payment_Id;
            InitEvents();
            setProperties();
            
        }
        void InitEvents()
        {
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            this.Load += new System.EventHandler(this.frm_PhanbotientheoPTTT_Load);
            this.KeyDown += new KeyEventHandler(frm_PhanbotientheoPTTT_KeyDown);
            cmdAccept.Click += cmdAccept_Click;
            grdList.KeyDown += grdList_KeyDown;
            grdList.UpdatingCell += grdList_UpdatingCell;
            txtSotien.KeyDown += txtSotien_KeyDown;
        }

        void txtSotien_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || (e.Control && e.KeyCode == Keys.A) || (e.Control && e.KeyCode == Keys.C))
                {
                    if (txtPttt.myCode == "-1")
                    {
                        txtPttt.Focus();
                        txtPttt.SelectAll();
                        return;
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            string ma_pttt = txtPttt.myCode;
                            decimal tong_tien = Utility.DecimaltoDbnull(txtSotien.Text,0);
                            if (tong_tien > 0)
                            {
                                DataRow[] arrDr = m_dtData.Select("ma_pttt='" + ma_pttt + "'");
                                if (arrDr.Length > 0)
                                {
                                    arrDr[0]["so_tien"] = tong_tien;
                                }
                                m_dtData.AcceptChanges();
                                txtSotien.Text = "";
                                txtPttt.SetCode("-1");
                                txtPttt.Focus();
                                txtPttt.SelectAll();
                            }
                            return;
                        }
                        else if (e.Control && e.KeyCode == Keys.C)
                        {
                            string ma_pttt = txtPttt.myCode;
                            decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                            decimal tongtienkhac = 0;
                            decimal conlai = 0;
                            DataRow[] arrDr = m_dtData.Select("ma_pttt<>'" + ma_pttt + "'");
                            if (arrDr.Length > 0)
                            {
                                tongtienkhac = Utility.DecimaltoDbnull(arrDr.CopyToDataTable().Compute("SUM(so_tien)", "1=1"), 0);
                            }
                            conlai = Utility.DecimaltoDbnull(tong_tien, 0) - tongtienkhac;
                            arrDr = m_dtData.Select("ma_pttt='" + ma_pttt + "'");
                            if (arrDr.Length > 0)
                            {
                                arrDr[0]["so_tien"] = conlai;
                            }
                            m_dtData.AcceptChanges();
                            txtPttt.SetCode("-1");
                            return;
                        }
                        else if (e.Control && e.KeyCode == Keys.A)
                        {
                            string ma_pttt = txtPttt.myCode;
                            decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                            foreach (DataRow dr in m_dtData.Rows)
                            {
                                if (Utility.sDbnull(dr["ma_pttt"], "") != ma_pttt)
                                {
                                    dr["so_tien"] = 0;
                                }
                                else
                                {
                                    dr["so_tien"] = tong_tien;
                                }
                            }
                            m_dtData.AcceptChanges();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                CheckSum();
            }
            
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                if (e.Control && e.KeyCode == Keys.C)
                {
                    string ma_pttt = Utility.GetValueFromGridColumn(grdList, "ma_pttt");
                    decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                    decimal tongtienkhac = 0;
                    decimal conlai = 0;
                    DataRow[] arrDr = m_dtData.Select("ma_pttt<>'" + ma_pttt + "'");
                    if (arrDr.Length > 0)
                    {
                        tongtienkhac = Utility.DecimaltoDbnull(arrDr.CopyToDataTable().Compute("SUM(so_tien)", "1=1"), 0);
                    }
                    conlai = Utility.DecimaltoDbnull(tong_tien, 0) - tongtienkhac;
                    arrDr = m_dtData.Select("ma_pttt='" + ma_pttt + "'");
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["so_tien"] = conlai;
                    }
                    m_dtData.AcceptChanges();
                    return;
                }
                if (e.Control && e.KeyCode == Keys.A)
                {
                    string ma_pttt = Utility.GetValueFromGridColumn(grdList, "ma_pttt");
                    decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                    decimal tongtienkhac = 0;
                    foreach (DataRow dr in m_dtData.Rows)
                    {
                        if (Utility.sDbnull(dr["ma_pttt"], "") != ma_pttt)
                        {
                            dr["so_tien"] = 0;
                        }
                        else
                        {
                            dr["so_tien"] = tong_tien;
                        }
                    }
                    m_dtData.AcceptChanges();
                    return;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                CheckSum();
            }

        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;
                if (e.Column.Key == "so_tien")
                {
                    errorProvider1.SetError(txtTongtien, "");
                    string ma_pttt=Utility.GetValueFromGridColumn(grdList,"ma_pttt");
                    decimal tong_tien = Utility.DecimaltoDbnull(txtTongtien.Text);
                    decimal tongtienkhac = 0;
                    DataRow[] arrDr= m_dtData.Select("ma_pttt<>'" + ma_pttt + "'");
                    if (arrDr.Length > 0)
                    {
                        tongtienkhac = Utility.DecimaltoDbnull(arrDr.CopyToDataTable().Compute("SUM(so_tien)", "1=1"), 0);
                    }
                    if (tongtienkhac + Utility.DecimaltoDbnull(e.Value) > Utility.DecimaltoDbnull(tong_tien, 0))
                    {
                       // e.Cancel = true;
                        errorProvider1.SetError(txtTongtien, "Tổng tiền phân bổ theo các phương thức thanh toán cần phải bằng tổng tiền thanh toán. Mời bạn kiểm tra lại");
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        void CheckSum()
        {
            errorProvider1.SetError(txtTongtien, "");
            if (Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
            {
                errorProvider1.SetError(txtTongtien, "Tổng tiền phân bổ theo các phương thức thanh toán cần phải bằng tổng tiền thanh toán. Mời bạn kiểm tra lại");
            }
        }
        void cmdAccept_Click(object sender, EventArgs e)
        {
            if (m_dtData.Select("so_tien<0").Length>0 || Utility.DecimaltoDbnull(m_dtData.Compute("SUM(so_tien)", "1=1"), 0) != Utility.DecimaltoDbnull(txtTongtien.Text, 0))
            {
                errorProvider1.SetError(txtTongtien, "Tổng tiền phân bổ theo các phương thức thanh toán cần phải bằng tổng tiền thanh toán và tiền PTTT không được phép chứa giá trị nhỏ hơn không. Mời bạn kiểm tra lại");
                return;
            }
            string errMsg = "";
            if (_THANHTOAN.UpdateTienphanbotheoPTTT(m_dtData, ref errMsg) != ActionResult.Success)
            {
                Utility.ShowMsg(errMsg);
            }
            else
            {
                this.Close();
            }
        }
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        /// <summary>
        /// /hàm thực hiện load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PhanbotientheoPTTT_Load(object sender, EventArgs e)
        {
            txtPttt.Init();
            GetData();
            Utility.focusCell(grdList, "so_tien");
            m_blnLoaded = true;
        }
        
        /// <summary>
        /// lấy thông tin dữ liệu
        /// </summary>
        private void GetData()
        {
            try
            {
                m_dtData = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPTTT(v_Payment_Id);
                m_dtData.AcceptChanges();
                grdList.DataSource = m_dtData;
                if (m_dtData != null && m_dtData.Rows.Count > 0)
                {
                    if (m_dtData.Select("Tong_tien>0").Length > 0)
                        txtTongtien.Text = Utility.sDbnull(m_dtData.Select("Tong_tien>0")[0]["Tong_tien"], "0");
                    else
                    {
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(v_Payment_Id);
                        if (objThanhtoan != null)
                            txtTongtien.Text = Utility.sDbnull(objThanhtoan.BnhanChitra, "0");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                
            }
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
        private void frm_PhanbotientheoPTTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdAccept_Click(cmdAccept, new EventArgs());
        }
        
       
       
    }
}
