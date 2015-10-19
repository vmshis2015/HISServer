using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.UCs;
using Janus.Windows.EditControls;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_KCB_Lapphieuhen_Tiemchung : Form
    {
        DataSet dsData = new DataSet();
        public KcbLuotkham objLuotkham = null;
        public frm_KCB_Lapphieuhen_Tiemchung()
        {
            InitializeComponent();
        }
        void InitEvents()
        {
            this.Load += frm_KCB_Lapphieuhen_Tiemchung_Load;
            this.KeyDown += frm_KCB_Lapphieuhen_Tiemchung_KeyDown;
            chkMuithu1.CheckedChanged += _CheckedChanged;
            chkMuithu2.CheckedChanged += _CheckedChanged;
            chkMuithu3.CheckedChanged += _CheckedChanged;
            chkMuithu4.CheckedChanged += _CheckedChanged;
            chkMuithu5.CheckedChanged += _CheckedChanged;
            cmdSave.Click += cmdSave_Click;
            cmdExit.Click += cmdExit_Click;
            cmdPrint.Click += cmdPrint_Click;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            grdList.KeyDown += grdList_KeyDown;
            txtDichvuTiemchung._OnEnterMe += txtDichvuTiemchung__OnEnterMe;
        }

        void txtDichvuTiemchung__OnEnterMe()
        {
            if (dsData.Tables[1].Select(KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung + "='" + txtDichvuTiemchung.myCode + "'").Length > 0)
            {
                Utility.GotoNewRowJanus(grdList, KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung, txtDichvuTiemchung.myCode);
                grdList_KeyDown(grdList, new KeyEventArgs(Keys.Enter));
            }
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!Utility.isValidGrid(grdList)) return;
                string id = Utility.GetValueFromGridColumn(grdList, KcbPhieuhenTiemchungChitiet.Columns.Id);
                txtDichvuTiemchung.SetCode( Utility.GetValueFromGridColumn(grdList, KcbPhieuhenTiemchung.Columns.MaLoaidvuTiemchung));
                DataRow[] arrDr = dsData.Tables[1].Select("id='" + id + "'");
                if (arrDr.Length > 0)
                {
                    var q = from p in arrDr.AsEnumerable()
                            where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "1"
                            select p;
                    if (q.Any())
                    {
                        chkMuithu1.Checked = true ;
                    }
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "2"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu2.Checked = true;
                    }
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "3"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu3.Checked = true;
                    }
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "4"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu4.Checked = true;
                    }
                    q = from p in arrDr.AsEnumerable()
                        where Utility.sDbnull(p[KcbPhieuhenTiemchungChitiet.Columns.MuiThu], "0") == "5"
                        select p;
                    if (q.Any())
                    {
                        chkMuithu5.Checked = true;
                    }
                }
                else
                {
                    ResetView();

                }
            }
        }
        void ResetView()
        {
            chkMuithu1.Checked = false;
            chkMuithu2.Checked = false;
            chkMuithu3.Checked = false;
            chkMuithu4.Checked = false;
            chkMuithu5.Checked = false;
        }
        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdList_KeyDown(grdList, new KeyEventArgs(Keys.Enter));
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
            UICheckBox chk = sender as UICheckBox;
            int _tag=Utility.Int32Dbnull(chk.Tag,-1);
            DataRow[] arrDr = dsData.Tables[1].Select("mui_thu=" + _tag.ToString() + " AND ma_loaidvu_tiemchung='"+txtDichvuTiemchung.myCode+"'");
            switch (_tag)
            {
                case 1:
                    dtpNgaytiem1.Enabled = chk.Enabled;
                    if (arrDr.Length > 0)
                    {
                        dtpNgaytiem1.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                        txtNguoitiem1.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                        dtpNgaytiem1.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                        cmdDatiem1.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"],"0")=="0";
                        cmdChuatiem1.Enabled = !cmdDatiem1.Enabled;
                    }

                    break;
                case 2:
                    dtpNgaytiem2.Enabled = chk.Enabled;
                    if (arrDr.Length > 0)
                    {
                        dtpNgaytiem2.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                        txtNguoitiem2.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                        dtpNgaytiem2.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                        cmdDatiem2.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                        cmdChuatiem2.Enabled = !cmdDatiem2.Enabled;
                    }

                    break;
                case 3:
                    dtpNgaytiem3.Enabled = chk.Enabled;
                    if (arrDr.Length > 0)
                    {
                        dtpNgaytiem3.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                        txtNguoitiem3.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                        dtpNgaytiem3.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                        cmdDatiem3.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                        cmdChuatiem3.Enabled = !cmdDatiem3.Enabled;
                    }

                    break;
                case 4:
                    dtpNgaytiem4.Enabled = chk.Enabled;
                    if (arrDr.Length > 0)
                    {
                        dtpNgaytiem4.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                        txtNguoitiem4.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                        dtpNgaytiem4.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                        cmdDatiem4.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                        cmdChuatiem4.Enabled = !cmdDatiem4.Enabled;
                    }

                    break;
                case 5:
                    dtpNgaytiem5.Enabled = chk.Enabled;
                    if (arrDr.Length > 0)
                    {
                        dtpNgaytiem5.Text = Utility.sDbnull(arrDr[0]["sngay_hen"]);
                        txtNguoitiem5.SetId(Utility.sDbnull(arrDr[0]["nguoi_tiem"]));
                        dtpNgaytiem5.Text = Utility.sDbnull(arrDr[0]["ngay_tiem"]);
                        cmdDatiem5.Enabled = Utility.sDbnull(arrDr[0]["trang_thai"], "0") == "0";
                        cmdChuatiem5.Enabled = !cmdDatiem5.Enabled;
                    }

                    break;
                default:
                    break;
            }

        }

        void frm_KCB_Lapphieuhen_Tiemchung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F2)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F3)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F4)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                _CheckedChanged(chkMuithu1, e);
                return;
            }
        }

        void frm_KCB_Lapphieuhen_Tiemchung_Load(object sender, EventArgs e)
        {
            try
            {
                InitView();
                Getdata();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void InitView()
        {
            txtDichvuTiemchung.Init();
            DataTable Nguoitiem = THU_VIEN_CHUNG.Laydanhsachnhanvien("NGUOITIEM");
            txtNguoitiem1.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem2.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem3.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem4.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            txtNguoitiem5.Init(Nguoitiem, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
        }
        void Getdata()
        {
            
           dsData = KCB_TIEMCHUNG.KcbTiemchungPhieuhen(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
           Utility.SetDataSourceForDataGridEx(grdList, dsData.Tables[0], false, true, "", "STT_HTHI,tenloaidvu_tiemchung");
        }
    }
}
