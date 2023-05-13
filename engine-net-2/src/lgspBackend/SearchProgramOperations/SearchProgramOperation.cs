/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class for all search program operations, containing concatenation fields,
    /// so that search program operations can form a linked search program list
    /// - double linked list; next points to the following list element or null;
    /// previous points to the preceding list element 
    /// or the enclosing search program operation within the list anchor element
    /// </summary>
    abstract class SearchProgramOperation
    {
        public SearchProgramOperation Next;
        public SearchProgramOperation Previous;

        /// <summary>
        /// dumps search program operation (as string) into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Dump(SourceBuilder builder);

        /// <summary>
        /// emits c# code implementing search program operation into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Emit(SourceBuilder sourceCode);

        /// <summary>
        /// Appends the given element to the search program operations list
        /// whose closing element until now was this element.
        /// Returns the new closing element - the given element.
        /// </summary>
        public SearchProgramOperation Append(SearchProgramOperation newElement)
        {
            Debug.Assert(Next == null, "Append only at end of list");
            Debug.Assert(newElement.Previous == null, "Append only of element without predecessor");
            Next = newElement;
            newElement.Previous = this;
            return newElement;
        }

        /// <summary>
        /// Insert the given element into the search program operations list
        /// between this and the succeeding element.
        /// Returns the element after this - the given element.
        /// </summary>
        public SearchProgramOperation Insert(SearchProgramOperation newElement)
        {
            Debug.Assert(newElement.Previous == null, "Insert only of single unconnected element (previous)");
            Debug.Assert(newElement.Next == null, "Insert only of single unconnected element (next)");
            
            if(Next == null)
                return Append(newElement);

            SearchProgramOperation Successor = Next;
            Next = newElement;
            newElement.Next = Successor;
            Successor.Previous = newElement;
            newElement.Previous = this;           
            return newElement;
        }

        /// <summary>
        /// returns whether operation is a search nesting operation 
        /// containing other elements within some list inside
        /// bearing the search nesting/iteration structure.
        /// default: false (cause only few operations are search nesting operations)
        /// </summary>
        public virtual bool IsSearchNestingOperation()
        {
            return false;
        }

        /// <summary>
        /// returns the nested search operations list anchor
        /// null if list not created or IsSearchNestingOperation == false.
        /// default: null (cause only few search operations are nesting operations)
        /// </summary>
        public virtual SearchProgramOperation GetNestedSearchOperationsList()
        {
            return null;
        }

        /// <summary>
        /// returns operation enclosing this operation
        /// </summary>
        public SearchProgramOperation GetEnclosingSearchOperation()
        {
            SearchProgramOperation potentiallyNestingOperation = this;
            SearchProgramOperation nestedOperation;

            // iterate list leftwards, leftmost list element is list anchor element,
            // which contains uplink to enclosing search operation in it's previous member
            // step over search nesting operations we're not nested in 
            do
            {
                nestedOperation = potentiallyNestingOperation;
                potentiallyNestingOperation = nestedOperation.Previous;
            }
            while(!potentiallyNestingOperation.IsSearchNestingOperation() 
                || potentiallyNestingOperation.GetNestedSearchOperationsList()!=nestedOperation);

            return potentiallyNestingOperation;
        }
    }

    /// <summary>
    /// Search program list anchor element,
    /// containing first list element within inherited Next member
    /// Inherited to be able to access the first element via Next
    /// Previous points to enclosing search program operation
    /// (starts list, but doesn't contain one)
    /// </summary>
    class SearchProgramList : SearchProgramOperation
    {
        public SearchProgramList(SearchProgramOperation enclosingOperation)
        {
            Previous = enclosingOperation;
        }

        public override void Dump(SourceBuilder builder)
        {
            SearchProgramOperation currentOperation = Next;

            // depth first walk over nested search program lists
            // walk current list here, recursive descent within local dump-methods
            while(currentOperation != null)
            {
                currentOperation.Dump(builder);
                currentOperation = currentOperation.Next;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            SearchProgramOperation currentOperation = Next;

            // depth first walk over nested search program lists
            // walk current list here, recursive descent within local Emit-methods
            while(currentOperation != null)
            {
                currentOperation.Emit(sourceCode);
                currentOperation = currentOperation.Next;
            }
        }
    }
}
