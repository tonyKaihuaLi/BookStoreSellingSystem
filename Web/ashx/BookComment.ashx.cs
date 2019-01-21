using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using BLL;

namespace Web.ashx
{
    /// <summary>
    /// BookComment 的摘要说明
    /// </summary>
    public class BookComment : IHttpHandler
    {
        
        BookCommentManager bookCommentManager= new BookCommentManager();


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (action == "add")
            {
                AddComment(context);
            }
            else if (action=="load")
            {
                LoadComment(context);
            }

            //context.Response.Write("Hello World");        

        }

        private void LoadComment(HttpContext context)
        {
            int bookId = Convert.ToInt32(context.Request["bookId"]);
            List<Model.BookComment> list = bookCommentManager.GetModelList("BookId="+bookId);

            JavaScriptSerializer js = new JavaScriptSerializer();

            context.Response.Write(js.Serialize(list));
        }

        private void AddComment(HttpContext context)
        {
            Model.BookComment bookComment = new Model.BookComment();
            bookComment.BookId = Convert.ToInt32(context.Request["bookId"]);
            bookComment.Msg = context.Request["msg"];
            bookComment.CreateDateTime = DateTime.Now;
            BookCommentManager bookCommentManager = new BookCommentManager();
            if (bookCommentManager.Add(bookComment) > 0)
            {
                context.Response.Write("OK");
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