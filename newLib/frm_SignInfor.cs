using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.DAL;
namespace VNS.Libs
{
    public partial class frm_SignInfor : Form
{
	public bool mv_bChapNhan = false;
	public string mv_sFontName;
	public string mv_sFontStyle;
	public string mv_sFontSize;
   // public string mv_sSoLuongCanIn ="1";
	private void cmdQuit_Click(object sender, System.EventArgs e)
	{
		this.Close();
	}

	private void cmdOK_Click(object sender, System.EventArgs e)
	{
		//thuc hien lay gia tri
		mv_bChapNhan = true;
		this.Close();
	}
	
	private void frm_SignInfor_Load(object sender, System.EventArgs e)
	{
      
		try {
            w = this.Width;
			txtBaoCao.Enabled = false;
			txtBaoCao.BackColor = Color.WhiteSmoke;
           // txtTrinhky.SetFontFamily(new FontFamily("Arial"));
            txtTrinhky.Font = new Font("Arial", 10f, FontStyle.Regular);
		}

		catch (Exception ex) {
			//SetLanguage(gv_sLanguageDisplay, this, "GOLFMAN", gv_oSqlCnn);
		}

	}
	public frm_SignInfor()
	{
        InitializeComponent();
       
        
        InitializeEvents();
	    //cmdUpdateAllUser.Visible = globalVariables.IsAdmin;     
	}
    int w = 0;
    private void InitializeEvents()
    { 
        this.Load+=new EventHandler(frm_SignInfor_Load);
        cmdOK.Click+=new EventHandler(cmdOK_Click);
        cmdQuit.Click+=new EventHandler(cmdQuit_Click);
        chkPortrait.CheckedChanged += chkPortrait_CheckedChanged;
    }

    void chkPortrait_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPortrait.Checked)
            this.Width = 943;
        else
            this.Width = w;
    }
    private void cmdOK_Click_1(object sender, EventArgs e)
    {

    }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    private void frm_SignInfor_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.KeyCode==Keys.Escape)cmdQuit.PerformClick();
        if(e.KeyCode==Keys.A&&e.Control)cmdOK.PerformClick();
        //if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
    }
/// <summary>
/// hàm thực hiện toàn bô các user có dạng báo cáo trình ký
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>


        private void cmdOK_Click_2(object sender, EventArgs e)
    {

    }

        private void cmdUpdateAllUser_Click(object sender, EventArgs e)
        {
            frm_SysUsers frm=new frm_SysUsers();
            frm.TenBaoCao = txtBaoCao.Text;
           
            frm.ShowDialog();
        }

        private void frm_SignInfor_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                Utility.SetResize(this); 
            }catch(Exception exception)
            {
                
            }
            
        }
}

}
