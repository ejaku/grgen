/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.Visitor;
import de.unika.ipd.grgen.util.Walkable;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A backend for the C interface to grgen. 
 */
public abstract class CBackend extends Base implements Backend {

  /** The output path as handed over by the frontend. */
	private File path;

  /** The unit to generate code for. */
	protected Unit unit;
	
	/** node type to type id map. (Type -> Integer) */
	protected Map nodeTypeMap = new HashMap();
	
	/** node type to type id map. (Type -> Integer) */
	protected Map edgeTypeMap = new HashMap();
	
	/** node attribute map. (Entity -> Integer) */
	protected Map nodeAttrMap = new HashMap();

	/** node attribute map. (Entity -> Integer) */
	protected Map edgeAttrMap = new HashMap();

	/** enum value map. (Enum -> Integer) */
	protected Map enumMap = new HashMap();

	/** action map. (Action -> Integer) */
	protected Map actionMap = new HashMap();

	/** The error reporter. */
	protected ErrorReporter error;

	/** the extension of the generated include files */
	public final String incExtension = ".inc";
  
  /**
   * Write a string buffer to a file.
   * @param fname The name of the file.
   * @param sb The string buffer to write to the file.
   */
  public void writeFile(String fname, StringBuffer sb) {
    try {
      FileOutputStream fos = 
      	new FileOutputStream(new File(path, fname));
      PrintStream ps = new PrintStream(fos);
      
      ps.print(sb);
      fos.close();
    } catch (FileNotFoundException e) {
      error.error(e.getMessage());
    } catch (IOException e) {
      error.error(e.getMessage());
    }
  }
  
  /**
   * Mangle an identifier.
   * @param id The identifier. 
   * @return A mangled name.
   */
  protected String mangle(Identifiable id) {
  	String clsName = id.getClass().getName();
  	String s = id.getIdent().toString();

		s = s.replaceAll("_", "__");
		s = s.replace('$', '_');
  		
		return s;
  }

	/**
	 * Assign an id to each type in the IR graph.
	 * This method puts all IR object in the IR graph that are instance of
	 * <code>cl</code> into the map <code>typeMap</code> and associates
	 * it with an id that is unique within the map. The id starts with 0.
	 * @param typeMap The type map to fill. 
	 * @param cl The class that an IR object must be instance of, to be put 
	 * into the type map. 
	 */
	protected void makeTypeIds(Map typeMap, Class cl) {
		final Class typeClass = cl;
		final Map map = typeMap;
		 
		/*
		 * This visitor enters each type into a hashmap with 
		 * an id as value, and the type object as key.
		 */
		
		Visitor v = new Visitor() {
			private int id = 0;
						  		 
			public void visit(Walkable w) {
				if(typeClass.isInstance(w)) 
					map.put(w, new Integer(id++));
			}
		};
  	
		(new PostWalker(v)).walk(unit);
	}
	
	/**
	 * Make the attribute Ids.
	 * @param attrMap	A map that will be filled with all attributes and there Ids.
	 * @param cl			The attribute class.
	 */
	protected void makeAttrIds(Map attrMap, Class cl) {
		final Class attrClass = cl;
		final Map map = attrMap;
		
		Visitor v = new Visitor() {
			private int id = 0;
			
			public void visit(Walkable w) {
				if(attrClass.isInstance(w)) {
					CompoundType ty = (CompoundType) w;
					for(Iterator it = ty.getMembers(); it.hasNext();) {
						Entity ent = (Entity) it.next();
						assert !map.containsKey(ent) : "entity must not be in map";
						map.put(ent, new Integer(id++));
					}
				}
			}
		};
		
		(new PostWalker(v)).walk(unit);
	}
	
	/**
	 * Make all enum type Ids.
	 * @param enumMap A map that will be filled with all Enum types and there Ids.
	 */
	protected void makeEnumIds(Map enumMap) {
		final Map map = enumMap;
		
		Visitor v = new Visitor() {
			private int id = 0;
			
			public void visit(Walkable w) {
				if(w instanceof EnumType) {
					map.put(w, new Integer(id++));
				}
			}
		};
		
		(new PostWalker(v)).walk(unit);
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
	
		sb.append("/* Use this macro to check, if an id is a valid type */\n");
		sb.append("#define GR_" + labelAdd + "_TYPE_VALID(t) "
			+ "((t) >= 0 && (t) < " + typeMap.size() + ")\n\n");

		sb.append("/* The number of types defined */\n");
		sb.append("#define GR_" + labelAdd + "_TYPES " + typeMap.size() 
			+ "\n\n");
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			Ident id = ty.getIdent();
			
			sb.append("/* type " + id + " defined at line " 
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

		sb.append("/* Number of attributes macro for " + labelAdd + " */\n");
		sb.append("#define GR_" + labelAdd + "_ATTRS " + attrMap.size() + "\n\n");

		sb.append("/* Attribute valid macro for " + labelAdd + " */\n");
		sb.append("#define GR_" + labelAdd + "_ATTR_VALID(a) " 
			+ "((a) >= 0 && (a) < " + attrMap.size() + ")\n\n");

		for(Iterator it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = (Entity) it.next();
			Ident id = ent.getIdent();
				
			sb.append("/* Attribute " + id + " of "
				+ ent.getOwner().getIdent() + " in line " 
				+ id.getCoords().getLine() + " */\n");
			sb.append("#define GR_DEF_" + labelAdd + "_ATTR_"
				+ mangle(ent.getOwner()) + "_" 
				+ mangle(ent) + " " + attrMap.get(ent) + "\n\n"); 
		}
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
	 * @param add A string to add to the identifier of the map.
	 */
	protected void makeAttrMap(StringBuffer sb, Map attrMap, 
		Map typeMap, String add) {
		
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
			sb.append("  { " + owner[i] + ", " + formatString(name[i]) + ", " + types[i].classify() + " },\n");
		}
		sb.append("  { 0, NULL, 0 }\n};\n\n");
		
	}
	
	/**
	 * Gets a set with all types the given type is compatible with.
	 * (It builds the transitive closure over the subtype relation.)
	 * @param ty The type to determine all compatible types for.
	 * @param isaMap A temporary map, where this method can record data.
	 * @return A set containing all compatible types to <code>ty</code>.
	 */
	protected Set getIsA(InheritanceType ty, Map isaMap) {
		Set res; 
		
		if(!isaMap.containsKey(ty)) {
			res = new HashSet();
			isaMap.put(ty, res);
			for(Iterator it = ty.getInherits(); it.hasNext();) {
				InheritanceType t = (InheritanceType) it.next();
				res.add(t);
				res.addAll(getIsA(t, isaMap));				
			}
		} else
			res = (Set) isaMap.get(ty);
			
		return res;
	}
	
	/**
	 * Make a matrix that represents the type relation.
	 * @param buf The string buffer to add the code to.
	 * @param typeMap The type map to use.
	 * @param add A string to add to the identifier.
	 */
	protected void makeIsAMatrix(StringBuffer buf, Map typeMap, String add) {
		Map isaMap = new HashMap();
		
		// since all type id's are given from zero on, the maximum type id
		// (not used) is the number of entries in the type map. 
		int maxTypeId = typeMap.size();
		
		int[][] matrix = new int[maxTypeId][maxTypeId];
		
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			int tid = getTypeId(typeMap, ty);
			Set isa = getIsA(ty, isaMap);
			
			for(Iterator i = isa.iterator(); i.hasNext();) {
				int isaTid = getTypeId(typeMap, (InheritanceType) i.next());
				assert isaTid < maxTypeId : "wrong type id: " + isaTid;
				matrix[tid][isaTid] = 1;		
			}
			
			matrix[tid][tid] = 1;
		}
		
		buf.append("/** The matrix showing valid type attributes for " + add + " */\n");
		buf.append("static char " + add + "_is_a_matrix[" + maxTypeId + "]["
			+ maxTypeId + "] = {\n");
		for(int i = 0; i < maxTypeId; i++) {
			buf.append("  { ");
			for(int j = 0; j < maxTypeId; j++) 
				buf.append((j != 0 ? ", " : "") + matrix[i][j]);
			buf.append(" },\n"); 
		}
		buf.append("};\n\n");
	}
	
	/**
	 * Make the attribute matrix for a given attribute type.
	 * 
	 * @param sb      The string buffer to add the code to.
	 * @param add     The mattrix prefix.
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
	
  /**
   * @param map The map to look into.
   * @param ty The inheritance type to get the id for.
   * @return The type id for this type.
   */
  protected int getTypeId(Map map, IR obj) {
		Integer res = (Integer) map.get(obj);
    return res.intValue();
  }

	protected void makeActionIds(Map actionMap) {
		final Map map = actionMap;
		
		Visitor v = new Visitor() {
			private int id = 0;
			
			public void visit(Walkable w) {
				if(w instanceof Action) 
					map.put(w, new Integer(id++));					
			}
		};
		
		(new PostWalker(v)).walk(unit);
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
	 * Adds a XML enumvalue tag to the string buffer.
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
				
				Iterator inhIt = type.getInherits();
				
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
		sb.append("  AT_TYPE_INTEGER = " + Type.IS_INTEGER + ", /**< an integer or enum */\n");
		sb.append("  AT_TYPE_BOOLEAN = " + Type.IS_BOOLEAN + ", /**< a boolean */\n");
		sb.append("  AT_TYPE_STRING  = " + Type.IS_STRING + ", /**< a string */\n");
		sb.append("} attribute_type;\n\n");
		
		sb.append("/** The attribute type. */\n");
		sb.append("typedef struct {\n"
			+ "  int type_id;       /**< the ID of attributes type */\n"
			+ "  const char *name;  /**< the name of the attribute */\n"
			+ "  attribute_type at; /**< the attribute type kind */\n"
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
	}

  /**
   * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter)
   */
  public void init(Unit unit, ErrorReporter reporter, String outputPath) {
    this.unit = unit;
    this.error = reporter;
    this.path = new File(outputPath);
    path.mkdirs();
    
		makeTypeIds(nodeTypeMap,  NodeType.class);
		makeTypeIds(edgeTypeMap,  EdgeType.class);
		makeAttrIds(nodeAttrMap,  NodeType.class);
		makeAttrIds(edgeAttrMap,  EdgeType.class);
		makeEnumIds(enumMap);
		makeActionIds(actionMap);
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
  	sb.append("/* name of the unit */\n");
  	sb.append("#define UNIT_NAME " + unitName + "\n\n");
		makeTypeDefines(sb, nodeTypeMap, "NODE");
		makeTypeDefines(sb, edgeTypeMap, "EDGE");
		makeAttrDefines(sb, nodeAttrMap, "NODE");
		makeAttrDefines(sb, edgeAttrMap, "EDGE");
//		makeEnumDefines(sb, enumMap, "ENUM");
		writeFile("graph" + incExtension, sb);

		// Make the "is a" matrix.
		sb = new StringBuffer();		
		makeIsAMatrix(sb, nodeTypeMap, "node");
		makeIsAMatrix(sb, edgeTypeMap, "edge");
		writeFile("is_a" + incExtension, sb);

		sb = new StringBuffer();
		makeAttrMatrix(sb, "node", nodeAttrMap, nodeTypeMap);
		makeAttrMatrix(sb, "edge", edgeAttrMap, edgeTypeMap);
		writeFile("attr" + incExtension, sb);
		
		// Make arrays with names of the types.
		sb = new StringBuffer();				
		makeTypeMap(sb, nodeTypeMap, "node");
		makeTypeMap(sb, edgeTypeMap, "edge");
		makeAttrMap(sb, nodeAttrMap, nodeTypeMap, "node");
		makeAttrMap(sb, edgeAttrMap, edgeTypeMap, "edge");
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

	public static final char BREAK_LINE = '\f';

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
		for(int i = 0, j = 0; i < s.length(); i++) {
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
					break;
				case '\f':
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
