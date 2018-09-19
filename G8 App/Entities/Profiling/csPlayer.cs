using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G8_App.Entities.Profiling
{
    public class csPlayer
    {
        private string _player;
        public string Player
        {
            set { _player = value; }
            get { return this._player; }
        }

        private string _idplayer;
        public string IdPlayer
        {
            set { _idplayer = value; }
            get { return this._idplayer; }
        }

        public csPlayer(string player, string idplayer)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _idplayer = idplayer ?? throw new ArgumentNullException(nameof(idplayer));
        }

        public csPlayer(){}
    }
}