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

        public NavigationLink Link { get; set; }

        public bool Active { get; set; }

        public List<NavigationListItem> Items { get; set; }

        public bool HasChildren
        {
            get
            {
                return Items != null && Items.Any();
            }
        }
    }
}