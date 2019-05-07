﻿namespace Harriers.Web.Models.Navigation
{
    public class NavigationLink
    {
        public NavigationLink()
        {
        }

        public NavigationLink(string url, string text = null, bool newWindow = false, string title = null)
        {
            Text = text;
            Url = url;
            NewWindow = newWindow;
            Title = title;
        }

        public string Text { get; set; }

        public string Url { get; set; }

        public bool NewWindow { get; set; }

        public string Target { get { return NewWindow ? "_blank" : null; } }

        public string Title { get; set; }
    }
}