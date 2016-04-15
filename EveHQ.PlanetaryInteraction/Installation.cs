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

        public Installation(PlanetaryPin pin)
        {
            _pin = pin;
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
            get { return _pin.ContentTypeName; }
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
    }
}
