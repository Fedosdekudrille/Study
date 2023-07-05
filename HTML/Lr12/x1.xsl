<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
<html> 
<body>
	<h2>Список специальностей факультета</h2>
	<table border="1">
		<tr bgcolor="#9acd32">
			<th style="text-align:left">Имя</th>
			<th style="text-align:left">Фамилия</th>
			<th style="text-align:left">Профессия</th>
			<th style="text-align:left">Увлечения</th>
			<th style="text-align:left">Зарплата</th>
		</tr>
		<xsl:for-each select="faculty/specialization">
			<xsl:sort select="salary" order="descending"/>
				<tr>
					<td><xsl:value-of select="name"/></td>
					<td><xsl:value-of select="surname"/></td>
					<td><xsl:value-of select="profession"/></td>
					<td><xsl:value-of select="hobbies"/></td>
					<td><xsl:value-of select="salary"/></td>
				</tr>
    </xsl:for-each>
  </table>
</body>
</html>
</xsl:template>
</xsl:stylesheet>