// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\JavaProgramGraphs-GraBaTs08\JavaProgramGraphs.grg" on Sun Jan 12 22:15:16 CET 2020

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_JavaProgramGraphs;

namespace de.unika.ipd.grGen.Model_JavaProgramGraphs
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

	public enum NodeTypes { @Node=0, @Package=1, @Classifier=2, @Class=3, @Interface=4, @Variable=5, @Operation=6, @MethodBody=7, @Expression=8, @Access=9, @Update=10, @Call=11, @Instantiation=12, @Operator=13, @Return=14, @Block=15, @Literal=16, @Parameter=17 };

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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Node"; } }
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

	// *** Node Package ***

	public interface IPackage : GRGEN_LIBGR.INode
	{
		string @name { get; set; }
	}

	public sealed partial class @Package : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IPackage
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Package[] pool = new GRGEN_MODEL.@Package[10];
		
		// explicit initializations of Package for target Package
		// implicit initializations of Package for target Package
		static @Package() {
		}
		
		public @Package() : base(GRGEN_MODEL.NodeType_Package.typeVar)
		{
			// implicit initialization, container creation of Package
			// explicit initializations of Package for target Package
		}

		public static GRGEN_MODEL.NodeType_Package TypeInstance { get { return GRGEN_MODEL.NodeType_Package.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Package(this); }

		private @Package(GRGEN_MODEL.@Package oldElem) : base(GRGEN_MODEL.NodeType_Package.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Package)) return false;
			@Package that_ = (@Package)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Package CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Package node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Package();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Package
				node.@name = null;
				// explicit initializations of Package for target Package
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Package CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Package node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Package();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Package
				node.@name = null;
				// explicit initializations of Package for target Package
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
			}
			throw new NullReferenceException(
				"The node type \"Package\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Package\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Package
			this.@name = null;
			// explicit initializations of Package for target Package
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Package does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Package does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Package : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Package typeVar = new GRGEN_MODEL.NodeType_Package();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public NodeType_Package() : base((int) NodeTypes.@Package)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
		}
		public override string Name { get { return "Package"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Package"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IPackage"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Package"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Package();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Package newNode = new GRGEN_MODEL.@Package();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Package:
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

	public class Comparer_Package_name : Comparer<GRGEN_MODEL.IPackage>
	{
		private static GRGEN_MODEL.IPackage nodeBearingAttributeForSearch = new GRGEN_MODEL.@Package();
		private static Comparer_Package_name thisComparer = new Comparer_Package_name();
		public override int Compare(GRGEN_MODEL.IPackage a, GRGEN_MODEL.IPackage b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IPackage> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IPackage> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IPackage> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IPackage> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IPackage> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IPackage> ArrayOrderAscendingBy(List<GRGEN_MODEL.IPackage> list)
		{
			List<GRGEN_MODEL.IPackage> newList = new List<GRGEN_MODEL.IPackage>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Classifier ***

	public interface IClassifier : GRGEN_LIBGR.INode
	{
		string @name { get; set; }
		string @visibility { get; set; }
		bool @isAbstract { get; set; }
	}

	public sealed partial class @Classifier : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IClassifier
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Classifier[] pool = new GRGEN_MODEL.@Classifier[10];
		
		// explicit initializations of Classifier for target Classifier
		// implicit initializations of Classifier for target Classifier
		static @Classifier() {
		}
		
		public @Classifier() : base(GRGEN_MODEL.NodeType_Classifier.typeVar)
		{
			// implicit initialization, container creation of Classifier
			// explicit initializations of Classifier for target Classifier
		}

		public static GRGEN_MODEL.NodeType_Classifier TypeInstance { get { return GRGEN_MODEL.NodeType_Classifier.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Classifier(this); }

		private @Classifier(GRGEN_MODEL.@Classifier oldElem) : base(GRGEN_MODEL.NodeType_Classifier.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
			visibility_M0no_suXx_h4rD = oldElem.visibility_M0no_suXx_h4rD;
			isAbstract_M0no_suXx_h4rD = oldElem.isAbstract_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Classifier)) return false;
			@Classifier that_ = (@Classifier)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
				&& visibility_M0no_suXx_h4rD == that_.visibility_M0no_suXx_h4rD
				&& isAbstract_M0no_suXx_h4rD == that_.isAbstract_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Classifier CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Classifier node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Classifier();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Classifier
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Classifier
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Classifier CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Classifier node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Classifier();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Classifier
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Classifier
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}

		private string visibility_M0no_suXx_h4rD;
		public string @visibility
		{
			get { return visibility_M0no_suXx_h4rD; }
			set { visibility_M0no_suXx_h4rD = value; }
		}

		private bool isAbstract_M0no_suXx_h4rD;
		public bool @isAbstract
		{
			get { return isAbstract_M0no_suXx_h4rD; }
			set { isAbstract_M0no_suXx_h4rD = value; }
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
				"The node type \"Classifier\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"Classifier\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Classifier
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			// explicit initializations of Classifier for target Classifier
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Classifier does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Classifier does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Classifier : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Classifier typeVar = new GRGEN_MODEL.NodeType_Classifier();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public static GRGEN_LIBGR.AttributeType AttributeType_visibility;
		public static GRGEN_LIBGR.AttributeType AttributeType_isAbstract;
		public NodeType_Classifier() : base((int) NodeTypes.@Classifier)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_visibility = new GRGEN_LIBGR.AttributeType("visibility", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_isAbstract = new GRGEN_LIBGR.AttributeType("isAbstract", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Classifier"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Classifier"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IClassifier"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Classifier"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Classifier();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Classifier newNode = new GRGEN_MODEL.@Classifier();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Classifier:
				case (int) GRGEN_MODEL.NodeTypes.@Class:
				case (int) GRGEN_MODEL.NodeTypes.@Interface:
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

	public class Comparer_Classifier_name : Comparer<GRGEN_MODEL.IClassifier>
	{
		private static GRGEN_MODEL.IClassifier nodeBearingAttributeForSearch = new GRGEN_MODEL.@Classifier();
		private static Comparer_Classifier_name thisComparer = new Comparer_Classifier_name();
		public override int Compare(GRGEN_MODEL.IClassifier a, GRGEN_MODEL.IClassifier b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClassifier> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClassifier> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClassifier> list)
		{
			List<GRGEN_MODEL.IClassifier> newList = new List<GRGEN_MODEL.IClassifier>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Classifier_visibility : Comparer<GRGEN_MODEL.IClassifier>
	{
		private static GRGEN_MODEL.IClassifier nodeBearingAttributeForSearch = new GRGEN_MODEL.@Classifier();
		private static Comparer_Classifier_visibility thisComparer = new Comparer_Classifier_visibility();
		public override int Compare(GRGEN_MODEL.IClassifier a, GRGEN_MODEL.IClassifier b)
		{
			return StringComparer.InvariantCulture.Compare(a.@visibility, b.@visibility);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClassifier> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClassifier> list, string entry)
		{
			nodeBearingAttributeForSearch.@visibility = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClassifier> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClassifier> list)
		{
			List<GRGEN_MODEL.IClassifier> newList = new List<GRGEN_MODEL.IClassifier>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Classifier_isAbstract : Comparer<GRGEN_MODEL.IClassifier>
	{
		private static GRGEN_MODEL.IClassifier nodeBearingAttributeForSearch = new GRGEN_MODEL.@Classifier();
		private static Comparer_Classifier_isAbstract thisComparer = new Comparer_Classifier_isAbstract();
		public override int Compare(GRGEN_MODEL.IClassifier a, GRGEN_MODEL.IClassifier b)
		{
			return a.@isAbstract.CompareTo(b.@isAbstract);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClassifier> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClassifier> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClassifier> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClassifier> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClassifier> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isAbstract = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClassifier> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClassifier> list)
		{
			List<GRGEN_MODEL.IClassifier> newList = new List<GRGEN_MODEL.IClassifier>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Class ***

	public interface IClass : IClassifier
	{
		bool @isFinal { get; set; }
	}

	public sealed partial class @Class : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IClass
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
			// implicit initialization, container creation of Class
			// explicit initializations of Classifier for target Class
			// explicit initializations of Class for target Class
		}

		public static GRGEN_MODEL.NodeType_Class TypeInstance { get { return GRGEN_MODEL.NodeType_Class.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Class(this); }

		private @Class(GRGEN_MODEL.@Class oldElem) : base(GRGEN_MODEL.NodeType_Class.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
			visibility_M0no_suXx_h4rD = oldElem.visibility_M0no_suXx_h4rD;
			isAbstract_M0no_suXx_h4rD = oldElem.isAbstract_M0no_suXx_h4rD;
			isFinal_M0no_suXx_h4rD = oldElem.isFinal_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Class)) return false;
			@Class that_ = (@Class)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
				&& visibility_M0no_suXx_h4rD == that_.visibility_M0no_suXx_h4rD
				&& isAbstract_M0no_suXx_h4rD == that_.isAbstract_M0no_suXx_h4rD
				&& isFinal_M0no_suXx_h4rD == that_.isFinal_M0no_suXx_h4rD
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
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				node.@isFinal = false;
				// explicit initializations of Classifier for target Class
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


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}

		private string visibility_M0no_suXx_h4rD;
		public string @visibility
		{
			get { return visibility_M0no_suXx_h4rD; }
			set { visibility_M0no_suXx_h4rD = value; }
		}

		private bool isAbstract_M0no_suXx_h4rD;
		public bool @isAbstract
		{
			get { return isAbstract_M0no_suXx_h4rD; }
			set { isAbstract_M0no_suXx_h4rD = value; }
		}

		private bool isFinal_M0no_suXx_h4rD;
		public bool @isFinal
		{
			get { return isFinal_M0no_suXx_h4rD; }
			set { isFinal_M0no_suXx_h4rD = value; }
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
				"The node type \"Class\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"Class\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Class
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			this.@isFinal = false;
			// explicit initializations of Classifier for target Class
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
		public static bool[] isA = new bool[] { true, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_isFinal;
		public NodeType_Class() : base((int) NodeTypes.@Class)
		{
			AttributeType_isFinal = new GRGEN_LIBGR.AttributeType("isFinal", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Class"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Class"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IClass"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Class"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Class();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Class newNode = new GRGEN_MODEL.@Class();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Classifier:
				case (int) GRGEN_MODEL.NodeTypes.@Interface:
					// copy attributes for: Classifier
					{
						GRGEN_MODEL.IClassifier old = (GRGEN_MODEL.IClassifier) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@Class:
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

	public class Comparer_Class_name : Comparer<GRGEN_MODEL.IClass>
	{
		private static GRGEN_MODEL.IClass nodeBearingAttributeForSearch = new GRGEN_MODEL.@Class();
		private static Comparer_Class_name thisComparer = new Comparer_Class_name();
		public override int Compare(GRGEN_MODEL.IClass a, GRGEN_MODEL.IClass b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClass> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClass> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClass> list)
		{
			List<GRGEN_MODEL.IClass> newList = new List<GRGEN_MODEL.IClass>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Class_visibility : Comparer<GRGEN_MODEL.IClass>
	{
		private static GRGEN_MODEL.IClass nodeBearingAttributeForSearch = new GRGEN_MODEL.@Class();
		private static Comparer_Class_visibility thisComparer = new Comparer_Class_visibility();
		public override int Compare(GRGEN_MODEL.IClass a, GRGEN_MODEL.IClass b)
		{
			return StringComparer.InvariantCulture.Compare(a.@visibility, b.@visibility);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClass> list, string entry)
		{
			nodeBearingAttributeForSearch.@visibility = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClass> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClass> list)
		{
			List<GRGEN_MODEL.IClass> newList = new List<GRGEN_MODEL.IClass>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Class_isAbstract : Comparer<GRGEN_MODEL.IClass>
	{
		private static GRGEN_MODEL.IClass nodeBearingAttributeForSearch = new GRGEN_MODEL.@Class();
		private static Comparer_Class_isAbstract thisComparer = new Comparer_Class_isAbstract();
		public override int Compare(GRGEN_MODEL.IClass a, GRGEN_MODEL.IClass b)
		{
			return a.@isAbstract.CompareTo(b.@isAbstract);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClass> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isAbstract = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClass> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClass> list)
		{
			List<GRGEN_MODEL.IClass> newList = new List<GRGEN_MODEL.IClass>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Class_isFinal : Comparer<GRGEN_MODEL.IClass>
	{
		private static GRGEN_MODEL.IClass nodeBearingAttributeForSearch = new GRGEN_MODEL.@Class();
		private static Comparer_Class_isFinal thisComparer = new Comparer_Class_isFinal();
		public override int Compare(GRGEN_MODEL.IClass a, GRGEN_MODEL.IClass b)
		{
			return a.@isFinal.CompareTo(b.@isFinal);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IClass> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IClass> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isFinal = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IClass> ArrayOrderAscendingBy(List<GRGEN_MODEL.IClass> list)
		{
			List<GRGEN_MODEL.IClass> newList = new List<GRGEN_MODEL.IClass>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Interface ***

	public interface IInterface : IClassifier
	{
	}

	public sealed partial class @Interface : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IInterface
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
			// implicit initialization, container creation of Interface
			// explicit initializations of Classifier for target Interface
			// explicit initializations of Interface for target Interface
		}

		public static GRGEN_MODEL.NodeType_Interface TypeInstance { get { return GRGEN_MODEL.NodeType_Interface.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Interface(this); }

		private @Interface(GRGEN_MODEL.@Interface oldElem) : base(GRGEN_MODEL.NodeType_Interface.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
			visibility_M0no_suXx_h4rD = oldElem.visibility_M0no_suXx_h4rD;
			isAbstract_M0no_suXx_h4rD = oldElem.isAbstract_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Interface)) return false;
			@Interface that_ = (@Interface)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
				&& visibility_M0no_suXx_h4rD == that_.visibility_M0no_suXx_h4rD
				&& isAbstract_M0no_suXx_h4rD == that_.isAbstract_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Interface CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Interface node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Interface();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Interface
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Interface
				// explicit initializations of Interface for target Interface
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Interface CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Interface node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Interface();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Interface
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				// explicit initializations of Classifier for target Interface
				// explicit initializations of Interface for target Interface
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}

		private string visibility_M0no_suXx_h4rD;
		public string @visibility
		{
			get { return visibility_M0no_suXx_h4rD; }
			set { visibility_M0no_suXx_h4rD = value; }
		}

		private bool isAbstract_M0no_suXx_h4rD;
		public bool @isAbstract
		{
			get { return isAbstract_M0no_suXx_h4rD; }
			set { isAbstract_M0no_suXx_h4rD = value; }
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
				"The node type \"Interface\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"Interface\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Interface
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			// explicit initializations of Classifier for target Interface
			// explicit initializations of Interface for target Interface
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Interface does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Interface does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Interface : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Interface typeVar = new GRGEN_MODEL.NodeType_Interface();
		public static bool[] isA = new bool[] { true, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Interface() : base((int) NodeTypes.@Interface)
		{
		}
		public override string Name { get { return "Interface"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Interface"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IInterface"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Interface"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Interface();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Interface newNode = new GRGEN_MODEL.@Interface();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Classifier:
				case (int) GRGEN_MODEL.NodeTypes.@Class:
					// copy attributes for: Classifier
					{
						GRGEN_MODEL.IClassifier old = (GRGEN_MODEL.IClassifier) oldNode;
						newNode.@name = old.@name;
						newNode.@visibility = old.@visibility;
						newNode.@isAbstract = old.@isAbstract;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@Interface:
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

	public class Comparer_Interface_name : Comparer<GRGEN_MODEL.IInterface>
	{
		private static GRGEN_MODEL.IInterface nodeBearingAttributeForSearch = new GRGEN_MODEL.@Interface();
		private static Comparer_Interface_name thisComparer = new Comparer_Interface_name();
		public override int Compare(GRGEN_MODEL.IInterface a, GRGEN_MODEL.IInterface b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IInterface> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IInterface> ArrayOrderAscendingBy(List<GRGEN_MODEL.IInterface> list)
		{
			List<GRGEN_MODEL.IInterface> newList = new List<GRGEN_MODEL.IInterface>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Interface_visibility : Comparer<GRGEN_MODEL.IInterface>
	{
		private static GRGEN_MODEL.IInterface nodeBearingAttributeForSearch = new GRGEN_MODEL.@Interface();
		private static Comparer_Interface_visibility thisComparer = new Comparer_Interface_visibility();
		public override int Compare(GRGEN_MODEL.IInterface a, GRGEN_MODEL.IInterface b)
		{
			return StringComparer.InvariantCulture.Compare(a.@visibility, b.@visibility);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IInterface> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IInterface> list, string entry)
		{
			nodeBearingAttributeForSearch.@visibility = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IInterface> ArrayOrderAscendingBy(List<GRGEN_MODEL.IInterface> list)
		{
			List<GRGEN_MODEL.IInterface> newList = new List<GRGEN_MODEL.IInterface>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Interface_isAbstract : Comparer<GRGEN_MODEL.IInterface>
	{
		private static GRGEN_MODEL.IInterface nodeBearingAttributeForSearch = new GRGEN_MODEL.@Interface();
		private static Comparer_Interface_isAbstract thisComparer = new Comparer_Interface_isAbstract();
		public override int Compare(GRGEN_MODEL.IInterface a, GRGEN_MODEL.IInterface b)
		{
			return a.@isAbstract.CompareTo(b.@isAbstract);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IInterface> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IInterface> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IInterface> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IInterface> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IInterface> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isAbstract = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IInterface> ArrayOrderAscendingBy(List<GRGEN_MODEL.IInterface> list)
		{
			List<GRGEN_MODEL.IInterface> newList = new List<GRGEN_MODEL.IInterface>(list);
			newList.Sort(thisComparer);
			return newList;
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

	public sealed partial class @Variable : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IVariable
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Variable[] pool = new GRGEN_MODEL.@Variable[10];
		
		// explicit initializations of Variable for target Variable
		// implicit initializations of Variable for target Variable
		static @Variable() {
		}
		
		public @Variable() : base(GRGEN_MODEL.NodeType_Variable.typeVar)
		{
			// implicit initialization, container creation of Variable
			// explicit initializations of Variable for target Variable
		}

		public static GRGEN_MODEL.NodeType_Variable TypeInstance { get { return GRGEN_MODEL.NodeType_Variable.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Variable(this); }

		private @Variable(GRGEN_MODEL.@Variable oldElem) : base(GRGEN_MODEL.NodeType_Variable.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
			visibility_M0no_suXx_h4rD = oldElem.visibility_M0no_suXx_h4rD;
			isStatic_M0no_suXx_h4rD = oldElem.isStatic_M0no_suXx_h4rD;
			isFinal_M0no_suXx_h4rD = oldElem.isFinal_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Variable)) return false;
			@Variable that_ = (@Variable)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
				&& visibility_M0no_suXx_h4rD == that_.visibility_M0no_suXx_h4rD
				&& isStatic_M0no_suXx_h4rD == that_.isStatic_M0no_suXx_h4rD
				&& isFinal_M0no_suXx_h4rD == that_.isFinal_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Variable CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Variable node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Variable();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Variable
				node.@name = null;
				node.@visibility = null;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Variable for target Variable
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Variable CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Variable node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Variable();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Variable
				node.@name = null;
				node.@visibility = null;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Variable for target Variable
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}

		private string visibility_M0no_suXx_h4rD;
		public string @visibility
		{
			get { return visibility_M0no_suXx_h4rD; }
			set { visibility_M0no_suXx_h4rD = value; }
		}

		private bool isStatic_M0no_suXx_h4rD;
		public bool @isStatic
		{
			get { return isStatic_M0no_suXx_h4rD; }
			set { isStatic_M0no_suXx_h4rD = value; }
		}

		private bool isFinal_M0no_suXx_h4rD;
		public bool @isFinal
		{
			get { return isFinal_M0no_suXx_h4rD; }
			set { isFinal_M0no_suXx_h4rD = value; }
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
				"The node type \"Variable\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"Variable\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Variable
			this.@name = null;
			this.@visibility = null;
			this.@isStatic = false;
			this.@isFinal = false;
			// explicit initializations of Variable for target Variable
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Variable does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Variable does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Variable : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Variable typeVar = new GRGEN_MODEL.NodeType_Variable();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public static GRGEN_LIBGR.AttributeType AttributeType_visibility;
		public static GRGEN_LIBGR.AttributeType AttributeType_isStatic;
		public static GRGEN_LIBGR.AttributeType AttributeType_isFinal;
		public NodeType_Variable() : base((int) NodeTypes.@Variable)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_visibility = new GRGEN_LIBGR.AttributeType("visibility", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_isStatic = new GRGEN_LIBGR.AttributeType("isStatic", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
			AttributeType_isFinal = new GRGEN_LIBGR.AttributeType("isFinal", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Variable"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Variable"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IVariable"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Variable"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Variable();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Variable newNode = new GRGEN_MODEL.@Variable();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Variable:
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

	public class Comparer_Variable_name : Comparer<GRGEN_MODEL.IVariable>
	{
		private static GRGEN_MODEL.IVariable nodeBearingAttributeForSearch = new GRGEN_MODEL.@Variable();
		private static Comparer_Variable_name thisComparer = new Comparer_Variable_name();
		public override int Compare(GRGEN_MODEL.IVariable a, GRGEN_MODEL.IVariable b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IVariable> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IVariable> ArrayOrderAscendingBy(List<GRGEN_MODEL.IVariable> list)
		{
			List<GRGEN_MODEL.IVariable> newList = new List<GRGEN_MODEL.IVariable>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Variable_visibility : Comparer<GRGEN_MODEL.IVariable>
	{
		private static GRGEN_MODEL.IVariable nodeBearingAttributeForSearch = new GRGEN_MODEL.@Variable();
		private static Comparer_Variable_visibility thisComparer = new Comparer_Variable_visibility();
		public override int Compare(GRGEN_MODEL.IVariable a, GRGEN_MODEL.IVariable b)
		{
			return StringComparer.InvariantCulture.Compare(a.@visibility, b.@visibility);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IVariable> list, string entry)
		{
			nodeBearingAttributeForSearch.@visibility = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IVariable> ArrayOrderAscendingBy(List<GRGEN_MODEL.IVariable> list)
		{
			List<GRGEN_MODEL.IVariable> newList = new List<GRGEN_MODEL.IVariable>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Variable_isStatic : Comparer<GRGEN_MODEL.IVariable>
	{
		private static GRGEN_MODEL.IVariable nodeBearingAttributeForSearch = new GRGEN_MODEL.@Variable();
		private static Comparer_Variable_isStatic thisComparer = new Comparer_Variable_isStatic();
		public override int Compare(GRGEN_MODEL.IVariable a, GRGEN_MODEL.IVariable b)
		{
			return a.@isStatic.CompareTo(b.@isStatic);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IVariable> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isStatic = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IVariable> ArrayOrderAscendingBy(List<GRGEN_MODEL.IVariable> list)
		{
			List<GRGEN_MODEL.IVariable> newList = new List<GRGEN_MODEL.IVariable>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Variable_isFinal : Comparer<GRGEN_MODEL.IVariable>
	{
		private static GRGEN_MODEL.IVariable nodeBearingAttributeForSearch = new GRGEN_MODEL.@Variable();
		private static Comparer_Variable_isFinal thisComparer = new Comparer_Variable_isFinal();
		public override int Compare(GRGEN_MODEL.IVariable a, GRGEN_MODEL.IVariable b)
		{
			return a.@isFinal.CompareTo(b.@isFinal);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IVariable> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IVariable> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isFinal = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IVariable> ArrayOrderAscendingBy(List<GRGEN_MODEL.IVariable> list)
		{
			List<GRGEN_MODEL.IVariable> newList = new List<GRGEN_MODEL.IVariable>(list);
			newList.Sort(thisComparer);
			return newList;
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

	public sealed partial class @Operation : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IOperation
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Operation[] pool = new GRGEN_MODEL.@Operation[10];
		
		// explicit initializations of Operation for target Operation
		// implicit initializations of Operation for target Operation
		static @Operation() {
		}
		
		public @Operation() : base(GRGEN_MODEL.NodeType_Operation.typeVar)
		{
			// implicit initialization, container creation of Operation
			// explicit initializations of Operation for target Operation
		}

		public static GRGEN_MODEL.NodeType_Operation TypeInstance { get { return GRGEN_MODEL.NodeType_Operation.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Operation(this); }

		private @Operation(GRGEN_MODEL.@Operation oldElem) : base(GRGEN_MODEL.NodeType_Operation.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
			visibility_M0no_suXx_h4rD = oldElem.visibility_M0no_suXx_h4rD;
			isAbstract_M0no_suXx_h4rD = oldElem.isAbstract_M0no_suXx_h4rD;
			isStatic_M0no_suXx_h4rD = oldElem.isStatic_M0no_suXx_h4rD;
			isFinal_M0no_suXx_h4rD = oldElem.isFinal_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Operation)) return false;
			@Operation that_ = (@Operation)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
				&& visibility_M0no_suXx_h4rD == that_.visibility_M0no_suXx_h4rD
				&& isAbstract_M0no_suXx_h4rD == that_.isAbstract_M0no_suXx_h4rD
				&& isStatic_M0no_suXx_h4rD == that_.isStatic_M0no_suXx_h4rD
				&& isFinal_M0no_suXx_h4rD == that_.isFinal_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Operation CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Operation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operation();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Operation
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

		public static GRGEN_MODEL.@Operation CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Operation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operation();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Operation
				node.@name = null;
				node.@visibility = null;
				node.@isAbstract = false;
				node.@isStatic = false;
				node.@isFinal = false;
				// explicit initializations of Operation for target Operation
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}

		private string visibility_M0no_suXx_h4rD;
		public string @visibility
		{
			get { return visibility_M0no_suXx_h4rD; }
			set { visibility_M0no_suXx_h4rD = value; }
		}

		private bool isAbstract_M0no_suXx_h4rD;
		public bool @isAbstract
		{
			get { return isAbstract_M0no_suXx_h4rD; }
			set { isAbstract_M0no_suXx_h4rD = value; }
		}

		private bool isStatic_M0no_suXx_h4rD;
		public bool @isStatic
		{
			get { return isStatic_M0no_suXx_h4rD; }
			set { isStatic_M0no_suXx_h4rD = value; }
		}

		private bool isFinal_M0no_suXx_h4rD;
		public bool @isFinal
		{
			get { return isFinal_M0no_suXx_h4rD; }
			set { isFinal_M0no_suXx_h4rD = value; }
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
				"The node type \"Operation\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"Operation\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Operation
			this.@name = null;
			this.@visibility = null;
			this.@isAbstract = false;
			this.@isStatic = false;
			this.@isFinal = false;
			// explicit initializations of Operation for target Operation
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Operation does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Operation does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Operation : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Operation typeVar = new GRGEN_MODEL.NodeType_Operation();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public static GRGEN_LIBGR.AttributeType AttributeType_visibility;
		public static GRGEN_LIBGR.AttributeType AttributeType_isAbstract;
		public static GRGEN_LIBGR.AttributeType AttributeType_isStatic;
		public static GRGEN_LIBGR.AttributeType AttributeType_isFinal;
		public NodeType_Operation() : base((int) NodeTypes.@Operation)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_visibility = new GRGEN_LIBGR.AttributeType("visibility", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
			AttributeType_isAbstract = new GRGEN_LIBGR.AttributeType("isAbstract", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
			AttributeType_isStatic = new GRGEN_LIBGR.AttributeType("isStatic", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
			AttributeType_isFinal = new GRGEN_LIBGR.AttributeType("isFinal", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Operation"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Operation"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IOperation"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Operation"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Operation();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Operation newNode = new GRGEN_MODEL.@Operation();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Operation:
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

	public class Comparer_Operation_name : Comparer<GRGEN_MODEL.IOperation>
	{
		private static GRGEN_MODEL.IOperation nodeBearingAttributeForSearch = new GRGEN_MODEL.@Operation();
		private static Comparer_Operation_name thisComparer = new Comparer_Operation_name();
		public override int Compare(GRGEN_MODEL.IOperation a, GRGEN_MODEL.IOperation b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IOperation> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IOperation> ArrayOrderAscendingBy(List<GRGEN_MODEL.IOperation> list)
		{
			List<GRGEN_MODEL.IOperation> newList = new List<GRGEN_MODEL.IOperation>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Operation_visibility : Comparer<GRGEN_MODEL.IOperation>
	{
		private static GRGEN_MODEL.IOperation nodeBearingAttributeForSearch = new GRGEN_MODEL.@Operation();
		private static Comparer_Operation_visibility thisComparer = new Comparer_Operation_visibility();
		public override int Compare(GRGEN_MODEL.IOperation a, GRGEN_MODEL.IOperation b)
		{
			return StringComparer.InvariantCulture.Compare(a.@visibility, b.@visibility);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@visibility.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IOperation> list, string entry)
		{
			nodeBearingAttributeForSearch.@visibility = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IOperation> ArrayOrderAscendingBy(List<GRGEN_MODEL.IOperation> list)
		{
			List<GRGEN_MODEL.IOperation> newList = new List<GRGEN_MODEL.IOperation>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Operation_isAbstract : Comparer<GRGEN_MODEL.IOperation>
	{
		private static GRGEN_MODEL.IOperation nodeBearingAttributeForSearch = new GRGEN_MODEL.@Operation();
		private static Comparer_Operation_isAbstract thisComparer = new Comparer_Operation_isAbstract();
		public override int Compare(GRGEN_MODEL.IOperation a, GRGEN_MODEL.IOperation b)
		{
			return a.@isAbstract.CompareTo(b.@isAbstract);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isAbstract.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IOperation> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isAbstract = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IOperation> ArrayOrderAscendingBy(List<GRGEN_MODEL.IOperation> list)
		{
			List<GRGEN_MODEL.IOperation> newList = new List<GRGEN_MODEL.IOperation>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Operation_isStatic : Comparer<GRGEN_MODEL.IOperation>
	{
		private static GRGEN_MODEL.IOperation nodeBearingAttributeForSearch = new GRGEN_MODEL.@Operation();
		private static Comparer_Operation_isStatic thisComparer = new Comparer_Operation_isStatic();
		public override int Compare(GRGEN_MODEL.IOperation a, GRGEN_MODEL.IOperation b)
		{
			return a.@isStatic.CompareTo(b.@isStatic);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isStatic.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IOperation> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isStatic = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IOperation> ArrayOrderAscendingBy(List<GRGEN_MODEL.IOperation> list)
		{
			List<GRGEN_MODEL.IOperation> newList = new List<GRGEN_MODEL.IOperation>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Operation_isFinal : Comparer<GRGEN_MODEL.IOperation>
	{
		private static GRGEN_MODEL.IOperation nodeBearingAttributeForSearch = new GRGEN_MODEL.@Operation();
		private static Comparer_Operation_isFinal thisComparer = new Comparer_Operation_isFinal();
		public override int Compare(GRGEN_MODEL.IOperation a, GRGEN_MODEL.IOperation b)
		{
			return a.@isFinal.CompareTo(b.@isFinal);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperation> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@isFinal.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IOperation> list, bool entry)
		{
			nodeBearingAttributeForSearch.@isFinal = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IOperation> ArrayOrderAscendingBy(List<GRGEN_MODEL.IOperation> list)
		{
			List<GRGEN_MODEL.IOperation> newList = new List<GRGEN_MODEL.IOperation>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node MethodBody ***

	public interface IMethodBody : GRGEN_LIBGR.INode
	{
	}

	public sealed partial class @MethodBody : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IMethodBody
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@MethodBody[] pool = new GRGEN_MODEL.@MethodBody[10];
		
		// explicit initializations of MethodBody for target MethodBody
		// implicit initializations of MethodBody for target MethodBody
		static @MethodBody() {
		}
		
		public @MethodBody() : base(GRGEN_MODEL.NodeType_MethodBody.typeVar)
		{
			// implicit initialization, container creation of MethodBody
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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_MethodBody() : base((int) NodeTypes.@MethodBody)
		{
		}
		public override string Name { get { return "MethodBody"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "MethodBody"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IMethodBody"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@MethodBody"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@MethodBody();
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
			return new GRGEN_MODEL.@MethodBody();
		}

	}

	// *** Node Expression ***

	public interface IExpression : GRGEN_LIBGR.INode
	{
	}

	public sealed partial class @Expression : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IExpression
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Expression[] pool = new GRGEN_MODEL.@Expression[10];
		
		// explicit initializations of Expression for target Expression
		// implicit initializations of Expression for target Expression
		static @Expression() {
		}
		
		public @Expression() : base(GRGEN_MODEL.NodeType_Expression.typeVar)
		{
			// implicit initialization, container creation of Expression
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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Expression() : base((int) NodeTypes.@Expression)
		{
		}
		public override string Name { get { return "Expression"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Expression"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IExpression"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Expression"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Expression();
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
			return new GRGEN_MODEL.@Expression();
		}

	}

	// *** Node Access ***

	public interface IAccess : IExpression
	{
		bool @this_ { get; set; }
	}

	public sealed partial class @Access : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IAccess
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
			// implicit initialization, container creation of Access
			// explicit initializations of Expression for target Access
			// explicit initializations of Access for target Access
		}

		public static GRGEN_MODEL.NodeType_Access TypeInstance { get { return GRGEN_MODEL.NodeType_Access.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Access(this); }

		private @Access(GRGEN_MODEL.@Access oldElem) : base(GRGEN_MODEL.NodeType_Access.typeVar)
		{
			this__M0no_suXx_h4rD = oldElem.this__M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Access)) return false;
			@Access that_ = (@Access)that;
			return true
				&& this__M0no_suXx_h4rD == that_.this__M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Access CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Access node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Access();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Access
				node.@this_ = false;
				// explicit initializations of Expression for target Access
				// explicit initializations of Access for target Access
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Access CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Access node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Access();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Access
				node.@this_ = false;
				// explicit initializations of Expression for target Access
				// explicit initializations of Access for target Access
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool this__M0no_suXx_h4rD;
		public bool @this_
		{
			get { return this__M0no_suXx_h4rD; }
			set { this__M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "this_": return this.@this_;
			}
			throw new NullReferenceException(
				"The node type \"Access\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "this_": this.@this_ = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Access\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Access
			this.@this_ = false;
			// explicit initializations of Expression for target Access
			// explicit initializations of Access for target Access
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Access does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Access does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Access : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Access typeVar = new GRGEN_MODEL.NodeType_Access();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_this_;
		public NodeType_Access() : base((int) NodeTypes.@Access)
		{
			AttributeType_this_ = new GRGEN_LIBGR.AttributeType("this_", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Access"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Access"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IAccess"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Access"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Access();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_this_;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "this_" : return AttributeType_this_;
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
			GRGEN_MODEL.@Access newNode = new GRGEN_MODEL.@Access();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Access:
					// copy attributes for: Access
					{
						GRGEN_MODEL.IAccess old = (GRGEN_MODEL.IAccess) oldNode;
						newNode.@this_ = old.@this_;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_Access_this_ : Comparer<GRGEN_MODEL.IAccess>
	{
		private static GRGEN_MODEL.IAccess nodeBearingAttributeForSearch = new GRGEN_MODEL.@Access();
		private static Comparer_Access_this_ thisComparer = new Comparer_Access_this_();
		public override int Compare(GRGEN_MODEL.IAccess a, GRGEN_MODEL.IAccess b)
		{
			return a.@this_.CompareTo(b.@this_);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IAccess> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IAccess> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IAccess> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IAccess> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IAccess> list, bool entry)
		{
			nodeBearingAttributeForSearch.@this_ = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IAccess> ArrayOrderAscendingBy(List<GRGEN_MODEL.IAccess> list)
		{
			List<GRGEN_MODEL.IAccess> newList = new List<GRGEN_MODEL.IAccess>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Update ***

	public interface IUpdate : IExpression
	{
		bool @this_ { get; set; }
	}

	public sealed partial class @Update : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IUpdate
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
			// implicit initialization, container creation of Update
			// explicit initializations of Expression for target Update
			// explicit initializations of Update for target Update
		}

		public static GRGEN_MODEL.NodeType_Update TypeInstance { get { return GRGEN_MODEL.NodeType_Update.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Update(this); }

		private @Update(GRGEN_MODEL.@Update oldElem) : base(GRGEN_MODEL.NodeType_Update.typeVar)
		{
			this__M0no_suXx_h4rD = oldElem.this__M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Update)) return false;
			@Update that_ = (@Update)that;
			return true
				&& this__M0no_suXx_h4rD == that_.this__M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Update CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Update node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Update();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Update
				node.@this_ = false;
				// explicit initializations of Expression for target Update
				// explicit initializations of Update for target Update
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Update CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Update node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Update();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Update
				node.@this_ = false;
				// explicit initializations of Expression for target Update
				// explicit initializations of Update for target Update
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool this__M0no_suXx_h4rD;
		public bool @this_
		{
			get { return this__M0no_suXx_h4rD; }
			set { this__M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "this_": return this.@this_;
			}
			throw new NullReferenceException(
				"The node type \"Update\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "this_": this.@this_ = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Update\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Update
			this.@this_ = false;
			// explicit initializations of Expression for target Update
			// explicit initializations of Update for target Update
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Update does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Update does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Update : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Update typeVar = new GRGEN_MODEL.NodeType_Update();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, true, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_this_;
		public NodeType_Update() : base((int) NodeTypes.@Update)
		{
			AttributeType_this_ = new GRGEN_LIBGR.AttributeType("this_", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Update"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Update"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IUpdate"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Update"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Update();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_this_;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "this_" : return AttributeType_this_;
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
			GRGEN_MODEL.@Update newNode = new GRGEN_MODEL.@Update();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Update:
					// copy attributes for: Update
					{
						GRGEN_MODEL.IUpdate old = (GRGEN_MODEL.IUpdate) oldNode;
						newNode.@this_ = old.@this_;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_Update_this_ : Comparer<GRGEN_MODEL.IUpdate>
	{
		private static GRGEN_MODEL.IUpdate nodeBearingAttributeForSearch = new GRGEN_MODEL.@Update();
		private static Comparer_Update_this_ thisComparer = new Comparer_Update_this_();
		public override int Compare(GRGEN_MODEL.IUpdate a, GRGEN_MODEL.IUpdate b)
		{
			return a.@this_.CompareTo(b.@this_);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IUpdate> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IUpdate> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IUpdate> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IUpdate> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IUpdate> list, bool entry)
		{
			nodeBearingAttributeForSearch.@this_ = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IUpdate> ArrayOrderAscendingBy(List<GRGEN_MODEL.IUpdate> list)
		{
			List<GRGEN_MODEL.IUpdate> newList = new List<GRGEN_MODEL.IUpdate>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Call ***

	public interface ICall : IExpression
	{
		bool @this_ { get; set; }
		bool @super { get; set; }
	}

	public sealed partial class @Call : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ICall
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
			// implicit initialization, container creation of Call
			// explicit initializations of Expression for target Call
			// explicit initializations of Call for target Call
		}

		public static GRGEN_MODEL.NodeType_Call TypeInstance { get { return GRGEN_MODEL.NodeType_Call.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Call(this); }

		private @Call(GRGEN_MODEL.@Call oldElem) : base(GRGEN_MODEL.NodeType_Call.typeVar)
		{
			this__M0no_suXx_h4rD = oldElem.this__M0no_suXx_h4rD;
			super_M0no_suXx_h4rD = oldElem.super_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Call)) return false;
			@Call that_ = (@Call)that;
			return true
				&& this__M0no_suXx_h4rD == that_.this__M0no_suXx_h4rD
				&& super_M0no_suXx_h4rD == that_.super_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Call CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Call node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Call();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Call
				node.@this_ = false;
				node.@super = false;
				// explicit initializations of Expression for target Call
				// explicit initializations of Call for target Call
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Call CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Call node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Call();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Call
				node.@this_ = false;
				node.@super = false;
				// explicit initializations of Expression for target Call
				// explicit initializations of Call for target Call
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool this__M0no_suXx_h4rD;
		public bool @this_
		{
			get { return this__M0no_suXx_h4rD; }
			set { this__M0no_suXx_h4rD = value; }
		}

		private bool super_M0no_suXx_h4rD;
		public bool @super
		{
			get { return super_M0no_suXx_h4rD; }
			set { super_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "this_": return this.@this_;
				case "super": return this.@super;
			}
			throw new NullReferenceException(
				"The node type \"Call\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "this_": this.@this_ = (bool) value; return;
				case "super": this.@super = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Call\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Call
			this.@this_ = false;
			this.@super = false;
			// explicit initializations of Expression for target Call
			// explicit initializations of Call for target Call
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Call does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Call does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Call : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Call typeVar = new GRGEN_MODEL.NodeType_Call();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, true, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_this_;
		public static GRGEN_LIBGR.AttributeType AttributeType_super;
		public NodeType_Call() : base((int) NodeTypes.@Call)
		{
			AttributeType_this_ = new GRGEN_LIBGR.AttributeType("this_", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
			AttributeType_super = new GRGEN_LIBGR.AttributeType("super", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null, null, null, typeof(bool));
		}
		public override string Name { get { return "Call"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Call"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.ICall"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Call"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Call();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_this_;
				yield return AttributeType_super;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "this_" : return AttributeType_this_;
				case "super" : return AttributeType_super;
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
			GRGEN_MODEL.@Call newNode = new GRGEN_MODEL.@Call();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Call:
					// copy attributes for: Call
					{
						GRGEN_MODEL.ICall old = (GRGEN_MODEL.ICall) oldNode;
						newNode.@this_ = old.@this_;
						newNode.@super = old.@super;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_Call_this_ : Comparer<GRGEN_MODEL.ICall>
	{
		private static GRGEN_MODEL.ICall nodeBearingAttributeForSearch = new GRGEN_MODEL.@Call();
		private static Comparer_Call_this_ thisComparer = new Comparer_Call_this_();
		public override int Compare(GRGEN_MODEL.ICall a, GRGEN_MODEL.ICall b)
		{
			return a.@this_.CompareTo(b.@this_);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@this_.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ICall> list, bool entry)
		{
			nodeBearingAttributeForSearch.@this_ = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ICall> ArrayOrderAscendingBy(List<GRGEN_MODEL.ICall> list)
		{
			List<GRGEN_MODEL.ICall> newList = new List<GRGEN_MODEL.ICall>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_Call_super : Comparer<GRGEN_MODEL.ICall>
	{
		private static GRGEN_MODEL.ICall nodeBearingAttributeForSearch = new GRGEN_MODEL.@Call();
		private static Comparer_Call_super thisComparer = new Comparer_Call_super();
		public override int Compare(GRGEN_MODEL.ICall a, GRGEN_MODEL.ICall b)
		{
			return a.@super.CompareTo(b.@super);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@super.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@super.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@super.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ICall> list, bool entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@super.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ICall> list, bool entry)
		{
			nodeBearingAttributeForSearch.@super = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ICall> ArrayOrderAscendingBy(List<GRGEN_MODEL.ICall> list)
		{
			List<GRGEN_MODEL.ICall> newList = new List<GRGEN_MODEL.ICall>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Instantiation ***

	public interface IInstantiation : IExpression
	{
	}

	public sealed partial class @Instantiation : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IInstantiation
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
			// implicit initialization, container creation of Instantiation
			// explicit initializations of Expression for target Instantiation
			// explicit initializations of Instantiation for target Instantiation
		}

		public static GRGEN_MODEL.NodeType_Instantiation TypeInstance { get { return GRGEN_MODEL.NodeType_Instantiation.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Instantiation(this); }

		private @Instantiation(GRGEN_MODEL.@Instantiation oldElem) : base(GRGEN_MODEL.NodeType_Instantiation.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Instantiation)) return false;
			@Instantiation that_ = (@Instantiation)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Instantiation CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Instantiation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Instantiation();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Instantiation
				// explicit initializations of Expression for target Instantiation
				// explicit initializations of Instantiation for target Instantiation
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Instantiation CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Instantiation node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Instantiation();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Instantiation
				// explicit initializations of Expression for target Instantiation
				// explicit initializations of Instantiation for target Instantiation
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
				"The node type \"Instantiation\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Instantiation\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Instantiation
			// explicit initializations of Expression for target Instantiation
			// explicit initializations of Instantiation for target Instantiation
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Instantiation does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Instantiation does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Instantiation : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Instantiation typeVar = new GRGEN_MODEL.NodeType_Instantiation();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, true, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Instantiation() : base((int) NodeTypes.@Instantiation)
		{
		}
		public override string Name { get { return "Instantiation"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Instantiation"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IInstantiation"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Instantiation"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Instantiation();
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
			return new GRGEN_MODEL.@Instantiation();
		}

	}

	// *** Node Operator ***

	public interface IOperator : IExpression
	{
		string @name { get; set; }
	}

	public sealed partial class @Operator : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IOperator
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
			// implicit initialization, container creation of Operator
			// explicit initializations of Expression for target Operator
			// explicit initializations of Operator for target Operator
		}

		public static GRGEN_MODEL.NodeType_Operator TypeInstance { get { return GRGEN_MODEL.NodeType_Operator.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Operator(this); }

		private @Operator(GRGEN_MODEL.@Operator oldElem) : base(GRGEN_MODEL.NodeType_Operator.typeVar)
		{
			name_M0no_suXx_h4rD = oldElem.name_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Operator)) return false;
			@Operator that_ = (@Operator)that;
			return true
				&& name_M0no_suXx_h4rD == that_.name_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Operator CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Operator node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operator();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Operator
				node.@name = null;
				// explicit initializations of Expression for target Operator
				// explicit initializations of Operator for target Operator
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Operator CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Operator node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Operator();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Operator
				node.@name = null;
				// explicit initializations of Expression for target Operator
				// explicit initializations of Operator for target Operator
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string name_M0no_suXx_h4rD;
		public string @name
		{
			get { return name_M0no_suXx_h4rD; }
			set { name_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "name": return this.@name;
			}
			throw new NullReferenceException(
				"The node type \"Operator\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "name": this.@name = (string) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Operator\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Operator
			this.@name = null;
			// explicit initializations of Expression for target Operator
			// explicit initializations of Operator for target Operator
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Operator does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Operator does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Operator : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Operator typeVar = new GRGEN_MODEL.NodeType_Operator();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_name;
		public NodeType_Operator() : base((int) NodeTypes.@Operator)
		{
			AttributeType_name = new GRGEN_LIBGR.AttributeType("name", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
		}
		public override string Name { get { return "Operator"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Operator"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IOperator"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Operator"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Operator();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Operator newNode = new GRGEN_MODEL.@Operator();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Operator:
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

	public class Comparer_Operator_name : Comparer<GRGEN_MODEL.IOperator>
	{
		private static GRGEN_MODEL.IOperator nodeBearingAttributeForSearch = new GRGEN_MODEL.@Operator();
		private static Comparer_Operator_name thisComparer = new Comparer_Operator_name();
		public override int Compare(GRGEN_MODEL.IOperator a, GRGEN_MODEL.IOperator b)
		{
			return StringComparer.InvariantCulture.Compare(a.@name, b.@name);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperator> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IOperator> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperator> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IOperator> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@name.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IOperator> list, string entry)
		{
			nodeBearingAttributeForSearch.@name = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IOperator> ArrayOrderAscendingBy(List<GRGEN_MODEL.IOperator> list)
		{
			List<GRGEN_MODEL.IOperator> newList = new List<GRGEN_MODEL.IOperator>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Return ***

	public interface IReturn : IExpression
	{
	}

	public sealed partial class @Return : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IReturn
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
			// implicit initialization, container creation of Return
			// explicit initializations of Expression for target Return
			// explicit initializations of Return for target Return
		}

		public static GRGEN_MODEL.NodeType_Return TypeInstance { get { return GRGEN_MODEL.NodeType_Return.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Return(this); }

		private @Return(GRGEN_MODEL.@Return oldElem) : base(GRGEN_MODEL.NodeType_Return.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Return)) return false;
			@Return that_ = (@Return)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Return CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Return node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Return();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Return
				// explicit initializations of Expression for target Return
				// explicit initializations of Return for target Return
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Return CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Return node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Return();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Return
				// explicit initializations of Expression for target Return
				// explicit initializations of Return for target Return
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
				"The node type \"Return\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Return\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Return
			// explicit initializations of Expression for target Return
			// explicit initializations of Return for target Return
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Return does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Return does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Return : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Return typeVar = new GRGEN_MODEL.NodeType_Return();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Return() : base((int) NodeTypes.@Return)
		{
		}
		public override string Name { get { return "Return"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Return"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IReturn"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Return"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Return();
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
			return new GRGEN_MODEL.@Return();
		}

	}

	// *** Node Block ***

	public interface IBlock : IExpression
	{
	}

	public sealed partial class @Block : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IBlock
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
			// implicit initialization, container creation of Block
			// explicit initializations of Expression for target Block
			// explicit initializations of Block for target Block
		}

		public static GRGEN_MODEL.NodeType_Block TypeInstance { get { return GRGEN_MODEL.NodeType_Block.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Block(this); }

		private @Block(GRGEN_MODEL.@Block oldElem) : base(GRGEN_MODEL.NodeType_Block.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Block)) return false;
			@Block that_ = (@Block)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Block CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Block node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Block();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Block
				// explicit initializations of Expression for target Block
				// explicit initializations of Block for target Block
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Block CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Block node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Block();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Block
				// explicit initializations of Expression for target Block
				// explicit initializations of Block for target Block
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
				"The node type \"Block\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Block\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Block
			// explicit initializations of Expression for target Block
			// explicit initializations of Block for target Block
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Block does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Block does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Block : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Block typeVar = new GRGEN_MODEL.NodeType_Block();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Block() : base((int) NodeTypes.@Block)
		{
		}
		public override string Name { get { return "Block"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Block"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IBlock"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Block"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Block();
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
			return new GRGEN_MODEL.@Block();
		}

	}

	// *** Node Literal ***

	public interface ILiteral : GRGEN_LIBGR.INode
	{
		string @value { get; set; }
	}

	public sealed partial class @Literal : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ILiteral
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Literal[] pool = new GRGEN_MODEL.@Literal[10];
		
		// explicit initializations of Literal for target Literal
		// implicit initializations of Literal for target Literal
		static @Literal() {
		}
		
		public @Literal() : base(GRGEN_MODEL.NodeType_Literal.typeVar)
		{
			// implicit initialization, container creation of Literal
			// explicit initializations of Literal for target Literal
		}

		public static GRGEN_MODEL.NodeType_Literal TypeInstance { get { return GRGEN_MODEL.NodeType_Literal.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Literal(this); }

		private @Literal(GRGEN_MODEL.@Literal oldElem) : base(GRGEN_MODEL.NodeType_Literal.typeVar)
		{
			value_M0no_suXx_h4rD = oldElem.value_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Literal)) return false;
			@Literal that_ = (@Literal)that;
			return true
				&& value_M0no_suXx_h4rD == that_.value_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@Literal CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Literal node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Literal();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Literal
				node.@value = null;
				// explicit initializations of Literal for target Literal
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Literal CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Literal node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Literal();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Literal
				node.@value = null;
				// explicit initializations of Literal for target Literal
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private string value_M0no_suXx_h4rD;
		public string @value
		{
			get { return value_M0no_suXx_h4rD; }
			set { value_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "value": return this.@value;
			}
			throw new NullReferenceException(
				"The node type \"Literal\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": this.@value = (string) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Literal\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Literal
			this.@value = null;
			// explicit initializations of Literal for target Literal
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Literal does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Literal does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Literal : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Literal typeVar = new GRGEN_MODEL.NodeType_Literal();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_value;
		public NodeType_Literal() : base((int) NodeTypes.@Literal)
		{
			AttributeType_value = new GRGEN_LIBGR.AttributeType("value", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
		}
		public override string Name { get { return "Literal"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Literal"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.ILiteral"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Literal"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Literal();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_MODEL.@Literal newNode = new GRGEN_MODEL.@Literal();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@Literal:
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

	public class Comparer_Literal_value : Comparer<GRGEN_MODEL.ILiteral>
	{
		private static GRGEN_MODEL.ILiteral nodeBearingAttributeForSearch = new GRGEN_MODEL.@Literal();
		private static Comparer_Literal_value thisComparer = new Comparer_Literal_value();
		public override int Compare(GRGEN_MODEL.ILiteral a, GRGEN_MODEL.ILiteral b)
		{
			return StringComparer.InvariantCulture.Compare(a.@value, b.@value);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ILiteral> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ILiteral> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ILiteral> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ILiteral> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ILiteral> list, string entry)
		{
			nodeBearingAttributeForSearch.@value = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ILiteral> ArrayOrderAscendingBy(List<GRGEN_MODEL.ILiteral> list)
		{
			List<GRGEN_MODEL.ILiteral> newList = new List<GRGEN_MODEL.ILiteral>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node Parameter ***

	public interface IParameter : GRGEN_LIBGR.INode
	{
	}

	public sealed partial class @Parameter : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IParameter
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Parameter[] pool = new GRGEN_MODEL.@Parameter[10];
		
		// explicit initializations of Parameter for target Parameter
		// implicit initializations of Parameter for target Parameter
		static @Parameter() {
		}
		
		public @Parameter() : base(GRGEN_MODEL.NodeType_Parameter.typeVar)
		{
			// implicit initialization, container creation of Parameter
			// explicit initializations of Parameter for target Parameter
		}

		public static GRGEN_MODEL.NodeType_Parameter TypeInstance { get { return GRGEN_MODEL.NodeType_Parameter.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Parameter(this); }

		private @Parameter(GRGEN_MODEL.@Parameter oldElem) : base(GRGEN_MODEL.NodeType_Parameter.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Parameter)) return false;
			@Parameter that_ = (@Parameter)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Parameter CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Parameter node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Parameter();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Parameter
				// explicit initializations of Parameter for target Parameter
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Parameter CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Parameter node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Parameter();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Parameter
				// explicit initializations of Parameter for target Parameter
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
				"The node type \"Parameter\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Parameter\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Parameter
			// explicit initializations of Parameter for target Parameter
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Parameter does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Parameter does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Parameter : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Parameter typeVar = new GRGEN_MODEL.NodeType_Parameter();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Parameter() : base((int) NodeTypes.@Parameter)
		{
		}
		public override string Name { get { return "Parameter"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Parameter"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IParameter"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Parameter"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Parameter();
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
			return new GRGEN_MODEL.@Parameter();
		}

	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2, @belongsTo=3, @type_=4, @extends_=5, @imports=6, @implements=7, @parameter=8, @actualParameter=9, @binding=10, @link=11, @expression=12, @inBlock=13, @inClass=14 };

	// *** Edge AEdge ***


	public sealed partial class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IDEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@Edge"; } }
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
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IUEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@UEdge"; } }
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

	// *** Edge belongsTo ***

	public interface IbelongsTo : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @belongsTo : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IbelongsTo
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
			// implicit initialization, container creation of belongsTo
			// explicit initializations of belongsTo for target belongsTo
		}

		public static GRGEN_MODEL.EdgeType_belongsTo TypeInstance { get { return GRGEN_MODEL.EdgeType_belongsTo.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@belongsTo(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @belongsTo(GRGEN_MODEL.@belongsTo oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_belongsTo.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @belongsTo)) return false;
			@belongsTo that_ = (@belongsTo)that;
			return true
			;
		}

		public static GRGEN_MODEL.@belongsTo CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@belongsTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@belongsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of belongsTo
				// explicit initializations of belongsTo for target belongsTo
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@belongsTo CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@belongsTo edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@belongsTo(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of belongsTo
				// explicit initializations of belongsTo for target belongsTo
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
				"The edge type \"belongsTo\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"belongsTo\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of belongsTo
			// explicit initializations of belongsTo for target belongsTo
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("belongsTo does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("belongsTo does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_belongsTo : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_belongsTo typeVar = new GRGEN_MODEL.EdgeType_belongsTo();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_belongsTo() : base((int) EdgeTypes.@belongsTo)
		{
		}
		public override string Name { get { return "belongsTo"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "belongsTo"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IbelongsTo"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@belongsTo"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@belongsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@belongsTo((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge type_ ***

	public interface Itype_ : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @type_ : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Itype_
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
			// implicit initialization, container creation of type_
			// explicit initializations of type_ for target type_
		}

		public static GRGEN_MODEL.EdgeType_type_ TypeInstance { get { return GRGEN_MODEL.EdgeType_type_.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@type_(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @type_(GRGEN_MODEL.@type_ oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_type_.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @type_)) return false;
			@type_ that_ = (@type_)that;
			return true
			;
		}

		public static GRGEN_MODEL.@type_ CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@type_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@type_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of type_
				// explicit initializations of type_ for target type_
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@type_ CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@type_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@type_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of type_
				// explicit initializations of type_ for target type_
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
				"The edge type \"type_\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"type_\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of type_
			// explicit initializations of type_ for target type_
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("type_ does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("type_ does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_type_ : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_type_ typeVar = new GRGEN_MODEL.EdgeType_type_();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_type_() : base((int) EdgeTypes.@type_)
		{
		}
		public override string Name { get { return "type_"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "type_"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Itype_"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@type_"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@type_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@type_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge extends_ ***

	public interface Iextends_ : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @extends_ : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iextends_
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
			// implicit initialization, container creation of extends_
			// explicit initializations of extends_ for target extends_
		}

		public static GRGEN_MODEL.EdgeType_extends_ TypeInstance { get { return GRGEN_MODEL.EdgeType_extends_.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@extends_(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @extends_(GRGEN_MODEL.@extends_ oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_extends_.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @extends_)) return false;
			@extends_ that_ = (@extends_)that;
			return true
			;
		}

		public static GRGEN_MODEL.@extends_ CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@extends_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@extends_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of extends_
				// explicit initializations of extends_ for target extends_
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@extends_ CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@extends_ edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@extends_(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of extends_
				// explicit initializations of extends_ for target extends_
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
				"The edge type \"extends_\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"extends_\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of extends_
			// explicit initializations of extends_ for target extends_
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("extends_ does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("extends_ does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_extends_ : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_extends_ typeVar = new GRGEN_MODEL.EdgeType_extends_();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_extends_() : base((int) EdgeTypes.@extends_)
		{
		}
		public override string Name { get { return "extends_"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "extends_"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Iextends_"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@extends_"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@extends_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@extends_((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge imports ***

	public interface Iimports : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @imports : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iimports
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
			// implicit initialization, container creation of imports
			// explicit initializations of imports for target imports
		}

		public static GRGEN_MODEL.EdgeType_imports TypeInstance { get { return GRGEN_MODEL.EdgeType_imports.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@imports(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @imports(GRGEN_MODEL.@imports oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_imports.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @imports)) return false;
			@imports that_ = (@imports)that;
			return true
			;
		}

		public static GRGEN_MODEL.@imports CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@imports edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@imports(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of imports
				// explicit initializations of imports for target imports
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@imports CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@imports edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@imports(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of imports
				// explicit initializations of imports for target imports
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
				"The edge type \"imports\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"imports\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of imports
			// explicit initializations of imports for target imports
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("imports does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("imports does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_imports : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_imports typeVar = new GRGEN_MODEL.EdgeType_imports();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_imports() : base((int) EdgeTypes.@imports)
		{
		}
		public override string Name { get { return "imports"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "imports"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Iimports"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@imports"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@imports((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@imports((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge implements ***

	public interface Iimplements : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @implements : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iimplements
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
			// implicit initialization, container creation of implements
			// explicit initializations of implements for target implements
		}

		public static GRGEN_MODEL.EdgeType_implements TypeInstance { get { return GRGEN_MODEL.EdgeType_implements.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@implements(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @implements(GRGEN_MODEL.@implements oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_implements.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @implements)) return false;
			@implements that_ = (@implements)that;
			return true
			;
		}

		public static GRGEN_MODEL.@implements CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@implements edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@implements(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of implements
				// explicit initializations of implements for target implements
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@implements CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@implements edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@implements(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of implements
				// explicit initializations of implements for target implements
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
				"The edge type \"implements\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"implements\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of implements
			// explicit initializations of implements for target implements
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("implements does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("implements does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_implements : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_implements typeVar = new GRGEN_MODEL.EdgeType_implements();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_implements() : base((int) EdgeTypes.@implements)
		{
		}
		public override string Name { get { return "implements"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "implements"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Iimplements"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@implements"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@implements((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@implements((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge parameter ***

	public interface Iparameter : GRGEN_LIBGR.IDEdge
	{
		int @order { get; set; }
	}

	public sealed partial class @parameter : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iparameter
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
			// implicit initialization, container creation of parameter
			// explicit initializations of parameter for target parameter
		}

		public static GRGEN_MODEL.EdgeType_parameter TypeInstance { get { return GRGEN_MODEL.EdgeType_parameter.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@parameter(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @parameter(GRGEN_MODEL.@parameter oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_parameter.typeVar, newSource, newTarget)
		{
			order_M0no_suXx_h4rD = oldElem.order_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @parameter)) return false;
			@parameter that_ = (@parameter)that;
			return true
				&& order_M0no_suXx_h4rD == that_.order_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@parameter CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@parameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@parameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of parameter
				edge.@order = 0;
				// explicit initializations of parameter for target parameter
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@parameter CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@parameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@parameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of parameter
				edge.@order = 0;
				// explicit initializations of parameter for target parameter
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int order_M0no_suXx_h4rD;
		public int @order
		{
			get { return order_M0no_suXx_h4rD; }
			set { order_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "order": return this.@order;
			}
			throw new NullReferenceException(
				"The edge type \"parameter\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "order": this.@order = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"parameter\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of parameter
			this.@order = 0;
			// explicit initializations of parameter for target parameter
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("parameter does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("parameter does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_parameter : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_parameter typeVar = new GRGEN_MODEL.EdgeType_parameter();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, true, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_order;
		public EdgeType_parameter() : base((int) EdgeTypes.@parameter)
		{
			AttributeType_order = new GRGEN_LIBGR.AttributeType("order", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "parameter"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "parameter"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Iparameter"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@parameter"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@parameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;
			GRGEN_MODEL.@parameter newEdge = new GRGEN_MODEL.@parameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
			switch(oldEdge.Type.TypeID)
			{
				case (int) GRGEN_MODEL.EdgeTypes.@parameter:
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

	public class Comparer_parameter_order : Comparer<GRGEN_MODEL.Iparameter>
	{
		private static GRGEN_MODEL.Iparameter nodeBearingAttributeForSearch = new GRGEN_MODEL.@parameter(null, null);
		private static Comparer_parameter_order thisComparer = new Comparer_parameter_order();
		public override int Compare(GRGEN_MODEL.Iparameter a, GRGEN_MODEL.Iparameter b)
		{
			return a.@order.CompareTo(b.@order);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.Iparameter> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.Iparameter> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.Iparameter> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.Iparameter> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.Iparameter> list, int entry)
		{
			nodeBearingAttributeForSearch.@order = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.Iparameter> ArrayOrderAscendingBy(List<GRGEN_MODEL.Iparameter> list)
		{
			List<GRGEN_MODEL.Iparameter> newList = new List<GRGEN_MODEL.Iparameter>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Edge actualParameter ***

	public interface IactualParameter : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @actualParameter : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IactualParameter
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
			// implicit initialization, container creation of actualParameter
			// explicit initializations of actualParameter for target actualParameter
		}

		public static GRGEN_MODEL.EdgeType_actualParameter TypeInstance { get { return GRGEN_MODEL.EdgeType_actualParameter.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@actualParameter(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @actualParameter(GRGEN_MODEL.@actualParameter oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_actualParameter.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @actualParameter)) return false;
			@actualParameter that_ = (@actualParameter)that;
			return true
			;
		}

		public static GRGEN_MODEL.@actualParameter CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@actualParameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@actualParameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of actualParameter
				// explicit initializations of actualParameter for target actualParameter
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@actualParameter CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@actualParameter edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@actualParameter(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of actualParameter
				// explicit initializations of actualParameter for target actualParameter
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
				"The edge type \"actualParameter\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"actualParameter\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of actualParameter
			// explicit initializations of actualParameter for target actualParameter
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("actualParameter does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("actualParameter does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_actualParameter : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_actualParameter typeVar = new GRGEN_MODEL.EdgeType_actualParameter();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_actualParameter() : base((int) EdgeTypes.@actualParameter)
		{
		}
		public override string Name { get { return "actualParameter"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "actualParameter"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IactualParameter"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@actualParameter"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@actualParameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@actualParameter((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge binding ***

	public interface Ibinding : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @binding : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Ibinding
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
			// implicit initialization, container creation of binding
			// explicit initializations of binding for target binding
		}

		public static GRGEN_MODEL.EdgeType_binding TypeInstance { get { return GRGEN_MODEL.EdgeType_binding.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@binding(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @binding(GRGEN_MODEL.@binding oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_binding.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @binding)) return false;
			@binding that_ = (@binding)that;
			return true
			;
		}

		public static GRGEN_MODEL.@binding CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@binding edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@binding(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of binding
				// explicit initializations of binding for target binding
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@binding CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@binding edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@binding(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of binding
				// explicit initializations of binding for target binding
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
				"The edge type \"binding\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"binding\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of binding
			// explicit initializations of binding for target binding
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("binding does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("binding does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_binding : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_binding typeVar = new GRGEN_MODEL.EdgeType_binding();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_binding() : base((int) EdgeTypes.@binding)
		{
		}
		public override string Name { get { return "binding"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "binding"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Ibinding"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@binding"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@binding((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@binding((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge link ***

	public interface Ilink : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @link : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Ilink
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
			// implicit initialization, container creation of link
			// explicit initializations of link for target link
		}

		public static GRGEN_MODEL.EdgeType_link TypeInstance { get { return GRGEN_MODEL.EdgeType_link.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@link(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @link(GRGEN_MODEL.@link oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_link.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @link)) return false;
			@link that_ = (@link)that;
			return true
			;
		}

		public static GRGEN_MODEL.@link CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@link edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@link(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of link
				// explicit initializations of link for target link
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@link CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@link edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@link(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of link
				// explicit initializations of link for target link
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
				"The edge type \"link\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"link\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of link
			// explicit initializations of link for target link
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("link does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("link does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_link : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_link typeVar = new GRGEN_MODEL.EdgeType_link();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_link() : base((int) EdgeTypes.@link)
		{
		}
		public override string Name { get { return "link"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "link"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Ilink"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@link"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@link((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@link((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge expression ***

	public interface Iexpression : GRGEN_LIBGR.IDEdge
	{
		int @order { get; set; }
	}

	public sealed partial class @expression : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iexpression
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
			// implicit initialization, container creation of expression
			// explicit initializations of expression for target expression
		}

		public static GRGEN_MODEL.EdgeType_expression TypeInstance { get { return GRGEN_MODEL.EdgeType_expression.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@expression(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @expression(GRGEN_MODEL.@expression oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_expression.typeVar, newSource, newTarget)
		{
			order_M0no_suXx_h4rD = oldElem.order_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @expression)) return false;
			@expression that_ = (@expression)that;
			return true
				&& order_M0no_suXx_h4rD == that_.order_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@expression CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@expression edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@expression(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of expression
				edge.@order = 0;
				// explicit initializations of expression for target expression
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@expression CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@expression edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@expression(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of expression
				edge.@order = 0;
				// explicit initializations of expression for target expression
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int order_M0no_suXx_h4rD;
		public int @order
		{
			get { return order_M0no_suXx_h4rD; }
			set { order_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "order": return this.@order;
			}
			throw new NullReferenceException(
				"The edge type \"expression\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "order": this.@order = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"expression\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of expression
			this.@order = 0;
			// explicit initializations of expression for target expression
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("expression does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("expression does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_expression : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_expression typeVar = new GRGEN_MODEL.EdgeType_expression();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_order;
		public EdgeType_expression() : base((int) EdgeTypes.@expression)
		{
			AttributeType_order = new GRGEN_LIBGR.AttributeType("order", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "expression"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "expression"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.Iexpression"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@expression"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@expression((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
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
			GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;
			GRGEN_MODEL.@expression newEdge = new GRGEN_MODEL.@expression((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
			switch(oldEdge.Type.TypeID)
			{
				case (int) GRGEN_MODEL.EdgeTypes.@expression:
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

	public class Comparer_expression_order : Comparer<GRGEN_MODEL.Iexpression>
	{
		private static GRGEN_MODEL.Iexpression nodeBearingAttributeForSearch = new GRGEN_MODEL.@expression(null, null);
		private static Comparer_expression_order thisComparer = new Comparer_expression_order();
		public override int Compare(GRGEN_MODEL.Iexpression a, GRGEN_MODEL.Iexpression b)
		{
			return a.@order.CompareTo(b.@order);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.Iexpression> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.Iexpression> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.Iexpression> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.Iexpression> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@order.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.Iexpression> list, int entry)
		{
			nodeBearingAttributeForSearch.@order = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.Iexpression> ArrayOrderAscendingBy(List<GRGEN_MODEL.Iexpression> list)
		{
			List<GRGEN_MODEL.Iexpression> newList = new List<GRGEN_MODEL.Iexpression>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Edge inBlock ***

	public interface IinBlock : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @inBlock : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IinBlock
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
			// implicit initialization, container creation of inBlock
			// explicit initializations of inBlock for target inBlock
		}

		public static GRGEN_MODEL.EdgeType_inBlock TypeInstance { get { return GRGEN_MODEL.EdgeType_inBlock.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@inBlock(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @inBlock(GRGEN_MODEL.@inBlock oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_inBlock.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @inBlock)) return false;
			@inBlock that_ = (@inBlock)that;
			return true
			;
		}

		public static GRGEN_MODEL.@inBlock CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@inBlock edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inBlock(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of inBlock
				// explicit initializations of inBlock for target inBlock
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@inBlock CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@inBlock edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inBlock(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of inBlock
				// explicit initializations of inBlock for target inBlock
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
				"The edge type \"inBlock\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"inBlock\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of inBlock
			// explicit initializations of inBlock for target inBlock
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("inBlock does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("inBlock does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_inBlock : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_inBlock typeVar = new GRGEN_MODEL.EdgeType_inBlock();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_inBlock() : base((int) EdgeTypes.@inBlock)
		{
		}
		public override string Name { get { return "inBlock"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "inBlock"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IinBlock"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@inBlock"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@inBlock((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@inBlock((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge inClass ***

	public interface IinClass : GRGEN_LIBGR.IDEdge
	{
	}

	public sealed partial class @inClass : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IinClass
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
			// implicit initialization, container creation of inClass
			// explicit initializations of inClass for target inClass
		}

		public static GRGEN_MODEL.EdgeType_inClass TypeInstance { get { return GRGEN_MODEL.EdgeType_inClass.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@inClass(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @inClass(GRGEN_MODEL.@inClass oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_inClass.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @inClass)) return false;
			@inClass that_ = (@inClass)that;
			return true
			;
		}

		public static GRGEN_MODEL.@inClass CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@inClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of inClass
				// explicit initializations of inClass for target inClass
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@inClass CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@inClass edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@inClass(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of inClass
				// explicit initializations of inClass for target inClass
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
				"The edge type \"inClass\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"inClass\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of inClass
			// explicit initializations of inClass for target inClass
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("inClass does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("inClass does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_inClass : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_inClass typeVar = new GRGEN_MODEL.EdgeType_inClass();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_inClass() : base((int) EdgeTypes.@inClass)
		{
		}
		public override string Name { get { return "inClass"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "inClass"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.IinClass"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_JavaProgramGraphs.@inClass"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@inClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@inClass((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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

	public class JavaProgramGraphsIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public JavaProgramGraphsIndexSet(GRGEN_LGSP.LGSPGraph graph)
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
		private System.Type[] typeTypes = {
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
		public System.Type[] TypeTypes { get { return typeTypes; } }
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
			GRGEN_MODEL.NodeType_Access.AttributeType_this_,
			GRGEN_MODEL.NodeType_Update.AttributeType_this_,
			GRGEN_MODEL.NodeType_Call.AttributeType_this_,
			GRGEN_MODEL.NodeType_Call.AttributeType_super,
			GRGEN_MODEL.NodeType_Operator.AttributeType_name,
			GRGEN_MODEL.NodeType_Literal.AttributeType_value,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
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
		private System.Type[] typeTypes = {
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
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.EdgeType_parameter.AttributeType_order,
			GRGEN_MODEL.EdgeType_expression.AttributeType_order,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel (LGSPGraphModel) implementation
	//
	public sealed class JavaProgramGraphsGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public JavaProgramGraphsGraphModel()
		{
			FullyInitializeExternalTypes();
		}

		private JavaProgramGraphsNodeModel nodeModel = new JavaProgramGraphsNodeModel();
		private JavaProgramGraphsEdgeModel edgeModel = new JavaProgramGraphsEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new JavaProgramGraphsIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((JavaProgramGraphsIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "JavaProgramGraphs"; } }
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

		public override string MD5Hash { get { return "9ed13ad8bc778f9123d81b2ebb16880c"; } }
	}

	//
	// IGraph (LGSPGraph) / IGraphModel implementation
	//
	public class JavaProgramGraphsGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public JavaProgramGraphsGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@Package CreateNodePackage()
		{
			return GRGEN_MODEL.@Package.CreateNode(this);
		}

		public GRGEN_MODEL.@Classifier CreateNodeClassifier()
		{
			return GRGEN_MODEL.@Classifier.CreateNode(this);
		}

		public GRGEN_MODEL.@Class CreateNodeClass()
		{
			return GRGEN_MODEL.@Class.CreateNode(this);
		}

		public GRGEN_MODEL.@Interface CreateNodeInterface()
		{
			return GRGEN_MODEL.@Interface.CreateNode(this);
		}

		public GRGEN_MODEL.@Variable CreateNodeVariable()
		{
			return GRGEN_MODEL.@Variable.CreateNode(this);
		}

		public GRGEN_MODEL.@Operation CreateNodeOperation()
		{
			return GRGEN_MODEL.@Operation.CreateNode(this);
		}

		public GRGEN_MODEL.@MethodBody CreateNodeMethodBody()
		{
			return GRGEN_MODEL.@MethodBody.CreateNode(this);
		}

		public GRGEN_MODEL.@Expression CreateNodeExpression()
		{
			return GRGEN_MODEL.@Expression.CreateNode(this);
		}

		public GRGEN_MODEL.@Access CreateNodeAccess()
		{
			return GRGEN_MODEL.@Access.CreateNode(this);
		}

		public GRGEN_MODEL.@Update CreateNodeUpdate()
		{
			return GRGEN_MODEL.@Update.CreateNode(this);
		}

		public GRGEN_MODEL.@Call CreateNodeCall()
		{
			return GRGEN_MODEL.@Call.CreateNode(this);
		}

		public GRGEN_MODEL.@Instantiation CreateNodeInstantiation()
		{
			return GRGEN_MODEL.@Instantiation.CreateNode(this);
		}

		public GRGEN_MODEL.@Operator CreateNodeOperator()
		{
			return GRGEN_MODEL.@Operator.CreateNode(this);
		}

		public GRGEN_MODEL.@Return CreateNodeReturn()
		{
			return GRGEN_MODEL.@Return.CreateNode(this);
		}

		public GRGEN_MODEL.@Block CreateNodeBlock()
		{
			return GRGEN_MODEL.@Block.CreateNode(this);
		}

		public GRGEN_MODEL.@Literal CreateNodeLiteral()
		{
			return GRGEN_MODEL.@Literal.CreateNode(this);
		}

		public GRGEN_MODEL.@Parameter CreateNodeParameter()
		{
			return GRGEN_MODEL.@Parameter.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@belongsTo CreateEdgebelongsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@belongsTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@type_ CreateEdgetype_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@type_.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@extends_ CreateEdgeextends_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@extends_.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@imports CreateEdgeimports(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@imports.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@implements CreateEdgeimplements(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@implements.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@parameter CreateEdgeparameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@parameter.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@actualParameter CreateEdgeactualParameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@actualParameter.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@binding CreateEdgebinding(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@binding.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@link CreateEdgelink(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@link.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@expression CreateEdgeexpression(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@expression.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@inBlock CreateEdgeinBlock(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@inBlock.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@inClass CreateEdgeinClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@inClass.CreateEdge(this, source, target);
		}

		private JavaProgramGraphsNodeModel nodeModel = new JavaProgramGraphsNodeModel();
		private JavaProgramGraphsEdgeModel edgeModel = new JavaProgramGraphsEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new JavaProgramGraphsIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((JavaProgramGraphsIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "JavaProgramGraphs"; } }
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

		public string MD5Hash { get { return "9ed13ad8bc778f9123d81b2ebb16880c"; } }
	}

	//
	// INamedGraph (LGSPNamedGraph) / IGraphModel implementation
	//
	public class JavaProgramGraphsNamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel
	{
		public JavaProgramGraphsNamedGraph() : base(GetNextGraphName())
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

		public GRGEN_MODEL.@Package CreateNodePackage()
		{
			return GRGEN_MODEL.@Package.CreateNode(this);
		}

		public GRGEN_MODEL.@Package CreateNodePackage(string nodeName)
		{
			return GRGEN_MODEL.@Package.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Classifier CreateNodeClassifier()
		{
			return GRGEN_MODEL.@Classifier.CreateNode(this);
		}

		public GRGEN_MODEL.@Classifier CreateNodeClassifier(string nodeName)
		{
			return GRGEN_MODEL.@Classifier.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Class CreateNodeClass()
		{
			return GRGEN_MODEL.@Class.CreateNode(this);
		}

		public GRGEN_MODEL.@Class CreateNodeClass(string nodeName)
		{
			return GRGEN_MODEL.@Class.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Interface CreateNodeInterface()
		{
			return GRGEN_MODEL.@Interface.CreateNode(this);
		}

		public GRGEN_MODEL.@Interface CreateNodeInterface(string nodeName)
		{
			return GRGEN_MODEL.@Interface.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Variable CreateNodeVariable()
		{
			return GRGEN_MODEL.@Variable.CreateNode(this);
		}

		public GRGEN_MODEL.@Variable CreateNodeVariable(string nodeName)
		{
			return GRGEN_MODEL.@Variable.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Operation CreateNodeOperation()
		{
			return GRGEN_MODEL.@Operation.CreateNode(this);
		}

		public GRGEN_MODEL.@Operation CreateNodeOperation(string nodeName)
		{
			return GRGEN_MODEL.@Operation.CreateNode(this, nodeName);
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

		public GRGEN_MODEL.@Access CreateNodeAccess()
		{
			return GRGEN_MODEL.@Access.CreateNode(this);
		}

		public GRGEN_MODEL.@Access CreateNodeAccess(string nodeName)
		{
			return GRGEN_MODEL.@Access.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Update CreateNodeUpdate()
		{
			return GRGEN_MODEL.@Update.CreateNode(this);
		}

		public GRGEN_MODEL.@Update CreateNodeUpdate(string nodeName)
		{
			return GRGEN_MODEL.@Update.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Call CreateNodeCall()
		{
			return GRGEN_MODEL.@Call.CreateNode(this);
		}

		public GRGEN_MODEL.@Call CreateNodeCall(string nodeName)
		{
			return GRGEN_MODEL.@Call.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Instantiation CreateNodeInstantiation()
		{
			return GRGEN_MODEL.@Instantiation.CreateNode(this);
		}

		public GRGEN_MODEL.@Instantiation CreateNodeInstantiation(string nodeName)
		{
			return GRGEN_MODEL.@Instantiation.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Operator CreateNodeOperator()
		{
			return GRGEN_MODEL.@Operator.CreateNode(this);
		}

		public GRGEN_MODEL.@Operator CreateNodeOperator(string nodeName)
		{
			return GRGEN_MODEL.@Operator.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Return CreateNodeReturn()
		{
			return GRGEN_MODEL.@Return.CreateNode(this);
		}

		public GRGEN_MODEL.@Return CreateNodeReturn(string nodeName)
		{
			return GRGEN_MODEL.@Return.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Block CreateNodeBlock()
		{
			return GRGEN_MODEL.@Block.CreateNode(this);
		}

		public GRGEN_MODEL.@Block CreateNodeBlock(string nodeName)
		{
			return GRGEN_MODEL.@Block.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Literal CreateNodeLiteral()
		{
			return GRGEN_MODEL.@Literal.CreateNode(this);
		}

		public GRGEN_MODEL.@Literal CreateNodeLiteral(string nodeName)
		{
			return GRGEN_MODEL.@Literal.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@Parameter CreateNodeParameter()
		{
			return GRGEN_MODEL.@Parameter.CreateNode(this);
		}

		public GRGEN_MODEL.@Parameter CreateNodeParameter(string nodeName)
		{
			return GRGEN_MODEL.@Parameter.CreateNode(this, nodeName);
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

		public @GRGEN_MODEL.@belongsTo CreateEdgebelongsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@belongsTo.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@belongsTo CreateEdgebelongsTo(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@belongsTo.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@type_ CreateEdgetype_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@type_.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@type_ CreateEdgetype_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@type_.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@extends_ CreateEdgeextends_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@extends_.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@extends_ CreateEdgeextends_(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@extends_.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@imports CreateEdgeimports(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@imports.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@imports CreateEdgeimports(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@imports.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@implements CreateEdgeimplements(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@implements.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@implements CreateEdgeimplements(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@implements.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@parameter CreateEdgeparameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@parameter.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@parameter CreateEdgeparameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@parameter.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@actualParameter CreateEdgeactualParameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@actualParameter.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@actualParameter CreateEdgeactualParameter(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@actualParameter.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@binding CreateEdgebinding(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@binding.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@binding CreateEdgebinding(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@binding.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@link CreateEdgelink(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@link.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@link CreateEdgelink(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@link.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@expression CreateEdgeexpression(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@expression.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@expression CreateEdgeexpression(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@expression.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@inBlock CreateEdgeinBlock(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@inBlock.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@inBlock CreateEdgeinBlock(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@inBlock.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@inClass CreateEdgeinClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@inClass.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@inClass CreateEdgeinClass(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@inClass.CreateEdge(this, source, target, edgeName);
		}

		private JavaProgramGraphsNodeModel nodeModel = new JavaProgramGraphsNodeModel();
		private JavaProgramGraphsEdgeModel edgeModel = new JavaProgramGraphsEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new JavaProgramGraphsIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((JavaProgramGraphsIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "JavaProgramGraphs"; } }
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

		public string MD5Hash { get { return "9ed13ad8bc778f9123d81b2ebb16880c"; } }
	}
}
