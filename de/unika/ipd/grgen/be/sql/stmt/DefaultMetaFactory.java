/**
 * DefaultDialect.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.stmt;
import de.unika.ipd.grgen.be.sql.*;
import de.unika.ipd.grgen.be.sql.meta.*;


import de.unika.ipd.grgen.be.TypeID;
import de.unika.ipd.grgen.ir.ConstraintEntity;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import java.util.List;
import java.util.Map;



public class DefaultMetaFactory implements MetaFactory {
	
	private final TypeStatementFactory stmt;
	
	private final GraphTableFactory table;

	private final Dialect dialect;
	
	public DefaultMetaFactory(Dialect dialect,
														SQLParameters params,
														Map nodeAttrMap,
														Map edgeAttrMap) {
		
		this.dialect = dialect;
		stmt = new DefaultStatementFactory(dialect);
		table = new DefaultGraphTableFactory(params, dialect, nodeAttrMap, edgeAttrMap);
	}
	
	public DataType getBooleanType() {
		return dialect.getBooleanType();
	}
	
	public DataType getStringType() {
		return dialect.getStringType();
	}
	
	public DataType getIntType() {
		return dialect.getIntType();
	}
	
	public DataType getIdType() {
		return dialect.getIdType();
	}
	
	public Term expression(int opcode, Term[] operands) {
		return stmt.expression(opcode, operands);
	}
	
	public Term expression(int opcode, Term exp0, Term exp1, Term exp2) {
		return stmt.expression(opcode, exp0, exp1, exp2);
	}
	
	public Term addExpression(int opcode, Term exp0, Term exp1) {
		return stmt.addExpression(opcode, exp0, exp1);
	}
	
	public Term expression(int opcode, Term exp0, Term exp1) {
		return stmt.expression(opcode, exp0, exp1);
	}
	
	public Term expression(int opcode, Term exp0) {
		return stmt.expression(opcode, exp0);
	}
	
	public Term expression(Column col) {
		return stmt.expression(col);
	}
	
	public Term expression(Query query) {
		return stmt.expression(query);
	}
	
	public Term markerExpression(MarkerSource markerSource, DataType type) {
		return stmt.markerExpression(markerSource, type);
	}
	
	public Term constant(int integer) {
		return stmt.constant(integer);
	}
	
	public Term constant(String string) {
		return stmt.constant(string);
	}
	
	public Term constant(boolean bool) {
		return stmt.constant(bool);
	}
	
	public Term constantNull() {
		return stmt.constantNull();
	}
	
	public Aggregate aggregate(int which, Column col) {
		return stmt.aggregate(which, col);
	}
	
	public Query simpleQuery(List columns, List relations, Term cond, int limit) {
		return stmt.simpleQuery(columns, relations, cond, limit);
	}
	
	public Query simpleQuery(List columns, List relations, Term cond, List groupBy, Term having) {
		return stmt.simpleQuery(columns, relations, cond, groupBy, having);
	}
	
	public Query explicitQuery(boolean distinct, List columns, Relation relation,
														 List groupBy, Term having, int limit) {
		return stmt.explicitQuery(distinct, columns, relation, groupBy, having, limit);
	}
	
	public Join join(int kind, Relation left, Relation right, Term cond) {
		return stmt.join(kind, left, right, cond);
	}
	
	public ManipulationStatement makeUpdate(Table table, List columns, List exprs, Term cond) {
		return stmt.makeUpdate(table, columns, exprs, cond);
	}
	
	public NodeTable nodeTable(Node node) {
		return table.nodeTable(node);
	}
	
	public EdgeTable edgeTable(Edge edge) {
		return table.edgeTable(edge);
	}
	
	public AttributeTable nodeAttrTable(Node node) {
		return table.nodeAttrTable(node);
	}
	
	public AttributeTable edgeAttrTable(Edge edge) {
		return table.edgeAttrTable(edge);
	}
	
	public NodeTable originalNodeTable() {
		return table.originalNodeTable();
	}
	
	public EdgeTable originalEdgeTable() {
		return table.originalEdgeTable();
	}
	
	public AttributeTable originalNodeAttrTable() {
		return table.originalNodeAttrTable();
	}
	
	public AttributeTable originalEdgeAttrTable() {
		return table.originalEdgeAttrTable();
	}
	
	public NodeTable nodeTable(String alias) {
		return table.nodeTable(alias);
	}
	
	public EdgeTable edgeTable(String alias) {
		return table.edgeTable(alias);
	}
	
	public AttributeTable nodeAttrTable(String alias) {
		return table.nodeAttrTable(alias);
	}
	
	public AttributeTable edgeAttrTable(String alias) {
		return table.edgeAttrTable(alias);
	}
	
	public Table neutralTable() {
		return table.neutralTable();
	}
	
	public Table neutralTable(String alias) {
		return table.neutralTable(alias);
	}
	
	public Term isA(TypeIdTable table, NodeType node, TypeID typeID) {
		return stmt.isA(table, node, typeID);
	}
	
	public Term isA(TypeIdTable table, EdgeType edge, TypeID typeID) {
		return stmt.isA(table, edge, typeID);
	}
	
	public Term isA(TypeIdTable table, ConstraintEntity ent, boolean isNode, TypeID typeID) {
		return stmt.isA(table, ent, isNode, typeID);
	}
	
}

