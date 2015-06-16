using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VNS.Libs
{
    public class BaseForm : System.Windows.Forms.Form
    {
        #region "Private Variables(Class Level)"
       public string AssName = "";
        TraceInfor Trace;
        #endregion
        public BaseForm(): base()
        {
            VietBaInitialize();
            this.Text = this.Text + "-" + globalVariables.Branch_Name + "-@Copyright by VinaSoft";
            
            //this.Icon=
            //string sPath = Application.StartupPath;
            //System.Drawing.Icon ico = new System.Drawing.Icon(sPath+@"\icon\khachhang.icon");
            //this.Icon = ico;
            //this.ShowIcon = true;
           // this.Text =globalVariables.Branch_Name+"-VietBaIT JSC";)
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(BaseForm_KeyDown);
        }
        #region "Event Handlers"
        void BaseForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !this.ActiveControl.Name.ToUpper().Equals("LSTDRUGTIP")) ProcessTabKey(true);
            if (e.KeyCode == System.Windows.Forms.Keys.Escape) this.Close();
        }
        #endregion
        #region "Private Methods including Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        /// <summary>
        /// Thiết lập các giá trị mặc định cho class
        /// </summary>
        private void VietBaInitialize()
        {
            //Lấy về tên DLL. ManifestModule.Name ban đầu có dạng "DLLName.dll"-->Ta chỉ lấy phần "DLLName"
            AssName = this.GetType().Assembly.ManifestModule.Name;// System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;
            //Thiết lập chế độ MultiLanguage cho class
            
            //Tạo Trace cho chức năng này
            Trace = new TraceInfor(globalVariables.Branch_ID, globalVariables.UserName, System.DateTime.Now.ToShortDateString(), Utility.GetIPAddress(), AssName.Split('.')[0], globalVariables.SubSystemName, globalVariables.FunctionID, globalVariables.FunctionName, Utility.GetComputerName(), Utility.GetAccountName());
            Utility.InitSubSonic(globalVariables.SqlConnectionString, globalVariables.ProviderName);
        }
#endregion

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}
