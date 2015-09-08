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
using SortOrder = Janus.Windows.GridEX.SortOrder;
using VNS.Properties;

namespace VNS.UI.QMS
{
    public partial class frm_SoKham_GoiLai : Form
    {
        private DataTable m_dtData=new DataTable();
        public delegate void OnActiveQMS();
        public event OnActiveQMS _OnActiveQMS;
        public frm_SoKham_GoiLai()
        {
            InitializeComponent();
            cmdReset.Click+=cmdReset_Click;
            grdListGoiLaiSoKham.DoubleClick += grdListGoiLaiSoKham_DoubleClick;
            grdListGoiLaiSoKham.ColumnHeaderClick += grdListGoiLaiSoKham_ColumnHeaderClick;
            grdListGoiLaiSoKham.RowCheckStateChanged += grdListGoiLaiSoKham_RowCheckStateChanged;
            cmdReset4me.Click += cmdReset4me_Click;
        }

        void grdListGoiLaiSoKham_RowCheckStateChanged(object sender, Janus.Windows.GridEX.RowCheckStateChangeEventArgs e)
        {
            ModifyCommands();
        }

        void grdListGoiLaiSoKham_ColumnHeaderClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            ModifyCommands();
        }

        void cmdReset4me_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdListGoiLaiSoKham))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 dòng QMS cần khôi phục để dùng cho máy của bạn");
                    grdListGoiLaiSoKham.MoveFirst();
                    return;
                }
                int id = Utility.Int32Dbnull(grdListGoiLaiSoKham.CurrentRow.Cells[KcbQm.Columns.Id].Value);
                new Update(KcbQm.Schema)
                    .Set(KcbQm.Columns.TrangThai).EqualTo(1)
                    .Set(KcbQm.Columns.MaQuay).EqualTo(PropertyLib._HISQMSProperties.MaQuay)
                    .Where(KcbQm.Columns.Id).IsEqualTo(id).Execute();
                DataRow[] arrdr = m_dtData.Select(KcbQm.Columns.Id + "=" + id.ToString());
                if (arrdr.Length > 0)
                {
                    arrdr[0][KcbQm.Columns.TrangThai] = 1;
                }

                m_dtData.AcceptChanges();
                if (_OnActiveQMS != null) _OnActiveQMS();
                ModifyCommands();
            }
            catch (Exception)
            {
            }
        }

        void grdListGoiLaiSoKham_DoubleClick(object sender, EventArgs e)
        {
            
        }
        private void ModifyCommands()
        {
            cmdReset.Enabled = grdListGoiLaiSoKham.GetCheckedRows().Length>0;
        }
        private void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdListGoiLaiSoKham.GetCheckedRows().Length <= 0 && Utility.isValidGrid(grdListGoiLaiSoKham))
                {
                    grdListGoiLaiSoKham.CurrentRow.BeginEdit();
                    grdListGoiLaiSoKham.CurrentRow.IsChecked = true;
                    grdListGoiLaiSoKham.CurrentRow.EndEdit();
                }
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdListGoiLaiSoKham.GetCheckedRows())
                {
                    int id = Utility.Int32Dbnull(gridExRow.Cells[KcbQm.Columns.Id].Value);
                    new Update(KcbQm.Schema)
                        .Set(KcbQm.Columns.TrangThai).EqualTo(0)
                        .Set(KcbQm.Columns.MaQuay).EqualTo(string.Empty)
                        .Where(KcbQm.Columns.Id).IsEqualTo(id).Execute();
                    DataRow[] arrdr = m_dtData.Select(KcbQm.Columns.Id + "=" + id.ToString());
                    if (arrdr.Length > 0)
                    {
                        arrdr[0][KcbQm.Columns.TrangThai] = 0;
                    }
                }
                //if (_OnActiveQMS != null) _OnActiveQMS();
                m_dtData.AcceptChanges();
                ModifyCommands();
            }
            catch (Exception)
            {
            }
        }

        private void frm_SoKham_GoiLai_Load(object sender, EventArgs e)
        {
            GetData();
            ModifyCommands();
        }

        private void GetData()
        {
            m_dtData = SPs.QmsLaysoQMSGoilai("-1", globalVariables.MA_KHOA_THIEN, PropertyLib._HISQMSProperties.LoaiQMS).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdListGoiLaiSoKham,m_dtData,false,true,"trang_thai>=3","So_qms asc");
        }

        
    }
}
