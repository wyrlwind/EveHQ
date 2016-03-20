using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.EveCrest.Models.Cache
{
    class CacheItem
    {
        private int _cacheTime;
        internal int cacheTime {
            get { return _cacheTime; }
            private set {
                this.expiredTimestamp = ((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)) + value * 60 * 100;
                this._cacheTime = value;
            }
        }
        internal long expiredTimestamp { get; private set; }
        internal string cachedData { get; private set; }

        internal CacheItem(int hours, string data)
        {
            this.cacheTime = hours;
            this.cachedData = data;
        }
    }
}
