using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace WindowsFormsApplication1
{
    public static class SQLiteHelper
    {
        //需要添加引用--框架，System.Configuration，添加再using，用来读取配置文件的数据库链接字符串
        private static string conStr = ConfigurationManager.ConnectionStrings["SQLiteDB"].ConnectionString;//"Data Source=D:\\workspace\\workspace_smy\\smy\\data\\test.db;Version=3"; //
                                                                                                           // private static readonly string conStr = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
        #region 增删改方法，前面为sql语句 后面为对个参数列表
        /// <summary>
        /// 封装增加、删、改方法，方法名可以随便写，但是为了方便查看，用了和SqlCommand类一一样的方法名
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="pms">SQL参数，因为不知道会有多少个参数，所以用可变参数params</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SqliteParameter[] pms)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        throw;
                    }


                    return cmd.ExecuteNonQuery();
                }
            }




        }
        #endregion

        #region 查询单个结果返回object
        /// <summary>
        /// 查询单个结果，一般和聚合函数 一起使用
        /// </summary>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="pms">SQL参数</param>
        /// <returns>返回查询对象，查询结果第一行第一列</returns>
        public static object ExecuteScalar(string sql, params SQLiteParameter[] pms)
        {

            using (SQLiteConnection conn = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        throw;
                    }


                    return cmd.ExecuteScalar();
                }
            }

        }
        #endregion

        #region 查询多行数据，列表查询前面为sql语句 ，后面为参数
        /// <summary>
        /// 查询多行
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="pms">SQL参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static SQLiteDataReader ExcuteReader(string sql, params SQLiteParameter[] pms)
        {

            //这里不能用using，不然在返回SqlDataReader时候会报错，因为返回时候已经在using中关闭了。
            //事实上，在使用数据库相关类中，SqlConnection是必须关闭的，但是其他可以选择关闭，因为CG回自动回收
            SQLiteConnection conn = new SQLiteConnection(conStr);
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    conn.Open();
                    //传入System.Data.CommandBehavior.CloseConnection枚举是为了让在外面使用完毕SqlDataReader后，只要关闭了SqlDataReader就会关闭对应的SqlConnection
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }
        }
        #endregion
    }
}