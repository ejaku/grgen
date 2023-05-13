/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a lambda expression filter call,
    /// employing a filter containing a lambda sequence expression on the matches array (stemming from an action or a match class).
    /// Built by the sequence parser, may be built by the user at API level (employing the sequence expression parser, or potentially building the sequence expression directly), 
    /// in order to carry out lambda expression filter calls (currently supported ("package prefixed") filter names: <![CDATA[removeIf, assign<match-element-name>]]>).
    /// </summary>
    public class FilterCallWithLambdaExpression : FilterCall
    {
        public FilterCallWithLambdaExpression(String packagePrefixedName,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpression)
            : base(packagePrefixedName)
        {
            this.arrayAccess = arrayAccess;
            this.index = index;
            this.element = element;
            this.lambdaExpression = lambdaExpression;
        }

        public FilterCallWithLambdaExpression(String packagePrefixedName,
            SequenceVariable initArrayAccess, SequenceExpression initExpression,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpression)
        : base(packagePrefixedName)
        {
            this.initArrayAccess = initArrayAccess;
            this.initExpression = initExpression;
            this.arrayAccess = arrayAccess;
            this.previousAccumulationAccess = previousAccumulationAccess;
            this.index = index;
            this.element = element;
            this.lambdaExpression = lambdaExpression;
        }

        public string Entity
        {
            get
            {
                if(PackagePrefixedName.IndexOf('<') == -1)
                    return null;
                int beginIndexOfEntity = PackagePrefixedName.IndexOf('<') + 1;
                int length = PackagePrefixedName.Length - beginIndexOfEntity - 1; // removes closing '>'
                return PackagePrefixedName.Substring(beginIndexOfEntity, length);
            }
        }

        public string PlainName
        {
            get
            {
                if(PackagePrefixedName.IndexOf('<') == -1)
                    return PackagePrefixedName;
                return PackagePrefixedName.Substring(0, PackagePrefixedName.IndexOf('<'));
            }
        }

        /// <summary>
        /// The optional init array access variable (used to access the array a second time during the iteration)
        /// </summary>
        public readonly SequenceVariable initArrayAccess;

        /// <summary>
        /// The optional init expression evaluated before the sequence of lambda expression evaluations.
        /// </summary>
        public readonly SequenceExpression initExpression;

        /// <summary>
        /// The array access variable (used to access the array a second time during the iteration) -- optional, null if does not apply.
        /// </summary>
        public readonly SequenceVariable arrayAccess;

        /// <summary>
        /// The previous accumulation access variable (used to access the accumulation variable value of the previous iteration) -- optional, null if does not apply.
        /// </summary>
        public readonly SequenceVariable previousAccumulationAccess;

        /// <summary>
        /// The index variable (gives the number of the current match in the matches array) -- optional, null if does not apply.
        /// </summary>
        public readonly SequenceVariable index;

        /// <summary>
        /// The element variable (used for iterating over the content of the matches array).
        /// </summary>
        public readonly SequenceVariable element;

        /// <summary>
        /// The lambda expression evaluated for each match.
        /// </summary>
        public readonly SequenceExpression lambdaExpression;
    }
}
