using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using de.unika.ipd.grGen.libGr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMSAGL
{
    // intended to use mstest 2 of VS2019, but didn't manage to get it running (without the System.Resources.Extensions exception thrown by the MSAGL graph viewer) - but ok as only some smoketest / playground intended
    // no idea why, but adding the package reference (with version 7) to the project file was sufficient to run MSAGL, no binding redirect in app.config needed
    // note that you have to copy the files from the top level bin folder (the released dlls/apps) to your project bin folder in order to get a running yComp
    public partial class MSAGLTestHost : Form
    {
        public MSAGLTestHost()
        {
            InitializeComponent();
        }

        private void MSAGLTestHost_Load(object sender, EventArgs e)
        {
        }

        YCompServerProxy yCompServerProxy;
        BasicGraphViewerClientHost msaglClientHost;
        IBasicGraphViewerClient graphViewer;

        private void comboBoxGraphViewerChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if(comboBoxGraphViewerChooser.SelectedItem as string == "yComp")
            {
                yCompServerProxy = new YCompServerProxy(YCompServerProxy.GetFreeTCPPort());
                int connectionTimeout = 20000;
                int port = yCompServerProxy.port;
                graphViewer = new YCompClient(connectionTimeout, port);
            }
            else
            {
                msaglClientHost = new BasicGraphViewerClientHost();
                graphViewer = new MSAGLClient(msaglClientHost);
            }
            graphViewer.AddNodeRealizer("nr1", GrColor.Black, GrColor.Yellow, GrColor.Black, GrNodeShape.Box);
            graphViewer.AddNodeRealizer("nrsub1", GrColor.Black, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
        }

        private void buttonAddNestedGroupThenFillIt_Click(object sender, EventArgs e)
        {
            graphViewer.AddSubgraphNode("group", "nrsub1", "group");
            graphViewer.AddNode("node", "nr1", "node");

            graphViewer.AddNode("nestedNode", "nr1", "nestedNode");
            graphViewer.MoveNode("nestedNode", "group");

            graphViewer.AddSubgraphNode("nestedGroup", "nrsub1", "nestedGroup");
            graphViewer.AddNode("nestedNestedNode", "nr1", "nestedNestedNode");

            graphViewer.MoveNode("nestedGroup", "group");
            graphViewer.MoveNode("nestedNestedNode", "nestedGroup");

            graphViewer.Show();
        }

        private void buttonFillNestedGroupThenAddIt_Click(object sender, EventArgs e)
        {
            graphViewer.AddSubgraphNode("group", "nrsub1", "group");
            graphViewer.AddNode("node", "nr1", "node");

            graphViewer.AddNode("nestedNode", "nr1", "nestedNode");
            graphViewer.MoveNode("nestedNode", "group");

            graphViewer.AddSubgraphNode("nestedGroup", "nrsub1", "nestedGroup");
            graphViewer.AddNode("nestedNestedNode", "nr1", "nestedNestedNode");

            graphViewer.MoveNode("nestedNestedNode", "nestedGroup");
            graphViewer.MoveNode("nestedGroup", "group");

            graphViewer.Show();
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            graphViewer.DeleteNode("group", "group");
            graphViewer.Show();
        }

        private void buttonDeleteNode_Click(object sender, EventArgs e)
        {
            graphViewer.DeleteNode("node", "node");
            graphViewer.Show();
        }
        private void buttonDeleteNestedGroup_Click(object sender, EventArgs e)
        {
            graphViewer.DeleteNode("nestedGroup", "nestedGroup");
            graphViewer.Show();
        }

        private void buttonDeleteNestedNode_Click(object sender, EventArgs e)
        {
            graphViewer.DeleteNode("nestedNode", "nestedNode");
            graphViewer.Show();
        }

        private void buttonDeleteNestedNestedNode_Click(object sender, EventArgs e)
        {
            graphViewer.DeleteNode("nestedNestedNode", "nestedNestedNode");
            graphViewer.Show();
        }

        private void buttonClearGraph_Click(object sender, EventArgs e)
        {
            graphViewer.ClearGraph();
            graphViewer.Show();
        }
    }
}
