using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Path.Combine(
            // HttpRuntime.AppDomainAppPath
            //Request.MapPath();

            //  string filePath = HttpContext.Current.Request.MapPath("/Images/body.jpg");
            ParameterizedThreadStart par = new
            ParameterizedThreadStart(GetFilePath);
            Thread thread1 = new Thread(par);
            thread1.IsBackground = true;
            thread1.Start(HttpContext.Current);
            // GetFilePath();

        }
        protected void GetFilePath(object context)
        {
            Common.WebCommon.GetFilePath(context);
        }
    }
}