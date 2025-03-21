/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.model.decl.ModelNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphBaseNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
import de.unika.ipd.grgen.ast.type.MatchTypeIteratedNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.Walkable;

/**
 * The base class for AST nodes.
 * Base AST storage in ANTLR is insufficient due to the
 * children/sibling storing scheme. This reimplemented here.
 * AST root node is UnitNode.
 */
public abstract class BaseNode extends Base implements GraphDumpable, Walkable
{
	public static final int CONTEXT_LHS_OR_RHS = 1;
	public static final int CONTEXT_LHS = 0;
	public static final int CONTEXT_RHS = 1;
	public static final int CONTEXT_ACTION_OR_PATTERN = 1 << 1;
	public static final int CONTEXT_ACTION = 0 << 1;
	public static final int CONTEXT_PATTERN = 1 << 1;
	public static final int CONTEXT_TEST_OR_RULE = 1 << 2; // only valid if CONTEXT_ACTION
	public static final int CONTEXT_TEST = 0 << 2;
	public static final int CONTEXT_RULE = 1 << 2;
	public static final int CONTEXT_NEGATIVE = 1 << 3;
	public static final int CONTEXT_INDEPENDENT = 1 << 4;
	public static final int CONTEXT_PARAMETER = 1 << 5;
	public static final int CONTEXT_COMPUTATION = 1 << 6;
	public static final int CONTEXT_FUNCTION_OR_PROCEDURE = 1 << 7;
	public static final int CONTEXT_FUNCTION = 0 << 7;
	public static final int CONTEXT_PROCEDURE = 1 << 7;
	public static final int CONTEXT_METHOD = 1 << 8;

	/**
	 * AST global name map, that maps from Class to String.
	 * Needed as in some situations only the class object itself is available
	 * (no instance objects of the class)
	 */
	private static final Map<Class<? extends BaseNode>, String> names =
		new HashMap<Class<? extends BaseNode>, String>();

	/** A dummy AST node used in case of an error */
	private static final BaseNode NULL = new ErrorNode();

	/** Print verbose error messages. */
	private static boolean verboseErrorMsg = true;

	/** Location in the source corresponding to this node */
	private Coords coords = Coords.getInvalid();

	/** The current scope, with which the scopes of the new BaseNodes are initialized. */
	private static Scope currScope = Scope.getInvalid();

	/** The scope in which this node occurred. */
	private Scope scope;

	/** The parent node of this node. */
	protected Set<BaseNode> parents = new LinkedHashSet<BaseNode>();

	/** Has this base node already been resolved? */
	private boolean resolved = false;

	/** The result of the resolution. */
	private boolean resolveResult = false;

	/** Has this base node already been visited during check walk? */
	private boolean checkVisited = false;

	/** Has this base node already been checked? */
	private boolean checked = false;

	/** The result of the check, if checked. */
	private boolean checkResult = false;

	/** The IR object for this node. */
	private IR irObject = null;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * Make a new base node with given coordinates.
	 * @param coords The coordinates of this node.
	 */
	protected BaseNode(Coords coords)
	{
		this();
		this.coords = coords;
	}

	/**
	 * Make a new base node without a location.
	 * It is assumed, that the location is set afterwards using
	 * {@link #setLocation(Location)}.
	 */
	protected BaseNode()
	{
		this.scope = currScope;
	}

	/**
	 * Ordinary to string cast method
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString()
	{
		return getName();
	}

	/**
	 * Strip the package name from the class name.
	 * @param cls The class.
	 * @return stripped class name.
	 */
	protected static String shortClassName(Class<?> cls)
	{
		String s = cls.getName();
		return s.substring(s.lastIndexOf('.') + 1);
	}

	/**
	 * Get the name of a class.
	 * <code>cls</code> should be the Class object of a subclass of
	 * <code>BaseNode</code>. If this class is registered in the {@link #names}
	 * map, the name is returned, otherwise the name of the class.
	 * @param cls A class to get its name.
	 * @return The registered name of the class or the class name.
	 */
	public static String getName(Class<? extends BaseNode> cls)
	{
		return names.containsKey(cls) ? names.get(cls) : "<" + shortClassName(cls) + ">";
	}

	/**
	 * Set the name of a AST node class.
	 * @param cls The AST node class.
	 * @param name A human readable name for that class.
	 */
	protected static void setName(Class<? extends BaseNode> cls, String name)
	{
		names.put(cls, name);
	}

	/**
	 * Get the name of this node.
	 * @return The name
	 */
	public String getName()
	{
		Class<? extends BaseNode> cls = getClass();
		String name = getName(cls);

		if(verboseErrorMsg)
			name += " <" + getId() + "," + shortClassName(cls) + ">";

		return name;
	}

	/**
	 * Set the name of the node.
	 * @param name The new name.
	 */
	protected final void setName(String name)
	{
		names.put(getClass(), name);
	}

	// Employs reflection while a simple virtual method overriden in the subclasses would be more appropriate,
	// but a static method that can be used via reflection is required anyhow by the resolvers and checkers,
	// so it is better to re-use getKindStr than adding another method to all classes (basically duplicating the functionality).
	public final String getKind()
	{
		String res = "<unknown>";
		try {
			res = (String)getClass().getMethod("getKindStr").invoke(null);
		} catch(Exception e) {
			assert false : e.toString();
		}
		return res;
	}

	/**
	 * Returns a string characterizing the kind of the class, to be used for error reporting (e.g. "node class" or "rule").
	 * This method is to be implemented as a static method in all classes of relevance,
	 * it will be used via reflection in the resolvers and checkers,
	 * and via the non-static method getKind() (that uses the runtime type of the object).
	 */
	public static String getKindStr()
	{
		return "base node";
	}

	/**
	 * Gets an error node
	 * @return an error node
	 */
	public static BaseNode getErrorNode()
	{
		return NULL;
	}

	/**
	 * Extra info for the node, that is used by {@link #getNodeInfo()}
	 * to compose the node info.
	 * @return extra info for the node (return null, if no extra info
	 * shall be available).
	 */
	protected String extraNodeInfo()
	{
		return null;
	}

	/**
	 * Enable or disable more verbose messages.
	 * @param verbose If true, the AST classes generate slightly more verbose
	 * error messages.
	 */
	public static void setVerbose(boolean verbose)
	{
		verboseErrorMsg = verbose;
	}

	/**
	 * @return true, if this node is an error node
	 */
	public boolean isError()
	{
		return false;
	}

	/**
	 * Report an error message concerning this node
	 * @param msg The message to report.
	 */
	public final void reportError(String msg)
	{
		// error.error(getCoords(), "At " + getName() + ": " + msg + ".");
		error.error(getCoords(), msg);
	}

	public final void reportWarning(String msg)
	{
		error.warning(getCoords(), msg);
	}

	/**
	 * Get the coordinates within the source code of this node.
	 * @return The coordinates.
	 */
	public final Coords getCoords()
	{
		return coords;
	}

	/**
	 * Set the coordinates within the source code of this node.
	 * @param coords The coordinates.
	 */
	public final void setCoords(Coords coords)
	{
		this.coords = coords;
	}

	public final String getAtCoords()
	{
		return coords.getAtCoords();
	}

	/** Get an error message part telling about the coordinates the symbol was declared at 
	 * (assuming a declaration, to be satisfied by the caller) (prefixed with a space, so it can be used as a drop-in)
	 * or an empty string in case of invalid or builtin coordinates. */
	public final String getDeclarationCoords()
	{
		return coords.getDeclarationCoords(false);
	}

	public final String toStringWithDeclarationCoords()
	{
		boolean implicitly = this instanceof MatchTypeActionNode || this instanceof MatchTypeIteratedNode;

		// assumption: at least one of both parts (name or coordinates) is available
		if(this instanceof DeclaredTypeNode) {
			DeclNode decl = ((DeclaredTypeNode)this).getDecl();
			return userFriendlyToString() + (decl != null ? decl.getCoords().getDeclarationCoords(implicitly) : "");
		} else
			return userFriendlyToString() + coords.getDeclarationCoords(implicitly);
	}
	
	public final String toStringWithDeclarationCoordsIfCoordsAreOfInterest()
	{
		boolean implicitly = this instanceof MatchTypeActionNode || this instanceof MatchTypeIteratedNode;
		
		// assumption: at least one of both parts (name or coordinates) is available
		if(this instanceof DeclaredTypeNode) {
			DeclNode decl = ((DeclaredTypeNode)this).getDecl();
			if(decl == null || decl.getCoords().getDeclarationCoords(implicitly) == "")
				return "";
			return " (" + userFriendlyToString() + " is" + (decl != null ? decl.getCoords().getDeclarationCoords(implicitly) : "") + ")";
		} else {
			if(coords.getDeclarationCoords(implicitly) == "")
				return "";
			return " (" + userFriendlyToString() + " is" + coords.getDeclarationCoords(implicitly) + ")";
		}
	}

	// TODO: this should be the default -- think about replacing the current toString intended for debugging the compiler/compiler-internal error messages (that should be a differently named method, e.g. toStringExtended)
	public final String userFriendlyToString()
	{
		if(this instanceof PatternGraphBaseNode)
			return ((PatternGraphBaseNode)this).nameOfGraph;
		else if(this instanceof TypeNode) // maybe getDecl()
			return ((TypeNode)this).getTypeName();
		else if(this instanceof DeclNode)
			return ((DeclNode)this).dotOrArrowWhenAnonymous();//.getIdentNode().toString();
		else
			return toString();
	}

	/**
	 * Get the scope of this AST node.
	 * @return The scope in which the node was created.
	 */
	public final Scope getScope()
	{
		return scope;
	}

	/**
	 * Set a new current scope.
	 * This function is called from the parser as new scopes are entered
	 * or left.
	 * @param scope The new current scope.
	 */
	public static void setCurrScope(Scope scope)
	{
		currScope = scope;
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	// Children, Parents, AST structure handling
	//////////////////////////////////////////////////////////////////////////////////////////

	/** returns children of this node */
	public abstract Collection<? extends BaseNode> getChildren();

	/** returns names of the children, same order as in getChildren */
	protected abstract Collection<String> getChildrenNames();

	/** implementation of Walkable by getChildren
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	@Override
	public Collection<? extends BaseNode> getWalkableChildren()
	{
		return getChildren();
	}

	/** helper: remove ourself as parent of child to throw out, become parent of child to adopt instead */
	public final void switchParenthood(BaseNode throwOut, BaseNode adopt)
	{
		throwOut.parents.remove(this);
		adopt.parents.add(this);
	}

	/**
	 * helper: become parent of child to adopt
	 * @return The given parameter
	 **/
	public final <T extends BaseNode> T becomeParent(T adopt)
	{
		if(adopt != null) {
			adopt.parents.add(this);
		}
		return adopt;
	}

	/** helper: if resolution yielded some new node, become parent of it and return it; otherwise just return old node */
	protected final <T extends BaseNode> T ownedResolutionResult(T original, T resolved)
	{
		if(resolved != null && resolved != original) {
			becomeParent(resolved);
			return resolved;
		} else {
			return original;
		}
	}

	/** Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. */
	protected final <T extends BaseNode> T getValidResolvedVersion(T firstResolved, T secondResolved)
	{
		assert isResolved() : this;
		if(firstResolved != null) {
			return firstResolved;
		}
		if(secondResolved != null) {
			return secondResolved;
		}
		assert false : this;
		return null;
	}

	/** Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. */
	protected final <T extends BaseNode> T getValidResolvedVersion(T firstResolved, T secondResolved, T thirdResolved)
	{
		assert isResolved() : this;
		if(firstResolved != null) {
			return firstResolved;
		}
		if(secondResolved != null) {
			return secondResolved;
		}
		if(thirdResolved != null) {
			return thirdResolved;
		}
		assert false : this;
		return null;
	}

	/** Return the currently valid member. Currently valid depends on variable was already resolved. */
	protected final <T extends BaseNode> T getValidVersion(T unresolved, T resolved)
	{
		if(isResolved()) {
			return resolved;
		}
		return unresolved;
	}

	/** Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. */
	protected final <T extends BaseNode> T getValidVersion(T unresolved, T firstResolved, T secondResolved)
	{
		if(isResolved()) {
			if(firstResolved != null) {
				return firstResolved;
			}
			if(secondResolved != null) {
				return secondResolved;
			}
		}
		return unresolved;
	}

	/** Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. */
	protected final <T extends BaseNode> T getValidVersion(T unresolved,
			T firstResolved, T secondResolved, T thirdResolved)
	{
		if(isResolved()) {
			if(firstResolved != null) {
				return firstResolved;
			}
			if(secondResolved != null) {
				return secondResolved;
			}
			if(thirdResolved != null) {
				return thirdResolved;
			}
		}
		return unresolved;
	}

	/** Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. */
	protected final <T extends BaseNode> T getValidVersion(T unresolved,
			T firstResolved, T secondResolved, T thirdResolved, T fourthResolved)
	{
		if(isResolved()) {
			if(firstResolved != null) {
				return firstResolved;
			}
			if(secondResolved != null) {
				return secondResolved;
			}
			if(thirdResolved != null) {
				return thirdResolved;
			}
			if(fourthResolved != null) {
				return fourthResolved;
			}
		}
		return unresolved;
	}

	/** Return new vector containing elements of the currently valid member vector. Currently valid depends on vector was already resolved. */
	protected final <T extends BaseNode> Vector<T> getValidVersionVector(Vector<? extends T> unresolved,
			Vector<? extends T> resolved)
	{
		Vector<T> result = new Vector<T>();
		if(isResolved()) {
			for(int i = 0; i < resolved.size(); ++i) {
				result.add(resolved.get(i));
			}
		} else {
			for(int i = 0; i < unresolved.size(); ++i) {
				result.add(unresolved.get(i));
			}
		}
		return result;
	}

	/** Return new vector containing elements of the currently valid member vector.
	 *  Currently valid depends on vector was already resolved and resolution result. */
	protected final <T extends BaseNode> Vector<T> getValidVersionVector(Vector<? extends T> unresolved,
			Vector<? extends T> firstResolved, Vector<? extends T> secondResolved)
	{
		Vector<T> result = new Vector<T>();
		if(isResolved()) {
			if(!firstResolved.isEmpty()) {
				for(int i = 0; i < firstResolved.size(); ++i) {
					result.add(firstResolved.get(i));
				}
			} else {
				for(int i = 0; i < secondResolved.size(); ++i) {
					result.add(secondResolved.get(i));
				}
			}
		} else {
			for(int i = 0; i < unresolved.size(); ++i) {
				result.add(unresolved.get(i));
			}
		}
		return result;
	}

	/** Check whether this AST node is a root node (i.e. it has no predecessors)
	 * @return true, if it's a root node, false, if not. */
	public final boolean isRoot()
	{
		return parents.isEmpty();
	}

	/** Get the parent nodes of this node.
	 * Mostly only one parent (syntax tree), few nodes with multiple parents (syntax DAG), root node without parents.*/
	public final Collection<BaseNode> getParents()
	{
		return Collections.unmodifiableCollection(parents);
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	// Resolving, Checking, Type Checking
	//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * Finish up the AST.
	 * This method runs all resolvers, checks the AST and type checks it.
	 * It should be called after complete AST construction from the driver.
	 * @param node The root node of the AST.
	 * @return true, if everything went right, false, if not.
	 */
	public static final boolean manifestAST(UnitNode node)
	{
		UnitNode.setRoot(node); // gives quick access to some general model flags in checking

		// resolve AST
		boolean resolved = node.resolve();

		// check AST if successfully resolved
		if(resolved) {
			return node.check();
		} else {
			return false;
		}
	}

	/**
	 * Resolve the identifier nodes in the AST
	 * f.ex. replace an identifier AST node representing a declared type by the declared type AST node.
	 * Resolving is organized as a preorder walk over the AST.
	 * The walk is implemented here once and for all, calling resolve on it's children;
	 * first doing local resolve, then descending to the children
	 * but only if the node was not yet visited during resolving (AST in reality a DAG, so it might happen)
	 * @return true, if resolution of the AST beginning with this node finished successfully;
	 * false, if there was some error.
	 */
	public final boolean resolve()
	{
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, getCoords(), "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = resolveLocal();
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, getCoords(), "local resolve ERROR in " + this);
		}

		for(BaseNode child : getChildren()) {
			boolean res = (child != null) && child.resolve();
			//assert(res || this instanceof InvalidDeclNode);
			successfullyResolved &= res;
		}

		if(!successfullyResolved) {
			debug.report(NOTE, getCoords(), "child resolve ERROR in " + this);
		}

		return successfullyResolved;
	}

	/**
	 * local resolving of the current node to be implemented by the subclasses, called from the resolve AST walk
	 * @return true, if resolution of the AST locally finished successfully;
	 * false, if there was some error.
	 */
	protected abstract boolean resolveLocal();

	/** Mark this node as resolved and set the result of the resolution. */
	private void nodeResolvedSetResult(boolean resolveResult)
	{
		resolved = true;
		this.resolveResult = resolveResult;
	}

	/** Returns whether this node has been resolved already. */
	public final boolean isResolved()
	{
		return resolved;
	}

	/** Returns the result of the resolution (as set by nodeResolvedSetResult earlier on). */
	public final boolean resolutionResult()
	{
		assert isResolved() : this;
		return resolveResult;
	}

	/**
	 * Check the sanity and types of the AST
	 * Checking is organized as a postorder walk over the AST.
	 * The walk is implemented here once and for all, calling check on it's children;
	 * first descending to the children, then doing local checking
	 * but only if the node was not yet visited during checking (AST in reality a DAG, so it might happen)
	 * @return true, if checking of the AST beginning with this node finished successfully;
	 * false, if there was some error.
	 */
	public final boolean check()
	{
		debug.report(NOTE, getCoords(), "check in: " + getId() + "(" + getClass() + ")");

		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}

		boolean successfullyChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();

			for(BaseNode child : getChildren()) {
				boolean res = child.check();
				//assert(res || this instanceof InvalidDeclNode);
				successfullyChecked &= res;
			}
		}

		if(!successfullyChecked)
			debug.report(NOTE, getCoords(), "child check ERROR in " + this);

		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);

		if(!locallyChecked)
			debug.report(NOTE, getCoords(), "local check ERROR in " + this);

		return successfullyChecked && locallyChecked;
	}

	/**
	 * Local checking of the current node to be implemented by the subclasses,
	 * called from the check AST walk.
	 *
	 * @return true, if checking of the AST locally finished successfully;
	 *         false, if there was some error.
	 */
	protected abstract boolean checkLocal();

	/** Mark this node as checked and set the result of the check. */
	protected final void nodeCheckedSetResult(boolean checkResult)
	{
		checked = true;
		this.checkResult = checkResult;
	}

	/** Has this node already been checked? */
	public final boolean isChecked()
	{
		return checked;
	}

	/** Yields result of checking this AST node */
	protected final boolean getChecked()
	{
		assert isChecked() : this;
		return checkResult;
	}

	/** Mark this node as visited during check walk. */
	protected final void setCheckVisited()
	{
		checkVisited = true;
	}

	/** Has this node already been visited during check? */
	protected final boolean visitedDuringCheck()
	{
		return checkVisited;
	}

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: fully extract and unify this method to a common place/remove code duplication
	 * better yet: move it to own pass before resolving
	 */
	public static boolean fixupDefinition(BaseNode elem, Scope scope)
	{
		if(!(elem instanceof IdentNode)) {
			return true;
		}
		return fixupDefinition((IdentNode)elem, scope);
	}

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: fully extract and unify this method to a common place/remove code duplication
	 * better yet: move it to own pass before resolving
	 */
	public static boolean fixupDefinition(IdentNode id, Scope scope)
	{
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// second chance lookup
		if(!res && id instanceof AmbiguousIdentNode) {
			AmbiguousIdentNode ambigId = (AmbiguousIdentNode)id;
			def = scope.getCurrDef(ambigId.getOtherSymbol());
			debug.report(NOTE, "definition now is: " + def);
			res = def.isValid();
		}

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else, an error is emitted,
		// since this ident was supposed to be defined in this scope.
		if(res) {
			id.setSymDef(def);
		} else {
			id.reportError("The identifier " + id + " has not been declared in this scope: " + scope.toStringWithOpeningCoords() + ".");
		}

		return res;
	}

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: fully extract and unify this method to a common place/remove code duplication
	 * better yet: move it to own pass before resolving
	 */
	public static boolean tryFixupDefinition(BaseNode elem, Scope scope)
	{
		if(!(elem instanceof IdentNode)) {
			return false;
		}
		IdentNode id = (IdentNode)elem;

		debug.report(NOTE, "try Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else nothing happens as this ident may be referenced in an
		// attribute initialization expression within a node/edge type declaration
		// and attributes from super types are not found in this stage
		// this fixup stuff is crappy as hell
		if(def.isValid()) {
			id.setSymDef(def);
			return true;
		} else {
			return false;
		}
	}

	/*
	 * This sets the symbol defintion to the right place, if the defintion is behind the actual position.
	 * TODO: fully extract and unify this method to a common place/remove code duplication
	 * better yet: move it to own pass before resolving
	 * notice: getLocalDef here versus getCurrDef above
	 */
	protected static boolean fixupDefinition(IdentNode id, Scope scope, boolean reportErr)
	{
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getLocalDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else, an error is emitted,
		// since this ident was supposed to be defined in this scope.
		if(res)
			id.setSymDef(def);
		else if(reportErr)
			id.reportError("The identifier " + id + " has not been declared in this scope: " + scope.toStringWithOpeningCoords() + ".");

		return res;
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	// IR handling
	//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * Get the IR object for this AST node.
	 * This method gets the IR object, if it was already constructed.
	 * If not, it calls {@link #constructIR()} to construct the
	 * IR object and stores the result. This assures, that for each AST
	 * node, {@link #constructIR()} is just called once.
	 * @return The constructed/stored IR object.
	 */
	public final IR getIR()
	{
		if(irObject == null)
			setIR(constructIR());
		return irObject;
	}

	/**
	 * Set the IR object for this AST node.
	 *
	 * This method ensures that, you cannot set two different IR object.
	 */
	protected final void setIR(IR ir)
	{
		if(irObject == null) {
			irObject = ir;
			return;
		}

		if(irObject != ir) {
			assert false : "Another IR object already exists.";
		}

		return;
	}

	protected final boolean isIRAlreadySet()
	{
		return irObject != null;
	}

	/**
	 * Checks whether the IR object of this AST node is an instance
	 * of a certain, given Class <code>cls</code>.
	 * If it is not, an assertion is raised, else, the IR object is returned.
	 * @param cls The class to check the IR object for.
	 * @return The IR object.
	 */
	public final <T extends IR> T checkIR(Class<T> cls)
	{
		IR ir = getIR();

		debug.report(NOTE, getCoords(), "checking ir object in \"" + getName()
				+ "\" should be \"" + cls + "\" is \"" + ir.getClass() + "\"");
		assert cls.isInstance(ir) : "checking ir object in \"" + getName()
				+ "\" should be \"" + cls + "\" is \"" + ir.getClass() + "\"";

		return cls.cast(ir);
	}

	/**
	 * Construct the IR object.
	 * This method should never be called. It is used by {@link #getIR()}.
	 * @return The constructed IR object.
	 */
	protected IR constructIR()
	{
		return IR.getBad();
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	// graph dumping
	//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor()
	 */
	@Override
	public Color getNodeColor()
	{
		return Color.WHITE;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeId()
	 */
	@Override
	public final String getNodeId()
	{
		return getId();
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	@Override
	public String getNodeInfo()
	{
		String extra = extraNodeInfo();
		return "ID: " + getId() + (extra != null ? "\n" + extra : "");
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeLabel()
	 */
	@Override
	public String getNodeLabel()
	{
		return this.getName();
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeShape()
	 */
	@Override
	public int getNodeShape()
	{
		return GraphDumper.DEFAULT;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
	 */
	@Override
	public final String getEdgeLabel(int edge)
	{
		Collection<String> childrenNames = getChildrenNames();
		// iterate to corresponding children name
		int currentEdge = -1;
		for(Iterator<String> it = childrenNames.iterator(); it.hasNext();) {
			++currentEdge;
			String name = it.next();
			if(currentEdge == edge) {
				return name;
			}
		}
		return "" + edge;
	}

	private TypeDeclNode findType(String rootName)
	{
		// get root node
		BaseNode root = this;
		while(!root.isRoot()) {
			root = root.getParents().iterator().next();
		}

		// find a root-type
		TypeDeclNode rootType = null;
		ModelNode model = ((UnitNode)root).getStdModel();
		assert model.isResolved();
		Collection<TypeDeclNode> types = model.decls.getChildren();

		for(Iterator<TypeDeclNode> it = types.iterator(); it.hasNext();) {
			TypeDeclNode candidate = it.next();
			String name = candidate.ident.getSymbol().getText();
			if(name.equals(rootName)) {
				rootType = candidate;
			}
		}
		return rootType;
	}

	public final TypeDeclNode getNodeRootTypeDecl()
	{
		return findType("Node");
	}

	public final TypeDeclNode getArbitraryEdgeRootTypeDecl()
	{
		return findType("AEdge");
	}

	public final TypeDeclNode getDirectedEdgeRootTypeDecl()
	{
		return findType("Edge");
	}

	public final TypeDeclNode getUndirectedEdgeRootTypeDecl()
	{
		return findType("UEdge");
	}
}
