using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookCommentService
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BookComment");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.BookComment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BookComment(");
            strSql.Append("Msg,CreateDateTime,BookId)");
            strSql.Append(" values (");
            strSql.Append("@Msg,@CreateDateTime,@BookId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Msg", SqlDbType.NVarChar),
                    new SqlParameter("@CreateDateTime", SqlDbType.DateTime),
                    new SqlParameter("@BookId", SqlDbType.Int,4)};
            parameters[0].Value = model.Msg;
            parameters[1].Value = model.CreateDateTime;
            parameters[2].Value = model.BookId;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Model.BookComment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BookComment set ");
            strSql.Append("Msg=@Msg,");
            strSql.Append("CreateDateTime=@CreateDateTime,");
            strSql.Append("BookId=@BookId");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Msg", SqlDbType.NVarChar),
                    new SqlParameter("@CreateDateTime", SqlDbType.DateTime),
                    new SqlParameter("@BookId", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Msg;
            parameters[2].Value = model.CreateDateTime;
            parameters[3].Value = model.BookId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BookComment ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.BookComment GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Msg,CreateDateTime,BookId from BookComment ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            Model.BookComment model = new Model.BookComment();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                if (ds.Tables[0].Rows[0]["CreateDateTime"].ToString() != "")
                {
                    model.CreateDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BookId"].ToString() != "")
                {
                    model.BookId = int.Parse(ds.Tables[0].Rows[0]["BookId"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Msg,CreateDateTime,BookId ");
            strSql.Append(" FROM BookComment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,Msg,CreateDateTime,BookId ");
            strSql.Append(" FROM BookComment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        #endregion  成员方法
    }
}
