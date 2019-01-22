using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        public List<Model.Cart> CartList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            UserManager userManager = new UserManager();
            if (userManager.ValidateUserLogin())
            {
                BindCartList();
            }
            else
            {
                
                Common.WebCommon.RedirectPage();
            }
            
        }

        protected void BindCartList()
        {
            BLL.CartManager cartManager= new CartManager();
            cartManager.GetModelList("UserId"+((Model.User)Session["userInfo"]).Id);
        }
    }
}