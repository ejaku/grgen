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
import de.unika.ipd.grgen.ast.decl.executable.FilterAutoDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FilterAutoSuppliedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
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

	private IdentNode packageId;

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

	public void setCurrentPackage(IdentNode id)
	{
		this.packageId = id;
	}

	public IdentNode getCurrentPackage()
	{
		return packageId;
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

	public boolean isKnownPackage(String packageName)
	{
		switch(packageName) {
		case "Math":
		case "File":
		case "Time":
		case "Debug":
		case "Transaction":
			return true;
		default:
			return false;
		}
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
		if(pack == null || !pack.getText().equals("Math"))
			return false;
		
		switch(i.getText()) {
		case "min":
		case "max":
		case "sin":
		case "cos":
		case "tan":
		case "arcsin":
		case "arccos":
		case "arctan":
		case "sqr":
		case "sqrt":
		case "pow":
		case "log":
		case "ceil":
		case "floor":
		case "round":
		case "truncate":
		case "abs":
		case "sgn":
		case "pi":
		case "e":
		case "byteMin":
		case "byteMax":
		case "shortMin":
		case "shortMax":
		case "intMin":
		case "intMax":
		case "longMin":
		case "longMax":
		case "floatMin":
		case "floatMax":
		case "doubleMin":
		case "doubleMax":
			return true;
		default:
			return false;
		}
	}

	boolean isFileFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack == null || !pack.getText().equals("File"))
			return false;
		
		switch(i.getText()) {
		case "exists":
		case "import":
			return true;
		default:
			return false;
		}
	}

	boolean isTimeFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack == null || !pack.getText().equals("Time"))
			return false;

		switch(i.getText()) {
		case "now":
			return true;
		default:
			return false;
		}
	}

	public boolean isGlobalFunction(Token pack, Token i, CollectNode<ExprNode> params)
	{
		return isGlobalFunction(i.getText(), params.getChildren().size());
	}

	public boolean isGlobalFunction(String functionName, int numParams)
	{
		switch(functionName) {
		case "nodes":
		case "edges":
			return numParams <= 1;
		case "countNodes":
		case "countEdges":
			return numParams <= 1;
		case "empty":
		case "size":
			return numParams == 0;
		case "source":
		case "target":
			return numParams == 1;
		case "opposite":
			return numParams == 2;
		case "nodeByName":
		case "edgeByName":
			return numParams >= 1 && numParams <= 2;
		case "nodeByUnique":
		case "edgeByUnique":
			return numParams >= 1 && numParams <= 2;
		case "incoming":
		case "outgoing":
		case "incident":
			return numParams >= 1 && numParams <= 3;
		case "adjacentIncoming":
		case "adjacentOutgoing":
		case "adjacent":
			return numParams >= 1 && numParams <= 3;
		case "reachableIncoming":
		case "reachableOutgoing":
		case "reachable":
			return numParams >= 1 && numParams <= 3;
		case "reachableEdgesIncoming":
		case "reachableEdgesOutgoing":
		case "reachableEdges":
			return numParams >= 1 && numParams <= 3;
		case "boundedReachableIncoming":
		case "boundedReachableOutgoing":
		case "boundedReachable":
			return numParams >= 2 && numParams <= 4;
		case "boundedReachableEdgesIncoming":
		case "boundedReachableEdgesOutgoing":
		case "boundedReachableEdges":
			return numParams >= 2 && numParams <= 4;
		case "boundedReachableWithRemainingDepthIncoming":
		case "boundedReachableWithRemainingDepthOutgoing":
		case "boundedReachableWithRemainingDepth":
			return numParams >= 2 && numParams <= 4;
		case "countIncoming":
		case "countOutgoing":
		case "countIncident":
			return numParams >= 1 && numParams <= 3;
		case "countAdjacentIncoming":
		case "countAdjacentOutgoing":
		case "countAdjacent":
			return numParams >= 1 && numParams <= 3;
		case "countReachableIncoming":
		case "countReachableOutgoing":
		case "countReachable":
			return numParams >= 1 && numParams <= 3;
		case "countReachableEdgesIncoming":
		case "countReachableEdgesOutgoing":
		case "countReachableEdges":
			return numParams >= 1 && numParams <= 3;
		case "countBoundedReachableIncoming":
		case "countBoundedReachableOutgoing":
		case "countBoundedReachable":
			return numParams >= 2 && numParams <= 4;
		case "countBoundedReachableEdgesIncoming":
		case "countBoundedReachableEdgesOutgoing":
		case "countBoundedReachableEdges":
			return numParams >= 2 && numParams <= 4;
		case "isIncoming":
		case "isOutgoing":
		case "isIncident":
			return numParams >= 2 && numParams <= 4;
		case "isAdjacentIncoming":
		case "isAdjacentOutgoing":
		case "isAdjacent":
			return numParams >= 2 && numParams <= 4;
		case "isReachableIncoming":
		case "isReachableOutgoing":
		case "isReachable":
			return numParams >= 2 && numParams <= 4;
		case "isReachableEdgesIncoming":
		case "isReachableEdgesOutgoing":
		case "isReachableEdges":
			return numParams >= 2 && numParams <= 4;
		case "isBoundedReachableIncoming":
		case "isBoundedReachableOutgoing":
		case "isBoundedReachable":
			return numParams >= 3 && numParams <= 5;
		case "isBoundedReachableEdgesIncoming":
		case "isBoundedReachableEdgesOutgoing":
		case "isBoundedReachableEdges":
			return numParams >= 3 && numParams <= 5;
		case "random":
			return numParams >= 0 && numParams <= 1;
		case "canonize":
			return numParams == 1;
		case "inducedSubgraph":
		case "definedSubgraph":
			return numParams == 1;
		case "equalsAny":
		case "equalsAnyStructurally":
			return numParams == 2;
		case "copy":
			return numParams == 1;
		case "nameof":
			return numParams == 1 || numParams == 0;
		case "uniqueof":
			return numParams == 1 || numParams == 0;
		default:
			return false;
		}
	}

	public boolean isKnownForFunction(String name)
	{
		switch(name) {
		case "adjacent":
		case "adjacentIncoming":
		case "adjacentOutgoing":
		case "incident":
		case "incoming":
		case "outgoing":
		case "reachable":
		case "reachableIncoming":
		case "reachableOutgoing":
		case "reachableEdges":
		case "reachableEdgesIncoming":
		case "reachableEdgesOutgoing":
		case "boundedReachable":
		case "boundedReachableIncoming":
		case "boundedReachableOutgoing":
		case "boundedReachableEdges":
		case "boundedReachableEdgesIncoming":
		case "boundedReachableEdgesOutgoing":
		case "nodes":
		case "edges":
			return true;
		default:
			return false;
		}
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
		if(pack == null || !pack.getText().equals("File"))
			return false;
		
		switch(i.getText()) {
		case "export":
		case "delete":
			return true;
		default:
			return false;
		}
	}

	boolean isTransactionProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack == null || !pack.getText().equals("Transaction"))
			return false;
		
		switch(i.getText()) {
		case "start":
		case "pause":
		case "resume":
		case "commit":
		case "rollback":
			return true;
		default:
			return false;
		}
	}

	boolean isDebugProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		if(pack == null || !pack.getText().equals("Debug"))
			return false;
		
		switch(i.getText()) {
		case "add":
		case "rem":
		case "emit":
		case "halt":
		case "highlight":
			return true;
		default:
			return false;
		}
	}

	public boolean isGlobalProcedure(Token pack, Token i, CollectNode<ExprNode> params)
	{
		return isGlobalProcedure(i.getText(), params.getChildren().size());
	}

	public boolean isGlobalProcedure(String procedureName, int numParams)
	{
		switch(procedureName) {
		case "valloc":
			return numParams == 0;
		case "vfree":
		case "vfreenonreset":
		case "vreset":
			return numParams == 1;
		case "record":
			return numParams == 1;
		case "emit":
		case "emitdebug":
			return true;
		case "add":
			return (numParams == 1 || numParams == 3);
		case "rem":
			return numParams == 1;
		case "clear":
			return numParams == 0;
		case "retype":
			return numParams == 2;
		case "addCopy":
			return (numParams == 1 || numParams == 3);
		case "merge":
			return (numParams >= 2 && numParams <= 3);
		case "redirectSource":
			return (numParams >= 2 && numParams <= 3);
		case "redirectTarget":
			return (numParams >= 2 && numParams <= 3);
		case "redirectSourceAndTarget":
			return (numParams >= 3 && numParams <= 5);
		case "insert":
			return numParams == 1;
		case "insertCopy":
			return numParams == 2;
		case "insertInduced":
		case "insertDefined":
			return numParams == 2;
		default:
			return false;
		}
	}

	public boolean isArrayAttributeAccessMethodName(String name)
	{
		switch(name) {
		case "indexOfBy":
		case "indexOfOrderedBy":
		case "lastIndexOfBy":
		case "orderAscendingBy":
		case "orderDescendingBy":
		case "keepOneForEach":
		case "extract":
			return true;
		default:
			return false;
		}
	}

	public boolean isAutoSuppliedFilterName(String name)
	{
		switch(name) {
		case "keepFirst":
		case "keepLast":
		case "removeFirst":
		case "removeLast":
		case "keepFirstFraction":
		case "keepLastFraction":
		case "removeFirstFraction":
		case "removeLastFraction":
			return true;
		default:
			return false;
		}
	}

	public boolean isAutoGeneratedBaseFilterName(String name)
	{
		switch(name) {
		case "orderAscendingBy":
		case "orderDescendingBy":
		case "groupBy":
		case "keepSameAsFirst":
		case "keepSameAsLast":
		case "keepOneForEach":
		case "keepOneForEachAccumulateBy":
			return true;
		default:
			return false;
		}
	}

	public ArrayList<FilterAutoDeclNode> getFiltersAutoSupplied(IteratedDeclNode iterated)
	{
		ArrayList<FilterAutoDeclNode> autoSuppliedFilters = new ArrayList<FilterAutoDeclNode>();

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

	public FilterAutoDeclNode getFilterAutoSupplied(String ident, IteratedDeclNode iterated)
	{
		IdentNode filterIdent = new IdentNode(define(ParserEnvironment.ACTIONS, ident, iterated.getCoords()));
		return new FilterAutoSuppliedDeclNode(filterIdent, iterated.getIdentNode());
	}

	public abstract UnitNode parseActions(File inputFile);

	public abstract ModelNode parseModel(File inputFile);

	public abstract void pushFile(Lexer lexer, File inputFile) throws RecognitionException;

	public abstract boolean popFile(Lexer lexer);

	public abstract String getFilename();

	public abstract boolean hadError();
}
