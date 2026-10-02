/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    // potentially available graph viewer client types (debugger types)
    public enum GraphViewerTypes
    {
        YComp, // console and external app
        MSAGL, MSAGLExt, // GUI frame/console and internal API
        ExtMSAGLExt // console and external app
    }

    public delegate void ConnectionLostHandler();

    /// <summary>
    /// Class communicating with yComp or MSAGL, over a simple live graph viewer protocol (transmitted over a loopback TCP socket or entirely internally),
    /// some very basic shared functionality is implemented here, real graph handling is contained in the GraphViewerClient.
    /// </summary>
    public class GraphViewerBaseClient
    {
        GraphViewerApplicationStarter graphViewerApplicationStarter; // not null in case the basicClient is a YCompClient or an ExtMSAGLExtClient
        internal IBasicGraphViewerClient basicClient; // either the proxy YCompClient/ExtMSAGLExtClient or a MSAGLClient/MSAGLExtClient

        protected ElementRealizers realizers;

        static Dictionary<String, bool> availableMSAGLLayouts;


        static GraphViewerBaseClient()
        {
            availableMSAGLLayouts = new Dictionary<string, bool>(); // layouts of internal MSAGL client
            availableMSAGLLayouts.Add("SugiyamaScheme", true);
            availableMSAGLLayouts.Add("MDS", true);
            availableMSAGLLayouts.Add("Ranking", true);
            availableMSAGLLayouts.Add("IcrementalLayout", true);
        }

        /// <summary>
        /// Creates a new GraphViewerBaseClient instance.
        /// Internally, it creates a YCompClient/ExtMSAGLExtClient and connects to the external local yComp/extMSAGLExt server,
        /// or creates an internal MSAGLClient/MSAGLExtClient, inside the basicGraphViewerClientHost (which may be a GuiConsoleDebuggerHost) in case one is supplied,
        /// depending on the graph viewer type that is requested (the layout is expected to be one of the valid layouts of the corresponding graph viewer client).
        /// </summary>
        public GraphViewerBaseClient(GraphViewerTypes graphViewerType, String layoutModule,
            ElementRealizers realizers, IBasicGraphViewerClientHost basicGraphViewerClientHost)
        {
            if(IsInternalGraphViewer(graphViewerType))
            {
                IHostCreator guiConsoleDebuggerHostCreator = GetGuiConsoleDebuggerHostCreator();
                IBasicGraphViewerClientCreator basicGraphViewerClientCreator = GetBasicGraphViewerClientCreator();
                IBasicGraphViewerClientHost host = basicGraphViewerClientHost;
                if(host == null)
                    host = guiConsoleDebuggerHostCreator.CreateBasicGraphViewerClientHost();
                basicClient = basicGraphViewerClientCreator.Create(graphViewerType, host);
                host.BasicGraphViewerClient = basicClient;
            }
            else
            {
                if(graphViewerType == GraphViewerTypes.ExtMSAGLExt)
                    graphViewerApplicationStarter = new ExtMSAGLExtStarter(ExtMSAGLExtStarter.GetFreeTCPPort());
                else // default is YCompClient
                    graphViewerApplicationStarter = new YCompStarter(YCompStarter.GetFreeTCPPort());
                int connectionTimeout = 20000;
                int port = graphViewerApplicationStarter.port;
                if(graphViewerType == GraphViewerTypes.ExtMSAGLExt)
                    basicClient = new ExtMSAGLExtClient(connectionTimeout, port);
                else
                    basicClient = new YCompClient(connectionTimeout, port);
            }

            basicClient.SetLayout(layoutModule);

            this.realizers = realizers;
            realizers.RegisterGraphViewerClient(this.basicClient);
        }

        // normally the GraphViewerClient should be used not exposing this one...
        public IBasicGraphViewerClient GetBasicClient() // either the traditional YCompClient or one of the MSAGL based clients
        {
            return basicClient;
        }

        /// <summary>
        /// returns a host creator from graphViewerAndSequenceDebuggerWindowsForms.dll
        /// </summary>
        public static IHostCreator GetGuiConsoleDebuggerHostCreator()
        {
            Type guiConsoleDebuggerHostCreatorType = de.unika.ipd.grGen.libConsoleAndOS.TypeCreator.GetSingleImplementationOfInterfaceFromAssembly("graphViewerAndSequenceDebuggerWindowsForms.dll", "IHostCreator");
            IHostCreator guiConsoleDebuggerHostCreator = (IHostCreator)Activator.CreateInstance(guiConsoleDebuggerHostCreatorType);
            return guiConsoleDebuggerHostCreator;
        }

        private static IBasicGraphViewerClientCreator GetBasicGraphViewerClientCreator()
        {
            Type basicGraphViewerClientCreatorType = de.unika.ipd.grGen.libConsoleAndOS.TypeCreator.GetSingleImplementationOfInterfaceFromAssembly("graphViewerAndSequenceDebuggerWindowsForms.dll", "IBasicGraphViewerClientCreator");
            IBasicGraphViewerClientCreator basicGraphViewerClientCreator = (IBasicGraphViewerClientCreator)Activator.CreateInstance(basicGraphViewerClientCreatorType);
            return basicGraphViewerClientCreator;
        }

        public virtual void Close()
        {
            realizers.UnregisterGraphViewerClient();

            basicClient.Close();
            basicClient = null;

            if(graphViewerApplicationStarter != null)
                graphViewerApplicationStarter.Close();
            graphViewerApplicationStarter = null;
        }

        public static IEnumerable<String> AvailableLayouts(GraphViewerTypes type)
        {
            if(type == GraphViewerTypes.YComp)
                return YCompClient.AvailableLayouts;
            else if(type == GraphViewerTypes.ExtMSAGLExt)
                return ExtMSAGLExtClient.AvailableLayouts;
            else if(IsInternalGraphViewer(type))
            {
                // better to be handled by the corresponding graph viewer client that has that knowledge,
                // but this would require to create an instance of a dll we don't want to instantiate for technology reasons unless really requested
                // this function is also used for general error checking/printing without prior request, though
                return availableMSAGLLayouts.Keys;
            }
            else
                return null;
        }

        public static bool IsValidLayout(GraphViewerTypes type, String layoutName)     // TODO: allow case insensitive layout name
        {
            if(type == GraphViewerTypes.YComp)
                return YCompClient.IsValidLayout(layoutName);
            else if(type == GraphViewerTypes.ExtMSAGLExt)
                return ExtMSAGLExtClient.IsValidLayout(layoutName);
            else if(IsInternalGraphViewer(type))
            {
                // better to be handled by the corresponding graph viewer client that has that knowledge,
                // but this would require to create an instance of a dll we don't want to instantiate for technology reasons unless really requested
                // this function is also used for general error checking/printing without prior request, though
                return availableMSAGLLayouts.ContainsKey(layoutName);
            }
            else
                return false;
        }

        public static bool IsInternalGraphViewer(GraphViewerTypes graphViewerType)
        {
            return graphViewerType == GraphViewerTypes.MSAGL
                || graphViewerType == GraphViewerTypes.MSAGLExt;
        }
    }
}
