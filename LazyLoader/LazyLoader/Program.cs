using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoader
{
    class Program
    {
        static void Main( string [ ] args )
        {
            //Let's see how Wilson does...
            var Wilson = new LazyLoader();
            //Get Wilson's schedule for the week
            Wilson.GetSchedules( @"C:\Users\Marky\Downloads\sample.txt" );
            //Process the weeks work
            Wilson.DoWork();
            //Report production output
            Wilson.PrintForecastReport();
        }
    }
}
