using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProMvc03
{
    public class DbContext
    {
        public static IDbConnection Db
        {
            get
            {
                DapperHelper1.GetInstance();
                return DapperHelper1.OPenCurrentDbConnection();
            }
        }

        //经过上面的操作，Db是已经链接到数据库了，可以进行数据的查询了


    }
}
