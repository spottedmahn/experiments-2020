<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var blah2 = JsonConvert.SerializeObject(new mike { hello = "world" });
	blah2.Dump();
	JsonConvert.DeserializeObject(blah2).Dump();
}

// Define other methods, classes and namespaces here
class mike
{
	public string hello { get; set; }
}