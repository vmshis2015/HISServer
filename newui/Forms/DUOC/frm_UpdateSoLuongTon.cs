using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

using VNS.Properties;
using Janus.Windows.GridEX;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_UpdateSoLuongTon : Form
    {
        private DataTable m_dtKhothuoc=new DataTable();
        private DataTable m_dataFull = new DataTable();
        private DataTable m_dtkho = new DataTable();
        private HisDuocProperties HisDuocProperties;
        bool hasLoaded = false;
        string kieu_thuocvattu = "THUOC";
        public frm_UpdateSoLuongTon()
        {
            InitializeComponent();
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            grdDieuchinh.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(grdDieuchinh_UpdatingCell);
            grdDieuchinh.SelectionChanged += new EventHandler(grdDieuchinh_SelectionChanged);
            grdList.UpdatingCell += grdList_UpdatingCell;

            grdKho.UpdatingCell += grdKho_UpdatingCell;
            cmdUp.Click += new EventHandler(cmdUp_Click);
            cmdDown.Click += new EventHandler(cmdDown_Click);
            lnkNgayhethan.Click += new EventHandler(lnkNgayhethan_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);
            optFIFO.CheckedChanged += new EventHandler(_CheckedChanged);
            optLIFO.CheckedChanged += new EventHandler(_CheckedChanged);
            optUutien.CheckedChanged += new EventHandler(_CheckedChanged);
            optExpireDate.CheckedChanged += new EventHandler(_CheckedChanged);
            Cauhinh();
        }

        void grdKho_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == TThuockho.Columns.ChophepKedon)
                {
                      int idKho = Utility.Int32Dbnull(grdKho.CurrentRow.Cells[TThuockho.Columns.IdKho].Value);
                    int IdThuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TThuockho.Columns.IdThuoc].Value);
                    SPs.ThuocCapnhattrangthaikedon(IdThuoc, idKho, (byte)e.Value).Execute();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        void grdList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == TThuockho.Columns.ChophepKetutruc)
                {
                    int idKho = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TThuockho.Columns.IdKho].Value);
                    int IdThuoc = Utility.Int32Dbnull(grdList.CurrentRow.Cells[TThuockho.Columns.IdThuoc].Value);
                    
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.ChophepKetutruc).EqualTo(e.Value)
                            .Where(TThuockho.Columns.IdThuoc).IsEqualTo(IdThuoc)
                            .And(TThuockho.Columns.IdKho).IsEqualTo(idKho)
                            .Execute();
                   
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            try
            {
                string _value="STT";
                if (optFIFO.Checked)
                    _value = "FIFO";
                if (optLIFO.Checked)
                    _value = "LIFO";
                if (optExpireDate.Checked)
                    _value = "EXP";
                if (optUutien.Checked)
                    _value = "STT";
                THU_VIEN_CHUNG.Capnhatgiatrithamsohethong("THUOC_KIEUXUATTHUOC", _value);
                ChangeOutType();
            }
            catch
            {
            }
            
        }


        void SetOutType()
        {
            try
            {
                string _value = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_KIEUXUATTHUOC", "STT", true);
                switch (_value)
                {
                    case "FIFO":
                        optFIFO.Checked = true;
                        break;
                    case "LIFO":
                        optLIFO.Checked = true;
                        break;
                    case "EXP":
                        optExpireDate.Checked = true;
                        break;
                    case "STT":
                        optUutien.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }
        void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._ThuocProperties);
            if (_Properties.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Cauhinh();
        }
        void Cauhinh()
        {
            if (PropertyLib._ThuocProperties != null)
            {
                grdDieuchinh.RootTable.Columns[TThuockho.Columns.SoLuong].EditType = PropertyLib._ThuocProperties.Chophepsuasoluong ? EditType.TextBox : EditType.NoEdit;
                grdDieuchinh.RootTable.Columns[TThuockho.Columns.GiaBan].EditType = PropertyLib._ThuocProperties.Chophepsuagiaban ? EditType.TextBox : EditType.NoEdit;
            }
        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                foreach (DataRow drv in m_dataFull.GetChanges().Rows)
                {
                    int IdThuockho = Utility.Int32Dbnull(drv[TThuockho.Columns.IdThuockho]);
                    int SttBan = Utility.Int32Dbnull(drv[TThuockho.Columns.SttBan]);
                    int SoLuong = Utility.Int32Dbnull(drv[TThuockho.Columns.SoLuong]);
                    decimal GiaBan = Utility.DecimaltoDbnull(drv[TThuockho.Columns.GiaBan]);
                    new Update(TThuockho.Schema)
                       .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                       .Set(TThuockho.Columns.SoLuong).EqualTo(SoLuong)
                       .Set(TThuockho.Columns.GiaBan).EqualTo(GiaBan)
                       .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                       .Execute();
                }
            }
            catch
            {
            }
            
        }

        void grdDieuchinh_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdDieuchinh))
            {
                cmdUp.Enabled = false;
                cmdDown.Enabled = false;
                return;
            }
            bool canDown = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position < grdDieuchinh.RowCount - 1;
            bool canUp = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position > 0;
            cmdUp.Enabled = canUp;
            cmdDown.Enabled = canDown;
        }

        void lnkNgayhethan_Click(object sender, EventArgs e)
        {
            
        }

        void cmdDown_Click(object sender, EventArgs e)
        {
            try
            {
                int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                grdDieuchinh.MoveNext();
                int SttBan = grdDieuchinh.CurrentRow.Position + 1;
                if (chkAutoupdate.Checked)
                {
                    new Update(TThuockho.Schema)
                       .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                       .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                       .Execute();
                }
                updateData(IdThuockho.ToString(), SttBan);
                bool canDown = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position < grdDieuchinh.RowCount - 1;
                bool canUp = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position > 0;
                cmdUp.Enabled = canUp;
                cmdDown.Enabled = canDown;
            }
            catch (Exception ex)
            {
            }
        }
        void updateData(string idthuockho, int SttBan)
        {
            try
            {
                DataRow[] arrDR = m_dataFull.Select(TThuockho.Columns.IdThuockho + "=" + idthuockho);
                if (arrDR.Length > 0)
                    arrDR[0][TThuockho.Columns.SttBan] = SttBan;
                m_dataFull.AcceptChanges();
            }
            catch
            {
            }
        }
        void cmdUp_Click(object sender, EventArgs e)
        {
            try
            {
                int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                grdDieuchinh.MovePrevious();
                int SttBan = grdDieuchinh.CurrentRow.Position+1;
                if (chkAutoupdate.Checked)
                {
                    new Update(TThuockho.Schema)
                        .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                        .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                        .Execute();
                }
                updateData(IdThuockho.ToString(), SttBan);
                bool canDown = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position < grdDieuchinh.RowCount - 1;
                bool canUp = grdDieuchinh.RowCount > 0 && grdDieuchinh.CurrentRow.Position > 0;
                cmdUp.Enabled = canUp;
                cmdDown.Enabled = canDown;
            }
            catch (Exception ex)
            {
            }
        }

        void grdDieuchinh_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == TThuockho.Columns.SttBan)
                {
                    int SttBan =Utility.Int32Dbnull( e.Value);
                    int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    if (chkAutoupdate.Checked)
                    {
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.SttBan).EqualTo(SttBan)
                            .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                            .Execute();
                    }
                    m_dataFull.AcceptChanges();
                    grdDieuchinh.Refetch();
                    Utility.GotoNewRowJanus(grdDieuchinh, TThuockho.Columns.IdThuockho, IdThuockho.ToString());
                }
                if (e.Column.Key == TThuockho.Columns.SoLuong)
                {
                    if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn sửa số lượng tồn kho? Bạn chỉ nên sửa khi không sử dụng các chức năng nhập xuất tồn.\nChú ý, nếu đồng ý sửa thì các báo cáo liên quan đến số liệu nhập kho sẽ không chính xác", "Cảnh báo", true))
                    {
                        e.Cancel = true;
                        return;
                    }
                    int SoLuong = Utility.Int32Dbnull(e.Value);
                    int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    if (chkAutoupdate.Checked)
                    {
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.SoLuong).EqualTo(SoLuong)
                            .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                            .Execute();
                    }
                    m_dataFull.AcceptChanges();
                    grdDieuchinh.Refetch();
                    Utility.GotoNewRowJanus(grdDieuchinh, TThuockho.Columns.IdThuockho, IdThuockho.ToString());
                }
                if (e.Column.Key == TThuockho.Columns.GiaBan)
                {
                    decimal GiaBan = Utility.DecimaltoDbnull(e.Value);
                    int IdThuockho = Utility.Int32Dbnull(grdDieuchinh.CurrentRow.Cells[TThuockho.Columns.IdThuockho].Value);
                    if (chkAutoupdate.Checked)
                    {
                        new Update(TThuockho.Schema)
                            .Set(TThuockho.Columns.GiaBan).EqualTo(GiaBan)
                            .Where(TThuockho.Columns.IdThuockho).IsEqualTo(IdThuockho)
                            .Execute();
                    }
                    m_dataFull.AcceptChanges();
                    grdDieuchinh.Refetch();
                    Utility.GotoNewRowJanus(grdDieuchinh, TThuockho.Columns.IdThuockho, IdThuockho.ToString());
                }
            }
            catch(Exception ex)
            {
            }
        }

        void grdList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    pnlDieuchinh.Height = 0;
                    m_dtkho.Rows.Clear();
                }
                else
                {
                    pnlDieuchinh.Height = 300;
                    object idthuoc = Utility.getValueOfGridCell(grdList, TThuockho.Columns.IdThuoc);
                    if (idthuoc != null)
                    {
                        m_dtkho = SPs.ThuocKhochuathuoc(Utility.Int32Dbnull(idthuoc, 0), kieu_thuocvattu).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx(grdKho, m_dtkho, true, true, "1=1", TDmucKho.Columns.TenKho);
                        m_dataFull.DefaultView.RowFilter = TThuockho.Columns.IdThuoc + "=" + idthuoc.ToString();
                    }
                    else
                    {
                        m_dtkho.Rows.Clear();
                        m_dataFull.DefaultView.RowFilter = "1=2";
                    }
                }


            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện viecj thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CauHinh()
        {
            try
            {
                grdList.RootTable.Columns[TThuockho.Columns.SoLuong].Selectable = HisDuocProperties.ChoPhepSuaSLuongTon || globalVariables.IsAdmin;
                if(PropertyLib._HisDuocProperties.KieuThuocVattu=="VT")
                {
                    grdList.RootTable.Columns[TThuockho.Columns.NgayHethan].Selectable = true;
                    grdList.RootTable.Columns[TThuockho.Columns.GiaNhap].Selectable = true;
                    grdList.RootTable.Columns[TThuockho.Columns.GiaBan].Selectable = true;
                }
               
                cmdSave.Visible = HisDuocProperties.ChoPhepSuaSLuongTon || globalVariables.IsAdmin;
                timer1.Enabled = HisDuocProperties.Tudonglaysolieu > 0;
                timer1.Interval = Convert.ToInt32(HisDuocProperties.Tudonglaysolieu * 1000);
                timer1.Start();
            }
            catch
            {
            }
        }
        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadThuocTrongKho();
                pnlDieuchinh.Height = grdList.GetDataRows().Length > 0 ? 300 : 0;
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
        /// <summary>
        /// hàm thực hiện việc load lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_UpdateSoLuongTon_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombobox(cboKho, CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA(), TDmucKho.Columns.IdKho,
                                 TDmucKho.Columns.TenKho,"--Chọn kho thuốc--",true);
            SetOutType();
            LoadThuocTrongKho();
            CauHinh();
            hasLoaded = true;
        }

        private void LoadThuocTrongKho()
        {
            TDmucKho _kho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKho.SelectedValue, -1));
            if (_kho != null)
            {
                kieu_thuocvattu = _kho.KhoThuocVt;
                if (kieu_thuocvattu != "THUOC" || kieu_thuocvattu != "VT")
                    kieu_thuocvattu = "ALL";
                DataSet ds = SPs.ThuocLaythongtinthuoctontrongkhoTheokho(Utility.Int32Dbnull(cboKho.SelectedValue, -1)).GetDataSet();
                m_dtKhothuoc = ds.Tables[0];
                m_dataFull = ds.Tables[1];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtKhothuoc, true, true, "1=1", "TEN_THUOC");
                string Orderby = TThuockho.Columns.SttBan;
                Utility.SetDataSourceForDataGridEx(grdDieuchinh, m_dataFull, true, true, "1=2", getOrderOut());

                if (grdList.GetDataRows().Length > 0)
                {
                    grdList.MoveFirst();
                    grdList_CurrentCellChanged(grdList, new EventArgs());
                }
            }
            else
            {
                m_dtKhothuoc.Rows.Clear();
                m_dataFull.Rows.Clear();
                m_dtkho.Rows.Clear();
            }
        }
        string getOrderOut()
        {
            string Orderby = TThuockho.Columns.SttBan + " ," + TThuockho.Columns.NgayHethan;
            if (optFIFO.Checked)
                Orderby = TThuockho.Columns.NgayNhap + " ," + TThuockho.Columns.NgayHethan;
            if (optLIFO.Checked)
                Orderby = TThuockho.Columns.NgayNhap + " desc," + TThuockho.Columns.NgayHethan;
            if (optExpireDate.Checked)
                Orderby = TThuockho.Columns.NgayHethan + " ," + TThuockho.Columns.NgayNhap;
            if (optUutien.Checked)
                Orderby = TThuockho.Columns.SttBan + " ," + TThuockho.Columns.NgayHethan;
            return Orderby;
        }
        void ChangeOutType()
        {

            m_dataFull.DefaultView.Sort = getOrderOut();
        }
        private void grdList_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            
        }

       

        private void frm_UpdateSoLuongTon_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
           
            if(e.KeyCode==Keys.S&&e.Control)cmdSave.PerformClick();
        }

      

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (HisDuocProperties.Tudonglaysolieu > 0 && !chkTamdung.Checked)
                    LoadThuocTrongKho();
            }
            catch
            {
            }
        }

       

        
       
    }
}
