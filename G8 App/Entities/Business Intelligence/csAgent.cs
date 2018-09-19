using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Business_Intelligence
{
    public class csAgent
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public csAgent(string n, int v)
        {
            Name = n;
            Value = v;
        }

        public csAgent(){}
    }
}