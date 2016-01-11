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
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_Quanly_Maukiemnghiem : Form
    {
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        private DataTable m_dtPatient=new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChiDinhCLS = new DataTable();
        private bool m_blnHasloaded = false;
        string Args = "ALL";
       
        public frm_Quanly_Maukiemnghiem(string Args)
        {
            InitializeComponent();
            this.Args = Args;
            //this.InitTrace();
            this.KeyPreview = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;
            
            InitEvents();
        }
        void InitEvents()
        {
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            this.txtPatient_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_ID_KeyDown);
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
           

            this.cmdThemMoiBN.Click += new System.EventHandler(this.cmdThemMoiBN_Click);
            this.cmdSuaThongTinBN.Click += new System.EventHandler(this.cmdSuaThongTinBN_Click);
            this.cmdThemLanKham.Click += new System.EventHandler(this.cmdThemLanKham_Click);
            this.cmdXoaBenhNhan.Click += new System.EventHandler(this.cmdXoaBenhNhan_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            
           

            this.grdAssignDetail.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignDetail_CellValueChanged);
            this.grdAssignDetail.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdAssignDetail_FormattingRow);
            this.grdAssignDetail.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignDetail_ColumnHeaderClick);
            this.grdAssignDetail.SelectionChanged += new System.EventHandler(this.grdAssignDetail_SelectionChanged);
            this.cmdXoaChiDinh.Click += new System.EventHandler(this.cmdXoaChiDinh_Click);
            this.cmdSuaChiDinh.Click += new System.EventHandler(this.cmdSuaChiDinh_Click);
            this.cmdThemChiDinh.Click += new System.EventHandler(this.cmdThemChiDinh_Click);
           
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
           
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Quanly_Maukiemnghiem_FormClosing);
            this.Load += new System.EventHandler(this.frm_Quanly_Maukiemnghiem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanly_Maukiemnghiem_KeyDown);
          
            cmdPrintAssign.Click += new EventHandler(cmdPrintAssign_Click);

        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuaThongTinBN.PerformClick();
        }

        void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string  mayin="";
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");

                }
                KCB_INPHIEU.InphieuChidinhCLS((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId, v_AssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked, ref mayin);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       
      
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                int Hos_status = -1;
                //if (radNgoaiTru.Checked) Hos_status = 0;
                //if (radNoiTru.Checked) Hos_status = 1;
                m_dtPatient = _KCB_DANGKY.KcbTiepdonTimkiemBenhnhan(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") :"01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Hos_status,
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text),"","", globalVariables.MA_KHOA_THIEN,0,(byte ) 100,this.Args.Split('-')[0]);
                Utility.SetDataSourceForDataGridEx(grdList, m_dtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                //Utility.SetMsg(lblTongSo, string.Format("&Tổng số bản ghi :{0}", m_dtPatient.Rows.Count), true);
                if (grdList.GetDataRows().Length <= 0)
                    m_dataDataRegExam.Rows.Clear();
                UpdateGroup();
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtPatient.AsEnumerable().GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool AllowTextChanged;
        
      
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Quanly_Maukiemnghiem_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
            if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
            AllowTextChanged = true;
            LayDsach_DoituongKCB();
          
            TimKiemThongTin(true);
            AutoloadSaveAndPrintConfig();
            m_blnHasloaded = true;
        }
      
        private void LayDsach_DoituongKCB()
        {
            DataBinding.BindDataCombobox(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                       DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Chọn đối tượng KCB---", true);
        }

        private void LoadChiDinh()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            m_dtChiDinhCLS =_KCB_DANGKY.LayChiDinhCLS_KhongKham(MaLuotkham, Patient_ID, 200);
            Utility.SetDataSourceForDataGridEx(grdAssignDetail,m_dtChiDinhCLS,false,true,"1=1","");
            UpdateWhanChanged();
            ModifycommandAssignDetail();
        }
        private DataTable m_dataDataRegExam=new DataTable();
       
        private void UpdateWhanChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if(gridExRow.RowType==RowType.Record)
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["TT"].Value =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0)*
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value);

                }
            }
            grdList.UpdateData();
            m_dtChiDinhCLS.AcceptChanges();
            UpdateSumOfChiDinh();
        }

        private void UpdateSumOfChiDinh()
        {
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdAssignDetail.RootTable.Columns["TT"];
            Janus.Windows.GridEX.GridEXColumn gridExColumnPhuThu = grdAssignDetail.RootTable.Columns[KcbChidinhclsChitiet.Columns.PhuThu];
            decimal Thanhtien = Utility.DecimaltoDbnull(grdAssignDetail.GetTotal(gridExColumn, AggregateFunction.Sum));
            decimal phuthu = Utility.DecimaltoDbnull(grdAssignDetail.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));

        }
        string MA_DTUONG = "DV";
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    if (m_dataDataRegExam != null) m_dataDataRegExam.Clear();
                    objLuotkham = null;
                    return;
                }
                if (grdList.CurrentRow != null)
                {
                    objLuotkham = CreatePatientExam();
                    MA_DTUONG = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaDoituongKcb));
                  
                   
                  
                    LoadChiDinh();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
              
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc cáu hình trạng thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            cmdSuaThongTinBN.Enabled =
           cmdXoaBenhNhan.Enabled =
           cmdThemLanKham.Enabled = Utility.isValidGrid(grdList);
           
        }
        /// <summary>
        /// hàm thực hiện viecj nhận formating trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
           
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }
        private void frm_Quanly_Maukiemnghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

       
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
       
        private KcbLuotkham CreatePatientExam()
        {
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham objLuotkham = new KcbLuotkham();

            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Patient_ID);
            objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
            return objLuotkham;


        }
        private void ModifycommandAssignDetail()
        {
            try
            {
                cmdSuaChiDinh.Enabled = Utility.isValidGrid(grdAssignDetail);
                cmdXoaChiDinh.Enabled = grdAssignDetail.GetCheckedRows().Length > 0;
                cmdPrintAssign.Enabled =Utility.isValidGrid(grdAssignDetail);
                chkIntach.Enabled = cmdPrintAssign.Enabled;
                cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            }
            catch (Exception exception)
            {
            }
            //cmdSend.Enabled = grdAssignInfo.RowCount > 0;
        }

        private void cmdThemChiDinh_Click(object sender, EventArgs e)
        {
            KcbLuotkham objLuotkham = CreatePatientExam();
            if(objLuotkham!=null)
            {
                frm_Chidinh_Maukiemnghiem frm = new frm_Chidinh_Maukiemnghiem("MAUKIEMNGHIEM", 3);
                frm.Exam_ID = Utility.Int32Dbnull(-1, -1);
                frm.txtAssign_ID.Text = "-100";
                frm.objLuotkham = objLuotkham;
                frm.m_eAction = action.Insert;
                frm.HosStatus = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LoadChiDinh();
                    UpdateSumOfChiDinh();
                }
                ModifycommandAssignDetail();
            }
            ModifyCommand();
        }
        private bool InValiUpdateChiDinh()
        {
            int Assign_ID = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(Assign_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu chỉ định này đã thanh toán, Bạn không được phép sửa(Có thể liên hệ với Quầy thanh toán để hủy thanh toán trước khi sửa lại)", "Thông báo");
                cmdThemChiDinh.Focus();
                return false;
            }
            return true;
        }
        private void cmdSuaChiDinh_Click(object sender, EventArgs e)
        {
              KcbLuotkham objLuotkham = CreatePatientExam();
              if (objLuotkham != null)
              {
                  if (!InValiUpdateChiDinh()) return;
                  frm_Chidinh_Maukiemnghiem frm = new frm_Chidinh_Maukiemnghiem("MAUKIEMNGHIEM",3);
                  frm.HosStatus = 0;
                
                  frm.Exam_ID = Utility.Int32Dbnull(-1, -1);
                  frm.objLuotkham = CreatePatientExam();
                  frm.m_eAction = action.Update;
                  frm.txtAssign_ID.Text = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                  frm.ShowDialog();
                  if (!frm.m_blnCancel)
                  {
                      //  LoadChiDinhCLS();
                      LoadChiDinh();
                      UpdateSumOfChiDinh();
                  }
                  ModifycommandAssignDetail();
              }
            ModifyCommand();
        }
        private bool InValiAssign()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;
                    break;
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private void PerforActionDeleteAssign()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int Assign_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);

                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtChiDinhCLS.AcceptChanges();
            }
            UpdateSumOfChiDinh();
        }

        private void cmdXoaChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InValiAssign()) return;
                var query = (from chidinh in grdAssignDetail.GetCheckedRows().AsEnumerable()
                             let x = Utility.sDbnull(chidinh.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value)
                             select x).ToArray();
                string ServiceDetail_Name = string.Join("; ", query);
                string Question = string.Format("Bạn có muốn xóa thông tin chỉ đinh {0} \n đang chọn không", ServiceDetail_Name);
                if (Utility.AcceptQuestion(Question, "Thông báo", true))
                {
                    PerforActionDeleteAssign();
                    //ModifyCommmand();
                    ModifycommandAssignDetail();
                }

            }
            catch(Exception ex)
            {
            }
           
           
        }

        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void txtTongChiPhiKham_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtTongChiPhiKham);
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Dangky_Kiemnghiem frm = new frm_Dangky_Kiemnghiem(this.Args);
                frm._OnAssign += frm__OnAssign;
                frm.m_enAction = action.Insert;
                frm.m_dtPatient = m_dtPatient;
              
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
                
            }
            catch (Exception exception)
            {
                
                
            }
            finally
            {
               // CauHinh();
            }
           
        }

        void frm__OnAssign()
        {
            cmdThemChiDinh.PerformClick();
        }

        void frm__OnActionSuccess()
        {
            UpdateGroup();
        }
        /// <summary>
        /// hàm thục hiện việc thêm lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemLanKham_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn bệnh nhân để thêm lượt khám mới");
                    return;
                }
                DataTable _temp = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                if (_temp != null && Utility.ByteDbnull(_temp.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) > 0 && Utility.ByteDbnull(_temp.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) < 4)
                {
                    Utility.ShowMsg("Bệnh nhân đang ở trạng thái nội trú và chưa ra viện nên không thể thêm lần khám mới. Đề nghị bạn xem lại");
                    return ;
                }
                frm_Dangky_Kiemnghiem frm = new frm_Dangky_Kiemnghiem(this.Args);
                frm._OnAssign += frm__OnAssign;
                frm.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                
                frm.m_enAction = action.Add;
                frm._OnActionSuccess+=frm__OnActionSuccess;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
               
            }
            catch (Exception)
            {
            }
            finally
            {
                //CauHinh();
            }
        }
        /// <summary>
        /// hàm thực hiện sửa thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để sửa thông tin");
                    return;
                }

                frm_Dangky_Kiemnghiem frm = new frm_Dangky_Kiemnghiem(this.Args);
                frm.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                
                frm._OnActionSuccess+=frm__OnActionSuccess;
                frm.m_enAction = action.Update;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
                
            }
            catch (Exception)
            {


            }
            finally
            {
                //CauHinh();
            }
          
        }
        #region "Thông tin khám chữa bệnh"
       
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";
        private string m_strDefaultLazerPrinterName = "";
        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch
            {
            }
        }
        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";
        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                Try2CreateFolder();
                
                using (var _reader = new StreamReader(strSaveandprintPath1))
                {
                    m_strDefaultLazerPrinterName = _reader.ReadLine().Trim();

                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
       
        
      
       
       
     
        KcbLuotkham objLuotkham = null;
       

        #endregion
       
        private void frm_Quanly_Maukiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);      
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
           
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSuaThongTinBN.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaBenhNhan.PerformClick();
            if (e.KeyCode == Keys.K && e.Control) cmdThemLanKham.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaBenhNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để xóa");
                    return;
                }
                string ErrMgs = "";
                string v_MaLuotkham =
                   Utility.sDbnull(
                     grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                       "");
                int v_Patient_ID =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);

                if (!IsValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin lần khám này không", "Thông báo", true))
                {
                  
                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeletePatientExam(null,v_MaLuotkham,
                                                                                                       v_Patient_ID, ref ErrMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            try
                            {
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Delete();
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                grdList_SelectionChanged(grdList, e);

                            }
                            catch
                            {

                            }
                            m_dtPatient.AcceptChanges();
                            UpdateGroup();
                            //Utility.ShowMsg("Xóa lần khám thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (ErrMgs != "")
                                Utility.ShowMsg(ErrMgs);
                            else
                                Utility.ShowMsg("Bệnh nhân đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần khám này", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }
               
                ModifyCommand();
            }
            catch
            {
            }
            finally
            {
               
            }
        }
        private bool IsValidDeleteData()
        {
             string v_MaLuotkham =
              Utility.sDbnull(
                grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                  "");
           int v_Patient_ID =
                Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
           if (objLuotkham != null)
           {
               if (objLuotkham.TrangthaiNgoaitru > 0)
               {
                   Utility.ShowMsg("Bệnh nhân đang chọn đã thực hiện khám ngoại trú nên bạn không được phép xóa");
                   return false;
               }
               if (objLuotkham.TrangthaiNoitru > 0)
               {
                   Utility.ShowMsg("Bệnh nhân đang chọn đã nhập viện nội trú nên bạn không được phép xóa");
                   return false;
               }
           }
            SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Bản ghi trong đăng ký khám đã thanh toán, Bạn không xóa được");
                return false;
            }
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                    new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
                        KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham).And(KcbChidinhcl.Columns.IdBenhnhan).
                        IsEqualTo(v_Patient_ID))
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bạn không thể xóa bệnh nhân trên vì bệnh nhân đã thanh toán một số dịch vụ cận lâm sàng", "Thông báo");
                return false;
            }
            sqlQuery = new Select().From(KcbDonthuoc.Schema)
                .Where(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham).And(KcbDonthuoc.Columns.IdBenhnhan).
                        IsEqualTo(v_Patient_ID)
                .And(KcbDonthuoc.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bạn không thể xóa bệnh nhân trên vì bệnh nhân đã có đơn thuốc được thanh toán", "Thông báo");
                return false;
            }
            sqlQuery = new Select().From(KcbThanhtoan.Schema)
                .Where(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if(sqlQuery.GetRecordCount()>0)
            {
                Utility.ShowMsg("Bệnh nhân đã có hóa đơn. Mời bạn qua hủy hóa đơn trước khi thực hiện xóa bệnh nhân", "Thông báo");
                return false;
            }
            return true;
        }
        /// <summary>
        /// ham thực hiện viecj in phiếu cỉnh định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuChiDinh_Click(object sender, EventArgs e)
        {
            if (grdAssignDetail.CurrentRow != null)
            {
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                if (objAssignInfo != null)
                {
                    frm_INPHIEU_CLS frm = new frm_INPHIEU_CLS();
                    frm.objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                    frm.ShowDialog();
                }

            }
           
        }
       
        /// <summary>
        /// hàm thực hiện viec lọc thông in trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void grdList_AddingRecord(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

       
        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "") 
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string patient_ID = Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientCode.Text = patient_ID;
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatient_ID.Text.Trim())!="") 
                {
                    string _code = txtPatientCode.Text.Trim();
                    txtPatientCode.Clear();
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatientCode.Text = _code;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }

    }
}
