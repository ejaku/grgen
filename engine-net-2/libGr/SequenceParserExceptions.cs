using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An exception thrown by SequenceParser describing,
    /// which rule caused the problem and how it was used
    /// </summary>
    public class SequenceParserRuleException : Exception
    {
        /// <summary>
        /// The name of the rule.
        /// </summary>
        public String RuleName;

        /// <summary>
        /// The associated action instance. If it is null, there was no rule with the name specified in RuleName.
        /// </summary>
        public IAction Action;

        /// <summary>
        /// The number of inputs given to the rule.
        /// </summary>
        public int NumGivenInputs;

        /// <summary>
        /// The number of outputs given to the rule.
        /// </summary>
        public int NumGivenOutputs;

        /// <summary>
        /// Creates an instance of a SequenceParserRuleException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">The name of the rule.</param>
        /// <param name="action">The associated action instance.
        /// If it is null, there was no rule with the name specified in RuleName.</param>
        /// <param name="numGivenInputs">The number of inputs given to the rule.</param>
        /// <param name="numGivenOutputs">The number of outputs given to the rule.</param>
        public SequenceParserRuleException(String ruleName, IAction action, int numGivenInputs, int numGivenOutputs)
        {
            RuleName = ruleName;
            Action = action;
            NumGivenInputs = numGivenInputs;
            NumGivenOutputs = numGivenOutputs;
        }
    }
}
