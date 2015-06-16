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
    public partial class frm_Add_GiuongNoiTru : Form
    {
        private string sKieuDanhMuc = "LYDOTANG";
        public NoitruDmucGiuongbenh objBed;

        public delegate void timkiem();

        public timkiem MyGetData;
        #region "khai báo biến"
        public DataTable p_dtDataGiuong=new DataTable();
        public action m_enAct = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public string Loai = ""; 


        #endregion
        public frm_Add_GiuongNoiTru()
        {
            InitializeComponent();
            //this.sKieuDanhMuc = KieuDanhMuc;
            
            this.KeyDown+=new KeyEventHandler(frm_Add_DCHUNG_KeyDown);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            txtTEN.LostFocus +=new EventHandler(txtTEN_LostFocus);

        }
       //ry>
        /// hàm thực hiện việc phím tắt của đơn vị tính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Add_DCHUNG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F5) NhapLienTuc();
        }
          /// <summary>
        /// hàm thực hiện việc load thông tin của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Add_DCHUNG_Load(object sender, EventArgs e)
          {
             InitData();
              
            if(m_enAct==action.Insert)
            {
                IntialDataControl();
            }
            
            Getdata();
            txtMa.Focus();
            txtMa.SelectAll();
            bHasloaded = true;
        }
        //private DataTable m_dtKieuDungChung=new DataTable();
        private DataTable m_dtQuanHe=new DataTable();
        private DataTable m_dtKhoaNoiTru=new DataTable();
        private void InitData()
        {

            m_dtKhoaNoiTru =
                THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong, "", true);
            txtDonvitinh.Init();
            DataTable dtBuong = SPs.NoitruTimkiembuongTheokhoa(-1).GetDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboPhongNoiTru, dtBuong, NoitruDmucBuong.Columns.IdBuong,
                                       NoitruDmucBuong.Columns.TenBuong, "", true);
           
        }

       
        /// <summary>
        /// hàm thưucj hiện lấy thông tin của dữ liệu
        /// </summary>
        private void Getdata()
        {
            // objRoom = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            //if (objRoom != null)
            //{
            //    txt_Mo_Ta.Text = Utility.sDbnull(objRoom.MotaThem, "");
            //    txt_STT_HTHI.Text = Utility.sDbnull(objRoom.SttHthi, 1);
            //    txtTEN.Text = Utility.sDbnull(objRoom.Ten, "")b;
            //}
            if (m_enAct == action.Update)
            {
                chkcodefrom.Enabled = false;
                chkAutoupdate.Visible=lblSample.Visible = lblSuffix.Visible = false;
                
                if (objBed != null)
                {
                    txtID.Text = Utility.sDbnull(objBed.IdGiuong);
                    txtMa.Text = objBed.MaGiuong;
                    txtTen_BHYT.Text = Utility.sDbnull(objBed.TenBhyt);
                    txt_Mo_Ta.Text = Utility.sDbnull(objBed.MotaThem);
                    txt_STT_HTHI.Text = Utility.sDbnull(objBed.SttHthi);
                    txtTEN.Text = Utility.sDbnull(objBed.TenGiuong);
                    txtSuChua.Text = Utility.sDbnull(objBed.SonguoiToida);
                    txtDongia.Text = Utility.sDbnull(objBed.DonGia);
                    chktutuc.Checked = Utility.Byte2Bool(objBed.TthaiTunguyen);
                    chkTrangThai.Checked = Utility.Int32Dbnull(objBed.TrangThai) == 1;
                    if (Utility.Int32Dbnull(objBed.IdKhoanoitru) > 0)
                        cboKhoaNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoiTru, objBed.IdKhoanoitru.ToString());
                    if (Utility.Int32Dbnull(objBed.IdBuong) > 0)
                        cboPhongNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboPhongNoiTru, objBed.IdBuong.ToString());
                    txtDonvitinh.SetCode(objBed.MaDonvitinh);
                }
            }

            m_dtQuanHe = SPs.NoitruLaydulieuqheDoituongBuonggiuong(Utility.Int32Dbnull(txtID.Text)).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdQuanheGiuong, m_dtQuanHe, false, true, "1=1", "");
            NoitruQheDoituongBuonggiuongCollection roomBedObjectTypeCollection = new Select().From(NoitruQheDoituongBuonggiuong.Schema)
                .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(objBed.IdGiuong)).ExecuteAsCollection
                <NoitruQheDoituongBuonggiuongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetDataRows())
            {
                var query = from giuong in roomBedObjectTypeCollection.AsEnumerable()
                            where
                                Utility.sDbnull(giuong.MaDoituongKcb) ==
                                Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value)
                            select giuong;
                if (query.Any())
                {
                    gridExRow.IsChecked = true;
                }
                else
                {
                    gridExRow.IsChecked = false;
                }
            }
            if (roomBedObjectTypeCollection.Count() <= 0) grdQuanheGiuong.GetCheckedRows();
            chkApDungGia.Checked = roomBedObjectTypeCollection.Count() > 0;
            //  Utility.TryToSetBindData(dtNgayLapPhieu, "Text", objTreatment,
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
        /// <summary>
        /// hàm thực hiện bắt sự kiện của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
          
            if(!IsValidData())return;
            PerformAction();
        }
        private bool IsValidData()
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                Utility.ShowMsg("Bạn phải nhập thông tin mã giường ", "Thông báo", MessageBoxIcon.Warning);
                txtMa.Focus();
                return false;
            }
            if (chkcodefrom.Checked)
            {
                if (Utility.DoTrim(txtCodefrom.Text) == "")
                {
                    Utility.SetMsg(lblMessage, "Bạn phải nhập mã bắt đầu", true);
                    txtCodefrom.Focus();
                    txtCodefrom.SelectAll();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtCodefrom.Text, 0) <= 0)
                {
                    Utility.SetMsg(lblMessage, "Hậu tố mã phòng bắt đầu phải >0", true);
                    txtCodefrom.Focus();
                    txtCodefrom.SelectAll();
                    return false;
                }
                if (Utility.DoTrim(txtCode2.Text) == "")
                {
                    Utility.SetMsg(lblMessage, "Bạn phải nhập mã kết thúc", true);
                    txtCode2.Focus();
                    txtCode2.SelectAll();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtCodefrom.Text, 0) > Utility.DecimaltoDbnull(txtCode2.Text, 0))
                {
                    Utility.SetMsg(lblMessage, "Hậu tố mã phòng bắt đầu < mã phòng đến", true);
                    txtCode2.Focus();
                    txtCode2.SelectAll();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtTEN.Text))
            {
                Utility.ShowMsg("Bạn phải nhập thông tin tên giường ", "Thông báo", MessageBoxIcon.Warning);
                txtTEN.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtSuChua.Text) <= 0)
            {
                Utility.ShowMsg("Sức chứa không được <=0 ", "Thông báo", MessageBoxIcon.Warning);
                txtSuChua.Focus();
                return false;
            }
            DmucKhoaphong objDmucKhoaphong = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue));
            if (objDmucKhoaphong == null)
            {
                Utility.ShowMsg("Bạn chọn khoa nội trú ", "Thông báo", MessageBoxIcon.Warning);
                cboKhoaNoiTru.Focus();
                return false;
            }
            NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(cboPhongNoiTru.SelectedValue));
            if (objRoom == null)
            {
                Utility.ShowMsg("Bạn chọn phòng nội trú ", "Thông báo", MessageBoxIcon.Warning);
                cboPhongNoiTru.Focus();
                return false;
            }
            if (m_enAct == action.Insert)
            {
                if (!chkcodefrom.Checked)//Thêm đơn lẻ từng chiếc mới kiểm tra
                {
                    SqlQuery sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                   .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(cboPhongNoiTru.SelectedValue, ""));
                    sqlQuery.And(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(Utility.sDbnull(txtMa.Text));
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Mã giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                        txtMa.Focus();
                        return false;
                    }
                    sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                   .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(cboPhongNoiTru.SelectedValue, ""));
                    sqlQuery.And(NoitruDmucGiuongbenh.Columns.TenGiuong).IsEqualTo(Utility.sDbnull(txtTEN.Text));
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Tên giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                        txtTEN.Focus();
                        return false;
                    }
                }

            }
            else
            {
                SqlQuery sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
              .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(cboPhongNoiTru.SelectedValue, ""))
                .And(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(Utility.sDbnull(txtMa.Text))
                    .And(NoitruDmucGiuongbenh.Columns.IdGiuong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mã giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }
                sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                  .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(cboPhongNoiTru.SelectedValue, ""))
                .And(NoitruDmucGiuongbenh.Columns.TenGiuong).IsEqualTo(Utility.sDbnull(txtTEN.Text))
                    .And(NoitruDmucGiuongbenh.Columns.IdGiuong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Tên giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                    txtTEN.Focus();
                    return false;
                }
            }

            return true;
        }

        public bool b_Cancel = false;
        private void PerformAction()
        {
            switch (m_enAct)
            {
                case action.Insert:
                    if (chkcodefrom.Checked)
                        MultiInsertData();
                    else
                        InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
            b_Cancel = true;

        }

        private Query _Query = NoitruDmucGiuongbenh.CreateQuery();

        private NoitruDmucGiuongbenh createBed()
        {
            NoitruDmucGiuongbenh objBed=new NoitruDmucGiuongbenh();
            if (m_enAct == action.Update)
            {
                objBed.MarkOld();
                objBed.IsLoaded = true;
                objBed.IsNew = false;
                objBed.IdGiuong = Utility.Int16Dbnull(txtID.Text);
            }
            objBed.IdKhoanoitru = Utility.Int16Dbnull(cboKhoaNoiTru.SelectedValue);
            objBed.IdBuong = Utility.Int16Dbnull(cboPhongNoiTru.SelectedValue);
            objBed.MotaThem = Utility.sDbnull(txt_Mo_Ta.Text);
            objBed.SonguoiToida = Utility.Int16Dbnull(txtSuChua.Value);
            objBed.SttHthi = Utility.Int16Dbnull(txt_STT_HTHI.Value);
            objBed.MaGiuong = Utility.sDbnull(txtMa.Text);
            objBed.TenGiuong = Utility.sDbnull(txtTEN.Text);
            objBed.TenBhyt = Utility.sDbnull(txtTen_BHYT.Text);
            objBed.MaDonvitinh = txtDonvitinh.myCode;
            objBed.TthaiTunguyen = Utility.Bool2byte(chktutuc.Checked);
            objBed.DonGia = Utility.DecimaltoDbnull( txtDongia.Text,0);
            objBed.TrangThai = (byte?) (chkTrangThai.Checked ? 1 : 0);
            return objBed;
        }
        private void MultiInsertData()
        {
            try
            {
                 Int16 STTHthi=Utility.Int16Dbnull(txt_STT_HTHI.Text);
                 for (int i = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCodefrom.Text), 0); i <= Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCode2.Text), 0); i++)
                 {
                     NoitruDmucGiuongbenh objBed = new NoitruDmucGiuongbenh();
                     objBed.IdKhoanoitru = Utility.Int16Dbnull(cboKhoaNoiTru.SelectedValue);
                     objBed.IdBuong = Utility.Int16Dbnull(cboPhongNoiTru.SelectedValue);
                     objBed.MotaThem = Utility.sDbnull(txt_Mo_Ta.Text);
                     objBed.SonguoiToida = Utility.Int16Dbnull(txtSuChua.Value);
                     objBed.SttHthi = Utility.Int16Dbnull(txt_STT_HTHI.Value);
                     objBed.MaGiuong = Utility.sDbnull(txtMa.Text)+i.ToString();
                     objBed.TenGiuong = Utility.sDbnull(txtTEN.Text)+ " "+i.ToString();
                     objBed.TenBhyt = Utility.sDbnull(txtTEN.Text) + " " + i.ToString();
                     objBed.MaDonvitinh = txtDonvitinh.myCode;
                     objBed.TthaiTunguyen = Utility.Bool2byte(chktutuc.Checked);
                     objBed.DonGia =Utility.DecimaltoDbnull(txtDongia.Text, 0);
                     objBed.TrangThai = (byte?)(chkTrangThai.Checked ? 1 : 0);
                     NoitruDmucGiuongbenh objcheck = new Select().From(NoitruDmucGiuongbenh.Schema).Where(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(objBed.MaGiuong).ExecuteSingle<NoitruDmucGiuongbenh>();
                     if (objcheck != null)
                     {
                         if (chkAutoupdate.Checked)
                         {
                             objBed.IdGiuong = objcheck.IdGiuong;
                             objBed.MarkOld();
                             objBed.IsNew = true;
                         }
                         else
                         {
                             continue;
                         }
                     }
                     {
                         STTHthi += 1;
                         objBed.IsNew = true;
                     }

                     objBed.SttHthi = STTHthi;
                     objBed.Save();

                     if (objBed != null)
                     {
                         

                         if (chkApDungGia.Checked)
                         {
                             new Delete().From(NoitruQheDoituongBuonggiuong.Schema)
                      .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).Execute();
                             foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetCheckedRows())
                             {
                                 NoitruQheDoituongBuonggiuong objectType = new NoitruQheDoituongBuonggiuong();
                                 objectType.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.DonGia);
                                 objectType.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuDungtuyen].Value,0);
                                 objectType.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuTraituyen].Value,0);
                                 objectType.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value);
                                 // DmucDoituongkcb objectType1=DmucDoituongkcb.FetchByID()
                                 SqlQuery sqlQuery =
                                    new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(
                                        objectType.MaDoituongKcb);
                                 DmucDoituongkcb objectType1 = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                                 if (objectType1 != null)
                                 {
                                     objectType.IdLoaidoituongKcb = Utility.Int32Dbnull(objectType1.IdLoaidoituongKcb);
                                 }
                                 objectType.KieuThue = "GIUONG";
                                 objectType.IdBuong = Utility.Int16Dbnull(cboPhongNoiTru.SelectedValue);
                                 objectType.IdGiuong = Utility.Int16Dbnull(objBed.IdGiuong, -1);

                                 objectType.IsNew = true;
                                 objectType.Save();
                             }
                         }
                         
                     }
                 }
                 if (MyGetData != null)
                 {
                     MyGetData();
                     Utility.GonewRowJanus(grdList, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtID.Text));
                 }
                Utility.SetMsg(lblMessage, "Bạn thực hiện thêm mới thành công", true);
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();

            }
            catch (Exception)
            {

               
            }

        }

        /// <summary>
        /// hàm thực hiện viẹc thêmm ới thông tin 
        /// </summary>
        private void InsertData()
        {
            try
            {

                NoitruDmucGiuongbenh objBed = createBed();
                objBed.IsNew = true;
                objBed.Save();
               
                if (objBed != null)
                {
                    if (MyGetData != null)
                    {
                        MyGetData();
                        Utility.GonewRowJanus(grdList, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtID.Text));
                    }

                    if (chkApDungGia.Checked)
                    {
                        new Delete().From(NoitruQheDoituongBuonggiuong.Schema)
                 .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).Execute();
                        foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetCheckedRows())
                        {
                            NoitruQheDoituongBuonggiuong objectType = new NoitruQheDoituongBuonggiuong();
                            objectType.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value,objBed.DonGia);
                            objectType.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuDungtuyen].Value,0);
                            objectType.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuTraituyen].Value,0);
                            objectType.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value);
                            // DmucDoituongkcb objectType1=DmucDoituongkcb.FetchByID()
                            SqlQuery sqlQuery =
                               new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(
                                   objectType.MaDoituongKcb);
                            DmucDoituongkcb objectType1 = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                            if (objectType1 != null)
                            {
                                objectType.IdLoaidoituongKcb = Utility.Int32Dbnull(objectType1.IdLoaidoituongKcb);
                            }
                            objectType.KieuThue = "GIUONG";
                            objectType.IdBuong = Utility.Int16Dbnull(cboPhongNoiTru.SelectedValue);
                            objectType.IdGiuong = Utility.Int16Dbnull(objBed.IdGiuong, -1);

                            objectType.IsNew = true;
                            objectType.Save();
                        }
                    }
                  
                }
                Utility.SetMsg(lblMessage,"Bạn thực hiện thêm mới thành công", true);
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        /// <summary>
        /// hàm thưucj hiện việc cập nhập lại thông tin thành công
        /// </summary>
        private void UpdateData()
        {
          objBed=  createBed();
          objBed.MarkOld();
          objBed.IsNew = false;
          objBed.Save();
            if (MyGetData != null)
            {
                MyGetData();
                Utility.GonewRowJanus(grdList, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtID.Text));
            }
            if (chkApDungGia.Checked)
            {
                new Delete().From(NoitruQheDoituongBuonggiuong.Schema)
             .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).Execute();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetCheckedRows())
                {
                    NoitruQheDoituongBuonggiuong objectType = new NoitruQheDoituongBuonggiuong();
                    objectType.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.DonGia);
                    objectType.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuDungtuyen].Value,0);
                    objectType.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuTraituyen].Value,0);
                    objectType.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value);
                    SqlQuery sqlQuery =
                        new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(
                            objectType.MaDoituongKcb);
                    DmucDoituongkcb objectType1 = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                    if (objectType1 != null)
                    {
                        objectType.IdLoaidoituongKcb = Utility.Int32Dbnull(objectType1.IdLoaidoituongKcb);
                    }
                    objectType.KieuThue = "GIUONG";
                    // DmucDoituongkcb objectType1=DmucDoituongkcb.FetchByID()
                    objectType.IdBuong = Utility.Int16Dbnull(cboPhongNoiTru.SelectedValue);
                    objectType.IdGiuong = Utility.Int16Dbnull(objBed.IdGiuong, -1);
                    objectType.IsNew = true;
                    objectType.Save();
                }

            }
            Utility.SetMsg(lblMessage, "Bạn thực hiện sửa thông tin  thành công", true);
            if (chkthemmoilientuc.Checked)
            {
                ClearControl();
            }
            else
                this.Close();
        }

        private void txtTEN_LostFocus(object sender, EventArgs e)
        {
           // txtTEN.Text = Utility.chuanhoachuoi(txtTEN.Text);
        }
       

        private void txtTEN_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// hàm thực hiện cho phép nhập liên tục thông tin 
        /// </summary>
        private void NhapLienTuc()
        {

            m_enAct = action.Insert;
            ClearControl();
            IntialDataControl();

        }
        private void IntialDataControl()
        {
            if (m_enAct == action.Insert)
            {
                int MaxSTT =
                    Utility.Int32Dbnull(
                        _Query.AddWhere(NoitruDmucGiuongbenh.Columns.IdBuong, Comparison.Equals, Utility.Int32Dbnull(cboPhongNoiTru.SelectedValue)).GetMax(
                            NoitruDmucBuong.Columns.SttHthi), 0);

                txt_STT_HTHI.Value = Utility.Int32Dbnull(MaxSTT+ 1);
                //txtID_DICHVU.Text = Utility.sDbnull(Utility.Int32Dbnull(_Query.GetMax(DDichVu.Columns.IdDvu)) + 1, "1");
               // txtMa.Focus();
            }

        }
        private void ClearControl()
        {
         
           // Getdata();
            m_enAct = action.Insert;
            foreach (Control control in this.grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox) control.Text = string.Empty;

            }
            if (chkcodefrom.Checked)
            {
                txtCodefrom.Text = (Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCode2.Text)) + 1).ToString();
                txtCode2.Text = txtCodefrom.Text;
            }
            chkcodefrom.Checked = false;
            txtMa.Enabled = true;
            //cboKhoaNoiTru.SelectedIndex = 0;
            txtMa.Focus();
            txtID.Text = "Tự sinh";

        }

        private bool bHasloaded = false;
        private void cboKhoaNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bHasloaded)
            {

                DataTable dtBuong = SPs.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue)).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboPhongNoiTru, dtBuong, NoitruDmucBuong.Columns.IdBuong,
                                           NoitruDmucBuong.Columns.TenBuong, "", true);
               

                IntialDataControl();
            }
            
        }

        private void cboPhongNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntialDataControl();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
        }

        private void grdQuanheGiuong_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void cmdSaoChep_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTen_BHYT.Text = txtTEN.Text;
        }

        private void txtDonGia_Click(object sender, EventArgs e)
        {
        }

        private void chkcodefrom_CheckedChanged(object sender, EventArgs e)
        {
            txtCodefrom.Enabled = txtCode2.Enabled = chkcodefrom.Checked;
            lblmatiento.Text = chkcodefrom.Checked ?"Mã chung": "Mã giường" ;
            lblSuffix.Visible = lblSample.Visible = chkcodefrom.Checked;
        }
    }
}
