using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.MLB
{
    public class csTeams
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }


        public csTeams(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SetVisitorHome(name);
        }


        public void SetVisitorHome(string txt)
        {
            char[] v = { 'v', 's' };
            var split = txt.Split(v);

            this.Name1 = split[0].Replace("*", "").Trim();
            this.Name2 = split[2].Replace("*", "").Trim();
        }

        public csTeams(){}
    }
}