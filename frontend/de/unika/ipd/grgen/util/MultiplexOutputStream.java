/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * MultiplexOutputStream.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.io.IOException;
import java.io.OutputStream;

public class MultiplexOutputStream extends OutputStream
{
	private final OutputStream[] mux = new OutputStream[32];

	private int fill = 0;

	public void addStream(OutputStream s)
	{
		if(fill == mux.length - 1) {
			OutputStream[] newMux = new OutputStream[2 * fill];
			System.arraycopy(mux, 0, newMux, 0, mux.length);
		}

		mux[fill++] = s;
	}

	/**
	 * Writes the specified byte to this output stream. The general
	 * contract for <code>write</code> is that one byte is written
	 * to the output stream. The byte to be written is the eight
	 * low-order bits of the argument <code>b</code>. The 24
	 * high-order bits of <code>b</code> are ignored.
	 * <p>
	 * Subclasses of <code>OutputStream</code> must provide an
	 * implementation for this method.
	 *
	 * @param      b   the <code>byte</code>.
	 * @exception  IOException  if an I/O error occurs. In particular,
	 *             an <code>IOException</code> may be thrown if the
	 *             output stream has been closed.
	 */
	@Override
	public void write(int b) throws IOException
	{
		for(int i = 0; i < fill; i++) {
			mux[i].write(b);
		}
	}

	/**
	 * Writes <code>len</code> bytes from the specified byte array
	 * starting at offset <code>off</code> to this output stream.
	 * The general contract for <code>write(b, off, len)</code> is that
	 * some of the bytes in the array <code>b</code> are written to the
	 * output stream in order; element <code>b[off]</code> is the first
	 * byte written and <code>b[off+len-1]</code> is the last byte written
	 * by this operation.
	 * <p>
	 * The <code>write</code> method of <code>OutputStream</code> calls
	 * the write method of one argument on each of the bytes to be
	 * written out. Subclasses are encouraged to override this method and
	 * provide a more efficient implementation.
	 * <p>
	 * If <code>b</code> is <code>null</code>, a
	 * <code>NullPointerException</code> is thrown.
	 * <p>
	 * If <code>off</code> is negative, or <code>len</code> is negative, or
	 * <code>off+len</code> is greater than the length of the array
	 * <code>b</code>, then an <tt>IndexOutOfBoundsException</tt> is thrown.
	 *
	 * @param      b     the data.
	 * @param      off   the start offset in the data.
	 * @param      len   the number of bytes to write.
	 * @exception  IOException  if an I/O error occurs. In particular,
	 *             an <code>IOException</code> is thrown if the output
	 *             stream is closed.
	 */
	@Override
	public void write(byte[] b, int off, int len) throws IOException
	{
		for(int i = 0; i < fill; i++) {
			mux[i].write(b, off, len);
		}
	}

	/**
	 * Writes <code>b.length</code> bytes from the specified byte array
	 * to this output stream. The general contract for <code>write(b)</code>
	 * is that it should have exactly the same effect as the call
	 * <code>write(b, 0, b.length)</code>.
	 *
	 * @param      b   the data.
	 * @exception  IOException  if an I/O error occurs.
	 * @see        java.io.OutputStream#write(byte[], int, int)
	 */
	@Override
	public void write(byte[] b) throws IOException
	{
		write(b, 0, b.length);
	}

	/**
	 * Flushes this output stream and forces any buffered output bytes
	 * to be written out. The general contract of <code>flush</code> is
	 * that calling it is an indication that, if any bytes previously
	 * written have been buffered by the implementation of the output
	 * stream, such bytes should immediately be written to their
	 * intended destination.
	 * <p>
	 * If the intended destination of this stream is an abstraction provided by
	 * the underlying operating system, for example a file, then flushing the
	 * stream guarantees only that bytes previously written to the stream are
	 * passed to the operating system for writing; it does not guarantee that
	 * they are actually written to a physical device such as a disk drive.
	 * <p>
	 * The <code>flush</code> method of <code>OutputStream</code> does nothing.
	 *
	 * @exception  IOException  if an I/O error occurs.
	 */
	@Override
	public void flush() throws IOException
	{
		for(int i = 0; i < fill; i++) {
			mux[i].flush();
		}
	}

	/**
	 * Closes this output stream and releases any system resources
	 * associated with this stream. The general contract of <code>close</code>
	 * is that it closes the output stream. A closed stream cannot perform
	 * output operations and cannot be reopened.
	 * <p>
	 * The <code>close</code> method of <code>OutputStream</code> does nothing.
	 *
	 * @exception  IOException  if an I/O error occurs.
	 */
	@Override
	public void close() throws IOException
	{
		for(int i = 0; i < fill; i++) {
			mux[i].close();
		}
	}
}
