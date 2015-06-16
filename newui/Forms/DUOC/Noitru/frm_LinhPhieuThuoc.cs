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

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu_NoiTru
{
    public partial class frm_LinhPhieuThuoc : Form
    {
        public string madoituong { get; set; }
        public int id_khoXuat { get; set; }
        public bool IsPhieuBoSung { get; set; }
        public int idKhoaLinh { get; set; }
        public string loaiphieu { get; set; }
        public frm_LinhPhieuThuoc()
        {
            InitializeComponent();

        }

        private void frm_CapNhapPhatThuoc_Load(object sender, EventArgs e)
        {
            getData();
        }
        private void getData()
        {
            DPhieuCapphat objPhieuCapphat = DPhieuCapphat.FetchByID(Utility.Int32Dbnull(txtID_CAPPHAT.Text));
            if (objPhieuCapphat != null)
            {
                txtID_CAPPHAT.Text = Utility.sDbnull(objPhieuCapphat.IdCapphat);
                dtNgayCapPhat.Value = dtNgayCapPhat.Value;
                txtID_KHOA_LINH.Text = Utility.sDbnull(objPhieuCapphat.IdKhoaLinh);
                idKhoaLinh = Utility.Int32Dbnull(objPhieuCapphat.IdKhoaLinh);
                LDepartment objLDepartment = LDepartment.FetchByID(objPhieuCapphat.IdKhoaLinh);
                if (objLDepartment != null)
                {
                    txtTen_KHOA_LINH.Text = Utility.sDbnull(objLDepartment.DepartmentName);
                }
                //chkIsBoSung.Checked = Convert.ToBoolean(objPhieuCapphat.LinhBSung);

                IsPhieuBoSung = Convert.ToBoolean(objPhieuCapphat.LinhBSung);
                radLinhBoSung.Checked = IsPhieuBoSung;
                loaiphieu = Utility.sDbnull(objPhieuCapphat.LoaiPhieu);
                radThuoc.Checked = loaiphieu == "THUOC";
                radLinhVTYT.Checked = loaiphieu == "VT";
                txtID_NVIEN.Text = Utility.sDbnull(objPhieuCapphat.IdNvien);
                LStaff objStaff = LStaff.FetchByID(objPhieuCapphat.IdNvien);
                if (objStaff != null)
                {
                    txtTen_NVIEN.Text = Utility.sDbnull(objStaff.StaffName);
                }
                txtId_KhoXuat.Text = Utility.sDbnull(objPhieuCapphat.IdKhoXuat);
                id_khoXuat = Utility.Int32Dbnull(objPhieuCapphat.IdKhoXuat);
                DKho objKho = DKho.FetchByID(objPhieuCapphat.IdKhoXuat);
                if (objKho != null)
                {
                    txtTenKho.Text = Utility.sDbnull(objKho.TenKho);
                }
                madoituong = Utility.sDbnull(objPhieuCapphat.MaDoiTuong);
                txtMaDoiTuong.Text = Utility.sDbnull(madoituong);
                SqlQuery sqlQuery = new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(madoituong);
                LObjectType objectType = sqlQuery.ExecuteSingle<LObjectType>();
                if (objectType != null) txtObjectType_Name.Text = Utility.sDbnull(objectType.ObjectTypeName);
                txtMOTA_THEM.Text = Utility.sDbnull(objPhieuCapphat.MotaThem);
            }
            else
            {
                dtNgayCapPhat.Value = BusinessHelper.GetSysDateTime();
                txtID_KHOA_LINH.Text = Utility.sDbnull(idKhoaLinh);
                LDepartment objLDepartment = LDepartment.FetchByID(idKhoaLinh);
                if (objLDepartment != null)
                {
                    txtTen_KHOA_LINH.Text = Utility.sDbnull(objLDepartment.DepartmentName);
                }
                txtID_NVIEN.Text = Utility.sDbnull(globalVariables.gv_StaffID);
                LStaff objStaff = LStaff.FetchByID(globalVariables.gv_StaffID);
                if (objStaff != null)
                {
                    txtTen_NVIEN.Text = Utility.sDbnull(objStaff.StaffName);
                }

                txtId_KhoXuat.Text = Utility.sDbnull(id_khoXuat);
                DKho objKho = DKho.FetchByID(id_khoXuat);
                if (objKho != null)
                {
                    txtTenKho.Text = Utility.sDbnull(objKho.TenKho);
                }
                //loaiphieu = Utility.sDbnull(objPhieuCapphat.LoaiPhieu);
                radThuoc.Checked = loaiphieu == "THUOC";
                radLinhVTYT.Checked = loaiphieu == "VT";
                radLinhBoSung.Checked = IsPhieuBoSung;
                // IsPhieuBoSung = Convert.ToBoolean(objPhieuCapphat.LinhBSung);
                txtMaDoiTuong.Text = Utility.sDbnull(madoituong);
                SqlQuery sqlQuery = new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(madoituong);
                LObjectType objectType = sqlQuery.ExecuteSingle<LObjectType>();
                if (objectType != null) txtObjectType_Name.Text = Utility.sDbnull(objectType.ObjectTypeName);
            }
            LoadPhieuDonThuoc();
        }
        private DataTable m_dtDonThuoc=new DataTable();
        private void LoadPhieuDonThuoc()
        {
            m_dtDonThuoc =
                SPs.DuocTimDonThuocDeChiaBn(Utility.Int32Dbnull(txtID_CAPPHAT.Text,-100)).GetDataSet().Tables[0];

                

            Utility.SetDataSourceForDataGridEx(grdPres, m_dtDonThuoc, true, true, "IsChon=0", "");
            //Modifycommand();
        }
        /// <summary>
        /// hàm thực hiện việc phát thuốc cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPresDetail_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
           
        }
        private DataTable m_dtPhieuCapPhatLichSuCT = new DataTable();
        private DataSet ds = new DataSet();
        /// <summary>
        /// hàm thực hiện việc load thông tin cấp phát chi tiết
        /// </summary>
        private void LoadPhieuCapPhatCT()
        {
            if (grdPres.CurrentRow != null && grdPres.CurrentRow.RowType == RowType.Record)
            {
                int idcapphat = Utility.Int32Dbnull(grdPres.GetValue(DPhieuCapphat.Columns.IdCapphat));
                int Id_donThuoc = Utility.Int32Dbnull(grdPres.GetValue(TPrescription.Columns.PresId));
                m_dtPhieuCapPhatLichSuCT = SPs.DuocCapPhatPhieuCapPhatCT(Id_donThuoc, idcapphat).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtPhieuCapPhatLichSuCT,true,true,"1=1","");


            }
            else
            {
                grdPresDetail. DataSource = null;
                //grdChiTietCapPhat.DataSource = null;
            }
        }
        private void grdPres_SelectionChanged(object sender, EventArgs e)
        {
            LoadPhieuCapPhatCT();
        }
        /// <summary>
        /// hàm thực hiện viecj phát thuốc hết cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPhatHet_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Janus.Windows.GridEX.GridEXRow grdRow in grdPresDetail.GetDataRows())
            {
                grdRow.BeginEdit();
                grdRow.Cells[DPhieuCapphatCt.Columns.DaLinh].Value = 1;
                grdRow.EndEdit();
            }
        }

        private void grdPresDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void cmdLuuLai_Click(object sender, EventArgs e)
        {
         
            foreach (Janus.Windows.GridEX.GridEXRow grdExRow in grdPresDetail.GetDataRows())
            {
                int IdCapPhatCt = Utility.Int32Dbnull(grdExRow.Cells[DPhieuCapphatCt.Columns.IdCapPhatCt].Value);
                int presdetail_id = Utility.Int32Dbnull(grdExRow.Cells[DPhieuCapphatCt.Columns.IdDonthuocCtiet].Value);
                int soluong = Utility.Int32Dbnull(grdExRow.Cells[DPhieuCapphatCt.Columns.SoLuong].Value);
                int DaLinh = Utility.Int32Dbnull(grdExRow.Cells[DPhieuCapphatCt.Columns.DaLinh].Value);
               // int value = Utility.Int32Dbnull(e.Value);
                StoredProcedure sp = SPs.DuocUpdateLinhSoLuongThuoc(IdCapPhatCt, presdetail_id, DaLinh);
                sp.Execute();
                var query = from thuoc in m_dtPhieuCapPhatLichSuCT.AsEnumerable()
                    where
                        Utility.Int32Dbnull(DPhieuCapphatCt.Columns.IdCapPhatCt) ==
                        Utility.Int32Dbnull(grdExRow.Cells[DPhieuCapphatCt.Columns.IdCapPhatCt].Value)
                    select thuoc;
                if (query.Any())
                {
                    var firstrow = query.FirstOrDefault();
                    if (firstrow != null)
                    {
                        firstrow[TPrescriptionDetail.Columns.SluongLinh] = soluong == 1 ? soluong : 0;
                        firstrow.AcceptChanges();
                    }
                }
                grdPresDetail.UpdateData();
            }
              

           
        }

        private void chkChuaPhat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string _rowFilter = "1=1";
                if (chkChuaPhat.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", DPhieuCapphatCt.Columns.DaLinh, false);

                }
                m_dtPhieuCapPhatLichSuCT.DefaultView.RowFilter = _rowFilter;
            }
            catch (Exception)
            {
                
                //throw;
            }
          
        }

        private void chkDaLinh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string _rowFilter = "1=1";
                if (chkChuaPhat.Checked)
                {
                    _rowFilter = string.Format("{0}={1}", DPhieuCapphatCt.Columns.DaLinh, true);

                }
                m_dtPhieuCapPhatLichSuCT.DefaultView.RowFilter = _rowFilter;
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
}
