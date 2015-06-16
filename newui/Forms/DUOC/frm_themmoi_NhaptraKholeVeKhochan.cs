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
using VNS.HIS.NGHIEPVU.THUOC;



namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_NhaptraKholeVeKhochan : Form
    {
        private DataTable m_dtKhoLinh, m_dtKhoTraLai = new DataTable();
        #region "khai báo biến "
        private DataTable m_dtDataPhieuChiTiet = new DataTable();
        public DataTable p_dtPhieuNhapTra = new DataTable();
        public action em_Action = action.Insert;
        public bool b_Cancel = false;
        public Janus.Windows.GridEX.GridEX grdList;
        string KIEU_THUOC_VT = "THUOC";
        #endregion
        #region "khai báo Form"
        public frm_themmoi_NhaptraKholeVeKhochan(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            dtNgayNhap.Value = globalVariables.SysDate;
            InitEvents();
           
            
        }
        void InitEvents()
        {
            cmdInPhieuNhap.Click += new EventHandler(cmdInPhieuNhap_Click);
            cboKhoTra.SelectedIndexChanged += new EventHandler(cboKhoTra_SelectedIndexChanged);
            cmdExit.Click += new EventHandler(cmdExit_Click);
            grdKhoXuat.KeyDown += new KeyEventHandler(grdKhoXuat_KeyDown);
            grdKhoXuat.UpdatingCell += new UpdatingCellEventHandler(grdKhoXuat_UpdatingCell);
            this.KeyDown += new KeyEventHandler(frm_themmoi_NhaptraKholeVeKhochan_KeyDown);
            txtthuoc._OnEnterMe+=new UCs.AutoCompleteTextbox_Thuoc.OnEnterMe(txtthuoc__OnEnterMe);
            txtthuoc._OnSelectionChanged+=new UCs.AutoCompleteTextbox_Thuoc.OnSelectionChanged(txtthuoc__OnSelectionChanged);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.cboKhoLinh.SelectedIndexChanged += new System.EventHandler(this.cboKhoLinh_SelectedIndexChanged);
            this.cmdPrevius.Click += new System.EventHandler(this.cmdPrevius_Click);
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            this.chkIsHetHan.CheckedChanged += new System.EventHandler(this.chkIsHetHan_CheckedChanged);
            cmdAddDetail.Click+=new EventHandler(cmdAddDetail_Click);
        }

       
        void txtthuoc__OnSelectionChanged()
        {
            try
            {
                int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
                if (_idthuoc > 0)
                {
                    var q = from p in grdKhoXuat.GetDataRows()
                            where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                            select p;
                    if (q.Count() > 0)
                    {
                        cmdAddDetail.Enabled = true;
                        grdKhoXuat.MoveTo(q.First());

                    }
                    else
                    {
                        cmdAddDetail.Enabled = false;
                    }
                    var q1 = from p in grdPhieuXuatChiTiet.GetDataRows()
                             where Utility.Int32Dbnull(p.Cells[DmucThuoc.Columns.IdThuoc].Value, 0) == _idthuoc
                             select p;
                    if (q1.Count() > 0)
                    {
                        grdPhieuXuatChiTiet.MoveTo(q1.First());
                    }

                }
                else
                {
                    cmdAddDetail.Enabled = false;
                    grdKhoXuat.MoveFirst();
                }
            }
            catch
            {
            }
        }

       

        void txtthuoc__OnEnterMe()
        {
            int _idthuoc = Utility.Int32Dbnull(txtthuoc.MyID, -1);
            if (_idthuoc > 0)
            {

            }
            else
            {

            }
        }

        void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidDetailData()) return;
                if (Utility.isValidGrid(grdKhoXuat))
                {
                    int soluong = Utility.Int32Dbnull(grdKhoXuat.CurrentRow.Cells["SO_LUONG"].Value, 0);
                    if (Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1) > soluong)
                    {
                        Utility.SetMsg(lblMsg, string.Format("Số lượng hủy {0} phải <= Số lượng có {1}", txtSoluongdutru.Text, soluong.ToString()), true);
                        txtSoluongdutru.SelectAll();
                        txtSoluongdutru.Focus();
                        return;
                    }
                    grdKhoXuat.CurrentRow.BeginEdit();
                    grdKhoXuat.CurrentRow.Cells["SO_LUONG_CHUYEN"].Value = Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1);
                    AddDetailNext(grdKhoXuat.CurrentRow);
                    grdKhoXuat.CurrentRow.EndEdit();
                    //-------------------
                    txtSoluongdutru.Clear();
                    txtthuoc.ClearText();
                    txtthuoc.Focus();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        bool isValidDetailData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (txtthuoc.MyID == txtthuoc.DefaultID)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn thuốc hủy", true);
                txtthuoc.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoluongdutru.Text, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập số lượng hủy", true);
                txtSoluongdutru.Focus();
                return false;
            }

            return true;
        }

        void frm_themmoi_NhaptraKholeVeKhochan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
            if (e.KeyCode == Keys.F2)
            {
                grdKhoXuat.Focus();
                grdKhoXuat.MoveFirst();
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
                grdKhoXuat.Col = gridExColumn.Position;
            }
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void grdKhoXuat_KeyDown(object sender, KeyEventArgs e)
        {
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdKhoXuat.RootTable.Columns["SO_LUONG_CHUYEN"];
            if (e.Control && e.KeyCode == Keys.Enter)// && Utility.Int32Dbnull(grdKhoXuat.GetValue(gridExColumn.Key), 0) > 0 && grdKhoXuat.CurrentColumn.Position == gridExColumn.Position)
            {
                grdKhoXuat.Focus();
                cmdNext_Click(cmdNext, new EventArgs());
            }

        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc xóa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaThongTin_Click(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
            {
                int ID = Utility.Int32Dbnull(gridExRow.Cells[TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieuChitiet].Value);
                TPhieutrathuocKholeVekhochanChitiet.Delete(ID);
                gridExRow.Delete();
                grdPhieuXuatChiTiet.UpdateData();
                m_dtDataPhieuChiTiet.AcceptChanges();
            }
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc trạng thái thông tin 
        /// của nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdPhieuXuatChiTiet.RowCount > 0;
                cmdInPhieuNhap.Enabled = grdPhieuXuatChiTiet.RowCount > 0;
               // cmdXoaThongTin.Enabled = grdPhieuXuatChiTiet.GetCheckedRows().Length > 0;
                cboKhoTra.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;
                cmdInPhieuNhap.Enabled = em_Action == action.Update;
              
                // cboKhoTra.Enabled = grdPhieuXuatChiTiet.RowCount <= 0;
            }
            catch (Exception exception)
            {

            }

            //TinhSumThanhTien();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                int IDPhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TPhieutrathuocKholeVekhochan.Columns.IdPhieu), -1);
                TPhieutrathuocKholeVekhochan objPhieuNhap = TPhieutrathuocKholeVekhochan.FetchByID(IDPhieuNhap);
                if (objPhieuNhap != null)
                {
                    VNS.HIS.UI.Baocao.thuoc_phieuin_nhapxuat.InphieuTraKholeVeKhochan(IDPhieuNhap, "PHIẾU NHẬP TRẢ", globalVariables.SysDate);
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
           
        }
        private void InitData()
        {
            DataBinding.BindDataCombobox(cboNhanVien, CommonLoadDuoc.LAYTHONGTIN_NHANVIEN(),
                                      DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Nhân viên---",false);
            cboNhanVien.Enabled = false;
            LoadKho();
        }
        private DataTable m_dtKhoaNoiTru=new DataTable();
        private void LoadKho()
        {
           try
           {
               if (KIEU_THUOC_VT == "THUOC")
               {
                   m_dtKhoLinh = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN();
                   m_dtKhoTraLai = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE();
               }
               else
               {
                   m_dtKhoLinh = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
                   m_dtKhoTraLai = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU", "NOITRU" });
               }
               cboNhanVien.SelectedValue = globalVariables.gv_intIDNhanvien;
               DataBinding.BindDataCombobox(cboKhoLinh, m_dtKhoLinh,
                                          TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Kho lĩnh---",false);
               DataBinding.BindDataCombobox(cboKhoTra, m_dtKhoTraLai,
                                          TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Kho trả---",false);
              
               
           }catch(Exception exception)
           {
               
           }
           
           
        }

        private void getData()
        {
            if (em_Action == action.Update)
            {
                TPhieutrathuocKholeVekhochan objPhieuNhap = TPhieutrathuocKholeVekhochan.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                if (objPhieuNhap != null)
                {

                    dtNgayNhap.Value = Convert.ToDateTime(objPhieuNhap.NgayTra);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);

                   // cboKhoLinh.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhonhan);
                    if (Utility.Int32Dbnull(objPhieuNhap.IdKhonhan) > 0)
                        cboKhoTra.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhotra);
                    if (Utility.Int32Dbnull(objPhieuNhap.IdKhotra) > 0)
                        cboKhoLinh.SelectedValue = Utility.sDbnull(objPhieuNhap.IdKhonhan);
                    if (Utility.Int32Dbnull(objPhieuNhap.IdNhanvien) > 0)
                        cboNhanVien.SelectedValue = Utility.sDbnull(objPhieuNhap.IdNhanvien);
                    txtLydotra._Text = objPhieuNhap.MotaThem;
                    m_dtDataPhieuChiTiet = SPs.ThuocLaychitietphieunhaptrakholevekhochan(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text)).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdPhieuXuatChiTiet, m_dtDataPhieuChiTiet, false, true, "1=1", "");
                }
            }
            if (em_Action == action.Insert)
            {
                m_dtDataPhieuChiTiet =
                       SPs.ThuocLaychitietphieunhaptrakholevekhochan(-100).GetDataSet()
                            .Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPhieuXuatChiTiet, m_dtDataPhieuChiTiet, false, true, "SO_LUONG>0", "");
            }
            UpdateWhenChanged();
        }
        private void SetStatusControl()
        {
           
        }
        private void frm_AddXuatKho_Load(object sender, EventArgs e)
        {
            InitData();
            AutocompleteThuoc();
            getData();
            ModifyCommand();
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLaythuoctrongkhoxuatAutocomplete(Utility.Int32Dbnull(cboKhoTra.SelectedValue, -1), KIEU_THUOC_VT).GetDataSet().Tables[0];// new Select().From(DmucThuoc.Schema).Where(DmucThuoc.KieuThuocvattuColumn).IsEqualTo(KIEU_THUOC_VT).And(DmucThuoc.TrangThaiColumn).IsEqualTo(1).ExecuteDataSet().Tables[0];
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
        //hàm thực hiện việc ẩn hiện thông tin 
        private void cboKhoTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            try
            {
                if (cboKhoTra.SelectedIndex > 0)
                {
                    _rowFilter = string.Format("{0}<>{1}", TDmucKho.Columns.IdKho,
                                               Utility.Int32Dbnull(cboKhoTra.SelectedValue));
                }
            }
            catch (Exception)
            {
                _rowFilter = "1=1";
            }
            m_dtKhoLinh.DefaultView.RowFilter = _rowFilter;
            m_dtKhoLinh.AcceptChanges();

            SetStatusControl();
            getThuocTrongKho();
            AutocompleteThuoc();

        }
        private DataTable m_dtDataThuocKho=new DataTable();
        private void getThuocTrongKho()
        {
            try
            {
                m_dtDataThuocKho =
                    SPs.ThuocLaythuoctrongkhoxuat(Utility.Int32Dbnull(cboKhoTra.SelectedValue),
                                                    chkIsHetHan.Checked ? 1 : 0,KIEU_THUOC_VT).GetDataSet().Tables[0];
                Utility.AddColumToDataTable(ref  m_dtDataThuocKho,"ShortName",typeof(string));
                foreach (DataRow drv in m_dtDataThuocKho.Rows)
                {
                    drv["ShortName"] = Utility.UnSignedCharacter(Utility.sDbnull(drv["Ten_Thuoc"]));
                }
                m_dtDataThuocKho.AcceptChanges();
                BindKhoThuoc(m_dtDataThuocKho);
                
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private void BindKhoThuoc(DataTable dataTable)
        {
            Utility.SetDataSourceForDataGridEx(grdKhoXuat, dataTable, false, true, "So_luong>0", "");
        }

        /// <summary>
        /// HÀM HỰC HIỆN VIỆC LƯU LẠI THÔNG TIN 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!InValiNhapKho()) return;
            PerformAction();
        }
        private void PerformAction()
        {
            try
            {
                switch (em_Action)
                {
                    case action.Insert:
                        ThemPhieuXuatKho();
                        ModifyCommand();
                        break;
                    case action.Update:
                        UpdatePhieuXuatKho();
                        ModifyCommand();
                        break;
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        #region "khai báo các đối tượng để thực hiện việc "
        private TPhieutrathuocKholeVekhochan CreatePhieuNhapKho()
        {
            TPhieutrathuocKholeVekhochan objPhieuNhapTra = new TPhieutrathuocKholeVekhochan();
            if (em_Action == action.Update)
            {
                objPhieuNhapTra.MarkOld();
                objPhieuNhapTra.IsLoaded = true;
                objPhieuNhapTra.IdPhieu = Utility.Int32Dbnull(txtIDPhieuNhapKho.Text, -1);
                objPhieuNhapTra.NguoiSua = globalVariables.UserName;
                objPhieuNhapTra.NgaySua = globalVariables.SysDate;
            }
            objPhieuNhapTra.IdKhoatra = (short)globalVariables.IdKhoaNhanvien;
            objPhieuNhapTra.IdKhonhan = Utility.Int16Dbnull(cboKhoLinh.SelectedValue, -1);
            objPhieuNhapTra.IdKhotra = Utility.Int16Dbnull(cboKhoTra.SelectedValue, -1);
            objPhieuNhapTra.MaNhacungcap = "-1";
            objPhieuNhapTra.TrangThai = 0;
            objPhieuNhapTra.MotaThem = txtLydotra.Text;
            objPhieuNhapTra.IdNhanvien = Utility.Int16Dbnull(cboNhanVien.SelectedValue, -1);
            if (Utility.Int32Dbnull(objPhieuNhapTra.IdNhanvien, -1) <= 0)
                objPhieuNhapTra.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objPhieuNhapTra.NgayTra = dtNgayNhap.Value;
            objPhieuNhapTra.NgayTao = globalVariables.SysDate;
            objPhieuNhapTra.NguoiTao = globalVariables.UserName;
            objPhieuNhapTra.LoaiPhieu = (byte?)LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan;
            objPhieuNhapTra.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
            objPhieuNhapTra.KieuThuocvattu = KIEU_THUOC_VT;
            return objPhieuNhapTra;
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin chi tiết
        /// </summary>
        /// <returns></returns>
        private TPhieutrathuocKholeVekhochanChitiet[] CreateArrPhieuChiTiet()
        {
            List<TPhieutrathuocKholeVekhochanChitiet> lstItems = new List<TPhieutrathuocKholeVekhochanChitiet>();
            foreach (DataRow dr in m_dtDataPhieuChiTiet.Rows)
            {
                TPhieutrathuocKholeVekhochanChitiet newItem = new TPhieutrathuocKholeVekhochanChitiet();
                newItem.IdThuoc =
                     Utility.Int32Dbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.IdThuoc]);
                newItem.NgayHethan =
                     Convert.ToDateTime(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.NgayHethan]).Date;
                newItem.GiaNhap = Utility.DecimaltoDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.GiaNhap], 0);
                newItem.GiaBan = Utility.DecimaltoDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.GiaBan], 0);
                newItem.SoLuong = Utility.Int32Dbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.SoLuong], 0);
                newItem.ThanhTien =
                     Utility.DecimaltoDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.GiaNhap]) *
                     Utility.Int32Dbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.SoLuong]);
                newItem.Vat = Utility.DecimaltoDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.Vat], 0);
                newItem.IdPhieu = Utility.Int32Dbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieu], -1);
                newItem.KieuThuocvattu = KIEU_THUOC_VT;
                newItem.NgayNhap = Convert.ToDateTime(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.NgayNhap]).Date;
                newItem.GiaBhyt = Utility.DecimaltoDbnull(dr[TPhieuNhapxuatthuocChitiet.Columns.GiaBhyt]);
                newItem.PhuthuDungtuyen = Utility.DecimaltoDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.PhuthuDungtuyen]);
                newItem.PhuthuTraituyen = Utility.DecimaltoDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.PhuthuTraituyen]);
                newItem.SoLo = Utility.sDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.SoLo], 0);
                newItem.IdThuockho = Utility.Int32Dbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.IdThuockho], 0);
                newItem.IdChuyen = Utility.Int32Dbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.IdChuyen], 0);
                newItem.MaNhacungcap = Utility.sDbnull(dr[TPhieutrathuocKholeVekhochanChitiet.Columns.MaNhacungcap], 0);
                lstItems.Add(newItem);
            }
            return lstItems.ToArray();
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        private void ThemPhieuXuatKho()
        {
            TPhieutrathuocKholeVekhochan objPhieuNhap = CreatePhieuNhapKho();

            ActionResult actionResult = new PhieuTraLai().ThemPhieuTraLaiKho
                (objPhieuNhap, CreateArrPhieuChiTiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    txtIDPhieuNhapKho.Text = Utility.sDbnull(objPhieuNhap.IdPhieu);
                    txtMaPhieu.Text = Utility.sDbnull(objPhieuNhap.MaPhieu);
                    TPhieutrathuocKholeVekhochan objDPhieuNhap = TPhieutrathuocKholeVekhochan.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow newDr = p_dtPhieuNhapTra.NewRow();
                    Utility.FromObjectToDatarow(objDPhieuNhap, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoLinh.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoTra.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khoxuat"] = Utility.sDbnull(objKho.TenKho);
                    DmucNhanvien objStaff = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cboNhanVien.SelectedValue));
                    if(objStaff!=null)
                    {
                        newDr["ten_nhanvien"] = Utility.sDbnull(objStaff.TenNhanvien);
                    }
                    p_dtPhieuNhapTra.Rows.Add(newDr);
                    grdList.UpdateData();
                    //Utility.ShowMsg("Bạn thêm mới phiếu thành công", "Thông báo");
                    Utility.GonewRowJanus(grdList, TPhieutrathuocKholeVekhochan.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    em_Action = action.Insert;
                    b_Cancel = true;
                    this.Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        private void UpdatePhieuXuatKho()
        {
            TPhieutrathuocKholeVekhochan objPhieuNhap = CreatePhieuNhapKho();

            ActionResult actionResult = new PhieuTraLai().UpdatePhieuTraLaiKho(objPhieuNhap, CreateArrPhieuChiTiet());
            switch (actionResult)
            {
                case ActionResult.Success:
                    TPhieutrathuocKholeVekhochan objDPhieuNhap = TPhieutrathuocKholeVekhochan.FetchByID(Utility.Int32Dbnull(txtIDPhieuNhapKho.Text));
                    DataRow[] arrDr =
                        p_dtPhieuNhapTra.Select(string.Format("{0}={1}", TPhieutrathuocKholeVekhochan.Columns.IdPhieu,
                                                                 Utility.Int32Dbnull(txtIDPhieuNhapKho.Text)));
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0].Delete();
                    }
                    DataRow newDr = p_dtPhieuNhapTra.NewRow();
                    Utility.FromObjectToDatarow(objDPhieuNhap, ref newDr);
                    TDmucKho objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoLinh.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khonhap"] = Utility.sDbnull(objKho.TenKho);
                    objKho = TDmucKho.FetchByID(Utility.Int32Dbnull(cboKhoTra.SelectedValue, -1));
                    if (objKho != null)
                        newDr["ten_khoxuat"] = Utility.sDbnull(objKho.TenKho);
                    p_dtPhieuNhapTra.Rows.Add(newDr);
                    Utility.GonewRowJanus(grdList, TPhieutrathuocKholeVekhochan.Columns.IdPhieu, Utility.sDbnull(txtIDPhieuNhapKho.Text));
                    Utility.ShowMsg("Bạn sửa  phiếu thành công", "Thông báo");
                    em_Action = action.Insert;
                    this.Close();
                    b_Cancel = true;
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc Invalinhap khoa thuốc
        /// </summary>
        /// <returns></returns>
        private bool InValiNhapKho()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (cboKhoTra.SelectedValue.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn kho trả thuốc",true);
                cboKhoTra.Focus();
                return false;
            }

            if (cboKhoLinh.SelectedValue.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn kho lĩnh thuốc", true);
                cboKhoLinh.Focus();
                return false;
            }
            if (cboKhoLinh.SelectedValue.ToString() == cboKhoTra.SelectedValue.ToString())
            {
                Utility.SetMsg(lblMsg, "Kho trả và kho lĩnh phải khác nhau", true);
                cboKhoLinh.Focus();
                return false;
            }
            return true;
        }

        private void cboKhoTra_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void chkIsHetHan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                getThuocTrongKho();
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            getThuocTrongKho();
        }
       

        private void grdKhoXuat_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if(e.Column.Key=="SO_LUONG_CHUYEN")
            {
                int soluongchuyen = Utility.Int32Dbnull(e.Value);
                int soluongchuyencu = Utility.Int32Dbnull(e.InitialValue);
                int soluongthat = Utility.Int32Dbnull(grdKhoXuat.GetValue("So_luong"));
                if(soluongchuyen<0)
                {
                    Utility.ShowMsg("Số lượng thuốc cần chuyển phải >=0","Thông báo",MessageBoxIcon.Warning);
                    e.Cancel = true;
                }else
                {
                    if(soluongchuyen>soluongthat)
                    {
                        Utility.ShowMsg("Số lượng thuốc cần chuyển phải <= số lượng thuốc có trong kho", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = soluongchuyencu;
                        e.Cancel = true;
                    }
                    else
                    {
                        grdKhoXuat.CurrentRow.IsChecked = soluongchuyen>0;
                    }
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đấy thông tin của 
        /// dược từ kho này sang kho kia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNext_Click(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
            {
                AddDetailNext(gridExRow);
            }
            UpdateWhenChanged();
            ResetValueInGridEx();
            ModifyCommand();
        }

        private void AddDetailNext(Janus.Windows.GridEX.GridEXRow gridExRow)
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                decimal GiaBhyt = 0m;
                DateTime NgayNhap = DateTime.Now;
                Int32 soluongchuyen = 0;
                decimal vat = 0m;
                int isHetHan = 0;
                long idthuockho = 0;
                int tongsoluongchuyen = 0;
                int soluongthat = 0;
                tongsoluongchuyen = 0;
                soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                soluongchuyen = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_CHUYEN"].Value, 0);
                if (soluongchuyen > 0)
                {
                    NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                    NgayNhap = Convert.ToDateTime(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap].Value).Date;
                    solo = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.SoLo].Value);
                    id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                    idthuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                    dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                    Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);
                    GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
                    vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                    isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                    manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);
                    DataRow[] arrDr = m_dtDataPhieuChiTiet.Select(TThuockho.Columns.IdThuockho + "=" + idthuockho.ToString());
                    if (arrDr.Length <= 0)
                    {
                        DataRow drv = m_dtDataPhieuChiTiet.NewRow();

                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.IdThuoc] = id_thuoc;
                        drv["ten_donvitinh"] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value);
                        drv["IsHetHan"] = isHetHan;
                        DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                        if (objLDrug != null)
                        {
                            drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                            drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                            drv[DmucThuoc.Columns.HoatChat] = Utility.sDbnull(objLDrug.HoatChat);
                            drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                            drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                        }
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuDungtuyen].Value, 0);
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.PhuthuTraituyen].Value, 0);

                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.Vat] = vat;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.NgayNhap] = NgayNhap;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.GiaNhap] = dongia;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.MaNhacungcap] = manhacungcap;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.SoLo] = solo;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho] = idthuockho;
                        drv[TPhieuNhapxuatthuocChitiet.Columns.IdChuyen] = idthuockho;//Cho biết chuyển từ id_thuockho nào sang
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.GiaBan] = Giaban;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.GiaBhyt] = GiaBhyt;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.SoLuong] = soluongchuyen;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.ThanhTien] = dongia * soluongchuyen;
                        drv[TPhieutrathuocKholeVekhochanChitiet.Columns.NgayHethan] = NgayHethan;
                        tongsoluongchuyen = soluongchuyen;
                        m_dtDataPhieuChiTiet.Rows.Add(drv);
                    }
                     else
                    {
                       
                        arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluongchuyen;
                        arrDr[0][TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * Utility.Int32Dbnull(arrDr[0]["SO_LUONG"],0);
                        tongsoluongchuyen = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]);
                        m_dtDataPhieuChiTiet.AcceptChanges();
                       
                    }
                    //Update lại dữ liệu từ kho xuất
                    gridExRow.BeginEdit();
                    gridExRow.Cells["SO_LUONG"].Value = soluongthat - tongsoluongchuyen;
                    gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
                    gridExRow.IsChecked = false;
                    gridExRow.EndEdit();
                }
                grdKhoXuat.UpdateData();
                m_dtDataThuocKho.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển thuốc:\n" + ex.Message);
            }
        }
        private void RemoveDetails()
        {
            try
            {

                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhieuXuatChiTiet.GetCheckedRows())
                {
                    RemoveDetails(gridExRow);
                }
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển thuốc:\n" + ex.Message);
            }
        }
        private void RemoveDetails(GridEXRow gridExRow)
        {
            try
            {
                string manhacungcap = "";
                string NgayHethan = "";
                string solo = "";
                int id_thuoc = -1;
                decimal dongia = 0m;
                decimal Giaban = 0m;
                Int32 soluong = 0;
                decimal vat = 0m;
                int isHetHan = 0;
                long idthuockho = 0;
                decimal GiaBhyt = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBhyt].Value, 0);
                DateTime  NgayNhap = Convert.ToDateTime(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.NgayNhap].Value).Date;
                NgayHethan = Utility.sDbnull(gridExRow.Cells["NGAY_HET_HAN"].Value);
                solo = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.SoLo].Value);
                id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuoc].Value, -1);
                idthuockho = Utility.Int32Dbnull(gridExRow.Cells[TThuockho.Columns.IdThuockho].Value, -1);
                dongia = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaNhap].Value, 0);
                Giaban = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.GiaBan].Value, 0);
                soluong = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG"].Value, 0);
                vat = Utility.DecimaltoDbnull(gridExRow.Cells[TThuockho.Columns.Vat].Value, 0);
                isHetHan = Utility.Int32Dbnull(gridExRow.Cells["IsHetHan"].Value, 0);
                manhacungcap = Utility.sDbnull(gridExRow.Cells[TThuockho.Columns.MaNhacungcap].Value, 0);
                DataRow[] arrDr = m_dtDataThuocKho.Select(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho + "=" + idthuockho.ToString());
                if (arrDr.Length <= 0)
                {
                    DataRow drv = m_dtDataThuocKho.NewRow();
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdThuoc] = id_thuoc;
                    drv["ten_donvitinh"] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value);
                    drv["IsHetHan"] = isHetHan;
                    DmucThuoc objLDrug = DmucThuoc.FetchByID(id_thuoc);
                    if (objLDrug != null)
                    {
                        drv[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(objLDrug.TenThuoc);
                        drv[DmucThuoc.Columns.HamLuong] = Utility.sDbnull(objLDrug.HamLuong);
                        drv[DmucThuoc.Columns.HoatChat] = Utility.sDbnull(objLDrug.HoatChat);
                        drv[DmucThuoc.Columns.NuocSanxuat] = Utility.sDbnull(objLDrug.NuocSanxuat);
                        drv[DmucThuoc.Columns.HangSanxuat] = Utility.sDbnull(objLDrug.HangSanxuat);
                    }
                    drv[TThuockho.Columns.GiaBhyt] = GiaBhyt;
                    drv[TThuockho.Columns.NgayNhap] = NgayNhap;
                    drv[TThuockho.Columns.Vat] = vat;
                    drv[TThuockho.Columns.GiaNhap] = dongia;
                    drv[TThuockho.Columns.MaNhacungcap] = manhacungcap;
                    drv[TThuockho.Columns.SoLo] = solo;
                    drv[TThuockho.Columns.IdThuockho] = idthuockho;
                    drv[TThuockho.Columns.GiaBan] = Giaban;

                    drv[TPhieuNhapxuatthuocChitiet.Columns.SoLuong] = soluong;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ThanhTien] = dongia * soluong;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.ChietKhau] = 0;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.NgayHethan] = NgayHethan;
                    drv[TPhieuNhapxuatthuocChitiet.Columns.IdPhieu] = -1;
                    m_dtDataThuocKho.Rows.Add(drv);
                }
                else
                {
                    arrDr[0]["SO_LUONG"] = Utility.Int32Dbnull(arrDr[0]["SO_LUONG"]) + soluong;
                    arrDr[0]["SO_LUONG_THAT"] = arrDr[0]["SO_LUONG"];
                    m_dtDataThuocKho.AcceptChanges();
                }
                gridExRow.Delete();
                grdPhieuXuatChiTiet.UpdateData();
                grdPhieuXuatChiTiet.Refresh();
                m_dtDataPhieuChiTiet.AcceptChanges();
                m_dtDataThuocKho.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy chuyển thuốc:\n" + ex.Message);
            }
        }
        private void ResetValueInGridEx()
        {
             foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetCheckedRows())
             {
                 gridExRow.BeginEdit();
                 gridExRow.Cells["SO_LUONG_CHUYEN"].Value = 0;
                 gridExRow.IsChecked = false;
                 gridExRow.BeginEdit();
             }
            grdKhoXuat.UpdateData();
            m_dtDataThuocKho.AcceptChanges();
        }
        private  void UpdateWhenChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoXuat.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    var query = from thuoc in grdPhieuXuatChiTiet.GetDataRows()
                                let sl = Utility.Int32Dbnull(thuoc.Cells["SO_LUONG"].Value)
                                let idthuockho = Utility.Int32Dbnull(thuoc.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                where idthuockho == Utility.Int32Dbnull(gridExRow.Cells[TPhieuNhapxuatthuocChitiet.Columns.IdThuockho].Value)
                                select sl;
                    if (query.Any())
                    {
                        int soluong = Utility.Int32Dbnull(query.FirstOrDefault());
                        int soluongthat = Utility.Int32Dbnull(gridExRow.Cells["SO_LUONG_THAT"].Value);
                        gridExRow.BeginEdit();
                        gridExRow.Cells["SO_LUONG"].Value = soluongthat - soluong;
                        gridExRow.EndEdit();
                        grdKhoXuat.UpdateData();
                        m_dtDataThuocKho.AcceptChanges();
                    }
                }
            }
        }

        private void cmdPrevius_Click(object sender, EventArgs e)
        {
            RemoveDetails();
        }

        

     

        private void cboKhoLinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radTuKhoLe_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
