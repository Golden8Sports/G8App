using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Entities
{
    public class csLastLine
    {
        private string _valueChange;
        public string ValueChange
        {
            get { return _valueChange; }
            set { _valueChange = value; }
        }


        private int _play;
        public int Play
        {
            get { return _play; }
            set { _play = value; }
        }


        private int _teamNumber;
        public int TeamNumber
        {
            get { return _teamNumber; }
            set { _teamNumber = value; }
        }



        private Nullable<double> _homeSpecial;
        public Nullable<double> HomeSpecial
        {
            get { return _homeSpecial; }
            set { _homeSpecial = value; }
        }


        private Nullable<double> _visitorSpecial;
        public Nullable<double> VisitorSpecial
        {
            get { return _visitorSpecial; }
            set { _visitorSpecial = value; }
        }




        private Nullable<int> _homeOdds;
        public Nullable<int> HomeOdds
        {
            get { return _homeOdds; }
            set { _homeOdds = value; }
        }


        private Nullable<int> _visitorOdds;
        public Nullable<int> VisitorOdds
        {
            get { return _visitorOdds; }
            set { _visitorOdds = value; }
        }



        private int _type;
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }



        private Nullable<int> _homeMoneyLine;
        public Nullable<int> HomeMoneyLine
        {
            get { return _homeMoneyLine; }
            set { _homeMoneyLine = value; }
        }



        private Nullable<int> _visitorMoneyLine;
        public Nullable<int> VisitorMoneyLine
        {
            get { return _visitorMoneyLine; }
            set { _visitorMoneyLine = value; }
        }



        private Nullable<int> _moneyLine;
        public Nullable<int> MoneyLine
        {
            get { return _moneyLine; }
            set { _moneyLine = value; }
        }



        private void SetValueChange(string txt, int p)
        {
            string v1 = "";
            string v2 = "";
            bool flag = false;
            

            for (int i = 0; i < txt.Length; i++)
            {

                if (!flag && txt[i] != ' ')
                {
                    v2 += txt[i];
                }
                else if(txt[i] != ' ' && flag)
                {
                    v1 += txt[i];
                }

                if (txt[i] == ' ')
                {
                    flag = true;
                }              
            }

            // CultureInfo culture = culture.NumberFormat(",");

            if (p == 0)
            {
                //MessageBox.Show(v2.ToString("#.##", CultureInfo.InvariantCulture));
                _visitorSpecial = Convert.ToDouble(String.Format(CultureInfo.InvariantCulture,"{0:#.##}", v2));

                if (_visitorSpecial == 15) _visitorSpecial = 1.5;
                else if (_visitorSpecial == -15) _visitorSpecial = -1.5;
                else if (_visitorSpecial == 5) _visitorSpecial = 0.5;
                else if (_visitorSpecial == -5) _visitorSpecial = -0.5;

                 _visitorOdds = Convert.ToInt32(v1);
                _type = 0;

            }else if(p == 1)
            {
                _homeSpecial = Double.Parse(v2);

                if (_homeSpecial == 15) _homeSpecial = 1.5;
                else if (_homeSpecial == -15) _homeSpecial = -1.5;
                else if (_homeSpecial == 5) _homeSpecial = 0.5;
                else if (_homeSpecial == -5) _homeSpecial = -0.5;

                _homeOdds = Convert.ToInt32(v1);
                _type = 1;
            }

        }


        public csLastLine(string value, int play, int team)
        {
            this._valueChange = value;
            this._play = play;
            this._teamNumber = team;
            SetValueChange(value,play);
        }



        public csLastLine(int ml)
        {
            this._moneyLine = ml;
        }
        


    }
}
