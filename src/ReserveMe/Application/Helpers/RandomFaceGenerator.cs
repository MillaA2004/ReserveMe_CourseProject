namespace Application.Helpers
{
	public class RandomFaceGenerator
	{
		private static readonly HttpClient httpClient = new HttpClient();
		private const string ExternalImageProviderUrl = "https://thispersondoesnotexist.com";
		private const string ImageFolder = "profiles\\";
		private static string _fullPath = string.Empty;

		public static async Task<byte[]> GetRandomFaceAsync(string envPath)
		{
			_fullPath = Path.Combine(envPath, ImageFolder);
			if (!Directory.Exists(_fullPath))
			{
				Directory.CreateDirectory(_fullPath);
			}

			var files = Directory.GetFiles(_fullPath);
			if (files.Length > 25) //when we have 25 images we will stop generating new ones.
			{
				var randomFile = files[new Random().Next(files.Length)];
				return await File.ReadAllBytesAsync(randomFile);
			}

			return await FetchAndStoreRandomFaceAsync();
		}

		private static async Task<byte[]> FetchAndStoreRandomFaceAsync()
		{
			HttpResponseMessage response = await httpClient.GetAsync(ExternalImageProviderUrl);

			if (response.IsSuccessStatusCode)
			{
				var imageData = await response.Content.ReadAsByteArrayAsync();
				var fileName = Path.Combine(_fullPath, $"{Guid.NewGuid()}.png");
				await File.WriteAllBytesAsync(fileName, imageData);
				return imageData;
			}

			throw new Exception("Unable to fetch random face image.");
		}
	}
}
