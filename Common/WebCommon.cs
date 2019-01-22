using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        public static string GetTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 365)
            {
                return Math.Floor(timeSpan.TotalDays / 365) + "years ago";
            }
            else if (timeSpan.TotalDays >= 30)
            {
                return Math.Floor(timeSpan.TotalDays / 30) + "months ago";

            }
            else if(timeSpan.TotalHours>=24)
            {
                return Math.Floor(timeSpan.TotalDays) + "days ago";

            }
            else if(timeSpan.TotalHours>=1)
            {
                return Math.Floor(timeSpan.TotalHours) + "hours ago";
            }
            else if(timeSpan.TotalMinutes>=1)
            {
                return Math.Floor(timeSpan.TotalMinutes) + "minutes ago";
            }
            else
            {
                return "Just Now";
            }
        }

        public static string UbbToHtml(string argString)
        {
            string tString = argString;
            if (tString != "")
            {
                Regex tRegex;
                bool tState = true;
                tString = tString.Replace("&", "&amp;");
                tString = tString.Replace(">", "&gt;");
                tString = tString.Replace("<", "&lt;");
                tString = tString.Replace("\"", "&quot;");
                tString = Regex.Replace(tString, @"\[br\]", "<br />", RegexOptions.IgnoreCase);
                string[,] tRegexAry = 
                {
                    {@"\[p\]([^\[]*?)\[\/p\]", "$1<br />"},
                    {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
                    {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
                    {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
                    {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
                    {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
                    {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
                    {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
                    {@"\[quote\]([^\[]*?)\[\/quote\]", "<div class=\"ubb_quote\">$1</div>"},
                    {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
                    {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
                    {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
                    {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
                    {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}

                };
                while (tState)
                {
                    tState = false;
                    for (int ti = 0; ti < tRegexAry.GetLength(0); ti++)
                    {
                        tRegex = new Regex(tRegexAry[ti, 0], RegexOptions.IgnoreCase);
                        if (tRegex.Match(tString).Success)
                        {
                            tState = true;
                            tString = Regex.Replace(tString, tRegexAry[ti, 0], tRegexAry[ti, 1], RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            return tString;
        }
    }
}
