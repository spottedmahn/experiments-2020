<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>ID3</NuGetReference>
  <Namespace>Id3</Namespace>
  <Namespace>Id3.Frames</Namespace>
</Query>

var doesntHave = @"C:\Users\mdepouw\OneDrive\Music\_me music attempt 2\Trick Daddy\Thug Matrimony Married to the Streets~Rap~2004\08~U Neva Know.mp3";
var doesHave = @"C:\Users\mdepouw\OneDrive\Music\_me music attempt 2\Trick Daddy\Thug Holiday~Rap~2002\07~Play No Games.mp3";

using (var mp3 = new Mp3(doesHave))
using (var mp32 = new Mp3(doesntHave))
{
	Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
	Id3Tag tag2 = mp32.GetTag(Id3TagFamily.Version2X);
	//if (!tag.Year.HasValue)
	//	continue;
	//if (tag.Year >= 1980 && tag.Year < 1990)
	//	yield return mp3FilePath;
	var hasFrontCover = tag.Pictures.Any(p => p.PictureType == PictureType.FrontCover);
	var hasFrontCover2 = tag2.Pictures.Any(p => p.PictureType == PictureType.FrontCover);
}