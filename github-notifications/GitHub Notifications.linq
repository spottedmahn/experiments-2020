<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

DateTime sinceDateTime = new DateTime(2020, 1, 20);
string base64EncodedUsername = Environment.GetEnvironmentVariable("GitHub Personal Access Token")
  ?? throw new Exception("Unable to get a GitHub Personal Access Token from ENV Variables");
Dictionary<string, string> detailsCache;

async Task Main()
{
	//prime cache
	PrimeDetailsCache();

	//get json
	var notificationsJson = await GetNotificationsJson();

	//get issue links for each notification
	var convertedNotifications = await GetNotifications(notificationsJson);

	//dump
	convertedNotifications.Dump();

	PersistDetailsCache();
}

async Task<List<Notification>> GetNotifications(string notificationsJson)
{
	using (var httpClient = new HttpClient())
	{
		PrimeHttpClient(httpClient);

		var jArray = JArray.Parse(notificationsJson);
		//jArray[0]["subject"].Dump();

		var notifications = new List<UserQuery.Notification>();

		//todo handle PRs
		//temp filter out prs
		var noPrs = jArray.Where(a => a["subject"]["type"].ToString() != "PullRequest");

		foreach (var notificationJson in noPrs)
		{
			var latestCommentUrl = notificationJson["subject"]["latest_comment_url"].ToString();

			string commentResponse = null;

			commentResponse = detailsCache.GetValueOrDefault(latestCommentUrl);

			if (commentResponse == null)
			{
				commentResponse = await httpClient.GetStringAsync(latestCommentUrl);
				detailsCache[latestCommentUrl] = commentResponse;
			}

			//commentResponse.Dump("blah");
			var commentJObject = JObject.Parse(commentResponse);
			var notification = new Notification
			{
				Title = new Hyperlinq(commentJObject["html_url"].ToString(), notificationJson["subject"]["title"].ToString()),
				Url = notificationJson["subject"]["url"].ToString(),
				LatestCommentUrl = notificationJson["subject"]["latest_comment_url"].ToString(),
				Unread = (bool)notificationJson["unread"],
				UpdatedAt = (DateTime)notificationJson["updated_at"],
				Type = notificationJson["subject"]["type"].ToString(),
				Repo = notificationJson["repository"]["full_name"].ToString()
			};

			notifications.Add(notification);
		}

		//notifications.Dump();
		return notifications;
	}
}

class Notification
{
	public Hyperlinq Title { get; set; }
	public string Url { get; set; }
	public string LatestCommentUrl { get; set; }
	public bool Unread { get; set; }
	public DateTime UpdatedAt { get; set; }
	public string @Type { get; set; }
	public string Repo { get; set; }

	object ToDump() => new { Title, UpdatedAt, @Type, Unread, Repo };
}

async Task<string> GetNotificationsJson()
{
	//https://api.github.com/notifications?since=2020-01-07T08:00:00Z&all=true
	//http header Authorization Basic dXNlcm5hbWU6NWY2OGMzN2MzNjM5NmEzNTM5MjgxN2JhODcwNDI5MzI1ZjY5ODNkOQ
	//which is base64 encoding of personal access token
	//from fiddler
	//Authorization Header is present: Basic dXNlcm5hbWU6NWY2OGMzN2MzNjM5NmEzNTM5MjgxN2JhODcwNDI5MzI1ZjY5ODNkOQ==
	//Decoded Username:Password = username:5f68c37c36396a35392817ba870429325f6983d9

	//https://developer.github.com/v3/activity/notifications/#list-your-notifications

	//https://developer.github.com/v3/auth/#via-oauth-and-personal-access-tokens

	using (var httpClient = new HttpClient())
	{
		PrimeHttpClient(httpClient);

		//ISO 8601 
		//https://stackoverflow.com/a/115034/185123
		//since=2020-01-01T00:00:00
		var queryString = HttpUtility.ParseQueryString("");
		queryString["all"] = "true";
		queryString["since"] = $"{sinceDateTime:s}Z";

		//var encodedParams = HttpUtility.UrlEncode(queryString.ToString());
		//queryString.ToString().Dump();
		var decodedParams = HttpUtility.UrlDecode(queryString.ToString());
		//decodedParams.Dump();
		//todo why decode?
		//all=true&since=2020-01-25T00%3a00%3a00
		//all=true&since=2020-01-25T00:00:00

		var urlStart = "https://api.github.com/notifications";
		var url = $"{urlStart}?{decodedParams}";
		//url.Dump();

		var responseString = await httpClient.GetStringAsync(url);
		//responseString.Dump();

		return responseString;
	}
}

void PrimeHttpClient(HttpClient httpClient)
{
	SetHttpAuthToken(httpClient);
	SetHttpAgent(httpClient);
}

void SetHttpAuthToken(HttpClient httpClient)
{
	httpClient.DefaultRequestHeaders.Authorization
			= new AuthenticationHeaderValue("Basic", base64EncodedUsername);
}

void SetHttpAgent(HttpClient httpClient)
{
	httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("Mike D's LinqPad Query");
}

string detailsCachePath = Path.GetTempPath() + "\\GitHub Issue Details Cache.json";

void PrimeDetailsCache()
{
	if (File.Exists(detailsCachePath))
		detailsCache = JsonConvert.DeserializeObject<Dictionary<string, string>>
									 (File.ReadAllText(detailsCachePath));
	else
		detailsCache = new Dictionary<string, string>();
}

void PersistDetailsCache()
{
	var json = JsonConvert.SerializeObject(detailsCache);
	File.WriteAllText(detailsCachePath, json);
}

public static class MyClass
{
	//https://stackoverflow.com/a/538751/185123
	public static TValue GetValueOrDefault<TKey, TValue>
		(this IDictionary<TKey, TValue> dictionary, TKey key) =>
		dictionary.TryGetValue(key, out var ret) ? ret : default;
}