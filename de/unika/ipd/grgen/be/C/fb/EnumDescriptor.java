/**
 * EnumDescriptor.java
 *
 * @author Veit Batz
 * @version $Id$
 */

package de.unika.ipd.grgen.be.C.fb;
import java.util.Vector;
import de.unika.ipd.grgen.ir.EnumItem;



public class EnumDescriptor
{
	public int  type_id;
	public String name;
	public int n_items;
	public Vector<EnumItem> items = new Vector<EnumItem>();
}

