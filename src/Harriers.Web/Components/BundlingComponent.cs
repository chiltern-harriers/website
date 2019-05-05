using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Resolvers;
using System.Web.Optimization;
using Umbraco.Core.Composing;

namespace Harriers.Web.Components
{
    public class BundlingComponent : IComponent
    {
        public void Initialize()
        {
            RegisterBundles(BundleTable.Bundles);
        }

        public void Terminate()
        {
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            BundleResolver.Current = new CustomBundleResolver();

            bundles.Add(new CustomStyleBundle("~/bundles/styles")
                .Include(
                    "~/Content/core.scss",
                    "~/Content/fonts.css"));

            bundles.Add(new CustomScriptBundle("~/bundles/scripts")
                .Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/popper.js",
                    "~/Scripts/bootstrap.js"));
        }
    }
}