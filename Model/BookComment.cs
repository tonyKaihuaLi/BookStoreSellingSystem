using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BookComment
    {
        public BookComment()
        { }
        #region Model
        private int _id;
        private string _msg;
        private DateTime _createdatetime;
        private int _bookid;
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
        public string Msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDateTime
        {
            set { _createdatetime = value; }
            get { return _createdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BookId
        {
            set { _bookid = value; }
            get { return _bookid; }
        }
        #endregion Model

    }
}
