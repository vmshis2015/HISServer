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
using VNS.HIS.NGHIEPVU;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_nhanvien : Form
    {
        #region "Declare Variable (Level private)"
        private DataTable m_dtStaffList=new DataTable();
        private DataTable m_dtStaffType=new DataTable();
        private DataTable m_dtRank=new DataTable();

        #endregion
        #region "Contructor"
        public frm_dmuc_nhanvien()
        {
            try
            {
                InitializeComponent();
                
                this.KeyDown += new KeyEventHandler(frm_dmuc_nhanvien_KeyDown);
                grdStaffList.ApplyingFilter += new CancelEventHandler(grdStaffList_ApplyingFilter);
                grdStaffList.SelectionChanged += new EventHandler(grdStaffList_SelectionChanged);
                grdStaffList.FilterApplied += new EventHandler(grdStaffList_FilterApplied);
                m_dtStaffType = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAINHANVIEN", true);


                grdStaffList.CellValueChanged += new ColumnActionEventHandler(grdStaffList_CellValueChanged);
                grdStaffList.UpdatingCell += new UpdatingCellEventHandler(grdStaffList_UpdatingCell);
                grdStaffList.CellUpdated += new ColumnActionEventHandler(grdStaffList_CellUpdated);
            }
            catch
            {
            }
        }
        #endregion
        #region"Method of Event Form"
        /// <summary>
        /// hàm thực hiện dóng Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện load dữ liệu khi load thông tin Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_nhanvien_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                Timkiemdulieu();
                ModifyCommand();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm dùng phím tắt của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_nhanvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
        }
        /// <summary>
        /// trạn thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = grdStaffList.RowCount > 0 && grdStaffList.CurrentRow.RowType == RowType.Record;
                cmdDelete.Enabled = grdStaffList.RowCount > 0 && grdStaffList.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {
                
                
            }
          
        }
        /// <summary>
        /// sự kiện tìm kiếm khi nhấn tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
           
            Timkiemdulieu();
            ModifyCommand();
        }
       
        private int v_StaffList_Id = -1;
        private bool IsValidData4Delete()
        {
            try
            {
                v_StaffList_Id = Utility.Int32Dbnull(grdStaffList.GetValue(DmucNhanvien.Columns.IdNhanvien), -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhcl.Schema)
                    .Where(KcbChidinhcl.Columns.IdBacsiChidinh).IsEqualTo(v_StaffList_Id);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                    return false;
                }
                sqlQuery = new Select().From(KcbDonthuoc.Schema)
                    .Where(KcbDonthuoc.Columns.IdBacsiChidinh).IsEqualTo(v_StaffList_Id);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                    return false;
                }
                sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                 .Where(KcbDangkyKcb.Columns.IdBacsikham).IsEqualTo(v_StaffList_Id);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                    return false;
                }
                sqlQuery = new Select().From(KcbThanhtoan.Schema)
                 .Where(KcbThanhtoan.Columns.IdNhanvienThanhtoan).IsEqualTo(v_StaffList_Id);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidData4Delete()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa nhân viên đang chọn không", "Thông báo", true))
                {
                    v_StaffList_Id = Utility.Int32Dbnull(grdStaffList.GetValue(DmucNhanvien.Columns.IdNhanvien), -1);
                    string ErrMsg=dmucnhanvien_busrule.Delete(v_StaffList_Id);
                    if (ErrMsg==string.Empty)
                    {
                        DataRow[] arrDr = m_dtStaffList.Select(DmucNhanvien.Columns.IdNhanvien + "=" + v_StaffList_Id);
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0].Delete();
                        }
                        m_dtStaffList.AcceptChanges();
                    }
                    else
                    {
                        Utility.ShowMsg(ErrMsg);
                    }
                }
                ModifyCommand();
            }
            catch (Exception ex)
            {
            }
        }

        private void grdStaffList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdStaffList)) return;
            if(grdStaffList.CurrentRow!=null)
            {
                v_StaffList_Id = Utility.Int32Dbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.IdNhanvien].Value, -1);
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện thêm mới nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            frm_themmoi_nhanvien frm=new frm_themmoi_nhanvien();
            frm.em_Action = action.Insert;
            frm.p_dtStaffList = m_dtStaffList;
            frm.ShowDialog();
            ModifyCommand();
        }
        /// <summary>
        /// sửa thông tin của phần nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdStaffList)) return;
            if(v_StaffList_Id<=-1)return;
            frm_themmoi_nhanvien frm = new frm_themmoi_nhanvien();
            frm.em_Action = action.Update;
            frm.txtID.Text = v_StaffList_Id.ToString();
            frm.UserName = Utility.sDbnull(grdStaffList.GetValue("user_name"));
            frm.p_dtStaffList = m_dtStaffList;
            frm.ShowDialog();
            ModifyCommand();
        }

        #endregion

        #region "Method of Common Form"

        private DataTable m_dtDepartment = new DataTable();
        private void InitData()
        {
            try
            {
                m_dtDepartment = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", -1);
                DataBinding.BindDataCombox(cboStaffType, m_dtStaffType, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "--- Chọn kiểu nhân viên---", true);
                DataBinding.BindDataCombox(cboParent, m_dtDepartment, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "--- Khoa phòng---", true);
            }
            catch
            {
            }
        }
        private void Timkiemdulieu()
        {
            try
            {

                SqlQuery _sqlquery = new Select().From(VDmucNhanvien.Schema);
                if (Utility.Int32Dbnull(cboDepartment.SelectedValue, -1) != -1)
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.IdPhong).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.IdPhong).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));

                if (Utility.Int32Dbnull(cboParent.SelectedValue, -1) != -1)
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.IdKhoa).IsEqualTo(Utility.Int32Dbnull(cboParent.SelectedValue, -1));
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.IdKhoa).IsEqualTo(Utility.Int32Dbnull(cboParent.SelectedValue, -1));


                if (Utility.sDbnull(cboStaffType.SelectedValue, "-1") != "-1")
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.MaLoainhanvien).IsEqualTo(Utility.sDbnull(cboStaffType.SelectedValue, "-1"));
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.MaLoainhanvien).IsEqualTo(Utility.sDbnull(cboStaffType.SelectedValue, "-1"));

                if (chknoUID.Checked)
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.UserName).IsEqualTo("");
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.UserName).IsEqualTo("");
                else
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.UserName).IsNotEqualTo("");
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.UserName).IsNotEqualTo("");
                m_dtStaffList = _sqlquery.ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdStaffList, m_dtStaffList, true, true, "", "");
                ModifyCommand();
            }
            catch
            {
            }
        }
       
        #endregion

        private void grdStaffList_FilterApplied(object sender, EventArgs e)
        {

            ModifyCommand();
        }

        

        private void cboParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable mDataTable =
                    THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboParent.SelectedValue, -1), -1);
                DataBinding.BindDataCombox(cboDepartment, mDataTable, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn phòng---", true);
            }
            catch
            {
            }
        }

        private void grdStaffList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void cmdReportAsUser_Click(object sender, EventArgs e)
        {
            ReportAsUserName();
        }

        private void ReportAsUserName()
        {
            //if(grdStaffList.CurrentRow!=null)
            //{
            //    v_StaffList_Id = Utility.Int32Dbnull(grdStaffList.CurrentRow.Cells["Staff_ID"].Value, -1);
            //    DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(v_StaffList_Id);
            //    if (string.IsNullOrEmpty(objDmucNhanvien.UserName))
            //    {
            //        Utility.ShowMsg("Nhân viên này chưa được gán User,Bạn xem lại ","Thông báo",MessageBoxIcon.Warning);
            //        return;
            //    }
            //    if(objDmucNhanvien!=null)
            //    {
            //        frm_SysTrinhKyTheoUser frm = new frm_SysTrinhKyTheoUser();
            //        frm.objStaff = objDmucNhanvien;
            //        frm.ShowDialog();
            //    }
            //    ModifyCommand();
            //}
        }

        private void cmdGanSysTrinhKy_Click(object sender, EventArgs e)
        {
            ReportAsUserName();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// Sửa thông tin trực tiếp trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdStaffList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                grdStaffList.UpdateData();
                grdStaffList.Refresh();
                m_dtStaffList.AcceptChanges();
                int record = new Update(DmucNhanvien.Schema)
                 .Set(DmucNhanvien.Columns.TenNhanvien).EqualTo(Utility.sDbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.TenNhanvien].Value, ""))
                 .Set(DmucNhanvien.Columns.MaNhanvien).EqualTo(Utility.sDbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.MaNhanvien].Value, ""))
                 .Set(DmucNhanvien.Columns.MotaThem).EqualTo(Utility.sDbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.MotaThem].Value, ""))
                 .Set(DmucNhanvien.Columns.TrangThai).EqualTo(Utility.Int16Dbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.TrangThai].Value))
                 .Where(DmucNhanvien.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.IdNhanvien].Value, -1)).Execute();

                if (record > 0)
                {
                    grdStaffList.UpdateData();
                    grdStaffList.Refresh();
                    m_dtStaffList.AcceptChanges();
                    //Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                    return;
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void grdStaffList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                var objValue = new object();

                if (e.Column.Key == DmucNhanvien.Columns.MaNhanvien)
                {
                    objValue = e.Value;
                    SqlQuery q = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.MaNhanvien).IsEqualTo(Utility.sDbnull(objValue)).And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(grdStaffList.CurrentRow.Cells[DmucNhanvien.Columns.MaNhanvien].Value, -1));
                    if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                    {
                        e.Cancel = true;
                        Utility.ShowMsg("Mã nhân viên không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                    }
                    if (q.GetRecordCount() > 0)
                    {
                        e.Cancel = true;
                        Utility.ShowMsg("Đã tồn tại mã nhân viên", "Thông báo tồn tại", MessageBoxIcon.Warning);
                    }
                }
                else if (e.Column.Key == DmucNhanvien.Columns.TenNhanvien)
                {
                    objValue = e.Value;
                    if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                    {
                        e.Cancel = true;
                        Utility.ShowMsg("Tên nhân viên không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {
            }
        }

        private void grdStaffList_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
           
        }
    }
}
