/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An expression node.
 */
public abstract class Expression extends IR
{
	private static final String[] childrenNames = { "type" };
	
	/** The type of the expression. */
	protected Type type;
	
	public Expression(String name, Type type)
	{
		super(name);
		setChildrenNames(childrenNames);
		this.type = type;
	}
	
	/**
	 * Get the type of the expression.
	 * @return The type of the expression.
	 */
	public Type getType()
	{
		return type;
	}
	
}
