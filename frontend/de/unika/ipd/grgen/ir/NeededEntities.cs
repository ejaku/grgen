/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using MemberExpression = de.unika.ipd.grgen.ir.expr.MemberExpression;
	using ArrayMapExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMapExpr;
	using ArrayRemoveIfExpr = de.unika.ipd.grgen.ir.expr.array.ArrayRemoveIfExpr;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// Holds a collection of entities needed by an expression.
	/// </summary>
	public class NeededEntities
	{
		// specifies the needed entities that are to be collected
		public enum Needs
		{
			NODES = 1, // Specifies, whether needed nodes shall be collected.
			EDGES = 2, // Specifies, whether needed edges shall be collected.
			VARS = 4, // Specifies, whether needed variables shall be collected.
			ALL_ENTITIES = 8, // Specifies, whether all needed entities (nodes, edges, vars) shall be collected.
			ALL_ATTRIBUTES = 16, // Specifies, whether all pattern graph entities needed for attributes
							// and the according attributes shall be collected. If this is true,
							// the pattern graph entities used to access the attributes will not be
							// automatically added to the nodes, edges, and entities sets, but only
							// in the attrNodes and attrEdges sets.
			CONTAINER_EXPRS = 32, // Specifies, whether map, set, array, deque expressions shall be collected.
			COMPUTATION_CONTEXT = 64, // Specifies, whether entities declared in computation context shall be collected.
			MEMBERS = 128, // Specifies, whether entities referenced in member expressions
					 // of member initializations in the model shall be collected.
			LAMBDAS = 256 // Specifies, whether lamba expressions (to be evaluated multiple times) shall be collected
					// also causes lambda expression variables to appear in the variables/entities in case these are collected.
		}

		/// <summary>
		/// Instantiates a new NeededEntities object.
		/// </summary>
		public NeededEntities(Needs needs)
		{
			if((needs & Needs.NODES) == Needs.NODES)
				nodes = new LinkedHashSet<Node>();
			if((needs & Needs.EDGES) == Needs.EDGES)
				edges = new LinkedHashSet<Edge>();
			if((needs & Needs.VARS) == Needs.VARS)
				variables = new LinkedHashSet<Variable>();
			if((needs & Needs.ALL_ENTITIES) == Needs.ALL_ENTITIES)
				entities = new LinkedHashSet<Entity>();
			if((needs & Needs.ALL_ATTRIBUTES) == Needs.ALL_ATTRIBUTES)
			{
				attrEntityMap = new LinkedHashMap<GraphEntity, ISet<Entity>>();
				attrNodes = new LinkedHashSet<Node>();
				attrEdges = new LinkedHashSet<Edge>();
			}
			if((needs & Needs.CONTAINER_EXPRS) == Needs.CONTAINER_EXPRS)
			{
				this.collectContainerExprs = true;
				containerExprs = new LinkedHashSet<Expression>();
			}
			if((needs & Needs.MEMBERS) == Needs.MEMBERS)
				members = new LinkedHashSet<Entity>();
			if((needs & Needs.COMPUTATION_CONTEXT) == Needs.COMPUTATION_CONTEXT)
				collectComputationContext = true;
			if((needs & Needs.LAMBDAS) == Needs.LAMBDAS)
				lambdaExprs = new LinkedHashSet<Expression>();
		}

		/// <summary>
		/// Specifies whether the graph is needed.
		/// </summary>
		public bool isGraphUsed;

		/// <summary>
		/// The nodes needed.
		/// </summary>
		public ISet<Node> nodes;

		/// <summary>
		/// The edges needed.
		/// </summary>
		public ISet<Edge> edges;

		/// <summary>
		/// The variables needed.
		/// </summary>
		public ISet<Variable> variables;

		/// <summary>
		/// The entities needed (nodes, edges, and variables).
		/// </summary>
		public ISet<Entity> entities;

		/// <summary>
		/// The members needed (from member expressions for member initialization).
		/// </summary>
		public ISet<Entity> members;

		/// <summary>
		/// The pattern graph entities needed for attributes mapped to the according attributes.
		/// </summary>
		public IDictionary<GraphEntity, ISet<Entity>> attrEntityMap;

		/// <summary>
		/// The nodes needed for attributes.
		/// </summary>
		public ISet<Node> attrNodes;

		/// <summary>
		/// The edges needed for attributes.
		/// </summary>
		public ISet<Edge> attrEdges;

		/// <summary>
		/// Specifies whether container expressions should be collected.
		/// Needs to temporarily set to false, that's why nulling containerExprs is not sufficient.
		/// </summary>
		public bool collectContainerExprs;

		/// <summary>
		/// The container expressions.
		/// </summary>
		public ISet<Expression> containerExprs;

		/// <summary>
		/// Specifies whether entities declared in computation context should be collected.
		/// </summary>
		public bool collectComputationContext = false;

		/// <summary>
		/// The lambda expressions.
		/// </summary>
		public ISet<Expression> lambdaExprs;

		/// <summary>
		/// Adds a needed graph entity. </summary>
		/// <param name="entity"> The needed entity. </param>
		public virtual void Add(GraphEntity entity)
		{
			if((entity.Context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
					&& !collectComputationContext)
				return;

			if(entity is Node)
			{
				if(nodes != null)
					nodes.Add((Node)entity);
			}
			else if(entity is Edge)
			{
				if(edges != null)
					edges.Add((Edge)entity);
			}
			else
				throw new System.NotSupportedException("Unsupported entity (" + entity + ")");

			if(entities != null)
				entities.Add(entity);
		}

		/// <summary>
		/// Adds a needed node. </summary>
		/// <param name="node"> The needed node. </param>
		public virtual void Add(Node node)
		{
			if((node.Context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
					&& !collectComputationContext)
				return;
			if(nodes != null)
				nodes.Add(node);
			if(entities != null)
				entities.Add(node);
		}

		/// <summary>
		/// Adds a needed edge. </summary>
		/// <param name="edge"> The needed edge. </param>
		public virtual void Add(Edge edge)
		{
			if((edge.Context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
					&& !collectComputationContext)
				return;
			if(edges != null)
				edges.Add(edge);
			if(entities != null)
				entities.Add(edge);
		}

		/// <summary>
		/// Adds a needed variable. </summary>
		/// <param name="var"> The needed variable. </param>
		public virtual void Add(Variable var)
		{
			if((var.Context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
					&& !collectComputationContext)
				return;
			if(variables != null && (!var.isLambdaExpressionVariable || lambdaExprs != null))
				variables.Add(var);
			if(entities != null && (!var.isLambdaExpressionVariable || lambdaExprs != null))
				entities.Add(var);
		}

		/// <summary>
		/// Adds a needed attribute. </summary>
		/// <param name="grEnt"> The entity being accessed. </param>
		/// <param name="attr"> The needed attribute. </param>
		public virtual void AddAttr(GraphEntity grEnt, Entity attr)
		{
			if((grEnt.Context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION
					&& !collectComputationContext)
				return;

			if(attrEntityMap == null)
			{
				Add(grEnt);
				return;
			}

			ISet<Entity> attrs;
			attrEntityMap.TryGetValue(grEnt, out attrs);
			if(attrs == null)
				attrEntityMap[grEnt] = attrs = new LinkedHashSet<Entity>();
			attrs.Add(attr);

			if(grEnt is Node)
				attrNodes.Add((Node)grEnt);
			else if(grEnt is Edge)
				attrEdges.Add((Edge)grEnt);
			else
				throw new System.NotSupportedException("Unsupported entity (" + grEnt + ")");
		}

		/// <summary>
		/// Adds a container expression. </summary>
		/// <param name="expr"> The container expressions. </param>
		public virtual void Add(Expression expr)
		{
			if(collectContainerExprs)
				containerExprs.Add(expr);
		}

		/// <summary>
		/// Adds a member expression. </summary>
		/// <param name="expr"> The member expressions. </param>
		public virtual void Add(MemberExpression expr)
		{
			if(members != null)
				members.Add(expr.Member);
		}

		/// <summary>
		/// Adds a lambda expression. </summary>
		/// <param name="expr"> The lambda expressions. </param>
		public virtual void Add(ArrayMapExpr expr)
		{
			if(lambdaExprs != null)
				lambdaExprs.Add(expr);
		}

		/// <summary>
		/// Adds a lambda expression. </summary>
		/// <param name="expr"> The lambda expressions. </param>
		public virtual void Add(ArrayRemoveIfExpr expr)
		{
			if(lambdaExprs != null)
				lambdaExprs.Add(expr);
		}

		public virtual void NeedsGraph()
		{
			isGraphUsed = true;
		}
	}

}
