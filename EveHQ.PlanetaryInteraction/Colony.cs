using EveHQ.Core;
using EveHQ.EveData;
using EveHQ.NewEveApi.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace EveHQ.PlanetaryInteraction
{
    class Colony
    {
        private PlanetaryColony _colony;
        private Dictionary<long, Installation> _installations;
        private List<Link> _links;
        private Dictionary<long, Route> _routes;

        public Colony(PlanetaryColony colony)
        {
            _colony = colony;
        }

        public void addInstallation(PlanetaryPin pin)
        {
            if (_installations == null)
            {
                _installations = new Dictionary<long, Installation>();
            }
            _installations[pin.PinID] = new Installation(pin, this);
        }

        public void addLink(PlanetaryLink link)
        {
            if (_links == null)
            {
                _links = new List<Link>();
            }
            _links.Add(new Link(link, this));
        }

        public void addRoute(PlanetaryRoute route)
        {
            if (_routes == null)
            {
                _routes = new Dictionary<long, Route>();
            }
            _routes[route.RouteID] = new Route(route, this);
        }

        public List<Route> Routes
        {
            get
            {
                List<Route> routes = null;
                if (_routes.Count != 0)
                {
                    routes = new List<Route>();
                    foreach (KeyValuePair<long, Route> route in _routes)
                    {
                        routes.Add(route.Value);
                    }
                }
                return routes;
            }
            set
            {
                foreach (Route route in value)
                {
                    if (_routes == null)
                    {
                        _routes = new Dictionary<long, Route>();
                    }
                    _routes[route.RouteID] = route;
                }
            }
        }

        public List<Link> Links
        {
            get { return _links; }
            set { _links = value; }
        }

        public List<Installation> Installations
        {
            get
            {
                List<Installation> installations = null;
                if (_installations.Count != 0)
                {
                    installations = new List<Installation>();
                    foreach (KeyValuePair<long, Installation> installation in _installations)
                    {
                        installations.Add(installation.Value);
                    }
                }
                return installations;
            }
            set
            {
                foreach (Installation installation in value)
                {
                    if (_installations == null)
                    {
                        _installations = new Dictionary<long, Installation>();
                    }
                    _installations[installation.Id] = installation;
                }
            }
        }

        public int PlanetID
        {
            get { return _colony.PlanetID; }
        }

        public string Planet
        {
            get { return _colony.PlanetName; }
        }

        public string System
        {
            get { return _colony.SolarSystemName; }
        }

        public string PlanetType
        {
            get { return _colony.PlanetTypeName; }
        }

        public string LastUpdate
        {
            get
            {
                if (_colony.LastUpdate.Ticks == 0)
                {
                    return "--";
                }
                return _colony.LastUpdate.ToString();
            }
        }

        public int UpgradeLevel
        {
            get { return _colony.UpgradeLevel; }
        }

        public int InstallationCount
        {
            get { return _colony.NumberOfPins; }
        }

        public int LinksCount
        {
            get { return _links.Count; }
        }
        public int RoutesCount
        {
            get { return _routes.Count; }
        }

        public Image PlanetIcon
        {
            get
            {
                return ImageHandler.GetImage(_colony.PlanetTypeID, 32);
            }
        }

        public double PlanetRadius
        {
            get
            {
                return StaticData.Planets[_colony.PlanetID].Radius/1000;
            }
        }
    }
}
