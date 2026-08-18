/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{

	using System.Collections.Generic;

	using CopyKind = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode.CopyKind;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Annotations = de.unika.ipd.grgen.util.Annotations;

	/// <summary>
	/// Abstract base class for entities occurring in graphs
	/// </summary>
	public abstract class GraphEntity : Entity
	{
		/// <summary>
		/// Type of the entity. </summary>
		protected internal readonly new InheritanceType type;

		/// <summary>
		/// The annotations of this entity. </summary>
		protected internal readonly Annotations annotations;

		/// <summary>
		/// The retyped version of this entity if any. </summary>
		protected internal Dictionary<PatternGraphBase, GraphEntity> retyped = null;

		/// <summary>
		/// The entity from which this one will inherit its dynamic type </summary>
		protected internal GraphEntity @typeof = null;
		protected internal CopyKind copyKind = CopyKind.None;

		/// <summary>
		/// The interface type of the parameter if any. </summary>
		protected internal InheritanceType parameterInterfaceType = null;

		/// <summary>
		/// The storage from which to get the node or edge, if any (i.e. not null) </summary>
		public StorageAccess storageAccess = null;

		/// <summary>
		/// The index to the storage from which to get the node or edge, if any (i.e. not null) </summary>
		public StorageAccessIndex storageAccessIndex = null;

		/// <summary>
		/// The index from which to get the node or edge, if any (i.e. not null) </summary>
		public IndexAccess indexAccess = null;

		/// <summary>
		/// The indices from which to get the node or edge when using a multiple index query, if any (i.e. not empty) </summary>
		public IList<IndexAccessOrdering> multipleIndexAccesses = new List<IndexAccessOrdering>();

		/// <summary>
		/// The name map access used to get the node or edge, if any (i.e. not null) </summary>
		public NameLookup nameMapAccess = null;

		/// <summary>
		/// The unique index access used to get the node or edge, if any (i.e. not null) </summary>
		public UniqueLookup uniqueIndexAccess = null;

		protected internal ISet<InheritanceType> constraints = MyCollectionHelper.CreateEmptySet<InheritanceType>();

		private bool maybeDeleted;
		private bool maybeRetyped;

		// null or an expression used to initialize the node
		public Expression initialization;

		public IList<NameOrAttributeInitialization> nameOrAttributeInitialization = new List<NameOrAttributeInitialization>();

		/// <summary>
		/// Dependencies because of match by storage access (element must be matched before storage map access with it) </summary>
		protected internal int dependencyLevel = 0;

		/// <summary>
		/// Make a new graph entity of a given type. </summary>
		/// <param name="name"> The name of the entity. </param>
		/// <param name="ident"> The declaring identifier. </param>
		/// <param name="type"> The type used in the declaration. </param>
		/// <param name="maybeDeleted"> Indicates whether this element might be deleted due to homomorphy. </param>
		/// <param name="maybeRetyped"> Indicates whether this element might be retyped due to homomorphy. </param>
		/// <param name="isDefToBeYieldedTo"> Is the entity a defined entity only, to be filled with yields from nested patterns. </param>
		/// <param name="context"> The context of the declaration. </param>
		protected internal GraphEntity(string name, Ident ident, InheritanceType type, Annotations annots,
				bool maybeDeleted, bool maybeRetyped, bool isDefToBeYieldedTo, int context)
			: base(name, ident, type, false, isDefToBeYieldedTo, context)
		{
			ChildrenNames = childrenNames;
			this.type = type;
			this.annotations = annots;
			this.maybeDeleted = maybeDeleted;
			this.maybeRetyped = maybeRetyped;
			this.context = context;
		}

		public virtual InheritanceType InheritanceType
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// Sets the entity this one inherits its dynamic type from </summary>
		public virtual void SetTypeofCopy(GraphEntity @typeof, CopyKind copyKind)
		{
			this.@typeof = @typeof;
			this.copyKind = copyKind;
		}

		/// <summary>
		/// Sets the type constraints for this entity </summary>
		public virtual ISet<InheritanceType> Constraints
		{
			set
			{
				this.constraints = value;
			}
			get
			{
				return constraints; // TODO: Collections.UnmodifiableSet
			}
		}


		/// <returns> The annotations. </returns>
		public override Annotations Annotations
		{
			get
			{
				return annotations;
			}
		}

		public virtual bool IsMaybeDeleted()
		{
			return maybeDeleted;
		}

		public virtual bool IsMaybeRetyped()
		{
			return maybeRetyped;
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["valid_types"] = constraints.GetEnumerator();
			fields["retyped"] = MyCollectionHelper.CreateSingletonSet(retyped);
			fields["typeof"] = MyCollectionHelper.CreateSingletonSet(@typeof);
		}

		/// <returns> true, if this is a retyped entity, i.e. the result of a retype, else false </returns>
		public override bool IsRetyped()
		{
			return false;
		}

		/// <returns> true, if this entity changes its type </returns>
		/// <param name="patternGraph"> The pattern graph where the entity is queried to change its type;
		/// if null any pattern graph will match, i.e. return is true as soon as one pattern graph exists where type changes </param>
		public virtual bool ChangesType(PatternGraphBase patternGraph)
		{
			if(patternGraph == null)
				return this.retyped != null;
			return GetRetypedEntity(patternGraph) != null;
		}

		/// <summary>
		/// Sets the corresponding retyped version of this entity </summary>
		/// <param name="retyped"> The retyped version </param>
		/// <param name="patternGraph"> The pattern graph where the entity gets retyped </param>
		public virtual void SetRetypedEntity(GraphEntity retyped, PatternGraphBase patternGraph)
		{
			if(this.retyped == null)
				this.retyped = new Dictionary<PatternGraphBase, GraphEntity>();
			this.retyped[patternGraph] = retyped;
		}

		/// <summary>
		/// Returns the corresponding retyped version of this entity </summary>
		/// <param name="patternGraph"> The pattern graph where the entity might get retyped </param>
		/// <returns> The retyped version or <code>null</code> </returns>
		public virtual GraphEntity GetRetypedEntity(PatternGraphBase patternGraph)
		{
			if(this.retyped == null)
				return null;
			return this.retyped[patternGraph];
		}

		/// <summary>
		/// Get the entity from which this entity inherits its dynamic type </summary>
		public virtual GraphEntity Typeof
		{
			get
			{
				return @typeof;
			}
		}

		/// <summary>
		/// returns whether the inherited type / typeof is the extended version in fact,
		/// named copy, copying the attributes too  
		/// </summary>
		public virtual CopyKind Copy
		{
			get
			{
				return copyKind;
			}
		}

		/// <returns> true, if this entity inherits its type from some other entitiy </returns>
		public virtual bool InheritsType()
		{
			return @typeof != null;
		}

		public virtual InheritanceType ParameterInterfaceType
		{
			set
			{
				parameterInterfaceType = value;
			}
			get
			{
				return parameterInterfaceType;
			}
		}


		public virtual StorageAccess Storage
		{
			set
			{
				this.storageAccess = value;
			}
		}

		public virtual StorageAccessIndex StorageIndex
		{
			set
			{
				this.storageAccessIndex = value;
			}
		}

		public virtual IndexAccess Index
		{
			set
			{
				this.indexAccess = value;
			}
		}

		public virtual void AddIndex(IndexAccessOrdering indexAccess)
		{
			this.multipleIndexAccesses.Add(indexAccess);
		}

		public virtual NameLookup NameMapAccess
		{
			set
			{
				this.nameMapAccess = value;
			}
		}

		public virtual UniqueLookup UniqueIndexAccess
		{
			set
			{
				this.uniqueIndexAccess = value;
			}
		}

		public virtual Expression Initialization
		{
			set
			{
				this.initialization = value;
			}
		}

		public virtual void AddNameOrAttributeInitialization(NameOrAttributeInitialization nai)
		{
			this.nameOrAttributeInitialization.Add(nai);
		}

		public virtual bool HasNameInitialization()
		{
			foreach(NameOrAttributeInitialization nai in nameOrAttributeInitialization)
			{
				if(nai.attribute == null)
					return true;
			}
			return false;
		}

		public virtual NameOrAttributeInitialization NameInitialization
		{
			get
			{
				foreach(NameOrAttributeInitialization nai in nameOrAttributeInitialization)
				{
					if(nai.attribute == null)
						return nai;
				}
				return null;
			}
		}

		public virtual bool HasAttributeInitialization()
		{
			foreach(NameOrAttributeInitialization nai in nameOrAttributeInitialization)
			{
				if(nai.attribute != null)
					return true;
			}
			return false;
		}

		public virtual void IncrementDependencyLevel()
		{
			++dependencyLevel;
		}

		public virtual int DependencyLevel
		{
			get
			{
				return dependencyLevel;
			}
		}

		public override string NodeInfo
		{
			get
			{
				return base.NodeInfo
						+ "\nconstraints: " + Constraints;
			}
		}
	}

}
