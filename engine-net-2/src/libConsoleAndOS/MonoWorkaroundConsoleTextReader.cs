/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Moritz Kroll, Claude Code with Edgar Jakumeit

using System;
using System.IO;

namespace de.unika.ipd.grGen.libConsoleAndOS
{
    internal class MonoWorkaroundConsoleTextReader : TextReader
    {
        char[] lineBuffer = null;
        int curLineBufferPos = -1;

        // TextReader.ReadLine (used by the debugger) is built on Read() and Peek(), whose base implementations return -1 (i.e. EOF),
        // so they must be overridden, too, not only the Read(char[], int, int) used by the GrShell parser
        public override int Read()
        {
            if(lineBuffer == null)
                FillLineBuffer();
            int ch = lineBuffer[curLineBufferPos];
            ++curLineBufferPos;
            if(curLineBufferPos == lineBuffer.Length)
                lineBuffer = null;
            return ch;
        }

        public override int Peek()
        {
            if(lineBuffer == null)
                FillLineBuffer();
            return lineBuffer[curLineBufferPos];
        }

        public override int Read(char[] buffer, int index, int count)
        {
            int num;
            if(lineBuffer != null)
            {
                num = lineBuffer.Length - curLineBufferPos;
                if(num > count)
                    num = count;
                Array.ConstrainedCopy(lineBuffer, curLineBufferPos, buffer, index, num);
                curLineBufferPos += num;
                if(curLineBufferPos == lineBuffer.Length)
                    lineBuffer = null;
                index += num;
                count -= num;
                if(count == 0)
                    return num;
            }
            FillLineBuffer();
            num = lineBuffer.Length;
            if(num > count)
                num = count;
            Array.ConstrainedCopy(lineBuffer, 0, buffer, index, num);
            curLineBufferPos += num;
            if(curLineBufferPos == lineBuffer.Length)
                lineBuffer = null;
            return num;
        }

        // reads a line from the console into the line buffer, filtering out special key codes, the line buffer is never empty (contains at least the newline)
        private void FillLineBuffer()
        {
            String line = Console.ReadLine() + Environment.NewLine;
            //lineBuffer = line.ToCharArray();
            int realChars = 0;
            for(int i = 0; i < line.Length; ++i)
            {
                if(line[i] != 0)                                // not special key code? (e.g. for arrow keys)
                {
                    if(line[i] == 27 && i + 3 < line.Length)    // del escape sequence?
                    {
                        if(line[i + 1] == 91 && line[i + 2] == 51 && line[i + 3] == 126)
                        {
                            i += 3;
                            continue;   // -> i++ => i += 4
                        }
                    }
                    ++realChars;
                }
            }

            lineBuffer = new char[realChars];
            for(int i = 0, j = 0; i < line.Length; ++i)
            {
                if(line[i] != 0)                                // not special key code? (e.g. for arrow keys)
                {
                    if(line[i] == 27 && i + 3 < line.Length)    // del escape sequence?
                    {
                        if(line[i + 1] == 91 && line[i + 2] == 51 && line[i + 3] == 126)
                        {
                            i += 3;
                            continue;   // -> i++ => i += 4
                        }
                    }
                    lineBuffer[j++] = line[i];
                }
            }

            curLineBufferPos = 0;
        }
    }
}
