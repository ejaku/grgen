/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    public class Annotations : IEnumerable<KeyValuePair<string, string>>
    {
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return annotations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return annotations.GetEnumerator();
        }

        /// <summary>
        /// Indexer for the annotations, returns annotation of the corresponding name (in O(1) by Dictionary lookup).
        /// </summary>
        public object this[string key]
        {
            get
            {
                return annotations[key];
            }
        }

        /// <summary>
        /// Returns whether the annotations contain the given annotation.
        /// </summary>
        public bool ContainsAnnotation(string key)
        {
            return annotations.ContainsKey(key);
        }

        /// <summary>
        /// The annotations of the attribute, use the methods above for access (member only available for post-generation changes)
        /// </summary>
        public readonly IDictionary<string, string> annotations = new Dictionary<string, string>();
    }
}
