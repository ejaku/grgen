/**
 * A GrGen Backend which generates C code for a frame-based
 * graph model and a frame based graph matcher
 * @author Veit Batz
 * @version 
 */
package de.unika.ipd.grgen.be.C;

import java.io.File;
import java.util.Iterator;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.util.Util;
import java.util.Vector;

public class FrameBasedBackend extends IDBase implements Backend {

	// The unit to generate code for.
	protected Unit unit;
	// keine Ahnung wozu das gut sein soll
	protected Sys system;
	// The output path as handed over by the frontend.
	private File path;

	
	
	/**
	 * Initializes the FrameBasedBackend 
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.Sys, java.io.File)
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		this.unit = unit;
		this.path = outputPath;
		this.system = system; 
	}
		
	/**
	 * Starts the C-code Genration of the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {

		// the StringBuffer the generated C code to be stored in
		StringBuffer sb = new StringBuffer();
		
		// first: Generate the graph type information
		makeTypes(unit);
		genGraphTypeInfo(sb);
		// write StrinBuffer to file
		writeFile("generated_graph_type_info.inc", sb);
		
	}

	/**
	 * Finishes FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#done()
	 */
	public void done() {
		/* nothing to do yet */
	}
	
	
	
	
	/* generates the graph type information specified by the grg-file,
	 * that is info about edge and node classes and their attributes
	 * and their inheritance relation
	 */
	private void genGraphTypeInfo(StringBuffer sb) {
		//gen the file preamble
		sb.append(
				"/*\n" +
				" * File 'generated_graph_type_info.inc', created automatically for\n" +
				" * the FrameBased-Backend by the GrGen class 'FrameBasedBackend'.\n" +
				" *\n" +
				" * The data structures initialized in this file represent the type information" +
				" * as specified by the GrGen input file '" + unit.getFilename() + "'.\n" +
				" */\n\n");

		
		//gen a define: the name of the unit  
		sb.append(
				"/* the name of the GrGen unit this file is created from */\n" +
				"#define fb_UNIT_NAME \"" + unit.getName() + "\"\n\n");

		
		//gen three defines: the number of node- and edge-types
		int n_node_types = getIDs(true).length; 
		int n_edge_types = getIDs(false).length;
		int n_enum_types = enumMap.size(); 
		int n_node_attrs = nodeAttrMap.size();
		int n_edge_attrs = edgeAttrMap.size();
		sb.append(
				"/* the overall number of the edge-, node-, enum-types and of the\n" +
				" * declared node- and edge attributes */\n" +
				"#define fb_n_node_types " + n_node_types + "\n" +
				"#define fb_n_edge_types " + n_edge_types + "\n" +
				"#define fb_n_ednum_types " + n_enum_types + "\n" +
				"#define fb_n_node_attr_decls " + n_node_attrs + "\n" +
				"#define fb_n_edge_attr_decls " + n_edge_attrs + "\n\n");
		
				
		/* gen the array representing the node-subtype relation... */
		//get the inheritance information from the GrGen internal IR
		short[][] node_is_a_matrix = getIsAMatrix(true);
		short[][] edge_is_a_matrix = getIsAMatrix(false);
		if (n_node_types > 0) {
			sb.append(
				"/* Two arrays representing the node/edge-subtype relation:\n" +
				" * Example:  t1 'is_a' t2   IFF   array[t1][t1] == k != 0\n" +
				" * k shows the distance of t1 and t2  in the type hierarchie,\n" +
				" * i.e. k == 1 means, that t2 is a DIRECT supertype.\n" +
				" * ATTENTION: k == 0 means, that t1 is NOT a subtype of t2 !!!\n" +
				" */\n" +
				"const int fb_node_sub_type[fb_n_node_types][fb_n_node_types] = {\n");
			//gen the node-sub-type array content: go over the rows
			for (int row=0; row < n_node_types; row++) {
				//go over the columns: result col will be e.g. { 0,0,1,0,4,0 }
				//if there are n entries in a row, then there are only n-1 ',' chars:
				sb.append("  { " + node_is_a_matrix[0][0]);
				for (int col=1; col < n_node_types; col++)
					sb.append("," + node_is_a_matrix[row][col]);
				sb.append(" }\n");
			}
			sb.append("};\n");
		}
		else {
			sb.append(
					"/* Two arrays representing the node/edge-subtype relation:\n" +
					" * Example:  t1 'is_a' t2   IFF   array[t1][t1] == k != 0\n" +
					" * k shows the distance of t1 and t2  in the type hierarchie,\n" +
					" * i.e. k == 1 means, that t2 is a DIRECT supertype.\n" +
					" * ATTENTION: k == 0 means, that t1 is NOT a subtype of t2 !!!\n" +
					" */\n" +
					"const int fb_node_sub_type[0][0] = {};\n\n");
		}
		if (n_edge_types > 0) {
			/* ...and gen the array representing the edge-subtype relation */
			sb.append(
				"const int fb_edge_sub_type[fb_n_edge_types][fb_n_edge_types] = {\n");
			//gen the edge-sub-type array content: go over the rows
			for (int row=0; row < n_edge_types; row++) {
				//go over the columns: result col will be e.g. { 0,0,1,0,4,0 }
				//if there are n entries in a row, then there are only n-1 ',' chars: 
				sb.append("  { " + edge_is_a_matrix[0][0]);
				for (int col=1; col < n_edge_types; col++)
					sb.append("," + edge_is_a_matrix[row][col]);
				sb.append(" }\n");
			}
			sb.append("};\n\n");
		}
		else {
			sb.append(
			"const int fb_edge_sub_type[0][0] = {};\n\n");
		}

		
		
		/* gen an array, which maps node-type-ids on node-type names */
		if (n_node_types > 0) {
			sb.append(
				"/* An array mapping a node type id to the name of that node type */\n" +
				"const char *fb_name_of_node_type[fb_n_node_types] = {\n" +
				"  \"" + getTypeName(true, 0) + "\"");
			for (int nt=1; nt < n_node_types; nt++)
				sb.append(",\n  \"" + getTypeName(true, nt) + "\"");
			sb.append("\n};\n\n");
		}
		else {
			sb.append(
					"/* An array mapping a node type id to the name of that node type */\n" +
					"const char *fb_name_of_node_type[0] = {};\n\n");
		}
		if (n_edge_types > 0) {
			/* gen an array, which maps edge-type-ids on edge-type names */
			sb.append(
				"/* An array mapping a edge type id to the name of that edge type */\n" +
				"const char *fb_name_of_edge_type[fb_n_edge_types] = {\n" +
				"  \"" + getTypeName(false, 0) + "\"");
			for (int et=1; et < n_edge_types; et++)
				sb.append(",\n  \"" + getTypeName(false, et) + "\"");
			sb.append("\n};\n\n");
		}
		else {
			sb.append(
					"/* An array mapping a edge type id to the name of that edge type */\n" +
					"const char *fb_name_of_edge_type[0] = {};\n\n");
			
		}
		
		/* gen an array, which maps a node-type-id to the number of attr the type has */
		//count the number of attr for each node type and store the result in an array
		int n_attr_of_node_type[] = new int[n_node_types];
		if (n_node_types > 0) {
			sb.append(
				"/* An array mapping a node type id to the number of attributes that type has */\n" +
				"const char *fb_n_attr_of_node_type[fb_n_node_types] = {\n");
			//fill that array with 0
			for (int i=0; i < n_node_types; i++) n_attr_of_node_type[i] = 0;
			//count number of attributes
			for (Iterator it =  nodeAttrMap.keySet().iterator(); it.hasNext(); ) {
				Entity attr = (Entity) it.next();
				assert attr.hasOwner():
					"Thought, that the Entity represented a node class attr and that\n" +
					"thus there had to be a type that owned the entity, but there was non.";
				Type node_type = attr.getOwner();
				int node_type_id = ((Integer)nodeTypeMap.get(node_type)).intValue();
				assert node_type_id < n_node_types:
					"Tried to use a node-type-id as array index, " +
					"but the id exceeded the number of node types";
				n_attr_of_node_type[node_type_id]++;
			}
			//transfer the counting result from the array to the generated array
			sb.append("  " + n_attr_of_node_type[0]);
			for (int nt=1; nt < n_node_types; nt++)
				sb.append("," + n_attr_of_node_type[nt]);
			sb.append("\n};\n\n");
		}
		else {
			sb.append(
					"/* An array mapping a node type id to the number of attributes that type has */\n" +
					"const char *fb_n_attr_of_node_type[0] = {};\n\n");
		}

		/* gen an array, which maps a edge-type-id to the number of attr the type has */
		//count the number of attr for each edge type and store the result in an array
		int n_attr_of_edge_type[] = new int[n_edge_types];
		if (n_edge_types > 0) {
			sb.append(
				"/* An array mapping an edge type id to the number of attributes that type has */\n" +
				"const char *fb_n_attr_of_edge_type[fb_n_edge_types] = {\n");
			//fill that array with 0
			for (int i=0; i < n_edge_types; i++) n_attr_of_edge_type[i] = 0;
			//count number of attributes
			for (Iterator it =  edgeAttrMap.keySet().iterator(); it.hasNext(); ) {
				Entity attr = (Entity) it.next();
				assert attr.hasOwner():
					"Thought, that the Entity represented an edge class attr and that\n" +
					"thus there had to be a type that owned the entity, but there was non.";
				Type edge_type = attr.getOwner();
				int edge_type_id = ((Integer)edgeTypeMap.get(edge_type)).intValue();
				assert edge_type_id < n_edge_types:
					"Tried to use an edge-type-id as array index," +
					"but the id exceeded the number of edge types";
				n_attr_of_edge_type[edge_type_id]++;
			}
			//transfer the counting result from the array to the generated array
			sb.append("  " + n_attr_of_edge_type[0]);
			for (int et=1; et < n_edge_types; et++)
				sb.append("," + n_attr_of_edge_type[et]);
			sb.append("\n};\n\n");
		}
		else {
			sb.append(
					"/* An array mapping a node type id to the number of attributes that type has */\n" +
					"const char *fb_n_attr_of_edge_type[0] = {};\n\n");
		}


		
		/* gen an array which maps node attribute ids to attribute descriptors */
		AttrTypeDescriptor[] node_attr_info = new AttrTypeDescriptor[0];
		if (n_node_attrs > 0) {
			sb.append(
					"/* An array mapping node attribute ids to attribute descriptors */\n" +
					"const fb_attr_descr_t fb_node_attr_info[fb_n_node_attr_decls] = {\n");
			/* first: collect all needed information about node attributes */
			node_attr_info = new AttrTypeDescriptor[n_node_attrs];

			for (Iterator it = nodeAttrMap.keySet().iterator(); it.hasNext(); ) {
				Entity attr = (Entity) it.next();
				assert attr.hasOwner():
					"Thought, that the Entity represented an node attr and that thus\n" +
					"there had to be a type that owned the entity, but there was non.";
				NodeType node_type = (NodeType) attr.getOwner();
				int node_type_id = ((Integer)nodeTypeMap.get(node_type)).intValue();
				int attr_id = ((Integer)nodeAttrMap.get(attr)).intValue();

				node_attr_info[attr_id] = new AttrTypeDescriptor();
				//set the attr id
				node_attr_info[attr_id].attr_id = attr_id;
				//get the attributes name
				node_attr_info[attr_id].name = attr.getIdent().toString();
				//get the owners type id
				node_attr_info[attr_id].decl_owner_type_id = node_type_id; 
				//get the attributes kind
				if (attr.getType() instanceof IntType)
					node_attr_info[attr_id].kind = AttrTypeDescriptor.INTEGER;
				else if (attr.getType() instanceof BooleanType)
					node_attr_info[attr_id].kind= AttrTypeDescriptor.BOOLEAN;
				else if (attr.getType() instanceof StringType)
					node_attr_info[attr_id].kind = AttrTypeDescriptor.STRING;
				else if (attr.getType() instanceof EnumType) {
					node_attr_info[attr_id].kind = AttrTypeDescriptor.ENUM;
					node_attr_info[attr_id].enum_id = ((Integer)enumMap.get(attr.getType())).intValue();
				}
				else {
					System.err.println("Key element of AttrNodeMap has a type, which is " +
							"neither one of 'int', 'boolean', 'string' nor an enumeration type.");
					System.exit(0);
				}
			}
			AttrTypeDescriptor at = node_attr_info[0];
			sb.append(
					"  { " + genFbKindFromInt(at.kind) + ",(gr_id_t)" + at.attr_id + "," +
					"\"" + at.name + "\",(gr_id_t)" + at.decl_owner_type_id + "," +
					"(gr_id_t)" + at.enum_id +" }");
			for (int attr_id=1; attr_id < n_node_attrs; attr_id++) {
				at = node_attr_info[attr_id]; 
				sb.append(
						",\n  { " + genFbKindFromInt(at.kind) + ",(gr_id_t)" + at.attr_id + "," +
						"\"" + at.name + "\",(gr_id_t)" + at.decl_owner_type_id + "," +
						"(gr_id_t)" + at.enum_id +" }");
			}
			sb.append("\n};\n\n");
		}
		else {
			sb.append(
					"/* An array mapping node attribute ids to attribute descriptors */\n" +
					"const fb_attr_descr_t fb_node_attr_info[0] = {};\n\n");
		}
		
		
		
		/* gen an array which maps edge attribute ids to attribute descriptors */
		AttrTypeDescriptor[] edge_attr_info = new AttrTypeDescriptor[0];
		if (n_edge_attrs > 0) {
			sb.append(
					"/* An array mapping edge attribute ids to attribute descriptors */\n" +
					"const fb_attr_descr_t fb_edge_attr_info[fb_n_edge_attr_decls] = {\n");
			/* first: collect all needed information about node attributes */
			edge_attr_info = new AttrTypeDescriptor[n_edge_attrs];

			for (Iterator it = edgeAttrMap.keySet().iterator(); it.hasNext(); ) {
				Entity attr = (Entity) it.next();
				assert attr.hasOwner():
					"Thought, that the Entity represented an edge attr and that thus\n" +
					"there had to be a type that owned the entity, but there was non.";
				EdgeType edge_type = (EdgeType) attr.getOwner();
				int edge_type_id = ((Integer)edgeTypeMap.get(edge_type)).intValue();
				int attr_id = ((Integer)edgeAttrMap.get(attr)).intValue();

				edge_attr_info[attr_id] = new AttrTypeDescriptor();
				//set the attr id
				edge_attr_info[attr_id].attr_id = attr_id;
				//get the attributes name
				edge_attr_info[attr_id].name = attr.getIdent().toString();
				//get the owners type id
				edge_attr_info[attr_id].decl_owner_type_id = edge_type_id; 
				//get the attributes kind
				if (attr.getType() instanceof IntType)
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.INTEGER;
				else if (attr.getType() instanceof BooleanType)
					edge_attr_info[attr_id].kind= AttrTypeDescriptor.BOOLEAN;
				else if (attr.getType() instanceof StringType)
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.STRING;
				else if (attr.getType() instanceof EnumType) {
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.ENUM;
					edge_attr_info[attr_id].enum_id = ((Integer)enumMap.get(attr.getType())).intValue();
				}
				else {
					System.err.println("Key element of AttrEdgeMap has a type, which is " +
							"neither one of 'int', 'boolean', 'string' nor an enumeration type.");
					System.exit(0);
				}
			}
			AttrTypeDescriptor at = edge_attr_info[0];
			sb.append(
					"  { " + genFbKindFromInt(at.kind) + ",(gr_id_t)" + at.attr_id + "," +
					"\"" + at.name + "\",(gr_id_t)" + at.decl_owner_type_id + "," +
					"(gr_id_t)" + at.enum_id +" }");
			for (int attr_id=1; attr_id < n_edge_attrs; attr_id++) {
				at =edge_attr_info[attr_id]; 
				sb.append(
						",\n  { " + genFbKindFromInt(at.kind) + ",(gr_id_t)" + at.attr_id + "," +
						"\"" + at.name + "\",(gr_id_t)" + at.decl_owner_type_id + "," +
						"(gr_id_t)" + at.enum_id +" }");
			}
			sb.append("\n};\n\n");
		}
		else {
			sb.append(
					"/* An array mapping edge attribute ids to attribute descriptors */\n" +
					"const fb_attr_descr_t fb_edge_attr_info[0] = {};\n\n");
		}
		

		
		/* gen an array which implements the map
		 * node_type_id  x  attr_id  -->  attr_index
		 * where attr index is the position the attr has in the attr layout of the node type */
		//first: compute the attr layout of the node types (then you can gen the array)
		int node_attr_index[][] = new int[0][0];
		if (n_node_attrs > 0) {
			node_attr_index = new int[n_node_types][n_node_attrs];
			//for all node types...
			for (int nt = 0; nt < n_node_types; nt++) {
				//the index the current attr will get in the current node layout, if it's a member
				int attr_index = 0; 
				//...and all node attribute IDs...
				for (int attr_id = 0; attr_id < n_node_attrs; attr_id++) {
					//...check wether the attr is owned by the node type or one of its supertype
					int owner = node_attr_info[attr_id].decl_owner_type_id;
					if ( owner == nt || node_is_a_matrix[nt][owner] > 0)
						//setup the attrs index in the layout of the current node type
						node_attr_index[nt][attr_id] = attr_index++;
					else
						//-1 means that the current attr is not a member of the current node type 
						node_attr_index[nt][attr_id] = -1;
				}
			}
			//now gen the array
			sb.append(
					"/* An array mapping pairs (node_type_id, node_attr_id) to the index position\n" +
					" * the attribute has in the layout of the node type. A negative value indicates\n" +
					" * that the attr is NOT a member of a node type. */\n" +
					"const int fb_node_attr_index[fb_n_node_types][fb_n_node_attr_decls] = {\n");
			for (int nt=0; nt < n_node_types; nt++) {
				if (nt == 0) sb.append("  { ");
				else sb.append(",\n  { ");
				for (int attr = 0; attr < n_node_attrs; attr++) {
					if (attr == 0) sb.append(node_attr_index[nt][attr]);
					else sb.append("," + node_attr_index[nt][attr]);
				}
				sb.append(" }");
			}
			sb.append("\n};\n\n");
		}
		else
			sb.append(
					"/* An array mapping pairs (node_type_id, node_attr_id) to the index position\n" +
					" * the attribute has in the layout of the node type. A negative value indicates\n" +
					" * that the attr is NOT a member of a node type. */\n" +
					"const int fb_node_attr_index[0][0] = {};\n\n");



		/* gen an array which implements the map
		 * edge_type_id  x  attr_id  -->  attr_index
		 * where attr index is the position the attr has in the attr layout of the edge type */
		//first: compute the attr layout of the edge types (then you can gen the array)
		int edge_attr_index[][] = new int[0][0];
		if (n_edge_attrs > 0) {
			edge_attr_index = new int[n_edge_types][n_edge_attrs];
			//for all edge types...
			for (int et = 0; et < n_edge_types; et++) {
				//the index the current attr will get in the current edge layout, if it's a member
				int attr_index = 0; 
				//...and all edge attribute IDs...
				for (int attr_id = 0; attr_id < n_edge_attrs; attr_id++) {
					//...check wether the attr is owned by the edge type or one of its supertype
					int owner = edge_attr_info[attr_id].decl_owner_type_id;
					if ( owner == et || edge_is_a_matrix[et][owner] > 0)
						//setup the attrs index in the layout of the current node type
						node_attr_index[et][attr_id] = attr_index++;
					else
						//-1 means that the current attr is not a member of the current node type 
						node_attr_index[et][attr_id] = -1;
				}
			}
			//now gen the array
			sb.append(
					"/* An array mapping pairs (edge_type_id, edge_attr_id) to the index position\n" +
					" * the attribute has in the layout of the edge type. A negative value indicates\n" +
					" * that the attr is NOT a member of an edge type. */\n" +
					"const int fb_edge_attr_index[fb_n_edge_types][fb_n_edge_attr_decls] = {\n");
			for (int et=0; et < n_edge_types; et++) {
				if (et == 0) sb.append("  { ");
				else sb.append(",\n  { ");
				for (int attr = 0; attr < n_edge_attrs; attr++) {
					if (attr == 0) sb.append(edge_attr_index[et][attr]);
					else sb.append("," + edge_attr_index[et][attr]);
				}
				sb.append(" }");
			}
			sb.append("\n};\n\n");
		}
		else
			sb.append(
					"/* An array mapping pairs (edge_type_id, edge_attr_id) to the index position\n" +
					" * the attribute has in the layout of the edge type. A negative value indicates\n" +
					" * that the attr is NOT a member of an edge type. */\n" +
					"const int fb_edge_attr_index[0][0] = {};\n\n");

		

		//TODO fb_enum_type_info[enum_type_id] erzeugen
		EnumDescriptor enum_type_descriptors[] = new EnumDescriptor[0];
		if (n_enum_types > 0) {
			//collect the information about the enumeration types
			enum_type_descriptors = new EnumDescriptor[n_enum_types];
			for (Iterator it = enumMap.keySet().iterator(); it.hasNext(); ) {
				EnumType enum_type = (EnumType) it.next();
				//store the info about the current enum type in an array...
				//...type id
				int enum_type_id = ((Integer)enumMap.get(enum_type)).intValue();
				enum_type_descriptors[enum_type_id].type_id = enum_type_id;
				//...the identifier used in the grg-file to declare thar enum type
				enum_type_descriptors[enum_type_id].name = enum_type.getIdent().toString();
				//..the items in this enumeration type
				for (Iterator item_it = enum_type.getItems(); item_it.hasNext(); ) {
					enum_type_descriptors[enum_type_id].items.add(item_it.next());
				}
				//...the number of items
				enum_type_descriptors[enum_type_id].n_items =
											enum_type_descriptors[enum_type_id].items.size();
			}
			//actual gen
			sb.append(
					"/* An array mapping enum type ids to enum type descriptors */\n" +
					"const fb_enum_descr_t fb_enum_type_info[fb_n_enum_type] = {\n");
			boolean first1 = true;
			for (int enum_type_id = 0; enum_type_id < n_enum_types; enum_type_id++) {
				if (!first1) sb.append(",\n");
				first1 = false;
				EnumDescriptor current_enum = enum_type_descriptors[enum_type_id];
				sb.append("  { (gr_id_t)" + current_enum.type_id + ",\"" +
						current_enum.name + "\"," + current_enum.n_items + ", { ");
				boolean first2 = true;
				for (Iterator item_it = current_enum.items.iterator(); item_it.hasNext(); ) {
					if (!first2) sb.append(",");
					first2 = false;
					EnumItem enum_item = (EnumItem) item_it.next();
					sb.append(
							"{(gr_int_t)" + ((Integer)enum_item.getValue().getValue()).intValue() + 
							"," + enum_item.getIdent() + "}");
				}
				sb.append(" } }");
			}
			sb.append("\n};\n\n");
		}
		else
			sb.append(
					"/* An array mapping enum type ids to enum type descriptors */\n" +
					"const fb_enum_descr_t fb_enum_type_info[0] = {};\n\n\n");
	}



	/*
	 *  gives a kind tag for a fb backend attr descriptor
	 */
	protected String genFbKindFromInt(int kind) {
		switch (kind) {
			case AttrTypeDescriptor.INTEGER:
				return "fb_kind_prim_int";
			case AttrTypeDescriptor.BOOLEAN:
				return "fb_kind_prim_boolean";
			case AttrTypeDescriptor.STRING:
				return "fb_kind_prim_string";
			case AttrTypeDescriptor.ENUM:
				return "fb_kind_enum";
		}
		return "ERROR_invalid_kind";
	}
	

	/*
	 *  writes a character sequence to a new file
	 */
 	protected void writeFile(String filename, CharSequence cs) {
		Util.writeFile(new File(path, filename), cs, error);
	}
	
	/*
	 *  auxilary inner class, wrapping attr type information
	 */
  class AttrTypeDescriptor {
  	int kind;								//0: integer, 1: boolean, 2: string, 3: enum
  	int attr_id;						//this attributes id
		String name;						//the attr identifier used in the '.grg' file
		int decl_owner_type_id;	//the id of the type owning this attr 
		int enum_id = -1;				//the id of the enum type (if the attr IS of an enum type)
		
		static final int INTEGER = 0;
		static final int BOOLEAN = 1;
		static final int STRING = 2;
		static final int ENUM = 3;
  }
  
  class EnumDescriptor {
      int  type_id;
      String name; 
      int n_items; 
      Vector items = new Vector();
  }
}

