<Query Kind="Statements">
  <Output>DataGrids</Output>
  <Namespace>System.Windows</Namespace>
</Query>

//Clipboard.SetData(DataFormats.Html, File.ReadAllText(@"C:\_temp\MS teams html clipboard.txt"));
//Clipboard.SetData(DataFormats.Html, "<a href=\"https://google.com\">Test</a>");

//var blah = Clipboard.GetDataObject();
//blah.SetData("DropDescription", blah.GetData("DropDescription"));

var textToDisplay = "Test";
var url = "https://stackoverflow.com/";
var arbitraryText = "Mike D.";
var dataObject = new DataObject();
//dataObject.SetData(DataFormats.Html, File.ReadAllText(@"C:\_temp\MS teams html clipboard 4.txt"));
//to my surprise, the Fragment comments ARE required
dataObject.SetData(DataFormats.Html, @$"<html><body>
	<!--StartFragment-->
	<a href=""{url}"">{textToDisplay}</a>
	<!--EndFragment-->
	</body></html>");
//have to set the Text format too otherwise it won't work
dataObject.SetData(DataFormats.Text, arbitraryText);
Clipboard.SetDataObject(dataObject);