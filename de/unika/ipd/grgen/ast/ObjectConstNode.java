/**
 * A const node of type object.
 * There is exactly one possible value for constants of type object, namely
 * the value 'null' represented by {@see #NULL_VALUE NULL_VALUE}.
 * @see de.unika.ipd.grgen.ast.ObjectConstNode.NullObject
 *
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ast;
import de.unika.ipd.grgen.parser.Coords;

public class ObjectConstNode extends ConstNode
{
	/**
	 * @param coords The coordinates.
	 */
	public ObjectConstNode(Coords coords)
	{
		super(coords, "object", ObjectTypeNode.Value.NULL);
	}
	
	public TypeNode getType()
	{
		return BasicTypeNode.objectType;
	}
	
	protected ConstNode doCastTo(TypeNode type)
	{
		if ( type.isEqual(BasicTypeNode.objectType) ) return this;
		return ConstNode.getInvalid();
	}
	
	public String toString() {
		return "Const (object)null";
	}
}

