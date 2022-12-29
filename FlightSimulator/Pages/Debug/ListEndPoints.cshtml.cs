using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Listener.Pages.Debug
{
    /// <exclude />
    public class ListEndPointsModel : PageModel
    {
        private readonly IEnumerable<EndpointDataSource> endpointSources;

        /// <exclude />
        public record EndPoint
        {
            /// <exclude />
            public string? Method;
            /// <exclude />
            public string? Route;
            /// <exclude />
            public string? Action;
            /// <exclude />
            public string? ControllerMethod;
        }

        /// <exclude />
        public List<EndPoint>? endpoints;
        /// <exclude />
        public ListEndPointsModel(IEnumerable<EndpointDataSource> endpointSources)
        {
            this.endpointSources = endpointSources;
        }

        /// <exclude />
        public void OnGet()
        {
            var endpoints = endpointSources.SelectMany(es => es.Endpoints).OfType<RouteEndpoint>();
            this.endpoints = endpoints.Select(
                e =>
                {
                    var controller = e.Metadata
                        .OfType<ControllerActionDescriptor>()
                        .FirstOrDefault();
                    var action = controller != null
                        ? $"{controller.ControllerName}.{controller.ActionName}"
                        : null;
                    var controllerMethod = controller != null
                        ? $"{controller.ControllerTypeInfo.FullName}:{controller.MethodInfo.Name}"
                        : null;
                    return new EndPoint
                    {
                        Method = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods?[0],
                        Route = $"/{e.RoutePattern.RawText?.TrimStart('/')}",
                        Action = action,
                        ControllerMethod = controllerMethod
                    };
                }
            ).ToList< EndPoint>();
        }
    }
}
