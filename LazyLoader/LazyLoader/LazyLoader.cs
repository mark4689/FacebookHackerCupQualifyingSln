using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoader
{
    class LazyLoader
    {
        private static List<Load> weeksLoads;
        private static List<List<Load>> daysLoads;
        private static List<string> forecastedTripsByDay;

        //Create Lazy Loader instances
        public LazyLoader()
        {
            //Prime the pumps
            weeksLoads = new List<Load>();
            daysLoads = new List<List<Load>>();
            forecastedTripsByDay = new List<string>();
        }
        //Process weeks work
        public void DoWork()
        {
            foreach ( var load in weeksLoads )
            {
                int numberOfTrips = 0;
                //Add a new list of loads for the current days loads
                daysLoads.Add(new List<Load>());
                //While there are still items in that days loads
                while ( load.Items.Any() )
                {
                    //Increment the number of trips
                    numberOfTrips++;

                    //Add a new Load for that days list of loads
                    daysLoads.Last().Add( new Load() );
                    //Add the heaviest item to that loads list of items (on top of bag to fool old Julie)
                    daysLoads.Last().Last().Items = new List<int>();
                    daysLoads.Last().Last().Items = new int [ ] { load.TopItemWeight };
                    //Deduct that item from the load
                    var remainingList = load.Items.ToList();
                    remainingList.Remove( load.TopItemWeight );
                    load.Items = remainingList;

                    //Make sure the remaining weight isn't too small for at least one more load
                    if ( load.Items.Any() && load.Weight < 50 )
                    {
                        //If he can't squeeze out one last load add the remaining items and remove from that days loads
                        var list = daysLoads.Last().Last().Items.ToList();
                        list.AddRange( load.Items );
                        daysLoads.Last().Last().Items = list;

                        remainingList.RemoveAll(x => load.Items.Contains(x));
                        load.Items = remainingList;
                    }
                    //Nooooiceeee!!!
                    else
                    {
                        //while there are still items in the days load and the last loads weight is less than 50
                        while ( load.Items.Any() && daysLoads.Last().Last().Weight < 50 )
                        {
                            //Add the smallest objects possible until we have to take the remaining items in a final load >:-]
                            var list = daysLoads.Last().Last().Items.ToList();
                            list.Add( load.Items.ToList().Min() );
                            daysLoads.Last().Last().Items = list;
                            //And remove from that days load
                            remainingList.Remove( load.Items.ToList().Min() );
                            load.Items = remainingList;
                            
                            //Make sure the last load isn't too light
                            if ( load.Weight < 50 )
                            {
                                list.RemoveAll(x => list.Contains(x));
                                list.AddRange( load.Items );

                                remainingList.RemoveAll(x => load.Items.Contains(x));

                                daysLoads.Last().Last().Items = list;
                                load.Items = remainingList;
                            }
                        }
                    }
                }
                //Concat the number of trips in the string format specified in the problem
                string output = string.Format("Case #{0}: {1}", weeksLoads.IndexOf( load )+1, numberOfTrips);
                forecastedTripsByDay.Add(output + "\r\n");
            }
        }
        //Read weeks work
        public void GetSchedules(string filePath)
        {
            //Make sure we don't blow a gasket looking for a schedule that doesn't exist!
            if ( File.Exists( filePath ) )
            {
                //Read dat ish
                var lines = File.ReadAllLines( filePath );
                //How many days we working this week?
                int firstLine = int.Parse(lines.First());
                //Alright, prime the pumps and lets do this...
                int nextLine = 1;

                //Load them loads into the load list of loads lists o.O
                while( firstLine-- > 0 )
                {
                    weeksLoads.Add( new Load() );
                    weeksLoads.Last().Items = new List<int>();
                    weeksLoads.Last().Items = lines.ToList().GetRange(nextLine+1,int.Parse(lines.ElementAt(nextLine))).Select(x => int.Parse(x));
                    nextLine += int.Parse( lines.ElementAt( nextLine ) ) + 1;
                }
            }
        }
        //Report weeks work
        public void PrintForecastReport()
        {
            //All done, report production results for the week and file away
            var fs = File.Create( @"C:\Users\Marky\Downloads\output.txt");
            var bytes = forecastedTripsByDay.SelectMany( s => Encoding.ASCII.GetBytes( s ) ).ToArray();
            fs.Write(bytes, 0, bytes.Length);
        }
    }
}