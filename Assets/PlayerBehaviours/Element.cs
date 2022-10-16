using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PlayerBehaviours
{
    internal class Element
    {
        public int Earth { get; set; }
        public int Air { get; set; }
        public int Fire { get; set; }
        public int Water { get; set; }

        public Element()
        {
            Earth = 0;
            Air = 0;
            Fire = 0;
            Water = 0;
        }
        public int FindHighestEle()
        {
            if(Earth>Air&& Earth> Fire&& Earth> Water)
            {
                return 1;
            }
            else if(Air> Earth &&Air> Fire&&Air > Water)
            {
                return 2;
            }
            else if (Fire > Earth && Fire > Air && Fire > Water)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}
