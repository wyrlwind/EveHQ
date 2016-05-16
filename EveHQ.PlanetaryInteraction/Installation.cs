using EveHQ.EveData;
using EveHQ.NewEveApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.PlanetaryInteraction
{
    class Installation
    {
        private PlanetaryPin _pin;
        private Colony _colony;

        public Installation(PlanetaryPin pin, Colony colony)
        {
            _pin = pin;
            _colony = colony;
        }

        public string Type
        {
            get { return _pin.TypeName; }
        }

        public int CycleTime
        {
            get { return _pin.CycleTime; }
        }

        public int QuantityPerCycle
        {
            get { return _pin.QuantityPerCycle; }
        }

        public string Expiration
        {
            get
            {
                if (_pin.ExpiryTime.Ticks == 0)
                {
                    return "--";
                }
                return _pin.ExpiryTime.ToString();
            }
        }

        public string Commodity
        {
            get
            {
                HashSet<String> commodities = new HashSet<String>();
                foreach (Route route in _colony.Routes)
                {
                    if (route.SourceID == _pin.PinID)
                    {
                        commodities.Add(route.Commodity);
                    }
                }
                String commodityList = "";
                foreach (String commodity in commodities)
                {
                    if (commodityList.Length != 0)
                    {
                        commodityList += ",";
                    }
                    commodityList += commodity;
                }
                if (commodityList.Length == 0)
                {
                    commodityList = _pin.ContentTypeName;
                }
                return commodityList;
            }
        }

        public int Quantity
        {
            get { return _pin.ContentQuantity; }
        }

        public double Volume
        {
            get
            {
                double volume = StaticData.Types[_pin.ContentTypeID].Volume;
                volume = volume * _pin.ContentQuantity;
                return volume;
            }
        }

        public TimeSpan TimeLeft
        {
            get
            {
                DateTimeOffset utcTime = DateTimeOffset.UtcNow;
                if (DateTimeOffset.Compare(_pin.ExpiryTime, utcTime) > 0)
                {
                    TimeSpan timediff = _pin.ExpiryTime - utcTime;
                    return timediff;
                }
                return new TimeSpan(0);
            }
        }

        public string Planet
        {
            get { return _colony.Planet; }
        }

        public long Id
        {
            get { return _pin.PinID; }
        }

        public double Latitude
        {
            get { return _pin.Latitude; }
        }

        public double Longitude
        {
            get { return _pin.Longitude; }
        }
    }
}
