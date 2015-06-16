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
using VNS.Libs;


namespace VNS.HIS.UI.THUOC
{
    public partial class frm_PhieuDuTru : Form
    {
        private DataTable m_Thuoc = new DataTable();
        string KIEU_THUOC_VT = "THUOC";
        bool hasLoaded = false;
        public frm_PhieuDuTru(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
            txtthuoc._OnEnterMe += new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtthuoc__OnEnterMe);
            txtSoluongdutru.KeyDown += new KeyEventHandler(txtSoluongdutru_KeyDown);
            txtthuoc._OnSelectionChanged += new UCs.AutoCompleteTextbox_Thuoc.OnSelectionChanged(txtthuoc__OnSelectionChanged);
            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            this.KeyDown += new KeyEventHandler(frm_PhieuDuTru_KeyDown);
            mnuHuy.Click += new EventHandler(mnuHuy_Click);
            mnuHuyAll.Click += new EventHandler(mnuHuyAll_Click);
            chkUpdate.CheckedChanged += new EventHandler(chkUpdate_CheckedChanged);
            chkHienthithuoccoDutru.CheckedChanged += new EventHandler(chkHienthithuoccoDutru_CheckedChanged);
        }

        void chkHienthithuoccoDutru_CheckedChanged(object sender, EventArgs e)
        {
            m_Thuoc.DefaultView.Sort = chkHienthithuoccoDutru.Checked ? "COQUANHE desc,ten_thuoc,ten_donvitinh" : "ten_thuoc,ten_donvitinh"; ;
        }

        void chkUpdate_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                mnuHuy.Enabled = false;

            }
            else
            {
                int SOLUONG = Utility.Int32Dbnull(grdList.GetValue("SO_LUONG"), 0);
                mnuHuy.Enabled = SOLUONG > 0;
            }
        }

        void mnuHuyAll_Click(object sender, EventArgs e)
        {
            Huydutru();

        }

        void mnuHuy_Click(object sender, EventArgs e)
        {
            Huydutru();
        }

        void frm_PhieuDuTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5) cboKho_SelectedIndexChanged(cboKho, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave_Click(cmdSave, new EventArgs());
        }

        void txtthuoc__OnSelectionChanged()
        {
            try
            {
                int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                if (_idthuoc > 0)
                {
                    var q = from p in grdList.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                            select p;
                    if (q.Count() > 0)
                    {
                        grdList.MoveTo(q.First());
                    }
                }
                else
                {
                    grdList.MoveFirst();
                }
            }
            catch
            {
            }
        }

        void txtSoluongdutru_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.Int32Dbnull(txtthuoc.MyID, -1) > 0)
                {
                    int IDTHUOC = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                    Int16 IDKHO = Utility.Int16Dbnull(cboKho.SelectedValue);
                    int SOLUONG = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluongdutru.Text, 0));
                    var q = from p in grdList.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == IDTHUOC
                            select p;

                    int SLUONG_TRONGKHO = q.Count() > 0 ? Utility.Int32Dbnull(q.FirstOrDefault().Cells["SLUONG_TRONGKHO"].Value, 0) : 0;

                    int SLUONG_CANCHUYEN = SOLUONG - SLUONG_TRONGKHO;
                    SLUONG_CANCHUYEN = SLUONG_CANCHUYEN <= 0 ? 0 : SLUONG_CANCHUYEN;

                    DataRow[] dr = m_Thuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + IDTHUOC.ToString());
                    if (dr.Length > 0)
                    {
                        dr[0]["SO_LUONG"] = SOLUONG <= 0 ? 0 : SOLUONG;
                        dr[0]["COQUANHE"] = SOLUONG <= 0 ? 0 : 1;
                        grdList.SetValue("SLUONG_CANCHUYEN", SLUONG_CANCHUYEN);
                        m_Thuoc.AcceptChanges();
                    }

                    TDutruThuocCollection lst =
                        new Select().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                            .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT)
                            .ExecuteAsCollection<TDutruThuocCollection>();
                    if (lst.Count > 0)
                    {
                        if (SOLUONG <= 0)
                        {
                            new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                            .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                        }
                        else
                        {
                            new Update(TDutruThuoc.Schema)
                                .Set("SO_LUONG").EqualTo(SOLUONG)
                                .Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC).And(TDutruThuoc.Columns.IdKho).
                                IsEqualTo(IDKHO)
                                .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                        }


                    }
                    else
                    {
                        TDutruThuoc objThongTin = new TDutruThuoc();
                        objThongTin.IdThuoc = IDTHUOC;
                        objThongTin.KieuThuocVt = KIEU_THUOC_VT;
                        objThongTin.IdKho = IDKHO;
                        objThongTin.SoluongDutru = SOLUONG;
                        objThongTin.IsNew = true;
                        objThongTin.Save();
                    }
                    txtthuoc.ResetText();
                    txtthuoc.Focus();
                }
            }
            catch
            {
            }
            finally
            {
               
            }
        }

        void txtthuoc__OnEnterMe()
        {
            int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
            if (_idthuoc > 0)
            {
                if (m_Thuoc != null && m_Thuoc.Columns.Count > 0)
                {
                    DataRow[] dr = m_Thuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + _idthuoc.ToString());
                    if (dr.Length > 0)
                    {
                        txtSoluongdutru.Text = Utility.sDbnull(dr[0]["SO_LUONG"], "0");
                    }
                }

                txtSoluongdutru.Focus();
                txtSoluongdutru.SelectAll();
            }
            else
            {
                txtSoluongdutru.Clear();
            }
        }

        private void frm_PhieuDuTru_Load(object sender, EventArgs e)
        {
            AutocompleteThuoc();
            DataBinding.BindDataCombobox(cboKho, this.KIEU_THUOC_VT.TrimStart().TrimEnd() == "THUOC" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOCLE_TUTRUC(this.KIEU_THUOC_VT.TrimStart().TrimEnd()) : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" }), TDmucKho.Columns.IdKho,
                                 TDmucKho.Columns.TenKho, "Chọn kho", true);
            hasLoaded = true;
            LoadThongTinThuoc();
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
        private void LoadThongTinThuoc()
        {
            if (!hasLoaded || Utility.Int16Dbnull(cboKho.SelectedValue) <= 0)
            {
                m_Thuoc.Rows.Clear();
                return;
            }
            else
                m_Thuoc = SPs.ThuocLaythongtinDutruthuoc(Utility.Int16Dbnull(cboKho.SelectedValue), KIEU_THUOC_VT).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx_Basic(grdList, m_Thuoc, true, true, "1=1", "ten_thuoc,ten_donvitinh");
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(cboKho.SelectedValue) > 0)
            {
                LoadThongTinThuoc();
            }
            else
            {
            }
        }



        private void grdList_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                {
                    int IDTHUOC = Utility.Int32Dbnull(grdList.GetValue(DmucThuoc.Columns.IdThuoc), 0);
                    Int16 IDKHO = Utility.Int16Dbnull(cboKho.SelectedValue);
                    int SOLUONG = Utility.Int32Dbnull(grdList.GetValue("SO_LUONG"), 0);
                    int SLUONG_TRONGKHO = Utility.Int32Dbnull(grdList.GetValue("SLUONG_TRONGKHO"), 0);
                    grdList.CurrentRow.BeginEdit();
                    if (SOLUONG <= 0)
                    {
                        grdList.CurrentRow.Cells["COQUANHE"].Value= 0;
                        grdList.CurrentRow.Cells["SO_LUONG"].Value= 0;
                        grdList.CurrentRow.Cells["SLUONG_CANCHUYEN"].Value = 0;
                    }
                    else
                    {
                        grdList.CurrentRow.Cells["COQUANHE"].Value= 1;
                        grdList.CurrentRow.Cells["SO_LUONG"].Value= SOLUONG;
                        grdList.CurrentRow.Cells["SLUONG_CANCHUYEN"].Value= SOLUONG - SLUONG_TRONGKHO;
                    }
                    grdList.CurrentRow.EndEdit();
                    TDutruThuocCollection lst =
                        new Select().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                            .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT)
                            .ExecuteAsCollection<TDutruThuocCollection>();
                    if (lst.Count > 0)
                    {
                        if (SOLUONG <= 0)
                            new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                            .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                        else
                            new Update(TDutruThuoc.Schema)
                                .Set(TDutruThuoc.Columns.SoluongDutru).EqualTo(SOLUONG)
                                .Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC).And(TDutruThuoc.Columns.IdKho).
                                IsEqualTo(IDKHO)
                                .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();

                    }
                    else
                    {
                        TDutruThuoc objThongTin = new TDutruThuoc();
                        objThongTin.IdThuoc = IDTHUOC;
                        objThongTin.KieuThuocVt = KIEU_THUOC_VT;
                        objThongTin.IdKho = IDKHO;
                        objThongTin.SoluongDutru = SOLUONG;
                        objThongTin.IsNew = true;
                        objThongTin.Save();

                    }
                    grdList.UpdateData();
                    grdList.Refetch();
                    m_Thuoc.AcceptChanges();
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lưu lại toàn bộ thông tin dự trù thuốc/vật tư trong kho?", "Thông báo", true)) 
                    return;
                foreach (GridEXRow _row in grdList.GetDataRows())
                {
                    int IDTHUOC = Utility.Int32Dbnull(grdList.GetValue(DmucThuoc.Columns.IdThuoc), 0);
                    Int16 IDKHO = Utility.Int16Dbnull(cboKho.SelectedValue);
                    int SOLUONG = Utility.Int32Dbnull(grdList.GetValue("SO_LUONG"), 0);

                    if (SOLUONG <= 0)
                        new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                        .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                        .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                    else
                        new Update(TDutruThuoc.Schema)
                            .Set(TDutruThuoc.Columns.SoluongDutru).EqualTo(SOLUONG)
                            .Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC).And(TDutruThuoc.Columns.IdKho).
                            IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                }
            }
            catch
            {
            }
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThongTinThuoc();
        }

        private void cmdHuydutru_all_Click(object sender, EventArgs e)
        {
            Huydutru();
        }
        void Huydutru()
        {
            try
            {
                int count_checked = grdList.GetCheckedRows().Count();
                 Int16 IDKHO= Utility.Int16Dbnull(cboKho.SelectedValue);
                 if (count_checked<=0  )
                {
                    if (Utility.isValidGrid(grdList))
                    {
                        GridEXRow _currentRow = grdList.CurrentRow;
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy dự trù thuốc/vật tư {0} trong kho {1} hay không?", Utility.sDbnull(_currentRow.Cells[DmucThuoc.Columns.TenThuoc].Value, "không xác định"), cboKho.Text), "Cảnh báo", true))
                        {
                           
                            int IDTHUOC = 0;


                            IDTHUOC = Utility.Int32Dbnull(_currentRow.Cells[DmucThuoc.Columns.IdThuoc].Value, 0);
                            new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                          .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                          .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                            _currentRow.BeginEdit();
                            _currentRow.Cells["COQUANHE"].Value = 0;
                            _currentRow.Cells["SO_LUONG"].Value = 0;
                            _currentRow.Cells["SLUONG_CANCHUYEN"].Value = 0;
                            _currentRow.EndEdit();

                            grdList.UpdateData();
                            m_Thuoc.AcceptChanges();
                            Utility.ShowMsg(string.Format("Đã hủy dự trù thuốc/vật tư {0} trong kho {1} thành công!", Utility.sDbnull(_currentRow.Cells[DmucThuoc.Columns.TenThuoc].Value, "không xác định"), cboKho.Text));
                        }
                    }
                    else
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một thuốc có dự trù để có thể thực hiện thao tác hủy dự trù thuốc!");
                    }
                }
                else//Hủy toàn bộ
                {
                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy toàn bộ dự trù thuốc/vật tư trong kho {0} hay không?", cboKho.Text), "Cảnh báo", true))
                    {
                          int IDTHUOC = 0;
                        foreach(GridEXRow _row in grdList.GetCheckedRows())
                        {
                            IDTHUOC = Utility.Int32Dbnull(_row.Cells[DmucThuoc.Columns.IdThuoc].Value, 0);
                              new Delete().From(TDutruThuoc.Schema).Where(TDutruThuoc.Columns.IdThuoc).IsEqualTo(IDTHUOC)
                            .And(TDutruThuoc.Columns.IdKho).IsEqualTo(IDKHO)
                            .And(TDutruThuoc.Columns.KieuThuocVt).IsEqualTo(KIEU_THUOC_VT).Execute();
                            _row.BeginEdit();
                            _row.Cells["COQUANHE"].Value= 0;
                            _row.Cells["SO_LUONG"].Value= 0;
                            _row.Cells["SLUONG_CANCHUYEN"].Value=0;
                            _row.EndEdit();
                        }
                        grdList.UpdateData();
                        m_Thuoc.AcceptChanges();
                        Utility.ShowMsg(string.Format("Đã hủy toàn bộ dự trù thuốc/vật tư trong kho {0} thành công!", cboKho.Text));
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy dự trù thuốc/vật tư. Liên hệ để được trợ giúp", ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
