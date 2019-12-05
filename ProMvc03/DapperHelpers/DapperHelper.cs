using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProMvc03
{
    public class DapperHelper1
    {
        //字段和属性的区别：https://www.cnblogs.com/green-jcx/p/9023141.html
        //字段
        private static string _connection = string.Empty;
        //属性     
        public static string Connection
        {
            get { return _connection; }
        }

        private static IDbConnection dbConnection = null;

        public static DapperHelper1 uniqueInstance;

        private static readonly object locker = new object();

        private DapperHelper1()
        {
             _connection= @"server=.;uid=sa;pwd=sasasa;database=Dapper";
        }

        //创建数据库连接对象并打开链接
        public static IDbConnection OPenCurrentDbConnection()
        {
            if (dbConnection == null)
            {
                dbConnection = new SqlConnection(Connection);
            }
            //判断连接状态
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }


        //获取实例，保证只存在一个实例
        public static DapperHelper1 GetInstance()
        {
            //双重锁定实现单利模式，在外层加个判空条件主要是为了减少枷锁，释放锁的不必要的损耗
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new DapperHelper1();
                    }
                }
            }
            return uniqueInstance;
        }
    }
}
