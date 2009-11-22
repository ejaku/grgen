// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ProgramGraphs\ProgramGraphs.grg" on Sun Nov 22 13:11:39 CET 2009

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_ProgramGraphs
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_ProgramGraphs;
	//
	// Enums
	//

	public class Enums
	{
	}

	//
	// Node types
	//

	public enum NodeTypes { @Node, @Entity, @MethodBody, @Expression, @Declaration, @Class, @Feature, @MethodSignature, @Attribute, @Constant, @Variabel };

	// *** Node Node ***


	public sealed class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Node[] pool = new GRGEN_MODEL.@Node[10];
		
		static @Node() {
		}
		
		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, map/set creation of Node
		}

		public static GRGEN_MODEL.NodeType_Node TypeInstance { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Node(this); }

		private @Node(GRGEN_MODEL.@Node oldElem) : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
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
				// implicit initialization, map/set creation of Node
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of Node
			}
			graph.AddNode(node, varName);
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
				"The node type \"Node\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Node\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Node
		}
	}

	public sealed class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Node typeVar = new GRGEN_MODEL.NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Node();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class NodeType_Entity : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Entity typeVar = new GRGEN_MODEL.NodeType_Entity();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Entity() : base((int) NodeTypes.@Entity)
		{
		}
		public override string Name { get { return "Entity"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IEntity"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Entity cannot be instantiated!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @MethodBody : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IMethodBody
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
			// implicit initialization, map/set creation of MethodBody
			// explicit initializations of Entity for target MethodBody
			// explicit initializations of MethodBody for target MethodBody
		}

		public static GRGEN_MODEL.NodeType_MethodBody TypeInstance { get { return GRGEN_MODEL.NodeType_MethodBody.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@MethodBody(this); }

		private @MethodBody(GRGEN_MODEL.@MethodBody oldElem) : base(GRGEN_MODEL.NodeType_MethodBody.typeVar)
		{
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
				// implicit initialization, map/set creation of MethodBody
				// explicit initializations of Entity for target MethodBody
				// explicit initializations of MethodBody for target MethodBody
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@MethodBody CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of MethodBody
				// explicit initializations of Entity for target MethodBody
				// explicit initializations of MethodBody for target MethodBody
			}
			graph.AddNode(node, varName);
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
				"The node type \"MethodBody\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"MethodBody\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of MethodBody
			// explicit initializations of Entity for target MethodBody
			// explicit initializations of MethodBody for target MethodBody
		}
	}

	public sealed class NodeType_MethodBody : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_MethodBody typeVar = new GRGEN_MODEL.NodeType_MethodBody();
		public static bool[] isA = new bool[] { true, true, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, };
		public NodeType_MethodBody() : base((int) NodeTypes.@MethodBody)
		{
		}
		public override string Name { get { return "MethodBody"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IMethodBody"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@MethodBody"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@MethodBody();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @Expression : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IExpression
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
			// implicit initialization, map/set creation of Expression
			// explicit initializations of Entity for target Expression
			// explicit initializations of Expression for target Expression
		}

		public static GRGEN_MODEL.NodeType_Expression TypeInstance { get { return GRGEN_MODEL.NodeType_Expression.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Expression(this); }

		private @Expression(GRGEN_MODEL.@Expression oldElem) : base(GRGEN_MODEL.NodeType_Expression.typeVar)
		{
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
				// implicit initialization, map/set creation of Expression
				// explicit initializations of Entity for target Expression
				// explicit initializations of Expression for target Expression
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Expression CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of Expression
				// explicit initializations of Entity for target Expression
				// explicit initializations of Expression for target Expression
			}
			graph.AddNode(node, varName);
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
				"The node type \"Expression\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Expression\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Expression
			// explicit initializations of Entity for target Expression
			// explicit initializations of Expression for target Expression
		}
	}

	public sealed class NodeType_Expression : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Expression typeVar = new GRGEN_MODEL.NodeType_Expression();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, };
		public NodeType_Expression() : base((int) NodeTypes.@Expression)
		{
		}
		public override string Name { get { return "Expression"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IExpression"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@Expression"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Expression();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class NodeType_Declaration : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Declaration typeVar = new GRGEN_MODEL.NodeType_Declaration();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, true, };
		public NodeType_Declaration() : base((int) NodeTypes.@Declaration)
		{
		}
		public override string Name { get { return "Declaration"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IDeclaration"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Declaration cannot be instantiated!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @Class : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IClass
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
			// implicit initialization, map/set creation of Class
			// explicit initializations of Entity for target Class
			// explicit initializations of Declaration for target Class
			// explicit initializations of Class for target Class
		}

		public static GRGEN_MODEL.NodeType_Class TypeInstance { get { return GRGEN_MODEL.NodeType_Class.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Class(this); }

		private @Class(GRGEN_MODEL.@Class oldElem) : base(GRGEN_MODEL.NodeType_Class.typeVar)
		{
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
				// implicit initialization, map/set creation of Class
				// explicit initializations of Entity for target Class
				// explicit initializations of Declaration for target Class
				// explicit initializations of Class for target Class
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Class CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of Class
				// explicit initializations of Entity for target Class
				// explicit initializations of Declaration for target Class
				// explicit initializations of Class for target Class
			}
			graph.AddNode(node, varName);
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
				"The node type \"Class\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Class\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Class
			// explicit initializations of Entity for target Class
			// explicit initializations of Declaration for target Class
			// explicit initializations of Class for target Class
		}
	}

	public sealed class NodeType_Class : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Class typeVar = new GRGEN_MODEL.NodeType_Class();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, };
		public NodeType_Class() : base((int) NodeTypes.@Class)
		{
		}
		public override string Name { get { return "Class"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IClass"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@Class"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Class();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class NodeType_Feature : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Feature typeVar = new GRGEN_MODEL.NodeType_Feature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, true, true, true, true, };
		public NodeType_Feature() : base((int) NodeTypes.@Feature)
		{
		}
		public override string Name { get { return "Feature"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IFeature"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Feature cannot be instantiated!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @MethodSignature : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IMethodSignature
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
			// implicit initialization, map/set creation of MethodSignature
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
				// implicit initialization, map/set creation of MethodSignature
				// explicit initializations of Entity for target MethodSignature
				// explicit initializations of Declaration for target MethodSignature
				// explicit initializations of Feature for target MethodSignature
				// explicit initializations of MethodSignature for target MethodSignature
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@MethodSignature CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of MethodSignature
				// explicit initializations of Entity for target MethodSignature
				// explicit initializations of Declaration for target MethodSignature
				// explicit initializations of Feature for target MethodSignature
				// explicit initializations of MethodSignature for target MethodSignature
			}
			graph.AddNode(node, varName);
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
				"The node type \"MethodSignature\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"MethodSignature\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of MethodSignature
			// explicit initializations of Entity for target MethodSignature
			// explicit initializations of Declaration for target MethodSignature
			// explicit initializations of Feature for target MethodSignature
			// explicit initializations of MethodSignature for target MethodSignature
		}
	}

	public sealed class NodeType_MethodSignature : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_MethodSignature typeVar = new GRGEN_MODEL.NodeType_MethodSignature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, };
		public NodeType_MethodSignature() : base((int) NodeTypes.@MethodSignature)
		{
		}
		public override string Name { get { return "MethodSignature"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IMethodSignature"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@MethodSignature"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@MethodSignature();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class NodeType_Attribute : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Attribute typeVar = new GRGEN_MODEL.NodeType_Attribute();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, true, true, };
		public NodeType_Attribute() : base((int) NodeTypes.@Attribute)
		{
		}
		public override string Name { get { return "Attribute"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IAttribute"; } }
		public override string NodeClassName { get { return null; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Attribute cannot be instantiated!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @Constant : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IConstant
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
			// implicit initialization, map/set creation of Constant
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
				// implicit initialization, map/set creation of Constant
				// explicit initializations of Entity for target Constant
				// explicit initializations of Declaration for target Constant
				// explicit initializations of Feature for target Constant
				// explicit initializations of Attribute for target Constant
				// explicit initializations of Constant for target Constant
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Constant CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of Constant
				// explicit initializations of Entity for target Constant
				// explicit initializations of Declaration for target Constant
				// explicit initializations of Feature for target Constant
				// explicit initializations of Attribute for target Constant
				// explicit initializations of Constant for target Constant
			}
			graph.AddNode(node, varName);
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
				"The node type \"Constant\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Constant\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Constant
			// explicit initializations of Entity for target Constant
			// explicit initializations of Declaration for target Constant
			// explicit initializations of Feature for target Constant
			// explicit initializations of Attribute for target Constant
			// explicit initializations of Constant for target Constant
		}
	}

	public sealed class NodeType_Constant : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Constant typeVar = new GRGEN_MODEL.NodeType_Constant();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, };
		public NodeType_Constant() : base((int) NodeTypes.@Constant)
		{
		}
		public override string Name { get { return "Constant"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IConstant"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@Constant"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Constant();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @Variabel : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IVariabel
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
			// implicit initialization, map/set creation of Variabel
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
				// implicit initialization, map/set creation of Variabel
				// explicit initializations of Entity for target Variabel
				// explicit initializations of Declaration for target Variabel
				// explicit initializations of Feature for target Variabel
				// explicit initializations of Attribute for target Variabel
				// explicit initializations of Variabel for target Variabel
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Variabel CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
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
				// implicit initialization, map/set creation of Variabel
				// explicit initializations of Entity for target Variabel
				// explicit initializations of Declaration for target Variabel
				// explicit initializations of Feature for target Variabel
				// explicit initializations of Attribute for target Variabel
				// explicit initializations of Variabel for target Variabel
			}
			graph.AddNode(node, varName);
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
				"The node type \"Variabel\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Variabel\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Variabel
			// explicit initializations of Entity for target Variabel
			// explicit initializations of Declaration for target Variabel
			// explicit initializations of Feature for target Variabel
			// explicit initializations of Attribute for target Variabel
			// explicit initializations of Variabel for target Variabel
		}
	}

	public sealed class NodeType_Variabel : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Variabel typeVar = new GRGEN_MODEL.NodeType_Variabel();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, };
		public NodeType_Variabel() : base((int) NodeTypes.@Variabel)
		{
		}
		public override string Name { get { return "Variabel"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IVariabel"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@Variabel"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Variabel();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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
	// Node model
	//

	public sealed class ProgramGraphsNodeModel : GRGEN_LIBGR.INodeModel
	{
		public ProgramGraphsNodeModel()
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
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @contains, @references, @hasType, @bindsTo, @uses, @writesTo, @calls, @containedInClass, @containedInMethodBody };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override string Name { get { return "AEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return null; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Arbitrary; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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


	public sealed class @Edge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Edge[] pool = new GRGEN_MODEL.@Edge[10];
		
		static @Edge() {
		}
		
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, map/set creation of Edge
		}

		public static GRGEN_MODEL.EdgeType_Edge TypeInstance { get { return GRGEN_MODEL.EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @Edge(GRGEN_MODEL.@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, newSource, newTarget)
		{
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
				// implicit initialization, map/set creation of Edge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of Edge
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"Edge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"Edge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Edge
		}
	}

	public sealed class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_Edge typeVar = new GRGEN_MODEL.EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@Edge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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


	public sealed class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@UEdge[] pool = new GRGEN_MODEL.@UEdge[10];
		
		static @UEdge() {
		}
		
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, map/set creation of UEdge
		}

		public static GRGEN_MODEL.EdgeType_UEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @UEdge(GRGEN_MODEL.@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, newSource, newTarget)
		{
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
				// implicit initialization, map/set creation of UEdge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of UEdge
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"UEdge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"UEdge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of UEdge
		}
	}

	public sealed class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_UEdge typeVar = new GRGEN_MODEL.EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@UEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Undirected; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public interface Icontains : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @contains : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Icontains
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
			// implicit initialization, map/set creation of contains
			// explicit initializations of contains for target contains
		}

		public static GRGEN_MODEL.EdgeType_contains TypeInstance { get { return GRGEN_MODEL.EdgeType_contains.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@contains(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @contains(GRGEN_MODEL.@contains oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_contains.typeVar, newSource, newTarget)
		{
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
				// implicit initialization, map/set creation of contains
				// explicit initializations of contains for target contains
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@contains CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of contains
				// explicit initializations of contains for target contains
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"contains\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"contains\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of contains
			// explicit initializations of contains for target contains
		}
	}

	public sealed class EdgeType_contains : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_contains typeVar = new GRGEN_MODEL.EdgeType_contains();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, };
		public EdgeType_contains() : base((int) EdgeTypes.@contains)
		{
		}
		public override string Name { get { return "contains"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.Icontains"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@contains"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@contains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public interface Ireferences : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @references : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Ireferences
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
			// implicit initialization, map/set creation of references
			// explicit initializations of references for target references
		}

		public static GRGEN_MODEL.EdgeType_references TypeInstance { get { return GRGEN_MODEL.EdgeType_references.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@references(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @references(GRGEN_MODEL.@references oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_references.typeVar, newSource, newTarget)
		{
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
				// implicit initialization, map/set creation of references
				// explicit initializations of references for target references
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@references CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of references
				// explicit initializations of references for target references
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"references\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"references\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of references
			// explicit initializations of references for target references
		}
	}

	public sealed class EdgeType_references : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_references typeVar = new GRGEN_MODEL.EdgeType_references();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, false, false, };
		public EdgeType_references() : base((int) EdgeTypes.@references)
		{
		}
		public override string Name { get { return "references"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.Ireferences"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@references"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@references((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @hasType : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IhasType
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
			// implicit initialization, map/set creation of hasType
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
				// implicit initialization, map/set creation of hasType
				// explicit initializations of references for target hasType
				// explicit initializations of hasType for target hasType
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@hasType CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of hasType
				// explicit initializations of references for target hasType
				// explicit initializations of hasType for target hasType
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"hasType\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"hasType\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of hasType
			// explicit initializations of references for target hasType
			// explicit initializations of hasType for target hasType
		}
	}

	public sealed class EdgeType_hasType : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_hasType typeVar = new GRGEN_MODEL.EdgeType_hasType();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, };
		public EdgeType_hasType() : base((int) EdgeTypes.@hasType)
		{
		}
		public override string Name { get { return "hasType"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IhasType"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@hasType"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@hasType((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @bindsTo : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IbindsTo
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
			// implicit initialization, map/set creation of bindsTo
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
				// implicit initialization, map/set creation of bindsTo
				// explicit initializations of references for target bindsTo
				// explicit initializations of bindsTo for target bindsTo
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@bindsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of bindsTo
				// explicit initializations of references for target bindsTo
				// explicit initializations of bindsTo for target bindsTo
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"bindsTo\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"bindsTo\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of bindsTo
			// explicit initializations of references for target bindsTo
			// explicit initializations of bindsTo for target bindsTo
		}
	}

	public sealed class EdgeType_bindsTo : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_bindsTo typeVar = new GRGEN_MODEL.EdgeType_bindsTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, };
		public EdgeType_bindsTo() : base((int) EdgeTypes.@bindsTo)
		{
		}
		public override string Name { get { return "bindsTo"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IbindsTo"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@bindsTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@bindsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @uses : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iuses
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
			// implicit initialization, map/set creation of uses
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
				// implicit initialization, map/set creation of uses
				// explicit initializations of references for target uses
				// explicit initializations of uses for target uses
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@uses CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of uses
				// explicit initializations of references for target uses
				// explicit initializations of uses for target uses
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"uses\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"uses\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of uses
			// explicit initializations of references for target uses
			// explicit initializations of uses for target uses
		}
	}

	public sealed class EdgeType_uses : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_uses typeVar = new GRGEN_MODEL.EdgeType_uses();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, };
		public EdgeType_uses() : base((int) EdgeTypes.@uses)
		{
		}
		public override string Name { get { return "uses"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.Iuses"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@uses"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@uses((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @writesTo : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IwritesTo
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
			// implicit initialization, map/set creation of writesTo
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
				// implicit initialization, map/set creation of writesTo
				// explicit initializations of references for target writesTo
				// explicit initializations of writesTo for target writesTo
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@writesTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of writesTo
				// explicit initializations of references for target writesTo
				// explicit initializations of writesTo for target writesTo
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"writesTo\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"writesTo\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of writesTo
			// explicit initializations of references for target writesTo
			// explicit initializations of writesTo for target writesTo
		}
	}

	public sealed class EdgeType_writesTo : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_writesTo typeVar = new GRGEN_MODEL.EdgeType_writesTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, };
		public EdgeType_writesTo() : base((int) EdgeTypes.@writesTo)
		{
		}
		public override string Name { get { return "writesTo"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IwritesTo"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@writesTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@writesTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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

	public sealed class @calls : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Icalls
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
			// implicit initialization, map/set creation of calls
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
				// implicit initialization, map/set creation of calls
				// explicit initializations of references for target calls
				// explicit initializations of calls for target calls
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@calls CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
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
				// implicit initialization, map/set creation of calls
				// explicit initializations of references for target calls
				// explicit initializations of calls for target calls
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"calls\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"calls\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of calls
			// explicit initializations of references for target calls
			// explicit initializations of calls for target calls
		}
	}

	public sealed class EdgeType_calls : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_calls typeVar = new GRGEN_MODEL.EdgeType_calls();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, };
		public EdgeType_calls() : base((int) EdgeTypes.@calls)
		{
		}
		public override string Name { get { return "calls"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.Icalls"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@calls"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@calls((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@calls((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge containedInClass ***

	public interface IcontainedInClass : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @containedInClass : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IcontainedInClass
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@containedInClass[] pool = new GRGEN_MODEL.@containedInClass[10];
		
		// explicit initializations of containedInClass for target containedInClass
		// implicit initializations of containedInClass for target containedInClass
		static @containedInClass() {
		}
		
		public @containedInClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_containedInClass.typeVar, source, target)
		{
			// implicit initialization, map/set creation of containedInClass
			// explicit initializations of containedInClass for target containedInClass
		}

		public static GRGEN_MODEL.EdgeType_containedInClass TypeInstance { get { return GRGEN_MODEL.EdgeType_containedInClass.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@containedInClass(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @containedInClass(GRGEN_MODEL.@containedInClass oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_containedInClass.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@containedInClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@containedInClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@containedInClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set creation of containedInClass
				// explicit initializations of containedInClass for target containedInClass
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@containedInClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@containedInClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@containedInClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set creation of containedInClass
				// explicit initializations of containedInClass for target containedInClass
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"containedInClass\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"containedInClass\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of containedInClass
			// explicit initializations of containedInClass for target containedInClass
		}
	}

	public sealed class EdgeType_containedInClass : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_containedInClass typeVar = new GRGEN_MODEL.EdgeType_containedInClass();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, };
		public EdgeType_containedInClass() : base((int) EdgeTypes.@containedInClass)
		{
		}
		public override string Name { get { return "containedInClass"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IcontainedInClass"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@containedInClass"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@containedInClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@containedInClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge containedInMethodBody ***

	public interface IcontainedInMethodBody : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @containedInMethodBody : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IcontainedInMethodBody
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@containedInMethodBody[] pool = new GRGEN_MODEL.@containedInMethodBody[10];
		
		// explicit initializations of containedInMethodBody for target containedInMethodBody
		// implicit initializations of containedInMethodBody for target containedInMethodBody
		static @containedInMethodBody() {
		}
		
		public @containedInMethodBody(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar, source, target)
		{
			// implicit initialization, map/set creation of containedInMethodBody
			// explicit initializations of containedInMethodBody for target containedInMethodBody
		}

		public static GRGEN_MODEL.EdgeType_containedInMethodBody TypeInstance { get { return GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@containedInMethodBody(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @containedInMethodBody(GRGEN_MODEL.@containedInMethodBody oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@containedInMethodBody CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@containedInMethodBody edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@containedInMethodBody(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set creation of containedInMethodBody
				// explicit initializations of containedInMethodBody for target containedInMethodBody
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@containedInMethodBody CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@containedInMethodBody edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@containedInMethodBody(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set creation of containedInMethodBody
				// explicit initializations of containedInMethodBody for target containedInMethodBody
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"containedInMethodBody\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"containedInMethodBody\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of containedInMethodBody
			// explicit initializations of containedInMethodBody for target containedInMethodBody
		}
	}

	public sealed class EdgeType_containedInMethodBody : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_containedInMethodBody typeVar = new GRGEN_MODEL.EdgeType_containedInMethodBody();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, };
		public EdgeType_containedInMethodBody() : base((int) EdgeTypes.@containedInMethodBody)
		{
		}
		public override string Name { get { return "containedInMethodBody"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.IcontainedInMethodBody"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_ProgramGraphs.@containedInMethodBody"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@containedInMethodBody((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@containedInMethodBody((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class ProgramGraphsEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public ProgramGraphsEdgeModel()
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
				GRGEN_MODEL.EdgeType_containedInClass.typeVar,
				GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar,
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
				GRGEN_MODEL.EdgeType_containedInClass.typeVar,
				GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_contains.typeVar,
				GRGEN_MODEL.EdgeType_references.typeVar,
				GRGEN_MODEL.EdgeType_containedInClass.typeVar,
				GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar,
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
			};
			GRGEN_MODEL.EdgeType_contains.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_contains.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
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
			GRGEN_MODEL.EdgeType_containedInClass.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_containedInClass.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_containedInClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_containedInClass.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_containedInClass.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_containedInClass.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_containedInClass.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_containedInClass.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_containedInClass.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_containedInClass.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar,
			};
			GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
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
				case "contains" : return GRGEN_MODEL.EdgeType_contains.typeVar;
				case "references" : return GRGEN_MODEL.EdgeType_references.typeVar;
				case "hasType" : return GRGEN_MODEL.EdgeType_hasType.typeVar;
				case "bindsTo" : return GRGEN_MODEL.EdgeType_bindsTo.typeVar;
				case "uses" : return GRGEN_MODEL.EdgeType_uses.typeVar;
				case "writesTo" : return GRGEN_MODEL.EdgeType_writesTo.typeVar;
				case "calls" : return GRGEN_MODEL.EdgeType_calls.typeVar;
				case "containedInClass" : return GRGEN_MODEL.EdgeType_containedInClass.typeVar;
				case "containedInMethodBody" : return GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar;
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
			GRGEN_MODEL.EdgeType_containedInClass.typeVar,
			GRGEN_MODEL.EdgeType_containedInMethodBody.typeVar,
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
			typeof(GRGEN_MODEL.EdgeType_containedInClass),
			typeof(GRGEN_MODEL.EdgeType_containedInMethodBody),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class ProgramGraphsGraphModel : GRGEN_LIBGR.IGraphModel
	{
		private ProgramGraphsNodeModel nodeModel = new ProgramGraphsNodeModel();
		private ProgramGraphsEdgeModel edgeModel = new ProgramGraphsEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_contains.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_references.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Declaration.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Feature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 1, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_bindsTo.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_uses.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_writesTo.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Variabel.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_calls.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "ProgramGraphs"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "e6271fc2f2794368b53b1fb118947e8d"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class ProgramGraphs : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public ProgramGraphs() : base(GetNextGraphName())
		{
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode(string varName)
		{
			return GRGEN_MODEL.@Node.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@MethodBody CreateNodeMethodBody()
		{
			return GRGEN_MODEL.@MethodBody.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodBody CreateNodeMethodBody(string varName)
		{
			return GRGEN_MODEL.@MethodBody.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Expression CreateNodeExpression()
		{
			return GRGEN_MODEL.@Expression.CreateNode(this);
		}

		public GRGEN_MODEL.@Expression CreateNodeExpression(string varName)
		{
			return GRGEN_MODEL.@Expression.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Class CreateNodeClass()
		{
			return GRGEN_MODEL.@Class.CreateNode(this);
		}

		public GRGEN_MODEL.@Class CreateNodeClass(string varName)
		{
			return GRGEN_MODEL.@Class.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@MethodSignature CreateNodeMethodSignature()
		{
			return GRGEN_MODEL.@MethodSignature.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodSignature CreateNodeMethodSignature(string varName)
		{
			return GRGEN_MODEL.@MethodSignature.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Constant CreateNodeConstant()
		{
			return GRGEN_MODEL.@Constant.CreateNode(this);
		}

		public GRGEN_MODEL.@Constant CreateNodeConstant(string varName)
		{
			return GRGEN_MODEL.@Constant.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Variabel CreateNodeVariabel()
		{
			return GRGEN_MODEL.@Variabel.CreateNode(this);
		}

		public GRGEN_MODEL.@Variabel CreateNodeVariabel(string varName)
		{
			return GRGEN_MODEL.@Variabel.CreateNode(this, varName);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@contains.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@contains.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@references.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@references.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@hasType.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@hasType.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@bindsTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@bindsTo.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@uses.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@uses.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@writesTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@writesTo.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@calls.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@calls.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@containedInClass CreateEdgecontainedInClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@containedInClass.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@containedInClass CreateEdgecontainedInClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@containedInClass.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@containedInMethodBody CreateEdgecontainedInMethodBody(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@containedInMethodBody.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@containedInMethodBody CreateEdgecontainedInMethodBody(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@containedInMethodBody.CreateEdge(this, source, target, varName);
		}

		private ProgramGraphsNodeModel nodeModel = new ProgramGraphsNodeModel();
		private ProgramGraphsEdgeModel edgeModel = new ProgramGraphsEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_contains.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_references.typeVar, GRGEN_MODEL.NodeType_Entity.typeVar, GRGEN_MODEL.NodeType_Declaration.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_hasType.typeVar, GRGEN_MODEL.NodeType_Feature.typeVar, GRGEN_MODEL.NodeType_Class.typeVar, 1, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_bindsTo.typeVar, GRGEN_MODEL.NodeType_MethodBody.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_uses.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Attribute.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_writesTo.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_Variabel.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_calls.typeVar, GRGEN_MODEL.NodeType_Expression.typeVar, GRGEN_MODEL.NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "ProgramGraphs"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "e6271fc2f2794368b53b1fb118947e8d"; } }
	}
}
