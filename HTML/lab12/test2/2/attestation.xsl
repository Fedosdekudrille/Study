<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html> 
<body>
	<h2>Аттестация студентов 1 курса по ОАИП</h2>
	<table border="1">
		<tr bgcolor="#9acd32">
			<th style="text-align:left">Имя</th>
			<th style="text-align:left">Фамилия</th>
			<th style="text-align:left">Итоговый балл</th>
			<th style="text-align:left">Посещаемость</th>
		</tr>
		<xsl:for-each select="attestation/student">
			<tr>
				<td><xsl:value-of select="name"/></td>
				<td><xsl:value-of select="surname"/></td>
				<xsl:choose>
					<xsl:when test="score &gt; 8"> 
						<td bgcolor="#00ff00"><xsl:value-of select="score"/></td>
					</xsl:when>
					<xsl:when test="score &lt; 4"> 
						<td bgcolor="#ff0000"><xsl:value-of select="score"/></td>
					</xsl:when>
					<xsl:otherwise>
						<td><xsl:value-of select="score"/></td>
					</xsl:otherwise>
				</xsl:choose>
				<td><xsl:value-of select="attendance"/></td>
			</tr>
    </xsl:for-each>
  </table>
</body>
</html>
</xsl:template>
</xsl:stylesheet>