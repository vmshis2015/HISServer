using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.DAL;
using VNS.Libs;
using SubSonic;


namespace  VNS.HIS.UI.NGOAITRU
{
    public partial class frm_NOITIET_LSU_KCB : Form
    {
        public frm_NOITIET_LSU_KCB()
        {
            InitializeComponent();
        }

        private DataTable m_dtDoctorAssign;
        DataTable dt_ICD_PHU = new DataTable();
                                                        
        private void frm_NOITIET_LSU_KCB_Load(object sender, EventArgs e)
        {
            SetPropertion();
            m_dtDoctorAssign = THU_VIEN_CHUNG.LAYTHONGTIN_BACSY_NGOAITRU();
            DataBinding.BindDataCombox(cboDoctorAssign, m_dtDoctorAssign, LStaff.Columns.StaffId,LStaff.Columns.StaffName, "---Bác sỹ chỉ định---");
            DataBinding.BindDataCombox(cboNhomMau, THU_VIEN_CHUNG.LayThongTin_NhomMau(), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "--Nhóm máu--");
            cboNhomMau.SelectedIndex = 0;
            cboDoctorAssign.SelectedIndex = 0;
            if (!dt_ICD_PHU.Columns.Contains("MA_ICD")){dt_ICD_PHU.Columns.Add("MA_ICD", typeof (string));}
            if (!dt_ICD_PHU.Columns.Contains("TEN_ICD")){dt_ICD_PHU.Columns.Add("TEN_ICD", typeof (string));}
            txtMaTimKiem.ForeColor = chkMaBenhNhan.Checked ? Color.Green : Color.IndianRed;
        }
        private void SetPropertion()
        {
            try
            {
                foreach (Control control in grpThongTinBenhNhan.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtControl = new EditBox();
                        if (txtControl.Tag != "NO")
                        {
                            txtControl = ((EditBox) (control));
                            txtControl.ReadOnly = true;
                            txtControl.BackColor = Color.WhiteSmoke;
                            txtControl.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
                            txtControl.Font.Bold.CompareTo(true);
                            txtControl.ReadOnly = true;
                        }
                        txtControl.ForeColor = Color.Black;
                    }
                    if (control is CalendarCombo)
                    {
                        var cboControl = new CalendarCombo();
                        if (cboControl.Tag != "NO")
                        {
                            cboControl = (CalendarCombo) control;
                            cboControl.ReadOnly = true;
                            cboControl.Font.Bold.CompareTo(true);
                            cboControl.BorderStyle = Janus.Windows.CalendarCombo.BorderStyle.None;
                            cboControl.ShowUpDown = false;
                            cboControl.ShowDropDown = false;
                            cboControl.ForeColor = Color.Black;
                            cboControl.BackColor = Color.WhiteSmoke;
                            cboControl.ReadOnly = true;
                        }
                    }
                    if (control is UICheckBox)
                    {
                        var chkControl = new UICheckBox();
                        if (chkControl.Tag != "NO")
                        {
                            chkControl = (UICheckBox) control;
                            chkControl.Enabled = false;
                           
                        }
                    }
                }

                foreach (Control control in grpThongTinLanKham.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtControl = new EditBox();
                        if (txtControl.Tag != "NO")
                        {
                            txtControl = ((EditBox) (control));
                            txtControl.ReadOnly = true;
                            txtControl.BackColor = Color.WhiteSmoke;
                            txtControl.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
                            txtControl.Font.Bold.CompareTo(true);
                        }
                        txtControl.ForeColor = Color.Black;
                    }
                    if (control is CalendarCombo)
                    {
                        var cboControl = new CalendarCombo();
                        if (cboControl.Tag != "NO")
                        {
                            cboControl = (CalendarCombo) control;
                            cboControl.ReadOnly = true;
                            cboControl.Font.Bold.CompareTo(true);
                            cboControl.BorderStyle = Janus.Windows.CalendarCombo.BorderStyle.None;
                            cboControl.ShowUpDown = false;
                            cboControl.ShowDropDown = false;
                            cboControl.ForeColor = Color.Black;
                            cboControl.BackColor = Color.WhiteSmoke;
                        }
                    }
                    if (control is UICheckBox)
                    {
                        var chkControl = new UICheckBox();
                        if (chkControl.Tag != "NO")
                        {
                            chkControl = (UICheckBox) control;
                            chkControl.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình set thuộc tính control");
                throw;
            }
        }
        private DataTable dtPatientInfo;
        private DataTable dtPatientExam;
        private void txtMaTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                TimKiem();
            }
        }
        void TimKiem()
        {
            try
            {
                int KieuTimKiem = -1;
                string sTimKiem = "";
                grdListPatientInfo.DataSource = null;
                if(tabTimKiem.SelectedIndex == 0)
                {
                  
                    if(chkMaBenhNhan.Checked)
                    {
                        KieuTimKiem = 0;

                    }else if(chkMaLanKham.Checked)
                    {
                        KieuTimKiem = 1;
                    }
                    sTimKiem = Utility.sDbnull(txtMaTimKiem.Text, "");
                    
                }
                else if(tabTimKiem.SelectedIndex == 1)
                {
                    KieuTimKiem = 2;
                    sTimKiem = Utility.sDbnull(txtTenBenhNhan.Text, "");
                }
                dtPatientInfo = SPs.NoitietTimkiemLsuBenhnhan(dtpTuNgay.Value, dtpDenNgay.Value, sTimKiem, KieuTimKiem).GetDataSet().Tables[0];
                grdListPatientInfo.DataSource = dtPatientInfo;
                grdListPatientInfo.AutoSizeColumns();
                if(dtPatientInfo.Rows.Count > 0)
                {
                    grdListPatientInfo.MoveFirst();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá tìm kiếm thông tin bệnh nhân");
                throw;
            }
        }
        int PatientId = -1;
        private void grdListPatientInfo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                grdListPatientExam.DataSource = null;
                if(grdListPatientInfo.CurrentRow != null)
                {
                    PatientId = Utility.Int32Dbnull(grdListPatientInfo.CurrentRow.Cells[TPatientInfo.Columns.PatientId].Value, -1);                 
                    radNam.Checked = Utility.Int32Dbnull(grdListPatientInfo.GetValue(TPatientInfo.Columns.PatientSex), 0) == 0;
                    radNu.Checked = Utility.Int32Dbnull(grdListPatientInfo.GetValue(TPatientInfo.Columns.PatientSex), 0) == 1;
                    txtPatient_Name.Text = Utility.sDbnull(grdListPatientInfo.GetValue(TPatientInfo.Columns.PatientName), "");
                    txtDiaChi.Text = Utility.sDbnull(grdListPatientInfo.GetValue(TPatientInfo.Columns.PatientAddr), "");
                    txtYear_Of_Birth.Text = Utility.sDbnull(grdListPatientInfo.GetValue(TPatientInfo.Columns.YearOfBirth), globalVariables.SysDate.Year);
                    txtNgheNghiep.Text = Utility.sDbnull(grdListPatientInfo.GetValue(TPatientInfo.Columns.PatientJob), "");
                    txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) - Utility.Int32Dbnull(txtYear_Of_Birth.Text));
                    txtPatient_ID.Text = Utility.sDbnull(PatientId, "");
                    dtPatientExam = new Select().From(TPatientExam.Schema).Where(TPatientExam.Columns.PatientId).IsEqualTo(PatientId).ExecuteDataSet().Tables[0];
                    grdListPatientExam.DataSource = dtPatientExam;
                    grdListPatientExam.AutoSizeColumns();
                    if(dtPatientExam.Rows.Count > 0)
                        grdListPatientExam.MoveFirst();
                }
                else
                {
                    grdListPatientExam.ClearItems();                   
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin lần khám");                
            }
        }
        private void ClearControl()
        {
            try
            {              
                foreach (Control control in grpThongTinBenhNhan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                }
                foreach (Control control in grpThongTinLanKham.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                }
                foreach (Control control in grpThongTinChanDoan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private TPatientExam ObjPatientExam;
        private void grdListPatientExam_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(grdListPatientExam.CurrentRow != null)
                {
                    string PatientCode = Utility.sDbnull(grdListPatientExam.GetValue(TPatientExam.Columns.PatientCode), "");
                    ObjPatientExam = new Select().From(TPatientExam.Schema).Where(TPatientExam.Columns.PatientCode).IsEqualTo(PatientCode)
                        .And(TPatientExam.Columns.PatientId).IsEqualTo(PatientId).ExecuteSingle<TPatientExam>();                   
                    if (ObjPatientExam != null)
                    {
                         
                         txtPatient_Code.Text = Utility.sDbnull(ObjPatientExam.PatientCode, "");
                         barcode.Data = Utility.sDbnull(ObjPatientExam.PatientCode,"");

                        txtObjectType_Name.Text = BusinessHelper.GetObjectTypeName(ObjPatientExam.ObjectTypeId);
                        txtObjectType_Code.Text = Utility.sDbnull(ObjPatientExam.MaDoiTuong, "");
                        txtSoBHYT.Text = Utility.sDbnull(ObjPatientExam.InsuranceNum, "");                               
                        txtBHTT.Text = Utility.sDbnull(ObjPatientExam.DiscountRate, "0");
                        txtHanTheBHYT.Text = Utility.sDbnull(ObjPatientExam.InsuranceFromDate, "");                        
                        Load_Trieuchung_LanKham(ObjPatientExam.PatientCode, PatientId);
                        DataTable dt_TRegExam = new Select().From(TRegExam.Schema)
                            .Where(TRegExam.Columns.PatientCode).IsEqualTo(ObjPatientExam.PatientCode)
                            .And(TRegExam.Columns.PatientId).IsEqualTo(ObjPatientExam.PatientId).ExecuteDataSet().Tables[0];
                        if(dt_TRegExam.Rows.Count > 0)
                        {
                            cboRegExam.DataSource = dt_TRegExam;
                            cboRegExam.ValueMember = TRegExam.Columns.RegId;
                            cboRegExam.DisplayMember = TRegExam.Columns.KieuKhambenh;
                            cboRegExam.SelectedIndex = 0;
                            if(dt_TRegExam.Rows.Count > 1)
                            {
                                cboRegExam.Enabled = true;
                            }else
                            {
                                cboRegExam.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        ClearControl();
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin lấy khám");             
            }
        }


        private void Load_Trieuchung_LanKham(string patient_Code, int patient_Id)
        {
            try
            {
              //  TExamFull objExamFull = new Select().From(TExamFull.Schema)
              //.Where(TExamFull.Columns.PatientCode).IsEqualTo(patient_Code)
              //.And(TExamFull.Columns.PatientId).IsEqualTo(patient_Id).ExecuteSingle<TExamFull>();
              //  if (objExamFull != null)
              //  {
              //      txtHa.Text = Utility.sDbnull(objExamFull.BloodPres);
              //      txtNhipTim.Text = Utility.sDbnull(objExamFull.Artery);
              //      txtNhipTho.Text = Utility.sDbnull(objExamFull.BreatheRate);
              //      txtNhietDo.Text = Utility.sDbnull(objExamFull.BodyTemp);
              //      if (!string.IsNullOrEmpty(Utility.sDbnull(objExamFull.BloodType)) && Utility.sDbnull(objExamFull.BloodType) != "-1")
              //          cboNhomMau.SelectedValue = Utility.sDbnull(objExamFull.BloodType);
              //  }
              //  else
              //  {
              //      txtHa.Text = "";
              //      txtNhietDo.Text = "";
              //      txtNhipTho.Text = "";
              //      txtNhipTim.Text = "";
              //      cboNhomMau.SelectedIndex = 0;
              //  }
            }
            catch (Exception)
            {
              Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin triệu trứng lần khám");
            }


        }

        private void cboRegExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSoLanKham.Text = Utility.sDbnull(cboRegExam.SelectedValue, -1);
                TRegExam objRegExam = TRegExam.FetchByID(Utility.Int32Dbnull(txtSoLanKham.Text));
                if (objRegExam != null)
                {
                    DataTable m_dtThongTin =
                        SPs.LaokhoaLaythongtinBenhnhanNgoaitruLankham(ObjPatientExam.PatientCode,
                                                                      Utility.Int32Dbnull(ObjPatientExam.PatientId, -1),
                                                                      Utility.Int32Dbnull(txtSoLanKham.Text)).GetDataSet
                            ().Tables[0];
                    if (m_dtThongTin.Rows.Count > 0)
                    {
                        DataRow dr = m_dtThongTin.Rows[0];
                        if (dr != null)
                        {
                            dtInput_Date.Value = Convert.ToDateTime(dr[TPatientExam.Columns.InputDate]);

                            txtExam_ID.Text = Utility.sDbnull(objRegExam.RegId);
                            dtpCreatedDate.Value = Convert.ToDateTime(objRegExam.RegDate);
                            txtDepartment_ID.Text = Utility.sDbnull(objRegExam.DepartmentId);
                            LDepartment objDepartment = LDepartment.FetchByID(Utility.Int32Dbnull(txtDepartment_ID.Text));
                            if (objDepartment != null)
                                txtDepartment_Name.Text = Utility.sDbnull(objDepartment.DepartmentName);
                            txtTenKham.Text = Utility.sDbnull(objRegExam.KieuKhambenh);
                            if (objRegExam.UserId == "ADMIN")
                            {
                                txtNguoiTiepNhan.Text = objRegExam.UserId;
                            }
                            else
                            {
                                LStaff lStaff = BusinessHelper.GetStaffByUserName(objRegExam.UserId);
                                if (lStaff != null)
                                {
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(lStaff.StaffName);
                                }else
                                {
                                    txtNguoiTiepNhan.Text = objRegExam.UserId;
                                }
                                
                            }
                            if (Utility.Int32Dbnull(objRegExam.IdBsyThien, -1) <= 0)
                            {

                                cboDoctorAssign.SelectedValue = Utility.Int32Dbnull(objRegExam.IdBsyThien, -1);
                            }
                            else
                            {

                                cboDoctorAssign.SelectedIndex = 0;
                            }
                            //TExamInfo objExam = TExamInfo.FetchByID(Utility.Int32Dbnull(txtExam_ID.Text));
                            //if (objExam != null)
                            //{
                            //    txtKet_Luan.Text = Utility.sDbnull(objExam.KetLuan);
                            //    txtHuongdieutri.Text = objExam.HuongDieuTri;
                            //    nmrSongayDT.Value = Utility.DecimaltoDbnull(objExam.SoNgay, 0);
                            //    var objDiagInfo = new Select().From(TDiagInfo.Schema)
                            //        .Where(TDiagInfo.Columns.ExamId).IsEqualTo(objExam.ExamId).ExecuteSingle<TDiagInfo>();
                            //    if (objDiagInfo != null)
                            //    {
                            //        txtChanDoan.Text = Utility.sDbnull(objDiagInfo.DiagInfo);
                            //        txtMaBenhChinh.Text = Utility.sDbnull(objDiagInfo.MainDiseaseId);
                            //        string dataString = Utility.sDbnull(objDiagInfo.AuxiDiseaseId, "");
                            //        if (!string.IsNullOrEmpty(dataString))
                            //        {
                            //            dt_ICD_PHU.Clear();                                                                               
                            //            string[] rows = dataString.Split(',');
                            //            foreach (string row in rows)
                            //            {
                            //                if (!string.IsNullOrEmpty(row))
                            //                {
                            //                    DataRow newDr = dt_ICD_PHU.NewRow();
                            //                    newDr["MA_ICD"] = row;
                            //                    newDr["TEN_ICD"] = GetTenBenh(row);
                            //                    dt_ICD_PHU.Rows.Add(newDr);
                            //                    dt_ICD_PHU.AcceptChanges();
                            //                }
                            //            }
                            //            grd_ICD.DataSource = dt_ICD_PHU;
                            //        }
                            //        else
                            //        {
                            //            dt_ICD_PHU.Clear();
                            //        }
                            //    }
                            //}
                            GetDataChiDinh();
                        }
                    }
                }

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khám chữa bệnh");
            }
        }

        private void GetDataChiDinh()
        {
            DataSet ds =SPs.YhhqLaythongtinCdinhThuocTheolankham(Utility.Int32Dbnull(txtPatient_ID.Text, -1),Utility.sDbnull(txtPatient_Code.Text, ""),Utility.Int32Dbnull(txtExam_ID.Text)).GetDataSet();
            DataTable m_dtAssignDetail = ds.Tables[0];
            DataTable m_dtPresDetail = ds.Tables[1];
            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "", "GroupIntOrder,sIntOrder,ServiceDetail_Name");
            grdAssignDetail.AutoSizeColumns();
            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtPresDetail, false, true, "",TPrescriptionDetail.Columns.ThuTuIn);
            grdPresDetail.AutoSizeColumns();
            getResult();
        }
        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh = globalVariables.g_dtDiseaseList.Select(string.Format("Disease_Code='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][LDisease.Columns.DiseaseName], "");
            return TenBenh;
        }
        void getResult()
        {
            try
            {

                DataTable temdt = SPs.LayKetquaCls(Utility.sDbnull(txtPatient_Code.Text)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdKetQua, temdt, true, true, "", "");
                grdKetQua.AutoSizeColumns();
                uiGroupBox7.Visible = (temdt != null && temdt.Rows.Count > 0);                
            }
            catch
            {
                uiGroupBox7.Visible = false;
            }
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTimKiem.ForeColor = Color.IndianRed;
        }

        private void chkMaLanKham_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTimKiem.ForeColor = Color.Green;
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void frm_NOITIET_LSU_KCB_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                    case Keys.Escape:
                    Close();
                    break;
                    case Keys.F1:
                    tabDiagInfo.SelectedIndex = 0;
                    break;
                    case Keys.F2:
                    tabDiagInfo.SelectedIndex = 1;
                    break;
                    case Keys.F3:
                    tabDiagInfo.SelectedIndex = 2;
                    break;
            }
        }

    }  
} 


