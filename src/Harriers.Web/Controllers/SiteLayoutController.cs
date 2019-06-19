using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Runtime.Caching;
using Harriers.Web.Models.Navigation;
using Umbraco.Core.Models.PublishedContent;

namespace Harriers.Web.Controllers
{
    public class SiteLayoutController : SurfaceController
    {
        public const string VIEW_FOLDER_PATH = "~/Views/Partials/SiteLayout/";

        /// <summary>
        /// Renders the sidebar navigation partial
        /// </summary>
        /// <returns>Partial view with a model</returns>
        public ActionResult SidebarNavigation()
        {
            List<NavigationListItem> nav = GetObjectFromCache<List<NavigationListItem>>(
                "mainNav", 
                5,
                GetHardCodedList);

            return PartialView(VIEW_FOLDER_PATH + "_SidebarNavigation.cshtml", nav);
        }

        /// <summary>
        /// Renders the sidebar navigation partial
        /// </summary>
        /// <returns>Partial view with a model</returns>
        public ActionResult TopNavigation()
        {
            List<NavigationListItem> nav = GetObjectFromCache<List<NavigationListItem>>(
                "mainNav",
                5,
                GetHardCodedList);

            return PartialView(VIEW_FOLDER_PATH + "_TopNavigation.cshtml", nav);
        }

        /// <summary>
        /// A generic function for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="objectSettingFunction">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
                cachedObject = objectSettingFunction();
                cache.Set(cacheItemName, cachedObject, policy);
            }
            return cachedObject;
        }

        private List<NavigationListItem> GetHardCodedList()
        {
            var nav = new List<NavigationListItem>();

            nav.Add(
                new NavigationListItem(new NavigationLink("/", "Home")));

            nav.Add(
                new NavigationListItem(
                    new NavigationLink("/About", "About"),
                        new NavigationListItem(new NavigationLink("/About", "About Us")),
                        new NavigationListItem(new NavigationLink("/", "Training Times"),
                            new NavigationListItem(new NavigationLink("/", "Juniors")),
                            new NavigationListItem(new NavigationLink("/", "Seniors"))),
                        new NavigationListItem(new NavigationLink("/", "Club Contacts"))));

            nav.Add(
                new NavigationListItem(new NavigationLink("/", "Membership"),
                    new NavigationListItem(new NavigationLink("/", "Membership Details")),
                    new NavigationListItem(new NavigationLink("/", "Club Kit")),
                    new NavigationListItem(new NavigationLink("/", "Code of Practise")),
                    new NavigationListItem(new NavigationLink("/", "Offers for the Club"))));

            nav.Add(
                new NavigationListItem(new NavigationLink("/", "Calendar"),
                    new NavigationListItem(new NavigationLink("/", "Race Calendar")),
                    new NavigationListItem(new NavigationLink("/", "Club Events"))));

            nav.Add(
                new NavigationListItem(new NavigationLink("/", "Events"),
                    new NavigationListItem(new NavigationLink("/", "Cross Country")),
                    new NavigationListItem(new NavigationLink("/", "Track and Field")),
                    new NavigationListItem(new NavigationLink("/", "Junior Triathlon")),
                    new NavigationListItem(new NavigationLink("/", "Shardeloes 10k")),
                    new NavigationListItem(new NavigationLink("/", "Herbert's Hole")),
                    new NavigationListItem(new NavigationLink("/", "Pednor 5"))));

            nav.Add(
                new NavigationListItem(new NavigationLink("/", "Club News"),
                    new NavigationListItem(new NavigationLink("/", "Latest News")),
                    new NavigationListItem(new NavigationLink("/", "Notices"))));

            return nav;
        }

        /// <summary>
        /// Finds the home page and gets the navigation structure based on it and it's children
        /// </summary>
        /// <returns>A List of NavigationListItems, representing the structure of the site.</returns>
        private List<NavigationListItem> GetNavigationModelFromDatabase()
        {
            const int HomePagePositionInPath = 2;

            List<NavigationListItem> nav = new List<NavigationListItem>();

            string[] pageIds = CurrentPage.Path.Split(',');
            if (pageIds.Length > HomePagePositionInPath)
            {
                int homePageId = int.Parse(pageIds[HomePagePositionInPath]);

                IPublishedContent homePage = Umbraco.Content(homePageId);
                nav.Add(new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name)));

                List<NavigationListItem> childPages = GetChildNavigationList(homePage);
                if (childPages != null && childPages.Any())
                    nav.AddRange(GetChildNavigationList(homePage));
            }            

            return nav;
        }

        /// <summary>
        /// Loops through the child pages of a given page and their children to get the structure of the site.
        /// </summary>
        /// <param name="page">The parent page which you want the child structure for</param>
        /// <returns>A List of NavigationListItems, representing the structure of the pages below a page.</returns>
        private List<NavigationListItem> GetChildNavigationList(dynamic page)
        {
            List<NavigationListItem> listItems = null;
            IPublishedContent[] children = page.Children;

            var childPages = children.Cast<dynamic>().Where(n => !n.HideInNavigation);

            if (childPages != null && childPages.Any())
            {
                listItems = new List<NavigationListItem>();

                foreach (var childPage in childPages)
                {
                    NavigationListItem listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildNavigationList(childPage);
                    listItems.Add(listItem);
                }
            }

            return listItems;
        }
    }
}