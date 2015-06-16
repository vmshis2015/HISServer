using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SubSonic;
using VNS.Libs;
using SubSonic;
using VNS.HIS.DAL;
namespace VNS.Core.Classes
{
   
   public class LoginService
    {
      static VietBaIT.Encrypt objEncrypt = new VietBaIT.Encrypt(globalVariables.gv_sSymmetricAlgorithmName); 
       public LoginService()
       {
           //globalVariables.SqlConnectionString = new ConnectionSQL().KhoiTaoKetNoi();
           //Utility.InitSubSonic(globalVariables.SqlConnectionString, "ORM");
       }
       public  DateTime GetSysDateTime()
       {
           DataTable dataTable = new DataTable();
           DateTime dt = new DateTime();
           dt = DateTime.Now;
           //dataTable = SPs.GetSYSDateTime().GetDataSet().Tables[0];
           DateTime dateTime = new SubSonic.InlineQuery().ExecuteScalar<DateTime>("select getdate()");

           return dateTime;
       }
       public bool KiemTraUserName_Admin(string UserName)
       {


           SqlQuery sqlQuery = new Select().From(SysAdministrator.Schema).Where(SysAdministrator.Columns.PkSAdminID).IsEqualTo(UserName);
           if (sqlQuery.GetRecordCount() > 0) return true;
           else return false;

       }
       public bool KiemTraPassword_Admin(string UserName, string Password)
       {


           SqlQuery sqlQuery = new Select().From(SysAdministrator.Schema).Where(SysAdministrator.Columns.PkSAdminID).IsEqualTo(UserName).And(SysAdministrator.Columns.SPWD).IsEqualTo(objEncrypt.Mahoa(Password));
           if (sqlQuery.GetRecordCount() > 0) return true;
           else return false;

       }

       public bool KiemTraUserName(string UserName)
       {

         
           SqlQuery sqlQuery = new Select().From(SysUser.Schema).Where(SysUser.Columns.PkSuid).IsEqualTo(UserName);
           if (sqlQuery.GetRecordCount() > 0) return true;
           else return KiemTraUserName_Admin(UserName);

       }
       public bool KiemTraPassword(string UserName,string Password)
       {


           SqlQuery sqlQuery = new Select().From(SysUser.Schema).Where(SysUser.Columns.PkSuid).IsEqualTo(UserName).And(SysUser.Columns.SPwd).IsEqualTo(objEncrypt.Mahoa(Password));
           if (sqlQuery.GetRecordCount() > 0) return true;
           else return KiemTraPassword_Admin(UserName, Password);

       }
       public bool isAdmin(string UserName, string Password)
       {
           bool _success = false;
           SysAdministrator _admin = new Select().From(SysAdministrator.Schema).Where(SysAdministrator.Columns.PkSAdminID).IsEqualTo(UserName)
              .And(SysAdministrator.Columns.SPWD).IsEqualTo(objEncrypt.Mahoa(Password)).ExecuteSingle<SysAdministrator>();

           if (_admin != null)
           {
               globalVariables.LoginSuceess = true;
               globalVariables.UserName = _admin.PkSAdminID;
               globalVariables.IsAdmin = true;
               _success = true;
           }
           else
           {
              
                   _success = false;
           }
           if (!_success) return false;

           SqlQuery sqlQueryUnit =
               new Select().From(SysManagementUnit.Schema).Where(SysManagementUnit.Columns.PkSBranchID).IsEqualTo(
                   _admin.FpSBranchID);
           SysManagementUnit objManagementUnit = sqlQueryUnit.ExecuteSingle<SysManagementUnit>();
           if (objManagementUnit != null)
           {
               globalVariables.Branch_ID = objManagementUnit.PkSBranchID;
               globalVariables.Branch_Address = objManagementUnit.SAddress;
               globalVariables.Branch_Name = objManagementUnit.SName;
               globalVariables.Branch_Email = objManagementUnit.SEMAIL;
               globalVariables.Branch_Phone = objManagementUnit.SPhone;
               globalVariables.ParentBranch_Name = objManagementUnit.SParentBranchName;
               globalVariables.Branch_Website = objManagementUnit.Website;
               globalVariables._NumberofBrlink = 3;
               globalVariables.SysLogo = objManagementUnit.Logo;

           }
           return true;
       }
       /// <summary>
       /// hàm thực hiện việc cho phép thông tin đăng nhập thông tin 
       /// </summary>
       /// <param name="UserName"></param>
       /// <param name="Password"></param>
       /// <returns></returns>
       public bool DangNhap(string UserName, string Password)
       {
           bool _success = false;
           SysUser objUser = null;
           globalVariables.IsAdmin = false;
           SysAdministrator _admin = new Select().From(SysAdministrator.Schema).Where(SysAdministrator.Columns.PkSAdminID).IsEqualTo(UserName)
              .And(SysAdministrator.Columns.SPWD).IsEqualTo(objEncrypt.Mahoa(Password)).ExecuteSingle<SysAdministrator>();

           if (_admin != null)
           {
               globalVariables.LoginSuceess = true;
               globalVariables.UserName = _admin.PkSAdminID;
               globalVariables.IsAdmin = true;
               _success = true;
           }
           else
           {
                objUser = new Select().From(SysUser.Schema).Where(SysUser.Columns.PkSuid).IsEqualTo(UserName)
                  .And(SysUser.Columns.SPwd).IsEqualTo(objEncrypt.Mahoa(Password)).ExecuteSingle<SysUser>();
               if (objUser != null)
               {
                   globalVariables._NumberofBrlink = 1;
                   globalVariables.LoginSuceess = true;
                   globalVariables.IsAdmin = objUser.ISecurityLevel == 1;
                   globalVariables.UserName = objUser.PkSuid;
                   _success = true;
               }
               else
                   _success = false;
           }
           if (!_success) return false;
           
           SqlQuery sqlQueryUnit =
               new Select().From(SysManagementUnit.Schema).Where(SysManagementUnit.Columns.PkSBranchID).IsEqualTo(
                   objUser.FpSBranchID);
           SysManagementUnit objManagementUnit = sqlQueryUnit.ExecuteSingle<SysManagementUnit>();
           if (objManagementUnit != null)
           {
               globalVariables.Branch_Address = objManagementUnit.SAddress;
               globalVariables.Branch_Name = objManagementUnit.SName;
               globalVariables.Branch_Email = objManagementUnit.SEMAIL;
               globalVariables.Branch_Phone = objManagementUnit.SPhone;
               globalVariables.ParentBranch_Name = objManagementUnit.SParentBranchName;
               globalVariables.Branch_Website = objManagementUnit.Website;
               globalVariables._NumberofBrlink = 3;
               globalVariables.SysLogo = objManagementUnit.Logo;
           }
           return true;
       }
      
       public  bool CheckExistUserPass(string UserName,string Password)
       {
          
           SqlQuery sqlQuery = new Select().From(SysUser.Schema)
            .Where(SysUser.Columns.PkSuid).IsEqualTo(globalVariables.UserName)
            .And(SysUser.Columns.SPwd).IsEqualTo(objEncrypt.Mahoa(Password));
           if (sqlQuery.GetRecordCount() <= 0) return false;
           else return true;
       }
       public static int UpdatePass(string Password)
       {
           bool UpdateStatus = false;
           int record = -1;
           if ( globalVariables.IsAdmin)
           {
               record = new Update(SysAdministrator.Schema)
              .Set(SysAdministrator.Columns.SPWD).EqualTo(objEncrypt.Mahoa(Password))
              .Where(SysAdministrator.Columns.PkSAdminID).IsEqualTo(globalVariables.UserName).Execute();
           }
           else
           {
               record = new Update(SysUser.Schema)
              .Set(SysUser.Columns.SPwd).EqualTo(objEncrypt.Mahoa(Password))
              .Where(SysUser.Columns.PkSuid).IsEqualTo(globalVariables.UserName).Execute();
           }
          
           if (record > 0)
           {
               UpdateStatus = true;
           }
          return record;
       }
    }
}
