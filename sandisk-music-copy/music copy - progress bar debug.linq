<Query Kind="Statements">
  <Connection>
    <ID>b7928571-9e20-4181-a79a-17f0beb245e2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>DigaSign</Database>
    <DisplayName>Local SSS</DisplayName>
    <MapXmlToString>true</MapXmlToString>
    <DriverData />
  </Connection>
  <Output>DataGrids</Output>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var progressBar = new Util.ProgressBar().Dump();
var items = Enumerable.Range(0, 10);
Parallel.ForEach(items, async item => 
{
	for (var j = 0; j <= 64; j++)
	{
		await Task.Delay(10);
		//progressBar.Fraction = (double)j / 64.0;
		progressBar.Percent = (int) ((double)j / 64.0) * 100;
	}
});

//for (int i = 0; i < 10; i++)
//{
//	var progressBar = new Util.ProgressBar(i.ToString()).Dump();
//	Task.Run(async () =>
//   {
//	   for (var j = 0; j <= 64; j++)
//	   {
//		   await Task.Delay(10);
//		   progressBar.Fraction = (double)j / 64.0;
//	   }
//   });
//}