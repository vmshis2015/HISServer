using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Properties;
using VNS.Libs;
using System.IO;
using System.Diagnostics;
using SubSonic;
using Microsoft.VisualBasic;
using VNS.Core.Classes;
using VNS.Libs.AppUI;
using System.Xml.Serialization;
using QMS;
using VNS.HIS.DAL;

namespace VNS.QMS
{
    public partial class frm_SoKham : Form
    {
        private QMSProperties _QMSProperties;
        bool hasLoaded = false;
        public frm_SoKham()
        {
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            if (!new ConnectionSQL().ReadConfig())
            {
                Try2ExitApp();
                return;
            }
            InitializeComponent();
            globalVariables.m_strPropertiesFolder = Application.StartupPath;
            _QMSProperties = GetQMSProperties();
            this.KeyDown += new KeyEventHandler(frm_SoKham_KeyDown);
            Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            this.Load+=new EventHandler(frm_SoKham_Load);
            CauHinh();
            this.cmdGetSoKham.Click += new EventHandler(this.cmdGetSoKham_Click);
            cmdgetBHYT.Click += new EventHandler(cmdgetBHYT_Click);
            this.cmdKhamUuTien.Click += new EventHandler(this.cmdKhamUuTien_Click);
            this.cmdKhamYC.Click += new EventHandler(this.cmdKhamYC_Click);

            chkLaythemsoYC.CheckedChanged += new EventHandler(chkLaythemsoYC_CheckedChanged);
            chkLaythemsothuong.CheckedChanged += new EventHandler(chkLaythemsothuong_CheckedChanged);
            chkLaythemsouutien.CheckedChanged += new EventHandler(chkLaythemsouutien_CheckedChanged);
            linkLabel1.Click += new EventHandler(linkLabel1_Click);

            nmrLaythemsothuong.ValueChanged += new EventHandler(nmrLaythemsothuong_ValueChanged);
            nmrLaythemsoUutien.ValueChanged += new EventHandler(nmrLaythemsoUutien_ValueChanged);
            nmrLaythemsoYC.ValueChanged += new EventHandler(nmrLaythemsoYC_ValueChanged);

            cmdConfig.Click+=new EventHandler(cmdConfig_Click);
        }

        void cmdgetBHYT_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            int soluong = 1;
            if (chkLaythemsoBhyt.Checked)
                soluong = (int)nmrLaythemsoBHYT.Value;
            for (int i = 1; i <= soluong; i++)
            {
                this.LaySokham(1, txtSoKhamBHYT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh),"BHYT", 0);
                if (_QMSProperties.PrintStatus)
                {
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdgetBHYT, Utility.sDbnull(txtSoKhamBHYT.Text), 0);
                }
            }
            Utility.DefaultNow(this);
        }

        void nmrLaythemsoYC_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpsoYC.Text = "Nếu bạn nhấn nút Lấy số yêu cầu. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + nmrLaythemsoYC.Value).ToString();
        }

        void nmrLaythemsoUutien_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpUutien.Text = "Nếu bạn nhấn nút Lấy số ưu tiên. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + nmrLaythemsoUutien.Value).ToString();
        }

        void nmrLaythemsothuong_ValueChanged(object sender, EventArgs e)
        {
            if (!hasLoaded) return;
            lblHelpsothuong.Text = "Nếu bạn nhấn nút Lấy số thường. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(txtSoKham.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(txtSoKham.Text, 0) + nmrLaythemsothuong.Value).ToString();
        }
        public static QMSProperties GetQMSProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QMSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi đọc cấu hình Properties\n"+ex.Message);
                return new QMSProperties();
            }
        }
        void linkLabel1_Click(object sender, EventArgs e)
        {
            CleanTemporaryFolders();
        }

        void chkLaythemsouutien_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsoUutien.Enabled = chkLaythemsouutien.Checked;
            lblHelpUutien.Visible = chkLaythemsouutien.Checked;
            if (chkLaythemsouutien.Checked)
            {
                lblHelpUutien.Text = "Nếu bạn nhấn nút Lấy số ưu tiên. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(txtSoKhamUuTien.Text, 0) + nmrLaythemsoUutien.Value).ToString();
                nmrLaythemsoUutien.Focus();
            }
            else
            {
                cmdKhamUuTien.Focus();
            }
        }

        void chkLaythemsothuong_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsothuong.Enabled = chkLaythemsothuong.Checked;
            lblHelpsothuong.Visible = chkLaythemsothuong.Checked;
            if (chkLaythemsothuong.Checked)
            {
                lblHelpsothuong.Text = "Nếu bạn nhấn nút Lấy số thường. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(txtSoKham.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(txtSoKham.Text, 0) + nmrLaythemsothuong.Value).ToString();
                nmrLaythemsothuong.Focus();
            }
            else
            {
                cmdGetSoKham.Focus();
            }
        }

        void chkLaythemsoYC_CheckedChanged(object sender, EventArgs e)
        {
            nmrLaythemsoYC.Enabled = chkLaythemsoYC.Checked;
            lblHelpsoYC.Visible = chkLaythemsoYC.Checked;
            if (chkLaythemsoYC.Checked)
            {
                lblHelpsoYC.Text = "Nếu bạn nhấn nút Lấy số yêu cầu. Hệ thống sẽ tự động in từ số " + (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + 1).ToString() + " đến số " + (Utility.Int32Dbnull(txtSoKhamYeuCau.Text, 0) + nmrLaythemsoYC.Value).ToString();
                nmrLaythemsoYC.Focus();
            }
            else
            {
                cmdKhamYC.Focus();
            }
        }

        void frm_SoKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.LaySokham(0, txtSoKham, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), "DV", 0);
                this.LaySokham(0, txtSoKhamBHYT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), "BHYT", 0);
                this.LaySokham(0, txtSoKhamUuTien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh), "ALL", 1);
                this.LaySokham(0, txtSoKhamYeuCau, Utility.sDbnull(_QMSProperties.MaKhoaYeuCau), "ALL", 0);
            }
            if (e.KeyCode == Keys.F1)
                cmdGetSoKham_Click(cmdGetSoKham, new EventArgs());
            if (e.KeyCode == Keys.F2)
                cmdKhamYC_Click(cmdKhamYC, new EventArgs());
            if (e.KeyCode == Keys.F3)
                cmdKhamUuTien_Click(cmdKhamUuTien, new EventArgs());
        }
        void Try2ExitApp()
        {
            try
            {
                this.Close();
                this.Dispose();
                Application.Exit();
            }
            catch
            {
            }
        }
      

        private void CauHinh()
        {
            Utility.SetMsg(this.lblMessage, _QMSProperties.TenBenhVien, true);
            txtSoKham.BackColor = _QMSProperties.MauB1;
            txtSoKham.ForeColor = _QMSProperties.MauF1;

            txtSoKhamYeuCau.BackColor = _QMSProperties.MauB2;
            txtSoKhamYeuCau.ForeColor = _QMSProperties.MauF2;

            txtSoKhamUuTien.BackColor = _QMSProperties.MauB3;
            txtSoKhamUuTien.ForeColor = _QMSProperties.MauF3;
        }

        private void CleanTemporaryFolders()
        {
            try
            {
                Utility.WaitNow(this);
                string folderName = Environment.ExpandEnvironmentVariables("%TEMP%");
                this.EmptyFolderContents(folderName);
            }
            catch (Exception)
            {
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(_QMSProperties);
            frm.ShowDialog();
            CauHinh();
        }
        private void cmdGetSoKham_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            int soluong = 1;
            if (chkLaythemsothuong.Checked)
                soluong = (int)nmrLaythemsothuong.Value;
            for (int i = 1; i <= soluong; i++)
            {
                this.LaySokham(1,txtSoKham, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh),"DV", 0);
                if (_QMSProperties.PrintStatus)
                {
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdGetSoKham, Utility.sDbnull(txtSoKham.Text), 0);
                }
            }
            Utility.DefaultNow(this);
        }
        private void cmdKhamYC_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
            int soluong = 1;
            if (chkLaythemsoYC.Checked)
                soluong = (int)nmrLaythemsoYC.Value;
            for (int i = 1; i <= soluong; i++)
            {
                this.LaySokham(1,txtSoKhamYeuCau, Utility.sDbnull(_QMSProperties.MaKhoaYeuCau),"ALL", 0);
                if (_QMSProperties.PrintStatus)
                {
                    this.InPhieuKham(_QMSProperties.TenKhoaYeuCau, this.cmdKhamYC, Utility.sDbnull(this.txtSoKhamYeuCau.Text), 0);
                }
            }
            Utility.DefaultNow(this);
        }
        private void cmdKhamUuTien_Click(object sender, EventArgs e)
        {
            Utility.WaitNow(this);
             int soluong = 1;
            if (chkLaythemsouutien.Checked)
                soluong = (int)nmrLaythemsoUutien.Value;
            for (int i = 1; i <= soluong; i++)
            {
                this.LaySokham(1,txtSoKhamUuTien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh),"ALL", 1);
                if (_QMSProperties.PrintStatus)
                {
                    this.InPhieuKham(_QMSProperties.TenKhoaKhamBenh, this.cmdKhamUuTien, Utility.sDbnull(txtSoKhamUuTien.Text), 1);
                }
            }
            Utility.DefaultNow(this);
        }
        private void EmptyFolderContents(string folderName)
        {
            try
            {
                Exception exception;
                foreach (string str in Directory.GetDirectories(folderName))
                {
                    try
                    {
                        Directory.Delete(str, true);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Debug.WriteLine(exception);
                    }
                }
                foreach (string str2 in Directory.GetFiles(folderName))
                {
                    try
                    {
                        File.Delete(str2);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        Debug.WriteLine(exception);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void frm_SoKham_Load(object sender, EventArgs e)
        {
           this.LaySokham(0,txtSoKham, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh),"DV", 0);
           this.LaySokham(0,txtSoKhamUuTien, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh),"ALL", 1);
           this.LaySokham(0, txtSoKhamYeuCau, Utility.sDbnull(_QMSProperties.MaKhoaYeuCau),"ALL", 0);
           this.LaySokham(0, txtSoKhamBHYT, Utility.sDbnull(_QMSProperties.MaKhoaKhamBenh),"BHYT", 0);
           hasLoaded = true;
           
           
        }
        private void InPhieuKham(string tenkhoa, Button button, string sokham, int isUuTien)
        {
            try
            {
                
                string val = "BỆNH NHÂN THƯỜNG";
                if (_QMSProperties.MaKhoaKhamBenh == "KKB")
                {
                    val = (isUuTien == 0) ? "BỆNH NHÂN THƯỜNG" : "BỆNH NHÂN ƯU TIÊN";
                }
                Utility.WaitNow(this);
                UIAction._EnableControl(button, false, "");
                DataTable dataTable = new DataTable();
                Utility.AddColumToDataTable(ref dataTable, "So_Kham", typeof(string));
                DataRow row = dataTable.NewRow();
                row["So_Kham"] = Utility.sDbnull(sokham);
                dataTable.Rows.Add(row);
                CRPT_SOKHAM crpt_sokham = new CRPT_SOKHAM();
                crpt_sokham.SetDataSource(dataTable);
                crpt_sokham.SetParameterValue("TEN_BENH_VIEN", _QMSProperties.TenBenhVien);
                string str2 = NgayIn(DateTime.Now);
                crpt_sokham.SetParameterValue("PrintDate", str2);
                crpt_sokham.SetParameterValue("TenKhoa", Utility.sDbnull(tenkhoa));
                crpt_sokham.SetParameterValue("LoaiDoiTuong", val);
                if (!string.IsNullOrEmpty(_QMSProperties.PrinterName))
                {
                    crpt_sokham.PrintOptions.PrinterName = Utility.sDbnull(_QMSProperties.PrinterName);
                }
                crpt_sokham.PrintToPrinter(1, false, 0, 0);
                UIAction._EnableControl(button, true, "");
                button.Focus();
                this.CleanTemporaryFolders();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi in số khám\n" + ex.Message);
            }
            finally
            {
                UIAction._EnableControl(cmdKhamYC, true,"");
                Utility.DefaultNow(this);
            }
        }

       

        private void LaySokham(int status,Label txt, string MaKhoa,string madoituongkcb, int IsUuTien)
        {
            try
            {
                int num = 0;
                int sttkham = 0;
                StoredProcedure procedure = SPs.QmsTaoso(new int?(status), _QMSProperties.MaQuay, MaKhoa,-1, madoituongkcb, new int?(num),sttkham, _QMSProperties.PrintStatus ? true : false, new int?(IsUuTien),-1,-1,-1);
                procedure.Execute();
               
                num = Utility.Int32Dbnull(procedure.OutputValues[0]);
                string str = Utility.sDbnull(num);
                if (Utility.Int32Dbnull(num) < 10)
                {
                    str = Utility.FormatNumberToString(num, "00");
                }
                txt.Text = Utility.sDbnull(str);
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi thực hiện lấy số thứ tự tiếp đón\n"+ex.Message);
            }
        }

        public static string NgayIn(DateTime dt)
        {
            string str = "Ngày ";
            return ((((((str + Strings.Right("0" + dt.Day.ToString(), 2)) + "/" + Strings.Right("0" + dt.Month.ToString(), 2)) + "/" + dt.Year) + " " + Strings.Right("0" + dt.Hour.ToString(), 2)) + ":" + Strings.Right("0" + dt.Minute.ToString(), 2)) + ":" + Strings.Right("0" + dt.Second.ToString(), 2));
        }

        private void cmdGetSoKham_Click_1(object sender, EventArgs e)
        {

        }

    }
}
