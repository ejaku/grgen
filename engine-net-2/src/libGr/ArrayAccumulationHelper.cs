/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    public static partial class ContainerHelper
    {
        public static int Sum(List<SByte> array)
        {
            int sum = 0;
            foreach(SByte elem in array)
            {
                sum += elem;
            }
            return sum;
        }

        public static int Sum(List<Int16> array)
        {
            int sum = 0;
            foreach(Int16 elem in array)
            {
                sum += elem;
            }
            return sum;
        }

        public static int Sum(List<Int32> array)
        {
            int sum = 0;
            foreach(Int32 elem in array)
            {
                sum += elem;
            }
            return sum;
        }

        public static long Sum(List<Int64> array)
        {
            long sum = 0;
            foreach(Int64 elem in array)
            {
                sum += elem;
            }
            return sum;
        }

        public static float Sum(List<Single> array)
        {
            float sum = 0.0f;
            foreach(Single elem in array)
            {
                sum += elem;
            }
            return sum;
        }

        public static double Sum(List<Double> array)
        {
            double sum = 0.0;
            foreach(Double elem in array)
            {
                sum += elem;
            }
            return sum;
        }

        public static object Sum(IList array)
        {
            if(array is List<SByte>)
                return Sum(array as List<SByte>);
            else if(array is List<Int16>)
                return Sum(array as List<Int16>);
            else if(array is List<Int32>)
                return Sum(array as List<Int32>);
            else if(array is List<Int64>)
                return Sum(array as List<Int64>);
            else if(array is List<Single>)
                return Sum(array as List<Single>);
            else
                return Sum(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static int Prod(List<SByte> array)
        {
            int prod = 1;
            foreach(SByte elem in array)
            {
                prod *= elem;
            }
            return prod;
        }

        public static int Prod(List<Int16> array)
        {
            int prod = 1;
            foreach(Int16 elem in array)
            {
                prod *= elem;
            }
            return prod;
        }

        public static int Prod(List<Int32> array)
        {
            int prod = 1;
            foreach(Int32 elem in array)
            {
                prod *= elem;
            }
            return prod;
        }

        public static long Prod(List<Int64> array)
        {
            long prod = 1;
            foreach(Int64 elem in array)
            {
                prod *= elem;
            }
            return prod;
        }

        public static float Prod(List<Single> array)
        {
            float prod = 1.0f;
            foreach(Single elem in array)
            {
                prod *= elem;
            }
            return prod;
        }

        public static double Prod(List<Double> array)
        {
            double prod = 1.0;
            foreach(Double elem in array)
            {
                prod *= elem;
            }
            return prod;
        }

        public static object Prod(IList array)
        {
            if(array is List<SByte>)
                return Prod(array as List<SByte>);
            else if(array is List<Int16>)
                return Prod(array as List<Int16>);
            else if(array is List<Int32>)
                return Prod(array as List<Int32>);
            else if(array is List<Int64>)
                return Prod(array as List<Int64>);
            else if(array is List<Single>)
                return Prod(array as List<Single>);
            else
                return Prod(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static int Min(List<SByte> array)
        {
            int min = SByte.MaxValue;
            foreach(SByte elem in array)
            {
                min = Math.Min(min, elem);
            }
            return min;
        }

        public static int Min(List<Int16> array)
        {
            int min = Int16.MaxValue;
            foreach(Int16 elem in array)
            {
                min = Math.Min(min, elem);
            }
            return min;
        }

        public static int Min(List<Int32> array)
        {
            int min = Int32.MaxValue;
            foreach(Int32 elem in array)
            {
                min = Math.Min(min, elem);
            }
            return min;
        }

        public static long Min(List<Int64> array)
        {
            long min = Int64.MaxValue;
            foreach(Int64 elem in array)
            {
                min = Math.Min(min, elem);
            }
            return min;
        }

        public static float Min(List<Single> array)
        {
            float min = Single.MaxValue;
            foreach(Single elem in array)
            {
                min = Math.Min(min, elem);
            }
            return min;
        }

        public static double Min(List<Double> array)
        {
            double min = Double.MaxValue;
            foreach(Double elem in array)
            {
                min = Math.Min(min, elem);
            }
            return min;
        }

        public static object Min(IList array)
        {
            if(array is List<SByte>)
                return Min(array as List<SByte>);
            else if(array is List<Int16>)
                return Min(array as List<Int16>);
            else if(array is List<Int32>)
                return Min(array as List<Int32>);
            else if(array is List<Int64>)
                return Min(array as List<Int64>);
            else if(array is List<Single>)
                return Min(array as List<Single>);
            else
                return Min(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static int Max(List<SByte> array)
        {
            int max = SByte.MinValue;
            foreach(SByte elem in array)
            {
                max = Math.Max(max, elem);
            }
            return max;
        }

        public static int Max(List<Int16> array)
        {
            int max = Int16.MinValue;
            foreach(Int16 elem in array)
            {
                max = Math.Max(max, elem);
            }
            return max;
        }

        public static int Max(List<Int32> array)
        {
            int max = Int32.MinValue;
            foreach(Int32 elem in array)
            {
                max = Math.Max(max, elem);
            }
            return max;
        }

        public static long Max(List<Int64> array)
        {
            long max = Int64.MinValue;
            foreach(Int64 elem in array)
            {
                max = Math.Max(max, elem);
            }
            return max;
        }

        public static float Max(List<Single> array)
        {
            float max = Single.MinValue;
            foreach(Single elem in array)
            {
                max = Math.Max(max, elem);
            }
            return max;
        }

        public static double Max(List<Double> array)
        {
            double max = Double.MinValue;
            foreach(Double elem in array)
            {
                max = Math.Max(max, elem);
            }
            return max;
        }

        public static object Max(IList array)
        {
            if(array is List<SByte>)
                return Max(array as List<SByte>);
            else if(array is List<Int16>)
                return Max(array as List<Int16>);
            else if(array is List<Int32>)
                return Max(array as List<Int32>);
            else if(array is List<Int64>)
                return Max(array as List<Int64>);
            else if(array is List<Single>)
                return Max(array as List<Single>);
            else
                return Max(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static double Avg(List<SByte> array)
        {
            double avg = 0.0;
            int t = 1;
            foreach(SByte elem in array)
            {
                avg += (elem - avg) / t;
                ++t;
            }
            return avg;
        }

        public static double Avg(List<Int16> array)
        {
            double avg = 0.0;
            int t = 1;
            foreach(Int16 elem in array)
            {
                avg += (elem - avg) / t;
                ++t;
            }
            return avg;
        }

        public static double Avg(List<Int32> array)
        {
            double avg = 0.0;
            int t = 1;
            foreach(Int32 elem in array)
            {
                avg += (elem - avg) / t;
                ++t;
            }
            return avg;
        }

        public static double Avg(List<Int64> array)
        {
            double avg = 0.0;
            int t = 1;
            foreach(Int64 elem in array)
            {
                avg += (elem - avg) / t;
                ++t;
            }
            return avg;
        }

        public static double Avg(List<Single> array)
        {
            double avg = 0.0;
            int t = 1;
            foreach(Single elem in array)
            {
                avg += (elem - avg) / t;
                ++t;
            }
            return avg;
        }

        public static double Avg(List<Double> array)
        {
            double avg = 0.0;
            int t = 1;
            foreach(Double elem in array)
            {
                avg += (elem - avg) / t;
                ++t;
            }
            return avg;
        }

        public static double Avg(IList array)
        {
            if(array is List<SByte>)
                return Avg(array as List<SByte>);
            else if(array is List<Int16>)
                return Avg(array as List<Int16>);
            else if(array is List<Int32>)
                return Avg(array as List<Int32>);
            else if(array is List<Int64>)
                return Avg(array as List<Int64>);
            else if(array is List<Single>)
                return Avg(array as List<Single>);
            else
                return Avg(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static double Med(List<SByte> array)
        {
            if(array.Count == 0)
                return 0.0;

            if(array.Count % 2 == 1)
                return array[array.Count / 2];
            else
                return (array[array.Count / 2 - 1] + array[array.Count / 2]) / 2.0;
        }

        public static double Med(List<Int16> array)
        {
            if(array.Count == 0)
                return 0.0;

            if(array.Count % 2 == 1)
                return array[array.Count / 2];
            else
                return (array[array.Count / 2 - 1] + array[array.Count / 2]) / 2.0;
        }

        public static double Med(List<Int32> array)
        {
            if(array.Count == 0)
                return 0.0;

            if(array.Count % 2 == 1)
                return array[array.Count / 2];
            else
                return (array[array.Count / 2 - 1] + array[array.Count / 2]) / 2.0;
        }

        public static double Med(List<Int64> array)
        {
            if(array.Count == 0)
                return 0.0;

            if(array.Count % 2 == 1)
                return array[array.Count / 2];
            else
                return (array[array.Count / 2 - 1] + array[array.Count / 2]) / 2.0;
        }

        public static double Med(List<Single> array)
        {
            if(array.Count == 0)
                return 0.0;

            if(array.Count % 2 == 1)
                return array[array.Count / 2];
            else
                return (array[array.Count / 2 - 1] + array[array.Count / 2]) / 2.0;
        }

        public static double Med(List<Double> array)
        {
            if(array.Count == 0)
                return 0.0;

            if(array.Count % 2 == 1)
                return array[array.Count / 2];
            else
                return (array[array.Count / 2 - 1] + array[array.Count / 2]) / 2.0;
        }

        public static double Med(IList array)
        {
            if(array is List<SByte>)
                return Med(array as List<SByte>);
            else if(array is List<Int16>)
                return Med(array as List<Int16>);
            else if(array is List<Int32>)
                return Med(array as List<Int32>);
            else if(array is List<Int64>)
                return Med(array as List<Int64>);
            else if(array is List<Single>)
                return Med(array as List<Single>);
            else
                return Med(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static double MedUnordered(List<SByte> array)
        {
            List<SByte> arrayOrdered = ArrayOrderAscending(array);
            return Med(arrayOrdered);
        }

        public static double MedUnordered(List<Int16> array)
        {
            List<Int16> arrayOrdered = ArrayOrderAscending(array);
            return Med(arrayOrdered);
        }

        public static double MedUnordered(List<Int32> array)
        {
            List<Int32> arrayOrdered = ArrayOrderAscending(array);
            return Med(arrayOrdered);
        }

        public static double MedUnordered(List<Int64> array)
        {
            List<Int64> arrayOrdered = ArrayOrderAscending(array);
            return Med(arrayOrdered);
        }

        public static double MedUnordered(List<Single> array)
        {
            List<Single> arrayOrdered = ArrayOrderAscending(array);
            return Med(arrayOrdered);
        }

        public static double MedUnordered(List<Double> array)
        {
            List<Double> arrayOrdered = ArrayOrderAscending(array);
            return Med(arrayOrdered);
        }

        public static double MedUnordered(IList array)
        {
            if(array is List<SByte>)
                return MedUnordered(array as List<SByte>);
            else if(array is List<Int16>)
                return MedUnordered(array as List<Int16>);
            else if(array is List<Int32>)
                return MedUnordered(array as List<Int32>);
            else if(array is List<Int64>)
                return MedUnordered(array as List<Int64>);
            else if(array is List<Single>)
                return MedUnordered(array as List<Single>);
            else
                return MedUnordered(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static double Var(List<SByte> array)
        {
            if(array.Count == 0 || array.Count == 1)
                return 0.0;

            double avg = Avg(array);

            double var = 0.0;
            foreach(SByte elem in array)
            {
                var += (elem - avg) * (elem - avg);
            }
            return var / (array.Count - 1);
        }

        public static double Var(List<Int16> array)
        {
            if(array.Count == 0 || array.Count == 1)
                return 0.0;

            double avg = Avg(array);

            double var = 0.0;
            foreach(Int16 elem in array)
            {
                var += (elem - avg) * (elem - avg);
            }
            return var / (array.Count - 1);
        }

        public static double Var(List<Int32> array)
        {
            if(array.Count == 0 || array.Count == 1)
                return 0.0;

            double avg = Avg(array);

            double var = 0.0;
            foreach(Int32 elem in array)
            {
                var += (elem - avg) * (elem - avg);
            }
            return var / (array.Count - 1);
        }

        public static double Var(List<Int64> array)
        {
            if(array.Count == 0 || array.Count == 1)
                return 0.0;

            double avg = Avg(array);

            double var = 0.0;
            foreach(Int64 elem in array)
            {
                var += (elem - avg) * (elem - avg);
            }
            return var / (array.Count - 1);
        }

        public static double Var(List<Single> array)
        {
            if(array.Count == 0 || array.Count == 1)
                return 0.0;

            double avg = Avg(array);

            double var = 0.0;
            foreach(Single elem in array)
            {
                var += (elem - avg) * (elem - avg);
            }
            return var / (array.Count - 1);
        }

        public static double Var(List<Double> array)
        {
            if(array.Count == 0 || array.Count == 1)
                return 0.0;

            double avg = Avg(array);

            double var = 0.0;
            foreach(Double elem in array)
            {
                var += (elem - avg) * (elem - avg);
            }
            return var / (array.Count - 1);
        }

        public static double Var(IList array)
        {
            if(array is List<SByte>)
                return Var(array as List<SByte>);
            else if(array is List<Int16>)
                return Var(array as List<Int16>);
            else if(array is List<Int32>)
                return Var(array as List<Int32>);
            else if(array is List<Int64>)
                return Var(array as List<Int64>);
            else if(array is List<Single>)
                return Var(array as List<Single>);
            else
                return Var(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static double Dev(List<SByte> array)
        {
            return Math.Sqrt(Var(array));
        }

        public static double Dev(List<Int16> array)
        {
            return Math.Sqrt(Var(array));
        }

        public static double Dev(List<Int32> array)
        {
            return Math.Sqrt(Var(array));
        }

        public static double Dev(List<Int64> array)
        {
            return Math.Sqrt(Var(array));
        }

        public static double Dev(List<Single> array)
        {
            return Math.Sqrt(Var(array));
        }

        public static double Dev(List<Double> array)
        {
            return Math.Sqrt(Var(array));
        }

        public static double Dev(IList array)
        {
            if(array is List<SByte>)
                return Dev(array as List<SByte>);
            else if(array is List<Int16>)
                return Dev(array as List<Int16>);
            else if(array is List<Int32>)
                return Dev(array as List<Int32>);
            else if(array is List<Int64>)
                return Dev(array as List<Int64>);
            else if(array is List<Single>)
                return Dev(array as List<Single>);
            else
                return Dev(array as List<Double>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static bool And(List<Boolean> array)
        {
            if(array.Count == 0)
                return true;

            bool and = true;
            foreach(bool elem in array)
            {
                and = and && elem;
            }
            return and;
        }

        public static bool And(IList array)
        {
            return And(array as List<Boolean>);
        }

        /////////////////////////////////////////////////////////////////////////////////

        public static bool Or(List<Boolean> array)
        {
            if(array.Count == 0)
                return false;

            bool or = false;
            foreach(bool elem in array)
            {
                or = or || elem;
            }
            return or;
        }

        public static bool Or(IList array)
        {
            return Or(array as List<Boolean>);
        }
    }
}
