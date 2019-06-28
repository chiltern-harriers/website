namespace Harriers.Web.Models.Navigation
{
    public class NavigationLink
    {
        public NavigationLink()
        {
        }

        public NavigationLink(string url, string text = null, string title = null)
        {
            Text = text;
            Url = url;
            Title = title;
        }

        public string Text { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }
    }
}