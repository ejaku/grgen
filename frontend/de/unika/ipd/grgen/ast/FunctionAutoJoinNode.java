/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.NestingStatement;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.MatchAccess;
import de.unika.ipd.grgen.ir.expr.MatchInit;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.VariableExpression;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.Assignment;
import de.unika.ipd.grgen.ir.stmt.ConditionStatement;
import de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
import de.unika.ipd.grgen.ir.stmt.DefDeclVarStatement;
import de.unika.ipd.grgen.ir.stmt.ReturnStatement;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddItem;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a function auto node
 */
public class FunctionAutoJoinNode extends FunctionAutoNode
{
	static {
		setName(FunctionAutoJoinNode.class, "function auto");
	}

	private String joinFunction;
	
	private CollectNode<VarDeclNode> arguments = new CollectNode<VarDeclNode>();
	private CollectNode<IdentNode> argumentsUnresolved = new CollectNode<IdentNode>();

	public FunctionAutoJoinNode(Coords coords, String function, String joinFunction, CollectNode<IdentNode> arguments)
	{
		super(coords, function);
		this.joinFunction = joinFunction;
		this.argumentsUnresolved = arguments;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(argumentsUnresolved, arguments));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final CollectResolver<VarDeclNode> argumentsResolver =
			new CollectResolver<VarDeclNode>(new DeclarationResolver<VarDeclNode>(VarDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	public boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		arguments = argumentsResolver.resolve(argumentsUnresolved, this);

		return successfullyResolved;
	}
	
	@Override
	public boolean checkLocal()
	{
		if(!function.equals("join")) {
			reportError("Unknown function in auto(), supported is: join (e.g. join<natural>)");
			return false;
		}
		
		if(!joinFunction.equals("natural") && !joinFunction.equals("cartesian")) {
			reportError("Unknown join function in auto(), supported are natural and cartesian (giving e.g. join<natural>)");
			return false;
		}
	
		boolean result = true;

		int i = 1;
		for(VarDeclNode argument : arguments.getChildren()) {
			if(!(argument.getDeclType() instanceof ArrayTypeNode)) {
				reportError("argument " + i + " to " + shortSignature() + " must be an array");
				result = false;
				continue;
			}
			ArrayTypeNode argumentType = (ArrayTypeNode)argument.getDeclType();
			if(!(argumentType.getElementType() instanceof MatchTypeActionNode)
					&& !(argumentType.getElementType() instanceof DefinedMatchTypeNode)) {
				reportError("argument " + i + " to " + shortSignature() + " must be an array<match<T>> or array<match<class T>>");
				result = false;
				continue;
			}
			++i;
		}
		
		if(arguments.getChildren().size() != 2) {
			reportError(shortSignature() + " must have 2 arguments");
			result = false;
		}

		VarDeclNode leftArgument = arguments.getChildrenAsVector().get(0);
		ArrayTypeNode leftArrayType = (ArrayTypeNode)leftArgument.getDeclType();
		MatchTypeNode leftMatchType = (MatchTypeNode)leftArrayType.valueType;

		VarDeclNode rightArgument = arguments.getChildrenAsVector().get(1);
		ArrayTypeNode rightArrayType = (ArrayTypeNode)rightArgument.getDeclType();
		MatchTypeNode rightMatchType = (MatchTypeNode)rightArrayType.valueType;
		
		Set<String> sharedNames = getNamesOfCommonEntities(leftMatchType, rightMatchType);
		for(String sharedName : sharedNames) {
			TypeNode leftMemberType = leftMatchType.tryGetMember(sharedName).getDeclType();
			TypeNode rightMemberType = rightMatchType.tryGetMember(sharedName).getDeclType();
			if(!leftMemberType.isEqual(rightMemberType)) {
				reportError("The member " + sharedName + " must be of same type in " 
						+ leftMatchType.getIdentNode().toString() + " and in "
						+ rightMatchType.getIdentNode().toString() + " (but is of type "
						+ leftMemberType.getTypeName() + " and " + rightMemberType.getTypeName() + ")");
			}
		}

		return result;
	}
	
	public Set<String> getNamesOfCommonEntities(MatchTypeNode this_, MatchTypeNode that)
	{
		Set<String> namesFromThis = this_.getNamesOfEntities();
		Set<String> namesFromThat = that.getNamesOfEntities();
		namesFromThis.retainAll(namesFromThat);
		return namesFromThis;
	}

	@Override
	public boolean checkLocal(FunctionDeclNode functionDecl)
	{
		if(!(functionDecl.getResultType() instanceof ArrayTypeNode)) {
			reportError("result type of function employing " + shortSignature() + " must be an array (not " + functionDecl.getResultType().getTypeName() + ")");
			return false;
		}
		ArrayTypeNode resultType = (ArrayTypeNode)functionDecl.getResultType();
		if(!(resultType.getElementType() instanceof DefinedMatchTypeNode)) {
			reportError("result type of function employing " + shortSignature() + " must be an array<match<class T>> (not " + functionDecl.getResultType().getTypeName() + ")");
			return false;
		}
		
		boolean result = true;
		
		DefinedMatchTypeNode resultMatchType = (DefinedMatchTypeNode)resultType.getElementType();
		for(DeclNode resultMember : resultMatchType.getEntities()) {
			String resultMemberName = resultMember.getIdentNode().toString();
			TypeNode resultMemberType = resultMember.getDeclType();
			for(VarDeclNode argument : arguments.getChildren()) {
				ArrayTypeNode argumentType = (ArrayTypeNode)argument.getDeclType();
				MatchTypeNode argumentMatchType = (MatchTypeNode)argumentType.getElementType();
				DeclNode argumentMember = argumentMatchType.tryGetMember(resultMemberName);
				if(argumentMember != null) {
					TypeNode argumentMemberType = argumentMember.getDeclType();
					if(!argumentMemberType.isEqual(resultMemberType)) {
						reportError("The member " + resultMemberName + " must be of same type in " 
								+ resultMatchType.getIdentNode().toString() + " and in "
								+ argumentMatchType.getIdentNode().toString() + " (but is of type "
								+ resultMemberType.getTypeName() + " and " + argumentMemberType.getTypeName() + ")");
						result = false;
					}
				}
			}
		}

		// members of resulting type without counterparts in argument types are ignored / kept untouched
		// (i.e. they contain the C# initialization values)

		return result;
	}

	@Override
	public void getStatements(FunctionDeclNode functionDecl, Function function)
	{
		/* generates code for joining that looks like this example from the queries test:
		function naturalJoin(ref matchesSameCompany:array<match<sameCompany>>, ref matchesSharedInterest:array<match<sharedInterest>>) : array<match<class SameCompanySharedInterest>>
		{
			def ref res:array<match<class SameCompanySharedInterest>> = array<match<class SameCompanySharedInterest>>[];
			for(matchSameCompany:match<sameCompany> in matchesSameCompany)
			{
				for(matchSharedInterest:match<sharedInterest> in matchesSharedInterest)
				{
					if(matchSameCompany.subject == matchSharedInterest.subject && matchSameCompany.person == matchSharedInterest.person) {
						def ref m:match<class SameCompanySharedInterest> = match<class SameCompanySharedInterest>();
						m.subject = matchSameCompany.subject;
						m.person = matchSameCompany.person;
						m.company = matchSameCompany.company;
						m.interest = matchSharedInterest.interest;
						res.add(m);
					}
				}
			}
			return(res);
		}
		*/
		
		NestingStatement insertionPoint = function;

		ArrayTypeNode resultArrayType = (ArrayTypeNode)functionDecl.resultType;
		DefinedMatchTypeNode resultMatchType = (DefinedMatchTypeNode)resultArrayType.getElementType();

		Ident resultVarIdent = new Ident("res", getCoords());
		ArrayType resultVarType = (ArrayType)function.getReturnType();
		PatternGraphLhs fakePatternGraph = PatternGraphLhsNode.getInvalid().checkIR(PatternGraphLhs.class);
		Variable resultVar = new Variable("res", resultVarIdent, resultVarType,
				true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
		Expression emptyArray = new ArrayInit(new Vector<Expression>(), null, resultVarType, true);
		resultVar.setInitialization(emptyArray);
		DefDeclVarStatement resultVarDecl = new DefDeclVarStatement(resultVar);
		insertionPoint.addStatement(resultVarDecl);
		
		VarDeclNode leftArgument = arguments.getChildrenAsVector().get(0);
		String leftIterationVarName = "$match_" + leftArgument.getIdentNode().toString();
		Ident leftIterationVarIdent = new Ident(leftIterationVarName, getCoords());
		ArrayTypeNode leftArrayType = (ArrayTypeNode)leftArgument.getDeclType();
		MatchTypeNode leftMatchType = (MatchTypeNode)leftArrayType.valueType;
		Type leftIterationVarType = leftMatchType.checkIR(Type.class);
		Variable leftIterationVar = new Variable(leftIterationVarName, leftIterationVarIdent, leftIterationVarType,
				true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
		ContainerAccumulationYield leftMatchesIteration = new ContainerAccumulationYield(leftIterationVar, null,
				leftArgument.checkIR(Variable.class));
		insertionPoint.addStatement(leftMatchesIteration);
		insertionPoint = leftMatchesIteration;

		VarDeclNode rightArgument = arguments.getChildrenAsVector().get(1);
		String rightIterationVarName = "$match_" + rightArgument.getIdentNode().toString();
		Ident rightIterationVarIdent = new Ident(rightIterationVarName, getCoords());
		ArrayTypeNode rightArrayType = (ArrayTypeNode)rightArgument.getDeclType();
		MatchTypeNode rightMatchType = (MatchTypeNode)rightArrayType.valueType;
		Type rightIterationVarType = rightMatchType.checkIR(Type.class);
		Variable rightIterationVar = new Variable(rightIterationVarName, rightIterationVarIdent, rightIterationVarType,
				true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
		ContainerAccumulationYield rightMatchesIteration = new ContainerAccumulationYield(rightIterationVar, null,
				rightArgument.checkIR(Variable.class));
		insertionPoint.addStatement(rightMatchesIteration);
		insertionPoint = rightMatchesIteration;

		Set<String> sharedNames = getNamesOfCommonEntities(leftMatchType, rightMatchType);
		if(joinFunction.equals("natural")) {
			Expression condition = new Constant(BasicTypeNode.booleanType.getType(), Boolean.TRUE);
			for(String sharedName : sharedNames) {
				DeclNode leftMemberDecl = leftMatchType.tryGetMember(sharedName);
				Entity leftMember = leftMemberDecl.checkIR(Entity.class);

				DeclNode rightMemberDecl = rightMatchType.tryGetMember(sharedName);
				Entity rightMember = rightMemberDecl.checkIR(Entity.class);
				
				Operator opEqual = new Operator(BasicTypeNode.booleanType.getType(), Operator.OperatorCode.EQ);
				opEqual.addOperand(new MatchAccess(new VariableExpression(leftIterationVar), leftMember));
				opEqual.addOperand(new MatchAccess(new VariableExpression(rightIterationVar), rightMember));
				
				Operator opAnd = new Operator(BasicTypeNode.booleanType.getType(), Operator.OperatorCode.LOG_AND);
				opAnd.addOperand(condition);
				opAnd.addOperand(opEqual);
				
				condition = opAnd;
			}

			ConditionStatement condStmt = new ConditionStatement(condition);
			insertionPoint.addStatement(condStmt);
			insertionPoint = condStmt;
		}

		Ident matchVarIdent = new Ident("$m", getCoords());
		DefinedMatchType matchVarType = (DefinedMatchType)resultVarType.getValueType();
		Variable matchVar = new Variable("$m", matchVarIdent, matchVarType,
				true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
		Expression matchInit = new MatchInit(matchVarType);
		matchVar.setInitialization(matchInit);
		DefDeclVarStatement matchVarDecl = new DefDeclVarStatement(matchVar);
		insertionPoint.addStatement(matchVarDecl);
	
		for(DeclNode leftMember : leftMatchType.getEntities())
		{
			String memberName = leftMember.getIdentNode().toString();
			if(memberName.startsWith("$"))
				continue;
			if(resultMatchType.tryGetMember(memberName) == null)
				continue;
			
			Entity matchMember = matchVarType.getPatternGraph().tryGetMember(memberName);
			Qualification lhsQual = new Qualification(matchVar, matchMember);
			Qualification rhsQual = new Qualification(leftIterationVar, leftMember.checkIR(Entity.class));
			Assignment assignment = new Assignment(lhsQual, rhsQual);
			insertionPoint.addStatement(assignment);
		}
		
		for(DeclNode rightMember : rightMatchType.getEntities())
		{
			String memberName = rightMember.getIdentNode().toString();
			if(memberName.startsWith("$"))
				continue;
			if(resultMatchType.tryGetMember(memberName) == null)
				continue;
			if(sharedNames.contains(memberName))
				continue;
			
			Entity matchMember = matchVarType.getPatternGraph().tryGetMember(memberName);
			Qualification lhsQual = new Qualification(matchVar, matchMember);
			Qualification rhsQual = new Qualification(rightIterationVar, rightMember.checkIR(Entity.class));
			Assignment assignment = new Assignment(lhsQual, rhsQual);
			insertionPoint.addStatement(assignment);
		}
		
		Expression matchVarExpr = new VariableExpression(matchVar);
		ArrayVarAddItem arrayAddItem = new ArrayVarAddItem(resultVar, matchVarExpr, null);
		insertionPoint.addStatement(arrayAddItem);
				
		Expression returnValueExpr = new VariableExpression(resultVar);
		ReturnStatement returnStmt = new ReturnStatement(returnValueExpr);
		function.addStatement(returnStmt);
	}
	
	private String shortSignature()
	{
		return "join<" + joinFunction + ">(.,.)";
	}
}
