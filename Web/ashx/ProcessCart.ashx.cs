using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Model;

namespace Web.ashx
{
    /// <summary>
    /// ProcessCart 的摘要说明
    /// </summary>
    public class ProcessCart : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            UserManager userManager=new UserManager();
            if (userManager.validateUserLogin())
            {
                int bookId = Convert.ToInt32((context.Request["bookId"]));
                BookManager bookManager= new BookManager();
                Model.Book bookModel = bookManager.GetModel(bookId);
                if (bookModel != null)
                {
                    int userId = ((Model.User)context.Session["userInfo"]).Id;
                    CartManager cartManager=new CartManager();
                    //Model.Book bookModel = bookManager.GetModel(bookId);
                    Model.Cart cartModel = cartManager.GetModel(userId,bookId);
                    if (cartModel != null)
                    {
                        cartModel.Count = cartModel.Count + 1;
                        cartManager.Update(cartModel);

                    }
                    else
                    {
                        Cart modelCart=new Cart();
                        modelCart.Count = 1;
                        modelCart.Book = bookModel;
                        modelCart.User = (User)context.Session["userInfo"];
                        cartManager.Add(modelCart);
                    }

                    context.Response.Write("Success");

                }
                else
                {
                    context.Response.Write("No Such Stuff");
                }

                context.Response.Write("Success");
            }
            else
            {
                context.Response.Write("Please Login");
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