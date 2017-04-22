using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLab4
{
    class TeamsJournalEntry
    {
        // Name of collection
        public string CollectionName { get; set; }
        // Event took place in Collection
        public string CollectionEvent { get; set; }
        //Number of new element
        public int NumberOfEl;
        // Constructor, initialysing the class fields:
        public TeamsJournalEntry( string Name, string Ev, int numOfEl) {

            CollectionName = Name;
            CollectionEvent = Ev;
            NumberOfEl = numOfEl;
        }
        public override string ToString()
        {
            return string.Format("Collection name: {0} \n Event: {1} \n Number of el {2} \n",this.CollectionName,this.CollectionEvent,this.NumberOfEl);
        }
    }
}
