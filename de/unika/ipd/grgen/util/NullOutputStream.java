/**
 * NullOutputStream.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.io.IOException;
import java.io.OutputStream;

public class NullOutputStream extends OutputStream {
	
	public static final OutputStream STREAM = new NullOutputStream();
	
	public void write(int p1) throws IOException {
		System.out.println("write to null stream");
	}
	
	private NullOutputStream() {
	}

}

