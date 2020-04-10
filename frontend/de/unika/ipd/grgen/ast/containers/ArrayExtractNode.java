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
	private DeclNode member;

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
		if(valueType instanceof MatchTypeIteratedNode) {
			MatchTypeIteratedNode matchType = (MatchTypeIteratedNode)valueType;
			if(!matchType.resolve()) {
				return false;
			}
			TestDeclNode test = matchType.getTest();
			IteratedNode iterated = matchType.getIterated();
			member = matchType.tryGetMember(attribute.toString());
			if(member == null) {
				String memberName = attribute.toString();
				String actionName = test.getIdentNode().toString();
				String iteratedName = iterated.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in iterated " + iteratedName + " of test/rule " + actionName);
				return false;
			}
		} else if(valueType instanceof MatchTypeNode) {
			MatchTypeNode matchType = (MatchTypeNode)valueType;
			if(!matchType.resolve()) {
				return false;
			}
			TestDeclNode test = matchType.getTest();
			member = matchType.tryGetMember(attribute.toString());
			if(member == null) {
				String memberName = attribute.toString();
				String actionName = test.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in test/rule " + actionName);
				return false;
			}
		} else if(valueType instanceof DefinedMatchTypeNode) {
			DefinedMatchTypeNode definedMatchType = (DefinedMatchTypeNode)valueType;
			if(!definedMatchType.resolve()) {
				return false;
			}
			member = definedMatchType.tryGetMember(attribute.toString());
			if(member == null) {
				String memberName = attribute.toString();
				String matchClassName = definedMatchType.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in match class type " + matchClassName);
				return false;
			}
		} else if(valueType instanceof InheritanceTypeNode) {
			ScopeOwner o = (ScopeOwner) valueType;
			o.fixupDefinition(attribute);
			InheritanceTypeNode inheritanceType = (InheritanceTypeNode)valueType;
			member = (MemberDeclNode)inheritanceType.tryGetMember(attribute.getIdent().toString());
			if(member == null) {
				String memberName = attribute.toString();
				reportError("Unknown member " + memberName + ", can't find in node/edge class type " + inheritanceType.getIdentNode().toString());
				return false;
			}
		} else {
			String memberName = attribute.toString();
			reportError("Unknown member " + memberName);
		}

		TypeNode type = getTypeOfElementToBeExtracted();
		if(!(type instanceof DeclaredTypeNode)) {
			reportError("The type "+ type + " is not an allowed type (basic type or node or edge class - set, map, array, deque are forbidden).");
			return false;
		}

		DeclaredTypeNode declType = (DeclaredTypeNode)type;
		extractedArrayType = new ArrayTypeNode(declType.getIdentNode());
		
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
		if(member != null)
			return member.getDeclType();
		return null;
	}
	
	@Override
	protected IR constructIR() {
		Entity accessedMember = null;
		if(member != null)
			accessedMember = member.checkIR(Entity.class);
		
		return new ArrayExtract(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
