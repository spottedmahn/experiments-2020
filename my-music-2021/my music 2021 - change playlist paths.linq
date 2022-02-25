<Query Kind="Program">
  <Connection>
    <ID>12f5fe67-24c2-46ce-9ec6-95ff2ade3f31</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Server>10.0.3.34,1601</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>DigaSignSupport</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAARTHN+mqS5ku41TtT+eHMIwAAAAACAAAAAAADZgAAwAAAABAAAAB4+n+nxaUPly2QXxzO5u87AAAAAASAAACgAAAAEAAAAErw9eTvmbzH1KhYIEU+upQYAAAAmK+lTDxbasDS7yY3XH1Bl7/eAakyv+urFAAAAMpS/CpNUBnD2mzhXShfW5Fa8ZrT</Password>
    <Database>DigaSign</Database>
    <MapXmlToString>true</MapXmlToString>
    <DriverData />
  </Connection>
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var m3usRootPath = @"C:\Users\mdepouw\OneDrive\Music\My Music\_Playlists";
	var oldRootPath = @"C:\Users\mdepouw\OneDrive\Music\_me music attempt 2";
	var newRootPath = @"C:\Users\mdepouw\OneDrive\Music\My Music";
	
	Parallel.ForEach(Directory.GetFiles(m3usRootPath, "*.m3u").Take(1), m3u =>
	{
		var contents = File.ReadLines(m3u);
		var updatedPaths = contents.Select(c => c.Replace(oldRootPath, newRootPath));
		File.WriteAllLines(m3u, updatedPaths);
	});
}
