<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="text"/>
	<xsl:template match="/unit">
<xsl:text>
struct {
</xsl:text>
		<xsl:for-each select="node_type">
			<xsl:text>	gr_id_t N_</xsl:text>
			<xsl:value-of select="@name"/>
<xsl:text>
</xsl:text>
			<xsl:for-each select="attributes/entity">
				<xsl:text>	gr_id_t NA_</xsl:text>
				<xsl:value-of select="../../@name"/>
				<xsl:text>__</xsl:text>
				<xsl:value-of select="@name"/>
<xsl:text>
</xsl:text>
			</xsl:for-each>
		</xsl:for-each>
<xsl:text>
} karlchen;

/**********************************************************/

struct {
</xsl:text>
		<xsl:for-each select="edge_type">
			<xsl:text>	gr_id_t E_</xsl:text>
			<xsl:value-of select="@name"/>
<xsl:text>
</xsl:text>
			<xsl:for-each select="attributes/entity">
				<xsl:text>	gr_id_t EA_</xsl:text>
				<xsl:value-of select="../../@name"/>
				<xsl:text>__</xsl:text>
				<xsl:value-of select="@name"/>
<xsl:text>
</xsl:text>
			</xsl:for-each>
		</xsl:for-each>
<xsl:text>
} michel;

/**********************************************************/

struct {
</xsl:text>
		<xsl:for-each select="enum_type">
			<xsl:text>	gr_id_t T_</xsl:text>
			<xsl:value-of select="@name"/>
<xsl:text>
</xsl:text>
		</xsl:for-each>
} gudrun;
	</xsl:template>
</xsl:stylesheet>
