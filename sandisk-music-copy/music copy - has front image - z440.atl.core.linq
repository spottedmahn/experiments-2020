<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>z440.atl.core</NuGetReference>
  <Namespace>ATL</Namespace>
</Query>

var doesntHave = @"C:\Users\mdepouw\OneDrive\Music\_me music attempt 2\Trick Daddy\Thug Matrimony Married to the Streets~Rap~2004\08~U Neva Know.mp3";
var doesHave = @"C:\Users\mdepouw\OneDrive\Music\_me music attempt 2\Trick Daddy\Thug Holiday~Rap~2002\07~Play No Games.mp3";
var m4aTest = @"C:\Users\mdepouw\OneDrive\Music\_me music attempt 2\Yo Gotti\I Am\01 I Am.m4a";
m4aTest = @"C:\Users\mdepouw\OneDrive\Music\DJ Freckles\DJ_Freckles_Da_New_Shit_Vol_9\07_50_cent-06-round_here-whoa_$.mp3";
// Load audio file information into memory
Track theTrack = new Track(m4aTest);

//PictureInfo.PIC_TYPE.Generic
//PictureInfo.PIC_TYPE.Front

// Get picture list
System.Collections.Generic.IList<PictureInfo> embeddedPictures = theTrack.EmbeddedPictures;