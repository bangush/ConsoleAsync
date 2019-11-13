using System;
using System.Collections.Generic;
using System.Text;

namespace NetStandard_Helper
{

    public interface IGuidService
    {
        string id();
        //string name();
        //string ScopedServicefun();
    }

    public interface ITransientService : IGuidService
    {
    }

    public interface IScopedService : IGuidService
    { }

    public interface ISingletonService : IGuidService
    {
    }




  public  class life_cycle : IGuidService
    {

        private readonly string   _item;
        Random rnd = new Random();
        public life_cycle()
        {
            _item = rnd.Next(0,100).ToString();
        }

        public string id()
        {
            return _item;
        }
    }

    public class TransientService : life_cycle, ITransientService
    {
    }

    public class ScopedService : life_cycle, IScopedService
    {
        //private readonly string _NewItem;
        //public ScopedService()
        //{
        //    _NewItem = "朱凯斌";
        //}

        //public string ScopedServicefun()
        //{
        //    return _NewItem;
        //}

    }

    public class SingletonService : life_cycle, ISingletonService
    {
    }
}
