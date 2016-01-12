using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UI.Baocao;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_thethuoc : Form
    {
        private readonly string KieuKho = "ALL"; //ALL: Bốc cả kho thuốc chẵn lẻ; CHAN=BỐC KHO CHẴN;LE=BỐC KHO LẺ
        private HisDuocProperties HisDuocProperties;
        private string KIEU_THUOC_VT = "THUOC";
        private TDmucKho _item;
        private bool allowChanged;
        private DataTable m_dtDrugData = new DataTable();

        public frm_thethuoc(string args)
        {
            InitializeComponent();
            KieuKho = args.Split('-')[0];
            KIEU_THUOC_VT = args.Split('-')[1];

            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            cmdExit.Click += cmdExit_Click;
            Load += frm_thethuoc_Load;
            cmdBaoCao.Click += cmdBaoCao_Click;
            KeyDown += frm_thethuoc_KeyDown;
            cboKho.SelectedIndexChanged += cboKho_SelectedIndexChanged;
            gridEXExporter1.GridEX = grdListKhoChan;
            chkThekhochitiet.CheckedChanged += chkThekhochitiet_CheckedChanged;
            chkChanle.CheckedChanged += chkChanle_CheckedChanged;
            chkSimple.CheckedChanged += chkSimple_CheckedChanged;
            CauHinh();
        }

        private void chkSimple_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        private void chkChanle_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        private void chkThekhochitiet_CheckedChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            if (_item == null)
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(cboKho.SelectedValue)).ExecuteSingle<TDmucKho>();
            GetKieuThuocVT();
        }

        private void txtLoaithuoc__OnEnterMe()
        {
        }


        private void txtLoaithuoc__OnSelectionChanged()
        {
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            SelectStock();
            modifyTieude();
        }

        private void SelectStock()
        {
            if (Utility.Int32Dbnull(cboKho.SelectedValue, -1) < 0)
                _item = null;
            else
            {
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(cboKho.SelectedValue)).ExecuteSingle<TDmucKho>();
                GetKieuThuocVT();
                BindThuocVT();
            }
        }

        private void BindThuocVT()
        {
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
        }

        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc =
                SPs.ThuocLayDanhmucLoaiThuocTheokho(Utility.Int32Dbnull(cboKho.SelectedValue, -1)).GetDataSet().Tables[0
                    ];
            txtLoaithuoc.Init(dtLoaithuoc,
                              new List<string>
                                  {
                                      DmucLoaithuoc.Columns.IdLoaithuoc,
                                      DmucLoaithuoc.Columns.MaLoaithuoc,
                                      DmucLoaithuoc.Columns.TenLoaithuoc
                                  });
        }

        private void AutocompleteThuoc()
        {
            try
            {
                DataTable _dataThuoc =
                    SPs.ThuocLayDanhmucThuocTheokho(Utility.Int32Dbnull(cboKho.SelectedValue, -1)).GetDataSet().Tables[0
                        ];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }

        private void modifyTieude()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                if (chkSimple.Checked)
                {
                    baocaO_TIEUDE1.Init("thuoc_thethuoc");
                    grdThethuoc.BringToFront();
                }
                else
                {
                    if (chkThekhochitiet.Checked)
                    {
                        baocaO_TIEUDE1.Init("thuoc_thethuoc_chitiet");
                        grdListChitiet.BringToFront();
                    }
                    else
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                        {
                            baocaO_TIEUDE1.Init("thuoc_thethuoc_tutruc");
                            grdThethuoctutruc.BringToFront();
                        }
                        else if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                        {
                            baocaO_TIEUDE1.Init("thuoc_thethuoc_khochan");
                            grdListKhoChan.BringToFront();
                        }
                        else
                        {
                            baocaO_TIEUDE1.Init("thuoc_thethuoc_khole");
                            grdListKhole.BringToFront();
                        }
                    }
                }
            }
            else //VTTH
            {
                if (chkSimple.Checked)
                {
                    baocaO_TIEUDE1.Init("vt_thevt");
                    grdThethuoc.BringToFront();
                }
                else
                {
                    if (chkThekhochitiet.Checked)
                    {
                        baocaO_TIEUDE1.Init("vt_thevt_chitiet");
                        grdListChitiet.BringToFront();
                    }
                    else
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                        {
                            baocaO_TIEUDE1.Init("thuoc_thevt_tutruc");
                            grdThethuoctutruc.BringToFront();
                        }
                        else if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                        {
                            baocaO_TIEUDE1.Init("vt_thevt_khochan");
                            grdListKhoChan.BringToFront();
                        }
                        else
                        {
                            baocaO_TIEUDE1.Init("vt_thevt_khole");
                            grdListKhole.BringToFront();
                        }
                    }
                }
            }
        }

        private void GetKieuThuocVT()
        {
            try
            {
                if (_item != null)
                {
                    KIEU_THUOC_VT = _item.KhoThuocVt;

                    lblLoaikho.Text = _item.KieuKho == "CHAN"
                                          ? "Kho chẵn"
                                          : (_item.KieuKho == "LE" ? "Kho lẻ" : "Kho chẵn lẻ");
                    chkChanle.Enabled = _item.KieuKho == "CHANLE";
                    modifyTieude();
                }
                else
                {
                    lblLoaikho.Text = "Không xác định";
                    KIEU_THUOC_VT = "ALL";
                }
            }
            catch
            {
                KIEU_THUOC_VT = "ALL";
            }
        }

        private void CauHinh()
        {
            HisDuocProperties = PropertyLib._HisDuocProperties;
        }

        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// load thông tin 
        /// của form hiện tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_thethuoc_Load(object sender, EventArgs e)
        {
            if (KIEU_THUOC_VT == "THUOC")
                baocaO_TIEUDE1.Init("thuoc_thethuoc");
            else
                baocaO_TIEUDE1.Init("vt_thevt");

            DataBinding.BindData(cboKho,
                                 KieuKho == "ALL"
                                     ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA()
                                     : (KieuKho == "CHAN"
                                            ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN()
                                            : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()), TDmucKho.Columns.IdKho,
                                 TDmucKho.Columns.TenKho);
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;
            cboKho_SelectedIndexChanged(cboKho, e);
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
        }

        

        /// <summary>
        /// hàm thực hiện in phiếu báo cáo 
        /// thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string nhomthuoc = "-1";
                if (cboKho.SelectedIndex < 0)
                {
                    Utility.ShowMsg("Bạn phải chọn Kho thuốc để xem thẻ thuốc");
                    cboKho.Focus();
                    return;
                }
                if (txtthuoc.MyCode == "-1")
                {
                    Utility.ShowMsg("Bạn phải chọn thuốc để xem thẻ thuốc");
                    txtthuoc.Focus();
                    return;
                }
                nhomthuoc = txtLoaithuoc.MyID.ToString();
                DataTable m_dtReport = null;
                string fromdate = "01/01/1900";
                string todate = "01/01/1900";
                string _value = "1";
                if (optThang.Checked)
                {
                    if (cboThang.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Tháng báo cáo");
                        cboThang.Focus();
                        return;
                    }
                    _value = cboThang.SelectedValue.ToString();
                    switch (_value)
                    {
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 29).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                        case "6":
                        case "9":
                        case "11":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 30).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optQuy.Checked)
                {
                    if (cboQuy.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Quý báo cáo");
                        cboQuy.Focus();
                        return;
                    }
                    _value = cboQuy.SelectedValue.ToString();
                    switch (_value)
                    {
                        case "1":
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                            break;
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, 4, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 6, 30).ToString("dd/MM/yyyy");
                            break;
                        case "3":
                            fromdate = new DateTime(dtpNam.Value.Year, 7, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 9, 30).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                            fromdate = new DateTime(dtpNam.Value.Year, 10, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optNam.Checked)
                {
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                    todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                }
                else
                {
                    fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                    todate = dtToDate.Value.ToString("dd/MM/yyyy");
                }
                if (chkSimple.Checked)
                {
                    m_dtReport =
                        BAOCAO_THUOC.ThuocThethuoc(fromdate, todate,
                                                   Utility.Int32Dbnull(cboKho.SelectedValue),
                                                   Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc,
                                                   chkBiendong.Checked ? 1 : 0);
                }
                else
                {
                    if (chkThekhochitiet.Checked )
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                            m_dtReport =
                                BAOCAO_THUOC.ThuocThethuocTutrucChitiet(fromdate, todate,
                                    Utility.Int32Dbnull(cboKho.SelectedValue), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                                    nhomthuoc, chkBiendong.Checked ? 1 : 0);
                        else
                            m_dtReport =
                                BAOCAO_THUOC.ThuocThethuocChitiet(fromdate, todate,
                                    Utility.Int32Dbnull(cboKho.SelectedValue), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                                    nhomthuoc, chkBiendong.Checked ? 1 : 0);
                    }
                    else
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                            m_dtReport =
                                BAOCAO_THUOC.ThuocThethuocTutruc(fromdate, todate,
                                    Utility.Int32Dbnull(cboKho.SelectedValue), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                                    nhomthuoc, chkBiendong.Checked ? 1 : 0);
                        else

                        {
                            if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                                m_dtReport =
                                    BAOCAO_THUOC.ThuocThethuocKhochan(fromdate, todate,
                                        Utility.Int32Dbnull(cboKho.SelectedValue),
                                        Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc, chkBiendong.Checked ? 1 : 0);
                            else
                                m_dtReport =
                                    BAOCAO_THUOC.ThuocThethuocKhole(fromdate, todate,
                                        Utility.Int32Dbnull(cboKho.SelectedValue),
                                        Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc, chkBiendong.Checked ? 1 : 0);
                        }
                    }
                }
                if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                    THU_VIEN_CHUNG.CreateXML(m_dtReport,
                                             chkThekhochitiet.Checked
                                                 ? "ThuocThethuocTutrucChitiet.xml"
                                                 : "ThuocThethuocTutruc.xml");
                else
                    THU_VIEN_CHUNG.CreateXML(m_dtReport,
                                             chkThekhochitiet.Checked
                                                 ? "thethuoc_chitiet.xml"
                                                 : (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked)
                                                        ? "Thethuoc_khochan.xml"
                                                        : "Thethuoc_khole.xml"));
                Utility.SetDataSourceForDataGridEx(
                    chkSimple.Checked
                        ? grdThethuoc
                        : (
                              chkThekhochitiet.Checked
                                  ? grdListChitiet
                                  : (
                                        _item != null && Utility.Byte2Bool(_item.LaTuthuoc)
                                            ? grdThethuoctutruc
                                            : (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked)
                                                   ? grdListKhoChan
                                                   : grdListKhole)
                                    )
                          )
                    , m_dtReport, true, true, "1=1", "");
                if (m_dtReport==null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                if (chkSimple.Checked)
                {
                    ProcessDataThethuoc(ref m_dtReport);
                    thuoc_baocao.Thethuoc(m_dtReport, KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                          dtNgayIn.Value, FromDateToDate,
                                          Utility.sDbnull(cboKho.Text));
                }
                else
                {
                    if (chkThekhochitiet.Checked)
                    {
                        ProcessDataChitiet(ref m_dtReport);
                        thuoc_baocao.ThethuocChitiet(m_dtReport, KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                                     dtNgayIn.Value, FromDateToDate,
                                                     Utility.sDbnull(cboKho.Text));
                    }
                    else
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                        {
                            ProcessDataTutruc(ref m_dtReport);
                            thuoc_baocao.Thethuoctutruc(m_dtReport, KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                                        dtNgayIn.Value, FromDateToDate,
                                                        Utility.sDbnull(cboKho.Text));
                        }
                        else
                        {
                            if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                            {
                                ProcessDataKhochan(ref m_dtReport);
                                thuoc_baocao.Thethuockhochan(m_dtReport, KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                                             dtNgayIn.Value, FromDateToDate,
                                                             Utility.sDbnull(cboKho.Text));
                            }
                            else
                            {
                                ProcessDataKhole(ref m_dtReport);
                                thuoc_baocao.ThethuocKhole(m_dtReport, KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                                           dtNgayIn.Value, FromDateToDate,
                                                           Utility.sDbnull(cboKho.Text));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void ProcessDataTutruc(ref DataTable m_dtReport)
        {
            DataTable dtTemp = m_dtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                int iFound = 0;
                if (m_dtReport.Rows.Count == 1 && Utility.sDbnull(m_dtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    m_dtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    m_dtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.ToString("dd/MM/yyyy");
                }
                DataRow rowdata = m_dtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                //Vong lap nay tao cac du lieu tu tuong lai
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = m_dtReport.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            //Không cần làm gì cả
                            rowdata = arrDr[0];
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] = _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["Tontruoc"] = 0;
                            newDr["NHAP_KLE"] = 0;
                            newDr["XUAT_BN"] = 0;
                            newDr["TRA_KHOLE"] = 0;
                            newDr["TONKC"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = m_dtReport.Copy();
                }
                 _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["TONKC"] = Utility.Int32Dbnull(arrDr[0]["Tontruoc"], 0) +
                                                Utility.Int32Dbnull(arrDr[0]["NHAP_KLE"], 0)
                                                - Utility.Int32Dbnull(arrDr[0]["XUAT_BN"], 0) -
                                                Utility.Int32Dbnull(arrDr[0]["TRA_KHOLE"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) < Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["Tontruoc"] = Utility.Int32Dbnull(drPrevious["TONKC"], 0);
                                arrDr[0]["TONKC"] = Utility.Int32Dbnull(arrDr[0]["Tontruoc"], 0) +
                                                    Utility.Int32Dbnull(arrDr[0]["NHAP_KLE"], 0)
                                                    - Utility.Int32Dbnull(arrDr[0]["XUAT_BN"], 0) -
                                                    Utility.Int32Dbnull(arrDr[0]["TRA_KHOLE"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else //Ca
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch
            {
            }
            finally
            {
                m_dtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataKhole(ref DataTable m_dtReport)
        {
            DataTable dtTemp = m_dtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                int iFound = 0;
                if (m_dtReport.Rows.Count == 1 && Utility.sDbnull(m_dtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    m_dtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    m_dtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date;
                }
                DataRow rowdata = m_dtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = m_dtReport.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] = _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["Tontruoc"] = 0;
                            newDr["NhapTuKhoChan"] = 0;
                            newDr["KPTRALAI"] = 0;
                            newDr["XUATKP"] = 0;
                            newDr["XuatBN"] = 0;
                            newDr["XuatTraKhoChan"] = 0;
                            newDr["Xuatkhac"] = 0;
                            newDr["TONKC"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = m_dtReport.Copy();
                }

                _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["TONKC"] = Utility.Int32Dbnull(arrDr[0]["Tontruoc"], 0) +
                                                Utility.Int32Dbnull(arrDr[0]["NhapTuKhoChan"], 0)
                                                + Utility.Int32Dbnull(arrDr[0]["KPTRALAI"], 0)
                                                - Utility.Int32Dbnull(arrDr[0]["XUATKP"], 0) -
                                                Utility.Int32Dbnull(arrDr[0]["XuatBN"], 0) -
                                                Utility.Int32Dbnull(arrDr[0]["XuatTraKhoChan"], 0) -
                                                Utility.Int32Dbnull(arrDr[0]["Xuatkhac"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) < Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["Tontruoc"] = Utility.Int32Dbnull(drPrevious["TONKC"], 0);
                                arrDr[0]["TONKC"] = Utility.Int32Dbnull(arrDr[0]["Tontruoc"], 0) +
                                                    Utility.Int32Dbnull(arrDr[0]["NhapTuKhoChan"], 0)
                                                    + Utility.Int32Dbnull(arrDr[0]["KPTRALAI"], 0)
                                                    - Utility.Int32Dbnull(arrDr[0]["XUATKP"], 0) -
                                                    Utility.Int32Dbnull(arrDr[0]["XuatBN"], 0) -
                                                    Utility.Int32Dbnull(arrDr[0]["XuatTraKhoChan"], 0)
                                                    - Utility.Int32Dbnull(arrDr[0]["Xuatkhac"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch
            {
            }
            finally
            {
                m_dtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataChitiet(ref DataTable m_dtReport)
        {
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);

                int iFound = 0;
                int idx = 0;
                foreach (DataRow dr in m_dtReport.Rows)
                {
                    DateTime _ngaybiendong = Convert.ToDateTime(dr[TBiendongThuoc.Columns.NgayBiendong]);
                    long _myId = Utility.Int64Dbnull(dr[TBiendongThuoc.Columns.IdBiendong], 0);
                    iFound++;
                    if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                    {
                        dr["Toncuoi"] = Utility.Int32Dbnull(dr["Tondau"], 0) + Utility.Int32Dbnull(dr["SoLuongNhap"], 0)
                                        - Utility.Int32Dbnull(dr["SoLuongXuat"], 0);
                    }
                    else
                    {
                        OrderedEnumerableRowCollection<DataRow> q = from p in m_dtReport.AsEnumerable()
                                                                    where
                                                                        Convert.ToDateTime(
                                                                            p[TBiendongThuoc.Columns.NgayBiendong]) <=
                                                                        _ngaybiendong
                                                                        &&
                                                                        Utility.Int64Dbnull(
                                                                            p[TBiendongThuoc.Columns.IdBiendong], 0) !=
                                                                        _myId
                                                                        && Utility.Int32Dbnull(p["processed"], 0) > 0
                                                                    orderby
                                                                        Utility.Int32Dbnull(p["processed"], 0)
                                                                        descending
                                                                    //Convert.ToDateTime(p[TBiendongThuoc.Columns.NgayBiendong]) descending
                                                                    select p;
                        if (q.Count() > 0)
                        {
                            DataRow drPrevious = q.FirstOrDefault();
                            dr["Tondau"] = Utility.Int32Dbnull(drPrevious["Toncuoi"], 0);
                            dr["Toncuoi"] = Utility.Int32Dbnull(dr["Tondau"], 0) +
                                            Utility.Int32Dbnull(dr["SoLuongNhap"], 0)
                                            - Utility.Int32Dbnull(dr["SoLuongXuat"], 0);
                        }
                    }
                    idx++;
                    dr["processed"] = idx;
                    m_dtReport.AcceptChanges();
                }
            }
            catch
            {
            }
        }

        private void ProcessDataKhochan(ref DataTable m_dtReport)
        {
            DataTable dtTemp = m_dtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                if (m_dtReport.Rows.Count == 1 && Utility.sDbnull(m_dtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    m_dtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    m_dtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date;
                }
                int iFound = 0;
                DataRow rowdata = m_dtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = m_dtReport.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] = _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["TonThangKetChuyen"] = 0;
                            newDr["TonThangTruocKetChuyen"] = 0;
                            newDr["SoLuongNhap"] = 0;
                            newDr["Tralaitukhole"] = 0;
                            newDr["SoLuongXuat"] = 0;
                            newDr["Tranhacungcap"] = 0;
                            newDr["Xuatkhac"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = m_dtReport.Copy();
                }
                _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["TonThangKetChuyen"] = Utility.Int32Dbnull(arrDr[0]["TonThangTruocKetChuyen"], 0) +
                                                            Utility.Int32Dbnull(arrDr[0]["SoLuongNhap"], 0)
                                                            + Utility.Int32Dbnull(arrDr[0]["Tralaitukhole"], 0)
                                                            - Utility.Int32Dbnull(arrDr[0]["SoLuongXuat"], 0) -
                                                            Utility.Int32Dbnull(arrDr[0]["Tranhacungcap"], 0) -
                                                            Utility.Int32Dbnull(arrDr[0]["Xuatkhac"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) <  Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["TonThangTruocKetChuyen"] = Utility.Int32Dbnull(
                                    drPrevious["TonThangKetChuyen"], 0);
                                arrDr[0]["TonThangKetChuyen"] =
                                    Utility.Int32Dbnull(arrDr[0]["TonThangTruocKetChuyen"], 0) +
                                    Utility.Int32Dbnull(arrDr[0]["SoLuongNhap"], 0)
                                    + Utility.Int32Dbnull(arrDr[0]["Tralaitukhole"], 0)
                                    - Utility.Int32Dbnull(arrDr[0]["SoLuongXuat"], 0) -
                                    Utility.Int32Dbnull(arrDr[0]["Tranhacungcap"], 0)
                                    - Utility.Int32Dbnull(arrDr[0]["Xuatkhac"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch
            {
            }
            finally
            {
                m_dtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataThethuoc(ref DataTable m_dtReport)
        {
           
            DataTable dtTemp = m_dtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                if (m_dtReport.Rows.Count == 1 && Utility.sDbnull(m_dtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    m_dtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    m_dtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date.ToString("dd/MM/yyyy");
                }
                int iFound = 0;
                DataRow rowdata = m_dtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = m_dtReport.Select("YYYYMMDD=" +Utility.GetYYYYMMDD( _dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] =
                                _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["Xuat"] = 0;
                            newDr["Tondau"] = 0;
                            newDr["Toncuoi"] = 0;
                            newDr["Nhap"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = m_dtReport.Copy();
                }
                _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["Toncuoi"] = Utility.Int32Dbnull(arrDr[0]["Tondau"], 0) +
                                                  Utility.Int32Dbnull(arrDr[0]["Nhap"], 0)
                                                  - Utility.Int32Dbnull(arrDr[0]["Xuat"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) < Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["Tondau"] = Utility.Int32Dbnull(drPrevious["Toncuoi"], 0);
                                arrDr[0]["Toncuoi"] = Utility.Int32Dbnull(arrDr[0]["Tondau"], 0) +
                                                      Utility.Int32Dbnull(arrDr[0]["Nhap"], 0)
                                                      - Utility.Int32Dbnull(arrDr[0]["Xuat"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                //Utility.CatchException(ex);
            }
            finally{
                m_dtReport = dtTemp.Copy();
            }
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_thethuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }


        private void cmdBaoCao_Click_1(object sender, EventArgs e)
        {
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkThekhochitiet.Checked)
                {
                    if (grdListChitiet.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu chi tiết để xuất file excel", "Thông báo");
                        return;
                    }
                    gridEXExporter1.GridEX = grdListChitiet;
                }
                else
                {
                    if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                    {
                        if (grdListKhoChan.RowCount <= 0)
                        {
                            Utility.ShowMsg("Không có dữ liệu để xuất file excel", "Thông báo");
                            return;
                        }
                        gridEXExporter1.GridEX = grdListKhoChan;
                    }
                    else if (grdListKhole.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu để xuất file excel", "Thông báo");
                        return;
                    }
                    gridEXExporter1.GridEX = grdListKhole;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", baocaO_TIEUDE1.TIEUDE);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    var fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
            }
        }

        private void frm_thethuoc_KeyDown_1(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// hàm thực hiện việc 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetDataDrug_Click(object sender, EventArgs e)
        {
        }
    }
}