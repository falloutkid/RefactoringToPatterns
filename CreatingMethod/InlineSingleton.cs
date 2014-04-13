using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    class InclimentCounter
    {
        InclimentCounter instance;
        int count;

        private InclimentCounter()
        {
            count = 0;
        }
        public InclimentCounter getInstance()
        {
            if (instance == null)
                instance = new InclimentCounter();
            return instance;
        }
        public int increment()
        {
            count++;
            return count;
        }
    }
}
