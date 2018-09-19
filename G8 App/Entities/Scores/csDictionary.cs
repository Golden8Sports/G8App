using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Scores
{
    public class csDictionary
    {
        public string Header { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }

        public csDictionary(string header, string val1, string val2)
        {
            Header = header;
            Val1 = val1;
            Val2 = val2;
        }

        public csDictionary()
        {
        }
    }
}