/**
 * Created on Mar 15, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.ui;

import javax.swing.*;

import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.TableHandler;
import de.unika.ipd.libgr.graph.Graph;
import de.unika.ipd.libgr.ui.util.TypeModelInfoPanel;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.LinkedList;
import java.util.List;
import javax.swing.filechooser.FileFilter;


/**
 *
 */
public class UITest extends Base {
	
	private final JFrame frame;
	
	private final Dimension screenSize;
	
	private final JPanel drawingArea;
	
	private final JTabbedPane graphPane;
	
	private final List inputs = new LinkedList();
	
	private final List layouters = new LinkedList();
	
	private final ErrorReporter reporter = new ErrorReporter();
	
	private final FileFilter grgFileFilter = new ExtensionFileFilter("grg", "GrGen File");

	private Graph makeNewGraph(String name) {
		
		return null;
	}
	
	private Unit loadFile() {
    JFileChooser chooser = new JFileChooser();
    chooser.setFileFilter(grgFileFilter);
    int returnVal = chooser.showOpenDialog(frame);
    if(returnVal == JFileChooser.APPROVE_OPTION) {
    	
    	String filename = chooser.getSelectedFile().getName();
    	
      reporter.note("You chose to open this file: " + filename);
      frame.repaint();
       
   		boolean res = false;

			// TODO Parse file here.
		}
    return null;
	}
	
	private JMenuBar buildMenu() {
		JMenuItem item;
		
		JMenuBar mb = new JMenuBar();
		JMenu fileMenu = new JMenu("File");
		
		item = new JMenuItem("Load");
		fileMenu.add(item);
		fileMenu.addSeparator();
		
		item = new JMenuItem("Exit");
		item.addActionListener(exitListener);
		fileMenu.add(item);
		
		mb.add(fileMenu);
		
		return mb;
	}
	
	private ActionListener exitListener = new ActionListener() {
		public void actionPerformed(ActionEvent e) {
			System.exit(0);
		}
	};
	
	public UITest() {
		
		// This is the main drawing area
		drawingArea = new JPanel();
		
		// Get the screen size
		screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		Dimension preferredSize = new Dimension(screenSize.width - 100,
				screenSize.height - 100);
		
		
		// Make the button panel
		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));

		JButton exitButton = new JButton("Exit");
		exitButton.addActionListener(exitListener);
		
		JButton newButton = new JButton("New Graph");
		newButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				Graph graph = makeNewGraph("Test");
				graphPane.add(new TypeModelInfoPanel(graph.getTypeModel()));
			}
		});
		
		JButton openButton = new JButton("Open");
		openButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				loadFile();
			}
		});
		
		buttonPanel.add(newButton);
		buttonPanel.add(openButton);
		buttonPanel.add(exitButton);
		
		graphPane = new JTabbedPane();
		drawingArea.add(graphPane);
		
		// Init the main panel
		JPanel panel = new JPanel();
		panel.setPreferredSize(preferredSize);
		panel.setLayout(new BorderLayout());
		panel.add(drawingArea, BorderLayout.CENTER);
		panel.add(buttonPanel, BorderLayout.NORTH);
		
		// Add a message handler on the bottom
		TableHandler handler = new TableHandler();
		JTable reportTable = new JTable(handler);
		reportTable.setPreferredSize(new Dimension(screenSize.width,
				screenSize.height / 6));
		reporter.addHandler(handler);
		
		panel.add(reportTable, BorderLayout.SOUTH);
		
		// Build the main frame
		frame = new JFrame("LibGr Test");
		frame.setContentPane(panel);
		frame.addWindowListener(new WindowAdapter() {
			public void windowClosing(WindowEvent e) {
				System.exit(0);
			}
		});
		
		// frame.setJMenuBar(buildMenu());
		frame.pack();
		
		Rectangle bounds = frame.getBounds();
		frame.setLocation((screenSize.width - bounds.width) / 2,
				(screenSize.height - bounds.height) / 2);
		
		frame.setVisible(true);
	}
	
	private void loadDrivers() {
		try {
			Class cls = Class.forName("org.postgresql.Driver");
		} catch (ClassNotFoundException e) {
			System.out.println("Cannot load driver.");
			System.exit(1);
		}
	}
	
	public void run() {
		loadDrivers();
	}
	
	public static void main(String[] args) {
		UITest app = new UITest();
		app.run();
	}
}
