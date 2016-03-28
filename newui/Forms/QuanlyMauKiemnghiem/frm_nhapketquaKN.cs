using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.UI.QMS;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.BENH_AN;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class frm_nhapketquaKN : Form
    {
        public delegate void OnResult(Int64 id_chitiet, byte tthai_cls);
        public event OnResult _OnResult;

        string ma_luotkham = "";
        Int64 id_benhnhan = -1;
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        KcbChidinhcl objChidinh = null;
        DataTable dtChidinh = null; 
        public frm_nhapketquaKN()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_nhapketquaKN_Load;
            this.KeyDown += frm_nhapketquaKN_KeyDown;
            cmdSearch.Click += cmdSearch_Click;
            txtMahoamau.KeyDown += txtMahoamau_KeyDown;
            grdKetqua.UpdatingCell += grdKetqua_UpdatingCell;
            mnuCancelResult.Click += mnuCancelResult_Click;
            cmdConfirm.Click += cmdConfirm_Click;
        }
        public void AutoSearch(string mahoamau)
        {
            txtMahoamau.Text = mahoamau;
            SearchData();
        }
        void cmdConfirm_Click(object sender, EventArgs e)
        {
            Confirm(); 
        }
        void Confirm()
        {
            byte _result = 3;
            try
            {
                foreach (GridEXRow row in grdKetqua.GetDataRows())
                {
                    List<KcbKetquaCl> lstResult = new List<KcbKetquaCl>();
                    List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
                    int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbKetquaCl.Columns.IdKq), -1);
                    int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    int IdChitietchidinhcha = Utility.Int32Dbnull(dtChidinh.Rows[0][KcbChidinhclsChitiet.Columns.IdChitietchidinh], -1);


                    int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                    DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                    int CoChitiet = Utility.Int32Dbnull(objcls.CoChitiet, 0);
                    KcbKetquaCl _item = null;
                    KcbChidinhclsChitiet _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                    KcbChidinhclsChitiet _itemchitietcha = null;
                    if (CoChitiet == 1)
                    {

                        _itemchitietcha = KcbChidinhclsChitiet.FetchByID(IdChitietchidinhcha);
                        if (_itemchitietcha != null)
                        {
                            _itemchitietcha.IsNew = false;
                            _itemchitietcha.MarkOld();
                        }
                    }
                    _itemchitiet.IsNew = false;
                    _itemchitiet.MarkOld();
                    if (id_kq > 0)
                    {
                        _item = KcbKetquaCl.FetchByID(id_kq);
                        _item.IsNew = false;
                        _item.NguoiSua = globalVariables.UserName;
                        _item.NgaySua = globalVariables.SysDate;
                        _item.IpMaysua = globalVariables.gv_strIPAddress;
                        _item.TenMaysua = globalVariables.gv_strComputerName;
                        _item.MarkOld();
                    }
                    else
                    {
                        _item = new KcbKetquaCl();
                        _item.IsNew = true;
                        _item.NguoiTao = globalVariables.UserName;
                        _item.NgayTao = globalVariables.SysDate;
                        _item.IpMaytao = globalVariables.gv_strIPAddress;
                        _item.TenMaytao = globalVariables.gv_strComputerName;
                    }

                    if (objcls != null)
                    {
                        _item.MaChidinh = objChidinh.MaChidinh;
                        _item.MaBenhpham = objChidinh.MaChidinh;
                        _item.Barcode = objChidinh.Barcode;
                        _item.IdBenhnhan = id_benhnhan;
                        _item.MaLuotkham = ma_luotkham;
                        _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                        _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                        _item.SttIn = objcls.SttHthi;
                        _item.BtNam = objcls.BinhthuongNam;
                        _item.BtNu = objcls.BinhthuongNu;
                        _item.KetQua = Utility.DoTrim(Utility.GetValueFromGridColumn(row, "Ket_qua"));
                        if (_item.TrangThai < 3)
                            _item.TrangThai = 3;
                        if (chkSaveAndConfirm.Checked)
                            _item.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ

                        if (Utility.DoTrim(_item.KetQua) == "")
                            _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                        //_item.TenDonvitinh = objcls.TenDonvitinh;
                        _itemchitiet.KetQua = _item.KetQua;
                        if (_itemchitiet.TrangThai < 3)
                            _itemchitiet.TrangThai = 3;
                        if (chkSaveAndConfirm.Checked)
                            _itemchitiet.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                        if (Utility.DoTrim(_itemchitiet.KetQua) == "")
                            _itemchitiet.TrangThai = 2;//Quay ve trang thai chuyen can

                        if (_itemchitietcha != null && _itemchitietcha.TrangThai < 3)
                            _itemchitietcha.TrangThai = 3;
                        if (_itemchitietcha != null && chkSaveAndConfirm.Checked)
                            _itemchitietcha.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                        if (_itemchitietcha != null && _itemchitiet.KetQua == "")
                            _itemchitietcha.TrangThai = 2;//Quay ve trang thai chuyen can

                        _item.TenThongso = "";
                        _item.TenKq = "";
                        _item.LoaiKq = 0;
                        _item.ChophepHienthi = 1;
                        _item.ChophepIn = 1;
                        
                        _item.MotaThem = objcls.MotaThem;
                        lstResult.Add(_item);
                        lstDetails.Add(_itemchitiet);
                        if (_itemchitietcha != null)
                            lstDetails.Add(_itemchitietcha);
                        if (clsXN.UpdateResult(lstResult, lstDetails) != ActionResult.Success)
                        {

                        }
                        else
                        {
                            if (_OnResult != null) _OnResult(_itemchitiet.IdChitietchidinh, Utility.ByteDbnull(_itemchitiet.TrangThai, 2));
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        void mnuCancelResult_Click(object sender, EventArgs e)
        {
            if (grdKetqua.SelectedItems.Count > 1)
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy kết quả các xét nghiệm đang chọn", "Hủy kết quả", true))
                    return;
            List< KcbKetquaCl> lstResult =new List<KcbKetquaCl>();
            List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
            foreach (GridEXRow row in grdKetqua.SelectedItems)
            {
                KcbKetquaCl _item = null;
                KcbChidinhclsChitiet _itemchitiet = null;
                try
                {
                    int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbKetquaCl.Columns.IdKq), -1);
                    int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                    _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                    _itemchitiet.IsNew = false;
                    _itemchitiet.MarkOld();
                    if (id_kq > 0)
                    {
                        _item = KcbKetquaCl.FetchByID(id_kq);
                        _item.IsNew = false;
                        _item.NguoiSua = globalVariables.UserName;
                        _item.NgaySua = globalVariables.SysDate;
                        _item.IpMaysua = globalVariables.gv_strIPAddress;
                        _item.TenMaysua = globalVariables.gv_strComputerName;
                        _item.MarkOld();
                    }
                    else
                    {
                        _item = new KcbKetquaCl();
                        _item.IsNew = true;
                        _item.NguoiTao = globalVariables.UserName;
                        _item.NgayTao = globalVariables.SysDate;
                        _item.IpMaytao = globalVariables.gv_strIPAddress;
                        _item.TenMaytao = globalVariables.gv_strComputerName;
                    }
                    DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                    if (objcls != null)
                    {
                        _item.MaChidinh = objChidinh.MaChidinh;
                        _item.MaBenhpham = objChidinh.MaChidinh;
                        _item.Barcode = objChidinh.Barcode;
                        _item.IdBenhnhan = id_benhnhan;
                        _item.MaLuotkham = ma_luotkham;
                        _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                        _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                        _item.SttIn = objcls.SttHthi;
                        _item.BtNam = objcls.BinhthuongNam;
                        _item.BtNu = objcls.BinhthuongNu;
                        _item.KetQua = Utility.sDbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.KetQua), -1);
                        if (_item.TrangThai < 3)
                            _item.TrangThai = 3;
                        if (chkSaveAndConfirm.Checked)
                            _item.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ

                        if (Utility.DoTrim(_item.KetQua) == "")
                            _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                        //_item.TenDonvitinh = objcls.TenDonvitinh;
                        _itemchitiet.KetQua = Utility.sDbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.KetQua), -1);
                        if (_itemchitiet.TrangThai < 3)
                            _itemchitiet.TrangThai = 3;
                        if (chkSaveAndConfirm.Checked)
                            _itemchitiet.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                        if (Utility.DoTrim(_itemchitiet.KetQua) == "")
                            _itemchitiet.TrangThai = 2;//Quay ve trang thai chuyen can
                        _item.TenThongso = "";
                        _item.TenKq = "";
                        _item.LoaiKq = 0;
                        _item.ChophepHienthi = 1;
                        _item.ChophepIn = 1;
                        _item.MotaThem = objcls.MotaThem;
                        
                    }
                    lstResult.Add(_item);
                    lstDetails.Add(_itemchitiet);
                }
                catch (Exception)
                {


                }
            }
            if (clsXN.UpdateResult(lstResult, lstDetails) == ActionResult.Success)
            {
                Utility.ShowMsg("Đã hủy kết quả các xét nghiệm đang chọn thành công");
                if (_OnResult != null) _OnResult(-1,2);
            }
            else
            {
                Utility.ShowMsg("Lỗi khi thực hiện hủy kết quả xét nghiệm");
            }
        }
        
        //KcbChidinhclsChitiet.Trang_thai:0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
        void grdKetqua_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                List<KcbKetquaCl> lstResult = new List<KcbKetquaCl>();
                List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
                int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua,KcbKetquaCl.Columns.IdKq) ,-1);
                int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                int IdChitietchidinhcha = Utility.Int32Dbnull(dtChidinh.Rows[0][KcbChidinhclsChitiet.Columns.IdChitietchidinh], -1);
                

                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                int CoChitiet =Utility.Int32Dbnull( objcls.CoChitiet,0);
                KcbKetquaCl _item = null;
                KcbChidinhclsChitiet _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                KcbChidinhclsChitiet _itemchitietcha = null;
                if (CoChitiet==1)
                {
                    
                    _itemchitietcha = KcbChidinhclsChitiet.FetchByID(IdChitietchidinhcha);
                    if (_itemchitietcha != null)
                    {
                        _itemchitietcha.IsNew = false;
                        _itemchitietcha.MarkOld();
                    }
                }
                _itemchitiet.IsNew = false;
                _itemchitiet.MarkOld();
                if (id_kq >0)
                {
                    _item = KcbKetquaCl.FetchByID(id_kq);
                    _item.IsNew = false;
                    _item.NguoiSua = globalVariables.UserName;
                    _item.NgaySua = globalVariables.SysDate;
                    _item.IpMaysua = globalVariables.gv_strIPAddress;
                    _item.TenMaysua = globalVariables.gv_strComputerName;
                    _item.MarkOld();
                }
                else
                {
                    _item = new KcbKetquaCl();
                    _item.IsNew = true;
                    _item.NguoiTao = globalVariables.UserName;
                    _item.NgayTao = globalVariables.SysDate;
                    _item.IpMaytao = globalVariables.gv_strIPAddress;
                    _item.TenMaytao = globalVariables.gv_strComputerName;
                }
                
                if (objcls != null)
                {
                    _item.MaChidinh =objChidinh.MaChidinh;
                    _item.MaBenhpham = objChidinh.MaChidinh;
                    _item.Barcode = objChidinh.Barcode;
                    _item.IdBenhnhan = id_benhnhan;
                    _item.MaLuotkham = ma_luotkham;
                    _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                    _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                    _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                    _item.SttIn = objcls.SttHthi;
                    _item.BtNam = objcls.BinhthuongNam;
                    _item.BtNu = objcls.BinhthuongNu;
                    _item.KetQua = Utility.sDbnull(e.Value, "");
                    if (_item.TrangThai < 3)
                        _item.TrangThai = 3;
                    if (chkSaveAndConfirm.Checked)
                        _item.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ

                    if (Utility.DoTrim(_item.KetQua) == "")
                        _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                    //_item.TenDonvitinh = objcls.TenDonvitinh;
                    _itemchitiet.KetQua = Utility.sDbnull(e.Value, "");
                    if (_itemchitiet.TrangThai < 3)
                        _itemchitiet.TrangThai = 3;
                    if (chkSaveAndConfirm.Checked)
                        _itemchitiet.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                    if (Utility.DoTrim(_itemchitiet.KetQua) == "")
                        _itemchitiet.TrangThai = 2;//Quay ve trang thai chuyen can

                    if (_itemchitietcha!=null && _itemchitietcha.TrangThai < 3)
                        _itemchitietcha.TrangThai = 3;
                    if (_itemchitietcha != null && chkSaveAndConfirm.Checked)
                        _itemchitietcha.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                    if (_itemchitietcha != null && Utility.DoTrim(Utility.sDbnull(e.Value, "")) == "")
                        _itemchitietcha.TrangThai = 1;//Quay ve trang thai chuyen can

                    _item.TenThongso = "";
                    _item.TenKq = "";
                    _item.LoaiKq = 0;
                    _item.ChophepHienthi = 1;
                    _item.ChophepIn = 1;
                    _item.MotaThem = objcls.MotaThem;
                    lstResult.Add(_item);
                    lstDetails.Add(_itemchitiet);
                    if(_itemchitietcha!=null)
                        lstDetails.Add(_itemchitietcha);
                    if (clsXN.UpdateResult(lstResult, lstDetails) != ActionResult.Success)
                    {
                        e.Cancel = true;
                    }
                    else
                        if (_OnResult != null) _OnResult(_itemchitiet.IdChitietchidinh, Utility.ByteDbnull(_itemchitiet.TrangThai, 2));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                
            }
        }

       
        void HienthiNhapketqua(int id_dichvu,int co_chitiet)
        {
            try
            {
                DataTable dt = SPs.ClsTimkiemthongsoXNNhapketqua(ma_luotkham, MaChidinh, MaBenhpham, id_chidinh, co_chitiet, id_dichvu, IdChitietdichvu).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", DmucDichvuclsChitiet.Columns.SttHthi );

                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {
                
                
            }
        }
        void txtMahoamau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        

        void frm_nhapketquaKN_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F3)
            {
                txtMahoamau.SelectAll();
                txtMahoamau.Focus();
                return;
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                cmdConfirm_Click(cmdConfirm, new EventArgs());
                return;
            }
            if (e.KeyCode == Keys.F2 && grdKetqua.GetDataRows().Length > 0)
            {
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                SearchData();
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
        }

        void frm_nhapketquaKN_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMahoamau.Text))
                txtMahoamau.Focus();
            else
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            Application.DoEvents();
            chkSaveAndConfirm.Enabled = cmdConfirm.Enabled = grdKetqua.GetDataRows().Count() > 0;
        }

        void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        void FillPatientData(DataRow dr)
        {
            if (dr == null)
            {
                
                txtPatient_Name.Clear();
                txtDiaChi.Clear();

               
            }
            else
            {
               
                txtPatient_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
                
            }
        }
      
        void SearchData()
        {
            try
            {
                dtChidinh = SPs.SpMaukiemnghiemTimkiemchitieu(txtMahoamau.Text).GetDataSet().Tables[0];
                if (dtChidinh != null && dtChidinh.Rows.Count > 0)
                {
                    id_dichvu = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.IdDichvu], 0);
                    IdChitietdichvu = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.IdChitietdichvu], 0);
                    co_chitiet = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.CoChitiet], 0);
                    id_chidinh = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.IdChidinh], 0);
                    ma_luotkham = Utility.sDbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.MaLuotkham], "");
                    MaChidinh = Utility.sDbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.MaChidinh], "");
                    MaBenhpham = Utility.sDbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.MaBenhpham], "");
                    objChidinh = KcbChidinhcl.FetchByID(id_chidinh);
                    HienthiNhapketqua(id_dichvu, co_chitiet);
                }
                else
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu chỉ định kiểm nghiệm để nhập kết quả. Liên hệ IT để được trợ giúp");
                }
            }
            catch (Exception)
            {

            }
            finally
            {
             
            }
        }

        private void grdKetqua_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }
    }
}
