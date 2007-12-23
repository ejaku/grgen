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

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

public class EdgeTypeNode extends InheritanceTypeNode
{
	static {
		setName(EdgeTypeNode.class, "edge type");
	}

	private static final int CAS = 2; // connection assertions

	private static final String[] childrenNames = {
		"extends", "body", "cas"
	};

	private static final Checker extendsChecker =
		new CollectChecker(new SimpleChecker(EdgeTypeNode.class));

	private static final Checker casChecker = // TODO use this
		new CollectChecker(new SimpleChecker(ConnAssertNode.class));

	private static final Resolver extendsResolver =
		new CollectResolver(new DeclTypeResolver(EdgeTypeNode.class));

	private static final Resolver casResolver =
		new CollectResolver(new DeclTypeResolver(ConnAssertNode.class));

	/**
	 * Make a new edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public EdgeTypeNode(CollectNode ext, CollectNode cas,  CollectNode body,
			int modifiers, String externalName) {
		super(ext, body, extendsChecker);
		addChild(cas);
		setChildrenNames(childrenNames);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = bodyResolver.resolve(this, BODY) && successfullyResolved;
		successfullyResolved = extendsResolver.resolve(this, EXTENDS) && successfullyResolved;
		successfullyResolved = casResolver.resolve(this, CAS) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(EXTENDS).resolve() && successfullyResolved;
		successfullyResolved = getChild(BODY).resolve() && successfullyResolved;
		successfullyResolved = getChild(CAS).resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = check();
		nodeCheckedSetResult(successfullyChecked);
		if(successfullyChecked) {
			assert(!isTypeChecked());
			successfullyChecked = typeCheck();
			nodeTypeCheckedSetResult(successfullyChecked);
		}
		
		successfullyChecked = getChild(EXTENDS).doCheck() && successfullyChecked;
		successfullyChecked = getChild(BODY).doCheck() && successfullyChecked;
		successfullyChecked = getChild(CAS).doCheck() && successfullyChecked;
		return successfullyChecked;
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

		for(BaseNode n : getChild(CAS).getChildren()) {
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
