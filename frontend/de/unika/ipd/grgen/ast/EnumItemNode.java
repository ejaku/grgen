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

import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.util.BooleanResultVisitor;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.Walkable;
import de.unika.ipd.grgen.util.Walker;

/**
 * A class for enum items.
 */
public class EnumItemNode extends MemberDeclNode
{
	static {
		setName(EnumItemNode.class, "enum item");
	}
	
	ExprNode value;
	
	/** Position of this item in the enum. */
	private final int pos;
	
	/**
	 * Make a new enum item node.
	 */
	public EnumItemNode(IdentNode identifier, IdentNode type, ExprNode value, int pos)
	{
		super(identifier, type);
		this.value = value;
		becomeParent(this.value);
		this.pos = pos;
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(type);
		children.add(value);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident"); 
		childrenNames.add("type");
		childrenNames.add("value");
		return childrenNames;
	}
	
  	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		BaseNode resolved = typeResolver.resolve(type);
		successfullyResolved = resolved!=null && successfullyResolved;
		type = ownedResolutionResult(type, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = type.resolve() && successfullyResolved;
		successfullyResolved = value.resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			childrenChecked = ident.check() && childrenChecked;
			childrenChecked = type.check() && childrenChecked;
			childrenChecked = value.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()
	{
		Checker typeChecker = new SimpleChecker(new Class[] { EnumTypeNode.class });
		return (new SimpleChecker(IdentNode.class)).check(ident, error)
			&& typeChecker.check(type, error)
			&& (new SimpleChecker(ExprNode.class)).check(value, error);
	}
	
	/**
	 * Check the validity of the initialisation expression.
	 * @return true, if the init expression is ok, false if not.
	 */
	protected boolean checkType()
	{
		final EnumItemNode thisOne = this;
		
		// Check, if this enum item was defined with a latter one.
		// This may not be.
		BooleanResultVisitor v = new BooleanResultVisitor(true)
		{
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
		w.walk(this.value);
		
		if(!v.booleanResult()) {
			return false;
		}
		
		if(!value.isConst()) {
			reportError("Initialization of enum item is not constant");
			return false;
		}
		
		// Adjust the values type to int, else emit an error.
		if(value.getType().isCompatibleTo(BasicTypeNode.intType)) {
			ExprNode adjusted = value.adjustType(BasicTypeNode.intType);
			becomeParent(adjusted);
			value = adjusted;
		} else {
			reportError("The type of the initializator must be integer");
			return false;
		}
		
		return true;
	}
	
	protected ConstNode getValue()
	{
		// TODO are we allowed to cast to a ConstNode here???
		ConstNode res = value.getConst().castTo(BasicTypeNode.intType);
		debug.report(NOTE, "type: " + res.getType());
		
		Object obj = res.getValue();
		if ( ! (obj instanceof Integer) ) {
			return ConstNode.getInvalid();
		}
		
		int v = ((Integer) obj).intValue();
		debug.report(NOTE, "result: " + res);
		
		return new EnumConstNode(getCoords(), getIdentNode(), v);
	}
	
	protected EnumItem getItem()
	{
		return (EnumItem) checkIR(EnumItem.class);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR()
	{
		IdentNode id = (IdentNode) ident;
		ConstNode c = getValue();
		
		assert ( ! c.equals(ConstNode.getInvalid()) ):
			"an error should have been reported in an earlier phase";

		c.castTo(BasicTypeNode.intType);
		return new EnumItem(id.getIdent(), getValue().getConstant());
	}
	
	public static String getUseStr()
	{
		return "enum item";
	}
}
