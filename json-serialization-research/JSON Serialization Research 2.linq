<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var blah = JsonConvert.SerializeObject("hello world");
	blah.Dump();
	JsonConvert.DeserializeObject<string>(blah).Dump();
	//JsonConvert.DeserializeObject<mike>("{\"hello\":\"world\"}").Dump();

	JsonConvert.DeserializeObject<string>("\"hello world\"").Dump();

	var blah2 = JsonConvert.SerializeObject(new mike { hello = "world" });
	blah2.Dump();
	JsonConvert.DeserializeObject(blah2).Dump();
}

// Define other methods, classes and namespaces here
class mike
{
	public string hello {get;set;}
}