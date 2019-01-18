using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Model;

namespace Web.ashx
{
    /// <summary>
    /// EditCart 的摘要说明
    /// </summary>
    public class EditCart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (action == "edit")
            {
                int count = Convert.ToInt32(context.Request["count"]);
                int cartId = Convert.ToInt32(context.Request["cartId"]);
                CartManager cartManager = new CartManager();
                Cart cartModel = cartManager.GetModel(cartId);
                cartModel.Count = count;
                cartManager.Update(cartModel);
                context.Response.Write("ok");
            }
            else if(action=="delete")
            {
                int cartId = Convert.ToInt32(context.Request["cartId"]);
                CartManager cartManager = new CartManager();
                cartManager.Delete(cartId);
                context.Response.Write("ok");
            }//context.Response.Write("Hello World");

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