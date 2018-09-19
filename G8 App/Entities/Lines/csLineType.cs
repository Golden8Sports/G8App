using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Lines
{
    public class csLineType
    {
        public int IdLineType { get; set; }
        public string Description { get; set; }

        public csLineType(int idLineType, string description)
        {
            IdLineType = idLineType;
            Description = description;
        }

        public csLineType()
        {
        }
    }
}