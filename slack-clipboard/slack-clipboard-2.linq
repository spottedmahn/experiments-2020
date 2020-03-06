<Query Kind="Statements">
  <Output>DataGrids</Output>
  <Namespace>System.Windows</Namespace>
</Query>

//Clipboard.SetData(DataFormats.Html, File.ReadAllText(@"C:\_temp\MS teams html clipboard.txt"));

var blah = Clipboard.GetDataObject();
blah.GetFormats().Dump();
blah.GetData(DataFormats.Html).Dump("HTML");
blah.GetData(DataFormats.Text).Dump("Text");
blah.GetData(DataFormats.Locale).Dump("Locale");
blah.GetData(DataFormats.UnicodeText).Dump("Unicode Text");
blah.GetData("System.String").Dump("System.String");
blah.GetData("ExcludeClipboardContentFromMonitorProcessing").Dump("Exlcude");
blah.GetData("ClipboardHistoryItemId").Dump("Clipboard History Item Id");
blah.GetData("DropDescription").Dump("DropDescription");

