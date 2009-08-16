using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace parseoutput
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, object> solutions = new Dictionary<int, object>();
            int numLines = 0;

            using (StreamReader reader = new StreamReader("..\\..\\..\\solutions.txt"))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    int i = 0;
                    int val = 0;
                    int curval;
                    do
                    {
                        int paper = (int)(line[i + 1] - '0');
                        int timeslot = (int)(line[i + 3] - '0');
                        Debug.Assert(paper > 0 && paper < 10);
                        Debug.Assert(timeslot > 0 && timeslot < 7);
                        curval = timeslot;
                        for (int j = 0; j < paper; j++)
                            curval *= 7;
                        val += curval;
                        i += 5;
                    }
                    while (i < line.Length);
                    numLines++;
                    solutions[val] = null;
                }
            }

            int[] solArray = new int[solutions.Count];
            int ind = 0;
            foreach (int val in solutions.Keys)
            {
                solArray[ind++] = val;
            }
            Array.Sort<int>(solArray);

            foreach (int val in solArray)
            {
                int curval = val / 7;
                for (int paper = 1; paper < 10; paper++)
                {
                    int timeslot = curval % 7;
                    curval /= 7;
                    Console.Write("P{0}@{1},", paper, timeslot);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Num lines: " + numLines + "\nNum solutions: " + solutions.Count);
        }
    }
}
