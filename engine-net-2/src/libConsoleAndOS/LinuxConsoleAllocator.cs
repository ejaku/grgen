// by Claude Code with Edgar Jakumeit

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace de.unika.ipd.grGen.libConsoleAndOS
{
    /// <summary>
    /// The Linux counterpart of the Windows AllocConsole function (which is not available there):
    /// opens a terminal emulator window that displays the output of the process.
    /// A named pipe (FIFO) is created and the terminal emulator is started with a command that copies the pipe content to its window;
    /// then the standard output and standard error file descriptors (1 and 2) of the process are redirected into the pipe,
    /// so everything written to the console from then on (also via a freshly opened Console.OpenStandardOutput()) ends up in the window.
    /// Output only, the standard input of the process stays as it is.
    /// </summary>
    public static class LinuxConsoleAllocator
    {
        const int O_RDWR = 2; // opening a FIFO read-write does not block until a reader appears (Linux)
        const int STDOUT_FD = 1;
        const int STDERR_FD = 2;

        [DllImport("libc", SetLastError = true)]
        static extern int mkfifo(string path, uint mode);

        [DllImport("libc", SetLastError = true)]
        static extern int open(string path, int flags);

        [DllImport("libc", SetLastError = true)]
        static extern int dup(int fd);

        [DllImport("libc", SetLastError = true)]
        static extern int dup2(int oldFd, int newFd);

        [DllImport("libc", SetLastError = true)]
        static extern int close(int fd);

        [DllImport("libc", SetLastError = true)]
        static extern int unlink(string path);

        // terminal emulators with the arguments that precede the command to execute, in the order of fallback preference:
        // the terminal of the desktop environment is tried first (see desktopTerminals), then the other desktop-specific and modern ones,
        // and the general ones (x-terminal-emulator, xterm) come last
        static readonly string[,] terminalEmulators =
        {
            { "konsole", "-e" },
            { "gnome-terminal", "--title=\"GrGen console\" --" },
            { "xfce4-terminal", "--title=\"GrGen console\" -x" },
            { "mate-terminal", "--title=\"GrGen console\" -x" },
            { "lxterminal", "--title=\"GrGen console\" -e" },
            { "qterminal", "-e" },
            { "alacritty", "--title \"GrGen console\" -e" },
            { "kitty", "--title \"GrGen console\"" },
            { "x-terminal-emulator", "-e" },
            { "xterm", "-T \"GrGen console\" -e" },
        };

        // the terminal belonging to a desktop environment, as named in XDG_CURRENT_DESKTOP (a colon-separated list, e.g. "ubuntu:GNOME")
        static readonly string[,] desktopTerminals =
        {
            { "KDE", "konsole" },
            { "GNOME", "gnome-terminal" },
            { "Unity", "gnome-terminal" },
            { "X-Cinnamon", "gnome-terminal" },
            { "XFCE", "xfce4-terminal" },
            { "MATE", "mate-terminal" },
            { "LXQt", "qterminal" },
            { "LXDE", "lxterminal" },
        };

        static string fifoPath;
        static int savedStdoutFd = -1;
        static int savedStderrFd = -1;

        /// <summary>
        /// Opens a terminal emulator window and redirects stdout and stderr into it.
        /// Returns true on success (or if the console was already allocated), false if no console could be allocated
        /// (e.g. because there is no display, or no known terminal emulator is installed), the reason is printed to stderr then.
        /// </summary>
        public static bool Alloc()
        {
            if(fifoPath != null)
                return true;

            if(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DISPLAY"))
                && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WAYLAND_DISPLAY")))
            {
                Console.Error.WriteLine("Cannot allocate a console: no graphical display available.");
                return false;
            }

            string terminal;
            string terminalArguments;
            if(!FindTerminalEmulator(out terminal, out terminalArguments))
            {
                Console.Error.WriteLine("Cannot allocate a console: no terminal emulator found.");
                return false;
            }

            string path = Path.Combine(Path.GetTempPath(), "grgen-console-" + Process.GetCurrentProcess().Id + "-" + Guid.NewGuid().ToString("N"));
            if(mkfifo(path, Convert.ToUInt32("600", 8)) != 0)
            {
                Console.Error.WriteLine("Cannot allocate a console: creating the pipe " + path + " failed (errno " + Marshal.GetLastWin32Error() + ").");
                return false;
            }

            int fifoFd = open(path, O_RDWR);
            if(fifoFd < 0)
            {
                Console.Error.WriteLine("Cannot allocate a console: opening the pipe " + path + " failed (errno " + Marshal.GetLastWin32Error() + ").");
                unlink(path);
                return false;
            }

            // stays open until the console is freed or the process ends, the terminal then displays a hint instead of closing at once,
            // so error messages written just before the end are not lost
            string command = "cat '" + path + "'; printf '\\n[end of output - press Enter to close this window]\\n'; read dummy";
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(terminal, terminalArguments + " sh -c \"" + command + "\"");
                startInfo.UseShellExecute = false;
                Process.Start(startInfo);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Cannot allocate a console: starting " + terminal + " failed: " + ex.Message);
                close(fifoFd);
                unlink(path);
                return false;
            }

            Console.Out.Flush();
            Console.Error.Flush();
            savedStdoutFd = dup(STDOUT_FD);
            savedStderrFd = dup(STDERR_FD);
            dup2(fifoFd, STDOUT_FD);
            dup2(fifoFd, STDERR_FD);
            close(fifoFd);

            fifoPath = path;
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            return true;
        }

        /// <summary>
        /// Restores stdout and stderr, which closes the pipe to the terminal emulator window (it then shows a hint and waits for Enter).
        /// Returns true if a console was allocated before.
        /// </summary>
        public static bool Free()
        {
            if(fifoPath == null)
                return false;

            Console.Out.Flush();
            Console.Error.Flush();
            if(savedStdoutFd >= 0)
            {
                dup2(savedStdoutFd, STDOUT_FD);
                close(savedStdoutFd);
                savedStdoutFd = -1;
            }
            if(savedStderrFd >= 0)
            {
                dup2(savedStderrFd, STDERR_FD);
                close(savedStderrFd);
                savedStderrFd = -1;
            }

            unlink(fifoPath); // the terminal emulator has opened the pipe long since, otherwise the window displayed nothing anyway
            fifoPath = null;
            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;
            return true;
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            Free();
        }

        static bool FindTerminalEmulator(out string terminal, out string arguments)
        {
            // the terminal of the current desktop environment is preferred
            string preferredTerminal = GetTerminalOfDesktopEnvironment();
            if(preferredTerminal != null)
            {
                for(int i = 0; i < terminalEmulators.GetLength(0); ++i)
                {
                    if(terminalEmulators[i, 0] != preferredTerminal)
                        continue;
                    string fullPath = FindInPath(preferredTerminal);
                    if(fullPath != null)
                    {
                        terminal = fullPath;
                        arguments = terminalEmulators[i, 1];
                        return true;
                    }
                }
            }

            // fallback
            for(int i = 0; i < terminalEmulators.GetLength(0); ++i)
            {
                string fullPath = FindInPath(terminalEmulators[i, 0]);
                if(fullPath != null)
                {
                    terminal = fullPath;
                    arguments = terminalEmulators[i, 1];
                    return true;
                }
            }
            terminal = null;
            arguments = null;
            return false;
        }

        static string GetTerminalOfDesktopEnvironment()
        {
            string desktops = Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP");
            if(string.IsNullOrEmpty(desktops))
                desktops = Environment.GetEnvironmentVariable("DESKTOP_SESSION");
            if(string.IsNullOrEmpty(desktops))
                return null;

            foreach(string desktop in desktops.Split(':'))
            {
                for(int i = 0; i < desktopTerminals.GetLength(0); ++i)
                {
                    if(string.Equals(desktop, desktopTerminals[i, 0], StringComparison.OrdinalIgnoreCase))
                        return desktopTerminals[i, 1];
                }
            }
            return null;
        }

        static string FindInPath(string executable)
        {
            string path = Environment.GetEnvironmentVariable("PATH");
            if(path == null)
                return null;
            foreach(string directory in path.Split(Path.PathSeparator))
            {
                if(directory.Length == 0)
                    continue;
                string candidate = Path.Combine(directory, executable);
                if(File.Exists(candidate))
                    return candidate;
            }
            return null;
        }
    }
}
