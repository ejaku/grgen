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
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.EnumTypeNode;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.util.BooleanResultVisitor;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.Walkable;
import de.unika.ipd.grgen.util.Walker;

/**
 * A class for enum items.
 */
public class EnumItemNode extends MemberDeclNode {

	static {
		setName(EnumItemNode.class, "enum item");
	}

	/** Index of the value child. */
	private static final int VALUE = LAST + 1;
	
	/** Position of this item in the enum. */
	private final int pos;
	
	private static final Resolver typeResolver =
		new DeclTypeResolver(EnumTypeNode.class);

  /**
   * Make a new enum item node.
   */
  public EnumItemNode(IdentNode identifier, BaseNode type, BaseNode value, int pos) {
    super(identifier, type);
    this.pos = pos;
    addChild(value);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
 		return checkChild(IDENT, IdentNode.class)
			&& checkChild(TYPE, EnumTypeNode.class)
      && checkChild(VALUE, ExprNode.class);
  }
  
  /**
   * Check the validity of the initialisation expression.
   * @return true, if the init expression is ok, false if not.
   */
  protected boolean checkType() {
  	final EnumItemNode thisOne = this;
  	
  	// Check, if this enum item was defined with a latter one.
  	// This may not be.
  	BooleanResultVisitor v = new BooleanResultVisitor(true) {
  		public void visit(Walkable w) {
  			if(w instanceof EnumItemNode) {
  				EnumItemNode item = (EnumItemNode) w;
  				if(item.pos <= pos) {
  					thisOne.reportError("Enum item must not be defined with a previous one");
  					setResult(false);
  				}
  			}
  			
  		}
  	};
  	
  	Walker w = new PostWalker(v);
  	w.walk(getChild(VALUE));
  	
  	if(!v.booleanResult())
  		return false;
  		
  	
  	ExprNode value = (ExprNode) getChild(VALUE);
	
  	if(!value.isConst()) {
  		reportError("Initialization of enum item is not constant");
  		return false;
  	}
  	
  	// Adjust the values type to int, else emit an error.
  	if(value.getType().isCompatibleTo(BasicTypeNode.intType)) {
			replaceChild(VALUE, value.adjustType(BasicTypeNode.intType));
  	} else {
  		reportError("The type of the initializator must be integer");
  		return false;
  	}
  	
  	return true;
  }
  
  protected ConstNode getValue() {
  	ExprNode expr = (ExprNode) getChild(VALUE);
  	// TODO are we allowed to cast to a ConstNode here???
  	ConstNode res = expr.getConst().castTo(BasicTypeNode.intType);
  	debug.report(NOTE, "type: " + res.getType());
  	int v = ((Integer) res.getValue()).intValue();
  	debug.report(NOTE, "result: " + res);
  	
  	return new EnumConstNode(getCoords(), getIdentNode(), v);
  }

	protected EnumItem getItem() {
		return (EnumItem) checkIR(EnumItem.class);
	}

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
		IdentNode id = (IdentNode) getChild(IDENT);
		//ConstNode c = getValue().castTo(BasicTypeNode.intType);
  	return new EnumItem(id.getIdent(), getValue().getConstant());
  }

  public static String getUseStr()
  {
	  return "enum item";
  }
}
