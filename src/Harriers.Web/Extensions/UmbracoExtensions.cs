﻿using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Harriers.Web.Extensions
{
    /// <summary>
    /// A few useful extension methods for Umbraco Models
    /// </summary>
    public static partial class UmbracoExtensions
    {

        /// <summary>
        /// Check if the IPublished Content has a value
        /// </summary>
        /// <param name="content">The current content</param>
        /// <returns>If a value is set</returns>
        public static bool HasValue(this IPublishedContent content)
        {
            return content != null;
        }

        public static bool HasValue(this Link content)
        {
            return content != null && content.Url != "";
        }

        public static bool HasValue(this IPublishedElement content)
        {
            return content != null;
        }
    }
}