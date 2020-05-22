/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.ast.decl.FilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.MatchClassFilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.RuleDeclNode;
import de.unika.ipd.grgen.ast.decl.SequenceDeclNode;
import de.unika.ipd.grgen.ast.decl.SubpatternDeclNode;
import de.unika.ipd.grgen.ast.decl.TestDeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.DefinedMatchType;
import de.unika.ipd.grgen.ir.FilterFunction;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.MatchClassFilterFunction;
import de.unika.ipd.grgen.ir.MatchType;
import de.unika.ipd.grgen.ir.PackageActionType;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Sequence;
import de.unika.ipd.grgen.ir.typedecl.Function;
import de.unika.ipd.grgen.ir.typedecl.Procedure;

/**
 * A package type AST node, for packages from the actions (in contrast to the models).
 */
public class PackageActionTypeNode extends CompoundTypeNode
{
	static {
		setName(PackageActionTypeNode.class, "package in actions type");
	}

	private CollectNode<SubpatternDeclNode> subpatterns;
	private CollectNode<IdentNode> subpatternsUnresolved;

	private CollectNode<TestDeclNode> actions; // of type TestDeclNode or RuleDeclNode
	private CollectNode<IdentNode> actionsUnresolved;

	private CollectNode<MatchTypeNode> matchTypes;
	private CollectNode<IdentNode> matchTypesUnresolved;

	private CollectNode<FilterFunctionDeclNode> filterFunctions;
	private CollectNode<IdentNode> filterFunctionsUnresolved;

	private CollectNode<TypeDeclNode> matchClassDecls;
	private CollectNode<IdentNode> matchClassesUnresolved;

	private CollectNode<MatchClassFilterFunctionDeclNode> matchClassFilterFunctions;
	private CollectNode<IdentNode> matchClassFilterFunctionsUnresolved;

	private CollectNode<FunctionDeclNode> functions;
	private CollectNode<IdentNode> functionsUnresolved;

	private CollectNode<ProcedureDeclNode> procedures;
	private CollectNode<IdentNode> proceduresUnresolved;

	private CollectNode<SequenceDeclNode> sequences;
	private CollectNode<IdentNode> sequencesUnresolved;

	public PackageActionTypeNode(CollectNode<IdentNode> subpatterns, CollectNode<IdentNode> actions,
			CollectNode<IdentNode> matchTypes, CollectNode<IdentNode> filterFunctions,
			CollectNode<IdentNode> matchClasses, CollectNode<IdentNode> matchClassFilterFunctions,
			CollectNode<IdentNode> functions, CollectNode<IdentNode> procedures,
			CollectNode<IdentNode> sequences)
	{
		this.subpatternsUnresolved = subpatterns;
		becomeParent(this.subpatternsUnresolved);
		this.actionsUnresolved = actions;
		becomeParent(this.actionsUnresolved);
		this.matchTypesUnresolved = matchTypes;
		becomeParent(this.matchTypesUnresolved);
		this.filterFunctionsUnresolved = filterFunctions;
		becomeParent(this.filterFunctionsUnresolved);
		this.matchClassesUnresolved = matchClasses;
		becomeParent(this.matchClassesUnresolved);
		this.matchClassFilterFunctionsUnresolved = matchClassFilterFunctions;
		becomeParent(this.matchClassFilterFunctionsUnresolved);
		this.functionsUnresolved = functions;
		becomeParent(this.functionsUnresolved);
		this.proceduresUnresolved = procedures;
		becomeParent(this.proceduresUnresolved);
		this.sequencesUnresolved = sequences;
		becomeParent(this.sequencesUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(subpatternsUnresolved, subpatterns));
		children.add(getValidVersion(actionsUnresolved, actions));
		children.add(getValidVersion(matchTypesUnresolved, matchTypes));
		children.add(getValidVersion(filterFunctionsUnresolved, filterFunctions));
		children.add(getValidVersion(matchClassesUnresolved, matchClassDecls));
		children.add(getValidVersion(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
		children.add(getValidVersion(functionsUnresolved, functions));
		children.add(getValidVersion(proceduresUnresolved, procedures));
		children.add(getValidVersion(sequencesUnresolved, sequences));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subpatterns");
		childrenNames.add("actions");
		childrenNames.add("match types");
		childrenNames.add("filter functions");
		childrenNames.add("match classes");
		childrenNames.add("match class filter functions");
		childrenNames.add("functions");
		childrenNames.add("procedures");
		childrenNames.add("sequences");
		return childrenNames;
	}

	private static final CollectResolver<SubpatternDeclNode> subpatternsResolver =
			new CollectResolver<SubpatternDeclNode>(new DeclarationResolver<SubpatternDeclNode>(SubpatternDeclNode.class));

	private static final CollectResolver<TestDeclNode> actionsResolver =
			new CollectResolver<TestDeclNode>(new DeclarationResolver<TestDeclNode>(TestDeclNode.class));

	private static CollectResolver<MatchTypeNode> matchTypesResolver =
			new CollectResolver<MatchTypeNode>(new DeclarationTypeResolver<MatchTypeNode>(MatchTypeNode.class));

	private static final CollectResolver<FilterFunctionDeclNode> filterFunctionsResolver =
			new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(FilterFunctionDeclNode.class));

	private static final CollectResolver<TypeDeclNode> matchClassesResolver =
			new CollectResolver<TypeDeclNode>(new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));

	private static final CollectResolver<MatchClassFilterFunctionDeclNode> matchClassFilterFunctionsResolver =
			new CollectResolver<MatchClassFilterFunctionDeclNode>(new DeclarationResolver<MatchClassFilterFunctionDeclNode>(MatchClassFilterFunctionDeclNode.class));

	private static final CollectResolver<FunctionDeclNode> functionsResolver =
			new CollectResolver<FunctionDeclNode>(new DeclarationResolver<FunctionDeclNode>(FunctionDeclNode.class));

	private static final CollectResolver<ProcedureDeclNode> proceduresResolver =
			new CollectResolver<ProcedureDeclNode>(new DeclarationResolver<ProcedureDeclNode>(ProcedureDeclNode.class));

	private static final CollectResolver<SequenceDeclNode> sequencesResolver =
			new CollectResolver<SequenceDeclNode>(new DeclarationResolver<SequenceDeclNode>(SequenceDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		subpatterns = subpatternsResolver.resolve(subpatternsUnresolved, this);
		actions = actionsResolver.resolve(actionsUnresolved, this);
		matchTypes = matchTypesResolver.resolve(matchTypesUnresolved, this);
		filterFunctions = filterFunctionsResolver.resolve(filterFunctionsUnresolved, this);
		matchClassDecls = matchClassesResolver.resolve(matchClassesUnresolved, this);
		matchClassFilterFunctions = matchClassFilterFunctionsResolver.resolve(matchClassFilterFunctionsUnresolved, this);
		functions = functionsResolver.resolve(functionsUnresolved, this);
		procedures = proceduresResolver.resolve(proceduresUnresolved, this);
		sequences = sequencesResolver.resolve(sequencesUnresolved, this);

		return subpatterns != null && actions != null
				&& matchTypes != null && filterFunctions != null
				&& matchClassDecls != null && matchClassFilterFunctions != null
				&& functions != null && procedures != null && sequences != null;
	}

	/** Check the collect nodes containing the model declarations, subpattern declarations, action declarations
	 *  @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = true;
		for(SubpatternDeclNode subpattern : subpatterns.getChildren()) {
			res &= UnitNode.checkStatementsLHS(subpattern, subpattern.pattern);
			if(subpattern.right != null)
				res &= UnitNode.checkStatementsRHS(subpattern, subpattern.right.graph);
		}
		for(TestDeclNode action : actions.getChildren()) {
			res &= UnitNode.checkStatementsLHS(action, action.pattern);
			if(action instanceof RuleDeclNode) {
				RuleDeclNode rule = (RuleDeclNode)action;
				res &= UnitNode.checkStatementsRHS(action, rule.right.graph);
			}
		}
		for(FilterFunctionDeclNode filterFunction : filterFunctions.getChildren()) {
			if(filterFunction.evals != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, filterFunction, null, filterFunction.evals, true);
		}
		for(MatchClassFilterFunctionDeclNode matchClassFilterFunction : matchClassFilterFunctions.getChildren()) {
			if(matchClassFilterFunction.evals != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, matchClassFilterFunction, null,
						matchClassFilterFunction.evals, true);
		}
		for(FunctionDeclNode function : functions.getChildren()) {
			res &= EvalStatementNode.checkStatements(true, function, null, function.evals, true);
		}
		for(ProcedureDeclNode procedure : procedures.getChildren()) {
			res &= EvalStatementNode.checkStatements(false, procedure, null, procedure.evals, true);
		}
		return res;
	}

	/** Returns the IR object for this package action type node. */
	public PackageActionType getPackage()
	{
		return checkIR(PackageActionType.class);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		Ident id = getIdentNode().checkIR(Ident.class);
		PackageActionType res = new PackageActionType(id);

		for(SubpatternDeclNode n : subpatterns.getChildren()) {
			Rule subRule = n.getAction();
			subRule.setPackageContainedIn(id.toString());
			res.addSubpatternRule(subRule);
		}

		for(TestDeclNode n : actions.getChildren()) {
			Rule rule = n.getAction();
			rule.setPackageContainedIn(id.toString());
			res.addActionRule(rule);
		}

		for(MatchTypeNode n : matchTypes.getChildren()) {
			MatchType matchType = n.getMatchType();
			matchType.setPackageContainedIn(id.toString());
			//no adding to package as nothing needs to be generated from this type / already happens with action
		}

		for(FilterFunctionDeclNode n : filterFunctions.getChildren()) {
			FilterFunction filter = n.getFilterFunction();
			filter.setPackageContainedIn(id.toString());
			res.addFilterFunction(filter);
		}

		for(TypeDeclNode n : matchClassDecls.getChildren()) {
			DefinedMatchTypeNode matchClassNode = (DefinedMatchTypeNode)n.getDeclType();
			DefinedMatchType matchClass = matchClassNode.getDefinedMatchType();
			matchClass.setPackageContainedIn(id.toString());
			res.addMatchClass(matchClass);
		}

		for(MatchClassFilterFunctionDeclNode n : matchClassFilterFunctions.getChildren()) {
			MatchClassFilterFunction matchClassFilter = n.getMatchClassFilterFunction();
			matchClassFilter.setPackageContainedIn(id.toString());
			res.addMatchClassFilterFunction(matchClassFilter);
		}

		for(FunctionDeclNode n : functions.getChildren()) {
			Function function = n.getFunction();
			function.setPackageContainedIn(id.toString());
			res.addFunction(function);
		}

		for(ProcedureDeclNode n : procedures.getChildren()) {
			Procedure procedure = n.getProcedure();
			procedure.setPackageContainedIn(id.toString());
			res.addProcedure(procedure);
		}

		for(SequenceDeclNode n : sequences.getChildren()) {
			Sequence sequence = n.getSequence();
			sequence.setPackageContainedIn(id.toString());
			res.addSequence(sequence);
		}

		return res;
	}

	@Override
	public String toString()
	{
		return "package " + getIdentNode();
	}

	public static String getKindStr()
	{
		return "package type";
	}

	public static String getUseStr()
	{
		return "package";
	}
}
