using EveHQ.EveCrest.Models;
using EveHQ.EveCrest.Models.Cache;
using EveHQ.EveCrest.Models.Crest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace EveHQ.EveCrest
{
    public class EveCrest
    {
        private static Dictionary<Uri, CacheItem> _cache;
        private static EveCrest _instance;

        public static EveCrest getInstance()
        {
            if (_instance == null)
                _instance = new EveCrest();
            return _instance;
        }

        private EveCrest()
        {
            _cache = new Dictionary<Uri, CacheItem>();
        }

        #region "Industry"
        /// <summary>
        /// Enable to get information from a specific system (including system name and system indices)
        /// </summary>
        /// <param name="systemId">The system ID from which you're looking data for</param>
        public IndustrySystem getIndustrySystem(int systemId)
        {
            var systems = getIndustrySystems();
            if (systems != null)
            {
                foreach (var system in systems)
                {
                    if (system.solarSystem.id == systemId)
                        return system;
                }
            }

            return null;
        }

        /// <summary>
        /// Fetch industry system data from CREST (including system name and indices)
        /// </summary>
        private IEnumerable<IndustrySystem> getIndustrySystems()
        {
            string result;
            Uri crestEndpoint = new Uri("https://public-crest.eveonline.com/industry/systems/");
            CrestResult<IndustrySystem> parsedResult;

            // parse the result
            result = fetchData(crestEndpoint).ToString();

            if (!String.IsNullOrEmpty(result))
            {
                try {
                    parsedResult = Newtonsoft.Json.JsonConvert.DeserializeObject<CrestResult<IndustrySystem>>(result);
                    return parsedResult.items;
                } catch (Exception e) {

                }
            }

            return null;
        }
        #endregion

        #region "Market"
        /// <summary>
        /// Enable to return market adjusted and average prices for a specific item
        /// </summary>
        /// <param name="itemId">The item ID</param>
        public MarketPrice getMarketPrice(int itemId)
        {
            var marketPrices = fetchMarketPrices();
            if (marketPrices != null)
            {
                foreach (var marketPrice in marketPrices)
                {
                    if (marketPrice.type.id == itemId)
                        return marketPrice;
                }
            }

            return null;
        }

        /// <summary>
        /// Fetch market prices (average and adjusted) from CREST
        /// </summary>
        private IEnumerable<MarketPrice> fetchMarketPrices()
        {
            string result;
            Uri crestEndpoint = new Uri("https://public-crest.eveonline.com/market/prices/");
            CrestResult<MarketPrice> parsedResult;

            // parse the result
            result = fetchData(crestEndpoint).ToString();

            if (!String.IsNullOrEmpty(result))
            {
                try {
                    parsedResult = Newtonsoft.Json.JsonConvert.DeserializeObject<CrestResult<MarketPrice>>(result);
                    return parsedResult.items;
                } catch (Exception e) {

                }
            }

            return null;
        }
        #endregion

        #region "Helpers"
        /// <summary>
        /// Enable to fetch data from CREST
        /// </summary>
        /// <param name="crestEndpoint">The CREST url to call</param>
        private string fetchData(Uri crestEndpoint)
        {
            string result = "";
            
            if (isCached(crestEndpoint))
                return _cache[crestEndpoint].cachedData;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(crestEndpoint);
            request.UserAgent = String.Format("{0} {1}",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream)) {
                        result = reader.ReadToEnd();
                    }
                }
            }
            
            if (!String.IsNullOrEmpty(result))
                _cache.Add(crestEndpoint, new CacheItem(1, result));

            return result;
        }
        
        /// <summary>
        /// Enable to check if an endpoint result is still in cache
        /// </summary>
        /// <param name="crestEndpoint">The CREST endpoint to check</param>
        /// <returns>True if the endpoint is cached</returns>
        private bool isCached(Uri crestEndpoint)
        {
            long currentTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            if (_cache.ContainsKey(crestEndpoint))
            {
                if (_cache[crestEndpoint].expiredTimestamp > currentTimestamp)
                    return true;

                _cache.Remove(crestEndpoint);
            }

            return false;
        }
        #endregion
    }
}
