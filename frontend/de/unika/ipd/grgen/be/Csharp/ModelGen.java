/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

/**
 * ModelGen.java
 *
 * Generates the model files for the SearchPlanBackend2 backend.
 *
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.be.IDBase;

public class ModelGen extends CSharpBase {
	public ModelGen(SearchPlanBackend2 backend) {
		be = backend;
	}

	/**
	 * Generates the model sourcecode for the current unit.
	 */
	public void genModel() {
		StringBuffer sb = new StringBuffer();
		String filename = formatIdentifiable(be.unit) + "Model.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.models." + formatIdentifiable(be.unit) + "\n");
		sb.append("{\n");

		System.out.println("    generating enums...");
		genEnums(sb);

		System.out.println("    generating node types...");
		sb.append("\n");
		genTypes(sb, be.nodeTypeMap.keySet(), true);

		System.out.println("    generating node model...");
		sb.append("\n");
		genModelClass(sb, be.nodeTypeMap.keySet(), true);

		System.out.println("    generating edge types...");
		sb.append("\n");
		genTypes(sb, be.edgeTypeMap.keySet(), false);

		System.out.println("    generating edge model...");
		sb.append("\n");
		genModelClass(sb, be.edgeTypeMap.keySet(), false);

		System.out.println("    generating graph model...");
		sb.append("\n");
		genGraphModel(sb);

		sb.append("}\n");

		writeFile(be.path, filename, sb);
	}

	private void genEnums(StringBuffer sb) {
		sb.append("\t//\n");
		sb.append("\t// Enums\n");
		sb.append("\t//\n");
		sb.append("\n");

		for(EnumType enumt : be.enumMap.keySet()) {
			sb.append("\tpublic enum ENUM_" + formatIdentifiable(enumt) + " { ");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("@" + formatIdentifiable(enumi) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}

		sb.append("\tpublic class Enums\n");
		sb.append("\t{\n");
		for(EnumType enumt : be.enumMap.keySet()) {
			sb.append("\t\tpublic static EnumAttributeType @" + formatIdentifiable(enumt)
					+ " = new EnumAttributeType(\"ENUM_" + formatIdentifiable(enumt)
					+ "\", new EnumMember[] {\n");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("\t\t\tnew EnumMember(" + enumi.getValue().getValue()
						+ ", \"" + formatIdentifiable(enumi) + "\"),\n");
			}
			sb.append("\t\t});\n");
		}
		sb.append("\t}\n");
	}

	/**
	 * Generates code for all given element types.
	 */
	private void genTypes(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " types\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic enum " + formatNodeOrEdge(isNode) + "Types ");
		genSet(sb, types, "@", "", true);
		sb.append(";\n");

		for(InheritanceType type : types) {
			genType(sb, types, type);
		}
	}

	/**
	 * Generates all code for a given type.
	 */
	private void genType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\n");
		sb.append("\t// *** " + formatNodeOrEdge(type) + " " + formatIdentifiable(type) + " ***\n");
		sb.append("\n");

		genElementInterface(sb, type);
		genElementImplementation(sb, type);
		genTypeImplementation(sb, types, type);
	}

	//////////////////////////////////
	// Element interface generation //
	//////////////////////////////////

	/**
	 * Generates the element interface for the given type
	 */
	private void genElementInterface(StringBuffer sb, InheritanceType type) {
		String iname = "I" + formatNodeOrEdge(type) + "_" + formatIdentifiable(type);
		sb.append("\tpublic interface " + iname + " : ");
		genDirectSuperTypeList(sb, type);
		sb.append("\n");
		sb.append("\t{\n");
		genAttributeAccess(sb, type);
		sb.append("\t}\n");
	}

	/**
	 * Generate a list of direct supertypes of the given type.
	 */
	private void genDirectSuperTypeList(StringBuffer sb, InheritanceType type) {
		Collection<InheritanceType> directSuperTypes = type.getDirectSuperTypes();

		if(directSuperTypes.isEmpty())
			sb.append("IAttributes");

		for(Iterator<InheritanceType> i = directSuperTypes.iterator(); i.hasNext(); ) {
			InheritanceType superType = i.next();
			sb.append("I" + formatNodeOrEdge(type) + "_" + formatIdentifiable(superType));
			if(i.hasNext())
				sb.append(", ");
		}
	}

	/**
	 * Generate the attribute accessor declarations of the given type
	 */
	private void genAttributeAccess(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getMembers()) {
			sb.append("\t\t" + formatAttributeType(e) + " @" + formatIdentifiable(e) + " { get; set; }\n");
		}
	}

	///////////////////////////////////////
	// Element implementation generation //
	///////////////////////////////////////

	/**
	 * Generates the element implementation for the given type
	 */
	private void genElementImplementation(StringBuffer sb, InheritanceType type) {
		String cname = formatNodeOrEdge(type) + "_" + formatIdentifiable(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? extName : cname;
		String tname = formatTypeClass(type);
		String iname = "I" + cname;
		boolean isNode = type instanceof NodeType;
		String elemKind = isNode ? "Node" : "Edge";

		if(extName == null)
			sb.append("\n\tpublic sealed class ");
		else
			sb.append("\n\tpublic abstract class ");
		sb.append(cname + " : LGSP" + elemKind + ", " + iname + "\n"
				+ "\t{\n"
				+ "\t\tprivate static int poolLevel = 0;\n"
				+ "\t\tprivate static " + cname + "[] pool = new " + cname + "[10];\n");

		if(isNode) {
			sb.append("\t\tpublic " + cname + "() : base("+ tname + ".typeVar)\n"
					+ "\t\t{\n");
			initAllMembers(sb, type, "this", "\t\t\t", false);
			sb.append("\t\t}\n"
					+ "\t\tprivate " + cname + "(" + cname + " oldElem) : base(" + tname + ".typeVar)\n");
		}
		else {
			sb.append("\t\tpublic " + cname + "(LGSPNode source, LGSPNode target)\n"
					+ "\t\t\t: base("+ tname + ".typeVar, source, target)\n"
					+ "\t\t{\n");
			initAllMembers(sb, type, "this", "\t\t\t", false);
			sb.append("\t\t}\n"
					+ "\t\tprivate " + cname + "(" + cname + " oldElem, LGSPNode newSource, LGSPNode newTarget)\n"
					+ "\t\t\t: base(" + tname + ".typeVar, newSource, newTarget)\n");
		}
		sb.append("\t\t{\n");
		for(Entity member : type.getAllMembers()) {
			String attrName = formatIdentifiable(member);
			sb.append("\t\t\t_" + attrName + " = oldElem._" + attrName + ";\n");
		}
		sb.append("\t\t}\n");

		if(isNode) {
			sb.append("\t\tpublic override INode Clone() { return new " + allocName + "(this); }\n"
					+ "\t\tpublic static " + cname + " CreateNode(LGSPGraph graph)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " node;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tnode = new " + allocName + "();\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tnode = pool[--poolLevel];\n"
					+ "\t\t\t\tnode.inhead = null;\n"
					+ "\t\t\t\tnode.outhead = null;\n"
					+ "\t\t\t\tnode.hasVariables = false;\n");
			initAllMembers(sb, type, "node", "\t\t\t\t", true);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddNode(node);\n"
					+ "\t\t\treturn node;\n"
					+ "\t\t}\n\n"
					+ "\t\tpublic static " + cname + " CreateNode(LGSPGraph graph, String varName)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " node;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tnode = new " + allocName + "();\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tnode = pool[--poolLevel];\n"
					+ "\t\t\t\tnode.inhead = null;\n"
					+ "\t\t\t\tnode.outhead = null;\n"
					+ "\t\t\t\tnode.hasVariables = false;\n");
			initAllMembers(sb, type, "node", "\t\t\t\t", true);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddNode(node, varName);\n"
					+ "\t\t\treturn node;\n"
					+ "\t\t}\n\n");
		}
		else {
			sb.append("\t\tpublic override IEdge Clone(INode newSource, INode newTarget)\n"
					+ "\t\t{ return new " + cname + "(this, (LGSPNode) newSource, (LGSPNode) newTarget); }\n"
					+ "\t\tpublic static " + cname + " CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " edge;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tedge = new " + allocName + "(source, target);\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tedge = pool[--poolLevel];\n"
					+ "\t\t\t\tedge.hasVariables = false;\n"
					+ "\t\t\t\tedge.source = source;\n"
					+ "\t\t\t\tedge.target = target;\n");
			initAllMembers(sb, type, "edge", "\t\t\t\t", true);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddEdge(edge);\n"
					+ "\t\t\treturn edge;\n"
					+ "\t\t}\n\n"
					+ "\t\tpublic static " + cname + " CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " edge;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tedge = new " + allocName + "(source, target);\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tedge = pool[--poolLevel];\n"
					+ "\t\t\t\tedge.hasVariables = false;\n"
					+ "\t\t\t\tedge.source = source;\n"
					+ "\t\t\t\tedge.target = target;\n");
			initAllMembers(sb, type, "edge", "\t\t\t\t", true);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddEdge(edge, varName);\n"
					+ "\t\t\treturn edge;\n"
					+ "\t\t}\n\n");
		}
		sb.append("\t\tpublic override void Recycle()\n"
				+ "\t\t{\n"
				+ "\t\t\tif(poolLevel < 10)\n"
				+ "\t\t\t\tpool[poolLevel++] = this;\n"
				+ "\t\t}\n\n");
		genAttributeAccessImpl(sb, type);
		sb.append("\t}\n");
	}

	/*	private void initAllMembers(StringBuffer sb, InheritanceType type, String varName,
			String indentString, boolean withDefaultInits) {
		Collection<MemberInit> memberInits = type.getMemberInits();

		HashMap<Entity, Expression> initializedMembers = new HashMap<Entity, Expression>();
		for(MemberInit mi : memberInits)
			initializedMembers.put(mi.getMember(), mi.getExpression());

		curMemberOwner = varName;

		for(Entity member : type.getAllMembers()) {
			Expression expr = initializedMembers.get(member);
			if(expr == null && !withDefaultInits) continue;

			String attrName = formatIdentifiable(member);
			sb.append(indentString + varName + ".@" + attrName + " = ");

			if(expr != null) {
				genExpression(sb, expr);
				sb.append(";\n");
			}
			else {
				Type t = member.getType();
				if(t instanceof IntType || t instanceof DoubleType || t instanceof EnumType)
					sb.append("0;\n");
				else if(t instanceof FloatType)
					sb.append("0f;\n");
				else if(t instanceof BooleanType)
					sb.append("false;\n");
				else if(t instanceof StringType || t instanceof ObjectType)
					sb.append("null;\n");
				else throw new IllegalArgumentException("Unknown Entity: " + member + "(" + t + ")");
			}
		}

		curMemberOwner = null;
	 }*/

	private void initAllMembers(StringBuffer sb, InheritanceType type, String varName,
			String indentString, boolean withDefaultInits) {
		curMemberOwner = varName;

		if(withDefaultInits) {
			for(Entity member : type.getAllMembers()) {
				String attrName = formatIdentifiable(member);
				sb.append(indentString + varName + ".@" + attrName + " = ");
				Type t = member.getType();
				if(t instanceof IntType || t instanceof DoubleType || t instanceof EnumType)
					sb.append("0;\n");
				else if(t instanceof FloatType)
					sb.append("0f;\n");
				else if(t instanceof BooleanType)
					sb.append("false;\n");
				else if(t instanceof StringType || t instanceof ObjectType)
					sb.append("null;\n");
				else throw new IllegalArgumentException("Unknown Entity: " + member + "(" + t + ")");
			}
		}

		for(InheritanceType superType : type.getAllSuperTypes())
			genMemberInit(sb, superType, indentString, varName);
		genMemberInit(sb, type, indentString, varName);

		curMemberOwner = null;
	}

	private void genMemberInit(StringBuffer sb, InheritanceType type, String indentString, String varName) {
		for(MemberInit mi : type.getMemberInits()) {
			String attrName = formatIdentifiable(mi.getMember());
			sb.append(indentString + varName + ".@" + attrName + " = ");
			genExpression(sb, mi.getExpression());
			sb.append(";\n");
		}
	}

	protected void genQualAccess(Entity entity, StringBuffer sb, Qualification qual) {
		sb.append("((I" + (entity instanceof Node ? "Node" : "Edge") + "_" +
				formatIdentifiable(entity.getType()) + ") ");
		sb.append(formatEntity(entity) + ").@" + formatIdentifiable(qual.getMember()));
	}

	protected void genMemberAccess(Entity member, StringBuffer sb) {
		if(curMemberOwner != null)
			sb.append(curMemberOwner + ".");
		sb.append("@" + formatIdentifiable(member));
	}


	/**
	 * Generate the attribute accessor implementations of the given type
	 */
	private void genAttributeAccessImpl(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getAllMembers()) {
			sb.append("\t\tprivate " + formatAttributeType(e) + " _" + formatIdentifiable(e) + ";\n");
			sb.append("\t\tpublic " + formatAttributeType(e) + " @" + formatIdentifiable(e) + "\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tget { return _" + formatIdentifiable(e) + "; }\n");
			sb.append("\t\t\tset { _" + formatIdentifiable(e) + " = value; }\n");
			sb.append("\t\t}\n");
			sb.append("\n");
		}

		sb.append("\t\tpublic override object GetAttribute(string attrName)\n");
		sb.append("\t\t{\n");
		if(type.getAllMembers().size() != 0) {
			sb.append("\t\t\tswitch(attrName)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : type.getAllMembers()) {
				String name = formatIdentifiable(e);
				sb.append("\t\t\t\tcase \"" + name + "\": return _" + name + ";\n");
			}
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new NullReferenceException(\n");
		sb.append("\t\t\t\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\" + attrName + \\\"\\\"!\");\n");
		sb.append("\t\t}\n");

		sb.append("\t\tpublic override void SetAttribute(string attrName, object value)\n");
		sb.append("\t\t{\n");
		if(type.getAllMembers().size() != 0) {
			sb.append("\t\t\tswitch(attrName)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : type.getAllMembers()) {
				String name = formatIdentifiable(e);
				sb.append("\t\t\t\tcase \"" + name + "\": _" + name + " = ("
						+ formatAttributeType(e) + ") value; return;\n");
			}
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new NullReferenceException(\n");
		sb.append("\t\t\t\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\" + attrName + \\\"\\\"!\");\n");
		sb.append("\t\t}\n");

		sb.append("\t\tpublic override void ResetAllAttributes()\n");
		sb.append("\t\t{\n");
		initAllMembers(sb, type, "this", "\t\t\t", true);
		sb.append("\t\t}\n");
	}

	////////////////////////////////////
	// Type implementation generation //
	////////////////////////////////////

	/**
	 * Generates the type implementation
	 */
	private void genTypeImplementation(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		String typeName = formatIdentifiable(type);
		String tname = formatTypeClass(type);
		String cname = formatElementClass(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? extName : cname;
		boolean isNode = type instanceof NodeType;
		String elemKind = isNode ? "Node" : "Edge";

		sb.append("\n");
		sb.append("\tpublic sealed class " + tname + " : " + elemKind + "Type\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic static " + tname + " typeVar = new " + tname + "();\n");
		genIsA(sb, types, type);
		genIsMyType(sb, types, type);
		genAttributeAttributes(sb, type);
		sb.append("\t\tpublic " + tname + "() : base((int) " + formatNodeOrEdge(type) + "Types.@" + typeName + ")\n");
		sb.append("\t\t{\n");
		genAttributeInit(sb, type);
		sb.append("\t\t}\n");
		sb.append("\t\tpublic override String Name { get { return \"" + typeName + "\"; } }\n");

		if(isNode) {
			sb.append("\t\tpublic override INode CreateNode() { return new " + allocName + "(); }\n");
		}
		else {
			sb.append("\t\tpublic override IEdge CreateEdge(INode source, INode target)\n"
					+ "\t\t{\n"
					+ "\t\t\treturn new " + allocName + "((LGSPNode) source, (LGSPNode) target);\n"
					+ "\t\t}\n");
		}

		sb.append("\t\tpublic override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnum(sb, type);
		genGetAttributeType(sb, type);

		sb.append("\t\tpublic override bool IsA(GrGenType other)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn (this == other) || isA[other.TypeID];\n");
		sb.append("\t\t}\n");

		genCreateWithCopyCommons(sb, type);
		sb.append("\t}\n");
	}

	private void genIsA(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\tpublic static bool[] isA = new bool[] { ");
		for(InheritanceType nt : types) {
			if(type.isCastableTo(nt))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}

	private void genIsMyType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\tpublic static bool[] isMyType = new bool[] { ");
		for(InheritanceType nt : types) {
			if(nt.isCastableTo(type))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}

	private void genAttributeAttributes(StringBuffer sb, InheritanceType type) {
		for(Entity member : type.getMembers()) // only for locally defined members
			sb.append("\t\tpublic static AttributeType " + formatAttributeTypeName(member) + ";\n");
	}

	private void genAttributeInit(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getMembers()) {
			sb.append("\t\t\t" + formatAttributeTypeName(e) + " = new AttributeType(");
			sb.append("\"" + formatIdentifiable(e) + "\", this, AttributeKind.");
			Type t = e.getType();

			if (t instanceof IntType)
				sb.append("IntegerAttr, null");
			else if (t instanceof FloatType)
				sb.append("FloatAttr, null");
			else if (t instanceof DoubleType)
				sb.append("DoubleAttr, null");
			else if (t instanceof BooleanType)
				sb.append("BooleanAttr, null");
			else if (t instanceof StringType)
				sb.append("StringAttr, null");
			else if (t instanceof EnumType)
				sb.append("EnumAttr, Enums.@" + formatIdentifiable(t));
			else if (t instanceof ObjectType)
				sb.append("ObjectAttr, null");
			else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");

			sb.append(");\n");
		}
	}

	private void genAttributeTypesEnum(StringBuffer sb, InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.append("\t\tpublic override IEnumerable<AttributeType> AttributeTypes");

		if(allMembers.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tget\n");
			sb.append("\t\t\t{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.append("\t\t\t\tyield return " + formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tyield return " + formatTypeClass(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t}\n");
		}
	}

	private void genGetAttributeType(StringBuffer sb, InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.append("\t\tpublic override AttributeType GetAttributeType(String name)");

		if(allMembers.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tswitch(name)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.append("\t\t\t\tcase \"" + formatIdentifiable(e) + "\" : return " +
							formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tcase \"" + formatIdentifiable(e) + "\" : return " +
							formatTypeClass(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t\treturn null;\n");
			sb.append("\t\t}\n");
		}
	}

	private void getFirstCommonAncestors(InheritanceType curType,
			InheritanceType type, Set<InheritanceType> resTypes) {
		if(type.isCastableTo(curType))
			resTypes.add(curType);
		else
			for(InheritanceType superType : curType.getDirectSuperTypes())
				getFirstCommonAncestors(superType, type, resTypes);
	}

	private void genCreateWithCopyCommons(StringBuffer sb, InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		String cname = formatElementClass(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? extName : cname;
		String kindName = isNode ? "Node" : "Edge";

		if(isNode) {
			sb.append("\t\tpublic override INode CreateNodeWithCopyCommons(INode oldINode)\n"
					+ "\t\t{\n"
					+ "\t\t\tLGSPNode oldNode = (LGSPNode) oldINode;\n"
					+ "\t\t\t" + cname + " newNode = new " + allocName + "();\n");
		}
		else {
			sb.append("\t\tpublic override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)\n"
					+ "\t\t{\n"
					+ "\t\t\tLGSPEdge oldEdge = (LGSPEdge) oldIEdge;\n"
					+ "\t\t\t" + cname + " newEdge = new " + allocName + "((LGSPNode) source, (LGSPNode) target);\n");
		}

		Map<BitSet, LinkedList<InheritanceType>> commonGroups = new LinkedHashMap<BitSet, LinkedList<InheritanceType>>();

		Collection<? extends InheritanceType> typeSet =
			isNode ? (Collection<? extends InheritanceType>) be.nodeTypeMap.keySet()
			: (Collection<? extends InheritanceType>) be.edgeTypeMap.keySet();
		for(InheritanceType itype : typeSet) {
			Set<InheritanceType> firstCommonAncestors = new LinkedHashSet<InheritanceType>();
			getFirstCommonAncestors(itype, type, firstCommonAncestors);

			TreeSet<InheritanceType> sortedCommonTypes = new TreeSet<InheritanceType>(
				new Comparator<InheritanceType>() {
					public int compare(InheritanceType o1, InheritanceType o2) {
						return o2.getMaxDist() - o1.getMaxDist();
					}
				});

			sortedCommonTypes.addAll(firstCommonAncestors);
			Iterator<InheritanceType> iter = sortedCommonTypes.iterator();
			while(iter.hasNext()) {
				InheritanceType commonType = iter.next();
				if(!firstCommonAncestors.contains(commonType)) continue;
				for(InheritanceType superType : commonType.getAllSuperTypes()) {
					firstCommonAncestors.remove(superType);
				}
			}

			boolean mustCopyAttribs = false;
			for(InheritanceType commonType : firstCommonAncestors) {
				if(commonType.getAllMembers().size() != 0) {
					mustCopyAttribs = true;
					break;
				}
			}

			if(!mustCopyAttribs) continue;

			BitSet commonTypesBitset = new BitSet();
			for(InheritanceType commonType : firstCommonAncestors) {
				commonTypesBitset.set(commonType.getTypeID());
			}
			LinkedList<InheritanceType> commonList = commonGroups.get(commonTypesBitset);
			if(commonList == null) {
				commonList = new LinkedList<InheritanceType>();
				commonGroups.put(commonTypesBitset, commonList);
			}
			commonList.add(itype);
		}

		if(commonGroups.size() != 0) {
			sb.append("\t\t\tswitch(old" + kindName + ".Type.TypeID)\n"
					+ "\t\t\t{\n");
			for(Map.Entry<BitSet, LinkedList<InheritanceType>> entry : commonGroups.entrySet()) {
				for(InheritanceType itype : entry.getValue()) {
					sb.append("\t\t\t\tcase (int) " + kindName + "Types.@"
							+ formatIdentifiable(itype) + ":\n");
				}
				BitSet bitset = entry.getKey();
				HashSet<Entity> copiedAttribs = new HashSet<Entity>();
				for(int i = bitset.nextSetBit(0); i >= 0; i = bitset.nextSetBit(i+1)) {
					InheritanceType commonType = InheritanceType.getByTypeID(i);
					Collection<Entity> members = commonType.getAllMembers();
					if(members.size() != 0) {
						sb.append("\t\t\t\t\t// copy attributes for: "
								+ formatIdentifiable(commonType) + "\n");
						boolean alreadyCasted = false;
						for(Entity member : members) {
							if(copiedAttribs.contains(member)) {
								sb.append("\t\t\t\t\t\t// already copied: " + formatIdentifiable(member) + "\n");
								continue;
							}
							if(!alreadyCasted) {
								alreadyCasted = true;
								sb.append("\t\t\t\t\t{\n"
										+ "\t\t\t\t\t\t" + formatCastedAssign(commonType, "I", "old")
										+ "old" + kindName + ";\n");
							}
							copiedAttribs.add(member);
							String memberName = formatIdentifiable(member);
							sb.append("\t\t\t\t\t\tnew" + kindName + ".@" + memberName
									+ " = old.@" + memberName + ";\n");
						}
						if(alreadyCasted)
							sb.append("\t\t\t\t\t}\n");
					}
				}
				sb.append("\t\t\t\t\tbreak;\n");
			}
			sb.append("\t\t\t}\n");
		}

		sb.append("\t\t\treturn new" + kindName + ";\n"
				+ "\t\t}\n\n");
	}

	////////////////////////////
	// Model class generation //
	////////////////////////////

	/**
	 * Generates the model class for the edge or node types.
	 */
	private void genModelClass(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " model\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic sealed class " + formatIdentifiable(be.unit) + formatNodeOrEdge(isNode)
				+ "Model : I" + (isNode ? "Node" : "Edge") + "Model\n");
		sb.append("\t{\n");

		InheritanceType rootType = genModelConstructor(sb, isNode, types);

		sb.append("\t\tpublic bool IsNodeModel { get { return " + (isNode?"true":"false") +"; } }\n");
		sb.append("\t\tpublic " + (isNode ? "Node" : "Edge") + "Type RootType { get { return "
				+ formatTypeClass(rootType) + ".typeVar; } }\n");
		sb.append("\t\tGrGenType ITypeModel.RootType { get { return " + formatTypeClass(rootType) + ".typeVar; } }\n");
		sb.append("\t\tpublic " + (isNode ? "Node" : "Edge") + "Type GetType(String name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t\tcase \"" + formatIdentifiable(type) + "\" : return " + formatTypeClass(type) + ".typeVar;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
		sb.append("\t\tGrGenType ITypeModel.GetType(String name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn GetType(name);\n");
		sb.append("\t\t}\n");

		String elemKind = isNode ? "Node" : "Edge";
		sb.append("\t\tprivate " + elemKind + "Type[] types = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t" + formatTypeClass(type) + ".typeVar,\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic " + elemKind + "Type[] Types { get { return types; } }\n");
		sb.append("\t\tGrGenType[] ITypeModel.Types { get { return types; } }\n");

		sb.append("\t\tprivate Type[] typeTypes = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\ttypeof(" + formatTypeClass(type) + "),\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic Type[] TypeTypes { get { return typeTypes; } }\n");

		sb.append("\t\tprivate AttributeType[] attributeTypes = {\n");
		for(InheritanceType type : types) {
			String ctype = formatTypeClass(type);
			for(Entity member : type.getMembers())
				sb.append("\t\t\t" + ctype + "." + formatAttributeTypeName(member) + ",\n");
		}
		sb.append("\t\t};\n");
		sb.append("\t\tpublic IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }\n");

		sb.append("\t}\n");
	}

	private InheritanceType genModelConstructor(StringBuffer sb, boolean isNode, Set<? extends InheritanceType> types) {
		InheritanceType rootType = null;

		sb.append("\t\tpublic " + formatIdentifiable(be.unit) + formatNodeOrEdge(isNode) + "Model()\n");
		sb.append("\t\t{\n");
		for(InheritanceType type : types) {
			String ctype = formatTypeClass(type);
			sb.append("\t\t\t" + ctype + ".typeVar.subOrSameGrGenTypes = "
					+ formatTypeClass(type) + ".typeVar.subOrSameTypes = new "
					+ (isNode ? "Node" : "Edge") + "Type[] {\n");
			sb.append("\t\t\t\t" + ctype + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && otherType.isCastableTo(type))
					sb.append("\t\t\t\t" + formatTypeClass(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");

			sb.append("\t\t\t" + ctype + ".typeVar.superOrSameGrGenTypes = "
					+ formatTypeClass(type) + ".typeVar.superOrSameTypes = new "
					+ (isNode ? "Node" : "Edge") + "Type[] {\n");
			sb.append("\t\t\t\t" + ctype + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && type.isCastableTo(otherType))
					sb.append("\t\t\t\t" + formatTypeClass(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");
			if(type.isRoot())
				rootType = type;
		}
		sb.append("\t\t}\n");

		return rootType;
	}

	/**
	 * Generates the graph model class.
	 */
	private void genGraphModel(StringBuffer sb) {
		String unitName = formatIdentifiable(be.unit);
		sb.append("\t//\n");
		sb.append("\t// IGraphModel implementation\n");
		sb.append("\t//\n");
		sb.append("\n");

		sb.append("\tpublic sealed class " + unitName + "GraphModel : IGraphModel\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate " + unitName + "NodeModel nodeModel = new " + unitName + "NodeModel();\n");
		sb.append("\t\tprivate " + unitName + "EdgeModel edgeModel = new " + unitName + "EdgeModel();\n");
		genValidate(sb);
		sb.append("\n");

		sb.append("\t\tpublic String Name { get { return \"" + unitName + "\"; } }\n");
		sb.append("\t\tpublic INodeModel NodeModel { get { return nodeModel; } }\n");
		sb.append("\t\tpublic IEdgeModel EdgeModel { get { return edgeModel; } }\n");
		sb.append("\t\tpublic IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }\n");
		sb.append("\t\tpublic String MD5Hash { get { return \"" + be.unit.getTypeDigest() + "\"; } }\n");

		sb.append("\t}\n");
	}

	private void genValidate(StringBuffer sb) {
		sb.append("\t\tprivate ValidateInfo[] validateInfos = {\n");

		for(EdgeType edgeType : be.edgeTypeMap.keySet()) {
			for(ConnAssert ca :edgeType.getConnAsserts()) {
				sb.append("\t\t\tnew ValidateInfo(");
				sb.append(formatTypeClass(edgeType) + ".typeVar, ");
				sb.append(formatTypeClass(ca.getSrcType()) + ".typeVar, ");
				sb.append(formatTypeClass(ca.getTgtType()) + ".typeVar, ");
				sb.append(formatInt(ca.getSrcLower()) + ", ");
				sb.append(formatInt(ca.getSrcUpper()) + ", ");
				sb.append(formatInt(ca.getTgtLower()) + ", ");
				sb.append(formatInt(ca.getTgtUpper()));
				sb.append("),\n");
			}
		}

		sb.append("\t\t};\n");
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;
	private String curMemberOwner = null;
}

