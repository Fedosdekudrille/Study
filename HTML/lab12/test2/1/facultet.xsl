<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html> 
<body>
	<h2>Список специальностей факультета</h2>
	<table border="1">
		<tr bgcolor="#9acd32">
			<th style="text-align:left">Специальность</th>
			<th style="text-align:left">Срок обучения</th>
			<th style="text-align:left">Предметы ЦТ</th>
			<th style="text-align:left">План набора</th>
			<th style="text-align:left">Проходной балл</th>
		</tr>
		<xsl:for-each select="faculty/specialization">
			<xsl:sort select="passing" order="ascending"/>
				<tr>
					<td><xsl:value-of select="name"/></td>
					<td><xsl:value-of select="time"/></td>
					 <td><xsl:value-of select="exam"/></td>
					<td><xsl:value-of select="pages"/></td>
					<td><xsl:value-of select="passing"/></td>
				</tr>
    </xsl:for-each>
  </table>
</body>
</html>
</xsl:template>
</xsl:stylesheet>