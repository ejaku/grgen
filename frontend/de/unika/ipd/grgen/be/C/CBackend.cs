/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.be.C
{

	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	using Sys = de.unika.ipd.grgen.Sys;
	using Backend = de.unika.ipd.grgen.be.Backend;
	using IDBase = de.unika.ipd.grgen.be.IDBase;
	using de.unika.ipd.grgen.ir;
	using Action = de.unika.ipd.grgen.ir.executable.Action;
	using MatchingAction = de.unika.ipd.grgen.ir.executable.MatchingAction;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using ConnAssert = de.unika.ipd.grgen.ir.model.ConnAssert;
	using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using TypeClass = de.unika.ipd.grgen.ir.type.Type.TypeClass;
	using Util = de.unika.ipd.grgen.util.Util;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	/// <summary>
	/// A backend for the C interface to grgen.
	/// </summary>
	public abstract class CBackend : IDBase, Backend
	{
		/// <summary>
		/// The unit to generate code for. </summary>
		protected internal Unit unit;

		/// <summary>
		/// The output path as handed over by the frontend. </summary>
		private File path;

		/// <summary>
		/// the extension of the generated include files </summary>
		public readonly string incExtension = ".inc";

		/// <summary>
		/// The error reporter. </summary>
		protected internal new ErrorReporter error;

		/// <summary>
		/// Get the IR root node. </summary>
		/// <returns> The Unit node of the IR. </returns>
		protected internal virtual Unit Unit
		{
			get
			{
				return unit;
			}
		}

		/// <summary>
		/// Mangle an identifier. </summary>
		/// <param name="id"> The identifier. </param>
		/// <returns> A mangled name. </returns>
		protected internal static string Mangle(Identifiable id)
		{
			string s = id.Ident.ToString();

			s = s.ReplaceAll("_", "__");
			s = s.Replace('$', '_');

			return s;
		}

		/// <summary>
		/// Write a character sequence to a file using the path set. </summary>
		/// <param name="filename"> The filename. </param>
		/// <param name="cs"> A character sequence. </param>
		protected internal void WriteFile(string filename, CharSequence cs)
		{
			Util.WriteFile(new File(path, filename), cs, error);
		}

		protected internal PrintStream OpenFile(string filename)
		{
			return Util.OpenFile(new File(path, filename), error);
		}

		protected internal static void CloseFile(PrintStream ps)
		{
			Util.CloseFile(ps);
		}

		/// <summary>
		/// Make C defines for each type in a type map.
		/// This method makes defines like<br>
		/// #define GR_<code>labelAdd</code>_TYPE_<code>typename<InheritanceType/code> </summary>
		/// <param name="sb"> The string buffer to add to. </param>
		/// <param name="typeMap"> The type map containing the types to dump. </param>
		/// <param name="labelAdd"> The string that should be added to the define. </param>
		protected internal static void MakeTypeDefines<T1>(PrintStream ps, IDictionary<T1, int> typeMap, string labelAdd) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			ps.Print("/** Use this macro to check, if an id is a valid type */\n");
			ps.Print("#define GR_" + labelAdd + "_TYPE_VALID(t) "
					+ "((t) >= 0 && (t) < " + typeMap.Count + ")\n\n");

			ps.Print("/** The number of types defined */\n");
			ps.Print("#define GR_" + labelAdd + "_TYPES " + typeMap.Count + "\n\n");
			foreach(InheritanceType ty in typeMap.Keys)
			{
				Ident id = ty.Ident;

				ps.Print("/** type " + id + " defined at line " + id.Coords.Line + " */\n");
				ps.Print("#define GR_DEF_" + labelAdd + "_TYPE_" + Mangle(ty) + " " + typeMap[ty] + "\n\n");
			}
		}

		/// <summary>
		/// Make defines for attribute IDs. </summary>
		/// <param name="sb"> The string buffer to add the code to. </param>
		/// <param name="attrMap"> The attribute map to use. </param>
		/// <param name="labelAdd"> The string to add to the define's name. </param>
		protected internal static void MakeAttrDefines(PrintStream ps, IDictionary<Entity, int> attrMap, string labelAdd)
		{
			ps.Print("/** Number of attributes macro for " + labelAdd + " */\n");
			ps.Print("#define GR_" + labelAdd + "_ATTRS " + attrMap.Count + "\n\n");

			ps.Print("/** Attribute valid macro for " + labelAdd + " */\n");
			ps.Print("#define GR_" + labelAdd + "_ATTR_VALID(a) " + "((a) >= 0 && (a) < " + attrMap.Count + ")\n\n");

			foreach(Entity ent in attrMap.Keys)
			{
				Ident id = ent.Ident;

				ps.Print("/** Attribute " + id + " of " + ent.Owner.Ident + " in line " + id.Coords.Line + " */\n");
				ps.Print("#define GR_DEF_" + labelAdd + "_ATTR_" + Mangle(ent.Owner) + "_" + Mangle(ent) + " " + attrMap[ent] + "\n\n");
			}
		}

		/// <summary>
		/// Make defines for enum types. </summary>
		/// <param name="sb"> The string buffer to add the code to. </param>
		/// <param name="map"> The enum type map. </param>
		protected internal static void MakeEnumDefines(PrintStream ps, IDictionary<EnumType, int> map)
		{
			ps.Print("/** Number of enum types. */\n");
			ps.Print("#define GR_DEF_ENUMS " + map.Count + "\n\n");

			ps.Print("/** Use this macro to check, if an id is a valid enum type */\n");
			ps.Print("#define GR_ENUM_TYPE_VALID(t) ((t) >= 0 && (t) < " + map.Count + ")\n\n");
		}

		/// <summary>
		/// Make a C array containing the strings made of the names of the
		/// objects in the map. The index of a string corresponds to the
		/// integer value in the map. </summary>
		/// <param name="sb"> The string buffer to append the text to. </param>
		/// <param name="map"> The type map which contains the types. </param>
		/// <param name="add"> A string which shall prepend the name of the array. </param>
		protected internal static void MakeTypeMap(PrintStream ps, IDictionary<InheritanceType, int> map, string add)
		{
			string[] names = new string[map.Count];

			foreach(InheritanceType ty in map.Keys)
			{
				int index = GetTypeId(map, ty);
				names[index] = ty.Ident.ToString();
			}

			ps.Print("static const char *" + add + "_type_map[] = {\n");
			for(int i = 0; i < names.Length; i++)
				ps.Print("  \"" + names[i] + "\",\n");
			ps.Print("  NULL\n};\n\n");
		}

		/// <summary>
		/// Make the attribute map.
		/// Each entry consists of an type ID that represents the attributes
		/// owner type, and the name of the attribute. </summary>
		/// <param name="sb"> The string buffer to add the code to. </param>
		/// <param name="attrMap"> The attribute map. </param>
		/// <param name="typeMap"> The type map for these attributes. </param>
		/// <param name="enumMap"> The enum type map. </param>
		/// <param name="add"> A string to add to the identifier of the map. </param>
		protected internal static void MakeAttrMap<T1>(PrintStream ps, IDictionary<Entity, int> attrMap,
				IDictionary<T1, int> typeMap,
				IDictionary<EnumType, int> enumMap, string add) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			string[] name = new string[attrMap.Count];
			Type[] types = new Type[attrMap.Count];
			int[] owner = new int[attrMap.Count];

			foreach(Entity ent in attrMap.Keys)
			{
				int index = attrMap[ent];
				name[index] = ent.Ident.ToString();
				owner[index] = GetTypeId(typeMap, ent.Owner);
				types[index] = ent.Type;
			}

			ps.Print("/** The attribute map for " + add + " attributes. */\n");
			ps.Print("static const attr_t " + add + "_attr_map[] = {\n");
			for(int i = 0; i < name.Length; i++)
			{
				ps.Print("  { " + owner[i] + ", " + FormatString(name[i]) + ", " + types[i].Classify() + ", ");

				if(types[i] is EnumType)
				{
					int id = GetTypeId(enumMap, types[i]);
					ps.Print(id + " },\n");
				}
				else
					ps.Print("-1 },\n");
			}
			ps.Print("  { 0, NULL, 0 }\n};\n\n");

		}

		/// <summary>
		/// Make a matrix that represents the type relation. </summary>
		/// <param name="buf"> The string buffer to add the code to. </param>
		/// <param name="typeMap"> The type map to use. </param>
		/// <param name="add"> A string to add to the identifier. </param>
		protected internal virtual void MakeIsAMatrix(PrintStream ps, bool forNode, string add)
		{
			// since all type id's are given from zero on, the maximum type id
			// (not used) is the number of entries in the type map.
			short[][] matrix = GetIsAMatrix(forNode);
			int maxTypeId = matrix.Length;
			string matrixName = add + "_type_is_a_matrix";

			ps.Print("/** The matrix showing valid type attributes for " + add + ". */\n");
			ps.Print("static const char " + matrixName + "[" + maxTypeId + "][" + maxTypeId + "] = {\n");

			for(int i = 0; i < maxTypeId; i++)
			{
				ps.Print("  { ");
				for(int j = 0; j < maxTypeId; j++)
				{
					ps.Print(j != 0 ? ", " : "");
					ps.Print(matrix[i][j]);
				}
				ps.Print(" }, /* ");
				ps.Print(i);
				ps.Print(' ');
				ps.Print(GetTypeName(forNode, i));
				ps.Print(" */\n");
			}
			ps.Print("};\n\n");
			ps.Print("/** Function to test for type compatibility. */\n");
			ps.Print("static inline int ");
			ps.Print(add);
			ps.Print("_type_is_a(int t1, int t2) {\n");
			ps.Print("  return t1 == t2 || " + matrixName + "[t1][t2] != 0;\n}\n\n");
		}

		protected internal virtual void MakeSuperSubTypes(PrintStream ps, bool forNode, string add)
		{
			int[] types = GetIDs(forNode);
			int maxTypeId = types.Length;

			ps.Print("static const char " + add + "_super_types[" + (maxTypeId + 1) + "][" + maxTypeId + "] = {\n");

			for(int i = 0; i < maxTypeId; i++)
			{
				int[] superTypes = GetSuperTypes(forNode, i);
				ps.Print("  /* super types of ");
				ps.Print(GetTypeName(forNode, i));
				ps.Print(": ");
				for(int j = 0; j < superTypes.Length; j++)
				{
					ps.Print(GetTypeName(forNode, superTypes[j]));
					ps.Print(" ");
				}
				ps.Print(" */\n");

				ps.Print("  { ");
				for(int j = 0; j < superTypes.Length; j++)
				{
					ps.Print(superTypes[j]);
					ps.Print(", ");
				}
				ps.Print("-1 },\n\n");
			}
			ps.Print("};\n\n");

			ps.Print("static const char " + add + "_sub_types[" + (maxTypeId + 1) + "][" + maxTypeId + "] = {\n");
			for(int i = 0; i < maxTypeId; i++)
			{
				int[] subTypes = GetSubTypes(forNode, i);
				ps.Print("  /* sub types of ");
				ps.Print(GetTypeName(forNode, i));
				ps.Print(": ");
				for(int j = 0; j < subTypes.Length; j++)
				{
					ps.Print(GetTypeName(forNode, subTypes[j]));
					ps.Print(" ");
				}
				ps.Print(" */\n  {");
				for(int j = 0; j < subTypes.Length; j++)
				{
					ps.Print(subTypes[j]);
					ps.Print(", ");
				}
				ps.Print("-1 },\n\n");
			}
			ps.Print("};\n\n");
		}

		/// <summary>
		/// Make the attribute matrix for a given attribute type.
		/// </summary>
		/// <param name="sb">      The string buffer to add the code to. </param>
		/// <param name="add">     The matrix prefix. </param>
		/// <param name="attrMap"> The map of all attributes. </param>
		/// <param name="typeMap"> The type map to use. </param>
		protected internal static void MakeAttrMatrix<T1>(PrintStream ps, string add,
				IDictionary<Entity, int> attrMap, IDictionary<T1, int> typeMap) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{

			int maxTypeId = typeMap.Count;
			int maxAttrId = attrMap.Count;
			int[][] matrix = RectangularArrays.RectangularIntArray(maxTypeId, maxAttrId);

			foreach(Entity ent in attrMap.Keys)
			{
				int attrId = attrMap[ent];
				int typeId = GetTypeId(typeMap, ent.Owner);
				matrix[typeId][attrId] = 1;
			}

			ps.Print("static const char " + add + "_attr_matrix[" + maxTypeId + "][" + maxAttrId + "] = {\n");

			for(int i = 0; i < maxTypeId; i++)
			{
				ps.Print("  { ");
				for(int j = 0; j < maxAttrId; j++)
					ps.Print((j != 0 ? ", " : "") + matrix[i][j]);
				ps.Print(" },\n");
			}
			ps.Print("};\n");
		}

		protected internal static void MakeActionMap(PrintStream ps, IDictionary<Rule, int> map)
		{
			Action[] actions = new Action[map.Count];

			foreach(Rule r in map.Keys)
			{
				int index = map[r];
				actions[index] = r;
			}

			ps.Print("#define GR_ACTION_VALID(x) ((x) >= 0 && (x) < " + actions.Length + ")\n\n");
			ps.Print("#define GR_ACTIONS " + actions.Length + "\n\n");

			ps.Print("static const action_t action_map[] = {\n");
			for(int i = 0; i < actions.Length; i++)
			{
				Action a = actions[i];
				string kind = "gr_action_kind_test";

				if(a is Rule && ((Rule)a).Right != null)
					kind = "gr_action_kind_rule";

				ps.Print("  { " + FormatString(a.Ident.ToString()) + ", " + kind + ", 0, 0, NULL, NULL },\n");
			}
			ps.Print("  { NULL, -1, 0, 0, NULL, NULL }\n};\n\n");
		}

		/// <summary>
		/// Generate code for matching actions. </summary>
		/// <param name="sb"> The string buffer to add the code to. </param>
		protected internal virtual void MakeActions(PrintStream ps)
		{
			foreach(Rule a in actionRuleMap.Keys)
			{
				int id = actionRuleMap[a];
				GenMatch(ps, a, id);
				GenFinish(ps, a, id);
			}
		}

		/// <summary>
		/// Adds a XML type tag to the string buffer.
		/// </summary>
		/// <param name="depth">  indentation depth </param>
		/// <param name="sb">     the string buffer </param>
		/// <param name="ending"> the end of the XML tag, either ">" or "/>" </param>
		/// <param name="inh">    the type </param>
		protected internal static void DumpXMLTag(int depth, PrintStream ps, string ending, Type inh)
		{
			for(int i = 0; i < depth; ++i)
				ps.Print("  ");
			ps.Print("<" + inh.Name.Replace(' ', '_') + " name=\"" + inh.Ident + "\"" + ending);
		}

		/// <summary>
		/// Adds a XML end type tag to the string buffer.
		/// </summary>
		/// <param name="depth"> indentation depth </param>
		/// <param name="sb">    the string buffer </param>
		/// <param name="inh">   the type </param>
		protected internal static void DumpXMLEndTag(int depth, PrintStream ps, Type inh)
		{
			for(int i = 0; i < depth; ++i)
				ps.Print("  ");
			ps.Print("</" + inh.Name.Replace(' ', '_') + ">\n");
		}

		/// <summary>
		/// Adds an XML entity tag to the string buffer.
		/// </summary>
		/// <param name="depth">  indentation depth </param>
		/// <param name="sb">     the string buffer </param>
		/// <param name="ending"> the end of the XML tag, either ">" or "/>" </param>
		/// <param name="ent">    the entity </param>
		protected internal static void DumpXMLTag(int depth, PrintStream ps, string ending, Entity ent)
		{
			for(int i = 0; i < depth; ++i)
				ps.Print("  ");
			ps.Print("<" + ent.Name.Replace(' ', '_') + " name=\"" + ent.Ident + "\"" + " type=\"" + ent.Type.Ident + "\"" + ending);
		}

		/// <summary>
		/// Adds a XML end entity tag to the string buffer.
		/// </summary>
		/// <param name="depth"> indentation depth. </param>
		/// <param name="sb">    the string buffer. </param>
		/// <param name="ent">   the entity. </param>
		protected internal static void DumpXMLEndTag(int depth, PrintStream ps, Entity ent)
		{
			for(int i = 0; i < depth; ++i)
				ps.Print("  ");
			ps.Print("</" + ent.Name.Replace(' ', '_') + ">\n");
		}

		/// <summary>
		/// Adds a XML enum value tag to the string buffer.
		/// </summary>
		/// <param name="depth">  indentation depth. </param>
		/// <param name="sb">     the string buffer. </param>
		/// <param name="ending"> the end of the XML tag, either ">" or "/>". </param>
		/// <param name="ev">     the enum item. </param>
		protected internal static void DumpXMLTag(int depth, PrintStream ps, string ending, EnumItem ev)
		{
			for(int i = 0; i < depth; ++i)
				ps.Print("  ");
			ps.Print("<" + ev.Name.Replace(' ', '_') + " name=\"" + ev + "\" value=\"" + ev.Value.Value + "\"" + ending);
		}

		/// <summary>
		/// Dump an overview of all declared types, attributes and enums to
		/// an XML file.
		/// </summary>
		/// <param name="sb"> The string buffer to put the XML stuff to. </param>
		protected internal virtual void WriteOverview(PrintStream ps)
		{
			ICollection<IDictionary<InheritanceType, int>> maps = new LinkedHashSet<IDictionary<InheritanceType, int>>();
			maps.Add(GetTypeMap(nodeTypeMap));
			maps.Add(GetTypeMap(edgeTypeMap));

			ps.Print("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");

			ps.Print("<unit>\n");

	// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
	// ORIGINAL LINE: for(java.util.Map<? extends de.unika.ipd.grgen.ir.model.type.InheritanceType, int> map : maps)
			foreach(IDictionary<InheritanceType, int> map in maps)
			{
				foreach(InheritanceType type in map.Keys)
				{
					dumpXMLTag(1, ps, ">\n", type);

					if(type.DirectSuperTypes.Count > 0)
						ps.Print("    <inherits>\n");
					foreach(InheritanceType inh in type.DirectSuperTypes)
						dumpXMLTag(3, ps, "/>\n", inh);
					if(type.DirectSuperTypes.Count > 0)
						ps.Print("    </inherits>\n");

					if(type.Members.Count > 0)
						ps.Print("    <attributes>\n");
					foreach(Entity ent in type.Members)
						DumpXMLTag(3, ps, "/>\n", ent);
					if(type.Members.Count > 0)
						ps.Print("    </attributes>\n");

					dumpXMLEndTag(1, ps, type);
				}
			}

			foreach(EnumType type in enumMap.Keys)
			{
				dumpXMLTag(1, ps, ">\n", type);

				if(type.Items.Count > 0)
					ps.Print("    <attributes>\n");
				ps.Print("    <items>\n");
				foreach(EnumItem ev in type.Items)
					DumpXMLTag(3, ps, "/>\n", ev);
				if(type.Items.Count > 0)
					ps.Print("    </items>\n");

				dumpXMLEndTag(1, ps, type);
			}

			ps.Print("</unit>\n");
		}

		/// <summary>
		/// Make some additional C type declarations that must probably be used
		/// in the generated code. </summary>
		/// <param name="sb"> The string buffer to the stuff to. </param>
		protected internal static void MakeCTypes(PrintStream ps)
		{
			ps.Print("/** The attribute type classification. */\n");
			ps.Print("typedef enum _attribute_type {\n");
			ps.Print("  AT_TYPE_INTEGER = " + Type.TypeClass.IS_INTEGER + ", /**< an integer */\n");
			ps.Print("  AT_TYPE_BOOLEAN = " + Type.TypeClass.IS_BOOLEAN + ", /**< a boolean */\n");
			ps.Print("  AT_TYPE_STRING  = " + Type.TypeClass.IS_STRING + ", /**< a string */\n");
			ps.Print("} attribute_type;\n\n");

			ps.Print("/** The attribute type. */\n");
			ps.Print("typedef struct {\n"
					+ "  int type_id;       /**< the ID of attributes type */\n"
					+ "  const char *name;  /**< the name of the attribute */\n"
					+ "  attribute_type at; /**< the attribute type kind */\n"
					+ "  int enum_id;       /**< the Id of the enum type or -1 */\n"
					+ "} attr_t;\n\n");

			ps.Print("/** The type of an action. */\n");
			ps.Print("typedef struct {\n"
					+ "  const char *name;\n"
					+ "  gr_action_kind_t kind;\n"
					+ "  int ins;\n"
					+ "  int outs;\n"
					+ "  const gr_value_kind_t *in_types;\n"
					+ "  const gr_value_kind_t *out_types;\n"
					+ "} action_t;\n\n");

			ps.Print("/** The type of an enum item declaration. */\n");
			ps.Print("typedef struct {\n"
					+ "  const char *name;    /**< the name of the enum item */\n"
					+ "  int value;           /**< the value of the enum item */\n"
					+ "} enum_item_decl_t;\n\n");

			ps.Print("/** The type of an enum declaration. */\n");
			ps.Print("typedef struct {\n"
					+ "  const char *name;    /**< the name of the enum type */\n"
					+ "  int num_items;       /**< the number of items in this enum type */\n"
					+ "  const enum_item_decl_t *items;  /**< the items of this enum type */\n"
					+ "} enum_type_decl_t;\n\n");

			ps.Print("/** The type of the enum table. */\n");
			ps.Print("typedef struct {\n"
					+ "  const enum_type_decl_t *type; /**< declaration of the type */\n"
					+ "  int type_id;                  /**< the Id of this enum type */\n"
					+ "} enum_types_t;\n\n");
		}

		/// <summary>
		/// Dump all enum type declarations to a string buffer.
		/// </summary>
		/// <param name="sb">   The string buffer. </param>
		/// <param name="map">  A map containing all enum types. </param>
		protected internal static void MakeEnumDeclarations(PrintStream ps, IDictionary<EnumType, int> map)
		{
			// build the description of all enum types
			foreach(EnumType type in map.Keys)
			{
				Ident name = type.Ident;

				ps.Print("/** The items for the " + name + " enum type. */\n");
				ps.Print("static const enum_item_decl_t _" + name + "_items[] = {\n");

				foreach(EnumItem ev in type.Items)
					ps.Print(" { \"" + ev + "\", " + ev.Value.Value + " },\n");
				ps.Print("};\n\n");

				ps.Print("/** The declaration of the " + name + " enum type. */\n");
				ps.Print("static const enum_type_decl_t " + name + "_decl = {\n"
						+ "  \"" + name + "\",\n"
						+ "  sizeof(_" + name + "_items)/sizeof(_" + name + "_items[0]),\n"
						+ "  _" + name + "_items,\n"
						+ "};\n\n");
			}

			// dump all enums to a table
			ps.Print("/** All enum types. */\n");
			ps.Print("static const enum_types_t enum_types[] = {\n");

			string[] names = new string[map.Count];
			foreach(EnumType type in map.Keys)
			{
				int index = GetTypeId(map, type);

				names[index] = type.Ident.ToString();
			}

			for(int i = 0; i < map.Count; ++i)
				ps.Print("  { &" + names[i] + "_decl, " + i + " },\n");
			ps.Print("};\n\n");
		}

		/// <seealso cref="de.unika.ipd.grgen.be.Backend.init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter)"/>
		public virtual void Init(Unit unit, Sys sys, File outputPath)
		{
			this.unit = unit;
			error = sys.ErrorReporter;
			this.path = outputPath;
			path.Mkdirs();

			MakeTypes(unit);
		}

		/// <seealso cref="de.unika.ipd.grgen.be.Backend.generate()"/>
		public virtual void Generate()
		{
			string unitName = FormatString(unit.UnitName);

			// Emit the C types file
			PrintStream ps = OpenFile("types" + incExtension);
			MakeCTypes(ps);
			CloseFile(ps);

			// Emit the type defines.
			ps = OpenFile("graph" + incExtension);
			ps.Println("/** name of the unit */\n");
			ps.Println("#define UNIT_NAME " + unitName + "\n\n");

			ps.Println("/** type model digest */\n");
			ps.Println("#define TYPE_MODEL_DIGEST \"" + unit.TypeDigest + "\"\n\n");

			MakeTypeDefines(ps, nodeTypeMap, "NODE");
			MakeTypeDefines(ps, edgeTypeMap, "EDGE");
			MakeAttrDefines(ps, nodeAttrMap, "NODE");
			MakeAttrDefines(ps, edgeAttrMap, "EDGE");
			MakeEnumDefines(ps, enumMap);
			CloseFile(ps);

			ps = OpenFile("enums" + incExtension);
			MakeEnumDeclarations(ps, enumMap);
			CloseFile(ps);

			// Make the "is a" matrices.
			ps = OpenFile("is_a" + incExtension);
			MakeIsAMatrix(ps, true, "node");
			MakeIsAMatrix(ps, false, "edge");
			CloseFile(ps);

			ps = OpenFile("super_sub_types" + incExtension);
			MakeSuperSubTypes(ps, true, "node");
			MakeSuperSubTypes(ps, false, "edge");
			CloseFile(ps);

			// Make the attribute matrices
			ps = OpenFile("attr" + incExtension);
			MakeAttrMatrix(ps, "node", nodeAttrMap, nodeTypeMap);
			MakeAttrMatrix(ps, "edge", edgeAttrMap, edgeTypeMap);
			CloseFile(ps);

			// Make arrays with names of the types.
			ps = OpenFile("names" + incExtension);
			MakeTypeMap(ps, GetTypeMap(nodeTypeMap), "node");
			MakeTypeMap(ps, GetTypeMap(edgeTypeMap), "edge");
			MakeAttrMap(ps, nodeAttrMap, nodeTypeMap, enumMap, "node");
			MakeAttrMap(ps, edgeAttrMap, edgeTypeMap, enumMap, "edge");
			CloseFile(ps);

			ps = OpenFile("actions" + incExtension);
			MakeActionMap(ps, actionRuleMap);
			CloseFile(ps);

			ps = OpenFile("action_impl" + incExtension);
			MakeActions(ps);
			CloseFile(ps);

			// write an overview of all generated Ids
			ps = OpenFile("overview.xml");
			WriteOverview(ps);
			CloseFile(ps);

			// Make validate data structures.
			GenValidateStatements();

			// a hook for special generated things
			GenExtra();
		}

		protected internal abstract void GenMatch(PrintStream sb, MatchingAction a, int id);

		protected internal abstract void GenFinish(PrintStream sb, MatchingAction a, int id);

		/// <summary>
		/// Generate some extra stuff.
		/// This function is called after everything else is generated.
		/// </summary>
		protected internal abstract void GenExtra();

		/// <seealso cref="de.unika.ipd.grgen.be.Backend.done()"/>
		public virtual void Done()
		{
			// nothing to do
		}

		/// <seealso cref="de.unika.ipd.grgen.be.C.Formatter.formatId(java.lang.String)"/>
		public static string FormatId(string id)
		{
			return id;
		}

		/// <summary>
		/// Format a string into a C string.
		/// This takes a Java string and produces a C string literal of it by escaping
		/// some characters and putting quotes around it.
		/// If a character is equal to the constant <code>BREAK_LINE</code> defined
		/// above, the string literal is ended and continued at the next line.
		/// This gives a better readability, if used properly. </summary>
		/// <param name="s"> A string. </param>
		/// <returns> A C string literal. </returns>
		public static string FormatString(string s)
		{
			MemoryStream bos = new MemoryStream(s.Length * 2);
			PrintStream ps = new PrintStream(bos);
			FormatString(ps, s);
			ps.Flush();
			ps.close();
			return bos.ToString();
		}

		public static void FormatString(PrintStream ps, string s)
		{
			ps.Print('\"');
			for(int i = 0; i < s.Length; i++)
			{
				char ch = s[i];
				switch(ch)
				{
				case '\"':
					ps.Print("\\\"");
					break;
				case '\'':
					ps.Print("\\\'");
					break;
				case '\n':
				case '\t':
					break;
				default:
					ps.Print(ch);
				break;
				}
			}
			ps.Print('\"');
		}

		protected internal virtual void GenValidateStatements()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("\n/** The Validate Info */\n\n");
			sb.Append("static gr_validate_info_t valid_info[] = {\n");

			foreach(EdgeType edgeType in edgeTypeMap.Keys)
			{
				foreach(ConnAssert ca in edgeType.ConnAsserts)
				{
					sb.Append("\n{\n");
					sb.Append("  " + GetId(edgeType) + ",\n");
					sb.Append("  " + GetId(ca.SrcType) + ",\n");
					sb.Append("  " + GetId(ca.TgtType) + ",\n");
					sb.Append("  " + ca.SrcLower + ",\n");
					sb.Append("  " + ca.SrcUpper + ",\n");
					sb.Append("  " + ca.TgtLower + ",\n");
					sb.Append("  " + ca.TgtUpper + ",\n");
					sb.Append("},\n");
				}
			}
			sb.Append("\n{-1, -1, -1, -1, -1, -1, -1}\n\n};\n\n");

			WriteFile("valid_info" + incExtension, sb);
		}
	}

}
