using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MoovIt
{
    public class MoovIt : IMoovIt
    {
        private HashSet<Route> routes;
        private Dictionary<string, Route> routesById;

        public MoovIt()
        {
            routes = new HashSet<Route>();
            routesById = new Dictionary<string, Route>();
        }
        public int Count => routes.Count;

        public void AddRoute(Route route)
        {
            if (routes.Contains(route))
            {
                throw new ArgumentException();
            }

            routes.Add(route);
            routesById.Add(route.Id, route);
        }

        public void ChooseRoute(string routeId)
        {
            var route = GetRoute(routeId);

            route.Popularity++;
        }

        public bool Contains(Route route)
            => routes.Contains(route);

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
            => routes.Where(x => x.IsFavorite && destinationPoint == x.LocationPoints[x.LocationPoints.Count - 1])
                     .OrderBy(x => x.Distance)
                     .ThenByDescending(x => x.Popularity);


        public Route GetRoute(string routeId)
        {
            if (!routesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }

            var route = routesById[routeId];

            return route;
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
            => routes.OrderByDescending(x => x.Popularity).ThenBy(x => x.Distance).ThenBy(x => x.LocationPoints.Count).Take(5);

        public void RemoveRoute(string routeId)
        {
            var route = GetRoute(routeId);

            routes.Remove(route);
            routesById.Remove(routeId);
        }

        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
            => routes.Where(x => x.LocationPoints.Contains(startPoint) && x.LocationPoints.Contains(endPoint) &&
                                 x.LocationPoints.IndexOf(startPoint) < x.LocationPoints.IndexOf(endPoint))
                     .OrderBy(x => x.IsFavorite)
                     .ThenBy(x => x.LocationPoints.IndexOf(endPoint) - x.LocationPoints.IndexOf(startPoint))
                     .ThenByDescending(x => x.Popularity);
    }
}
