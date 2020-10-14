using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics.CodeAnalysis;

namespace FindMeFoodTrucks.WebAPI.Filters
{
    [ExcludeFromCodeCoverage]
    public class ApiExplorerIgnores : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerName.Equals("Error"))
                action.ApiExplorer.IsVisible = false;
        }
    }
}
