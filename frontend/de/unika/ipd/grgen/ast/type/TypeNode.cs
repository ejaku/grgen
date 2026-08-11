/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{
using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using Type = de.unika.ipd.grgen.ir.type.Type;


/// <summary>
/// Base class for all AST nodes representing types.
/// </summary>
public abstract class TypeNode : BaseNode
{
	/// <summary>
	/// A map, that maps each basic type to a set of all other basic types
	///  that are compatible to the type. 
	/// </summary>
	private static readonly IDictionary<TypeNode, HashSet<TypeNode>> compatibleMap = new Dictionary<TypeNode, HashSet<TypeNode>>();

	/// <summary>
	/// A map, that maps each type to a set of all other types
	/// that are castable to the type. 
	/// </summary>
	private static readonly IDictionary<TypeNode, HashSet<TypeNode>> castableMap = new Dictionary<TypeNode, HashSet<TypeNode>>();

	// Cache variables
	private ISet<TypeNode> compatibleToTypes;
	private ISet<TypeNode> castableToTypes;

	public static string KindStr
	{
		get
		{
		return "type";
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	/// <summary>
	/// Compute the distance of indirect type compatibility (where 'compatibility'
	/// means implicit castability of attribute types; accordingly the distance
	/// means the required number of implicit type casts).
	/// <br><bf>Note</bf> that this method only supports indirections of a
	/// distance upto two. If you need more you have to implement this!
	/// </summary>
	/// <param name="type">	a TypeNode
	/// 
	/// @return		the compatibility distance, Integer.MAX_VALUE if no compatibility could
	/// 				be found </param>
	public virtual int CompatibilityDistance(TypeNode type)
	{
		if(this.IsEqual(type))
			return 0;
		if(this.IsCompatibleTo(type))
			return 1;

		foreach(TypeNode t in CompatibleToTypes)
		{
			if(t.IsCompatibleTo(type))
				return 2;
		}

		return int.MaxValue;
	}

	/// <summary>
	/// Check, if this type is compatible (implicitly castable) or equal to <code>t</code>. </summary>
	/// <param name="t"> A type. </param>
	/// <returns> true, if this type is compatible or equal to <code>t</code> </returns>
	public virtual bool IsCompatibleTo(TypeNode t)
	{
		if(IsEqual(t))
			return true;

		return CompatibleToTypes.Contains(t);
	}

	/// <summary>
	/// Check, if this type is only castable (explicitly castable)
	/// to <code>t</code> </summary>
	/// <param name="t"> A type. </param>
	/// <returns> true, if this type is just castable to <code>t</code>. </returns>
	public virtual bool IsCastableTo(TypeNode t)
	{
		return CastableToTypes.Contains(t);
	}

	public override Color NodeColor
	{
		get
		{
		return Color.MAGENTA;
		}
	}

	/// <summary>
	/// Get the IR object as type.
	/// The cast must always succeed. </summary>
	/// <returns> The IR object as type. </returns>
	public virtual Type IRType
	{
		get
		{
		return CheckIR(typeof(Type));
		}
	}

	/// <summary>
	/// Checks, if two types are equal. </summary>
	/// <param name="t"> The type to check for. </param>
	/// <returns> true, if this and <code>t</code> are of the same type. </returns>
	public virtual bool IsEqual(TypeNode t)
	{
		if(t == this)
			return true;
		else if(t is SetTypeNode && this is SetTypeNode)
			return ((SetTypeNode)t).valueType == ((SetTypeNode)this).valueType;
		else if(t is MapTypeNode && this is MapTypeNode)
			return ((MapTypeNode)t).keyType == ((MapTypeNode)this).keyType
					&& ((MapTypeNode)t).valueType == ((MapTypeNode)this).valueType;
		else if(t is ArrayTypeNode && this is ArrayTypeNode)
			return ((ArrayTypeNode)t).valueType == ((ArrayTypeNode)this).valueType;
		else if(t is DequeTypeNode && this is DequeTypeNode)
			return ((DequeTypeNode)t).valueType == ((DequeTypeNode)this).valueType;
		else
			return false;
	}

	/// <summary>
	/// Check, if the type is a basic type (integer, boolean, string, void). </summary>
	/// <returns> true, if the type is a basic type. </returns>
	public virtual bool IsBasic()
	{
		return false;
	}

	/// <summary>
	/// Returns a collection of all compatible types which are compatible to this one.
	/// </summary>
	public ICollection<TypeNode> CompatibleToTypes
	{
		get
		{
		if(compatibleToTypes == null)
		{
			compatibleToTypes = new HashSet<TypeNode>();
			DoGetCompatibleToTypes(compatibleToTypes);
			compatibleToTypes.Add(this);
			compatibleToTypes = Collections.UnmodifiableSet(compatibleToTypes);
		}
		return compatibleToTypes;
		}
	}

	public static void AddCompatibility(TypeNode a, TypeNode b)
	{
		if(compatibleMap[a] == null)
			compatibleMap[a] = new HashSet<TypeNode>();
		compatibleMap[a].Add(b);
	}

	public static void AddCastability(TypeNode from, TypeNode to)
	{
		if(castableMap[from] == null)
			castableMap[from] = new HashSet<TypeNode>();
		castableMap[from].Add(to);
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.type.TypeNode.getCompatibleTypes(java.util.Collection)"/>
	public virtual void DoGetCompatibleToTypes(ICollection<TypeNode> coll)
	{
		debug.Report(NOTE, "compatible types to " + Name + ":");

		ICollection<TypeNode> compatibleTypes = compatibleMap[this];
		if(compatibleTypes == null)
			return;

		if(debug.WillReport(NOTE))
		{
			foreach(BaseNode compatibleType in compatibleTypes)
				debug.Report(NOTE, "" + compatibleType.Name);
		}
		coll.AddAll(compatibleTypes);
	}

	/// <summary>
	/// Returns a collection of all types this one is castable (implicitly and explicitly) to.
	/// </summary>
	protected internal ICollection<TypeNode> CastableToTypes
	{
		get
		{
		if(castableToTypes == null)
		{
			castableToTypes = new HashSet<TypeNode>();
			DoGetCastableToTypes(castableToTypes);
			castableToTypes.AddAll(CompatibleToTypes);
			castableToTypes = Collections.UnmodifiableSet(castableToTypes);
		}
		return castableToTypes;
		}
	}

	private void DoGetCastableToTypes(ICollection<TypeNode> coll)
	{
		ICollection<TypeNode> castable = castableMap[this];
		if(castable != null)
			coll.AddAll(castable);
	}

	public virtual bool IsFilterableType()
	{
		if(IsOrderableType())
			return true;
		if(this is NodeTypeNode)
			return true;
		if(this is EdgeTypeNode)
			return true;
		return false;
	}

	public virtual bool IsOrderableType()
	{
		if(IsAccumulatableType())
			return true;
		if(IsEqual(BasicTypeNode.stringType))
			return true;
		if(IsEqual(BasicTypeNode.booleanType))
			return true;
		if(this is EnumTypeNode)
			return true;
		return false;
	}

	public virtual bool IsAccumulatableType()
	{
		return IsNumericType();
	}

	public virtual bool IsAccumulationTargetType()
	{
		if(IsEqual(BasicTypeNode.intType))
			return true;
		if(IsEqual(BasicTypeNode.longType))
			return true;
		if(IsEqual(BasicTypeNode.floatType))
			return true;
		if(IsEqual(BasicTypeNode.doubleType))
			return true;
		return false;
	}

	public virtual bool IsNumericType()
	{
		if(IsEqual(BasicTypeNode.byteType))
			return true;
		if(IsEqual(BasicTypeNode.shortType))
			return true;
		if(IsEqual(BasicTypeNode.intType))
			return true;
		if(IsEqual(BasicTypeNode.longType))
			return true;
		if(IsEqual(BasicTypeNode.floatType))
			return true;
		if(IsEqual(BasicTypeNode.doubleType))
			return true;
		return false;
	}

	public static string FilterableTypesAsString
	{
		get
		{
		return OrderableTypesAsString + " or a node or edge class";
		}
	}

	public static string OrderableTypesAsString
	{
		get
		{
		return AccumulatableTypesAsString + ", string, boolean";
		}
	}

	public static string AccumulatableTypesAsString
	{
		get
		{
		return NumericTypesAsString;
		}
	}

	public static string AccumulationTargetTypesAsString
	{
		get
		{
		return "int, long, float, double";
		}
	}

	public static string NumericTypesAsString
	{
		get
		{
		return "byte, short, int, long, float, double";
		}
	}

	public virtual bool IsValueType()
	{
		return this is BasicTypeNode
				|| this is EnumTypeNode
				|| this is ExternalObjectTypeNode;
	}

	public virtual bool IsReferenceType()
	{
		return this is ContainerTypeNode
				|| this is MatchTypeNode
				|| this is InternalObjectTypeNode
				|| this is InternalTransientObjectTypeNode;
	}

	public virtual bool IsLockableType()
	{
		return !IsOrderableType();
	}

	// returns type name (to be used in error reporting)
	public virtual string TypeName
	{
		get
		{
		return ToString();
		}
	}
}

}
