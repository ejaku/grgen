// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ExternalAttributeEvaluationExample\ExternalAttributeEvaluation.grg" on Thu Jul 18 15:34:04 CEST 2013

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_ExternalAttributeEvaluation
{
	public partial class Own
	{
		// You must implement this class in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:
	}

	public partial class OwnPown : Own
	{
		// You must implement this class in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:
	}

	public partial class OwnPownHome : OwnPown
	{
		// You must implement this class in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:
	}

	public partial class AttributeTypeObjectEmitterParser
	{
		// You must implement this class in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:
		// You must implement the functions called by the following functions inside that class (same name plus suffix Impl):

		// Called during .grs import, at exactly the position in the text reader where the attribute begins.
		// For attribute type object or a user defined type, which is treated as object.
		// The implementation must parse from there on the attribute type requested.
		// It must not parse beyond the serialized representation of the attribute, 
		// i.e. Peek() must return the first character not belonging to the attribute type any more.
		// Returns the parsed object.
		public static object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return ParseImpl(reader, attrType, graph);
			//reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l' // default implementation
			//return null; // default implementation
		}

		// Called during .grs export, the implementation must return a string representation for the attribute.
		// For attribute type object or a user defined type, which is treated as object.
		// The serialized string must be parseable by Parse.
		public static string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return SerializeImpl(attribute, attrType, graph);
			//Console.WriteLine("Warning: Exporting attribute of object type to null"); // default implementation
			//return "null"; // default implementation
		}

		// Called during debugging or emit writing, the implementation must return a string representation for the attribute.
		// For attribute type object or a user defined type, which is treated as object.
		// The attribute type may be null.
		// The string is meant for consumption by humans, it does not need to be parseable.
		public static string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return EmitImpl(attribute, attrType, graph);
			//return "null"; // default implementation
		}
	}

	public partial class AttributeTypeObjectCopierComparer
	{
		// You must implement the following functions in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:

		// Called when a graph element is cloned/copied.
		// For attribute type object.
		// If "copy class" is not specified, objects are copied by copying the reference, i.e. they are identical afterwards.
		// All other attribute types are copied by-value (so changing one later on has no effect on the other).
		//public static object Copy(object);

		// Called during comparison of graph elements from graph isomorphy comparison, or attribute comparison.
		// For attribute type object.
		// If "== class" is not specified, objects are equal if they are identical,
		// i.e. by-reference-equality (same pointer); all other attribute types are compared by-value.
		//public static bool IsEqual(object, object);

		// Called during attribute comparison.
		// For attribute type object.
		// If "< class" is not specified, objects can't be compared for ordering, only for equality.
		//public static bool IsLower(object, object);


		// The same functions, just for each user defined type.
		// Those are normally treated as object (if no "copy class or == class or < class" is specified),
		// i.e. equal if identical references, no ordered comparisons available, and copy just copies the reference (making them identical).
		// Here you can overwrite the default reference semantics with value semantics, fitting better to the other attribute types.

		//public static Own Copy(Own);
		//public static bool IsEqual(Own, Own);
		//public static bool IsLower(Own, Own);

		//public static OwnPown Copy(OwnPown);
		//public static bool IsEqual(OwnPown, OwnPown);
		//public static bool IsLower(OwnPown, OwnPown);

		//public static OwnPownHome Copy(OwnPownHome);
		//public static bool IsEqual(OwnPownHome, OwnPownHome);
		//public static bool IsLower(OwnPownHome, OwnPownHome);
	}

}

namespace de.unika.ipd.grGen.expression
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

	public partial class ExternalFunctions
	{
		// You must implement the following functions in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:

		//public static bool foo(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, int, double, GRGEN_MODEL.ENUM_Enu, string);
		//public static object bar(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, object, object);
		//public static bool isnull(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, object);
		//public static bool bla(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.IN, GRGEN_MODEL.IE);
		//public static GRGEN_MODEL.IN blo(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_LIBGR.INode, GRGEN_LIBGR.IEdge);
		//public static GRGEN_MODEL.OwnPown har(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.Own, GRGEN_MODEL.OwnPown);
		//public static bool hur(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.OwnPown);
		//public static bool hurdur(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.OwnPownHome);
		//public static GRGEN_MODEL.Own own(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph);
		//public static GRGEN_MODEL.OwnPown ownPown(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph);
		//public static GRGEN_MODEL.OwnPownHome ownPownHome(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph);
	}
}

namespace de.unika.ipd.grGen.expression
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;

	public partial class ExternalProcedures
	{
		// You must implement the following procedures in the same partial class in ./ExternalAttributeEvaluationModelExternalFunctionsImpl.cs:

		//public static void fooProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, int, double, GRGEN_MODEL.ENUM_Enu, string);
		//public static void barProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, object, object, out object);
		//public static void isnullProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, object, out bool);
		//public static void blaProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.IN, GRGEN_MODEL.IE, out bool, out bool);
		//public static void bloProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_LIBGR.INode, GRGEN_LIBGR.IEdge, out GRGEN_MODEL.IN);
		//public static void harProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.Own, GRGEN_MODEL.OwnPown, out GRGEN_MODEL.OwnPown, out GRGEN_MODEL.Own, out GRGEN_MODEL.IN);
		//public static void hurProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.OwnPown);
		//public static void hurdurProc(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_MODEL.OwnPownHome);
	}
}
