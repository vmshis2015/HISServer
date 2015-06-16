using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using SubSonic;
using VNS.Libs;
namespace  VNS.HIS.UI.NGOAITRU
{
    public partial class frm_MOKHOA_BENHNHAN : Form
    {
        public frm_MOKHOA_BENHNHAN()
        {
            InitializeComponent();
        }

        private void txtPatientCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CleanData();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình nhập ký tự");
                throw;
            }
          
        }

        private TPatientExam patientExam = new TPatientExam();
        void GetData()
        {
            try
            {
                 patientExam =
                    new Select().From(TPatientExam.Schema).Where(TPatientExam.Columns.PatientCode).IsEqualTo(
                        txtPatientCode.Text).ExecuteSingle<TPatientExam>();
                if(patientExam != null)
                {
                    TPatientInfo patientInfo = TPatientInfo.FetchByID(patientExam.PatientId);
                    
                    txtMaBenhNhan.Text = Utility.sDbnull(patientInfo.PatientId, "");
                    txtMaLanKham.Text = Utility.sDbnull(patientExam.PatientCode, "");
                    txtTenBenhNhan.Text = Utility.sDbnull(patientInfo.PatientName, "");
                    txtDiaChiBenhNhan.Text = Utility.sDbnull(patientInfo.DiaChiBn,"");
                    txtTuoiBN.Text = Utility.sDbnull(Utility.Int32Dbnull(BusinessHelper.GetSysDateTime().Year) - Utility.Int32Dbnull(patientInfo.YearOfBirth), -1);
                    txtDoiTuong.Text = BusinessHelper.GetObjectTypeName(Utility.Int32Dbnull(patientExam.ObjectTypeId));
                    cmdMoKhoa.Enabled = Utility.Int32Dbnull(patientExam.Locked) > 0;

                    switch (Utility.Int32Dbnull(patientExam.HosStatus, -1))
                    {
                        case 0:
                            txtTrangThaiBN.Text = "Ngoại trú";                           
                            break;
                        case 1:
                            txtTrangThaiBN.Text = "Nội trú";                           
                            break;
                        case 2:
                            txtTrangThaiBN.Text = "Đang chờ ra viện";
                            
                            break;
                        case 3:
                            txtTrangThaiBN.Text = "Đã ra viện";
                            cmdMoKhoa.Enabled = true;

                            break;
                    }
                    switch (Utility.Int32Dbnull(patientExam.Locked,0))
                    {
                        case 0:
                            txtTrangthai.Text = "Chưa khóa";
                            break;
                        case 1:
                            txtTrangthai.Text = "Đã khóa";
                            break;
                    }
                    if(Utility.sDbnull(patientExam.MaDoiTuong, "") == "BHYT")
                    {
                        txtSoBHYT.Text = Utility.sDbnull(patientExam.InsuranceNum, "");
                        txtDiaChiBHYT.Text = Utility.sDbnull(patientInfo.PatientAddr,"");
                    }
                    else
                    {
                        txtSoBHYT.Text = "";
                        txtDiaChiBHYT.Text = "";
                    }
                    txtNgayVaoKham.Text = patientExam.InputDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    CleanData();
                    Utility.SetMessageError(errorProvider1, txtPatientCode,"Không tồn tại bệnh nhân có mã lần khám đã nhập");
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                throw;
            }
        }

        private DataTable dtPatient;
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
                return;
            dtPatient = SPs.NoitietTimkiemBenhnhan(txtPatientCode.Text, -1, -1).GetDataSet().Tables[0];
            if (txtPatientCode.TextLength < 8 && dtPatient.Rows.Count > 0)
            {
                    frm_DSACH_BN_TKIEM frm = new frm_DSACH_BN_TKIEM();
                    frm.PatientCode = txtPatientCode.Text;
                    frm.dtPatient = dtPatient;
                    frm.ShowDialog();
                    if (!frm.has_Cancel)
                    {
                        txtPatientCode.Text = frm.PatientCode;
                    }
                GetData();
            }
            else if (txtPatientCode.TextLength == 8)
            {
                GetData();
            }
        }
        void CleanData()
        {
            txtMaBenhNhan.Text = "";
            txtTenBenhNhan.Text = "";
            txtDiaChiBenhNhan.Text = "";
            txtDoiTuong.Text = "";
            txtSoBHYT.Text = "";
            txtDiaChiBHYT.Text = "";
            txtNgayVaoKham.Text = "";
            txtMaLanKham.Text = "";
            txtTrangThaiBN.Text = "";
            txtTuoiBN.Text = "";
            txtTrangThaiBN.Text = "";
            txtTrangthai.Text = "";
            Utility.ResetMessageError(errorProvider1);
            cmdMoKhoa.Enabled = false;
        }

        private void frm_MOKHOA_BENHNHAN_Load(object sender, EventArgs e)
        {
            CleanData();
        }

        private void cmdMoKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Utility.AcceptQuestion("Bạn có chắc chắn muốn mở khóa bệnh nhân này không ?","Xác nhận", true))
                    return;
                int record = -1;
                record =
                    new Update(TPatientExam.Schema).Set(TPatientExam.Columns.Locked).EqualTo(0).Where(
                        TPatientExam.Columns.PatientCode)
                        .IsEqualTo(txtPatientCode.Text).Execute();
                if(record > 0)
                {
                    Utility.ShowMsg("Đã cập nhật thông tin thành công");
                    patientExam.Locked = 0;
                    txtTrangthai.Text = "Đã khóa";
                }
                else
                {
                    Utility.ShowMsg("Chưa cập nhật được thông tin vào cơ sở dữ liệu");
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình cập nhật thông tin vào cơ sở dữ liệu");
                
            }
        }
    }
}
