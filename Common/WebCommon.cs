using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class WebCommon
    {
        public static void GetFilePath(object obj)
        {


            HttpContext context = (HttpContext)obj;
            string filePath = context.Request.MapPath("/Images/body.jpg");

            // string filePath = HostingEnvironment.MapPath("/Images/body.jpg");
            //string filePath=
            // return "";
        }
        public static bool CheckValidateCode(string validateCode)
        {
            bool isSuccess = false;
            if (HttpContext.Current.Session["Vcode"] != null)
            {
                
                string sysCode = HttpContext.Current.Session["Vcode"].ToString();
                if (sysCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    isSuccess = true;
                    HttpContext.Current.Session["vCode"] = null;
                }
            }

            return isSuccess;
        }

        public static void RedirectPage()
        {
            HttpContext.Current.Response.Redirect("/Member/Login.aspx?returnUrl="+HttpContext.Current.Request.Url);
        }

        public static string GetMd5String(string str)
        {
            MD5 md5 = MD5.Create(); 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte VARIABLE in md5Buffer)
            {
                sb.Append(VARIABLE.ToString("x2"));


            }

            return sb.ToString();
        }
    }
}
