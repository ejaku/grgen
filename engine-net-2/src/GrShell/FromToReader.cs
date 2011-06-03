using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.grShell
{
    /// <summary>
    /// A wrapper for a TextReader,
    /// which ensures that reading starts from the line following a line consisting of a given from string,
    /// and ends at the line before the line consisting of a given to string.
    /// From or to might be null meaning start at file start or end at file end.
    /// Only the subset of the methods needed by the CSharpCC lexer is implemented.
    /// </summary>
    class FromToReader : TextReader
    {
        public FromToReader(TextReader wrappedReader, string from, string to)
        {
            this.wrappedReader = wrappedReader;
            this.from = from;
            this.to = to;
            firstRead = true;
            endFound = false;
        }

        public override void Close()
        {
            wrappedReader.Close();
        }

        public new void Dispose()
        {
            wrappedReader.Dispose();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            if(firstRead)
            {
                firstRead = false;

                if(from!=null)
                {
                    string line;
                    do
                    {
                        line = wrappedReader.ReadLine();
                        if(line == null)
                        {
                            System.Console.Error.WriteLine("WARNING: past end searching for replay start line \"" + from + "\"!");
                            return 0;
                        }
                    }
                    while(line!=from);
                }
            }

            if(to!=null)
            {
                if(bufferWithCarry==null)
                    bufferWithCarry = new StringBuilder(8192);

                if(endFound)
                    return 0;

                while(bufferWithCarry.Length < count)
                {
                    string line = wrappedReader.ReadLine();
                    if(line == null || line == to)
                    {
                        if(line == null)
                            System.Console.Error.WriteLine("WARNING: past end searching for replay stop line \"" + to + "\"!");
                        else
                            endFound = true;
                        int length = bufferWithCarry.Length;
                        bufferWithCarry.CopyTo(0, buffer, index, length);
                        bufferWithCarry.Remove(0, length);
                        return length;
                    }
                    bufferWithCarry.AppendLine(line);
                }

                bufferWithCarry.CopyTo(0, buffer, index, count);
                bufferWithCarry.Remove(0, count);
                return count;
            }
            else
            {
                return wrappedReader.Read(buffer, index, count);
            }
        }

        public override int Peek() { throw new NotImplementedException(); }
        public override int Read() { throw new NotImplementedException(); }
        public override int ReadBlock(char[] buffer, int index, int count) { throw new NotImplementedException(); }
        public override string ReadLine() { throw new NotImplementedException(); }
        public override string ReadToEnd() { throw new NotImplementedException(); }
        public new static TextReader Synchronized(TextReader reader) { throw new NotImplementedException(); }

        private TextReader wrappedReader;
        private String from;
        private String to;
        private bool firstRead;
        private StringBuilder bufferWithCarry;
        private bool endFound;
    }
}
