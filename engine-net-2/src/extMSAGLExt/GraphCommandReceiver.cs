// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    /// <summary>
    /// Async TCP server (accepting a single connection) that receives GrGen-yComp protocol command lines and hands them
    /// to the CommandLineParser and then to a CommandLineInterpreter, which executes them on the IBasicGraphViewerClient (typically MSAGLExtClient).
    /// Started from the UI thread; async/await + WinForms SynchronizationContext keeps
    /// all callbacks on the UI thread -- no Invoke needed.
    /// </summary>
    public class GraphCommandReceiver
    {
        CommandLineInterpreter commandLineInterpreter;
        TcpListener listener;
        TcpClient connectedClient;

        const int MaxLineBytes = 64 * 1024 * 1024;

        // ---- public API ----

        /// <summary>
        /// Starts listening on the given loopback port.
        /// Returns an error message if the port can't be opened, null on success.
        /// </summary>
        public string StartListening(int port)
        {
            listener = new TcpListener(IPAddress.Loopback, port);
            try { listener.Start(); }
            catch(Exception ex)
            {
                return "cannot listen on port " + port + " of loopback address: " + ex.Message;
            }
            Console.WriteLine("extMSAGLExt: listening on port " + port + " of loopback address");
            return null;
        }

        /// <summary>
        /// Accepts the first connection (and only that one, no further connections are accepted, also not after it ended)
        /// and processes the commands received over it.
        /// Syntax errors in the protocol are passed on as exceptions (ProtocolSyntaxException), as are the
        /// semantic errors reported by the graph viewer client.
        /// </summary>
        public async Task RunAsync(IBasicGraphViewerClient graphClient)
        {
            commandLineInterpreter = new CommandLineInterpreter(graphClient);

            TcpClient tcpClient;
            try { tcpClient = await listener.AcceptTcpClientAsync(); }
            catch(ObjectDisposedException) { return; } // stopped while waiting for the connection
            catch(SocketException) { return; }
            finally { listener.Stop(); } // refuse any further connection attempts

            connectedClient = tcpClient;
            await HandleConnectionAsync(tcpClient);
        }

        /// <summary>
        /// Stops listening and closes the connection if one is established (ending the debugging session).
        /// </summary>
        public void Stop()
        {
            commandLineInterpreter?.Stop();
            try { listener?.Stop(); } catch { }
            try { connectedClient?.Close(); } catch { }
        }

        // ---- connection loop ----

        async Task HandleConnectionAsync(TcpClient tcpClient)
        {
            Console.WriteLine("extMSAGLExt: client connected");
            using(tcpClient)
            using(NetworkStream stream = tcpClient.GetStream())
            {
                byte[] readBuf = new byte[65536];
                MemoryStream lineBuf = new MemoryStream();
                bool closed = false;

                while(!closed)
                {
                    int n;
                    try { n = await stream.ReadAsync(readBuf, 0, readBuf.Length); }
                    catch(IOException) { break; }
                    catch(ObjectDisposedException) { break; }
                    if(n == 0) break;

                    for(int i = 0; i < n && !closed; i++)
                    {
                        if(readBuf[i] == (byte)'\n')
                        {
                            string line = Encoding.ASCII.GetString(lineBuf.GetBuffer(), 0, (int)lineBuf.Length);
                            lineBuf.SetLength(0);
                            List<string> args = CommandLineParser.Tokenize(line);
                            closed = await commandLineInterpreter.InterpretAsync(args, stream);
                        }
                        else
                        {
                            lineBuf.WriteByte(readBuf[i]);
                            if(lineBuf.Length > MaxLineBytes)
                                return; // line too long, close the connection
                        }
                    }
                }
            }
            Console.WriteLine("extMSAGLExt: client disconnected");
        }
    }
}
