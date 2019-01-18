using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class User
    {
        public User()
        { }
        #region Model
        private int _id;
        private string _loginid;
        private string _loginpwd;
        private string _name;
        private string _address;
        private string _phone;
        private string _mail;
        //private int _userroleid;
        //private int _userstateid;

        private UserState _userState = new UserState();


        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginId
        {
            set { _loginid = value; }
            get { return _loginid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginPwd
        {
            set { _loginpwd = value; }
            get { return _loginpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mail
        {
            set { _mail = value; }
            get { return _mail; }
        }
        /// <summary>
        /// 
        /// </summary>
        //public int UserRoleId
        //{
        //    set{ _userroleid=value;}
        //    get{return _userroleid;}
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public int UserStateId
        //{
        //    set{ _userstateid=value;}
        //    get{return _userstateid;}
        //}


        public UserState UserState
        {
            get { return _userState; }
            set { _userState = value; }
        }
        #endregion Model

    }
}
