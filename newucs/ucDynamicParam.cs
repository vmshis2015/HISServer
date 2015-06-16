using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;

namespace VNS.UCs
{
    public partial class ucDynamicParam : UserControl
    {
        DataRow dr = null;
        bool AutoSaveWhenEnterKey = false;
        public bool isSaved=false;
        public delegate void OnEnterKey(ucDynamicParam obj);
        public event OnEnterKey _OnEnterKey;
        public ucDynamicParam()
        {
            InitializeComponent();
           
        }
        public ucDynamicParam(DataRow dr,bool AutoSaveWhenEnterKey)
        {
            InitializeComponent();
            this.dr = dr;
            this.AutoSaveWhenEnterKey = AutoSaveWhenEnterKey;
            txtValue.KeyDown += txtValue_KeyDown;
            txtValue.TextChanged += txtValue_TextChanged;
        }
        public bool _ReadOnly
        {
            set { txtValue.ReadOnly = value; }
        }
        void txtValue_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }

        void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                if (AutoSaveWhenEnterKey)
                    Save();
                if (_OnEnterKey != null) _OnEnterKey(this);
            }
        }
        public void FocusMe()
        {
            txtValue.SelectAll();
            txtValue.Focus();
        }
        public void Init()
        {
            try
            {
               if (dr != null )
               {
                   txtValue.Text = Utility.sDbnull(dr[DynamicValue.Columns.Giatri], "");
                   lblName.Text = Utility.sDbnull(dr[DynamicField.Columns.Mota], "") + "(" + Utility.sDbnull(dr[DynamicField.Columns.Ma], "") + ")";
                   toolTip1.SetToolTip(lblName, Utility.sDbnull(dr[DynamicField.Columns.Ma], ""));
               }
               else
               {
                   lblName.Text = "UnKnown";
                   txtValue.Text = "";
               }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
       
        public string _Mota
        {
            set { lblName.Text = value; }
            get { return lblName.Text; }
        }
        public string _Giatri
        {
            set { txtValue.Text = value; }
            get { return txtValue.Text; }
        }
        public void Save()
        {
            try
            {
                List<DynamicValue> lstValues = new List<DynamicValue>();
                if (dr != null )
                {
                    DynamicValue objVal = null;


                    if (Utility.Int32Dbnull(dr["idValue"], -1) > 0)
                    {
                        objVal = DynamicValue.FetchByID(Utility.Int32Dbnull(dr["idValue"], -1));
                        objVal.IsNew = false;
                        objVal.MarkOld();
                    }
                    else
                    {
                        objVal = new DynamicValue();
                        objVal.IsNew = true;
                    }

                    objVal.Id = Utility.Int32Dbnull(dr["idValue"], -1);
                    objVal.Ma = Utility.sDbnull(dr[DynamicField.Columns.Ma], "-1");
                    objVal.Giatri = Utility.DoTrim(txtValue.Text);
                    objVal.ImageId = -1;
                    objVal.IdChidinhchitiet = Utility.Int32Dbnull(dr[DynamicValue.Columns.IdChidinhchitiet], -1);

                    lstValues.Add(objVal);
                    clsHinhanh.UpdateDynamicValues(lstValues);
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                
            }  
        }
        
    }
}
