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
  	reporter.entering("Test1");
		reporter.report(1, "Hallo1");
		reporter.report(2, "Hallo2");
		reporter.report(128, "Never Seen");
		reporter.entering("Test2");
		reporter.report(0, "Hallo4");
		reporter.leaving();
		reporter.report(1, "Hallo5");
		reporter.leaving();
		reporter.report(2, "Hallo6");
		reporter.leaving();  	
  }
}
