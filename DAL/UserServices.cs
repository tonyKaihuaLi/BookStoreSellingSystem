using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class UserServices
    {

        UserStateServices userStateServices = new UserStateServices();

        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Users");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Users");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Users(");
            strSql.Append("LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId)");
            strSql.Append(" values (");
            strSql.Append("@LoginId,@LoginPwd,@Name,@Address,@Phone,@Mail,@UserStateId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginId", SqlDbType.NVarChar,50),
                    new SqlParameter("@LoginPwd", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200),
                    new SqlParameter("@Phone", SqlDbType.NVarChar,100),
                    new SqlParameter("@Mail", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserStateId", SqlDbType.Int,4)};
            parameters[0].Value = model.LoginId;
            parameters[1].Value = model.LoginPwd;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Address;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Mail;
            parameters[6].Value = model.UserState.Id;


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
        public void Update(Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users set ");
            strSql.Append("LoginId=@LoginId,");
            strSql.Append("LoginPwd=@LoginPwd,");
            strSql.Append("Name=@Name,");
            strSql.Append("Address=@Address,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Mail=@Mail,");
            strSql.Append("UserStateId=@UserStateId");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@LoginId", SqlDbType.NVarChar,50),
                    new SqlParameter("@LoginPwd", SqlDbType.NVarChar,50),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200),
                    new SqlParameter("@Phone", SqlDbType.NVarChar,100),
                    new SqlParameter("@Mail", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserStateId", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.LoginId;
            parameters[2].Value = model.LoginPwd;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.Phone;
            parameters[6].Value = model.Mail;
            parameters[7].Value = model.UserState.Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Users ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.User GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId from Users ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            Model.User model = new Model.User();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                model.LoginPwd = ds.Tables[0].Rows[0]["LoginPwd"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                model.Mail = ds.Tables[0].Rows[0]["Mail"].ToString();

                if (ds.Tables[0].Rows[0]["UserStateId"].ToString() != "")
                {
                    int UserStateId = int.Parse(ds.Tables[0].Rows[0]["UserStateId"].ToString());
                    model.UserState = userStateServices.GetModel(UserStateId);
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
            strSql.Append("select Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId ");
            strSql.Append(" FROM Users ");
            if (strWhere != null && strWhere.Trim() != "")
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
            strSql.Append(" Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserRoleId,UserStateId ");
            strSql.Append(" FROM Users ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }



        #endregion  成员方法
    }
}
