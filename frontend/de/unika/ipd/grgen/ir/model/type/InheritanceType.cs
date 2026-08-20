/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>
namespace de.unika.ipd.grgen.ir.model.type
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using Constructor = de.unika.ipd.grgen.ir.Constructor;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using FunctionMethod = de.unika.ipd.grgen.ir.executable.FunctionMethod;
	using ProcedureMethod = de.unika.ipd.grgen.ir.executable.ProcedureMethod;
	using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
	using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
	using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
	using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
	using MemberInit = de.unika.ipd.grgen.ir.model.MemberInit;
	using CompoundType = de.unika.ipd.grgen.ir.type.CompoundType;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// Abstract base class for types that inherit from other types.
	/// </summary>
	public abstract class InheritanceType : CompoundType
	{
		public const int ABSTRACT = 1;
		public const int CONST = 2;

		private static int nextTypeID = 0;
		private static List<InheritanceType> typesByID = new List<InheritanceType>();

		private int typeID;
		private int inheritanceTypeID;
		private int maxDist = -1;
		private readonly ISet<InheritanceType> directSuperTypes = new LinkedHashSet<InheritanceType>();
		private readonly ISet<InheritanceType> directSubTypes = new LinkedHashSet<InheritanceType>();

		private ISet<InheritanceType> allSuperTypes = null;
		private ISet<InheritanceType> allSubTypes = null;

		private List<Constructor> constructors = new List<Constructor>();

		/// <summary>
		/// The list of member initializers </summary>
		private List<MemberInit> memberInitializers = new List<MemberInit>();

		private List<MapInit> mapInitializers = new List<MapInit>();
		private List<SetInit> setInitializers = new List<SetInit>();
		private List<ArrayInit> arrayInitializers = new List<ArrayInit>();
		private List<DequeInit> dequeInitializers = new List<DequeInit>();

		/// <summary>
		/// Collection containing all members defined in that type and in its supertype.
		///  This field is used for caching. 
		/// </summary>
		private IDictionary<string, Entity> allMembers = null;

		private IDictionary<string, FunctionMethod> allFunctionMethods = null;
		private IDictionary<string, ProcedureMethod> allProcedureMethods = null;

		/// <summary>
		/// Map between overriding and overridden members </summary>
		private IDictionary<Entity, Entity> overridingMembers = null;

		/// <summary>
		/// The type modifiers. </summary>
		private readonly int modifiers;

		/// <summary>
		/// The name of the external implementation of this type or null. </summary>
		private string externalName = null;

		/// <param name="name"> The name of the type. </param>
		/// <param name="ident"> The identifier, declaring this type. </param>
		/// <param name="modifiers"> The modifiers for this type. </param>
		/// <param name="externalName"> The name of the external implementation of this type or null. </param>
		protected internal InheritanceType(string name, Ident ident, int modifiers, string externalName)
			: base(name, ident)
		{
			this.modifiers = modifiers;
			this.externalName = externalName;
			typeID = nextTypeID++;
			typesByID.Add(this);
		}

		/// <returns> a unique type identifier starting with zero. (Used in SearchPlanBackend2.java) </returns>
		public virtual int TypeID
		{
			get
			{
				return typeID;
			}
		}

		// returned value is only valid if the property is queried after a all types were created
		public static int MaxTypeID
		{
			get
			{
				return nextTypeID;
			}
		}

		public static InheritanceType GetByTypeID(int typeID)
		{
			return typesByID[typeID];
		}

		/// <returns> a unique type identifier starting with zero, separate for the nodes, for the edges, and for the classes,
		/// i.e. gapless ascending in each type. </returns>
		public virtual int InheritanceTypeID
		{
			get
			{
				return inheritanceTypeID;
			}
			set
			{
				this.inheritanceTypeID = value;
			}
		}


		/// <returns> true, if this type does not inherit from some other types, being the root of an inheritance hierarchy. </returns>
		public virtual bool IsRoot()
		{
			return directSuperTypes.Count == 0;
		}

		/// <summary>
		/// Adds a supertype, this type should inherit from. </summary>
		public virtual void AddDirectSuperType(InheritanceType t)
		{
			Debug.Assert(allSubTypes == null && allSuperTypes == null, "wrong order of calls");
			if(allSubTypes != null || allSuperTypes != null) // todo: remove this constraint/work around it
				error.Error(t.Ident.Coords, "A container in a type is not allowed to reference a subtype.");
			directSuperTypes.Add(t);
			t.directSubTypes.Add(this);
		}

		/// <returns> Set of all types, this type directly inherits from. </returns>
		public virtual ISet<InheritanceType> DirectSuperTypes
		{
			get
			{
				return directSuperTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		/// <returns> Set of all super types this type inherits from (not including itself). </returns>
		public virtual ISet<InheritanceType> AllSuperTypes
		{
			get
			{
				if(allSuperTypes == null)
				{
					allSuperTypes = new LinkedHashSet<InheritanceType>();

					foreach(InheritanceType type in directSuperTypes)
					{
						allSuperTypes.AddAll(type.AllSuperTypes);
						allSuperTypes.Add(type);
					}
				}
				return allSuperTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		/// <returns> Set of all sub types this type inherits from (including itself). </returns>
		public virtual ISet<InheritanceType> AllSubTypes
		{
			get
			{
				if(allSubTypes == null)
				{
					allSubTypes = new LinkedHashSet<InheritanceType>();
					allSubTypes.Add(this);

					foreach(InheritanceType type in directSubTypes)
					{
						allSubTypes.AddAll(type.AllSubTypes);
						allSubTypes.Add(type);
					}
				}
				return allSubTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		/// <summary>
		/// Get all subtypes of this type. </summary>
		public virtual ISet<InheritanceType> DirectSubTypes
		{
			get
			{
				return directSubTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		/// <summary>
		/// Adds all members of the given type to allMembers, handling overwriting
		/// of abstract members including filling the overridingMembers map
		/// </summary>
		private void AddMembers(InheritanceType type)
		{
			foreach(Entity member in type.Members)
			{
				string memberName = member.Ident.ToString();
				Entity curMember = allMembers[memberName];
				if(curMember != null)
				{
					if(curMember.Type.IsVoid())
					{
						// we have an abstract member, it's OK to overwrite it
						overridingMembers[member] = curMember;
					}
					else
					{
						Type ownerType = member.Owner;
						Type curMemberType = curMember.Owner;
						string curMemberDeclarationCoords = curMemberType.Ident.Coords.GetDeclarationCoords(false);
						error.Error(member.Ident.Coords, "The " + member
								+ " of " + ownerType + " is already defined."
								+ " It is also declared in " + curMemberType + curMemberDeclarationCoords + ".");
					}
				}
				allMembers[memberName] = member;
			}
		}

		private void AddFunctionMethods(InheritanceType type)
		{
			foreach(FunctionMethod fm in type.FunctionMethods)
			{
				string functionName = fm.Ident.ToString();
				allFunctionMethods[functionName] = fm;
			}
		}

		private void AddProcedureMethods(InheritanceType type)
		{
			foreach(ProcedureMethod pm in type.ProcedureMethods)
			{
				string procedureName = pm.Ident.ToString();
				allProcedureMethods[procedureName] = pm;
			}
		}

		/// <summary>
		/// Method getAllMembers computes the transitive closure of the members (attributes) of a type. </summary>
		/// <returns>   a Collection containing all members defined in that type and in its supertype. </returns>
		public virtual ICollection<Entity> AllMembers
		{
			get
			{
				if(allMembers == null)
				{
					allMembers = new LinkedHashMap<string, Entity>();
					overridingMembers = new LinkedHashMap<Entity, Entity>();

					// add the members of the super types
					foreach(InheritanceType superType in AllSuperTypes)
						AddMembers(superType);

					// add members of the current type
					AddMembers(this);
				}

				return allMembers.Values;
			}
		}

		public virtual ICollection<FunctionMethod> AllFunctionMethods
		{
			get
			{
				if(allFunctionMethods == null)
				{
					allFunctionMethods = new LinkedHashMap<string, FunctionMethod>();

					// add the members of the super types
					foreach(InheritanceType superType in AllSuperTypes)
						AddFunctionMethods(superType);

					// add members of the current type
					AddFunctionMethods(this);
				}

				return allFunctionMethods.Values;
			}
		}

		public virtual IDictionary<string, FunctionMethod> AllFunctionMethodsByName
		{
			get
			{
				if(allFunctionMethods == null)
				{
					ICollection<FunctionMethod> temp = AllFunctionMethods; // ensure the function methods are computed
				}

				return allFunctionMethods;
			}
		}

		public virtual ICollection<ProcedureMethod> AllProcedureMethods
		{
			get
			{
				if(allProcedureMethods == null)
				{
					allProcedureMethods = new LinkedHashMap<string, ProcedureMethod>();

					// add the members of the super types
					foreach(InheritanceType superType in AllSuperTypes)
						AddProcedureMethods(superType);

					// add members of the current type
					AddProcedureMethods(this);
				}

				return allProcedureMethods.Values;
			}
		}

		public virtual IDictionary<string, ProcedureMethod> AllProcedureMethodsByName
		{
			get
			{
				if(allProcedureMethods == null)
				{
					ICollection<ProcedureMethod> temp = AllProcedureMethods; // ensure the procedure methods are computed
				}

				return allProcedureMethods;
			}
		}

		public virtual bool SuperTypeDefinesFunctionMethod(FunctionMethod functionMethod)
		{
			foreach(InheritanceType superType in AllSuperTypes)
			{
				if(superType.AllFunctionMethodsByName.ContainsKey(functionMethod.Ident.ToString()))
					return true;
			}
			return false;
		}

		public virtual bool SuperTypeDefinesProcedureMethod(ProcedureMethod procedureMethod)
		{
			foreach(InheritanceType superType in AllSuperTypes)
			{
				if(superType.AllProcedureMethodsByName.ContainsKey(procedureMethod.Ident.ToString()))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the overridden member for a given member, if one exists. </summary>
		/// <param name="overridingMember"> The member, which eventually overrides another member. </param>
		/// <returns> The overridden member, or null, if no such exists. </returns>
		public virtual Entity GetOverriddenMember(Entity overridingMember)
		{
			return overridingMembers[overridingMember];
		}

		public virtual void AddConstructor(Constructor constr)
		{
			constructors.Add(constr);
		}

		public virtual ICollection<Constructor> Constructor
		{
			get
			{
				return constructors.AsReadOnly();
			}
		}

		/// <summary>
		/// Adds the given member initializer to this type. </summary>
		public virtual void AddMemberInit(MemberInit init)
		{
			memberInitializers.Add(init);
		}

		/// <returns> A collection containing all member initializers of this type. </returns>
		public virtual ICollection<MemberInit> MemberInits
		{
			get
			{
				return memberInitializers.AsReadOnly();
			}
		}

		public virtual void AddMapInit(MapInit init)
		{
			mapInitializers.Add(init);
		}

		public virtual ICollection<MapInit> MapInits
		{
			get
			{
				return mapInitializers.AsReadOnly();
			}
		}

		public virtual void AddSetInit(SetInit init)
		{
			setInitializers.Add(init);
		}

		public virtual ICollection<SetInit> SetInits
		{
			get
			{
				return setInitializers.AsReadOnly();
			}
		}

		public virtual void AddArrayInit(ArrayInit init)
		{
			arrayInitializers.Add(init);
		}

		public virtual ICollection<ArrayInit> ArrayInits
		{
			get
			{
				return arrayInitializers.AsReadOnly();
			}
		}

		public virtual void AddDequeInit(DequeInit init)
		{
			dequeInitializers.Add(init);
		}

		public virtual ICollection<DequeInit> DequeInits
		{
			get
			{
				return dequeInitializers.AsReadOnly();
			}
		}

		/// <summary>
		/// Check, if this type is a direct sub type of another type.
		/// This means, that this type inherited from the other type. </summary>
		/// <param name="t"> The other type. </param>
		/// <returns> true, iff this type inherited from <code>t</code>. </returns>
		public virtual bool IsDirectSubTypeOf(InheritanceType t)
		{
			return directSuperTypes.Contains(t);
		}

		/// <summary>
		/// Check, if this type is a direct super type of another type. </summary>
		/// <param name="t"> The other type </param>
		/// <returns> true, iff <code>t</code> inherits from this type. </returns>
		public virtual bool IsDirectSuperTypeOf(InheritanceType t)
		{
			return t.IsDirectSubTypeOf(this);
		}

		/// <summary>
		/// Check, if this inheritance type is castable to another one.
		/// This means, that this type must be a sub type <code>t</code>. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.castableTo(de.unika.ipd.grgen.ir.type.Type)"/>
		protected internal override bool CastableTo(Type t)
		{
			if(!(t is InheritanceType))
				return false;

			InheritanceType ty = (InheritanceType)t;

			if(IsDirectSubTypeOf(ty))
				return true;

			foreach(InheritanceType inh in DirectSuperTypes)
			{
				if(inh.CastableTo(ty))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Get the maximum distance to the root inheritance type.
		/// This method returns the length of the longest path (considering the inheritance
		/// relation) from this type to the root type. </summary>
		/// <returns> The length of the longest path to the root type. </returns>
		public int MaxDist
		{
			get
			{

				if(maxDist == -1)
				{
					maxDist = 0;

					foreach(InheritanceType inh in directSuperTypes)
					{
						int dist = inh.MaxDist + 1;
						maxDist = dist > maxDist ? dist : maxDist;
					}
				}

				return maxDist;
			}
		}

		public string ExternalName
		{
			get
			{
				return externalName;
			}
		}

		/// <summary>
		/// Check, if this type is abstract.
		/// If a type is abstract, no entities of this types may be instantiated.
		/// Its body must also be empty. </summary>
		/// <returns> true, if this type is abstract, false if not. </returns>
		public bool IsAbstract()
		{
			return (modifiers & ABSTRACT) != 0;
		}

		/// <summary>
		/// Check, if this type is const.
		/// Members of entities of a const type may not be modified. </summary>
		/// <returns> true, if this type is const, false if not. </returns>
		public bool IsConst()
		{
			return (modifiers & CONST) != 0;
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["inherits"] = directSuperTypes.GetEnumerator();
			fields["const"] = Convert.ToBoolean(IsConst());
			fields["abstract "] = Convert.ToBoolean(IsAbstract());
		}
	}

}
