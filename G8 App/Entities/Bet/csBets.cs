using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Bet
{
    public class csBets
    {
        private int _idWager;
        public int IdWager
        {
            set { _idWager = value; }
            get { return _idWager; }
        }

        private int _net;
        public int Net
        {
            set { _net = value; }
            get { return _net; }
        }

        private DateTime _placedDate;
        public DateTime PlacedDate
        {
            set { _placedDate = value; }
            get { return _placedDate; }
        }

        private string _player;
        public string Player
        {
            set { _player = value; }
            get { return _player; }
        }

        public csBets(int idWager, int net, DateTime placedDate, string player)
        {
            _idWager = idWager;
            _net = net;
            _placedDate = placedDate;
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public csBets()
        {
        }
    }
}