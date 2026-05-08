/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace GGrShell
{
    public class ClipboardLineSource
    {
        private readonly List<String> clipboardContent;

        // initially, when constructed, the clipboard is read
        public ClipboardLineSource()
        {
            clipboardContent = new List<String>(Clipboard.GetText().Split('\n'));
            clipboardContent.Reverse(); // clipboard content should be delivered from the end of the list, where a remove is O(1)
        }

        public string GetNextLine()
        {
            StringBuilder lineBuilder = new StringBuilder();

            bool lastLineEndedWithBackslash = false;
            do
            {
                int indexOfLastLine = clipboardContent.Count - 1;
                if(indexOfLastLine < 0)
                    break;
                string lastLine = clipboardContent[indexOfLastLine].Trim('\r');
                clipboardContent.RemoveAt(indexOfLastLine);
                lastLineEndedWithBackslash = lastLine.EndsWith("\\");
                lineBuilder.Append(lastLineEndedWithBackslash ? lastLine.Substring(0, lastLine.Length - 1) : lastLine);
            }
            while(lastLineEndedWithBackslash);

            return lineBuilder.ToString();
        }

        public bool IsEmpty { get { return clipboardContent.Count == 0; } }
    }
}
