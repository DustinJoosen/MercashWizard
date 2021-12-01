using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	public class VersionController : ControllerBase
	{

		public Version Get()
		{
			return Version.LoadLatest();
		}

		[HttpPost]
		public Version Save(Version version)
		{
			Version.SaveLatest(version);
			return version;
		}
	}
}
