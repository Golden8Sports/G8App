using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csTeam
    {
        public string Name { get; set; }
        public string Real { get; set; }

        public csTeam(string name, string real)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Real = real;
        }
    }
}