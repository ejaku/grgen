/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.type
{

	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;

	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

	/// <summary>
	/// Abstract base class for types.
	/// Subclasses distinguished into primitive (string, int, boolean, ...) and compound
	/// </summary>
	public abstract class Type : Identifiable
	{
		/// <summary>
		/// helper class for comparing objects of type Type, used in compareTo, overwriting comparteTo of Identifiable </summary>
		private new static readonly IComparer<Type> COMPARATOR = new ComparatorAnonymousInnerClass();

		private class ComparatorAnonymousInnerClass : IComparer<Type>
		{
			private readonly Type outerInstance;

			public int Compare(Type t1, Type t2)
			{
				if(t1.IsEqual(t2))
					return 0;

				if((t1 is InheritanceType) && (t2 is InheritanceType))
				{
					int distT1 = ((InheritanceType)t1).MaxDist;
					int distT2 = ((InheritanceType)t2).MaxDist;

					if(distT1 < distT2)
						return -1;
					else if(distT1 > distT2)
						return 1;
				}

				return t1.Ident.CompareTo(t2.Ident);
			}
		}

		public enum TypeClass
		{
			IS_UNKNOWN,
			IS_BYTE,
			IS_SHORT,
			IS_INTEGER, // includes ENUM
			IS_LONG,
			IS_FLOAT,
			IS_DOUBLE,
			IS_BOOLEAN,
			IS_STRING,
			IS_TYPE,
			IS_OBJECT,
			IS_SET,
			IS_MAP,
			IS_ARRAY,
			IS_DEQUE,
			IS_UNTYPED_EXEC_VAR_TYPE,
			IS_EXTERNAL_CLASS_OBJECT,
			IS_GRAPH,
			IS_MATCH,
			IS_DEFINED_MATCH,
			IS_NODE,
			IS_EDGE,
			IS_INTERNAL_CLASS_OBJECT,
			IS_INTERNAL_TRANSIENT_CLASS_OBJECT
		}

		/// <summary>
		/// Make a new type. </summary>
		/// <param name="name"> The name of the type (test, group, ...). </param>
		/// <param name="ident"> The identifier used to declare that type. </param>
		public Type(string name, Ident ident)
			: base(name, ident)
		{
		}

		/// <summary>
		/// Decides, if two types are equal. </summary>
		/// <param name="t"> The other type. </param>
		/// <returns> true, if the types are equal. </returns>
		public virtual bool IsEqual(Type t)
		{
			return t == this;
		}

		/// <summary>
		/// Compute, if this type is castable to another type.
		/// You do not have to check, if <code>t == this</code>. </summary>
		/// <param name="t"> The other type. </param>
		/// <returns> true, if this type is castable. </returns>
		protected internal virtual bool CastableTo(Type t)
		{
			return false;
		}

		/// <summary>
		/// Checks, if this type is castable to another type.
		/// This method is final, to implement the castability, overwrite <code>castableTo</code>, which is called by this method. </summary>
		/// <param name="t"> The other type. </param>
		/// <returns> true, if this type can be casted to <code>t</code>, false otherwise. </returns>
		public bool IsCastableTo(Type t)
		{
			return IsEqual(t) || CastableTo(t);
		}

		/// <returns> true, if this type is a void type. </returns>
		public virtual bool IsVoid()
		{
			return false;
		}

		/// <summary>
		/// Return a classification of a type for the IR. </summary>
		public virtual TypeClass Classify()
		{
			return TypeClass.IS_UNKNOWN;
		}

		internal static IComparer<Type> Comparator
		{
			get
			{
				return COMPARATOR;
			}
		}

		public override int CompareTo(Identifiable id)
		{
			if(id is Type)
				return COMPARATOR.Compare(this, (Type)id);

			Debug.Assert(false);
			return base.CompareTo(id);
		}

		public virtual bool IsOrderableType()
		{
			if(Classify() == TypeClass.IS_BYTE)
				return true;
			if(Classify() == TypeClass.IS_SHORT)
				return true;
			if(Classify() == TypeClass.IS_INTEGER) // includes ENUM
				return true;
			if(Classify() == TypeClass.IS_LONG)
				return true;
			if(Classify() == TypeClass.IS_FLOAT)
				return true;
			if(Classify() == TypeClass.IS_DOUBLE)
				return true;
			if(Classify() == TypeClass.IS_STRING)
				return true;
			if(Classify() == TypeClass.IS_BOOLEAN)
				return true;
			return false;
		}

		public virtual bool IsFilterableType()
		{
			if(IsOrderableType())
				return true;
			if(Classify() == TypeClass.IS_NODE)
				return true;
			if(Classify() == TypeClass.IS_EDGE)
				return true;
			if(Classify() == TypeClass.IS_INTERNAL_CLASS_OBJECT)
				return true;
			return false;
		}

		public virtual bool IsArrayOfMatchType()
		{
			if(Classify() != TypeClass.IS_ARRAY)
				return false;
			if(((ArrayType)this).valueType.Classify() != TypeClass.IS_MATCH)
				return false;
			return true;
		}

		public virtual bool IsArrayOfMatchClassType()
		{
			if(Classify() != TypeClass.IS_ARRAY)
				return false;
			if(((ArrayType)this).valueType.Classify() != TypeClass.IS_DEFINED_MATCH)
				return false;
			return true;
		}

		/// <summary>
		/// Add this type to the digest. </summary>
		public virtual void AddToDigest(StringBuilder sb)
		{
			// sensible base implementation, to be overwritten selectively
		}
	}

}
