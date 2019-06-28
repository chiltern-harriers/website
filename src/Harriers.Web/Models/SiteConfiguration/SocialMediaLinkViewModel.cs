using System.Collections.Generic;

namespace Harriers.Web.Models.SiteConfiguration
{
    public class SocialMediaLinkViewModel
    {
        public List<SocialMediaLink> Links { get; set; }

        public bool IncludeName { get; set; }
    }
}