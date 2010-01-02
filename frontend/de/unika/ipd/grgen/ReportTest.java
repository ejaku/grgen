/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen;

import java.awt.BorderLayout;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JTree;

import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.Reporter;
import de.unika.ipd.grgen.util.report.StreamHandler;
import de.unika.ipd.grgen.util.report.TreeHandler;

/**
 *
 */
public class ReportTest extends JPanel {

	/**
	 *
	 */
	private static final long serialVersionUID = 3732095867547713469L;
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
