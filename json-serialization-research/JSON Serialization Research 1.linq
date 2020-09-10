<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

//JsonConvert.DeserializeObject<dynamic>("{\"hello\":\"world\"}").Dump();
JsonConvert.DeserializeObject<string>("\"hello world\"").Dump();
"\"{\"hello\":\"world\"}\"".Dump();
JsonConvert.DeserializeObject<string>("\"{\"hello\":\"world\"}\"").Dump();