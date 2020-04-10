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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.containers.ArrayKeepOneForEachBy;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayKeepOneForEachByNode extends ExprNode
{
	static {
		setName(ArrayKeepOneForEachByNode.class, "array keep one for each by");
	}

	private ExprNode targetExpr;
	private IdentNode attribute;
	private MemberDeclNode member;
	private NodeDeclNode node;
	private EdgeDeclNode edge;
	private VarDeclNode var;
	
	public ArrayKeepOneForEachByNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
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

	private static final DeclarationResolver<MemberDeclNode> memberResolver
		= new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array keepOneForEach expression must be of type array<T>");
			return false;
		}
		
		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)
				&& !(arrayType.valueType instanceof DefinedMatchTypeNode)) {
			reportError("keepOneForEach can only be employed on an array of nodes or edges or an array of match types.");
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
			PatternGraphNode iteratedPattern = iterated.getLeft();
			node = iteratedPattern.tryGetNode(attribute.toString());
			edge = iteratedPattern.tryGetEdge(attribute.toString());
			var = iteratedPattern.tryGetVar(attribute.toString());
			if(node==null && edge==null && var==null) {
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
			node = test.tryGetNode(attribute.toString());
			edge = test.tryGetEdge(attribute.toString());
			var = test.tryGetVar(attribute.toString());
			if(node==null && edge==null && var==null) {
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
			node = definedMatchType.tryGetNode(attribute.toString());
			edge = definedMatchType.tryGetEdge(attribute.toString());
			var = definedMatchType.tryGetVar(attribute.toString());
			if(node==null && edge==null && var==null) {
				String memberName = attribute.toString();
				String matchClassName = definedMatchType.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in match class type " + matchClassName);
				return false;
			}
		} else {
			ScopeOwner o = (ScopeOwner) arrayType.valueType;
			o.fixupDefinition(attribute);
			member = memberResolver.resolve(attribute, this);
			if(member == null)
				return false;
				
			if(member.isConst()) {
				reportError("keepOneForEach cannot be used on const attributes.");
			}
		}

		TypeNode memberType = getTypeOfElementToBeExtracted();
		if(!(memberType.equals(BasicTypeNode.byteType))
			&&!(memberType.equals(BasicTypeNode.shortType))
			&& !(memberType.equals(BasicTypeNode.intType))
			&& !(memberType.equals(BasicTypeNode.longType))
			&& !(memberType.equals(BasicTypeNode.floatType))
			&& !(memberType.equals(BasicTypeNode.doubleType))
			&& !(memberType.equals(BasicTypeNode.stringType))
			&& !(memberType.equals(BasicTypeNode.booleanType))
			&& !(memberType instanceof EnumTypeNode)) {
			targetExpr.reportError("array method keepOneForEach only available for attributes of type byte,short,int,long,float,double,string,boolean,enum of a graph element");
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((ArrayTypeNode)targetExpr.getType());
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
		
		return new ArrayKeepOneForEachBy(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
