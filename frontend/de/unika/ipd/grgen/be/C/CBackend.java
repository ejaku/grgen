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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.PrintStream;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.ir.Action;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.Util;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A backend for the C interface to grgen.
 */
public abstract class CBackend extends IDBase implements Backend {


	/** The unit to generate code for. */
	protected Unit unit;

	/** The output path as handed over by the frontend. */
	private File path;

	/** the extension of the generated include files */
	public final String incExtension = ".inc";

	/** The error reporter. */
	protected ErrorReporter error;


	/**
	 * Get the IR root node.
	 * @return The Unit node of the IR.
	 */
	protected Unit getUnit() {
		return unit;
	}

	/**
	 * Mangle an identifier.
	 * @param id The identifier.
	 * @return A mangled name.
	 */
	protected String mangle(Identifiable id) {
		String s = id.getIdent().toString();

		s = s.replaceAll("_", "__");
		s = s.replace('$', '_');

		return s;
	}

	/**
	 * Write a character sequence to a file using the path set.
	 * @param filename The filename.
	 * @param cs A character sequence.
	 */
	protected final void writeFile(String filename, CharSequence cs) {
		Util.writeFile(new File(path, filename), cs, error);
	}

	protected final PrintStream openFile(String filename) {
		return Util.openFile(new File(path, filename), error);
	}

	protected final void closeFile(PrintStream ps) {
		Util.closeFile(ps);
	}

	/**
	 * Make C defines for each type in a type map.
	 * This method makes defines like<br>
	 * #define GR_<code>labelAdd</code>_TYPE_<code>typename<InheritanceType/code>
	 * @param sb The string buffer to add to.
	 * @param typeMap The type map containing the types to dump.
	 * @param labelAdd The string that should be added to the define.
	 */
	protected void makeTypeDefines(PrintStream ps, Map<? extends InheritanceType, Integer> typeMap,
								   String labelAdd) {
		ps.print("/** Use this macro to check, if an id is a valid type */\n");
		ps.print("#define GR_" + labelAdd + "_TYPE_VALID(t) "
					 + "((t) >= 0 && (t) < " + typeMap.size() + ")\n\n");

		ps.print("/** The number of types defined */\n");
		ps.print("#define GR_" + labelAdd + "_TYPES " + typeMap.size()
					 + "\n\n");
		for(InheritanceType ty : typeMap.keySet()) {
			Ident id = ty.getIdent();

			ps.print("/** type " + id + " defined at line "
						 + id.getCoords().getLine() + " */\n");
			ps.print("#define GR_DEF_" + labelAdd + "_TYPE_"
						 + mangle(ty) + " " + typeMap.get(ty) + "\n\n");
		}
	}

	/**
	 * Make defines for attribute IDs.
	 * @param sb The string buffer to add the code to.
	 * @param attrMap The attribute map to use.
	 * @param labelAdd The string to add to the define's name.
	 */
	protected void makeAttrDefines(PrintStream ps, Map<Entity, Integer> attrMap,
								   String labelAdd) {
		ps.print("/** Number of attributes macro for " + labelAdd + " */\n");
		ps.print("#define GR_" + labelAdd + "_ATTRS " + attrMap.size() + "\n\n");

		ps.print("/** Attribute valid macro for " + labelAdd + " */\n");
		ps.print("#define GR_" + labelAdd + "_ATTR_VALID(a) "
					 + "((a) >= 0 && (a) < " + attrMap.size() + ")\n\n");

		for(Iterator<Entity> it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = it.next();
			Ident id = ent.getIdent();

			ps.print("/** Attribute " + id + " of "
						 + ent.getOwner().getIdent() + " in line "
						 + id.getCoords().getLine() + " */\n");
			ps.print("#define GR_DEF_" + labelAdd + "_ATTR_"
						 + mangle(ent.getOwner()) + "_"
						 + mangle(ent) + " " + attrMap.get(ent) + "\n\n");
		}
	}

	/**
	 * Make defines for enum types.
	 * @param sb The string buffer to add the code to.
	 * @param map The enum type map.
	 */
	protected void makeEnumDefines(PrintStream ps, Map<EnumType, Integer> map) {
		ps.print("/** Number of enum types. */\n");
		ps.print("#define GR_DEF_ENUMS " + map.size() + "\n\n");

		ps.print("/** Use this macro to check, if an id is a valid enum type */\n");
		ps.print("#define GR_ENUM_TYPE_VALID(t) ((t) >= 0 && (t) < " + map.size() +")\n\n");
	}

	/**
	 * Make a C array containing the strings made of the names of the
	 * objects in the map. The index of a string corresponds to the
	 * integer value in the map.
	 * @param sb The string buffer to append the text to.
	 * @param map The type map which contains the types.
	 * @param add A string which shall prepend the name of the array.
	 */
	protected void makeTypeMap(PrintStream ps, Map<? extends InheritanceType, Integer> map, String add) {
		String[] names = new String[map.size()];

		for(Identifiable ty : map.keySet()) {
			int index = getTypeId(map, ty);
			names[index] = ty.getIdent().toString();
		}

		ps.print("static const char *" + add + "_type_map[] = {\n");
		for(int i = 0; i < names.length; i++) {
			ps.print("  \"" + names[i] + "\",\n");
		}
		ps.print("  NULL\n};\n\n");
	}

	/**
	 * Make the attribute map.
	 * Each entry consists of an type ID that represents the attributes
	 * owner type, and the name of the attribute.
	 * @param sb The string buffer to add the code to.
	 * @param attrMap The attribute map.
	 * @param typeMap The type map for these attributes.
	 * @param enumMap The enum type map.
	 * @param add A string to add to the identifier of the map.
	 */
	protected void makeAttrMap(PrintStream ps, Map<Entity, Integer> attrMap,
							   Map<? extends InheritanceType, Integer> typeMap,
							   Map<EnumType, Integer> enumMap, String add) {
		String[] name = new String[attrMap.size()];
		Type[] types = new Type[attrMap.size()];
		Integer[] owner = new Integer[attrMap.size()];

		for(Iterator<Entity> it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = it.next();
			int index = attrMap.get(ent).intValue();
			name[index] = ent.getIdent().toString();
			owner[index] = new Integer(getTypeId(typeMap, ent.getOwner()));
			types[index] = ent.getType();
		}

		ps.print("/** The attribute map for " + add + " attributes. */\n");
		ps.print("static const attr_t " + add + "_attr_map[] = {\n");
		for(int i = 0; i < name.length; i++) {
			ps.print("  { " + owner[i] + ", " + formatString(name[i]) + ", " + types[i].classify() + ", ");

			if (types[i] instanceof EnumType) {
				int id = getTypeId(enumMap, types[i]);
				ps.print(id + " },\n");
			}
			else
				ps.print("-1 },\n");
		}
		ps.print("  { 0, NULL, 0 }\n};\n\n");

	}


	/**
	 * Make a matrix that represents the type relation.
	 * @param buf The string buffer to add the code to.
	 * @param typeMap The type map to use.
	 * @param add A string to add to the identifier.
	 */
	protected void makeIsAMatrix(PrintStream ps, boolean forNode, String add) {
		// since all type id's are given from zero on, the maximum type id
		// (not used) is the number of entries in the type map.
		short[][] matrix = getIsAMatrix(forNode);
		int maxTypeId = matrix.length;
		String matrixName = add + "_type_is_a_matrix";

		ps.print("/** The matrix showing valid type attributes for " + add + ". */\n");
		ps.print("static const char " + matrixName + "[" + maxTypeId + "]["
					 + maxTypeId + "] = {\n");

		for(int i = 0; i < maxTypeId; i++) {
			ps.print("  { ");
			for(int j = 0; j < maxTypeId; j++) {
				ps.print(j != 0 ? ", " : "");
				ps.print(matrix[i][j]);
			}
			ps.print(" }, /* ");
			ps.print(i);
			ps.print(' ');
			ps.print(getTypeName(forNode, i));
			ps.print(" */\n");
		}
		ps.print("};\n\n");
		ps.print("/** Function to test for type compatibility. */\n");
		ps.print("static inline int ");
		ps.print(add);
		ps.print("_type_is_a(int t1, int t2) {\n");
		ps.print("  return t1 == t2 || " + matrixName + "[t1][t2] != 0;\n}\n\n");
	}

	protected void makeSuperSubTypes(PrintStream ps, boolean forNode, String add) {
		int[] types = getIDs(forNode);
		int maxTypeId = types.length;

		ps.print("static const char " + add + "_super_types[" + (maxTypeId + 1) + "]["
					 + maxTypeId + "] = {\n");

		for(int i = 0; i < maxTypeId; i++) {
			int[] superTypes = getSuperTypes(forNode, i);
			ps.print("  /* super types of ");
			ps.print(getTypeName(forNode, i));
			ps.print(": ");
			for(int j = 0; j < superTypes.length; j++) {
				ps.print(getTypeName(forNode, superTypes[j]));
				ps.print(" ");
			}
			ps.print(" */\n");

			ps.print("  { ");
			for(int j = 0; j < superTypes.length; j++) {
				ps.print(superTypes[j]);
				ps.print(", ");
			}
			ps.print("-1 },\n\n");
		}
		ps.print("};\n\n");

		ps.print("static const char " + add + "_sub_types[" + (maxTypeId + 1) + "]["
					 + maxTypeId + "] = {\n");
		for(int i = 0; i < maxTypeId; i++) {
			int[] subTypes = getSubTypes(forNode, i);
			ps.print("  /* sub types of ");
			ps.print(getTypeName(forNode, i));
			ps.print(": ");
			for(int j = 0; j < subTypes.length; j++) {
				ps.print(getTypeName(forNode, subTypes[j]));
				ps.print(" ");
			}
			ps.print(" */\n  {");
			for(int j = 0; j < subTypes.length; j++) {
				ps.print(subTypes[j]);
				ps.print(", ");
			}
			ps.print("-1 },\n\n");
		}
		ps.print("};\n\n");
	}

	/**
	 * Make the attribute matrix for a given attribute type.
	 *
	 * @param sb      The string buffer to add the code to.
	 * @param add     The matrix prefix.
	 * @param attrMap The map of all attributes.
	 * @param typeMap The type map to use.
	 */
	protected void makeAttrMatrix(PrintStream ps, String add,
								  Map<Entity, Integer> attrMap, Map<? extends InheritanceType, Integer> typeMap) {

		int maxTypeId = typeMap.size();
		int maxAttrId = attrMap.size();
		int[][] matrix = new int[maxTypeId][maxAttrId];

		for(Iterator<Entity> it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = it.next();
			int attrId = attrMap.get(ent).intValue();
			int typeId = getTypeId(typeMap, ent.getOwner());
			matrix[typeId][attrId] = 1;
		}

		ps.print("static const char " + add + "_attr_matrix[" + maxTypeId + "]["
					 + maxAttrId + "] = {\n");

		for(int i = 0; i < maxTypeId; i++) {
			ps.print("  { ");
			for(int j = 0; j < maxAttrId; j++)
				ps.print((j != 0 ? ", " : "") + matrix[i][j]);
			ps.print(" },\n");
		}
		ps.print("};\n");
	}

	protected void makeActionMap(PrintStream ps, Map<Rule, Integer> map) {
		Action[] actions = new Action[map.size()];

		for(Iterator<Rule> it = map.keySet().iterator(); it.hasNext();) {
			Action a = it.next();
			int index = map.get(a).intValue();
			actions[index] = a;
		}

		ps.print("#define GR_ACTION_VALID(x) ((x) >= 0 && (x) < "
					 + actions.length + ")\n\n");
		ps.print("#define GR_ACTIONS " + actions.length + "\n\n");

		ps.print("static const action_t action_map[] = {\n");
		for(int i = 0; i < actions.length; i++) {
			Action a = actions[i];
			String kind = "gr_action_kind_test";

			if(a instanceof Rule && ((Rule)a).getRight()!=null)
				kind = "gr_action_kind_rule";

			ps.print("  { " + formatString(a.getIdent().toString()) + ", "
						 + kind + ", 0, 0, NULL, NULL },\n");
		}
		ps.print("  { NULL, -1, 0, 0, NULL, NULL }\n};\n\n");
	}

	/**
	 * Generate code for matching actions.
	 * @param sb The string buffer to add the code to.
	 */
	protected void makeActions(PrintStream ps) {
		for(Iterator<Rule> it = actionRuleMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = actionRuleMap.get(a).intValue();
			genMatch(ps, a, id);
			genFinish(ps, a, id);
		}
	}

	/**
	 * Adds a XML type tag to the string buffer.
	 *
	 * @param depth  indentation depth
	 * @param sb     the string buffer
	 * @param ending the end of the XML tag, either ">" or "/>"
	 * @param inh    the type
	 */
	protected void dumpXMLTag(int depth, PrintStream ps, String ending, Type inh) {
		for (int i = 0; i < depth; ++i)
			ps.print("  ");
		ps.print("<" + inh.getName().replace(' ', '_')
					 + " name=\"" + inh.getIdent() + "\"" + ending);
	}

	/**
	 * Adds a XML end type tag to the string buffer.
	 *
	 * @param depth indentation depth
	 * @param sb    the string buffer
	 * @param inh   the type
	 */
	protected void dumpXMLEndTag(int depth, PrintStream ps, Type inh) {
		for (int i = 0; i < depth; ++i)
			ps.print("  ");
		ps.print("</" + inh.getName().replace(' ', '_') + ">\n");
	}

	/**
	 * Adds an XML entity tag to the string buffer.
	 *
	 * @param depth  indentation depth
	 * @param sb     the string buffer
	 * @param ending the end of the XML tag, either ">" or "/>"
	 * @param ent    the entity
	 */
	protected void dumpXMLTag(int depth, PrintStream ps, String ending, Entity ent) {
		for (int i = 0; i < depth; ++i)
			ps.print("  ");
		ps.print("<" + ent.getName().replace(' ', '_')
					 + " name=\"" + ent.getIdent() + "\""
					 + " type=\"" + ent.getType().getIdent() + "\"" + ending);
	}

	/**
	 * Adds a XML end entity tag to the string buffer.
	 *
	 * @param depth indentation depth.
	 * @param sb    the string buffer.
	 * @param ent   the entity.
	 */
	protected void dumpXMLEndTag(int depth, PrintStream ps, Entity ent) {
		for (int i = 0; i < depth; ++i)
			ps.print("  ");
		ps.print("</" + ent.getName().replace(' ', '_') + ">\n");
	}

	/**
	 * Adds a XML enum value tag to the string buffer.
	 *
	 * @param depth  indentation depth.
	 * @param sb     the string buffer.
	 * @param ending the end of the XML tag, either ">" or "/>".
	 * @param ev     the enum item.
	 */
	protected void dumpXMLTag(int depth, PrintStream ps, String ending, EnumItem ev) {
		for (int i = 0; i < depth; ++i)
			ps.print("  ");
		ps.print("<" + ev.getName().replace(' ', '_')
					 + " name=\"" + ev + "\" value=\"" + ev.getValue().getValue() + "\"" + ending);
	}

	/**
	 * Dump an overview of all declared types, attributes and enums to
	 * an XML file.
	 *
	 * @param sb The string buffer to put the XML stuff to.
	 */
	protected void writeOverview(PrintStream ps) {
		Map<? extends InheritanceType, Integer>[] maps = new Map[] {
			nodeTypeMap,
			edgeTypeMap
		};

		ps.print("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");

		ps.print("<unit>\n");

		for(int i = 0; i < maps.length; i++) {
			for(Iterator<? extends InheritanceType> it = maps[i].keySet().iterator(); it.hasNext();) {
				InheritanceType type = it.next();
				dumpXMLTag(1, ps, ">\n", type);

				Iterator<InheritanceType> inhIt = type.getDirectSuperTypes().iterator();

				if (inhIt.hasNext()) {
					ps.print("    <inherits>\n");
					for(; inhIt.hasNext();) {
						InheritanceType inh = inhIt.next();
						dumpXMLTag(3, ps, "/>\n", inh);
					}
					ps.print("    </inherits>\n");
				}

				Iterator<Entity> attrIt = type.getMembers().iterator();
				if (attrIt.hasNext()) {
					ps.print("    <attributes>\n");
					for(; attrIt.hasNext();) {
						Entity ent = attrIt.next();

						dumpXMLTag(3, ps, "/>\n", ent);
					}
					ps.print("    </attributes>\n");
				}

				dumpXMLEndTag(1, ps, type);
			}
		}

		for(Iterator<EnumType> it = enumMap.keySet().iterator(); it.hasNext();) {
			EnumType type = it.next();

			dumpXMLTag(1, ps, ">\n", type);
			Iterator<EnumItem> itemIt = type.getItems().iterator();
			if (itemIt.hasNext()) {
				ps.print("    <items>\n");
				for(; itemIt.hasNext();) {
					EnumItem ev = itemIt.next();

					dumpXMLTag(3, ps, "/>\n", ev);
				}
				ps.print("    </items>\n");
			}

			dumpXMLEndTag(1, ps, type);
		}

		ps.print("</unit>\n");
	}

	/**
	 * Make some additional C type declarations that must probably be used
	 * in the generated code.
	 * @param sb The string buffer to the stuff to.
	 */
	protected void makeCTypes(PrintStream ps) {
		ps.print("/** The attribute type classification. */\n");
		ps.print("typedef enum _attribute_type {\n");
		ps.print("  AT_TYPE_INTEGER = " + Type.IS_INTEGER + ", /**< an integer */\n");
		ps.print("  AT_TYPE_BOOLEAN = " + Type.IS_BOOLEAN + ", /**< a boolean */\n");
		ps.print("  AT_TYPE_STRING  = " + Type.IS_STRING + ", /**< a string */\n");
		ps.print("} attribute_type;\n\n");

		ps.print("/** The attribute type. */\n");
		ps.print("typedef struct {\n"
					 + "  int type_id;       /**< the ID of attributes type */\n"
					 + "  const char *name;  /**< the name of the attribute */\n"
					 + "  attribute_type at; /**< the attribute type kind */\n"
					 + "  int enum_id;       /**< the Id of the enum type or -1 */\n"
					 + "} attr_t;\n\n");

		ps.print("/** The type of an action. */\n");
		ps.print("typedef struct {\n"
					 + "  const char *name;\n"
					 + "  gr_action_kind_t kind;\n"
					 + "  int ins;\n"
					 + "  int outs;\n"
					 + "  const gr_value_kind_t *in_types;\n"
					 + "  const gr_value_kind_t *out_types;\n"
					 + "} action_t;\n\n");

		ps.print("/** The type of an enum item declaration. */\n");
		ps.print("typedef struct {\n"
					 + "  const char *name;    /**< the name of the enum item */\n"
					 + "  int value;           /**< the value of the enum item */\n"
					 + "} enum_item_decl_t;\n\n");

		ps.print("/** The type of an enum declaration. */\n");
		ps.print("typedef struct {\n"
					 + "  const char *name;    /**< the name of the enum type */\n"
					 + "  int num_items;       /**< the number of items in this enum type */\n"
					 + "  const enum_item_decl_t *items;  /**< the items of this enum type */\n"
					 + "} enum_type_decl_t;\n\n");

		ps.print("/** The type of the enum table. */\n");
		ps.print("typedef struct {\n"
					 + "  const enum_type_decl_t *type; /**< declaration of the type */\n"
					 + "  int type_id;                  /**< the Id of this enum type */\n"
					 + "} enum_types_t;\n\n");
	}

	/**
	 * Dump all enum type declarations to a string buffer.
	 *
	 * @param sb   The string buffer.
	 * @param map  A map containing all enum types.
	 */
	protected void makeEnumDeclarations(PrintStream ps, Map<EnumType, Integer> map) {
		// build the description of all enum types
		for(Iterator<EnumType> it = map.keySet().iterator(); it.hasNext();) {
			EnumType type = it.next();
			Ident name = type.getIdent();

			ps.print("/** The items for the " + name + " enum type. */\n");
			ps.print("static const enum_item_decl_t _" + name + "_items[] = {\n");

			for(EnumItem ev : type.getItems()) {
				ps.print(" { \"" + ev + "\", " + ev.getValue().getValue() + " },\n");
			}
			ps.print("};\n\n");

			ps.print("/** The declaration of the " + name + " enum type. */\n");
			ps.print("static const enum_type_decl_t " + name + "_decl = {\n"
						 + "  \"" + name + "\",\n"
						 + "  sizeof(_" + name + "_items)/sizeof(_" + name + "_items[0]),\n"
						 + "  _" + name + "_items,\n"
						 + "};\n\n");
		}

		// dump all enums to a table
		ps.print("/** All enum types. */\n");
		ps.print("static const enum_types_t enum_types[] = {\n");

		String[] names = new String[map.size()];
		for(Iterator<EnumType> it = map.keySet().iterator(); it.hasNext();) {
			EnumType type = it.next();
			int index = getTypeId(map, type);

			names[index] = type.getIdent().toString();
		}

		for(int i = 0; i < map.size(); ++i) {
			ps.print("  { &" + names[i] + "_decl, " + i + " },\n");
		}
		ps.print("};\n\n");
	}

	/**
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		this.unit = unit;
		this.error = system.getErrorReporter();
		this.path = outputPath;
		path.mkdirs();

		makeTypes(unit);
	}

	/**
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {

		String unitName = formatString(unit.getUnitName());

		// Emit the C types file
		PrintStream ps = openFile("types" + incExtension);
		makeCTypes(ps);
		closeFile(ps);

		// Emit the type defines.
		ps = openFile("graph" + incExtension);
		ps.println("/** name of the unit */\n");
		ps.println("#define UNIT_NAME " + unitName + "\n\n");

		ps.println("/** type model digest */\n");
		ps.println("#define TYPE_MODEL_DIGEST \"" + unit.getTypeDigest() + "\"\n\n");

		makeTypeDefines(ps, nodeTypeMap, "NODE");
		makeTypeDefines(ps, edgeTypeMap, "EDGE");
		makeAttrDefines(ps, nodeAttrMap, "NODE");
		makeAttrDefines(ps, edgeAttrMap, "EDGE");
		makeEnumDefines(ps, enumMap);
		closeFile(ps);

		ps = openFile("enums" + incExtension);
		makeEnumDeclarations(ps, enumMap);
		closeFile(ps);


		// Make the "is a" matrices.
		ps = openFile("is_a" + incExtension);
		makeIsAMatrix(ps, true, "node");
		makeIsAMatrix(ps, false, "edge");
		closeFile(ps);

		ps = openFile("super_sub_types" + incExtension);
		makeSuperSubTypes(ps, true, "node");
		makeSuperSubTypes(ps, false, "edge");
		closeFile(ps);

		// Make the attribute matrices
		ps = openFile("attr" + incExtension);
		makeAttrMatrix(ps, "node", nodeAttrMap, nodeTypeMap);
		makeAttrMatrix(ps, "edge", edgeAttrMap, edgeTypeMap);
		closeFile(ps);

		// Make arrays with names of the types.
		ps = openFile("names" + incExtension);
		makeTypeMap(ps, nodeTypeMap, "node");
		makeTypeMap(ps, edgeTypeMap, "edge");
		makeAttrMap(ps, nodeAttrMap, nodeTypeMap, enumMap, "node");
		makeAttrMap(ps, edgeAttrMap, edgeTypeMap, enumMap, "edge");
		closeFile(ps);

		ps = openFile("actions" + incExtension);
		makeActionMap(ps, actionRuleMap);
		closeFile(ps);

		ps = openFile("action_impl" + incExtension);
		makeActions(ps);
		closeFile(ps);

		// write an overview of all generated Ids
		ps = openFile("overview.xml");
		writeOverview(ps);
		closeFile(ps);

		// Make validate data structures.
		genValidateStatements();

		// a hook for special generated things
		genExtra();
	}


	protected abstract void genMatch(PrintStream sb, MatchingAction a, int id);

	protected abstract void genFinish(PrintStream sb, MatchingAction a, int id);

	/**
	 * Generate some extra stuff.
	 * This function is called after everything else is generated.
	 */
	protected abstract void genExtra();

	/**
	 * @see de.unika.ipd.grgen.be.Backend#done()
	 */
	public void done() {
	}

	/**
	 * @see de.unika.ipd.grgen.be.C.Formatter#formatId(java.lang.String)
	 */
	public String formatId(String id) {
		return id;
	}

	/**
	 * Format a string into a C string.
	 * This takes a Java string and produces a C string literal of it by escaping
	 * some characters and putting quotes around it.
	 * If a character is equal to the constant <code>BREAK_LINE</code> defined
	 * above, the string literal is ended and continued at the next line.
	 * This gives a better readability, if used properly.
	 * @param s A string.
	 * @return A C string literal.
	 */
	public static String formatString(String s) {
		ByteArrayOutputStream bos = new ByteArrayOutputStream(s.length() * 2);
		PrintStream ps = new PrintStream(bos);
		formatString(ps, s);
		ps.flush();
		ps.close();
		return bos.toString();
	}

	public static void formatString(PrintStream ps, String s) {
		ps.print('\"');
		for(int i = 0; i < s.length(); i++) {
			char ch = s.charAt(i);
			switch(ch) {
				case '\"':
					ps.print("\\\"");
					break;
				case '\'':
					ps.print("\\\'");
					break;
				case '\n':
				case '\t':
					break;
				default:
					ps.print(ch);
			}
		}
		ps.print('\"');
	}

	protected void genValidateStatements() {
		StringBuffer sb = new StringBuffer();

		sb.append("\n/** The Validate Info */\n\n");
		sb.append("static gr_validate_info_t valid_info[] = {\n");

		for(InheritanceType et : edgeTypeMap.keySet()) {
			EdgeType edgeType = (EdgeType)et;
			for(ConnAssert ca : edgeType.getConnAsserts()) {
				sb.append("\n{\n");
				sb.append("  "+getId(edgeType)+",\n");
				sb.append("  "+getId(ca.getSrcType())+",\n");
				sb.append("  "+getId(ca.getTgtType())+",\n");
				sb.append("  "+ca.getSrcLower()+",\n");
				sb.append("  "+ca.getSrcUpper()+",\n");
				sb.append("  "+ca.getTgtLower()+",\n");
				sb.append("  "+ca.getTgtUpper()+",\n");
				sb.append("},\n");
			}
		}
		sb.append("\n{-1, -1, -1, -1, -1, -1, -1}\n\n};\n\n");

		writeFile("valid_info" + incExtension, sb);
	}
}







