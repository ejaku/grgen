/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using RecognitionException = org.antlr.runtime.RecognitionException;
	using Token = org.antlr.runtime.Token;
	using Lexer = org.antlr.runtime.Lexer;

	using de.unika.ipd.grgen.ast;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using FilterAutoDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterAutoDeclNode;
	using FilterAutoSuppliedDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterAutoSuppliedDeclNode;
	using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IntConstNode = de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
	using ConnAssertNode = de.unika.ipd.grgen.ast.model.ConnAssertNode;
	using ModelNode = de.unika.ipd.grgen.ast.model.decl.ModelNode;
	using ArbitraryEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
	using DirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
	using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using UndirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using Base = de.unika.ipd.grgen.util.Base;

	public abstract class ParserEnvironment : Base
	{
		public const string MODEL_SUFFIX = ".gm";

		public const int TYPES = 0;
		public const int PATTERNS = TYPES; // patterns are also constructible, like types
		public const int ENTITIES = 1;
		public const int ACTIONS = 2;
		public const int ALTERNATIVES = 3;
		public const int ITERATEDS = 4;
		public const int NEGATIVES = 5;
		public const int INDEPENDENTS = 6;
		public const int REPLACES = 7;
		public const int MODELS = 8;
		public const int FUNCTIONS_AND_EXTERNAL_FUNCTIONS = 9;
		public const int COMPUTATION_BLOCKS = 10;
		public const int INDICES = 11;
		public const int PACKAGES = 12;

		private readonly SymbolTable[] symTabs = new SymbolTable[]
		{
			new SymbolTable("types", TYPES),
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

		private readonly IntConstNode one = new IntConstNode(Coords.Builtin, 1);

		private readonly IntConstNode zero = new IntConstNode(Coords.Builtin, 0);

		private Scope currScope;

		private readonly IdentNode nodeRoot;

		private readonly IdentNode arbitraryEdgeRoot;
		private readonly IdentNode directedEdgeRoot;
		private readonly IdentNode undirectedEdgeRoot;

		private readonly IdentNode internalObjectRoot;
		private readonly IdentNode internalTransientObjectRoot;

		private readonly Sys sys;

		private readonly ModelNode stdModel;

		private HashSet<string> keywords = new HashSet<string>();

		private IdentNode packageId;

		private IdentNode id;

		private CollectNode<IdentNode> matchTypeChilds = new CollectNode<IdentNode>();

		// ANTLR is only SLL, not LL, can't disambiguate based on the context = the stuff on the stack,
		// here we emulate a "stack" for a particular question where we just can't get along without,
		// with only one type of tokens pushable to that stack, so a counter of the stack depth is sufficient
		// private int containerInitNestingLevel = 0; 

		/// <summary>
		/// Make a new parser environment.
		/// </summary>
		public ParserEnvironment(Sys sys)
		{
			this.sys = sys;

			// Make the root scope
			currScope = new Scope(sys.ErrorReporter);
			BaseNode.CurrScope = currScope;

			// Add some keywords to the symbol table
			for(int i = 0; i < symTabs.Length; i++)
			{
				symTabs[i].EnterKeyword("byte");
				symTabs[i].EnterKeyword("short");
				symTabs[i].EnterKeyword("int");
				symTabs[i].EnterKeyword("long");
				symTabs[i].EnterKeyword("string");
				symTabs[i].EnterKeyword("boolean");
				symTabs[i].EnterKeyword("float");
				symTabs[i].EnterKeyword("double");
				symTabs[i].EnterKeyword("object");
				symTabs[i].EnterKeyword("graph");
			}

			InitLexerKeywords();

			// The standard model
			CollectNode<IdentNode> stdModelPackages = new CollectNode<IdentNode>();
			CollectNode<IdentNode> stdModelChilds = new CollectNode<IdentNode>();
			stdModel = new ModelNode(Predefine(ENTITIES, "Std"), stdModelPackages, stdModelChilds,
					new CollectNode<IdentNode>(), new CollectNode<IdentNode>(),
					new CollectNode<IdentNode>(), new CollectNode<ModelNode>(),
					false, false, false, false, false, false, false, false, false, false, 0, 0);

			// The node type root
			NodeTypeNode nodeRootType = new NodeTypeNode(new CollectNode<IdentNode>(), new CollectNode<BaseNode>(), 0, null);
			nodeRoot = PredefineType("Node", nodeRootType);
			NodeTypeNode.nodeType = nodeRootType;

			// The edge type roots
			ArbitraryEdgeTypeNode arbitraryEdgeRootType = new ArbitraryEdgeTypeNode(new CollectNode<IdentNode>(),
					new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), InheritanceTypeNode.MOD_ABSTRACT, null);
			arbitraryEdgeRoot = PredefineType("AEdge", arbitraryEdgeRootType);
			CollectNode<IdentNode> superTypes = new CollectNode<IdentNode>();
			superTypes.AddChild(arbitraryEdgeRoot);
			EdgeTypeNode.arbitraryEdgeType = arbitraryEdgeRootType;

			DirectedEdgeTypeNode directedEdgeRootType = new DirectedEdgeTypeNode(superTypes,
					new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), 0, null);
			directedEdgeRoot = PredefineType("Edge", directedEdgeRootType);
			EdgeTypeNode.directedEdgeType = directedEdgeRootType;
			UndirectedEdgeTypeNode undirectedEdgeRootType = new UndirectedEdgeTypeNode(superTypes,
					new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), 0, null);
			undirectedEdgeRoot = PredefineType("UEdge", undirectedEdgeRootType);
			EdgeTypeNode.undirectedEdgeType = undirectedEdgeRootType;

			// The internal object type root
			InternalObjectTypeNode internalObjectRootType = new InternalObjectTypeNode(new CollectNode<IdentNode>(), new CollectNode<BaseNode>(), 0);
			internalObjectRoot = PredefineType("Object", internalObjectRootType);
			InternalObjectTypeNode.internalObjectType = internalObjectRootType;

			InternalTransientObjectTypeNode internalTransientObjectRootType = new InternalTransientObjectTypeNode(new CollectNode<IdentNode>(), new CollectNode<BaseNode>(), 0);
			internalTransientObjectRoot = PredefineType("TransientObject", internalTransientObjectRootType);
			InternalTransientObjectTypeNode.internalTransientObjectType = internalTransientObjectRootType;

			stdModelChilds.AddChild(nodeRoot);
			stdModelChilds.AddChild(arbitraryEdgeRoot);
			stdModelChilds.AddChild(directedEdgeRoot);
			stdModelChilds.AddChild(undirectedEdgeRoot);
			stdModelChilds.AddChild(internalObjectRoot);
			stdModelChilds.AddChild(internalTransientObjectRoot);

			stdModelChilds.AddChild(PredefineType("byte", BasicTypeNode.byteType));
			stdModelChilds.AddChild(PredefineType("short", BasicTypeNode.shortType));
			stdModelChilds.AddChild(PredefineType("int", BasicTypeNode.intType));
			stdModelChilds.AddChild(PredefineType("long", BasicTypeNode.longType));
			stdModelChilds.AddChild(PredefineType("string", BasicTypeNode.stringType));
			stdModelChilds.AddChild(PredefineType("boolean", BasicTypeNode.booleanType));
			stdModelChilds.AddChild(PredefineType("float", BasicTypeNode.floatType));
			stdModelChilds.AddChild(PredefineType("double", BasicTypeNode.doubleType));
			stdModelChilds.AddChild(PredefineType("object", BasicTypeNode.objectType));
			stdModelChilds.AddChild(PredefineType("graph", BasicTypeNode.graphType));
		}

		public virtual ModelNode StdModel
		{
			get
			{
				return stdModel;
			}
		}

		public virtual File FindModel(string modelName)
		{
			File modelPath = sys.ModelPath;
			string modelFile = modelName.EndsWith(MODEL_SUFFIX, StringComparison.Ordinal) ? modelName : modelName + MODEL_SUFFIX;

			File curr;
			if(modelPath.GetPath().Equals("."))
				curr = new File(modelFile);
			else
				curr = new File(modelPath, modelFile);
			debug.Report(NOTE, "trying: " + curr);

			File res = null;
			if(curr.Exists())
				res = curr;
			return res;
		}

		/// <summary>
		/// Predefine an identifier. </summary>
		/// <param name="symTab"> The symbol table to enter the identifier in. </param>
		/// <param name="text"> The string of the identifier. </param>
		/// <returns> An AST identifier node for this identifier. </returns>
		private IdentNode Predefine(int symTab, string text)
		{
			return new IdentNode(Define(symTab, text, Coords.BUILTIN));
		}

		/// <summary>
		/// Predefine a type.
		/// This method creates the type declaration of a given type. </summary>
		/// <param name="text"> The name of the type. </param>
		/// <param name="type"> The AST type node. </param>
		/// <returns> An AST identifier node for this type. </returns>
		private IdentNode PredefineType(string text, TypeNode type)
		{
			IdentNode id = Predefine(TYPES, text);
			id.Decl = new TypeDeclNode(id, type);
			return id;
		}

		public virtual Scope CurrScope
		{
			get
			{
				return currScope;
			}
		}

		public virtual void PushScope(IdentNode ident)
		{
			currScope = currScope.NewScope(ident);
			BaseNode.CurrScope = currScope;
		}

		public virtual void PushScope(string str, Coords coords)
		{
			PushScope(new IdentNode(
					new Symbol.Definition(CurrScope, coords, new Symbol(str, SymbolTable.Invalid))));
		}

		public virtual void PopScope()
		{
			if(!currScope.IsRoot())
				currScope = currScope.LeaveScope();
			BaseNode.CurrScope = currScope;
		}

		public virtual Symbol.Definition Define(int symTab, string text, Coords coords)
		{
			Debug.Assert(symTab >= 0 && symTab < symTabs.Length, "Illegal symbol table index");
			Symbol sym = symTabs[symTab].Get(text);
			return currScope.Define(sym, coords);
		}

		public virtual IdentNode DefineAnonymousEntity(string text, Coords coords)
		{
			Symbol.Definition def = currScope.DefineAnonymous(text, symTabs[ENTITIES], coords);
			return new IdentNode(def);
		}

		public virtual Symbol.Occurrence Occurs(int symTab, string text, Coords coords)
		{
			Debug.Assert(symTab >= 0 && symTab < symTabs.Length, "Illegal symbol table index");
			Symbol sym = symTabs[symTab].Get(text);
			return currScope.Occurs(sym, coords);
		}

		public virtual bool Test(int symTab, string text)
		{
			Debug.Assert(symTab >= 0 && symTab < symTabs.Length, "Illegal symbol table index");
			return symTabs[symTab].Test(text);
		}

		public virtual IdentNode CurrentPackage
		{
			set
			{
				this.packageId = value;
			}
			get
			{
				return packageId;
			}
		}


		public virtual IdentNode CurrentActionOrSubpattern
		{
			set
			{
				this.id = value;
			}
			get
			{
				return id;
			}
		}


		public virtual CollectNode<IdentNode> MatchTypeChilds
		{
			set
			{
				this.matchTypeChilds = value;
			}
		}

		public virtual void AddMatchTypeChild(IdentNode matchTypeChild)
		{
			this.matchTypeChilds.AddChild(matchTypeChild);
		}

		public virtual IdentNode GetMatchTypeChild(IdentNode actionOrSubpattern, IdentNode iterated)
		{
			foreach(IdentNode matchTypeChild in matchTypeChilds.ChildrenExact)
			{
				string iteratedMatchType = "match<" + actionOrSubpattern.Symbol.Text + "." + iterated.Symbol.Text + ">";
				if(matchTypeChild.Symbol.Text.Equals(iteratedMatchType))
				{
					return matchTypeChild;
				}
			}
			iterated.ReportError("An iterated " + actionOrSubpattern.Symbol.Text + "." + iterated.Symbol.Text + " is not known.");
			return IdentNode.Invalid;
		}

		/// <summary>
		/// Get the node root identifier. </summary>
		/// <returns> The node root type identifier. </returns>
		public virtual IdentNode NodeRoot
		{
			get
			{
				return nodeRoot;
			}
		}

		/// <summary>
		/// Get the directed edge root identifier. </summary>
		/// <returns> The directed edge root type identifier. </returns>
		public virtual IdentNode DirectedEdgeRoot
		{
			get
			{
				return directedEdgeRoot;
			}
		}

		/// <summary>
		/// Get the arbitrary edge root identifier. </summary>
		/// <returns> The arbitrary edge root type identifier. </returns>
		public virtual IdentNode ArbitraryEdgeRoot
		{
			get
			{
				return arbitraryEdgeRoot;
			}
		}

		/// <summary>
		/// Get the undirected edge root identifier. </summary>
		/// <returns> The undirected edge root type identifier. </returns>
		public virtual IdentNode UndirectedEdgeRoot
		{
			get
			{
				return undirectedEdgeRoot;
			}
		}

		public virtual IdentNode InternalObjectRoot
		{
			get
			{
				return internalObjectRoot;
			}
		}

		public virtual IdentNode InternalTransientObjectRoot
		{
			get
			{
				return internalTransientObjectRoot;
			}
		}

		public virtual IntConstNode One
		{
			get
			{
				return one;
			}
		}

		public virtual IntConstNode Zero
		{
			get
			{
				return zero;
			}
		}

		public virtual Sys Sys
		{
			get
			{
				return sys;
			}
		}

		/// <summary>
		/// Get an initializer for an AST node.
		/// This defaults to the error node. </summary>
		/// <returns> An initialization AST node. </returns>
		public static BaseNode InitNode()
		{
			return BaseNode.ErrorNode;
		}

		public static ExprNode InitExprNode()
		{
			return ExprNode.Invalid;
		}

		public static VarDeclNode InitVarNode(PatternGraphLhsNode directlyNestingLHSGraph, int context)
		{
			return VarDeclNode.GetInvalidVar(directlyNestingLHSGraph, context);
		}

		public virtual NodeDeclNode GetDummyNodeDecl(int context, PatternGraphLhsNode directlyNestingLHSGraph)
		{
			return NodeDeclNode.GetDummy(DefineAnonymousEntity("dummy_node",
					new Coords()), this.NodeRoot, context, directlyNestingLHSGraph);
		}

		/// <summary>
		/// Get an initializer for an identifier AST node.
		/// This defaults to the invalid identifier. </summary>
		/// <returns> An initialization AST identifier node. </returns>
		public static IdentNode DummyIdent
		{
			get
			{
				return IdentNode.Invalid;
			}
		}

		public static Coords InvalidCoords
		{
			get
			{
				return Coords.Invalid;
			}
		}

		public virtual bool IsLexerKeyword(string str)
		{
			return keywords.Contains(str);
		}

		/// <summary>
		/// Initializes the lexer keywords hash set (i.e. all identifiers considered as keyword by the lexer (not the parser)).
		/// </summary>
		private void InitLexerKeywords()
		{
			// To automatically generate the following lines, copy the keyword lines
			// at the end of antlr/GrGen.g to the file antlr/keywords.txt and
			// execute antlr/gen-keywords-code.sh writing to antlr/keywords.out

			keywords.Add("abstract");
			keywords.Add("alternative");
			keywords.Add("arbitrary");
			keywords.Add("array");
			keywords.Add("auto");
			keywords.Add("break");
			keywords.Add("case");
			keywords.Add("class");
			keywords.Add("copy");
			keywords.Add("connect");
			keywords.Add("const");
			keywords.Add("continue");
			keywords.Add("count");
			keywords.Add("def");
			keywords.Add("delete");
			keywords.Add("directed");
			keywords.Add("do");
			keywords.Add("edge");
			keywords.Add("else");
			keywords.Add("emit");
			keywords.Add("emitdebug");
			keywords.Add("emithere");
			keywords.Add("emitheredebug");
			keywords.Add("enum");
			keywords.Add("eval");
			keywords.Add("evalhere");
			keywords.Add("exact");
			keywords.Add("exec");
			keywords.Add("extends");
			keywords.Add("external");
			keywords.Add("false");
			keywords.Add("filter");
			keywords.Add("for");
			keywords.Add("function");
			keywords.Add("hom");
			keywords.Add("if");
			keywords.Add("in");
			keywords.Add("independent");
			keywords.Add("index");
			keywords.Add("induced");
			keywords.Add("iterated");
			keywords.Add("lock");
			keywords.Add("map");
			keywords.Add("match");
			keywords.Add("modify");
			keywords.Add("multiple");
			keywords.Add("nameof");
			keywords.Add("negative");
			keywords.Add("node");
			keywords.Add("null");
			keywords.Add("optional");
			keywords.Add("package");
			keywords.Add("pattern");
			keywords.Add("patternpath");
			keywords.Add("procedure");
			keywords.Add("deque");
			keywords.Add("replace");
			keywords.Add("return");
			keywords.Add("rule");
			keywords.Add("sequence");
			keywords.Add("set");
			keywords.Add("switch");
			keywords.Add("test");
			keywords.Add("true");
			keywords.Add("typeof");
			keywords.Add("undirected");
			keywords.Add("using");
			keywords.Add("visited");
			keywords.Add("while");
			keywords.Add("yield");
		}

		public static bool IsKnownPackage(string packageName)
		{
			switch(packageName)
			{
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

		public static bool IsKnownFunction(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(IsMathFunction(pack, i, @params)
					|| IsFileFunction(pack, i, @params)
					|| IsTimeFunction(pack, i, @params)
					|| IsGlobalFunction(pack, i, @params))
				return true;
			return false;
		}

		internal static bool IsMathFunction(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("Math"))
				return false;

			switch(i.GetText())
			{
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

		internal static bool IsFileFunction(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("File"))
				return false;

			switch(i.GetText())
			{
			case "exists":
			case "import":
				return true;
			default:
				return false;
			}
		}

		internal static bool IsTimeFunction(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("Time"))
				return false;

			switch(i.GetText())
			{
			case "now":
				return true;
			default:
				return false;
			}
		}

		public static bool IsGlobalFunction(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			return IsGlobalFunction(i.GetText(), @params.ChildrenExact.Count);
		}

		public static bool IsGlobalFunction(string functionName, int numParams)
		{
			switch(functionName)
			{
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
			case "getEquivalent":
			case "getEquivalentStructurally":
				return numParams == 2;
			case "copy":
			case "clone":
				return numParams == 1;
			case "nameof":
				return numParams == 1 || numParams == 0;
			case "uniqueof":
				return numParams == 1 || numParams == 0;
			case "graphof":
				return numParams == 1;
			default:
				return false;
			}
		}

		public static bool IsIsInIndexFunction(string name)
		{
			switch(name)
			{
			case "isInNodesFromIndex":
				return true; //return numParams == 2;
			case "isInNodesFromIndexSame":
			case "isInNodesFromIndexFrom":
			case "isInNodesFromIndexFromExclusive":
			case "isInNodesFromIndexTo":
			case "isInNodesFromIndexToExclusive":
				return true; //return numParams == 3;
			case "isInNodesFromIndexFromTo":
			case "isInNodesFromIndexFromExclusiveTo":
			case "isInNodesFromIndexFromToExclusive":
			case "isInNodesFromIndexFromExclusiveToExclusive":
				return true; //return numParams == 4;
			case "isInEdgesFromIndex":
				return true; //return numParams == 2;
			case "isInEdgesFromIndexSame":
			case "isInEdgesFromIndexFrom":
			case "isInEdgesFromIndexFromExclusive":
			case "isInEdgesFromIndexTo":
			case "isInEdgesFromIndexToExclusive":
				return true; //return numParams == 3;
			case "isInEdgesFromIndexFromTo":
			case "isInEdgesFromIndexFromExclusiveTo":
			case "isInEdgesFromIndexFromToExclusive":
			case "isInEdgesFromIndexFromExclusiveToExclusive":
				return true; //return numParams == 4;
			default:
				return false;
			}
		}

		public static bool IsNonIsInIndexFunction(string name)
		{
			switch(name)
			{
			case "nodesFromIndex":
				return true; //return numParams == 1;
			case "nodesFromIndexSame":
			case "nodesFromIndexFrom":
			case "nodesFromIndexFromExclusive":
			case "nodesFromIndexTo":
			case "nodesFromIndexToExclusive":
				return true; //return numParams == 2;
			case "nodesFromIndexFromTo":
			case "nodesFromIndexFromExclusiveTo":
			case "nodesFromIndexFromToExclusive":
			case "nodesFromIndexFromExclusiveToExclusive":
				return true; //return numParams == 3;
			case "edgesFromIndex":
				return true; //return numParams == 1;
			case "edgesFromIndexSame":
			case "edgesFromIndexFrom":
			case "edgesFromIndexFromExclusive":
			case "edgesFromIndexTo":
			case "edgesFromIndexToExclusive":
				return true; //return numParams == 2;
			case "edgesFromIndexFromTo":
			case "edgesFromIndexFromExclusiveTo":
			case "edgesFromIndexFromToExclusive":
			case "edgesFromIndexFromExclusiveToExclusive":
				return true; //return numParams == 3;
			case "countNodesFromIndex":
				return true; //return numParams == 1;
			case "countNodesFromIndexSame":
			case "countNodesFromIndexFrom":
			case "countNodesFromIndexFromExclusive":
			case "countNodesFromIndexTo":
			case "countNodesFromIndexToExclusive":
				return true; //return numParams == 2;
			case "countNodesFromIndexFromTo":
			case "countNodesFromIndexFromExclusiveTo":
			case "countNodesFromIndexFromToExclusive":
			case "countNodesFromIndexFromExclusiveToExclusive":
				return true; //return numParams == 3;
			case "countEdgesFromIndex":
				return true; //return numParams == 1;
			case "countEdgesFromIndexSame":
			case "countEdgesFromIndexFrom":
			case "countEdgesFromIndexFromExclusive":
			case "countEdgesFromIndexTo":
			case "countEdgesFromIndexToExclusive":
				return true; //return numParams == 2;
			case "countEdgesFromIndexFromTo":
			case "countEdgesFromIndexFromExclusiveTo":
			case "countEdgesFromIndexFromToExclusive":
			case "countEdgesFromIndexFromExclusiveToExclusive":
				return true; //return numParams == 3;
			case "nodesFromIndexAsArrayAscending":
			case "nodesFromIndexAsArrayDescending":
				return true; //return numParams == 1;
			case "nodesFromIndexSameAsArray":
			case "nodesFromIndexFromAsArrayAscending":
			case "nodesFromIndexFromExclusiveAsArrayAscending":
			case "nodesFromIndexToAsArrayAscending":
			case "nodesFromIndexToExclusiveAsArrayAscending":
			case "nodesFromIndexFromAsArrayDescending":
			case "nodesFromIndexFromExclusiveAsArrayDescending":
			case "nodesFromIndexToAsArrayDescending":
			case "nodesFromIndexToExclusiveAsArrayDescending":
				return true; //return numParams == 2;
			case "nodesFromIndexFromToAsArrayAscending":
			case "nodesFromIndexFromExclusiveToAsArrayAscending":
			case "nodesFromIndexFromToExclusiveAsArrayAscending":
			case "nodesFromIndexFromExclusiveToExclusiveAsArrayAscending":
			case "nodesFromIndexFromToAsArrayDescending":
			case "nodesFromIndexFromExclusiveToAsArrayDescending":
			case "nodesFromIndexFromToExclusiveAsArrayDescending":
			case "nodesFromIndexFromExclusiveToExclusiveAsArrayDescending":
				return true; //return numParams == 3;
			case "edgesFromIndexAsArrayAscending":
			case "edgesFromIndexAsArrayDescending":
				return true; //return numParams == 1;
			case "edgesFromIndexSameAsArray":
			case "edgesFromIndexFromAsArrayAscending":
			case "edgesFromIndexFromExclusiveAsArrayAscending":
			case "edgesFromIndexToAsArrayAscending":
			case "edgesFromIndexToExclusiveAsArrayAscending":
			case "edgesFromIndexFromAsArrayDescending":
			case "edgesFromIndexFromExclusiveAsArrayDescending":
			case "edgesFromIndexToAsArrayDescending":
			case "edgesFromIndexToExclusiveAsArrayDescending":
				return true; //return numParams == 2;
			case "edgesFromIndexFromToAsArrayAscending":
			case "edgesFromIndexFromExclusiveToAsArrayAscending":
			case "edgesFromIndexFromToExclusiveAsArrayAscending":
			case "edgesFromIndexFromExclusiveToExclusiveAsArrayAscending":
			case "edgesFromIndexFromToAsArrayDescending":
			case "edgesFromIndexFromExclusiveToAsArrayDescending":
			case "edgesFromIndexFromToExclusiveAsArrayDescending":
			case "edgesFromIndexFromExclusiveToExclusiveAsArrayDescending":
				return true; //return numParams == 3;
			case "nodesFromIndexMultipleFromTo":
			case "edgesFromIndexMultipleFromTo":
				return true; //return numParams >= 3;
			case "countFromIndex":
				return true; //return numParams == 2;
			case "minNodeFromIndex":
			case "maxNodeFromIndex":
			case "minEdgeFromIndex":
			case "maxEdgeFromIndex":
			case "indexSize":
				return true; //return numParams == 1;
			default:
				return false;
			}
		}

		public static bool IsKnownForFunction(string name)
		{
			switch(name)
			{
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

		public static bool IsKnownForIndexFunction(string name)
		{
			switch(name)
			{
			case "nodesFromIndexSame":
			case "edgesFromIndexSame":
				return true;
			case "nodesFromIndexAscending":
			case "nodesFromIndexFromAscending":
			case "nodesFromIndexFromExclusiveAscending":
			case "nodesFromIndexToAscending":
			case "nodesFromIndexToExclusiveAscending":
			case "nodesFromIndexFromToAscending":
			case "nodesFromIndexFromExclusiveToAscending":
			case "nodesFromIndexFromToExclusiveAscending":
			case "nodesFromIndexFromExclusiveToExclusiveAscending":
			case "edgesFromIndexAscending":
			case "edgesFromIndexFromAscending":
			case "edgesFromIndexFromExclusiveAscending":
			case "edgesFromIndexToAscending":
			case "edgesFromIndexToExclusiveAscending":
			case "edgesFromIndexFromToAscending":
			case "edgesFromIndexFromExclusiveToAscending":
			case "edgesFromIndexFromToExclusiveAscending":
			case "edgesFromIndexFromExclusiveToExclusiveAscending":
				return true;
			case "nodesFromIndexDescending":
			case "nodesFromIndexFromDescending":
			case "nodesFromIndexFromExclusiveDescending":
			case "nodesFromIndexToDescending":
			case "nodesFromIndexToExclusiveDescending":
			case "nodesFromIndexFromToDescending":
			case "nodesFromIndexFromExclusiveToDescending":
			case "nodesFromIndexFromToExclusiveDescending":
			case "nodesFromIndexFromExclusiveToExclusiveDescending":
			case "edgesFromIndexDescending":
			case "edgesFromIndexFromDescending":
			case "edgesFromIndexFromExclusiveDescending":
			case "edgesFromIndexToDescending":
			case "edgesFromIndexToExclusiveDescending":
			case "edgesFromIndexFromToDescending":
			case "edgesFromIndexFromExclusiveToDescending":
			case "edgesFromIndexFromToExclusiveDescending":
			case "edgesFromIndexFromExclusiveToExclusiveDescending":
				return true;
			case "nodesFromIndexMultipleFromTo":
			case "edgesFromIndexMultipleFromTo":
				return true;
			default:
				return false;
			}
		}

		public static bool IsKnownProcedure(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(IsFileProcedure(pack, i, @params)
					|| IsTransactionProcedure(pack, i, @params)
					|| IsDebugProcedure(pack, i, @params)
					|| IsSynchronizationProcedure(pack, i, @params)
					|| IsGlobalProcedure(pack, i, @params))
				return true;
			return false;
		}

		internal static bool IsFileProcedure(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("File"))
				return false;

			switch(i.GetText())
			{
			case "export":
			case "delete":
				return true;
			default:
				return false;
			}
		}

		internal static bool IsTransactionProcedure(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("Transaction"))
				return false;

			switch(i.GetText())
			{
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

		internal static bool IsDebugProcedure(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("Debug"))
				return false;

			switch(i.GetText())
			{
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

		internal static bool IsSynchronizationProcedure(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			if(pack == null || !pack.GetText().Equals("Synchronization"))
				return false;

			switch(i.GetText())
			{
			case "enter":
			case "tryenter":
			case "exit":
				return true;
			default:
				return false;
			}
		}

		public static bool IsGlobalProcedure(Token pack, Token i, CollectNode<ExprNode> @params)
		{
			return IsGlobalProcedure(i.GetText(), @params.ChildrenExact.Count);
		}

		public static bool IsGlobalProcedure(string procedureName, int numParams)
		{
			switch(procedureName)
			{
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
			case "addClone":
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
			case "getEquivalentOrAdd":
			case "getEquivalentStructurallyOrAdd":
				return numParams == 2;
			case "assert":
			case "assertAlways":
				return true;
			default:
				return false;
			}
		}

		public static bool IsArrayAttributeAccessMethodName(string name)
		{
			switch(name)
			{
			case "indexOfBy":
			case "indexOfOrderedBy":
			case "lastIndexOfBy":
			case "orderAscendingBy":
			case "orderDescendingBy":
			case "groupBy":
			case "keepOneForEach":
			case "extract":
				return true;
			default:
				return false;
			}
		}

		public static bool IsAutoSuppliedFilterName(string name)
		{
			switch(name)
			{
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

		public static bool IsAutoGeneratedBaseFilterName(string name)
		{
			switch(name)
			{
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

		public virtual List<FilterAutoDeclNode> GetFiltersAutoSupplied(IteratedDeclNode iterated)
		{
			List<FilterAutoDeclNode> autoSuppliedFilters = new List<FilterAutoDeclNode>();

			if(iterated != null) // may happen due to syntactic predicate / backtracking peek ahead
			{
				autoSuppliedFilters.Add(GetFilterAutoSupplied("keepFirst", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("keepLast", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("removeFirst", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("removeLast", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("keepFirstFraction", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("keepLastFraction", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("removeFirstFraction", iterated));
				autoSuppliedFilters.Add(GetFilterAutoSupplied("removeLastFraction", iterated));
			}

			return autoSuppliedFilters;
		}

		public virtual FilterAutoDeclNode GetFilterAutoSupplied(string ident, IteratedDeclNode iterated)
		{
			IdentNode filterIdent = new IdentNode(Define(ParserEnvironment.ACTIONS, ident, iterated.Coords));
			return new FilterAutoSuppliedDeclNode(filterIdent, iterated.Ident);
		}

		public abstract UnitNode ParseActions(File inputFile);

		public abstract ModelNode ParseModel(File inputFile);

		public abstract void PushFile(Lexer lexer, File inputFile);

		public abstract bool PopFile(Lexer lexer);

		public abstract string Filename {get;}

		public abstract bool HadError();
	}

}
