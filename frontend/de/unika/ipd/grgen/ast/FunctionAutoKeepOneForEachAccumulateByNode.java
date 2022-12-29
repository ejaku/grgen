/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.FunctionAutoKeepOneForEachAccumulateBy;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.parser.Coords;

public class FunctionAutoKeepOneForEachAccumulateByNode extends FunctionAutoNode
{
	static {
		setName(FunctionAutoKeepOneForEachAccumulateByNode.class, "auto keep one for each accumulate by");
	}

	private IdentNode target;
	private VarDeclNode targetVar;

	private IdentNode attribute;
	private DeclNode member;

	private IdentNode accumulationAttribute;
	private DeclNode accumulationMember;
	
	private String accumulationMethod;

	public FunctionAutoKeepOneForEachAccumulateByNode(Coords coords, String function, 
			IdentNode attribute, IdentNode accumulationAttribute, String accumulationMethod,
			IdentNode target)
	{
		super(coords, function);
		this.attribute = attribute;
		this.accumulationAttribute = accumulationAttribute;
		this.accumulationMethod = accumulationMethod;
		this.target = target;
	}
	
	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("targetExpr");
		return childrenNames;
	}

	private static final DeclarationResolver<VarDeclNode> targetResolver =
			new DeclarationResolver<VarDeclNode>(VarDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	public boolean resolveLocal()
	{
		targetVar = targetResolver.resolve(target, this);
		return targetVar != null;
	}
	
	@Override
	public boolean checkLocal()
	{
		if(!function.equals("keepOneForEachAccumulateBy")) {
			reportError("Unknown function in auto(), expected keepOneForEachAccumulateBy (e.g. keepOneForEach<foo>Accumulate<bar>By<sum>).");
			return false;
		}

		ArrayTypeNode arrayType = getTargetType();
		if(!(arrayType.valueType instanceof MatchTypeActionNode)
				&& !(arrayType.valueType instanceof DefinedMatchTypeNode)) {
			reportError("The auto-generated function keepOneForEachAccumulateBy can only be employed on an array of match or match class types"
					+ " (but is employed on an array of " + arrayType.valueType.getTypeName() + ").");
			return false;
		}

		TypeNode valueType = arrayType.valueType;
		member = Resolver.resolveMember(valueType, attribute);
		if(member == null)
			return false;

		TypeNode memberType = getTypeOfElementToBeExtracted();
		if(!memberType.isFilterableType()) {
			target.reportError("The keepOneForEach argument of the auto-generated function keepOneForEachAccumulateBy is only available for attributes of type "
					+ TypeNode.getFilterableTypesAsString() + " (but is employed on an attribute of type " + memberType.getTypeName() + ").");
			return false;
		}

		accumulationMember = Resolver.resolveMember(valueType, accumulationAttribute);
		if(accumulationMember == null)
			return false;

		TypeNode accumulationMemberType = getTypeOfAccumulationElementToBeExtracted();
		if(!accumulationMemberType.isAccumulatableType()) {
			target.reportError("The accumulate argument of the auto-generated function keepOneForEachAccumulateBy is only available for attributes of type "
					+ TypeNode.getAccumulatableTypesAsString() + " (but is employed on an attribute of type " + accumulationMemberType.getTypeName() + ").");
			return false;
		}

		return true;
	}

	@Override
	public boolean checkLocal(FunctionDeclNode functionDecl)
	{
		if(!(functionDecl.getResultType() instanceof ArrayTypeNode)) {
			reportError("The result type of the function " + functionDecl.getIdentNode()
					+ " employing the auto-generated function " + functionName()
					+ " must be an array (but is " + functionDecl.getResultType().getTypeName() + ").");
			return false;
		}
		ArrayTypeNode resultType = (ArrayTypeNode)functionDecl.getResultType();
		if(!(resultType.getElementType() instanceof DefinedMatchTypeNode)
				&& !(resultType.getElementType() instanceof MatchTypeActionNode)) {
			reportError("The result type of the function " + functionDecl.getIdentNode()
					+ " employing the auto-generated function " + functionName()
					+ " must be an array<match<class T>> or array<match<T>>"
				+ " (but is " + functionDecl.getResultType().getTypeName() + ").");
			return false;
		}
		
		return true;
	}

	public TypeNode getType()
	{
		return getTargetType();
	}

	protected ArrayTypeNode getTargetType()
	{
		TypeNode targetType = targetVar.getDeclType();
		return (ArrayTypeNode)targetType;
	}

	private TypeNode getTypeOfElementToBeExtracted()
	{
		if(member != null)
			return member.getDeclType();
		return null;
	}

	private TypeNode getTypeOfAccumulationElementToBeExtracted()
	{
		if(accumulationMember != null)
			return accumulationMember.getDeclType();
		return null;
	}

	@Override
	public void getStatements(FunctionDeclNode functionDecl, Function function)
	{
		Entity accessedMember = member.checkIR(Entity.class);

		Variable accessedAccumulationMember = accumulationMember.checkIR(Variable.class);
		
		FunctionAutoKeepOneForEachAccumulateBy stmt = new FunctionAutoKeepOneForEachAccumulateBy(
				targetVar.checkIR(Variable.class), 
				accessedMember, accessedAccumulationMember, accumulationMethod);
		function.addStatement(stmt);
	}
	
	private String functionName()
	{
		return "keepOneForEach<" + attribute.getIdent() 
				+ ">Accumulate<" + accumulationAttribute.getIdent()
				+ ">By<"+ accumulationMethod + ">";
	}
}
