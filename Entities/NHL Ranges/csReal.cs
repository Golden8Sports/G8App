using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csReal
    {
        public csReal() { }

        private string _searchName;
        public string SearchName
        {
           get { return _searchName;  }
           set { _searchName = value; }
        }


        private string _realName;
        public string RealName
        {
            get { return _realName; }
            set { _realName = value; }
        }


        public csReal(string s, string r)
        {
            this._searchName = s;
            this._realName = r;
        }
    }
}
