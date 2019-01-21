using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace Web.Member
{
    public partial class Login : System.Web.UI.Page
    {
        public string ReturnUrl = string.Empty;
        BLL.UserManager userManager= new UserManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UserLogin();
            }
            else
            {
                ReturnUrl = Request["returnUrl"];
                CheckCookieInfo();
            }
        }

        public string Msg { get; set; }

        private void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string userName = Request.Cookies["cp1"].Value;
                string userPwd = Request.Cookies["cp2"].Value;
                User userInfo = userManager.GetModel(userName);
                if (userInfo != null)
                {
                    if (userPwd == Common.WebCommon.GetMd5String(userInfo.LoginPwd))
                    {
                        Session["userInfo"] = userInfo;
                        if (!string.IsNullOrEmpty(Request["returnUrl"]))
                        {
                            Response.Redirect(Request["returnUrl"]);
                        }
                        else
                        {
                            Response.Redirect("Register.aspx/Default.aspx");
                        }
                    }
                }
                Response.Cookies["cp1"].Expires=DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires=DateTime.Now.AddDays(-1);

            } 
        }

        private void UserLogin()
        {
            string userName = Request["txtLoginId"];
            string txtLoginPwd = Request["txtLoginPwd"];
            string msg = string.Empty;
            //UserManager userManager=new UserManager();
            User userInfo = new User();
            userManager.CheckUserInfo(userName, txtLoginPwd, out msg, out userInfo);
            bool b = userManager.CheckUserInfo(userName, txtLoginPwd, out msg, out userInfo);
            if (b)
            {
                Session["userInfo"] = userInfo;
                if (!string.IsNullOrEmpty(Request["cbAutoLogin"]))
                {
                    HttpCookie cookie1=new HttpCookie("cp1",userName);
                    HttpCookie cookie2=new HttpCookie("cp2",Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(txtLoginPwd)));
                    cookie1.Expires=DateTime.Now.AddDays(7);
                    cookie2.Expires=DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }
                if (string.IsNullOrEmpty(Request["hiddenReturnUrl"]))
                {
                    Response.Redirect("/Default.aspx");
                }
                else
                {
                    Response.Redirect(Request["hiddenReturnUrl"]);
                }

                //Response.Redirect("Register.aspx/Default.aspx");
            }
            else
            {
                Msg = msg;
            }
        }
    }
}