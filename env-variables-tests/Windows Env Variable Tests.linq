<Query Kind="Statements">
  <Output>DataGrids</Output>
</Query>

Environment.GetEnvironmentVariable("Space Test").Dump(); //should return Blah
Environment.GetEnvironmentVariable("Emoji Test 😜").Dump(); //should return Yeah
//Environment.GetEnvironmentVariable("OneDrivE").Dump();