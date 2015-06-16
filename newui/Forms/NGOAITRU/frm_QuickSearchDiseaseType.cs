using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.Libs;
using SubSonic;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_QuickSearchDiseasesType : Form
    {
        #region "Hàm thực hiện khai báo thuộc tính"
        public DataTable p_dtDiseases=new DataTable();
        private DataTable dt_DiseaseInfo=new DataTable();
        private string _rowFilter = "1=1";
        public bool m_blnCancel = false;
        public string v_DiseasesTypeCode = "";
        public int DiseaseType_ID = -1;
        #endregion

        #region "Hàm khởi tạo đối tượng"
        public frm_QuickSearchDiseasesType()
        {
            InitializeComponent();
            Utility.loadIconToForm(this);
            this.KeyPreview = true;
        }
        #endregion

        #region "phần khai báo sự kiện của form"
        /// <summary>
        /// đong form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        /// <summary>
        /// hàm thực hiện dùng phiims tắt của fiorm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuickSearchDiseases_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdClose.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện load thông tin của form lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuickSearchDiseases_Load(object sender, EventArgs e)
        {
            BindData();
            grdSearch.Focus();
        }
        /// <summary>
        /// hàm thực hiện chấp nhận 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (grdSearch.CurrentRow != null && grdSearch.RowCount > 0)
            {
              
                DiseaseType_ID = Utility.Int32Dbnull(grdSearch.CurrentRow.Cells["DiseaseType_ID"].Value.ToString(), -1);
                m_blnCancel = true;
                Close();
            }
        }
        /// <summary>
        /// hàm thực hiện sự kiện click chuột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
            cmdAccept.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện phím tát trên grdivewi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdSearch.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                    cmdAccept.PerformClick();
            }
        }
        /// <summary>
        /// hàm thực hiện bind thông tin 
        /// </summary>
        private void BindData()
        {
            dt_DiseaseInfo =
                new Select().From(LDiseaseType.Schema).OrderAsc(LDiseaseType.Columns.IntOrder).ExecuteDataSet().Tables[0
                    ];
            grdSearch.DataSource = dt_DiseaseInfo;
        }
        #endregion

      

      

      

       
    }
}
