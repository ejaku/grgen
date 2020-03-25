/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.containers.ArrayExtract;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayExtractNode extends ExprNode
{
	static {
		setName(ArrayExtractNode.class, "array extract");
	}

	private ExprNode targetExpr;
	private IdentNode attribute;
	private MemberDeclNode member;
	private NodeDeclNode node;
	private EdgeDeclNode edge;
	private VarDeclNode var;

	private ArrayTypeNode extractedArrayType;
	
	public ArrayExtractNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.attribute = attribute; 
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		boolean ownerResolveResult = targetExpr.resolve();
		if (!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}
		
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to extract method call must be of type array<T>.");
			return false;
		}
		
		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)
				&& !(arrayType.valueType instanceof DefinedMatchTypeNode)) {
			targetExpr.reportError("This argument to extract method call must be of type array<match<T>> or array<match<class T>> or array<T> where T extends node/edge");
			return false;
		}

		TypeNode valueType = arrayType.valueType;
		if(valueType instanceof MatchTypeNode) {
			MatchTypeNode matchType = (MatchTypeNode)valueType;
			if(!matchType.resolve()) {
				reportError("Unkown test/rule referenced by match type");
				return false;
			}
			TestDeclNode test = matchType.getTest();
			if(!test.resolve()) {
				reportError("Error in test/rule referenced by match type");
				return false;
			}
			node = test.tryGetNode(attribute);
			edge = test.tryGetEdge(attribute);
			var = test.tryGetVar(attribute);
			if(node==null && edge==null && var==null) {
				String memberName = attribute.toString();
				String actionName = test.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in test/rule " + actionName);
				return false;
			}
		} else if(valueType instanceof DefinedMatchTypeNode) {
			DefinedMatchTypeNode definedMatchType = (DefinedMatchTypeNode)valueType;
			/*if(!definedMatchType.resolve()) {
				reportError("Unkown match class referenced by match class type in match class filter function");
				return false;
			}*/
			node = definedMatchType.tryGetNode(attribute);
			edge = definedMatchType.tryGetEdge(attribute);
			var = definedMatchType.tryGetVar(attribute);
			if(node==null && edge==null && var==null) {
				String memberName = attribute.toString();
				String matchClassName = definedMatchType.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in match class type " + matchClassName);
				return false;
			}
		} else {
			ScopeOwner o = (ScopeOwner) valueType;
			o.fixupDefinition(attribute);
			if(!(valueType instanceof InheritanceTypeNode)) {
				String memberName = attribute.toString();
				reportError("Unknown member " + memberName);
			}
			InheritanceTypeNode inheritanceType = (InheritanceTypeNode)valueType;
			member = (MemberDeclNode)inheritanceType.getAllMembers().get(attribute.getIdent().toString());
			if(member == null) {
				String memberName = attribute.toString();
				reportError("Unknown member " + memberName + ", can't find in node/edge class type " + inheritanceType.getIdentNode().toString());
				return false;
			}
		}

		TypeNode type = getTypeOfElementToBeExtracted();
		if(!(type instanceof DeclaredTypeNode)) {
			reportError("The type "+ type + " is not an allowed type (basic type or node or edge class - set, map, array, deque are forbidden).");
			return false;
		}

		DeclaredTypeNode declType = (DeclaredTypeNode)type;
		extractedArrayType = ArrayTypeNode.getArrayType(declType.getIdentNode());
		
		return extractedArrayType.resolve();
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	public TypeNode getType() {
		assert(isResolved());
		return extractedArrayType;
	}

	private TypeNode getTypeOfElementToBeExtracted() {
		if(member!=null)
			return member.getDeclType();
		else if(node!=null)
			return node.getDeclType();
		else if(edge!=null)
			return edge.getDeclType();
		else if(var!=null)
			return var.getDeclType();
		return null;
	}
	
	@Override
	protected IR constructIR() {
		Entity accessedMember;
		if(member!=null)
			accessedMember = member.checkIR(Entity.class);
		else if(node!=null)
			accessedMember = node.checkIR(Entity.class);
		else if(edge!=null)
			accessedMember = edge.checkIR(Entity.class);
		else
			accessedMember = var.checkIR(Entity.class);
		
		return new ArrayExtract(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
