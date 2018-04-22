using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.NHL_Ranges
{
    public class csTeam
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public csTeam() { }

        public csTeam(int id, string name)
        {
            this._id = id;
            this._name = name;
        }
    }
}