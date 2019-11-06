using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetStandard_Helper
{
    public interface IDateTimeData
    {
        string  GetDate();
       
    }
         


  public  class DateTimeData: IDateTimeData
    {
        public string GetDate()
        {
            return "from  DateTimeData.GetDate()";
        }

      



    }
}
