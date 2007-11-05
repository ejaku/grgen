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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen;

import de.unika.ipd.grgen.util.report.*;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

/**
 *
 */
public class ReportTest extends JPanel {
	
	Reporter reporter;
	
	public ReportTest(JFrame frame) {
		TreeHandler treeHandler = new TreeHandler();
		StreamHandler streamHandler = new StreamHandler(System.out);
		reporter = new ErrorReporter();
		reporter.addHandler(treeHandler);
		reporter.addHandler(streamHandler);
		
		reportSomething();
		
		JTree tree = new JTree(treeHandler);
		
		setLayout(new BorderLayout());
		setPreferredSize(new Dimension(800, 600));
		add(tree, BorderLayout.CENTER);
	}
	
	
	public static void main(String[] args) {
		
		JFrame frame = new JFrame("Tree Handler Demo");
		ReportTest test = new ReportTest(frame);
		
		Container contentPane = frame.getContentPane();
		contentPane.setLayout(new GridLayout(1, 0));
		contentPane.add(test);
		
		frame.addWindowListener(new WindowAdapter() {
					public void windowClosing(WindowEvent e) {
						System.exit(0);
					}
				});
		
		frame.pack();
		frame.setVisible(true);
		
		
	}
	
	public void reportSomething() {
		reporter.report(0, "Hallo");
		reporter.report(1, "Hallo1");
		reporter.report(2, "Hallo2");
		reporter.report(128, "Never Seen");
		reporter.report(0, "Hallo4");
		reporter.report(1, "Hallo5");
		reporter.report(2, "Hallo6");
	}
}
