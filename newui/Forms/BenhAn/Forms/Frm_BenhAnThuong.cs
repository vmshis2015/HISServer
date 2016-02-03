using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;

namespace VNS.HIS.UI.BENH_AN
{
    public partial class Frm_BenhAnThuong : Form
    {
        private readonly string strBenhAn = Application.StartupPath + @"\CAUHINH\BAn_NTru_DongSauLuu.txt";
        private bool AllowTextChanged;
        private string MaBenhAnText = "";
        private string MaBenhNhanText = "";
        private bool SuaBenhAn;
        public action em_Action = action.Insert;
        public string loaibenhan = "";

        public Frm_BenhAnThuong()
        {
            InitializeComponent();
            InitEvents();
            txtMaBN.KeyDown += txtMaBN_KeyDown;
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
        }

        private void InitEvents()
        {
            txtChanDoanBanDau._OnSaveAs += txtChanDoanKemTheo__OnSaveAs;
            txtChanDoanBanDau._OnShowData += txtChanDoanBanDau__OnShowData;

            txtBAT_LDVV._OnSaveAs += txtBAT_LDVV__OnSaveAs;
            txtBAT_LDVV._OnShowData += txtBAT_LDVV__OnShowData;

            txtBAT_QTBL._OnSaveAs += txtBAT_QTBL__OnSaveAs;
            txtBAT_QTBL._OnShowData += txtBAT_QTBL__OnShowData;

            txtBAT_BT._OnSaveAs += txtBAT_BT__OnSaveAs;
            txtBAT_BT._OnShowData += txtBAT_BT__OnShowData;

            txtBAT_GD._OnSaveAs += txtBAT_GD__OnSaveAs;
            txtBAT_GD._OnShowData += txtBAT_GD__OnShowData;

            txtBAT_TT._OnSaveAs += txtBAT_TT__OnSaveAs;
            txtBAT_TT._OnShowData += txtBAT_TT__OnShowData;

            txtBAT_CBP._OnSaveAs += txtBAT_CBP__OnSaveAs;
            txtBAT_CBP._OnShowData += txtBAT_CBP__OnShowData;

            txtBAT_TTLQCLSC._OnSaveAs += txtBAT_TTLQCLSC__OnSaveAs;
            txtBAT_TTLQCLSC._OnShowData += txtBAT_TTLQCLSC__OnShowData;

            TXTKBChanDoanBD._OnSaveAs += TXTKBChanDoanBD__OnSaveAs;
            TXTKBChanDoanBD._OnShowData += TXTKBChanDoanBD__OnShowData;


            TXTKBDaXuLy._OnSaveAs += TXTKBDaXuLy__OnSaveAs;
            TXTKBDaXuLy._OnShowData += TXTKBDaXuLy__OnShowData;

        }
        private void GetDanhMucChung()
        {
            txtChanDoanBanDau.Init();
            txtBAT_LDVV.Init();
            txtBAT_QTBL.Init();
            txtBAT_BT.Init();
            txtBAT_GD.Init();
            txtBAT_TT.Init();
            txtBAT_CBP.Init();
            txtBAT_TTLQCLSC.Init();
            TXTKBChanDoanBD.Init();
            TXTKBDaXuLy.Init();
        }
        private void txtChanDoanBanDau__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoanBanDau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoanBanDau.myCode;
                txtChanDoanBanDau.Init();
                txtChanDoanBanDau.SetCode(oldCode);
                txtChanDoanBanDau.Focus();
            }
        }
        private void txtChanDoanKemTheo__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoanBanDau.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoanBanDau.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtChanDoanBanDau.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoanBanDau.myCode;
                txtChanDoanBanDau.Init();
                txtChanDoanBanDau.SetCode(oldCode);
                txtChanDoanBanDau.Focus();
            }
        }
        private void txtBAT_LDVV__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_LDVV.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_LDVV.myCode;
                txtBAT_LDVV.Init();
                txtBAT_LDVV.SetCode(oldCode);
                txtBAT_LDVV.Focus();
            }
        }
        private void txtBAT_LDVV__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_LDVV.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_LDVV.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_LDVV.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_LDVV.myCode;
                txtBAT_LDVV.Init();
                txtBAT_LDVV.SetCode(oldCode);
                txtBAT_LDVV.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBAT_QTBL__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_QTBL.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_QTBL.myCode;
                txtBAT_QTBL.Init();
                txtBAT_QTBL.SetCode(oldCode);
                txtBAT_QTBL.Focus();
            }
        }
        private void txtBAT_QTBL__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_QTBL.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_QTBL.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_QTBL.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_QTBL.myCode;
                txtBAT_QTBL.Init();
                txtBAT_QTBL.SetCode(oldCode);
                txtBAT_QTBL.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBAT_BT__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_BT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_BT.myCode;
                txtBAT_BT.Init();
                txtBAT_BT.SetCode(oldCode);
                txtBAT_BT.Focus();
            }
        }
        private void txtBAT_BT__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_BT.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_BT.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_BT.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_BT.myCode;
                txtBAT_BT.Init();
                txtBAT_BT.SetCode(oldCode);
                txtBAT_BT.Focus();
            }
        }
        private void txtBAT_GD__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_GD.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_GD.myCode;
                txtBAT_GD.Init();
                txtBAT_GD.SetCode(oldCode);
                txtBAT_GD.Focus();
            }
        }
        private void txtBAT_GD__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_GD.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_GD.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_GD.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_GD.myCode;
                txtBAT_GD.Init();
                txtBAT_GD.SetCode(oldCode);
                txtBAT_GD.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBAT_TT__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_TT.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_TT.myCode;
                txtBAT_TT.Init();
                txtBAT_TT.SetCode(oldCode);
                txtBAT_TT.Focus();
            }
        }
        private void txtBAT_TT__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_TT.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_TT.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_TT.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_TT.myCode;
                txtBAT_TT.Init();
                txtBAT_TT.SetCode(oldCode);
                txtBAT_TT.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBAT_CBP__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_CBP.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_CBP.myCode;
                txtBAT_CBP.Init();
                txtBAT_CBP.SetCode(oldCode);
                txtBAT_CBP.Focus();
            }
        }
        private void txtBAT_CBP__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_CBP.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_CBP.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_CBP.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_CBP.myCode;
                txtBAT_CBP.Init();
                txtBAT_CBP.SetCode(oldCode);
                txtBAT_CBP.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBAT_TTLQCLSC__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_TTLQCLSC.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_TTLQCLSC.myCode;
                txtBAT_TTLQCLSC.Init();
                txtBAT_TTLQCLSC.SetCode(oldCode);
                txtBAT_TTLQCLSC.Focus();
            }
        }
        private void txtBAT_TTLQCLSC__OnSaveAs()
        {
            if (Utility.DoTrim(txtBAT_TTLQCLSC.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtBAT_TTLQCLSC.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtBAT_TTLQCLSC.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtBAT_TTLQCLSC.myCode;
                txtBAT_TTLQCLSC.Init();
                txtBAT_TTLQCLSC.SetCode(oldCode);
                txtBAT_TTLQCLSC.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXTKBChanDoanBD__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(TXTKBChanDoanBD.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = TXTKBChanDoanBD.myCode;
                TXTKBChanDoanBD.Init();
                TXTKBChanDoanBD.SetCode(oldCode);
                TXTKBChanDoanBD.Focus();
            }
        }
        private void TXTKBChanDoanBD__OnSaveAs()
        {
            if (Utility.DoTrim(TXTKBChanDoanBD.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(TXTKBChanDoanBD.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, TXTKBChanDoanBD.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = TXTKBChanDoanBD.myCode;
                TXTKBChanDoanBD.Init();
                TXTKBChanDoanBD.SetCode(oldCode);
                TXTKBChanDoanBD.Focus();
            }
        }
        private void TXTKBDaXuLy__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(TXTKBDaXuLy.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = TXTKBDaXuLy.myCode;
                TXTKBDaXuLy.Init();
                TXTKBDaXuLy.SetCode(oldCode);
                TXTKBDaXuLy.Focus();
            }
        }
        private void TXTKBDaXuLy__OnSaveAs()
        {
            if (Utility.DoTrim(TXTKBDaXuLy.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(TXTKBDaXuLy.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, TXTKBDaXuLy.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = TXTKBDaXuLy.myCode;
                TXTKBDaXuLy.Init();
                TXTKBDaXuLy.SetCode(oldCode);
                TXTKBDaXuLy.Focus();
            }
        }
        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            ClearControl();
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
                FindPatientID(txtMaBN.Text.Trim());
                SuaBenhAn = false;
            }
        }
        
        // LẤY THÔNG TIN BỆNH ÁN CỦA BỆNH NHÂN
        private void LayThongTinBenhNhan(string id_benhnhan)
        {
            var objThongTinBenhNhan = new Select()
                .From(KcbDanhsachBenhnhan.Schema)
                .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(id_benhnhan)).ExecuteSingle
                <KcbDanhsachBenhnhan>();
            try
            {
                txtDiaChi.Text = Utility.sDbnull(objThongTinBenhNhan.DiaChi, "");
                txtHoTen.Text = Utility.sDbnull(objThongTinBenhNhan.TenBenhnhan, "");
                txtNamSinh.Text = Utility.sDbnull(objThongTinBenhNhan.NamSinh, "");
                cboGioiTinh.SelectedValue = Utility.sDbnull(objThongTinBenhNhan.GioiTinh, "");
                txtNgheNghiep.Text = Utility.sDbnull(objThongTinBenhNhan.NgheNghiep, "");
                dtThoiDiemDkKham.Value = objThongTinBenhNhan.NgayTiepdon;

                txtDanToc.Text = Utility.sDbnull(objThongTinBenhNhan.DanToc);

                QueryCommand cmd = KcbLuotkham.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "Select max(ma_luotkham) as ma_luotkham from Kcb_Luotkham where id_benhnhan = " +
                                 id_benhnhan;
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count > 0)
                {
                    MaBenhNhanText = id_benhnhan;
                    string malankham = temdt.Rows[0]["ma_luotkham"].ToString();


                    QueryCommand cmd1 = KcbLuotkham.CreateQuery().BuildCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandSql = "Select *  from Kcb_Luotkham where ma_luotkham ='" +
                                      malankham + "'";
                    DataTable temdt1 = DataService.GetDataSet(cmd1).Tables[0];

                    txtDoiTuong.Text = Utility.sDbnull(temdt1.Rows[0]["ma_doituong_kcb"].ToString());
                    dtInsToDate.Text = temdt1.Rows[0]["ngayketthuc_bhyt"].ToString();
                    txtSoBaoHiemYte.Text = Utility.sDbnull(temdt1.Rows[0]["mathe_bhyt"].ToString());

                }
                else
                {
                    MessageBox.Show("Không tồn tại mã bệnh nhân vừa nhập");
                    txtMaBN.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm thông tin BN:\n" + ex.Message);
                txtMaBN.Focus();
            }
        }

        //TÌM BỆNH NHAN THEO MÃ BỆNH NHÂN
        private void FindPatientID(string id_benhnhan)
        {
            LayThongTinBenhNhan(txtMaBN.Text);
            dtDieuTriNgoaiTruTu.Value = dtDieuTriNgoaiTruDen.Value = DateTime.Now;
            try
            {
                SqlQuery SoKham =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtMaBN.Text));

                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (SoKham.GetRecordCount() > 0)
                {
                    em_Action = action.Update;
                    LayDuLieu();
                    txtNgaySinh.Focus();
                    MaBenhAnText = txtMaBenhAn.Text;
                    cmdInBenhAn.Enabled = true;
                }
                    //chưa tồn tại thông tin ngoại tru. Them
                else
                {
                    em_Action = action.Insert;
                    txtMaBenhAn.Clear();
                    txtNgaySinh.Focus();
                    // MaBenhAnText = txtMaBenhAn.Text;
                    cmdInBenhAn.Enabled = false;
                    label94.Text = "- Bệnh nhân chưa có bệnh án: Ấn Lưu, hoặc nhập số bệnh án cũ và Lưu";
                    label95.Text = "- Người Tạo:";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }


        //SINH MÃ BỆNH ÁN
        private void SinhMaBenhAn()
        {
            if (em_Action == action.Insert)
            {

                QueryCommand cmd1 = KcbLuotkham.CreateQuery().BuildCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandSql = "Select *  from Kcb_Luotkham where ma_luotkham ='" +
                                  txtMaLanKham.Text + "'";
                DataTable temdt1 = DataService.GetDataSet(cmd1).Tables[0];
                if (Utility.sDbnull(temdt1.Rows[0]["so_benh_an"].ToString().Trim(), "-1") == "-1" || Utility.sDbnull(temdt1.Rows[0]["so_benh_an"].ToString().Trim(), "-1") == "")
                {
                    txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn(loaibenhan);
                }
                else
                {
                    txtMaBenhAn.Text = Utility.sDbnull(temdt1.Rows[0]["so_benh_an"].ToString());
                }
            }
        }

        // LAY DU LIEU BENH AN

        private void LayDuLieu()
        {
            try
            {
                var objBenhAnNgoaiTru =
                    new Select("*").From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(
                        Utility.Int32Dbnull(txtMaBN.Text.Trim())).ExecuteSingle<KcbBenhAn>();
                //  KcbBenhAn objBenhAnNgoaiTru = KcbBenhAn.FetchByID(Utility.Int32Dbnull(txtSoKham.Text));
                if (objBenhAnNgoaiTru != null)
                {
                    switch (objBenhAnNgoaiTru.LoaiBa)
                    {
                        case "DTD":
                            this.Text = "Bệnh án ĐáiTháo Đường";
                            break;
                        case "BAS":
                            this.Text = "Bệnh án Basedow";
                            break;
                        case "RHM":
                            this.Text = "Bệnh án Răng Hàm Mặt";
                            break;
                        case "TMH":
                            this.Text = "Bệnh án Tai Mũi Họng";
                            break;
                        case "COP":
                            this.Text = "Bệnh án Cop";
                            break;
                        case "VGB":
                            this.Text = "Bệnh án Viêm Gan B";
                            break;
                        case "THA":
                            this.Text = "Bệnh án Tăng huyết áp";
                            break;
                    }

                    txtID_BA.Text = Utility.sDbnull(objBenhAnNgoaiTru.Id, "");
                    txtMaBenhAn.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBenhAn, -1);
                    txtMaBN.Text = Utility.sDbnull(objBenhAnNgoaiTru.IdBnhan, "");
                    txtNgaySinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.NgaySinh, "");
                    txtThangSinh.Text = Utility.sDbnull(objBenhAnNgoaiTru.ThangSinh, "");
                    txtDanToc.Text = Utility.sDbnull(objBenhAnNgoaiTru.DanToc, "");
                    txtNoiLamViec.Text = Utility.sDbnull(objBenhAnNgoaiTru.NoiLamviec, "");
                    txtThongTinLienHe.Text = Utility.sDbnull(objBenhAnNgoaiTru.ThongtinLhe, "");
                    txtDienThoai.Text = Utility.sDbnull(objBenhAnNgoaiTru.DthoaiLhe, "");
                    txtChanDoanBanDau.Text = Utility.sDbnull(objBenhAnNgoaiTru.CdoanGioithieu, "");
                    chkYTe.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 1;
                    chkTuDen.Checked = Utility.Int16Dbnull(objBenhAnNgoaiTru.YTe) == 2;
                    dtThoiDiemDkKham.Value = Convert.ToDateTime(objBenhAnNgoaiTru.NgayKham);

                    txtBAT_LDVV._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatLdvv);
                    txtBAT_QTBL._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatBhQtbl);
                    txtBAT_BT._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatHbBt);
                    txtBAT_GD._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatHbGd);
                    txtBAT_TT._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatKbTt);
                    txtBAT_CBP._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatKbCbp);
                    txtBAT_TTLQCLSC._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatKbKqcls);
                    TXTKBChanDoanBD._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatKbCdbd);
                    TXTKBDaXuLy._Text = Utility.sDbnull(objBenhAnNgoaiTru.BatKbDxl);
                    txtkbChanDoanRaVien.Text = Utility.sDbnull(objBenhAnNgoaiTru.BatKbCdrv);


                    txtMach.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbMach, "");
                    txtNhietDo.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhietdo);
                    txtHuyetApTu.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHuyetap1);
                    txtHuyetApDen.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbHuyetap2);
                    txtNhipTho.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbNhiptho);
                    txtCanNang.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbCannang);
                    txtChieuCao.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbChieucao);
                    if(Utility.DoubletoDbnull(objBenhAnNgoaiTru.KcbBmi) >0)
                    txtBMI.Text = Utility.sDbnull(objBenhAnNgoaiTru.KcbBmi);

                    dtDieuTriNgoaiTruTu.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriTungay);
                    dtDieuTriNgoaiTruDen.Value = Convert.ToDateTime(objBenhAnNgoaiTru.KcbDtriDenngay);


                    txtDoiTuong.Text = Utility.sDbnull(objBenhAnNgoaiTru.DoiTuong);
                    txtSoBaoHiemYte.Text = Utility.sDbnull(objBenhAnNgoaiTru.SoBhyt);
                    txtDiaChi.Text = Utility.sDbnull(objBenhAnNgoaiTru.DiaChi);
                    label94.Text = "Bệnh nhân đã có bệnh án";
                    label95.Text = Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm thông tin BN:\n" + ex.Message);
            }
        }


        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Update:
                    UpdatePatient();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Select:
                    // AddNewPatient();
                    break;
                case action.Delete:
                    DeletePatient();
                    break;
            }
        }

        private void DeletePatient()
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn xóa BỆNH ÁN", "Thông Báo", true))
                {
                    new Delete().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(
                        Utility.sDbnull(txtMaBenhAn.Text)).Execute();
                    ClearControl();
                    FindPatientID(MaBenhNhanText);
                }
                else
                {
                    FindPatientID(MaBenhNhanText);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void UpdatePatient()
        {
            try
            {
                SqlQuery KT_IN_BENH_AN =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.InPhieuLog).IsEqualTo(Utility.Int16Dbnull(1)).And(
                            KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.Int32Dbnull(MaBenhNhanText, -1));
                if (KT_IN_BENH_AN.GetRecordCount() > 0)
                {
                    MessageBox.Show("Bệnh án đã được in, vui lòng mở khóa bệnh án để chỉnh sửa");
                }
                else
                {
                    SqlQuery KT_SO_BENH_AN =
                        new Select().From(KcbBenhAn.Schema).
                            Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(txtMaBenhAn.Text))
                            .And(KcbBenhAn.Columns.IdBnhan).IsNotEqualTo(Utility.sDbnull(txtMaBN.Text, -1))
                            .And(KcbBenhAn.Columns.LoaiBa).IsEqualTo(Utility.sDbnull(loaibenhan));
                    if (KT_SO_BENH_AN.GetRecordCount() > 0)
                    {
                        MessageBox.Show("Mã bệnh đã được sử dụng, Kiểm tra lại?");
                    }
                    else
                    {
                        KcbBenhAn objBenhAnNgoaiTru = CreateBenhAnNgoaiTru();
                        objBenhAnNgoaiTru.Save();
                        MessageBox.Show("Sửa thành công");
                        if (chkDongSauKhiLuu.Checked) Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void InsertPatient()
        {
            try
            {
                if (SuaBenhAn)
                {
                    if (string.IsNullOrEmpty(txtMaBenhAn.Text))
                    {
                        SinhMaBenhAn();
                    }
                }
                else
                {
                    SinhMaBenhAn();
                }

                MaBenhAnText = txtMaBenhAn.Text;
                SqlQuery KT_SO_BENH_AN =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(Utility.sDbnull(txtMaBenhAn.Text)).And(
                            KcbBenhAn.Columns.LoaiBa).IsEqualTo(Utility.sDbnull(loaibenhan));
                if (KT_SO_BENH_AN.GetRecordCount() > 0)
                {
                    MessageBox.Show("Mã bệnh đã được sử dụng, Kiểm tra lại?");
                }
                else
                {
                    KcbBenhAn objBenhAnNgoaiTru = CreateBenhAnNgoaiTru();
                    objBenhAnNgoaiTru.IsNew = true;
                    objBenhAnNgoaiTru.Save();
                    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(string.Format("{0}-{1}",objBenhAnNgoaiTru.LoaiBa,objBenhAnNgoaiTru.SoBenhAn))
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objBenhAnNgoaiTru.MaLuotkham).And(
                            KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objBenhAnNgoaiTru.IdBnhan).Execute();
                    MessageBox.Show("Lưu Thành Công Thông Tin Bệnh Án");

                    if (chkDongSauKhiLuu.Checked) Close();
                    else
                    {
                        FindPatientID(txtMaBN.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private KcbBenhAn CreateBenhAnNgoaiTru()
        {
            var objBenhAnNgoaiTru = new KcbBenhAn();
            try
            {
                if (em_Action == action.Update)
                {
                    objBenhAnNgoaiTru.IsLoaded = true;
                    objBenhAnNgoaiTru.MarkOld();
                    objBenhAnNgoaiTru.Id = Utility.Int32Dbnull(txtID_BA.Text, -1);
                    objBenhAnNgoaiTru.NgaySua = globalVariables.SysDate;
                    objBenhAnNgoaiTru.NguoiSua = globalVariables.UserName;
                }
                if (em_Action == action.Insert)
                {
                    objBenhAnNgoaiTru.NguoiTao = globalVariables.UserName;
                    objBenhAnNgoaiTru.NgayTao = globalVariables.SysDate;
                }
                objBenhAnNgoaiTru.LoaiBa = loaibenhan;
                objBenhAnNgoaiTru.MaLuotkham = txtMaLanKham.Text;
                objBenhAnNgoaiTru.SoBenhAn = Utility.sDbnull(txtMaBenhAn.Text);
                objBenhAnNgoaiTru.IdBnhan = Utility.Int32Dbnull(txtMaBN.Text);
                objBenhAnNgoaiTru.NgaySinh = Utility.sDbnull(txtNgaySinh.Text);
                objBenhAnNgoaiTru.ThangSinh = Utility.sDbnull(txtThangSinh.Text);
                objBenhAnNgoaiTru.DanToc = Utility.sDbnull(txtDanToc.Text);
                objBenhAnNgoaiTru.NoiLamviec = Utility.sDbnull(txtNoiLamViec.Text);
                objBenhAnNgoaiTru.ThongtinLhe = Utility.sDbnull(txtThongTinLienHe.Text);
                objBenhAnNgoaiTru.DthoaiLhe = Utility.sDbnull(txtDienThoai.Text);
                objBenhAnNgoaiTru.CdoanGioithieu = Utility.sDbnull(txtChanDoanBanDau.Text);
                objBenhAnNgoaiTru.NgayKham = Convert.ToDateTime(dtThoiDiemDkKham.Value);


                if (chkYTe.Checked) objBenhAnNgoaiTru.YTe = 1;
                else if (chkTuDen.Checked)
                {
                    objBenhAnNgoaiTru.YTe = 2;
                }
                else
                {
                    objBenhAnNgoaiTru.YTe = 3;
                }


                objBenhAnNgoaiTru.BatLdvv = Utility.sDbnull(txtBAT_LDVV.Text);
                objBenhAnNgoaiTru.BatBhQtbl = Utility.sDbnull(txtBAT_QTBL.Text);
                objBenhAnNgoaiTru.BatHbBt = Utility.sDbnull(txtBAT_BT.Text);
                objBenhAnNgoaiTru.BatHbGd = Utility.sDbnull(txtBAT_GD.Text);
                objBenhAnNgoaiTru.BatKbTt = Utility.sDbnull(txtBAT_TT.Text);
                objBenhAnNgoaiTru.BatKbCbp = Utility.sDbnull(txtBAT_CBP.Text);
                objBenhAnNgoaiTru.BatKbKqcls = Utility.sDbnull(txtBAT_TTLQCLSC.Text);
                objBenhAnNgoaiTru.BatKbCdbd = Utility.sDbnull(TXTKBChanDoanBD.Text);
                objBenhAnNgoaiTru.BatKbDxl = Utility.sDbnull(TXTKBDaXuLy.Text);
                objBenhAnNgoaiTru.BatKbCdrv = Utility.sDbnull(txtkbChanDoanRaVien.Text);


                objBenhAnNgoaiTru.KcbMach = Utility.sDbnull(txtMach.Text);
                objBenhAnNgoaiTru.KcbNhietdo = Utility.sDbnull(txtNhietDo.Text);
                objBenhAnNgoaiTru.KcbHuyetap1 = Utility.sDbnull(txtHuyetApTu.Text);
                objBenhAnNgoaiTru.KcbHuyetap2 = Utility.sDbnull(txtHuyetApDen.Text);
                objBenhAnNgoaiTru.KcbNhiptho = Utility.sDbnull(txtNhipTho.Text);
                objBenhAnNgoaiTru.KcbCannang = Utility.sDbnull(txtCanNang.Text);
                objBenhAnNgoaiTru.KcbChieucao = Utility.sDbnull(txtChieuCao.Text);
                objBenhAnNgoaiTru.KcbBmi = Utility.sDbnull(txtBMI.Text);
                objBenhAnNgoaiTru.TrangThai = 0;


                objBenhAnNgoaiTru.KcbDtriTungay = Convert.ToDateTime(dtDieuTriNgoaiTruTu.Value);
                objBenhAnNgoaiTru.KcbDtriDenngay = Convert.ToDateTime(dtDieuTriNgoaiTruDen.Value);

                objBenhAnNgoaiTru.DoiTuong = Utility.sDbnull(txtDoiTuong.Text);
                objBenhAnNgoaiTru.SoBhyt = Utility.sDbnull(txtSoBaoHiemYte.Text);
                objBenhAnNgoaiTru.DiaChi = Utility.sDbnull(txtDiaChi.Text);
                objBenhAnNgoaiTru.NamSinh = Utility.Int16Dbnull(txtNamSinh.Text);
                objBenhAnNgoaiTru.InPhieuLog = Utility.Int16Dbnull(0);
                objBenhAnNgoaiTru.LoaiBenhAn = Utility.Int16Dbnull(3);
                return objBenhAnNgoaiTru;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objBenhAnNgoaiTru;
            }
        }


        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieu()) return;
                PerformAction();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
         
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                Utility.ShowMsg("Chưa chọn bệnh nhân ", "Thông báo", MessageBoxIcon.Warning);
                txtMaBN.Focus();

                return false;
            }


            if (txtMaBN.Text != MaBenhNhanText)
            {
                Utility.ShowMsg("Mã bệnh nhân đã bị thay đổi:Chọn lại Bệnh Nhân", "Thông báo", MessageBoxIcon.Warning);
                txtMaBN.Focus();

                return false;
            }
            if (SuaBenhAn)
            {
                if (txtMaBenhAn.Text != "")
                {
                    if (globalVariables.SO_BENH_AN == -1)
                    {
                        return true;
                    }
                    else
                    {
                        if (Utility.Int32Dbnull(txtMaBenhAn.Text) < globalVariables.SO_BENH_AN)
                        {
                            Utility.ShowMsg("Số Bệnh án phải nhỏ hơn " + globalVariables.SO_BENH_AN.ToString(),
                                            "Thông báo",
                                            MessageBoxIcon.Warning);

                            txtMaBenhAn.Focus();
                            txtMaBenhAn.SelectAll();
                            return false;
                        }
                        else
                        {
                            SqlQuery sqlkt = new Select().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(txtMaBenhAn.Text).And(KcbBenhAn.Columns.LoaiBa).IsEqualTo(loaibenhan);
                            if(sqlkt.GetRecordCount()>0)
                            {
                                Utility.ShowMsg("Số Bệnh án đã được dùng cho bệnh nhân khác ",
                                          "Thông báo",
                                          MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void cmdInBenhAn_Click_1(object sender, EventArgs e)
        {
            // kiểm tra chưa khóa : in

            DataTable dsTable = SPs.KcbThamkhamLaythongtinBenhanNgoaitru(txtMaBN.Text).GetDataSet().Tables[0];
            THU_VIEN_CHUNG.CreateXML(dsTable, "thamkham_benh_an_thuong");
            ReportDocument crpt;
            string reportcode = "thamkham_benh_an_thuong";
            switch (loaibenhan)
            {
                case "VGB":
                    reportcode = "thamkham_benh_an_thuong";
                    break;
                case "DTD":
                    reportcode = "thamkham_benh_an_thuong";
                    break;
                case "THA":
                    reportcode = "thamkham_benh_an_thuong";
                    break;
                case "BAS":
                    reportcode = "thamkham_benh_an_thuong";
                    break;
                case "COP":
                    reportcode = "thamkham_benh_an_thuong";
                    break;
                case "TMH":
                    reportcode = "thamkham_benhan_taimuihong";
                    break;
                case "RHM":
                    reportcode = "thamkham_benhan_rhm";
                    break;
            }
            string tieude = "", reportname = "";
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;
            try
            {
                var objForm = new frmPrintPreview("bệnh án", crpt, true, true);
                crpt.SetDataSource(dsTable);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                var objBenhAnNgoaiTru = new KcbBenhAn();
                objBenhAnNgoaiTru.IsLoaded = true;
                objBenhAnNgoaiTru.MarkOld();
                objBenhAnNgoaiTru.Id = Utility.Int32Dbnull(txtID_BA.Text, -1);
                // objBenhAnNgoaiTru.InPhieuLog = Utility.Int16Dbnull(1);
                objBenhAnNgoaiTru.NgayIn = globalVariables.SysDate;
                objBenhAnNgoaiTru.NguoiIn = globalVariables.UserName;
                objBenhAnNgoaiTru.Save();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
          
        }

        private void txtMaBenhAn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                SuaBenhAn = true;
            }
        }


        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            ClearControl();
            if (e.KeyCode == Keys.Enter && txtMaLanKham.Text.Trim() != "")
            {
                var objMaBn =
                    new Select("*").From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.MaLuotkham).
                        IsEqualTo(
                            Utility.Int32Dbnull(txtMaLanKham.Text.Trim())).ExecuteSingle<KcbChandoanKetluan>();
                //  KcbBenhAn objBenhAnNgoaiTru = KcbBenhAn.FetchByID(Utility.Int32Dbnull(txtSoKham.Text));
                if (objMaBn != null)
                {
                    txtMaBN.Text = Utility.sDbnull(objMaBn.IdBenhnhan, "");
                    MaBenhNhanText = txtMaBN.Text;
                    FindPatientID(txtMaBN.Text.Trim());
                    SuaBenhAn = false;
                }
                else
                {
                    MessageBox.Show("Mã lần khám không tồn tại,Kiểm tra lại ");
                    txtMaLanKham.Focus();
                }
            }
        }

        private void txtMaLanKham_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMaBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNgaySinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtThangSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNamSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }


        private void txtNgaySinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNgaySinh.Text))
            {
                if (txtNgaySinh.Text.Length > 2)
                {
                    Utility.ShowMsg("Ngày sinh phải nhỏ hơn 2 số", "Thông báo", MessageBoxIcon.Warning);
                    txtNgaySinh.Focus();
                    txtNgaySinh.SelectAll();
                }
            }
        }

        private void txtThangSinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtThangSinh.Text))
            {
                if (txtThangSinh.Text.Length > 2)
                {
                    Utility.ShowMsg("Tháng phải nhỏ hơn 2 số", "Thông báo", MessageBoxIcon.Warning);
                    txtThangSinh.Focus();
                    txtThangSinh.SelectAll();
                }
            }
        }

        private void txtNamSinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNamSinh.Text))
            {
                if ((txtNamSinh.Text.Length > 4) || (txtNamSinh.Text.Length < 4))
                {
                    Utility.ShowMsg("Năm sinh phải là 4 số", "Thông báo", MessageBoxIcon.Warning);
                    txtNamSinh.Focus();
                    txtNamSinh.SelectAll();
                }
            }
        }


        private void AutoDongFrom()
        {
            try
            {
                Try2CreateFolder();
                AllowTextChanged = false;
                using (var _reader = new StreamReader(strBenhAn))
                {
                    chkDongSauKhiLuu.Checked = _reader.ReadLine().Trim() == "1";
                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
            }
            catch
            {
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strBenhAn)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strBenhAn));
            }
            catch
            {
            }
        }

        private void chkInSauKhiLuu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                using (var _writer = new StreamWriter(strBenhAn))
                {
                    _writer.WriteLine(chkDongSauKhiLuu.Checked ? "1" : "0");
                    _writer.Flush();
                    _writer.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            //ClearControl();
            if (e.KeyCode == Keys.Enter && txtMaBenhAn.Text.Trim() != "")
            {
                var objMa_Benh_An =
                    new Select("*").From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(
                        Utility.Int32Dbnull(txtMaBenhAn.Text.Trim())).ExecuteSingle<KcbBenhAn>();
                //  KcbBenhAn objBenhAnNgoaiTru = KcbBenhAn.FetchByID(Utility.Int32Dbnull(txtSoKham.Text));
                if (objMa_Benh_An != null)
                {
                    txtMaBN.Text = Utility.sDbnull(objMa_Benh_An.IdBnhan, "");
                    MaBenhNhanText = txtMaBN.Text;
                    FindPatientID(txtMaBN.Text.Trim());
                    SuaBenhAn = false;
                }
                else
                {
                    MessageBox.Show("Không Tồn Số Bệnh Án Vừa Nhập ");
                    txtMaBenhAn.Focus();
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            em_Action = action.Delete;
            PerformAction();
        }

        private void ClearControl()
        {
            txtNgaySinh.Clear();
            txtThangSinh.Clear();
            txtDienThoai.Clear();
            txtDanToc.Clear();
            txtMaLanKham.Clear();
            txtThongTinLienHe.Clear();
            txtChanDoanBanDau.Clear();
            txtNoiLamViec.Clear();

            foreach (Control control in KHAMBENH.Controls)
            {
                if (control is EditBox)
                {
                    var txtControl = new EditBox();
                    txtControl = ((EditBox) control);
                    txtControl.Clear();
                }
                if (control is CalendarCombo)
                {
                    var txtControl = new CalendarCombo();
                    txtControl = ((CalendarCombo) control);
                    txtControl.NullValue = true;
                }

                if (control is UICheckBox)
                {
                    var txtControl = new UICheckBox();
                    txtControl = ((UICheckBox) control);
                    txtControl.Checked = false;
                }
                if (control is MaskedEditBox)
                {
                    var txtControl = new MaskedEditBox();
                    txtControl = ((MaskedEditBox) control);
                    txtControl.Clear();
                }
                if (control is RichTextBox)
                {
                    var txtControl = new RichTextBox();
                    txtControl = ((RichTextBox) control);
                    txtControl.Clear();
                }
            }

            foreach (Control control in LDVV.Controls)
            {
                if (control is EditBox)
                {
                    var txtControl = new EditBox();
                    txtControl = ((EditBox) control);
                    txtControl.Clear();
                }
                if (control is CalendarCombo)
                {
                    var txtControl = new CalendarCombo();
                    txtControl = ((CalendarCombo) control);
                    txtControl.NullValue = true;
                }

                if (control is UICheckBox)
                {
                    var txtControl = new UICheckBox();
                    txtControl = ((UICheckBox) control);
                    txtControl.Checked = false;
                }
                if (control is MaskedEditBox)
                {
                    var txtControl = new MaskedEditBox();
                    txtControl = ((MaskedEditBox) control);
                    txtControl.Clear();
                }

                if (control is RichTextBox)
                {
                    var txtControl = new RichTextBox();
                    txtControl = ((RichTextBox) control);
                    txtControl.Clear();
                }
            }
            foreach (Control control in HOIBENH.Controls)
            {
                if (control is EditBox)
                {
                    var txtControl = new EditBox();
                    txtControl = ((EditBox) control);
                    txtControl.Clear();
                }
                if (control is CalendarCombo)
                {
                    var txtControl = new CalendarCombo();
                    txtControl = ((CalendarCombo) control);
                    txtControl.NullValue = true;
                }

                if (control is UICheckBox)
                {
                    var txtControl = new UICheckBox();
                    txtControl = ((UICheckBox) control);
                    txtControl.Checked = false;
                }
                if (control is MaskedEditBox)
                {
                    var txtControl = new MaskedEditBox();
                    txtControl = ((MaskedEditBox) control);
                    txtControl.Clear();
                }
                if (control is RichTextBox)
                {
                    var txtControl = new RichTextBox();
                    txtControl = ((RichTextBox) control);
                    txtControl.Clear();
                }
            }
        }

        private void Frm_BenhAnThuong_Load_1(object sender, EventArgs e)
        {
            try
            {
                GetDanhMucChung();
                if (txtMaBN.Text.Trim() == "")
                {
                    txtMaBN.SelectAll();
                    txtMaBN.Focus();
                }
                else
                {
                    MaBenhNhanText = txtMaBN.Text;
                    LayThongTinBenhNhan(txtMaBN.Text);
                    dtDieuTriNgoaiTruTu.Value = dtDieuTriNgoaiTruDen.Value = globalVariables.SysDate;
                    if (em_Action == action.Insert)
                    {
                        SinhMaBenhAn();
                        cmdInBenhAn.Enabled = false;
                        txtNgaySinh.Focus();
                    }
                    if (em_Action == action.Update)
                    {
                        LayDuLieu();
                        cmdInBenhAn.Enabled = true;
                        MaBenhAnText = txtMaBenhAn.Text;
                        txtNgaySinh.Focus();
                        txtMaBenhAn.Enabled = false;
                    }
                    chkDongSauKhiLuu.Checked = true;
                    AutoDongFrom();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void Frm_BenhAnThuong_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F1)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }
        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            //Regex regex = new Regex(@"[^0-9^]");
            //MatchCollection matches = regex.Matches(txtCanNang.Text);
            //if (matches.Count > 0)
            //{
                txtBMI.Text = String.Format("{0:0.00}", (Convert.ToDouble(Utility.DoubletoDbnull(txtCanNang.Text, 0)) /
                                   ((Convert.ToDouble(Utility.DoubletoDbnull(txtChieuCao.Text, 100)) / 100)
                                   * (Convert.ToDouble(Utility.DoubletoDbnull(txtChieuCao.Text, 100)) / 100))));
           // }
                 
        }

        private void txtChieuCao_TextChanged(object sender, EventArgs e)
        {
            // Regex regex = new Regex(@"[^0-9^]");
            //MatchCollection matches = regex.Matches(txtChieuCao.Text);
            //if (matches.Count > 0)
            //{
                txtBMI.Text = String.Format("{0:0.00}", (Convert.ToDouble(Utility.DoubletoDbnull(txtCanNang.Text, 0))/
                                                         ((Convert.ToDouble(Utility.DoubletoDbnull(txtChieuCao.Text, 100))/
                                                           100)*
                                                          (Convert.ToDouble(Utility.DoubletoDbnull(txtChieuCao.Text, 100))/
                                                           100))));
           // }
        }

        private void txtCanNang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^+^\-^\/^\*^\(^\)]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
        }

        private void txtChieuCao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^+^\-^\/^\*^\(^\)]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
        }

    

    }
}