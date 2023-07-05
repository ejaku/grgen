/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;
using de.unika.ipd.grGen.libGr;
using System.IO;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A model of a GrGen graph, base class from implementation.
    /// Defines a thin layer above IGraphModel, enriched with index creating functionality, the real stuff is generated.
    /// It allows the generic LGSPGraph to create and bind the index set when it is configured with the graph model.
    /// </summary>
    public abstract class LGSPGraphModel : IGraphModel
    {
        public abstract String ModelName { get; }
        public abstract INodeModel NodeModel { get; }
        public abstract IEdgeModel EdgeModel { get; }
        public abstract IObjectModel ObjectModel { get; }
        public abstract ITransientObjectModel TransientObjectModel { get; }
        public abstract IEnumerable<String> Packages { get; }
        public abstract IEnumerable<EnumAttributeType> EnumAttributeTypes { get; }
        public abstract IEnumerable<ValidateInfo> ValidateInfo { get; }
        public abstract IEnumerable<IndexDescription> IndexDescriptions { get; }
        public abstract bool GraphElementUniquenessIsEnsured { get; }
        public abstract bool GraphElementsAreAccessibleByUniqueId { get; }
        public abstract bool AreFunctionsParallelized { get; }
        public abstract int BranchingFactorForEqualsAny { get; }
        public abstract int ThreadPoolSizeForSequencesParallelExecution { get; }
        public abstract IUniquenessHandler CreateUniquenessHandler(IGraph graph);
        public abstract IIndexSet CreateIndexSet(IGraph graph);
        public abstract void FillIndexSetAsClone(IGraph graph, IGraph originalGraph, IDictionary<IGraphElement, IGraphElement> oldToNewMap);

        #region Emitting and parsing of attributes of object or a user defined type

        public virtual object Parse(TextReader reader, AttributeType attrType, IGraph graph)
        {
            reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l'
            return null;
        }

        public virtual string Serialize(object attribute, AttributeType attrType, IGraph graph)
        {
            ConsoleUI.outWriter.WriteLine("Warning: Exporting attribute of object type to null");
            return "null";
        }

        public virtual string Emit(object attribute, AttributeType attrType, IGraph graph)
        {
            return attribute != null ? attribute.ToString() : "null";
        }

        public virtual void External(string line, IGraph graph)
        {
            ConsoleUI.outWriter.Write("Ignoring: ");
            ConsoleUI.outWriter.WriteLine(line);
        }

        public virtual INamedGraph AsGraph(object attribute, AttributeType attrType, IGraph graph)
        {
            return null;
        }

        #endregion Emitting and parsing of attributes of object or a user defined type

        #region Comparison of attributes of object or user defined type, external types in general

        public abstract ExternalObjectType[] ExternalObjectTypes { get; }

        public virtual bool IsEqualClassDefined
        {
            get { return false; }
        }

        public virtual bool IsLowerClassDefined
        {
            get { return false; }
        }

        public virtual bool IsEqual(object this_, object that, IDictionary<object, object> visitedObjects)
        {
            return this_ == that; // reference comparison
        }

        public virtual bool IsLower(object this_, object that, IDictionary<object, object> visitedObjects)
        {
            return this_ == that; // dummy implementation
        }

        #endregion Comparison of attributes of object or user defined type, external types in general

        #region Array processing helpers

        public abstract IList ArrayOrderAscendingBy(IList array, string member);
        public abstract IList ArrayOrderDescendingBy(IList array, string member);
        public abstract IList ArrayGroupBy(IList array, string member);
        public abstract IList ArrayKeepOneForEach(IList array, string member);

        public abstract int ArrayIndexOfBy(IList array, string member, object value);
        public abstract int ArrayIndexOfBy(IList array, string member, object value, int startIndex);
        public abstract int ArrayLastIndexOfBy(IList array, string member, object value);
        public abstract int ArrayLastIndexOfBy(IList array, string member, object value, int startIndex);
        public abstract int ArrayIndexOfOrderedBy(IList array, string member, object value);

        #endregion Array processing helpers

        public abstract void FailAssertion();
        public abstract String MD5Hash { get; }
    }
}
