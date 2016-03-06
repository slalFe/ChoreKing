using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoreKing
{
    public class Chore
    {
        public string Name { get; set; }
        public DateTimeOffset WhenNext { get; set; }

        public Chore(string name, DateTimeOffset whenNext)
        {
            Name = name;
            WhenNext = whenNext;
        }
    }
}
