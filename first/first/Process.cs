using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first
{
    public class Process
    {
        public int id { get; set; }
        public char state { get; set; }
        public int arrival { get; set; }
        public int cpu { get; set; }
        
      
        public long runtime { get; set; }
        public int priority { get; set; }
        public int timeleft { get; set; }
        public int contextSwitches { get; set; }
        public int start { get; set; }
        public int finish { get; set; }
        public ProcessBlock block { get; set; }
        public int cpu_burst { get; set; }




        public Process()
        {
            id = 0;
            arrival = 0;
            cpu_burst = 0;
          // timeleft = cpu_burst;
            contextSwitches = 0;
            state = 'R';
        }


    }
}
