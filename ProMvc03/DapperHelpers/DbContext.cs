using ProMvc03.DapperHelpers;
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
                DapperHelperBase.GetInstance();
                return DapperHelperBase.OPenCurrentDbConnection();
            }
        }


       

        //经过上面的操作，Db是已经链接到数据库了，可以进行数据的查询了


    }
}
