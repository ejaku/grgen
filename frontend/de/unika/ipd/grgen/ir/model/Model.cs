/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Model.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.model
{

	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;

	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
	using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
	using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using ExternalObjectType = de.unika.ipd.grgen.ir.model.type.ExternalObjectType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;
	using InternalTransientObjectType = de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using PackageType = de.unika.ipd.grgen.ir.model.type.PackageType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using PrimitiveType = de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

	public class Model : Identifiable, NodeEdgeEnumBearer
	{
		private List<Model> usedModels = new List<Model>();
		private List<PackageType> packages = new List<PackageType>();
		private List<Type> types = new List<Type>();

		private ISet<NodeType> nodeTypes = new LinkedHashSet<NodeType>();
		private ISet<EdgeType> edgeTypes = new LinkedHashSet<EdgeType>();
		private ISet<InternalObjectType> objectTypes = new LinkedHashSet<InternalObjectType>();
		private ISet<InternalTransientObjectType> transientObjectTypes = new LinkedHashSet<InternalTransientObjectType>();
		private ISet<EnumType> enumTypes = new LinkedHashSet<EnumType>();
		private ISet<Index> indices = new LinkedHashSet<Index>();
		private ISet<ExternalObjectType> externalObjectTypes = new LinkedHashSet<ExternalObjectType>();
		private ISet<ExternalFunction> externalFuncs = new LinkedHashSet<ExternalFunction>();
		private ISet<ExternalProcedure> externalProcs = new LinkedHashSet<ExternalProcedure>();
		private bool isEmitClassDefined_;
		private bool isEmitGraphClassDefined_;
		private bool isCopyClassDefined_;
		private bool isEqualClassDefined_;
		private bool isLowerClassDefined_;
		private bool isGraphofDefined_;
		private bool isUniqueDefined_;
		private bool isUniqueResulting_;
		private bool isUniqueClassDefined_;
		private bool isUniqueIndexDefined_;
		private bool areFunctionsParallel_;
		private int isoParallel;
		private int sequencesParallel;
		private List<NodeType> allNodeTypes;
		private List<EdgeType> allEdgeTypes;
		private List<InternalObjectType> allObjectTypes;
		private List<InternalTransientObjectType> allTransientObjectTypes;
		private List<InheritanceType> allGraphElementTypes;
		private List<InheritanceType> allInheritanceTypes;

		public Model(Ident ident, bool isEmitClassDefined, bool isEmitGraphClassDefined, bool isCopyClassDefined,
				bool isEqualClassDefined, bool isLowerClassDefined, bool isGraphofDefined,
				bool isUniqueDefined, bool isUniqueClassDefined, bool isUniqueIndexDefined,
				bool areFunctionsParallel, int isoParallel, int sequencesParallel)
			: base("model", ident)
		{

			this.isEmitClassDefined_ = isEmitClassDefined;
			this.isEmitGraphClassDefined_ = isEmitGraphClassDefined;
			this.isCopyClassDefined_ = isCopyClassDefined;
			this.isEqualClassDefined_ = isEqualClassDefined;
			this.isLowerClassDefined_ = isLowerClassDefined;
			this.isGraphofDefined_ = isGraphofDefined;
			this.isUniqueDefined_ = isUniqueDefined;
			this.isUniqueResulting_ = isUniqueDefined;
			this.isUniqueClassDefined_ = isUniqueClassDefined;
			this.isUniqueIndexDefined_ = isUniqueIndexDefined;
			this.areFunctionsParallel_ = areFunctionsParallel;
			this.isoParallel = isoParallel;
			this.sequencesParallel = sequencesParallel;
		}

		public virtual void AddUsedModel(Model model)
		{
			usedModels.Add(model);
			foreach(Type type in model.Types)
				AddType(type);
			foreach(PackageType pack in model.Packages)
				AddPackage(pack);
			foreach(ExternalFunction externalFunc in model.ExternalFunctions)
				AddExternalFunction(externalFunc);
		}

		public virtual void AddPackage(PackageType p)
		{
			packages.Add(p);
		}

		public virtual ICollection<PackageType> Packages
		{
			get
			{
				return packages.AsReadOnly();
			}
		}

		/// <summary>
		/// Add the given type to the type model. </summary>
		public virtual void AddType(Type type)
		{
			types.Add(type);
			if(type is NodeType)
				nodeTypes.Add((NodeType)type);
			else if(type is EdgeType)
				edgeTypes.Add((EdgeType)type);
			else if(type is EnumType)
				enumTypes.Add((EnumType)type);
			else if(type is ExternalObjectType)
				externalObjectTypes.Add((ExternalObjectType)type);
			else if(type is InternalObjectType)
				objectTypes.Add((InternalObjectType)type);
			else if(type is InternalTransientObjectType)
				transientObjectTypes.Add((InternalTransientObjectType)type);
			else if(!(type is PrimitiveType))
				Debug.Assert(false, "Unexpected type added to model: " + type);
		}

		public virtual void AddIndex(Index index)
		{
			indices.Add(index);
		}

		public virtual ICollection<Index> Indices
		{
			get
			{
				return indices; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual void AddExternalFunction(ExternalFunction externalFunc)
		{
			externalFuncs.Add(externalFunc);
		}

		public virtual ICollection<ExternalFunction> ExternalFunctions
		{
			get
			{
				return externalFuncs; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual void AddExternalProcedure(ExternalProcedure externalProc)
		{
			externalProcs.Add(externalProc);
		}

		public virtual ICollection<ExternalProcedure> ExternalProcedures
		{
			get
			{
				return externalProcs; // TODO: Collections.UnmodifiableSet
			}
		}

		/// <returns> The types in the type model. </returns>
		public virtual ICollection<Type> Types
		{
			get
			{
				return types.AsReadOnly();
			}
		}

		public virtual ICollection<NodeType> NodeTypes
		{
			get
			{
				return nodeTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<NodeType> AllNodeTypes
		{
			get
			{
				if(allNodeTypes == null)
				{
					List<NodeType> allNodeTypes = new List<NodeType>();
					allNodeTypes.AddRange(NodeTypes);
					foreach(PackageType pt in Packages)
						allNodeTypes.AddRange(pt.NodeTypes);
					int typeID = 0;
					foreach(NodeType nt in allNodeTypes)
					{
						nt.InheritanceTypeID = typeID;
						++typeID;
					}
					this.allNodeTypes = allNodeTypes;
				}
				return allNodeTypes.AsReadOnly();
			}
		}

		public virtual ICollection<EdgeType> EdgeTypes
		{
			get
			{
				return edgeTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<EdgeType> AllEdgeTypes
		{
			get
			{
				if(allEdgeTypes == null)
				{
					List<EdgeType> allEdgeTypes = new List<EdgeType>();
					allEdgeTypes.AddRange(EdgeTypes);
					foreach(PackageType pt in Packages)
						allEdgeTypes.AddRange(pt.EdgeTypes);
					int typeID = 0;
					foreach(EdgeType et in allEdgeTypes)
					{
						et.InheritanceTypeID = typeID;
						++typeID;
					}
					this.allEdgeTypes = allEdgeTypes;
				}
				return allEdgeTypes.AsReadOnly();
			}
		}

		public virtual ICollection<InheritanceType> AllGraphElementTypes
		{
			get
			{
				if(allGraphElementTypes == null)
				{
					List<InheritanceType> allNodeAndEdgeTypes = new List<InheritanceType>();
					allNodeAndEdgeTypes.AddRange(AllNodeTypes);
					allNodeAndEdgeTypes.AddRange(AllEdgeTypes);
					this.allGraphElementTypes = allNodeAndEdgeTypes;
				}
				return allGraphElementTypes.AsReadOnly();
			}
		}

		public virtual ICollection<InternalObjectType> ObjectTypes
		{
			get
			{
				return objectTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<InternalObjectType> AllObjectTypes
		{
			get
			{
				if(allObjectTypes == null)
				{
					List<InternalObjectType> allObjectTypes = new List<InternalObjectType>();
					allObjectTypes.AddRange(ObjectTypes);
					foreach(PackageType pt in Packages)
						allObjectTypes.AddRange(pt.ObjectTypes);
					int typeID = 0;
					foreach(InternalObjectType ot in allObjectTypes)
					{
						ot.InheritanceTypeID = typeID;
						++typeID;
					}
					this.allObjectTypes = allObjectTypes;
				}
				return allObjectTypes.AsReadOnly();
			}
		}

		public virtual ICollection<InternalTransientObjectType> TransientObjectTypes
		{
			get
			{
				return transientObjectTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<InternalTransientObjectType> AllTransientObjectTypes
		{
			get
			{
				if(allTransientObjectTypes == null)
				{
					List<InternalTransientObjectType> allTransientObjectTypes = new List<InternalTransientObjectType>();
					allTransientObjectTypes.AddRange(TransientObjectTypes);
					foreach(PackageType pt in Packages)
						allTransientObjectTypes.AddRange(pt.TransientObjectTypes);
					int typeID = 0;
					foreach(InternalTransientObjectType ot in allTransientObjectTypes)
					{
						ot.InheritanceTypeID = typeID;
						++typeID;
					}
					this.allTransientObjectTypes = allTransientObjectTypes;
				}
				return allTransientObjectTypes.AsReadOnly();
			}
		}

		public virtual ICollection<InheritanceType> AllInheritanceTypes
		{
			get
			{
				if(allInheritanceTypes == null)
				{
					List<InheritanceType> allInheritanceTypes = new List<InheritanceType>();
					allInheritanceTypes.AddRange(AllNodeTypes);
					allInheritanceTypes.AddRange(AllEdgeTypes);
					allInheritanceTypes.AddRange(AllObjectTypes);
					allInheritanceTypes.AddRange(AllTransientObjectTypes);
					this.allInheritanceTypes = allInheritanceTypes;
				}
				return allInheritanceTypes.AsReadOnly();
			}
		}

		public virtual ICollection<EnumType> EnumTypes
		{
			get
			{
				return enumTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<ExternalObjectType> ExternalObjectTypes
		{
			get
			{
				return externalObjectTypes; // TODO: Collections.UnmodifiableSet
			}
		}

		public virtual ICollection<Model> UsedModels
		{
			get
			{
				return usedModels.AsReadOnly();
			}
		}

		public virtual bool IsEmitClassDefined()
		{
			return isEmitClassDefined_;
		}

		public virtual bool IsEmitGraphClassDefined()
		{
			return isEmitGraphClassDefined_;
		}

		public virtual bool IsCopyClassDefined()
		{
			return isCopyClassDefined_;
		}

		public virtual bool IsEqualClassDefined()
		{
			return isEqualClassDefined_;
		}

		public virtual bool IsLowerClassDefined()
		{
			return isLowerClassDefined_;
		}

		public virtual bool IsGraphofDefined()
		{
			return isGraphofDefined_;
		}

		public virtual bool IsUniqueDefined()
		{
			return isUniqueDefined_;
		}

		public virtual void ForceUniqueDefined()
		{
			isUniqueDefined_ = true;
		}

		public virtual bool IsUniqueResulting()
		{
			return isUniqueResulting_;
		}

		public virtual void ForceUniqueResulting()
		{
			isUniqueResulting_ = true;
		}

		public virtual bool IsUniqueClassDefined()
		{
			return isUniqueClassDefined_;
		}

		public virtual bool IsUniqueIndexDefined()
		{
			return isUniqueIndexDefined_;
		}

		public virtual void ForceFunctionsParallel()
		{
			areFunctionsParallel_ = true;
		}

		public virtual bool AreFunctionsParallel()
		{
			return areFunctionsParallel_;
		}

		public virtual int IsoParallel
		{
			get
			{
				return isoParallel;
			}
		}

		public virtual int SequencesParallel
		{
			get
			{
				return sequencesParallel;
			}
		}

		/// <summary>
		/// Canonicalize the type model. </summary>
		protected internal override void CanonicalizeLocal()
		{
			//Collections.sort(types, Identifiable.COMPARATOR);
			//Collections.sort(types);

			foreach(Type ty in types)
			{
				ty.Canonicalize();
				if(ty is EdgeType)
					((EdgeType)ty).CanonicalizeConnectionAsserts();
			}
		}

		public virtual void AddToDigest(StringBuilder sb)
		{
			sb.Append(this);
			sb.Append('[');

			foreach(Model model in usedModels)
				model.AddToDigest(sb);

			foreach(Type ty in types)
				ty.AddToDigest(sb);

			sb.Append(']');
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["usedModels"] = usedModels.GetEnumerator();
			fields["types"] = types.GetEnumerator();
		}
	}

}
