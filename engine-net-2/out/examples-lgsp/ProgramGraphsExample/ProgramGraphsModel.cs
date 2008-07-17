// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ProgramGraphs\ProgramGraphs.grg" on Thu Jul 17 11:13:00 GMT+01:00 2008

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

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


	public sealed class @Node : LGSPNode, INode
	{
		private static int poolLevel = 0;
		private static @Node[] pool = new @Node[10];
		public @Node() : base(NodeType_Node.typeVar)
		{
		}

		public static NodeType_Node TypeInstance { get { return NodeType_Node.typeVar; } }

		public override INode Clone() { return new @Node(this); }

		private @Node(@Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public static @Node CreateNode(LGSPGraph graph)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Node CreateNode(LGSPGraph graph, String varName)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Node : NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
		public override INode CreateNode()
		{
			return new @Node();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Node();
		}

	}

	// *** Node Entity ***

	public interface IEntity : INode
	{
	}

	public sealed class NodeType_Entity : NodeType
	{
		public static NodeType_Entity typeVar = new NodeType_Entity();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Entity() : base((int) NodeTypes.@Entity)
		{
		}
		public override String Name { get { return "Entity"; } }
		public override INode CreateNode()
		{
			throw new Exception("The abstract node type Entity cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Entity!");
		}
	}

	// *** Node MethodBody ***

	public interface IMethodBody : IEntity
	{
	}

	public sealed class @MethodBody : LGSPNode, IMethodBody
	{
		private static int poolLevel = 0;
		private static @MethodBody[] pool = new @MethodBody[10];
		public @MethodBody() : base(NodeType_MethodBody.typeVar)
		{
		}

		public static NodeType_MethodBody TypeInstance { get { return NodeType_MethodBody.typeVar; } }

		public override INode Clone() { return new @MethodBody(this); }

		private @MethodBody(@MethodBody oldElem) : base(NodeType_MethodBody.typeVar)
		{
		}
		public static @MethodBody CreateNode(LGSPGraph graph)
		{
			@MethodBody node;
			if(poolLevel == 0)
				node = new @MethodBody();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @MethodBody CreateNode(LGSPGraph graph, String varName)
		{
			@MethodBody node;
			if(poolLevel == 0)
				node = new @MethodBody();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_MethodBody : NodeType
	{
		public static NodeType_MethodBody typeVar = new NodeType_MethodBody();
		public static bool[] isA = new bool[] { true, true, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, };
		public NodeType_MethodBody() : base((int) NodeTypes.@MethodBody)
		{
		}
		public override String Name { get { return "MethodBody"; } }
		public override INode CreateNode()
		{
			return new @MethodBody();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @MethodBody();
		}

	}

	// *** Node Expression ***

	public interface IExpression : IEntity
	{
	}

	public sealed class @Expression : LGSPNode, IExpression
	{
		private static int poolLevel = 0;
		private static @Expression[] pool = new @Expression[10];
		public @Expression() : base(NodeType_Expression.typeVar)
		{
		}

		public static NodeType_Expression TypeInstance { get { return NodeType_Expression.typeVar; } }

		public override INode Clone() { return new @Expression(this); }

		private @Expression(@Expression oldElem) : base(NodeType_Expression.typeVar)
		{
		}
		public static @Expression CreateNode(LGSPGraph graph)
		{
			@Expression node;
			if(poolLevel == 0)
				node = new @Expression();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Expression CreateNode(LGSPGraph graph, String varName)
		{
			@Expression node;
			if(poolLevel == 0)
				node = new @Expression();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Expression : NodeType
	{
		public static NodeType_Expression typeVar = new NodeType_Expression();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, };
		public NodeType_Expression() : base((int) NodeTypes.@Expression)
		{
		}
		public override String Name { get { return "Expression"; } }
		public override INode CreateNode()
		{
			return new @Expression();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Expression();
		}

	}

	// *** Node Declaration ***

	public interface IDeclaration : IEntity
	{
	}

	public sealed class NodeType_Declaration : NodeType
	{
		public static NodeType_Declaration typeVar = new NodeType_Declaration();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, true, };
		public NodeType_Declaration() : base((int) NodeTypes.@Declaration)
		{
		}
		public override String Name { get { return "Declaration"; } }
		public override INode CreateNode()
		{
			throw new Exception("The abstract node type Declaration cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Declaration!");
		}
	}

	// *** Node Class ***

	public interface IClass : IDeclaration
	{
	}

	public sealed class @Class : LGSPNode, IClass
	{
		private static int poolLevel = 0;
		private static @Class[] pool = new @Class[10];
		public @Class() : base(NodeType_Class.typeVar)
		{
		}

		public static NodeType_Class TypeInstance { get { return NodeType_Class.typeVar; } }

		public override INode Clone() { return new @Class(this); }

		private @Class(@Class oldElem) : base(NodeType_Class.typeVar)
		{
		}
		public static @Class CreateNode(LGSPGraph graph)
		{
			@Class node;
			if(poolLevel == 0)
				node = new @Class();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Class CreateNode(LGSPGraph graph, String varName)
		{
			@Class node;
			if(poolLevel == 0)
				node = new @Class();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Class : NodeType
	{
		public static NodeType_Class typeVar = new NodeType_Class();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, };
		public NodeType_Class() : base((int) NodeTypes.@Class)
		{
		}
		public override String Name { get { return "Class"; } }
		public override INode CreateNode()
		{
			return new @Class();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Class();
		}

	}

	// *** Node Feature ***

	public interface IFeature : IDeclaration
	{
	}

	public sealed class NodeType_Feature : NodeType
	{
		public static NodeType_Feature typeVar = new NodeType_Feature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, true, true, true, true, };
		public NodeType_Feature() : base((int) NodeTypes.@Feature)
		{
		}
		public override String Name { get { return "Feature"; } }
		public override INode CreateNode()
		{
			throw new Exception("The abstract node type Feature cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Feature!");
		}
	}

	// *** Node MethodSignature ***

	public interface IMethodSignature : IFeature
	{
	}

	public sealed class @MethodSignature : LGSPNode, IMethodSignature
	{
		private static int poolLevel = 0;
		private static @MethodSignature[] pool = new @MethodSignature[10];
		public @MethodSignature() : base(NodeType_MethodSignature.typeVar)
		{
		}

		public static NodeType_MethodSignature TypeInstance { get { return NodeType_MethodSignature.typeVar; } }

		public override INode Clone() { return new @MethodSignature(this); }

		private @MethodSignature(@MethodSignature oldElem) : base(NodeType_MethodSignature.typeVar)
		{
		}
		public static @MethodSignature CreateNode(LGSPGraph graph)
		{
			@MethodSignature node;
			if(poolLevel == 0)
				node = new @MethodSignature();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @MethodSignature CreateNode(LGSPGraph graph, String varName)
		{
			@MethodSignature node;
			if(poolLevel == 0)
				node = new @MethodSignature();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_MethodSignature : NodeType
	{
		public static NodeType_MethodSignature typeVar = new NodeType_MethodSignature();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, };
		public NodeType_MethodSignature() : base((int) NodeTypes.@MethodSignature)
		{
		}
		public override String Name { get { return "MethodSignature"; } }
		public override INode CreateNode()
		{
			return new @MethodSignature();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @MethodSignature();
		}

	}

	// *** Node Attribute ***

	public interface IAttribute : IFeature
	{
	}

	public sealed class NodeType_Attribute : NodeType
	{
		public static NodeType_Attribute typeVar = new NodeType_Attribute();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, true, true, };
		public NodeType_Attribute() : base((int) NodeTypes.@Attribute)
		{
		}
		public override String Name { get { return "Attribute"; } }
		public override INode CreateNode()
		{
			throw new Exception("The abstract node type Attribute cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			throw new Exception("Cannot retype to the abstract type Attribute!");
		}
	}

	// *** Node Constant ***

	public interface IConstant : IAttribute
	{
	}

	public sealed class @Constant : LGSPNode, IConstant
	{
		private static int poolLevel = 0;
		private static @Constant[] pool = new @Constant[10];
		public @Constant() : base(NodeType_Constant.typeVar)
		{
		}

		public static NodeType_Constant TypeInstance { get { return NodeType_Constant.typeVar; } }

		public override INode Clone() { return new @Constant(this); }

		private @Constant(@Constant oldElem) : base(NodeType_Constant.typeVar)
		{
		}
		public static @Constant CreateNode(LGSPGraph graph)
		{
			@Constant node;
			if(poolLevel == 0)
				node = new @Constant();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Constant CreateNode(LGSPGraph graph, String varName)
		{
			@Constant node;
			if(poolLevel == 0)
				node = new @Constant();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Constant : NodeType
	{
		public static NodeType_Constant typeVar = new NodeType_Constant();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, };
		public NodeType_Constant() : base((int) NodeTypes.@Constant)
		{
		}
		public override String Name { get { return "Constant"; } }
		public override INode CreateNode()
		{
			return new @Constant();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Constant();
		}

	}

	// *** Node Variabel ***

	public interface IVariabel : IAttribute
	{
	}

	public sealed class @Variabel : LGSPNode, IVariabel
	{
		private static int poolLevel = 0;
		private static @Variabel[] pool = new @Variabel[10];
		public @Variabel() : base(NodeType_Variabel.typeVar)
		{
		}

		public static NodeType_Variabel TypeInstance { get { return NodeType_Variabel.typeVar; } }

		public override INode Clone() { return new @Variabel(this); }

		private @Variabel(@Variabel oldElem) : base(NodeType_Variabel.typeVar)
		{
		}
		public static @Variabel CreateNode(LGSPGraph graph)
		{
			@Variabel node;
			if(poolLevel == 0)
				node = new @Variabel();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Variabel CreateNode(LGSPGraph graph, String varName)
		{
			@Variabel node;
			if(poolLevel == 0)
				node = new @Variabel();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Variabel : NodeType
	{
		public static NodeType_Variabel typeVar = new NodeType_Variabel();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, };
		public NodeType_Variabel() : base((int) NodeTypes.@Variabel)
		{
		}
		public override String Name { get { return "Variabel"; } }
		public override INode CreateNode()
		{
			return new @Variabel();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Variabel();
		}

	}

	//
	// Node model
	//

	public sealed class ProgramGraphsNodeModel : INodeModel
	{
		public ProgramGraphsNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
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
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new NodeType[] {
			};
			NodeType_Entity.typeVar.subOrSameGrGenTypes = NodeType_Entity.typeVar.subOrSameTypes = new NodeType[] {
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
			NodeType_Entity.typeVar.directSubGrGenTypes = NodeType_Entity.typeVar.directSubTypes = new NodeType[] {
				NodeType_MethodBody.typeVar,
				NodeType_Expression.typeVar,
				NodeType_Declaration.typeVar,
			};
			NodeType_Entity.typeVar.superOrSameGrGenTypes = NodeType_Entity.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Entity.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Entity.typeVar.directSuperGrGenTypes = NodeType_Entity.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_MethodBody.typeVar.subOrSameGrGenTypes = NodeType_MethodBody.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_MethodBody.typeVar,
			};
			NodeType_MethodBody.typeVar.directSubGrGenTypes = NodeType_MethodBody.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_MethodBody.typeVar.superOrSameGrGenTypes = NodeType_MethodBody.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_MethodBody.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
			};
			NodeType_MethodBody.typeVar.directSuperGrGenTypes = NodeType_MethodBody.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Expression.typeVar.subOrSameGrGenTypes = NodeType_Expression.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Expression.typeVar,
			};
			NodeType_Expression.typeVar.directSubGrGenTypes = NodeType_Expression.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Expression.typeVar.superOrSameGrGenTypes = NodeType_Expression.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Expression.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
			};
			NodeType_Expression.typeVar.directSuperGrGenTypes = NodeType_Expression.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Declaration.typeVar.subOrSameGrGenTypes = NodeType_Declaration.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Declaration.typeVar,
				NodeType_Class.typeVar,
				NodeType_Feature.typeVar,
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Declaration.typeVar.directSubGrGenTypes = NodeType_Declaration.typeVar.directSubTypes = new NodeType[] {
				NodeType_Class.typeVar,
				NodeType_Feature.typeVar,
			};
			NodeType_Declaration.typeVar.superOrSameGrGenTypes = NodeType_Declaration.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Declaration.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
			};
			NodeType_Declaration.typeVar.directSuperGrGenTypes = NodeType_Declaration.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Entity.typeVar,
			};
			NodeType_Class.typeVar.subOrSameGrGenTypes = NodeType_Class.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Class.typeVar,
			};
			NodeType_Class.typeVar.directSubGrGenTypes = NodeType_Class.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Class.typeVar.superOrSameGrGenTypes = NodeType_Class.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Class.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
			};
			NodeType_Class.typeVar.directSuperGrGenTypes = NodeType_Class.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Declaration.typeVar,
			};
			NodeType_Feature.typeVar.subOrSameGrGenTypes = NodeType_Feature.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Feature.typeVar,
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Feature.typeVar.directSubGrGenTypes = NodeType_Feature.typeVar.directSubTypes = new NodeType[] {
				NodeType_MethodSignature.typeVar,
				NodeType_Attribute.typeVar,
			};
			NodeType_Feature.typeVar.superOrSameGrGenTypes = NodeType_Feature.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Feature.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
			};
			NodeType_Feature.typeVar.directSuperGrGenTypes = NodeType_Feature.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Declaration.typeVar,
			};
			NodeType_MethodSignature.typeVar.subOrSameGrGenTypes = NodeType_MethodSignature.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_MethodSignature.typeVar,
			};
			NodeType_MethodSignature.typeVar.directSubGrGenTypes = NodeType_MethodSignature.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_MethodSignature.typeVar.superOrSameGrGenTypes = NodeType_MethodSignature.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_MethodSignature.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
			};
			NodeType_MethodSignature.typeVar.directSuperGrGenTypes = NodeType_MethodSignature.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Feature.typeVar,
			};
			NodeType_Attribute.typeVar.subOrSameGrGenTypes = NodeType_Attribute.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Attribute.typeVar,
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Attribute.typeVar.directSubGrGenTypes = NodeType_Attribute.typeVar.directSubTypes = new NodeType[] {
				NodeType_Constant.typeVar,
				NodeType_Variabel.typeVar,
			};
			NodeType_Attribute.typeVar.superOrSameGrGenTypes = NodeType_Attribute.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Attribute.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
			};
			NodeType_Attribute.typeVar.directSuperGrGenTypes = NodeType_Attribute.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Feature.typeVar,
			};
			NodeType_Constant.typeVar.subOrSameGrGenTypes = NodeType_Constant.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Constant.typeVar,
			};
			NodeType_Constant.typeVar.directSubGrGenTypes = NodeType_Constant.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Constant.typeVar.superOrSameGrGenTypes = NodeType_Constant.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Constant.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
				NodeType_Attribute.typeVar,
			};
			NodeType_Constant.typeVar.directSuperGrGenTypes = NodeType_Constant.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Attribute.typeVar,
			};
			NodeType_Variabel.typeVar.subOrSameGrGenTypes = NodeType_Variabel.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Variabel.typeVar,
			};
			NodeType_Variabel.typeVar.directSubGrGenTypes = NodeType_Variabel.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Variabel.typeVar.superOrSameGrGenTypes = NodeType_Variabel.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Variabel.typeVar,
				NodeType_Node.typeVar,
				NodeType_Entity.typeVar,
				NodeType_Declaration.typeVar,
				NodeType_Feature.typeVar,
				NodeType_Attribute.typeVar,
			};
			NodeType_Variabel.typeVar.directSuperGrGenTypes = NodeType_Variabel.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Attribute.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public NodeType RootType { get { return NodeType_Node.typeVar; } }
		GrGenType ITypeModel.RootType { get { return NodeType_Node.typeVar; } }
		public NodeType GetType(String name)
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
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
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
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
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
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @contains, @references, @hasType, @bindsTo, @uses, @writesTo, @calls };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override String Name { get { return "AEdge"; } }
		public override Directedness Directedness { get { return Directedness.Arbitrary; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			throw new Exception("Cannot retype to the abstract type AEdge!");
		}
	}

	// *** Edge Edge ***


	public sealed class @Edge : LGSPEdge, IEdge
	{
		private static int poolLevel = 0;
		private static @Edge[] pool = new @Edge[10];
		public @Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}

		public static EdgeType_Edge TypeInstance { get { return EdgeType_Edge.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @Edge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @Edge(@Edge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static @Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_Edge : EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @Edge((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @Edge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed class @UEdge : LGSPEdge, IEdge
	{
		private static int poolLevel = 0;
		private static @UEdge[] pool = new @UEdge[10];
		public @UEdge(LGSPNode source, LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
		}

		public static EdgeType_UEdge TypeInstance { get { return EdgeType_UEdge.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @UEdge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @UEdge(@UEdge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static @UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_UEdge : EdgeType
	{
		public static EdgeType_UEdge typeVar = new EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override String Name { get { return "UEdge"; } }
		public override Directedness Directedness { get { return Directedness.Undirected; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @UEdge((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @UEdge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge contains ***

	public interface Icontains : IEdge
	{
	}

	public sealed class @contains : LGSPEdge, Icontains
	{
		private static int poolLevel = 0;
		private static @contains[] pool = new @contains[10];
		public @contains(LGSPNode source, LGSPNode target)
			: base(EdgeType_contains.typeVar, source, target)
		{
		}

		public static EdgeType_contains TypeInstance { get { return EdgeType_contains.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @contains(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @contains(@contains oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_contains.typeVar, newSource, newTarget)
		{
		}
		public static @contains CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@contains edge;
			if(poolLevel == 0)
				edge = new @contains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @contains CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@contains edge;
			if(poolLevel == 0)
				edge = new @contains(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_contains : EdgeType
	{
		public static EdgeType_contains typeVar = new EdgeType_contains();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, };
		public EdgeType_contains() : base((int) EdgeTypes.@contains)
		{
		}
		public override String Name { get { return "contains"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @contains((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @contains((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge references ***

	public interface Ireferences : IEdge
	{
	}

	public sealed class @references : LGSPEdge, Ireferences
	{
		private static int poolLevel = 0;
		private static @references[] pool = new @references[10];
		public @references(LGSPNode source, LGSPNode target)
			: base(EdgeType_references.typeVar, source, target)
		{
		}

		public static EdgeType_references TypeInstance { get { return EdgeType_references.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @references(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @references(@references oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_references.typeVar, newSource, newTarget)
		{
		}
		public static @references CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@references edge;
			if(poolLevel == 0)
				edge = new @references(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @references CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@references edge;
			if(poolLevel == 0)
				edge = new @references(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_references : EdgeType
	{
		public static EdgeType_references typeVar = new EdgeType_references();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, true, true, true, true, true, };
		public EdgeType_references() : base((int) EdgeTypes.@references)
		{
		}
		public override String Name { get { return "references"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @references((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @references((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge hasType ***

	public interface IhasType : Ireferences
	{
	}

	public sealed class @hasType : LGSPEdge, IhasType
	{
		private static int poolLevel = 0;
		private static @hasType[] pool = new @hasType[10];
		public @hasType(LGSPNode source, LGSPNode target)
			: base(EdgeType_hasType.typeVar, source, target)
		{
		}

		public static EdgeType_hasType TypeInstance { get { return EdgeType_hasType.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @hasType(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @hasType(@hasType oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_hasType.typeVar, newSource, newTarget)
		{
		}
		public static @hasType CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@hasType edge;
			if(poolLevel == 0)
				edge = new @hasType(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @hasType CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@hasType edge;
			if(poolLevel == 0)
				edge = new @hasType(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_hasType : EdgeType
	{
		public static EdgeType_hasType typeVar = new EdgeType_hasType();
		public static bool[] isA = new bool[] { true, true, false, false, true, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, };
		public EdgeType_hasType() : base((int) EdgeTypes.@hasType)
		{
		}
		public override String Name { get { return "hasType"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @hasType((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @hasType((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge bindsTo ***

	public interface IbindsTo : Ireferences
	{
	}

	public sealed class @bindsTo : LGSPEdge, IbindsTo
	{
		private static int poolLevel = 0;
		private static @bindsTo[] pool = new @bindsTo[10];
		public @bindsTo(LGSPNode source, LGSPNode target)
			: base(EdgeType_bindsTo.typeVar, source, target)
		{
		}

		public static EdgeType_bindsTo TypeInstance { get { return EdgeType_bindsTo.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @bindsTo(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @bindsTo(@bindsTo oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_bindsTo.typeVar, newSource, newTarget)
		{
		}
		public static @bindsTo CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@bindsTo edge;
			if(poolLevel == 0)
				edge = new @bindsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @bindsTo CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@bindsTo edge;
			if(poolLevel == 0)
				edge = new @bindsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_bindsTo : EdgeType
	{
		public static EdgeType_bindsTo typeVar = new EdgeType_bindsTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, };
		public EdgeType_bindsTo() : base((int) EdgeTypes.@bindsTo)
		{
		}
		public override String Name { get { return "bindsTo"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @bindsTo((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @bindsTo((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge uses ***

	public interface Iuses : Ireferences
	{
	}

	public sealed class @uses : LGSPEdge, Iuses
	{
		private static int poolLevel = 0;
		private static @uses[] pool = new @uses[10];
		public @uses(LGSPNode source, LGSPNode target)
			: base(EdgeType_uses.typeVar, source, target)
		{
		}

		public static EdgeType_uses TypeInstance { get { return EdgeType_uses.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @uses(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @uses(@uses oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_uses.typeVar, newSource, newTarget)
		{
		}
		public static @uses CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@uses edge;
			if(poolLevel == 0)
				edge = new @uses(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @uses CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@uses edge;
			if(poolLevel == 0)
				edge = new @uses(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_uses : EdgeType
	{
		public static EdgeType_uses typeVar = new EdgeType_uses();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, };
		public EdgeType_uses() : base((int) EdgeTypes.@uses)
		{
		}
		public override String Name { get { return "uses"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @uses((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @uses((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge writesTo ***

	public interface IwritesTo : Ireferences
	{
	}

	public sealed class @writesTo : LGSPEdge, IwritesTo
	{
		private static int poolLevel = 0;
		private static @writesTo[] pool = new @writesTo[10];
		public @writesTo(LGSPNode source, LGSPNode target)
			: base(EdgeType_writesTo.typeVar, source, target)
		{
		}

		public static EdgeType_writesTo TypeInstance { get { return EdgeType_writesTo.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @writesTo(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @writesTo(@writesTo oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_writesTo.typeVar, newSource, newTarget)
		{
		}
		public static @writesTo CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@writesTo edge;
			if(poolLevel == 0)
				edge = new @writesTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @writesTo CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@writesTo edge;
			if(poolLevel == 0)
				edge = new @writesTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_writesTo : EdgeType
	{
		public static EdgeType_writesTo typeVar = new EdgeType_writesTo();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, };
		public EdgeType_writesTo() : base((int) EdgeTypes.@writesTo)
		{
		}
		public override String Name { get { return "writesTo"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @writesTo((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @writesTo((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge calls ***

	public interface Icalls : Ireferences
	{
	}

	public sealed class @calls : LGSPEdge, Icalls
	{
		private static int poolLevel = 0;
		private static @calls[] pool = new @calls[10];
		public @calls(LGSPNode source, LGSPNode target)
			: base(EdgeType_calls.typeVar, source, target)
		{
		}

		public static EdgeType_calls TypeInstance { get { return EdgeType_calls.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @calls(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @calls(@calls oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_calls.typeVar, newSource, newTarget)
		{
		}
		public static @calls CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@calls edge;
			if(poolLevel == 0)
				edge = new @calls(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @calls CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@calls edge;
			if(poolLevel == 0)
				edge = new @calls(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_calls : EdgeType
	{
		public static EdgeType_calls typeVar = new EdgeType_calls();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, };
		public EdgeType_calls() : base((int) EdgeTypes.@calls)
		{
		}
		public override String Name { get { return "calls"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @calls((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @calls((LGSPNode) source, (LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class ProgramGraphsEdgeModel : IEdgeModel
	{
		public ProgramGraphsEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new EdgeType[] {
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
			};
			EdgeType_AEdge.typeVar.directSubGrGenTypes = EdgeType_AEdge.typeVar.directSubTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.superOrSameGrGenTypes = EdgeType_AEdge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.directSuperGrGenTypes = EdgeType_AEdge.typeVar.directSuperTypes = new EdgeType[] {
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_contains.typeVar,
				EdgeType_references.typeVar,
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new EdgeType[] {
				EdgeType_contains.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_Edge.typeVar.directSuperGrGenTypes = EdgeType_Edge.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.subOrSameGrGenTypes = EdgeType_UEdge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_UEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSubGrGenTypes = EdgeType_UEdge.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_UEdge.typeVar.superOrSameGrGenTypes = EdgeType_UEdge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_UEdge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSuperGrGenTypes = EdgeType_UEdge.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_contains.typeVar.subOrSameGrGenTypes = EdgeType_contains.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_contains.typeVar,
			};
			EdgeType_contains.typeVar.directSubGrGenTypes = EdgeType_contains.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_contains.typeVar.superOrSameGrGenTypes = EdgeType_contains.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_contains.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_contains.typeVar.directSuperGrGenTypes = EdgeType_contains.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_references.typeVar.subOrSameGrGenTypes = EdgeType_references.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_references.typeVar,
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
			};
			EdgeType_references.typeVar.directSubGrGenTypes = EdgeType_references.typeVar.directSubTypes = new EdgeType[] {
				EdgeType_hasType.typeVar,
				EdgeType_bindsTo.typeVar,
				EdgeType_uses.typeVar,
				EdgeType_writesTo.typeVar,
				EdgeType_calls.typeVar,
			};
			EdgeType_references.typeVar.superOrSameGrGenTypes = EdgeType_references.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_references.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_references.typeVar.directSuperGrGenTypes = EdgeType_references.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_hasType.typeVar.subOrSameGrGenTypes = EdgeType_hasType.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_hasType.typeVar,
			};
			EdgeType_hasType.typeVar.directSubGrGenTypes = EdgeType_hasType.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_hasType.typeVar.superOrSameGrGenTypes = EdgeType_hasType.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_hasType.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_hasType.typeVar.directSuperGrGenTypes = EdgeType_hasType.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_bindsTo.typeVar.subOrSameGrGenTypes = EdgeType_bindsTo.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_bindsTo.typeVar,
			};
			EdgeType_bindsTo.typeVar.directSubGrGenTypes = EdgeType_bindsTo.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_bindsTo.typeVar.superOrSameGrGenTypes = EdgeType_bindsTo.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_bindsTo.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_bindsTo.typeVar.directSuperGrGenTypes = EdgeType_bindsTo.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_uses.typeVar.subOrSameGrGenTypes = EdgeType_uses.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_uses.typeVar,
			};
			EdgeType_uses.typeVar.directSubGrGenTypes = EdgeType_uses.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_uses.typeVar.superOrSameGrGenTypes = EdgeType_uses.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_uses.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_uses.typeVar.directSuperGrGenTypes = EdgeType_uses.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_writesTo.typeVar.subOrSameGrGenTypes = EdgeType_writesTo.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_writesTo.typeVar,
			};
			EdgeType_writesTo.typeVar.directSubGrGenTypes = EdgeType_writesTo.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_writesTo.typeVar.superOrSameGrGenTypes = EdgeType_writesTo.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_writesTo.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_writesTo.typeVar.directSuperGrGenTypes = EdgeType_writesTo.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_references.typeVar,
			};
			EdgeType_calls.typeVar.subOrSameGrGenTypes = EdgeType_calls.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_calls.typeVar,
			};
			EdgeType_calls.typeVar.directSubGrGenTypes = EdgeType_calls.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_calls.typeVar.superOrSameGrGenTypes = EdgeType_calls.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_calls.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_references.typeVar,
			};
			EdgeType_calls.typeVar.directSuperGrGenTypes = EdgeType_calls.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_references.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public EdgeType RootType { get { return EdgeType_AEdge.typeVar; } }
		GrGenType ITypeModel.RootType { get { return EdgeType_AEdge.typeVar; } }
		public EdgeType GetType(String name)
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
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
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
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
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
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class ProgramGraphsGraphModel : IGraphModel
	{
		private ProgramGraphsNodeModel nodeModel = new ProgramGraphsNodeModel();
		private ProgramGraphsEdgeModel edgeModel = new ProgramGraphsEdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_contains.typeVar, NodeType_Entity.typeVar, NodeType_Entity.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_references.typeVar, NodeType_Entity.typeVar, NodeType_Declaration.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_hasType.typeVar, NodeType_Feature.typeVar, NodeType_Class.typeVar, 1, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_bindsTo.typeVar, NodeType_MethodBody.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_uses.typeVar, NodeType_Expression.typeVar, NodeType_Attribute.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_writesTo.typeVar, NodeType_Expression.typeVar, NodeType_Variabel.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_calls.typeVar, NodeType_Expression.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
		};

		public String ModelName { get { return "ProgramGraphs"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "55e6b03b0709e956ce939adc7c071dcd"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class ProgramGraphs : LGSPGraph, IGraphModel
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

		public @Edge CreateEdgeEdge(LGSPNode source, LGSPNode target)
		{
			return @Edge.CreateEdge(this, source, target);
		}

		public @Edge CreateEdgeEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return @Edge.CreateEdge(this, source, target, varName);
		}

		public @UEdge CreateEdgeUEdge(LGSPNode source, LGSPNode target)
		{
			return @UEdge.CreateEdge(this, source, target);
		}

		public @UEdge CreateEdgeUEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return @UEdge.CreateEdge(this, source, target, varName);
		}

		public @contains CreateEdgecontains(LGSPNode source, LGSPNode target)
		{
			return @contains.CreateEdge(this, source, target);
		}

		public @contains CreateEdgecontains(LGSPNode source, LGSPNode target, String varName)
		{
			return @contains.CreateEdge(this, source, target, varName);
		}

		public @references CreateEdgereferences(LGSPNode source, LGSPNode target)
		{
			return @references.CreateEdge(this, source, target);
		}

		public @references CreateEdgereferences(LGSPNode source, LGSPNode target, String varName)
		{
			return @references.CreateEdge(this, source, target, varName);
		}

		public @hasType CreateEdgehasType(LGSPNode source, LGSPNode target)
		{
			return @hasType.CreateEdge(this, source, target);
		}

		public @hasType CreateEdgehasType(LGSPNode source, LGSPNode target, String varName)
		{
			return @hasType.CreateEdge(this, source, target, varName);
		}

		public @bindsTo CreateEdgebindsTo(LGSPNode source, LGSPNode target)
		{
			return @bindsTo.CreateEdge(this, source, target);
		}

		public @bindsTo CreateEdgebindsTo(LGSPNode source, LGSPNode target, String varName)
		{
			return @bindsTo.CreateEdge(this, source, target, varName);
		}

		public @uses CreateEdgeuses(LGSPNode source, LGSPNode target)
		{
			return @uses.CreateEdge(this, source, target);
		}

		public @uses CreateEdgeuses(LGSPNode source, LGSPNode target, String varName)
		{
			return @uses.CreateEdge(this, source, target, varName);
		}

		public @writesTo CreateEdgewritesTo(LGSPNode source, LGSPNode target)
		{
			return @writesTo.CreateEdge(this, source, target);
		}

		public @writesTo CreateEdgewritesTo(LGSPNode source, LGSPNode target, String varName)
		{
			return @writesTo.CreateEdge(this, source, target, varName);
		}

		public @calls CreateEdgecalls(LGSPNode source, LGSPNode target)
		{
			return @calls.CreateEdge(this, source, target);
		}

		public @calls CreateEdgecalls(LGSPNode source, LGSPNode target, String varName)
		{
			return @calls.CreateEdge(this, source, target, varName);
		}

		private ProgramGraphsNodeModel nodeModel = new ProgramGraphsNodeModel();
		private ProgramGraphsEdgeModel edgeModel = new ProgramGraphsEdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_contains.typeVar, NodeType_Entity.typeVar, NodeType_Entity.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_references.typeVar, NodeType_Entity.typeVar, NodeType_Declaration.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_hasType.typeVar, NodeType_Feature.typeVar, NodeType_Class.typeVar, 1, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_bindsTo.typeVar, NodeType_MethodBody.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_uses.typeVar, NodeType_Expression.typeVar, NodeType_Attribute.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_writesTo.typeVar, NodeType_Expression.typeVar, NodeType_Variabel.typeVar, 0, 1, 1, long.MaxValue),
			new ValidateInfo(EdgeType_calls.typeVar, NodeType_Expression.typeVar, NodeType_MethodSignature.typeVar, 0, 1, 1, long.MaxValue),
		};

		public String ModelName { get { return "ProgramGraphs"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "55e6b03b0709e956ce939adc7c071dcd"; } }
	}
}
