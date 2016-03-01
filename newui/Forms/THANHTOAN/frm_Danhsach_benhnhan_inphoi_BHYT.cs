using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Libs.AppUI;
using VNS.Properties;

namespace  VNS.HIS.UI.THANHTOAN
{
    public partial class frm_Danhsach_benhnhan_inphoi_BHYT : Form
    {
        private DataTable m_dtTimKiem=new DataTable();
        private bool b_Hasloaded = false;
        private string _sLocalPath = Application.StartupPath + "XML";
        public frm_Danhsach_benhnhan_inphoi_BHYT()
        {
            InitializeComponent();
            dtToDate.Value = dtFromDate.Value = globalVariables.SysDate;
            txtMaLanKham.LostFocus+=new EventHandler(txtMaLanKham_LostFocus);
            txtMaLanKham.KeyDown+=new KeyEventHandler(txtMaLanKham_KeyDown);
            Utility.VisiableGridEx(grdList,KcbPhieuDct.Columns.IdPhieuDct,globalVariables.IsAdmin);
            PropertyLib._xmlproperties = PropertyLib.GetXMLProperties();

        }
        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MalanKam = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(MalanKam) && txtMaLanKham.Text.Length < 8)
                {
                    MalanKam = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
                    txtMaLanKham.Text = MalanKam;
                    txtMaLanKham.Select(txtMaLanKham.Text.Length, txtMaLanKham.Text.Length);
                }
                if (!string.IsNullOrEmpty(txtMaLanKham.Text))
                {
                    cmdTimKiem.PerformClick();
                }

            }
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        private void frm_Danhsach_benhnhan_inphoi_BHYT_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            txtMaLanKham.Focus();
            txtMaLanKham.SelectAll();
        }
        private string MalanKam { get; set; }
        private void txtMaLanKham_LostFocus(object sender, EventArgs eventArgs)
        {
            try
            {
                MalanKam = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(MalanKam) && txtMaLanKham.Text.Length < 8)
                {
                    MalanKam = Utility.GetYY(globalVariables.SysDate) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLanKham.Text, 0), "000000");
                    txtMaLanKham.Text = MalanKam;
                }

            }
            catch (Exception)
            {

                // throw;
            }
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void TimKiemThongTin()
        {
            try
            {
                int tinhtrang = -1;
                int trangthai = -1;
                int chuaketthuc = 0;
                if (chkAllTinhTrang.Checked) tinhtrang = -1;
                else
                {
                    tinhtrang = radNgoaiTru.Checked ? 0 : 1;
                }
                if (chkAllTrangThai.Checked) trangthai = -1;
                else
                {
                    trangthai = radDaduyet.Checked ? 1 : 0;
                }
                if (chkChuaKetThuc.Checked) chuaketthuc = 1;
                else
                {
                    chuaketthuc = 0;
                }
                if (!chkByDate.Checked) dtFromDate.Value = Convert.ToDateTime("1900-01-01");
                m_dtTimKiem =
                    SPs.ThanhtoanDanhsachInphoiBhyt(dtFromDate.Value, dtToDate.Value,
                                                    Utility.sDbnull(txtMaLanKham.Text, ""),Utility.Int16Dbnull(trangthai),
                                                    Utility.Int32Dbnull(tinhtrang)
                                                    , Utility.sDbnull(chuaketthuc,0)).GetDataSet().
                        Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtTimKiem, true, true, "1=1", "");
                Utility.SetGridEXSortKey(grdList, KcbPhieuDct.Columns.IdPhieuDct, Janus.Windows.GridEX.SortOrder.Ascending);
                b_Hasloaded = true;
                ModifyCommand();
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
          
        }

        private void ModifyCommand()
        {
            if(b_Hasloaded)
            {
                var query = from daxuatxml in m_dtTimKiem.AsEnumerable()
                            where Utility.Int32Dbnull(daxuatxml["trangthai_xml"]) == 1
                            select daxuatxml;
                var query1 = from chuaxuatxml in m_dtTimKiem.AsEnumerable()
                             where Utility.Int32Dbnull(chuaxuatxml["trangthai_xml"]) == 0
                             select chuaxuatxml;
                Utility.SetMsg(lblDaKetThuc, string.Format("Đã xuất xml: {0} ", query.Count()), true);
                Utility.SetMsg(lblChuaKetThuc, string.Format("Chưa xuất xml: {0}", query1.Count()), true);
            }
           
            cmdXuatExcel.Enabled = grdList.RowCount > 0;
        }

        private void cmdXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                ExcelUtlity.ExportGridEx(grdList);
                //saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                //saveFileDialog1.FileName = string.Format("{0}.xls", "DanhSachInPhoiBHYT");
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    string sPath = saveFileDialog1.FileName;
                //    var fs = new FileStream(sPath, FileMode.Create);
                //    fs.CanWrite.CompareTo(true);
                //    fs.CanRead.CompareTo(true);
                //    gridEXExporter1.Export(fs);
                //    fs.Dispose();
                //}
                //saveFileDialog1.Dispose();
                //saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện việc chưa kết thúc thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkChuaKetThuc_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }

        private void frm_Danhsach_benhnhan_inphoi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)this.Close();
            if(e.KeyCode==Keys.F2)
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }

        }
        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của phần mã lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaLanKham.Text)) chkByDate.Checked = false;
        }

        private void radNgoaiTru_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radNoiTru_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radChuaduyet_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void radDaduyet_CheckedChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }
        
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            if (PropertyLib._xmlproperties != null)
            {
                frm_Properties frm = new frm_Properties(PropertyLib._xmlproperties);
                frm.ShowDialog();
                //CauHinhKCB();
            }
          
        }

        private void cmdExportXML_Click(object sender, EventArgs e)
        {
            try
            {
               // var xmlWriterSettings = new XmlWriterSettings()
               // {
               //     Indent = true,
               //     IndentChars = "\t"
               //     //NewLineOnAttributes = true
               // };
               // xmlWriter = XmlWriter.Create("C:\\abc.xml", xmlWriterSettings);
               //// xmlWriter.WriteProcessingInstruction("xml", string.Format("version={0}1.0{0} encoding={0}UTF-8{0} standalone={0}yes{0}", ((char)34)));
               // xmlWriter.WriteStartElement("CHECKOUT");
               // xmlWriter.WriteStartElement("CHECKOUT1");
               // xmlWriter.WriteStartElement("CHECKOUT2");
               // xmlWriter.WriteElementString("a", "aaa");
               // xmlWriter.WriteElementString("b", "bbb");
               // xmlWriter.WriteElementString("c", "ccc");
               // xmlWriter.WriteElementString("d", "ddd");
               // xmlWriter.WriteEndElement();
               // xmlWriter.Flush();
               // this.UseWaitCursor = true;
                int i = 0;
                Utility.ResetProgressBar(prgBar, grdList.RowCount, true);
                foreach (Janus.Windows.GridEX.GridEXRow row in grdList.GetCheckedRows())
                {
                    string maluot_kham = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                    int id_benhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value);
                    ProcessCreateXML(maluot_kham, id_benhnhan);
                    new Update(KcbPhieuDct.Schema).Set(KcbPhieuDct.Columns.TrangthaiXml).EqualTo(1).Where(
                        KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(id_benhnhan).And(KcbPhieuDct.Columns.MaLuotkham).
                        IsEqualTo(maluot_kham).Execute();
                    i = i + 1;
                    UIAction.SetValue4Prg(prgBar, 1);
                    // row.Cells["trangthai_xml"].Value = 1;
                }
                Utility.SetMsg(lblmsg, string.Format("Tổng số File XML là {0} file", i.ToString()), false);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" +ex.Message);
            }
            finally
            {
                if(xmlWriter !=null)
                {
                    //xmlWriter.WriteEndElement();
                    //xmlWriter.WriteEndDocument();
                    xmlWriter.Close();
                }
              //  this.UseWaitCursor = false;
            }
        }
        private string sLocalFilePath;
        private string sFileNgay;
        private string sFileName;
        private DataSet dtXML = new DataSet();
        private XmlWriter xmlWriter = null;
        private bool ProcessCreateXML(string maluotKham, int idBenhnhan)
        {
            try
            {
              
                if (dtFromDate.Value == dtToDate.Value)
                {
                    sFileNgay = dtFromDate.Value.ToString("ddMMyyyy");
                }
                else
                {
                    sFileNgay = string.Format("{0}-{1}", dtFromDate.Value.ToString("ddMMyyyy"), dtToDate.Value.ToString("ddMMyyyy"));
                }
               
                 dtXML = SPs.ViettelLaythongtinDuyetbaohiem(Utility.sDbnull(maluotKham, ""),
                                                                   Utility.Int32Dbnull(idBenhnhan, -1)).GetDataSet();
                if(dtXML.Tables[0].Rows.Count <=0) return false;
              
                _sLocalPath = Utility.sDbnull(PropertyLib._xmlproperties.Chonduongdan);
                if (!Directory.Exists(_sLocalPath)) Directory.CreateDirectory(_sLocalPath);
                var xmlWriterSettings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "\t",
                    OmitXmlDeclaration = true
                };
                sFileName = Utility.sDbnull(dtXML.Tables[2].Rows[0]["NGAY_VAO"]) + "_" +
                            Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_THE"]) + "_CheckOut.xml";
                string sDirectory = _sLocalPath;
                if (!Directory.Exists(sDirectory)) Directory.CreateDirectory(sDirectory);
                sLocalFilePath = _sLocalPath + "\\" + sFileName;
              
                xmlWriter = XmlWriter.Create(sLocalFilePath, xmlWriterSettings);
                ProcessXMLWrite();
                grdList.UnCheckAllRecords();
              
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                return false;
            }
            finally
            {
                //xmlWriter.WriteEndElement();
                //xmlWriter.WriteEndDocument();
                if(xmlWriter !=null)
                xmlWriter.Close();
            }
        }

        private bool ProcessXMLWrite()
        {
            try
            {
                xmlWriter.WriteProcessingInstruction("xml",string.Format("version={0}1.0{0}", ((char)34)));
                xmlWriter.WriteStartElement("CHECKOUT");
                if(dtXML.Tables[0].Rows.Count>0)
                {
                    xmlWriter.WriteStartElement("THONGTINBENHNHAN");
                    xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(dtXML.Tables[0].Rows[0]["MA_LK"]));
                    xmlWriter.WriteElementString("NGAYGIOVAO", Utility.sDbnull(dtXML.Tables[0].Rows[0]["NGAYGIOVAO"]));
                    xmlWriter.WriteElementString("NGAYGIORA", Utility.sDbnull(dtXML.Tables[0].Rows[0]["NGAYGIORA"]));
                    xmlWriter.WriteElementString("MABENHVIEN", Utility.sDbnull(dtXML.Tables[0].Rows[0]["MABENHVIEN"]));
                    xmlWriter.WriteElementString("CHANDOAN", Utility.sDbnull(dtXML.Tables[0].Rows[0]["CHANDOAN"]));
                    xmlWriter.WriteElementString("TRANGTHAI", Utility.sDbnull(dtXML.Tables[0].Rows[0]["TRANGTHAI"]));
                    xmlWriter.WriteElementString("KETQUA", Utility.sDbnull(dtXML.Tables[0].Rows[0]["KETQUA"]));
                    xmlWriter.WriteElementString("SODIENTHOAI_LH", Utility.sDbnull(dtXML.Tables[0].Rows[0]["SODIENTHOAI_LH"]));
                    xmlWriter.WriteElementString("NGUOILIENHE", Utility.sDbnull(dtXML.Tables[0].Rows[0]["NGUOILIENHE"]));
                    xmlWriter.WriteEndElement();
                }
                if(dtXML.Tables[1].Rows.Count>0)
                {
                    xmlWriter.WriteStartElement("CHUYENTUYEN");
                    xmlWriter.WriteElementString("SOHOSO", Utility.sDbnull(dtXML.Tables[1].Rows[0]["SOHOSO"]));
                    xmlWriter.WriteElementString("SOCHUYENTUYEN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["SOCHUYENTUYEN"]));
                    xmlWriter.WriteElementString("MA_BV_CHUYENDEN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["MA_BV_CHUYENDEN"]));
                    xmlWriter.WriteElementString("MA_BV_KHAMBENH", Utility.sDbnull(dtXML.Tables[1].Rows[0]["MA_BV_KHAMBENH"]));
                    xmlWriter.WriteElementString("TEN_CS_KHAMBENH", Utility.sDbnull(dtXML.Tables[1].Rows[0]["TEN_CS_KHAMBENH"]));
                    xmlWriter.WriteElementString("NGHENGHIEP", Utility.sDbnull(dtXML.Tables[1].Rows[0]["NGHENGHIEP"]));
                    xmlWriter.WriteElementString("NOILAMVIEC", Utility.sDbnull(dtXML.Tables[1].Rows[0]["NOILAMVIEC"]));
                    xmlWriter.WriteElementString("LAMSANG", Utility.sDbnull(dtXML.Tables[1].Rows[0]["LAMSANG"]));
                    xmlWriter.WriteElementString("KETQUAXETNGHIEM", Utility.sDbnull(dtXML.Tables[1].Rows[0]["KETQUAXETNGHIEM"]));
                    xmlWriter.WriteElementString("CHANDOAN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["CHANDOAN"]));
                    xmlWriter.WriteElementString("PHUONGPHAPDIEUTRI", Utility.sDbnull(dtXML.Tables[1].Rows[0]["PHUONGPHAPDIEUTRI"]));
                    xmlWriter.WriteElementString("LYDO_CHUYENTUYEN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["LYDO_CHUYENTUYEN"]));
                    xmlWriter.WriteElementString("HUONGDIEUTRI", Utility.sDbnull(dtXML.Tables[1].Rows[0]["HUONGDIEUTRI"]));
                    xmlWriter.WriteElementString("THOIGIAN_CHUYEN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["THOIGIAN_CHUYEN"]));
                    xmlWriter.WriteElementString("PHUONGTIEN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["PHUONGTIEN"]));
                    xmlWriter.WriteElementString("NGUOI_HOTONG", Utility.sDbnull(dtXML.Tables[1].Rows[0]["NGUOI_HOTONG"]));
                    xmlWriter.WriteElementString("MA_QUOCTICH", Utility.sDbnull(dtXML.Tables[1].Rows[0]["MA_QUOCTICH"]));
                    xmlWriter.WriteElementString("MA_DANTOC", Utility.sDbnull(dtXML.Tables[1].Rows[0]["MA_DANTOC"]));
                    xmlWriter.WriteStartElement("DSCHUYENVIEN");
                    xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(dtXML.Tables[1].Rows[0]["MA_LK"]));
                    xmlWriter.WriteElementString("MABV", Utility.sDbnull(dtXML.Tables[1].Rows[0]["MABV"]));
                    xmlWriter.WriteElementString("TUYEN", Utility.sDbnull(dtXML.Tables[1].Rows[0]["TUYEN"]));
                    xmlWriter.WriteElementString("TUNGAY", Utility.sDbnull(dtXML.Tables[1].Rows[0]["TUNGAY"]));
                    xmlWriter.WriteElementString("DENNGAY", Utility.sDbnull(dtXML.Tables[1].Rows[0]["DENNGAY"]));
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }

                if(dtXML.Tables[2].Rows.Count>0)
                {
                    xmlWriter.WriteStartElement("THONGTINCHITIET");

                    xmlWriter.WriteStartElement("TONGHOP");
                    xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_LK"]));
                    xmlWriter.WriteElementString("STT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["STT"]));
                    xmlWriter.WriteElementString("MA_BN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_BN"]));
                    xmlWriter.WriteElementString("HO_TEN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["HO_TEN"]));
                    xmlWriter.WriteElementString("NGAY_SINH", Utility.sDbnull(dtXML.Tables[2].Rows[0]["NGAY_SINH"]));
                    xmlWriter.WriteElementString("GIOI_TINH", Utility.sDbnull(dtXML.Tables[2].Rows[0]["GIOI_TINH"]));
                    xmlWriter.WriteElementString("DIA_CHI", Utility.sDbnull(dtXML.Tables[2].Rows[0]["DIA_CHI"]));
                    xmlWriter.WriteElementString("MA_THE", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_THE"]));
                    xmlWriter.WriteElementString("MA_DKBD", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_DKBD"]));
                    xmlWriter.WriteElementString("GT_THE_TU", Utility.sDbnull(dtXML.Tables[2].Rows[0]["GT_THE_TU"]));
                    xmlWriter.WriteElementString("GT_THE_DEN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["GT_THE_DEN"]));
                    xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_BENH"]));
                    xmlWriter.WriteElementString("MA_BENHKHAC", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_BENHKHAC"]));
                    xmlWriter.WriteElementString("TEN_BENH", Utility.sDbnull(dtXML.Tables[2].Rows[0]["TEN_BENH"]));
                    xmlWriter.WriteElementString("MA_LYDO_VVIEN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_LYDO_VVIEN"]));
                    xmlWriter.WriteElementString("MA_NOI_CHUYEN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_NOI_CHUYEN"]));
                    xmlWriter.WriteElementString("MA_TAI_NAN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_TAI_NAN"]));
                    xmlWriter.WriteElementString("NGAY_VAO", Utility.sDbnull(dtXML.Tables[2].Rows[0]["NGAY_VAO"]));
                    xmlWriter.WriteElementString("NGAY_RA", Utility.sDbnull(dtXML.Tables[2].Rows[0]["NGAY_RA"]));
                    xmlWriter.WriteElementString("SO_NGAY_DTRI", Utility.sDbnull(dtXML.Tables[2].Rows[0]["SO_NGAY_DTRI"]));
                    xmlWriter.WriteElementString("KET_QUA_DTRI", Utility.sDbnull(dtXML.Tables[2].Rows[0]["KET_QUA_DTRI"]));
                    xmlWriter.WriteElementString("TINH_TRANG_RV", Utility.sDbnull(dtXML.Tables[2].Rows[0]["TINH_TRANG_RV"]));
                    xmlWriter.WriteElementString("NGAY_TTOAN", Utility.sDbnull(dtXML.Tables[2].Rows[0]["NGAY_TTOAN"]));
                    xmlWriter.WriteElementString("MUC_HUONG", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MUC_HUONG"]));
                    xmlWriter.WriteElementString("T_THUOC", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_THUOC"]));
                    xmlWriter.WriteElementString("T_VTYT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_VTYT"]));
                    xmlWriter.WriteElementString("T_TONGCHI", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_TONGCHI"]));
                    xmlWriter.WriteElementString("T_BNTT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_BNTT"]));
                    xmlWriter.WriteElementString("T_BHTT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_BHTT"]));
                    xmlWriter.WriteElementString("T_NGUONKHAC", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_NGUONKHAC"]));
                    xmlWriter.WriteElementString("T_NGOAIDS", Utility.sDbnull(dtXML.Tables[2].Rows[0]["T_NGOAIDS"]));
                    xmlWriter.WriteElementString("NAM_QT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["NAM_QT"]));
                    xmlWriter.WriteElementString("THANG_QT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["THANG_QT"]));
                    xmlWriter.WriteElementString("MA_LOAIKCB", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_LOAIKCB"]));
                    xmlWriter.WriteElementString("MA_CSKCB", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_CSKCB"]));
                    xmlWriter.WriteElementString("MA_KHUVUC", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_KHUVUC"],"_"));
                    xmlWriter.WriteElementString("MA_PTTT_QT", Utility.sDbnull(dtXML.Tables[2].Rows[0]["MA_PTTT_QT"],"_"));
                    xmlWriter.WriteElementString("SO_PHIEU", Utility.sDbnull(dtXML.Tables[2].Rows[0]["SO_PHIEU"]));
                    xmlWriter.WriteEndElement();
                   
                    if(dtXML.Tables[3].Rows.Count>0)
                    {
                        xmlWriter.WriteStartElement("BANG_CTTHUOC");
                        foreach (DataRow row in dtXML.Tables[3].Rows)
                        {
                            xmlWriter.WriteStartElement("CTTHUOC");
                            xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            xmlWriter.WriteElementString("MA_THUOC", Utility.sDbnull(row["MA_THUOC"]));
                            xmlWriter.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            xmlWriter.WriteElementString("TEN_THUOC", Utility.sDbnull(row["TEN_THUOC"]));
                            xmlWriter.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            xmlWriter.WriteElementString("HAM_LUONG", Utility.sDbnull(row["HAM_LUONG"]));
                            xmlWriter.WriteElementString("DUONG_DUNG", Utility.sDbnull(row["DUONG_DUNG"]));
                            xmlWriter.WriteElementString("SO_DANG_KY", Utility.sDbnull(row["SO_DANG_KY"]));
                            xmlWriter.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            xmlWriter.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            xmlWriter.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            xmlWriter.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            xmlWriter.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            xmlWriter.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            xmlWriter.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));
                           
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    if (dtXML.Tables[4].Rows.Count > 0)
                    {
                        xmlWriter.WriteStartElement("BANG_CTDV");
                        foreach (DataRow row in dtXML.Tables[4].Rows)
                        {
                            xmlWriter.WriteStartElement("CTDV");
                            xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            xmlWriter.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            xmlWriter.WriteElementString("MA_VAT_TU", Utility.sDbnull(row["MA_VAT_TU"]));
                            xmlWriter.WriteElementString("MA_NHOM", Utility.sDbnull(row["MA_NHOM"]));
                            xmlWriter.WriteElementString("TEN_DICH_VU", Utility.sDbnull(row["TEN_DICH_VU"]));
                            xmlWriter.WriteElementString("DON_VI_TINH", Utility.sDbnull(row["DON_VI_TINH"]));
                            xmlWriter.WriteElementString("SO_LUONG", Utility.sDbnull(row["SO_LUONG"]));
                            xmlWriter.WriteElementString("DON_GIA", Utility.sDbnull(row["DON_GIA"]));
                            xmlWriter.WriteElementString("TYLE_TT", Utility.sDbnull(row["TYLE_TT"]));
                            xmlWriter.WriteElementString("THANH_TIEN", Utility.sDbnull(row["THANH_TIEN"]));
                            xmlWriter.WriteElementString("MA_KHOA", Utility.sDbnull(row["MA_KHOA"]));
                            xmlWriter.WriteElementString("MA_BAC_SI", Utility.sDbnull(row["MA_BAC_SI"]));
                            xmlWriter.WriteElementString("MA_BENH", Utility.sDbnull(row["MA_BENH"]));
                            xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            xmlWriter.WriteElementString("NGAY_KQ", Utility.sDbnull(row["NGAY_KQ"]));
                            xmlWriter.WriteElementString("TEN_KHOABV", Utility.sDbnull(row["TEN_KHOABV"]));
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    if (dtXML.Tables[5].Rows.Count > 0)
                    {
                        xmlWriter.WriteStartElement("BANG_CT_CLS");
                        foreach (DataRow row in dtXML.Tables[5].Rows)
                        {
                            xmlWriter.WriteStartElement("CLS");
                            xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            xmlWriter.WriteElementString("MA_DICH_VU", Utility.sDbnull(row["MA_DICH_VU"]));
                            xmlWriter.WriteElementString("MA_CHI_SO", Utility.sDbnull(row["MA_CHI_SO"]));
                            xmlWriter.WriteElementString("TEN_CHI_SO", Utility.sDbnull(row["TEN_CHI_SO"]));
                            xmlWriter.WriteElementString("GIA_TRI", Utility.sDbnull(row["GIA_TRI"]));
                            xmlWriter.WriteElementString("MA_MAY", Utility.sDbnull(row["MA_MAY"]));
                            xmlWriter.WriteElementString("MO_TA", Utility.sDbnull(row["MO_TA"]));
                            xmlWriter.WriteElementString("KET_LUAN", Utility.sDbnull(row["KET_LUAN"]));
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    if(dtXML.Tables[6].Rows.Count > 0)
                    {
                        xmlWriter.WriteStartElement("BANG_DIENBIENBENH");
                        foreach (DataRow row in dtXML.Tables[6].Rows)
                        {
                            xmlWriter.WriteStartElement("DIENBIENBENH");
                            xmlWriter.WriteElementString("MA_LK", Utility.sDbnull(row["MA_LK"]));
                            xmlWriter.WriteElementString("STT", Utility.sDbnull(row["STT"]));
                            xmlWriter.WriteElementString("DIENBIEN", Utility.sDbnull(row["DIENBIEN"]));
                            xmlWriter.WriteElementString("HOI_CHAN", Utility.sDbnull(row["HOI_CHAN"]));
                            xmlWriter.WriteElementString("PHAU_THUAT", Utility.sDbnull(row["PHAU_THUAT"]));
                            xmlWriter.WriteElementString("NGAY_YL", Utility.sDbnull(row["NGAY_YL"]));
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteFullEndElement();
                }
                xmlWriter.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
          

        }
    }
}
