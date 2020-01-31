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

async Task Main()
{
	//get json
	var notificationsJson = await GetNotificationsJson();

	//convert?

	//dump
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
		httpClient.DefaultRequestHeaders.Authorization
			= new AuthenticationHeaderValue("Basic", base64EncodedUsername);
		httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("Mike D's LinqPad Query");

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

		var jArray = JArray.Parse(responseString);
		
		List<Notification> notifications = new List<UserQuery.Notification>();
		
		//todo handle PRs
		//temp filter out prs
		var noPrs = jArray.Where(a => a["subject"]["type"].ToString() != "PullRequest");
		
		foreach (var notificationJson in noPrs)
		{
			var latestCommentUrl = notificationJson["subject"]["latest_comment_url"].ToString();
			var commentResponse = await httpClient.GetStringAsync(latestCommentUrl);
			//commentResponse.Dump("blah");
			var commentJObject = JObject.Parse(commentResponse);
			var notification =  new Notification
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
		//jArray[0]["subject"].Dump();
//		var notifications2 = jArray
//			.Select(async a =>
//			{
//				using (var httpClient2 = new HttpClient())
//				{
//					httpClient2.DefaultRequestHeaders.Authorization
//						= new AuthenticationHeaderValue("Basic", base64EncodedUsername);
//					httpClient2.DefaultRequestHeaders.UserAgent.TryParseAdd("Mike D's LinqPad Query");
//
//					var latestCommentUrl = a["subject"]["latest_comment_url"].ToString();
//					var commentResponse = await httpClient2.GetStringAsync(latestCommentUrl);
//					commentResponse.Dump("blah");
//					return new Notification
//					{
//						Title = new Hyperlinq(a["subject"]["latest_comment_url"].ToString(), a["subject"]["title"].ToString()),
//						Url = a["subject"]["url"].ToString(),
//						LatestCommentUrl = a["subject"]["latest_comment_url"].ToString(),
//						Unread = (bool)a["unread"],
//						UpdatedAt = (DateTime)a["updated_at"],
//						Type = a["subject"]["type"].ToString()
//					};
//				}
//			});
		notifications.Dump();
		//url.Dump();
		return await Task.FromResult("");

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