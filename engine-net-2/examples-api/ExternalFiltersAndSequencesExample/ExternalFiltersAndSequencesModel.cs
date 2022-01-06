// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ExternalFiltersAndSequencesExample\ExternalFiltersAndSequences.grg" on Thu Jan 06 09:31:50 CET 2022

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Diagnostics;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalFiltersAndSequences;

namespace de.unika.ipd.grGen.Model_ExternalFiltersAndSequences
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

	public enum NodeTypes { @Node=0, @N=1, @M=2 };

	// *** Node Node ***


	public sealed partial class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@Node[] pool;

		static @Node() {
		}

		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, container creation of Node
		}

		public static GRGEN_MODEL.NodeType_Node TypeInstance { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@Node(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@Node(this, graph, oldToNewObjectMap);
		}

		private @Node(GRGEN_MODEL.@Node oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @Node))
				return false;
			@Node that_ = (@Node)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@Node[GRGEN_LGSP.LGSPGraph.poolSize];
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
				if(pool == null)
					pool = new GRGEN_MODEL.@Node[GRGEN_LGSP.LGSPGraph.poolSize];
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
			if(pool == null)
				pool = new GRGEN_MODEL.@Node[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Node type \"Node\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Node type \"Node\" does not have the attribute \"" + attrName + "\"!");
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
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Node();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
	}

	public sealed partial class @N : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IN
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@N[] pool;

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

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@N(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@N(this, graph, oldToNewObjectMap);
		}

		private @N(GRGEN_MODEL.@N oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
			s_M0no_suXx_h4rD = oldElem.s_M0no_suXx_h4rD;
			o_M0no_suXx_h4rD = oldElem.o_M0no_suXx_h4rD;
			b_M0no_suXx_h4rD = oldElem.b_M0no_suXx_h4rD;
			f_M0no_suXx_h4rD = oldElem.f_M0no_suXx_h4rD;
			d_M0no_suXx_h4rD = oldElem.d_M0no_suXx_h4rD;
			enu_M0no_suXx_h4rD = oldElem.enu_M0no_suXx_h4rD;
			si_M0no_suXx_h4rD = new Dictionary<int, GRGEN_LIBGR.SetValueType>(oldElem.si_M0no_suXx_h4rD);
			mso_M0no_suXx_h4rD = new Dictionary<string, object>(oldElem.mso_M0no_suXx_h4rD);
			a_M0no_suXx_h4rD = new List<double>(oldElem.a_M0no_suXx_h4rD);
			de_M0no_suXx_h4rD = new GRGEN_LIBGR.Deque<double>(oldElem.de_M0no_suXx_h4rD);
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @N))
				return false;
			@N that_ = (@N)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				&& s_M0no_suXx_h4rD == that_.s_M0no_suXx_h4rD
				&& o_M0no_suXx_h4rD == that_.o_M0no_suXx_h4rD
				&& b_M0no_suXx_h4rD == that_.b_M0no_suXx_h4rD
				&& f_M0no_suXx_h4rD == that_.f_M0no_suXx_h4rD
				&& d_M0no_suXx_h4rD == that_.d_M0no_suXx_h4rD
				&& enu_M0no_suXx_h4rD == that_.enu_M0no_suXx_h4rD
				&& GRGEN_LIBGR.ContainerHelper.DeeplyEqual(si_M0no_suXx_h4rD, that_.si_M0no_suXx_h4rD, visitedObjects)
				&& GRGEN_LIBGR.ContainerHelper.DeeplyEqual(mso_M0no_suXx_h4rD, that_.mso_M0no_suXx_h4rD, visitedObjects)
				&& GRGEN_LIBGR.ContainerHelper.DeeplyEqual(a_M0no_suXx_h4rD, that_.a_M0no_suXx_h4rD, visitedObjects)
				&& GRGEN_LIBGR.ContainerHelper.DeeplyEqual(de_M0no_suXx_h4rD, that_.de_M0no_suXx_h4rD, visitedObjects)
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@N[GRGEN_LGSP.LGSPGraph.poolSize];
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
				if(pool == null)
					pool = new GRGEN_MODEL.@N[GRGEN_LGSP.LGSPGraph.poolSize];
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
			if(pool == null)
				pool = new GRGEN_MODEL.@N[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
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
			}
			throw new NullReferenceException(
				"The Node type \"N\" does not have the attribute \"" + attrName + "\"!");
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
			}
			throw new NullReferenceException(
				"The Node type \"N\" does not have the attribute \"" + attrName + "\"!");
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
				default: throw new NullReferenceException("N does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("N does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_N : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_N typeVar = new GRGEN_MODEL.NodeType_N();
		public static bool[] isA = new bool[] { true, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, };
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
		}
		public override string Name { get { return "N"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "N"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.IN"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@N"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@N();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 11; } }
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
			}
			return null;
		}
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
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@N newNode = new GRGEN_MODEL.@N();
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
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_N_i : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_i thisComparer = new Comparer_N_i();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ReverseComparer_N_i : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_i thisComparer = new ReverseComparer_N_i();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ArrayHelper_N_i
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, int entry)
		{
			instanceBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_i.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@i)) {
					seenValues[list[pos].@i].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@i, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@i)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@i, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@i);
			return resultList;
		}
	}


	public class Comparer_N_s : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_s thisComparer = new Comparer_N_s();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
	}

	public class ReverseComparer_N_s : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_s thisComparer = new ReverseComparer_N_s();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
	}

	public class ArrayHelper_N_s
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, string entry)
		{
			instanceBearingAttributeForSearch.@s = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_s.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_s.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_s.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<string, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<string, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@s)) {
					seenValues[list[pos].@s].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@s, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<string, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<string, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@s, null);
				}
			}
			return newList;
		}
		public static List<string> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<string> resultList = new List<string>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@s);
			return resultList;
		}
	}


	public class Comparer_N_b : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_b thisComparer = new Comparer_N_b();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@b.CompareTo(b.@b);
		}
	}

	public class ReverseComparer_N_b : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_b thisComparer = new ReverseComparer_N_b();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return a.@b.CompareTo(b.@b);
		}
	}

	public class ArrayHelper_N_b
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, bool entry)
		{
			instanceBearingAttributeForSearch.@b = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_b.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_b.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_b.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<bool, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<bool, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b)) {
					seenValues[list[pos].@b].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<bool, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<bool, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b, null);
				}
			}
			return newList;
		}
		public static List<bool> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<bool> resultList = new List<bool>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@b);
			return resultList;
		}
	}


	public class Comparer_N_f : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_f thisComparer = new Comparer_N_f();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@f.CompareTo(b.@f);
		}
	}

	public class ReverseComparer_N_f : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_f thisComparer = new ReverseComparer_N_f();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return a.@f.CompareTo(b.@f);
		}
	}

	public class ArrayHelper_N_f
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, float entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, float entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, float entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, float entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@f.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, float entry)
		{
			instanceBearingAttributeForSearch.@f = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_f.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_f.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_f.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<float, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<float, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@f)) {
					seenValues[list[pos].@f].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@f, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<float, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<float, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@f)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@f, null);
				}
			}
			return newList;
		}
		public static List<float> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<float> resultList = new List<float>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@f);
			return resultList;
		}
	}


	public class Comparer_N_d : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_d thisComparer = new Comparer_N_d();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@d.CompareTo(b.@d);
		}
	}

	public class ReverseComparer_N_d : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_d thisComparer = new ReverseComparer_N_d();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return a.@d.CompareTo(b.@d);
		}
	}

	public class ArrayHelper_N_d
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, double entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, double entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, double entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, double entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, double entry)
		{
			instanceBearingAttributeForSearch.@d = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_d.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_d.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_d.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<double, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<double, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@d)) {
					seenValues[list[pos].@d].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@d, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<double, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<double, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@d)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@d, null);
				}
			}
			return newList;
		}
		public static List<double> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<double> resultList = new List<double>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@d);
			return resultList;
		}
	}


	public class Comparer_N_enu : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_enu thisComparer = new Comparer_N_enu();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@enu.CompareTo(b.@enu);
		}
	}

	public class ReverseComparer_N_enu : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_enu thisComparer = new ReverseComparer_N_enu();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return a.@enu.CompareTo(b.@enu);
		}
	}

	public class ArrayHelper_N_enu
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@enu.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, GRGEN_MODEL.ENUM_Enu entry)
		{
			instanceBearingAttributeForSearch.@enu = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_enu.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_enu.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_enu.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<GRGEN_MODEL.ENUM_Enu, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<GRGEN_MODEL.ENUM_Enu, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@enu)) {
					seenValues[list[pos].@enu].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@enu, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<GRGEN_MODEL.ENUM_Enu, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.ENUM_Enu, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@enu)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@enu, null);
				}
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ENUM_Enu> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.ENUM_Enu> resultList = new List<GRGEN_MODEL.ENUM_Enu>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@enu);
			return resultList;
		}
	}


	// *** Node M ***

	public interface IM : GRGEN_LIBGR.INode
	{
	}

	public sealed partial class @M : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IM
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@M[] pool;

		// explicit initializations of M for target M
		// implicit initializations of M for target M
		static @M() {
		}

		public @M() : base(GRGEN_MODEL.NodeType_M.typeVar)
		{
			// implicit initialization, container creation of M
			// explicit initializations of M for target M
		}

		public static GRGEN_MODEL.NodeType_M TypeInstance { get { return GRGEN_MODEL.NodeType_M.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@M(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@M(this, graph, oldToNewObjectMap);
		}

		private @M(GRGEN_MODEL.@M oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_M.typeVar)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @M))
				return false;
			@M that_ = (@M)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@M CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@M node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@M();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@M[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of M
				// explicit initializations of M for target M
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@M CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@M node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@M();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@M[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of M
				// explicit initializations of M for target M
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@M[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Node type \"M\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Node type \"M\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of M
			// explicit initializations of M for target M
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("M does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("M does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_M : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_M typeVar = new GRGEN_MODEL.NodeType_M();
		public static bool[] isA = new bool[] { true, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_M() : base((int) NodeTypes.@M)
		{
		}
		public override string Name { get { return "M"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "M"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.IM"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@M"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@M();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			return new GRGEN_MODEL.@M();
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
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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


	public sealed partial class @Edge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IDEdge
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@Edge[] pool;

		static @Edge() {
		}

		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, container creation of Edge
		}

		public static GRGEN_MODEL.EdgeType_Edge TypeInstance { get { return GRGEN_MODEL.EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @Edge(GRGEN_MODEL.@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @Edge))
				return false;
			@Edge that_ = (@Edge)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@Edge[GRGEN_LGSP.LGSPGraph.poolSize];
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
				if(pool == null)
					pool = new GRGEN_MODEL.@Edge[GRGEN_LGSP.LGSPGraph.poolSize];
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
			if(pool == null)
				pool = new GRGEN_MODEL.@Edge[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
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
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IDEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@Edge"; } }
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
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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


	public sealed partial class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IUEdge
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@UEdge[] pool;

		static @UEdge() {
		}

		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, container creation of UEdge
		}

		public static GRGEN_MODEL.EdgeType_UEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @UEdge(GRGEN_MODEL.@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @UEdge))
				return false;
			@UEdge that_ = (@UEdge)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@UEdge[GRGEN_LGSP.LGSPGraph.poolSize];
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
				if(pool == null)
					pool = new GRGEN_MODEL.@UEdge[GRGEN_LGSP.LGSPGraph.poolSize];
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
			if(pool == null)
				pool = new GRGEN_MODEL.@UEdge[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
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
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IUEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@UEdge"; } }
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
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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

	public interface IE : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @E : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IE
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@E[] pool;

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

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@E(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@E(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @E(GRGEN_MODEL.@E oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_E.typeVar, newSource, newTarget)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @E))
				return false;
			@E that_ = (@E)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@E CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@E edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@E(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@E[GRGEN_LGSP.LGSPGraph.poolSize];
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
				if(pool == null)
					pool = new GRGEN_MODEL.@E[GRGEN_LGSP.LGSPGraph.poolSize];
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
			if(pool == null)
				pool = new GRGEN_MODEL.@E[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Edge type \"E\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Edge type \"E\" does not have the attribute \"" + attrName + "\"!");
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
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.IE"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@E"; } }
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
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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

	//
	// Object types
	//

	public enum ObjectTypes { @Object=0 };

	// *** Object Object ***


	public sealed partial class @Object : GRGEN_LGSP.LGSPObject, GRGEN_LIBGR.IObject
	{

		static @Object() {
		}

		//create object by CreateObject of the type class, not this internal-use constructor
		public @Object(long uniqueId) : base(GRGEN_MODEL.ObjectType_Object.typeVar, uniqueId)
		{
			// implicit initialization, container creation of Object
		}

		public static GRGEN_MODEL.ObjectType_Object TypeInstance { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }

		public override GRGEN_LIBGR.IObject Clone(GRGEN_LIBGR.IGraph graph) {
			GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(this, graph, null);
			((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
			return newObject;
		}

		public override GRGEN_LIBGR.IObject Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(this, graph, oldToNewObjectMap);
			((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
			return newObject;
		}

		private @Object(GRGEN_MODEL.@Object oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.ObjectType_Object.typeVar, graph.GlobalVariables.FetchObjectUniqueId())
		{
			if(oldToNewObjectMap != null)
				oldToNewObjectMap.Add(oldElem, this);
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @Object))
				return false;
			@Object that_ = (@Object)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Object type \"Object\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Object type \"Object\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Object
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Object does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Object does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class ObjectType_Object : GRGEN_LIBGR.ObjectType
	{
		public static GRGEN_MODEL.ObjectType_Object typeVar = new GRGEN_MODEL.ObjectType_Object();
		public static bool[] isA = new bool[] { true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public ObjectType_Object() : base((int) ObjectTypes.@Object)
		{
		}
		public override string Name { get { return "Object"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Object"; } }
		public override string ObjectInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.IObject"; } }
		public override string ObjectClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@Object"; } }
		public override GRGEN_LIBGR.IObject CreateObject(GRGEN_LIBGR.IGraph graph, long uniqueId)
		{
			if(uniqueId != -1) {
				GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(graph.GlobalVariables.FetchObjectUniqueId(uniqueId));
				((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
				return newObject;
			} else {
				GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(graph.GlobalVariables.FetchObjectUniqueId());
				((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
				return newObject;
			}
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
	}

	//
	// Transient object types
	//

	public enum TransientObjectTypes { @TransientObject=0 };

	// *** TransientObject TransientObject ***


	public sealed partial class @TransientObject : GRGEN_LGSP.LGSPTransientObject, GRGEN_LIBGR.ITransientObject
	{

		static @TransientObject() {
		}

		//create object by CreateTransientObject of the type class, not this internal-use constructor
		public @TransientObject() : base(GRGEN_MODEL.TransientObjectType_TransientObject.typeVar)
		{
			// implicit initialization, container creation of TransientObject
		}

		public static GRGEN_MODEL.TransientObjectType_TransientObject TypeInstance { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }

		public override GRGEN_LIBGR.ITransientObject Clone() {
			return new GRGEN_MODEL.@TransientObject(this, null, null);
		}

		public override GRGEN_LIBGR.ITransientObject Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@TransientObject(this, graph, oldToNewObjectMap);
		}

		private @TransientObject(GRGEN_MODEL.@TransientObject oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.TransientObjectType_TransientObject.typeVar)
		{
			if(oldToNewObjectMap != null)
				oldToNewObjectMap.Add(oldElem, this);
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @TransientObject))
				return false;
			@TransientObject that_ = (@TransientObject)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The TransientObject type \"TransientObject\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The TransientObject type \"TransientObject\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of TransientObject
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("TransientObject does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("TransientObject does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class TransientObjectType_TransientObject : GRGEN_LIBGR.TransientObjectType
	{
		public static GRGEN_MODEL.TransientObjectType_TransientObject typeVar = new GRGEN_MODEL.TransientObjectType_TransientObject();
		public static bool[] isA = new bool[] { true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public TransientObjectType_TransientObject() : base((int) TransientObjectTypes.@TransientObject)
		{
		}
		public override string Name { get { return "TransientObject"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "TransientObject"; } }
		public override string TransientObjectInterfaceName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.ITransientObject"; } }
		public override string TransientObjectClassName { get { return "de.unika.ipd.grGen.Model_ExternalFiltersAndSequences.@TransientObject"; } }
		public override GRGEN_LIBGR.ITransientObject CreateTransientObject()
		{
			return new GRGEN_MODEL.@TransientObject();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
	}

	public sealed class ExternalObjectType_object : GRGEN_LIBGR.ExternalObjectType
	{
		public ExternalObjectType_object()
			: base("object", typeof(object))
		{
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }

		public static object ThrowCopyClassMissingException() { throw new Exception("Cannot copy/clone external object, copy class specification is missing in the model."); }
	}

	//
	// Indices
	//

	public class ExternalFiltersAndSequencesIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public ExternalFiltersAndSequencesIndexSet(GRGEN_LGSP.LGSPGraph graph)
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

	public sealed class ExternalFiltersAndSequencesNodeModel : GRGEN_LIBGR.INodeModel
	{
		public ExternalFiltersAndSequencesNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_M.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_M.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_N.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_N.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_M.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_M.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_M.typeVar,
			};
			GRGEN_MODEL.NodeType_M.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_M.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_M.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_M.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_M.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_M.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_M.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(string name)
		{
			switch(name)
			{
				case "Node" : return GRGEN_MODEL.NodeType_Node.typeVar;
				case "N" : return GRGEN_MODEL.NodeType_N.typeVar;
				case "M" : return GRGEN_MODEL.NodeType_M.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_N.typeVar,
			GRGEN_MODEL.NodeType_M.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_N),
			typeof(GRGEN_MODEL.NodeType_M),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
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
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class ExternalFiltersAndSequencesEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public ExternalFiltersAndSequencesEdgeModel()
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
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
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
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
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
		GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_E),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Object model
	//

	public sealed class ExternalFiltersAndSequencesObjectModel : GRGEN_LIBGR.IObjectModel
	{
		public ExternalFiltersAndSequencesObjectModel()
		{
			GRGEN_MODEL.ObjectType_Object.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.subOrSameTypes = new GRGEN_LIBGR.ObjectType[] {
				GRGEN_MODEL.ObjectType_Object.typeVar,
			};
			GRGEN_MODEL.ObjectType_Object.typeVar.directSubGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.directSubTypes = new GRGEN_LIBGR.ObjectType[] {
			};
			GRGEN_MODEL.ObjectType_Object.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.superOrSameTypes = new GRGEN_LIBGR.ObjectType[] {
				GRGEN_MODEL.ObjectType_Object.typeVar,
			};
			GRGEN_MODEL.ObjectType_Object.typeVar.directSuperGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.directSuperTypes = new GRGEN_LIBGR.ObjectType[] {
			};
		}
		public bool IsTransientModel { get { return false; } }
		public GRGEN_LIBGR.ObjectType RootType { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.RootType { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }
		public GRGEN_LIBGR.ObjectType GetType(string name)
		{
			switch(name)
			{
				case "Object" : return GRGEN_MODEL.ObjectType_Object.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.ObjectType[] types = {
			GRGEN_MODEL.ObjectType_Object.typeVar,
		};
		public GRGEN_LIBGR.ObjectType[] Types { get { return types; } }
		GRGEN_LIBGR.BaseObjectType[] GRGEN_LIBGR.IBaseObjectTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.ObjectType_Object),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// TransientObject model
	//

	public sealed class ExternalFiltersAndSequencesTransientObjectModel : GRGEN_LIBGR.ITransientObjectModel
	{
		public ExternalFiltersAndSequencesTransientObjectModel()
		{
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.subOrSameTypes = new GRGEN_LIBGR.TransientObjectType[] {
				GRGEN_MODEL.TransientObjectType_TransientObject.typeVar,
			};
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSubGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSubTypes = new GRGEN_LIBGR.TransientObjectType[] {
			};
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.superOrSameTypes = new GRGEN_LIBGR.TransientObjectType[] {
				GRGEN_MODEL.TransientObjectType_TransientObject.typeVar,
			};
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSuperGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSuperTypes = new GRGEN_LIBGR.TransientObjectType[] {
			};
		}
		public bool IsTransientModel { get { return true; } }
		public GRGEN_LIBGR.TransientObjectType RootType { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.RootType { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }
		public GRGEN_LIBGR.TransientObjectType GetType(string name)
		{
			switch(name)
			{
				case "TransientObject" : return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.TransientObjectType[] types = {
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar,
		};
		public GRGEN_LIBGR.TransientObjectType[] Types { get { return types; } }
		GRGEN_LIBGR.BaseObjectType[] GRGEN_LIBGR.IBaseObjectTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.TransientObjectType_TransientObject),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel (LGSPGraphModel) implementation
	//
	public sealed class ExternalFiltersAndSequencesGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public ExternalFiltersAndSequencesGraphModel()
		{
			FullyInitializeExternalObjectTypes();
		}

		private ExternalFiltersAndSequencesNodeModel nodeModel = new ExternalFiltersAndSequencesNodeModel();
		private ExternalFiltersAndSequencesEdgeModel edgeModel = new ExternalFiltersAndSequencesEdgeModel();
		private ExternalFiltersAndSequencesObjectModel objectModel = new ExternalFiltersAndSequencesObjectModel();
		private ExternalFiltersAndSequencesTransientObjectModel transientObjectModel = new ExternalFiltersAndSequencesTransientObjectModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
			GRGEN_MODEL.Enums.@Enu,
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override GRGEN_LIBGR.IUniquenessHandler CreateUniquenessHandler(GRGEN_LIBGR.IGraph graph) {
			return null;
		}
		public override GRGEN_LIBGR.IIndexSet CreateIndexSet(GRGEN_LIBGR.IGraph graph) {
			return new ExternalFiltersAndSequencesIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ExternalFiltersAndSequencesIndexSet)graph.Indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "ExternalFiltersAndSequences"; } }
		public override GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public override GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public override GRGEN_LIBGR.IObjectModel ObjectModel { get { return objectModel; } }
		public override GRGEN_LIBGR.ITransientObjectModel TransientObjectModel { get { return transientObjectModel; } }
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
		public override bool AreFunctionsParallelized { get { return false; } }
		public override int BranchingFactorForEqualsAny { get { return 0; } }
		public override int ThreadPoolSizeForSequencesParallelExecution { get { return 0; } }

		public static GRGEN_LIBGR.ExternalObjectType externalObjectType_object = new ExternalObjectType_object();
		private GRGEN_LIBGR.ExternalObjectType[] externalObjectTypes = { externalObjectType_object };
		public override GRGEN_LIBGR.ExternalObjectType[] ExternalObjectTypes { get { return externalObjectTypes; } }

		private void FullyInitializeExternalObjectTypes()
		{
			externalObjectType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalObjectType[] { } );
		}

		public override global::System.Collections.IList ArrayOrderAscendingBy(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				case "s":
					return ArrayHelper_N_s.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				case "b":
					return ArrayHelper_N_b.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				case "f":
					return ArrayHelper_N_f.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				case "d":
					return ArrayHelper_N_d.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				case "enu":
					return ArrayHelper_N_enu.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "M":
				switch(member)
				{
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "E":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override global::System.Collections.IList ArrayOrderDescendingBy(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				case "s":
					return ArrayHelper_N_s.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				case "b":
					return ArrayHelper_N_b.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				case "f":
					return ArrayHelper_N_f.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				case "d":
					return ArrayHelper_N_d.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				case "enu":
					return ArrayHelper_N_enu.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "M":
				switch(member)
				{
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "E":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override global::System.Collections.IList ArrayGroupBy(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				case "s":
					return ArrayHelper_N_s.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				case "b":
					return ArrayHelper_N_b.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				case "f":
					return ArrayHelper_N_f.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				case "d":
					return ArrayHelper_N_d.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				case "enu":
					return ArrayHelper_N_enu.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "M":
				switch(member)
				{
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "E":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override global::System.Collections.IList ArrayKeepOneForEach(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				case "s":
					return ArrayHelper_N_s.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				case "b":
					return ArrayHelper_N_b.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				case "f":
					return ArrayHelper_N_f.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				case "d":
					return ArrayHelper_N_d.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				case "enu":
					return ArrayHelper_N_enu.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "M":
				switch(member)
				{
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "E":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override int ArrayIndexOfBy(global::System.Collections.IList array, string member, object value)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value);
				case "s":
					return ArrayHelper_N_s.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (string)value);
				case "b":
					return ArrayHelper_N_b.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (bool)value);
				case "f":
					return ArrayHelper_N_f.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (float)value);
				case "d":
					return ArrayHelper_N_d.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (double)value);
				case "enu":
					return ArrayHelper_N_enu.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (GRGEN_MODEL.ENUM_Enu)value);
				default:
					return -1;
				}
			case "M":
				switch(member)
				{
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "E":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayIndexOfBy(global::System.Collections.IList array, string member, object value, int startIndex)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value, startIndex);
				case "s":
					return ArrayHelper_N_s.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (string)value, startIndex);
				case "b":
					return ArrayHelper_N_b.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (bool)value, startIndex);
				case "f":
					return ArrayHelper_N_f.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (float)value, startIndex);
				case "d":
					return ArrayHelper_N_d.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (double)value, startIndex);
				case "enu":
					return ArrayHelper_N_enu.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (GRGEN_MODEL.ENUM_Enu)value, startIndex);
				default:
					return -1;
				}
			case "M":
				switch(member)
				{
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "E":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayLastIndexOfBy(global::System.Collections.IList array, string member, object value)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value);
				case "s":
					return ArrayHelper_N_s.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (string)value);
				case "b":
					return ArrayHelper_N_b.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (bool)value);
				case "f":
					return ArrayHelper_N_f.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (float)value);
				case "d":
					return ArrayHelper_N_d.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (double)value);
				case "enu":
					return ArrayHelper_N_enu.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (GRGEN_MODEL.ENUM_Enu)value);
				default:
					return -1;
				}
			case "M":
				switch(member)
				{
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "E":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayLastIndexOfBy(global::System.Collections.IList array, string member, object value, int startIndex)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value, startIndex);
				case "s":
					return ArrayHelper_N_s.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (string)value, startIndex);
				case "b":
					return ArrayHelper_N_b.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (bool)value, startIndex);
				case "f":
					return ArrayHelper_N_f.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (float)value, startIndex);
				case "d":
					return ArrayHelper_N_d.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (double)value, startIndex);
				case "enu":
					return ArrayHelper_N_enu.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (GRGEN_MODEL.ENUM_Enu)value, startIndex);
				default:
					return -1;
				}
			case "M":
				switch(member)
				{
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "E":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayIndexOfOrderedBy(global::System.Collections.IList array, string member, object value)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (int)value);
				case "s":
					return ArrayHelper_N_s.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (string)value);
				case "b":
					return ArrayHelper_N_b.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (bool)value);
				case "f":
					return ArrayHelper_N_f.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (float)value);
				case "d":
					return ArrayHelper_N_d.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (double)value);
				case "enu":
					return ArrayHelper_N_enu.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (GRGEN_MODEL.ENUM_Enu)value);
				default:
					return -1;
				}
			case "M":
				switch(member)
				{
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "E":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}


		public override void FailAssertion() { Debug.Assert(false); }
		public override string MD5Hash { get { return "05f4fa5ec9c398824a1d5cb06f40c628"; } }
	}

	//
	// IGraph (LGSPGraph) implementation
	//
	public class ExternalFiltersAndSequencesGraph : GRGEN_LGSP.LGSPGraph
	{
		public ExternalFiltersAndSequencesGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new ExternalFiltersAndSequencesGraphModel(), globalVariables, GetGraphName())
		{
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@N CreateNodeN()
		{
			return GRGEN_MODEL.@N.CreateNode(this);
		}

		public GRGEN_MODEL.@M CreateNodeM()
		{
			return GRGEN_MODEL.@M.CreateNode(this);
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

	}

	//
	// INamedGraph (LGSPNamedGraph) implementation
	//
	public class ExternalFiltersAndSequencesNamedGraph : GRGEN_LGSP.LGSPNamedGraph
	{
		public ExternalFiltersAndSequencesNamedGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new ExternalFiltersAndSequencesGraphModel(), globalVariables, GetGraphName(), 0)
		{
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

		public GRGEN_MODEL.@M CreateNodeM()
		{
			return GRGEN_MODEL.@M.CreateNode(this);
		}

		public GRGEN_MODEL.@M CreateNodeM(string nodeName)
		{
			return GRGEN_MODEL.@M.CreateNode(this, nodeName);
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

	}
}
