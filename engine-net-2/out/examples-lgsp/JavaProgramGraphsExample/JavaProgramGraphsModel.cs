// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\JavaProgramGraphs\JavaProgramGraphs.grg" on Mon Apr 20 04:38:20 CEST 2009

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_JavaProgramGraphs
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_JavaProgramGraphs;
	//
	// Enums
	//

	public class Enums
	{
	}

	//
	// Node types
	//

	public enum NodeTypes { @Node, @Package, @Classifier, @Class, @Interface, @Variable, @Operation, @MethodBody, @Expression, @Access, @Update, @Call, @Instantiation, @Operator, @Return, @Block, @Literal, @Parameter };

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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
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

	// *** Node Package ***

	public interface IPackage : GRGEN_LIBGR.INode
	{
		string @name { get; set; }
	}

	public sealed class @Package : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IPackage
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Package[] pool = new GRGEN_MODEL.@Package[10];
		
		// explicit initializations of Package for target Package
		// implicit initializations of Package for target Package
		static @Package() {
		}
		
		public @Package() : base(GRGEN_MODEL.NodeType_Package.typeVar)
		{
			// implicit initialization, map/set creation of Package
			// explicit initializations of Package for target Package
		}

		public static GRGEN_MODEL.NodeType_Package TypeInstance { get { return GRGEN_MODEL.NodeType_Package.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Package(this); }

		private @Package(GRGEN_MODEL.@Package oldElem) : base(GRGEN_MODEL.NodeType_Package.typeVar)
		{
			_name = oldElem._name;
		}
		public static GRGEN_MODEL.@Package CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Package node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Package();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Package
				node.@name = null;
				// explicit initializations of Package for target Package
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Package CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Package node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Package();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Package
				node.@name = null;
				// explicit initializations of Package for target Package
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
			}
			throw new NullReferenceException(
				"The node type \"Package\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Package\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Package
			this.@name = null;
			// explicit initializations of Package for target Package
		}
	}

	public sealed class NodeType_Package : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Package typeVar = new GRGEN_MODEL.NodeType_Package();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public NodeType_Package() : base((int) NodeTypes.@Package)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
		}
		public override string Name { get { return "Package"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Package();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_name;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return AttributeType_name;
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
			GRGEN_MODEL.@Package newNode = new GRGEN_MODEL.@Package();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Package:
					// copy attributes for: Package
					{
						GRGEN_MODEL.IPackage old = (GRGEN_MODEL.IPackage) oldNode;
						newNode.@name = old.@name;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Classifier ***

	public interface IClassifier : GRGEN_LIBGR.INode
	{
		string @name { get; set; }
		string @visibility { get; set; }
		bool @isAbstract { get; set; }
	}

	public sealed class @Classifier : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IClassifier
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Classifier[] pool = new GRGEN_MODEL.@Classifier[10];
		
		// explicit initializations of Classifier for target Classifier
		// implicit initializations of Classifier for target Classifier
		static @Classifier() {
		}
		
		public @Classifier() : base(GRGEN_MODEL.NodeType_Classifier.typeVar)
		{
			// implicit initialization, map/set creation of Classifier
			// explicit initializations of Classifier for target Classifier
		}

		public static GRGEN_MODEL.NodeType_Classifier TypeInstance { get { return GRGEN_MODEL.NodeType_Classifier.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Classifier(this); }

		private @Classifier(GRGEN_MODEL.@Classifier oldElem) : base(GRGEN_MODEL.NodeType_Classifier.typeVar)
		{
			_name = oldElem._name;
			_visibility = oldElem._visibility;
			_isAbstract = oldElem._isAbstract;
		}
		public static GRGEN_MODEL.@Classifier CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Classifier node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Classifier();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Classifier
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Classifier
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Classifier CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Classifier node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Classifier();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Classifier
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Classifier
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _visibility;
		public string @visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		private bool _isAbstract;
		public bool @isAbstract
		{
			get { return _isAbstract; }
			set { _isAbstract = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
				case "visibility": return this.@visibility;
				case "isAbstract": return this.@isAbstract;
			}
			throw new NullReferenceException(
				"The node type \"Classifier\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
				case "visibility": this.@visibility = (string) value; return;
				case "isAbstract": this.@isAbstract = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Classifier\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Classifier
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			// explicit initializations of Classifier for target Classifier
		}
	}

	public sealed class NodeType_Classifier : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Classifier typeVar = new GRGEN_MODEL.NodeType_Classifier();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public static GRGEN_LIBGR.AttributeType AttributeType_visibility;
		public static GRGEN_LIBGR.AttributeType AttributeType_isAbstract;
		public NodeType_Classifier() : base((int) NodeTypes.@Classifier)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
			AttributeType_visibility = new GRGEN_LIBGR.AttributeType("visibility", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
			AttributeType_isAbstract = new GRGEN_LIBGR.AttributeType("isAbstract", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Classifier"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Classifier();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_name;
				yield return AttributeType_visibility;
				yield return AttributeType_isAbstract;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return AttributeType_name;
				case "visibility" : return AttributeType_visibility;
				case "isAbstract" : return AttributeType_isAbstract;
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
			GRGEN_MODEL.@Classifier newNode = new GRGEN_MODEL.@Classifier();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Classifier:
				case (int) NodeTypes.@Class:
				case (int) NodeTypes.@Interface:
					// copy attributes for: Classifier
					{
						GRGEN_MODEL.IClassifier old = (GRGEN_MODEL.IClassifier) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Class ***

	public interface IClass : IClassifier
	{
		bool @isFinal { get; set; }
	}

	public sealed class @Class : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IClass
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Class[] pool = new GRGEN_MODEL.@Class[10];
		
		// explicit initializations of Classifier for target Class
		// implicit initializations of Classifier for target Class
		// explicit initializations of Class for target Class
		// implicit initializations of Class for target Class
		static @Class() {
		}
		
		public @Class() : base(GRGEN_MODEL.NodeType_Class.typeVar)
		{
			// implicit initialization, map/set creation of Class
			// explicit initializations of Classifier for target Class
			// explicit initializations of Class for target Class
		}

		public static GRGEN_MODEL.NodeType_Class TypeInstance { get { return GRGEN_MODEL.NodeType_Class.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Class(this); }

		private @Class(GRGEN_MODEL.@Class oldElem) : base(GRGEN_MODEL.NodeType_Class.typeVar)
		{
			_name = oldElem._name;
			_visibility = oldElem._visibility;
			_isAbstract = oldElem._isAbstract;
			_isFinal = oldElem._isFinal;
		}
		public static GRGEN_MODEL.@Class CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Class node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Class();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Class
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				node.@isFinal = false;
				// explicit initializations of Classifier for target Class
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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Class
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				node.@isFinal = false;
				// explicit initializations of Classifier for target Class
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


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _visibility;
		public string @visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		private bool _isAbstract;
		public bool @isAbstract
		{
			get { return _isAbstract; }
			set { _isAbstract = value; }
		}

		private bool _isFinal;
		public bool @isFinal
		{
			get { return _isFinal; }
			set { _isFinal = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
				case "visibility": return this.@visibility;
				case "isAbstract": return this.@isAbstract;
				case "isFinal": return this.@isFinal;
			}
			throw new NullReferenceException(
				"The node type \"Class\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
				case "visibility": this.@visibility = (string) value; return;
				case "isAbstract": this.@isAbstract = (bool) value; return;
				case "isFinal": this.@isFinal = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Class\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Class
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			this.@isFinal = false;
			// explicit initializations of Classifier for target Class
			// explicit initializations of Class for target Class
		}
	}

	public sealed class NodeType_Class : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Class typeVar = new GRGEN_MODEL.NodeType_Class();
		public static bool[] isA = new bool[] { true, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_isFinal;
		public NodeType_Class() : base((int) NodeTypes.@Class)
		{
			AttributeType_isFinal = new GRGEN_LIBGR.AttributeType("isFinal", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Class"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Class();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 4; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_Classifier.AttributeType_name;
				yield return GRGEN_MODEL.NodeType_Classifier.AttributeType_visibility;
				yield return GRGEN_MODEL.NodeType_Classifier.AttributeType_isAbstract;
				yield return AttributeType_isFinal;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return GRGEN_MODEL.NodeType_Classifier.AttributeType_name;
				case "visibility" : return GRGEN_MODEL.NodeType_Classifier.AttributeType_visibility;
				case "isAbstract" : return GRGEN_MODEL.NodeType_Classifier.AttributeType_isAbstract;
				case "isFinal" : return AttributeType_isFinal;
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
			GRGEN_MODEL.@Class newNode = new GRGEN_MODEL.@Class();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Classifier:
				case (int) NodeTypes.@Interface:
					// copy attributes for: Classifier
					{
						GRGEN_MODEL.IClassifier old = (GRGEN_MODEL.IClassifier) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
					}
					break;
				case (int) NodeTypes.@Class:
					// copy attributes for: Class
					{
						GRGEN_MODEL.IClass old = (GRGEN_MODEL.IClass) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
						newNode.@isFinal = old.@isFinal;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Interface ***

	public interface IInterface : IClassifier
	{
	}

	public sealed class @Interface : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IInterface
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Interface[] pool = new GRGEN_MODEL.@Interface[10];
		
		// explicit initializations of Classifier for target Interface
		// implicit initializations of Classifier for target Interface
		// explicit initializations of Interface for target Interface
		// implicit initializations of Interface for target Interface
		static @Interface() {
		}
		
		public @Interface() : base(GRGEN_MODEL.NodeType_Interface.typeVar)
		{
			// implicit initialization, map/set creation of Interface
			// explicit initializations of Classifier for target Interface
			// explicit initializations of Interface for target Interface
		}

		public static GRGEN_MODEL.NodeType_Interface TypeInstance { get { return GRGEN_MODEL.NodeType_Interface.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Interface(this); }

		private @Interface(GRGEN_MODEL.@Interface oldElem) : base(GRGEN_MODEL.NodeType_Interface.typeVar)
		{
			_name = oldElem._name;
			_visibility = oldElem._visibility;
			_isAbstract = oldElem._isAbstract;
		}
		public static GRGEN_MODEL.@Interface CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Interface node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Interface();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Interface
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Interface
				// explicit initializations of Interface for target Interface
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Interface CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Interface node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Interface();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Interface
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Interface
				// explicit initializations of Interface for target Interface
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _visibility;
		public string @visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		private bool _isAbstract;
		public bool @isAbstract
		{
			get { return _isAbstract; }
			set { _isAbstract = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
				case "visibility": return this.@visibility;
				case "isAbstract": return this.@isAbstract;
			}
			throw new NullReferenceException(
				"The node type \"Interface\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
				case "visibility": this.@visibility = (string) value; return;
				case "isAbstract": this.@isAbstract = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Interface\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Interface
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			// explicit initializations of Classifier for target Interface
			// explicit initializations of Interface for target Interface
		}
	}

	public sealed class NodeType_Interface : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Interface typeVar = new GRGEN_MODEL.NodeType_Interface();
		public static bool[] isA = new bool[] { true, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public NodeType_Interface() : base((int) NodeTypes.@Interface)
		{
		}
		public override string Name { get { return "Interface"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Interface();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_Classifier.AttributeType_name;
				yield return GRGEN_MODEL.NodeType_Classifier.AttributeType_visibility;
				yield return GRGEN_MODEL.NodeType_Classifier.AttributeType_isAbstract;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return GRGEN_MODEL.NodeType_Classifier.AttributeType_name;
				case "visibility" : return GRGEN_MODEL.NodeType_Classifier.AttributeType_visibility;
				case "isAbstract" : return GRGEN_MODEL.NodeType_Classifier.AttributeType_isAbstract;
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
			GRGEN_MODEL.@Interface newNode = new GRGEN_MODEL.@Interface();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Classifier:
				case (int) NodeTypes.@Class:
					// copy attributes for: Classifier
					{
						GRGEN_MODEL.IClassifier old = (GRGEN_MODEL.IClassifier) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
					}
					break;
				case (int) NodeTypes.@Interface:
					// copy attributes for: Interface
					{
						GRGEN_MODEL.IInterface old = (GRGEN_MODEL.IInterface) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Variable ***

	public interface IVariable : GRGEN_LIBGR.INode
	{
		string @name { get; set; }
		string @visibility { get; set; }
		bool @isStatic { get; set; }
		bool @isFinal { get; set; }
	}

	public sealed class @Variable : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IVariable
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Variable[] pool = new GRGEN_MODEL.@Variable[10];
		
		// explicit initializations of Variable for target Variable
		// implicit initializations of Variable for target Variable
		static @Variable() {
		}
		
		public @Variable() : base(GRGEN_MODEL.NodeType_Variable.typeVar)
		{
			// implicit initialization, map/set creation of Variable
			// explicit initializations of Variable for target Variable
		}

		public static GRGEN_MODEL.NodeType_Variable TypeInstance { get { return GRGEN_MODEL.NodeType_Variable.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Variable(this); }

		private @Variable(GRGEN_MODEL.@Variable oldElem) : base(GRGEN_MODEL.NodeType_Variable.typeVar)
		{
			_name = oldElem._name;
			_visibility = oldElem._visibility;
			_isStatic = oldElem._isStatic;
			_isFinal = oldElem._isFinal;
		}
		public static GRGEN_MODEL.@Variable CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Variable node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Variable();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Variable
				node.@name = null;
				node.@visibility = null;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Variable for target Variable
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Variable CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Variable node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Variable();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Variable
				node.@name = null;
				node.@visibility = null;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Variable for target Variable
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _visibility;
		public string @visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		private bool _isStatic;
		public bool @isStatic
		{
			get { return _isStatic; }
			set { _isStatic = value; }
		}

		private bool _isFinal;
		public bool @isFinal
		{
			get { return _isFinal; }
			set { _isFinal = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
				case "visibility": return this.@visibility;
				case "isStatic": return this.@isStatic;
				case "isFinal": return this.@isFinal;
			}
			throw new NullReferenceException(
				"The node type \"Variable\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
				case "visibility": this.@visibility = (string) value; return;
				case "isStatic": this.@isStatic = (bool) value; return;
				case "isFinal": this.@isFinal = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Variable\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Variable
			this.@name = null;
			this.@visibility = null;
			this.@isStatic = false;
			this.@isFinal = false;
			// explicit initializations of Variable for target Variable
		}
	}

	public sealed class NodeType_Variable : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Variable typeVar = new GRGEN_MODEL.NodeType_Variable();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public static GRGEN_LIBGR.AttributeType AttributeType_visibility;
		public static GRGEN_LIBGR.AttributeType AttributeType_isStatic;
		public static GRGEN_LIBGR.AttributeType AttributeType_isFinal;
		public NodeType_Variable() : base((int) NodeTypes.@Variable)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
			AttributeType_visibility = new GRGEN_LIBGR.AttributeType("visibility", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
			AttributeType_isStatic = new GRGEN_LIBGR.AttributeType("isStatic", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
			AttributeType_isFinal = new GRGEN_LIBGR.AttributeType("isFinal", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Variable"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Variable();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 4; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_name;
				yield return AttributeType_visibility;
				yield return AttributeType_isStatic;
				yield return AttributeType_isFinal;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return AttributeType_name;
				case "visibility" : return AttributeType_visibility;
				case "isStatic" : return AttributeType_isStatic;
				case "isFinal" : return AttributeType_isFinal;
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
			GRGEN_MODEL.@Variable newNode = new GRGEN_MODEL.@Variable();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Variable:
					// copy attributes for: Variable
					{
						GRGEN_MODEL.IVariable old = (GRGEN_MODEL.IVariable) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isStatic = old.@isStatic;
						newNode.@isFinal = old.@isFinal;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Operation ***

	public interface IOperation : GRGEN_LIBGR.INode
	{
		string @name { get; set; }
		string @visibility { get; set; }
		bool @isAbstract { get; set; }
		bool @isStatic { get; set; }
		bool @isFinal { get; set; }
	}

	public sealed class @Operation : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IOperation
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Operation[] pool = new GRGEN_MODEL.@Operation[10];
		
		// explicit initializations of Operation for target Operation
		// implicit initializations of Operation for target Operation
		static @Operation() {
		}
		
		public @Operation() : base(GRGEN_MODEL.NodeType_Operation.typeVar)
		{
			// implicit initialization, map/set creation of Operation
			// explicit initializations of Operation for target Operation
		}

		public static GRGEN_MODEL.NodeType_Operation TypeInstance { get { return GRGEN_MODEL.NodeType_Operation.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Operation(this); }

		private @Operation(GRGEN_MODEL.@Operation oldElem) : base(GRGEN_MODEL.NodeType_Operation.typeVar)
		{
			_name = oldElem._name;
			_visibility = oldElem._visibility;
			_isAbstract = oldElem._isAbstract;
			_isStatic = oldElem._isStatic;
			_isFinal = oldElem._isFinal;
		}
		public static GRGEN_MODEL.@Operation CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Operation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operation();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Operation
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Operation for target Operation
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Operation CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Operation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operation();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Operation
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Operation for target Operation
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _visibility;
		public string @visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		private bool _isAbstract;
		public bool @isAbstract
		{
			get { return _isAbstract; }
			set { _isAbstract = value; }
		}

		private bool _isStatic;
		public bool @isStatic
		{
			get { return _isStatic; }
			set { _isStatic = value; }
		}

		private bool _isFinal;
		public bool @isFinal
		{
			get { return _isFinal; }
			set { _isFinal = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
				case "visibility": return this.@visibility;
				case "isAbstract": return this.@isAbstract;
				case "isStatic": return this.@isStatic;
				case "isFinal": return this.@isFinal;
			}
			throw new NullReferenceException(
				"The node type \"Operation\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
				case "visibility": this.@visibility = (string) value; return;
				case "isAbstract": this.@isAbstract = (bool) value; return;
				case "isStatic": this.@isStatic = (bool) value; return;
				case "isFinal": this.@isFinal = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Operation\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Operation
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			this.@isStatic = false;
			this.@isFinal = false;
			// explicit initializations of Operation for target Operation
		}
	}

	public sealed class NodeType_Operation : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Operation typeVar = new GRGEN_MODEL.NodeType_Operation();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public static GRGEN_LIBGR.AttributeType AttributeType_visibility;
		public static GRGEN_LIBGR.AttributeType AttributeType_isAbstract;
		public static GRGEN_LIBGR.AttributeType AttributeType_isStatic;
		public static GRGEN_LIBGR.AttributeType AttributeType_isFinal;
		public NodeType_Operation() : base((int) NodeTypes.@Operation)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
			AttributeType_visibility = new GRGEN_LIBGR.AttributeType("visibility", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
			AttributeType_isAbstract = new GRGEN_LIBGR.AttributeType("isAbstract", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
			AttributeType_isStatic = new GRGEN_LIBGR.AttributeType("isStatic", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
			AttributeType_isFinal = new GRGEN_LIBGR.AttributeType("isFinal", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Operation"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Operation();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 5; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_name;
				yield return AttributeType_visibility;
				yield return AttributeType_isAbstract;
				yield return AttributeType_isStatic;
				yield return AttributeType_isFinal;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return AttributeType_name;
				case "visibility" : return AttributeType_visibility;
				case "isAbstract" : return AttributeType_isAbstract;
				case "isStatic" : return AttributeType_isStatic;
				case "isFinal" : return AttributeType_isFinal;
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
			GRGEN_MODEL.@Operation newNode = new GRGEN_MODEL.@Operation();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Operation:
					// copy attributes for: Operation
					{
						GRGEN_MODEL.IOperation old = (GRGEN_MODEL.IOperation) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
						newNode.@isStatic = old.@isStatic;
						newNode.@isFinal = old.@isFinal;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node MethodBody ***

	public interface IMethodBody : GRGEN_LIBGR.INode
	{
	}

	public sealed class @MethodBody : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IMethodBody
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@MethodBody[] pool = new GRGEN_MODEL.@MethodBody[10];
		
		// explicit initializations of MethodBody for target MethodBody
		// implicit initializations of MethodBody for target MethodBody
		static @MethodBody() {
		}
		
		public @MethodBody() : base(GRGEN_MODEL.NodeType_MethodBody.typeVar)
		{
			// implicit initialization, map/set creation of MethodBody
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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of MethodBody
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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of MethodBody
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
			// explicit initializations of MethodBody for target MethodBody
		}
	}

	public sealed class NodeType_MethodBody : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_MethodBody typeVar = new GRGEN_MODEL.NodeType_MethodBody();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public NodeType_MethodBody() : base((int) NodeTypes.@MethodBody)
		{
		}
		public override string Name { get { return "MethodBody"; } }
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

	public interface IExpression : GRGEN_LIBGR.INode
	{
	}

	public sealed class @Expression : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IExpression
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Expression[] pool = new GRGEN_MODEL.@Expression[10];
		
		// explicit initializations of Expression for target Expression
		// implicit initializations of Expression for target Expression
		static @Expression() {
		}
		
		public @Expression() : base(GRGEN_MODEL.NodeType_Expression.typeVar)
		{
			// implicit initialization, map/set creation of Expression
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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Expression
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
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Expression
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
			// explicit initializations of Expression for target Expression
		}
	}

	public sealed class NodeType_Expression : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Expression typeVar = new GRGEN_MODEL.NodeType_Expression();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, };
		public NodeType_Expression() : base((int) NodeTypes.@Expression)
		{
		}
		public override string Name { get { return "Expression"; } }
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

	// *** Node Access ***

	public interface IAccess : IExpression
	{
		bool @this { get; set; }
	}

	public sealed class @Access : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IAccess
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Access[] pool = new GRGEN_MODEL.@Access[10];
		
		// explicit initializations of Expression for target Access
		// implicit initializations of Expression for target Access
		// explicit initializations of Access for target Access
		// implicit initializations of Access for target Access
		static @Access() {
		}
		
		public @Access() : base(GRGEN_MODEL.NodeType_Access.typeVar)
		{
			// implicit initialization, map/set creation of Access
			// explicit initializations of Expression for target Access
			// explicit initializations of Access for target Access
		}

		public static GRGEN_MODEL.NodeType_Access TypeInstance { get { return GRGEN_MODEL.NodeType_Access.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Access(this); }

		private @Access(GRGEN_MODEL.@Access oldElem) : base(GRGEN_MODEL.NodeType_Access.typeVar)
		{
			_this = oldElem._this;
		}
		public static GRGEN_MODEL.@Access CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Access node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Access();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Access
				node.@this = false;
				// explicit initializations of Expression for target Access
				// explicit initializations of Access for target Access
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Access CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Access node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Access();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Access
				node.@this = false;
				// explicit initializations of Expression for target Access
				// explicit initializations of Access for target Access
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool _this;
		public bool @this
		{
			get { return _this; }
			set { _this = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "this": return this.@this;
			}
			throw new NullReferenceException(
				"The node type \"Access\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "this": this.@this = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Access\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Access
			this.@this = false;
			// explicit initializations of Expression for target Access
			// explicit initializations of Access for target Access
		}
	}

	public sealed class NodeType_Access : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Access typeVar = new GRGEN_MODEL.NodeType_Access();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_this;
		public NodeType_Access() : base((int) NodeTypes.@Access)
		{
			AttributeType_this = new GRGEN_LIBGR.AttributeType("this", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Access"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Access();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_this;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "this" : return AttributeType_this;
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
			GRGEN_MODEL.@Access newNode = new GRGEN_MODEL.@Access();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Access:
					// copy attributes for: Access
					{
						GRGEN_MODEL.IAccess old = (GRGEN_MODEL.IAccess) oldNode;
						newNode.@this = old.@this;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Update ***

	public interface IUpdate : IExpression
	{
		bool @this { get; set; }
	}

	public sealed class @Update : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IUpdate
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Update[] pool = new GRGEN_MODEL.@Update[10];
		
		// explicit initializations of Expression for target Update
		// implicit initializations of Expression for target Update
		// explicit initializations of Update for target Update
		// implicit initializations of Update for target Update
		static @Update() {
		}
		
		public @Update() : base(GRGEN_MODEL.NodeType_Update.typeVar)
		{
			// implicit initialization, map/set creation of Update
			// explicit initializations of Expression for target Update
			// explicit initializations of Update for target Update
		}

		public static GRGEN_MODEL.NodeType_Update TypeInstance { get { return GRGEN_MODEL.NodeType_Update.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Update(this); }

		private @Update(GRGEN_MODEL.@Update oldElem) : base(GRGEN_MODEL.NodeType_Update.typeVar)
		{
			_this = oldElem._this;
		}
		public static GRGEN_MODEL.@Update CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Update node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Update();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Update
				node.@this = false;
				// explicit initializations of Expression for target Update
				// explicit initializations of Update for target Update
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Update CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Update node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Update();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Update
				node.@this = false;
				// explicit initializations of Expression for target Update
				// explicit initializations of Update for target Update
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool _this;
		public bool @this
		{
			get { return _this; }
			set { _this = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "this": return this.@this;
			}
			throw new NullReferenceException(
				"The node type \"Update\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "this": this.@this = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Update\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Update
			this.@this = false;
			// explicit initializations of Expression for target Update
			// explicit initializations of Update for target Update
		}
	}

	public sealed class NodeType_Update : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Update typeVar = new GRGEN_MODEL.NodeType_Update();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_this;
		public NodeType_Update() : base((int) NodeTypes.@Update)
		{
			AttributeType_this = new GRGEN_LIBGR.AttributeType("this", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Update"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Update();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_this;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "this" : return AttributeType_this;
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
			GRGEN_MODEL.@Update newNode = new GRGEN_MODEL.@Update();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Update:
					// copy attributes for: Update
					{
						GRGEN_MODEL.IUpdate old = (GRGEN_MODEL.IUpdate) oldNode;
						newNode.@this = old.@this;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Call ***

	public interface ICall : IExpression
	{
		bool @this { get; set; }
		bool @super { get; set; }
	}

	public sealed class @Call : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ICall
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Call[] pool = new GRGEN_MODEL.@Call[10];
		
		// explicit initializations of Expression for target Call
		// implicit initializations of Expression for target Call
		// explicit initializations of Call for target Call
		// implicit initializations of Call for target Call
		static @Call() {
		}
		
		public @Call() : base(GRGEN_MODEL.NodeType_Call.typeVar)
		{
			// implicit initialization, map/set creation of Call
			// explicit initializations of Expression for target Call
			// explicit initializations of Call for target Call
		}

		public static GRGEN_MODEL.NodeType_Call TypeInstance { get { return GRGEN_MODEL.NodeType_Call.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Call(this); }

		private @Call(GRGEN_MODEL.@Call oldElem) : base(GRGEN_MODEL.NodeType_Call.typeVar)
		{
			_this = oldElem._this;
			_super = oldElem._super;
		}
		public static GRGEN_MODEL.@Call CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Call node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Call();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Call
				node.@this = false;
				node.@super = false;
				// explicit initializations of Expression for target Call
				// explicit initializations of Call for target Call
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Call CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Call node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Call();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Call
				node.@this = false;
				node.@super = false;
				// explicit initializations of Expression for target Call
				// explicit initializations of Call for target Call
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool _this;
		public bool @this
		{
			get { return _this; }
			set { _this = value; }
		}

		private bool _super;
		public bool @super
		{
			get { return _super; }
			set { _super = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "this": return this.@this;
				case "super": return this.@super;
			}
			throw new NullReferenceException(
				"The node type \"Call\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "this": this.@this = (bool) value; return;
				case "super": this.@super = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Call\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Call
			this.@this = false;
			this.@super = false;
			// explicit initializations of Expression for target Call
			// explicit initializations of Call for target Call
		}
	}

	public sealed class NodeType_Call : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Call typeVar = new GRGEN_MODEL.NodeType_Call();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_this;
		public static GRGEN_LIBGR.AttributeType AttributeType_super;
		public NodeType_Call() : base((int) NodeTypes.@Call)
		{
			AttributeType_this = new GRGEN_LIBGR.AttributeType("this", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
			AttributeType_super = new GRGEN_LIBGR.AttributeType("super", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, GRGEN_LIBGR.AttributeKind.BooleanAttr, GRGEN_LIBGR.AttributeKind.BooleanAttr);
		}
		public override string Name { get { return "Call"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Call();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_this;
				yield return AttributeType_super;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "this" : return AttributeType_this;
				case "super" : return AttributeType_super;
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
			GRGEN_MODEL.@Call newNode = new GRGEN_MODEL.@Call();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Call:
					// copy attributes for: Call
					{
						GRGEN_MODEL.ICall old = (GRGEN_MODEL.ICall) oldNode;
						newNode.@this = old.@this;
						newNode.@super = old.@super;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Instantiation ***

	public interface IInstantiation : IExpression
	{
	}

	public sealed class @Instantiation : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IInstantiation
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Instantiation[] pool = new GRGEN_MODEL.@Instantiation[10];
		
		// explicit initializations of Expression for target Instantiation
		// implicit initializations of Expression for target Instantiation
		// explicit initializations of Instantiation for target Instantiation
		// implicit initializations of Instantiation for target Instantiation
		static @Instantiation() {
		}
		
		public @Instantiation() : base(GRGEN_MODEL.NodeType_Instantiation.typeVar)
		{
			// implicit initialization, map/set creation of Instantiation
			// explicit initializations of Expression for target Instantiation
			// explicit initializations of Instantiation for target Instantiation
		}

		public static GRGEN_MODEL.NodeType_Instantiation TypeInstance { get { return GRGEN_MODEL.NodeType_Instantiation.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Instantiation(this); }

		private @Instantiation(GRGEN_MODEL.@Instantiation oldElem) : base(GRGEN_MODEL.NodeType_Instantiation.typeVar)
		{
		}
		public static GRGEN_MODEL.@Instantiation CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Instantiation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Instantiation();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Instantiation
				// explicit initializations of Expression for target Instantiation
				// explicit initializations of Instantiation for target Instantiation
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Instantiation CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Instantiation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Instantiation();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Instantiation
				// explicit initializations of Expression for target Instantiation
				// explicit initializations of Instantiation for target Instantiation
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
				"The node type \"Instantiation\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Instantiation\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Instantiation
			// explicit initializations of Expression for target Instantiation
			// explicit initializations of Instantiation for target Instantiation
		}
	}

	public sealed class NodeType_Instantiation : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Instantiation typeVar = new GRGEN_MODEL.NodeType_Instantiation();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, };
		public NodeType_Instantiation() : base((int) NodeTypes.@Instantiation)
		{
		}
		public override string Name { get { return "Instantiation"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Instantiation();
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
			return new GRGEN_MODEL.@Instantiation();
		}

	}

	// *** Node Operator ***

	public interface IOperator : IExpression
	{
		string @name { get; set; }
	}

	public sealed class @Operator : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IOperator
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Operator[] pool = new GRGEN_MODEL.@Operator[10];
		
		// explicit initializations of Expression for target Operator
		// implicit initializations of Expression for target Operator
		// explicit initializations of Operator for target Operator
		// implicit initializations of Operator for target Operator
		static @Operator() {
		}
		
		public @Operator() : base(GRGEN_MODEL.NodeType_Operator.typeVar)
		{
			// implicit initialization, map/set creation of Operator
			// explicit initializations of Expression for target Operator
			// explicit initializations of Operator for target Operator
		}

		public static GRGEN_MODEL.NodeType_Operator TypeInstance { get { return GRGEN_MODEL.NodeType_Operator.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Operator(this); }

		private @Operator(GRGEN_MODEL.@Operator oldElem) : base(GRGEN_MODEL.NodeType_Operator.typeVar)
		{
			_name = oldElem._name;
		}
		public static GRGEN_MODEL.@Operator CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Operator node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operator();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Operator
				node.@name = null;
				// explicit initializations of Expression for target Operator
				// explicit initializations of Operator for target Operator
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Operator CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Operator node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operator();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Operator
				node.@name = null;
				// explicit initializations of Expression for target Operator
				// explicit initializations of Operator for target Operator
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _name;
		public string @name
		{
			get { return _name; }
			set { _name = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
			}
			throw new NullReferenceException(
				"The node type \"Operator\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Operator\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Operator
			this.@name = null;
			// explicit initializations of Expression for target Operator
			// explicit initializations of Operator for target Operator
		}
	}

	public sealed class NodeType_Operator : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Operator typeVar = new GRGEN_MODEL.NodeType_Operator();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public NodeType_Operator() : base((int) NodeTypes.@Operator)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
		}
		public override string Name { get { return "Operator"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Operator();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_name;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "name" : return AttributeType_name;
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
			GRGEN_MODEL.@Operator newNode = new GRGEN_MODEL.@Operator();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Operator:
					// copy attributes for: Operator
					{
						GRGEN_MODEL.IOperator old = (GRGEN_MODEL.IOperator) oldNode;
						newNode.@name = old.@name;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Return ***

	public interface IReturn : IExpression
	{
	}

	public sealed class @Return : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IReturn
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Return[] pool = new GRGEN_MODEL.@Return[10];
		
		// explicit initializations of Expression for target Return
		// implicit initializations of Expression for target Return
		// explicit initializations of Return for target Return
		// implicit initializations of Return for target Return
		static @Return() {
		}
		
		public @Return() : base(GRGEN_MODEL.NodeType_Return.typeVar)
		{
			// implicit initialization, map/set creation of Return
			// explicit initializations of Expression for target Return
			// explicit initializations of Return for target Return
		}

		public static GRGEN_MODEL.NodeType_Return TypeInstance { get { return GRGEN_MODEL.NodeType_Return.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Return(this); }

		private @Return(GRGEN_MODEL.@Return oldElem) : base(GRGEN_MODEL.NodeType_Return.typeVar)
		{
		}
		public static GRGEN_MODEL.@Return CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Return node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Return();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Return
				// explicit initializations of Expression for target Return
				// explicit initializations of Return for target Return
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Return CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Return node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Return();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Return
				// explicit initializations of Expression for target Return
				// explicit initializations of Return for target Return
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
				"The node type \"Return\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Return\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Return
			// explicit initializations of Expression for target Return
			// explicit initializations of Return for target Return
		}
	}

	public sealed class NodeType_Return : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Return typeVar = new GRGEN_MODEL.NodeType_Return();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public NodeType_Return() : base((int) NodeTypes.@Return)
		{
		}
		public override string Name { get { return "Return"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Return();
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
			return new GRGEN_MODEL.@Return();
		}

	}

	// *** Node Block ***

	public interface IBlock : IExpression
	{
	}

	public sealed class @Block : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IBlock
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Block[] pool = new GRGEN_MODEL.@Block[10];
		
		// explicit initializations of Expression for target Block
		// implicit initializations of Expression for target Block
		// explicit initializations of Block for target Block
		// implicit initializations of Block for target Block
		static @Block() {
		}
		
		public @Block() : base(GRGEN_MODEL.NodeType_Block.typeVar)
		{
			// implicit initialization, map/set creation of Block
			// explicit initializations of Expression for target Block
			// explicit initializations of Block for target Block
		}

		public static GRGEN_MODEL.NodeType_Block TypeInstance { get { return GRGEN_MODEL.NodeType_Block.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Block(this); }

		private @Block(GRGEN_MODEL.@Block oldElem) : base(GRGEN_MODEL.NodeType_Block.typeVar)
		{
		}
		public static GRGEN_MODEL.@Block CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Block node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Block();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Block
				// explicit initializations of Expression for target Block
				// explicit initializations of Block for target Block
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Block CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Block node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Block();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Block
				// explicit initializations of Expression for target Block
				// explicit initializations of Block for target Block
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
				"The node type \"Block\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Block\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Block
			// explicit initializations of Expression for target Block
			// explicit initializations of Block for target Block
		}
	}

	public sealed class NodeType_Block : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Block typeVar = new GRGEN_MODEL.NodeType_Block();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public NodeType_Block() : base((int) NodeTypes.@Block)
		{
		}
		public override string Name { get { return "Block"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Block();
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
			return new GRGEN_MODEL.@Block();
		}

	}

	// *** Node Literal ***

	public interface ILiteral : GRGEN_LIBGR.INode
	{
		string @value { get; set; }
	}

	public sealed class @Literal : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ILiteral
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Literal[] pool = new GRGEN_MODEL.@Literal[10];
		
		// explicit initializations of Literal for target Literal
		// implicit initializations of Literal for target Literal
		static @Literal() {
		}
		
		public @Literal() : base(GRGEN_MODEL.NodeType_Literal.typeVar)
		{
			// implicit initialization, map/set creation of Literal
			// explicit initializations of Literal for target Literal
		}

		public static GRGEN_MODEL.NodeType_Literal TypeInstance { get { return GRGEN_MODEL.NodeType_Literal.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Literal(this); }

		private @Literal(GRGEN_MODEL.@Literal oldElem) : base(GRGEN_MODEL.NodeType_Literal.typeVar)
		{
			_value = oldElem._value;
		}
		public static GRGEN_MODEL.@Literal CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Literal node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Literal();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Literal
				node.@value = null;
				// explicit initializations of Literal for target Literal
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Literal CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Literal node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Literal();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Literal
				node.@value = null;
				// explicit initializations of Literal for target Literal
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string _value;
		public string @value
		{
			get { return _value; }
			set { _value = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "value": return this.@value;
			}
			throw new NullReferenceException(
				"The node type \"Literal\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": this.@value = (string) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Literal\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Literal
			this.@value = null;
			// explicit initializations of Literal for target Literal
		}
	}

	public sealed class NodeType_Literal : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Literal typeVar = new GRGEN_MODEL.NodeType_Literal();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_value;
		public NodeType_Literal() : base((int) NodeTypes.@Literal)
		{
			AttributeType_value = new GRGEN_LIBGR.AttributeType("value", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, GRGEN_LIBGR.AttributeKind.StringAttr, GRGEN_LIBGR.AttributeKind.StringAttr);
		}
		public override string Name { get { return "Literal"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Literal();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
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
			GRGEN_MODEL.@Literal newNode = new GRGEN_MODEL.@Literal();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Literal:
					// copy attributes for: Literal
					{
						GRGEN_MODEL.ILiteral old = (GRGEN_MODEL.ILiteral) oldNode;
						newNode.@value = old.@value;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Parameter ***

	public interface IParameter : GRGEN_LIBGR.INode
	{
	}

	public sealed class @Parameter : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IParameter
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Parameter[] pool = new GRGEN_MODEL.@Parameter[10];
		
		// explicit initializations of Parameter for target Parameter
		// implicit initializations of Parameter for target Parameter
		static @Parameter() {
		}
		
		public @Parameter() : base(GRGEN_MODEL.NodeType_Parameter.typeVar)
		{
			// implicit initialization, map/set creation of Parameter
			// explicit initializations of Parameter for target Parameter
		}

		public static GRGEN_MODEL.NodeType_Parameter TypeInstance { get { return GRGEN_MODEL.NodeType_Parameter.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Parameter(this); }

		private @Parameter(GRGEN_MODEL.@Parameter oldElem) : base(GRGEN_MODEL.NodeType_Parameter.typeVar)
		{
		}
		public static GRGEN_MODEL.@Parameter CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Parameter node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Parameter();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Parameter
				// explicit initializations of Parameter for target Parameter
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Parameter CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Parameter node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Parameter();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Parameter
				// explicit initializations of Parameter for target Parameter
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
				"The node type \"Parameter\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Parameter\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of Parameter
			// explicit initializations of Parameter for target Parameter
		}
	}

	public sealed class NodeType_Parameter : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Parameter typeVar = new GRGEN_MODEL.NodeType_Parameter();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public NodeType_Parameter() : base((int) NodeTypes.@Parameter)
		{
		}
		public override string Name { get { return "Parameter"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Parameter();
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
			return new GRGEN_MODEL.@Parameter();
		}

	}

	//
	// Node model
	//

	public sealed class JavaProgramGraphsNodeModel : GRGEN_LIBGR.INodeModel
	{
		public JavaProgramGraphsNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Package.typeVar,
				GRGEN_MODEL.NodeType_Classifier.typeVar,
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Interface.typeVar,
				GRGEN_MODEL.NodeType_Variable.typeVar,
				GRGEN_MODEL.NodeType_Operation.typeVar,
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Access.typeVar,
				GRGEN_MODEL.NodeType_Update.typeVar,
				GRGEN_MODEL.NodeType_Call.typeVar,
				GRGEN_MODEL.NodeType_Instantiation.typeVar,
				GRGEN_MODEL.NodeType_Operator.typeVar,
				GRGEN_MODEL.NodeType_Return.typeVar,
				GRGEN_MODEL.NodeType_Block.typeVar,
				GRGEN_MODEL.NodeType_Literal.typeVar,
				GRGEN_MODEL.NodeType_Parameter.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Package.typeVar,
				GRGEN_MODEL.NodeType_Classifier.typeVar,
				GRGEN_MODEL.NodeType_Variable.typeVar,
				GRGEN_MODEL.NodeType_Operation.typeVar,
				GRGEN_MODEL.NodeType_MethodBody.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Literal.typeVar,
				GRGEN_MODEL.NodeType_Parameter.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Package.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Package.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Package.typeVar,
			};
			GRGEN_MODEL.NodeType_Package.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Package.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Package.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Package.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Package.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Package.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Package.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Classifier.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Classifier.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Classifier.typeVar,
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Interface.typeVar,
			};
			GRGEN_MODEL.NodeType_Classifier.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Classifier.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Interface.typeVar,
			};
			GRGEN_MODEL.NodeType_Classifier.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Classifier.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Classifier.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Classifier.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Classifier.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Class.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Class.typeVar,
			};
			GRGEN_MODEL.NodeType_Class.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Class.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Class.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Classifier.typeVar,
			};
			GRGEN_MODEL.NodeType_Class.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Class.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Classifier.typeVar,
			};
			GRGEN_MODEL.NodeType_Interface.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Interface.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Interface.typeVar,
			};
			GRGEN_MODEL.NodeType_Interface.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Interface.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Interface.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Interface.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Interface.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Classifier.typeVar,
			};
			GRGEN_MODEL.NodeType_Interface.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Interface.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Classifier.typeVar,
			};
			GRGEN_MODEL.NodeType_Variable.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Variable.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Variable.typeVar,
			};
			GRGEN_MODEL.NodeType_Variable.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Variable.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Variable.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Variable.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Variable.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Variable.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Variable.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Operation.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Operation.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Operation.typeVar,
			};
			GRGEN_MODEL.NodeType_Operation.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Operation.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Operation.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Operation.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Operation.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Operation.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Operation.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
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
			};
			GRGEN_MODEL.NodeType_MethodBody.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_MethodBody.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Access.typeVar,
				GRGEN_MODEL.NodeType_Update.typeVar,
				GRGEN_MODEL.NodeType_Call.typeVar,
				GRGEN_MODEL.NodeType_Instantiation.typeVar,
				GRGEN_MODEL.NodeType_Operator.typeVar,
				GRGEN_MODEL.NodeType_Return.typeVar,
				GRGEN_MODEL.NodeType_Block.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Access.typeVar,
				GRGEN_MODEL.NodeType_Update.typeVar,
				GRGEN_MODEL.NodeType_Call.typeVar,
				GRGEN_MODEL.NodeType_Instantiation.typeVar,
				GRGEN_MODEL.NodeType_Operator.typeVar,
				GRGEN_MODEL.NodeType_Return.typeVar,
				GRGEN_MODEL.NodeType_Block.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Expression.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Expression.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Access.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Access.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Access.typeVar,
			};
			GRGEN_MODEL.NodeType_Access.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Access.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Access.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Access.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Access.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Access.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Access.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Update.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Update.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Update.typeVar,
			};
			GRGEN_MODEL.NodeType_Update.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Update.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Update.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Update.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Update.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Update.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Update.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Call.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Call.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Call.typeVar,
			};
			GRGEN_MODEL.NodeType_Call.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Call.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Call.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Call.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Call.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Call.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Call.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Instantiation.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Instantiation.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Instantiation.typeVar,
			};
			GRGEN_MODEL.NodeType_Instantiation.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Instantiation.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Instantiation.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Instantiation.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Instantiation.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Instantiation.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Instantiation.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Operator.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Operator.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Operator.typeVar,
			};
			GRGEN_MODEL.NodeType_Operator.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Operator.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Operator.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Operator.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Operator.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Operator.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Operator.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Return.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Return.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Return.typeVar,
			};
			GRGEN_MODEL.NodeType_Return.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Return.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Return.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Return.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Return.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Return.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Return.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Block.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Block.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Block.typeVar,
			};
			GRGEN_MODEL.NodeType_Block.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Block.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Block.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Block.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Block.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Block.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Block.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Expression.typeVar,
			};
			GRGEN_MODEL.NodeType_Literal.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Literal.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Literal.typeVar,
			};
			GRGEN_MODEL.NodeType_Literal.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Literal.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Literal.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Literal.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Literal.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Literal.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Literal.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Parameter.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Parameter.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Parameter.typeVar,
			};
			GRGEN_MODEL.NodeType_Parameter.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Parameter.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Parameter.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Parameter.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Parameter.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Parameter.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Parameter.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
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
				case "Package" : return GRGEN_MODEL.NodeType_Package.typeVar;
				case "Classifier" : return GRGEN_MODEL.NodeType_Classifier.typeVar;
				case "Class" : return GRGEN_MODEL.NodeType_Class.typeVar;
				case "Interface" : return GRGEN_MODEL.NodeType_Interface.typeVar;
				case "Variable" : return GRGEN_MODEL.NodeType_Variable.typeVar;
				case "Operation" : return GRGEN_MODEL.NodeType_Operation.typeVar;
				case "MethodBody" : return GRGEN_MODEL.NodeType_MethodBody.typeVar;
				case "Expression" : return GRGEN_MODEL.NodeType_Expression.typeVar;
				case "Access" : return GRGEN_MODEL.NodeType_Access.typeVar;
				case "Update" : return GRGEN_MODEL.NodeType_Update.typeVar;
				case "Call" : return GRGEN_MODEL.NodeType_Call.typeVar;
				case "Instantiation" : return GRGEN_MODEL.NodeType_Instantiation.typeVar;
				case "Operator" : return GRGEN_MODEL.NodeType_Operator.typeVar;
				case "Return" : return GRGEN_MODEL.NodeType_Return.typeVar;
				case "Block" : return GRGEN_MODEL.NodeType_Block.typeVar;
				case "Literal" : return GRGEN_MODEL.NodeType_Literal.typeVar;
				case "Parameter" : return GRGEN_MODEL.NodeType_Parameter.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_Package.typeVar,
			GRGEN_MODEL.NodeType_Classifier.typeVar,
			GRGEN_MODEL.NodeType_Class.typeVar,
			GRGEN_MODEL.NodeType_Interface.typeVar,
			GRGEN_MODEL.NodeType_Variable.typeVar,
			GRGEN_MODEL.NodeType_Operation.typeVar,
			GRGEN_MODEL.NodeType_MethodBody.typeVar,
			GRGEN_MODEL.NodeType_Expression.typeVar,
			GRGEN_MODEL.NodeType_Access.typeVar,
			GRGEN_MODEL.NodeType_Update.typeVar,
			GRGEN_MODEL.NodeType_Call.typeVar,
			GRGEN_MODEL.NodeType_Instantiation.typeVar,
			GRGEN_MODEL.NodeType_Operator.typeVar,
			GRGEN_MODEL.NodeType_Return.typeVar,
			GRGEN_MODEL.NodeType_Block.typeVar,
			GRGEN_MODEL.NodeType_Literal.typeVar,
			GRGEN_MODEL.NodeType_Parameter.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_Package),
			typeof(GRGEN_MODEL.NodeType_Classifier),
			typeof(GRGEN_MODEL.NodeType_Class),
			typeof(GRGEN_MODEL.NodeType_Interface),
			typeof(GRGEN_MODEL.NodeType_Variable),
			typeof(GRGEN_MODEL.NodeType_Operation),
			typeof(GRGEN_MODEL.NodeType_MethodBody),
			typeof(GRGEN_MODEL.NodeType_Expression),
			typeof(GRGEN_MODEL.NodeType_Access),
			typeof(GRGEN_MODEL.NodeType_Update),
			typeof(GRGEN_MODEL.NodeType_Call),
			typeof(GRGEN_MODEL.NodeType_Instantiation),
			typeof(GRGEN_MODEL.NodeType_Operator),
			typeof(GRGEN_MODEL.NodeType_Return),
			typeof(GRGEN_MODEL.NodeType_Block),
			typeof(GRGEN_MODEL.NodeType_Literal),
			typeof(GRGEN_MODEL.NodeType_Parameter),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_Package.AttributeType_name,
			GRGEN_MODEL.NodeType_Classifier.AttributeType_name,
			GRGEN_MODEL.NodeType_Classifier.AttributeType_visibility,
			GRGEN_MODEL.NodeType_Classifier.AttributeType_isAbstract,
			GRGEN_MODEL.NodeType_Class.AttributeType_isFinal,
			GRGEN_MODEL.NodeType_Variable.AttributeType_name,
			GRGEN_MODEL.NodeType_Variable.AttributeType_visibility,
			GRGEN_MODEL.NodeType_Variable.AttributeType_isStatic,
			GRGEN_MODEL.NodeType_Variable.AttributeType_isFinal,
			GRGEN_MODEL.NodeType_Operation.AttributeType_name,
			GRGEN_MODEL.NodeType_Operation.AttributeType_visibility,
			GRGEN_MODEL.NodeType_Operation.AttributeType_isAbstract,
			GRGEN_MODEL.NodeType_Operation.AttributeType_isStatic,
			GRGEN_MODEL.NodeType_Operation.AttributeType_isFinal,
			GRGEN_MODEL.NodeType_Access.AttributeType_this,
			GRGEN_MODEL.NodeType_Update.AttributeType_this,
			GRGEN_MODEL.NodeType_Call.AttributeType_this,
			GRGEN_MODEL.NodeType_Call.AttributeType_super,
			GRGEN_MODEL.NodeType_Operator.AttributeType_name,
			GRGEN_MODEL.NodeType_Literal.AttributeType_value,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @belongsTo, @type_, @extends_, @imports, @implements, @parameter, @actualParameter, @binding, @link, @expression, @inBlock, @inClass };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override string Name { get { return "AEdge"; } }
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
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
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
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
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
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
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

	// *** Edge belongsTo ***

	public interface IbelongsTo : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @belongsTo : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IbelongsTo
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@belongsTo[] pool = new GRGEN_MODEL.@belongsTo[10];
		
		// explicit initializations of belongsTo for target belongsTo
		// implicit initializations of belongsTo for target belongsTo
		static @belongsTo() {
		}
		
		public @belongsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_belongsTo.typeVar, source, target)
		{
			// implicit initialization, map/set creation of belongsTo
			// explicit initializations of belongsTo for target belongsTo
		}

		public static GRGEN_MODEL.EdgeType_belongsTo TypeInstance { get { return GRGEN_MODEL.EdgeType_belongsTo.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@belongsTo(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @belongsTo(GRGEN_MODEL.@belongsTo oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_belongsTo.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@belongsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@belongsTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@belongsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of belongsTo
				// explicit initializations of belongsTo for target belongsTo
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@belongsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@belongsTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@belongsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of belongsTo
				// explicit initializations of belongsTo for target belongsTo
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
				"The edge type \"belongsTo\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"belongsTo\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of belongsTo
			// explicit initializations of belongsTo for target belongsTo
		}
	}

	public sealed class EdgeType_belongsTo : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_belongsTo typeVar = new GRGEN_MODEL.EdgeType_belongsTo();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public EdgeType_belongsTo() : base((int) EdgeTypes.@belongsTo)
		{
		}
		public override string Name { get { return "belongsTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@belongsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@belongsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge type_ ***

	public interface Itype_ : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @type_ : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Itype_
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@type_[] pool = new GRGEN_MODEL.@type_[10];
		
		// explicit initializations of type_ for target type_
		// implicit initializations of type_ for target type_
		static @type_() {
		}
		
		public @type_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_type_.typeVar, source, target)
		{
			// implicit initialization, map/set creation of type_
			// explicit initializations of type_ for target type_
		}

		public static GRGEN_MODEL.EdgeType_type_ TypeInstance { get { return GRGEN_MODEL.EdgeType_type_.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@type_(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @type_(GRGEN_MODEL.@type_ oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_type_.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@type_ CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@type_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@type_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of type_
				// explicit initializations of type_ for target type_
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@type_ CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@type_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@type_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of type_
				// explicit initializations of type_ for target type_
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
				"The edge type \"type_\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"type_\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of type_
			// explicit initializations of type_ for target type_
		}
	}

	public sealed class EdgeType_type_ : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_type_ typeVar = new GRGEN_MODEL.EdgeType_type_();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public EdgeType_type_() : base((int) EdgeTypes.@type_)
		{
		}
		public override string Name { get { return "type_"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@type_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@type_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge extends_ ***

	public interface Iextends_ : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @extends_ : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iextends_
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@extends_[] pool = new GRGEN_MODEL.@extends_[10];
		
		// explicit initializations of extends_ for target extends_
		// implicit initializations of extends_ for target extends_
		static @extends_() {
		}
		
		public @extends_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_extends_.typeVar, source, target)
		{
			// implicit initialization, map/set creation of extends_
			// explicit initializations of extends_ for target extends_
		}

		public static GRGEN_MODEL.EdgeType_extends_ TypeInstance { get { return GRGEN_MODEL.EdgeType_extends_.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@extends_(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @extends_(GRGEN_MODEL.@extends_ oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_extends_.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@extends_ CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@extends_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@extends_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of extends_
				// explicit initializations of extends_ for target extends_
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@extends_ CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@extends_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@extends_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of extends_
				// explicit initializations of extends_ for target extends_
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
				"The edge type \"extends_\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"extends_\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of extends_
			// explicit initializations of extends_ for target extends_
		}
	}

	public sealed class EdgeType_extends_ : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_extends_ typeVar = new GRGEN_MODEL.EdgeType_extends_();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public EdgeType_extends_() : base((int) EdgeTypes.@extends_)
		{
		}
		public override string Name { get { return "extends_"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@extends_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@extends_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge imports ***

	public interface Iimports : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @imports : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iimports
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@imports[] pool = new GRGEN_MODEL.@imports[10];
		
		// explicit initializations of imports for target imports
		// implicit initializations of imports for target imports
		static @imports() {
		}
		
		public @imports(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_imports.typeVar, source, target)
		{
			// implicit initialization, map/set creation of imports
			// explicit initializations of imports for target imports
		}

		public static GRGEN_MODEL.EdgeType_imports TypeInstance { get { return GRGEN_MODEL.EdgeType_imports.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@imports(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @imports(GRGEN_MODEL.@imports oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_imports.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@imports CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@imports edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@imports(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of imports
				// explicit initializations of imports for target imports
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@imports CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@imports edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@imports(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of imports
				// explicit initializations of imports for target imports
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
				"The edge type \"imports\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"imports\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of imports
			// explicit initializations of imports for target imports
		}
	}

	public sealed class EdgeType_imports : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_imports typeVar = new GRGEN_MODEL.EdgeType_imports();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public EdgeType_imports() : base((int) EdgeTypes.@imports)
		{
		}
		public override string Name { get { return "imports"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@imports((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@imports((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge implements ***

	public interface Iimplements : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @implements : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iimplements
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@implements[] pool = new GRGEN_MODEL.@implements[10];
		
		// explicit initializations of implements for target implements
		// implicit initializations of implements for target implements
		static @implements() {
		}
		
		public @implements(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_implements.typeVar, source, target)
		{
			// implicit initialization, map/set creation of implements
			// explicit initializations of implements for target implements
		}

		public static GRGEN_MODEL.EdgeType_implements TypeInstance { get { return GRGEN_MODEL.EdgeType_implements.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@implements(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @implements(GRGEN_MODEL.@implements oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_implements.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@implements CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@implements edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@implements(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of implements
				// explicit initializations of implements for target implements
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@implements CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@implements edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@implements(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of implements
				// explicit initializations of implements for target implements
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
				"The edge type \"implements\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"implements\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of implements
			// explicit initializations of implements for target implements
		}
	}

	public sealed class EdgeType_implements : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_implements typeVar = new GRGEN_MODEL.EdgeType_implements();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public EdgeType_implements() : base((int) EdgeTypes.@implements)
		{
		}
		public override string Name { get { return "implements"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@implements((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@implements((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge parameter ***

	public interface Iparameter : GRGEN_LIBGR.IEdge
	{
		int @order { get; set; }
	}

	public sealed class @parameter : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iparameter
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@parameter[] pool = new GRGEN_MODEL.@parameter[10];
		
		// explicit initializations of parameter for target parameter
		// implicit initializations of parameter for target parameter
		static @parameter() {
		}
		
		public @parameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_parameter.typeVar, source, target)
		{
			// implicit initialization, map/set creation of parameter
			// explicit initializations of parameter for target parameter
		}

		public static GRGEN_MODEL.EdgeType_parameter TypeInstance { get { return GRGEN_MODEL.EdgeType_parameter.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@parameter(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @parameter(GRGEN_MODEL.@parameter oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_parameter.typeVar, newSource, newTarget)
		{
			_order = oldElem._order;
		}
		public static GRGEN_MODEL.@parameter CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@parameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@parameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of parameter
				edge.@order = 0;
				// explicit initializations of parameter for target parameter
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@parameter CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@parameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@parameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of parameter
				edge.@order = 0;
				// explicit initializations of parameter for target parameter
			}
			graph.AddEdge(edge, varName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _order;
		public int @order
		{
			get { return _order; }
			set { _order = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "order": return this.@order;
			}
			throw new NullReferenceException(
				"The edge type \"parameter\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "order": this.@order = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"parameter\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of parameter
			this.@order = 0;
			// explicit initializations of parameter for target parameter
		}
	}

	public sealed class EdgeType_parameter : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_parameter typeVar = new GRGEN_MODEL.EdgeType_parameter();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_order;
		public EdgeType_parameter() : base((int) EdgeTypes.@parameter)
		{
			AttributeType_order = new GRGEN_LIBGR.AttributeType("order", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, GRGEN_LIBGR.AttributeKind.IntegerAttr, GRGEN_LIBGR.AttributeKind.IntegerAttr);
		}
		public override string Name { get { return "parameter"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@parameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_order;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "order" : return AttributeType_order;
			}
			return null;
		}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;
			GRGEN_MODEL.@parameter newEdge = new GRGEN_MODEL.@parameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
			switch(oldEdge.Type.TypeID)
			{
				case (int) EdgeTypes.@parameter:
					// copy attributes for: parameter
					{
						GRGEN_MODEL.Iparameter old = (GRGEN_MODEL.Iparameter) oldEdge;
						newEdge.@order = old.@order;
					}
					break;
			}
			return newEdge;
		}

	}

	// *** Edge actualParameter ***

	public interface IactualParameter : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @actualParameter : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IactualParameter
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@actualParameter[] pool = new GRGEN_MODEL.@actualParameter[10];
		
		// explicit initializations of actualParameter for target actualParameter
		// implicit initializations of actualParameter for target actualParameter
		static @actualParameter() {
		}
		
		public @actualParameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_actualParameter.typeVar, source, target)
		{
			// implicit initialization, map/set creation of actualParameter
			// explicit initializations of actualParameter for target actualParameter
		}

		public static GRGEN_MODEL.EdgeType_actualParameter TypeInstance { get { return GRGEN_MODEL.EdgeType_actualParameter.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@actualParameter(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @actualParameter(GRGEN_MODEL.@actualParameter oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_actualParameter.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@actualParameter CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@actualParameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@actualParameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of actualParameter
				// explicit initializations of actualParameter for target actualParameter
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@actualParameter CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@actualParameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@actualParameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of actualParameter
				// explicit initializations of actualParameter for target actualParameter
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
				"The edge type \"actualParameter\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"actualParameter\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of actualParameter
			// explicit initializations of actualParameter for target actualParameter
		}
	}

	public sealed class EdgeType_actualParameter : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_actualParameter typeVar = new GRGEN_MODEL.EdgeType_actualParameter();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, };
		public EdgeType_actualParameter() : base((int) EdgeTypes.@actualParameter)
		{
		}
		public override string Name { get { return "actualParameter"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@actualParameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@actualParameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge binding ***

	public interface Ibinding : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @binding : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Ibinding
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@binding[] pool = new GRGEN_MODEL.@binding[10];
		
		// explicit initializations of binding for target binding
		// implicit initializations of binding for target binding
		static @binding() {
		}
		
		public @binding(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_binding.typeVar, source, target)
		{
			// implicit initialization, map/set creation of binding
			// explicit initializations of binding for target binding
		}

		public static GRGEN_MODEL.EdgeType_binding TypeInstance { get { return GRGEN_MODEL.EdgeType_binding.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@binding(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @binding(GRGEN_MODEL.@binding oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_binding.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@binding CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@binding edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@binding(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of binding
				// explicit initializations of binding for target binding
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@binding CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@binding edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@binding(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of binding
				// explicit initializations of binding for target binding
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
				"The edge type \"binding\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"binding\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of binding
			// explicit initializations of binding for target binding
		}
	}

	public sealed class EdgeType_binding : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_binding typeVar = new GRGEN_MODEL.EdgeType_binding();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, };
		public EdgeType_binding() : base((int) EdgeTypes.@binding)
		{
		}
		public override string Name { get { return "binding"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@binding((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@binding((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge link ***

	public interface Ilink : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @link : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Ilink
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@link[] pool = new GRGEN_MODEL.@link[10];
		
		// explicit initializations of link for target link
		// implicit initializations of link for target link
		static @link() {
		}
		
		public @link(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_link.typeVar, source, target)
		{
			// implicit initialization, map/set creation of link
			// explicit initializations of link for target link
		}

		public static GRGEN_MODEL.EdgeType_link TypeInstance { get { return GRGEN_MODEL.EdgeType_link.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@link(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @link(GRGEN_MODEL.@link oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_link.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@link CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@link edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@link(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of link
				// explicit initializations of link for target link
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@link CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@link edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@link(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of link
				// explicit initializations of link for target link
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
				"The edge type \"link\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"link\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of link
			// explicit initializations of link for target link
		}
	}

	public sealed class EdgeType_link : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_link typeVar = new GRGEN_MODEL.EdgeType_link();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public EdgeType_link() : base((int) EdgeTypes.@link)
		{
		}
		public override string Name { get { return "link"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@link((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@link((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge expression ***

	public interface Iexpression : GRGEN_LIBGR.IEdge
	{
		int @order { get; set; }
	}

	public sealed class @expression : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iexpression
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@expression[] pool = new GRGEN_MODEL.@expression[10];
		
		// explicit initializations of expression for target expression
		// implicit initializations of expression for target expression
		static @expression() {
		}
		
		public @expression(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_expression.typeVar, source, target)
		{
			// implicit initialization, map/set creation of expression
			// explicit initializations of expression for target expression
		}

		public static GRGEN_MODEL.EdgeType_expression TypeInstance { get { return GRGEN_MODEL.EdgeType_expression.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@expression(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @expression(GRGEN_MODEL.@expression oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_expression.typeVar, newSource, newTarget)
		{
			_order = oldElem._order;
		}
		public static GRGEN_MODEL.@expression CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@expression edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@expression(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of expression
				edge.@order = 0;
				// explicit initializations of expression for target expression
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@expression CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@expression edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@expression(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of expression
				edge.@order = 0;
				// explicit initializations of expression for target expression
			}
			graph.AddEdge(edge, varName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _order;
		public int @order
		{
			get { return _order; }
			set { _order = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "order": return this.@order;
			}
			throw new NullReferenceException(
				"The edge type \"expression\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "order": this.@order = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"expression\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of expression
			this.@order = 0;
			// explicit initializations of expression for target expression
		}
	}

	public sealed class EdgeType_expression : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_expression typeVar = new GRGEN_MODEL.EdgeType_expression();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_order;
		public EdgeType_expression() : base((int) EdgeTypes.@expression)
		{
			AttributeType_order = new GRGEN_LIBGR.AttributeType("order", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, GRGEN_LIBGR.AttributeKind.IntegerAttr, GRGEN_LIBGR.AttributeKind.IntegerAttr);
		}
		public override string Name { get { return "expression"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@expression((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_order;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "order" : return AttributeType_order;
			}
			return null;
		}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;
			GRGEN_MODEL.@expression newEdge = new GRGEN_MODEL.@expression((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
			switch(oldEdge.Type.TypeID)
			{
				case (int) EdgeTypes.@expression:
					// copy attributes for: expression
					{
						GRGEN_MODEL.Iexpression old = (GRGEN_MODEL.Iexpression) oldEdge;
						newEdge.@order = old.@order;
					}
					break;
			}
			return newEdge;
		}

	}

	// *** Edge inBlock ***

	public interface IinBlock : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @inBlock : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IinBlock
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@inBlock[] pool = new GRGEN_MODEL.@inBlock[10];
		
		// explicit initializations of inBlock for target inBlock
		// implicit initializations of inBlock for target inBlock
		static @inBlock() {
		}
		
		public @inBlock(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_inBlock.typeVar, source, target)
		{
			// implicit initialization, map/set creation of inBlock
			// explicit initializations of inBlock for target inBlock
		}

		public static GRGEN_MODEL.EdgeType_inBlock TypeInstance { get { return GRGEN_MODEL.EdgeType_inBlock.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@inBlock(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @inBlock(GRGEN_MODEL.@inBlock oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_inBlock.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@inBlock CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@inBlock edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inBlock(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of inBlock
				// explicit initializations of inBlock for target inBlock
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@inBlock CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@inBlock edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inBlock(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of inBlock
				// explicit initializations of inBlock for target inBlock
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
				"The edge type \"inBlock\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"inBlock\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of inBlock
			// explicit initializations of inBlock for target inBlock
		}
	}

	public sealed class EdgeType_inBlock : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_inBlock typeVar = new GRGEN_MODEL.EdgeType_inBlock();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public EdgeType_inBlock() : base((int) EdgeTypes.@inBlock)
		{
		}
		public override string Name { get { return "inBlock"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@inBlock((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@inBlock((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge inClass ***

	public interface IinClass : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @inClass : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IinClass
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@inClass[] pool = new GRGEN_MODEL.@inClass[10];
		
		// explicit initializations of inClass for target inClass
		// implicit initializations of inClass for target inClass
		static @inClass() {
		}
		
		public @inClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_inClass.typeVar, source, target)
		{
			// implicit initialization, map/set creation of inClass
			// explicit initializations of inClass for target inClass
		}

		public static GRGEN_MODEL.EdgeType_inClass TypeInstance { get { return GRGEN_MODEL.EdgeType_inClass.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@inClass(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @inClass(GRGEN_MODEL.@inClass oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_inClass.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@inClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@inClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of inClass
				// explicit initializations of inClass for target inClass
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@inClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@inClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
				// implicit initialization, map/set creation of inClass
				// explicit initializations of inClass for target inClass
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
				"The edge type \"inClass\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"inClass\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set creation of inClass
			// explicit initializations of inClass for target inClass
		}
	}

	public sealed class EdgeType_inClass : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_inClass typeVar = new GRGEN_MODEL.EdgeType_inClass();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public EdgeType_inClass() : base((int) EdgeTypes.@inClass)
		{
		}
		public override string Name { get { return "inClass"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@inClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@inClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class JavaProgramGraphsEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public JavaProgramGraphsEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_belongsTo.typeVar,
				GRGEN_MODEL.EdgeType_type_.typeVar,
				GRGEN_MODEL.EdgeType_extends_.typeVar,
				GRGEN_MODEL.EdgeType_imports.typeVar,
				GRGEN_MODEL.EdgeType_implements.typeVar,
				GRGEN_MODEL.EdgeType_parameter.typeVar,
				GRGEN_MODEL.EdgeType_actualParameter.typeVar,
				GRGEN_MODEL.EdgeType_binding.typeVar,
				GRGEN_MODEL.EdgeType_link.typeVar,
				GRGEN_MODEL.EdgeType_expression.typeVar,
				GRGEN_MODEL.EdgeType_inBlock.typeVar,
				GRGEN_MODEL.EdgeType_inClass.typeVar,
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
				GRGEN_MODEL.EdgeType_belongsTo.typeVar,
				GRGEN_MODEL.EdgeType_type_.typeVar,
				GRGEN_MODEL.EdgeType_extends_.typeVar,
				GRGEN_MODEL.EdgeType_imports.typeVar,
				GRGEN_MODEL.EdgeType_implements.typeVar,
				GRGEN_MODEL.EdgeType_parameter.typeVar,
				GRGEN_MODEL.EdgeType_actualParameter.typeVar,
				GRGEN_MODEL.EdgeType_binding.typeVar,
				GRGEN_MODEL.EdgeType_link.typeVar,
				GRGEN_MODEL.EdgeType_expression.typeVar,
				GRGEN_MODEL.EdgeType_inBlock.typeVar,
				GRGEN_MODEL.EdgeType_inClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_belongsTo.typeVar,
				GRGEN_MODEL.EdgeType_type_.typeVar,
				GRGEN_MODEL.EdgeType_extends_.typeVar,
				GRGEN_MODEL.EdgeType_imports.typeVar,
				GRGEN_MODEL.EdgeType_implements.typeVar,
				GRGEN_MODEL.EdgeType_parameter.typeVar,
				GRGEN_MODEL.EdgeType_actualParameter.typeVar,
				GRGEN_MODEL.EdgeType_binding.typeVar,
				GRGEN_MODEL.EdgeType_link.typeVar,
				GRGEN_MODEL.EdgeType_expression.typeVar,
				GRGEN_MODEL.EdgeType_inBlock.typeVar,
				GRGEN_MODEL.EdgeType_inClass.typeVar,
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
			GRGEN_MODEL.EdgeType_belongsTo.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_belongsTo.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_belongsTo.typeVar,
			};
			GRGEN_MODEL.EdgeType_belongsTo.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_belongsTo.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_belongsTo.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_belongsTo.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_belongsTo.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_belongsTo.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_belongsTo.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_type_.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_type_.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_type_.typeVar,
			};
			GRGEN_MODEL.EdgeType_type_.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_type_.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_type_.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_type_.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_type_.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_type_.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_type_.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_extends_.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_extends_.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_extends_.typeVar,
			};
			GRGEN_MODEL.EdgeType_extends_.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_extends_.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_extends_.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_extends_.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_extends_.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_extends_.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_extends_.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_imports.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_imports.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_imports.typeVar,
			};
			GRGEN_MODEL.EdgeType_imports.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_imports.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_imports.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_imports.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_imports.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_imports.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_imports.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_implements.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_implements.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_implements.typeVar,
			};
			GRGEN_MODEL.EdgeType_implements.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_implements.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_implements.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_implements.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_implements.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_implements.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_implements.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_parameter.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_parameter.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_parameter.typeVar,
			};
			GRGEN_MODEL.EdgeType_parameter.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_parameter.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_parameter.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_parameter.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_parameter.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_parameter.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_parameter.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_actualParameter.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_actualParameter.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_actualParameter.typeVar,
			};
			GRGEN_MODEL.EdgeType_actualParameter.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_actualParameter.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_actualParameter.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_actualParameter.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_actualParameter.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_actualParameter.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_actualParameter.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_binding.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_binding.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_binding.typeVar,
			};
			GRGEN_MODEL.EdgeType_binding.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_binding.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_binding.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_binding.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_binding.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_binding.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_binding.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_link.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_link.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_link.typeVar,
			};
			GRGEN_MODEL.EdgeType_link.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_link.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_link.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_link.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_link.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_link.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_link.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_expression.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_expression.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_expression.typeVar,
			};
			GRGEN_MODEL.EdgeType_expression.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_expression.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_expression.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_expression.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_expression.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_expression.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_expression.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_inBlock.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_inBlock.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_inBlock.typeVar,
			};
			GRGEN_MODEL.EdgeType_inBlock.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_inBlock.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_inBlock.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_inBlock.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_inBlock.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_inBlock.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_inBlock.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_inClass.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_inClass.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_inClass.typeVar,
			};
			GRGEN_MODEL.EdgeType_inClass.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_inClass.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_inClass.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_inClass.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_inClass.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_inClass.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_inClass.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
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
				case "belongsTo" : return GRGEN_MODEL.EdgeType_belongsTo.typeVar;
				case "type_" : return GRGEN_MODEL.EdgeType_type_.typeVar;
				case "extends_" : return GRGEN_MODEL.EdgeType_extends_.typeVar;
				case "imports" : return GRGEN_MODEL.EdgeType_imports.typeVar;
				case "implements" : return GRGEN_MODEL.EdgeType_implements.typeVar;
				case "parameter" : return GRGEN_MODEL.EdgeType_parameter.typeVar;
				case "actualParameter" : return GRGEN_MODEL.EdgeType_actualParameter.typeVar;
				case "binding" : return GRGEN_MODEL.EdgeType_binding.typeVar;
				case "link" : return GRGEN_MODEL.EdgeType_link.typeVar;
				case "expression" : return GRGEN_MODEL.EdgeType_expression.typeVar;
				case "inBlock" : return GRGEN_MODEL.EdgeType_inBlock.typeVar;
				case "inClass" : return GRGEN_MODEL.EdgeType_inClass.typeVar;
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
			GRGEN_MODEL.EdgeType_belongsTo.typeVar,
			GRGEN_MODEL.EdgeType_type_.typeVar,
			GRGEN_MODEL.EdgeType_extends_.typeVar,
			GRGEN_MODEL.EdgeType_imports.typeVar,
			GRGEN_MODEL.EdgeType_implements.typeVar,
			GRGEN_MODEL.EdgeType_parameter.typeVar,
			GRGEN_MODEL.EdgeType_actualParameter.typeVar,
			GRGEN_MODEL.EdgeType_binding.typeVar,
			GRGEN_MODEL.EdgeType_link.typeVar,
			GRGEN_MODEL.EdgeType_expression.typeVar,
			GRGEN_MODEL.EdgeType_inBlock.typeVar,
			GRGEN_MODEL.EdgeType_inClass.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_belongsTo),
			typeof(GRGEN_MODEL.EdgeType_type_),
			typeof(GRGEN_MODEL.EdgeType_extends_),
			typeof(GRGEN_MODEL.EdgeType_imports),
			typeof(GRGEN_MODEL.EdgeType_implements),
			typeof(GRGEN_MODEL.EdgeType_parameter),
			typeof(GRGEN_MODEL.EdgeType_actualParameter),
			typeof(GRGEN_MODEL.EdgeType_binding),
			typeof(GRGEN_MODEL.EdgeType_link),
			typeof(GRGEN_MODEL.EdgeType_expression),
			typeof(GRGEN_MODEL.EdgeType_inBlock),
			typeof(GRGEN_MODEL.EdgeType_inClass),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.EdgeType_parameter.AttributeType_order,
			GRGEN_MODEL.EdgeType_expression.AttributeType_order,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class JavaProgramGraphsGraphModel : GRGEN_LIBGR.IGraphModel
	{
		private JavaProgramGraphsNodeModel nodeModel = new JavaProgramGraphsNodeModel();
		private JavaProgramGraphsEdgeModel edgeModel = new JavaProgramGraphsEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "JavaProgramGraphs"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "8fd694f4f8cc691f8759cb852bb0c81c"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class JavaProgramGraphs : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public JavaProgramGraphs() : base(GetNextGraphName())
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

		public GRGEN_MODEL.@Package CreateNodePackage()
		{
			return GRGEN_MODEL.@Package.CreateNode(this);
		}

		public GRGEN_MODEL.@Package CreateNodePackage(string varName)
		{
			return GRGEN_MODEL.@Package.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Classifier CreateNodeClassifier()
		{
			return GRGEN_MODEL.@Classifier.CreateNode(this);
		}

		public GRGEN_MODEL.@Classifier CreateNodeClassifier(string varName)
		{
			return GRGEN_MODEL.@Classifier.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Class CreateNodeClass()
		{
			return GRGEN_MODEL.@Class.CreateNode(this);
		}

		public GRGEN_MODEL.@Class CreateNodeClass(string varName)
		{
			return GRGEN_MODEL.@Class.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Interface CreateNodeInterface()
		{
			return GRGEN_MODEL.@Interface.CreateNode(this);
		}

		public GRGEN_MODEL.@Interface CreateNodeInterface(string varName)
		{
			return GRGEN_MODEL.@Interface.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Variable CreateNodeVariable()
		{
			return GRGEN_MODEL.@Variable.CreateNode(this);
		}

		public GRGEN_MODEL.@Variable CreateNodeVariable(string varName)
		{
			return GRGEN_MODEL.@Variable.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Operation CreateNodeOperation()
		{
			return GRGEN_MODEL.@Operation.CreateNode(this);
		}

		public GRGEN_MODEL.@Operation CreateNodeOperation(string varName)
		{
			return GRGEN_MODEL.@Operation.CreateNode(this, varName);
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

		public GRGEN_MODEL.@Access CreateNodeAccess()
		{
			return GRGEN_MODEL.@Access.CreateNode(this);
		}

		public GRGEN_MODEL.@Access CreateNodeAccess(string varName)
		{
			return GRGEN_MODEL.@Access.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Update CreateNodeUpdate()
		{
			return GRGEN_MODEL.@Update.CreateNode(this);
		}

		public GRGEN_MODEL.@Update CreateNodeUpdate(string varName)
		{
			return GRGEN_MODEL.@Update.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Call CreateNodeCall()
		{
			return GRGEN_MODEL.@Call.CreateNode(this);
		}

		public GRGEN_MODEL.@Call CreateNodeCall(string varName)
		{
			return GRGEN_MODEL.@Call.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Instantiation CreateNodeInstantiation()
		{
			return GRGEN_MODEL.@Instantiation.CreateNode(this);
		}

		public GRGEN_MODEL.@Instantiation CreateNodeInstantiation(string varName)
		{
			return GRGEN_MODEL.@Instantiation.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Operator CreateNodeOperator()
		{
			return GRGEN_MODEL.@Operator.CreateNode(this);
		}

		public GRGEN_MODEL.@Operator CreateNodeOperator(string varName)
		{
			return GRGEN_MODEL.@Operator.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Return CreateNodeReturn()
		{
			return GRGEN_MODEL.@Return.CreateNode(this);
		}

		public GRGEN_MODEL.@Return CreateNodeReturn(string varName)
		{
			return GRGEN_MODEL.@Return.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Block CreateNodeBlock()
		{
			return GRGEN_MODEL.@Block.CreateNode(this);
		}

		public GRGEN_MODEL.@Block CreateNodeBlock(string varName)
		{
			return GRGEN_MODEL.@Block.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Literal CreateNodeLiteral()
		{
			return GRGEN_MODEL.@Literal.CreateNode(this);
		}

		public GRGEN_MODEL.@Literal CreateNodeLiteral(string varName)
		{
			return GRGEN_MODEL.@Literal.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Parameter CreateNodeParameter()
		{
			return GRGEN_MODEL.@Parameter.CreateNode(this);
		}

		public GRGEN_MODEL.@Parameter CreateNodeParameter(string varName)
		{
			return GRGEN_MODEL.@Parameter.CreateNode(this, varName);
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

		public @GRGEN_MODEL.@belongsTo CreateEdgebelongsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@belongsTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@belongsTo CreateEdgebelongsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@belongsTo.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@type_ CreateEdgetype_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@type_.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@type_ CreateEdgetype_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@type_.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@extends_ CreateEdgeextends_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@extends_.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@extends_ CreateEdgeextends_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@extends_.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@imports CreateEdgeimports(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@imports.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@imports CreateEdgeimports(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@imports.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@implements CreateEdgeimplements(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@implements.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@implements CreateEdgeimplements(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@implements.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@parameter CreateEdgeparameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@parameter.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@parameter CreateEdgeparameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@parameter.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@actualParameter CreateEdgeactualParameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@actualParameter.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@actualParameter CreateEdgeactualParameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@actualParameter.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@binding CreateEdgebinding(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@binding.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@binding CreateEdgebinding(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@binding.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@link CreateEdgelink(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@link.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@link CreateEdgelink(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@link.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@expression CreateEdgeexpression(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@expression.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@expression CreateEdgeexpression(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@expression.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@inBlock CreateEdgeinBlock(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@inBlock.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@inBlock CreateEdgeinBlock(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@inBlock.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@inClass CreateEdgeinClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@inClass.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@inClass CreateEdgeinClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@inClass.CreateEdge(this, source, target, varName);
		}

		private JavaProgramGraphsNodeModel nodeModel = new JavaProgramGraphsNodeModel();
		private JavaProgramGraphsEdgeModel edgeModel = new JavaProgramGraphsEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "JavaProgramGraphs"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "8fd694f4f8cc691f8759cb852bb0c81c"; } }
	}
}
