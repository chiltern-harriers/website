using System.Collections.Generic;
using Umbraco.Web.Models;

namespace Harriers.Web.Models.SiteConfiguration
{
    public class WebsiteConfiguration
    {
        public List<SocialMediaLink> SocialMedia { get; set; }

        public List<Link> FooterLinks { get; set; }
    }
}