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
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Nhapvien : Form
    {
        public KcbLuotkham objLuotkham;
        public int id_kham;
        public bool b_Cancel = false;
        public frm_Nhapvien()
        {
            InitializeComponent();
            txtGhiChu.LostFocus+=new EventHandler(txtGhiChu_LostFocus);
            this.KeyDown += new KeyEventHandler(frm_Nhapvien_KeyDown);
            dtNgayNhapVien.Value = globalVariables.SysDate;
            chkThoatngaysaukhinhapvien.CheckedChanged += new EventHandler(chkThoatngaysaukhinhapvien_CheckedChanged);
            if (globalVariables.gv_TuSinhSo_BA)
            {
                cmdTuSinh.Enabled = false;
                txtSoBenhAn.ReadOnly = true;
            }
        }

        void chkThoatngaysaukhinhapvien_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.ThoatNgaysaukhiNhapvienthanhcong = chkThoatngaysaukhinhapvien.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }

        void frm_Nhapvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdAccept_Click(cmdAccept, new EventArgs());
            if (e.Control && e.KeyCode == Keys.D) cmdHUY_VAO_VIEN_Click(cmdHUY_VAO_VIEN, new EventArgs());
        }
        private void txtGhiChu_LostFocus(object sender, EventArgs e)
        {
            //if (objLuotkham != null && Utility.Int32Dbnull( objLuotkham.IdKhoanoitru,-1)!=-1)
            //{
            //    new Update(KcbLuotkham.Schema)
            //  .Set(KcbLuotkham.Columns.MotaNhapvien).EqualTo(txtGhiChu.Text)
            //  .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
            //  .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
            //  .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //  .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
            //}
          
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPHIEU_VAOVIEN_Click(object sender, EventArgs e)
        {
            
            IN_PHIEU_KHAM_VAO_VIEN();
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoBenhAn.Text = Utility.sDbnull(THU_VIEN_CHUNG.LaySoBenhAn());
        }
        /// <summary>
        /// hàm thực hiện việc lấy thôn gtin của dữ liệu
        /// </summary>
        private void getData()
        {
           objLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
           if (objLuotkham != null)
           {
               txtKhoanoitru.SetId(objLuotkham.IdKhoanoitru);
               if (!string.IsNullOrEmpty(objLuotkham.NgayNhapvien.ToString()))
               {
                   dtNgayNhapVien.Value = Convert.ToDateTime(objLuotkham.NgayNhapvien);
               }
               else
               {
                   dtNgayNhapVien.Value = globalVariables.SysDate;
               }
               txtGhiChu.Text = Utility.sDbnull(objLuotkham.MotaNhapvien);
               txtSoBenhAn.Text = objLuotkham.SoBenhAn;
           }
           else
           {
           }
        }
        private void ModifyCommand()
        {
            try
            {
                cmdPHIEU_VAOVIEN.Enabled = Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, -1) > 0;
               // cmdAccept.Enabled =Utility.Int32Dbnull( objLuotkham.TrangthaiNoitru ,-1)<= 0;
                cmdHUY_VAO_VIEN.Enabled = Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, -1) >= 1;

                cmdAccept.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                cmdAccept.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
            }
            catch (Exception)
            {
            }

        }
        private bool InValiHuyVaoVien()
        {
            Utility.SetMsg(lblMsg, "", false);
            KcbLuotkham objKcbLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru <=0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân phải là bệnh nhân nội trú,Bạn mới có thể thực hiện việc hủy nội trú", true);
                return false;
            }
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru >1)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã có dữ liệu nội trú nên không thể hủy nhập viện được. Mời bạn kiểm tra lại", true);
                return false;
            }
           
            NoitruPhanbuonggiuongCollection objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
               .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
               .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể hủy thông tin nhập viện", true);
                return false;
            }
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 && Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã phân buồng giường nên bạn không thể hủy thông tin nhập viện ", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            
            
            NoitruTamung _NoitruTamung = new Select().From(NoitruTamung.Schema)
              .Where(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .And(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<NoitruTamung>();
            if (_NoitruTamung != null)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân này đã đóng tiền tạm ứng , Bạn không thể hủy nhập viện", true);
                return false;
            }
            return true;
        }
        private void cmdHUY_VAO_VIEN_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!InValiHuyVaoVien()) return;
                if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc hủy nhập viện cho bệnh nhân nội trú không,\n Nếu hủy thì bệnh nhân sẽ trở lại ngoại trú,Thông tin gói dịch vụ nội trú sẽ bị xóa theo ?", "Thông báo", true))
                {
                    if (objLuotkham != null)
                    {
                        ActionResult actionResult =
                      new noitru_nhapvien().Huynhapvien(
                     objLuotkham);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                objLuotkham.TrangthaiNoitru = 0;
                                objLuotkham.SoBenhAn = string.Empty;
                                objLuotkham.NgayNhapvien = null;
                                objLuotkham.IdKhoanoitru = -1;
                                objLuotkham.MotaNhapvien = "";
                                objLuotkham.IdNhapvien = -1;
                                txtSoBenhAn.Clear();
                                ModifyCommand();
                                Utility.SetMsg(lblMsg, "Bạn hủy nội trú cho bệnh nhân thành công", false);
                                break;
                            case ActionResult.Error:
                                Utility.SetMsg(lblMsg, "Lỗi trong quá trình hủy nội trú cho bệnh nhân", true);
                                break;
                        }
                    }
                }
            }
            catch
            { }
            finally
            {
                ModifyCommand();
            }
        }

        private void AutocompleteKhoanoitru()
        {
            DataTable m_dtKhoaNoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            try
            {
                if (m_dtKhoaNoitru == null) return;
                if (!m_dtKhoaNoitru.Columns.Contains("ShortCut"))
                    m_dtKhoaNoitru.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtKhoaNoitru.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucKhoaphong.Columns.TenKhoaphong].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucKhoaphong.Columns.TenKhoaphong].ToString().Trim());
                    shortcut = dr[DmucKhoaphong.Columns.MaKhoaphong].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
                var source = new List<string>();
                var query = from p in m_dtKhoaNoitru.AsEnumerable()
                            select p[DmucKhoaphong.Columns.IdKhoaphong].ToString() + "#" + p[DmucKhoaphong.Columns.MaKhoaphong].ToString() + "@" + p[DmucKhoaphong.Columns.TenKhoaphong].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtKhoanoitru.AutoCompleteList = source;
                this.txtKhoanoitru.TextAlign = HorizontalAlignment.Left;
                this.txtKhoanoitru.CaseSensitive = false;
                this.txtKhoanoitru.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {


            }
        }
        private void frm_Nhapvien_Load(object sender, EventArgs e)
        {
            AutocompleteKhoanoitru();
            getData();
            ModifyCommand();
            chkInNgaySauKhiNhapVien.Checked = PropertyLib._NoitruProperties.InphieuNhapvienNgaysaukhiNhapvien;
            chkThoatngaysaukhinhapvien.Checked = PropertyLib._NoitruProperties.ThoatNgaysaukhiNhapvienthanhcong;
            txtKhoanoitru.Focus();

        }
        private NoitruPhanbuonggiuong TaoBuonggiuong()
        {
            NoitruPhanbuonggiuong objPatientDept = new NoitruPhanbuonggiuong();
            objPatientDept.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objPatientDept.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
            objPatientDept.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
            objPatientDept.NgayTao = globalVariables.SysDate;
            objPatientDept.NgayVaokhoa = dtNgayNhapVien.Value;
            objPatientDept.IdKham = id_kham;
            objPatientDept.NguoiTao = globalVariables.UserName;
            objPatientDept.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
            objPatientDept.NoiTru = 1;
            objPatientDept.TrangthaiThanhtoan = 0;
            objPatientDept.TrangThai = 0;
            objPatientDept.DuyetBhyt = 0;
            objPatientDept.CachtinhSoluong = 0;
            objPatientDept.SoluongGio = 0;
            return objPatientDept;
          

        }
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            Utility.ResetMessageError(errorProvider1);
            if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn khoa nội trú", true);
                txtKhoanoitru.Focus();
                return false;
            }
            NoitruPhanbuonggiuongCollection objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
               .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
               .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể cập nhật lại thông tin nhập viện", true);
                return false;
            }
            SqlQuery sqlQuery2 = new Select().From(NoitruPhieudieutri.Schema)
              .Where(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
            if (sqlQuery2.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã có phiếu điều trị nên bạn không thể cập nhật lại thông tin nhập viện", true);
                return false;
            }
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 && Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã phân buồng giường nên bạn không thể cập nhật lại thông tin nhập viện", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            objLuotkham = new Select().From(KcbLuotkham.Schema)
              .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
              .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbLuotkham>();

            if (objLuotkham != null && objLuotkham.TrangthaiNoitru > 1)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân đã có dữ liệu nội trú nên bạn không thể cập nhật lại thông tin nhập viện. Mời bạn kiểm tra lại ", true);
                return false;
            }
           
            return true;
        }
        /// <summary>
        /// hàm thuwchj hiện việc nhập viện không có gói, gói sẽ là nulll cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
            bool Question = true;
            if (!isValidData())return;
            if (string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoBenhAn, "")))
            {
                txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
            }
            else
            {
                txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
            }
            //if (Utility.AcceptQuestion("Bạn có muốn nhập viện cho bệnh nhân này không","Thông báo nhập viện",true))
            //{
                objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBenhAn.Text);
                objLuotkham.MotaNhapvien =Utility.DoTrim( txtGhiChu.Text);
                ActionResult actionResult = new noitru_nhapvien().Nhapvien(TaoBuonggiuong(), objLuotkham, null);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
                        objLuotkham.IdKhoanoitru =Utility.Int16Dbnull( txtKhoanoitru.MyID,-1);
                        objLuotkham.TrangthaiNoitru = 1;
                        objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBenhAn.Text);
                        objLuotkham.NgayNhapvien = dtNgayNhapVien.Value;
                        objLuotkham.MotaNhapvien =Utility.DoTrim( txtGhiChu.Text);

                        b_Cancel = true;
                        Utility.SetMsg(lblMsg, Utility.sDbnull(cmdAccept.Tag, "0") == "0" ? "Bạn thực hiện nhập viện cho bệnh nhân thành công" : "Bạn thực hiện cập nhật khoa nội trú cho bệnh nhân thành công", false);
                       // this.Close();
                        if (chkInNgaySauKhiNhapVien.Checked)
                        {
                            IN_PHIEU_KHAM_VAO_VIEN();
                        }
                        if (chkThoatngaysaukhinhapvien.Checked) this.Close();
                        break;
                    case ActionResult.Error:
                        Utility.SetMsg(lblMsg, "Lỗi trong quá trình nhập viện ", true);
                        break;
                    case ActionResult.ExistedRecord:
                        Utility.SetMsg(lblMsg, "Bệnh nhân đã thanh toán, Mời bạn xem lại ", true);
                        break;

                }
            //}
               
            }
            catch
            { }
            finally
            {
                ModifyCommand();
            }
          
           
          
        }

        private  void IN_PHIEU_KHAM_VAO_VIEN()
        {
            DataTable dsTable =
               new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            if (dsTable.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào\n Mời bạn xem lại", "Thông báo",MessageBoxIcon.Error);
                return;
            }

            SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).OrderAsc(
                    KcbChandoanKetluan.Columns.NgayChandoan);
            var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
            string chandoan = "";
            string mabenh = "";
            string phongkhamvaovien = "";
            string khoanoitru = "";
            foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
            {
                string ICD_Name = "";
                string ICD_Code = "";
                GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                            Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                ? ICD_Name
                                : Utility.sDbnull(objDiagInfo.Chandoan);
                mabenh += ICD_Code;
            }
            
            //txtkbMa.Text = Utility.sDbnull(mabenh);

            DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
            DataTable dtThuoc = ds.Tables[0];
            DataTable dtketqua = ds.Tables[1];

            string[] query = (from thuoc in dtThuoc.AsEnumerable()
                              let y = Utility.sDbnull(thuoc["ten_thuoc"])
                              select y).ToArray();
            string donthuoc = string.Join(";", query);
            string[] querykq = (from kq in dtketqua.AsEnumerable()
                                let y = Utility.sDbnull(kq["ketqua"])
                                select y).ToArray();
            string ketquaCLS = string.Join("; ", querykq);
       

           

            //foreach (DataRow dr in dsTable.Rows)
            //{
            DataRow dr = dsTable.Rows[0];
            if (dr != null)
            {
                dr["thuockedon"] = donthuoc;
                dr["CHANDOAN_VAOVIEN"] = chandoan;
                dr["KETQUA_CLS"] = ketquaCLS;
            }
            
            dsTable.AcceptChanges();
            VNS.HIS.UI.Baocao.noitru_baocao.Inphieunhapvien(dsTable, "PHIẾU NHẬP VIỆN", globalVariables.SysDate);
        }
        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }

        private void chkInNgaySauKhiNhapVien_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.InphieuNhapvienNgaysaukhiNhapvien = chkInNgaySauKhiNhapVien.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }
    }
}
