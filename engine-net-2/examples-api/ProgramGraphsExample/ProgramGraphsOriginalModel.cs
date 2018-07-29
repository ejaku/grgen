// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ProgramGraphs\ProgramGraphsOriginal.grg" on Sun Jul 29 09:01:02 CEST 2018

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ProgramGraphsOriginal;

namespace de.unika.ipd.grGen.Model_ProgramGraphsOriginal
{

	//
	// Enums
	//

	public class Enums
	{
	}

	//
	// Node types
	//

	public enum NodeTypes { @Node=0, @Entity=1, @MethodBody=2, @Expression=3, @Declaration=4, @Class=5, @Feature=6, @MethodSignature=7, @Attribute=8, @Constant=9, @Variabel=10 };

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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@Node"; } }
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

	// *** Node Entity ***

	public interface IEntity : GRGEN_LIBGR.INode
	{
	}

	public sealed partial class NodeType_Entity : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Entity typeVar = new GRGEN_MODEL.NodeType_Entity();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, true, true, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Entity() : base((int) NodeTypes.@Entity)
		{
		}
		public override string Name { get { return "Entity"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Entity"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IEntity"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Entity cannot be instantiated!");
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
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Entity!");
		}
	}

	// *** Node MethodBody ***

	public interface IMethodBody : IEntity
	{
	}

	public sealed partial class @MethodBody : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IMethodBody
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@MethodBody[] pool = new GRGEN_MODEL.@MethodBody[10];
		
		// explicit initializations of Entity for target MethodBody
		// implicit initializations of Entity for target MethodBody
		// explicit initializations of MethodBody for target MethodBody
		// implicit initializations of MethodBody for target MethodBody
		static @MethodBody() {
		}
		
		public @MethodBody() : base(GRGEN_MODEL.NodeType_MethodBody.typeVar)
		{
			// implicit initialization, container creation of MethodBody
			// explicit initializations of Entity for target MethodBody
			// explicit initializations of MethodBody for target MethodBody
		}

		public static GRGEN_MODEL.NodeType_MethodBody TypeInstance { get { return GRGEN_MODEL.NodeType_MethodBody.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@MethodBody(this); }

		private @MethodBody(GRGEN_MODEL.@MethodBody oldElem) : base(GRGEN_MODEL.NodeType_MethodBody.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @MethodBody)) return false;
			@MethodBody that_ = (@MethodBody)that;
			return true
			;
		}

		public static GRGEN_MODEL.@MethodBody CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@MethodBody node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@MethodBody();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of MethodBody
				// explicit initializations of Entity for target MethodBody
				// explicit initializations of MethodBody for target MethodBody
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@MethodBody CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@MethodBody node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@MethodBody();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of MethodBody
				// explicit initializations of Entity for target MethodBody
				// explicit initializations of MethodBody for target MethodBody
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
				"The node type \"MethodBody\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"MethodBody\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of MethodBody
			// explicit initializations of Entity for target MethodBody
			// explicit initializations of MethodBody for target MethodBody
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("MethodBody does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("MethodBody does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_MethodBody : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_MethodBody typeVar = new GRGEN_MODEL.NodeType_MethodBody();
		public static bool[] isA = new bool[] { true, true, true, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_MethodBody() : base((int) NodeTypes.@MethodBody)
		{
		}
		public override string Name { get { return "MethodBody"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "MethodBody"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IMethodBody"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@MethodBody"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@MethodBody();
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
			return new GRGEN_MODEL.@MethodBody();
		}

	}

	// *** Node Expression ***

	public interface IExpression : IEntity
	{
	}

	public sealed partial class @Expression : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IExpression
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Expression[] pool = new GRGEN_MODEL.@Expression[10];
		
		// explicit initializations of Entity for target Expression
		// implicit initializations of Entity for target Expression
		// explicit initializations of Expression for target Expression
		// implicit initializations of Expression for target Expression
		static @Expression() {
		}
		
		public @Expression() : base(GRGEN_MODEL.NodeType_Expression.typeVar)
		{
			// implicit initialization, container creation of Expression
			// explicit initializations of Entity for target Expression
			// explicit initializations of Expression for target Expression
		}

		public static GRGEN_MODEL.NodeType_Expression TypeInstance { get { return GRGEN_MODEL.NodeType_Expression.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Expression(this); }

		private @Expression(GRGEN_MODEL.@Expression oldElem) : base(GRGEN_MODEL.NodeType_Expression.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Expression)) return false;
			@Expression that_ = (@Expression)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Expression CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Expression node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Expression();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Expression
				// explicit initializations of Entity for target Expression
				// explicit initializations of Expression for target Expression
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Expression CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Expression node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Expression();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Expression
				// explicit initializations of Entity for target Expression
				// explicit initializations of Expression for target Expression
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
				"The node type \"Expression\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Expression\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Expression
			// explicit initializations of Entity for target Expression
			// explicit initializations of Expression for target Expression
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Expression does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Expression does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Expression : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Expression typeVar = new GRGEN_MODEL.NodeType_Expression();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Expression() : base((int) NodeTypes.@Expression)
		{
		}
		public override string Name { get { return "Expression"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Expression"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IExpression"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@Expression"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Expression();
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
			return new GRGEN_MODEL.@Expression();
		}

	}

	// *** Node Declaration ***

	public interface IDeclaration : IEntity
	{
	}

	public sealed partial class NodeType_Declaration : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Declaration typeVar = new GRGEN_MODEL.NodeType_Declaration();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Declaration() : base((int) NodeTypes.@Declaration)
		{
		}
		public override string Name { get { return "Declaration"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Declaration"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IDeclaration"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Declaration cannot be instantiated!");
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
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Declaration!");
		}
	}

	// *** Node Class ***

	public interface IClass : IDeclaration
	{
	}

	public sealed partial class @Class : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IClass
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Class[] pool = new GRGEN_MODEL.@Class[10];
		
		// explicit initializations of Entity for target Class
		// implicit initializations of Entity for target Class
		// explicit initializations of Declaration for target Class
		// implicit initializations of Declaration for target Class
		// explicit initializations of Class for target Class
		// implicit initializations of Class for target Class
		static @Class() {
		}
		
		public @Class() : base(GRGEN_MODEL.NodeType_Class.typeVar)
		{
			// implicit initialization, container creation of Class
			// explicit initializations of Entity for target Class
			// explicit initializations of Declaration for target Class
			// explicit initializations of Class for target Class
		}

		public static GRGEN_MODEL.NodeType_Class TypeInstance { get { return GRGEN_MODEL.NodeType_Class.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Class(this); }

		private @Class(GRGEN_MODEL.@Class oldElem) : base(GRGEN_MODEL.NodeType_Class.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Class)) return false;
			@Class that_ = (@Class)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Class CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Class node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Class();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Class
				// explicit initializations of Entity for target Class
				// explicit initializations of Declaration for target Class
				// explicit initializations of Class for target Class
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Class CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Class node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Class();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Class
				// explicit initializations of Entity for target Class
				// explicit initializations of Declaration for target Class
				// explicit initializations of Class for target Class
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
				"The node type \"Class\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Class\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Class
			// explicit initializations of Entity for target Class
			// explicit initializations of Declaration for target Class
			// explicit initializations of Class for target Class
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Class does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Class does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Class : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Class typeVar = new GRGEN_MODEL.NodeType_Class();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Class() : base((int) NodeTypes.@Class)
		{
		}
		public override string Name { get { return "Class"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Class"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IClass"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@Class"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Class();
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
			return new GRGEN_MODEL.@Class();
		}

	}

	// *** Node Feature ***

	public interface IFeature : IDeclaration
	{
	}

	public sealed partial class NodeType_Feature : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Feature typeVar = new GRGEN_MODEL.NodeType_Feature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Feature() : base((int) NodeTypes.@Feature)
		{
		}
		public override string Name { get { return "Feature"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Feature"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IFeature"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Feature cannot be instantiated!");
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
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Feature!");
		}
	}

	// *** Node MethodSignature ***

	public interface IMethodSignature : IFeature
	{
	}

	public sealed partial class @MethodSignature : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IMethodSignature
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@MethodSignature[] pool = new GRGEN_MODEL.@MethodSignature[10];
		
		// explicit initializations of Entity for target MethodSignature
		// implicit initializations of Entity for target MethodSignature
		// explicit initializations of Declaration for target MethodSignature
		// implicit initializations of Declaration for target MethodSignature
		// explicit initializations of Feature for target MethodSignature
		// implicit initializations of Feature for target MethodSignature
		// explicit initializations of MethodSignature for target MethodSignature
		// implicit initializations of MethodSignature for target MethodSignature
		static @MethodSignature() {
		}
		
		public @MethodSignature() : base(GRGEN_MODEL.NodeType_MethodSignature.typeVar)
		{
			// implicit initialization, container creation of MethodSignature
			// explicit initializations of Entity for target MethodSignature
			// explicit initializations of Declaration for target MethodSignature
			// explicit initializations of Feature for target MethodSignature
			// explicit initializations of MethodSignature for target MethodSignature
		}

		public static GRGEN_MODEL.NodeType_MethodSignature TypeInstance { get { return GRGEN_MODEL.NodeType_MethodSignature.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@MethodSignature(this); }

		private @MethodSignature(GRGEN_MODEL.@MethodSignature oldElem) : base(GRGEN_MODEL.NodeType_MethodSignature.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @MethodSignature)) return false;
			@MethodSignature that_ = (@MethodSignature)that;
			return true
			;
		}

		public static GRGEN_MODEL.@MethodSignature CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@MethodSignature node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@MethodSignature();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of MethodSignature
				// explicit initializations of Entity for target MethodSignature
				// explicit initializations of Declaration for target MethodSignature
				// explicit initializations of Feature for target MethodSignature
				// explicit initializations of MethodSignature for target MethodSignature
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@MethodSignature CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@MethodSignature node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@MethodSignature();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of MethodSignature
				// explicit initializations of Entity for target MethodSignature
				// explicit initializations of Declaration for target MethodSignature
				// explicit initializations of Feature for target MethodSignature
				// explicit initializations of MethodSignature for target MethodSignature
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
				"The node type \"MethodSignature\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"MethodSignature\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of MethodSignature
			// explicit initializations of Entity for target MethodSignature
			// explicit initializations of Declaration for target MethodSignature
			// explicit initializations of Feature for target MethodSignature
			// explicit initializations of MethodSignature for target MethodSignature
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("MethodSignature does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("MethodSignature does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_MethodSignature : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_MethodSignature typeVar = new GRGEN_MODEL.NodeType_MethodSignature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_MethodSignature() : base((int) NodeTypes.@MethodSignature)
		{
		}
		public override string Name { get { return "MethodSignature"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "MethodSignature"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IMethodSignature"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@MethodSignature"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@MethodSignature();
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
			return new GRGEN_MODEL.@MethodSignature();
		}

	}

	// *** Node Attribute ***

	public interface IAttribute : IFeature
	{
	}

	public sealed partial class NodeType_Attribute : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Attribute typeVar = new GRGEN_MODEL.NodeType_Attribute();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Attribute() : base((int) NodeTypes.@Attribute)
		{
		}
		public override string Name { get { return "Attribute"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Attribute"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IAttribute"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Attribute cannot be instantiated!");
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
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Attribute!");
		}
	}

	// *** Node Constant ***

	public interface IConstant : IAttribute
	{
	}

	public sealed partial class @Constant : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IConstant
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Constant[] pool = new GRGEN_MODEL.@Constant[10];
		
		// explicit initializations of Entity for target Constant
		// implicit initializations of Entity for target Constant
		// explicit initializations of Declaration for target Constant
		// implicit initializations of Declaration for target Constant
		// explicit initializations of Feature for target Constant
		// implicit initializations of Feature for target Constant
		// explicit initializations of Attribute for target Constant
		// implicit initializations of Attribute for target Constant
		// explicit initializations of Constant for target Constant
		// implicit initializations of Constant for target Constant
		static @Constant() {
		}
		
		public @Constant() : base(GRGEN_MODEL.NodeType_Constant.typeVar)
		{
			// implicit initialization, container creation of Constant
			// explicit initializations of Entity for target Constant
			// explicit initializations of Declaration for target Constant
			// explicit initializations of Feature for target Constant
			// explicit initializations of Attribute for target Constant
			// explicit initializations of Constant for target Constant
		}

		public static GRGEN_MODEL.NodeType_Constant TypeInstance { get { return GRGEN_MODEL.NodeType_Constant.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Constant(this); }

		private @Constant(GRGEN_MODEL.@Constant oldElem) : base(GRGEN_MODEL.NodeType_Constant.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Constant)) return false;
			@Constant that_ = (@Constant)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Constant CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Constant node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Constant();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Constant
				// explicit initializations of Entity for target Constant
				// explicit initializations of Declaration for target Constant
				// explicit initializations of Feature for target Constant
				// explicit initializations of Attribute for target Constant
				// explicit initializations of Constant for target Constant
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Constant CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Constant node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Constant();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Constant
				// explicit initializations of Entity for target Constant
				// explicit initializations of Declaration for target Constant
				// explicit initializations of Feature for target Constant
				// explicit initializations of Attribute for target Constant
				// explicit initializations of Constant for target Constant
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
				"The node type \"Constant\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Constant\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Constant
			// explicit initializations of Entity for target Constant
			// explicit initializations of Declaration for target Constant
			// explicit initializations of Feature for target Constant
			// explicit initializations of Attribute for target Constant
			// explicit initializations of Constant for target Constant
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Constant does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Constant does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Constant : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Constant typeVar = new GRGEN_MODEL.NodeType_Constant();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Constant() : base((int) NodeTypes.@Constant)
		{
		}
		public override string Name { get { return "Constant"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Constant"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IConstant"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@Constant"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Constant();
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
			return new GRGEN_MODEL.@Constant();
		}

	}

	// *** Node Variabel ***

	public interface IVariabel : IAttribute
	{
	}

	public sealed partial class @Variabel : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IVariabel
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Variabel[] pool = new GRGEN_MODEL.@Variabel[10];
		
		// explicit initializations of Entity for target Variabel
		// implicit initializations of Entity for target Variabel
		// explicit initializations of Declaration for target Variabel
		// implicit initializations of Declaration for target Variabel
		// explicit initializations of Feature for target Variabel
		// implicit initializations of Feature for target Variabel
		// explicit initializations of Attribute for target Variabel
		// implicit initializations of Attribute for target Variabel
		// explicit initializations of Variabel for target Variabel
		// implicit initializations of Variabel for target Variabel
		static @Variabel() {
		}
		
		public @Variabel() : base(GRGEN_MODEL.NodeType_Variabel.typeVar)
		{
			// implicit initialization, container creation of Variabel
			// explicit initializations of Entity for target Variabel
			// explicit initializations of Declaration for target Variabel
			// explicit initializations of Feature for target Variabel
			// explicit initializations of Attribute for target Variabel
			// explicit initializations of Variabel for target Variabel
		}

		public static GRGEN_MODEL.NodeType_Variabel TypeInstance { get { return GRGEN_MODEL.NodeType_Variabel.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Variabel(this); }

		private @Variabel(GRGEN_MODEL.@Variabel oldElem) : base(GRGEN_MODEL.NodeType_Variabel.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Variabel)) return false;
			@Variabel that_ = (@Variabel)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Variabel CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Variabel node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Variabel();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Variabel
				// explicit initializations of Entity for target Variabel
				// explicit initializations of Declaration for target Variabel
				// explicit initializations of Feature for target Variabel
				// explicit initializations of Attribute for target Variabel
				// explicit initializations of Variabel for target Variabel
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Variabel CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Variabel node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Variabel();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Variabel
				// explicit initializations of Entity for target Variabel
				// explicit initializations of Declaration for target Variabel
				// explicit initializations of Feature for target Variabel
				// explicit initializations of Attribute for target Variabel
				// explicit initializations of Variabel for target Variabel
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
				"The node type \"Variabel\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Variabel\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Variabel
			// explicit initializations of Entity for target Variabel
			// explicit initializations of Declaration for target Variabel
			// explicit initializations of Feature for target Variabel
			// explicit initializations of Attribute for target Variabel
			// explicit initializations of Variabel for target Variabel
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Variabel does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Variabel does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Variabel : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Variabel typeVar = new GRGEN_MODEL.NodeType_Variabel();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Variabel() : base((int) NodeTypes.@Variabel)
		{
		}
		public override string Name { get { return "Variabel"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Variabel"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IVariabel"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@Variabel"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Variabel();
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
			return new GRGEN_MODEL.@Variabel();
		}

	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2, @contains=3, @references=4, @hasType=5, @bindsTo=6, @uses=7, @writesTo=8, @calls=9, @methodBodyContains=10, @classContainsClass=11 };

	// *** Edge AEdge ***


	public sealed partial class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, };
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


	public sealed partial class @Edge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IDEdge
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IDEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@Edge"; } }
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


	public sealed partial class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IUEdge
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
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IUEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@UEdge"; } }
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

	// *** Edge contains ***

	public interface Icontains : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @contains : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Icontains
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@contains[] pool = new GRGEN_MODEL.@contains[10];
		
		// explicit initializations of contains for target contains
		// implicit initializations of contains for target contains
		static @contains() {
		}
		
		public @contains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_contains.typeVar, source, target)
		{
			// implicit initialization, container creation of contains
			// explicit initializations of contains for target contains
		}

		public static GRGEN_MODEL.EdgeType_contains TypeInstance { get { return GRGEN_MODEL.EdgeType_contains.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@contains(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @contains(GRGEN_MODEL.@contains oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_contains.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @contains)) return false;
			@contains that_ = (@contains)that;
			return true
			;
		}

		public static GRGEN_MODEL.@contains CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@contains edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@contains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of contains
				// explicit initializations of contains for target contains
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@contains CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@contains edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@contains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of contains
				// explicit initializations of contains for target contains
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
				"The edge type \"contains\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"contains\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of contains
			// explicit initializations of contains for target contains
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("contains does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("contains does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_contains : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_contains typeVar = new GRGEN_MODEL.EdgeType_contains();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_contains() : base((int) EdgeTypes.@contains)
		{
		}
		public override string Name { get { return "contains"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "contains"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.Icontains"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@contains"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@contains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@contains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge references ***

	public interface Ireferences : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @references : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Ireferences
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@references[] pool = new GRGEN_MODEL.@references[10];
		
		// explicit initializations of references for target references
		// implicit initializations of references for target references
		static @references() {
		}
		
		public @references(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_references.typeVar, source, target)
		{
			// implicit initialization, container creation of references
			// explicit initializations of references for target references
		}

		public static GRGEN_MODEL.EdgeType_references TypeInstance { get { return GRGEN_MODEL.EdgeType_references.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@references(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @references(GRGEN_MODEL.@references oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_references.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @references)) return false;
			@references that_ = (@references)that;
			return true
			;
		}

		public static GRGEN_MODEL.@references CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@references edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@references(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of references
				// explicit initializations of references for target references
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@references CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@references edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@references(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of references
				// explicit initializations of references for target references
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
				"The edge type \"references\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"references\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of references
			// explicit initializations of references for target references
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("references does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("references does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_references : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_references typeVar = new GRGEN_MODEL.EdgeType_references();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_references() : base((int) EdgeTypes.@references)
		{
		}
		public override string Name { get { return "references"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "references"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.Ireferences"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@references"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@references((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@references((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge hasType ***

	public interface IhasType : Ireferences
	{
	}

	public sealed partial class @hasType : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IhasType
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@hasType[] pool = new GRGEN_MODEL.@hasType[10];
		
		// explicit initializations of references for target hasType
		// implicit initializations of references for target hasType
		// explicit initializations of hasType for target hasType
		// implicit initializations of hasType for target hasType
		static @hasType() {
		}
		
		public @hasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_hasType.typeVar, source, target)
		{
			// implicit initialization, container creation of hasType
			// explicit initializations of references for target hasType
			// explicit initializations of hasType for target hasType
		}

		public static GRGEN_MODEL.EdgeType_hasType TypeInstance { get { return GRGEN_MODEL.EdgeType_hasType.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@hasType(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @hasType(GRGEN_MODEL.@hasType oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_hasType.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @hasType)) return false;
			@hasType that_ = (@hasType)that;
			return true
			;
		}

		public static GRGEN_MODEL.@hasType CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@hasType edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@hasType(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of hasType
				// explicit initializations of references for target hasType
				// explicit initializations of hasType for target hasType
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@hasType CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@hasType edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@hasType(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of hasType
				// explicit initializations of references for target hasType
				// explicit initializations of hasType for target hasType
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
				"The edge type \"hasType\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"hasType\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of hasType
			// explicit initializations of references for target hasType
			// explicit initializations of hasType for target hasType
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("hasType does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("hasType does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_hasType : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_hasType typeVar = new GRGEN_MODEL.EdgeType_hasType();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_hasType() : base((int) EdgeTypes.@hasType)
		{
		}
		public override string Name { get { return "hasType"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "hasType"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IhasType"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@hasType"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@hasType((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@hasType((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge bindsTo ***

	public interface IbindsTo : Ireferences
	{
	}

	public sealed partial class @bindsTo : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IbindsTo
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@bindsTo[] pool = new GRGEN_MODEL.@bindsTo[10];
		
		// explicit initializations of references for target bindsTo
		// implicit initializations of references for target bindsTo
		// explicit initializations of bindsTo for target bindsTo
		// implicit initializations of bindsTo for target bindsTo
		static @bindsTo() {
		}
		
		public @bindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_bindsTo.typeVar, source, target)
		{
			// implicit initialization, container creation of bindsTo
			// explicit initializations of references for target bindsTo
			// explicit initializations of bindsTo for target bindsTo
		}

		public static GRGEN_MODEL.EdgeType_bindsTo TypeInstance { get { return GRGEN_MODEL.EdgeType_bindsTo.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@bindsTo(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @bindsTo(GRGEN_MODEL.@bindsTo oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_bindsTo.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @bindsTo)) return false;
			@bindsTo that_ = (@bindsTo)that;
			return true
			;
		}

		public static GRGEN_MODEL.@bindsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@bindsTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@bindsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of bindsTo
				// explicit initializations of references for target bindsTo
				// explicit initializations of bindsTo for target bindsTo
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@bindsTo CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@bindsTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@bindsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of bindsTo
				// explicit initializations of references for target bindsTo
				// explicit initializations of bindsTo for target bindsTo
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
				"The edge type \"bindsTo\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"bindsTo\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of bindsTo
			// explicit initializations of references for target bindsTo
			// explicit initializations of bindsTo for target bindsTo
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("bindsTo does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("bindsTo does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_bindsTo : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_bindsTo typeVar = new GRGEN_MODEL.EdgeType_bindsTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_bindsTo() : base((int) EdgeTypes.@bindsTo)
		{
		}
		public override string Name { get { return "bindsTo"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "bindsTo"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IbindsTo"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@bindsTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@bindsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@bindsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge uses ***

	public interface Iuses : Ireferences
	{
	}

	public sealed partial class @uses : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iuses
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@uses[] pool = new GRGEN_MODEL.@uses[10];
		
		// explicit initializations of references for target uses
		// implicit initializations of references for target uses
		// explicit initializations of uses for target uses
		// implicit initializations of uses for target uses
		static @uses() {
		}
		
		public @uses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_uses.typeVar, source, target)
		{
			// implicit initialization, container creation of uses
			// explicit initializations of references for target uses
			// explicit initializations of uses for target uses
		}

		public static GRGEN_MODEL.EdgeType_uses TypeInstance { get { return GRGEN_MODEL.EdgeType_uses.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@uses(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @uses(GRGEN_MODEL.@uses oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_uses.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @uses)) return false;
			@uses that_ = (@uses)that;
			return true
			;
		}

		public static GRGEN_MODEL.@uses CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@uses edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@uses(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of uses
				// explicit initializations of references for target uses
				// explicit initializations of uses for target uses
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@uses CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@uses edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@uses(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of uses
				// explicit initializations of references for target uses
				// explicit initializations of uses for target uses
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
				"The edge type \"uses\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"uses\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of uses
			// explicit initializations of references for target uses
			// explicit initializations of uses for target uses
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("uses does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("uses does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_uses : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_uses typeVar = new GRGEN_MODEL.EdgeType_uses();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_uses() : base((int) EdgeTypes.@uses)
		{
		}
		public override string Name { get { return "uses"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "uses"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.Iuses"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@uses"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@uses((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@uses((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge writesTo ***

	public interface IwritesTo : Ireferences
	{
	}

	public sealed partial class @writesTo : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IwritesTo
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@writesTo[] pool = new GRGEN_MODEL.@writesTo[10];
		
		// explicit initializations of references for target writesTo
		// implicit initializations of references for target writesTo
		// explicit initializations of writesTo for target writesTo
		// implicit initializations of writesTo for target writesTo
		static @writesTo() {
		}
		
		public @writesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_writesTo.typeVar, source, target)
		{
			// implicit initialization, container creation of writesTo
			// explicit initializations of references for target writesTo
			// explicit initializations of writesTo for target writesTo
		}

		public static GRGEN_MODEL.EdgeType_writesTo TypeInstance { get { return GRGEN_MODEL.EdgeType_writesTo.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@writesTo(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @writesTo(GRGEN_MODEL.@writesTo oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_writesTo.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @writesTo)) return false;
			@writesTo that_ = (@writesTo)that;
			return true
			;
		}

		public static GRGEN_MODEL.@writesTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@writesTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@writesTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of writesTo
				// explicit initializations of references for target writesTo
				// explicit initializations of writesTo for target writesTo
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@writesTo CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@writesTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@writesTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of writesTo
				// explicit initializations of references for target writesTo
				// explicit initializations of writesTo for target writesTo
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
				"The edge type \"writesTo\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"writesTo\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of writesTo
			// explicit initializations of references for target writesTo
			// explicit initializations of writesTo for target writesTo
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("writesTo does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("writesTo does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_writesTo : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_writesTo typeVar = new GRGEN_MODEL.EdgeType_writesTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_writesTo() : base((int) EdgeTypes.@writesTo)
		{
		}
		public override string Name { get { return "writesTo"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "writesTo"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IwritesTo"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@writesTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@writesTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@writesTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge calls ***

	public interface Icalls : Ireferences
	{
	}

	public sealed partial class @calls : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Icalls
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@calls[] pool = new GRGEN_MODEL.@calls[10];
		
		// explicit initializations of references for target calls
		// implicit initializations of references for target calls
		// explicit initializations of calls for target calls
		// implicit initializations of calls for target calls
		static @calls() {
		}
		
		public @calls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_calls.typeVar, source, target)
		{
			// implicit initialization, container creation of calls
			// explicit initializations of references for target calls
			// explicit initializations of calls for target calls
		}

		public static GRGEN_MODEL.EdgeType_calls TypeInstance { get { return GRGEN_MODEL.EdgeType_calls.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@calls(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @calls(GRGEN_MODEL.@calls oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_calls.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @calls)) return false;
			@calls that_ = (@calls)that;
			return true
			;
		}

		public static GRGEN_MODEL.@calls CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@calls edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@calls(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of calls
				// explicit initializations of references for target calls
				// explicit initializations of calls for target calls
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@calls CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@calls edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@calls(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of calls
				// explicit initializations of references for target calls
				// explicit initializations of calls for target calls
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
				"The edge type \"calls\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"calls\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of calls
			// explicit initializations of references for target calls
			// explicit initializations of calls for target calls
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("calls does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("calls does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_calls : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_calls typeVar = new GRGEN_MODEL.EdgeType_calls();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_calls() : base((int) EdgeTypes.@calls)
		{
		}
		public override string Name { get { return "calls"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "calls"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.Icalls"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@calls"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@calls((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@calls((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge methodBodyContains ***

	public interface ImethodBodyContains : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @methodBodyContains : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.ImethodBodyContains
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@methodBodyContains[] pool = new GRGEN_MODEL.@methodBodyContains[10];
		
		// explicit initializations of methodBodyContains for target methodBodyContains
		// implicit initializations of methodBodyContains for target methodBodyContains
		static @methodBodyContains() {
		}
		
		public @methodBodyContains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_methodBodyContains.typeVar, source, target)
		{
			// implicit initialization, container creation of methodBodyContains
			// explicit initializations of methodBodyContains for target methodBodyContains
		}

		public static GRGEN_MODEL.EdgeType_methodBodyContains TypeInstance { get { return GRGEN_MODEL.EdgeType_methodBodyContains.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@methodBodyContains(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @methodBodyContains(GRGEN_MODEL.@methodBodyContains oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_methodBodyContains.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @methodBodyContains)) return false;
			@methodBodyContains that_ = (@methodBodyContains)that;
			return true
			;
		}

		public static GRGEN_MODEL.@methodBodyContains CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@methodBodyContains edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@methodBodyContains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of methodBodyContains
				// explicit initializations of methodBodyContains for target methodBodyContains
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@methodBodyContains CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@methodBodyContains edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@methodBodyContains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of methodBodyContains
				// explicit initializations of methodBodyContains for target methodBodyContains
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
				"The edge type \"methodBodyContains\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"methodBodyContains\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of methodBodyContains
			// explicit initializations of methodBodyContains for target methodBodyContains
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("methodBodyContains does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("methodBodyContains does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_methodBodyContains : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_methodBodyContains typeVar = new GRGEN_MODEL.EdgeType_methodBodyContains();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_methodBodyContains() : base((int) EdgeTypes.@methodBodyContains)
		{
		}
		public override string Name { get { return "methodBodyContains"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "methodBodyContains"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.ImethodBodyContains"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@methodBodyContains"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@methodBodyContains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@methodBodyContains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge classContainsClass ***

	public interface IclassContainsClass : Icontains
	{
	}

	public sealed partial class @classContainsClass : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IclassContainsClass
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@classContainsClass[] pool = new GRGEN_MODEL.@classContainsClass[10];
		
		// explicit initializations of contains for target classContainsClass
		// implicit initializations of contains for target classContainsClass
		// explicit initializations of classContainsClass for target classContainsClass
		// implicit initializations of classContainsClass for target classContainsClass
		static @classContainsClass() {
		}
		
		public @classContainsClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_classContainsClass.typeVar, source, target)
		{
			// implicit initialization, container creation of classContainsClass
			// explicit initializations of contains for target classContainsClass
			// explicit initializations of classContainsClass for target classContainsClass
		}

		public static GRGEN_MODEL.EdgeType_classContainsClass TypeInstance { get { return GRGEN_MODEL.EdgeType_classContainsClass.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@classContainsClass(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @classContainsClass(GRGEN_MODEL.@classContainsClass oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_classContainsClass.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @classContainsClass)) return false;
			@classContainsClass that_ = (@classContainsClass)that;
			return true
			;
		}

		public static GRGEN_MODEL.@classContainsClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@classContainsClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@classContainsClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of classContainsClass
				// explicit initializations of contains for target classContainsClass
				// explicit initializations of classContainsClass for target classContainsClass
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@classContainsClass CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@classContainsClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@classContainsClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of classContainsClass
				// explicit initializations of contains for target classContainsClass
				// explicit initializations of classContainsClass for target classContainsClass
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
				"The edge type \"classContainsClass\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"classContainsClass\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of classContainsClass
			// explicit initializations of contains for target classContainsClass
			// explicit initializations of classContainsClass for target classContainsClass
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("classContainsClass does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("classContainsClass does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_classContainsClass : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_classContainsClass typeVar = new GRGEN_MODEL.EdgeType_classContainsClass();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_classContainsClass() : base((int) EdgeTypes.@classContainsClass)
		{
		}
		public override string Name { get { return "classContainsClass"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "classContainsClass"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IclassContainsClass"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphsOriginal.@classContainsClass"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@classContainsClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@classContainsClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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

	//
	// Indices
	//

	public class ProgramGraphsOriginalIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public ProgramGraphsOriginalIndexSet(GRGEN_LGSP.LGSPGraph graph)
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

	public sealed class ProgramGraphsOriginalNodeModel : GRGEN_LIBGR.INodeModel
	{
		public ProgramGraphsOriginalNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Entity.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Entity.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Entity.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Entity.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
			};
			GRGEN_MODEL.NodeType_Entity.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Entity.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Entity.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Entity.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_MethodBody.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_MethodBody.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
			};
			GRGEN_MODEL.NodeType_MethodBody.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_MethodBody.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_MethodBody.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_MethodBody.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_MethodBody.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_MethodBody.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_Declaration.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Declaration.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Declaration.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Declaration.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
			};
			GRGEN_MODEL.NodeType_Declaration.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Declaration.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_Declaration.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Declaration.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Entity.typeVar,
			};
			GRGEN_MODEL.NodeType_Class.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Class.typeVar,
			};
			GRGEN_MODEL.NodeType_Class.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Class.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
			};
			GRGEN_MODEL.NodeType_Class.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Declaration.typeVar,
			};
			GRGEN_MODEL.NodeType_Feature.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Feature.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Feature.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Feature.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
			};
			GRGEN_MODEL.NodeType_Feature.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Feature.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
			};
			GRGEN_MODEL.NodeType_Feature.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Feature.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Declaration.typeVar,
			};
			GRGEN_MODEL.NodeType_MethodSignature.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_MethodSignature.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
			};
			GRGEN_MODEL.NodeType_MethodSignature.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_MethodSignature.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_MethodSignature.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_MethodSignature.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_MethodSignature.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
			};
			GRGEN_MODEL.NodeType_MethodSignature.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_MethodSignature.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Feature.typeVar,
			};
			GRGEN_MODEL.NodeType_Attribute.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Attribute.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Attribute.typeVar,
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Attribute.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Attribute.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Attribute.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Attribute.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Attribute.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
			};
			GRGEN_MODEL.NodeType_Attribute.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Attribute.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Feature.typeVar,
			};
			GRGEN_MODEL.NodeType_Constant.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Constant.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Constant.typeVar,
			};
			GRGEN_MODEL.NodeType_Constant.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Constant.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Constant.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Constant.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Constant.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
			};
			GRGEN_MODEL.NodeType_Constant.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Constant.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Attribute.typeVar,
			};
			GRGEN_MODEL.NodeType_Variabel.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Variabel.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Variabel.typeVar,
			};
			GRGEN_MODEL.NodeType_Variabel.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Variabel.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Variabel.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Variabel.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Variabel.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Entity.typeVar,
				GRGEN_MODEL.NodeType_Declaration.typeVar,
				GRGEN_MODEL.NodeType_Feature.typeVar,
				GRGEN_MODEL.NodeType_Attribute.typeVar,
			};
			GRGEN_MODEL.NodeType_Variabel.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Variabel.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Attribute.typeVar,
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
				case "Entity" : return GRGEN_MODEL.NodeType_Entity.typeVar;
				case "MethodBody" : return GRGEN_MODEL.NodeType_MethodBody.typeVar;
				case "Expression" : return GRGEN_MODEL.NodeType_Expression.typeVar;
				case "Declaration" : return GRGEN_MODEL.NodeType_Declaration.typeVar;
				case "Class" : return GRGEN_MODEL.NodeType_Class.typeVar;
				case "Feature" : return GRGEN_MODEL.NodeType_Feature.typeVar;
				case "MethodSignature" : return GRGEN_MODEL.NodeType_MethodSignature.typeVar;
				case "Attribute" : return GRGEN_MODEL.NodeType_Attribute.typeVar;
				case "Constant" : return GRGEN_MODEL.NodeType_Constant.typeVar;
				case "Variabel" : return GRGEN_MODEL.NodeType_Variabel.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_Entity.typeVar,
			GRGEN_MODEL.NodeType_MethodBody.typeVar,
			GRGEN_MODEL.NodeType_Expression.typeVar,
			GRGEN_MODEL.NodeType_Declaration.typeVar,
			GRGEN_MODEL.NodeType_Class.typeVar,
			GRGEN_MODEL.NodeType_Feature.typeVar,
			GRGEN_MODEL.NodeType_MethodSignature.typeVar,
			GRGEN_MODEL.NodeType_Attribute.typeVar,
			GRGEN_MODEL.NodeType_Constant.typeVar,
			GRGEN_MODEL.NodeType_Variabel.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_Entity),
			typeof(GRGEN_MODEL.NodeType_MethodBody),
			typeof(GRGEN_MODEL.NodeType_Expression),
			typeof(GRGEN_MODEL.NodeType_Declaration),
			typeof(GRGEN_MODEL.NodeType_Class),
			typeof(GRGEN_MODEL.NodeType_Feature),
			typeof(GRGEN_MODEL.NodeType_MethodSignature),
			typeof(GRGEN_MODEL.NodeType_Attribute),
			typeof(GRGEN_MODEL.NodeType_Constant),
			typeof(GRGEN_MODEL.NodeType_Variabel),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class ProgramGraphsOriginalEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public ProgramGraphsOriginalEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_contains.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
				GRGEN_MODEL.EdgeType_hasType.typeVar,
				GRGEN_MODEL.EdgeType_bindsTo.typeVar,
				GRGEN_MODEL.EdgeType_uses.typeVar,
				GRGEN_MODEL.EdgeType_writesTo.typeVar,
				GRGEN_MODEL.EdgeType_calls.typeVar,
				GRGEN_MODEL.EdgeType_methodBodyContains.typeVar,
				GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
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
				GRGEN_MODEL.EdgeType_contains.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
				GRGEN_MODEL.EdgeType_hasType.typeVar,
				GRGEN_MODEL.EdgeType_bindsTo.typeVar,
				GRGEN_MODEL.EdgeType_uses.typeVar,
				GRGEN_MODEL.EdgeType_writesTo.typeVar,
				GRGEN_MODEL.EdgeType_calls.typeVar,
				GRGEN_MODEL.EdgeType_methodBodyContains.typeVar,
				GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_contains.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
				GRGEN_MODEL.EdgeType_methodBodyContains.typeVar,
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
			GRGEN_MODEL.EdgeType_contains.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_contains.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_contains.typeVar,
				GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_contains.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_contains.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_contains.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_contains.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_contains.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_contains.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_contains.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_references.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_references.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
				GRGEN_MODEL.EdgeType_hasType.typeVar,
				GRGEN_MODEL.EdgeType_bindsTo.typeVar,
				GRGEN_MODEL.EdgeType_uses.typeVar,
				GRGEN_MODEL.EdgeType_writesTo.typeVar,
				GRGEN_MODEL.EdgeType_calls.typeVar,
			};
			GRGEN_MODEL.EdgeType_references.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_references.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_hasType.typeVar,
				GRGEN_MODEL.EdgeType_bindsTo.typeVar,
				GRGEN_MODEL.EdgeType_uses.typeVar,
				GRGEN_MODEL.EdgeType_writesTo.typeVar,
				GRGEN_MODEL.EdgeType_calls.typeVar,
			};
			GRGEN_MODEL.EdgeType_references.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_references.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_references.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_references.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_hasType.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_hasType.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_hasType.typeVar,
			};
			GRGEN_MODEL.EdgeType_hasType.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_hasType.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_hasType.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_hasType.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_hasType.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_hasType.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_hasType.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_bindsTo.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_bindsTo.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_bindsTo.typeVar,
			};
			GRGEN_MODEL.EdgeType_bindsTo.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_bindsTo.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_bindsTo.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_bindsTo.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_bindsTo.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_bindsTo.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_bindsTo.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_uses.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_uses.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_uses.typeVar,
			};
			GRGEN_MODEL.EdgeType_uses.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_uses.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_uses.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_uses.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_uses.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_uses.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_uses.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_writesTo.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_writesTo.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_writesTo.typeVar,
			};
			GRGEN_MODEL.EdgeType_writesTo.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_writesTo.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_writesTo.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_writesTo.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_writesTo.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_writesTo.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_writesTo.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_calls.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_calls.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_calls.typeVar,
			};
			GRGEN_MODEL.EdgeType_calls.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_calls.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_calls.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_calls.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_calls.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_calls.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_calls.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_references.typeVar,
			};
			GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_methodBodyContains.typeVar,
			};
			GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_methodBodyContains.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_methodBodyContains.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_classContainsClass.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_classContainsClass.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_classContainsClass.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_classContainsClass.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_classContainsClass.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_classContainsClass.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_contains.typeVar,
			};
			GRGEN_MODEL.EdgeType_classContainsClass.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_classContainsClass.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_contains.typeVar,
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
				case "contains" : return GRGEN_MODEL.EdgeType_contains.typeVar;
				case "references" : return GRGEN_MODEL.EdgeType_references.typeVar;
				case "hasType" : return GRGEN_MODEL.EdgeType_hasType.typeVar;
				case "bindsTo" : return GRGEN_MODEL.EdgeType_bindsTo.typeVar;
				case "uses" : return GRGEN_MODEL.EdgeType_uses.typeVar;
				case "writesTo" : return GRGEN_MODEL.EdgeType_writesTo.typeVar;
				case "calls" : return GRGEN_MODEL.EdgeType_calls.typeVar;
				case "methodBodyContains" : return GRGEN_MODEL.EdgeType_methodBodyContains.typeVar;
				case "classContainsClass" : return GRGEN_MODEL.EdgeType_classContainsClass.typeVar;
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
			GRGEN_MODEL.EdgeType_contains.typeVar,
			GRGEN_MODEL.EdgeType_references.typeVar,
			GRGEN_MODEL.EdgeType_hasType.typeVar,
			GRGEN_MODEL.EdgeType_bindsTo.typeVar,
			GRGEN_MODEL.EdgeType_uses.typeVar,
			GRGEN_MODEL.EdgeType_writesTo.typeVar,
			GRGEN_MODEL.EdgeType_calls.typeVar,
			GRGEN_MODEL.EdgeType_methodBodyContains.typeVar,
			GRGEN_MODEL.EdgeType_classContainsClass.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_contains),
			typeof(GRGEN_MODEL.EdgeType_references),
			typeof(GRGEN_MODEL.EdgeType_hasType),
			typeof(GRGEN_MODEL.EdgeType_bindsTo),
			typeof(GRGEN_MODEL.EdgeType_uses),
			typeof(GRGEN_MODEL.EdgeType_writesTo),
			typeof(GRGEN_MODEL.EdgeType_calls),
			typeof(GRGEN_MODEL.EdgeType_methodBodyContains),
			typeof(GRGEN_MODEL.EdgeType_classContainsClass),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel (LGSPGraphModel) implementation
	//
	public sealed class ProgramGraphsOriginalGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public ProgramGraphsOriginalGraphModel()
		{
			FullyInitializeExternalTypes();
		}

		private ProgramGraphsOriginalNodeModel nodeModel = new ProgramGraphsOriginalNodeModel();
		private ProgramGraphsOriginalEdgeModel edgeModel = new ProgramGraphsOriginalEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_contains.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 2147483647, 0, 1, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_references.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Declaration.typeVar, 0, 2147483647, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Feature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 1, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_bindsTo.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_uses.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_writesTo.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Variabel.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_calls.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_methodBodyContains.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 2147483647, 0, 1, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_classContainsClass.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 0, 2147483647, 0, 1, false),
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new ProgramGraphsOriginalIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ProgramGraphsOriginalIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "ProgramGraphsOriginal"; } }
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

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object };
		public override GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
		}

		public override string MD5Hash { get { return "367e59eecca3afadcdac0347954e2ede"; } }
	}

	//
	// IGraph (LGSPGraph) / IGraphModel implementation
	//
	public class ProgramGraphsOriginalGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public ProgramGraphsOriginalGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodBody CreateNodeMethodBody()
		{
			return GRGEN_MODEL.@MethodBody.CreateNode(this);
		}

		public GRGEN_MODEL.@Expression CreateNodeExpression()
		{
			return GRGEN_MODEL.@Expression.CreateNode(this);
		}

		public GRGEN_MODEL.@Class CreateNodeClass()
		{
			return GRGEN_MODEL.@Class.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodSignature CreateNodeMethodSignature()
		{
			return GRGEN_MODEL.@MethodSignature.CreateNode(this);
		}

		public GRGEN_MODEL.@Constant CreateNodeConstant()
		{
			return GRGEN_MODEL.@Constant.CreateNode(this);
		}

		public GRGEN_MODEL.@Variabel CreateNodeVariabel()
		{
			return GRGEN_MODEL.@Variabel.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@contains.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@references.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@hasType.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@bindsTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@uses.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@writesTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@calls.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@methodBodyContains CreateEdgemethodBodyContains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@methodBodyContains.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@classContainsClass CreateEdgeclassContainsClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@classContainsClass.CreateEdge(this, source, target);
		}

		private ProgramGraphsOriginalNodeModel nodeModel = new ProgramGraphsOriginalNodeModel();
		private ProgramGraphsOriginalEdgeModel edgeModel = new ProgramGraphsOriginalEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_contains.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 2147483647, 0, 1, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_references.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Declaration.typeVar, 0, 2147483647, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Feature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 1, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_bindsTo.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_uses.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_writesTo.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Variabel.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_calls.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_methodBodyContains.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 2147483647, 0, 1, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_classContainsClass.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 0, 2147483647, 0, 1, false),
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new ProgramGraphsOriginalIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ProgramGraphsOriginalIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "ProgramGraphsOriginal"; } }
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
			reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l'
			return null;
		}
		public string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			Console.WriteLine("Warning: Exporting attribute of object type to null");
			return "null";
		}
		public string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return attribute!=null ? attribute.ToString() : "null";
		}
		public void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			Console.Write("Ignoring: ");
			Console.WriteLine(line);
		}
		public GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return null;
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object };
		public GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
		}

		public bool IsEqualClassDefined { get { return false; } }
		public bool IsLowerClassDefined { get { return false; } }
		public bool IsEqual(object this_, object that)
		{
			return this_ == that;
		}
		public bool IsLower(object this_, object that)
		{
			return this_ == that;
		}

		public string MD5Hash { get { return "367e59eecca3afadcdac0347954e2ede"; } }
	}

	//
	// INamedGraph (LGSPNamedGraph) / IGraphModel implementation
	//
	public class ProgramGraphsOriginalNamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel
	{
		public ProgramGraphsOriginalNamedGraph() : base(GetNextGraphName())
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

		public GRGEN_MODEL.@MethodBody CreateNodeMethodBody()
		{
			return GRGEN_MODEL.@MethodBody.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodBody CreateNodeMethodBody(string nodeName)
		{
			return GRGEN_MODEL.@MethodBody.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Expression CreateNodeExpression()
		{
			return GRGEN_MODEL.@Expression.CreateNode(this);
		}

		public GRGEN_MODEL.@Expression CreateNodeExpression(string nodeName)
		{
			return GRGEN_MODEL.@Expression.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Class CreateNodeClass()
		{
			return GRGEN_MODEL.@Class.CreateNode(this);
		}

		public GRGEN_MODEL.@Class CreateNodeClass(string nodeName)
		{
			return GRGEN_MODEL.@Class.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@MethodSignature CreateNodeMethodSignature()
		{
			return GRGEN_MODEL.@MethodSignature.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodSignature CreateNodeMethodSignature(string nodeName)
		{
			return GRGEN_MODEL.@MethodSignature.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Constant CreateNodeConstant()
		{
			return GRGEN_MODEL.@Constant.CreateNode(this);
		}

		public GRGEN_MODEL.@Constant CreateNodeConstant(string nodeName)
		{
			return GRGEN_MODEL.@Constant.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Variabel CreateNodeVariabel()
		{
			return GRGEN_MODEL.@Variabel.CreateNode(this);
		}

		public GRGEN_MODEL.@Variabel CreateNodeVariabel(string nodeName)
		{
			return GRGEN_MODEL.@Variabel.CreateNode(this, nodeName);
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

		public @GRGEN_MODEL.@contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@contains.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@contains.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@references.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@references.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@hasType.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@hasType.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@bindsTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@bindsTo.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@uses.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@uses.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@writesTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@writesTo.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@calls.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@calls.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@methodBodyContains CreateEdgemethodBodyContains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@methodBodyContains.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@methodBodyContains CreateEdgemethodBodyContains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@methodBodyContains.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@classContainsClass CreateEdgeclassContainsClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@classContainsClass.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@classContainsClass CreateEdgeclassContainsClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@classContainsClass.CreateEdge(this, source, target, edgeName);
		}

		private ProgramGraphsOriginalNodeModel nodeModel = new ProgramGraphsOriginalNodeModel();
		private ProgramGraphsOriginalEdgeModel edgeModel = new ProgramGraphsOriginalEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_contains.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 2147483647, 0, 1, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_references.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Declaration.typeVar, 0, 2147483647, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Feature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 1, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_bindsTo.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_uses.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_writesTo.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Variabel.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_calls.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 0, 2147483647, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_methodBodyContains.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 2147483647, 0, 1, false),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_classContainsClass.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 0, 2147483647, 0, 1, false),
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new ProgramGraphsOriginalIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((ProgramGraphsOriginalIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "ProgramGraphsOriginal"; } }
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
			reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l'
			return null;
		}
		public string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			Console.WriteLine("Warning: Exporting attribute of object type to null");
			return "null";
		}
		public string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return attribute!=null ? attribute.ToString() : "null";
		}
		public void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			Console.Write("Ignoring: ");
			Console.WriteLine(line);
		}
		public GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return null;
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object };
		public GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
		}

		public bool IsEqualClassDefined { get { return false; } }
		public bool IsLowerClassDefined { get { return false; } }
		public bool IsEqual(object this_, object that)
		{
			return this_ == that;
		}
		public bool IsLower(object this_, object that)
		{
			return this_ == that;
		}

		public string MD5Hash { get { return "367e59eecca3afadcdac0347954e2ede"; } }
	}
}
