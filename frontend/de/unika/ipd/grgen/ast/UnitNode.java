/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

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
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeIteratedNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
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
		setClassName(UnitNode.class, "unit declaration");
	}

	private static UnitNode root; // added this for quick access to some model flags, could be considered a bit smelly regarding architecture, but having no access to the root could be considered strange

	private ModelNode stdModel;
	private CollectNode<ModelNode> models;

	private CollectNode<SubpatternDeclNode> subpatterns;
	private CollectNode<IdentNode> subpatternsUnresolved;

	private CollectNode<ActionDeclNode> actions;
	private CollectNode<IdentNode> actionsUnresolved;

	private CollectNode<MatchTypeActionNode> matchTypes;
	private CollectNode<IdentNode> matchTypesUnresolved;

	private CollectNode<MatchTypeIteratedNode> matchTypesIterated;
	private CollectNode<IdentNode> matchTypesIteratedUnresolved;

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
			CollectNode<IdentNode> matchTypesIterated,
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
		this.matchTypesIteratedUnresolved = matchTypesIterated;
		becomeParent(this.matchTypesIteratedUnresolved);
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
		// the CSharp backend does not allow for multiple models (it will throw an exception in that case),
		// but for historic reasons there is code support for multiple models in the code that comes before it
		// multiple model references are combined into one model in the parser, there the model flags are merged,
		// neither the models nor the flags get merged that are added here with this function stemming from the by now unsupported command line arguments
		// this function will be used even nowadays in case the user supplies no rules file, but only a model file (in this case no further merging is needed)
		// other outdated backends used in the FIRM compiler may still support multiple models, but they don't support the features that require/support model flags
		// the grgen.net compiler using this JAVA frontend will not supply multiple models, this is only possible when it is used directly
		// TODO: reconsider this code during a code cleaning run
		models.addChild(model);
	}

	public ModelNode getModel()
	{
		return models.get(0); // see comment above, there should be only/exactly one model when resolving starts, with flags combined from all models, when the CSharp backend is targeted
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(models);
		children.add(getValidVersionCollectNode(subpatternsUnresolved, subpatterns));
		children.add(getValidVersionCollectNode(actionsUnresolved, actions));
		children.add(getValidVersionCollectNode(matchTypesUnresolved, matchTypes));
		children.add(getValidVersionCollectNode(filterFunctionsUnresolved, filterFunctions));
		children.add(getValidVersionCollectNode(matchClassesUnresolved, matchClassDecls));
		children.add(getValidVersionCollectNode(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
		children.add(getValidVersionCollectNode(matchTypesIteratedUnresolved, matchTypesIterated));
		children.add(getValidVersionCollectNode(functionsUnresolved, functions));
		children.add(getValidVersionCollectNode(proceduresUnresolved, procedures));
		children.add(getValidVersionCollectNode(sequencesUnresolved, sequences));
		children.add(getValidVersionCollectNode(packagesUnresolved, packages));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		childrenNames.add("models");
		childrenNames.add("subpatterns");
		childrenNames.add("actions");
		childrenNames.add("match types");
		childrenNames.add("filter functions");
		childrenNames.add("match classes");
		childrenNames.add("match class filter functions");
		childrenNames.add("match types iterated");
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

	private static final CollectResolver<MatchTypeActionNode> matchTypesResolver =
			new CollectResolver<MatchTypeActionNode>(new DeclarationTypeResolver<MatchTypeActionNode>(MatchTypeActionNode.class));

	private static final CollectResolver<FilterFunctionDeclNode> filterFunctionsResolver =
			new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(FilterFunctionDeclNode.class));

	private static final CollectResolver<TypeDeclNode> matchClassesResolver =
			new CollectResolver<TypeDeclNode>(new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));

	private static final CollectResolver<MatchClassFilterFunctionDeclNode> matchClassFilterFunctionsResolver =
			new CollectResolver<MatchClassFilterFunctionDeclNode>(new DeclarationResolver<MatchClassFilterFunctionDeclNode>(MatchClassFilterFunctionDeclNode.class));

	private static final CollectResolver<MatchTypeIteratedNode> matchTypesIteratedResolver =
			new CollectResolver<MatchTypeIteratedNode>(new DeclarationTypeResolver<MatchTypeIteratedNode>(MatchTypeIteratedNode.class));

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
		matchTypesIterated = matchTypesIteratedResolver.resolve(matchTypesIteratedUnresolved, this);
		functions = functionsResolver.resolve(functionsUnresolved, this);
		procedures = proceduresResolver.resolve(proceduresUnresolved, this);
		sequences = sequencesResolver.resolve(sequencesUnresolved, this);
		packages = packagesResolver.resolve(packagesUnresolved, this);

		return subpatterns != null && actions != null
				&& matchTypes != null && filterFunctions != null
				&& matchClassDecls != null && matchClassFilterFunctions != null
				&& matchTypesIterated != null
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
		for(ModelNode model : models.getChildrenExact()) {
			res = checkModelTypes(res, model.getTypeDecls());
			for(ModelNode usedModel : model.getUsedModels().getChildrenExact()) {
				res = checkModelTypes(res, usedModel.getTypeDecls());
				for(TypeDeclNode package_ : usedModel.getPackages().getChildrenExact()) {
					PackageTypeNode packageType = (PackageTypeNode)package_.getDeclType();
					res = checkModelTypes(res, packageType.getTypeDecls());
				}
			}
			for(TypeDeclNode package_ : model.getPackages().getChildrenExact()) {
				PackageTypeNode packageType = (PackageTypeNode)package_.getDeclType();
				res = checkModelTypes(res, packageType.getTypeDecls());
			}
		}
		for(SubpatternDeclNode subpattern : subpatterns.getChildrenExact()) {
			res &= checkStatementsLHS(subpattern, subpattern.pattern);
			if(subpattern.right != null)
				res &= checkStatementsRHS(subpattern, subpattern.right.patternGraph);
		}
		for(ActionDeclNode action : actions.getChildrenExact()) {
			res &= checkStatementsLHS(action, action.pattern);
			if(action instanceof RuleDeclNode) {
				RuleDeclNode rule = (RuleDeclNode)action;
				res &= checkStatementsRHS(action, rule.right.patternGraph);
			}
		}
		for(FilterFunctionDeclNode filterFunction : filterFunctions.getChildrenExact()) {
			if(filterFunction.evalStatements != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, filterFunction, null, filterFunction.evalStatements, true);
		}
		for(MatchClassFilterFunctionDeclNode matchClassFilterFunction : matchClassFilterFunctions.getChildrenExact()) {
			if(matchClassFilterFunction.evalStatements != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, matchClassFilterFunction, null,
						matchClassFilterFunction.evalStatements, true);
		}
		for(FunctionDeclNode function : functions.getChildrenExact()) {
			res &= EvalStatementNode.checkStatements(true, function, null, function.evalStatements, true);
		}
		for(ProcedureDeclNode procedure : procedures.getChildrenExact()) {
			res &= EvalStatementNode.checkStatements(false, procedure, null, procedure.evalStatements, true);
		}
		return res;
	}

	private static boolean checkModelTypes(boolean res, CollectNode<TypeDeclNode> typeDecls)
	{
		for(TypeDeclNode typeDecl : typeDecls.getChildrenExact()) {
			TypeNode declType = typeDecl.getDeclType();
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
		for(AlternativeDeclNode alt : curPattern.alts.getChildrenExact()) {
			for(AlternativeCaseDeclNode altCase : alt.getChildrenExact()) {
				res &= checkStatementsLHS(root, altCase.pattern);
				if(altCase.right != null)
					res &= checkStatementsRHS(root, altCase.right.patternGraph);
			}
		}
		for(IteratedDeclNode iter : curPattern.iters.getChildrenExact()) {
			res &= checkStatementsLHS(root, iter.pattern);
			if(iter.right != null)
				res &= checkStatementsRHS(root, iter.right.patternGraph);
		}
		for(PatternGraphLhsNode idpt : curPattern.idpts.getChildrenExact()) {
			res &= checkStatementsLHS(root, idpt);
		}

		// spawn checking computation statement structure
		for(EvalStatementsNode yields : curPattern.yields.getChildrenExact()) {
			res &= EvalStatementNode.checkStatements(true, root, null, yields.evalStatements, true);
		}

		return res;
	}

	public static boolean checkStatementsRHS(DeclNode root, PatternGraphRhsNode curGraph)
	{
		boolean res = true;

		// spawn checking computation statement structure
		for(EvalStatementsNode evals : curGraph.evals.getChildrenExact()) {
			res &= EvalStatementNode.checkStatements(false, root, null, evals.evalStatements, true);
		}

		return res;
	}

	/**
	 * Get the IR unit node for this AST node.
	 * @return The Unit for this AST node.
	 */
	public Unit getIRUnit()
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

		for(ModelNode model : models.getChildrenExact()) {
			Model modelIR = model.getIRModel();
			res.addModel(modelIR);
		}

		for(SubpatternDeclNode subpattern : subpatterns.getChildrenExact()) {
			Rule rule = subpattern.getIRMatcher();
			res.addSubpatternRule(rule);
		}

		for(ActionDeclNode action : actions.getChildrenExact()) {
			Rule rule = action.getIRMatcher();
			res.addActionRule(rule);
		}

		for(FilterFunctionDeclNode filter : filterFunctions.getChildrenExact()) {
			FilterFunction filterIR = filter.getIRFilterFunction();
			res.addFilterFunction(filterIR);
		}

		for(TypeDeclNode matchClass : matchClassDecls.getChildrenExact()) {
			DefinedMatchTypeNode matchClassDecl = (DefinedMatchTypeNode)matchClass.getDeclType();
			DefinedMatchType matchClassIR = matchClassDecl.getIRDefinedMatchType();
			res.addMatchClass(matchClassIR);
		}

		for(MatchClassFilterFunctionDeclNode matchClassFilter : matchClassFilterFunctions.getChildrenExact()) {
			MatchClassFilterFunction matchClassFilterIR = matchClassFilter.getIRMatchClassFilterFunction();
			res.addMatchClassFilterFunction(matchClassFilterIR);
		}

		for(FunctionDeclNode function : functions.getChildrenExact()) {
			Function functionIR = function.getIRFunction();
			res.addFunction(functionIR);
		}

		for(ProcedureDeclNode procedure : procedures.getChildrenExact()) {
			Procedure procedureIR = procedure.getIRProcedure();
			res.addProcedure(procedureIR);
		}

		for(SequenceDeclNode sequence : sequences.getChildrenExact()) {
			Sequence sequenceIR = sequence.getIRSequence();
			res.addSequence(sequenceIR);
		}

		for(TypeDeclNode packageType : packages.getChildrenExact()) {
			PackageActionType packageActionType = (PackageActionType)packageType.getDeclType().getIRType();
			res.addPackage(packageActionType);
		}

		return res;
	}

	public static UnitNode getRoot()
	{
		return UnitNode.root;
	}

	public static void setRoot(UnitNode root)
	{
		if(UnitNode.root != null)
			throw new RuntimeException("Internal error, change of root node");
		UnitNode.root = root;
	}

	public static void clearRoot()
	{
		UnitNode.root = null;
	}
}
