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
using SubSonic;
using VNS.HIS.UI.DANHMUC;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_ChuyenPhongkham : Form
    {
        DataTable m_PhongKham = null;
        DataTable m_kieuKham = null;
        DataTable m_ExamTypeRelationList = null;
        public decimal dongia = -1;
        bool AutoLoad = false;
        public bool m_blnCancel = true;
        public string MA_DTUONG = "DV";
        public DmucDichvukcb _DmucDichvukcb = null;
        public KcbDangkyKcb objDangkyKcb_Cu = null;
        public frm_ChuyenPhongkham()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_KCB_DANGKY_FormClosing);
            this.Load += new EventHandler(frm_ChuyenPhongkham_Load);
            this.KeyDown += new KeyEventHandler(frm_ChuyenPhongkham_KeyDown);

          
            txtKieuKham._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtKieuKham__OnSelectionChanged);
            txtPhongkham._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtPhongkham__OnSelectionChanged);
            txtPhongkham._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtPhongkham__OnEnterMe);
            txtKieuKham._OnEnterMe += new VNS.HIS.UCs.AutoCompleteTextbox.OnEnterMe(txtKieuKham__OnEnterMe);
            cmdChuyen.Click += new EventHandler(cmdChuyen_Click);
            cmdClose.Click += new EventHandler(cmdClose_Click);
            txtLydo._OnShowData += txtLydo__OnShowData;
            txtLydo._OnSaveAs += txtLydo__OnSaveAs;
        }

        void txtLydo__OnSaveAs()
        {
            if (Utility.DoTrim(txtLydo.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtLydo.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }   
        }

        void txtLydo__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            } 
        }

        void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        void cmdChuyen_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", true);
            
            if (_DmucDichvukcb == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn phòng khám cần chuyển", true);
                txtPhongkham.Focus();
                txtPhongkham.SelectAll();
                return;
            }
            if (txtPhongkham.MyID.ToString() == objDangkyKcb_Cu.IdPhongkham.ToString())
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn phòng khám khác "+txtPhonghientai.Text, true);
                txtPhongkham.Focus();
                txtPhongkham.SelectAll();
                return;
            }
            if(Utility.DoTrim( txtLydo.Text)=="")
            {
                 Utility.SetMsg(lblMsg, "Bạn cần nhập lý do chuyển phòng", true);
                 txtLydo.Focus();
                 return;
            }
            _DmucDichvukcb.IdPhongkham =Utility.Int16Dbnull( txtPhongkham.MyID,-1);
            if (ChuyenPhongkham.ChuyenPhong(objDangkyKcb_Cu.IdKham,Utility.DoTrim( txtLydo.Text), _DmucDichvukcb)==ActionResult.Success)
            {

                m_blnCancel = false;
                this.Close();
            }
           
        }

        void frm_ChuyenPhongkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdChuyen.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_ChuyenPhongkham_Load(object sender, EventArgs e)
        {
            try
            {
                txtLydo.Init();
                m_ExamTypeRelationList = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(MA_DTUONG,"ALL", dongia);
                Get_PHONGKHAM(MA_DTUONG);
                Get_KIEUKHAM(MA_DTUONG);
                AutocompleteKieuKham();
                AutocompletePhongKham();
                txtKieuKham.SetId(objDangkyKcb_Cu.IdKieukham);
                txtPhongkham.Focus();
                txtPhongkham.SelectAll();
            }
            catch(Exception ex)
            {
            }
        }
        void txtPhongkham__OnEnterMe()
        {
            AutoLoad = true;
            AutoLoadKieuKham();
        }
        void txtPhongkham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }
        void txtKieuKham__OnEnterMe()
        {
            AutoLoad = true;
            AutoLoadKieuKham();
        }
        private void AutoLoadKieuKham()
        {
            try
            {
                if (Utility.Int32Dbnull(txtKieuKham.MyID, -1) == -1 || Utility.Int32Dbnull(txtPhongkham.MyID, -1) == -1)
                {
                    _DmucDichvukcb= null;
                    return;
                }
                //_DmucDichvukcb=new Select().From(DmucDichvukcb.Schema).
                //    Where(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(Utility.Int32Dbnull(txtKieuKham.MyID, -1))
                //    .And(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(Utility.Int32Dbnull(txtPhongkham.MyID, -1))
                //    .And(DmucDichvukcb.Columns.DonGia).IsEqualTo(Utility.Int32Dbnull(txtPhongkham.MyID, -1))
                DataRow[] arrDr =
                    m_ExamTypeRelationList.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham=" +
                                                  txtKieuKham.MyID.ToString().Trim() + " AND  id_phongkham=" + txtPhongkham.MyID.ToString().Trim());
                //nếu ko có đích danh phòng thì lấy dịch vụ bất kỳ theo kiểu khám và đối tượng
                if (arrDr.Length <= 0)
                    arrDr = m_ExamTypeRelationList.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + MA_DTUONG + "') AND id_kieukham=" +
                                                  txtKieuKham.MyID.ToString().Trim() + " AND id_phongkham=-1 ");
                if (arrDr.Length <= 0)
                {
                    _DmucDichvukcb = null;
                    return ;
                }
                else
                {
                   
                    _DmucDichvukcb= new Select().From(DmucDichvukcb.Schema).Where(DmucDichvukcb.Columns.IdDichvukcb)
                        .IsEqualTo(arrDr[0][DmucDichvukcb.Columns.IdDichvukcb]).ExecuteSingle<DmucDichvukcb>();
                    return;
                }
            }
            catch
            {
                _DmucDichvukcb= null;
            }
            finally
            {
                AutoLoad = false;
            }
        }
        private void Get_PHONGKHAM(string MA_DTUONG)
        {
            m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM(MA_DTUONG);
            DataTable _tempt = m_PhongKham.Clone();
            foreach (DataRow dr in m_PhongKham.Rows)
            {
                if (m_ExamTypeRelationList.Select(DmucDichvukcb.Columns.IdPhongkham + "=" + Utility.Int32Dbnull(dr[DmucKhoaphong.Columns.IdKhoaphong], -1).ToString()).Length>0)
                {
                    _tempt.ImportRow(dr);
                }
            }
            m_PhongKham = _tempt.Copy();
        }

        private void Get_KIEUKHAM(string MA_DTUONG)
        {
            m_kieuKham = THU_VIEN_CHUNG.Get_KIEUKHAM(MA_DTUONG, dongia);
            DataTable _tempt = m_kieuKham.Clone();
            foreach (DataRow dr in m_kieuKham.Rows)
            {
                if (m_ExamTypeRelationList.Select(DmucDichvukcb.Columns.IdKieukham + "=" + Utility.Int32Dbnull(dr[DmucKieukham.Columns.IdKieukham], -1).ToString()).Length > 0)
                {
                    _tempt.ImportRow(dr);
                }
            }
            m_kieuKham = _tempt.Copy();
        }
        private void AutocompletePhongKham()
        {
            try
            {
                if (m_PhongKham == null) return;
                if (!m_PhongKham.Columns.Contains("ShortCut"))
                    m_PhongKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_PhongKham.Rows)
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
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in m_PhongKham.AsEnumerable()
                            select p.Field<Int16>(DmucKhoaphong.Columns.IdKhoaphong).ToString() + "#" + p.Field<string>(DmucKhoaphong.Columns.MaKhoaphong).ToString() + "@" + p.Field<string>(DmucKhoaphong.Columns.TenKhoaphong).ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtPhongkham.AutoCompleteList = source;
                this.txtPhongkham.TextAlign = HorizontalAlignment.Center;
                this.txtPhongkham.CaseSensitive = false;
                this.txtPhongkham.MinTypedCharacters = 1;

            }
        }

        private void AutocompleteKieuKham()
        {
            try
            {
                if (m_kieuKham == null) return;
                if (!m_kieuKham.Columns.Contains("ShortCut"))
                    m_kieuKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_kieuKham.Rows)
                {
                    
                        string shortcut = "";
                        string realName = dr[DmucKieukham.Columns.TenKieukham].ToString().Trim() + " " +
                                          Utility.Bodau(dr[DmucKieukham.Columns.TenKieukham].ToString().Trim());
                        shortcut = dr[DmucKieukham.Columns.MaKieukham].ToString().Trim();
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
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in m_kieuKham.AsEnumerable()
                            select p.Field<Int16>(DmucKieukham.Columns.IdKieukham).ToString() + "#" + p.Field<string>(DmucKieukham.Columns.MaKieukham).ToString() + "@" + p.Field<string>(DmucKieukham.Columns.TenKieukham).ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtKieuKham.AutoCompleteList = source;
                this.txtKieuKham.TextAlign = HorizontalAlignment.Center;
                this.txtKieuKham.CaseSensitive = false;
                this.txtKieuKham.MinTypedCharacters = 1;

            }
        }
    }
}
