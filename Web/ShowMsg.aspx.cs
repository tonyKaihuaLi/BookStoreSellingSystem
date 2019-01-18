using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class ShowMsg : System.Web.UI.Page
    {
        public string Msg { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Msg = string.IsNullOrEmpty(Request["msg"])?"????":Request["msg"];
            Title = string.IsNullOrEmpty(Request["txt"]) ? "List" : Request["msg"];
            Url = string.IsNullOrEmpty(Request["url"]) ? "/BookList.aspx" : Request["url"];
        }
    }
}