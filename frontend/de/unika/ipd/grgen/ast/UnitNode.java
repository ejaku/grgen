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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Action;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.Unit;

/**
 * The main node of the text. It is the root of the AST.
 */
public class UnitNode extends DeclNode
{
	static {
		setName(UnitNode.class, "unit declaration");
	}

	protected static final TypeNode mainType = new MainTypeNode();

	CollectNode models;
	CollectNode decls;

	/** Contains the classes of all valid types which can be declared */
	private static Class<?>[] validTypes = {
		TestDeclNode.class, RuleDeclNode.class
	};

	/**
	 * The filename for this main node.
	 */
	private String filename;

	public UnitNode(IdentNode id, String filename, CollectNode models, CollectNode patterns, CollectNode actions) {
		super(id, mainType);
		this.models = models;
		becomeParent(this.models);
		this.decls = actions;
		becomeParent(this.decls);
		this.filename = filename;
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(typeUnresolved);
		children.add(models);
		children.add(decls);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("models");
		childrenNames.add("decls");
		return childrenNames;
	}

  	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver declResolver = new DeclResolver(validTypes);
		successfullyResolved = decls.resolveChildren(declResolver) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = typeUnresolved.resolve() && successfullyResolved;
		successfullyResolved = models.resolve() && successfullyResolved;
		successfullyResolved = decls.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * The main node has an ident node and a collect node with
	 * - group declarations
	 * - edge class declarations
	 * - node class declarations
	 * as child.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker modelChecker = new CollectChecker(new SimpleChecker(ModelNode.class));
		Checker declChecker = new CollectChecker(new SimpleChecker(validTypes));
		return modelChecker.check(models, error)
			& declChecker.check(decls, error);
	}

	/**
	 * Get the IR unit node for this AST node.
	 * @return The Unit for this AST node.
	 */
	public Unit getUnit() {
		return (Unit) checkIR(Unit.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Ident id = (Ident) ident.checkIR(Ident.class);
		Unit res = new Unit(id, filename);

		for(BaseNode n : models.getChildren()) {
			Model model = ((ModelNode)n).getModel();
			res.addModel(model);
		}

		for(BaseNode n : decls.getChildren()) {
			Action act = ((ActionDeclNode)n).getAction();
			res.addAction(act);
		}

		return res;
	}
}
