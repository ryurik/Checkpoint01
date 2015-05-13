using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson09
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> List = new List<string>() { "0", "1", "2", "3", "4", "5", };

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var s in List)
            {
                stringBuilder.Append(s);
            }

            Console.WriteLine(stringBuilder.ToString());

            List<int> list = new List<int>(){1,2,3,4,5};

            foreach (var c in list)
            {
                
            }
            IEnumerator<int> iterator = null;

            try
            {
                iterator = list.GetEnumerator();
                while (iterator.MoveNext())
                {
                    
                }
            }
            finally 
            {
                if (iterator != null)
                {
                    iterator.Dispose();
                }
            }
        }
    }
}
