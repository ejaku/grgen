/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.io.File;
import java.util.Iterator;
import java.util.Map;

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
	protected void writeFile(String filename, CharSequence cs) {
		writeFile(new File(path, filename), cs);
	}
	
	/**
	 * Make C defines for each type in a type map.
	 * This method makes defines like<br>
	 * #define GR_<code>labelAdd</code>_TYPE_<code>typename</code>
	 * @param sb The string buffer to add to.
	 * @param typeMap The type map containing the types to dump.
	 * @param labelAdd The string that should be added to the define.
	 */
	protected void makeTypeDefines(StringBuffer sb, Map typeMap,
																 String labelAdd) {
		
		sb.append("/** Use this macro to check, if an id is a valid type */\n");
		sb.append("#define GR_" + labelAdd + "_TYPE_VALID(t) "
								+ "((t) >= 0 && (t) < " + typeMap.size() + ")\n\n");
		
		sb.append("/** The number of types defined */\n");
		sb.append("#define GR_" + labelAdd + "_TYPES " + typeMap.size()
								+ "\n\n");
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			Ident id = ty.getIdent();
			
			sb.append("/** type " + id + " defined at line "
									+ id.getCoords().getLine() + " */\n");
			
			sb.append("#define GR_DEF_" + labelAdd + "_TYPE_"
									+ mangle(ty) + " " + typeMap.get(ty) + "\n\n");
		}
	}
	
	/**
	 * Make defines for attribute IDs.
	 * @param sb The string buffer to add the code to.
	 * @param attrMap The attribute map to use.
	 * @param labelAdd The string to add to the define's name.
	 */
	protected void makeAttrDefines(StringBuffer sb, Map attrMap,
																 String labelAdd) {
		
		sb.append("/** Number of attributes macro for " + labelAdd + " */\n");
		sb.append("#define GR_" + labelAdd + "_ATTRS " + attrMap.size() + "\n\n");
		
		sb.append("/** Attribute valid macro for " + labelAdd + " */\n");
		sb.append("#define GR_" + labelAdd + "_ATTR_VALID(a) "
								+ "((a) >= 0 && (a) < " + attrMap.size() + ")\n\n");
		
		for(Iterator it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = (Entity) it.next();
			Ident id = ent.getIdent();
			
			sb.append("/** Attribute " + id + " of "
									+ ent.getOwner().getIdent() + " in line "
									+ id.getCoords().getLine() + " */\n");
			sb.append("#define GR_DEF_" + labelAdd + "_ATTR_"
									+ mangle(ent.getOwner()) + "_"
									+ mangle(ent) + " " + attrMap.get(ent) + "\n\n");
		}
	}
	
	/**
	 * Make defines for enum types.
	 * @param sb The string buffer to add the code to.
	 * @param map The enum type map.
	 */
	protected void makeEnumDefines(StringBuffer sb, Map map) {
		sb.append("/** Number of enum types. */\n");
		sb.append("#define GR_DEF_ENUMS " + map.size() + "\n\n");
		
		sb.append("/** Use this macro to check, if an id is a valid enum type */\n");
		sb.append("#define GR_ENUM_TYPE_VALID(t) ((t) >= 0 && (t) < " + map.size() +")\n\n");
	}
	
	/**
	 * Make a C array containing the strings made of the names of the
	 * objects in the map. The index of a string corresponds to the
	 * integer value in the map.
	 * @param sb The string buffer to append the text to.
	 * @param map The type map which contains the types.
	 * @param add A string which shall prepend the name of the array.
	 */
	protected void makeTypeMap(StringBuffer sb, Map map, String add) {
		String[] names = new String[map.size()];
		
		for(Iterator it = map.keySet().iterator(); it.hasNext();) {
			Identifiable ty = (Identifiable) it.next();
			int index = getTypeId(map, ty);
			names[index] = ty.getIdent().toString();
		}
		
		sb.append("static const char *" + add + "_type_map[] = {\n");
		for(int i = 0; i < names.length; i++) {
			sb.append("  \"" + names[i] + "\",\n");
		}
		sb.append("  NULL\n};\n\n");
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
	protected void makeAttrMap(StringBuffer sb, Map attrMap,
														 Map typeMap, Map enumMap, String add) {
		
		String[] name = new String[attrMap.size()];
		Type[] types = new Type[attrMap.size()];
		Integer[] owner = new Integer[attrMap.size()];
		
		for(Iterator it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = (Entity) it.next();
			int index = ((Integer) attrMap.get(ent)).intValue();
			name[index] = ent.getIdent().toString();
			owner[index] = new Integer(getTypeId(typeMap, ent.getOwner()));
			types[index] = ent.getType();
		}
		
		sb.append("/** The attribute map for " + add + " attributes. */\n");
		sb.append("static const attr_t " + add + "_attr_map[] = {\n");
		for(int i = 0; i < name.length; i++) {
			sb.append("  { " + owner[i] + ", " + formatString(name[i]) + ", " + types[i].classify() + ", ");
			
			if (types[i] instanceof EnumType) {
				int id = getTypeId(enumMap, types[i]);
				sb.append(id + " },\n");
			}
			else
				sb.append("-1 },\n");
		}
		sb.append("  { 0, NULL, 0 }\n};\n\n");
		
	}
	
	
	/**
	 * Make a matrix that represents the type relation.
	 * @param buf The string buffer to add the code to.
	 * @param typeMap The type map to use.
	 * @param add A string to add to the identifier.
	 */
	protected void makeIsAMatrix(StringBuffer buf, boolean forNode, String add) {
		// since all type id's are given from zero on, the maximum type id
		// (not used) is the number of entries in the type map.
		short[][] matrix = getIsAMatrix(forNode);
		int maxTypeId = matrix.length;
		String matrixName = add + "_is_a_matrix";
		
		buf.append("/** The matrix showing valid type attributes for " + add + ". */\n");
		buf.append("static const char " + add + "_type_is_a_matrix[" + maxTypeId + "]["
								 + maxTypeId + "] = {\n");
		for(int i = 0; i < maxTypeId; i++) {
			buf.append("  { ");
			for(int j = 0; j < maxTypeId; j++) {
				buf.append(j != 0 ? ", " : "");
				//buf.append(matrix[i][j] ? '1' : '0');
				buf.append(matrix[i][j]);
			}
			buf.append(" }, /* ");
			buf.append(getTypeName(forNode, i));
			buf.append(" */\n");
		}
		buf.append("};\n\n");
		buf.append("/** Function to test for type compatibility. */\n");
		buf.append("static inline int ");
		buf.append(add);
		buf.append("_type_is_a(int t1, int t2) {\n");
		buf.append("  return t1 == t2 || " + matrixName + "[t1][t2] != 0;\n}\n\n");
	}
	
	protected void makeSuperSubTypes(StringBuffer sb, boolean forNode, String add) {
		int[] types = getIDs(forNode);
		int maxTypeId = types.length;
		
		sb.append("static const char " + add + "_super_types[" + (maxTypeId + 1) + "]["
								+ maxTypeId + "] = {\n");
		
		for(int i = 0; i < maxTypeId; i++) {
			int[] superTypes = getSuperTypes(forNode, i);
			sb.append("  /* super types of ");
			sb.append(getTypeName(forNode, i));
			sb.append(": ");
			for(int j = 0; j < superTypes.length; j++) {
				sb.append(getTypeName(forNode, superTypes[j]));
				sb.append(" ");
			}
			sb.append(" */\n");
			
			sb.append("  { ");
			for(int j = 0; j < superTypes.length; j++) {
				sb.append(superTypes[j]);
				sb.append(", ");
			}
			sb.append("-1 },\n\n");
		}
		sb.append("};\n\n");
		
		sb.append("static const char " + add + "_sub_types[" + (maxTypeId + 1) + "]["
								+ maxTypeId + "] = {\n");
		for(int i = 0; i < maxTypeId; i++) {
			int[] subTypes = getSubTypes(forNode, i);
			sb.append("  /* sub types of ");
			sb.append(getTypeName(forNode, i));
			sb.append(": ");
			for(int j = 0; j < subTypes.length; j++) {
				sb.append(getTypeName(forNode, subTypes[j]));
				sb.append(" ");
			}
			sb.append(" */\n");

			sb.append("  { ");
			for(int j = 0; j < subTypes.length; j++) {
				sb.append(subTypes[j]);
				sb.append(", ");
			}
			sb.append("-1 },\n\n");
		}
		sb.append("};\n\n");
		
	}
	
	/**
	 * Make the attribute matrix for a given attribute type.
	 *
	 * @param sb      The string buffer to add the code to.
	 * @param add     The matrix prefix.
	 * @param attrMap The map of all attributes.
	 * @param typeMap The type map to use.
	 */
	protected void makeAttrMatrix(StringBuffer sb, String add,
																Map attrMap, Map typeMap) {
		
		int maxTypeId = typeMap.size();
		int maxAttrId = attrMap.size();
		int matrix[][] = new int[maxTypeId][maxAttrId];
		
		for(Iterator it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = (Entity) it.next();
			int attrId = ((Integer) attrMap.get(ent)).intValue();
			int typeId = getTypeId(typeMap, ent.getOwner());
			matrix[typeId][attrId] = 1;
		}
		
		sb.append("static const char " + add + "_attr_matrix[" + maxTypeId + "]["
								+ maxAttrId + "] = {\n");
		
		for(int i = 0; i < maxTypeId; i++) {
			sb.append("  { ");
			for(int j = 0; j < maxAttrId; j++)
				sb.append((j != 0 ? ", " : "") + matrix[i][j]);
			sb.append(" },\n");
		}
		sb.append("};\n");
	}
	
	protected void makeActionMap(StringBuffer sb, Map map) {
		Action[] actions = new Action[map.size()];
		
		for(Iterator it = map.keySet().iterator(); it.hasNext();) {
			Action a = (Action) it.next();
			int index = ((Integer) map.get(a)).intValue();
			actions[index] = a;
		}
		
		sb.append("#define GR_ACTION_VALID(x) ((x) >= 0 && (x) < "
								+ actions.length + ")\n\n");
		
		sb.append("#define GR_ACTIONS " + actions.length + "\n\n");
		
		sb.append("static const action_t action_map[] = {\n");
		for(int i = 0; i < actions.length; i++) {
			Action a = actions[i];
			String kind = "gr_action_kind_test";
			
			if(a instanceof Rule)
				kind = "gr_action_kind_rule";
			
			sb.append("  { " + formatString(a.getIdent().toString()) + ", "
									+ kind + ", 0, 0, NULL, NULL },\n");
		}
		sb.append("  { NULL, -1, 0, 0, NULL, NULL }\n};\n\n");
	}
	
	/**
	 * Generate code for matching actions.
	 * @param sb The string buffer to add the code to.
	 */
	protected void makeActions(StringBuffer sb) {
		for(Iterator it = actionMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = ((Integer) actionMap.get(a)).intValue();
			genMatch(sb, a, id);
			genFinish(sb, a, id);
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
	protected void dumpXMLTag(int depth, StringBuffer sb, String ending, Type inh) {
		
	  for (int i = 0; i < depth; ++i)
			sb.append("  ");
	  sb.append("<" + inh.getName().replace(' ', '_')
								+ " name=\"" + inh.getIdent() + "\"" + ending);
	}
	
	/**
	 * Adds a XML end type tag to the string buffer.
	 *
	 * @param depth indentation depth
	 * @param sb    the string buffer
	 * @param inh   the type
	 */
	protected void dumpXMLEndTag(int depth, StringBuffer sb, Type inh) {
		for (int i = 0; i < depth; ++i)
			sb.append("  ");
		sb.append("</" + inh.getName().replace(' ', '_') + ">\n");
	}
	
	/**
	 * Adds an XML entity tag to the string buffer.
	 *
	 * @param depth  indentation depth
	 * @param sb     the string buffer
	 * @param ending the end of the XML tag, either ">" or "/>"
	 * @param ent    the entity
	 */
	protected void dumpXMLTag(int depth, StringBuffer sb, String ending, Entity ent) {
		for (int i = 0; i < depth; ++i)
			sb.append("  ");
		sb.append("<" + ent.getName().replace(' ', '_')
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
	protected void dumpXMLEndTag(int depth, StringBuffer sb, Entity ent) {
		for (int i = 0; i < depth; ++i)
			sb.append("  ");
		sb.append("</" + ent.getName().replace(' ', '_') + ">\n");
	}
	
	/**
	 * Adds a XML enum value tag to the string buffer.
	 *
	 * @param depth  indentation depth.
	 * @param sb     the string buffer.
	 * @param ending the end of the XML tag, either ">" or "/>".
	 * @param ev     the enum item.
	 */
	protected void dumpXMLTag(int depth, StringBuffer sb, String ending, EnumItem ev) {
		for (int i = 0; i < depth; ++i)
			sb.append("  ");
		sb.append("<" + ev.getName().replace(' ', '_')
								+ " name=\"" + ev + "\" value=\"" + ev.getValue().getValue() + "\"" + ending);
	}
	
	/**
	 * Dump an overview of all declared types, attributes and enums to
	 * an XML file.
	 *
	 * @param sb The string buffer to put the XML stuff to.
	 */
	protected void writeOverview(StringBuffer sb) {
		Map[] maps = new Map[] {
			nodeTypeMap,
				edgeTypeMap
		};
		
		sb.append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
		
		sb.append("<unit>\n");
		
		for(int i = 0; i < maps.length; i++) {
			for(Iterator it = maps[i].keySet().iterator(); it.hasNext();) {
				InheritanceType type = (InheritanceType) it.next();
				dumpXMLTag(1, sb, ">\n", type);
				
				Iterator inhIt = type.getSuperTypes();
				
				if (inhIt.hasNext()) {
					sb.append("    <inherits>\n");
					for(; inhIt.hasNext();) {
						InheritanceType inh = (InheritanceType) inhIt.next();
						dumpXMLTag(3, sb, "/>\n", inh);
					}
					sb.append("    </inherits>\n");
				}
				
				Iterator attrIt = type.getMembers();
				if (attrIt.hasNext()) {
					sb.append("    <attributes>\n");
					for(; attrIt.hasNext();) {
						Entity ent = (Entity) attrIt.next();
						
						dumpXMLTag(3, sb, "/>\n", ent);
					}
					sb.append("    </attributes>\n");
				}
				
				dumpXMLEndTag(1, sb, type);
			}
		}
		
		for(Iterator it = enumMap.keySet().iterator(); it.hasNext();) {
			EnumType type = (EnumType) it.next();
			
			dumpXMLTag(1, sb, ">\n", type);
			Iterator itemIt = type.getItems();
			if (itemIt.hasNext()) {
				sb.append("    <items>\n");
				for(; itemIt.hasNext();) {
					EnumItem ev = (EnumItem) itemIt.next();
					
					dumpXMLTag(3, sb, "/>\n", ev);
				}
				sb.append("    </items>\n");
			}
			
			dumpXMLEndTag(1, sb, type);
		}
		
		sb.append("</unit>\n");
	}
	
	/**
	 * Make some additional C type declarations that must probably be used
	 * in the generated code.
	 * @param sb The string buffer to the stuff to.
	 */
	protected void makeCTypes(StringBuffer sb) {
		sb.append("/** The attribute type classification. */\n");
		sb.append("typedef enum _attribute_type {\n");
		sb.append("  AT_TYPE_INTEGER = " + Type.IS_INTEGER + ", /**< an integer */\n");
		sb.append("  AT_TYPE_BOOLEAN = " + Type.IS_BOOLEAN + ", /**< a boolean */\n");
		sb.append("  AT_TYPE_STRING  = " + Type.IS_STRING + ", /**< a string */\n");
		sb.append("} attribute_type;\n\n");
		
		sb.append("/** The attribute type. */\n");
		sb.append("typedef struct {\n"
								+ "  int type_id;       /**< the ID of attributes type */\n"
								+ "  const char *name;  /**< the name of the attribute */\n"
								+ "  attribute_type at; /**< the attribute type kind */\n"
								+ "  int enum_id;       /**< the Id of the enum type or -1 */\n"
								+ "} attr_t;\n\n");
		
		sb.append("/** The type of an action. */\n");
		sb.append("typedef struct {\n"
								+ "  const char *name;\n"
								+ "  gr_action_kind_t kind;\n"
								+ "  int ins;\n"
								+ "  int outs;\n"
								+ "  const gr_value_kind_t *in_types;\n"
								+ "  const gr_value_kind_t *out_types;\n"
								+ "} action_t;\n\n");
		
		sb.append("/** The type of an enum item declaration. */\n");
		sb.append("typedef struct {\n"
								+ "  const char *name;    /**< the name of the enum item */\n"
								+ "  int value;           /**< the value of the enum item */\n"
								+ "} enum_item_decl_t;\n\n");
		
		sb.append("/** The type of an enum declaration. */\n");
		sb.append("typedef struct {\n"
								+ "  const char *name;    /**< the name of the enum type */\n"
								+ "  int num_items;       /**< the number of items in this enum type */\n"
								+ "  const enum_item_decl_t *items;  /**< the items of this enum type */\n"
								+ "} enum_type_decl_t;\n\n");
		
		sb.append("/** The type of the enum table. */\n");
		sb.append("typedef struct {\n"
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
	protected void makeEnumDeclarations(StringBuffer sb, Map map) {
		// build the description of all enum types
		for(Iterator it = map.keySet().iterator(); it.hasNext();) {
			EnumType type = (EnumType) it.next();
			Ident name = type.getIdent();
			
			sb.append("/** The items for the " + name + " enum type. */\n");
			sb.append("static const enum_item_decl_t _" + name + "_items[] = {\n");
			
			for(Iterator itemIt = type.getItems(); itemIt.hasNext();) {
				EnumItem ev = (EnumItem) itemIt.next();
				
				sb.append(" { \"" + ev + "\", " + ev.getValue().getValue() + " },\n");
			}
			sb.append("};\n\n");
			
			sb.append("/** The declaration of the " + name + " enum type. */\n");
			sb.append("static const enum_type_decl_t " + name + "_decl = {\n"
									+ "  \"" + name + "\",\n"
									+ "  sizeof(_" + name + "_items)/sizeof(_" + name + "_items[0]),\n"
									+ "  _" + name + "_items,\n"
									+ "};\n\n");
		}
		
		// dump all enums to a table
		sb.append("/** All enum types. */\n");
		sb.append("static const enum_types_t enum_types[] = {\n");
		
		String[] names = new String[map.size()];
		for(Iterator it = map.keySet().iterator(); it.hasNext();) {
			EnumType type = (EnumType) it.next();
			int index = getTypeId(map, type);
			
			names[index] = type.getIdent().toString();
		}
		
		for(int i = 0; i < map.size(); ++i) {
			sb.append("  { &" + names[i] + "_decl, " + i + " },\n");
		}
		sb.append("};\n\n");
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
		
		StringBuffer sb;
		String unitName = formatString(unit.getIdent().toString());
		
		// Emit the C types file
		sb = new StringBuffer();
		makeCTypes(sb);
		writeFile("types" + incExtension, sb);
		
		// Emit the type defines.
		sb = new StringBuffer();
		sb.append("/** name of the unit */\n");
		sb.append("#define UNIT_NAME " + unitName + "\n\n");
		
		sb.append("/** type model digest */\n");
		sb.append("#define TYPE_MODEL_DIGEST \"" + unit.getTypeDigest() + "\"\n\n");
		
		makeTypeDefines(sb, nodeTypeMap, "NODE");
		makeTypeDefines(sb, edgeTypeMap, "EDGE");
		makeAttrDefines(sb, nodeAttrMap, "NODE");
		makeAttrDefines(sb, edgeAttrMap, "EDGE");
		makeEnumDefines(sb, enumMap);
		writeFile("graph" + incExtension, sb);
		
		sb = new StringBuffer();
		makeEnumDeclarations(sb, enumMap);
		writeFile("enums" + incExtension, sb);
		
	  // Make the "is a" matrices.
	  sb = new StringBuffer();
	  makeIsAMatrix(sb, true, "node");
	  makeIsAMatrix(sb, false, "edge");
	  writeFile("is_a" + incExtension, sb);
		
		sb = new StringBuffer();
		makeSuperSubTypes(sb, true, "node");
		makeSuperSubTypes(sb, false, "edge");
		writeFile("super_sub_types" + incExtension, sb);
		
		// Make the attribute matrices
		sb = new StringBuffer();
		makeAttrMatrix(sb, "node", nodeAttrMap, nodeTypeMap);
		makeAttrMatrix(sb, "edge", edgeAttrMap, edgeTypeMap);
		writeFile("attr" + incExtension, sb);
		
		// Make arrays with names of the types.
		sb = new StringBuffer();
		makeTypeMap(sb, nodeTypeMap, "node");
		makeTypeMap(sb, edgeTypeMap, "edge");
		makeAttrMap(sb, nodeAttrMap, nodeTypeMap, enumMap, "node");
		makeAttrMap(sb, edgeAttrMap, edgeTypeMap, enumMap, "edge");
		writeFile("names" + incExtension, sb);
		
		sb = new StringBuffer();
		makeActionMap(sb, actionMap);
		writeFile("actions" + incExtension, sb);
		
		sb = new StringBuffer();
		makeActions(sb);
		writeFile("action_impl" + incExtension, sb);
		
		// write an overview of all generated Ids
		sb = new StringBuffer();
		writeOverview(sb);
		writeFile("overview.xml", sb);
		
		// a hook for special generated things
		genExtra();
  }
	
	protected abstract void genMatch(StringBuffer sb, MatchingAction a, int id);
	
	protected abstract void genFinish(StringBuffer sb, MatchingAction a, int id);
	
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
  public String formatString(String s) {
		StringBuffer sb = new StringBuffer();
		sb.append("\"");
		for(int i = 0; i < s.length(); i++) {
			char ch = s.charAt(i);
			switch(ch) {
				case '\"':
					sb.append("\\\"");
					break;
				case '\'':
					sb.append("\\\'");
					break;
				case '\n':
					sb.append("\\n");
					// Ignore the BREAK_LINE, if it is the last character
				  if(i != s.length() - 1)
						sb.append("\" \\\n\"");
					break;
				default:
					sb.append(ch);
			}
		}
		sb.append("\"");
		return sb.toString();
  }
	
}


