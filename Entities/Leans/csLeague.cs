using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csLeague
    {
        private string _idLeague;
        public String IdLeague
        {
            set { _idLeague = value; }
            get { return _idLeague; }
        }


        private string _leagueDescription;
        public String LeagueDescription
        {
            set { _leagueDescription = value; }
            get { return _leagueDescription; }
        }


        public csLeague() { }

        public csLeague(string id, string des)
        {
            _idLeague = id;
            _leagueDescription = des;
        }
    }
}
