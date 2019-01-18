using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryServices
    {
        public CategoryServices()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Categories");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Categories");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Categories(");
            strSql.Append("Name)");
            strSql.Append(" values (");
            strSql.Append("@Name)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Name;

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
        public void Update(Model.Category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Categories set ");
            strSql.Append("Name=@Name");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Categories ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Category GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Name from Categories ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            Model.Category model = new Model.Category();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
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
            strSql.Append("select Id,Name ");
            strSql.Append(" FROM Categories ");
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
            strSql.Append(" Id,Name ");
            strSql.Append(" FROM Categories ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Categories";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  成员方法
    }
}
