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
using VNS.HIS.NGHIEPVU.NGOAITRU;
using VNS.HIS.DAL;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KetLuan : Form
    {
        public frm_KetLuan()
        {
            InitializeComponent();
            Utility.loadIconToForm(this);
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdNhapKetLuan_Click(object sender, EventArgs e)
        {
            int record=new Update(TAssignDetail.Schema)
                .Set(TAssignDetail.Columns.Summary).EqualTo(txtKetLuan.Text)
                .Set(TAssignDetail.Columns.BsThucHien).EqualTo(globalVariables.UserName)
                .Set(TAssignDetail.Columns.NgayThucHien).EqualTo(BusinessHelper.GetSysDateTime())
                .Where(TAssignDetail.Columns.AssignDetailId).IsEqualTo(AssignDetail_ID).Execute();
            if(record>0)
            {
                DataRow[] arr = P_dtDataTable.Select("AssignDetail_ID=" + AssignDetail_ID);
                if(arr.GetLength(0)>0)
                {
                    arr[0][TAssignDetail.Columns.Summary] = txtKetLuan.Text;
                    arr[0][TAssignDetail.Columns.NgayThucHien] = BusinessHelper.GetSysDateTime();
                    arr[0][TAssignDetail.Columns.BsThucHien] = globalVariables.UserName;
                }
                P_dtDataTable.AcceptChanges();
                Utility.ShowMsg("Bạn thực hiện thành công","Thông báo");
            }
        }

        public int AssignDetail_ID = -1;
        public DataTable P_dtDataTable=new DataTable();
        /// <summary>
        /// ham thực hiện 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KetLuan_Load(object sender, EventArgs e)
        {
            TAssignDetail assignDetail = TAssignDetail.FetchByID(AssignDetail_ID);
            if(assignDetail!=null)
            {
                txtKetLuan.Text = Utility.sDbnull(assignDetail.Summary, "");
            }
        }
    }
}
