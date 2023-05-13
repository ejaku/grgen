/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    struct WeightedSample
    {
        public WeightedSample(double sample, double weight)
        {
            this.sample = sample;
            this.weight = weight;
        }

        public readonly double sample;
        public readonly double weight;

        public static int Compare(WeightedSample a, WeightedSample b)
        {
            return a.sample.CompareTo(b.sample);
        }
    }


    /// <summary>
    /// The Über-Statistical-Estimator implementing a staged, present leaning, truncated average.
    /// Staged similar to the remedian for efficient computation in case of a large number of samples,
    /// present leaning with weights to get a preference for more current values, going into the direction of a sliding average, 
    /// truncated average to achieve resilience against outliers (compared to the average),
    /// and to not underestimate the volume (compared to the median in case about half of the values are small).
    /// </summary>
    public class UberEstimator
    {
        private const int BASE = 21;
        private readonly List<List<WeightedSample>> accumulator; // staged store for estimates/samples, 
                // when level k completes, its estimate is added to level k+1, then it is reset

        ////////////////////////////////////////////////////////////////////

        public UberEstimator()
        {
            accumulator = new List<List<WeightedSample>>();
            accumulator.Add(new List<WeightedSample>(BASE));
        }

        public void Add(double sample)
        {
            accumulator[0].Add(new WeightedSample(sample, 1.0));
            if(accumulator[0].Count == BASE)
                CompleteLevel(0);
        }

        private void CompleteLevel(int level)
        {
            double weightedAverage = WeightedAverage(level);
            accumulator[level].Clear();
            if(accumulator.Count <= level + 1)
                accumulator.Add(new List<WeightedSample>(BASE));
            accumulator[level + 1].Add(new WeightedSample(weightedAverage, Weight(level + 1, accumulator[level + 1].Count + 1)));
            if(accumulator[level + 1].Count == BASE)
                CompleteLevel(level + 1);
        }

        private double WeightedAverage(int level)
        {
            accumulator[level].Sort(WeightedSample.Compare);
            double sumWeights = 0.0;
            for(int i = 0; i < accumulator[level].Count; ++i)
            {
                sumWeights += accumulator[level][i].weight;
            }
            double curSumWeights = 0.0;
            double weightedAverage = 0.0;
            int numSamplesAverage = 0;
            for(int i = 0; i < accumulator[level].Count; ++i)
            {
                curSumWeights += accumulator[level][i].weight;
                if(curSumWeights >= 0.75 * sumWeights)
                {
                    weightedAverage = weightedAverage / numSamplesAverage;
                    break;
                }
                if(curSumWeights >= 0.35 * sumWeights)
                {
                    weightedAverage += accumulator[level][i].sample;
                    ++numSamplesAverage;
                }
            }
            return weightedAverage;
        }

        private double Weight(int level, int position)
        {
            // the higher the level the more important does history become
            return 1.0 + level * 0.1 * position / BASE;
        }

        public double Get()
        {
            if(accumulator.Count == 1)
            {
                if(accumulator[0].Count == 0)
                    return 0.0;
                if(accumulator[0].Count == 1)
                    return accumulator[0][0].sample;
            }
            double curSumAverage = WeightedAverage(0); // the side-effect of sorting has no impact at level 0
            double curSumWeights = accumulator[0].Count;
            for(int level = 1; level < accumulator.Count; ++level)
            {
                double levelWeight = Math.Pow(BASE, level);
                for(int i = 0; i < accumulator[level].Count; ++i)
                {
                    curSumAverage += accumulator[level][i].sample * levelWeight;
                    curSumWeights += accumulator[level][i].weight * levelWeight;
                }
            }
            return curSumAverage / curSumWeights;
        }
    }
}
