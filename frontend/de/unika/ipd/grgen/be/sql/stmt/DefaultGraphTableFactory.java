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
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.*;

import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.parser.Coords;
import java.io.PrintStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;


/**
 * A factory that produces node/edge and node/edge attribute tables.
 */
public class DefaultGraphTableFactory implements GraphTableFactory {
	
	/** Cache all tables here. */
	protected final Map<Object, Object> entTables = new HashMap<Object, Object>();
	
	/** Cache all attribute tables here. */
	protected final Map<Entity, AttributeTable> attrTables = new HashMap<Entity, AttributeTable>();
	
	/** The SQL parameters. */
	protected final SQLParameters parameters;
	
	/** List of node attributes (entities). */
	protected final Map<Entity, Integer> nodeAttrs;
	
	/** List of edge attributes (entities). */
	protected final Map<Entity, Integer> edgeAttrs;
	
	/** Names of the node table columns. */
	protected final String[] nodeTableColumns;
	
	/** Names of the edge table columns. */
	protected final String[] edgeTableColumns;
	
	/** The node table types. */
	protected final DataType[] nodeTableTypes;
	
	/** The edge table types. */
	protected final DataType[] edgeTableTypes;
	
	protected final NodeTable originalNodeTable;
	
	protected final EdgeTable originalEdgeTable;
	
	protected final AttributeTable originalNodeAttrTable;
	
	protected final AttributeTable originalEdgeAttrTable;
	
	protected final NeutralTable neutralTable;
	
	protected final TypeFactory typeFactory;
	
	public DefaultGraphTableFactory(SQLParameters parameters,
									TypeFactory typeFactory,
									Map<Entity, Integer> nodeAttrs, Map<Entity, Integer> edgeAttrs) {
		
		this.parameters = parameters;
		this.nodeAttrs = nodeAttrs;
		this.edgeAttrs = edgeAttrs;
		this.typeFactory = typeFactory;
		
		nodeTableColumns = new String[] {
			parameters.getColNodesId(),
				parameters.getColNodesTypeId()
		};
		
		edgeTableColumns = new String[] {
			parameters.getColEdgesId(),
				parameters.getColEdgesTypeId(),
				parameters.getColEdgesSrcId(),
				parameters.getColEdgesTgtId()
		};
		
		nodeTableTypes = new DataType[] {
			typeFactory.getIdType(),
				typeFactory.getIntType()
		};
		
		edgeTableTypes = new DataType[] {
			typeFactory.getIdType(),
				typeFactory.getIntType(),
				typeFactory.getIntType(),
				typeFactory.getIntType()
		};
		
		originalNodeTable = new DefaultNodeTable();
		originalEdgeTable = new DefaultEdgeTable();
		originalNodeAttrTable = new DefaultAttributeTable(parameters.getTableNodeAttrs(),
														  parameters.getColNodeAttrNodeId(), nodeAttrs);
		originalEdgeAttrTable = new DefaultAttributeTable(parameters.getTableEdgeAttrs(),
														  parameters.getColEdgeAttrEdgeId(), edgeAttrs);
		
		neutralTable = new NeutralTable("");
	}
	
	/**
	 * This mangles an entity to a string.
	 * If you want to change the mangling behaviour, just inherit and override
	 * this method.
	 * @param ent The entity.
	 * @return A string that is unique for this entity.
	 */
	protected String mangleEntity(Entity ent) {
		final StringBuffer sb = new StringBuffer();
		Ident id = ent.getIdent();
		Coords c = id.getCoords();
		
		sb.append('l').append(c.getLine()).append('c').append(c.getColumn()).append('_');
		if(ent.getOwner()==null)
			sb.append("_toplevel_");
		else
			sb.append(ent.getOwner().getIdent().toString().toLowerCase());
		sb.append('_').append(id);
		
		/*
		 sb.append(ent.getName()).append("_");
		 Ident id = ent.getIdent();
		 sb.append(id.getScope().getName());
		 sb.append(ent.getIdent().toString());
		 */
		
		return mangleString(sb.toString()).toString();
	}
	
	protected StringBuffer mangleString(CharSequence unmangled) {
		final char esc = 'Z';
		StringBuffer sb = new StringBuffer();
		for(int i = 0; i < unmangled.length(); i++) {
			final char ch = unmangled.charAt(i);
			
			switch(ch) {
				case '_':
					sb.append("__");
					break;
				case '$':
					sb.append(esc);
					break;
				case ' ':
					sb.append(esc);
					sb.append('_');
				case esc:
					sb.append(esc);
					sb.append(esc);
					break;
				default:
					if(Character.isUpperCase(ch))
						sb.append("_");
					sb.append(ch);
			}
		}
		
		return sb;
	}
	
	/**
	 * A simple column implementation.
	 */
	protected static class SimpleColumn extends DefaultDebug implements Column {
		protected final String alias;
		protected final String name;
		protected final Relation relation;
		protected final DataType type;
		protected final boolean canBeNull;
		
		SimpleColumn(String name, DataType type, Table table, boolean canBeNull, String debug) {
			super(debug);
			this.name = name;
			this.alias = table.getAliasName() + "." + name;
			this.relation = table;
			this.type = type;
			this.canBeNull = canBeNull;
		}
		
		SimpleColumn(String name, DataType type, Table table, boolean canBeNull) {
			this(name, type, table, canBeNull, "column " + table.getAliasName() + "." + name);
		}
		
		public DataType getType() {
			return type;
		}
		
		public String getDeclName() {
			return name;
		}
		
		public String getAliasName() {
			return alias;
		}
		
		public Relation getRelation() {
			return relation;
		}
		
		public void dump(PrintStream ps) {
			ps.print(getAliasName());
		}
		
		public PrintStream dumpDecl(PrintStream ps) {
			ps.print(getDeclName());
			ps.print(" ");
			getType().dump(ps);
			
			if(!canBeNull)
				ps.print(" NOT NULL");
			
			return ps;
		}
		
		public String toString() {
			return alias;
		}
	}
	
	protected class AliasTable extends DefaultDebug implements IdTable {
		protected final String declaration;
		protected final String alias;
		protected final String name;
		Column[] cols;
		
		AliasTable(String name) {
			this(name, name);
		}
		
		AliasTable(String name, String[] colNames, DataType[] colTypes) {
			this(name, name, colNames, colTypes);
		}
		
		AliasTable(String name, String alias) {
			super("table " + name + (alias.length() != 0 ? " AS " + alias : ""));
			boolean hasAlias = alias.length() != 0;
			this.name = name;
			this.alias = hasAlias ? alias : name;
			this.declaration = hasAlias ? name + " AS " + alias : name;
		}
		
		AliasTable(String name, String alias, String[] colNames, DataType[] colTypes) {
			this(name, alias);
			setColumns(colNames, colTypes);
		}
		
		protected final void setColumns(String[] colNames, DataType[] colTypes) {
			assert colNames.length == colTypes.length
				: "must have the same amount of colums and types";
			
			cols = new Column[colNames.length];
			for(int i = 0; i < cols.length; i++)
				cols[i] = new SimpleColumn(colNames[i], colTypes[i], this, false);
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Table#getName()
		 */
		public String getName() {
			return name;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Declared#getDeclName()
		 */
		public String getDeclName() {
			return getName();
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Aliased#getAliasName()
		 */
		public String getAliasName() {
			return alias;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.MetaBase#dump(java.lang.StringBuffer)
		 */
		public void dump(PrintStream ps) {
			ps.print(declaration);
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Relation#columnCount()
		 */
		public int columnCount() {
			return cols.length;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Relation#getColumn(int)
		 */
		public Column getColumn(int i) {
			return cols[i];
		}
		
		public Column colId() {
			return cols[0];
		}
		
		
		public PrintStream dumpDecl(PrintStream ps) {
			ps.print("CREATE TABLE ");
			ps.print(getDeclName());
			ps.print(" (");
			
			for(int i = 0; i < columnCount(); i++) {
				Column col = getColumn(i);
				ps.print(i > 0 ? ", " : "");
				col.dumpDecl(ps);
			}
			
			ps.print(")");
			return ps;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.AttributeTable#genGetStmt(java.lang.StringBuffer, de.unika.ipd.grgen.ir.Entity)
		 */
		public PrintStream genGetStmt(PrintStream ps, Column col) {
			ps.print("SELECT ");
			ps.print(col.getDeclName());
			ps.print(" FROM ");
			ps.print(getDeclName());
			ps.print(" WHERE ");
			ps.print(colId().getDeclName());
			ps.print(" = $1[");
			ps.print(colId().getType().getText());
			ps.print("]");
			
			return ps;
		}
		
		public Query genGetStmt(StatementFactory factory, MarkerSource ms, Column col) {
			Term cond = factory.expression(Opcodes.EQ,
										   factory.expression(colId()),
										   factory.markerExpression(ms, colId().getType()));
			
			return factory.simpleQuery(Collections.singletonList(col),
									   Collections.singletonList(this),
									   cond, StatementFactory.NO_LIMIT);
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.AttributeTable#genUpdateStmt(java.lang.StringBuffer, de.unika.ipd.grgen.ir.Entity)
		 */
		public PrintStream genUpdateStmt(PrintStream ps, Column col) {
			
			
			
			ps.print("UPDATE ");
			ps.print(getDeclName());
			ps.print(" SET ");
			ps.print(col.getDeclName());
			ps.print(" = $1[");
			ps.print(col.getType().getText());
			ps.print("] WHERE ");
			ps.print(colId().getDeclName());
			ps.print(" = $2[");
			ps.print(colId().getType().getText());
			ps.print("]");
			return ps;
		}
		
		public ManipulationStatement genUpdateStmt(StatementFactory factory,
												   MarkerSource ms, Column col) {
			Term expr = factory.markerExpression(ms, col.getType());
			Term cond = factory.expression(Opcodes.EQ,
										   factory.expression(colId()),
										   factory.markerExpression(ms, colId().getType()));
			
			return factory.makeUpdate(this, Collections.singletonList(col),
									  Collections.singletonList(expr), cond);
		}
		
		public String toString() {
			return alias;
		}
	}
	
	protected class DefaultNodeTable extends AliasTable implements NodeTable {
		
		private final Node node;
		
		DefaultNodeTable(Node node) {
			super(parameters.getTableNodes(), mangleEntity(node), nodeTableColumns, nodeTableTypes);
			this.node = node;
		}
		
		DefaultNodeTable(String alias) {
			super(parameters.getTableNodes(), alias, nodeTableColumns, nodeTableTypes);
			this.node = null;
		}
		
		DefaultNodeTable() {
			this("");
		}
		
		public Column colId() {
			return cols[0];
		}
		
		public Column colTypeId() {
			return cols[1];
		}
		
		public Node getNode() {
			return node;
		}
	}
	
	private class DefaultEdgeTable extends AliasTable implements EdgeTable {
		
		private final Edge edge;
		
		DefaultEdgeTable(Edge edge) {
			super(parameters.getTableEdges(), mangleEntity(edge), edgeTableColumns, edgeTableTypes);
			this.edge = edge;
		}
		
		DefaultEdgeTable(String alias) {
			super(parameters.getTableEdges(), alias, edgeTableColumns, edgeTableTypes);
			this.edge = null;
		}
		
		DefaultEdgeTable() {
			this("");
		}
		
		public Column colId() {
			return cols[0];
		}
		
		public Column colTypeId() {
			return cols[1];
		}
		
		public Column colSrcId() {
			return cols[2];
		}
		
		public Column colTgtId() {
			return cols[3];
		}
		
		public Column colEndId(boolean src) {
			return src ? colSrcId() : colTgtId();
		}
		
		public Edge getEdge() {
			return edge;
		}
	}
	
	private class DefaultAttributeTable extends AliasTable implements AttributeTable {
		
		final Map<Entity, Integer> attrIndices;
		
		DefaultAttributeTable(String name, String idCol, Entity ent, Map<Entity, Integer> attrIndices) {
			this(name, idCol, mangleEntity(ent) + "_attr", attrIndices);
		}
		
		DefaultAttributeTable(String name, String idCol, Map<Entity, Integer> attrIndices) {
			this(name, idCol, "", attrIndices);
		}
		
		DefaultAttributeTable(String name, String idCol, String alias, Map<Entity, Integer> attrIndices) {
			super(name, alias);
			this.attrIndices = attrIndices;
			Set<Entity> attrs = attrIndices.keySet();
			cols = new Column[attrs.size() + 1];
			cols[0] = new SimpleColumn(idCol, typeFactory.getIdType(), this, false);
			
			for(Iterator<Entity> it = attrs.iterator(); it.hasNext();) {
				Entity e = it.next();
				int index = attrIndices.get(e).intValue();
				
				cols[index + 1] =
					new SimpleColumn(mangleEntity(e), getDataType(e.getType()), this, true);
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.AttributeTable#colEntity(de.unika.ipd.grgen.ir.Entity)
		 */
		public Column colEntity(Entity ent) {
			Object obj = attrIndices.get(ent);
			assert obj != null : "" + ent + " " + ent.getId() + " not in table " + getAliasName();
			if(obj != null && obj instanceof Integer) {
				int index = ((Integer) obj).intValue() + 1;
				assert index >= 0 && index < cols.length : "Index is wrong";
				return cols[index];
			}
			
			return null;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.IdTable#colId()
		 */
		public Column colId() {
			return cols[0];
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory#edgeTable(de.unika.ipd.grgen.ir.Edge)
	 */
	public EdgeTable edgeTable(Edge edge) {
		EdgeTable res = null;
		
		if(entTables.containsKey(edge))
			res = (EdgeTable) entTables.get(edge);
		else {
			res = new DefaultEdgeTable(edge);
			entTables.put(edge, res);
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory#nodeTable(de.unika.ipd.grgen.ir.Node)
	 */
	public NodeTable nodeTable(Node node) {
		NodeTable res = null;
		
		if(entTables.containsKey(node))
			res = (NodeTable) entTables.get(node);
		else {
			res = new DefaultNodeTable(node);
			entTables.put(node, res);
		}
		
		return res;
	}
	
	private AttributeTable checkAttrTable(String name, String idCol, Entity ent, Map<Entity, Integer> indexMap) {
		AttributeTable res;
		
		if(attrTables.containsKey(ent))
			res = attrTables.get(ent);
		else {
			res = new DefaultAttributeTable(name, idCol, ent, indexMap);
			attrTables.put(ent, res);
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#nodeAttrTable(de.unika.ipd.grgen.ir.Node)
	 */
	public AttributeTable nodeAttrTable(Node node) {
		return checkAttrTable(parameters.getTableNodeAttrs(), parameters.getColNodeAttrNodeId(),
							  node, nodeAttrs);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#edgeAttrTable(de.unika.ipd.grgen.ir.Edge)
	 */
	public AttributeTable edgeAttrTable(Edge edge) {
		return checkAttrTable(parameters.getTableEdgeAttrs(), parameters.getColEdgeAttrEdgeId(),
							  edge, edgeAttrs);
	}
	
	
	
	private DataType getDataType(Type type) {
		switch(type.classify()) {
			case Type.IS_STRING:
				return typeFactory.getStringType();
			case Type.IS_BOOLEAN:
				return typeFactory.getBooleanType();
			case Type.IS_INTEGER:
				return typeFactory.getIntType();
			default:
				assert false : "No SQL data type found for: " + type;
		}
		return null;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalEdgeAttrTable()
	 */
	public AttributeTable originalEdgeAttrTable() {
		return originalEdgeAttrTable;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalEdgeTable()
	 */
	public EdgeTable originalEdgeTable() {
		return originalEdgeTable;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalNodeAttrTable()
	 */
	public AttributeTable originalNodeAttrTable() {
		return originalNodeAttrTable;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalNodeTable()
	 */
	public NodeTable originalNodeTable() {
		return originalNodeTable;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalEdgeAttrTable()
	 */
	public AttributeTable edgeAttrTable(String alias) {
		return new DefaultAttributeTable(parameters.getTableEdgeAttrs(),
										 parameters.getColEdgeAttrEdgeId(),
										 alias, edgeAttrs);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalEdgeTable()
	 */
	public EdgeTable edgeTable(String alias) {
		EdgeTable res;
		
		if(entTables.containsKey(alias))
			res = (EdgeTable) entTables.get(alias);
		else {
			res = new DefaultEdgeTable(alias);
			entTables.put(alias, res);
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalNodeAttrTable()
	 */
	public AttributeTable nodeAttrTable(String alias) {
		return new DefaultAttributeTable(parameters.getTableNodeAttrs(),
										 parameters.getColNodeAttrNodeId(),
										 alias, nodeAttrs);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#originalNodeTable()
	 */
	public NodeTable nodeTable(String alias) {
		NodeTable res;
		
		if(entTables.containsKey(alias))
			res = (NodeTable) entTables.get(alias);
		else {
			res = new DefaultNodeTable(alias);
			entTables.put(alias, res);
		}
		return res;
	}
	
	protected class NeutralTable extends AliasTable {
		NeutralTable(String alias) {
			super("neutral", alias, new String[] { "id" },
				  new DataType[] { typeFactory.getIntType() });
		}
	}
	
	public Table neutralTable() {
		return neutralTable;
	}
	
	public Table neutralTable(String alias) {
		return new NeutralTable(alias);
	}
}


