/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{
	using System;

	/// <summary>
	/// A post walker that visits only some of the nodes walked.
	/// </summary>
	public class ConstraintWalker : PostWalker
	{
		private class ConstraintVisitor : Visitor
		{
			/// <summary>
			/// A set containing all classes, that shall be visited </summary>
			internal Type[] classes;

			/// <summary>
			/// Visitor to invoke, if the walked class is legal. </summary>
			internal Visitor visitor;

			public ConstraintVisitor(Type[] classes, Visitor visitor)
			{
				this.classes = classes;
				this.visitor = visitor;
			}

			/// <seealso cref="de.unika.ipd.grgen.util.Visitor.visit(de.unika.ipd.grgen.util.Walkable)"/>
			public virtual void Visit(Walkable n)
			{
				for(int i = 0; i < classes.Length; i++)
				{
					if(classes[i].IsInstanceOfType(n))
					{
						visitor.Visit(n);
						return;
					}
				}
			}
		}

		/// <summary>
		/// Make a new constraint walker.
		/// The visitor is just called on objects that are instances
		/// of classes (and subclasses) in the <code>classes</code> array. </summary>
		/// <param name="classes"> An array containing all classes that shall be visited. </param>
		/// <param name="visitor"> The visitor to use. </param>
		public ConstraintWalker(Type[] classes, Visitor visitor)
			: base(new ConstraintVisitor(classes, visitor))
		{
		}

		/// <summary>
		/// Make a new constraint walker.
		/// The visitor is just called on objects that are instances
		/// of the class (and subclasses) given by <code>cl</code> </summary>
		/// <param name="cl"> The class whose objects shall be visited. </param>
		/// <param name="visitor"> The visitor to use. </param>
		public ConstraintWalker(Type cl, Visitor visitor)
			: base(new ConstraintVisitor(new Type[] {cl}, visitor))
		{
		}
	}

}
