/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.parser;

import java.io.File;
import java.util.ArrayList;
import java.util.HashSet;

import org.antlr.runtime.RecognitionException;
import org.antlr.runtime.Token;
import org.antlr.runtime.Lexer;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FilterAutoNode;
import de.unika.ipd.grgen.ast.decl.executable.FilterAutoSuppliedNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
import de.unika.ipd.grgen.ast.model.ConnAssertNode;
import de.unika.ipd.grgen.ast.model.decl.ModelNode;
import de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.EmptyAnnotations;

public abstract class ParserEnvironment extends Base
{
	public static final String MODEL_SUFFIX = ".gm";

	public static final int TYPES = 0;
	public static final int PATTERNS = TYPES; // patterns are also constructible, like types
	public static final int ENTITIES = 1;
	public static final int ACTIONS = 2;
	public static final int ALTERNATIVES = 3;
	public static final int ITERATEDS = 4;
	public static final int NEGATIVES = 5;
	public static final int INDEPENDENTS = 6;
	public static final int REPLACES = 7;
	public static final int MODELS = 8;
	public static final int FUNCTIONS_AND_EXTERNAL_FUNCTIONS = 9;
	public static final int COMPUTATION_BLOCKS = 10;
	public static final int INDICES = 11;
	public static final int PACKAGES = 12;

	private final SymbolTable[] symTabs = new SymbolTable[] {
			new SymbolTable("types", TYPES), // types and patterns
			new SymbolTable("entities", ENTITIES),
			new SymbolTable("actions", ACTIONS),
			new SymbolTable("alternatives", ALTERNATIVES),
			new SymbolTable("iterateds", ITERATEDS),
			new SymbolTable("negatives", NEGATIVES),
			new SymbolTable("independents", INDEPENDENTS),
			new SymbolTable("replaces", REPLACES),
			new SymbolTable("models", MODELS),
			new SymbolTable("functions and external functions", FUNCTIONS_AND_EXTERNAL_FUNCTIONS),
			new SymbolTable("computation blocks", COMPUTATION_BLOCKS),
			new SymbolTable("indices", INDICES),
			new SymbolTable("packages", PACKAGES)
	};

	private final IntConstNode one = new IntConstNode(Coords.getBuiltin(), 1);

	private final IntConstNode zero = new IntConstNode(Coords.getBuiltin(), 0);

	// TODO use or remove it
	// private final Scope rootScope;

	private Scope currScope;

	private final IdentNode nodeRoot;

	private final IdentNode arbitraryEdgeRoot;
	private final IdentNode directedEdgeRoot;
	private final IdentNode undirectedEdgeRoot;

	private final Sys system;

	private final ModelNode stdModel;

	private HashSet<String> keywords = new HashSet<String>();

	private IdentNode id;

	private CollectNode<IdentNode> matchTypeChilds = new CollectNode<IdentNode>();

	// ANTLR is only SLL, not LL, can't disambiguate based on the context = the stuff on the stack,
	// here we emulate a "stack" for a particular question where we just can't get along without,
	// with only one type of tokens pushable to that stack, so a counter of the stack depth is sufficient
	// private int containerInitNestingLevel = 0; 

	/**
	 * Make a new parser environment.
	 */
	public ParserEnvironment(Sys system)
	{
		this.system = system;

		// Make the root scope
		// currScope = rootScope = new Scope(system.getErrorReporter());
		currScope = new Scope(system.getErrorReporter());
		BaseNode.setCurrScope(currScope);

		// Add some keywords to the symbol table
		for(int i = 0; i < symTabs.length; i++) {
			symTabs[i].enterKeyword("byte");
			symTabs[i].enterKeyword("short");
			symTabs[i].enterKeyword("int");
			symTabs[i].enterKeyword("long");
			symTabs[i].enterKeyword("string");
			symTabs[i].enterKeyword("boolean");
			symTabs[i].enterKeyword("float");
			symTabs[i].enterKeyword("double");
			symTabs[i].enterKeyword("object");
			symTabs[i].enterKeyword("graph");
		}

		initLexerKeywords();

		// The standard model
		CollectNode<IdentNode> stdModelPackages = new CollectNode<IdentNode>();
		CollectNode<IdentNode> stdModelChilds = new CollectNode<IdentNode>();
		stdModel = new ModelNode(predefine(ENTITIES, "Std"), stdModelPackages, stdModelChilds,
				new CollectNode<IdentNode>(), new CollectNode<IdentNode>(),
				new CollectNode<IdentNode>(), new CollectNode<ModelNode>(),
				false, false, false, false, false, false, false, false, 0);

		// The node type root
		NodeTypeNode nodeRootType = new NodeTypeNode(new CollectNode<IdentNode>(), new CollectNode<BaseNode>(), 0, null);
		nodeRoot = predefineType("Node", nodeRootType);
		NodeTypeNode.nodeType = nodeRootType;

		// The edge type roots
		ArbitraryEdgeTypeNode arbitraryEdgeRootType = new ArbitraryEdgeTypeNode(new CollectNode<IdentNode>(),
				new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), InheritanceTypeNode.MOD_ABSTRACT, null);
		arbitraryEdgeRoot = predefineType("AEdge", arbitraryEdgeRootType);
		CollectNode<IdentNode> superTypes = new CollectNode<IdentNode>();
		superTypes.addChild(arbitraryEdgeRoot);
		EdgeTypeNode.arbitraryEdgeType = arbitraryEdgeRootType;

		DirectedEdgeTypeNode directedEdgeRootType = new DirectedEdgeTypeNode(superTypes,
				new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), 0, null);
		directedEdgeRoot = predefineType("Edge", directedEdgeRootType);
		EdgeTypeNode.directedEdgeType = directedEdgeRootType;
		UndirectedEdgeTypeNode undirectedEdgeRootType = new UndirectedEdgeTypeNode(superTypes,
				new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), 0, null);
		undirectedEdgeRoot = predefineType("UEdge", undirectedEdgeRootType);
		EdgeTypeNode.undirectedEdgeType = undirectedEdgeRootType;

		stdModelChilds.addChild(nodeRoot);
		stdModelChilds.addChild(arbitraryEdgeRoot);
		stdModelChilds.addChild(directedEdgeRoot);
		stdModelChilds.addChild(undirectedEdgeRoot);

		stdModelChilds.addChild(predefineType("byte", BasicTypeNode.byteType));
		stdModelChilds.addChild(predefineType("short", BasicTypeNode.shortType));
		stdModelChilds.addChild(predefineType("int", BasicTypeNode.intType));
		stdModelChilds.addChild(predefineType("long", BasicTypeNode.longType));
		stdModelChilds.addChild(predefineType("string", BasicTypeNode.stringType));
		stdModelChilds.addChild(predefineType("boolean", BasicTypeNode.booleanType));
		stdModelChilds.addChild(predefineType("float", BasicTypeNode.floatType));
		stdModelChilds.addChild(predefineType("double", BasicTypeNode.doubleType));
		stdModelChilds.addChild(predefineType("object", BasicTypeNode.objectType));
		stdModelChilds.addChild(predefineType("graph", BasicTypeNode.graphType));
	}

	public ModelNode getStdModel()
	{
		return stdModel;
	}

	public File findModel(String modelName)
	{
		File modelPath = system.getModelPath();
		String modelFile = modelName.endsWith(MODEL_SUFFIX) ? modelName : modelName + MODEL_SUFFIX;

		File curr;
		if(modelPath.getPath().equals("."))
			curr = new File(modelFile);
		else
			curr = new File(modelPath, modelFile);
		debug.report(NOTE, "trying: " + curr);

		File res = null;
		if(curr.exists())
			res = curr;
		return res;
	}

	/**
	 * Predefine an identifier.
	 * @param symTab The symbol table to enter the identifier in.
	 * @param text The string of the identifier.
	 * @return An AST identifier node for this identifier.
	 */
	private IdentNode predefine(int symTab, String text)
	{
		return new IdentNode(define(symTab, text, BaseNode.BUILTIN));
	}

	/**
	 * Predefine a type.
	 * This method creates the type declaration of a given type.
	 * @param text The name of the type.
	 * @param type The AST type node.
	 * @return An AST identifier node for this type.
	 */
	private IdentNode predefineType(String text, TypeNode type)
	{
		IdentNode id = predefine(TYPES, text);
		id.setDecl(new TypeDeclNode(id, type));
		return id;
	}

	public Scope getCurrScope()
	{
		return currScope;
	}

	public void pushScope(IdentNode ident)
	{
		currScope = currScope.newScope(ident);
		BaseNode.setCurrScope(currScope);
	}

	public void pushScope(String str, Coords coords)
	{
		pushScope(new IdentNode(
				new Symbol.Definition(getCurrScope(), coords, new Symbol(str, SymbolTable.getInvalid()))));
	}

	public void popScope()
	{
		if(!currScope.isRoot())
			currScope = currScope.leaveScope();
		BaseNode.setCurrScope(currScope);
	}

	public Symbol.Definition define(int symTab, String text, Coords coords)
	{
		assert symTab >= 0 && symTab < symTabs.length : "Illegal symbol table index";
		Symbol sym = symTabs[symTab].get(text);
		return currScope.define(sym, coords);
	}

	public IdentNode defineAnonymousEntity(String text, Coords coords)
	{
		Symbol.Definition def = currScope.defineAnonymous(text, symTabs[ENTITIES], coords);
		return new IdentNode(def);
	}

	public Symbol.Occurrence occurs(int symTab, String text, Coords coords)
	{
		assert symTab >= 0 && symTab < symTabs.length : "Illegal symbol table index";
		Symbol sym = symTabs[symTab].get(text);
		return currScope.occurs(sym, coords);
	}

	public boolean test(int symTab, String text)
	{
		assert symTab >= 0 && symTab < symTabs.length : "Illegal symbol table index";
		return symTabs[symTab].test(text);
	}

	public void setCurrentActionOrSubpattern(IdentNode id)
	{
		this.id = id;
	}

	public IdentNode getCurrentActionOrSubpattern()
	{
		return id;
	}

	public void setMatchTypeChilds(CollectNode<IdentNode> matchTypeChilds)
	{
		this.matchTypeChilds = matchTypeChilds;
	}

	public void addMatchTypeChild(IdentNode matchTypeChild)
	{
		this.matchTypeChilds.addChild(matchTypeChild);
	}

	/**
	 * Get the node root identifier.
	 * @return The node root type identifier.
	 */
	public IdentNode getNodeRoot()
	{
		return nodeRoot;
	}

	/**
	 * Get the directed edge root identifier.
	 * @return The directed edge root type identifier.
	 */
	public IdentNode getDirectedEdgeRoot()
	{
		return directedEdgeRoot;
	}

	/**
	 * Get the arbitrary edge root identifier.
	 * @return The arbitrary edge root type identifier.
	 */
	public IdentNode getArbitraryEdgeRoot()
	{
		return arbitraryEdgeRoot;
	}

	/**
	 * Get the undirected edge root identifier.
	 * @return The undirected edge root type identifier.
	 */
	public IdentNode getUndirectedEdgeRoot()
	{
		return undirectedEdgeRoot;
	}

	public IntConstNode getOne()
	{
		return one;
	}

	public IntConstNode getZero()
	{
		return zero;
	}

	public Sys getSystem()
	{
		return system;
	}

	/**
	 * Get an initializer for an AST node.
	 * This defaults to the error node.
	 * @return An initialization AST node.
	 */
	public BaseNode initNode()
	{
		return BaseNode.getErrorNode();
	}

	public ExprNode initExprNode()
	{
		return ExprNode.getInvalid();
	}

	public VarDeclNode initVarNode(PatternGraphNode directlyNestingLHSGraph, int context)
	{
		return VarDeclNode.getInvalidVar(directlyNestingLHSGraph, context);
	}

	public NodeDeclNode getDummyNodeDecl(int context, PatternGraphNode directlyNestingLHSGraph)
	{
		return NodeDeclNode.getDummy(defineAnonymousEntity("dummy_node",
				new Coords()), this.getNodeRoot(), context, directlyNestingLHSGraph);
	}

	/**
	 * Get an initializer for an identifier AST node.
	 * This defaults to the invalid identifier.
	 * @return An initialization AST identifier node.
	 */
	public IdentNode getDummyIdent()
	{
		return IdentNode.getInvalid();
	}

	public Annotations getEmptyAnnotations()
	{
		return EmptyAnnotations.get();
	}

	public Coords getInvalidCoords()
	{
		return Coords.getInvalid();
	}

	public boolean isLexerKeyword(String str)
	{
		return keywords.contains(str);
	}

	/**
	 * Initializes the lexer keywords hash set (i.e. all identifiers considered as keyword by the lexer (not the parser)).
	 */
	private void initLexerKeywords()
	{
		// To automatically generate the following lines, copy the keyword lines
		// at the end of antlr/GrGen.g to the file antlr/keywords.txt and
		// execute antlr/gen-keywords-code.sh writing to antlr/keywords.out

		keywords.add("abstract");
		keywords.add("alternative");
		keywords.add("arbitrary");
		keywords.add("array");
		keywords.add("auto");
		keywords.add("break");
		keywords.add("case");
		keywords.add("class");
		keywords.add("copy");
		keywords.add("connect");
		keywords.add("const");
		keywords.add("continue");
		keywords.add("count");
		keywords.add("def");
		keywords.add("delete");
		keywords.add("directed");
		keywords.add("do");
		keywords.add("edge");
		keywords.add("else");
		keywords.add("emit");
		keywords.add("emitdebug");
		keywords.add("emithere");
		keywords.add("emitheredebug");
		keywords.add("enum");
		keywords.add("eval");
		keywords.add("evalhere");
		keywords.add("exact");
		keywords.add("exec");
		keywords.add("extends");
		keywords.add("external");
		keywords.add("false");
		keywords.add("filter");
		keywords.add("for");
		keywords.add("function");
		keywords.add("hom");
		keywords.add("if");
		keywords.add("in");
		keywords.add("independent");
		keywords.add("index");
		keywords.add("induced");
		keywords.add("iterated");
		keywords.add("map");
		keywords.add("match");
		keywords.add("modify");
		keywords.add("multiple");
		keywords.add("nameof");
		keywords.add("negative");
		keywords.add("node");
		keywords.add("null");
		keywords.add("optional");
		keywords.add("package");
		keywords.add("pattern");
		keywords.add("patternpath");
		keywords.add("procedure");
		keywords.add("deque");
		keywords.add("replace");
		keywords.add("return");
		keywords.add("rule");
		keywords.add("sequence");
		keywords.add("set");
		keywords.add("switch");
		keywords.add("test");
		keywords.add("true");
		keywords.add("typeof");
		keywords.add("undirected");
		keywords.add("using");
		keywords.add("visited");
		keywords.add("while");
		keywords.add("yield");
	}

	public boolean isKnownFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(isMathFunction(pack, i, params)
				|| isFileFunction(pack, i, params)
				|| isTimeFunction(pack, i, params)
				|| isGlobalFunction(pack, i, params)) {
			return true;
		}
		return false;
	}

	boolean isMathFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack != null && pack.getText().equals("Math")) {
			if(i.getText().equals("min") || i.getText().equals("max")
					|| i.getText().equals("sin") || i.getText().equals("cos") || i.getText().equals("tan")
					|| i.getText().equals("arcsin") || i.getText().equals("arccos") || i.getText().equals("arctan")
					|| i.getText().equals("sqr") || i.getText().equals("sqrt")
					|| i.getText().equals("pow") || i.getText().equals("log")
					|| i.getText().equals("ceil") || i.getText().equals("floor") || i.getText().equals("round")
					|| i.getText().equals("truncate")
					|| i.getText().equals("abs") || i.getText().equals("sgn")
					|| i.getText().equals("pi") || i.getText().equals("e")
					|| i.getText().equals("byteMin") || i.getText().equals("byteMax")
					|| i.getText().equals("shortMin") || i.getText().equals("shortMax")
					|| i.getText().equals("intMin") || i.getText().equals("intMax")
					|| i.getText().equals("longMin") || i.getText().equals("longMax")
					|| i.getText().equals("floatMin") || i.getText().equals("floatMax")
					|| i.getText().equals("doubleMin") || i.getText().equals("doubleMax")) {
				return true;
			}
		}
		return false;
	}

	boolean isFileFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack != null && pack.getText().equals("File")) {
			if(i.getText().equals("exists")
					|| i.getText().equals("import")) {
				return true;
			}
		}
		return false;
	}

	boolean isTimeFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack != null && pack.getText().equals("Time")) {
			if(i.getText().equals("now")) {
				return true;
			}
		}
		return false;
	}

	public boolean isGlobalFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if((i.getText().equals("nodes") || i.getText().equals("edges"))
					&& params.getChildren().size() <= 1
			|| (i.getText().equals("countNodes") || i.getText().equals("countEdges"))
					&& params.getChildren().size() <= 1
			|| (i.getText().equals("empty") || i.getText().equals("size"))
					&& params.getChildren().size() == 0
			|| (i.getText().equals("source") || i.getText().equals("target"))
					&& params.getChildren().size() == 1
			|| i.getText().equals("opposite")
					&& params.getChildren().size() == 2
			|| (i.getText().equals("nodeByName") || i.getText().equals("edgeByName"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 2
			|| (i.getText().equals("nodeByUnique") || i.getText().equals("edgeByUnique"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 2
			|| (i.getText().equals("incoming") || i.getText().equals("outgoing") || i.getText().equals("incident"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("adjacentIncoming") || i.getText().equals("adjacentOutgoing") || i.getText().equals("adjacent"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("reachableIncoming") || i.getText().equals("reachableOutgoing") || i.getText().equals("reachable")) 
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("reachableEdgesIncoming") || i.getText().equals("reachableEdgesOutgoing") || i.getText().equals("reachableEdges"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("boundedReachableIncoming") || i.getText().equals("boundedReachableOutgoing") || i.getText().equals("boundedReachable"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("boundedReachableEdgesIncoming") || i.getText().equals("boundedReachableEdgesOutgoing") || i.getText().equals("boundedReachableEdges"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("boundedReachableWithRemainingDepthIncoming") || i.getText().equals("boundedReachableWithRemainingDepthOutgoing") || i.getText().equals("boundedReachableWithRemainingDepth"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("countIncoming") || i.getText().equals("countOutgoing") || i.getText().equals("countIncident")) 
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("countAdjacentIncoming") || i.getText().equals("countAdjacentOutgoing") || i.getText().equals("countAdjacent"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("countReachableIncoming") || i.getText().equals("countReachableOutgoing") || i.getText().equals("countReachable"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("countReachableEdgesIncoming") || i.getText().equals("countReachableEdgesOutgoing") || i.getText().equals("countReachableEdges"))
					&& params.getChildren().size() >= 1 && params.getChildren().size() <= 3
			|| (i.getText().equals("countBoundedReachableIncoming") || i.getText().equals("countBoundedReachableOutgoing") || i.getText().equals("countBoundedReachable"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("countBoundedReachableEdgesIncoming") || i.getText().equals("countBoundedReachableEdgesOutgoing") || i.getText().equals("countBoundedReachableEdges"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("isIncoming") || i.getText().equals("isOutgoing") || i.getText().equals("isIncident"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("isAdjacentIncoming") || i.getText().equals("isAdjacentOutgoing") || i.getText().equals("isAdjacent"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("isReachableIncoming") || i.getText().equals("isReachableOutgoing") || i.getText().equals("isReachable"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("isReachableEdgesIncoming") || i.getText().equals("isReachableEdgesOutgoing") || i.getText().equals("isReachableEdges"))
					&& params.getChildren().size() >= 2 && params.getChildren().size() <= 4
			|| (i.getText().equals("isBoundedReachableIncoming") || i.getText().equals("isBoundedReachableOutgoing") || i.getText().equals("isBoundedReachable"))
					&& params.getChildren().size() >= 3 && params.getChildren().size() <= 5
			|| (i.getText().equals("isBoundedReachableEdgesIncoming") || i.getText().equals("isBoundedReachableEdgesOutgoing") || i.getText().equals("isBoundedReachableEdges"))
					&& params.getChildren().size() >= 3 && params.getChildren().size() <= 5
			|| i.getText().equals("random")
					&& params.getChildren().size() >= 0 && params.getChildren().size() <= 1
			|| i.getText().equals("canonize")
					&& params.getChildren().size() == 1
			|| (i.getText().equals("inducedSubgraph") || i.getText().equals("definedSubgraph"))
					&& params.getChildren().size() == 1
			|| (i.getText().equals("equalsAny") || i.getText().equals("equalsAnyStructurally"))
					&& params.getChildren().size() == 2
			|| i.getText().equals("copy")
					&& params.getChildren().size() == 1
			|| i.getText().equals("nameof")
					&& (params.getChildren().size() == 1 || params.getChildren().size() == 0)
			|| i.getText().equals("uniqueof")
					&& (params.getChildren().size() == 1 || params.getChildren().size() == 0)) {
			return true;
		}
		return false;
	}

	public boolean isKnownForFunction(String name)
	{
		if(name.equals("adjacent") || name.equals("adjacentIncoming") || name.equals("adjacentOutgoing")
				|| name.equals("incident") || name.equals("incoming") || name.equals("outgoing")
				|| name.equals("reachable") || name.equals("reachableIncoming") || name.equals("reachableOutgoing")
				|| name.equals("reachableEdges") || name.equals("reachableEdgesIncoming")
				|| name.equals("reachableEdgesOutgoing")
				|| name.equals("boundedReachable") || name.equals("boundedReachableIncoming")
				|| name.equals("boundedReachableOutgoing")
				|| name.equals("boundedReachableEdges") || name.equals("boundedReachableEdgesIncoming")
				|| name.equals("boundedReachableEdgesOutgoing")
				|| name.equals("nodes") || name.equals("edges")) {
			return true;
		}
		return false;
	}

	public boolean isKnownProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(isFileProcedure(pack, i, params)
				|| isTransactionProcedure(pack, i, params)
				|| isDebugProcedure(pack, i, params)
				|| isGlobalProcedure(pack, i, params)) {
			return true;
		}
		return false;
	}

	boolean isFileProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack != null && pack.getText().equals("File")) {
			if(i.getText().equals("export")
					|| i.getText().equals("delete")) {
				return true;
			}
		}
		return false;
	}

	boolean isTransactionProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack != null && pack.getText().equals("Transaction")) {
			if(i.getText().equals("start")
					|| i.getText().equals("pause")
					|| i.getText().equals("resume")
					|| i.getText().equals("commit")
					|| i.getText().equals("rollback")) {
				return true;
			}
		}
		return false;
	}

	boolean isDebugProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack != null && pack.getText().equals("Debug")) {
			if(i.getText().equals("add")
					|| i.getText().equals("rem")
					|| i.getText().equals("emit")
					|| i.getText().equals("halt")
					|| i.getText().equals("highlight")) {
				return true;
			}
		}
		return false;
	}

	boolean isGlobalProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(i.getText().equals("valloc")
					&& params.getChildren().size() == 0
			|| i.getText().equals("vfree") || i.getText().equals("vfreenonreset") || i.getText().equals("vreset")
			|| i.getText().equals("record") || i.getText().equals("emit") || i.getText().equals("emitdebug")
			|| i.getText().equals("add")
					&& (params.getChildren().size() == 1 || params.getChildren().size() == 3)
			|| i.getText().equals("rem") || i.getText().equals("clear")
			|| i.getText().equals("retype")
					&& params.getChildren().size() == 2
			|| i.getText().equals("addCopy")
					&& (params.getChildren().size() == 1 || params.getChildren().size() == 3)
			|| i.getText().equals("merge")
			|| i.getText().equals("redirectSource") || i.getText().equals("redirectTarget")
			|| i.getText().equals("redirectSourceAndTarget")
			|| i.getText().equals("insert")
					&& params.getChildren().size() == 1
			|| i.getText().equals("insertCopy")
					&& params.getChildren().size() == 2
			|| (i.getText().equals("insertInduced") || i.getText().equals("insertDefined"))
					&& params.getChildren().size() == 2) {
			return true;
		}
		return false;
	}

	public boolean isArrayAttributeAccessMethodName(String name)
	{
		if(name.equals("indexOfBy") || name.equals("indexOfOrderedBy") || name.equals("lastIndexOfBy")
				|| name.equals("orderAscendingBy") || name.equals("orderDescendingBy")
				|| name.equals("keepOneForEach") || name.equals("extract")) {
			return true;
		}
		return false;
	}

	public boolean isAutoSuppliedFilterName(String name)
	{
		if(name.equals("keepFirst") || name.equals("keepLast")
				|| name.equals("removeFirst") || name.equals("removeLast")
				|| name.equals("keepFirstFraction") || name.equals("keepLastFraction")
				|| name.equals("removeFirstFraction") || name.equals("removeLastFraction")) {
			return true;
		}
		return false;
	}

	public boolean isAutoGeneratedBaseFilterName(String name)
	{
		if(name.equals("orderAscendingBy") || name.equals("orderDescendingBy") || name.equals("groupBy")
				|| name.equals("keepSameAsFirst") || name.equals("keepSameAsLast")
				|| name.equals("keepOneForEach") || name.equals("keepOneForEachAccumulateBy")) {
			return true;
		}
		return false;
	}

	public ArrayList<FilterAutoNode> getFiltersAutoSupplied(IteratedNode iterated)
	{
		ArrayList<FilterAutoNode> autoSuppliedFilters = new ArrayList<FilterAutoNode>();

		if(iterated != null) // may happen due to syntactic predicate / backtracking peek ahead
		{
			autoSuppliedFilters.add(getFilterAutoSupplied("keepFirst", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("keepLast", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("removeFirst", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("removeLast", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("keepFirstFraction", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("keepLastFraction", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("removeFirstFraction", iterated));
			autoSuppliedFilters.add(getFilterAutoSupplied("removeLastFraction", iterated));
		}

		return autoSuppliedFilters;
	}

	public FilterAutoNode getFilterAutoSupplied(String ident, IteratedNode iterated)
	{
		IdentNode filterIdent = new IdentNode(define(ParserEnvironment.ACTIONS, ident, iterated.getCoords()));
		return new FilterAutoSuppliedNode(filterIdent, iterated.getIdentNode());
	}

	public abstract UnitNode parseActions(File inputFile);

	public abstract ModelNode parseModel(File inputFile);

	public abstract void pushFile(Lexer lexer, File inputFile) throws RecognitionException;

	public abstract boolean popFile(Lexer lexer);

	public abstract String getFilename();

	public abstract boolean hadError();
}
