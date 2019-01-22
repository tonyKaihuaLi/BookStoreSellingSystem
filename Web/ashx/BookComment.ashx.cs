using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using BLL;
using Web.ViewModel;

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

            List<BookCommentViewModel> newList= new List<BookCommentViewModel>();
            foreach (Model.BookComment bookComment in list)
            {
                ViewModel.BookCommentViewModel viewModel = new BookCommentViewModel();
                viewModel.Msg = Common.WebCommon.UbbToHtml(bookComment.Msg);
                TimeSpan timeSpan = DateTime.Now - bookComment.CreateDateTime;
                viewModel.CreateDateTime = Common.WebCommon.GetTimeSpan(timeSpan);
                newList.Add(viewModel);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();

            context.Response.Write(js.Serialize(newList));
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