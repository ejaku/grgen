// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ProgramGraphs\ProgramGraphs.grg" on Tue Oct 21 13:43:34 CEST 2008

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_ProgramGraphs
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

	public enum NodeTypes { @Node, @Entity, @MethodBody, @Expression, @Declaration, @Class, @Feature, @MethodSignature, @Attribute, @Constant, @Variabel };

	// *** Node Node ***


	public sealed class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static @Node[] pool = new @Node[10];
		public @Node() : base(NodeType_Node.typeVar)
		{
		}

		public static NodeType_Node TypeInstance { get { return NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Node(this); }

		private @Node(@Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public static @Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Node CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Node();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Node();
		}

	}

	// *** Node Entity ***

	public interface IEntity : GRGEN_LIBGR.INode
	{
	}

	public sealed class NodeType_Entity : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Entity typeVar = new NodeType_Entity();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Entity() : base((int) NodeTypes.@Entity)
		{
		}
		public override String Name { get { return "Entity"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Entity cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
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

	public sealed class @MethodBody : GRGEN_LGSP.LGSPNode, IMethodBody
	{
		private static int poolLevel = 0;
		private static @MethodBody[] pool = new @MethodBody[10];
		public @MethodBody() : base(NodeType_MethodBody.typeVar)
		{
		}

		public static NodeType_MethodBody TypeInstance { get { return NodeType_MethodBody.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @MethodBody(this); }

		private @MethodBody(@MethodBody oldElem) : base(NodeType_MethodBody.typeVar)
		{
		}
		public static @MethodBody CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@MethodBody node;
			if(poolLevel == 0)
				node = new @MethodBody();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @MethodBody CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@MethodBody node;
			if(poolLevel == 0)
				node = new @MethodBody();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_MethodBody : GRGEN_LIBGR.NodeType
	{
		public static NodeType_MethodBody typeVar = new NodeType_MethodBody();
		public static bool[] isA = new bool[] { true, true, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, };
		public NodeType_MethodBody() : base((int) NodeTypes.@MethodBody)
		{
		}
		public override String Name { get { return "MethodBody"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @MethodBody();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @MethodBody();
		}

	}

	// *** Node Expression ***

	public interface IExpression : IEntity
	{
	}

	public sealed class @Expression : GRGEN_LGSP.LGSPNode, IExpression
	{
		private static int poolLevel = 0;
		private static @Expression[] pool = new @Expression[10];
		public @Expression() : base(NodeType_Expression.typeVar)
		{
		}

		public static NodeType_Expression TypeInstance { get { return NodeType_Expression.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Expression(this); }

		private @Expression(@Expression oldElem) : base(NodeType_Expression.typeVar)
		{
		}
		public static @Expression CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Expression node;
			if(poolLevel == 0)
				node = new @Expression();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Expression CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Expression node;
			if(poolLevel == 0)
				node = new @Expression();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_Expression : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Expression typeVar = new NodeType_Expression();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, };
		public NodeType_Expression() : base((int) NodeTypes.@Expression)
		{
		}
		public override String Name { get { return "Expression"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Expression();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Expression();
		}

	}

	// *** Node Declaration ***

	public interface IDeclaration : IEntity
	{
	}

	public sealed class NodeType_Declaration : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Declaration typeVar = new NodeType_Declaration();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, true, };
		public NodeType_Declaration() : base((int) NodeTypes.@Declaration)
		{
		}
		public override String Name { get { return "Declaration"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Declaration cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
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

	public sealed class @Class : GRGEN_LGSP.LGSPNode, IClass
	{
		private static int poolLevel = 0;
		private static @Class[] pool = new @Class[10];
		public @Class() : base(NodeType_Class.typeVar)
		{
		}

		public static NodeType_Class TypeInstance { get { return NodeType_Class.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Class(this); }

		private @Class(@Class oldElem) : base(NodeType_Class.typeVar)
		{
		}
		public static @Class CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Class node;
			if(poolLevel == 0)
				node = new @Class();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Class CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Class node;
			if(poolLevel == 0)
				node = new @Class();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_Class : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Class typeVar = new NodeType_Class();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, };
		public NodeType_Class() : base((int) NodeTypes.@Class)
		{
		}
		public override String Name { get { return "Class"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Class();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Class();
		}

	}

	// *** Node Feature ***

	public interface IFeature : IDeclaration
	{
	}

	public sealed class NodeType_Feature : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Feature typeVar = new NodeType_Feature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, true, true, true, true, };
		public NodeType_Feature() : base((int) NodeTypes.@Feature)
		{
		}
		public override String Name { get { return "Feature"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Feature cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
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

	public sealed class @MethodSignature : GRGEN_LGSP.LGSPNode, IMethodSignature
	{
		private static int poolLevel = 0;
		private static @MethodSignature[] pool = new @MethodSignature[10];
		public @MethodSignature() : base(NodeType_MethodSignature.typeVar)
		{
		}

		public static NodeType_MethodSignature TypeInstance { get { return NodeType_MethodSignature.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @MethodSignature(this); }

		private @MethodSignature(@MethodSignature oldElem) : base(NodeType_MethodSignature.typeVar)
		{
		}
		public static @MethodSignature CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@MethodSignature node;
			if(poolLevel == 0)
				node = new @MethodSignature();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @MethodSignature CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@MethodSignature node;
			if(poolLevel == 0)
				node = new @MethodSignature();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_MethodSignature : GRGEN_LIBGR.NodeType
	{
		public static NodeType_MethodSignature typeVar = new NodeType_MethodSignature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, };
		public NodeType_MethodSignature() : base((int) NodeTypes.@MethodSignature)
		{
		}
		public override String Name { get { return "MethodSignature"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @MethodSignature();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @MethodSignature();
		}

	}

	// *** Node Attribute ***

	public interface IAttribute : IFeature
	{
	}

	public sealed class NodeType_Attribute : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Attribute typeVar = new NodeType_Attribute();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, true, true, };
		public NodeType_Attribute() : base((int) NodeTypes.@Attribute)
		{
		}
		public override String Name { get { return "Attribute"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			throw new Exception("The abstract node type Attribute cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
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

	public sealed class @Constant : GRGEN_LGSP.LGSPNode, IConstant
	{
		private static int poolLevel = 0;
		private static @Constant[] pool = new @Constant[10];
		public @Constant() : base(NodeType_Constant.typeVar)
		{
		}

		public static NodeType_Constant TypeInstance { get { return NodeType_Constant.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Constant(this); }

		private @Constant(@Constant oldElem) : base(NodeType_Constant.typeVar)
		{
		}
		public static @Constant CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Constant node;
			if(poolLevel == 0)
				node = new @Constant();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Constant CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Constant node;
			if(poolLevel == 0)
				node = new @Constant();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_Constant : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Constant typeVar = new NodeType_Constant();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, };
		public NodeType_Constant() : base((int) NodeTypes.@Constant)
		{
		}
		public override String Name { get { return "Constant"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Constant();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Constant();
		}

	}

	// *** Node Variabel ***

	public interface IVariabel : IAttribute
	{
	}

	public sealed class @Variabel : GRGEN_LGSP.LGSPNode, IVariabel
	{
		private static int poolLevel = 0;
		private static @Variabel[] pool = new @Variabel[10];
		public @Variabel() : base(NodeType_Variabel.typeVar)
		{
		}

		public static NodeType_Variabel TypeInstance { get { return NodeType_Variabel.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Variabel(this); }

		private @Variabel(@Variabel oldElem) : base(NodeType_Variabel.typeVar)
		{
		}
		public static @Variabel CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Variabel node;
			if(poolLevel == 0)
				node = new @Variabel();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Variabel CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Variabel node;
			if(poolLevel == 0)
				node = new @Variabel();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		}
	}

	public sealed class NodeType_Variabel : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Variabel typeVar = new NodeType_Variabel();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, };
		public NodeType_Variabel() : base((int) NodeTypes.@Variabel)
		{
		}
		public override String Name { get { return "Variabel"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Variabel();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Variabel();
		}

	}

	//
	// Node model
	//

	public sealed class ProgramGraphsNodeModel : GRGEN_LIBGR.INodeModel
	{
		public ProgramGraphsNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_MethodBody.typeVar,
				NodeType_Expression.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Class.typeVar,
				NodeType_Feature.typeVar,
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Entity.typeVar.subOrSameGrGenTypes = NodeType_Entity.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Entity.typeVar,
				NodeType_MethodBody.typeVar,
				NodeType_Expression.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Class.typeVar,
				NodeType_Feature.typeVar,
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Entity.typeVar.directSubGrGenTypes = NodeType_Entity.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_MethodBody.typeVar,
				NodeType_Expression.typeVar,
				NodeType_Declaration.typeVar,
			};
			NodeType_Entity.typeVar.superOrSameGrGenTypes = NodeType_Entity.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Entity.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Entity.typeVar.directSuperGrGenTypes = NodeType_Entity.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_MethodBody.typeVar.subOrSameGrGenTypes = NodeType_MethodBody.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_MethodBody.typeVar,
			};
			NodeType_MethodBody.typeVar.directSubGrGenTypes = NodeType_MethodBody.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_MethodBody.typeVar.superOrSameGrGenTypes = NodeType_MethodBody.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_MethodBody.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
			};
			NodeType_MethodBody.typeVar.directSuperGrGenTypes = NodeType_MethodBody.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Expression.typeVar.subOrSameGrGenTypes = NodeType_Expression.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Expression.typeVar,
			};
			NodeType_Expression.typeVar.directSubGrGenTypes = NodeType_Expression.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Expression.typeVar.superOrSameGrGenTypes = NodeType_Expression.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Expression.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
			};
			NodeType_Expression.typeVar.directSuperGrGenTypes = NodeType_Expression.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Declaration.typeVar.subOrSameGrGenTypes = NodeType_Declaration.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Declaration.typeVar,
				NodeType_Class.typeVar,
				NodeType_Feature.typeVar,
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Declaration.typeVar.directSubGrGenTypes = NodeType_Declaration.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Class.typeVar,
				NodeType_Feature.typeVar,
			};
			NodeType_Declaration.typeVar.superOrSameGrGenTypes = NodeType_Declaration.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Declaration.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
			};
			NodeType_Declaration.typeVar.directSuperGrGenTypes = NodeType_Declaration.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Class.typeVar.subOrSameGrGenTypes = NodeType_Class.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Class.typeVar,
			};
			NodeType_Class.typeVar.directSubGrGenTypes = NodeType_Class.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Class.typeVar.superOrSameGrGenTypes = NodeType_Class.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Class.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
			};
			NodeType_Class.typeVar.directSuperGrGenTypes = NodeType_Class.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Declaration.typeVar,
			};
			NodeType_Feature.typeVar.subOrSameGrGenTypes = NodeType_Feature.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Feature.typeVar,
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Feature.typeVar.directSubGrGenTypes = NodeType_Feature.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
			};
			NodeType_Feature.typeVar.superOrSameGrGenTypes = NodeType_Feature.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Feature.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
			};
			NodeType_Feature.typeVar.directSuperGrGenTypes = NodeType_Feature.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Declaration.typeVar,
			};
			NodeType_MethodSignature.typeVar.subOrSameGrGenTypes = NodeType_MethodSignature.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_MethodSignature.typeVar,
			};
			NodeType_MethodSignature.typeVar.directSubGrGenTypes = NodeType_MethodSignature.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_MethodSignature.typeVar.superOrSameGrGenTypes = NodeType_MethodSignature.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_MethodSignature.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
			};
			NodeType_MethodSignature.typeVar.directSuperGrGenTypes = NodeType_MethodSignature.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Feature.typeVar,
			};
			NodeType_Attribute.typeVar.subOrSameGrGenTypes = NodeType_Attribute.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Attribute.typeVar.directSubGrGenTypes = NodeType_Attribute.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Attribute.typeVar.superOrSameGrGenTypes = NodeType_Attribute.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Attribute.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
			};
			NodeType_Attribute.typeVar.directSuperGrGenTypes = NodeType_Attribute.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Feature.typeVar,
			};
			NodeType_Constant.typeVar.subOrSameGrGenTypes = NodeType_Constant.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Constant.typeVar,
			};
			NodeType_Constant.typeVar.directSubGrGenTypes = NodeType_Constant.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Constant.typeVar.superOrSameGrGenTypes = NodeType_Constant.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Constant.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
				NodeType_Attribute.typeVar,
			};
			NodeType_Constant.typeVar.directSuperGrGenTypes = NodeType_Constant.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Attribute.typeVar,
			};
			NodeType_Variabel.typeVar.subOrSameGrGenTypes = NodeType_Variabel.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Variabel.typeVar,
			};
			NodeType_Variabel.typeVar.directSubGrGenTypes = NodeType_Variabel.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Variabel.typeVar.superOrSameGrGenTypes = NodeType_Variabel.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Variabel.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
				NodeType_Attribute.typeVar,
			};
			NodeType_Variabel.typeVar.directSuperGrGenTypes = NodeType_Variabel.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Attribute.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(String name)
		{
			switch(name)
			{
				case "Node" : return NodeType_Node.typeVar;
				case "Entity" : return NodeType_Entity.typeVar;
				case "MethodBody" : return NodeType_MethodBody.typeVar;
				case "Expression" : return NodeType_Expression.typeVar;
				case "Declaration" : return NodeType_Declaration.typeVar;
				case "Class" : return NodeType_Class.typeVar;
				case "Feature" : return NodeType_Feature.typeVar;
				case "MethodSignature" : return NodeType_MethodSignature.typeVar;
				case "Attribute" : return NodeType_Attribute.typeVar;
				case "Constant" : return NodeType_Constant.typeVar;
				case "Variabel" : return NodeType_Variabel.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_Entity.typeVar,
			NodeType_MethodBody.typeVar,
			NodeType_Expression.typeVar,
			NodeType_Declaration.typeVar,
			NodeType_Class.typeVar,
			NodeType_Feature.typeVar,
			NodeType_MethodSignature.typeVar,
			NodeType_Attribute.typeVar,
			NodeType_Constant.typeVar,
			NodeType_Variabel.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_Entity),
			typeof(NodeType_MethodBody),
			typeof(NodeType_Expression),
			typeof(NodeType_Declaration),
			typeof(NodeType_Class),
			typeof(NodeType_Feature),
			typeof(NodeType_MethodSignature),
			typeof(NodeType_Attribute),
			typeof(NodeType_Constant),
			typeof(NodeType_Variabel),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
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
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override String Name { get { return "AEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Arbitrary; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
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
		private static @Edge[] pool = new @Edge[10];
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}

		public static EdgeType_Edge TypeInstance { get { return EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @Edge(@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static @Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static @UEdge[] pool = new @UEdge[10];
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
		}

		public static EdgeType_UEdge TypeInstance { get { return EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @UEdge(@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static @UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_UEdge typeVar = new EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override String Name { get { return "UEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Undirected; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge contains ***

	public interface Icontains : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @contains : GRGEN_LGSP.LGSPEdge, Icontains
	{
		private static int poolLevel = 0;
		private static @contains[] pool = new @contains[10];
		public @contains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_contains.typeVar, source, target)
		{
		}

		public static EdgeType_contains TypeInstance { get { return EdgeType_contains.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @contains(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @contains(@contains oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_contains.typeVar, newSource, newTarget)
		{
		}
		public static @contains CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@contains edge;
			if(poolLevel == 0)
				edge = new @contains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @contains CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@contains edge;
			if(poolLevel == 0)
				edge = new @contains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_contains : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_contains typeVar = new EdgeType_contains();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, };
		public EdgeType_contains() : base((int) EdgeTypes.@contains)
		{
		}
		public override String Name { get { return "contains"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @contains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @contains((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge references ***

	public interface Ireferences : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @references : GRGEN_LGSP.LGSPEdge, Ireferences
	{
		private static int poolLevel = 0;
		private static @references[] pool = new @references[10];
		public @references(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_references.typeVar, source, target)
		{
		}

		public static EdgeType_references TypeInstance { get { return EdgeType_references.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @references(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @references(@references oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_references.typeVar, newSource, newTarget)
		{
		}
		public static @references CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@references edge;
			if(poolLevel == 0)
				edge = new @references(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @references CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@references edge;
			if(poolLevel == 0)
				edge = new @references(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_references : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_references typeVar = new EdgeType_references();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, false, false, };
		public EdgeType_references() : base((int) EdgeTypes.@references)
		{
		}
		public override String Name { get { return "references"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @references((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @references((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge hasType ***

	public interface IhasType : Ireferences
	{
	}

	public sealed class @hasType : GRGEN_LGSP.LGSPEdge, IhasType
	{
		private static int poolLevel = 0;
		private static @hasType[] pool = new @hasType[10];
		public @hasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_hasType.typeVar, source, target)
		{
		}

		public static EdgeType_hasType TypeInstance { get { return EdgeType_hasType.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @hasType(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @hasType(@hasType oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_hasType.typeVar, newSource, newTarget)
		{
		}
		public static @hasType CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@hasType edge;
			if(poolLevel == 0)
				edge = new @hasType(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @hasType CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@hasType edge;
			if(poolLevel == 0)
				edge = new @hasType(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_hasType : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_hasType typeVar = new EdgeType_hasType();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, };
		public EdgeType_hasType() : base((int) EdgeTypes.@hasType)
		{
		}
		public override String Name { get { return "hasType"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @hasType((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @hasType((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge bindsTo ***

	public interface IbindsTo : Ireferences
	{
	}

	public sealed class @bindsTo : GRGEN_LGSP.LGSPEdge, IbindsTo
	{
		private static int poolLevel = 0;
		private static @bindsTo[] pool = new @bindsTo[10];
		public @bindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_bindsTo.typeVar, source, target)
		{
		}

		public static EdgeType_bindsTo TypeInstance { get { return EdgeType_bindsTo.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @bindsTo(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @bindsTo(@bindsTo oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_bindsTo.typeVar, newSource, newTarget)
		{
		}
		public static @bindsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@bindsTo edge;
			if(poolLevel == 0)
				edge = new @bindsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @bindsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@bindsTo edge;
			if(poolLevel == 0)
				edge = new @bindsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_bindsTo : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_bindsTo typeVar = new EdgeType_bindsTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, };
		public EdgeType_bindsTo() : base((int) EdgeTypes.@bindsTo)
		{
		}
		public override String Name { get { return "bindsTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @bindsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @bindsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge uses ***

	public interface Iuses : Ireferences
	{
	}

	public sealed class @uses : GRGEN_LGSP.LGSPEdge, Iuses
	{
		private static int poolLevel = 0;
		private static @uses[] pool = new @uses[10];
		public @uses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_uses.typeVar, source, target)
		{
		}

		public static EdgeType_uses TypeInstance { get { return EdgeType_uses.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @uses(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @uses(@uses oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_uses.typeVar, newSource, newTarget)
		{
		}
		public static @uses CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@uses edge;
			if(poolLevel == 0)
				edge = new @uses(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @uses CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@uses edge;
			if(poolLevel == 0)
				edge = new @uses(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_uses : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_uses typeVar = new EdgeType_uses();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, };
		public EdgeType_uses() : base((int) EdgeTypes.@uses)
		{
		}
		public override String Name { get { return "uses"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @uses((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @uses((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge writesTo ***

	public interface IwritesTo : Ireferences
	{
	}

	public sealed class @writesTo : GRGEN_LGSP.LGSPEdge, IwritesTo
	{
		private static int poolLevel = 0;
		private static @writesTo[] pool = new @writesTo[10];
		public @writesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_writesTo.typeVar, source, target)
		{
		}

		public static EdgeType_writesTo TypeInstance { get { return EdgeType_writesTo.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @writesTo(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @writesTo(@writesTo oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_writesTo.typeVar, newSource, newTarget)
		{
		}
		public static @writesTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@writesTo edge;
			if(poolLevel == 0)
				edge = new @writesTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @writesTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@writesTo edge;
			if(poolLevel == 0)
				edge = new @writesTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_writesTo : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_writesTo typeVar = new EdgeType_writesTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, };
		public EdgeType_writesTo() : base((int) EdgeTypes.@writesTo)
		{
		}
		public override String Name { get { return "writesTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @writesTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @writesTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge calls ***

	public interface Icalls : Ireferences
	{
	}

	public sealed class @calls : GRGEN_LGSP.LGSPEdge, Icalls
	{
		private static int poolLevel = 0;
		private static @calls[] pool = new @calls[10];
		public @calls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_calls.typeVar, source, target)
		{
		}

		public static EdgeType_calls TypeInstance { get { return EdgeType_calls.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @calls(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @calls(@calls oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_calls.typeVar, newSource, newTarget)
		{
		}
		public static @calls CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@calls edge;
			if(poolLevel == 0)
				edge = new @calls(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @calls CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@calls edge;
			if(poolLevel == 0)
				edge = new @calls(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_calls : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_calls typeVar = new EdgeType_calls();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, };
		public EdgeType_calls() : base((int) EdgeTypes.@calls)
		{
		}
		public override String Name { get { return "calls"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @calls((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @calls((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge containedInClass ***

	public interface IcontainedInClass : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @containedInClass : GRGEN_LGSP.LGSPEdge, IcontainedInClass
	{
		private static int poolLevel = 0;
		private static @containedInClass[] pool = new @containedInClass[10];
		public @containedInClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_containedInClass.typeVar, source, target)
		{
		}

		public static EdgeType_containedInClass TypeInstance { get { return EdgeType_containedInClass.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @containedInClass(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @containedInClass(@containedInClass oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_containedInClass.typeVar, newSource, newTarget)
		{
		}
		public static @containedInClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@containedInClass edge;
			if(poolLevel == 0)
				edge = new @containedInClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @containedInClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@containedInClass edge;
			if(poolLevel == 0)
				edge = new @containedInClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_containedInClass : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_containedInClass typeVar = new EdgeType_containedInClass();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, };
		public EdgeType_containedInClass() : base((int) EdgeTypes.@containedInClass)
		{
		}
		public override String Name { get { return "containedInClass"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @containedInClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @containedInClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge containedInMethodBody ***

	public interface IcontainedInMethodBody : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @containedInMethodBody : GRGEN_LGSP.LGSPEdge, IcontainedInMethodBody
	{
		private static int poolLevel = 0;
		private static @containedInMethodBody[] pool = new @containedInMethodBody[10];
		public @containedInMethodBody(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_containedInMethodBody.typeVar, source, target)
		{
		}

		public static EdgeType_containedInMethodBody TypeInstance { get { return EdgeType_containedInMethodBody.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @containedInMethodBody(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @containedInMethodBody(@containedInMethodBody oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_containedInMethodBody.typeVar, newSource, newTarget)
		{
		}
		public static @containedInMethodBody CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@containedInMethodBody edge;
			if(poolLevel == 0)
				edge = new @containedInMethodBody(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @containedInMethodBody CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@containedInMethodBody edge;
			if(poolLevel == 0)
				edge = new @containedInMethodBody(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		}
	}

	public sealed class EdgeType_containedInMethodBody : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_containedInMethodBody typeVar = new EdgeType_containedInMethodBody();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, };
		public EdgeType_containedInMethodBody() : base((int) EdgeTypes.@containedInMethodBody)
		{
		}
		public override String Name { get { return "containedInMethodBody"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @containedInMethodBody((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @containedInMethodBody((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class ProgramGraphsEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public ProgramGraphsEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
				EdgeType_contains.typeVar,
				EdgeType_references.typeVar,
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
				EdgeType_containedInClass.typeVar,
				EdgeType_containedInMethodBody.typeVar,
			};
			EdgeType_AEdge.typeVar.directSubGrGenTypes = EdgeType_AEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.superOrSameGrGenTypes = EdgeType_AEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.directSuperGrGenTypes = EdgeType_AEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_contains.typeVar,
				EdgeType_references.typeVar,
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
				EdgeType_containedInClass.typeVar,
				EdgeType_containedInMethodBody.typeVar,
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_contains.typeVar,
				EdgeType_references.typeVar,
				EdgeType_containedInClass.typeVar,
				EdgeType_containedInMethodBody.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_Edge.typeVar.directSuperGrGenTypes = EdgeType_Edge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.subOrSameGrGenTypes = EdgeType_UEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_UEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSubGrGenTypes = EdgeType_UEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_UEdge.typeVar.superOrSameGrGenTypes = EdgeType_UEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_UEdge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSuperGrGenTypes = EdgeType_UEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_contains.typeVar.subOrSameGrGenTypes = EdgeType_contains.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_contains.typeVar,
			};
			EdgeType_contains.typeVar.directSubGrGenTypes = EdgeType_contains.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_contains.typeVar.superOrSameGrGenTypes = EdgeType_contains.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_contains.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_contains.typeVar.directSuperGrGenTypes = EdgeType_contains.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_references.typeVar.subOrSameGrGenTypes = EdgeType_references.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
			};
			EdgeType_references.typeVar.directSubGrGenTypes = EdgeType_references.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
			};
			EdgeType_references.typeVar.superOrSameGrGenTypes = EdgeType_references.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_references.typeVar.directSuperGrGenTypes = EdgeType_references.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_hasType.typeVar.subOrSameGrGenTypes = EdgeType_hasType.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_hasType.typeVar,
			};
			EdgeType_hasType.typeVar.directSubGrGenTypes = EdgeType_hasType.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_hasType.typeVar.superOrSameGrGenTypes = EdgeType_hasType.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_hasType.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_hasType.typeVar.directSuperGrGenTypes = EdgeType_hasType.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_bindsTo.typeVar.subOrSameGrGenTypes = EdgeType_bindsTo.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_bindsTo.typeVar,
			};
			EdgeType_bindsTo.typeVar.directSubGrGenTypes = EdgeType_bindsTo.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_bindsTo.typeVar.superOrSameGrGenTypes = EdgeType_bindsTo.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_bindsTo.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_bindsTo.typeVar.directSuperGrGenTypes = EdgeType_bindsTo.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_uses.typeVar.subOrSameGrGenTypes = EdgeType_uses.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_uses.typeVar,
			};
			EdgeType_uses.typeVar.directSubGrGenTypes = EdgeType_uses.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_uses.typeVar.superOrSameGrGenTypes = EdgeType_uses.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_uses.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_uses.typeVar.directSuperGrGenTypes = EdgeType_uses.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_writesTo.typeVar.subOrSameGrGenTypes = EdgeType_writesTo.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_writesTo.typeVar,
			};
			EdgeType_writesTo.typeVar.directSubGrGenTypes = EdgeType_writesTo.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_writesTo.typeVar.superOrSameGrGenTypes = EdgeType_writesTo.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_writesTo.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_writesTo.typeVar.directSuperGrGenTypes = EdgeType_writesTo.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_calls.typeVar.subOrSameGrGenTypes = EdgeType_calls.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_calls.typeVar,
			};
			EdgeType_calls.typeVar.directSubGrGenTypes = EdgeType_calls.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_calls.typeVar.superOrSameGrGenTypes = EdgeType_calls.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_calls.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_calls.typeVar.directSuperGrGenTypes = EdgeType_calls.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_containedInClass.typeVar.subOrSameGrGenTypes = EdgeType_containedInClass.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_containedInClass.typeVar,
			};
			EdgeType_containedInClass.typeVar.directSubGrGenTypes = EdgeType_containedInClass.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_containedInClass.typeVar.superOrSameGrGenTypes = EdgeType_containedInClass.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_containedInClass.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_containedInClass.typeVar.directSuperGrGenTypes = EdgeType_containedInClass.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_containedInMethodBody.typeVar.subOrSameGrGenTypes = EdgeType_containedInMethodBody.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_containedInMethodBody.typeVar,
			};
			EdgeType_containedInMethodBody.typeVar.directSubGrGenTypes = EdgeType_containedInMethodBody.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_containedInMethodBody.typeVar.superOrSameGrGenTypes = EdgeType_containedInMethodBody.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_containedInMethodBody.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_containedInMethodBody.typeVar.directSuperGrGenTypes = EdgeType_containedInMethodBody.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public GRGEN_LIBGR.EdgeType RootType { get { return EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return EdgeType_AEdge.typeVar; } }
		public GRGEN_LIBGR.EdgeType GetType(String name)
		{
			switch(name)
			{
				case "AEdge" : return EdgeType_AEdge.typeVar;
				case "Edge" : return EdgeType_Edge.typeVar;
				case "UEdge" : return EdgeType_UEdge.typeVar;
				case "contains" : return EdgeType_contains.typeVar;
				case "references" : return EdgeType_references.typeVar;
				case "hasType" : return EdgeType_hasType.typeVar;
				case "bindsTo" : return EdgeType_bindsTo.typeVar;
				case "uses" : return EdgeType_uses.typeVar;
				case "writesTo" : return EdgeType_writesTo.typeVar;
				case "calls" : return EdgeType_calls.typeVar;
				case "containedInClass" : return EdgeType_containedInClass.typeVar;
				case "containedInMethodBody" : return EdgeType_containedInMethodBody.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			EdgeType_AEdge.typeVar,
			EdgeType_Edge.typeVar,
			EdgeType_UEdge.typeVar,
			EdgeType_contains.typeVar,
			EdgeType_references.typeVar,
			EdgeType_hasType.typeVar,
			EdgeType_bindsTo.typeVar,
			EdgeType_uses.typeVar,
			EdgeType_writesTo.typeVar,
			EdgeType_calls.typeVar,
			EdgeType_containedInClass.typeVar,
			EdgeType_containedInMethodBody.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_AEdge),
			typeof(EdgeType_Edge),
			typeof(EdgeType_UEdge),
			typeof(EdgeType_contains),
			typeof(EdgeType_references),
			typeof(EdgeType_hasType),
			typeof(EdgeType_bindsTo),
			typeof(EdgeType_uses),
			typeof(EdgeType_writesTo),
			typeof(EdgeType_calls),
			typeof(EdgeType_containedInClass),
			typeof(EdgeType_containedInMethodBody),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
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
			new GRGEN_LIBGR.ValidateInfo(EdgeType_contains.typeVar, NodeType_Entity.typeVar, NodeType_Entity.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_references.typeVar, NodeType_Entity.typeVar, NodeType_Declaration.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_hasType.typeVar, NodeType_Feature.typeVar, NodeType_Class.typeVar, 1, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_bindsTo.typeVar, NodeType_MethodBody.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_uses.typeVar, NodeType_Expression.typeVar, NodeType_Attribute.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_writesTo.typeVar, NodeType_Expression.typeVar, NodeType_Variabel.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_calls.typeVar, NodeType_Expression.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "ProgramGraphs"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "e6271fc2f2794368b53b1fb118947e8d"; } }
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

		public @Node CreateNodeNode()
		{
			return @Node.CreateNode(this);
		}

		public @Node CreateNodeNode(String varName)
		{
			return @Node.CreateNode(this, varName);
		}

		public @MethodBody CreateNodeMethodBody()
		{
			return @MethodBody.CreateNode(this);
		}

		public @MethodBody CreateNodeMethodBody(String varName)
		{
			return @MethodBody.CreateNode(this, varName);
		}

		public @Expression CreateNodeExpression()
		{
			return @Expression.CreateNode(this);
		}

		public @Expression CreateNodeExpression(String varName)
		{
			return @Expression.CreateNode(this, varName);
		}

		public @Class CreateNodeClass()
		{
			return @Class.CreateNode(this);
		}

		public @Class CreateNodeClass(String varName)
		{
			return @Class.CreateNode(this, varName);
		}

		public @MethodSignature CreateNodeMethodSignature()
		{
			return @MethodSignature.CreateNode(this);
		}

		public @MethodSignature CreateNodeMethodSignature(String varName)
		{
			return @MethodSignature.CreateNode(this, varName);
		}

		public @Constant CreateNodeConstant()
		{
			return @Constant.CreateNode(this);
		}

		public @Constant CreateNodeConstant(String varName)
		{
			return @Constant.CreateNode(this, varName);
		}

		public @Variabel CreateNodeVariabel()
		{
			return @Variabel.CreateNode(this);
		}

		public @Variabel CreateNodeVariabel(String varName)
		{
			return @Variabel.CreateNode(this, varName);
		}

		public @Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @Edge.CreateEdge(this, source, target);
		}

		public @Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @Edge.CreateEdge(this, source, target, varName);
		}

		public @UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @UEdge.CreateEdge(this, source, target);
		}

		public @UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @UEdge.CreateEdge(this, source, target, varName);
		}

		public @contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @contains.CreateEdge(this, source, target);
		}

		public @contains CreateEdgecontains(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @contains.CreateEdge(this, source, target, varName);
		}

		public @references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @references.CreateEdge(this, source, target);
		}

		public @references CreateEdgereferences(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @references.CreateEdge(this, source, target, varName);
		}

		public @hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @hasType.CreateEdge(this, source, target);
		}

		public @hasType CreateEdgehasType(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @hasType.CreateEdge(this, source, target, varName);
		}

		public @bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @bindsTo.CreateEdge(this, source, target);
		}

		public @bindsTo CreateEdgebindsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @bindsTo.CreateEdge(this, source, target, varName);
		}

		public @uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @uses.CreateEdge(this, source, target);
		}

		public @uses CreateEdgeuses(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @uses.CreateEdge(this, source, target, varName);
		}

		public @writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @writesTo.CreateEdge(this, source, target);
		}

		public @writesTo CreateEdgewritesTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @writesTo.CreateEdge(this, source, target, varName);
		}

		public @calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @calls.CreateEdge(this, source, target);
		}

		public @calls CreateEdgecalls(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @calls.CreateEdge(this, source, target, varName);
		}

		public @containedInClass CreateEdgecontainedInClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @containedInClass.CreateEdge(this, source, target);
		}

		public @containedInClass CreateEdgecontainedInClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @containedInClass.CreateEdge(this, source, target, varName);
		}

		public @containedInMethodBody CreateEdgecontainedInMethodBody(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @containedInMethodBody.CreateEdge(this, source, target);
		}

		public @containedInMethodBody CreateEdgecontainedInMethodBody(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @containedInMethodBody.CreateEdge(this, source, target, varName);
		}

		private ProgramGraphsNodeModel nodeModel = new ProgramGraphsNodeModel();
		private ProgramGraphsEdgeModel edgeModel = new ProgramGraphsEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(EdgeType_contains.typeVar, NodeType_Entity.typeVar, NodeType_Entity.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_references.typeVar, NodeType_Entity.typeVar, NodeType_Declaration.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_hasType.typeVar, NodeType_Feature.typeVar, NodeType_Class.typeVar, 1, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_bindsTo.typeVar, NodeType_MethodBody.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_uses.typeVar, NodeType_Expression.typeVar, NodeType_Attribute.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_writesTo.typeVar, NodeType_Expression.typeVar, NodeType_Variabel.typeVar, 0, 1, 1, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_calls.typeVar, NodeType_Expression.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "ProgramGraphs"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "e6271fc2f2794368b53b1fb118947e8d"; } }
	}
}
