/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.FilterFunction;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.PackageActionType;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Sequence;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.ir.exprevals.Function;
import de.unika.ipd.grgen.ir.exprevals.Procedure;

/**
 * The main node of the text. It is the root of the AST.
 */
public class UnitNode extends BaseNode {
	static {
		setName(UnitNode.class, "unit declaration");
	}

	private ModelNode stdModel;
	private CollectNode<ModelNode> models;

	private CollectNode<SubpatternDeclNode> subpatterns;
	private CollectNode<IdentNode> subpatternsUnresolved;

	private CollectNode<TestDeclNode> actions; // of type TestDeclNode or RuleDeclNode
	private CollectNode<IdentNode> actionsUnresolved;

	private CollectNode<FilterFunctionDeclNode> filterFunctions;
	private CollectNode<IdentNode> filterFunctionsUnresolved;

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

	public UnitNode(String unitname, String filename, ModelNode stdModel,
			CollectNode<ModelNode> models, CollectNode<IdentNode> subpatterns, 
			CollectNode<IdentNode> actions, CollectNode<IdentNode> filterFunctions, 
			CollectNode<IdentNode> functions, CollectNode<IdentNode> procedures,
			CollectNode<IdentNode> sequences, CollectNode<IdentNode> packages) {
		this.stdModel = stdModel;
		this.models = models;
		becomeParent(this.models);
		this.subpatternsUnresolved = subpatterns;
		becomeParent(this.subpatternsUnresolved);
		this.actionsUnresolved = actions;
		becomeParent(this.actionsUnresolved);
		this.filterFunctionsUnresolved = filterFunctions;
		becomeParent(this.filterFunctionsUnresolved);
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

	protected ModelNode getStdModel() {
		return stdModel;
	}

	public void addModel(ModelNode model) {
		models.addChild(model);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(models);
		children.add(getValidVersion(subpatternsUnresolved, subpatterns));
		children.add(getValidVersion(actionsUnresolved, actions));
		children.add(getValidVersion(filterFunctionsUnresolved, filterFunctions));
		children.add(getValidVersion(functionsUnresolved, functions));
		children.add(getValidVersion(proceduresUnresolved, procedures));
		children.add(getValidVersion(sequencesUnresolved, sequences));
		children.add(getValidVersion(packagesUnresolved, packages));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("models");
		childrenNames.add("subpatterns");
		childrenNames.add("actions");
		childrenNames.add("filter functions");
		childrenNames.add("functions");
		childrenNames.add("procedures");
		childrenNames.add("sequences");
		childrenNames.add("packages");
		return childrenNames;
	}

	private static final CollectResolver<SubpatternDeclNode> subpatternsResolver = new CollectResolver<SubpatternDeclNode>(
			new DeclarationResolver<SubpatternDeclNode>(SubpatternDeclNode.class));

	private static final CollectResolver<TestDeclNode> actionsResolver = new CollectResolver<TestDeclNode>(
			new DeclarationResolver<TestDeclNode>(TestDeclNode.class));

	private static final CollectResolver<FilterFunctionDeclNode> filterFunctionsResolver = new CollectResolver<FilterFunctionDeclNode>(
			new DeclarationResolver<FilterFunctionDeclNode>(FilterFunctionDeclNode.class));

	private static final CollectResolver<FunctionDeclNode> functionsResolver = new CollectResolver<FunctionDeclNode>(
			new DeclarationResolver<FunctionDeclNode>(FunctionDeclNode.class));

	private static final CollectResolver<ProcedureDeclNode> proceduresResolver = new CollectResolver<ProcedureDeclNode>(
			new DeclarationResolver<ProcedureDeclNode>(ProcedureDeclNode.class));

	private static final CollectResolver<SequenceDeclNode> sequencesResolver = new CollectResolver<SequenceDeclNode>(
			new DeclarationResolver<SequenceDeclNode>(SequenceDeclNode.class));

	private static final CollectResolver<TypeDeclNode> packagesResolver = new CollectResolver<TypeDeclNode>(
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		subpatterns = subpatternsResolver.resolve(subpatternsUnresolved, this);
		actions = actionsResolver.resolve(actionsUnresolved, this);
		filterFunctions = filterFunctionsResolver.resolve(filterFunctionsUnresolved, this);
		functions = functionsResolver.resolve(functionsUnresolved, this);
		procedures = proceduresResolver.resolve(proceduresUnresolved, this);
		sequences = sequencesResolver.resolve(sequencesUnresolved, this);
		packages = packagesResolver.resolve(packagesUnresolved, this);

		return subpatterns != null && actions != null && filterFunctions != null
			&& functions != null && procedures != null 
			&& sequences != null && packages != null;
	}

	/** Check the collect nodes containing the model declarations, subpattern declarations, action declarations
	 *  @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		Checker modelChecker = new CollectChecker(new SimpleChecker(ModelNode.class));
		boolean res = modelChecker.check(models, error);
		for(ModelNode model : models.getChildren()) {
			res = checkModelTypes(res, model);
			for(ModelNode usedModel : model.getUsedModels().getChildren()) {
				res = checkModelTypes(res, usedModel);
			}
		}
		for(SubpatternDeclNode subpattern : subpatterns.getChildren()) {	
			res &= checkStatementsLHS(subpattern, subpattern.pattern);
			if(subpattern.right.size()>0)
				res &= checkStatementsRHS(subpattern, subpattern.right.children.get(0).graph);
		}
		for(TestDeclNode action : actions.getChildren()) {
			res &= checkStatementsLHS(action, action.pattern);
			if(action instanceof RuleDeclNode) {
				RuleDeclNode rule = (RuleDeclNode)action;
				res &= checkStatementsRHS(action, rule.right.graph);
			}
		}
		for(FilterFunctionDeclNode filterFunction : filterFunctions.getChildren()) {
			if(filterFunction.evals != null) // otherwise external filter function without statements
				res &= EvalStatementNode.checkStatements(true, filterFunction, null, filterFunction.evals, true);
		}
		for(FunctionDeclNode function : functions.getChildren()) {
			res &= EvalStatementNode.checkStatements(true, function, null, function.evals, true);
		}
		for(ProcedureDeclNode procedure : procedures.getChildren()) {
			res &= EvalStatementNode.checkStatements(false, procedure, null, procedure.evals, true);
		}
		return res;
	}

	private boolean checkModelTypes(boolean res, ModelNode model) {
		for(TypeDeclNode typeDecl : model.getTypeDecls().getChildren()) {
			DeclaredTypeNode declType = typeDecl.getDeclType();
			if(declType instanceof InheritanceTypeNode) {
				InheritanceTypeNode inhType = (InheritanceTypeNode)declType;
				res &= inhType.checkStatementsInMethods();
			}
		}
		return res;
	}
	
	protected static boolean checkStatementsLHS(DeclNode root, PatternGraphNode curPattern) {
		boolean res = true;

		// traverse graph structure
		for(AlternativeNode alt : curPattern.alts.getChildren()) {
			for(AlternativeCaseNode altCase : alt.getChildren()) {
				res &= checkStatementsLHS(root, altCase.pattern);
				if(altCase.right.size()>0)
					res &= checkStatementsRHS(root, altCase.right.children.get(0).graph);
			}
		}
		for(IteratedNode iter : curPattern.iters.getChildren()) {
			res &= checkStatementsLHS(root, iter.pattern);
			if(iter.right.size()>0)
				res &= checkStatementsRHS(root, iter.right.children.get(0).graph);
		}
		for(PatternGraphNode idpt : curPattern.idpts.getChildren()) {
			res &= checkStatementsLHS(root, idpt);
		}
		
		// spawn checking computation statement structure
		for(EvalStatementsNode yields : curPattern.yieldsEvals.getChildren()) {
			res &= EvalStatementNode.checkStatements(true, root, null, yields.evalStatements, true);
		}
		
		return res;
	}

	protected static boolean checkStatementsRHS(DeclNode root, GraphNode curGraph) {
		boolean res = true;

		// spawn checking computation statement structure
		for(EvalStatementsNode evals : curGraph.yieldsEvals.getChildren()) {
			res &= EvalStatementNode.checkStatements(false, root, null, evals.evalStatements, true);
		}
		
		return res;
	}

	/**
	 * Get the IR unit node for this AST node.
	 * @return The Unit for this AST node.
	 */
	public Unit getUnit() {
		return checkIR(Unit.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		Unit res = new Unit(unitname, filename);

		for(ModelNode n : models.getChildren()) {
			Model model = n.getModel();
			res.addModel(model);
		}

		for(SubpatternDeclNode n : subpatterns.getChildren()) {
			Rule rule = n.getAction();
			res.addSubpatternRule(rule);
		}

		for(TestDeclNode n : actions.getChildren()) {
			Rule rule = n.getAction();
			res.addActionRule(rule);
		}

		for(FilterFunctionDeclNode n : filterFunctions.getChildren()) {
			FilterFunction filter = n.getFilterFunction();
			res.addFilterFunction(filter);
		}

		for(FunctionDeclNode n : functions.getChildren()) {
			Function function = n.getFunction();
			res.addFunction(function);
		}

		for(ProcedureDeclNode n : procedures.getChildren()) {
			Procedure procedure = n.getProcedure();
			res.addProcedure(procedure);
		}

		for(SequenceDeclNode n : sequences.getChildren()) {
			Sequence sequence = n.getSequence();
			res.addSequence(sequence);
		}

		for(TypeDeclNode n : packages.getChildren()) {
			PackageActionType packageActionType = (PackageActionType)n.getDeclType().getType();
			res.addPackage(packageActionType);
		}

		return res;
	}
}
