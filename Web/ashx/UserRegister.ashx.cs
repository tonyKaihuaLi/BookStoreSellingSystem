using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// UserRegister 的摘要说明
    /// </summary>
    public class UserRegister : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string txtEmail = context.Request["txtEmail"];
            if (!IsValidate(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", txtEmail, context))
            {
                //判断邮箱是否被占用。

                context.Response.Write("邮箱错误!!");
                return;
            }

        }

        private bool IsValidate(string regex, string msg, HttpContext context)
        {

            Regex reg = new Regex(regex);
            bool isSucess = false;
            if (reg.IsMatch(msg))
            {

                isSucess = true;
            }
            return isSucess;
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