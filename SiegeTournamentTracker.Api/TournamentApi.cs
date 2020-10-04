using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiegeTournamentTracker.Api
{
    public interface ITournamentApi
    {
        Task<List<Match>> Matches();
        Task<List<Match>> Matches(string url);
    }

    public class TournamentApi : ITournamentApi
    {
        public const string BASE_URL = "https://liquipedia.net";

		public const string MATCHES_URL = BASE_URL + "/rainbowsix/Liquipedia:Upcoming_and_ongoing_matches";

        public Task<List<Match>> Matches()
        {
            return Matches(MATCHES_URL);
        }

        public async Task<List<Match>> Matches(string url)
        {
            var document = await LoadFromUrl(url);

            var tables = document.DocumentNode.SelectNodes("//table");

            if (tables == null)
                return new List<Match>();

            return Matches(tables).ToList();
        }

        public IEnumerable<Match> Matches(HtmlNodeCollection tables)
        {
            foreach (var table in tables)
            {
                if (!table.Attributes["class"].Value.Contains("infobox_matches_content"))
                    continue;

                var singleDoc = LoadFromString(table.InnerHtml);

                var match = new Match
                {
                    TeamOne = GetTeam(singleDoc),
                    TeamTwo = GetTeam(singleDoc, false),
                    League = League(singleDoc)
                };

                AddTimestamp(singleDoc, match);
                AddScore(singleDoc, match);

                yield return match;
            }
        }

        public void AddTimestamp(HtmlDocument document, Match match)
        {
            var start = "//td[@class='match-filler']/span[@class='match-countdown']/span";

            var item = document.DocumentNode.SelectSingleNode(start);

            var time = item?.Attributes["data-timestamp"]?.Value;
            match.Offset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(time));
        }

        public void AddScore(HtmlDocument document, Match match)
        {
            var start = "//td[@class='versus']";

            var text = document.DocumentNode.SelectSingleNode(start).InnerText.Trim();
            if (text.ToLower().Contains("(bo"))
			{
                var parts = text.ToLower().Split(new[] { "(bo" }, StringSplitOptions.RemoveEmptyEntries);
                var num = parts[1].Split(')')[0];

                text = text
                    .Replace("(Bo" + num + ")", "")
                    .Replace("(bo" + num + ")", "")
                    .Replace("(BO" + num + ")", "");

                if (int.TryParse(num, out int bestOf))
                    match.BestOf = bestOf;
                else
                    match.BestOf = 1;
			}

            if (text == "vs." ||
                text == "vs")
            {
                match.TeamOneScore = null;
                match.TeamTwoScore = null;
                return;
            }

            var args = text.Split(':');
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid score: " + text);
                return;
            }


            if (!int.TryParse(args[0], out int s1))
            {
                Console.WriteLine("Invalid Team One score: " + args[0]);
                return;
            }

            if (!int.TryParse(args[1], out int s2))
            {
                Console.WriteLine("Invalid Team Two score: " + args[1]);
                return;
            }

            match.TeamOneScore = s1;
            match.TeamTwoScore = s2;
        }

        public LinkItem League(HtmlDocument document)
        {
            try
            {
                var imageLinkStr = "//td[@class='match-filler']/div/span/a/img";
                var image = document.DocumentNode.SelectSingleNode(imageLinkStr);

                var linkStr = "//td[@class='match-filler']/div/div/a";
                var link = document.DocumentNode.SelectSingleNode(linkStr);

                var li = new LinkItem();

                if (image != null)
                    foreach (var attribute in image.Attributes)
                        if (attribute.Name == "src")
                            li.ImageUrl = AppendLink(attribute.Value.Trim());

                li.Url = AppendLink(link.Attributes["href"].Value.Trim());
                li.FullName = link.Attributes["title"].Value.Trim();
                li.Name = link.InnerText.Trim();

                return li;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public LinkItem GetTeam(HtmlDocument document, bool teamOne = true)
        {
            var start = teamOne ? "//td[@class='team-left']" : "//td[@class='team-right']";
            return new LinkItem
            {
                Url = AppendLink(document.DocumentNode.SelectSingleNode(start + "/span/span[@class='team-template-text']/a")?.Attributes["href"]?.Value?.Trim()),
                FullName = document.DocumentNode.SelectSingleNode(start + "/span/span[@class='team-template-text']/a")?.Attributes["title"]?.Value?.Replace("(page does not exist)", "")?.Trim(),
                Name = document.DocumentNode.SelectSingleNode(start)?.InnerText?.Trim(),
                ImageUrl = AppendLink(document.DocumentNode.SelectSingleNode(start + "/span/span[@class='team-template-image']/a/img")?.Attributes["src"]?.Value?.Trim())
            };
        }

        public string AppendLink(string url)
		{
            if (string.IsNullOrEmpty(url))
                return null;

            if (url.Contains("://"))
                return url;

            return BASE_URL + url;
		}

        public Task<HtmlDocument> LoadFromUrl(string url)
        {
            var web = new HtmlWeb();
            return web.LoadFromWebAsync(url);
        }

        public HtmlDocument LoadFromString(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
    }
}
