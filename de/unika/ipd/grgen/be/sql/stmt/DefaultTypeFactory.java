/**
 * DefaultTypeFactory.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.TypeFactory;
import de.unika.ipd.grgen.be.sql.meta.DataType;

public class DefaultTypeFactory implements TypeFactory {
	
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
	
	/** The int type. */
	protected final DataType intType = new DefaultDataType("int");
	
	/** The string type. */
	protected final DataType stringType = new DefaultDataType("text");
	
	/** The boolean type. */
	protected final DataType booleanType = new DefaultDataType("int");
	
	public DataType getIntType() {
		return intType;
	}
	
	public DataType getStringType() {
		return stringType;
	}
	
	public DataType getBooleanType() {
		return booleanType;
	}
	
}

