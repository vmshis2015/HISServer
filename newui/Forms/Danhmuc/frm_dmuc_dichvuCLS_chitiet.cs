using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.UI.HinhAnh;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_dichvuCLS_chitiet : Form
    {
        #region "KHAI BAO THUOC TINH"
        private  DataTable dsTable=new DataTable();
        private DataTable dsTableDetail = new DataTable();
        private string rowFilter = "1=1";
        private DataTable m_dtDichvuCLS = new DataTable();
        NLog.Logger mylog = NLog.LogManager.GetLogger("frm_dmuc_dichvuCLS_chitiet");
    

        #endregion

        #region "HAM KHOI TAO "
        /// <summary>
        /// HAM KHOI TAO THONG TIN CHI TIET TIM KIEM
        /// </summary>
        public frm_dmuc_dichvuCLS_chitiet()
        {
            InitializeComponent();
            this.KeyPreview = true;
            printPreviewDialog1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            grdServiceDetail.ApplyingFilter+=new CancelEventHandler(grdServiceDetail_ApplyingFilter);
            grdServiceDetail.CellEdited+=new ColumnActionEventHandler(grdServiceDetail_CellEdited);
            grdServiceDetail.SelectionChanged+=new EventHandler(grdServiceDetail_SelectionChanged);
            grdChitiet.SelectionChanged += new EventHandler(grdChitiet_SelectionChanged);
            grdServiceDetail.FilterApplied+=new EventHandler(grdServiceDetail_FilterApplied);
           
            grdChitiet.GotFocus += grdChitiet_GotFocus;
            grdServiceDetail.GotFocus += grdServiceDetail_GotFocus;

            grdServiceDetail.DoubleClick += new EventHandler(grdServiceDetail_DoubleClick);
            grdChitiet.DoubleClick += new EventHandler(grdChitiet_DoubleClick);
            cmdConfig.Click += cmdConfig_Click;
            txtLoaiDichvu._OnEnterMe += txtLoaiDichvu__OnEnterMe;
            txtLoaiDichvu._OnSelectionChanged += txtLoaiDichvu__OnSelectionChanged;
            
        }

        void txtLoaiDichvu__OnSelectionChanged()
        {
            try
            {
                mylog.Trace("SPs.DmucLaydanhmucDichvuclsChitiet....");
                dsTable = SPs.DmucLaydanhmucDichvuclsChitiet(1,  Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0) ).GetDataSet().Tables[0];
                mylog.Trace("SetDataSourceForDataGridEx....");
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
               
            }
            catch
            {
            }  
        }

        void txtLoaiDichvu__OnEnterMe()
        {
            try
            {
                mylog.Trace("SPs.DmucLaydanhmucDichvuclsChitiet....");
                dsTable = SPs.DmucLaydanhmucDichvuclsChitiet(1,  Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0) ).GetDataSet().Tables[0];
                mylog.Trace("SetDataSourceForDataGridEx....");
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
            }
            catch
            {
            }  
        }

        void cmdConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdServiceDetail))
                {
                    Utility.ShowMsg("Bạn cần chọn một dịch vụ liên quan đến hình ảnh(XQuang,Siêu Âm, Nội soi...) để thực hiện cấu hình");
                    return;
                }
                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdServiceDetail, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);

                DmucDichvuclsChitiet objDichvuchitiet = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                try
                {
                    if (objDichvuchitiet == null)
                    {
                        Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                        return;
                    }
                    frm_DynamicSetup _DynamicSetup = new frm_DynamicSetup();
                    _DynamicSetup.objDichvuchitiet = objDichvuchitiet;
                    _DynamicSetup.ImageID = -1;
                    _DynamicSetup.Id_chidinhchitiet = -1;
                    if (_DynamicSetup.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
            }
        }
        void grdChitiet_DoubleClick(object sender, EventArgs e)
        {
            cmdEdit.PerformClick();
        }

        void grdServiceDetail_DoubleClick(object sender, EventArgs e)
        {
            cmdEdit.PerformClick();
        }

        void grdServiceDetail_GotFocus(object sender, EventArgs e)
        {
            _currentGRd = grdServiceDetail;
        }

        void grdChitiet_GotFocus(object sender, EventArgs e)
        {
            _currentGRd = grdChitiet;
        }

        void grdChitiet_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdChitiet)) return;
                v_ServiceDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                ModifyCommand();
            }
            catch (Exception)
            {


            }
        }

        void cboService_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        #endregion

        bool hanchequyendanhmuc = false;
        #region "HAM DUNG CHUNG"
        /// <summary>
        /// HAM THUC HIEN LOAD THONG TIN CUA DICH VU
        /// </summary>
        void InitData()
        {
            try
            {
                mylog.Trace("Load DmucDichvucl....");
                m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                DataTable m_dtDichvuCLS_new = m_dtDichvuCLS.Clone();
                if (globalVariables.gv_dtQuyenNhanvien_Dmuc.Select(QheNhanvienDanhmuc.Columns.Loai + "= 0").Length <= 0)
                    m_dtDichvuCLS_new = m_dtDichvuCLS.Copy();
                else
                {
                    foreach (DataRow dr in m_dtDichvuCLS.Rows)
                    {
                        if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucDichvucl.Columns.IdLoaidichvu]), "0"))
                        {
                            hanchequyendanhmuc = true;
                            m_dtDichvuCLS_new.ImportRow(dr);
                        }
                    }
                }
                
                txtLoaiDichvu.Init(m_dtDichvuCLS_new, new List<string>() { DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu });
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// HAM THUC HIEN TIM KIEM BAT SU KIEN TIM KIEM
        /// </summary>
        void Search()
        {
            try
            {
                mylog.Trace("SPs.DmucLaydanhmucDichvuclsChitiet....");
                dsTable = SPs.DmucLaydanhmucDichvuclsChitiet(1,hanchequyendanhmuc?  Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0):-1 ).GetDataSet().Tables[0];
                mylog.Trace("SetDataSourceForDataGridEx....");
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, dsTable, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
                _currentGRd = grdServiceDetail;
                ModifyCommand();
            }
            catch (Exception)
            {
                
               ModifyCommand();
            }
           
        }
        void SearchDetail(long id_cha)
        {
            try
            {
                DataRow[] arrDr = dsTable.Select("id_cha=" + id_cha.ToString());
                if (arrDr.Length > 0) dsTableDetail = arrDr.CopyToDataTable();
                else dsTableDetail = null;
                Utility.SetDataSourceForDataGridEx(grdChitiet, dsTableDetail, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                //grdServiceDetail.DropDowns[0].DataSource = globalVariables.g_dtMeasureUnit;
                ModifyCommand();
            }
            catch (Exception)
            {

                ModifyCommand();
            }

        }
        /// <summary>
        /// HAM THUC HIEN ENABLE HOAC DISABLE CUA NUT
        /// </summary>
        void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = Utility.isValidGrid(grdServiceDetail) || Utility.isValidGrid(grdChitiet);
                cmdDeleteALL.Enabled = Utility.isValidGrid(grdServiceDetail) || Utility.isValidGrid(grdChitiet);
                cmdDelete.Enabled = Utility.isValidGrid(grdServiceDetail) || Utility.isValidGrid(grdChitiet);
            }
            catch (Exception)
            {
                
               
            }
           
            //cmdSearchGrid.Enabled = grdServiceDetail.RowCount > 0;
        }
        #endregion

        #region "HAM KHAI BAO DUNG SU KIEN CUA FORM"
        /// <summary>
        /// HAM BAT SU KIEN TIM KIEM KHI NHAN NUT TIM KIEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
                ModifyCommand();
            }
            catch (Exception)
            {
                
               
            }
          
        }

        /// <summary>
        /// HAM THUC HIEN XOA THONG TIN DANG CHON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private int v_ServiceDetail_Id = -1;
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == grdServiceDetail.Name)
                {
                    if (!Utility.isValidGrid(grdServiceDetail))
                    {
                        Utility.ShowMsg("Bạn cần chọn một dịch vụ trên lưới trước khi xóa");
                        return;
                    }
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    KcbChidinhclsChitiet item = new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbChidinhclsChitiet>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Dịch vụ bạn chọn xóa đã từng được Bác sĩ dùng để chỉ định cho Bệnh nhân nên bạn không thể xóa");
                        return;
                    }
                    if (Utility.AcceptQuestion("Bạn có muốn xoá dịch vụ đang chọn không", "Thông báo", true))
                    {
                         SPs.DmucXoadanhmucDichvuclsChitiet(v_ServiceDetail_Id).Execute();
                        dsTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id)[0].Delete();
                        dsTable.AcceptChanges();

                    }

                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == grdChitiet.Name)
                {
                    if (!Utility.isValidGrid(grdChitiet))
                    {
                        Utility.ShowMsg("Bạn cần chọn một dịch vụ trên lưới trước khi xóa");
                        return;
                    }
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    KcbKetquaCl item = new Select().From(KcbKetquaCl.Schema).Where(KcbKetquaCl.Columns.IdDichvuchitiet).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbKetquaCl>();
                    if (item != null)
                    {
                        Utility.ShowMsg("Dịch vụ bạn chọn xóa đã từng được Bác sĩ dùng để chỉ định cho Bệnh nhân nên bạn không thể xóa");
                        return;
                    }

                    if (Utility.AcceptQuestion("Bạn có muốn xoá dịch vụ đang chọn không", "Thông báo", true))
                    {
                        SPs.DmucXoadanhmucDichvuclsChitiet(v_ServiceDetail_Id).Execute();
                        dsTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id)[0].Delete();
                        dsTable.AcceptChanges();

                    }
                }
            }
            catch (Exception)
            {
                
               ModifyCommand();
            }
           
        }
        GridEX _currentGRd=null;
        /// <summary>
        /// HAM THUC HIEN XOA NHIEU LUA CHON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeleteALL_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentGRd == null) return;
                SqlQuery q;
                bool b_FlagService = false;
                if (!Utility.isValidCheckedGrid(_currentGRd))
                {
                    Utility.ShowMsg("Bạn cần check chọn ít nhất một dịch vụ trên lưới trước khi xóa");
                    return;
                }
                if (_currentGRd.GetCheckedRows().Length <= 0)
                {
                    Utility.ShowMsg("Bạn phải chọn một dịch vụ thực hiện xoá", "Thông báo");
                    _currentGRd.Focus();
                    return;
                }
                string lsterr = "";
                if (_currentGRd.CurrentRow != null)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xoá các dịch vụ đang chọn không", "Thông báo", true))
                    {
                        foreach (Janus.Windows.GridEX.GridEXRow gridExRow in _currentGRd.GetCheckedRows())
                        {
                            int _IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                            v_ServiceDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                            KcbChidinhclsChitiet item = new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(v_ServiceDetail_Id).ExecuteSingle<KcbChidinhclsChitiet>();
                            if (item != null)
                            {
                                lsterr = lsterr + Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "") + ";";
                            }
                            else
                            {
                                SPs.DmucXoadanhmucDichvuclsChitiet(_IdChitietdichvu).Execute();
                                gridExRow.Delete();
                                _currentGRd.UpdateData();
                                _currentGRd.Refetch();
                                dsTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + _IdChitietdichvu)[0].Delete();
                                dsTable.AcceptChanges();
                            }
                        }
                        if (Utility.DoTrim(lsterr) != "")
                        {
                            Utility.ShowMsg("Một số dịch vụ chi tiết sau đã có chi tiết nên bạn không thể xóa\n" + lsterr);
                        }
                    }
                    dsTable.AcceptChanges();
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                ModifyCommand();
            }

        }
        /// <summary>
        /// HAM THUC HIEN THOAT FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// HAM THUC HIEN DUNG PHIM TAT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_dichvuCLS_chitiet_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)toolStripButton1.PerformClick();
            if (e.KeyCode == Keys.F5) Search();
            if(e.Control&&e.KeyCode==Keys.N)cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
        }
        /// <summary>
        /// HAM THUC HIEN TAO MOI CHI TIET DICH VU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            frm_themmoi_dichvucls_chitiet frm=new frm_themmoi_dichvucls_chitiet();
            frm.txtID.Text = "-1";
            frm.m_enAction = action.Insert;
            frm.grdlistChitiet = grdChitiet;
            frm.grdlist = grdServiceDetail;
            frm.dtDataServiceDetail = dsTable;
            frm.Service_ID = Utility.Int32Dbnull(txtLoaiDichvu.MyID, -1);
            frm.ShowDialog();
            grdServiceDetail_SelectionChanged(grdServiceDetail, e);
            ModifyCommand();
        }
        /// <summary>
        /// HAM THUC HIEN SUA THONG TIN CHI TIET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (_currentGRd != null && _currentGRd.Name == grdServiceDetail.Name)
            {
                if (Utility.isValidGrid(grdServiceDetail))
                {
                    frm_themmoi_dichvucls_chitiet frm = new frm_themmoi_dichvucls_chitiet();
                    frm.txtID.Text = grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu).ToString();
                    frm.m_enAction = action.Update;
                    frm.grdlist = grdServiceDetail;
                    frm.grdlistChitiet = grdChitiet;
                    frm.dtDataServiceDetail = dsTable;
                    if (grdServiceDetail.CurrentRow != null)
                        frm.drServiceDetail = Utility.FetchOnebyCondition(dsTable, DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id);
                    frm.ShowDialog();
                    grdServiceDetail_SelectionChanged(grdServiceDetail, e);
                }
            }
            else 
            {
                if (Utility.isValidGrid( grdChitiet))
                {
                    frm_themmoi_dichvucls_chitiet frm = new frm_themmoi_dichvucls_chitiet();
                    frm.txtID.Text = grdChitiet.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu).ToString();
                    frm.m_enAction = action.Update;
                    frm.grdlist = grdChitiet;
                    frm.grdlistChitiet = grdChitiet;
                    frm.dtDataServiceDetail = dsTable;
                    if (grdChitiet.CurrentRow != null)
                        frm.drServiceDetail = Utility.FetchOnebyCondition(dsTable, DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_Id);
                    frm.ShowDialog();
                    grdServiceDetail_SelectionChanged(grdServiceDetail, e);
                }
            }
            ModifyCommand();
        }
        /// <summary>
        /// HAM THUC HIEN LOAD THONG TIN KHI LOAD FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_dichvuCLS_chitiet_Load(object sender, EventArgs e)
        {
            mylog.Trace("Load danh muc dung chung");
            DataTable dt = new Select().From(DmucChung.Schema).ExecuteDataSet().Tables[0];
            dt = new Select(DmucChung.Columns.Ten).From(DmucChung.Schema).ExecuteDataSet().Tables[0];
            InitData();
            Search();
            ModifyCommand();
        }
       
       

        #endregion

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có muốn lưu toàn bộ thông tin này không", "Thông báo", true))
                {
                    foreach (DataRowView drv in dsTable.DefaultView)
                    {
                        new Update(DmucDichvuclsChitiet.Schema)
                            .Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(
                                Utility.Int32Dbnull(drv[DmucDichvuclsChitiet.Columns.SttHthi], 1))
                            .Set(DmucDichvuclsChitiet.Columns.MaChitietdichvu).EqualTo(
                                Utility.sDbnull(drv[DmucDichvuclsChitiet.Columns.MaChitietdichvu], ""))
                            .Set(DmucDichvuclsChitiet.Columns.TenChitietdichvu).EqualTo(
                                Utility.sDbnull(drv[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""))
                            .Set(DmucDichvuclsChitiet.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(drv[DmucDichvuclsChitiet.Columns.DonGia], 0))
                            .Set(DmucDichvuclsChitiet.Columns.MaDonvitinh).EqualTo(
                                Utility.DecimaltoDbnull(drv[DmucDichvuclsChitiet.Columns.MaDonvitinh], 0))
                            .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(
                                Utility.Int32Dbnull(drv[DmucDichvuclsChitiet.Columns.IdChitietdichvu], -1)).Execute();

                    }
                    Utility.ShowMsg("Bạn thực hiện thành công", "Thông báo");
                }
            }
            catch (Exception)
            {
                
                ModifyCommand();
            }
          

        }
       

        private void grdServiceDetail_FilterApplied(object sender, EventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện di chuyển trn lươới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdServiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdServiceDetail.CurrentRow != null)
                {
                    v_ServiceDetail_Id = Utility.Int32Dbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    bool co_chitiet = Utility.sDbnull(grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.CoChitiet].Value, "0")=="1";
                    grdChitiet.Height = co_chitiet ? 200 : 0;
                    if (co_chitiet)
                        SearchDetail(v_ServiceDetail_Id);
                    ModifyCommand();
                }
            }
            catch (Exception)
            {
                
               
            }
           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void grdServiceDetail_CellEdited(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                string Code = "";
                Code = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.MaChitietdichvu].Value.ToString();
                string Id = "-1";
                Id = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value.ToString();
                new Update(DmucDichvuclsChitiet.Schema)
                    .Set(DmucDichvuclsChitiet.Columns.MaChitietdichvu).EqualTo(Code)
                    .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(
                        Utility.Int32Dbnull(Id)).Execute();
                ModifyCommand();
            }catch( Exception exception)
            {
                ModifyCommand();
            }
          
        }

        private void grdServiceDetail_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiên viecj cập nhập thôn tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdServiceDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            int ServiceDetail_ID = Utility.Int32Dbnull(grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu));
           
            if (e.Column.Key == DmucDichvuclsChitiet.Columns.SttHthi)
            {
                new Update(DmucDichvuclsChitiet.Schema)
                    .Set(DmucDichvuclsChitiet.Columns.SttHthi).EqualTo(
                        Utility.Int32Dbnull(grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.SttHthi)))
                    .Set(DmucDichvuclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(DmucDichvuclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(
                        Utility.Int32Dbnull(grdServiceDetail.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu))).Execute();

            }
            Utility.GotoNewRowJanus(grdServiceDetail, DmucDichvuclsChitiet.Columns.IdChitietdichvu, Utility.sDbnull(ServiceDetail_ID));
        }
       
       
    }
}
