using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace Web.ashx
{
    /// <summary>
    /// ValidateReg 的摘要说明 
    /// </summary>
    public class ValidateReg : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        UserManager userManager = new UserManager();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (action == "mail")
            {

                CheckUserMail(context);
            }
            else if(action=="code")
            {
                string validateCode = context.Request["validateCode"];

                if (Common.WebCommon.CheckValidateCode(validateCode))
                {
                    context.Response.Write("Correct");
                }
                else
                {
                    context.Response.Write("Wrong");
                }
            }

        }

        private void CheckUserMail(HttpContext context)
        {
            string userMail = context.Request["UserMail"];
            if (userManager.CheckUserMail(userMail))
            {
                context.Response.Write("Wrong");
            }
            else
            {
                context.Response.Write("Can be used");
            }
            

        }

        //private void CheckValidateCode(HttpContext context)
        //{
        //    //bool isSuccess = false;
        //    if()
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}