/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A walker, that visits only some of the nodes walked.
 */
public class ConstraintWalker extends PostWalker {

	static private class ConstraintVisitor implements Visitor {
		
		/** A set containing all classes, that shall be visited */
		private Class<?>[] classes;
		
		/** Visitor to invoke, if the walked class is legal. */
		private Visitor visitor;
		
		public ConstraintVisitor(Class<?>[] classes, Visitor visitor) {
			this.classes = classes;
			this.visitor = visitor;
		}
		
	  /**
     * @see de.unika.ipd.grgen.util.Visitor#visit(de.unika.ipd.grgen.util.Walkable)
     */
    public void visit(Walkable n) {
    	for(int i = 0; i < classes.length; i++)
    		if(classes[i].isInstance(n)) {
    			visitor.visit(n);
    			return;
    		}
    }
	}

  /**
   * Make a new constraint walker.
   * The visitor is just called on objects that are instances of classes
   * (and subclasses) in the <code>classes</code> array.
   * @param classes An array containing all classes that shall be visited.
   * @param visitor The visitor to use for visiting the nodes.
   */
  public ConstraintWalker(Class<?>[] classes, Visitor visitor) {
    super(new ConstraintVisitor(classes, visitor));
  }
  
  /**
   * Make a new constraint walker.
   * The visitor is just called on called on objects that are instances
   * of the class given by <code>cl</code>
   * @param cl The class whose objects shall be visited.
   * @param visitor The visitor to use.
   */
  public ConstraintWalker(Class<?> cl, Visitor visitor) {
  	super(new ConstraintVisitor(new Class<?>[] { cl }, visitor));
  }

}
