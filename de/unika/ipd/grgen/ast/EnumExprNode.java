/**
 * EnumExprNpde.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class EnumExprNode extends QualIdentNode implements DeclaredCharacter {
	
	static {
		setName(EnumExprNode.class, "enum access expression");
	}
	
	private static final Resolver ownerResolver =
		new DeclTypeResolver(EnumTypeNode.class);
	
	private static final Resolver declResolver =
		new DeclResolver(EnumItemNode.class);
	
	public EnumExprNode(Coords c, BaseNode owner, BaseNode member) {
		super(c, owner, member);
	}
	
	/**
	 * Resolve the identifier nodes.
	 * Each AST node can add resolvers to a list administrated by
	 * {@link BaseNode}. These resolvers replace the identifier node in the
	 * childrens of this node by something, that can be produced out of it.
	 * For example, an identifier representing a declared type is replaced by
	 * the declared type. The behaviour depends on the {@link Resolver}.
	 *
	 * This method first call all resolvers registered in this node
	 * and descends to the this node's children by invoking
	 * {@link #getResolve()} on each child.
	 *
	 * A base node subclass can overload this method, to apply another
	 * policy of resultion.
	 *
	 * @return true, if all resolvers finished their job and no error
	 * occurred, false, if there was some error.
	 */
	protected boolean resolve() {
		boolean res = false;
		IdentNode member = (IdentNode) getChild(MEMBER);
		
		ownerResolver.resolve(this, OWNER);
		BaseNode owner = getChild(OWNER);
		res = owner.getResolve();
		
		if(owner instanceof EnumTypeNode) {
			EnumTypeNode enumType = (EnumTypeNode) owner;
			enumType.fixupDefinition(member);
			declResolver.resolve(this, MEMBER);
			res = getChild(MEMBER).getResolve();
		} else {
			reportError("left hand side of :: is not an emum type");
			res = false;
		}
			
		setResolved(res);
		return res;
	}
		
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(OWNER, EnumTypeNode.class)
			&& checkChild(MEMBER, EnumItemNode.class);
	}
	
	/**
	 * Build the IR of an enum expression.
	 * @return An enum expression IR object.
	 */
	protected IR constructIR() {
		EnumType et = (EnumType) getChild(OWNER).checkIR(EnumType.class);
		EnumItem it = (EnumItem) getChild(MEMBER).checkIR(EnumItem.class);
		return new EnumExpression(et, it);
	}

}

