/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// ModelNode.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.model.decl
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using ExternalFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ExternalFunctionDeclNode;
using ExternalProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ExternalProcedureDeclNode;
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
using ModelTypeNode = de.unika.ipd.grgen.ast.model.type.ModelTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Ident = de.unika.ipd.grgen.ir.Ident;
using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
using Index = de.unika.ipd.grgen.ir.model.Index;
using Model = de.unika.ipd.grgen.ir.model.Model;
using PackageType = de.unika.ipd.grgen.ir.model.type.PackageType;

public class ModelNode : DeclNode
{
	static ModelNode()
	{
		SetClassName(typeof(ModelNode), "model declaration");
	}

	private static readonly TypeNode modelType = new ModelTypeNode();

	private CollectNode<ModelNode> usedModels;

	private CollectNode<IdentNode> packagesUnresolved;
	protected internal CollectNode<TypeDeclNode> packages;
	private CollectNode<IdentNode> declsUnresolved;
	public CollectNode<TypeDeclNode> decls;
	private CollectNode<IdentNode> externalFuncDeclsUnresolved;
	protected internal CollectNode<ExternalFunctionDeclNode> externalFuncDecls;
	private CollectNode<IdentNode> externalProcDeclsUnresolved;
	protected internal CollectNode<ExternalProcedureDeclNode> externalProcDecls;
	private CollectNode<IdentNode> indicesUnresolved;
	protected internal CollectNode<IndexDeclNode> indices;
	private ModelTypeNode type;
	private bool isEmitClassDefined;
	private bool isEmitGraphClassDefined;
	private bool isCopyClassDefined;
	private bool isEqualClassDefined;
	private bool isLowerClassDefined;
	private bool isGraphofDefined;
	private bool isUniqueDefined;
	private bool isUniqueClassDefined;
	private bool isUniqueIndexDefined;
	private bool areFunctionsParallel;
	private int isoParallel;
	private int sequencesParallel;

	public ModelNode(IdentNode id, CollectNode<IdentNode> packages, CollectNode<IdentNode> decls,
			CollectNode<IdentNode> externalFuncs, CollectNode<IdentNode> externalProcs,
			CollectNode<IdentNode> indices, CollectNode<ModelNode> usedModels,
			bool isEmitClassDefined, bool isEmitGraphClassDefined, bool isCopyClassDefined,
			bool isEqualClassDefined, bool isLowerClassDefined, bool isGraphofDefined,
			bool isUniqueDefined, bool isUniqueClassDefined, bool isUniqueIndexDefined,
			bool areFunctionsParallel, int isoParallel, int sequencesParallel)
		: base(id, modelType)
	{

		this.packagesUnresolved = packages;
		BecomeParent(this.packagesUnresolved);
		this.declsUnresolved = decls;
		BecomeParent(this.declsUnresolved);
		this.externalFuncDeclsUnresolved = externalFuncs;
		BecomeParent(this.externalFuncDeclsUnresolved);
		this.externalProcDeclsUnresolved = externalProcs;
		BecomeParent(this.externalProcDeclsUnresolved);
		this.indicesUnresolved = indices;
		BecomeParent(this.indicesUnresolved);
		this.usedModels = usedModels;
		BecomeParent(this.usedModels);
		this.isEmitClassDefined = isEmitClassDefined;
		this.isEmitGraphClassDefined = isEmitGraphClassDefined;
		this.isCopyClassDefined = isCopyClassDefined;
		this.isEqualClassDefined = isEqualClassDefined;
		this.isLowerClassDefined = isLowerClassDefined;
		this.isGraphofDefined = isGraphofDefined;
		this.isUniqueDefined = isUniqueDefined;
		this.isUniqueClassDefined = isUniqueClassDefined;
		this.isUniqueIndexDefined = isUniqueIndexDefined;
		this.areFunctionsParallel = areFunctionsParallel;
		this.isoParallel = isoParallel;
		this.sequencesParallel = sequencesParallel;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ident);
		children.Add(GetValidVersion(typeUnresolved, type));
		children.Add(GetValidVersionCollectNode(packagesUnresolved, packages));
		children.Add(GetValidVersionCollectNode(declsUnresolved, decls));
		children.Add(GetValidVersionCollectNode(externalFuncDeclsUnresolved, externalFuncDecls));
		children.Add(GetValidVersionCollectNode(externalProcDeclsUnresolved, externalProcDecls));
		children.Add(GetValidVersionCollectNode(indicesUnresolved, indices));
		children.Add(usedModels);
		return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("ident");
		childrenNames.Add("type");
		childrenNames.Add("packages");
		childrenNames.Add("decls");
		childrenNames.Add("externalFuncDecls");
		childrenNames.Add("externalProcDecls");
		childrenNames.Add("indices");
		childrenNames.Add("usedModels");
		return childrenNames;
		}
	}

	private static CollectResolver<TypeDeclNode> packagesResolver = new CollectResolver<TypeDeclNode>(
			new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode)));
	private static CollectResolver<TypeDeclNode> declsResolver = new CollectResolver<TypeDeclNode>(
			new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode)));
	private static CollectResolver<IndexDeclNode> indicesResolver = new CollectResolver<IndexDeclNode>(
			new DeclarationResolver<IndexDeclNode>(typeof(IndexDeclNode)));
	private static CollectResolver<ExternalFunctionDeclNode> externalFunctionsResolver = new CollectResolver<ExternalFunctionDeclNode>(
			new DeclarationResolver<ExternalFunctionDeclNode>(typeof(ExternalFunctionDeclNode)));
	private static CollectResolver<ExternalProcedureDeclNode> externalProceduresResolver = new CollectResolver<ExternalProcedureDeclNode>(
			new DeclarationResolver<ExternalProcedureDeclNode>(typeof(ExternalProcedureDeclNode)));

	private static DeclarationTypeResolver<ModelTypeNode> typeResolver =
			new DeclarationTypeResolver<ModelTypeNode>(typeof(ModelTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		if(isLowerClassDefined)
		{
			OperatorDeclNode.MakeBinOp(Operator.GE, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.GT, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LE, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LT, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
		}

		packages = packagesResolver.Resolve(packagesUnresolved, this);
		decls = declsResolver.Resolve(declsUnresolved, this);
		indices = indicesResolver.Resolve(indicesUnresolved, this);
		externalFuncDecls = externalFunctionsResolver.Resolve(externalFuncDeclsUnresolved, this);
		externalProcDecls = externalProceduresResolver.Resolve(externalProcDeclsUnresolved, this);
		type = typeResolver.Resolve(typeUnresolved, this);

		return decls != null && externalFuncDecls != null && externalProcDecls != null && type != null;
	}

	/// <summary>
	/// The main node has an ident node and a collect node with
	/// - group declarations
	/// - edge class decls
	/// - node class decls
	/// - object class decls
	/// - transient object class decls
	/// as child. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		return CheckInhCycleFree() && EqualityMustBeDefinedIfLowerIsDefined();
	}

	public virtual bool IsEmitClassDefined()
	{
		return isEmitClassDefined;
	}

	public virtual bool IsEmitGraphClassDefined()
	{
		return isEmitGraphClassDefined;
	}

	public virtual bool IsCopyClassDefined()
	{
		return isCopyClassDefined;
	}

	public virtual bool IsEqualClassDefined()
	{
		return isEqualClassDefined;
	}

	public virtual bool IsLowerClassDefined()
	{
		return isLowerClassDefined;
	}

	public virtual bool IsGraphofDefined()
	{
		return isGraphofDefined;
	}

	public virtual bool IsUniqueDefined()
	{
		return isUniqueDefined;
	}

	public virtual bool IsUniqueClassDefined()
	{
		return isUniqueClassDefined;
	}

	public virtual bool IsUniqueIndexDefined()
	{
		return isUniqueIndexDefined;
	}

	public virtual bool AreFunctionsParallel()
	{
		return areFunctionsParallel;
	}

	public virtual int IsoParallel()
	{
		return isoParallel;
	}

	public virtual int SequencesParallel()
	{
		return sequencesParallel;
	}

	public virtual CollectNode<TypeDeclNode> TypeDecls
	{
		get
		{
		return decls;
		}
	}

	public virtual CollectNode<IndexDeclNode> Indices
	{
		get
		{
		return indices;
		}
	}

	public virtual CollectNode<ModelNode> UsedModels
	{
		get
		{
		return usedModels;
		}
	}

	public virtual CollectNode<TypeDeclNode> Packages
	{
		get
		{
		return packages;
		}
	}

	/// <summary>
	/// Get the IR model node for this AST node. </summary>
	/// <returns> The model for this AST node. </returns>
	public virtual Model IRModel
	{
		get
		{
		return CheckIR(typeof(Model));
		}
	}

	/// <summary>
	/// Construct the IR object for this AST node.
	/// For a main node, this is a unit. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		Ident id = ident.CheckIR(typeof(Ident));
		Model res = new Model(id, isEmitClassDefined, isEmitGraphClassDefined, isCopyClassDefined,
				isEqualClassDefined, isLowerClassDefined, isGraphofDefined,
				isUniqueDefined, isUniqueClassDefined, isUniqueIndexDefined,
				areFunctionsParallel, isoParallel, sequencesParallel);
		foreach(ModelNode model in usedModels.ChildrenExact)
			res.AddUsedModel(model.IRModel);
		foreach(TypeDeclNode typeDecl in packages.ChildrenExact)
			res.AddPackage((PackageType)typeDecl.DeclType.IRType);
		foreach(TypeDeclNode typeDecl in decls.ChildrenExact)
			res.AddType(typeDecl.DeclType.IRType);
		foreach(IndexDeclNode indexDecl in indices.ChildrenExact)
			res.AddIndex(indexDecl.CheckIR(typeof(Index)));
		foreach(ExternalFunctionDeclNode externalFunctionDecl in externalFuncDecls.ChildrenExact)
			res.AddExternalFunction(externalFunctionDecl.CheckIR(typeof(ExternalFunction)));
		foreach(ExternalProcedureDeclNode externalProcedureDecl in externalProcDecls.ChildrenExact)
			res.AddExternalProcedure(externalProcedureDecl.CheckIR(typeof(ExternalProcedure)));
		return res;
	}

	private bool CheckInhCycleFreeRec(InheritanceTypeNode inhType,
			ICollection<BaseNode> inProgress, ICollection<BaseNode> done)
	{
		inProgress.Add(inhType);
		foreach(InheritanceTypeNode superType in inhType.DirectSuperTypes)
		{
			Debug.Assert((((inhType is NodeTypeNode) && (superType is NodeTypeNode)) ||
				((inhType is EdgeTypeNode) && (superType is EdgeTypeNode)) ||
				((inhType is InternalObjectTypeNode) && (superType is InternalObjectTypeNode)) ||
				((inhType is InternalTransientObjectTypeNode) && (superType is InternalTransientObjectTypeNode)) ||
				((inhType is ExternalObjectTypeNode) && (superType is ExternalObjectTypeNode))),
			"nodes should extend nodes and edges should extend edges");

			if(inProgress.Contains(superType))
			{
				inhType.Ident.ReportError("The class " + inhType.TypeName
					+ " extends " + superType.ToStringWithDeclarationCoords()
					+ " - this introduces a cycle into the type hierarchy.");
				return false;
			}
			if(!done.Contains(superType))
			{
				if(!CheckInhCycleFreeRec(superType, inProgress, done))
					return false;
			}
		}
		inProgress.Remove(inhType);
		done.Add(inhType);
		return true;
	}

	/// <summary>
	/// ensure there are no cycles in the inheritance hierarchy
	/// @return	<code>true</code> if there are no cycles,
	/// 			<code>false</code> otherwise
	/// </summary>
	private bool CheckInhCycleFree()
	{
		ICollection<TypeDeclNode> coll = decls.ChildrenExact;
		foreach(TypeDeclNode t in coll)
		{
			TypeNode type = t.DeclType;

			if(!(type is InheritanceTypeNode))
				continue;

			ICollection<BaseNode> inProgress = new HashSet<BaseNode>();
			ICollection<BaseNode> done = new HashSet<BaseNode>();

			bool isCycleFree = CheckInhCycleFreeRec((InheritanceTypeNode)type, inProgress, done);

			if(!isCycleFree)
				return false;
		}
		return true;
	}

	private bool EqualityMustBeDefinedIfLowerIsDefined()
	{
		if(isLowerClassDefined)
		{
			if(!isEqualClassDefined)
			{
				ReportError("A \"< class;\" requires a \"== class;\"");
				return false;
			}
		}
		return true;
	}

	public override TypeNode DeclType
	{
		get
		{
		Debug.Assert(IsResolved());

		return type;
		}
	}
}

}
