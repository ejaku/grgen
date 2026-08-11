using System;
using System.Collections.Generic;
using System.Text;

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
///*******************************************************************************
/// This file contains the code generator for firm-internal graph rewriting
/// ********************************************************************************
/// </summary>

/// <summary>
/// A GrGen Backend which generates C code for a frame-based
/// graph model impl and a frame based graph matcher
/// @author Veit Batz, Rubino Geiss, Andreas Schoesser
/// </summary>

namespace de.unika.ipd.grgen.be.C
{

using Sys = de.unika.ipd.grgen.Sys;
using Backend = de.unika.ipd.grgen.be.Backend;
using BackendFactory = de.unika.ipd.grgen.be.BackendFactory;
using de.unika.ipd.grgen.ir;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Cast = de.unika.ipd.grgen.ir.expr.Cast;
using Constant = de.unika.ipd.grgen.ir.expr.Constant;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Operator = de.unika.ipd.grgen.ir.expr.Operator;
using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using Assignment = de.unika.ipd.grgen.ir.stmt.Assignment;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
using Type = de.unika.ipd.grgen.ir.type.Type;
using TypeClass = de.unika.ipd.grgen.ir.type.Type.TypeClass;

public class SearchPlanBackend : MoreInformationCollector, BackendFactory
{
	private const int nodesInUse = 1;
	private const int edgesInUse = 2;

	/* for modified-flags */
	private const int MOD_DELETED = 4;
	private const int MOD_ASSIGNED = 2;
	private const int MOD_RETYPED = 1;

	private readonly string MODE_EDGE_NAME = "has_mode";
	private readonly string LS_MODE_EDGE_NAME = "has_ls_mode";

	protected internal readonly bool emit_subgraph_info = false;

	private enum GraphType
	{
		Pattern,
		Negative,
		Replacement
	}

	/* (binary) operator symbols (of the C-language) */
	// ATTENTION: the first two shift operations are signed shifts
	// 		the second right shift is signed. This backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	private static string GetOperatorSymbol(OperatorCode opCode)
	{
		switch(opCode)
		{
		case OperatorCode.LOG_OR:
			return "||";
		case OperatorCode.LOG_AND:
			return "&&";
		case OperatorCode.BIT_OR:
			return "|";
		case OperatorCode.BIT_XOR:
			return "^";
		case OperatorCode.BIT_AND:
			return "&";
		case OperatorCode.EQ:
			return "==";
		case OperatorCode.NE:
			return "!=";
		case OperatorCode.LT:
			return "<";
		case OperatorCode.LE:
			return "<=";
		case OperatorCode.GT:
			return ">";
		case OperatorCode.GE:
			return ">=";
		case OperatorCode.SHL:
			return "<<";
		case OperatorCode.SHR:
			return ">>";
		case OperatorCode.BIT_SHR:
			return ">>";
		case OperatorCode.ADD:
			return "+";
		case OperatorCode.SUB:
			return "-";
		case OperatorCode.MUL:
			return "*";
		case OperatorCode.DIV:
			return "/";
		case OperatorCode.MOD:
			return "%";
		default:
			throw new Exception("internal failure");
		}
	}

	// --------------------------------------------------
	// Generates a Id for a Pattern node and saves it for
	// later usage.
	// --------------------------------------------------

	private class IdGenerator<T>
	{
		private readonly SearchPlanBackend outerInstance;

		public IdGenerator(SearchPlanBackend outerInstance)
		{
			this.outerInstance = outerInstance;
		}

		internal LinkedHashMap<T, int> idMap = new LinkedHashMap<T, int>();

		internal int offset = 0;

		public virtual int NewKey
		{
			get
			{
			offset++;
			return MaxIndex;
			}
		}

		internal virtual int ComputeId(T elem)
		{
			if(!idMap.ContainsKey(elem))
				idMap.Put(elem, Convert.ToInt32(MaxIndex + 1));
			return idMap.Get(elem).IntValue();
		}

		internal virtual bool IsKnown(T elem)
		{
			return idMap.ContainsKey(elem);
		}

		public virtual int MaxIndex
		{
			get
			{
			return (idMap.Size() + offset - 1);
			}
		}
	}

	/* ------------------------------------------
	 * Create a new backend.
	 * @return A new backend.
	 * ------------------------------------------ */

	public virtual Backend Backend
	{
		get
		{
		return this;
		}
	}

	/* ---------------------------------------------------------------------------------------------------------
	 * Initializes the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.Sys, java.io.File)
	 * --------------------------------------------------------------------------------------------------------- */

	public override void Init(Unit unit, Sys system, File outputPath)
	{
		base.Init(unit, system, outputPath);
		//		this.unit = unit;
		//		this.path = outputPath;
		//		this.system = system;
		//		path.mkdirs();
	}

	/* ----------------------------------------------------
	 * Starts the C-code Genration of the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 * ---------------------------------------------------- */

	public override void Generate()
	{
		// Emit an include file for Makefiles
		PrintStream ps = OpenFile("unit.mak");
		ps.Println("#\n# generated by grgen, don't edit\n#");
		ps.Println("UNIT_NAME = " + FormatId(unit.UnitName));
		CloseFile(ps);

		Console.WriteLine("The frame-based GrGen backend...");
		Console.WriteLine("  generating the pattern...");

		// Fill the StringBuffer to be written as a file
		StringBuilder sb = new StringBuilder();
		sb.Append("/* generated by grgen, don't edit */\n\n");
		sb.Append("#include <assert.h>\n");
		sb.Append("#include \"config.h\"\n");
		sb.Append("#include \"benode_t.h\"\n");
		sb.Append("#include \"firm.h\"\n");
		sb.Append("#include \"grs.h\"\n");
		sb.Append("#include \"ia32_new_nodes.h\"\n");
		sb.Append("#include \"ia32_getset.h\"\n");

		FindModeType();
		FindConstType();
		GenTypes(sb);
		GenPatterns(sb);
		GenInterface(sb);
		WriteFile("gen_patterns.c", sb);

		Console.WriteLine("  generating XML overview...");

		// write an overview of all generated Ids
		ps = OpenFile("overview.xml");
		WriteOverview(ps);
		CloseFile(ps);

		Console.WriteLine("  done!");
	}

	/* -------------------------------------------------------------------
	 * Method findModeType
	 *
	 * Look for the generic 'Mode' node that serves as a supertype for all
	 * FIRM modes;
	 * ------------------------------------------------------------------- */

	internal NodeType MODE_TYPE;

	public virtual void FindModeType()
	{
		foreach(NodeType node in nodeTypeMap.Keys)
		{
			if(node.Ident.ToString().Equals("Mode"))
			{
				MODE_TYPE = node;
				return;
			}
		}
		Console.WriteLine("Warning: MODE_TYPE not found!");
	}

	/* -------------------------------------------
	 * Method findConstType
	 *
	 * Look up some other node types we need later
	 * when dumping conditions and evals.
	 * ------------------------------------------ */

	internal NodeType CONST_TYPE = null;
	internal NodeType COND_TYPE = null;
	internal NodeType VPROJ_TYPE = null;
	internal NodeType PROJ_TYPE = null;
	internal NodeType SYM_CONST = null;
	internal NodeType FRAMEADDR = null;
	internal NodeType MULTIPLE_ADD_TYPE = null;
	internal NodeType IA32_SUB = null;

	internal EdgeType DF = null;

	public virtual void FindConstType()
	{
		foreach(NodeType node in nodeTypeMap.Keys)
		{
			if(node.Ident.ToString().Equals("Const"))
				CONST_TYPE = node;
			if(node.Ident.ToString().Equals("Cond"))
				COND_TYPE = node;
			if(node.Ident.ToString().Equals("VProj"))
				VPROJ_TYPE = node;
			if(node.Ident.ToString().Equals("Proj"))
				PROJ_TYPE = node;
			if(node.Ident.ToString().Equals("MultipleAdd"))
				MULTIPLE_ADD_TYPE = node;
			if(node.Ident.ToString().Equals("SymConst"))
				SYM_CONST = node;
			if(node.Ident.ToString().Equals("ia32_Sub"))
				IA32_SUB = node;
			if(node.Ident.ToString().Equals("be_FrameAddr"))
				FRAMEADDR = node;
		}
		if(CONST_TYPE == null)
			Console.WriteLine("Warning: CONST_TYPE not found!");
		if(VPROJ_TYPE == null)
			Console.WriteLine("Warning: VPROJ_TYPE not found!");
		if(PROJ_TYPE == null)
			Console.WriteLine("Warning: PROJ_TYPE not found!");
		if(IA32_SUB == null)
			Console.WriteLine("Warning: IA32_SUB not found!");

		foreach(EdgeType edge in edgeTypeMap.Keys)
		{
			if(edge.Ident.ToString().Equals("df"))
				DF = edge;
		}
		if(DF == null)
			Console.WriteLine("Warning: DF not found!");
	}

	/* ---------------------------------------------------------------
	 * Method genTypes
	 *
	 * Generates all FIRM ops. Creates a new one if the FIRM op is not
	 * existing, or uses the existing one.
	 * @param    sb		  a  StringBuffer
	 * --------------------------------------------------------------- */
	private void GenTypes(StringBuilder sb)
	{
		string indent = "\t";
		StringBuilder initsb = new StringBuilder();
		sb.Append("/* nodeTypeMap */ \n");
		initsb.Append("/* init node ops and modes */\n");
		initsb.Append("static void init(void) {\n");
		foreach(NodeType nodeType in nodeTypeMap.Keys)
		{
			if(!nodeType.IsCastableTo(MODE_TYPE))
			{
				// Only dump nodes that are real FIRM nodes
				// => Skip the pseudo "Mode"-Nodes
				string type = nodeType.Ident.ToString();
				sb.Append("ir_op* grs_op_" + type + ";\n");
				initsb.Append(indent + "grs_op_" + type + " = ext_grs_lookup_op(\"" + type + "\");\n");
				initsb.Append(indent + "grs_op_" + type + " = grs_op_" + type +
						" ? grs_op_" + type + " : new_ir_op(get_next_ir_opcode(), \"" +
						type + "\", op_pin_state_pinned,  irop_flag_none, oparity_dynamic,  0, 0, NULL);\n");
			}
		}
		sb.Append("/* nodeTypeMap END */\n\n");
		initsb.Append("} /* init node ops and modes */\n\n");
		sb.Append(initsb);
	}

	/* ------------------------------------------------------------------
	 * Method genPatterns generates the patterns needed by the search plan
	 * builder. It consists mainly of the left hand side of the rule.
	 *
	 * @param    sb		  a  StringBuffer
	 * ------------------------------------------------------------------ */
	private void GenPatterns(StringBuilder sb)
	{
		string indent = "\t";
		foreach(Rule action in unit.ActionRules)
		{
			if(action.Right != null)
			{

				string actionName = action.Ident.ToString();

				StringBuilder sb2 = new StringBuilder(); // append pattern after condition
				IdGenerator<Node> nodeIds = new IdGenerator<Node>(this); // To generate uique numbers per rule
				IdGenerator<Edge> edgeIds = new IdGenerator<Edge>(this);

				// Initialize function
				sb2.Append("/* functions for building the pattern of action " + actionName + " */\n");
				sb2.Append("static INLINE ext_grs_action_t *grs_action_" + actionName + "_init(void) {\n");
				sb2.Append(indent + "ext_grs_action_t *act = ext_grs_new_action(ext_grs_k_rule, \"" + actionName + "\");\n");
				sb2.Append(indent + "int check;\n");

				GenPattern(sb2, action, nodeIds, edgeIds);

				sb2.Append(indent + "return act;\n");
				sb2.Append("} /* " + actionName + " */\n\n\n");

				// Conditions and Evals
				GenConditionFunctions(sb, indent, actionName, action, nodeIds, edgeIds);
				GenEvalFunctions(sb, indent, action, nodeIds, edgeIds);

				sb.Append(sb2);
			}
			else
				throw new System.NotSupportedException(action.ToString());
		}
	}

	/* ----------------------------------------------------------------------------------------------
	 * Method genConditionFunctions generates the fuctions that evaluate the conditions of an action.
	 *
	 * @param    sb		  a  StringBuffer
	 * @param    indent	      a  String
	 * @param    actionName	  a  String
	 * @param    rule		a  Rule
	 * ---------------------------------------------------------------------------------------------- */
	private void GenConditionFunctions(StringBuilder sb, string indent, string actionName, Rule rule, IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds)
	{
		sb.Append("/* functions for evaluation of conditions of action " + actionName + " */\n");

		// conditions for L
		GenConditionFunction(sb, indent, rule.Left, nodeIds, edgeIds);

		// conditions  for NACs
		foreach(PatternGraphLhs neg in rule.Left.Negs)
			GenConditionFunction(sb, indent, neg, nodeIds, edgeIds);
		sb.Append("\n");
	}

	/* -------------------------------------------
	 * Method genConditionFunction
	 *
	 * Generate the function body for a condition
	 * ------------------------------------------- */

	private void GenConditionFunction(StringBuilder sb, string indent, PatternGraphLhs graph, IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds)
	{
		foreach(Expression cond in graph.Conditions)
		{
			sb.Append("static int grs_cond_func_" + cond.Id + "(ir_node **pat_node_map, const ir_edge_t **edge_map) {\n");
			int useFlags = GetUnusedEvalParams(cond);
			if((useFlags & nodesInUse) == 0)
				sb.Append(indent + "(void) pat_node_map;\n");
			if((useFlags & edgesInUse) == 0)
				sb.Append(indent + "(void) edge_map;\n");
			sb.Append(indent + "return ");
			GenConditionEval(sb, cond, nodeIds, edgeIds);
			sb.Append(";\n");
			sb.Append("}\n");
		}
	}

	private int GetUnusedEvalParams(Expression cond)
	{
		if(cond is Operator)
		{
			Operator op = (Operator)cond;
			switch(op.Arity())
			{
			case 1:
				return GetUnusedEvalParams(op.GetOperand(0));
			case 2:
				return GetUnusedEvalParams(op.GetOperand(0)) | GetUnusedEvalParams(op.GetOperand(1));
			case 3:
				if(op.OpCode == OperatorCode.COND)
					return GetUnusedEvalParams(op.GetOperand(0))
							| GetUnusedEvalParams(op.GetOperand(1))
							| GetUnusedEvalParams(op.GetOperand(2));
				//$FALL-THROUGH$
			default:
				// nothing to do
					break;
			}
		}
		else if(cond is Qualification)
		{
			Qualification qual = (Qualification)cond;
			Entity entity = qual.Owner;

			if(entity is Node)
				return nodesInUse;
			else if(entity is Edge)
				return edgesInUse;
		}
		return 0;
	}

	/* -------------------------------------------
	 * Method genEvalFunctions
	 *
	 * Generates eval functions for each eval list
	 * ------------------------------------------- */
	private void GenEvalFunctions(StringBuilder sb, string indent, Rule rule, IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds)
	{
		sb.Append("/* function to do eval assignments */\n");

		StringBuilder ins = new StringBuilder();
		StringBuilder outs = new StringBuilder();

		foreach(EvalStatements evalStmts in rule.Evals)
		{
			foreach(EvalStatement evalStmt in evalStmts.evalStatements)
			{
				if(!(evalStmt is Assignment))
					continue;
				Assignment eval = (Assignment)evalStmt;
				Expression targetExpr = eval.Target;
				if(!(targetExpr is Qualification))
					throw new System.NotSupportedException("The C backend only supports assignments to qualified expressions, yet!");
				Qualification target = (Qualification)targetExpr;
				Entity targetOwner = target.Owner;
				Entity targetMember = target.Member;
				Expression expr = eval.Expression;
				StringBuilder cond_dummy = new StringBuilder();

				outs.Append("static void grs_eval_out_func_" + eval.Id
						+ "(ir_node ** const rpl_node_map, ir_edge_t ** const rpl_edge_map, ir_node **pat_node_map, ");
				if(eval.Expression.Type.Classify() == Type.TypeClass.IS_INTEGER)
					outs.Append("int data) {\n");
				else
					outs.Append("void *data) {\n");

				outs.Append(indent + "(void) pat_node_map;\n");
				outs.Append(indent + "(void) rpl_edge_map;\n");
				outs.Append(indent + "(void) data;\n");
				outs.Append(indent + "set_grgen_" + targetMember.Ident + "(");
				// Each node type has to be treated differently when accessing attributes
				// Care about that here.
				if(targetOwner is Node)
				{
					Node n = (Node)targetOwner;
					outs.Append("rpl_node_map[" + nodeIds.ComputeId(n) + "/* " + n.Ident + " */], ");
				}
				else if(targetOwner is Edge)
				{
					Edge e = (Edge)targetOwner;
					outs.Append("rpl_edge_map[" + edgeIds.ComputeId(e) + "/* " + e.Ident + " */], ");
				}
				else
					throw new System.NotSupportedException("Unsupported Entity (" + targetOwner + ")");

				if(expr is Constant)
				{
					/* we don't need eval_in functions for constant values */
					GenConditionEval(cond_dummy, expr, nodeIds, edgeIds);
					outs.Append(cond_dummy);
				}
				else
				{
					/* generate the eval_in function */
					ins.Append("static void *grs_eval_in_func_" + eval.Id
							+ "(ir_node ** const pat_node_map, ir_edge_t ** pat_edge_map) {\n");
					ins.Append(indent + "(void) pat_edge_map;\n");
					ins.Append(indent + "return (void*)");
					GenConditionEval(ins, expr, nodeIds, edgeIds);
					ins.Append(";\n}\n\n");
					outs.Append("data");
				}

				outs.Append(");\n}\n");
			}
		}
		sb.Append(ins);
		sb.Append(outs);
	}

	/* ------------------------------------------------------------------
	 * Method registerEvalFunctions
	 *
	 * Generates code to register an eval function to the pattern matcher
	 * ------------------------------------------------------------------ */
	private static void RegisterEvalFunctions(StringBuilder sb, string indent, Rule rule)
	{
		foreach(EvalStatements evalStmts in rule.Evals)
		{
			foreach(EvalStatement evalStmt in evalStmts.evalStatements)
			{
				if(!(evalStmt is Assignment))
					continue;
				Assignment eval = (Assignment)evalStmt;

				sb.Append(indent + "ext_grs_act_register_eval(act, ");
				if(eval.Expression is Constant)
					sb.Append("NULL");
				else
					sb.Append("(ext_grs_eval_in_func_t) &grs_eval_in_func_" + eval.Id);
				sb.Append(", (ext_grs_eval_out_func_t) &grs_eval_out_func_" + eval.Id + ");\n");
			}
		}
	}

	/* -------------------------------------------------------
	 * Method genPattern generates pattern graph for one rule.
	 *
	 * @param    sb		  a  StringBuffer
	 * @param    rule		a  Rule
	 * ------------------------------------------------------ */
	private void GenPattern(StringBuilder sb, Rule rule, IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds)
	{
		string indent = "\t";

		// code for the pattern graph
		sb.Append(indent + "{ /* The action */\n");
		GenPatternGraph(sb, indent + "\t", "ext_grs_act_get_pattern", rule.Left, nodeIds, edgeIds, GraphType.Pattern, rule);

		// code for the negative graphs
		sb.Append(indent + "  /* The negative parts of the pattern */\n");
		int i = 0;
		foreach(PatternGraphLhs neg in rule.Left.Negs)
		{
			sb.Append(indent + "  { /* NAC " + i + "  */\n");
			GenPatternGraph(sb, indent + "    ", "ext_grs_act_impose_negative", neg, nodeIds, edgeIds, GraphType.Negative, rule);
			sb.Append(indent + "  } /* NAC " + i + "  */\n");
			sb.Append("\n");
			i++;
		}

		sb.Append("\n\n");

		// Code for the replacement
		sb.Append(indent + "  { /* The replacement */\n");
		GenGraph(sb, indent + "     ", "ext_grs_act_get_replacement", rule.Right, nodeIds, edgeIds, GraphType.Replacement, rule);
		sb.Append(indent + "  } /* The replacement */\n\n");

		// Code for registering eval functions
		sb.Append(indent + "  /* Eval functions */\n");
		RegisterEvalFunctions(sb, indent + "\t", rule);

		// This is necessary to set the hom statements
		sb.Append(indent + "check = ext_grs_act_mature(act);\n");
		sb.Append(indent + "assert(check);\n");

		// Generate the hom statements.
		GenHom(sb, rule.Left, nodeIds, edgeIds, rule);

		sb.Append(indent + "} /* The Action */\n\n");
	}

	/* -----------------------------------------------------------------------------------
	 * Method genPatternGraph generates code for a L, or a NAC graph.
	 *
	 * @param    sb		  a  StringBuffer
	 * @param    indent	      a  String
	 * @param    funcName	    a  String containing the C-name for getting the graph
	 * @param    parentNodes	 a  Collection<Node> of the nodes of the parent graph
	 * @param    graph	       a  Graph
	 * @param    isNegativeGraph	 true, if current graph is negative
	 * ----------------------------------------------------------------------------------- */

	// The FIRM graph rewriter can't deal with identical node names in pattern/replacement or
	// pattern/NAC's. So for replacement graph or NAC's we change the node name artificially.
	// Remember the changed name in here in relatedNodes. The connection between related nodes
	// is not announced by name like in the GrGen Syntax but by special announce-functions

	internal Dictionary<Node, string> relatedNodes;

	private void GenPatternGraph(StringBuilder sb, string indent, string funcName,
			PatternGraphLhs graph,
			IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds,
			GraphType graphType, Rule rule)
	{
		// Generate the graph
		GenGraph(sb, indent, funcName, graph, nodeIds, edgeIds, graphType, rule);

		// code for the conditions
		GenConditions(sb, indent, graph);

	}

	private static void GenHom(StringBuilder sb, PatternGraphLhs graph,
			IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds, Rule rule)
	{
		foreach(Node n1 in graph.Nodes)
		{
			foreach(Node n2 in graph.Nodes)
			{
				if(n1 != n2 && graph.IsHomomorphic(n1, n2))
				{
					sb.Append("ext_grs_act_allow_nodes_hom(");
					sb.Append("n_" + n1.Ident + ", " + "n_" + n2.Ident + ");\n");
				}
			}
		}
	}

	private void GenGraph(StringBuilder sb, string indent, string funcName,
			PatternGraphBase graph,
			IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds,
			GraphType graphType, Rule rule)
	{
		sb.Append(indent + "ext_grs_graph_t *pattern = " + funcName + "(act);\n\n");

		// nodes
		relatedNodes = new Dictionary<Node, string>();
		GenPatternNodes(sb, indent, graph, nodeIds, graphType, rule);
		sb.Append("\n");

		//edges
		GenPatternEdges(sb, indent, graph, edgeIds, graphType);

		// Clean up
		relatedNodes.Clear();
	}

	/* --------------------------------------
	 * Method genPatterNodes
	 *
	 * Generates all Nodes of a given pattern
	 * -------------------------------------- */

	private int uin = 0;

	private void GenPatternNodes(StringBuilder sb, string indent, PatternGraphBase graph, IdGenerator<Node> nodeIds,
			GraphType graphType, Rule rule)
	{
		sb.Append(indent + "/* The nodes of the pattern */\n");

		foreach(Node node in graph.Nodes)
		{
			// Don't dump mode nodes
			if(node.NodeType.IsCastableTo(MODE_TYPE))
				continue;

			bool related = false;
			string nameSuffix = "";
			if(nodeIds.IsKnown(node))
			{
				nameSuffix = "_" + uin; // Node is already known (positive pattern), make sure that names are different
				uin++;
				related = true; // Flag indicates to emit an relation statement afterwards
			}

			int nodeId;
			string name, type;
			if(node.GetRetypedNode(graph) == null || graphType != GraphType.Replacement)
			{
				nodeId = nodeIds.ComputeId(node);
				name = node.Ident.ToString() + nameSuffix;
				type = node.NodeType.Ident.ToString();
			}
			else
			{ // node gets retyped
				nodeId = nodeIds.ComputeId(node.GetRetypedNode(graph));
				name = node.GetRetypedNode(graph).GetIdent().ToString() + nameSuffix;
				type = node.GetRetypedNode(graph).GetNodeType().Ident.ToString();
			}
			string mode = "ANY";
			string lsmode = "ANY"; // just for Load/Store nodes!

			// define the create_func
			string create_func = "new_rd_" + type;
			// TODO be_* nodes will become _bd_ t some point!
// JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
// ORIGINAL LINE: final String[] starts = { "ia32_", "arm_", "mips_", "ppc32_" };
			string[] starts = new string[] {"ia32_", "arm_", "mips_", "ppc32_"};
			foreach(string start in starts)
			{
				if(type.StartsWith(start, StringComparison.Ordinal))
					create_func = "new_bd_" + type;
			}
			if(type.Equals("IR_node"))
				create_func = "new_ir_node";

			// Search for the "Mode"- and "LS Mode"-edge
			foreach(Edge e in graph.GetOutgoing(node))
			{ // test iff we got an Mode-node
				if(e.EdgeType.Ident.ToString().Equals(MODE_EDGE_NAME))
				{
					// Found the "mode" edge. Save the mode of the current node for dumping
					Node modeNode = graph.GetTarget(e);
					//System.out.println("'" + modeNode.getNodeType().getIdent().toString() + "'");
					if(null != modeNode && (mode.Equals("ANY") || !rule.CommonEdges.Contains(e)))
						mode = modeNode.NodeType.Ident.ToString().Substring(5);
				}
				if(e.EdgeType.Ident.ToString().Equals(LS_MODE_EDGE_NAME))
				{
					Node modeNode = graph.GetTarget(e);
					if(null != modeNode)
						lsmode = modeNode.NodeType.Ident.ToString().Substring(5);
				}
			}

			// TODO make type constraints:
			// sb.append(indent + "/* TODO typeof("+name+") = " + type +
			//			  " \\ " + node.getConstraints()  +"*/\n");

			name = name.Replace('$', '_');

			// Check if the node is related to a positive node
			if(!related)
				sb.Append(indent + "ext_grs_node_t *n_" + name + // No, Write statement to file
						" = ext_grs_act_add_node(pattern, \"" +
						name + "\", grs_op_" + type + ", mode_" + mode + ", mode_" + lsmode +
						", " + nodeId + ", &" + create_func +
						", " + GetModifiedFlags(rule, node) + ");\n");

			else
			{
				string related_name = node.Ident.ToString(); // Yes, the regular node name without suffix

				sb.Append(indent + "ext_grs_node_t *n_" + name + " = ");
				if(graphType == GraphType.Negative)
				{
					sb.Append("ext_grs_act_add_related_node(pattern, \"" +
							name + "\", mode_" + mode + ", mode_" + lsmode + ", n_" +
							related_name + ", " + nodeIds.NewKey);
				}
				else
				{
					sb.Append("ext_grs_act_add_node_to_keep(pattern, \"" +
							name + "\", grs_op_" + type + ", mode_" + mode + ", mode_" + lsmode +
							", " + nodeId + ", n_" + related_name +
							", &" + create_func + ", " + GetModifiedFlags(rule, node));
				}

				sb.Append(");\n");
				sb.Append(indent + "(void) n_" + name + ";\n");
				//System.out.println(relatedNodes + "; " + node + "; " + name);
				relatedNodes[node] = name; // Name was changed for neg nodes. Remember new
												// name for the creation of edges.
			}
		}
	}

	private static int GetModifiedFlags(Rule rule, Node node)
	{
		int flags = 0;
		if(node.IsRetyped() || node.GetRetypedEntity(rule.Right) != null)
			flags |= MOD_RETYPED;

		if(!rule.CommonNodes.Contains(node))
			flags |= MOD_DELETED;

		foreach(EvalStatements evalStmts in rule.Evals)
		{
			foreach(EvalStatement evalStmt in evalStmts.evalStatements)
			{
				if(!(evalStmt is Assignment))
					continue;
				Assignment a = (Assignment)evalStmt;

				Expression targetExpr = a.Target;
				if(!(targetExpr is Qualification))
					throw new System.NotSupportedException("The C backend only supports assignments to qualified expressions, yet!");
				Qualification target = (Qualification)targetExpr;

				if(target.Owner.CompareTo(node) == 0)
					flags |= MOD_ASSIGNED;
			}
		}

		return flags;
	}

	/* ----------------------------
	 * Method genPatternEdges
	 *
	 * Dumps the edges of a pattern
	 * ---------------------------- */
	private void GenPatternEdges(StringBuilder sb, string indent, PatternGraphBase graph, IdGenerator<Edge> edgeIds, GraphType graphType)
	{
		sb.Append(indent + "/* The edges of the pattern */\n");
		foreach(Edge edge in graph.Edges)
		{
			// Don't dump edges to mode nodes
			if(edge.EdgeType.Ident.ToString().Equals(MODE_EDGE_NAME))
				continue;
			if(edge.EdgeType.Ident.ToString().Equals(LS_MODE_EDGE_NAME))
				continue;

			string nameSuffix = "";
			bool related = false;

			if(edgeIds.IsKnown(edge))
			{
				nameSuffix = "_" + uin; // Edge is already known (positive pattern), make sure the names are different
				uin++;
				related = true; // Flag indicates to emit an relation statement afterwards
			}
			else if(graphType != GraphType.Pattern)
			{
				nameSuffix = "_" + uin; // We're in a negative or replacement graph: Add suffix to avoid name
				uin++; // collision with positive edge names
			}

			int edgeId = edgeIds.ComputeId(edge);
			string edgePos = "ext_grs_NO_EDGE_POS";
			string name = edge.Ident.ToString().Replace('$', '_') + nameSuffix;
			//System.out.println("'" + edge.getIdent().toString() + "'\n");
			if(name.Length > 4 && name.Substring(0, 4).Matches("pos[0123456789]"))
				edgePos = edge.Ident.ToString().Substring(3, 1);
			if(edge.EdgeType.Ident.ToString().Equals("dep"))
			{
				/* dependency edges don't have a position.
				 * The edge type isn't put out, so we code the dep
				 * kindness into the position.
				 */
				edgePos = "ext_grs_DEPENDENCY_EDGE_POS";
			}

			Node src = graph.GetSource(edge);
			Node tgt = graph.GetTarget(edge);

			string sourceName = "";
			string targetName = "";

			// Check if source node is a negative node
			if(relatedNodes.ContainsKey(src))
			{
				// Yes, get negative node name
				sourceName = relatedNodes[src];
			}
			else
				sourceName = src.Ident.ToString(); // No, get regular node name

			// Check if dest node is a negative node
			if(relatedNodes.ContainsKey(tgt))
				targetName = relatedNodes[tgt]; // Yes, get negative node name
			else
				targetName = tgt.Ident.ToString(); // No, get regular node name

			sourceName = sourceName.Replace('$', '_');
			targetName = targetName.Replace('$', '_');

			// Check if the edge is related to a positive edge
			if(!related)
			{
				// Create a regular, independent edge
				sb.Append(indent + "ext_grs_edge_t *e_" + name + // Write statement to file
						" = ext_grs_act_add_edge(pattern, \"" + name +
						"\", " + edgePos + ", n_" + targetName + ", n_" +
						sourceName + ", " + edgeId + ");\n");
				sb.Append(indent + "(void) e_" + name + ";\n");
			}
			else
			{
				// Create a related edge
				string addRelatedEdgeFunc = (graphType == GraphType.Negative) ? "ext_grs_act_add_related_edge" : "ext_grs_act_add_edge_to_keep";
				string related_name = edge.Ident.ToString().Replace('$', '_'); // The original name without suffix
				sb.Append(indent + "ext_grs_edge_t *e_" + name + // Write statement to file
						" = " + addRelatedEdgeFunc + "(pattern, \"" + name +
						"\", " + edgePos + ", n_" + targetName + ", n_" +
						sourceName + ", " + edgeId + ", e_" + related_name + ");\n");
				sb.Append(indent + "(void) e_" + name + ";\n");
			}
		}
		sb.Append("\n");
	}

	/* ---------------------------------------------
	 * Method genConditions
	 *
	 * @param    sb		  a  StringBuffer
	 * @param    indent	      a  String
	 * @param    graph	       a  PatternGraph
	 * --------------------------------------------- */

	private void GenConditions(StringBuilder sb, string indent, PatternGraphLhs graph)
	{
		sb.Append(indent + "/* The conditions of the pattern */\n");
		foreach(Expression cond in graph.Conditions)
		{
			string indent2 = indent + "\t";
			ISet<Node> nodes = new HashSet<Node>();
			ISet<Edge> edges = new HashSet<Edge>();

			CollectNodesnEdges(nodes, edges, cond);

			sb.Append(indent + "{ /* if */\n");

			if(nodes.Count > 0)
			{
				sb.Append(indent2 + "ext_grs_node_t *nodes[" + nodes.Count + "] = ");
				GenSet(sb, nodes);
			}
			else
				sb.Append(indent2 + "ext_grs_node_t **nodes = NULL");
			sb.Append(";\n");

			if(edges.Count > 0)
			{
				sb.Append(indent2 + "ext_grs_edge_t *edges[" + edges.Count + "] = ");
				GenSet(sb, edges);
			}
			else
				sb.Append(indent2 + "ext_grs_edge_t **edges = NULL");
			sb.Append(";\n\n");

			sb.Append(indent2 + "ext_grs_act_register_condition(grs_cond_func_" + cond.Id + ", pattern, " + nodes.Count + ", nodes, " + edges.Count + ", edges);\n");

			sb.Append(indent + "} /* if */\n\n");
		}
	}

	/* ---------------
	 * Method genEvals
	 * --------------- */

	// TODO use or remove it
	/*private void genEvals(StringBuffer sb, String indent, Action action)
	{
		if(evalActions.containsKey(action.getId()))
		{
			//Evaluation eval = evalActions.get(action.getId());
		/* TODO:
			 * Enumarate through all evals of the current action
	 		 * Find out if eval has to be executed BEFORE or AFTER transformation
	 		 * Register eval using func_in and func_out parameters
	 		 * At some other place:
	 		 * Generate eval function
	 		 */
			  /*

				}
				else
				{
				System.out.println("Action has no evals!");
				}


				}*/

	/* ----------------------------------------------
	 * Method genSet dumps C-like Set representation.
	 *
	 * @param    sb		  a  StringBuffer
	 * @param    set		 a  Set
	 * ---------------------------------------------- */
	private static void GenSet<T1>(StringBuilder sb, ISet<T1> set) where T1 : Entity
	{
		sb.Append('{');

		string sep = "";
		foreach(Entity e in set)
		{
			sb.Append(sep);
			if(e is Node)
				sb.Append("n_" + e.Ident.ToString());
			else if(e is Edge)
				sb.Append("e_" + e.Ident.ToString());
			else
				sb.Append(e.Ident.ToString());
			sep = ", ";
		}
		sb.Append('}');
	}

	/* ---------------------------------------------------------------------------------
	 * Method collectNodesnEdges extracts the nodes and edges occuring in an Expression.
	 *
	 * @param    nodes	       a  Set to contain the nodes of cond
	 * @param    edges	       a  Set to contain the edges of cond
	 * @param    cond		an Expression
	 * --------------------------------------------------------------------------------- */
	private void CollectNodesnEdges(ISet<Node> nodes, ISet<Edge> edges, Expression cond)
	{
		if(cond is Qualification)
		{
			Entity entity = ((Qualification)cond).Owner;
			if(entity is Node)
				nodes.Add((Node)entity);
			else if(entity is Edge)
				edges.Add((Edge)entity);
			else
				throw new System.NotSupportedException("Unsupported Entity (" + entity + ")");
		}
		else if(cond is Operator)
		{
			foreach(Expression child in ((Operator)cond).WalkableChildren)
				CollectNodesnEdges(nodes, edges, child);
		}
	}

	/* ---------------------------------------------
	 * Method genConditionEval
	 *
	 * Generates C code for evaluating an expression
	 * in conditions and eval statements
	 * --------------------------------------------- */
	private void GenConditionEval(StringBuilder sb, Expression cond, IdGenerator<Node> nodeIds, IdGenerator<Edge> edgeIds)
	{
		if(cond is Operator)
		{
			Operator op = (Operator)cond;
			switch(op.Arity())
			{
			case 1:
				GenConditionEval(sb, op.GetOperand(0), nodeIds, edgeIds);
				break;
			case 2:
				GenConditionEval(sb, op.GetOperand(0), nodeIds, edgeIds);
				sb.Append(" " + GetOperatorSymbol(op.OpCode) + " ");
				GenConditionEval(sb, op.GetOperand(1), nodeIds, edgeIds);
				break;
			case 3:
				if(op.OpCode == OperatorCode.COND)
				{
					sb.Append("(");
					GenConditionEval(sb, op.GetOperand(0), nodeIds, edgeIds);
					sb.Append(") ? (");
					GenConditionEval(sb, op.GetOperand(1), nodeIds, edgeIds);
					sb.Append(") : (");
					GenConditionEval(sb, op.GetOperand(2), nodeIds, edgeIds);
					sb.Append(")");
					break;
				}
				//$FALL-THROUGH$
			default:
				throw new System.NotSupportedException("Unsupported Operation arrity (" + op.Arity() + ")");
			}
		}
		else if(cond is Qualification)
		{
			Qualification qual = (Qualification)cond;
			Entity entity = qual.Owner;

			if(entity is Node)
			{
				Node n = (Node)entity;

				// We have to treat special FIRM nodes specially

				// Query the proj_nr of a vproj_node
				if(n.NodeType.IsCastableTo(VPROJ_TYPE))
					sb.Append("get_VProj_proj(pat_node_map[" + nodeIds.ComputeId(n) + "/* " + entity.Ident + " */])");
				else if(n.NodeType.IsCastableTo(MULTIPLE_ADD_TYPE))
					sb.Append("get_irn_arity(pat_node_map[" + nodeIds.ComputeId(n) + "/* " + entity.Ident + " */])");
				else if(n.NodeType.IsCastableTo(SYM_CONST))
					sb.Append("get_SymConst_entity(pat_node_map[" + nodeIds.ComputeId(n) + "/* " + entity.Ident + " */])");
				else if(n.NodeType.IsCastableTo(COND_TYPE))
					sb.Append("get_Cond_default_proj(pat_node_map[" + nodeIds.ComputeId(n) + "/* " + entity.Ident + " */])");
				else
				{
					string attribute = qual.Member.Ident.ToString();

					sb.Append("get_grgen_" + attribute + "(pat_node_map[" + nodeIds.ComputeId(n) + "/* " + entity.Ident + " */])");
				}

			}
			else if(entity is Edge)
			{
				// Query the position of a MATCHED egde.
				if(qual.Member.Ident.ToString().Equals("pos"))
					sb.Append("get_edge_src_pos(edge_map[" + edgeIds.ComputeId((Edge)entity) + "/* " + entity.Ident + " */])");
				else
					throw new System.NotSupportedException("Unsupported Edge attribute (" + entity + ")");
			}
			else
				throw new System.NotSupportedException("Unsupported Entity (" + entity + ")");
		}
		else if(cond is Constant)
		{ // gen C-code for constant expressions
			Constant constant = (Constant)cond;
			Type type = constant.Type;

			switch(type.Classify())
			{
			case Type.TypeClass.IS_STRING: //emit C-code for string constants
				// CAUTION! This was modified for INTEGET CONSTANTS!
				// TODO: Make it general if you need it!
				// sb.append("\"" + constant.getValue() + "\"");
				sb.Append(constant.Value.ToString());

				break;
			case Type.TypeClass.IS_BOOLEAN: //emit C-code for boolean constans
				bool? bool_const = (bool?)constant.Value;
				if(bool_const.Value)
					sb.Append("1"); // true-value
				else
					sb.Append("0"); // false-value
				break;
			case Type.TypeClass.IS_INTEGER: //emit C-code for integer constants
				sb.Append(constant.Value.ToString()); // this also applys to enum constants
				break;
			default:
				break;
			}
		}
		else if(cond is Cast)
		{
			// Assumption: generated getter and setter have compatible types,
			// so ignore the cast.
			Cast cast = (Cast)cond;
			GenConditionEval(sb, cast.Expression, nodeIds, edgeIds);
		}
		else
			throw new System.NotSupportedException("Unsupported expression type (" + cond + ")");
	}

	/* -------------------------------------------------------------------------------
	 * Method genInterface
	 * Generates the init() functions and code to create all the ext_grs_op's
	 * if no corresponding FIRM op exists. Also appoints heritage between IR_OP Types.
	 * ------------------------------------------------------------------------------- */
	private void GenInterface(StringBuilder sb)
	{
		string indent = "\t";
		StringBuilder initsb = new StringBuilder();
		StringBuilder array_sb = new StringBuilder();
		string unitName = unit.UnitName;

		initsb.Append("static int init_firm_actions_done = 0;\n");

		initsb.Append("/* function for initializing the actions */\n");
		array_sb.Append("/* array of all actions */\n");
		int action_count = unit.ActionRules.Count;
		array_sb.Append("unsigned int ext_grs_all_actions_count = " + action_count + ";\n");
		array_sb.Append("ext_grs_action_t **ext_grs_all_actions[" + action_count + "] = {\n");
		sb.Append("/* global variables containing the actions */\n");

		// Initialize the actions.
		initsb.Append("void ext_grs_action_init_" + unitName + "(void) {\n");
		initsb.Append("if (init_firm_actions_done) return;\n");
		initsb.Append("init_firm_actions_done = 1;\n");
		initsb.Append(indent + "init();\n");
		foreach(Rule action in unit.ActionRules)
		{
			if(action.Right != null)
			{
				string actionName = action.Ident.ToString();
				string fqactionName = "ext_grs_action_" + unitName + "_" + actionName;

				initsb.Append(indent + fqactionName + " = grs_action_" + actionName + "_init();\n");
				sb.Append("ext_grs_action_t *" + fqactionName + ";\n");
				array_sb.Append(indent + "&" + fqactionName + ",\n");
			}
		}
		initsb.Append("\n" + indent + "/* establish inheritance */\n");
		foreach(InheritanceType type in nodeTypeMap.Keys)
		{

			if(!type.IsCastableTo(MODE_TYPE))
			{
				// Don't dump the inheritance of the pseudo "Mode"-Nodes

				string typeName = type.Ident.ToString();
				foreach(InheritanceType superType in type.AllSuperTypes)
					initsb.Append(indent + "ext_grs_appoint_heir(grs_op_" + typeName + ", grs_op_" + superType.Ident + ");\n");
				initsb.Append("\n");
			}
		}
		sb.Append("\n" + array_sb + "};\n");

		initsb.Append(indent + "ext_grs_inheritance_mature();\n");
		initsb.Append(indent + "return;\n");
		initsb.Append("}\n\n");

		// Delete functions
		foreach(Rule action in unit.ActionRules)
		{
			if(action.Right != null)
			{
				string actionName = action.Ident.ToString();

				initsb.Append("/* functions for building the pattern of action " + actionName + " */\n");
				initsb.Append("static INLINE void grs_action_" + actionName + "_del(void) {\n");
				initsb.Append(indent + "ext_grs_del_action(ext_grs_action_" + unit.UnitName + "_" + actionName + ");\n");
				initsb.Append(indent + "return;\n");

				initsb.Append("} /* " + actionName + " */\n\n\n");
			}
		}

		// Delete the actions.
		initsb.Append("void ext_grs_action_del_" + unitName + "(void) {\n");
		initsb.Append("if(!init_firm_actions_done) return;\n");
		initsb.Append("init_firm_actions_done = 0;\n");
		foreach(Rule action in unit.ActionRules)
		{
			if(action.Right != null)
			{
				string actionName = action.Ident.ToString();

				initsb.Append(indent + "grs_action_" + actionName + "_del();\n");
			}
		}
		initsb.Append(indent + "return;\n");
		initsb.Append("}\n\n");

		sb.Append("\n" + initsb);
	}

	/* --------------
	 * Dumps a figlet
	 * --------------
	 */
	// TODO use or remove it
	/*private void figlet(String indent, StringBuffer sb) {
		try {
			String line;
			Process p = Runtime.getRuntime().exec
			(System.getenv("windir") +"\\system32\\"+"tree.com /A");
			BufferedReader input = new BufferedReader(new InputStreamReader(p.getInputStream()));
			while ((line = input.readLine()) != null)
			{
				sb.append(indent + line);
			}
			input.close();
		}
		catch (Exception err) {
			err.printStackTrace();
		}
	}*/
}

}
