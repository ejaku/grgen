// cradle file for features to be implemented by AI code generators
// by Claude Code with Edgar Jakumeit

using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;
using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.grShell
{
    public class GrShellAiFeatures
    {
        // Realizer names for different type categories
        private const string NodeTypeRealizer = "uml_node_type";
        private const string EdgeTypeRealizer = "uml_edge_type";
        private const string ObjectTypeRealizer = "uml_object_type";
        private const string TransientObjectTypeRealizer = "uml_transient_object_type";
        private const string EnumTypeRealizer = "uml_enum_type";
        private const string InheritanceEdgeRealizer = "uml_inheritance";
        private const string PackageRealizer = "uml_package";

        private HashSet<string> createdPackages;

        public void RenderModelAsGraph(IGraphModel model, IBasicGraphViewerClient basicGraphViewer)
        {
            createdPackages = new HashSet<string>();

            RegisterRealizers(basicGraphViewer);

            RenderNodeTypes(model.NodeModel, basicGraphViewer);
            RenderEdgeTypes(model.EdgeModel, basicGraphViewer);
            RenderObjectTypes(model.ObjectModel, basicGraphViewer);
            RenderTransientObjectTypes(model.TransientObjectModel, basicGraphViewer);
            RenderEnumTypes(model.EnumAttributeTypes, basicGraphViewer);
        }

        private void RegisterRealizers(IBasicGraphViewerClient basicGraphViewer)
        {
            // Node types: yellow with dark yellow border (same as normal nodes)
            basicGraphViewer.AddNodeRealizer(NodeTypeRealizer,
                GrColor.DarkYellow, GrColor.Yellow, GrColor.Black, GrNodeShape.Box);

            // Edge types: khaki (similar to normal edges)
            basicGraphViewer.AddNodeRealizer(EdgeTypeRealizer,
                GrColor.DarkYellow, GrColor.Khaki, GrColor.Black, GrNodeShape.Box);

            // Object types (internal classes): white
            basicGraphViewer.AddNodeRealizer(ObjectTypeRealizer,
                GrColor.Grey, GrColor.White, GrColor.Black, GrNodeShape.Box);

            // Transient object types: light grey
            basicGraphViewer.AddNodeRealizer(TransientObjectTypeRealizer,
                GrColor.Grey, GrColor.LightGrey, GrColor.Black, GrNodeShape.Box);

            // Enum types: white
            basicGraphViewer.AddNodeRealizer(EnumTypeRealizer,
                GrColor.Grey, GrColor.White, GrColor.Black, GrNodeShape.Box);

            // Package nodes: light grey rounded box
            basicGraphViewer.AddNodeRealizer(PackageRealizer,
                GrColor.Grey, GrColor.LightGrey, GrColor.Black, GrNodeShape.Box);

            // Inheritance edges: grey continuous line
            basicGraphViewer.AddEdgeRealizer(InheritanceEdgeRealizer,
                GrColor.Grey, GrColor.Grey, 1, GrLineStyle.Continuous);
        }

        private void EnsurePackageNode(string package, IBasicGraphViewerClient basicGraphViewer)
        {
            if(package != null && createdPackages.Add(package))
            {
                basicGraphViewer.AddSubgraphNode("pkg_" + package, PackageRealizer, "<<package>>\\n" + package);
            }
        }

        private void MoveToPackageIfNeeded(string nodeName, string package, IBasicGraphViewerClient basicGraphViewer)
        {
            if(package != null)
                basicGraphViewer.MoveNode(nodeName, "pkg_" + package);
        }

        private void RenderNodeTypes(INodeModel nodeModel, IBasicGraphViewerClient basicGraphViewer)
        {
            foreach(NodeType nodeType in nodeModel.Types)
            {
                EnsurePackageNode(nodeType.Package, basicGraphViewer);
                string nodeName = GetNodeNameForType(nodeType);
                string label = BuildTypeLabel(nodeType);
                basicGraphViewer.AddNode(nodeName, NodeTypeRealizer, label);
                MoveToPackageIfNeeded(nodeName, nodeType.Package, basicGraphViewer);

                RenderInheritanceEdges(nodeType, nodeName, basicGraphViewer);
            }
        }

        private void RenderEdgeTypes(IEdgeModel edgeModel, IBasicGraphViewerClient basicGraphViewer)
        {
            foreach(EdgeType edgeType in edgeModel.Types)
            {
                EnsurePackageNode(edgeType.Package, basicGraphViewer);
                string nodeName = GetNodeNameForType(edgeType);
                string label = BuildTypeLabel(edgeType);
                basicGraphViewer.AddNode(nodeName, EdgeTypeRealizer, label);
                MoveToPackageIfNeeded(nodeName, edgeType.Package, basicGraphViewer);

                RenderInheritanceEdges(edgeType, nodeName, basicGraphViewer);
            }
        }

        private void RenderObjectTypes(IObjectModel objectModel, IBasicGraphViewerClient basicGraphViewer)
        {
            foreach(ObjectType objectType in objectModel.Types)
            {
                EnsurePackageNode(objectType.Package, basicGraphViewer);
                string nodeName = GetNodeNameForType(objectType);
                string label = BuildTypeLabel(objectType);
                basicGraphViewer.AddNode(nodeName, ObjectTypeRealizer, label);
                MoveToPackageIfNeeded(nodeName, objectType.Package, basicGraphViewer);

                RenderInheritanceEdges(objectType, nodeName, basicGraphViewer);
            }
        }

        private void RenderTransientObjectTypes(ITransientObjectModel transientObjectModel, IBasicGraphViewerClient basicGraphViewer)
        {
            foreach(TransientObjectType transientObjectType in transientObjectModel.Types)
            {
                EnsurePackageNode(transientObjectType.Package, basicGraphViewer);
                string nodeName = GetNodeNameForType(transientObjectType);
                string label = BuildTypeLabel(transientObjectType);
                basicGraphViewer.AddNode(nodeName, TransientObjectTypeRealizer, label);
                MoveToPackageIfNeeded(nodeName, transientObjectType.Package, basicGraphViewer);

                RenderInheritanceEdges(transientObjectType, nodeName, basicGraphViewer);
            }
        }

        private void RenderEnumTypes(IEnumerable<EnumAttributeType> enumTypes, IBasicGraphViewerClient basicGraphViewer)
        {
            foreach(EnumAttributeType enumType in enumTypes)
            {
                EnsurePackageNode(enumType.Package, basicGraphViewer);
                string nodeName = "enum_" + enumType.PackagePrefixedName;
                string label = BuildEnumLabel(enumType);
                basicGraphViewer.AddNode(nodeName, EnumTypeRealizer, label);
                MoveToPackageIfNeeded(nodeName, enumType.Package, basicGraphViewer);
            }
        }

        private void RenderInheritanceEdges(InheritanceType type, string childNodeName, IBasicGraphViewerClient basicGraphViewer)
        {
            foreach(InheritanceType superType in type.DirectSuperTypes)
            {
                string parentNodeName = GetNodeNameForType(superType);
                string edgeName = "inh_" + type.PackagePrefixedName + "_" + superType.PackagePrefixedName;
                basicGraphViewer.AddEdge(edgeName, childNodeName, parentNodeName, InheritanceEdgeRealizer, "");
            }
        }

        private string GetNodeNameForType(InheritanceType type)
        {
            if(type is NodeType)
                return "nt_" + type.PackagePrefixedName;
            else if(type is EdgeType)
                return "et_" + type.PackagePrefixedName;
            else if(type is TransientObjectType)
                return "tot_" + type.PackagePrefixedName;
            else if(type is ObjectType)
                return "ot_" + type.PackagePrefixedName;
            else
                return "type_" + type.PackagePrefixedName;
        }

        private string BuildTypeLabel(InheritanceType type)
        {
            StringBuilder sb = new StringBuilder();

            // Add abstract stereotype if applicable
            if(type.IsAbstract)
                sb.Append("<<abstract>>\\n");

            // Use non-prefixed name if in a package (package is shown by nesting)
            sb.Append(type.Package != null ? type.Name : type.PackagePrefixedName);

            // Add separator and attributes if any
            if(type.NumAttributes > 0)
            {
                sb.Append("\\n--------");
                foreach(AttributeType attr in type.AttributeTypes)
                {
                    // Only show attributes defined on this type, not inherited ones
                    if(attr.OwnerType == type)
                    {
                        sb.Append("\\n");
                        sb.Append(attr.Name);
                        sb.Append(": ");
                        sb.Append(TypesHelper.AttributeTypeToXgrsType(attr));
                    }
                }
            }

            return sb.ToString();
        }

        private string BuildEnumLabel(EnumAttributeType enumType)
        {
            StringBuilder sb = new StringBuilder();

            // Add enum stereotype
            sb.Append("<<enum>>\\n");

            // Use non-prefixed name if in a package (package is shown by nesting)
            sb.Append(enumType.Package != null ? enumType.Name : enumType.PackagePrefixedName);

            // Add separator and members
            sb.Append("\\n--------");
            foreach(EnumMember member in enumType.Members)
            {
                sb.Append("\\n");
                sb.Append(member.Name);
                sb.Append(" = ");
                sb.Append(member.Value);
            }

            return sb.ToString();
        }

    }
}
