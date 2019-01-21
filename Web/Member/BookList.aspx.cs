using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.Member
{
    public partial class BookList : System.Web.UI.Page
    {

        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindBookList();
        }

        protected void BindBookList()
        {
            int pageIndex;
            if (!int.TryParse(Request["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            int pageSize = 10;
            BLL.BookManager bookManager = new BLL.BookManager();
            int pageCount = bookManager.GetPageCount(pageSize);
            PageCount = pageCount;
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            PageIndex = pageIndex;
            this.BookListRepeater.DataSource = bookManager.GetPageList(pageIndex, pageSize);
            this.BookListRepeater.DataBind();

        }
        public string CutString(string str, int length)
        {
            return str.Length > length ? str.Substring(0, length) + "......" : str;
        }

        public string GetString(object obj)
        {
            DateTime time = Convert.ToDateTime(obj);
            return "/HtmlPage/"+time.Year+"/"+time.Month+"/"+time.Day+"/";
        }

        protected void BookListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}