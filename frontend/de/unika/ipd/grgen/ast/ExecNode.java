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
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;



import de.unika.ipd.grgen.ast.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

/**
 *
 */
public class ExecNode extends BaseNode {
	static {
		setName(ExecNode.class, "exec");
	}

	private static final CollectTripleResolver<VarDeclNode, NodeDeclNode, EdgeDeclNode> graphElementUsageOutsideOfCallResolver =
		new CollectTripleResolver<VarDeclNode, NodeDeclNode, EdgeDeclNode>(
		new DeclarationTripleResolver<VarDeclNode, NodeDeclNode, EdgeDeclNode>(VarDeclNode.class, NodeDeclNode.class, EdgeDeclNode.class));

	private StringBuilder sb = new StringBuilder();
	protected CollectNode<CallActionNode> callActions = new CollectNode<CallActionNode>();
	private CollectNode<VarDeclNode> varDecls = new CollectNode<VarDeclNode>();
	private CollectNode<IdentNode> graphElementUsageOutsideOfCallUnresolved = new CollectNode<IdentNode>();
	private CollectNode<DeclNode> graphElementUsageOutsideOfCall;

	public ExecNode(Coords coords) {
		super(coords);
		becomeParent(callActions);
	}

	public void append(Object n) {
		assert !isResolved();
		sb.append(n);
	}

	public String getXGRSString() {
		return sb.toString();
	}

	public void addCallAction(CallActionNode n) {
		assert !isResolved();
		becomeParent(n);
		callActions.addChild(n);
	}

	public void addVarDecls(VarDeclNode varDecl) {
		assert !isResolved();
		becomeParent(varDecl);
		varDecls.addChild(varDecl);
	}

	public void addGraphElementUsageOutsideOfCall(IdentNode id) {
		assert !isResolved();
		becomeParent(id);
		graphElementUsageOutsideOfCallUnresolved.addChild(id);
	}

	/** returns children of this node */
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> res = new Vector<BaseNode>();
		res.add(callActions);
		res.add(varDecls);
		res.add(getValidVersion(graphElementUsageOutsideOfCallUnresolved, graphElementUsageOutsideOfCall));
		return res;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("call actions");
		childrenNames.add("var decls");
		childrenNames.add("graph element usage outside of a call");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		Triple<CollectNode<VarDeclNode>, CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>> resolve =
			graphElementUsageOutsideOfCallResolver.resolve(graphElementUsageOutsideOfCallUnresolved);

		if (resolve != null) {
			if (resolve.first != null) {
				for (VarDeclNode c : resolve.first.getChildren()) {
					graphElementUsageOutsideOfCall.addChild(c);
				}
			}

			if (resolve.second != null) {
				for (NodeDeclNode c : resolve.second.getChildren()) {
					graphElementUsageOutsideOfCall.addChild(c);
				}
			}

			if (resolve.third != null) {
				for (EdgeDeclNode c : resolve.third.getChildren()) {
					graphElementUsageOutsideOfCall.addChild(c);
				}
			}
		}

		return resolve != null;
	}

	protected boolean checkLocal() {
		return true;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}

	protected IR constructIR() {
		Set<Entity> parameters = new LinkedHashSet<Entity>();
		for(DeclNode dn : graphElementUsageOutsideOfCall.getChildren())
			if(dn instanceof ConstraintDeclNode)
				parameters.add((Entity)dn.getIR());
		for(CallActionNode callActionNode : callActions.getChildren()) {
			callActionNode.checkPost();
			for(DeclNode param : callActionNode.getParams().getChildren())
				parameters.add((Entity) param.getIR());
		}
		Exec res= new Exec(getXGRSString(), parameters);
		return res;
	}
}

