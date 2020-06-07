/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ModelNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.model.decl;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ExternalFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ExternalProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.ExternalTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.ModelTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.executable.ExternalProcedure;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.type.PackageType;

public class ModelNode extends DeclNode
{
	static {
		setName(ModelNode.class, "model declaration");
	}

	private static final TypeNode modelType = new ModelTypeNode();

	private CollectNode<ModelNode> usedModels;

	private CollectNode<IdentNode> packagesUnresolved;
	protected CollectNode<TypeDeclNode> packages;
	private CollectNode<IdentNode> declsUnresolved;
	public CollectNode<TypeDeclNode> decls;
	private CollectNode<IdentNode> externalFuncDeclsUnresolved;
	protected CollectNode<ExternalFunctionDeclNode> externalFuncDecls;
	private CollectNode<IdentNode> externalProcDeclsUnresolved;
	protected CollectNode<ExternalProcedureDeclNode> externalProcDecls;
	private CollectNode<IdentNode> indicesUnresolved;
	protected CollectNode<IndexDeclNode> indices;
	private ModelTypeNode type;
	private boolean isEmitClassDefined;
	private boolean isEmitGraphClassDefined;
	private boolean isCopyClassDefined;
	private boolean isEqualClassDefined;
	private boolean isLowerClassDefined;
	private boolean isUniqueDefined;
	private boolean isUniqueIndexDefined;
	private boolean areFunctionsParallel;
	private int isoParallel;

	public ModelNode(IdentNode id, CollectNode<IdentNode> packages, CollectNode<IdentNode> decls,
			CollectNode<IdentNode> externalFuncs, CollectNode<IdentNode> externalProcs,
			CollectNode<IdentNode> indices, CollectNode<ModelNode> usedModels,
			boolean isEmitClassDefined, boolean isEmitGraphClassDefined, boolean isCopyClassDefined,
			boolean isEqualClassDefined, boolean isLowerClassDefined,
			boolean isUniqueDefined, boolean isUniqueIndexDefined,
			boolean areFunctionsParallel, int isoParallel)
	{
		super(id, modelType);

		this.packagesUnresolved = packages;
		becomeParent(this.packagesUnresolved);
		this.declsUnresolved = decls;
		becomeParent(this.declsUnresolved);
		this.externalFuncDeclsUnresolved = externalFuncs;
		becomeParent(this.externalFuncDeclsUnresolved);
		this.externalProcDeclsUnresolved = externalProcs;
		becomeParent(this.externalProcDeclsUnresolved);
		this.indicesUnresolved = indices;
		becomeParent(this.indicesUnresolved);
		this.usedModels = usedModels;
		becomeParent(this.usedModels);
		this.isEmitClassDefined = isEmitClassDefined;
		this.isEmitGraphClassDefined = isEmitGraphClassDefined;
		this.isCopyClassDefined = isCopyClassDefined;
		this.isEqualClassDefined = isEqualClassDefined;
		this.isLowerClassDefined = isLowerClassDefined;
		this.isUniqueDefined = isUniqueDefined;
		this.isUniqueIndexDefined = isUniqueIndexDefined;
		this.areFunctionsParallel = areFunctionsParallel;
		this.isoParallel = isoParallel;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(packagesUnresolved, packages));
		children.add(getValidVersion(declsUnresolved, decls));
		children.add(getValidVersion(externalFuncDeclsUnresolved, externalFuncDecls));
		children.add(getValidVersion(externalProcDeclsUnresolved, externalProcDecls));
		children.add(getValidVersion(indicesUnresolved, indices));
		children.add(usedModels);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("packages");
		childrenNames.add("decls");
		childrenNames.add("externalFuncDecls");
		childrenNames.add("externalProcDecls");
		childrenNames.add("indices");
		childrenNames.add("usedModels");
		return childrenNames;
	}

	private static CollectResolver<TypeDeclNode> packagesResolver = new CollectResolver<TypeDeclNode>(
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));
	private static CollectResolver<TypeDeclNode> declsResolver = new CollectResolver<TypeDeclNode>(
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));
	private static CollectResolver<IndexDeclNode> indicesResolver = new CollectResolver<IndexDeclNode>(
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class));
	private static CollectResolver<ExternalFunctionDeclNode> externalFunctionsResolver = new CollectResolver<ExternalFunctionDeclNode>(
			new DeclarationResolver<ExternalFunctionDeclNode>(ExternalFunctionDeclNode.class));
	private static CollectResolver<ExternalProcedureDeclNode> externalProceduresResolver = new CollectResolver<ExternalProcedureDeclNode>(
			new DeclarationResolver<ExternalProcedureDeclNode>(ExternalProcedureDeclNode.class));

	private static DeclarationTypeResolver<ModelTypeNode> typeResolver =
			new DeclarationTypeResolver<ModelTypeNode>(ModelTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(isLowerClassDefined) {
			OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.GE, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
			OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.GT, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
			OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.LE, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
			OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.LT, BasicTypeNode.booleanType,
					BasicTypeNode.objectType, BasicTypeNode.objectType, OperatorEvaluator.objectEvaluator);
		}

		packages = packagesResolver.resolve(packagesUnresolved, this);
		decls = declsResolver.resolve(declsUnresolved, this);
		indices = indicesResolver.resolve(indicesUnresolved, this);
		externalFuncDecls = externalFunctionsResolver.resolve(externalFuncDeclsUnresolved, this);
		externalProcDecls = externalProceduresResolver.resolve(externalProcDeclsUnresolved, this);
		type = typeResolver.resolve(typeUnresolved, this);

		return decls != null && externalFuncDecls != null && externalProcDecls != null && type != null;
	}

	/**
	 * The main node has an ident node and a collect node with
	 * - group declarations
	 * - edge class decls
	 * - node class decls
	 * as child.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		return checkInhCycleFree() && equalityMustBeDefinedIfLowerIsDefined();
	}

	public boolean IsEmitClassDefined()
	{
		return isEmitClassDefined;
	}

	public boolean IsEmitGraphClassDefined()
	{
		return isEmitGraphClassDefined;
	}

	public boolean IsCopyClassDefined()
	{
		return isCopyClassDefined;
	}

	public boolean IsEqualClassDefined()
	{
		return isEqualClassDefined;
	}

	public boolean IsLowerClassDefined()
	{
		return isLowerClassDefined;
	}

	public boolean IsUniqueDefined()
	{
		return isUniqueDefined;
	}

	public boolean IsUniqueIndexDefined()
	{
		return isUniqueIndexDefined;
	}

	public boolean AreFunctionsParallel()
	{
		return areFunctionsParallel;
	}

	public int IsoParallel()
	{
		return isoParallel;
	}

	public CollectNode<TypeDeclNode> getTypeDecls()
	{
		return decls;
	}

	public CollectNode<IndexDeclNode> getIndices()
	{
		return indices;
	}

	public CollectNode<ModelNode> getUsedModels()
	{
		return usedModels;
	}

	public CollectNode<TypeDeclNode> getPackages()
	{
		return packages;
	}

	/**
	 * Get the IR model node for this AST node.
	 * @return The model for this AST node.
	 */
	public Model getModel()
	{
		return checkIR(Model.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected Model constructIR()
	{
		Ident id = ident.checkIR(Ident.class);
		Model res = new Model(id, isEmitClassDefined, isEmitGraphClassDefined, isCopyClassDefined,
				isEqualClassDefined, isLowerClassDefined,
				isUniqueDefined, isUniqueIndexDefined,
				areFunctionsParallel, isoParallel);
		for(ModelNode model : usedModels.getChildren()) {
			res.addUsedModel(model.getModel());
		}
		for(TypeDeclNode typeDecl : packages.getChildren()) {
			res.addPackage((PackageType)typeDecl.getDeclType().getType());
		}
		for(TypeDeclNode typeDecl : decls.getChildren()) {
			res.addType(typeDecl.getDeclType().getType());
		}
		for(IndexDeclNode indexDecl : indices.getChildren()) {
			res.addIndex(indexDecl.checkIR(Index.class));
		}
		for(ExternalFunctionDeclNode externalFunctionDecl : externalFuncDecls.getChildren()) {
			res.addExternalFunction(externalFunctionDecl.checkIR(ExternalFunction.class));
		}
		for(ExternalProcedureDeclNode externalProcedureDecl : externalProcDecls.getChildren()) {
			res.addExternalProcedure(externalProcedureDecl.checkIR(ExternalProcedure.class));
		}
		return res;
	}

	private boolean checkInhCycleFree_rec(InheritanceTypeNode inhType,
			Collection<BaseNode> inProgress, Collection<BaseNode> done)
	{
		inProgress.add(inhType);
		for(BaseNode st : inhType.getDirectSuperTypes()) {
			if(!(st instanceof InheritanceTypeNode)) {
				continue;
			}

			assert(
				((inhType instanceof NodeTypeNode) && (st instanceof NodeTypeNode)) ||
				((inhType instanceof EdgeTypeNode) && (st instanceof EdgeTypeNode)) ||
				((inhType instanceof ExternalTypeNode) && (st instanceof ExternalTypeNode))
			) : "nodes should extend nodes and edges should extend edges";

			InheritanceTypeNode superType = (InheritanceTypeNode)st;

			if(inProgress.contains(superType)) {
				inhType.getIdentNode().reportError("\"" + inhType.getTypeName() + "\" extends \""
						+ superType.getTypeName() + "\", which introduces a cycle to the type hierarchy");
				return false;
			}
			if(!done.contains(superType)) {
				if(!checkInhCycleFree_rec(superType, inProgress, done)) {
					return false;
				}
			}
		}
		inProgress.remove(inhType);
		done.add(inhType);
		return true;
	}

	/**
	 * ensure there are no cycles in the inheritance hierarchy
	 * @return	<code>true</code> if there are no cycles,
	 * 			<code>false</code> otherwise
	 */
	private boolean checkInhCycleFree()
	{
		Collection<TypeDeclNode> coll = decls.getChildren();
		for(TypeDeclNode t : coll) {
			TypeNode type = t.getDeclType();

			if(!(type instanceof InheritanceTypeNode)) {
				continue;
			}

			Collection<BaseNode> inProgress = new HashSet<BaseNode>();
			Collection<BaseNode> done = new HashSet<BaseNode>();

			boolean isCycleFree = checkInhCycleFree_rec((InheritanceTypeNode)type, inProgress, done);

			if(!isCycleFree) {
				return false;
			}
		}
		return true;
	}

	private boolean equalityMustBeDefinedIfLowerIsDefined()
	{
		if(isLowerClassDefined) {
			if(!isEqualClassDefined) {
				reportError("A \"< class;\" requires a \"== class;\"");
				return false;
			}
		}
		return true;
	}

	@Override
	public ModelTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	/*
	 Collection<BaseNode> alreadyExtended = new HashSet<BaseNode>();
	 TypeNode type = (TypeNode) ((TypeDeclNode)t).getDeclType();
	 alreadyExtended.add(type);
	
	 for ( BaseNode tt : alreadyExtended ) {
	
	 if ( !(tt instanceof InheritanceTypeNode) ) continue ;
	
	 InheritanceTypeNode inhType = (InheritanceTypeNode) tt;
	 Collection<BaseNode> superTypes = inhType.getDirectSuperTypes();
	
	 for ( BaseNode s : superTypes ) {
	 if ( alreadyExtended.contains(s) ) {
	 s.reportError("extending \"" + s + "\" causes cyclic inheritance");
	 return false;
	 }
	 }
	 alreadyExtended.addAll(superTypes);
	 }
	 */
}
