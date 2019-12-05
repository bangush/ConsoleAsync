using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProMvc03.DapperHelper
{
    public class B2BOrder
    {
        //定义几个属性
        public string Mobile { get; set; }
        public string FetchCode { get; set; }
        public string OrderSN { get; set; }
        public Guid pk { get; set; }
    }

    public class Method
    {

         public static string Connection { set; get; }

        public static void Method_one(string PkB2BOrderHead,string msgid,string time)
        {
            string s = @"insert into OrderInvoiceSMSLog
                            (pk, pk_order_head,createdatetime,MessageId,SendTime)
                        values (newid(), @pk, getdate(), @messageid, @sendtime);";

            using (SqlConnection conn = new SqlConnection(Connection))
            {
                conn.Open();
                conn.Execute(s, new { pk = PkB2BOrderHead, messageid = msgid, sendtime = time });
            }

        }

        public static IList<B2BOrder> GetOrder(string date)
        {
            string str = "存储过程的名字";
            using (SqlConnection conn=new SqlConnection(Connection))
            {
                conn.Open();

                var q = conn.Query<B2BOrder>(str, new { date = date }, commandType: System.Data.CommandType.StoredProcedure);

                return q.ToList();
            }
        }


    }
}
