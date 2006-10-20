/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

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
 * A GrGen Backend which generates C# code for a search-plan-based
 * graph model impl and a frame based graph matcher
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.be.Csharp;


import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.be.C.fb.MoreInformationCollector;
import java.io.File;
import java.io.PrintStream;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

public class SearchPlanBackend extends MoreInformationCollector implements Backend, BackendFactory {
	
	private final int OUT = 0;
	private final int IN = 1;
	
	private final String MODE_EDGE_NAME = "has_mode";
	
	protected final boolean emit_subgraph_info = false;
	
	/* binary operator symbols of the C-language */
	// ATTENTION: the forst two shift operations are signed shifts
	// 		the second right shift is signed. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	private String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", null, null, null, null
	};
	
	
	private class IdGenerator<T> {
		ArrayList<T> list = new ArrayList<T>();
		Set<T> set = new HashSet<T>();
		
		private int computeId(T elem) {
			if(!set.contains(elem)) {
				set.add(elem);
				list.add(elem);
			}
			return list.indexOf(elem);
		}
		
		private boolean isKnown(T elem) {
			return set.contains(elem);
		}
	}
	
	//Kann man das entfernen???
	//ToDo: Pruefe das, ob man diese Methode wegschmeissen kann!!!
	
	/**
	 * Method makeEvals
	 *
	 * @param    ps                  a  PrintStream
	 *
	 */
	protected void makeEvals(PrintStream ps) {
		// TODO
	}
	
//	// The unit to generate code for.
//	protected Unit unit;
//	// keine Ahnung wozu das gut sein soll
//	protected Sys system;
//	// The output path as handed over by the frontend.
//	private File path;
	
	/**
	 * Create a new backend.
	 * @return A new backend.
	 */
	public Backend getBackend() throws BackendException {
		return this;
	}
	
	/**
	 * Initializes the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.Sys, java.io.File)
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		super.init(unit, system, outputPath);
//		this.unit = unit;
//		this.path = outputPath;
//		this.system = system;
//		path.mkdirs();
	}
	
	/**
	 * Starts the C-code Genration of the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		// Emit an include file for Makefiles
		
		System.out.println("The " + this.getClass() + " GrGen backend...");
		genModel();
		
		System.out.println("done!");
	}
	
	private void genModel() {
		StringBuffer sb = new StringBuffer();
		String filename = formatId(unit.getIdent().toString()) + "Model.cs";
		
		System.out.println("  generating the "+filename+" file...");
		
		
		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.models\n");
		sb.append("{\n");
		
		System.out.println("    generating enums...");
		sb.append("\t//\n");
		sb.append("\t// Enums\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic enum ENUM_State { ... };\n");
		
		System.out.println("    generating node types...");
		sb.append("\n");
		genModelTypes(sb, nodeTypeMap.keySet(), true);
		
		System.out.println("    generating node model...");
		sb.append("\n");
		genModelModel(sb, nodeTypeMap.keySet(), true);
		
		System.out.println("    generating edge types...");
		sb.append("\n");
		genModelTypes(sb, edgeTypeMap.keySet(), false);
		
		System.out.println("    generating edge model...");
		sb.append("\n");
		genModelModel(sb, edgeTypeMap.keySet(), false);
		
		
		sb.append("}\n");
		
		writeFile(filename, sb);
	}
	
	private void genModelTypes(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " types\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic enum " + formatNodeOrEdge(isNode) + "Types { ");
		for(Identifiable id : types) {
			String type = id.getIdent().toString();
			sb.append(formatId(type) + ", ");
		}
		sb.append("};\n");
		
		for(InheritanceType type : types) {
			genModelType(sb, types, type);
		}
	}
	
	private void genModelModel(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " model\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic sealed class " + formatNodeOrEdge(isNode) + "Model : ITypeModel\n");
		sb.append("\t{\n");
		
		InheritanceType rootType = genModelModel1(sb, isNode, types);
		
		genModelModel2(sb, isNode, rootType, types);
		
		genModelModel3(sb, types);
		
		sb.append("\t}\n");
	}
	
	private InheritanceType genModelModel1(StringBuffer sb, boolean isNode, Set<? extends InheritanceType> types) {
		InheritanceType rootType = null;
		
		sb.append("\t\tpublic " + formatNodeOrEdge(isNode) + "Model()\n");
		sb.append("\t\t{\n");
		for(InheritanceType type : types) {
			sb.append("\t\t\t" + formatType(type) + ".typeVar.subOrSameTypes = new ITypeFramework[] {\n");
			for(InheritanceType otherType : types) {
				if(otherType.isCastableTo(type))
					sb.append("\t\t\t\t" + formatType(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");
			
			sb.append("\t\t\t" + formatType(type) + ".typeVar.superOrSameTypes = new ITypeFramework[] {\n");
			for(InheritanceType otherType : types) {
				if(type.isCastableTo(otherType))
					sb.append("\t\t\t\t" + formatType(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");
			if(type.isRoot())
				rootType = type;
		}
		sb.append("\t\t}\n");
		
		return rootType;
	}
	
	private void genModelModel2(StringBuffer sb, boolean isNode, InheritanceType rootType, Set<? extends InheritanceType> types) {
		sb.append("\t\tpublic bool isNodeModel { get { return " + (isNode?"true":"false") +"; } }\n");
		sb.append("\t\tpublic IType RootType { get { return " + formatType(rootType) + ".typeVar; } }\n");
		sb.append("\t\tpublic IType GetType(String name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t\tcase \"" + type.getIdent() + "\" : return " + formatType(type) + ".typeVar;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
	}
	
	private void genModelModel3(StringBuffer sb, Set<? extends InheritanceType> types) {
		sb.append("\t\tprivate ITypeFramework[] types = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t" + formatType(type) + ".typeVar,\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic IType[] Types { get { return types; } }\n");
		
		sb.append("\t\tprivate Type[] typeTypes = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\ttypeof(" + formatType(type) + "),\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic Type[] TypeTypes { get { return typeTypes; } }\n");
		
		sb.append("\t\tprivate AttributeType[] attributeTypes = {\n");
		for(InheritanceType type : types)
			for(Entity member : type.getMembers())
				sb.append("\t\t\t" + formatType(type) + "." + formatAttributeType(member) + ",\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }\n");
	}
	
	private void genModelType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		String typeName = type.getIdent().toString();
		String cname = formatNodeOrEdge(type) + "_" + formatId(typeName);
		String tname = formatType(type);
		String iname = "I" + cname;
		
		sb.append("\n");
		sb.append("\t// *** " + formatNodeOrEdge(type) + " " + typeName + " ***\n");
		sb.append("\n");
		
		sb.append("\tpublic interface " + iname + " : ");
		genSuperTypeList(sb, type);
		sb.append("\n");
		sb.append("\t{\n");
		sb.append("\t}\n");
		
		sb.append("\n");
		sb.append("\tpublic sealed class " + cname + " : " + iname + "\n");
		sb.append("\t{\n");
		sb.append("\t}\n");
		
		sb.append("\n");
		sb.append("\tpublic sealed class " + tname + " : TypeFramework<" + tname + ", " + cname + ", " + iname + ">\n");
		sb.append("\t{\n");
		genAttributeAttributes(sb, type);
		sb.append("\t\tpublic " + tname + "()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\ttypeID   = (int) " + formatNodeOrEdge(type) + "Types." + typeName + ";\n");
		genIsA(sb, types, type);
		genIsMyType(sb, types, type);
		sb.append("\t\t}\n");
		sb.append("\t\tpublic override String Name { get { return \"" + typeName + "\"; } }\n");
		sb.append("\t\tpublic override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnum(sb, type);
		genGetAttributeType(sb, type);
		sb.append("\t}\n");
	}
	
	
	/**
	 * Generate a list of supertpes of the actual type.
	 */
	private void genSuperTypeList(StringBuffer sb, InheritanceType type) {
		Collection<InheritanceType> superTypes = type.getSuperTypes();
		
		if(superTypes.isEmpty())
			sb.append("IAttributes");
		
		for(Iterator<InheritanceType> i = superTypes.iterator(); i.hasNext(); ) {
			InheritanceType superType = i.next();
			sb.append("I" + formatNodeOrEdge(type) + "_" + superType.getIdent());
			if(i.hasNext())
				sb.append(", ");
		}
	}
	
	private void genAttributeAttributes(StringBuffer sb, InheritanceType type) {
		for(Entity member : type.getMembers()) // only for locally defined members
			sb.append("\t\tpublic static AttributeType " + formatAttributeType(member) + ";\n");
	}
	
	private void genIsA(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\t\tisA      = new bool[] { ");
		for(InheritanceType nt : types) {
			if(type.isCastableTo(nt))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}
	
	private void genIsMyType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\t\tisMyType      = new bool[] { ");
		for(InheritanceType nt : types) {
			if(nt.isCastableTo(type))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
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
					sb.append("\t\t\t\tyield return " + formatAttributeType(e) + ";\n");
				else
					sb.append("\t\t\t\tyield return " + formatType(ownerType) + "." + formatAttributeType(e) + ";\n");
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
					sb.append("\t\t\t\tcase \"" + e.getIdent() + "\" : return " +
								  formatAttributeType(e) + ";\n");
				else
					sb.append("\t\t\t\tcase \"" + e.getIdent() + "\" : return " +
								  formatType(ownerType) + "." + formatAttributeType(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t\treturn null;\n");
			sb.append("\t\t}\n");
		}
	}
	
	
	
	
	/**
	 * Method genSet dumps C-like Set representaion.
	 *
	 * @param    sb                  a  StringBuffer
	 * @param    set                 a  Set
	 *
	 */
	private void genSet(StringBuffer sb, Set<? extends Entity> set) {
		sb.append('{');
		for(Iterator<? extends Entity> i = set.iterator(); i.hasNext(); ) {
			Entity e = i.next();
			if(e instanceof Node)
				sb.append("n_" + e.getIdent().toString());
			else if (e instanceof Edge)
				sb.append("e_" + e.getIdent().toString());
			else
				sb.append(e.getIdent().toString());
			if(i.hasNext())
				sb.append(", ");
		}
		sb.append('}');
	}
	
	
	/**
	 * Method collectNodesnEdges extracts the nodes and edges occuring in an Expression.
	 *
	 * @param    nodes               a  Set to contain the nodes of cond
	 * @param    edges               a  Set to contain the edges of cond
	 * @param    cond                an Expression
	 *
	 */
	private void collectNodesnEdges(Set<Node> nodes, Set<Edge> edges, Expression cond) {
		if(cond instanceof Qualification) {
			Entity entity = ((Qualification)cond).getOwner();
			if(entity instanceof Node)
				nodes.add((Node)entity);
			else if(entity instanceof Edge)
				edges.add((Edge)entity);
			else
				throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		}
		else if(cond instanceof Operator)
			for(Expression child : ((Operator)cond).getWalkableChildren())
				collectNodesnEdges(nodes, edges, child);
	}
	
	
	private String formatAttributeType(Entity e) {
		return "AttributeType_" + formatId(e.getIdent().toString());
	}
	
	private String formatType(Type type) {
		return formatNodeOrEdge(type) + "Type_" + formatId(type.getIdent().toString());
	}
	
	private String formatNodeOrEdge(boolean isNode) {
		if(isNode)
			return "Node";
		else
			return "Edge";
	}
	
	private String formatNodeOrEdge(Type type) {
		if (type instanceof NodeType)
			return formatNodeOrEdge(true);
		else if (type instanceof EdgeType)
			return formatNodeOrEdge(false);
		else
			throw new IllegalArgumentException("Unknown type" + type + "(" + type.getClass() + ")");
	}
}



