using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Client
{
	class Version
	{
		public decimal VersionNumber { get; set; }
		public string Description { get; set; }
		public DateTime ReleasedAt { get; set; }
		public string FilesLocation { get; set; }


		public static Version Load()
		{
			var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "currentversion.json");
			return JsonSerializer.Deserialize<Version>(json);
		}

		public static void Save(Version version)
		{
			var json = JsonSerializer.Serialize(version);
			Save(json);
		}

		public static void Save(string version)
		{
			File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "currentversion.json", version);
		}

		public bool Equals(Version version)
		{
			return
				version.VersionNumber == this.VersionNumber &&
				version.ReleasedAt == this.ReleasedAt &&
				version.FilesLocation == this.FilesLocation &&
				version.Description == this.Description;
		}
	}
}
