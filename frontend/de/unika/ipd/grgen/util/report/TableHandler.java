/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Created on Feb 29, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import javax.swing.event.TableModelListener;
import javax.swing.table.TableModel;


/**
 * A reporter that is a table model. So reported messages can be viewed
 * using a JTable.
 */
public class TableHandler implements TableModel, Handler {

	private static final String[] msgNames = {
		"error", "warning", "note", "debug", "note"
	};

	private static final int COL_DEGREE = 0;
	private static final int COL_MSG = 1;

	private static final String[] columnNames = {
		"degree", "message"
	};

	private static class Message {

		byte degree;
		String msg;

		public Message(int degree, String msg) {
			this.degree = (byte) degree;
			this.msg = msg;

			if(degree < 0 || degree > 4)
				throw new IllegalArgumentException("degree is out of range!");
		}
	}

	private List<TableModelListener> listeners = new LinkedList<TableModelListener>();

	private Vector<TableHandler.Message> messages = new Vector<TableHandler.Message>();

	/**
	 * Add a listener.
	 * @param listener The listener.
	 */
	public void addTableModelListener(TableModelListener listener) {
		listeners.add(listener);
	}

	/**
	 * Remove a listener.
	 * @param listener The listener.
	 */
	public void removeTableModelListener(TableModelListener listener) {
		listeners.remove(listener);
	}

	/**
	 *
	 */
	public Class<String> getColumnClass(int arg0) {
		return String.class;
	}
	/**
	 *
	 */

	public int getColumnCount() {
		return columnNames.length;
	}

	/**
	 *
	 */
	public String getColumnName(int col) {
		return columnNames[col];
	}

	/**
	 *
	 */
	public int getRowCount() {
		return messages.size();
	}

	/**
	 *
	 */
	public Object getValueAt(int row, int col) {
		Message m = messages.get(row);
		String res = "";

		switch(col) {
		case COL_DEGREE:
			res = msgNames[m.degree];
			break;
		case COL_MSG:
			res = m.msg;
			break;
		}

		return res;
	}

	/**
	 * Is a cell editable.
	 * @param row The row.
	 * @param col The column.
	 * @return We always return false here.
	 */
	public boolean isCellEditable(int row, int col) {
		return false;
	}

	/**
	 * Editing is not allowed, so empty body here.
	 */
	public void setValueAt(Object arg0, int arg1, int arg2) {
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Handler#entering(java.lang.String)
	 */
	public void entering(String s) {
		// left empty.
	}

	public void leaving() {
		// left empty.
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Handler#report(int, de.unika.ipd.grgen.util.report.Location, java.lang.String)
	 */
	public void report(int level, Location loc, String msg) {
		messages.add(new Message(level, msg));
	}
}
