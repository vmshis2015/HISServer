﻿using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using SubSonic;

using SortOrder = Janus.Windows.GridEX.SortOrder;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_nhanvien : Form
    {
        #region "Public Variables(Class Level)
        DataTable m_dtDepartmentList = new DataTable();
        DataTable m_dtDepartmentListUp = new DataTable();
        public bool m_blnCancel = true;
        public DataTable p_dtStaffList=new DataTable();
        public action em_Action = action.Insert;
        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về đối tượng khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public DmucNhanvien m_objObjectReturn = null;

       

        #endregion


        public frm_themmoi_nhanvien()
        {
            InitializeComponent();
            
            InitEvents();
            txtName.LostFocus+=new EventHandler(txtName_LostFocus);
          //  cboStatus.SelectedIndex = 0;
        }
        void InitEvents()
        {
            grdKhoa.SelectionChanged += new System.EventHandler(grdKhoa_SelectionChanged);
        }

        void grdKhoa_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdKhoa))
                {
                    m_dtPhongkham.DefaultView.RowFilter = "1=1";
                    return;
                }
                m_dtPhongkham.DefaultView.RowFilter = "1=1";
                m_dtPhongkham.DefaultView.RowFilter = "ma_cha=" + Utility.sDbnull(grdKhoa.GetValue(DmucKhoaphong.Columns.MaCha), "-1");
            }
            catch (Exception ex)
            {


            }
        }
        private void uiGroupBox1_Click(object sender, EventArgs e)
        {
        }
        private void txtName_LostFocus(object sender, System.EventArgs e)
        {
            txtName.Text = Utility.chuanhoachuoi(txtName.Text);
        }
        private void txtSpecMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_dtPhongkham=new DataTable();
        private DataTable m_dtKhoangoaitru = new DataTable();
        private DataTable m_dtKhoanoitru = new DataTable();

        private void InitialData()
        {
            try
            {
                //Khởi tạo danh mục loại nhân viên
                DataTable v_dtStaffTypeList = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAINHANVIEN",true);
                //  v_dtStaffTypeList =

                cboStaffType.DataSource = v_dtStaffTypeList.DefaultView;
                v_dtStaffTypeList.DefaultView.Sort = DmucChung.Columns.SttHthi;
                cboStaffType.ValueMember = DmucChung.Columns.Ma;
                cboStaffType.DisplayMember =DmucChung.Columns.Ten;
                //Khởi tạo danh mục chức vụ
                
                //Khởi tạo danh mục phòng ban
                m_dtDepartmentList = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", -1);
                DataBinding.BindData(cboUpLevel, m_dtDepartmentList, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
                cboUpLevel_SelectedIndexChanged(cboUpLevel,new EventArgs());
                //Khởi tạo danh mục User
                DataTable v_dtUserList = new SysUserCollection().Load().ToDataTable();
                Utility.AddColumnAlltoUserDataTable(ref v_dtUserList, SysUser.Columns.PkSuid, "");
                v_dtUserList.DefaultView.Sort = SysUser.Columns.PkSuid+" ASC";
                txtUID.Init(v_dtUserList, new List<string>() { SysUser.Columns.PkSuid, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid });
                m_dtKhoThuoc = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA();
                Utility.SetDataSourceForDataGridEx(grdKhoThuoc, m_dtKhoThuoc, false, true, "1=1", TDmucKho.Columns.SttHthi);
                m_dtPhongkham = THU_VIEN_CHUNG.LaydanhmucPhong(0);
                Utility.SetDataSourceForDataGridEx(grdPhongkham, m_dtPhongkham, false, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

                m_dtKhoangoaitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI",0);
                Utility.SetDataSourceForDataGridEx(grdKhoa, m_dtKhoangoaitru, false, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

                m_dtKhoanoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
                Utility.SetDataSourceForDataGridEx(grdKhoanoitru, m_dtKhoanoitru, false, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private DataTable m_dtKhoThuoc=new DataTable();
       
        /// <summary>
        /// hàm thực hiện load thông tin của Form khi load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_nhanvien_Load(object sender, EventArgs e)
        {
            InitialData();
            if (em_Action == action.Update) GetData();
            SetStatusAlter();
        }

        private void frm_themmoi_nhanvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }

        #region "Method of common"
        /// <summary>
        /// hàm thực hiện validate trạng thái cần nhập
        /// </summary>
        private void SetStatusAlter()
        {
            Utility.SetMsg(lblMsg, "", true);
            if(string.IsNullOrEmpty(txtStaffCode.Text.Trim()))
            {
                Utility.SetMsg(lblMsg,"Bạn nhập phải nhập mã nhân viên",true);
                txtStaffCode.Focus();


            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn nhập tên nhân viên",true);
                txtName.Focus();


            }
            if (cboStaffType.SelectedIndex<=-1)
            {
                Utility.SetMsg(lblMsg, "Bạn nhập loại nhân viên",true);
                cboStaffType.Focus();


            }
           
        }
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!isValidData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!isValidData()) return;
                    PerformActionUpdate();
                    break;
            }

        }
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtStaffCode.Text))
            {
                Utility.SetMsg(lblMsg,  "Bạn phải nhập mã nhân viên",true);
                txtStaffCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên nhân viên",true);
                txtName.Focus();
                return false;

            }
           
            SqlQuery q = new Select().From(DmucNhanvien.Schema)
                .Where(DmucNhanvien.Columns.MaNhanvien).IsEqualTo(txtStaffCode.Text);
            if (em_Action == action.Update)
                q.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
            if(q.GetRecordCount()>0)
            {
                Utility.SetMsg(lblMsg, "Tồn tại mã nhân viên",true);
                txtStaffCode.Focus();
                return false;
            }
            
            if (txtUID.MyID!="-1")
            {
                SqlQuery q2 = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.UserName).IsEqualTo(txtUID.MyID);
                if (em_Action == action.Update)
                    q2.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
                if (q2.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Tên đăng nhập đã gán cho một nhân viên khác",true);
                    txtUID.Focus();
                    return false;
                }
            }
            return true;
        }

        private Query _Query = DmucNhanvien.CreateQuery();
        /// <summary>
        /// hamdf thực hiện thêm thông tin 
        /// </summary>
        private void PerformActionInsert()
        {
           
              
             DmucNhanvien objDmucNhanvien = CreateStaffNhanVien();
            objDmucNhanvien.IsNew = true;
            objDmucNhanvien.Save();
            QuanheNhanVienKho(objDmucNhanvien);
            QuanheBsi_khoaphong(objDmucNhanvien);
                DataRow dr = p_dtStaffList.NewRow();
                dr[DmucNhanvien.Columns.NguoiTao] =globalVariables.UserName;
                dr[DmucNhanvien.Columns.NgayTao] = globalVariables.SysDate;
                dr[DmucNhanvien.Columns.IdNhanvien] = Utility.Int32Dbnull(_Query.GetMax(DmucNhanvien.Columns.IdNhanvien), -1);
                dr[DmucNhanvien.Columns.MaNhanvien] = txtStaffCode.Text;
                dr[DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked?1:0;
                dr[DmucNhanvien.Columns.TenNhanvien] = Utility.sDbnull(txtName.Text, "");
                
                dr[DmucNhanvien.Columns.MotaThem] = Utility.DoTrim(txtmotathem.Text);
                dr[DmucNhanvien.Columns.IdKhoa] = Utility.Int16Dbnull(cboUpLevel.SelectedValue, 1);
                dr[DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                dr["ten_khoa"] = Utility.sDbnull(cboUpLevel.Text, "");
                dr[DmucNhanvien.Columns.IdPhong] = Utility.Int16Dbnull(cboDepart.SelectedValue, 1);
                dr["ten_phong"] = Utility.sDbnull(cboDepart.Text, "");
                dr["ten_loainhanvien"] = Utility.sDbnull(cboStaffType.Text, "");
                dr[DmucNhanvien.Columns.UserName] = Utility.sDbnull(txtUID.MyCode, "");
                dr[DmucNhanvien.Columns.MaLoainhanvien] = Utility.sDbnull(cboStaffType.SelectedValue, "");
                dr[DmucNhanvien.Columns.QuyenHuythanhtoanTatca] = chkCancelPaymentAll.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                dr[DmucNhanvien.Columns.QuyenMokhoaTatca] = chkUnlockAll.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                dr[DmucNhanvien.Columns.QuyenSuangayThanhtoan] = chkChangePaymentdate.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                dr[DmucNhanvien.Columns.QuyenTralaiTien] = chkRepay.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                dr[DmucNhanvien.Columns.QuyenKhamtatcacackhoaNoitru] = Utility.Bool2byte(chkAllNoitru.Checked);
                dr[DmucNhanvien.Columns.QuyenSuatieudebaocao] = Utility.Bool2byte(chkSuatieudebaocao.Checked);
                dr[DmucNhanvien.Columns.QuyenKhamtatcacacphongNgoaitru] = Utility.Bool2byte(chkAllNgoaitru.Checked);
                dr[DmucNhanvien.Columns.QuyenThemdanhmucdungchung] = Utility.Bool2byte(chkThemdanhmucchung.Checked);
                dr[DmucNhanvien.Columns.QuyenSuadonthuoc] = Utility.Bool2byte(chkQuyenSuadonthuoc.Checked);
                dr[DmucNhanvien.Columns.QuyenSuaphieuchidinhcls] = Utility.Bool2byte(chkQuyensuaCLS.Checked);
                p_dtStaffList.Rows.InsertAt(dr, 0);
                this.Close();
         
            
           
        }

        private void QuanheNhanVienKho(DmucNhanvien objDmucNhanvien)
        {
            new Delete().From(QheNhanvienKho.Schema)
                .Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetCheckedRows())
            {
                QheNhanvienKho objDNhanvienKho = new QheNhanvienKho();
                objDNhanvienKho.IdKho = Utility.Int16Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value);
                objDNhanvienKho.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objDNhanvienKho.IsNew = true;
                objDNhanvienKho.Save();
            }
        }
        private void QuanheBsi_khoaphong(DmucNhanvien objDmucNhanvien)
        {
            new Delete().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(objDmucNhanvien.IdNhanvien)
                .Execute();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoanoitru.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 1;
                objQheBacsiKhoaphong.IdPhong = -1;
                objQheBacsiKhoaphong.IsNew = true;
                objQheBacsiKhoaphong.Save();
            }
           
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.MaCha].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 0;
                objQheBacsiKhoaphong.IdPhong = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IsNew = true;
                objQheBacsiKhoaphong.Save();
            }
        }

     

        private DmucNhanvien CreateStaffNhanVien()
        {
            DmucNhanvien objDmucNhanvien=new DmucNhanvien();
            if(em_Action==action.Update)
            {
                objDmucNhanvien.MarkOld();
                objDmucNhanvien.IsLoaded = true;
                objDmucNhanvien.IdNhanvien = Utility.Int16Dbnull(txtID.Text, -1);
            }
            objDmucNhanvien.MaNhanvien = Utility.sDbnull(txtStaffCode.Text);
            objDmucNhanvien.TenNhanvien = Utility.sDbnull(txtName.Text);
            objDmucNhanvien.IdPhong = Utility.Int16Dbnull(cboDepart.SelectedValue, -1);
            objDmucNhanvien.IdKhoa = Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1);
            objDmucNhanvien.MaLoainhanvien = Utility.sDbnull(cboStaffType.SelectedValue, "-1");
            objDmucNhanvien.UserName = Utility.sDbnull(txtUID.MyCode, "");
            objDmucNhanvien.TrangThai = Convert.ToByte(chkHienThi.Checked?1:0);
          
            objDmucNhanvien.MotaThem = Utility.DoTrim(txtmotathem.Text);
            objDmucNhanvien.NgayTao = globalVariables.SysDate;
            objDmucNhanvien.NguoiTao = globalVariables.UserName;
            objDmucNhanvien.QuyenHuythanhtoanTatca = chkCancelPaymentAll.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
            objDmucNhanvien.QuyenMokhoaTatca = chkUnlockAll.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
            objDmucNhanvien.QuyenSuangayThanhtoan = chkChangePaymentdate.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
            objDmucNhanvien.QuyenTralaiTien = chkRepay.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
            objDmucNhanvien.QuyenKhamtatcacackhoaNoitru = Utility.Bool2byte(chkAllNoitru.Checked);
            objDmucNhanvien.QuyenKhamtatcacacphongNgoaitru = Utility.Bool2byte(chkAllNgoaitru.Checked);
            objDmucNhanvien.QuyenThemdanhmucdungchung = Utility.Bool2byte(chkThemdanhmucchung.Checked);
            objDmucNhanvien.QuyenXemphieudieutricuabacsinoitrukhac = Utility.Bool2byte(chkXemphieudieutricuaBacsikhac.Checked);
            objDmucNhanvien.QuyenSuatieudebaocao = Utility.Bool2byte(chkSuatieudebaocao.Checked);
            objDmucNhanvien.QuyenSuaphieuchidinhcls = Utility.Bool2byte(chkQuyensuaCLS.Checked);
            objDmucNhanvien.QuyenSuadonthuoc = Utility.Bool2byte(chkQuyenSuadonthuoc.Checked);
           
            
            return objDmucNhanvien;
        }

        private void PerformActionUpdate()
        {

            try
            {

                DmucNhanvien objDmucNhanvien = CreateStaffNhanVien();
                objDmucNhanvien.Save();
                QuanheNhanVienKho(objDmucNhanvien);
                QuanheBsi_khoaphong(objDmucNhanvien);
               

                DataRow[] dr = p_dtStaffList.Select(DmucNhanvien.Columns.IdNhanvien+ "=" + Utility.Int32Dbnull(txtID.Text, -1));
                if (dr.GetLength(0) > 0)
                {
                    dr[0][DmucNhanvien.Columns.UserName] = txtUID.MyCode;
                    dr[0][DmucNhanvien.Columns.MaLoainhanvien] = Utility.sDbnull(cboStaffType.SelectedValue, -1);
                    dr[0]["ten_loainhanvien"] = Utility.sDbnull(cboStaffType.Text, "");
                    dr[0]["ten_phong"] = Utility.sDbnull(cboDepart.Text, -1);
                    dr[0][DmucNhanvien.Columns.IdPhong] = Utility.Int32Dbnull(cboDepart.SelectedValue, -1);
                    dr[0]["ten_khoa"] = Utility.sDbnull(cboUpLevel.Text, -1);
                    dr[0][DmucNhanvien.Columns.IdKhoa] = Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1);
                    dr[0][DmucNhanvien.Columns.MaNhanvien] = txtStaffCode.Text;
                    dr[0][DmucNhanvien.Columns.TenNhanvien] = Utility.sDbnull(txtName.Text, "");
                    dr[0][DmucNhanvien.Columns.MotaThem] = Utility.DoTrim(txtmotathem.Text);
                    dr[0][DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                    dr[0][DmucNhanvien.Columns.NguoiSua] = globalVariables.UserName;
                    dr[0][DmucNhanvien.Columns.QuyenHuythanhtoanTatca] = chkCancelPaymentAll.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                    dr[0][DmucNhanvien.Columns.QuyenMokhoaTatca] = chkUnlockAll.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                    dr[0][DmucNhanvien.Columns.QuyenSuangayThanhtoan] = chkChangePaymentdate.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                    dr[0][DmucNhanvien.Columns.QuyenTralaiTien] = chkRepay.Checked ? Utility.ByteDbnull(1) : Utility.ByteDbnull(0);
                    dr[0][DmucNhanvien.Columns.QuyenSuatieudebaocao] = Utility.Bool2byte(chkSuatieudebaocao.Checked);
                    dr[0][DmucNhanvien.Columns.QuyenKhamtatcacackhoaNoitru] = Utility.Bool2byte(chkAllNoitru.Checked);
                    dr[0][DmucNhanvien.Columns.QuyenKhamtatcacacphongNgoaitru] = Utility.Bool2byte(chkAllNgoaitru.Checked);
                        dr[0][DmucNhanvien.Columns.QuyenThemdanhmucdungchung] = Utility.Bool2byte(chkThemdanhmucchung.Checked);
                        dr[0][DmucNhanvien.Columns.QuyenSuadonthuoc] = Utility.Bool2byte(chkQuyenSuadonthuoc.Checked);
                        dr[0][DmucNhanvien.Columns.QuyenSuaphieuchidinhcls] = Utility.Bool2byte(chkQuyensuaCLS.Checked);


                }
                p_dtStaffList.AcceptChanges();
                this.Close();

            }
            catch
            {
            }
        }
        void AutocheckPhongkham(string danhmucphongkham)
        {
            try
            {
                if (!string.IsNullOrEmpty(danhmucphongkham))
                {
                    string[] rows = danhmucphongkham.Split(',');
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetDataRows())
                    {
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {

                                if (Utility.sDbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value, "-1") == Utility.sDbnull(row))
                                {
                                    gridExRow.IsChecked = true;
                                    break;
                                }
                                else
                                {
                                    gridExRow.IsChecked = false;
                                }
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        private void GetData()
        {
            DmucNhanvien objStaffList = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objStaffList != null)
            {

               

                txtName.Text = Utility.sDbnull(objStaffList.TenNhanvien);
                txtStaffCode.Text = Utility.sDbnull(objStaffList.MaNhanvien);
               
                cboStaffType.SelectedIndex = Utility.GetSelectedIndex(cboStaffType,
                                                                       objStaffList.MaLoainhanvien.ToString());
                txtUID.SetCode(objStaffList.UserName);

              
                cboUpLevel.SelectedIndex = Utility.GetSelectedIndex(cboUpLevel,
                                                                       objStaffList.IdKhoa.ToString());
                cboDepart.SelectedIndex = Utility.GetSelectedIndex(cboDepart,
                                                                    objStaffList.IdPhong.ToString());
                chkHienThi.Checked =Utility.Int32Dbnull(objStaffList.TrangThai,0)==1;
                chkCancelPaymentAll.Checked = Utility.Int32Dbnull(objStaffList.QuyenHuythanhtoanTatca) == 1 ? true : false;
                chkChangePaymentdate.Checked = objStaffList.QuyenSuangayThanhtoan == 1 ? true : false;
                chkUnlockAll.Checked = Utility.Int32Dbnull(objStaffList.QuyenMokhoaTatca) == 1 ? true : false;
                chkRepay.Checked = Utility.Int32Dbnull(objStaffList.QuyenTralaiTien) == 1 ? true : false;

                chkAllNgoaitru.Checked = Utility.Byte2Bool(objStaffList.QuyenKhamtatcacackhoaNoitru);
                chkXemphieudieutricuaBacsikhac.Checked = Utility.Byte2Bool(objStaffList.QuyenXemphieudieutricuabacsinoitrukhac);
                chkAllNoitru.Checked = Utility.Byte2Bool(objStaffList.QuyenKhamtatcacacphongNgoaitru);
                chkThemdanhmucchung.Checked = Utility.Byte2Bool(objStaffList.QuyenThemdanhmucdungchung);
                chkSuatieudebaocao.Checked = Utility.Byte2Bool(objStaffList.QuyenSuatieudebaocao);
                chkQuyenSuadonthuoc.Checked = Utility.Byte2Bool(objStaffList.QuyenSuadonthuoc);
                chkQuyensuaCLS.Checked = Utility.Byte2Bool(objStaffList.QuyenSuaphieuchidinhcls);
               
                LoadQuanHeNhanVienKho();
                LoadQheBS_khoanoitru();
                LoadQheBS_khoangoaitru();
            }
        }
        private void LoadQheBS_khoanoitru()
        {
            QheBacsiKhoaphongCollection lstQheBacsiKhoaphong = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(1)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoanoitru.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphong.AsEnumerable()
                            where kho.IdKhoa == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheBS_khoangoaitru()
        {
            QheBacsiKhoaphongCollection lstQheBacsiKhoaphongCollection = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(0)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoa.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                            where kho.IdKhoa == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                            where kho.IdPhong == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienKho()
        {
            QheNhanvienKhoCollection objNhanvienKhoCollection = new Select().From(QheNhanvienKho.Schema)
                .Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienKhoCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in objNhanvienKhoCollection.AsEnumerable()
                            where kho.IdKho == Utility.Int32Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.Cells["IsChon"].Value = 1;
                    gridExRow.IsChecked = true;
                }
                  
                else
                {
                    gridExRow.Cells["IsChon"].Value = 0;
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();
                   
            }
        }

        #endregion
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        private void cboUpLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1),-1);
            DataBinding.BindData(cboDepart, dataTable, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
        }

    }
}
