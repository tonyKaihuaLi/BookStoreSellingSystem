using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace Web.ashx
{
    /// <summary>
    /// FindPwd 的摘要说明
    /// </summary>
    public class FindPwd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string name = context.Request["name"];
            string mail = context.Request["mail"];
            BLL.UserManager userManager= new UserManager();
            Model.User userInfo = userManager.GetModel(name);
            if (userInfo != null)
            {
                if (userInfo.Mail == mail)
                {
                    userManager.FindUserPwd(userInfo);            
                }
                else
                {
                    context.Response.Write("wrong");
                }


            }
            else
            {
                context.Response.Write("Wrong");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}