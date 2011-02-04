<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes" />

  <xsl:template match="go">

    <xsl:variable name="host" select="substring-before(detailsUrl, '/go/')" />
    <!-- 
      <xsl:variable name="host" select="substring-before(detailsUrl, '/go/')" />
      <xsl:variable name="host" select="'http://localhost:8153'" /> 
      -->

    <xsl:variable name="defaultJob" select="'defaultJob'" />

    <xsl:variable name="stageHistoryUrl" select="concat($host, '/go/tab/pipeline/history/', pipeline)" />
    <xsl:variable name="jobDetailUrl" select="concat($host, '/go/tab/build/detail/', pipeline, '/', label, '/', stage, '/', job, '/', $defaultJob)" />
    <xsl:variable name="jobDetailMaterialsUrl" select="concat($jobDetailUrl, '#tab-materials')" />
    <xsl:variable name="jobDetailTestsUrl" select="concat($jobDetailUrl, '#tab-tests')" />
    <xsl:variable name="jobDetailArtifactsUrl" select="concat($jobDetailUrl, '#tab-artifacts')" />
    <xsl:variable name="jobDetailConsoleUrl" select="concat($jobDetailUrl, '#tab-console')" />
    <xsl:variable name="jobDetailFailuresUrl" select="concat($jobDetailUrl, '#tab-failures')" />

    <html>
      <body bgcolor="#E7E1D1" style="font-family:Verdana">
        <table style="background-color:#ffffff;border-style:solid;border-color:#938B55;border-width:1px;padding:20px" width="100%">
          <tr>
            <td style="padding:10px;font-size:12pt;font-weight:bold;font-family:Verdana">
                <a style="text-decoration:none;color:#78351F" href="{$stageHistoryUrl}">
                  <xsl:value-of select="pipeline"/> Pipeline
                </a>
            </td>
          </tr>
          <tr>
              <xsl:attribute name="style">
                <xsl:choose>
                  <xsl:when test="status = 'passed' or status = 'is fixed'">background-color:#578810</xsl:when>
                  <xsl:otherwise>background-color:#D23221</xsl:otherwise>
                </xsl:choose>
              </xsl:attribute>
            <td style="font-size:10.5pt;padding:10px;color:#ffffff;font-family:Verdana">
              <b>
                <a style="text-decoration:none;color:#ffffff" href="{$jobDetailUrl}">
                  <xsl:value-of select="concat(stage, '\', $defaultJob)"/> (<xsl:value-of select="label"/>)</a> |
                <a style="text-decoration:none;color:#ffffff" href="{$jobDetailConsoleUrl}">Console</a> |
                <a style="text-decoration:none;color:#ffffff" href="{$jobDetailTestsUrl}">Tests</a> |
                <a style="text-decoration:none;color:#ffffff" href="{$jobDetailFailuresUrl}">Failures</a> |
                <a style="text-decoration:none;color:#ffffff" href="{$jobDetailArtifactsUrl}">Artifacts</a> |
                <a style="text-decoration:none;color:#ffffff" href="{$jobDetailMaterialsUrl}">Materials</a>
              </b>
            </td>
          </tr>
          <tr>
            <td style="font-family:Courier New;font-size:8pt;padding:10px">
              <pre>
                <xsl:value-of select="vcs"/>
              </pre>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
