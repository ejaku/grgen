/**
 * TypeConstNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.TypeExprConst;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Iterator;

/**
 * A type expression constant.
 */
public class TypeConstNode extends TypeExprNode {
	
	static {
		setName(TypeConstNode.class, "type expression const");
	}
	
	private static final int OPERANDS = 0;
	
	private static final Resolver typeResolver =
		new CollectResolver(new DeclTypeResolver(InheritanceTypeNode.class));
	
	private static final Checker typeChecker =
		new CollectChecker(new SimpleChecker(InheritanceTypeNode.class));
	
	public TypeConstNode(Coords coords, BaseNode collect) {
		super(coords, SET);
		addResolver(OPERANDS, typeResolver);
		addChild(collect);
	}
	
	public TypeConstNode(BaseNode singleton) {
		this(singleton.getCoords(), new CollectNode());
		getChild(OPERANDS).addChild(singleton);
	}
	
	protected boolean check() {
		return checkChild(OPERANDS, typeChecker);
	}

	protected IR constructIR() {
		TypeExprConst cnst = new TypeExprConst();
		
		for(Iterator i = getChild(OPERANDS).getChildren(); i.hasNext();) {
			BaseNode n = (BaseNode) i.next();
			InheritanceType inh = (InheritanceType) n.checkIR(InheritanceType.class);
			cnst.addOperand(inh);
		}
		
		return cnst;
	}
	
}

