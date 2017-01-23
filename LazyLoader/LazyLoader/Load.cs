using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoader
{
    class Load
    {
        //A list of the item's weights of the Load in question
        public IEnumerable<int> Items { get; set; }
        //The total weight of all the items for that particular Load
        public int Weight {
            //We only need to get it because we setting can be done automatically with LINQ
            get
            {
                //Da weights isn't da real onez... Willy's lazy
                //return the number of items in the load times the top item's weight
                return Items.ToList().Count * TopItemWeight;
            }
        }
        //Number of Items in the current Load
        //Simply just return the Count of the List object Items
        public int NumberOfItems { get { return Items.ToList().Count; } }
        //The weight of the top Item of the current Load
        //Simply just return the Max of the List object Items
        public int TopItemWeight { get { return Items.Max(); } }

    }
}
