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
import de.unika.ipd.grgen.ir.containers.ArrayOrderAscendingBy;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayOrderAscendingByNode extends ExprNode
{
	static {
		setName(ArrayOrderAscendingByNode.class, "array order ascending by");
	}

	private ExprNode targetExpr;
	private IdentNode attribute;
	private DeclNode member;
	
	public ArrayOrderAscendingByNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
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
			targetExpr.reportError("This argument to array orderAscendingBy expression must be of type array<T>");
			return false;
		}
		
		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)
				&& !(arrayType.valueType instanceof DefinedMatchTypeNode)) {
			reportError("orderAscendingBy can only be employed on an array of nodes or edges or an array of match types.");
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
		} else {
			ScopeOwner o = (ScopeOwner) arrayType.valueType;
			o.fixupDefinition(attribute);
			member = memberResolver.resolve(attribute, this);
			if(member == null)
				return false;
				
			if(((MemberDeclNode)member).isConst()) {
				reportError("orderAscendingBy cannot be used on const attributes.");
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
			targetExpr.reportError("array method orderAscendingBy only available for attributes of type byte,short,int,long,float,double,string,boolean,enum of a graph element");
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((ArrayTypeNode)targetExpr.getType());
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
		
		return new ArrayOrderAscendingBy(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
