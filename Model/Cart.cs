using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Cart
    {

        #region Model
        private int _id;
        //private int _userid;
        //private int _bookid;
        private User _user = new User();

        private Book _book = new Book();


        private int _count;

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

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        public Book Book
        {
            get { return _book; }
            set { _book = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }

        #endregion Model

    }
}
