using System;
using System.IO;

// class eases transformation (from Java to C#), potential TODO: remove it, replace it by the wrapped classes directly
public class PrintStream : IDisposable
{
    TextWriter tw;

    public PrintStream(Stream stream)
    {
        tw = new StreamWriter(stream);
    }

    public PrintStream(TextWriter writer)
    {
        tw = writer;
    }

    public void Print(string str)
    {
        tw.Write(str);
    }

    public void Print(int i)
    {
        tw.Write(i);
    }

    public void Print(char c)
    {
        tw.Write(c);
    }

    public void Println(string str)
    {
        tw.WriteLine(str);
    }

    public void Println(char c)
    {
        tw.WriteLine(c);
    }

    public void Flush()
    {
        tw.Flush();
    }

    public void Close()
    {
        tw.Close();
    }

    public void Dispose()
    {
        tw.Dispose();
    }
}
