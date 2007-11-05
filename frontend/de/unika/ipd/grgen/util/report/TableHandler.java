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
		"error", "warning", "note", "debug"
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
