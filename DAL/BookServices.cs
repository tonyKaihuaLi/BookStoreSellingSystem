using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookServices
    {
        PublisherServices publisherServices = new PublisherServices();
        CategoryServices categoryServices = new CategoryServices();
        public BookServices()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID+1的值 
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Books");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Books");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Book model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Books(");
            strSql.Append("Title,Author,PublisherId,PublishDate,ISBN,WordsCount,UnitPrice,ContentDescription,AurhorDescription,EditorComment,TOC,CategoryId,Clicks)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Author,@PublisherId,@PublishDate,@ISBN,@WordsCount,@UnitPrice,@ContentDescription,@AurhorDescription,@EditorComment,@TOC,@CategoryId,@Clicks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@Author", SqlDbType.NVarChar,200),
                    new SqlParameter("@PublisherId", SqlDbType.Int,4),
                    new SqlParameter("@PublishDate", SqlDbType.DateTime),
                    new SqlParameter("@ISBN", SqlDbType.NVarChar,50),
                    new SqlParameter("@WordsCount", SqlDbType.Int,4),
                    new SqlParameter("@UnitPrice", SqlDbType.Money,8),
                    new SqlParameter("@ContentDescription", SqlDbType.NVarChar),
                    new SqlParameter("@AurhorDescription", SqlDbType.NVarChar),
                    new SqlParameter("@EditorComment", SqlDbType.NVarChar),
                    new SqlParameter("@TOC", SqlDbType.NVarChar),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@Clicks", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Author;
            parameters[2].Value = model.Publisher.Id;
            parameters[2].Value = model.Publisher.Id;
            parameters[3].Value = model.PublishDate;
            parameters[4].Value = model.ISBN;
            parameters[5].Value = model.WordsCount;
            parameters[6].Value = model.UnitPrice;
            parameters[7].Value = model.ContentDescription;
            parameters[8].Value = model.AurhorDescription;
            parameters[9].Value = model.EditorComment;
            parameters[10].Value = model.TOC;
            parameters[11].Value = model.Category.Id;
            parameters[12].Value = model.Clicks;

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
        public void Update(Model.Book model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Books set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Author=@Author,");
            strSql.Append("PublisherId=@PublisherId,");
            strSql.Append("PublishDate=@PublishDate,");
            strSql.Append("ISBN=@ISBN,");
            strSql.Append("WordsCount=@WordsCount,");
            strSql.Append("UnitPrice=@UnitPrice,");
            strSql.Append("ContentDescription=@ContentDescription,");
            strSql.Append("AurhorDescription=@AurhorDescription,");
            strSql.Append("EditorComment=@EditorComment,");
            strSql.Append("TOC=@TOC,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("Clicks=@Clicks");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@Author", SqlDbType.NVarChar,200),
                    new SqlParameter("@PublisherId", SqlDbType.Int,4),
                    new SqlParameter("@PublishDate", SqlDbType.DateTime),
                    new SqlParameter("@ISBN", SqlDbType.NVarChar,50),
                    new SqlParameter("@WordsCount", SqlDbType.Int,4),
                    new SqlParameter("@UnitPrice", SqlDbType.Money,8),
                    new SqlParameter("@ContentDescription", SqlDbType.NVarChar),
                    new SqlParameter("@AurhorDescription", SqlDbType.NVarChar),
                    new SqlParameter("@EditorComment", SqlDbType.NVarChar),
                    new SqlParameter("@TOC", SqlDbType.NVarChar),
                    new SqlParameter("@CategoryId", SqlDbType.Int,4),
                    new SqlParameter("@Clicks", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Author;
            parameters[3].Value = model.Publisher.Id;
            parameters[4].Value = model.PublishDate;
            parameters[5].Value = model.ISBN;
            parameters[6].Value = model.WordsCount;
            parameters[7].Value = model.UnitPrice;
            parameters[8].Value = model.ContentDescription;
            parameters[9].Value = model.AurhorDescription;
            parameters[10].Value = model.EditorComment;
            parameters[11].Value = model.TOC;
            parameters[12].Value = model.Category.Id;
            parameters[13].Value = model.Clicks;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Books ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Book GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Title,Author,PublisherId,PublishDate,ISBN,WordsCount,UnitPrice,ContentDescription,AurhorDescription,EditorComment,TOC,CategoryId,Clicks from Books ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            Model.Book model = new Model.Book();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Author = ds.Tables[0].Rows[0]["Author"].ToString();
                if (ds.Tables[0].Rows[0]["PublisherId"].ToString() != "")
                {
                    //  model.PublisherId = int.Parse(ds.Tables[0].Rows[0]["PublisherId"].ToString()); 
                    int PublisherId = int.Parse(ds.Tables[0].Rows[0]["PublisherId"].ToString());
                    model.Publisher = publisherServices.GetModel(PublisherId);

                }
                if (ds.Tables[0].Rows[0]["PublishDate"].ToString() != "")
                {
                    model.PublishDate = DateTime.Parse(ds.Tables[0].Rows[0]["PublishDate"].ToString());
                }
                model.ISBN = ds.Tables[0].Rows[0]["ISBN"].ToString();
                if (ds.Tables[0].Rows[0]["WordsCount"].ToString() != "")
                {
                    model.WordsCount = int.Parse(ds.Tables[0].Rows[0]["WordsCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UnitPrice"].ToString() != "")
                {
                    model.UnitPrice = decimal.Parse(ds.Tables[0].Rows[0]["UnitPrice"].ToString());
                }
                model.ContentDescription = ds.Tables[0].Rows[0]["ContentDescription"].ToString();
                model.AurhorDescription = ds.Tables[0].Rows[0]["AurhorDescription"].ToString();
                model.EditorComment = ds.Tables[0].Rows[0]["EditorComment"].ToString();
                model.TOC = ds.Tables[0].Rows[0]["TOC"].ToString();
                if (ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {

                    int CategoryId = int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
                    model.Category = categoryServices.GetModel(CategoryId);
                }
                if (ds.Tables[0].Rows[0]["Clicks"].ToString() != "")
                {
                    model.Clicks = int.Parse(ds.Tables[0].Rows[0]["Clicks"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表  防止strWhere传null
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,Author,PublisherId,PublishDate,ISBN,WordsCount,UnitPrice,ContentDescription,AurhorDescription,EditorComment,TOC,CategoryId,Clicks ");
            strSql.Append(" FROM Books ");
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
            strSql.Append(" Id,Title,Author,PublisherId,PublishDate,ISBN,WordsCount,UnitPrice,ContentDescription,AurhorDescription,EditorComment,TOC,CategoryId,Clicks ");
            strSql.Append(" FROM Books ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetRecordCount()
        {
            string sql = "select count(*) from Books";
            return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
        }
        public DataSet GetPageList(int start, int end)
        {
            string sql = "select * from(select *,row_number()over(order by id) as num from Books) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] parameters = {
                new SqlParameter("@start", SqlDbType.Int,4),
                new SqlParameter("@end", SqlDbType.Int,4)
            };
            parameters[0].Value = start;
            parameters[1].Value = end;
            return DbHelperSQL.Query(sql, parameters);

        }

        #endregion  成员方法
    }
}
