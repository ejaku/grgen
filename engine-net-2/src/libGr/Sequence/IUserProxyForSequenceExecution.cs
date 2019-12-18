/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A proxy querying or simulating a user for choices during sequence execution
    /// TODO: general user proxy, not just for sequence execution
    /// </summary>
    public interface IUserProxyForSequenceExecution
    {
        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        int ChooseDirection(int direction, Sequence seq);

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq);

        /// <summary>
        /// returns the maybe user altered point within the interval series, denoting the sequence to execute next
        /// the randomly chosen point is supplied; the sequence with the intervals and their corresponding sequences is supplied
        /// </summary>
        double ChoosePoint(double pointToExecute, SequenceWeightedOne seq);

        /// <summary>
        /// returns the maybe user altered match to execute next for the sequence given
        /// the randomly chosen total match is supplied; the sequence with the rules and matches is supplied
        /// </summary>
        int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq);

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0.0 - 1.0 exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        double ChooseRandomNumber(double randomNumber, Sequence seq);

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        object ChooseValue(string type, Sequence seq);
    }
}
