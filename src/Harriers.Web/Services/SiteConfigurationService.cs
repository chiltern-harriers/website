using Harriers.Web.Models.SiteConfiguration;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Harriers.Web.Services
{
    public class SiteConfigurationService
    {
        public WebsiteConfiguration GetWebsiteConfiguration(UmbracoHelper umbraco)
        {
            var response = new WebsiteConfiguration();

            var websiteConfiguration = umbraco.ContentAtRoot().FirstOrDefault(c => c.Name == "Website Configuration");

            if (websiteConfiguration != null)
            {
                
                var mediaLinks = websiteConfiguration.Value<IEnumerable<PublishedElementModel>>("socialMediaLinks");
                if (mediaLinks != null)
                {
                    response.SocialMedia = mediaLinks.Select(ml => new SocialMediaLink
                    {
                        ClassNames = ml.Value<string>("iconClassName"),
                        Name = ml.Value<string>("socialMediaName"),
                        Link = new System.Uri(ml.Value<string>("siteUrl"))
                    }).ToList();
                }

                var footerLinks = websiteConfiguration.Value<IEnumerable<Link>>("footerLinks").ToList();
                response.FooterLinks = footerLinks;
            }

            return response;
        }
    }
}