/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using antlr;

namespace ASTdapter
{
    public class ParserPackage
    {
        private ConstructorInfo lexerStreamConstructorInfo;
        private ConstructorInfo lexerStringConstructorInfo;
        private ConstructorInfo parserConstructorInfo;
        private MethodInfo mainParsingMethodInfo;
        private Dictionary<int, string> typeMap;
        private static readonly object[] no_args = new object[0];

        public ParserPackage(String parserassemblyname, String mainmethodname)
        {
            Assembly assembly = Assembly.LoadFrom(parserassemblyname);
            Type lexerclass = GetLexerClass(assembly);
            Type parserclass = GetParserClass(assembly);
            Type tokentypesclass = GetTokenTypeClass(assembly);
            setParser(lexerclass, parserclass, mainmethodname, tokentypesclass);
        }

        public ParserPackage(String lexerclassname, String parserclassname, String mainmethodname, String tokentypesclassname)
        {
            Type lexerclass = Type.GetType(lexerclassname);
            Type parserclass = Type.GetType(parserclassname);
            Type tokentypesclass = Type.GetType(tokentypesclassname);
            setParser(lexerclass, parserclass, mainmethodname, tokentypesclass);
        }

        public ParserPackage(Type lexerclass, Type parserclass, String mainmethodname, Type tokentypesclass)
        {
            setParser(lexerclass, parserclass, mainmethodname, tokentypesclass);
        }

        public void setParser(Type lexerclass, Type parserclass, String mainmethodname, Type tokentypesclass)
        {
            lexerStreamConstructorInfo = lexerclass.GetConstructor(new Type[] { typeof(Stream) });
            if (lexerStreamConstructorInfo == null) throw new ArgumentException("Lexer class invalid (no constructor for Stream)");
            lexerStringConstructorInfo = lexerclass.GetConstructor(new Type[] { typeof(TextReader) });
            if (lexerStringConstructorInfo == null) throw new ArgumentException("Lexer class invalid (no constructor for TextReader)");
            parserConstructorInfo = parserclass.GetConstructor(new Type[] { typeof(TokenStream) });
            if (parserConstructorInfo == null) throw new ArgumentException("Parser class invalid (no constructor for TokenStream)");
            mainParsingMethodInfo = parserclass.GetMethod(mainmethodname);
            typeMap = GetTypeMap(tokentypesclass);
        }

        public void CallParser(LLkParser parser)
        {
            mainParsingMethodInfo.Invoke(parser, no_args);
        }

        public TokenStream GetLexer(Stream instream)
        {
            TokenStream result = (TokenStream)lexerStreamConstructorInfo.Invoke(new object[] { instream });
            return result;
        }

        public TokenStream GetLexer(string s)
        {
            TextReader r = new StringReader(s);
            TokenStream result = (TokenStream)lexerStringConstructorInfo.Invoke(new object[] { r });
            return result;
        }

        public LLkParser GetParser(TokenStream lexer)
        {
            LLkParser result = (LLkParser)parserConstructorInfo.Invoke(new object[] { lexer });
            return result;
        }

        public string GetTypeName(int iType)
        {
            return typeMap[iType];
        }

        private static Dictionary<int, string> GetTypeMap(Type tokenTypesType)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            FieldInfo[] fis = tokenTypesType.GetFields();
            foreach (FieldInfo fi in fis)
            {
                string name = fi.Name;
                int value = (int)fi.GetValue(null);
                result.Add(value, name);
            }
            return result;
        }

        private static Type GetLexerClass(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (type.GetInterface("TokenStream") == null) continue;
                return type;
            }
            return null;
        }

        private static Type GetParserClass(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (!type.IsSubclassOf(typeof(LLkParser))) continue;
                return type;
            }
            return null;
        }

        private static Type GetTokenTypeClass(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (!type.Name.EndsWith("TokenTypes")) continue;
                return type;
            }
            return null;
        }
    }
}
