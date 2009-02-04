using System;
using System.Collections.Generic;

namespace manualsolution
{
    class Program
    {
        /// <summary>
        /// An array of timeslots for each of the 9 papers.
        /// </summary>
        static int[] timeslots = new int[9];

        /// <summary>
        /// The number of valid solutions.
        /// </summary>
        static int numSolutions = 0;

        /// <summary>
        /// Checks, whether the given papers do not share any time slots.
        /// </summary>
        /// <param name="papers">The papers to be checked.</param>
        /// <returns>True, iff the papers do not share any time slots.</returns>
        static bool NotAtSameTime(params int[] papers)
        {
            Dictionary<int, object> usedTimes = new Dictionary<int, object>();
            foreach (int paper in papers)
            {
                int timeslot = timeslots[paper];
                if (usedTimes.ContainsKey(timeslot)) return false;
                usedTimes[timeslot] = null;
            }
            return true;
        }

        /// <summary>
        /// Checks whether the current solution given by the static timeslots array is valid
        /// and increases the numSolutions counter in that case.
        /// </summary>
        static void CheckSolution()
        {
            Dictionary<int, object> hashSet = new Dictionary<int, object>();

            // Check whether papers of one session are scheduled consecutively
            for (int session = 0; session < 3; session++)
            {
                hashSet.Clear();
                int minTime = int.MaxValue;

                // Collect timeslots of papers, make sure the timeslots are different, calculate minimum timeslot of current session
                for (int paper = session * 3; paper < session * 3 + 3; paper++)
                {
                    int timeslot = timeslots[paper];
                    if (hashSet.ContainsKey(timeslot)) return;
                    hashSet[timeslot] = null;
                    if (timeslot < minTime) minTime = timeslot;
                }

                // Make sure, we have three consecutive timeslots (of course, minTime must be in hashSet)
                if (!hashSet.ContainsKey(minTime + 1) || !hashSet.ContainsKey(minTime + 2))
                    return;
            }

            // Check constraints induced per authors and chair holders?
            if(!NotAtSameTime(2, 4, 7)) return;     // Schippers
            if(!NotAtSameTime(3, 4, 5, 6)) return;  // Van Gorp

            // Found a valid solution
            numSolutions++;
        }

        /// <summary>
        /// Brute-force the solutions by assigning every possible time slot to the index-th paper iteratively.
        /// </summary>
        /// <param name="index">The index of the paper to be mapped (starting with 0).</param>
        static void ChooseTimeSlot(int paperIndex)
        {
            // All 9 papers have a time slot?
            if (paperIndex == 9)
            {
                // Yes, check whether current solution is valid.
                CheckSolution();
                return;
            }

            // Try a solution with every timeslot for the current paper.
            for (int timeslot = 0; timeslot < 6; timeslot++)
            {
                timeslots[paperIndex] = timeslot;
                ChooseTimeSlot(paperIndex + 1);
            }
        }

        static void Main(string[] args)
        {
            // Start brute-forcing with first paper
            ChooseTimeSlot(0);
            Console.WriteLine("Num solutions: " + numSolutions);
        }
    }
}
