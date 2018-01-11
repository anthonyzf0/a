using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postfix
{
    class Program
    {
        static string terms = "+-/*%";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What is the expression");
                String input = Console.ReadLine();

                Console.WriteLine(evaluate(input));


            }
        }


        static String evaluate(String val)
        {
            List<String> simplified = new List<string>();

            //For ( and )
            int depth;
            bool par = false;
            
            while (val.Length != 0)
            {
                depth = 0;
                par = val[0] == '(';

                for (int i = 0; i < val.Length; i++) {

                    if (val[i] == ')') depth++;
                    if (val[i] == '(') depth--;

                    if (par && depth == 0)
                    {
                        String x = val.Substring(1, i - 1);
                        simplified.Add(evaluate(x));
                        if (i != val.Length - 1)
                            simplified.Add(val.Substring(i + 1, 1));
                        
                        val = (i==val.Length-1)?"":val.Substring(i + 2);
                    
                        break;
                    }
                    else if (terms.Contains(val[i]) && !par)
                    {
                        simplified.Add(val.Substring(0, i));
                        simplified.Add(val.Substring(i, 1));
                        val = val.Substring(i + 1);
                        break;
                    }
                }

                bool found = false;
                foreach (char c in terms)
                    if (val.Contains(c)) found = true;

                if (!found && val!="")
                {
                    simplified.Add(val);
                    break;
                }
            }

            int pos = 1;
            while (true)
            {

                if (simplified.Count == 0 || simplified.Count == 1)
                    break;

                if ("+-/*%".Contains(simplified[pos]))
                {
                    simplified.Insert(0, simplified[pos - 1] + simplified[pos + 1] + simplified[pos]);
                    simplified.RemoveAt(pos);
                    simplified.RemoveAt(pos);
                    simplified.RemoveAt(pos);
                }

            }

            return simplified[0];
        }
    }
}
