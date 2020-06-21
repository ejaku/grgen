/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.MatchClassFilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.RuleDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.SequenceDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeCaseDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.model.decl.ModelNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.PackageTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.ir.executable.FilterFunction;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
import de.unika.ipd.grgen.ir.executable.Procedure;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.executable.Sequence;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.PackageActionType;

/**
 * The main node of the text. It is the root of the AST.
 */
public class UnitNode extends BaseNode
{
	static {
		setName(UnitNode.class, "unit declaration");
	}

	private ModelNode stdModel;
	private CollectNode<ModelNode> models;

	private CollectNode<SubpatternDeclNode> subpatterns;
	private CollectNode<IdentNode> subpatternsUnresolved;

	private CollectNode<ActionDeclNode> actions;
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

	private CollectNode<TypeDeclNode> packages;
	private CollectNode<IdentNode> packagesUnresolved;

	/**
	 * The name for this unit node
	 */
	private String unitname;

	/**
	 * The filename for this main node.
	 */
	private String filename;

	public UnitNode(String unitname, String filename, 
			ModelNode stdModel, CollectNode<ModelNode> models,
			CollectNode<IdentNode> subpatterns, CollectNode<IdentNode> actions,
			CollectNode<IdentNode> matchTypes, CollectNode<IdentNode> filterFunctions,
			CollectNode<IdentNode> matchClasses, CollectNode<IdentNode> matchClassFilterFunctions,
			CollectNode<IdentNode> functions, CollectNode<IdentNode> procedures,
			CollectNode<IdentNode> sequences, CollectNode<IdentNode> packages)
	{
		this.stdModel = stdModel;
		this.models = models;
		becomeParent(this.models);
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
		this.packagesUnresolved = packages;
		becomeParent(this.packagesUnresolved);
		this.unitname = unitname;
		this.filename = filename;
	}

	protected ModelNode getStdModel()
	{
		return stdModel;
	}

	public void addModel(ModelNode model)
	{
		models.addChild(model);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(models);
		children.add(getValidVersion(subpatternsUnresolved, subpatterns));
		children.add(getValidVersion(actionsUnresolved, actions));
		children.add(getValidVersion(matchTypesUnresolved, matchTypes));
		children.add(getValidVersion(filterFunctionsUnresolved, filterFunctions));
		children.add(getValidVersion(matchClassesUnresolved, matchClassDecls));
		children.add(getValidVersion(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
		children.add(getValidVersion(functionsUnresolved, functions));
		children.add(getValidVersion(proceduresUnresolved, procedures));
		children.add(getValidVersion(sequencesUnresolved, sequences));
		children.add(getValidVersion(packagesUnresolved, packages));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("models");
		childrenNames.add("subpatterns");
		childrenNames.add("actions");
		childrenNames.add("match types");
		childrenNames.add("filter functions");
		childrenNames.add("match classes");
		childrenNames.add("match class filter functions");
		childrenNames.add("functions");
		childrenNames.add("procedures");
		childrenNames.add("sequences");
		childrenNames.add("packages");
		return childrenNames;
	}

	private static final CollectResolver<SubpatternDeclNode> subpatternsResolver =
			new CollectResolver<SubpatternDeclNode>(new DeclarationResolver<SubpatternDeclNode>(SubpatternDeclNode.class));

	private static final CollectResolver<ActionDeclNode> actionsResolver =
			new CollectResolver<ActionDeclNode>(new DeclarationResolver<ActionDeclNode>(ActionDeclNode.class));

	private static final CollectResolver<MatchTypeNode> matchTypesResolver =
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

	private static final CollectResolver<TypeDeclNode> packagesResolver =
			new CollectResolver<TypeDeclNode>(new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));

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
		packages = packagesResolver.resolve(packagesUnresolved, this);

		return subpatterns != null && actions != null
				&& matchTypes != null && filterFunctions != null
				&& matchClassDecls != null && matchClassFilterFunctions != null
				&& functions != null && procedures != null
				&& sequences != null && packages != null;
	}

	/** Check the collect nodes containing the model declarations, subpattern declarations, action declarations
	 *  @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		Checker modelChecker = new CollectChecker(new SimpleChecker(ModelNode.class));
		boolean res = modelChecker.check(models, error);
		for(ModelNode model : models.getChildren()) {
			res = checkModelTypes(res, model.getTypeDecls());
			for(ModelNode usedModel : model.getUsedModels().getChildren()) {
				res = checkModelTypes(res, usedModel.getTypeDecls());
				for(TypeDeclNode package_ : usedModel.getPackages().getChildren()) {
					PackageTypeNode packageType = (PackageTypeNode)package_.getDeclType();
					res = checkModelTypes(res, packageType.getTypeDecls());
				}
			}
			for(TypeDeclNode package_ : model.getPackages().getChildren()) {
				PackageTypeNode packageType = (PackageTypeNode)package_.getDeclType();
				res = checkModelTypes(res, packageType.getTypeDecls());
			}
		}
		for(SubpatternDeclNode subpattern : subpatterns.getChildren()) {
			res &= checkStatementsLHS(subpattern, subpattern.pattern);
			if(subpattern.right != null)
				res &= checkStatementsRHS(subpattern, subpattern.right.patternGraph);
		}
		for(ActionDeclNode action : actions.getChildren()) {
			res &= checkStatementsLHS(action, action.pattern);
			if(action instanceof RuleDeclNode) {
				RuleDeclNode rule = (RuleDeclNode)action;
				res &= checkStatementsRHS(action, rule.right.patternGraph);
			}
		}
		for(FilterFunctionDeclNode filterFunction : filterFunctions.getChildren()) {
			if(filterFunction.evalStatements != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, filterFunction, null, filterFunction.evalStatements, true);
		}
		for(MatchClassFilterFunctionDeclNode matchClassFilterFunction : matchClassFilterFunctions.getChildren()) {
			if(matchClassFilterFunction.evalStatements != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, matchClassFilterFunction, null,
						matchClassFilterFunction.evalStatements, true);
		}
		for(FunctionDeclNode function : functions.getChildren()) {
			res &= EvalStatementNode.checkStatements(true, function, null, function.evalStatements, true);
		}
		for(ProcedureDeclNode procedure : procedures.getChildren()) {
			res &= EvalStatementNode.checkStatements(false, procedure, null, procedure.evalStatements, true);
		}
		return res;
	}

	private static boolean checkModelTypes(boolean res, CollectNode<TypeDeclNode> typeDecls)
	{
		for(TypeDeclNode typeDecl : typeDecls.getChildren()) {
			DeclaredTypeNode declType = typeDecl.getDeclType();
			if(declType instanceof InheritanceTypeNode) {
				InheritanceTypeNode inhType = (InheritanceTypeNode)declType;
				res &= inhType.checkStatementsInMethods();
			}
		}
		return res;
	}

	public static boolean checkStatementsLHS(DeclNode root, PatternGraphLhsNode curPattern)
	{
		boolean res = true;

		// traverse graph structure
		for(AlternativeDeclNode alt : curPattern.alts.getChildren()) {
			for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
				res &= checkStatementsLHS(root, altCase.pattern);
				if(altCase.right != null)
					res &= checkStatementsRHS(root, altCase.right.patternGraph);
			}
		}
		for(IteratedDeclNode iter : curPattern.iters.getChildren()) {
			res &= checkStatementsLHS(root, iter.pattern);
			if(iter.right != null)
				res &= checkStatementsRHS(root, iter.right.patternGraph);
		}
		for(PatternGraphLhsNode idpt : curPattern.idpts.getChildren()) {
			res &= checkStatementsLHS(root, idpt);
		}

		// spawn checking computation statement structure
		for(EvalStatementsNode yields : curPattern.yields.getChildren()) {
			res &= EvalStatementNode.checkStatements(true, root, null, yields.evalStatements, true);
		}

		return res;
	}

	public static boolean checkStatementsRHS(DeclNode root, PatternGraphRhsNode curGraph)
	{
		boolean res = true;

		// spawn checking computation statement structure
		for(EvalStatementsNode evals : curGraph.evals.getChildren()) {
			res &= EvalStatementNode.checkStatements(false, root, null, evals.evalStatements, true);
		}

		return res;
	}

	/**
	 * Get the IR unit node for this AST node.
	 * @return The Unit for this AST node.
	 */
	public Unit getUnit()
	{
		return checkIR(Unit.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		Unit res = new Unit(unitname, filename);

		for(ModelNode model : models.getChildren()) {
			Model modelIR = model.getModel();
			res.addModel(modelIR);
		}

		for(SubpatternDeclNode subpattern : subpatterns.getChildren()) {
			Rule rule = subpattern.getAction();
			res.addSubpatternRule(rule);
		}

		for(ActionDeclNode action : actions.getChildren()) {
			Rule rule = action.getAction();
			res.addActionRule(rule);
		}

		for(FilterFunctionDeclNode filter : filterFunctions.getChildren()) {
			FilterFunction filterIR = filter.getFilterFunction();
			res.addFilterFunction(filterIR);
		}

		for(TypeDeclNode matchClass : matchClassDecls.getChildren()) {
			DefinedMatchTypeNode matchClassDecl = (DefinedMatchTypeNode)matchClass.getDeclType();
			DefinedMatchType matchClassIR = matchClassDecl.getDefinedMatchType();
			res.addMatchClass(matchClassIR);
		}

		for(MatchClassFilterFunctionDeclNode matchClassFilter : matchClassFilterFunctions.getChildren()) {
			MatchClassFilterFunction matchClassFilterIR = matchClassFilter.getMatchClassFilterFunction();
			res.addMatchClassFilterFunction(matchClassFilterIR);
		}

		for(FunctionDeclNode function : functions.getChildren()) {
			Function functionIR = function.getFunction();
			res.addFunction(functionIR);
		}

		for(ProcedureDeclNode procedure : procedures.getChildren()) {
			Procedure procedureIR = procedure.getProcedure();
			res.addProcedure(procedureIR);
		}

		for(SequenceDeclNode sequence : sequences.getChildren()) {
			Sequence sequenceIR = sequence.getSequence();
			res.addSequence(sequenceIR);
		}

		for(TypeDeclNode packageType : packages.getChildren()) {
			PackageActionType packageActionType = (PackageActionType)packageType.getDeclType().getType();
			res.addPackage(packageActionType);
		}

		return res;
	}
}
