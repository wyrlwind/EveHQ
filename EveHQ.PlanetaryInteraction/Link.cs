using EveHQ.NewEveApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.PlanetaryInteraction
{
    class Link
    {
        private PlanetaryLink _link;
        private Colony _colony;

        public Link(PlanetaryLink link, Colony colony)
        {
            _link = link;
            _colony = colony;
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
                        return installation.Id == _link.SourcePinID;
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
                        return installation.Id == _link.DestinationPinID;
                    });
                return destination.Type;
            }
        }

        public int Level
        {
            get { return _link.LinkLevel; }
        }

        public double Length
        {
            get
            {
                Installation source = _colony.Installations.Find(
                    delegate (Installation installation)
                    {
                        return installation.Id == _link.SourcePinID;
                    });
                Installation destination = _colony.Installations.Find(
                    delegate (Installation installation)
                    {
                        return installation.Id == _link.DestinationPinID;
                    });
                double distance = getDistanceFromLatLonInKm(source.Latitude, source.Longitude, destination.Latitude, destination.Longitude, _colony.PlanetRadius);
                return distance;
            }
        }

        private double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2, double radius)
        {
            double dLat = deg2rad(lat2 - lat1);  // deg2rad below
            double dLon = deg2rad(lon2 - lon1);
            double a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = radius * c; // Distance in km
            return d;
        }

        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
