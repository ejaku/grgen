options {
	STATIC=false;
}

PARSER_BEGIN(GRSImporter)
    namespace de.unika.ipd.grGen.libGr.porter;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using de.unika.ipd.grGen.libGr;

	struct Param
	{
        public String Key; // the attribute name
	
        // for basic types, enums
        public String Value; // the attribute value
        
        // for set, map attributed
        public String Type; // set/map(domain) type
        public String TgtType; // map target type
        public ArrayList Values; // set/map(domain) values 
        public ArrayList TgtValues; // map target values

        public Param(String key, String value)
        {
            Key = key;
            Value = value;
            Type = null;
            TgtType = null;
            Values = null;
            TgtValues = null;
        }
        
        public Param(String key, String value, String type)
        {
            Key = key;
            Value = value;
            Type = type;
            TgtType = null;
            Values = new ArrayList();
            TgtValues = null;
        }
        
        public Param(String key, String value, String type, String tgtType)
        {
            Key = key;
            Value = value;
            Type = type;
            TgtType = tgtType;
            Values = new ArrayList();
            TgtValues = new ArrayList(); 
        }
    }
    
	class ElementDef
    {
        public String ElemName;
        public String VarName;
        public String TypeName;
        public ArrayList Attributes;

        public ElementDef(String elemName, String varName, String typeName, ArrayList attributes)
        {
            ElemName = elemName;
            VarName = varName;
            TypeName = typeName;
            Attributes = attributes;
        }
    }
    
    class GRSImporter
	{
		NamedGraph graph;
		IBackend backend;
		IGraphModel model;
		String modelOverride;

        /// <summary>
        /// Imports the given graph from a file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used, 
        ///     it must be conformant to the model used in the file to be imported.</param>
        /// <returns>The imported graph. 
        /// A NamedGraph is returned. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>		
        public static IGraph Import(String importFilename, String modelOverride, IBackend backend)
        {
            return Import(new StreamReader(importFilename), modelOverride, backend);
        }

        /// <summary>
        /// Imports the given graph from the given text reader input stream.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="reader">The text reader input stream import source.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used, 
        ///     it must be conformant to the model used in the file to be imported.</param>
        /// <returns>The imported graph. 
        /// A NamedGraph is returned. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>
        public static IGraph Import(TextReader reader, String modelOverride, IBackend backend)
        {
            GRSImporter importer = new GRSImporter(reader);
            importer.backend = backend;
            importer.modelOverride = modelOverride;
            importer.model = null;
            while(importer.ParseGraphBuildingScript()) /*empty*/;
            return importer.graph;
        }

        /// <summary>
        /// Imports the given graph from a file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <returns>The imported graph. 
        /// A NamedGraph is returned. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>        
        public static IGraph Import(String importFilename, IBackend backend)
        {
            return Import(importFilename, null, backend);
        }

		/// <summary>
        /// Imports the given graph from a file with the given filename.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="importFilename">The filename of the file to be imported.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used.</param>
        /// <returns>The imported graph. 
        /// A NamedGraph is returned. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>
        public static IGraph Import(String importFilename, IBackend backend, IGraphModel graphModel)
		{
            return Import(new StreamReader(importFilename), backend, graphModel);
        }
        
        /// <summary>
        /// Imports the given graph from the given text reader input stream.
        /// Any errors will be reported by exception.
        /// </summary>
        /// <param name="reader">The text reader input stream import source.</param>
        /// <param name="backend">The backend to use to create the graph.</param>
        /// <param name="graphModel">The graph model to be used.</param>
        /// <returns>The imported graph. 
        /// A NamedGraph is returned. If you don't need it: cast to it, get the contained (lgsp) graph, and throw the named graph away
        /// (the naming requires about the same amount of memory the raw graph behind it requires).</returns>
        public static IGraph Import(TextReader reader, IBackend backend, IGraphModel graphModel)
        {
            GRSImporter importer = new GRSImporter(reader);
            importer.backend = backend;
            importer.modelOverride = null;
            importer.model = graphModel;
            while(importer.ParseGraphBuildingScript()) /*empty*/;
            return importer.graph;
        }
        
        public INode GetNodeByVar(String varName)
        {
            return (INode)GetElemByVar(varName);
        }

        public INode GetNodeByName(String elemName)
        {
            return (INode)GetElemByName(elemName);
        }

        public IEdge GetEdgeByVar(String varName)
        {
            return (IEdge)GetElemByVar(varName);
        }

        public IEdge GetEdgeByName(String elemName)
        {
            return (IEdge)GetElemByName(elemName);
        }

        public IGraphElement GetElemByVar(String varName)
        {
			object elem = graph.GetVariableValue(varName);
			if(elem==null) throw new Exception("Unknown variable "+varName);
            return (IGraphElement)elem;
        }

        public IGraphElement GetElemByName(String elemName)
        {
	        IGraphElement elem = graph.GetGraphElement(elemName);
	        if(elem==null) throw new Exception("Unknown graph element "+elemName);
            return elem;
        }
        
        public void NewNode(ElementDef elemDef)
        {
            NodeType nodeType;
            if(elemDef.TypeName != null)
            {
                nodeType = graph.Model.NodeModel.GetType(elemDef.TypeName);
                if(nodeType==null) throw new Exception("Unknown node type: \"" + elemDef.TypeName + "\"");
                if(nodeType.IsAbstract) throw new Exception("Abstract node type \"" + elemDef.TypeName + "\" may not be instantiated!");
            }
            else nodeType = graph.Model.NodeModel.RootType;

			INode node = graph.AddNode(nodeType, elemDef.VarName, elemDef.ElemName);
			if(node==null) throw new Exception("Can't create node");
            
            if(elemDef.Attributes!=null) SetAttributes(node, elemDef.Attributes);
        }

        public void NewEdge(ElementDef elemDef, INode node1, INode node2)
        {
            EdgeType edgeType;
            if(elemDef.TypeName != null)
            {
                edgeType = graph.Model.EdgeModel.GetType(elemDef.TypeName);
                if(edgeType==null) throw new Exception("Unknown edge type: \"" + elemDef.TypeName + "\"");
                if(edgeType.IsAbstract) throw new Exception("Abstract edge type \"" + elemDef.TypeName + "\" may not be instantiated!");
            }
            else edgeType = graph.Model.EdgeModel.RootType;

            IEdge edge = graph.AddEdge(edgeType, node1, node2, elemDef.VarName, elemDef.ElemName);
            if(edge==null) throw new Exception("Can't create edge");
            
            if(elemDef.Attributes!=null) SetAttributes(edge, elemDef.Attributes);
        }
        
        public void DeferredAttributeInitialization(IGraphElement elem, ArrayList defrAttrInit)
        {
	        SetAttributes(elem, defrAttrInit);
        }

		private object ParseAttributeValue(AttributeKind attrKind, String valueString) // not set/map/enum
        {
            object value = null;
            switch(attrKind)
            {
            case AttributeKind.BooleanAttr:
                if(valueString.Equals("true", StringComparison.OrdinalIgnoreCase))
                    value = true;
                else if(valueString.Equals("false", StringComparison.OrdinalIgnoreCase))
                    value = false;
                else
                    throw new Exception("Unknown boolean literal");
                break;
            case AttributeKind.IntegerAttr:
                if(valueString.StartsWith("0x"))
                    value = Int32.Parse(valueString.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber);
				else
					value = Int32.Parse(valueString);
                break;
            case AttributeKind.StringAttr:
                value = valueString;
                break;
            case AttributeKind.FloatAttr:
				if(valueString[valueString.Length-1]=='f') // cut f suffix (required, but be tolerant to incorrect format of pre 2.6RC3, so here optional)
					valueString = valueString.Substring(0, valueString.Length-1);
                value = Single.Parse(valueString, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case AttributeKind.DoubleAttr:
				if(valueString[valueString.Length-1]=='d') // cut d suffix if given
					valueString = valueString.Substring(0, valueString.Length-1);
				value = Double.Parse(valueString, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case AttributeKind.ObjectAttr:
				if(valueString!="null")
	                throw new Exception("(Non-null) Object attributes unsupported");
				value = null;
				break;
			case AttributeKind.NodeAttr:
				if(valueString[0]=='@' && valueString[1]=='(' && valueString[valueString.Length-1]==')') {
					if((valueString[2]=='\"' || valueString[2]=='\'') && (valueString[valueString.Length-2]=='\"' || valueString[valueString.Length-2]=='\''))
						value = GetNodeByName(valueString.Substring(3, valueString.Length-5));
					else
						value = GetNodeByName(valueString.Substring(2, valueString.Length-3));
				} else {
					value = GetNodeByVar(valueString);
				}
				break;
			case AttributeKind.EdgeAttr:
				if(valueString[0]=='@' && valueString[1]=='(' && valueString[valueString.Length-1]==')') {
					if((valueString[2]=='\"' || valueString[2]=='\'') && (valueString[valueString.Length-2]=='\"' || valueString[valueString.Length-2]=='\''))
						value = GetEdgeByName(valueString.Substring(3, valueString.Length-5));
					else
						value = GetEdgeByName(valueString.Substring(2, valueString.Length-3));					
				} else {
					value = GetEdgeByVar(valueString);
				}
				break;
            }
            return value;
        }

		private object ParseAttributeValue(AttributeType attrType, String valueString) // not set/map
        {
            object value = null;
            if(attrType.Kind==AttributeKind.EnumAttr)
            {
				if(valueString.IndexOf("::")!=-1) {
					valueString = valueString.Substring(valueString.IndexOf("::")+"::".Length);
				}

                int val;
                if(Int32.TryParse(valueString, out val)) {
                    value = Enum.ToObject(attrType.EnumType.EnumType, val);
                } else {
       	            value = Enum.Parse(attrType.EnumType.EnumType, valueString);
                }
                if(value == null) {
                    throw new Exception("Unknown enum member");
                }
            }
            else
            {
				value = ParseAttributeValue(attrType.Kind, valueString);
            }
            return value;
        }
        
        private void SetAttributes(IGraphElement elem, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = elem.Type.GetAttributeType(par.Key);
                object value = null;
                IDictionary setmap = null;
                switch(attrType.Kind)
                {
                case AttributeKind.SetAttr:
	                if(par.Value!="set") throw new Exception("Set literal expected");
	                setmap = DictionaryHelper.NewDictionary(
	                    DictionaryHelper.GetTypeFromNameForDictionary(par.Type, graph),
	                    typeof(de.unika.ipd.grGen.libGr.SetValueType));
	                foreach(object val in par.Values)
	                {
                        setmap.Add( ParseAttributeValue(attrType.ValueType, (String)val), null );
	                }
	                value = setmap;
	                break;
                case AttributeKind.MapAttr:
   	                if(par.Value!="map") throw new Exception("Map literal expected");
	                setmap = DictionaryHelper.NewDictionary(
	                    DictionaryHelper.GetTypeFromNameForDictionary(par.Type, graph),
	                    DictionaryHelper.GetTypeFromNameForDictionary(par.TgtType, graph));
	                IEnumerator tgtValEnum = par.TgtValues.GetEnumerator();
	                foreach(object val in par.Values)
	                {
	                    tgtValEnum.MoveNext();
                        setmap.Add( ParseAttributeValue(attrType.KeyType, (String)val),
                            ParseAttributeValue(attrType.ValueType, (String)tgtValEnum.Current) );
	                }
	                value = setmap;
	                break;
				default:
					value = ParseAttributeValue(attrType, par.Value);
					break;
                }

                AttributeChangeType changeType = AttributeChangeType.Assign;
                if (elem is INode)
                    graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
                else
                    graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
                elem.SetAttribute(par.Key, value);
            }
        }
    }
PARSER_END(GRSImporter)

// characters to be skipped
SKIP: {
	" " |
	"\t" |
	"\r" |
	"\\\r\n" |
	"\\\n" |
	"\\\r"
}

TOKEN: {
    < NL: "\n" >
|   < QUOTE: "\"" >
|   < SINGLEQUOTE: "\'" >
|   < EQUAL: "=" >
|   < COMMA: "," >
|   < ARROW: "->" >
|   < MINUS: "-" >
|   < LPARENTHESIS: "(" >
|   < RPARENTHESIS: ")" >
|   < AT: "@" >
|   < LANGLE: "<" >
|   < RANGLE: ">" >
|   < LBRACE: "{" >
|   < RBRACE: "}" >
}

TOKEN: {
    < FALSE: "false" >
|   < GRAPH: "graph" >
|   < NEW: "new" >
|   < NODE: "node" >
|   < NULL: "null" >
|   < TRUE: "true" >
|   < SET: "set" >
|   < MAP: "map" >
}

TOKEN: {
	< NUMBER: (["0"-"9"])+ >
|
	< HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
}

TOKEN: {
	< NUMFLOAT:
			("-")? (["0"-"9"])+ ("." (["0"-"9"])+)? (<EXPONENT>)? ["f", "F"]
		|	("-")? "." (["0"-"9"])+ (<EXPONENT>)? ["f", "F"]
	>
|
	< NUMDOUBLE:
			("-")? (["0"-"9"])+ "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
		|	("-")? "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
		|	("-")? (["0"-"9"])+ <EXPONENT> (["d", "D"])?
		|	("-")? (["0"-"9"])+ ["d", "D"]
	>
|
	< #EXPONENT: ["e", "E"] (["+", "-"])? (["0"-"9"])+ >
}

TOKEN: {
	< DOUBLEQUOTEDTEXT : "\"" (~["\"", "\n", "\r"])* "\"" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|	< SINGLEQUOTEDTEXT : "\'" (~["\'", "\n", "\r"])* "\'" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|	< WORD : ~["\'", "\"", "0"-"9", "=", ":", ";", ".", ",", "+", "-", "&", "%", "?", "$", "|", "<", ">", "(", ")", "{", "}", "[", "]", "*", "!", "#", " ", "@", "\n", "\r"]
	     (~["\'", "\"", "=", ":", ";", ".", ",", "+", "-", "&", "%", "?", "$", "|", "<", ">", "(", ")", "{", "}", "[", "]", "*", "!", "#", " ", "@", "\n", "\r"])*	>
}

SPECIAL_TOKEN: {
	< SINGLE_LINE_COMMENT: "#" (~["\n"])* >
}

<WithinFilename> SKIP: {
	" " |
	"\t" |
	"\r"
}

<WithinFilename> TOKEN: {
	< DOUBLEQUOTEDFILENAME: "\"" (~["\"", "\n", "\r"])* "\"" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|	< SINGLEQUOTEDFILENAME: "\'" (~["\'", "\n", "\r"])* "\'" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|	< FILENAME: ~["\'", "\"", "=", ";", "$", "|", "*", " ", "?", "\n", "\r"]
	     (~["\'", "\"", "=", ";", "$", "|", "*", " ", "?", "\n", "\r"])* > : DEFAULT
|	< NLINFILENAME: "\n" > : DEFAULT
|	< ERRORFILENAME: ~[] > : DEFAULT
}

String Word():
{
    Token tok;
}
{
    tok=<WORD>
    {
        return tok.image;
    }
}

String Text():
{
	Token tok;
}
{
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD>)
	{
		return tok.image;		
	}
}

String AttributeValue():
{
	Token tok;
	String enumName, enumValue;
}
{
	(
		LOOKAHEAD(2) enumName=Word() "::" enumValue=Word()
		{
			return enumName + "::" + enumValue;
		}
	|
		(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD> | tok=<NUMBER> | tok=<HEXNUMBER> | tok=<NUMFLOAT> | tok=<NUMDOUBLE> | tok=<TRUE> | tok=<FALSE> | tok=<NULL> )
		{
			return tok.image;		
		}
	)
}

String Filename():
{
    Token tok;
}
{
    {token_source.SwitchTo(WithinFilename);}
    (tok=<DOUBLEQUOTEDFILENAME> | tok=<SINGLEQUOTEDFILENAME> | tok=<FILENAME>)
	{
		return tok.image.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
	}
}

INode Node():
{
	INode node;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { node = GetNodeByName(str); }
	|
		str=Text() { node = GetNodeByVar(str); }
	)
	{ return node; }
}

IEdge Edge():
{
	IEdge edge;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { edge = GetEdgeByName(str); }
	|
		str=Text() { edge = GetEdgeByVar(str); }
	)
	{ return edge; }
}

IGraphElement GraphElement():
{
	IGraphElement elem;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { elem = GetElemByName(str); }
	|
		str=Text() { elem = GetElemByVar(str); }
	)
	{ return elem; }
}


void LineEnd():
{}
{
	(<NL> | <EOF>)
}

bool ParseGraphBuildingScript() :
{
	String modelFilename, graphName="";
	INode srcNode, tgtNode;
	ElementDef elemDef;
	IGraphElement elem;
	String attrName;
	ArrayList defrAttrInit;
}
{
	(
	"new"
		(
		"graph" modelFilename=Filename() (graphName=Text())? LineEnd()
			{
				if(modelOverride!=null) {
					modelFilename = modelOverride;
				} else {
					modelFilename += ".gm";
				}
				if(model!=null) {
					graph = new NamedGraph(backend.CreateGraph(model, graphName));
				} else {
					graph = new NamedGraph(backend.CreateGraph(modelFilename, graphName));
				}
		        return true;
			}
		| LOOKAHEAD(2) srcNode=Node() "-" elemDef=ElementDefinition() "->" tgtNode=Node() LineEnd()
			{
				NewEdge(elemDef, srcNode, tgtNode);
				return true;
			}
		| LOOKAHEAD(2) elemDef=ElementDefinition() LineEnd()
			{
				NewNode(elemDef);
				return true;
			}
		)
	|
		elem=GraphElement() "." { defrAttrInit = new ArrayList(); } SingleAttribute(defrAttrInit) LineEnd()
			{ 
				DeferredAttributeInitialization(elem, defrAttrInit);
				return true;
			}
	| <NL>
		{
			return true;
		}
	| <EOF>
		{
			return false;
		}
	)
}

ElementDef ElementDefinition():
{
	String varName = null, typeName = null, elemName = null;
	ArrayList attributes = new ArrayList();
}
{
	(varName=Text())?
	(
		":" typeName=Text()
		(
			"("
			(
				"$" "=" elemName=Text() ("," Attributes(attributes))?
			|
				Attributes(attributes)
			)?
			")"
		)?	
	)?
	{
		return new ElementDef(elemName, varName, typeName, attributes);
	}
}

void Attributes(ArrayList attributes):
{}
{
	SingleAttribute(attributes) (LOOKAHEAD(2) "," SingleAttribute(attributes) )*
}

void SingleAttribute(ArrayList attributes):
{
	String attribName, value, valueTgt;
	Token type, typeTgt;
	Param param;
}
{
	attribName=Text() "=" 
		(value=AttributeValue()
			{
				attributes.Add(new Param(attribName, value));
			}
		| <SET> <LANGLE> type=<WORD> <RANGLE> 
			{ param = new Param(attribName, "set", type.image); }
			<LBRACE> ( value=AttributeValue() { param.Values.Add(value); } )? 
			    (<COMMA> value=AttributeValue() { param.Values.Add(value); })* <RBRACE>
			{ attributes.Add(param); }
		| <MAP> <LANGLE> type=<WORD> <COMMA> typeTgt=<WORD> <RANGLE>
			{ param = new Param(attribName, "map", type.image, typeTgt.image); }
			<LBRACE> ( value=AttributeValue() { param.Values.Add(value); } <ARROW> valueTgt=AttributeValue() { param.TgtValues.Add(valueTgt); } )?
				( <COMMA> value=AttributeValue() { param.Values.Add(value); } <ARROW> valueTgt=AttributeValue() { param.TgtValues.Add(valueTgt); } )* <RBRACE>
			{ attributes.Add(param); }
		)
}
