<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>z440.atl.core</NuGetReference>
  <Namespace>ATL</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

bool logPicAdded = true;

void Main()
{
	var rootPath = @"C:\Users\mdepouw\OneDrive\Music\My Music";
	var missing = GetAllFilesMissingAPic(rootPath);
	var hasPicInFolders = HasPicInFolder(missing);
	AddPictureFromFolder(hasPicInFolders);
}

List<string> GetAllFilesMissingAPic(string rootPath)
{
	var result = new ConcurrentBag<string>();
	Parallel.ForEach(Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories)
		.Where(d => d.ToLower().EndsWith(".mp3")
			|| d.ToLower().EndsWith(".m4a"))
		.OrderBy(d => d),
		//.Take(600),
		filePath =>
		{
			var hasPic = HasFrontAlbumPic(filePath);
			if (!hasPic)
			{
				//Console.WriteLine(@$"missing a pic: '{Directory.GetParent(filePath).Parent.Name}\{Directory.GetParent(filePath).Name}\{Path.GetFileName(filePath)}'");
				result.Add(filePath);
			}
		});
	return result.ToList();
}

List<(string, bool)> HasPicInFolder(List<string> missing)
{
	//optimization
	//group by album & only check the disk once

	var result = new ConcurrentBag<(string, bool)>();
	Parallel.ForEach(missing, filePath =>
	{
		result.Add((filePath, HasPicAvailable(filePath)));
	});
	//result.OrderBy(r => r.Item1).Dump();
	return result.ToList();
}

bool HasPicAvailable(string filePath)
{
	return Directory.GetFiles(Path.GetDirectoryName(filePath))
		.Any(f => f.ToLower().EndsWith("folder.jpg"));
}

void AddPictureFromFolder(List<(string, bool)> hasPicInFolders)
{
	Parallel.ForEach(hasPicInFolders, hasPicInFolder =>
	{
		AddPictureFromFolderOnRecord(hasPicInFolder);
	});
}

void AddPictureFromFolderOnRecord((string, bool) hasPicInFolder)
{
	var musicfilePath = hasPicInFolder.Item1;
	var imagePath = Path.Combine(Path.GetDirectoryName(musicfilePath), "Folder.jpg");
	if (!File.Exists(imagePath))
	{
		Console.WriteLine($"can't find 'folder.jpg' to attach to: {musicfilePath}");
		return;
	}

	var track = new Track(musicfilePath);
	// Add 'CD' embedded picture
	var newPicture = PictureInfo.fromBinaryData(File.ReadAllBytes(imagePath), PictureInfo.PIC_TYPE.Front);
	track.EmbeddedPictures.Add(newPicture);

	var saveResult = track.Save();
	if (logPicAdded)
	{
		Console.WriteLine(@$"DBG - pic added to: '{Directory.GetParent(musicfilePath).Parent.Name}\{Directory.GetParent(musicfilePath).Name}\{Path.GetFileNameWithoutExtension(musicfilePath)}'");
	}
}

bool HasFrontAlbumPic(string musicfilePath)
{
	//using (var mp3 = new Mp3(mp3filePath))
	//{
	//	var tag = mp3.GetTag(Id3TagFamily.Version2X);
	//	return tag.Pictures.Any(p => p.PictureType == PictureType.FrontCover);
	//}
	var track = new Track(musicfilePath);
	return track.EmbeddedPictures.Any(ep => ep.PicType == ATL.PictureInfo.PIC_TYPE.Front
		|| ep.PicType == ATL.PictureInfo.PIC_TYPE.Generic);
}
