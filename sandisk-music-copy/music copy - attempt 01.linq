<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>ID3</NuGetReference>
  <NuGetReference>System.Text.Encoding.CodePages</NuGetReference>
  <NuGetReference>z440.atl.core</NuGetReference>
  <Namespace>ATL</Namespace>
  <Namespace>Id3</Namespace>
  <Namespace>Id3.Frames</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//todo's
//check for image
//check for image resolution too big; DMX, Plies (800 x 800 to 600 x 600)
//improve filenames

bool logMusicFileMissing = true;
bool logCopyFileDetails = false;
bool checkForHasFrontAlbumPic = true;
bool overwriteMp3s = false;
bool logPicAdded = true;

void Main()
{
	//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
	
	var playlistDir = @"C:\Users\mdepouw\OneDrive\Music\My Music\_Playlists";
	var playlistNames = new List<string>
	{
		"2021-09 DJ Freckles.m3u",
		"2018-05 - Adele, Biggie, Kendrick.m3u",
		"2018-06 - Juanes.m3u",
		"Best of All Time.m3u",
		"2021-11.m3u",
		"2019-10.m3u",
		"2018 - DJ Freckles - I'm Back.m3u",
		"2018-06 - Some Rap.m3u",
		"2018-08.m3u",
		//"2019-12 3 stars and up.m3u",
		"2020-03 TI.m3u",
		"Kids 2019.m3u",
		"2018-07.m3u",
		"2018-09 - Lil Boosie.m3u",
		"2020-03 TI.m3u",
		"2020-06.m3u",
	};
	//playlistNames = new List<string>()
	//{
	//	//"2018-06 - Some Rap.m3u",
	//	//"2021-11.m3u",
	//	//"2018-05 - Adele, Biggie, Kendrick.m3u",
	//	//"2018-08.m3u",
	//	//"2021-09 DJ Freckles.m3u",
	//	//"2019-10.m3u",
	//	"Kids 2019.m3u",
	//};
	
	var totalPlaylists = playlistNames.Count();
	var countPlaylist = 0;

	var pb = new Util.ProgressBar("Playlist Progress");
	pb.Dump();
	
	Parallel.ForEach(playlistNames,
		new ParallelOptions { MaxDegreeOfParallelism = 2 },
		playlistName =>
		{
			CopyPlaylist(playlistDir, playlistName, overwriteMp3s);	
			Interlocked.Increment(ref countPlaylist);
			pb.Fraction = (double) countPlaylist / totalPlaylists;
		});
}

void CopyPlaylist(string playlistDir, string playlistName, bool overwriteMp3s)
{
	//read m3u file
	var playlistM3U = ReadPlaylistM3U(playlistDir, playlistName);

	var sandiskMusicDir = @"D:\Music\";
	var sandiskPlaylistDir = @"D:\Playlists";

	//create playlist on sandisk
	//with sandisk dir structure
	var albumDir = Path.Combine(sandiskMusicDir, Path.GetFileNameWithoutExtension(playlistName));
	var sandiskM3U = ConvertToSandiskDirStructure(playlistM3U, albumDir);

	var sandiskM3uFilePath = WritePlaylistM3U(sandiskPlaylistDir, sandiskM3U);
	CopyMp3s(sandiskM3U, sandiskMusicDir, overwriteMp3s);
}

PlayListM3U ReadPlaylistM3U(string playlistDir, string playlistName)
{
	return ReadPlaylistM3UFull($@"{playlistDir}\{playlistName}");
}

PlayListM3U ReadPlaylistM3UFull(string m3uFullPath)
{
	if(!File.Exists(m3uFullPath))
	{
		throw new Exception($"can file playlist file to read: '{m3uFullPath}'");		
	}
	
	var result = new PlayListM3U
	{
		Name = Path.GetFileNameWithoutExtension(m3uFullPath),
		//read ANSI file? 🤷♂️
		//https://stackoverflow.com/a/66947109/185123
		//example album that was a problem
		//var mike = "Un Día Normal";
		//Mp3FilePaths = File.ReadAllLines(m3uFullPath, Encoding.UTF8).ToList(),
		//File.ReadAllLines(m3uFullPath, Encoding.GetEncoding("ISO-8859-1")).ToList(),
		Mp3FilePaths = File.ReadAllLines(m3uFullPath, CodePagesEncodingProvider.Instance.GetEncoding(1252)).ToList(),
		M3uFullPath = m3uFullPath
	};
	
	//var debug = File.ReadAllLines(m3uFullPath, Encoding.UTF8).ToList();
	
	return result;
}

HistoryPlayListM3U ConvertToSandiskDirStructure(PlayListM3U playlistM3U, string sandiskMusicDir)
{
	#region notes
	//apparently I have duplicates in my m3u files?
	//C:\Users\mdepouw\OneDrive\Music\_me music attempt 2\Biggie\Biggie~Life After Death~Rap~1997~1 of 2~Reg\05~Fuck You Tonight.mp3
	//C: \Users\mdepouw\OneDrive\Music\_me music attempt 2\Biggie\Biggie~Life After Death~Rap~1997~2 of 2~Reg\05~Fuck You Tonight.mp3
	//but they don't show up in Foobar2000 like this 🤷♂️
	//var mp3FilePathsThatExist = new ConcurrentBag<string>();
	//Parallel.ForEach(playlistM3U.Mp3FilePaths,
	//	mfp =>
	//	{
	//		if(File.Exists(mfp))
	//		{
	//			mp3FilePathsThatExist.Add(mfp);
	//		}
	//	});
	//var mp3FilePathsThatExist = playlistM3U.Mp3FilePaths
	//	.Select(mfp => new { mfp, exists = File.Exists(mfp)})
	//	.Where(mfp => mfp.exists)
	//	.Select(mfp => mfp.mfp)
	//	.ToList();
	//var debug = playlistM3U.Mp3FilePaths.Where(mfp => mfp.ToUpper().Contains("die"))
	//.ToList();
	#endregion
	
	CheckForDuplicates(playlistM3U);

	return new HistoryPlayListM3U
	{
		Name = playlistM3U.Name,
		Mp3FilePaths = playlistM3U.Mp3FilePaths.Select(mfp => 
		    ConvertMp3FilePathToSandiskMusicDir(mfp, sandiskMusicDir))
			.ToList(),
		OriginalFilePaths = playlistM3U.Mp3FilePaths.ToDictionary(mfp => GetDictionaryKey(mfp)
		   , mfp => mfp),
		M3uFullPath = playlistM3U.M3uFullPath
	};
}

void CheckForDuplicates(PlayListM3U playlistM3U)
{
	var duplicateMp3FileNames = playlistM3U.Mp3FilePaths.GroupBy(mfp => GetDictionaryKey(mfp))
			.Where(g => g.Count() > 1)
			.SelectMany(g => g)
			.Select(mfp => new { mfp, key = GetDictionaryKey(mfp) })
			.OrderBy(r => GetDictionaryKey(r.mfp))
			.ToList();
	if (duplicateMp3FileNames.Any())
	{
		throw new Exception("duplicate filenames; use debugger");
	}
}

string GetDictionaryKey(string mfp)
{
	return ShortenSandiskFileName($"{Directory.GetParent(mfp).Name}-{Path.GetFileName(mfp)}");
}

string GetDictionaryKeyFromSanDiskFileName(string mp3filePath)
{
	return Path.GetFileName(mp3filePath);
}

string ConvertMp3FilePathToSandiskMusicDir(string filePath, string sandiskMusicDir)
{
	return Path.Combine(sandiskMusicDir,
		ShortenSandiskFileName($"{Directory.GetParent(filePath).Name}-{Path.GetFileName(filePath)}"));
}

string WritePlaylistM3U(string sandiskPlaylistDir, PlayListM3U sandiskM3U)
{
	var fullPath = Path.Combine(sandiskPlaylistDir, $"{sandiskM3U.Name}.m3u");
	//turn off since I added progress indicators
	//Console.WriteLine($"Overwriting or creating m3u / playlist to Sandisk MP3 player: '{sandiskM3U.Name}'");
	File.WriteAllLines(fullPath, sandiskM3U.Mp3FilePaths);
	return fullPath;
}

void CopyMp3s(HistoryPlayListM3U historyPlayListM3U, string sandiskMusicPath, bool overwriteFile = false)
{
	CreateDirIfNeeded(historyPlayListM3U, sandiskMusicPath);

	var totalFiles = historyPlayListM3U.Mp3FilePaths.Count;
	var countFiles = 0;

	var pb = new Util.ProgressBar($"Playlist: '{historyPlayListM3U.Name}' Progress");
	pb.Dump();

	Parallel.ForEach(historyPlayListM3U.Mp3FilePaths,
		new ParallelOptions { MaxDegreeOfParallelism = 3 },
		(mp3filePath) =>
		{
			var copyMp3Result = CopyMp3(mp3filePath, historyPlayListM3U, overwriteFile);
			Interlocked.Increment(ref countFiles);
			pb.Fraction = (double) countFiles / totalFiles;
		});
	//Console.WriteLine($"DBG {historyPlayListM3U.Name} - {pb.Percent}");
	
	if(logCopyFileDetails)
	{
		Console.Write("Done copying file(s)");
	}
}

void CreateDirIfNeeded(HistoryPlayListM3U historyPlayListM3U, string sandiskMusicPath)
{
	var dirPath = Path.Combine(sandiskMusicPath, historyPlayListM3U.Name);
	if (!Directory.Exists(dirPath))
	{
		Directory.CreateDirectory(dirPath);
	}
}

CopyMp3Result CopyMp3(string destinationMp3filePath, HistoryPlayListM3U historyPlayListM3U, bool overwriteFile = false)
{
	var key = GetDictionaryKeyFromSanDiskFileName(destinationMp3filePath);
	var sourceMp3FilePath = historyPlayListM3U.OriginalFilePaths[key];

	if (!File.Exists(sourceMp3FilePath))
	{
		if (logMusicFileMissing)
		{
			Console.WriteLine($"WRN - MP3 file on playlist: '{historyPlayListM3U.Name}' doesn't exist: '{sourceMp3FilePath}'");
		}
		return CopyMp3Result.NotCopied;
	}

	if (checkForHasFrontAlbumPic)
	{
		var hasFrontAlbumPic = HasFrontAlbumPic(sourceMp3FilePath);
		if(!hasFrontAlbumPic)
		{
			var attached = AttachFrontAlbumPic(sourceMp3FilePath);
			if (!attached)
			{
				Console.WriteLine($@"mp3/m4a missing front album or generic cover: '{Directory.GetParent(sourceMp3FilePath).Parent.Name}'\'{Directory.GetParent(sourceMp3FilePath).Name}'\'{Path.GetFileNameWithoutExtension(sourceMp3FilePath)}'");
			}
		}
	}
	
	if(File.Exists(destinationMp3filePath)
		&& !overwriteFile)
	{
		return CopyMp3Result.NotCopied;
	}

	if (logCopyFileDetails)
	{
		Console.WriteLine($"copying: '{Path.GetFileNameWithoutExtension(destinationMp3filePath)}'");
	}
	
	//"The cloud operation was not completed before the time-out period expired
	//. : 'D:\Music\2019-12 3 stars and up\Complete Motown Singles The, Volume 02 1962-(Disc 4) 17 - Shake Sherrie (Single Version)-NASP-02.mp3'"
	//now I see it, these files aren't downloaded
	File.Copy(sourceMp3FilePath, destinationMp3filePath, overwriteFile);
	return CopyMp3Result.Copied;
}

bool AttachFrontAlbumPic(string musicfilePath)
{
	var hasFrontAlbumPic = HasFrontAlbumPic(musicfilePath);
	if(hasFrontAlbumPic)
	{
		return false;
	}
	var imagePath = Path.Combine(Path.GetDirectoryName(musicfilePath), "Folder.jpg");
	if(!File.Exists(imagePath))
	{
		Console.WriteLine($"can't find 'folder.jpg' to attach to: {musicfilePath}");
		return false;
	}
	
	var track = new Track(musicfilePath);
	// Add 'CD' embedded picture
	var newPicture = PictureInfo.fromBinaryData(File.ReadAllBytes(imagePath), PictureInfo.PIC_TYPE.Front);
	track.EmbeddedPictures.Add(newPicture);

	// Save modifications on the disc
	var saveResult = track.Save();
	if(logPicAdded)
	{
		Console.WriteLine($"DBG - pic added to: '{Directory.GetParent(musicfilePath).Parent.Name}'\'{Directory.GetParent(musicfilePath).Name}'\'{Path.GetFileNameWithoutExtension(musicfilePath)}'");
	}
	return saveResult;
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

enum CopyMp3Result
{
	NotCopied,
	Copied
}

/// <summary>workaround for filename lengths
/// D:\Music\2018 - 06 - Some Rap\Reg - 16~A Real Father.mp3
/// D:\Music\2018 - 06 - Some Rap\Reg - 18~I Pray.mp3
/// D:\Music\2018 - 06 - Some Rap\Round One -The Album~Rap~2002~1 of 1~Reg - 16~A Real Father.mp3
/// D:\Music\2018 - 06 - Some Rap\Round One -The Album~Rap~2002~1 of 1~Reg - 18~I Pray.mp3
/// the longer version is recognizing them as the same file in 
/// a playlist.
/// todo - try adding those files to a playlist
/// via their UI and inspect the playlist
/// </summary>
string ShortenSandiskFileName(string filename)
{
	//28 is a "random" guess, no clue
	//what the real limit is
	return Right(filename, 32);
}

static string Right(string sValue, int iMaxLength)
{
	//Check if the value is valid
	if (string.IsNullOrEmpty(sValue))
	{
		//Set valid empty string as string could be null
		sValue = string.Empty;
	}
	else if (sValue.Length > iMaxLength)
	{
		//Make the string no longer than the max length
		sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
	}

	//Return the string
	return sValue;
}

public class PlayListM3U
{
	public string Name { get; set; }
	public List<string> Mp3FilePaths { get; set; }
	public string M3uFullPath { get;set; }
}

public class HistoryPlayListM3U : PlayListM3U
{
	public Dictionary<string, string> OriginalFilePaths { get; set; }
}