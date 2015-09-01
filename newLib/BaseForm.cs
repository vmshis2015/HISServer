using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;

namespace VNS.Libs
{
    public partial class BaseForm : System.Windows.Forms.Form
    {
        #region "Private Variables(Class Level)"
       public string AssName = "";
        public SysTrace myTrace;
        #endregion
        public BaseForm(): base()
        {
            VMSInitialize();
            this.Text = this.Text + "-" + globalVariables.Branch_Name + "-@Copyright by VMS";
            //this.Icon=
            //string sPath = Application.StartupPath;
            //System.Drawing.Icon ico = new System.Drawing.Icon(sPath+@"\icon\khachhang.icon");
            //this.Icon = ico;
            //this.ShowIcon = true;
           // this.Text =globalVariables.Branch_Name+"-VMS";)
            this.KeyPreview = true;
        }
        #region "Event Handlers"
       
        #endregion
        #region "Private Methods including Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        /// <summary>
        /// Thiết lập các giá trị mặc định cho class
        /// </summary>
        private void VMSInitialize()
        {
            //Lấy về tên DLL. ManifestModule.Name ban đầu có dạng "DLLName.dll"-->Ta chỉ lấy phần "DLLName"
            AssName = this.GetType().Assembly.ManifestModule.Name;// System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;
            //Tạo Trace cho chức năng này
            myTrace = new SysTrace() {BranchID= globalVariables.Branch_ID,
                UserName= globalVariables.UserName,
               CreatedDate= System.DateTime.Now,IpAddress= Utility.GetIPAddress(),DLLName= AssName.Split('.')[0],
              SubSystemName= globalVariables.SubSystemName,FunctionID= globalVariables.FunctionID,FunctionName= globalVariables.FunctionName,ComputerName= Utility.GetComputerName(),AccountName= Utility.GetAccountName() };
         
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
