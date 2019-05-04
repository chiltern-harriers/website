using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Harriers.Web.Components
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ComponentComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<BundlingComponent>();
        }
    }
}