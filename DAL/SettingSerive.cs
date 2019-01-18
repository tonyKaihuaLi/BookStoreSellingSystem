using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SettingSerive
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Settings");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Settings model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Settings(");
            strSql.Append("Name,Value)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Value)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Value;

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
        public void Update(Model.Settings model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Settings set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Value=@Value");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Value;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Settings ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Settings GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Name,Value from Settings ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            Model.Settings model = new Model.Settings();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Value = ds.Tables[0].Rows[0]["Value"].ToString();
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
            strSql.Append("select Id,Name,Value ");
            strSql.Append(" FROM Settings ");
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
            strSql.Append(" Id,Name,Value ");
            strSql.Append(" FROM Settings ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        //-------------------------------------
        /// <summary>
        ///根据配置的名称 得到一个对象实体
        /// </summary>
        public Model.Settings GetModel(string name)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Name,Value from Settings ");
            strSql.Append(" where Name=@Name ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,50)};
            parameters[0].Value = name;

            Model.Settings model = new Model.Settings();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Value = ds.Tables[0].Rows[0]["Value"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion  成员方法
    }
}
