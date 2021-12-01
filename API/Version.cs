using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
	public class Version
	{
		public float VersionNumber { get; set; }
		public string Description { get; set; }
		public DateTime ReleasedAt { get; set; }
		public string FilesLocation { get; set; }


		public static Version LoadLatest()
		{
			using (var stream = File.Open("latestversion.json", FileMode.Open))
			{
				StreamReader sr = new StreamReader(stream);
				var json = sr.ReadToEnd();
				
				return JsonSerializer.Deserialize<Version>(json);
			}
		}

		public static void SaveLatest(Version version)
		{
			using(var stream = File.Open("latestversion.json", FileMode.Create))
			{
				var json = JsonSerializer.Serialize(version);

				var sw = new StreamWriter(stream);
				sw.WriteLine(json);
				sw.Close();
			}
		}
	}
}
