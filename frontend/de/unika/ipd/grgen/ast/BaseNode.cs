/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using ModelNode = de.unika.ipd.grgen.ast.model.decl.ModelNode;
	using PatternGraphBaseNode = de.unika.ipd.grgen.ast.pattern.PatternGraphBaseNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
	using MatchTypeIteratedNode = de.unika.ipd.grgen.ast.type.MatchTypeIteratedNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Bad = de.unika.ipd.grgen.ir.Bad;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Scope = de.unika.ipd.grgen.parser.Scope;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;
	using Base = de.unika.ipd.grgen.util.Base;
	using GraphDumpable = de.unika.ipd.grgen.util.GraphDumpable;
	using GraphDumper = de.unika.ipd.grgen.util.GraphDumper;
	using Walkable = de.unika.ipd.grgen.util.Walkable;
	using Color = de.unika.ipd.grgen.util.Color;
	using Shape = de.unika.ipd.grgen.util.Shape;

	/// <summary>
	/// The base class for AST nodes.
	/// Base AST storage in ANTLR is insufficient due to the
	/// children/sibling storing scheme. This reimplemented here.
	/// AST root node is UnitNode.
	/// </summary>
	public abstract class BaseNode : Base, GraphDumpable, Walkable
	{
		public const int CONTEXT_LHS_OR_RHS = 1;
		public const int CONTEXT_LHS = 0;
		public const int CONTEXT_RHS = 1;
		public const int CONTEXT_ACTION_OR_PATTERN = 1 << 1;
		public const int CONTEXT_ACTION = 0 << 1;
		public const int CONTEXT_PATTERN = 1 << 1;
		public const int CONTEXT_TEST_OR_RULE = 1 << 2; // only valid if CONTEXT_ACTION
		public const int CONTEXT_TEST = 0 << 2;
		public const int CONTEXT_RULE = 1 << 2;
		public const int CONTEXT_NEGATIVE = 1 << 3;
		public const int CONTEXT_INDEPENDENT = 1 << 4;
		public const int CONTEXT_PARAMETER = 1 << 5;
		public const int CONTEXT_COMPUTATION = 1 << 6;
		public const int CONTEXT_FUNCTION_OR_PROCEDURE = 1 << 7;
		public const int CONTEXT_FUNCTION = 0 << 7;
		public const int CONTEXT_PROCEDURE = 1 << 7;
		public const int CONTEXT_METHOD = 1 << 8;

		/// <summary>
		/// AST global name map, that maps from Class to String.
		/// Needed as in some situations only the class object itself is available
		/// (no instance objects of the class)
		/// </summary>
		private static readonly IDictionary<Type, string> names = new Dictionary<Type, string>();

		/// <summary>
		/// A dummy AST node used in case of an error </summary>
		private static readonly BaseNode NULL = new ErrorNode();

		/// <summary>
		/// Print verbose error messages. </summary>
		private static bool verboseErrorMsg = true;

		/// <summary>
		/// Location in the source corresponding to this node </summary>
		private Coords coords = Coords.Invalid;

		/// <summary>
		/// The current scope, with which the scopes of the new BaseNodes are initialized. </summary>
		private static Scope currScope = Scope.Invalid;

		/// <summary>
		/// The scope in which this node occurred. </summary>
		private Scope scope;

		/// <summary>
		/// The parent node of this node. </summary>
		protected internal ISet<BaseNode> parents = new LinkedHashSet<BaseNode>();

		/// <summary>
		/// Has this base node already been resolved? </summary>
		private bool resolved = false;

		/// <summary>
		/// The result of the resolution. </summary>
		private bool resolveResult = false;

		/// <summary>
		/// Has this base node already been visited during check walk? </summary>
		private bool checkVisited = false;

		/// <summary>
		/// Has this base node already been checked? </summary>
		private bool @checked = false;

		/// <summary>
		/// The result of the check, if checked. </summary>
		private bool checkResult = false;

		/// <summary>
		/// The IR object for this node. </summary>
		private IR irObject = null;

		//////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Make a new base node with given coordinates. </summary>
		/// <param name="coords"> The coordinates of this node. </param>
		protected internal BaseNode(Coords coords)
			: this()
		{
			this.coords = coords;
		}

		/// <summary>
		/// Make a new base node without a location.
		/// It is assumed, that the location is set afterwards using
		/// <seealso cref="setLocation(Location)"/>.
		/// </summary>
		protected internal BaseNode()
		{
			this.scope = currScope;
		}

		/// <summary>
		/// Ordinary to string cast method </summary>
		/// <seealso cref="java.lang.Object.toString()"/>
		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Strip the package name from the class name. </summary>
		/// <param name="cls"> The class. </param>
		/// <returns> stripped class name. </returns>
		protected internal static string StripPackageFromClasssName(Type cls)
		{
			string s = cls.FullName;
			return s.Substring(s.LastIndexOf('.') + 1);
		}

		/// <summary>
		/// Get the name of an AST node class.
		/// <code>cls</code> should be the Class object of a subclass of
		/// <code>BaseNode</code>. If this class is registered in the <seealso cref="names"/>
		/// map, the name is returned, otherwise the name of the class. </summary>
		/// <param name="cls"> A class to get its name. </param>
		/// <returns> The registered name of the class or the class name. </returns>
		public static string GetClassName(Type cls)
		{
			return names.ContainsKey(cls) ? names[cls] : "<" + StripPackageFromClasssName(cls) + ">";
		}

		/// <summary>
		/// Set the name of an AST node class. </summary>
		/// <param name="cls"> The AST node class. </param>
		/// <param name="name"> A human readable name for that class. </param>
		protected internal static void SetClassName(Type cls, string name)
		{
			names[cls] = name;
		}

		/// <summary>
		/// Get the name of this node. </summary>
		/// <returns> The name </returns>
		public virtual string Name
		{
			get
			{
				Type cls = this.GetType();
				string name = GetClassName(cls);

				if(verboseErrorMsg)
					name += " <" + Id + "," + StripPackageFromClasssName(cls) + ">";

				return name;
			}
		}

		// Employs reflection while a simple virtual method overriden in the subclasses would be more appropriate,
		// but a static method that can be used via reflection is required anyhow by the resolvers and checkers,
		// so it is better to re-use KindStr than adding another method to all classes (basically duplicating the functionality).
		public string Kind
		{
			get
			{
				string res = "<unknown>";
				try
				{
					Type type = this.GetType();
					System.Reflection.PropertyInfo prop = type.GetProperty("KindStr");
					while(prop == null) // all relevant classes inherit from BaseNode, which for sure offers a KindStr property (see below), causing termination
					{
						type = type.BaseType;
						prop = type.GetProperty("KindStr");
					}
					res = (string)prop.GetValue(null); // was previously in Java: getClass().getMethod("getKindStr").invoke(null)
				}
				catch(Exception e)
				{
					Debug.Assert(false, e.ToString());
				}
				return res;
			}
		}

		/// <summary>
		/// Returns a string characterizing the kind of the class, to be used for error reporting (e.g. "node class" or "rule").
		/// This method is to be implemented as a static method in all classes of relevance,
		/// it will be used via reflection in the resolvers and checkers,
		/// and via the non-static method getKind() (that uses the runtime type of the object).
		/// </summary>
		public static string KindStr
		{
			get
			{
				return "base node";
			}
		}

		/// <summary>
		/// Gets an error node </summary>
		/// <returns> an error node </returns>
		public static BaseNode ErrorNode
		{
			get
			{
				return NULL;
			}
		}

		/// <summary>
		/// Extra info for the node, that is used by <seealso cref="getNodeInfo()"/>
		/// to compose the node info. </summary>
		/// <returns> extra info for the node (return null, if no extra info
		/// shall be available). </returns>
		protected internal virtual string ExtraNodeInfo()
		{
			return null;
		}

		/// <summary>
		/// Enable or disable more verbose messages. </summary>
		/// <param name="verbose"> If true, the AST classes generate slightly more verbose
		/// error messages. </param>
		public static bool Verbose
		{
			set
			{
				verboseErrorMsg = value;
			}
		}

		/// <returns> true, if this node is an error node </returns>
		public virtual bool IsError()
		{
			return false;
		}

		/// <summary>
		/// Report an error message concerning this node </summary>
		/// <param name="msg"> The message to report. </param>
		public void ReportError(string msg)
		{
			// error.error(getCoords(), "At " + getName() + ": " + msg + ".");
			error.Error(Coords, msg);
		}

		public void ReportWarning(string msg)
		{
			error.Warning(Coords, msg);
		}

		/// <summary>
		/// Get the coordinates within the source code of this node. </summary>
		/// <returns> The coordinates. </returns>
		public Coords Coords
		{
			get
			{
				return coords;
			}
			set
			{
				this.coords = value;
			}
		}


		public string AtCoords
		{
			get
			{
				return coords.AtCoords;
			}
		}

		/// <summary>
		/// Get an error message part telling about the coordinates the symbol was declared at 
		/// (assuming a declaration, to be satisfied by the caller) (prefixed with a space, so it can be used as a drop-in)
		/// or an empty string in case of invalid or builtin coordinates. 
		/// </summary>
		public string DeclarationCoords
		{
			get
			{
				return coords.GetDeclarationCoords(false);
			}
		}

		public string ToStringWithDeclarationCoords()
		{
			bool implicitly = this is MatchTypeActionNode || this is MatchTypeIteratedNode;

			// assumption: at least one of both parts (name or coordinates) is available
			if(this is DeclaredTypeNode)
			{
				DeclNode decl = ((DeclaredTypeNode)this).Decl;
				return UserFriendlyToString() + (decl != null ? decl.Coords.GetDeclarationCoords(implicitly) : "");
			}
			else
				return UserFriendlyToString() + coords.GetDeclarationCoords(implicitly);
		}

		public string ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
		{
			bool implicitly = this is MatchTypeActionNode || this is MatchTypeIteratedNode;

			// assumption: at least one of both parts (name or coordinates) is available
			if(this is DeclaredTypeNode)
			{
				DeclNode decl = ((DeclaredTypeNode)this).Decl;
				if(decl == null || string.ReferenceEquals(decl.Coords.GetDeclarationCoords(implicitly), ""))
					return "";
				return " (" + UserFriendlyToString() + " is" + (decl != null ? decl.Coords.GetDeclarationCoords(implicitly) : "") + ")";
			}
			else
			{
				if(string.ReferenceEquals(coords.GetDeclarationCoords(implicitly), ""))
					return "";
				return " (" + UserFriendlyToString() + " is" + coords.GetDeclarationCoords(implicitly) + ")";
			}
		}

		// TODO: this should be the default -- think about replacing the current toString intended for debugging the compiler/compiler-internal error messages (that should be a differently named method, e.g. toStringExtended)
		public string UserFriendlyToString()
		{
			if(this is PatternGraphBaseNode)
				return ((PatternGraphBaseNode)this).nameOfGraph;
			else if(this is TypeNode) // maybe getDecl()
				return ((TypeNode)this).TypeName;
			else if(this is DeclNode)
				return ((DeclNode)this).DotOrArrowWhenAnonymous(); //.getIdent().toString();
			else
				return ToString();
		}

		/// <summary>
		/// Get the scope of this AST node. </summary>
		/// <returns> The scope in which the node was created. </returns>
		public Scope Scope
		{
			get
			{
				return scope;
			}
		}

		/// <summary>
		/// Set a new current scope.
		/// This function is called from the parser as new scopes are entered
		/// or left. </summary>
		/// <param name="scope"> The new current scope. </param>
		public static Scope CurrScope
		{
			set
			{
				currScope = value;
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////////
		// Children, Parents, AST structure handling
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// returns children of this node (only for reading) </summary>
		public abstract ICollection<BaseNode> Children {get;}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public abstract ICollection<string> ChildrenNames {get;}

		/// <summary>
		/// implementation of Walkable by getChildren </summary>
		/// <seealso cref="de.unika.ipd.grgen.util.Walkable.getWalkableChildren() "/>
		public virtual ICollection<BaseNode> WalkableChildren
		{
			get
			{
				return Children;
			}
		}

		/// <summary>
		/// helper: remove ourself as parent of child to throw out, become parent of child to adopt instead </summary>
		public void SwitchParenthood(BaseNode throwOut, BaseNode adopt)
		{
			throwOut.parents.Remove(this);
			adopt.parents.Add(this);
		}

		/// <summary>
		/// helper: become parent of child to adopt </summary>
		/// <returns> The given parameter
		///  </returns>
		public T BecomeParent<T>(T adopt) where T : BaseNode
		{
			if(adopt != null)
				adopt.parents.Add(this);
			return adopt;
		}

		/// <summary>
		/// helper: if resolution yielded some new node, become parent of it and return it; otherwise just return old node </summary>
		protected internal T OwnedResolutionResult<T>(T original, T resolved) where T : BaseNode
		{
			if(resolved != null && resolved != original)
			{
				BecomeParent(resolved);
				return resolved;
			}
			else
				return original;
		}

		/// <summary>
		/// Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. </summary>
		protected internal T GetValidResolvedVersion<T>(T firstResolved, T secondResolved) where T : BaseNode
		{
			Debug.Assert(IsResolved(), this.ToString());
			if(firstResolved != null)
				return firstResolved;
			if(secondResolved != null)
				return secondResolved;
			Debug.Assert(false, this.ToString());
			return default(T);
		}

		/// <summary>
		/// Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. </summary>
		protected internal T GetValidResolvedVersion<T>(T firstResolved, T secondResolved, T thirdResolved) where T : BaseNode
		{
			Debug.Assert(IsResolved(), this.ToString());
			if(firstResolved != null)
				return firstResolved;
			if(secondResolved != null)
				return secondResolved;
			if(thirdResolved != null)
				return thirdResolved;
			Debug.Assert(false, this.ToString());
			return default(T);
		}

		/// <summary>
		/// Return the currently valid member. Currently valid depends on variable was already resolved. </summary>
		protected internal BaseNode GetValidVersion(BaseNode unresolved, BaseNode resolved)
		{
			if(IsResolved())
				return resolved;
			return unresolved;
		}

		/// <summary>
		/// Return the currently valid member of the CollectNode. Currently valid depends on variable was already resolved. </summary>
		protected internal CollectBaseNode GetValidVersionCollectNode<T1, T2>(CollectNode<T1> unresolved, CollectNode<T2> resolved) where T1 : BaseNode where T2 : BaseNode
		{
			if(IsResolved())
				return resolved;
			return unresolved;
		}

		/// <summary>
		/// Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. </summary>
		protected internal BaseNode GetValidVersion(BaseNode unresolved, BaseNode firstResolved, BaseNode secondResolved)
		{
			if(IsResolved())
			{
				if(firstResolved != null)
					return firstResolved;
				if(secondResolved != null)
					return secondResolved;
			}
			return unresolved;
		}

		/// <summary>
		/// Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. </summary>
		protected internal BaseNode GetValidVersion(BaseNode unresolved,
				BaseNode firstResolved, BaseNode secondResolved, BaseNode thirdResolved)
		{
			if(IsResolved())
			{
				if(firstResolved != null)
					return firstResolved;
				if(secondResolved != null)
					return secondResolved;
				if(thirdResolved != null)
					return thirdResolved;
			}
			return unresolved;
		}

		/// <summary>
		/// Return the currently valid member. Currently valid depends on variable was already resolved and resolution result. </summary>
		protected internal BaseNode GetValidVersion(BaseNode unresolved,
				BaseNode firstResolved, BaseNode secondResolved, BaseNode thirdResolved, BaseNode fourthResolved)
		{
			if(IsResolved())
			{
				if(firstResolved != null)
					return firstResolved;
				if(secondResolved != null)
					return secondResolved;
				if(thirdResolved != null)
					return thirdResolved;
				if(fourthResolved != null)
					return fourthResolved;
			}
			return unresolved;
		}

		/// <summary>
		/// Return new list containing elements of the currently valid member list. Currently valid depends on list was already resolved. </summary>
		protected internal IList<BaseNode> GetValidVersionList<T1>(IList<BaseNode> unresolved,
				IList<T1> resolved) where T1 : BaseNode
		{
			IList<BaseNode> result = new List<BaseNode>();
			if(IsResolved())
			{
				for(int i = 0; i < resolved.Count; ++i)
					result.Add(resolved[i]);
			}
			else
			{
				for(int i = 0; i < unresolved.Count; ++i)
					result.Add(unresolved[i]);
			}
			return result;
		}

		/// <summary>
		/// Return new list containing elements of the currently valid member list.
		///  Currently valid depends on list was already resolved and resolution result. 
		/// </summary>
		protected internal IList<BaseNode> GetValidVersionList<T1, T2>(IList<BaseNode> unresolved,
				IList<T1> firstResolved, IList<T2> secondResolved) where T1 : BaseNode where T2 : BaseNode
		{
			IList<BaseNode> result = new List<BaseNode>();
			if(IsResolved())
			{
				if(firstResolved.Count > 0)
				{
					for(int i = 0; i < firstResolved.Count; ++i)
						result.Add(firstResolved[i]);
				}
				else
				{
					for(int i = 0; i < secondResolved.Count; ++i)
						result.Add(secondResolved[i]);
				}
			}
			else
			{
				for(int i = 0; i < unresolved.Count; ++i)
					result.Add(unresolved[i]);
			}
			return result;
		}

		/// <summary>
		/// Check whether this AST node is a root node (i.e. it has no predecessors) </summary>
		/// <returns> true, if it's a root node, false, if not.  </returns>
		public bool IsRoot()
		{
			return parents.Count == 0;
		}

		/// <summary>
		/// Get the parent nodes of this node.
		/// Mostly only one parent (syntax tree), few nodes with multiple parents (syntax DAG), root node without parents.
		/// </summary>
		public ICollection<BaseNode> Parents
		{
			get
			{
				return /*Collections.UnmodifiableSet(*/parents/*)*/; // no AsReadOnly() available on set like on list, the very limited IReadOnlyCollection is only implemented by the HashSet<T>, not by the ISet<T>, other options are not supported by .NET Framework/Mono => Collections.UnmodifiableSet left as comment (TODO: Collections.UnmodifiableSet maybe supply own implementation)
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////////
		// Resolving, Checking, Type Checking
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Finish up the AST.
		/// This method runs all resolvers, checks the AST and type checks it.
		/// It should be called after complete AST construction from the driver. </summary>
		/// <param name="node"> The root node of the AST. </param>
		/// <returns> true, if everything went right, false, if not. </returns>
		public static bool ManifestAST(UnitNode node)
		{
			UnitNode.Root = node; // gives quick access to some general model flags in checking

			// resolve AST
			bool resolved = node.Resolve();

			// check AST if successfully resolved
			if(resolved)
				return node.Check();
			else
				return false;
		}

		/// <summary>
		/// Resolve the identifier nodes in the AST
		/// f.ex. replace an identifier AST node representing a declared type by the declared type AST node.
		/// Resolving is organized as a preorder walk over the AST.
		/// The walk is implemented here once and for all, calling resolve on it's children;
		/// first doing local resolve, then descending to the children
		/// but only if the node was not yet visited during resolving (AST in reality a DAG, so it might happen) </summary>
		/// <returns> true, if resolution of the AST beginning with this node finished successfully;
		/// false, if there was some error. </returns>
		public bool Resolve()
		{
			if(IsResolved())
				return ResolutionResult();

			debug.Report(NOTE, Coords, "resolve in: " + Id + "(" + this.GetType() + ")");
			bool successfullyResolved = ResolveLocal();
			NodeResolvedSetResult(successfullyResolved); // local result
			if(!successfullyResolved)
				debug.Report(NOTE, Coords, "local resolve ERROR in " + this);

			foreach(BaseNode child in Children)
			{
				bool res = (child != null) && child.Resolve();
				//assert(res || this instanceof InvalidDeclNode);
				successfullyResolved &= res;
			}

			if(!successfullyResolved)
				debug.Report(NOTE, Coords, "child resolve ERROR in " + this);

			return successfullyResolved;
		}

		/// <summary>
		/// local resolving of the current node to be implemented by the subclasses, called from the resolve AST walk </summary>
		/// <returns> true, if resolution of the AST locally finished successfully;
		/// false, if there was some error. </returns>
		protected internal abstract bool ResolveLocal();

		/// <summary>
		/// Mark this node as resolved and set the result of the resolution. </summary>
		private void NodeResolvedSetResult(bool resolveResult)
		{
			resolved = true;
			this.resolveResult = resolveResult;
		}

		/// <summary>
		/// Returns whether this node has been resolved already. </summary>
		public bool IsResolved()
		{
			return resolved;
		}

		/// <summary>
		/// Returns the result of the resolution (as set by nodeResolvedSetResult earlier on). </summary>
		public bool ResolutionResult()
		{
			Debug.Assert(IsResolved(), this.ToString());
			return resolveResult;
		}

		/// <summary>
		/// Check the sanity and types of the AST
		/// Checking is organized as a postorder walk over the AST.
		/// The walk is implemented here once and for all, calling check on it's children;
		/// first descending to the children, then doing local checking
		/// but only if the node was not yet visited during checking (AST in reality a DAG, so it might happen) </summary>
		/// <returns> true, if checking of the AST beginning with this node finished successfully;
		/// false, if there was some error. </returns>
		public bool Check()
		{
			debug.Report(NOTE, Coords, "check in: " + Id + "(" + this.GetType() + ")");

			if(!ResolutionResult())
				return false;
			if(IsChecked())
				return CheckResult;

			bool successfullyChecked = true;
			if(!VisitedDuringCheck())
			{
				SetCheckVisited();

				foreach(BaseNode child in Children)
				{
					bool res = child.Check();
					//assert(res || this instanceof InvalidDeclNode);
					successfullyChecked &= res;
				}
			}

			if(!successfullyChecked)
				debug.Report(NOTE, Coords, "child check ERROR in " + this);

			bool locallyChecked = CheckLocal();
			NodeCheckedSetResult(locallyChecked);

			if(!locallyChecked)
				debug.Report(NOTE, Coords, "local check ERROR in " + this);

			return successfullyChecked && locallyChecked;
		}

		/// <summary>
		/// Local checking of the current node to be implemented by the subclasses,
		/// called from the check AST walk.
		/// </summary>
		/// <returns> true, if checking of the AST locally finished successfully;
		///         false, if there was some error. </returns>
		protected internal abstract bool CheckLocal();

		/// <summary>
		/// Mark this node as checked and set the result of the check. </summary>
		protected internal void NodeCheckedSetResult(bool checkResult)
		{
			@checked = true;
			this.checkResult = checkResult;
		}

		/// <summary>
		/// Has this node already been checked? </summary>
		public bool IsChecked()
		{
			return @checked;
		}

		/// <summary>
		/// Yields result of checking this AST node </summary>
		protected internal bool CheckResult
		{
			get
			{
				Debug.Assert(IsChecked(), this.ToString());
				return checkResult;
			}
		}

		/// <summary>
		/// Mark this node as visited during check walk. </summary>
		protected internal void SetCheckVisited()
		{
			checkVisited = true;
		}

		/// <summary>
		/// Has this node already been visited during check? </summary>
		protected internal bool VisitedDuringCheck()
		{
			return checkVisited;
		}

		/*
		 * This sets the symbol definition to the right place, if the definition is behind the actual position.
		 * TODO: fully extract and unify this method to a common place/remove code duplication
		 * better yet: move it to own pass before resolving
		 */
		public static bool FixupDefinition(BaseNode elem, Scope scope)
		{
			if(!(elem is IdentNode))
				return true;
			return FixupDefinition((IdentNode)elem, scope);
		}

		/*
		 * This sets the symbol definition to the right place, if the definition is behind the actual position.
		 * TODO: fully extract and unify this method to a common place/remove code duplication
		 * better yet: move it to own pass before resolving
		 */
		public static bool FixupDefinition(IdentNode id, Scope scope)
		{
			debug.Report(NOTE, "Fixup " + id + " in scope " + scope);

			// Get the definition of the ident's symbol local to the owned scope.
			Symbol.Definition def = scope.GetCurrDef(id.Symbol);
			debug.Report(NOTE, "definition is: " + def);

			// The result is true, if the definition's valid.
			bool res = def.IsValid();

			// second chance lookup
			if(!res && id is AmbiguousIdentNode)
			{
				AmbiguousIdentNode ambigId = (AmbiguousIdentNode)id;
				def = scope.GetCurrDef(ambigId.OtherSymbol);
				debug.Report(NOTE, "definition now is: " + def);
				res = def.IsValid();
			}

			// If this definition is valid, i.e. it exists,
			// the definition of the ident is rewritten to this definition,
			// else, an error is emitted,
			// since this ident was supposed to be defined in this scope.
			if(res)
				id.SymDef = def;
			else
				id.ReportError("The identifier " + id + " has not been declared in this scope: " + scope.ToStringWithOpeningCoords() + ".");

			return res;
		}

		/*
		 * This sets the symbol definition to the right place, if the definition is behind the actual position.
		 * TODO: fully extract and unify this method to a common place/remove code duplication
		 * better yet: move it to own pass before resolving
		 */
		public static bool TryFixupDefinition(BaseNode elem, Scope scope)
		{
			if(!(elem is IdentNode))
				return false;
			IdentNode id = (IdentNode)elem;

			debug.Report(NOTE, "try Fixup " + id + " in scope " + scope);

			// Get the definition of the ident's symbol local to the owned scope.
			Symbol.Definition def = scope.GetCurrDef(id.Symbol);
			debug.Report(NOTE, "definition is: " + def);

			// If this definition is valid, i.e. it exists,
			// the definition of the ident is rewritten to this definition,
			// else nothing happens as this ident may be referenced in an
			// attribute initialization expression within a node/edge type declaration
			// and attributes from super types are not found in this stage
			// this fixup stuff is crappy as hell
			if(def.IsValid())
			{
				id.SymDef = def;
				return true;
			}
			else
				return false;
		}

		/*
		 * This sets the symbol defintion to the right place, if the defintion is behind the actual position.
		 * TODO: fully extract and unify this method to a common place/remove code duplication
		 * better yet: move it to own pass before resolving
		 * notice: getLocalDef here versus getCurrDef above
		 */
		protected internal static bool FixupDefinition(IdentNode id, Scope scope, bool reportErr)
		{
			debug.Report(NOTE, "Fixup " + id + " in scope " + scope);

			// Get the definition of the ident's symbol local to the owned scope.
			Symbol.Definition def = scope.GetLocalDef(id.Symbol);
			debug.Report(NOTE, "definition is: " + def);

			// The result is true, if the definition's valid.
			bool res = def.IsValid();

			// If this definition is valid, i.e. it exists,
			// the definition of the ident is rewritten to this definition,
			// else, an error is emitted,
			// since this ident was supposed to be defined in this scope.
			if(res)
				id.SymDef = def;
			else if(reportErr)
				id.ReportError("The identifier " + id + " has not been declared in this scope: " + scope.ToStringWithOpeningCoords() + ".");

			return res;
		}

		//////////////////////////////////////////////////////////////////////////////////////////
		// IR handling
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Get the IR object for this AST node.
		/// This method gets the IR object, if it was already constructed.
		/// If not, it calls <seealso cref="constructIR()"/> to construct the
		/// IR object and stores the result. This assures, that for each AST
		/// node, <seealso cref="constructIR()"/> is just called once. </summary>
		/// <returns> The constructed/stored IR object. </returns>
		public IR IR
		{
			get
			{
				if(irObject == null)
					IR = ConstructIR();
				return irObject;
			}
			set // Set the IR object for this AST node. This method ensures that, you cannot set two different IR object.
			{
			if(irObject == null)
			{
				irObject = value;
				return;
			}

			if(irObject != value)
				Debug.Assert(false, "Another IR object already exists.");

			return;
			}
		}


		protected internal bool IsIRAlreadySet()
		{
			return irObject != null;
		}

		/// <summary>
		/// Checks whether the IR object of this AST node is an instance
		/// of a certain, given Class <code>cls</code>.
		/// If it is not, an assertion is raised, else, the IR object is returned. </summary>
		/// <param name="cls"> The class to check the IR object for. </param>
		/// <returns> The IR object. </returns>
		public T CheckIR<T>(Type cls) where T : de.unika.ipd.grgen.ir.IR
		{
			Debug.Assert(cls == typeof(T)); // TODO: remove cls parameter, use typeof(T) ... remainder from Java ... but maybe keep it so that an automatic port to Java from the by-now reference C# version is easy...

			IR ir = IR;

			debug.Report(NOTE, Coords, "checking ir object in \"" + Name
					+ "\" should be \"" + cls + "\" is \"" + ir.GetType() + "\"");
			Debug.Assert(cls.IsInstanceOfType(ir), "checking ir object in \"" + Name
					+ "\" should be \"" + cls + "\" is \"" + ir.GetType() + "\"");

			return (T)ir; // Convert.ChangeType(ir, cls) would resemble cls.Cast(ir) but without an according result type, so pointless ... but at least we can use a cast here and are not forced to switch to as
		}

		/// <summary>
		/// Construct the IR object.
		/// This method should never be called. It is used by <seealso cref="getIR()"/>. </summary>
		/// <returns> The constructed IR object. </returns>
		protected internal virtual IR ConstructIR()
		{
			return Bad.BadObject;
		}

		//////////////////////////////////////////////////////////////////////////////////////////
		// graph dumping
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeColor()"/>
		public virtual Color NodeColor
		{
			get
			{
				return Color.WHITE;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeId()"/>
		public string NodeId
		{
			get
			{
				return Id;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeInfo()"/>
		public virtual string NodeInfo
		{
			get
			{
				string extra = ExtraNodeInfo();
				return "ID: " + Id + (!string.ReferenceEquals(extra, null) ? "\n" + extra : "");
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeLabel()"/>
		public virtual string NodeLabel
		{
			get
			{
				return this.Name;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeShape()"/>
		public virtual Shape NodeShape
		{
			get
			{
				return Shape.DEFAULT;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getEdgeLabel(int)"/>
		public string GetEdgeLabel(int edge)
		{
			ICollection<string> childrenNames = ChildrenNames;
			// iterate to corresponding children name
			int currentEdge = -1;
			foreach(string name in childrenNames)
			{
				++currentEdge;
				if(currentEdge == edge)
					return name;
			}
			return "" + edge;
		}

		private TypeDeclNode FindType(string rootName)
		{
			// get root node
			BaseNode root = this;
			while(!root.IsRoot())
				root = EnumeratorHelper.GetFirstElement(root.Parents);

			// find a root-type
			TypeDeclNode rootType = null;
			ModelNode model = ((UnitNode)root).StdModel;
			Debug.Assert(model.IsResolved());
			ICollection<TypeDeclNode> types = model.decls.ChildrenExact;

			foreach(TypeDeclNode candidate in types)
			{
				string name = candidate.ident.Symbol.Text;
				if(name.Equals(rootName))
					rootType = candidate;
			}
			return rootType;
		}

		public TypeDeclNode NodeRootTypeDecl
		{
			get
			{
				return FindType("Node");
			}
		}

		public TypeDeclNode ArbitraryEdgeRootTypeDecl
		{
			get
			{
				return FindType("AEdge");
			}
		}

		public TypeDeclNode DirectedEdgeRootTypeDecl
		{
			get
			{
				return FindType("Edge");
			}
		}

		public TypeDeclNode UndirectedEdgeRootTypeDecl
		{
			get
			{
				return FindType("UEdge");
			}
		}
	}

}
