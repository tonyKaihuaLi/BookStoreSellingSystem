using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Model;

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

        public void FindUserPwd(Model.User userInfo)
        {
            BLL.SettingsManager settingsManager = new SettingsManager();

            string newPwd = Guid.NewGuid().ToString().Substring(0, 8);
            userInfo.LoginPwd = newPwd;
            dal.Update(userInfo);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(settingsManager.GetValue("系统邮件SMTP"));
            mailMessage.To.Add(new MailAddress(userInfo.Mail));
            mailMessage.Subject = "Forget Password";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UserName is " + userInfo.LoginId);
            stringBuilder.Append("Your new password is: " + newPwd);
            mailMessage.Body = stringBuilder.ToString();
            SmtpClient client = new SmtpClient(settingsManager.GetValue("系统邮件SMTP"));
            client.Credentials = new NetworkCredential(settingsManager.GetValue("系统邮件用户名"), settingsManager.GetValue("系统邮件密码"));
            client.Send(mailMessage);
        }

        public bool ValidateUserLogin()
        {
            var current = HttpContext.Current;
            if (current.Session["userInfo"] != null)
            {
                return true;
            }
            else
            {
                if (current.Request.Cookies["cp1"] != null && current.Request.Cookies["cp2"] != null)
                {
                    string userName = current.Request.Cookies["cp1"].Value;
                    string userPwd = current.Request.Cookies["cp2"].Value;
                    User userInfo = GetModel(userName);
                    if (userInfo != null)
                    {
                        if (userPwd == Common.WebCommon.GetMd5String(userInfo.LoginPwd))
                        {
                            current.Session["userInfo"] = userInfo;
                            return true;
                        }
                    }

                    current.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                    current.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                    //return false;
                }

                return false;
            }
        }


    }
}
