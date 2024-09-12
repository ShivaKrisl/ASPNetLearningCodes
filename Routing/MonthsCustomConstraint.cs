
using System.Text.RegularExpressions;

namespace ASPNetLearningCodes.Routing
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        bool IRouteConstraint.Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // defining regex for months here
            if (!values.ContainsKey(routeKey))
                return false;
            Regex regex = new Regex("^(jul|aug|sept|oct)$");
            string? monthValue = Convert.ToString(values[routeKey]);
            if (monthValue == null) return false;
            return regex.IsMatch(monthValue);
        }
    }
}
