using System.Collections.Generic;
using System.Linq;

namespace Harriers.Web.Models.Navigation
{
    public class NavigationListItem
    {
        public NavigationListItem()
        {
        }

        public NavigationListItem(NavigationLink link)
        {
            Link = link;
        }

        public NavigationListItem(NavigationLink link, params NavigationListItem[] children)
        {
            Link = link;
            this.Items = new List<NavigationListItem>(children);
        }

        public NavigationListItem(string text)
        {
            Text = text;
        }

        public NavigationListItem(string text, params NavigationListItem[] children)
        {
            Text = text;
            this.Items = new List<NavigationListItem>(children);
        }

        public string Text { get; set; }

        public NavigationLink Link { get; set; }

        public List<NavigationListItem> Items { get; set; }

        public bool HasChildren { get { return Items != null && Items.Any(); } }
    }
}