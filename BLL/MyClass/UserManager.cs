using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class UserManager
    {
        public int Add(Model.User model, out string msg)
        {
            int isSuccess = -1;
            if (ValidateUserName(model.LoginId))
            {
                msg = "Username has been registered";
                return isSuccess;
            }
            else
            {

                isSuccess= dal.Add(model);
                msg = "Success";
                return isSuccess;
            }
        }

        public bool ValidateUserName(string userName)
        {
            return dal.GetModel(userName) != null ? true : false;
        }

        public bool CheckUserMail(string mail)
        {
            return dal.CheckUserMail(mail) > 0 ? true : false;
        }

        public bool CheckUserInfo(string userName, string userPwd, out string msg, out Model.User user)
        {
            user = dal.GetModel(userName);
            if (user != null)
            {
                if (user.LoginPwd == userPwd)
                {
                    msg = "Success";
                    return true;
                }
                else
                {
                    msg = "wrong";
                    return false;
                }
            
            }
            else
            {
                msg = "wrong";
                return false;
            }
        }

        public Model.User GetModel(string userName)
        {
            return dal.GetModel(userName);
        }


    }
}
