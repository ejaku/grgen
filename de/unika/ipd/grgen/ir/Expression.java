/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An expression node.
 */
public abstract class Expression extends IR {

	/** The type of the expression. */
	protected Type type;

  public Expression(String name, Type type) {
  	super(name);
  	this.type = type;
  }

	public Type getType() {
		return type;
	}
}
