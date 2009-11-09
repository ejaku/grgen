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
    
    class GRSImporter {
		IGraph graph;
		IBackend backend;
		IGraphModel model;
		String modelOverride;
		
        public static IGraph Import(String importFilename, String modelOverride, IBackend backend)
        {
            TextReader reader = new StreamReader(importFilename);
            GRSImporter importer = new GRSImporter(reader);
            importer.backend = backend;
            importer.modelOverride = modelOverride;
            importer.model = null;
            while(importer.ParseGraphBuildingScript()) /*empty*/;
            return importer.graph;
        }
        
        public static IGraph Import(String importFilename, IBackend backend)
        {
			return Import(importFilename, null, backend);
        }
        
        public static IGraph Import(String importFilename, IBackend backend, IGraphModel graphModel)
        {
            TextReader reader = new StreamReader(importFilename);
            GRSImporter importer = new GRSImporter(reader);
            importer.backend = backend;
            importer.modelOverride = null;
            importer.model = graphModel;
            while(importer.ParseGraphBuildingScript()) /*empty*/;
            return importer.graph;
        }
        
        public INode GetNodeByVar(String varName)
        {
			object elem = graph.GetVariableValue(varName);
			if(elem==null) throw new Exception("Unknown variable "+varName);
            return (INode)elem;
        }

        public INode GetNodeByName(String elemName)
        {
	        IGraphElement elem;
			if(graph is NamedGraph) {
				elem = ((NamedGraph)graph).GetGraphElement(elemName);
			} else {
				elem = GetNodeByVar(elemName);
			}
	        if(elem==null) throw new Exception("Unknown graph element "+elemName);
            return (INode)elem;
        }

        public IEdge GetEdgeByVar(String varName)
        {
  			object elem = graph.GetVariableValue(varName);
			if(elem==null) throw new Exception("Unknown variable "+varName);
            return (IEdge)elem;
        }

        public IEdge GetEdgeByName(String elemName)
        {
	        IGraphElement elem;
   			if(graph is NamedGraph) {
   				elem = ((NamedGraph)graph).GetGraphElement(elemName);
   			} else {
   				elem = GetEdgeByVar(elemName);
   			}
	        if(elem==null) throw new Exception("Unknown graph element "+elemName);
            return (IEdge)elem;
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

			INode node;
			if(graph is NamedGraph) {
	            node = ((NamedGraph)graph).AddNode(nodeType, elemDef.VarName, elemDef.ElemName);
			} else {
				string varName = elemDef.VarName!=null ? elemDef.VarName : elemDef.ElemName;
	            node = graph.AddNode(nodeType, varName);
			}
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

            IEdge edge;
            if(graph is NamedGraph) {
	            edge = ((NamedGraph)graph).AddEdge(edgeType, node1, node2, elemDef.VarName, elemDef.ElemName);
			} else {
				string varName = elemDef.VarName!=null ? elemDef.VarName : elemDef.ElemName;
	            edge = graph.AddEdge(edgeType, node1, node2, varName);
			}
            if(edge==null) throw new Exception("Can't create edge");
            
            if(elemDef.Attributes!=null) SetAttributes(edge, elemDef.Attributes);
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
				value = Int32.Parse(valueString);
                break;
            case AttributeKind.StringAttr:
                value = valueString;
                break;
            case AttributeKind.FloatAttr:
                value = Single.Parse(valueString);
                break;
            case AttributeKind.DoubleAttr:
                value = Double.Parse(valueString);
                break;
            case AttributeKind.ObjectAttr:
                throw new Exception("Object attributes unsupported");
            }
            return value;
        }

		private object ParseAttributeValue(AttributeType attrType, String valueString) // not set/map
        {
            object value = null;
            if(attrType.Kind==AttributeKind.EnumAttr)
            {
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

String TextOrNumberOrBoolLit():
{
	Token tok;
}
{
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD> | tok=<NUMBER> | tok=<TRUE> | tok=<FALSE>)
	{
		return tok.image;		
	}
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
					graph = backend.CreateGraph(model, graphName);
				} else {
					graph = backend.CreateGraph(modelFilename, graphName);
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
		(value=TextOrNumberOrBoolLit()
			{
				attributes.Add(new Param(attribName, value));
			}
		| <SET> <LANGLE> type=<WORD> <RANGLE> 
			{ param = new Param(attribName, "set", type.image); }
			<LBRACE> ( value=TextOrNumberOrBoolLit() { param.Values.Add(value); } )? 
			    (<COMMA> value=TextOrNumberOrBoolLit() { param.Values.Add(value); })* <RBRACE>
			{ attributes.Add(param); }
		| <MAP> <LANGLE> type=<WORD> <COMMA> typeTgt=<WORD> <RANGLE>
			{ param = new Param(attribName, "map", type.image, typeTgt.image); }
			<LBRACE> ( value=TextOrNumberOrBoolLit() { param.Values.Add(value); } <ARROW> valueTgt=TextOrNumberOrBoolLit() { param.TgtValues.Add(valueTgt); } )?
				( <COMMA> value=TextOrNumberOrBoolLit() { param.Values.Add(value); } <ARROW> valueTgt=TextOrNumberOrBoolLit() { param.TgtValues.Add(valueTgt); } )* <RBRACE>
			{ attributes.Add(param); }
		)
}
