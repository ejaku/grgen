// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ExternalAttributeEvaluationExample\ExternalAttributeEvaluation.grg" on Sun Nov 01 11:28:05 CET 2015

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

namespace de.unika.ipd.grGen.Model_ExternalAttributeEvaluation
{

	//
	// Enums
	//

	public enum ENUM_Enu { @reh = 0, @lamm = 1, @hurz = 2, };

	public class Enums
	{
		public static GRGEN_LIBGR.EnumAttributeType @Enu = new GRGEN_LIBGR.EnumAttributeType("Enu", null, "Enu", typeof(GRGEN_MODEL.ENUM_Enu), new GRGEN_LIBGR.EnumMember[] {
			new GRGEN_LIBGR.EnumMember(0, "reh"),
			new GRGEN_LIBGR.EnumMember(1, "lamm"),
			new GRGEN_LIBGR.EnumMember(2, "hurz"),
		});
	}

	//
	// Node types
	//

	public enum NodeTypes { @Node=0, @N=1, @NN=2 };

	// *** Node Node ***


	public sealed partial class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Node[] pool = new GRGEN_MODEL.@Node[10];
		
		static @Node() {
		}
		
		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, container creation of Node
		}

		public static GRGEN_MODEL.NodeType_Node TypeInstance { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Node(this); }

		private @Node(GRGEN_MODEL.@Node oldElem) : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Node)) return false;
			@Node that_ = (@Node)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Node
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Node
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The node type \"Node\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Node\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Node
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Node does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Node does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Node typeVar = new GRGEN_MODEL.NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.@Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Node();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@Node();
		}

	}

	// *** Node N ***

	public interface IN : GRGEN_LIBGR.INode
	{
		int @i { get; set; }
		string @s { get; set; }
		object @o { get; set; }
		bool @b { get; set; }
		float @f { get; set; }
		double @d { get; set; }
		GRGEN_MODEL.ENUM_Enu @enu { get; set; }
		Dictionary<int, GRGEN_LIBGR.SetValueType> @si { get; set; }
		Dictionary<string, object> @mso { get; set; }
		List<double> @a { get; set; }
		GRGEN_LIBGR.Deque<double> @de { get; set; }
		GRGEN_MODEL.Own @ow { get; set; }
		GRGEN_MODEL.OwnPown @op { get; set; }
		GRGEN_MODEL.OwnPownHome @oh { get; set; }
		string fn(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string var_ss);
		GRGEN_MODEL.OwnPownHome fn2(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_MODEL.OwnPownHome var_oo);
		void pc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string var_ss);
		void pc2(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string var_ss, GRGEN_MODEL.OwnPown var_oo, out string _out_param_0, out GRGEN_MODEL.OwnPown _out_param_1);
	}

	public sealed partial class @N : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IN
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@N[] pool = new GRGEN_MODEL.@N[10];
		
		// explicit initializations of N for target N
		// implicit initializations of N for target N
		static @N() {
		}
		
		public @N() : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
			// implicit initialization, container creation of N
			this.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			this.@mso = new Dictionary<string, object>();
			this.@a = new List<double>();
			this.@de = new GRGEN_LIBGR.Deque<double>();
			// explicit initializations of N for target N
			this.@b = true;
		}

		public static GRGEN_MODEL.NodeType_N TypeInstance { get { return GRGEN_MODEL.NodeType_N.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@N(this); }

		private @N(GRGEN_MODEL.@N oldElem) : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
			s_M0no_suXx_h4rD = oldElem.s_M0no_suXx_h4rD;
			AttributeTypeObjectCopierComparer.Copy(oldElem.o_M0no_suXx_h4rD);
			b_M0no_suXx_h4rD = oldElem.b_M0no_suXx_h4rD;
			f_M0no_suXx_h4rD = oldElem.f_M0no_suXx_h4rD;
			d_M0no_suXx_h4rD = oldElem.d_M0no_suXx_h4rD;
			enu_M0no_suXx_h4rD = oldElem.enu_M0no_suXx_h4rD;
			si_M0no_suXx_h4rD = new Dictionary<int, GRGEN_LIBGR.SetValueType>(oldElem.si_M0no_suXx_h4rD);
			mso_M0no_suXx_h4rD = new Dictionary<string, object>(oldElem.mso_M0no_suXx_h4rD);
			a_M0no_suXx_h4rD = new List<double>(oldElem.a_M0no_suXx_h4rD);
			de_M0no_suXx_h4rD = new GRGEN_LIBGR.Deque<double>(oldElem.de_M0no_suXx_h4rD);
			AttributeTypeObjectCopierComparer.Copy(oldElem.ow_M0no_suXx_h4rD);
			AttributeTypeObjectCopierComparer.Copy(oldElem.op_M0no_suXx_h4rD);
			AttributeTypeObjectCopierComparer.Copy(oldElem.oh_M0no_suXx_h4rD);
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @N)) return false;
			@N that_ = (@N)that;
			return true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				&& s_M0no_suXx_h4rD == that_.s_M0no_suXx_h4rD
				&& AttributeTypeObjectCopierComparer.IsEqual(o_M0no_suXx_h4rD, that_.o_M0no_suXx_h4rD)
				&& b_M0no_suXx_h4rD == that_.b_M0no_suXx_h4rD
				&& f_M0no_suXx_h4rD == that_.f_M0no_suXx_h4rD
				&& d_M0no_suXx_h4rD == that_.d_M0no_suXx_h4rD
				&& enu_M0no_suXx_h4rD == that_.enu_M0no_suXx_h4rD
				&& GRGEN_LIBGR.ContainerHelper.Equal(si_M0no_suXx_h4rD, that_.si_M0no_suXx_h4rD)
				&& GRGEN_LIBGR.ContainerHelper.Equal(mso_M0no_suXx_h4rD, that_.mso_M0no_suXx_h4rD)
				&& GRGEN_LIBGR.ContainerHelper.Equal(a_M0no_suXx_h4rD, that_.a_M0no_suXx_h4rD)
				&& GRGEN_LIBGR.ContainerHelper.Equal(de_M0no_suXx_h4rD, that_.de_M0no_suXx_h4rD)
				&& AttributeTypeObjectCopierComparer.IsEqual(ow_M0no_suXx_h4rD, that_.ow_M0no_suXx_h4rD)
				&& AttributeTypeObjectCopierComparer.IsEqual(op_M0no_suXx_h4rD, that_.op_M0no_suXx_h4rD)
				&& AttributeTypeObjectCopierComparer.IsEqual(oh_M0no_suXx_h4rD, that_.oh_M0no_suXx_h4rD)
			;
		}

		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of N
				node.@i = 0;
				node.@s = null;
				node.@o = null;
				node.@b = false;
				node.@f = 0f;
				node.@d = 0;
				node.@enu = 0;
				node.@ow = null;
				node.@op = null;
				node.@oh = null;
				node.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
				node.@mso = new Dictionary<string, object>();
				node.@a = new List<double>();
				node.@de = new GRGEN_LIBGR.Deque<double>();
				// explicit initializations of N for target N
				node.@b = true;
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of N
				node.@i = 0;
				node.@s = null;
				node.@o = null;
				node.@b = false;
				node.@f = 0f;
				node.@d = 0;
				node.@enu = 0;
				node.@ow = null;
				node.@op = null;
				node.@oh = null;
				node.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
				node.@mso = new Dictionary<string, object>();
				node.@a = new List<double>();
				node.@de = new GRGEN_LIBGR.Deque<double>();
				// explicit initializations of N for target N
				node.@b = true;
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int i_M0no_suXx_h4rD;
		public int @i
		{
			get { return i_M0no_suXx_h4rD; }
			set { i_M0no_suXx_h4rD = value; }
		}

		private string s_M0no_suXx_h4rD;
		public string @s
		{
			get { return s_M0no_suXx_h4rD; }
			set { s_M0no_suXx_h4rD = value; }
		}

		private object o_M0no_suXx_h4rD;
		public object @o
		{
			get { return o_M0no_suXx_h4rD; }
			set { o_M0no_suXx_h4rD = value; }
		}

		private bool b_M0no_suXx_h4rD;
		public bool @b
		{
			get { return b_M0no_suXx_h4rD; }
			set { b_M0no_suXx_h4rD = value; }
		}

		private float f_M0no_suXx_h4rD;
		public float @f
		{
			get { return f_M0no_suXx_h4rD; }
			set { f_M0no_suXx_h4rD = value; }
		}

		private double d_M0no_suXx_h4rD;
		public double @d
		{
			get { return d_M0no_suXx_h4rD; }
			set { d_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.ENUM_Enu enu_M0no_suXx_h4rD;
		public GRGEN_MODEL.ENUM_Enu @enu
		{
			get { return enu_M0no_suXx_h4rD; }
			set { enu_M0no_suXx_h4rD = value; }
		}

		private Dictionary<int, GRGEN_LIBGR.SetValueType> si_M0no_suXx_h4rD;
		public Dictionary<int, GRGEN_LIBGR.SetValueType> @si
		{
			get { return si_M0no_suXx_h4rD; }
			set { si_M0no_suXx_h4rD = value; }
		}

		private Dictionary<string, object> mso_M0no_suXx_h4rD;
		public Dictionary<string, object> @mso
		{
			get { return mso_M0no_suXx_h4rD; }
			set { mso_M0no_suXx_h4rD = value; }
		}

		private List<double> a_M0no_suXx_h4rD;
		public List<double> @a
		{
			get { return a_M0no_suXx_h4rD; }
			set { a_M0no_suXx_h4rD = value; }
		}

		private GRGEN_LIBGR.Deque<double> de_M0no_suXx_h4rD;
		public GRGEN_LIBGR.Deque<double> @de
		{
			get { return de_M0no_suXx_h4rD; }
			set { de_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.Own ow_M0no_suXx_h4rD;
		public GRGEN_MODEL.Own @ow
		{
			get { return ow_M0no_suXx_h4rD; }
			set { ow_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.OwnPown op_M0no_suXx_h4rD;
		public GRGEN_MODEL.OwnPown @op
		{
			get { return op_M0no_suXx_h4rD; }
			set { op_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.OwnPownHome oh_M0no_suXx_h4rD;
		public GRGEN_MODEL.OwnPownHome @oh
		{
			get { return oh_M0no_suXx_h4rD; }
			set { oh_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "i": return this.@i;
				case "s": return this.@s;
				case "o": return this.@o;
				case "b": return this.@b;
				case "f": return this.@f;
				case "d": return this.@d;
				case "enu": return this.@enu;
				case "si": return this.@si;
				case "mso": return this.@mso;
				case "a": return this.@a;
				case "de": return this.@de;
				case "ow": return this.@ow;
				case "op": return this.@op;
				case "oh": return this.@oh;
			}
			throw new NullReferenceException(
				"The node type \"N\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "i": this.@i = (int) value; return;
				case "s": this.@s = (string) value; return;
				case "o": this.@o = (object) value; return;
				case "b": this.@b = (bool) value; return;
				case "f": this.@f = (float) value; return;
				case "d": this.@d = (double) value; return;
				case "enu": this.@enu = (GRGEN_MODEL.ENUM_Enu) value; return;
				case "si": this.@si = (Dictionary<int, GRGEN_LIBGR.SetValueType>) value; return;
				case "mso": this.@mso = (Dictionary<string, object>) value; return;
				case "a": this.@a = (List<double>) value; return;
				case "de": this.@de = (GRGEN_LIBGR.Deque<double>) value; return;
				case "ow": this.@ow = (GRGEN_MODEL.Own) value; return;
				case "op": this.@op = (GRGEN_MODEL.OwnPown) value; return;
				case "oh": this.@oh = (GRGEN_MODEL.OwnPownHome) value; return;
			}
			throw new NullReferenceException(
				"The node type \"N\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of N
			this.@i = 0;
			this.@s = null;
			this.@o = null;
			this.@b = false;
			this.@f = 0f;
			this.@d = 0;
			this.@enu = 0;
			this.@ow = null;
			this.@op = null;
			this.@oh = null;
			this.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			this.@mso = new Dictionary<string, object>();
			this.@a = new List<double>();
			this.@de = new GRGEN_LIBGR.Deque<double>();
			// explicit initializations of N for target N
			this.@b = true;
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "fn":
					return @fn(actionEnv, graph, (string)arguments[0]);
				case "fn2":
					return @fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)arguments[0]);
				default: throw new NullReferenceException("N does not have the function method " + name + "!");
			}
		}
		public string fn(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_ss)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			return ((var_ss + this.@s) + ((GRGEN_MODEL.IN) this).@fn(actionEnv, graph, var_ss));
		}
		public GRGEN_MODEL.OwnPownHome fn2(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, GRGEN_MODEL.OwnPownHome var_oo)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			GRGEN_MODEL.OwnPownHome var_o1 = (GRGEN_MODEL.OwnPownHome)(((GRGEN_MODEL.IN) this).@fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)var_oo));
			GRGEN_MODEL.OwnPownHome var_o2 = (GRGEN_MODEL.OwnPownHome)((var_oo).@fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)var_oo));
			bool var_b1 = (bool)(((var_o1).@fn3(actionEnv, graph) == (var_o2).@fn3(actionEnv, graph)));
			bool var_b2 = (bool)((this.@s == ((var_oo).@fn(actionEnv, graph, "foo") + (this.@op).@fn(actionEnv, graph, "42"))));
			return (((var_b1 && var_b2)) ? (var_oo) : ((var_oo).@fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)var_oo)));
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "pc":
				{
					@pc(actionEnv, graph, (string)arguments[0]);
					return ReturnArray_pc_N;
				}
				case "pc2":
				{
					string _out_param_0;
					GRGEN_MODEL.OwnPown _out_param_1;
					@pc2(actionEnv, graph, (string)arguments[0], (GRGEN_MODEL.OwnPown)arguments[1], out _out_param_0, out _out_param_1);
					ReturnArray_pc2_N[0] = _out_param_0;
					ReturnArray_pc2_N[1] = _out_param_1;
					return ReturnArray_pc2_N;
				}
				default: throw new NullReferenceException("N does not have the procedure method " + name + "!");
			}
		}
		private static object[] ReturnArray_pc_N = new object[0]; // helper array for multi-value-returns, to allow for contravariant parameter assignment
		private static object[] ReturnArray_pc2_N = new object[2]; // helper array for multi-value-returns, to allow for contravariant parameter assignment
		public void pc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_ss)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("pc", var_ss);
			string tempvar_0 = (string )var_ss;
			graph.ChangingNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_s, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@s = tempvar_0;
			graph.ChangedNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_s);
			((GRGEN_MODEL.IN) this).@pc(actionEnv, graph, var_ss);
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("pc");
			return;
		}
		public void pc2(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_ss, GRGEN_MODEL.OwnPown var_oo, out string _out_param_0, out GRGEN_MODEL.OwnPown _out_param_1)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("pc2", var_ss, var_oo);
			string outvar_0;
			GRGEN_MODEL.OwnPown outvar_1;
			((GRGEN_MODEL.IN) this).@pc2(actionEnv, graph, var_ss, (GRGEN_MODEL.OwnPown)var_oo, out outvar_0, out outvar_1);
			var_ss = (string) (outvar_0);
			var_oo = (GRGEN_MODEL.OwnPown) (outvar_1);
			var_oo.@pc(actionEnv, graph, null, var_ss);
			_out_param_0 = (var_oo).@fn(actionEnv, graph, var_ss);
			_out_param_1 = var_oo;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("pc2", _out_param_0, _out_param_1);
			return;
		}
	}

	public sealed partial class NodeType_N : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_N typeVar = new GRGEN_MODEL.NodeType_N();
		public static bool[] isA = new bool[] { true, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_i;
		public static GRGEN_LIBGR.AttributeType AttributeType_s;
		public static GRGEN_LIBGR.AttributeType AttributeType_o;
		public static GRGEN_LIBGR.AttributeType AttributeType_b;
		public static GRGEN_LIBGR.AttributeType AttributeType_f;
		public static GRGEN_LIBGR.AttributeType AttributeType_d;
		public static GRGEN_LIBGR.AttributeType AttributeType_enu;
		public static GRGEN_LIBGR.AttributeType AttributeType_si;
		public static GRGEN_LIBGR.AttributeType AttributeType_si_set_member_type;
		public static GRGEN_LIBGR.AttributeType AttributeType_mso;
		public static GRGEN_LIBGR.AttributeType AttributeType_mso_map_domain_type;
		public static GRGEN_LIBGR.AttributeType AttributeType_mso_map_range_type;
		public static GRGEN_LIBGR.AttributeType AttributeType_a;
		public static GRGEN_LIBGR.AttributeType AttributeType_a_array_member_type;
		public static GRGEN_LIBGR.AttributeType AttributeType_de;
		public static GRGEN_LIBGR.AttributeType AttributeType_de_deque_member_type;
		public static GRGEN_LIBGR.AttributeType AttributeType_ow;
		public static GRGEN_LIBGR.AttributeType AttributeType_op;
		public static GRGEN_LIBGR.AttributeType AttributeType_oh;
		public NodeType_N() : base((int) NodeTypes.@N)
		{
			AttributeType_i = new GRGEN_LIBGR.AttributeType("i", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
			AttributeType_s = new GRGEN_LIBGR.AttributeType("s", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_o = new GRGEN_LIBGR.AttributeType("o", this, GRGEN_LIBGR.AttributeKind.ObjectAttr, null, null, null, null, null, null, typeof(object));
			AttributeType_b = new GRGEN_LIBGR.AttributeType("b", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
			AttributeType_f = new GRGEN_LIBGR.AttributeType("f", this, GRGEN_LIBGR.AttributeKind.FloatAttr, null, null, null, null, null, null, typeof(float));
			AttributeType_d = new GRGEN_LIBGR.AttributeType("d", this, GRGEN_LIBGR.AttributeKind.DoubleAttr, null, null, null, null, null, null, typeof(double));
			AttributeType_enu = new GRGEN_LIBGR.AttributeType("enu", this, GRGEN_LIBGR.AttributeKind.EnumAttr, GRGEN_MODEL.Enums.@Enu, null, null, null, null, null, typeof(GRGEN_MODEL.ENUM_Enu));
			AttributeType_si_set_member_type = new GRGEN_LIBGR.AttributeType("si_set_member_type", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
			AttributeType_si = new GRGEN_LIBGR.AttributeType("si", this, GRGEN_LIBGR.AttributeKind.SetAttr, null, AttributeType_si_set_member_type, null, null, null, null, typeof(Dictionary<int, GRGEN_LIBGR.SetValueType>));
			AttributeType_mso_map_domain_type = new GRGEN_LIBGR.AttributeType("mso_map_domain_type", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_mso_map_range_type = new GRGEN_LIBGR.AttributeType("mso_map_range_type", this, GRGEN_LIBGR.AttributeKind.ObjectAttr, null, null, null, null, null, null, typeof(object));
			AttributeType_mso = new GRGEN_LIBGR.AttributeType("mso", this, GRGEN_LIBGR.AttributeKind.MapAttr, null, AttributeType_mso_map_range_type, AttributeType_mso_map_domain_type, null, null, null, typeof(Dictionary<string, object>));
			AttributeType_a_array_member_type = new GRGEN_LIBGR.AttributeType("a_array_member_type", this, GRGEN_LIBGR.AttributeKind.DoubleAttr, null, null, null, null, null, null, typeof(double));
			AttributeType_a = new GRGEN_LIBGR.AttributeType("a", this, GRGEN_LIBGR.AttributeKind.ArrayAttr, null, AttributeType_a_array_member_type, null, null, null, null, typeof(List<double>));
			AttributeType_de_deque_member_type = new GRGEN_LIBGR.AttributeType("de_deque_member_type", this, GRGEN_LIBGR.AttributeKind.DoubleAttr, null, null, null, null, null, null, typeof(double));
			AttributeType_de = new GRGEN_LIBGR.AttributeType("de", this, GRGEN_LIBGR.AttributeKind.DequeAttr, null, AttributeType_de_deque_member_type, null, null, null, null, typeof(GRGEN_LIBGR.Deque<double>));
			AttributeType_ow = new GRGEN_LIBGR.AttributeType("ow", this, GRGEN_LIBGR.AttributeKind.ObjectAttr, null, null, null, null, null, null, typeof(GRGEN_MODEL.Own));
			AttributeType_op = new GRGEN_LIBGR.AttributeType("op", this, GRGEN_LIBGR.AttributeKind.ObjectAttr, null, null, null, null, null, null, typeof(GRGEN_MODEL.OwnPown));
			AttributeType_oh = new GRGEN_LIBGR.AttributeType("oh", this, GRGEN_LIBGR.AttributeKind.ObjectAttr, null, null, null, null, null, null, typeof(GRGEN_MODEL.OwnPownHome));
		}
		public override string Name { get { return "N"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "N"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.IN"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.@N"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@N();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 14; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_i;
				yield return AttributeType_s;
				yield return AttributeType_o;
				yield return AttributeType_b;
				yield return AttributeType_f;
				yield return AttributeType_d;
				yield return AttributeType_enu;
				yield return AttributeType_si;
				yield return AttributeType_mso;
				yield return AttributeType_a;
				yield return AttributeType_de;
				yield return AttributeType_ow;
				yield return AttributeType_op;
				yield return AttributeType_oh;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "i" : return AttributeType_i;
				case "s" : return AttributeType_s;
				case "o" : return AttributeType_o;
				case "b" : return AttributeType_b;
				case "f" : return AttributeType_f;
				case "d" : return AttributeType_d;
				case "enu" : return AttributeType_enu;
				case "si" : return AttributeType_si;
				case "mso" : return AttributeType_mso;
				case "a" : return AttributeType_a;
				case "de" : return AttributeType_de;
				case "ow" : return AttributeType_ow;
				case "op" : return AttributeType_op;
				case "oh" : return AttributeType_oh;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_fn_N.Instance;
				yield return FunctionMethodInfo_fn2_N.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "fn" : return FunctionMethodInfo_fn_N.Instance;
				case "fn2" : return FunctionMethodInfo_fn2_N.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_pc_N.Instance;
				yield return ProcedureMethodInfo_pc2_N.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "pc" : return ProcedureMethodInfo_pc_N.Instance;
				case "pc2" : return ProcedureMethodInfo_pc2_N.Instance;
			}
			return null;
		}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@N newNode = new GRGEN_MODEL.@N();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@N:
				case (int) GRGEN_MODEL.NodeTypes.@NN:
					// copy attributes for: N
					{
						GRGEN_MODEL.IN old = (GRGEN_MODEL.IN) oldNode;
						newNode.@i = old.@i;
						newNode.@s = old.@s;
						newNode.@o = old.@o;
						newNode.@b = old.@b;
						newNode.@f = old.@f;
						newNode.@d = old.@d;
						newNode.@enu = old.@enu;
						newNode.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>(old.@si);
						newNode.@mso = new Dictionary<string, object>(old.@mso);
						newNode.@a = new List<double>(old.@a);
						newNode.@de = new GRGEN_LIBGR.Deque<double>(old.@de);
						newNode.@ow = old.@ow;
						newNode.@op = old.@op;
						newNode.@oh = old.@oh;
					}
					break;
			}
			return newNode;
		}

	}
	public class FunctionMethodInfo_fn_N : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn_N instance = null;
		public static FunctionMethodInfo_fn_N Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn_N(); } return instance; } }

		private FunctionMethodInfo_fn_N()
					: base(
						"fn",
						null, "fn",
						false,
						new String[] { "ss",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(string))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class FunctionMethodInfo_fn2_N : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn2_N instance = null;
		public static FunctionMethodInfo_fn2_N Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn2_N(); } return instance; } }

		private FunctionMethodInfo_fn2_N()
					: base(
						"fn2",
						null, "fn2",
						false,
						new String[] { "oo",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc_N : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc_N instance = null;
		public static ProcedureMethodInfo_pc_N Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc_N(); } return instance; } }

		private ProcedureMethodInfo_pc_N()
					: base(
						"pc",
						null, "pc",
						false,
						new String[] { "ss",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc2_N : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc2_N instance = null;
		public static ProcedureMethodInfo_pc2_N Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc2_N(); } return instance; } }

		private ProcedureMethodInfo_pc2_N()
					: base(
						"pc2",
						null, "pc2",
						false,
						new String[] { "ss", "oo",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public class Comparer_N_i : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_i thisComparer = new Comparer_N_i();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@i.CompareTo(b.@i);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, int entry)
		{
			nodeBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_s : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_s thisComparer = new Comparer_N_s();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, string entry)
		{
			nodeBearingAttributeForSearch.@s = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_o : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_o thisComparer = new Comparer_N_o();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, object entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, object entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, object entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, object entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, object entry)
		{
			nodeBearingAttributeForSearch.@o = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_b : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_b thisComparer = new Comparer_N_b();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@b.CompareTo(b.@b);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, bool entry)
		{
			nodeBearingAttributeForSearch.@b = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_f : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_f thisComparer = new Comparer_N_f();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@f.CompareTo(b.@f);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, float entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, float entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, float entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, float entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, float entry)
		{
			nodeBearingAttributeForSearch.@f = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_d : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_d thisComparer = new Comparer_N_d();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@d.CompareTo(b.@d);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, double entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, double entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, double entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, double entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, double entry)
		{
			nodeBearingAttributeForSearch.@d = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_enu : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_enu thisComparer = new Comparer_N_enu();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@enu.CompareTo(b.@enu);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			nodeBearingAttributeForSearch.@enu = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_ow : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_ow thisComparer = new Comparer_N_ow();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.Own entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.Own entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.Own entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.Own entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, GRGEN_MODEL.Own entry)
		{
			nodeBearingAttributeForSearch.@ow = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_op : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_op thisComparer = new Comparer_N_op();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPown entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPown entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPown entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPown entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPown entry)
		{
			nodeBearingAttributeForSearch.@op = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_N_oh : Comparer<GRGEN_MODEL.IN>
	{
		private static GRGEN_MODEL.IN nodeBearingAttributeForSearch = new GRGEN_MODEL.@N();
		private static Comparer_N_oh thisComparer = new Comparer_N_oh();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPownHome entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPownHome entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPownHome entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPownHome entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IN> list, GRGEN_MODEL.OwnPownHome entry)
		{
			nodeBearingAttributeForSearch.@oh = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node NN ***

	public interface INN : IN
	{
	}

	public sealed partial class @NN : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.INN
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@NN[] pool = new GRGEN_MODEL.@NN[10];
		
		// explicit initializations of N for target NN
		// implicit initializations of N for target NN
		// explicit initializations of NN for target NN
		// implicit initializations of NN for target NN
		static @NN() {
		}
		
		public @NN() : base(GRGEN_MODEL.NodeType_NN.typeVar)
		{
			// implicit initialization, container creation of NN
			this.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			this.@mso = new Dictionary<string, object>();
			this.@a = new List<double>();
			this.@de = new GRGEN_LIBGR.Deque<double>();
			// explicit initializations of N for target NN
			this.@b = true;
			// explicit initializations of NN for target NN
		}

		public static GRGEN_MODEL.NodeType_NN TypeInstance { get { return GRGEN_MODEL.NodeType_NN.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@NN(this); }

		private @NN(GRGEN_MODEL.@NN oldElem) : base(GRGEN_MODEL.NodeType_NN.typeVar)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
			s_M0no_suXx_h4rD = oldElem.s_M0no_suXx_h4rD;
			AttributeTypeObjectCopierComparer.Copy(oldElem.o_M0no_suXx_h4rD);
			b_M0no_suXx_h4rD = oldElem.b_M0no_suXx_h4rD;
			f_M0no_suXx_h4rD = oldElem.f_M0no_suXx_h4rD;
			d_M0no_suXx_h4rD = oldElem.d_M0no_suXx_h4rD;
			enu_M0no_suXx_h4rD = oldElem.enu_M0no_suXx_h4rD;
			si_M0no_suXx_h4rD = new Dictionary<int, GRGEN_LIBGR.SetValueType>(oldElem.si_M0no_suXx_h4rD);
			mso_M0no_suXx_h4rD = new Dictionary<string, object>(oldElem.mso_M0no_suXx_h4rD);
			a_M0no_suXx_h4rD = new List<double>(oldElem.a_M0no_suXx_h4rD);
			de_M0no_suXx_h4rD = new GRGEN_LIBGR.Deque<double>(oldElem.de_M0no_suXx_h4rD);
			AttributeTypeObjectCopierComparer.Copy(oldElem.ow_M0no_suXx_h4rD);
			AttributeTypeObjectCopierComparer.Copy(oldElem.op_M0no_suXx_h4rD);
			AttributeTypeObjectCopierComparer.Copy(oldElem.oh_M0no_suXx_h4rD);
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @NN)) return false;
			@NN that_ = (@NN)that;
			return true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				&& s_M0no_suXx_h4rD == that_.s_M0no_suXx_h4rD
				&& AttributeTypeObjectCopierComparer.IsEqual(o_M0no_suXx_h4rD, that_.o_M0no_suXx_h4rD)
				&& b_M0no_suXx_h4rD == that_.b_M0no_suXx_h4rD
				&& f_M0no_suXx_h4rD == that_.f_M0no_suXx_h4rD
				&& d_M0no_suXx_h4rD == that_.d_M0no_suXx_h4rD
				&& enu_M0no_suXx_h4rD == that_.enu_M0no_suXx_h4rD
				&& GRGEN_LIBGR.ContainerHelper.Equal(si_M0no_suXx_h4rD, that_.si_M0no_suXx_h4rD)
				&& GRGEN_LIBGR.ContainerHelper.Equal(mso_M0no_suXx_h4rD, that_.mso_M0no_suXx_h4rD)
				&& GRGEN_LIBGR.ContainerHelper.Equal(a_M0no_suXx_h4rD, that_.a_M0no_suXx_h4rD)
				&& GRGEN_LIBGR.ContainerHelper.Equal(de_M0no_suXx_h4rD, that_.de_M0no_suXx_h4rD)
				&& AttributeTypeObjectCopierComparer.IsEqual(ow_M0no_suXx_h4rD, that_.ow_M0no_suXx_h4rD)
				&& AttributeTypeObjectCopierComparer.IsEqual(op_M0no_suXx_h4rD, that_.op_M0no_suXx_h4rD)
				&& AttributeTypeObjectCopierComparer.IsEqual(oh_M0no_suXx_h4rD, that_.oh_M0no_suXx_h4rD)
			;
		}

		public static GRGEN_MODEL.@NN CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@NN node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@NN();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of NN
				node.@i = 0;
				node.@s = null;
				node.@o = null;
				node.@b = false;
				node.@f = 0f;
				node.@d = 0;
				node.@enu = 0;
				node.@ow = null;
				node.@op = null;
				node.@oh = null;
				node.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
				node.@mso = new Dictionary<string, object>();
				node.@a = new List<double>();
				node.@de = new GRGEN_LIBGR.Deque<double>();
				// explicit initializations of N for target NN
				node.@b = true;
				// explicit initializations of NN for target NN
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@NN CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@NN node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@NN();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of NN
				node.@i = 0;
				node.@s = null;
				node.@o = null;
				node.@b = false;
				node.@f = 0f;
				node.@d = 0;
				node.@enu = 0;
				node.@ow = null;
				node.@op = null;
				node.@oh = null;
				node.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
				node.@mso = new Dictionary<string, object>();
				node.@a = new List<double>();
				node.@de = new GRGEN_LIBGR.Deque<double>();
				// explicit initializations of N for target NN
				node.@b = true;
				// explicit initializations of NN for target NN
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int i_M0no_suXx_h4rD;
		public int @i
		{
			get { return i_M0no_suXx_h4rD; }
			set { i_M0no_suXx_h4rD = value; }
		}

		private string s_M0no_suXx_h4rD;
		public string @s
		{
			get { return s_M0no_suXx_h4rD; }
			set { s_M0no_suXx_h4rD = value; }
		}

		private object o_M0no_suXx_h4rD;
		public object @o
		{
			get { return o_M0no_suXx_h4rD; }
			set { o_M0no_suXx_h4rD = value; }
		}

		private bool b_M0no_suXx_h4rD;
		public bool @b
		{
			get { return b_M0no_suXx_h4rD; }
			set { b_M0no_suXx_h4rD = value; }
		}

		private float f_M0no_suXx_h4rD;
		public float @f
		{
			get { return f_M0no_suXx_h4rD; }
			set { f_M0no_suXx_h4rD = value; }
		}

		private double d_M0no_suXx_h4rD;
		public double @d
		{
			get { return d_M0no_suXx_h4rD; }
			set { d_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.ENUM_Enu enu_M0no_suXx_h4rD;
		public GRGEN_MODEL.ENUM_Enu @enu
		{
			get { return enu_M0no_suXx_h4rD; }
			set { enu_M0no_suXx_h4rD = value; }
		}

		private Dictionary<int, GRGEN_LIBGR.SetValueType> si_M0no_suXx_h4rD;
		public Dictionary<int, GRGEN_LIBGR.SetValueType> @si
		{
			get { return si_M0no_suXx_h4rD; }
			set { si_M0no_suXx_h4rD = value; }
		}

		private Dictionary<string, object> mso_M0no_suXx_h4rD;
		public Dictionary<string, object> @mso
		{
			get { return mso_M0no_suXx_h4rD; }
			set { mso_M0no_suXx_h4rD = value; }
		}

		private List<double> a_M0no_suXx_h4rD;
		public List<double> @a
		{
			get { return a_M0no_suXx_h4rD; }
			set { a_M0no_suXx_h4rD = value; }
		}

		private GRGEN_LIBGR.Deque<double> de_M0no_suXx_h4rD;
		public GRGEN_LIBGR.Deque<double> @de
		{
			get { return de_M0no_suXx_h4rD; }
			set { de_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.Own ow_M0no_suXx_h4rD;
		public GRGEN_MODEL.Own @ow
		{
			get { return ow_M0no_suXx_h4rD; }
			set { ow_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.OwnPown op_M0no_suXx_h4rD;
		public GRGEN_MODEL.OwnPown @op
		{
			get { return op_M0no_suXx_h4rD; }
			set { op_M0no_suXx_h4rD = value; }
		}

		private GRGEN_MODEL.OwnPownHome oh_M0no_suXx_h4rD;
		public GRGEN_MODEL.OwnPownHome @oh
		{
			get { return oh_M0no_suXx_h4rD; }
			set { oh_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "i": return this.@i;
				case "s": return this.@s;
				case "o": return this.@o;
				case "b": return this.@b;
				case "f": return this.@f;
				case "d": return this.@d;
				case "enu": return this.@enu;
				case "si": return this.@si;
				case "mso": return this.@mso;
				case "a": return this.@a;
				case "de": return this.@de;
				case "ow": return this.@ow;
				case "op": return this.@op;
				case "oh": return this.@oh;
			}
			throw new NullReferenceException(
				"The node type \"NN\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "i": this.@i = (int) value; return;
				case "s": this.@s = (string) value; return;
				case "o": this.@o = (object) value; return;
				case "b": this.@b = (bool) value; return;
				case "f": this.@f = (float) value; return;
				case "d": this.@d = (double) value; return;
				case "enu": this.@enu = (GRGEN_MODEL.ENUM_Enu) value; return;
				case "si": this.@si = (Dictionary<int, GRGEN_LIBGR.SetValueType>) value; return;
				case "mso": this.@mso = (Dictionary<string, object>) value; return;
				case "a": this.@a = (List<double>) value; return;
				case "de": this.@de = (GRGEN_LIBGR.Deque<double>) value; return;
				case "ow": this.@ow = (GRGEN_MODEL.Own) value; return;
				case "op": this.@op = (GRGEN_MODEL.OwnPown) value; return;
				case "oh": this.@oh = (GRGEN_MODEL.OwnPownHome) value; return;
			}
			throw new NullReferenceException(
				"The node type \"NN\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of NN
			this.@i = 0;
			this.@s = null;
			this.@o = null;
			this.@b = false;
			this.@f = 0f;
			this.@d = 0;
			this.@enu = 0;
			this.@ow = null;
			this.@op = null;
			this.@oh = null;
			this.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			this.@mso = new Dictionary<string, object>();
			this.@a = new List<double>();
			this.@de = new GRGEN_LIBGR.Deque<double>();
			// explicit initializations of N for target NN
			this.@b = true;
			// explicit initializations of NN for target NN
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "fn":
					return @fn(actionEnv, graph, (string)arguments[0]);
				case "fn2":
					return @fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)arguments[0]);
				default: throw new NullReferenceException("NN does not have the function method " + name + "!");
			}
		}
		public string fn(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_ss)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			return ((var_ss + this.@s) + ((GRGEN_MODEL.IN) this).@fn(actionEnv, graph, var_ss));
		}
		public GRGEN_MODEL.OwnPownHome fn2(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, GRGEN_MODEL.OwnPownHome var_oo)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			GRGEN_MODEL.OwnPownHome var_o1 = (GRGEN_MODEL.OwnPownHome)(((GRGEN_MODEL.IN) this).@fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)var_oo));
			GRGEN_MODEL.OwnPownHome var_o2 = (GRGEN_MODEL.OwnPownHome)((var_oo).@fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)var_oo));
			bool var_b1 = (bool)(((var_o1).@fn3(actionEnv, graph) == (var_o2).@fn3(actionEnv, graph)));
			bool var_b2 = (bool)((this.@s == ((var_oo).@fn(actionEnv, graph, "foo") + (this.@op).@fn(actionEnv, graph, "42"))));
			return (((var_b1 && var_b2)) ? (var_oo) : ((var_oo).@fn2(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)var_oo)));
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "pc":
				{
					@pc(actionEnv, graph, (string)arguments[0]);
					return ReturnArray_pc_NN;
				}
				case "pc2":
				{
					string _out_param_0;
					GRGEN_MODEL.OwnPown _out_param_1;
					@pc2(actionEnv, graph, (string)arguments[0], (GRGEN_MODEL.OwnPown)arguments[1], out _out_param_0, out _out_param_1);
					ReturnArray_pc2_NN[0] = _out_param_0;
					ReturnArray_pc2_NN[1] = _out_param_1;
					return ReturnArray_pc2_NN;
				}
				default: throw new NullReferenceException("NN does not have the procedure method " + name + "!");
			}
		}
		private static object[] ReturnArray_pc_NN = new object[0]; // helper array for multi-value-returns, to allow for contravariant parameter assignment
		private static object[] ReturnArray_pc2_NN = new object[2]; // helper array for multi-value-returns, to allow for contravariant parameter assignment
		public void pc(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_ss)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("pc", var_ss);
			string tempvar_0 = (string )var_ss;
			graph.ChangingNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_s, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@s = tempvar_0;
			graph.ChangedNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_s);
			((GRGEN_MODEL.IN) this).@pc(actionEnv, graph, var_ss);
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("pc");
			return;
		}
		public void pc2(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_ss, GRGEN_MODEL.OwnPown var_oo, out string _out_param_0, out GRGEN_MODEL.OwnPown _out_param_1)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("pc2", var_ss, var_oo);
			string outvar_0;
			GRGEN_MODEL.OwnPown outvar_1;
			((GRGEN_MODEL.IN) this).@pc2(actionEnv, graph, var_ss, (GRGEN_MODEL.OwnPown)var_oo, out outvar_0, out outvar_1);
			var_ss = (string) (outvar_0);
			var_oo = (GRGEN_MODEL.OwnPown) (outvar_1);
			var_oo.@pc(actionEnv, graph, null, var_ss);
			_out_param_0 = (var_oo).@fn(actionEnv, graph, var_ss);
			_out_param_1 = var_oo;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("pc2", _out_param_0, _out_param_1);
			return;
		}
	}

	public sealed partial class NodeType_NN : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_NN typeVar = new GRGEN_MODEL.NodeType_NN();
		public static bool[] isA = new bool[] { true, true, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_NN() : base((int) NodeTypes.@NN)
		{
		}
		public override string Name { get { return "NN"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "NN"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.INN"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.@NN"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@NN();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 14; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_N.AttributeType_i;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_s;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_o;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_b;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_f;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_d;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_enu;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_si;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_mso;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_a;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_de;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_ow;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_op;
				yield return GRGEN_MODEL.NodeType_N.AttributeType_oh;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "i" : return GRGEN_MODEL.NodeType_N.AttributeType_i;
				case "s" : return GRGEN_MODEL.NodeType_N.AttributeType_s;
				case "o" : return GRGEN_MODEL.NodeType_N.AttributeType_o;
				case "b" : return GRGEN_MODEL.NodeType_N.AttributeType_b;
				case "f" : return GRGEN_MODEL.NodeType_N.AttributeType_f;
				case "d" : return GRGEN_MODEL.NodeType_N.AttributeType_d;
				case "enu" : return GRGEN_MODEL.NodeType_N.AttributeType_enu;
				case "si" : return GRGEN_MODEL.NodeType_N.AttributeType_si;
				case "mso" : return GRGEN_MODEL.NodeType_N.AttributeType_mso;
				case "a" : return GRGEN_MODEL.NodeType_N.AttributeType_a;
				case "de" : return GRGEN_MODEL.NodeType_N.AttributeType_de;
				case "ow" : return GRGEN_MODEL.NodeType_N.AttributeType_ow;
				case "op" : return GRGEN_MODEL.NodeType_N.AttributeType_op;
				case "oh" : return GRGEN_MODEL.NodeType_N.AttributeType_oh;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_fn_NN.Instance;
				yield return FunctionMethodInfo_fn2_NN.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "fn" : return FunctionMethodInfo_fn_NN.Instance;
				case "fn2" : return FunctionMethodInfo_fn2_NN.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_pc_NN.Instance;
				yield return ProcedureMethodInfo_pc2_NN.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "pc" : return ProcedureMethodInfo_pc_NN.Instance;
				case "pc2" : return ProcedureMethodInfo_pc2_NN.Instance;
			}
			return null;
		}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@NN newNode = new GRGEN_MODEL.@NN();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@N:
					// copy attributes for: N
					{
						GRGEN_MODEL.IN old = (GRGEN_MODEL.IN) oldNode;
						newNode.@i = old.@i;
						newNode.@s = old.@s;
						newNode.@o = old.@o;
						newNode.@b = old.@b;
						newNode.@f = old.@f;
						newNode.@d = old.@d;
						newNode.@enu = old.@enu;
						newNode.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>(old.@si);
						newNode.@mso = new Dictionary<string, object>(old.@mso);
						newNode.@a = new List<double>(old.@a);
						newNode.@de = new GRGEN_LIBGR.Deque<double>(old.@de);
						newNode.@ow = old.@ow;
						newNode.@op = old.@op;
						newNode.@oh = old.@oh;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@NN:
					// copy attributes for: NN
					{
						GRGEN_MODEL.INN old = (GRGEN_MODEL.INN) oldNode;
						newNode.@i = old.@i;
						newNode.@s = old.@s;
						newNode.@o = old.@o;
						newNode.@b = old.@b;
						newNode.@f = old.@f;
						newNode.@d = old.@d;
						newNode.@enu = old.@enu;
						newNode.@si = new Dictionary<int, GRGEN_LIBGR.SetValueType>(old.@si);
						newNode.@mso = new Dictionary<string, object>(old.@mso);
						newNode.@a = new List<double>(old.@a);
						newNode.@de = new GRGEN_LIBGR.Deque<double>(old.@de);
						newNode.@ow = old.@ow;
						newNode.@op = old.@op;
						newNode.@oh = old.@oh;
					}
					break;
			}
			return newNode;
		}

	}
	public class FunctionMethodInfo_fn_NN : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn_NN instance = null;
		public static FunctionMethodInfo_fn_NN Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn_NN(); } return instance; } }

		private FunctionMethodInfo_fn_NN()
					: base(
						"fn",
						null, "fn",
						false,
						new String[] { "ss",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(string))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class FunctionMethodInfo_fn2_NN : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn2_NN instance = null;
		public static FunctionMethodInfo_fn2_NN Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn2_NN(); } return instance; } }

		private FunctionMethodInfo_fn2_NN()
					: base(
						"fn2",
						null, "fn2",
						false,
						new String[] { "oo",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc_NN : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc_NN instance = null;
		public static ProcedureMethodInfo_pc_NN Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc_NN(); } return instance; } }

		private ProcedureMethodInfo_pc_NN()
					: base(
						"pc",
						null, "pc",
						false,
						new String[] { "ss",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc2_NN : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc2_NN instance = null;
		public static ProcedureMethodInfo_pc2_NN Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc2_NN(); } return instance; } }

		private ProcedureMethodInfo_pc2_NN()
					: base(
						"pc2",
						null, "pc2",
						false,
						new String[] { "ss", "oo",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public class Comparer_NN_i : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_i thisComparer = new Comparer_NN_i();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return a.@i.CompareTo(b.@i);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, int entry)
		{
			nodeBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_s : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_s thisComparer = new Comparer_NN_s();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, string entry)
		{
			nodeBearingAttributeForSearch.@s = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_o : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_o thisComparer = new Comparer_NN_o();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, object entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, object entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, object entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, object entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@o.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, object entry)
		{
			nodeBearingAttributeForSearch.@o = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_b : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_b thisComparer = new Comparer_NN_b();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return a.@b.CompareTo(b.@b);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, bool entry)
		{
			nodeBearingAttributeForSearch.@b = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_f : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_f thisComparer = new Comparer_NN_f();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return a.@f.CompareTo(b.@f);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, float entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, float entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, float entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, float entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, float entry)
		{
			nodeBearingAttributeForSearch.@f = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_d : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_d thisComparer = new Comparer_NN_d();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return a.@d.CompareTo(b.@d);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, double entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, double entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, double entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, double entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, double entry)
		{
			nodeBearingAttributeForSearch.@d = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_enu : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_enu thisComparer = new Comparer_NN_enu();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return a.@enu.CompareTo(b.@enu);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.ENUM_Enu entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.ENUM_Enu entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			nodeBearingAttributeForSearch.@enu = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_ow : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_ow thisComparer = new Comparer_NN_ow();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.Own entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.Own entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.Own entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.Own entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@ow.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, GRGEN_MODEL.Own entry)
		{
			nodeBearingAttributeForSearch.@ow = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_op : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_op thisComparer = new Comparer_NN_op();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPown entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPown entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPown entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPown entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@op.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPown entry)
		{
			nodeBearingAttributeForSearch.@op = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_NN_oh : Comparer<GRGEN_MODEL.INN>
	{
		private static GRGEN_MODEL.INN nodeBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		private static Comparer_NN_oh thisComparer = new Comparer_NN_oh();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;
			if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;
			return 1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPownHome entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPownHome entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPownHome entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPownHome entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@oh.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.INN> list, GRGEN_MODEL.OwnPownHome entry)
		{
			nodeBearingAttributeForSearch.@oh = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2, @E=3 };

	// *** Edge AEdge ***


	public sealed partial class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override string Name { get { return "AEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "AEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return null; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Arbitrary; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge does not support source and target setting!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			throw new Exception("Cannot retype to the abstract type AEdge!");
		}
	}

	// *** Edge Edge ***


	public sealed partial class @Edge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Edge[] pool = new GRGEN_MODEL.@Edge[10];
		
		static @Edge() {
		}
		
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, container creation of Edge
		}

		public static GRGEN_MODEL.EdgeType_Edge TypeInstance { get { return GRGEN_MODEL.EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @Edge(GRGEN_MODEL.@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Edge)) return false;
			@Edge that_ = (@Edge)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of Edge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of Edge
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Edge
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Edge does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Edge does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_Edge typeVar = new GRGEN_MODEL.EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.@Edge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed partial class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@UEdge[] pool = new GRGEN_MODEL.@UEdge[10];
		
		static @UEdge() {
		}
		
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, container creation of UEdge
		}

		public static GRGEN_MODEL.EdgeType_UEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @UEdge(GRGEN_MODEL.@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @UEdge)) return false;
			@UEdge that_ = (@UEdge)that;
			return true
			;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of UEdge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of UEdge
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of UEdge
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("UEdge does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("UEdge does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_UEdge typeVar = new GRGEN_MODEL.EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.@UEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Undirected; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge E ***

	public interface IE : GRGEN_LIBGR.IEdge
	{
	}

	public sealed partial class @E : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IE
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@E[] pool = new GRGEN_MODEL.@E[10];
		
		// explicit initializations of E for target E
		// implicit initializations of E for target E
		static @E() {
		}
		
		public @E(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_E.typeVar, source, target)
		{
			// implicit initialization, container creation of E
			// explicit initializations of E for target E
		}

		public static GRGEN_MODEL.EdgeType_E TypeInstance { get { return GRGEN_MODEL.EdgeType_E.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@E(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @E(GRGEN_MODEL.@E oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_E.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @E)) return false;
			@E that_ = (@E)that;
			return true
			;
		}

		public static GRGEN_MODEL.@E CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@E edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@E(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of E
				// explicit initializations of E for target E
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@E CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@E edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@E(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of E
				// explicit initializations of E for target E
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"E\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"E\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of E
			// explicit initializations of E for target E
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("E does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("E does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_E : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_E typeVar = new GRGEN_MODEL.EdgeType_E();
		public static bool[] isA = new bool[] { true, true, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_E() : base((int) EdgeTypes.@E)
		{
		}
		public override string Name { get { return "E"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "E"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.IE"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ExternalAttributeEvaluation.@E"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@E((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@E((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	public sealed class ExternalType_object : GRGEN_LIBGR.ExternalType
	{
		public ExternalType_object()
			: base("object", typeof(object))
		{
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
	}

	public sealed class ExternalType_Own : GRGEN_LIBGR.ExternalType
	{
		public ExternalType_Own()
			: base("Own", typeof(Own))
		{
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
	}

	public sealed class ExternalType_OwnPown : GRGEN_LIBGR.ExternalType
	{
		public ExternalType_OwnPown()
			: base("OwnPown", typeof(OwnPown))
		{
		}
		public override int NumFunctionMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_fn_OwnPown.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "fn" : return FunctionMethodInfo_fn_OwnPown.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_pc_OwnPown.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "pc" : return ProcedureMethodInfo_pc_OwnPown.Instance;
			}
			return null;
		}
	}
	public class FunctionMethodInfo_fn_OwnPown : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn_OwnPown instance = null;
		public static FunctionMethodInfo_fn_OwnPown Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn_OwnPown(); } return instance; } }

		private FunctionMethodInfo_fn_OwnPown()
					: base(
						"fn",
						null, "fn",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(string))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc_OwnPown : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc_OwnPown instance = null;
		public static ProcedureMethodInfo_pc_OwnPown Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc_OwnPown(); } return instance; } }

		private ProcedureMethodInfo_pc_OwnPown()
					: base(
						"pc",
						null, "pc",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public sealed class ExternalType_OwnPownHome : GRGEN_LIBGR.ExternalType
	{
		public ExternalType_OwnPownHome()
			: base("OwnPownHome", typeof(OwnPownHome))
		{
		}
		public override int NumFunctionMethods { get { return 3; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_fn_OwnPownHome.Instance;
				yield return FunctionMethodInfo_fn2_OwnPownHome.Instance;
				yield return FunctionMethodInfo_fn3_OwnPownHome.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "fn" : return FunctionMethodInfo_fn_OwnPownHome.Instance;
				case "fn2" : return FunctionMethodInfo_fn2_OwnPownHome.Instance;
				case "fn3" : return FunctionMethodInfo_fn3_OwnPownHome.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_pc_OwnPownHome.Instance;
				yield return ProcedureMethodInfo_pc2_OwnPownHome.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "pc" : return ProcedureMethodInfo_pc_OwnPownHome.Instance;
				case "pc2" : return ProcedureMethodInfo_pc2_OwnPownHome.Instance;
			}
			return null;
		}
	}
	public class FunctionMethodInfo_fn_OwnPownHome : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn_OwnPownHome instance = null;
		public static FunctionMethodInfo_fn_OwnPownHome Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn_OwnPownHome(); } return instance; } }

		private FunctionMethodInfo_fn_OwnPownHome()
					: base(
						"fn",
						null, "fn",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(string))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class FunctionMethodInfo_fn2_OwnPownHome : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn2_OwnPownHome instance = null;
		public static FunctionMethodInfo_fn2_OwnPownHome Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn2_OwnPownHome(); } return instance; } }

		private FunctionMethodInfo_fn2_OwnPownHome()
					: base(
						"fn2",
						null, "fn2",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class FunctionMethodInfo_fn3_OwnPownHome : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_fn3_OwnPownHome instance = null;
		public static FunctionMethodInfo_fn3_OwnPownHome Instance { get { if (instance==null) { instance = new FunctionMethodInfo_fn3_OwnPownHome(); } return instance; } }

		private FunctionMethodInfo_fn3_OwnPownHome()
					: base(
						"fn3",
						null, "fn3",
						true,
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(string))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc_OwnPownHome : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc_OwnPownHome instance = null;
		public static ProcedureMethodInfo_pc_OwnPownHome Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc_OwnPownHome(); } return instance; } }

		private ProcedureMethodInfo_pc_OwnPownHome()
					: base(
						"pc",
						null, "pc",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}

	public class ProcedureMethodInfo_pc2_OwnPownHome : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_pc2_OwnPownHome instance = null;
		public static ProcedureMethodInfo_pc2_OwnPownHome Instance { get { if (instance==null) { instance = new ProcedureMethodInfo_pc2_OwnPownHome(); } return instance; } }

		private ProcedureMethodInfo_pc2_OwnPownHome()
					: base(
						"pc2",
						null, "pc2",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.Own)),  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.Own)),  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	//
	// Indices
	//

	public class ExternalAttributeEvaluationIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public ExternalAttributeEvaluationIndexSet(GRGEN_LGSP.LGSPGraph graph)
		{
		}


		public GRGEN_LIBGR.IIndex GetIndex(string indexName)
		{
			switch(indexName)
			{
				default: return null;
			}
		}

		public void FillAsClone(GRGEN_LGSP.LGSPGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
		{
		}
	}

	//
	// Node model
	//

	public sealed class ExternalAttributeEvaluationNodeModel : GRGEN_LIBGR.INodeModel
	{
		public ExternalAttributeEvaluationNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_N.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_NN.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_NN.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_NN.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_NN.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
			};
			GRGEN_MODEL.NodeType_NN.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(string name)
		{
			switch(name)
			{
				case "Node" : return GRGEN_MODEL.NodeType_Node.typeVar;
				case "N" : return GRGEN_MODEL.NodeType_N.typeVar;
				case "NN" : return GRGEN_MODEL.NodeType_NN.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_N.typeVar,
			GRGEN_MODEL.NodeType_NN.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_N),
			typeof(GRGEN_MODEL.NodeType_NN),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_N.AttributeType_i,
			GRGEN_MODEL.NodeType_N.AttributeType_s,
			GRGEN_MODEL.NodeType_N.AttributeType_o,
			GRGEN_MODEL.NodeType_N.AttributeType_b,
			GRGEN_MODEL.NodeType_N.AttributeType_f,
			GRGEN_MODEL.NodeType_N.AttributeType_d,
			GRGEN_MODEL.NodeType_N.AttributeType_enu,
			GRGEN_MODEL.NodeType_N.AttributeType_si,
			GRGEN_MODEL.NodeType_N.AttributeType_mso,
			GRGEN_MODEL.NodeType_N.AttributeType_a,
			GRGEN_MODEL.NodeType_N.AttributeType_de,
			GRGEN_MODEL.NodeType_N.AttributeType_ow,
			GRGEN_MODEL.NodeType_N.AttributeType_op,
			GRGEN_MODEL.NodeType_N.AttributeType_oh,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class ExternalAttributeEvaluationEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public ExternalAttributeEvaluationEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_E.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_E.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_E.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
			};
			GRGEN_MODEL.EdgeType_E.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_E.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_E.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public GRGEN_LIBGR.EdgeType RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		public GRGEN_LIBGR.EdgeType GetType(string name)
		{
			switch(name)
			{
				case "AEdge" : return GRGEN_MODEL.EdgeType_AEdge.typeVar;
				case "Edge" : return GRGEN_MODEL.EdgeType_Edge.typeVar;
				case "UEdge" : return GRGEN_MODEL.EdgeType_UEdge.typeVar;
				case "E" : return GRGEN_MODEL.EdgeType_E.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			GRGEN_MODEL.EdgeType_AEdge.typeVar,
			GRGEN_MODEL.EdgeType_Edge.typeVar,
			GRGEN_MODEL.EdgeType_UEdge.typeVar,
			GRGEN_MODEL.EdgeType_E.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_E),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel (LGSPGraphModel) implementation
	//
	public sealed class ExternalAttributeEvaluationGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public ExternalAttributeEvaluationGraphModel()
		{
			FullyInitializeExternalTypes();
		}

		private ExternalAttributeEvaluationNodeModel nodeModel = new ExternalAttributeEvaluationNodeModel();
		private ExternalAttributeEvaluationEdgeModel edgeModel = new ExternalAttributeEvaluationEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
			GRGEN_MODEL.Enums.@Enu,
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new ExternalAttributeEvaluationIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ExternalAttributeEvaluationIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "ExternalAttributeEvaluation"; } }
		public override GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public override GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public override IEnumerable<string> Packages { get { return packages; } }
		public override IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public override IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public override IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions { get { return indexDescriptions; } }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) { return indexDescriptions[i]; }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)
 		{
			for(int i=0; i<indexDescriptions.Length; ++i)
				if(indexDescriptions[i].Name==indexName)
					return indexDescriptions[i];
			return null;
		}
		public override bool GraphElementUniquenessIsEnsured { get { return false; } }
		public override bool GraphElementsAreAccessibleByUniqueId { get { return false; } }
		public override int BranchingFactorForEqualsAny { get { return 0; } }

		public override object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);
		}
		public override string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);
		}
		public override string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);
		}
		public override void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			AttributeTypeObjectEmitterParser.External(line, graph);
		}
		public override GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.AsGraph(attribute, attrType, graph);
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		public static GRGEN_LIBGR.ExternalType externalType_Own = new ExternalType_Own();
		public static GRGEN_LIBGR.ExternalType externalType_OwnPown = new ExternalType_OwnPown();
		public static GRGEN_LIBGR.ExternalType externalType_OwnPownHome = new ExternalType_OwnPownHome();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object, externalType_Own, externalType_OwnPown, externalType_OwnPownHome };
		public override GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
			externalType_Own.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_object } );
			externalType_OwnPown.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_Own, } );
			externalType_OwnPownHome.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_OwnPown, } );
		}

		public override bool IsEqualClassDefined { get { return true; } }
		public override bool IsLowerClassDefined { get { return true; } }
		public override bool IsEqual(object this_, object that)
		{
			return AttributeTypeObjectCopierComparer.IsEqual(this_, that);
		}
		public override bool IsLower(object this_, object that)
		{
			return AttributeTypeObjectCopierComparer.IsLower(this_, that);
		}

		public override string MD5Hash { get { return "c31b4a83d7adddb9205f28026a0414cd"; } }
	}

	//
	// IGraph (LGSPGraph) / IGraphModel implementation
	//
	public class ExternalAttributeEvaluationGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public ExternalAttributeEvaluationGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@N CreateNodeN()
		{
			return GRGEN_MODEL.@N.CreateNode(this);
		}

		public GRGEN_MODEL.@NN CreateNodeNN()
		{
			return GRGEN_MODEL.@NN.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@E CreateEdgeE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@E.CreateEdge(this, source, target);
		}

		private ExternalAttributeEvaluationNodeModel nodeModel = new ExternalAttributeEvaluationNodeModel();
		private ExternalAttributeEvaluationEdgeModel edgeModel = new ExternalAttributeEvaluationEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
			GRGEN_MODEL.Enums.@Enu,
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new ExternalAttributeEvaluationIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ExternalAttributeEvaluationIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "ExternalAttributeEvaluation"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<string> Packages { get { return packages; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions { get { return indexDescriptions; } }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) { return indexDescriptions[i]; }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)
 		{
			for(int i=0; i<indexDescriptions.Length; ++i)
				if(indexDescriptions[i].Name==indexName)
					return indexDescriptions[i];
			return null;
		}
		public bool GraphElementUniquenessIsEnsured { get { return false; } }
		public bool GraphElementsAreAccessibleByUniqueId { get { return false; } }
		public int BranchingFactorForEqualsAny { get { return 0; } }

		public object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);
		}
		public string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);
		}
		public string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);
		}
		public void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			AttributeTypeObjectEmitterParser.External(line, graph);
		}
		public GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.AsGraph(attribute, attrType, graph);
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		public static GRGEN_LIBGR.ExternalType externalType_Own = new ExternalType_Own();
		public static GRGEN_LIBGR.ExternalType externalType_OwnPown = new ExternalType_OwnPown();
		public static GRGEN_LIBGR.ExternalType externalType_OwnPownHome = new ExternalType_OwnPownHome();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object, externalType_Own, externalType_OwnPown, externalType_OwnPownHome };
		public GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
			externalType_Own.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_object } );
			externalType_OwnPown.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_Own, } );
			externalType_OwnPownHome.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_OwnPown, } );
		}

		public bool IsEqualClassDefined { get { return true; } }
		public bool IsLowerClassDefined { get { return true; } }
		public bool IsEqual(object this_, object that)
		{
			return AttributeTypeObjectCopierComparer.IsEqual(this_, that);
		}
		public bool IsLower(object this_, object that)
		{
			return AttributeTypeObjectCopierComparer.IsLower(this_, that);
		}

		public string MD5Hash { get { return "c31b4a83d7adddb9205f28026a0414cd"; } }
	}

	//
	// INamedGraph (LGSPNamedGraph) / IGraphModel implementation
	//
	public class ExternalAttributeEvaluationNamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel
	{
		public ExternalAttributeEvaluationNamedGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode(string nodeName)
		{
			return GRGEN_MODEL.@Node.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@N CreateNodeN()
		{
			return GRGEN_MODEL.@N.CreateNode(this);
		}

		public GRGEN_MODEL.@N CreateNodeN(string nodeName)
		{
			return GRGEN_MODEL.@N.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@NN CreateNodeNN()
		{
			return GRGEN_MODEL.@NN.CreateNode(this);
		}

		public GRGEN_MODEL.@NN CreateNodeNN(string nodeName)
		{
			return GRGEN_MODEL.@NN.CreateNode(this, nodeName);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@E CreateEdgeE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@E.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@E CreateEdgeE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@E.CreateEdge(this, source, target, edgeName);
		}

		private ExternalAttributeEvaluationNodeModel nodeModel = new ExternalAttributeEvaluationNodeModel();
		private ExternalAttributeEvaluationEdgeModel edgeModel = new ExternalAttributeEvaluationEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
			GRGEN_MODEL.Enums.@Enu,
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new ExternalAttributeEvaluationIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ExternalAttributeEvaluationIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "ExternalAttributeEvaluation"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<string> Packages { get { return packages; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions { get { return indexDescriptions; } }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) { return indexDescriptions[i]; }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)
 		{
			for(int i=0; i<indexDescriptions.Length; ++i)
				if(indexDescriptions[i].Name==indexName)
					return indexDescriptions[i];
			return null;
		}
		public bool GraphElementUniquenessIsEnsured { get { return false; } }
		public bool GraphElementsAreAccessibleByUniqueId { get { return false; } }
		public int BranchingFactorForEqualsAny { get { return 0; } }

		public object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);
		}
		public string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);
		}
		public string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);
		}
		public void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			AttributeTypeObjectEmitterParser.External(line, graph);
		}
		public GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return AttributeTypeObjectEmitterParser.AsGraph(attribute, attrType, graph);
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		public static GRGEN_LIBGR.ExternalType externalType_Own = new ExternalType_Own();
		public static GRGEN_LIBGR.ExternalType externalType_OwnPown = new ExternalType_OwnPown();
		public static GRGEN_LIBGR.ExternalType externalType_OwnPownHome = new ExternalType_OwnPownHome();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object, externalType_Own, externalType_OwnPown, externalType_OwnPownHome };
		public GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
			externalType_Own.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_object } );
			externalType_OwnPown.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_Own, } );
			externalType_OwnPownHome.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { externalType_OwnPown, } );
		}

		public bool IsEqualClassDefined { get { return true; } }
		public bool IsLowerClassDefined { get { return true; } }
		public bool IsEqual(object this_, object that)
		{
			return AttributeTypeObjectCopierComparer.IsEqual(this_, that);
		}
		public bool IsLower(object this_, object that)
		{
			return AttributeTypeObjectCopierComparer.IsLower(this_, that);
		}

		public string MD5Hash { get { return "c31b4a83d7adddb9205f28026a0414cd"; } }
	}
}
