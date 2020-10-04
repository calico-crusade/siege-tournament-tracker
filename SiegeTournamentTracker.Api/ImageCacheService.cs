using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;

namespace SiegeTournamentTracker.Api
{
	public interface IImageCacheService
	{
		Task<string> GetImageFile(string url);
	}

	public class ImageCacheService : IImageCacheService
	{
		private const string IMAGE_CACHE_FOLDER = "image_cache";

		public string GetImageCacheFolder()
		{
			var cur = Environment.CurrentDirectory;
			var path = Path.Combine(cur, IMAGE_CACHE_FOLDER);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}

		public string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (var md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}

		public string ImagePath(string url)
		{
			var file = CreateMD5(url) + ".png";
			return Path.Combine(GetImageCacheFolder(), file);
		}

		public async Task<string> GetImageFile(string url)
		{
			var filename = ImagePath(url);
			if (File.Exists(filename))
				return filename;


			using (var ws = new WebClient())
			{
				await ws.DownloadFileTaskAsync(url, filename);
			}

			return filename;
		}
	}
}
