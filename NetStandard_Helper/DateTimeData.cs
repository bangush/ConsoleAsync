using System;
using System.Collections.Generic;
using System.Text;

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
