/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * Created: Wed Jul  2 15:29:49 2003
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.util.*;
import java.util.*;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import java.awt.Color;

/**
 * The base class for AST nodes.
 * Base AST storage in ANTLR is insufficient due to the
 * children/sibling storing scheme. This reimplemented here.
 * AST root node is UnitNode.
 */
public abstract class BaseNode extends Base
	implements GraphDumpable, Walkable
{
	/**
	 * AST global name map, that maps from Class to String.
	 * Needed as in some situations only the class object itself is available
	 * (no instance objects of the class)
	 */
	private static final Map<Class<? extends BaseNode>, String> names =
		new HashMap<Class<? extends BaseNode>, String>();

	/** A dummy AST node used in case of an error */
	protected static final BaseNode NULL = new NullNode();

	/** Print verbose error messages. */
	private static boolean verboseErrorMsg = true;


	/** coordinates for builtin types and declarations */
	public static final Coords BUILTIN = new Coords(0, 0, "<builtin>");

	/** Location in the source corresponding to this node */
	private Coords coords = Coords.getInvalid();


	/** The current scope, with which the scopes of the new BaseNodes are initialized. */
	private static Scope currScope = Scope.getInvalid();

	/** The scope in which this node occurred. */
	private Scope scope;


	/** Vector of the children of this node */
	private final Vector<BaseNode> children = new Vector<BaseNode>();

	/** The parent node of this node. */
	private Set<BaseNode> parents = new LinkedHashSet<BaseNode>();

	/** Default array with the children names of the node */
	private static final String[] noChildrenNames = { };

	/** array with the children names */
	private String[] childrenNames = noChildrenNames;


	/** Has this base node already been resolved? */
	private boolean resolved = false;

	/** The result of the resolution. */
	private boolean resolveResult = false;

	/** Has this base node already been checked? */
	private boolean checked = false;

	/** The result of the check, if checked. */
	private boolean checkResult = false;

	/** Has this base node already been type checked? */
	private boolean typeChecked = false;

	/** The result of the type checking. */
	private boolean typeCheckResult = false;


	/** The IR object for this node. */
	private IR irObject = null;


	/** Reappears a pattern node or edge represented by this base node
	 * in the replace/modify part? */
	private boolean kept = false;


//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * Make a new base node with given coordinates.
	 * @param coords The coordinates of this node.
	 */
	protected BaseNode(Coords coords) {
		this();
		this.coords = coords;
	}

	/**
	 * Make a new base node without a location.
	 * It is assumed, that the location is set afterwards using
	 * {@link #setLocation(Location)}.
	 */
	protected BaseNode() {
		this.scope = currScope;
	}

	/**
	 * Ordinary to string cast method
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return getName();
	}

	/**
	 * Strip the package name from the class name.
	 * @param cls The class.
	 * @return stripped class name.
	 */
	protected static String shortClassName(Class<?> cls) {
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
	public static String getName(Class<? extends BaseNode> cls) {
		return names.containsKey(cls) ? names.get(cls)
			: "<" + shortClassName(cls) + ">";
	}

	/**
	 * Set the name of a AST node class.
	 * @param cls The AST node class.
	 * @param name A human readable name for that class.
	 */
	public static void setName(Class<? extends BaseNode> cls, String name) {
		names.put(cls, name);
	}

	/**
	 * Get the name of this node.
	 * @return The name
	 */
	public String getName() {
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
	protected void setName(String name) {
		names.put(getClass(), name);
	}

	public String getKindString() {
		String res = "<unknown>";
		try { res = (String) getClass().getMethod("getKindStr").invoke(null); }
		catch (Exception e) {}
		return res;
	}

	/**
	 * Get a string characterising the kind of this class, for example "base node".
	 * @return The characterisation.
	 */
	public static String getKindStr()
	{
		return "base node";
	}

	public String getUseString()
	{
		String res = "<unknown>";
		try { res = (String) getClass().getMethod("getUseStr").invoke(null); }
		catch (Exception e) {}
		return res;
	}

	public static String getUseStr()
	{
		return "base node";
	}

	/**
	 * Gets an error node
	 * @return an error node
	 */
	public static BaseNode getErrorNode() {
		return NULL;
	}

	/**
	 * Extra info for the node, that is used by {@link #getNodeInfo()}
	 * to compose the node info.
	 * @return extra info for the node (return null, if no extra info
	 * shall be available).
	 */
	protected String extraNodeInfo() {
		return null;
	}

	/**
	 * Enable or disable more verbose messages.
	 * @param verbose If true, the AST classes generate slightly more verbose
	 * error messages.
	 */
	public static void setVerbose(boolean verbose) {
		verboseErrorMsg = verbose;
	}
	
	/**
	 * @return true, if this node is an error node
	 */
	public boolean isError() {
		return false;
	}

	/**
	 * Report an error message concerning this node
	 * @param msg The message to report.
	 */
	public final void reportError(String msg) {

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
	public Coords getCoords() {
		return coords;
	}

	/**
	 * Set the coordinates within the source code of this node.
	 * @param coords The coordinates.
	 */
	public void setCoords(Coords coords) {
		this.coords = coords;
	}
	
	/**
	 * Get the scope of this AST node.
	 * @return The scope in which the node was created.
	 */
	public Scope getScope() {
		return scope;
	}
	
	/**
	 * Set a new current scope.
	 * This function is called from the parser as new scopes are entered
	 * or left.
	 * @param scope The new current scope.
	 */
	public static void setCurrScope(Scope scope) {
		currScope = scope;
	}
	
//////////////////////////////////////////////////////////////////////////////////////////
// Children, Parents, AST structure handling
//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * The walkable children are the children of this node
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Collection<? extends BaseNode> getWalkableChildren() {
		return children;
	}
	
	/**
	 * Set the names of the children of this node.
	 * By default the children names array is empty, so the number of
	 * the child is used as its name.
	 * @param names An array containing the names of the children.
	 */
	protected void setChildrenNames(String[] names) {
		childrenNames = names;
	}

	/**
	 * Add a child to the children list.
	 * @param n AST node to add to the children list.
	 */
	public final void addChild(BaseNode n) {
		n = n==null ? NULL : n;
		children.add(n);
		n.parents.add(this);
	}

	public final void setChild(int pos, BaseNode n) {
		n = n==null ? NULL : n;
		BaseNode oldChild = getChild(pos);
		oldChild.parents.remove(this);
		children.set(pos, n);
		n.parents.add(this);
	}

	/**
	 * Add the children of another node to this one.
	 * @param n The other node
	 */
	public final void addChildren(BaseNode n) {
		for(BaseNode child :  n.getChildren())
			addChild(child);
	}

	/**
	 * Get the children of this node
	 * @return An collection of all child nodes of this one.
	 */
	public final Collection<BaseNode> getChildren() {
		return children;
	}

	/**
	 * Get the child at a given position
	 * @param i The position to get the child from
	 * @return The child at position i, or a Error node, if this node contained
	 * less than i nodes.
	 */
	public final BaseNode getChild(int i) {
		return i < children.size() ? children.get(i) : NULL;
	}

	/**
	 * Get the number of children of this node.
	 * @return The number of children of this node.
	 */
	public final int children() {
		return children.size();
	}

	/**
	 * Replace the child at a given position.
	 * @param i The position.
	 * @param n The new child to replace the old.
	 * @return The old one.
	 */
	public final BaseNode replaceChild(int i, BaseNode n) {
		n = n==null ? NULL : n;
		BaseNode res = NULL;
		if(i < children.size()) {
			res = children.get(i);
			children.set(i, n);
			n.parents.add(this);
		}

		return res;
	}

	/**
	 * Check whether this AST node is a root node (i.e. it has no predecessors)
	 * @return true, if it's a root node, false, if not.
	 */
	public boolean isRoot() {
		return parents.isEmpty();
	}

	/**
	 * Get an iterator over the parent nodes of this node.
	 * Mostly only one parent (syntax tree),
	 * few nodes with multiple parents (syntax DAG),
	 * root node with immediately exhausting iterator.
	 * @return Iterator over the parent nodes of this node.
	 */
	public Collection<BaseNode> getParents() {
		return Collections.unmodifiableCollection(parents);
	}

	/**
	 * Replace this node with another one.
	 * This AST node gets replaced by another node in all its parents.
	 * @param n The other node.
	 * @return This node.
	 */
	public final BaseNode replaceWith(BaseNode n) {
		for(Iterator<BaseNode> it = parents.iterator(); it.hasNext();) {
			BaseNode parent = it.next();
			int index = parent.children.indexOf(this);
			assert index >= 0 : "Node must be in the children array of its parent";

			parent.replaceChild(index, n);
		}

		return this;
	}
	
//////////////////////////////////////////////////////////////////////////////////////////
// Resolving, Checking, Type Checking
//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * Finish up the AST.
	 * This method runs all resolvers, checks the AST and type checks it.
	 * It should be called after complete AST construction from
	 * the driver.
	 * @param node The root node of the AST.
	 * @return true, if everything went right, false, if not.
	 */
	public static final boolean manifestAST(BaseNode node) {
		// resolve AST
		boolean successfullyResolved = node.resolve();		
		// check AST
		boolean successfullyChecked = node.doCheck();
		
		return successfullyResolved && successfullyChecked;
	}

	/**
	 * Resolve the identifier nodes in the AST
	 * f.ex. replace an identifier AST node representing a declared type by the declared type AST node.
	 * Resolving is organized as a preorder walk over the AST with the current node calling resolve on it's children
	 * This must be implemented in the subclasses, first doing local resolution then descending to the children,
	 * but only if the node was not yet visited during resolution (AST in reality a DAG, so it might happen)
	 * @return true, if resolution of the AST beginning with this node finished successfully;
	 * false, if there was some error.
	 */
	protected abstract boolean resolve();

	/** Mark this node as resolved and set the result of the resolution. */
	protected final void nodeResolvedSetResult(boolean resolveResult) {
		resolved = true;
		this.resolveResult = resolveResult;
	}

	/** Returns whether this node has been resolved already. */
	public final boolean isResolved() {
		return resolved;
	}

	/** Returns the result of the resolution (as set by nodeResolvedSetResult earlier on). */
	public final boolean resolutionResult() {
		assert isResolved();
		return resolveResult;
	}

	/** checks AST subtree beginning with this node, descending to all children
	 *  returns success of checking the subtree (only true if every check succeeded)
	 *  replaces BooleanResultVisitor calling getCheck and getTypeCheck during PostWalk of AST */
	protected abstract boolean doCheck();
	
	/**
	 * Checks a child of this node to be of a certain type.
	 * If it is not, an error is reported via the ErrorFacility.
	 * @param childNum The number of the child to check
	 * @param cls The class the child must be an instance of
	 * @return true, if the selected child node was of type cls
	 */
	public final boolean checkChild(int childNum, Class<?> cls) {
		boolean res = false;

		BaseNode child = getChild(childNum);
		if(cls.isInstance(child))
			res = true;
		else
			reportChildError(childNum, cls);

		return res;
	}
	
	/**
	 * Reports a bad child node.
	 * @param childNum The number of the bad child
	 * @param child The bad child node
	 * @param cls The class the child should have been of
	 * @return true, if the selected child node was of type cls
	 */
	public void reportChildError(int childNum, Class<?> cls) {
		reportError("Child " + childNum + " \"" + getChild(childNum).getName() +
				"\"" + " needs to be instance of \"" + shortClassName(cls) + "\"");
	}

	/**
	 * Apply a checker to a specific child
	 * @param childNum The number of the child
	 * @param checker The checker to apply
	 * @return The result of the checker applied to the child.
	 */
	public final boolean checkChild(int childNum, Checker checker) {
		return checker.check(getChild(childNum), error);
	}

	/**
	 * Checks whether all children of this node are an instance of a
	 * certain class
	 * @param cls The class the children should be an instance of
	 * @return true, if all children of this node were an instance of cls
	 */
	public final boolean checkAllChildren(Class<?> cls) {
		boolean res = true;

		for(BaseNode n :  getChildren())
			if(!cls.isInstance(n))
				res = false;

		return res;
	}

	/**
	 * Check all children with a checker. The checker is applied to each
	 * child of this node
	 * @see Checker
	 * @param c The checker
	 * @return true, if the checker returned true for every node, false
	 * otherwise.
	 */
	public final boolean checkAllChildren(Checker c) {
		boolean res = true;

		for(BaseNode n :  getChildren()) {
			boolean checkRes = c.check(n, error);
			res = res && checkRes;
		}

		return res;
	}

	/**
	 * Check the sanity of this AST node. This ensures, that all the children have
	 * correct types. subclasses have to implement that the right way.
	 * @return true, if this node is in a correct state.
	 */
	protected boolean check() {
		return true;
	}

	/**
	 * Check the types of this AST node.
	 * Subclasses should implement this, if necessary.
	 * @return true, if all types are right. False, if not.
	 */
	protected boolean typeCheck() {
		return true;
	}

	/**
	 * Check this AST node.
	 * If the node has been checked before, the result of the former
	 * check is returned.
	 * @return true, if the node is ok, false if not.
	 */
	public final boolean getCheck() {
		if(!checked) {
			checked = true;
			checkResult = check();
		}

		return checkResult;
	}

	/**
	 * Check, if all types on this AST node are right.
	 * @return true, if all types were right, false, if not.
	 */
	public final boolean getTypeCheck() {
		if(!typeChecked) {
			typeChecked = true;
			typeCheckResult = typeCheck();
		}

		return typeCheckResult;
	}
	
	/** Has this node already been checked? */
	protected final boolean isChecked() {
		return checked;
	}
	
	/**
	 * yields result of checking this AST node, type checking included
	 * ATTENTION: different from getCheck without type checking
	 */
	protected final boolean getChecked() {
		assert(isChecked());
		if(!getCheck()) {
			return false;
		}
		return getTypeCheck();
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
	public final IR getIR() {
		if(irObject == null)
			irObject = constructIR();
		return irObject;
	}

	/**
	 * Checks whether the IR object of this AST node is an instance of a certain
	 * Class <code>cls</code>.
	 * If it is not, an assertion is raised, else, the IR object
	 * is returned.
	 * @param cls The class to check the IR object for.
	 * @return The IR object.
	 */
	public final IR checkIR(Class<? extends IR> cls) {
		IR ir = getIR();

		debug.report(NOTE, "checking ir object in \"" + getName()
			+ "\" should be \"" + cls + "\" is \"" + ir.getClass() + "\"");
		assert cls.isInstance(ir) : "checking ir object in \"" + getName()
			+ "\" should be \"" + cls + "\" is \"" + ir.getClass() + "\"";

		return ir;
	}

	/**
	 * Construct the IR object.
	 * This method should never be called. It is used by {@link #getIR()}.
	 * @return The constructed IR object.
	 */
	protected IR constructIR() {
		return IR.getBad();
	}
	
//////////////////////////////////////////////////////////////////////////////////////////
// graph dumping
//////////////////////////////////////////////////////////////////////////////////////////

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.WHITE;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeId()
	 */
	public String getNodeId() {
		return getId();
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	public String getNodeInfo() {
		String extra = extraNodeInfo();
		return "ID: " + getId() + (extra != null ? "\n" + extra : "");
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeLabel()
	 */
	public String getNodeLabel() {
		return this.getName();
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeShape()
	 */
	public int getNodeShape() {
		return GraphDumper.DEFAULT;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
	 */
	public String getEdgeLabel(int edge) {
		return edge < childrenNames.length ? childrenNames[edge] : "" + edge;
	}

//////////////////////////////////////////////////////////////////////////////////////////
// misplaced stuff
//////////////////////////////////////////////////////////////////////////////////////////
		
	/**
	 * tells whether the node or edge of the pattern part, that is represented
	 * by a base node, occurs in the replace/modify part again
	 */
	public final boolean isKept()
	{
		return kept;
	}

	/**
	 * set whether the node or edge of the pattern part, that is represented
	 * by a base node, occurs in the replace/modify part again
	 */
	public final void setKept(boolean x)
	{
		kept = x;
	}
}

