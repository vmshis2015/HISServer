using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;


namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_noiKCBBD : Form
    {
        #region "Declare variable private"
        private DataTable m_dtTinh_Thanhpho=new DataTable();
        private Query Query = DmucNoiKCBBD.CreateQuery();
        private string _rowFilter = "1=1";
        #endregion
        #region "Declare variable Public"
        public action em_Action = action.Insert;
      

        #endregion

        #region "Contructor"
        public frm_themmoi_noiKCBBD()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown+=new KeyEventHandler(frm_themmoi_noiKCBBD_KeyDown);
            
            m_dtTinh_Thanhpho = globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.Columns.LoaiDiachinh + "=0").CopyToDataTable();
        }
        #endregion

        #region "Method of Event Form"
        /// <summary>
        /// hàm thực hiện thoát khỏi Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện Lưu thông tin lại 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        /// <summary>
        /// hàm thực hiện kiểm tra xem đã nhập mã khám chữa bệnh ban đầu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDieaseCode_TextChanged(object sender, EventArgs e)
        {
            MofifyCommand();
        }
        /// <summary>
        /// hàm thực hiện xem đã nhập tên địa chỉ khám chữa bệnh ban đầu chưa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDieaseName_TextChanged(object sender, EventArgs e)
        {
            MofifyCommand();
        }
        /// <summary>
        /// hàm thực hiện Kiểm tra xem đã chọn chưa, hiển thị Validate dữ liệu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSurveys_SelectedIndexChanged(object sender, EventArgs e)
        {
            MofifyCommand();
        }
        /// <summary>
        /// hàm xử lý phím tắt của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_noiKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F5) ClearControl();
        }

        /// <summary>
        /// hàm thực hiện bắt sự kiện khi nhập text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSurveyCode_TextChanged(object sender, EventArgs e)
        {
            _rowFilter = "1=1";
            if (!string.IsNullOrEmpty(txtSurveyCode.Text))
            {
                _rowFilter =DmucDiachinh.Columns.MaDiachinh+ "='" + txtSurveyCode.Text.Trim() + "'";

            }
            m_dtTinh_Thanhpho.DefaultView.RowFilter = _rowFilter;
            m_dtTinh_Thanhpho.AcceptChanges();

        }
        /// <summary>
        /// hàm thực hiện load thông tin của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_noiKCBBD_Load(object sender, EventArgs e)
        {
            IntialData();
            MofifyCommand();
            if (em_Action == action.Update) GetData();
          
        }
        void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if(control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
                em_Action = action.Insert;
                txtClinicCode.Focus();
            }
        }
        #endregion
        #region "Method of Common"
        /// <summary>
        /// khởi tạo thông tin của Form
        /// </summary>
        private void IntialData()
        {
            DataBinding.BindData(cboSurveys, m_dtTinh_Thanhpho, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh);
            if (em_Action == action.Insert) txtIntOrder.Value = Utility.Int32Dbnull(Query.GetMax(DmucNoiKCBBD.Columns.SttHthi)) + 1;
        }
        /// <summary>
        /// xử lý thông tin khi nhấn save
        /// </summary>
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!InValiData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!InValiUpdateData()) return;
                    PerformActionUpdate();
                    break;
            }
            

        }
        /// <summary>
        /// Hàm thực hiện kiểm tra thông tin của phần thêm mới
        /// </summary>
        /// <returns></returns>
        private bool InValiData()
        {

            if (string.IsNullOrEmpty(txtClinicCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtClinicCode, "Bạn phải nhập mã KCBBĐ");
                txtClinicCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtDieaseName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtDieaseName, "Bạn phải nhập tên KCBBĐ");
                txtClinicCode.Focus();
                return false;

            }
            if (cboSurveys.SelectedIndex <= -1)
            {
                Utility.SetMsgError(errorProvider3, cboSurveys, "Bạn phải chọn địa chính");
                cboSurveys.Focus();
                return false;
            }
            DmucNoiKCBBDCollection objDmucNoiKCBBDCollection = new DmucNoiKCBBDController().FetchByQuery(
                DmucNoiKCBBD.CreateQuery().AddWhere(DmucNoiKCBBD.Columns.MaKcbbd,Comparison.Equals,txtClinicCode.Text).AND(DmucNoiKCBBD.Columns.MaDiachinh,Comparison.Equals,Utility.sDbnull(cboSurveys.SelectedValue,"")));
            if(objDmucNoiKCBBDCollection.Count()>0)
            {
                Utility.ShowMsg("Đã tồn tại mã khám chữa bệnh này");
                txtClinicCode.Focus();
                return false;
            }
            DmucNoiKCBBDCollection objClinicCollection = new DmucNoiKCBBDController().FetchByQuery(
                DmucNoiKCBBD.CreateQuery().AddWhere(DmucNoiKCBBD.Columns.TenKcbbd, Comparison.Equals, txtDieaseName.Text).AND(
                    DmucNoiKCBBD.Columns.MaDiachinh, Comparison.Equals, Utility.sDbnull(cboSurveys.SelectedValue, "")));

            if (objClinicCollection.Count() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên khám chữa bệnh này");
                txtDieaseName.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện kiểm tra thông tin của phần Update
        /// </summary>
        /// <returns></returns>
        private bool InValiUpdateData()
        {

            if (string.IsNullOrEmpty(txtClinicCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtClinicCode, "Bạn phải nhập mã KCBBĐ");
                txtClinicCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtDieaseName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtDieaseName, "Bạn phải nhập tên KCBBĐ");
                txtClinicCode.Focus();
                return false;

            }
            if (cboSurveys.SelectedIndex <= -1)
            {
                Utility.SetMsgError(errorProvider3, cboSurveys, "Bạn phải chọn địa chính");
                cboSurveys.Focus();
                return false;
            }
            DmucNoiKCBBDCollection objDmucNoiKCBBDCollection = new DmucNoiKCBBDController().FetchByQuery(
                DmucNoiKCBBD.CreateQuery().AddWhere(DmucNoiKCBBD.Columns.MaKcbbd, Comparison.Equals, txtClinicCode.Text)
                    .AND(DmucNoiKCBBD.Columns.IdKcbbd, Comparison.NotEquals, Utility.Int32Dbnull(txtClinic_ID.Text, -1)).AND
                    (DmucNoiKCBBD.Columns.MaDiachinh, Comparison.Equals, Utility.sDbnull(cboSurveys.SelectedValue, "")));

            if (objDmucNoiKCBBDCollection.Count() > 0)
            {
                
                Utility.ShowMsg("Đã tồn tại mã khám chữa bệnh này");
                txtClinicCode.Focus();
                return false;
            }
           
            if (objDmucNoiKCBBDCollection.Count() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên khám chữa bệnh này");
                txtDieaseName.Focus();
                return false;
            }
            DmucNoiKCBBDCollection objClinicCollection = new DmucNoiKCBBDController().FetchByQuery(
                DmucNoiKCBBD.CreateQuery().AddWhere(DmucNoiKCBBD.Columns.TenKcbbd, Comparison.Equals, txtDieaseName.Text).AND(
                    DmucNoiKCBBD.Columns.IdKcbbd, Comparison.NotEquals, Utility.Int32Dbnull(txtClinic_ID.Text, -1)).AND(
                        DmucNoiKCBBD.Columns.MaDiachinh, Comparison.Equals, Utility.sDbnull(cboSurveys.SelectedValue, "")));




            if (objClinicCollection.Count() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên khám chữa bệnh này");
                txtDieaseName.Focus();
                return false;
            }
            return true;
        }
        private Query _Query = DmucNoiKCBBD.CreateQuery();
        public Janus.Windows.GridEX.GridEX grdList;
        /// <summary>
        /// hàm thực hiện chèn thêm thông tin 
        /// </summary>
        private void PerformActionInsert()
        {
            DmucNoiKCBBD.Insert(Utility.sDbnull(cboSurveys.SelectedValue, ""), txtClinicCode.Text, Utility.sDbnull(txtDieaseName.Text, ""),Utility.Int16Dbnull(txtIntOrder.Text,1), txtsDesc.Text,"");
            DataRow dr = globalVariables.gv_dtDmucNoiKCBBD.NewRow();
            dr[DmucNoiKCBBD.Columns.IdKcbbd] = Utility.Int32Dbnull(_Query.GetMax(DmucNoiKCBBD.Columns.IdKcbbd), -1);
            dr[DmucNoiKCBBD.Columns.MaKcbbd] = txtClinicCode.Text;
            dr[DmucNoiKCBBD.Columns.TenKcbbd] = Utility.sDbnull(txtDieaseName.Text, "");
            dr[DmucNoiKCBBD.Columns.MaDiachinh] = Utility.sDbnull(cboSurveys.SelectedValue,"");
            dr[DmucNoiKCBBD.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
            dr[DmucNoiKCBBD.Columns.SttHthi] = Utility.Int16Dbnull(txtIntOrder.Text,1);
            dr[DmucDiachinh.Columns.TenDiachinh] = Utility.sDbnull(cboSurveys.Text, "");
            globalVariables.gv_dtDmucNoiKCBBD.Rows.Add(dr);
            Utility.GotoNewRowJanus(grdList, DmucNoiKCBBD.Columns.MaKcbbd, txtClinicCode.Text);
            this.Close();
        }
        /// <summary>
        /// Hàm thực hiện Update thông tin 
        /// </summary>
        private void PerformActionUpdate()
        {
          
          int record=  new Update(DmucNoiKCBBD.Schema)
                .Set(DmucNoiKCBBD.Columns.TenKcbbd).EqualTo(Utility.sDbnull(txtDieaseName.Text, ""))
                .Set(DmucNoiKCBBD.Columns.MaKcbbd).EqualTo(txtClinicCode.Text)
                .Set(DmucNoiKCBBD.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(txtIntOrder.Value, 1))
                .Set(DmucNoiKCBBD.Columns.MaDiachinh).EqualTo(Utility.sDbnull(cboSurveys.SelectedValue, ""))
                .Set(DmucNoiKCBBD.Columns.MotaThem).EqualTo(txtsDesc.Text)
               // .Set(DmucNoiKCBBD.Columns.s).EqualTo(txtsDesc.Text)
                .Where(DmucNoiKCBBD.Columns.IdKcbbd).IsEqualTo(Utility.DecimaltoDbnull(txtClinic_ID.Text, -1)).Execute();

            if(record>0)
            {
                DataRow[] dr = globalVariables.gv_dtDmucNoiKCBBD.Select(DmucNoiKCBBD.Columns.IdKcbbd+"=" + Utility.Int32Dbnull(txtClinic_ID.Text, -1));
                if (dr.GetLength(0) > 0)
                {
                    dr[0][DmucNoiKCBBD.Columns.SttHthi] = Utility.Int16Dbnull(txtIntOrder.Text, -1);
                    dr[0][DmucNoiKCBBD.Columns.MaKcbbd] = txtClinicCode.Text;
                    dr[0][DmucNoiKCBBD.Columns.TenKcbbd] = Utility.sDbnull(txtDieaseName.Text, "");
                    dr[0][DmucNoiKCBBD.Columns.MaDiachinh] = Utility.sDbnull(cboSurveys.SelectedValue, "");
                    dr[0][DmucNoiKCBBD.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
                    dr[0][DmucDiachinh.Columns.TenDiachinh] = Utility.sDbnull(cboSurveys.Text, "");

                }
                //globalVariables.gv_dtDmucNoiKCBBD.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, DmucNoiKCBBD.Columns.MaKcbbd, txtClinicCode.Text);
                this.Close();
            }
            else
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                return;
            }


        }
        private void GetData()
        {
            DmucNoiKCBBD objDisease = DmucNoiKCBBD.FetchByID(Utility.Int32Dbnull(txtClinic_ID.Text, -1));
            if (objDisease != null)
            {
                txtsDesc.Text = objDisease.MotaThem;
                txtClinicCode.Text = objDisease.MaKcbbd;
                txtDieaseName.Text = objDisease.TenKcbbd;
                cboSurveys.SelectedIndex = Utility.GetSelectedIndex(cboSurveys,
                                                                       objDisease.MaDiachinh.ToString());
                txtIntOrder.Value = Utility.Int16Dbnull(objDisease.SttHthi, 1);
            }
        }
        /// <summary>
        /// ham thuc hien kiem tra trang thai
        /// </summary>
        private void MofifyCommand()
        {

            if (string.IsNullOrEmpty(txtClinicCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtClinicCode, "Bạn phải nhập mã bệnh");
                txtClinicCode.Focus();


            }
            if (string.IsNullOrEmpty(txtDieaseName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtDieaseName, "Bạn phải nhập tên bệnh");
                //txtClinicCode.Focus();


            }
            if (cboSurveys.SelectedIndex <= 0)
            {
                Utility.SetMsgError(errorProvider3, cboSurveys, "Bạn phải chọn kiểu bệnh");
                //cboDieaseType.Focus();

            }
        }
        #endregion

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearControl();
        }
       
    }
}
