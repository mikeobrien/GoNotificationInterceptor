<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                >
  <xsl:output method="text" indent="no"/>

  <xsl:template match="go"><xsl:value-of select="stage"/> Release <xsl:choose><xsl:when test="status = 'passed' or status = 'is fixed'">Succeeded</xsl:when><xsl:otherwise>Failed</xsl:otherwise></xsl:choose> (<xsl:value-of select="label"/>)</xsl:template>

</xsl:stylesheet>
