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

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

public class EdgeTypeNode extends InheritanceTypeNode
{
	static {
		setName(EdgeTypeNode.class, "edge type");
	}

	BaseNode cas; // connection assertions

	private static final Checker casChecker = // TODO use this
		new CollectChecker(new SimpleChecker(ConnAssertNode.class));

	/**
	 * Make a new edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public EdgeTypeNode(CollectNode ext, CollectNode cas, CollectNode body,
			int modifiers, String externalName) {
		this.extend = ext==null ? NULL : ext;
		becomeParent(this.extend);
		this.body = body==null ? NULL : body;
		becomeParent(this.body);
		this.cas = cas==null ? NULL : cas;
		becomeParent(this.cas);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(extend);
		children.add(body);
		children.add(cas);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		childrenNames.add("cas");
		return childrenNames;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver bodyResolver = new DeclResolver(new Class[] {MemberDeclNode.class, MemberInitNode.class});
		Resolver extendsResolver = new DeclTypeResolver(EdgeTypeNode.class);
		Resolver casResolver = new DeclTypeResolver(ConnAssertNode.class);
		successfullyResolved = ((CollectNode)body).resolveChildren(bodyResolver) && successfullyResolved;
		successfullyResolved = ((CollectNode)extend).resolveChildren(extendsResolver) && successfullyResolved;
		successfullyResolved = ((CollectNode)cas).resolveChildren(casResolver) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = extend.resolve() && successfullyResolved;
		successfullyResolved = body.resolve() && successfullyResolved;
		successfullyResolved = cas.resolve() && successfullyResolved;
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
			
			childrenChecked = extend.check() && childrenChecked;
			childrenChecked = body.check() && childrenChecked;
			childrenChecked = cas.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() 
	{
		Checker extendsChecker = new CollectChecker(new SimpleChecker(EdgeTypeNode.class));
		return super.checkLocal()
			&& extendsChecker.check(extend, error);
	}
	
	/**
	 * Get the edge type IR object.
	 * @return The edge type IR object for this AST node.
	 */
	public EdgeType getEdgeType() {
		return (EdgeType) checkIR(EdgeType.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() 
	{
		EdgeType et = new EdgeType(getDecl().getIdentNode().getIdent(),
				getIRModifiers(), getExternalName());

		constructIR(et);

		for(BaseNode n : cas.getChildren()) {
			ConnAssertNode can = (ConnAssertNode)n;
			et.addConnAssert((ConnAssert)can.checkIR(ConnAssert.class));
		}

		return et;
	}

	public static String getKindStr() {
		return "edge type";
	}
	
	public static String getUseStr() {
		return "edge type";
	}
}
