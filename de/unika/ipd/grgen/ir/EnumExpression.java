/**
 * EnumExpression.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

public class EnumExpression extends Constant {
	
	private final EnumItem item;
	
	public EnumExpression(EnumType type, EnumItem item) {
		super(type, item.getValue().getValue());
		this.item = item;
		setName("enum expression");
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
	 */
	public String getNodeLabel() {
		return item + " " + getValue();
	}
	
}

