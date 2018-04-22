using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csGame
    {
        private int _idGame;
        public int IdGame
        {
            set { _idGame = value; }
            get { return _idGame; }
        }

        private int _homeNumber;
        public int HomeNumber
        {
            set { _homeNumber = value; }
            get { return _homeNumber; }
        }

        private string _homeTeam;
        public string HomeTeam
        {
            set { _homeTeam = value; }
            get { return _homeTeam; }
        }


        private int _visitorNumber;
        public int VisitorNumber
        {
            set { _visitorNumber = value; }
            get { return _visitorNumber; }
        }


        private string _visitorTeam;
        public string VisitorTeam
        {
            set { _visitorTeam = value; }
            get { return _visitorTeam; }
        }


        private int _idLeague;
        public int IdLeague
        {
            set { _idLeague = value; }
            get { return _idLeague; }
        }



        private string _idSport;
        public string IdSport
        {
            set { _idSport = value; }
            get { return _idSport; }
        }



        private DateTime _eventDate;
        public DateTime EventDate
        {
            set { _eventDate = value; }
            get { return _eventDate; }
        }


        public csGame(int idGame, string homeTeam, string visitorTeam, int idLeague, string idSport, int homeNumber, int visitorNumber,
                      DateTime dt)
        {
            _idGame = idGame;
            _homeTeam = homeTeam ?? throw new ArgumentNullException(nameof(homeTeam));
            _visitorTeam = visitorTeam ?? throw new ArgumentNullException(nameof(visitorTeam));
            _idLeague = idLeague;
            _idSport = idSport ?? throw new ArgumentNullException(nameof(idSport));
            _homeNumber = homeNumber;
            _visitorNumber = visitorNumber;
            _eventDate = dt;
        }

        public csGame() { }



    }
}
