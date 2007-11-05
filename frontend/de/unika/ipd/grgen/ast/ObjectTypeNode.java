/**
 * Represents the basic type 'object'
 *
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ast;
import de.unika.ipd.grgen.ir.ObjectType;
import de.unika.ipd.grgen.ir.IR;



public class ObjectTypeNode extends BasicTypeNode
{
	/**
	 * Singleton class representing the only constant value 'null' that
	 * the basic type 'object' has.
	 */
	public static class Value
	{
		public static Value NULL = new Value()
		{
			public String toString() { return "Const null"; }
		};

		private Value() {}

		public boolean equals(Object val)
		{
			return (this == val);
		}
	}

	protected static ObjectTypeNode OBJECT_TYPE = new ObjectTypeNode();

	private ObjectTypeNode() {}
	
	protected IR constructIR()
	{
		return new ObjectType(getIdentNode().getIdent());
	}

	public String toString() {
		return "object";
	}
}

