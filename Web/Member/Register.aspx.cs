using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
//using BookStore.Model;
using Web.Enum;

namespace Web.Member
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Common.WebCommon.CheckValidateCode(Request["txtCode"]))
                {
                    AddUserInfo();
                }
            }
        }


        protected void AddUserInfo()
        {
            Model.User userInfo=new User();
            userInfo.Address = Request["txtAddress"];
            userInfo.LoginId = Request["txtName"];
            userInfo.LoginPwd = Request["txtPwd"];
            userInfo.Mail = Request["txtEmail"];
            userInfo.Name = Request["txtRealName"];
            userInfo.Phone = Request["txtPhone"];
            userInfo.UserState.Id = Convert.ToInt32(UserStateEnum.NormalState);
            
            BLL.UserManager userManager=new UserManager();
            string msg = string.Empty;
            if (userManager.Add(userInfo, out msg) > 0)
            {
                Session["userInfo"] = userInfo;
                Response.Redirect("/Default.aspx");
            }
            else
            {
                Response.Redirect("/ShowMsg.aspx?msg="+msg+"$txt=firstPage"+"@url=/Default.aspx");
            }
            
        
        }
    }
}