// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    /// <summary>
    /// Async TCP server that receives GrGen-yComp protocol command lines and hands them
    /// to the CommandLineParser and then to a CommandLineInterpreter, which executes them on the IBasicGraphViewerClient (typically MSAGLExtClient).
    /// Started from the UI thread; async/await + WinForms SynchronizationContext keeps
    /// all callbacks on the UI thread -- no Invoke needed.
    /// </summary>
    public class GraphCommandReceiver
    {
        CommandLineInterpreter commandLineInterpreter;
        TcpListener listener;
        bool stopped;

        const int MaxLineBytes = 64 * 1024 * 1024;

        // ---- public API ----

        public async Task RunAsync(int port, IBasicGraphViewerClient graphClient)
        {
            commandLineInterpreter = new CommandLineInterpreter(graphClient);
            listener = new TcpListener(IPAddress.Loopback, port);
            try { listener.Start(); }
            catch(Exception ex)
            {
                Console.Error.WriteLine("extMSAGLExt: cannot listen on port " + port + ": " + ex.Message);
                return;
            }
            Console.WriteLine("extMSAGLExt: listening on port " + port);

            while(!stopped)
            {
                TcpClient tcpClient;
                try { tcpClient = await listener.AcceptTcpClientAsync(); }
                catch(Exception) { break; }

                await HandleConnectionAsync(tcpClient);
            }
            listener.Stop();
        }

        public void Stop()
        {
            stopped = true;
            try { listener?.Stop(); } catch { }
        }

        // ---- connection loop ----

        async Task HandleConnectionAsync(TcpClient tcpClient)
        {
            Console.WriteLine("extMSAGLExt: client connected");
            using(tcpClient)
            using(NetworkStream stream = tcpClient.GetStream())
            {
                byte[] readBuf = new byte[65536];
                var lineBuf = new StringBuilder();
                bool closed = false;

                while(!closed)
                {
                    int n;
                    try { n = await stream.ReadAsync(readBuf, 0, readBuf.Length); }
                    catch(Exception) { break; }
                    if(n == 0) break;

                    for(int i = 0; i < n && !closed; i++)
                    {
                        char c = (char)readBuf[i];
                        if(c == '\n')
                        {
                            List<string> args = CommandLineParser.Tokenize(lineBuf.ToString());
                            closed = await commandLineInterpreter.InterpretAsync(args, stream);
                            lineBuf.Clear();
                        }
                        else
                        {
                            lineBuf.Append(c);
                            if(lineBuf.Length > MaxLineBytes)
                            {
                                Console.Error.WriteLine("extMSAGLExt: line too long (>" + MaxLineBytes + " bytes) -- closing connection");
                                return;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("extMSAGLExt: client disconnected");
        }
    }
}
