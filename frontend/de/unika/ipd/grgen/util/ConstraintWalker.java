/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A post walker that visits only some of the nodes walked.
 */
public class ConstraintWalker extends PostWalker
{
	private static class ConstraintVisitor implements Visitor
	{
		/** A set containing all classes, that shall be visited */
		private Class<?>[] classes;

		/** Visitor to invoke, if the walked class is legal. */
		private Visitor visitor;

		public ConstraintVisitor(Class<?>[] classes, Visitor visitor)
		{
			this.classes = classes;
			this.visitor = visitor;
		}

		/**
		 * @see de.unika.ipd.grgen.util.Visitor#visit(de.unika.ipd.grgen.util.Walkable)
		 */
		public void visit(Walkable n)
		{
			for (int i = 0; i < classes.length; i++) {
				if (classes[i].isInstance(n)) {
					visitor.visit(n);
					return;
				}
			}
		}
	}

	/**
	 * Make a new constraint walker.
	 * The visitor is just called on objects that are instances
	 * of classes (and subclasses) in the <code>classes</code> array.
	 * @param classes An array containing all classes that shall be visited.
	 * @param visitor The visitor to use.
	 */
	public ConstraintWalker(Class<?>[] classes, Visitor visitor)
	{
		super(new ConstraintVisitor(classes, visitor));
	}

	/**
	 * Make a new constraint walker.
	 * The visitor is just called on objects that are instances
	 * of the class (and subclasses) given by <code>cl</code>
	 * @param cl The class whose objects shall be visited.
	 * @param visitor The visitor to use.
	 */
	public ConstraintWalker(Class<?> cl, Visitor visitor)
	{
		super(new ConstraintVisitor(new Class<?>[] { cl }, visitor));
	}
}
