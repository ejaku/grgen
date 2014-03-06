/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A class for managing deferred sequence execution.
    /// </summary>
    public class LGSPDeferredSequencesManager
    {
        public LGSPDeferredSequencesManager()
        {
            toBeExecuted = new Stack<Queue<LGSPEmbeddedSequenceClosure>>();
        }

        public void EnterRuleModifyAddingDeferredSequences()
        {
            toBeExecuted.Push(new Queue<LGSPEmbeddedSequenceClosure>());
        }

        public void AddDeferredSequence(LGSPEmbeddedSequenceClosure closure)
        {
            toBeExecuted.Peek().Enqueue(closure);
        }

        public void ExecuteDeferredSequencesThenExitRuleModify(LGSPGraphProcessingEnvironment procEnv)
        {
            while(toBeExecuted.Peek().Count > 0)
                toBeExecuted.Peek().Dequeue().exec(procEnv);
            toBeExecuted.Pop();
        }

        /// <summary>
        /// A global stack of queues with the sequences to be executed after execution of the current rule/test.
        /// These are sequences with their needed environment, used from within subpatterns/alternatives/iterateds.
        /// For every exec entry a queue is pushed, for every exit popped, to ensure that the exec processing
        /// of a nested rule is not executing the execs of the calling rule.
        /// </summary>
        private static Stack<Queue<LGSPEmbeddedSequenceClosure>> toBeExecuted;
    }
}
