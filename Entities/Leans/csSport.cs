using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csSport
    {
        private string _idSport;
        public String IdSport
        {
            set { _idSport = value; }
            get { return _idSport; }
        }


        private string _sportName;
        public String SportName
        {
            set { _sportName = value; }
            get { return _sportName; }
        }


        private string _sportOrder;
        public String SportOrder
        {
            set { _sportOrder = value; }
            get { return _sportOrder; }
        }


        public csSport() { }

        public csSport(string id, string name, string order)
        {
            _idSport = id;
            _sportName = name;
            _sportOrder = order;
        }
    }
}
