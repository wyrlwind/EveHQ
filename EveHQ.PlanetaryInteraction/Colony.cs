using EveHQ.NewEveApi.Entities;
using System.Collections.Generic;

namespace EveHQ.PlanetaryInteraction
{
    class Colony
    {
        private PlanetaryColony _colony;
        private List<Installation> _installations;
        private List<PlanetaryLink> _links;
        private List<PlanetaryRoute> _routes;

        public Colony(PlanetaryColony colony)
        {
            _colony = colony;
        }

        public void addInstallation(PlanetaryPin pin)
        {
            if (_installations == null)
            {
                _installations = new List<Installation>();
            }
            _installations.Add(new Installation(pin));
        }

        public void addLink(PlanetaryLink link)
        {
            if (_links == null)
            {
                _links = new List<PlanetaryLink>();
            }
            _links.Add(link);
        }

        public void addRoute(PlanetaryRoute route)
        {
            if (_routes == null)
            {
                _routes = new List<PlanetaryRoute>();
            }
            _routes.Add(route);
        }

        public void setLinks(List<PlanetaryLink> links)
        {
            _links = links;
        }

        public void setRoutes(List<PlanetaryRoute> routes)
        {
            _routes = routes;
        }

        public List<Installation> Installations
        {
            get { return _installations; }
            set { _installations = value; }
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
    }
}
