using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Sports
{
    public class csSport
    {
        private string _searchName;
        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; }
        }


        private string _realName;
        public string RealName
        {
            get { return _realName; }
            set { _realName = value; }
        }


        public csSport(string s, string r)
        {
            this._searchName = s;
            this._realName = r;
        }
    }
}