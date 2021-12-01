using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppPublisher
{
	public class Version
	{
		public decimal VersionNumber { get; set; }
		public string Description { get; set; }
		public DateTime ReleasedAt { get; set; }
		public string FilesLocation { get; set; }


		public async static Task<Version> Load()
		{
			using(var client = new HttpClient { BaseAddress = new Uri("https://localhost:44341") })
			{
				var response = await client.GetAsync("/version");
				var content = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<Version>(content);
			}
		}
	}
}
