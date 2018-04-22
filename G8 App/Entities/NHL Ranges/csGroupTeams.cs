using NHL_BL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_BL.Logic
{
    public class csGroupTeam
    {
        public csGroupTeam() { }
 
        public ObservableCollection<csTeamRange> Lista = new ObservableCollection<csTeamRange>();

        public void AddNewTeam(csTeamRange TR)
        {
            if(TR != null)
            {
                if (Lista == null)
                {
                    Lista.Add(TR);
                }
                else
                {
                    bool flag = false;

                    for (int i = 0; i < Lista.Count; i++)
                    {
                        if (Lista[i].TeamName == TR.TeamName)
                        {
                            flag = true;
                        }
                    }


                    if(!flag)
                    {
                        Lista.Add(TR);
                    }
                }
            }
        }




    }
}
