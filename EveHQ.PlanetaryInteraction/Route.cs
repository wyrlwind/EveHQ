using EveHQ.NewEveApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.PlanetaryInteraction
{
    class Route
    {
        private PlanetaryRoute _route;
        private Colony _colony;

        public Route(PlanetaryRoute route, Colony colony)
        {
            _route = route;
            _colony = colony;
        }

        public long RouteID
        {
            get { return _route.RouteID; }
        }

        public long SourceID
        {
            get { return _route.SourcePinID; }
        }

        public string Planet
        {
            get { return _colony.Planet; }
        }

        public string Source
        {
            get
            {
                Installation source = _colony.Installations.Find(
                    delegate (Installation installation)
                    {
                        return installation.Id == _route.SourcePinID;
                    });
                return source.Type;
            }
        }

        public string Destination
        {
            get
            {
                Installation destination = _colony.Installations.Find(
                    delegate (Installation installation)
                    {
                        return installation.Id == _route.DestinationPinID;
                    });
                return destination.Type;
            }
        }

        public string Commodity
        {
            get { return _route.ContentTypeName; }
        }

        public int Quantity
        {
            get { return _route.Quantity; }
        }

    }
}
