/**
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.meta.Column;
import de.unika.ipd.grgen.be.sql.meta.DataType;
import de.unika.ipd.grgen.be.sql.meta.Relation;
import de.unika.ipd.grgen.be.sql.meta.Table;
import de.unika.ipd.grgen.be.sql.meta.TypeFactory;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Type;


/**
 * A factory that produces node/edge and node/edge attribute tables. 
 */
public class DefaultGraphTableFactory implements GraphTableFactory, TypeFactory {

	/** Cache all tables here. */
	protected final Map entTables = new HashMap();
	
	/** Cache all attribute tables here. */
	protected final Map attrTables = new HashMap();
	
	/** The SQL parameters. */
	protected final SQLParameters parameters;
	
	/** List of node attributes (entities). */
	protected final Collection nodeAttrs;
	
	/** List of edge attributes (entities). */
	protected final Collection edgeAttrs;
	
	/** Names of the node table columns. */
	protected final String[] nodeTableColumns;
	
	/** Names of the edge table columns. */
	protected final String[] edgeTableColumns;
	
	/** Map a node attribute to an index. */ 
	protected final Map nodeAttrIndices = new HashMap();
	
	/** Map an edge attribute to an index. */ 
	protected final Map edgeAttrIndices = new HashMap();
	
	/** The int type. */
	private final DataType intType = new DefaultDataType("int");
	
	/** The string type. */
	private final DataType stringType = new DefaultDataType("text");
	
	/** The boolean type. */
	private final DataType booleanType = new DefaultDataType("boolean");
	
	/** The node table types. */
	protected final DataType[] nodeTableTypes;  

	/** The edge table types. */
	protected final DataType[] edgeTableTypes;  

	protected final NodeTable originalNodeTable;
	
	protected final EdgeTable originalEdgeTable;

	protected final AttributeTable originalNodeAttrTable;

	protected final AttributeTable originalEdgeAttrTable;

	public DefaultGraphTableFactory(SQLParameters parameters, 
			Collection nodeAttrs, Collection edgeAttrs) {
		
		this.parameters = parameters;
		this.nodeAttrs = nodeAttrs;
		this.edgeAttrs = edgeAttrs;
		
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
				getIntType(),
				getIntType()
		};
		
		edgeTableTypes = new DataType[] {
				getIntType(),
				getIntType(),
				getIntType(),
				getIntType()
		};
		
		int i = 0;
		for(Iterator it = nodeAttrs.iterator(); it.hasNext(); i++) {
			nodeAttrIndices.put(it.next(), new Integer(i));
		}

		i = 0;
		for(Iterator it = edgeAttrs.iterator(); it.hasNext(); i++) {
			edgeAttrIndices.put(it.next(), new Integer(i));
		}
		
		
		
		originalNodeTable = new DefaultNodeTable();
		originalEdgeTable = new DefaultEdgeTable();		
		originalNodeAttrTable = new DefaultAttributeTable(parameters.getTableNodeAttrs(), 
			nodeAttrIndices);
		originalEdgeAttrTable = new DefaultAttributeTable(parameters.getTableEdgeAttrs(), 
			nodeAttrIndices);

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
		final char esc = 'Z';
		String unmangled = ent.getIdent().toString();
		
		sb.append(ent.getName());
		sb.append('_');
		
		for(int i = 0; i < unmangled.length(); i++) {
			final char ch = unmangled.charAt(i);
			
			switch(ch) {
			case '_':
				sb.append("__");
				break;
			case '$':
				sb.append(esc);
				break;
			case esc:
				sb.append(esc);
				sb.append(esc);
				break;
			default:
				if(Character.isUpperCase(ch)) {
					sb.append("_");
				}
				
				sb.append(ch);	
			}
			
		}
		
		return sb.toString();
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
		
		public StringBuffer dump(StringBuffer sb) {
			return sb.append(getAliasName());
		}
		
		public StringBuffer dumpDecl(StringBuffer sb) {
			sb.append(getDeclName()).append(" ");
			getType().dump(sb);
			if(!canBeNull) 
				sb.append(" NOT NULL");
			
			return sb;
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
			super("table " + name + " AS " + alias);
			this.name = name;
			this.alias = alias;
			this.declaration = name.equals(alias) ? name : name + " AS " + alias;
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
		public StringBuffer dump(StringBuffer sb) {
			return sb.append(declaration);
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
		
		
		public StringBuffer dumpDecl(StringBuffer sb) {
			sb.append("CREATE TABLE ");
			sb.append(getDeclName());
			sb.append(" (");
			
			for(int i = 0; i < columnCount(); i++) {
				Column col = getColumn(i);
				sb.append(i > 0 ? ", " : "");
				col.dumpDecl(sb);
			}
			
			sb.append(")");
			return sb;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.AttributeTable#genGetStmt(java.lang.StringBuffer, de.unika.ipd.grgen.ir.Entity)
		 */
		public StringBuffer genGetStmt(StringBuffer sb, Column col) {
			sb.append("SELECT ");
			sb.append(col.getDeclName());
			sb.append(" FROM ");
			sb.append(getDeclName());
			sb.append(" WHERE ");
			sb.append(colId().getDeclName());
			sb.append(" = $1[");
			sb.append(colId().getType().getText());
			sb.append("]");
			
			return sb;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.AttributeTable#genUpdateStmt(java.lang.StringBuffer, de.unika.ipd.grgen.ir.Entity)
		 */
		public StringBuffer genUpdateStmt(StringBuffer sb, Column col) {
			sb.append("UPDATE ");
			sb.append(getDeclName());
			sb.append(" SET ");
			sb.append(col.getDeclName());
			sb.append(" = $1[");
			sb.append(col.getType().getText());
			sb.append("] WHERE ");
			sb.append(colId().getDeclName());
			sb.append(" = $2[");
			sb.append(colId().getType().getText());
			sb.append("]");
			return sb;
		}
	}
	
	protected class DefaultNodeTable extends AliasTable implements NodeTable {
		
		DefaultNodeTable(Node node) {
			super(parameters.getTableNodes(), mangleEntity(node), nodeTableColumns, nodeTableTypes);
		}
		
		DefaultNodeTable() {
			super(parameters.getTableNodes(), parameters.getTableNodes(), 
				nodeTableColumns, nodeTableTypes);
		}
		
		public Column colId() {
			return cols[0];
		}
		
		public Column colTypeId() {
			return cols[1];
		}
	}
	
	private class DefaultEdgeTable extends AliasTable implements EdgeTable {
		
		DefaultEdgeTable(Edge edge) {
			super(parameters.getTableEdges(), mangleEntity(edge), edgeTableColumns, edgeTableTypes);
		}
		
		DefaultEdgeTable() {
			super(parameters.getTableEdges(), parameters.getTableEdges(), 
				edgeTableColumns, edgeTableTypes);
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
	}
	
	private class DefaultAttributeTable extends AliasTable implements AttributeTable {
	
		final Map attrIndices;
		
		DefaultAttributeTable(String name, Entity ent, Map attrIndices) {
			this(name, mangleEntity(ent) + "_attr", attrIndices);
		}
		
		DefaultAttributeTable(String name, Map attrIndices) {
			this(name, name, attrIndices);
		}
		
		DefaultAttributeTable(String name, String alias, Map attrIndices) {
			super(name, alias);
			this.attrIndices = attrIndices;
			Set attrs = attrIndices.keySet();
			
			cols = new Column[attrs.size() + 1];
			cols[0] = new SimpleColumn("id", getIntType(), this, false); 
			
			for(Iterator it = attrs.iterator(); it.hasNext();) {
				Entity e = (Entity) it.next();
				int index = ((Integer) attrIndices.get(e)).intValue();
				cols[index + 1] = 
					new SimpleColumn(mangleEntity(e), getDataType(e.getType()), this, true); 
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.stmt.AttributeTable#colEntity(de.unika.ipd.grgen.ir.Entity)
		 */
		public Column colEntity(Entity ent) {
			Object obj = attrIndices.get(ent);
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
		EdgeTable res;
		
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
		NodeTable res;
		
		if(entTables.containsKey(node)) 
			res = (NodeTable) entTables.get(node);
		else {
			res = new DefaultNodeTable(node);
			entTables.put(node, res);
		}
		
		return res;
	}

	private AttributeTable checkAttrTable(String name, Entity ent, Map indexMap) {
		AttributeTable res;
		
		if(attrTables.containsKey(ent))
			res = (AttributeTable) attrTables.get(ent);
		else {
			res = new DefaultAttributeTable(name, ent, indexMap);
			attrTables.put(ent, res);
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#nodeAttrTable(de.unika.ipd.grgen.ir.Node)
	 */
	public AttributeTable nodeAttrTable(Node node) {
		return checkAttrTable(parameters.getTableNodeAttrs(), node, nodeAttrIndices);
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory#edgeAttrTable(de.unika.ipd.grgen.ir.Edge)
	 */
	public AttributeTable edgeAttrTable(Edge edge) {
		return checkAttrTable(parameters.getTableEdgeAttrs(), edge, edgeAttrIndices);
	}
	
	
	protected static class DefaultDataType implements DataType {
		private String text;
		
		DefaultDataType(String text) {
			this.text = text;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.DataType#getText()
		 */
		public String getText() {
			return text;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.MetaBase#debugInfo()
		 */
		public String debugInfo() {
			return "type " + text;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.MetaBase#dump(java.lang.StringBuffer)
		 */
		public StringBuffer dump(StringBuffer sb) {
			return sb.append(getText());
		}
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.TypeFactory#getBooleanType()
	 */
	public DataType getBooleanType() {
		return booleanType;
	}
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.TypeFactory#getIntType()
	 */
	public DataType getIntType() {
		return intType;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.TypeFactory#getStringType()
	 */
	public DataType getStringType() {
		return stringType;
	}
	
	private DataType getDataType(Type type) {
		switch(type.classify()) {
		case Type.IS_STRING:
			return getStringType();
		case Type.IS_BOOLEAN:
			return getBooleanType();
		case Type.IS_INTEGER:
			return getIntType();
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
		return originalNodeTable();
	}
}

