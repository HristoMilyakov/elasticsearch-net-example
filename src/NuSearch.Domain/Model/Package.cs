﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuSearch.Domain.Model
{
	public class Package
	{
		public Package()
		{

		}

		public Package(FeedPackage feed)
		{
			this.Id = feed.Id;
			this.Copyright = feed.Copyright;
			this.IconUrl = feed.IconUrl;
			this.Summary = feed.Summary;
			this.DownloadCount = feed.DownloadCount;
			this.Authors = feed.Authors.Split('|').Select(author => new PackageAuthor { Name = author }).ToList();
			this.Versions = new List<PackageVersion> { new PackageVersion(feed) };
			this.Suggest = new SuggestField
			{
				Input = new List<string>(feed.Id.Split('.')) { feed.Id },
				Output = feed.Id,
				Payload = new
				{
					id = feed.Id,
					downloadCount = feed.DownloadCount,
					summary = !string.IsNullOrEmpty(feed.Summary) ? string.Concat(feed.Summary.Take(200)) : string.Empty,
				},
				Weight = feed.DownloadCount
			};
		}

		public string Id { get; set; }
		public string IconUrl { get; set; }
		public string Summary { get; set; }
		public List<PackageAuthor> Authors { get; set; }
		public List<PackageVersion> Versions { get; set; }
		public string Copyright { get; set; }
		public int DownloadCount { get; set; }
		public SuggestField Suggest { get; set; }
	}
}
