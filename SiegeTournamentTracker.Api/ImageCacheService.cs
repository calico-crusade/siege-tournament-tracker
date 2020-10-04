using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;

namespace SiegeTournamentTracker.Api
{
	/// <summary>
	/// Used for caching images so as not to spam liquipedia for requests.
	/// Images are stored in the <see cref="IMAGE_CACHE_FOLDER"/> folder
	/// </summary>
	public interface IImageCacheService
	{
		/// <summary>
		/// Fetches the given image from either the cache or from the given URL
		/// </summary>
		/// <param name="url">The url to fetch the image from</param>
		/// <returns>The file path to where the image was cached</returns>
		Task<string> GetImageFile(string url);
	}

	/// <summary>
	/// Used for caching images so as not to spam liquipedia for requests.
	/// Images are stored in the <see cref="IMAGE_CACHE_FOLDER"/> folder
	/// </summary>
	public class ImageCacheService : IImageCacheService
	{
		/// <summary>
		/// The directory to use for storing the cached images
		/// </summary>
		private const string IMAGE_CACHE_FOLDER = "image_cache";

		/// <summary>
		/// Gets the image cache folder, and ensures it exists
		/// </summary>
		/// <returns>The image cache folder</returns>
		public string GetImageCacheFolder()
		{
			var cur = Environment.CurrentDirectory;
			var path = Path.Combine(cur, IMAGE_CACHE_FOLDER);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}

		/// <summary>
		/// Creates an MD5 hash of the given string.
		/// </summary>
		/// <param name="input">The string to hash</param>
		/// <returns>The MD5 hash</returns>
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

		/// <summary>
		/// Gets the file path for the given image URL
		/// </summary>
		/// <param name="url">The URL for the image</param>
		/// <returns>The image file path</returns>
		public string ImagePath(string url)
		{
			var file = CreateMD5(url) + ".png";
			return Path.Combine(GetImageCacheFolder(), file);
		}

		/// <summary>
		/// Fetches the given image from either the cache or from the given URL
		/// </summary>
		/// <param name="url">The url to fetch the image from</param>
		/// <returns>The file path to where the image was cached</returns>
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
