using EveHQ.EveCrest.Models;
using EveHQ.EveCrest.Models.Cache;
using EveHQ.EveCrest.Models.Crest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using EveHQ.Common;
using EveHQ.Common.Extensions;
using System.Net.Http;

namespace EveHQ.EveCrest
{
    public class EveCrest
    {
        private static Dictionary<Uri, CacheItem> _cache;
        private static EveCrest _instance;
        private readonly IHttpRequestProvider _requestProvider;

        public static EveCrest getInstance()
        {
            if (_instance == null)
                _instance = new EveCrest();
            return _instance;
        }

        private EveCrest()
        {
            _cache = new Dictionary<Uri, CacheItem>();
            _requestProvider = new HttpRequestProvider(null);
        }

        #region "Industry"
        /// <summary>
        /// Fetch industry system data from CREST (including system name and indices)
        /// </summary>
        public Task<Dictionary<long, IndustrySystem>> getIndustrySystems()
        {
            return Task<Dictionary<long, IndustrySystem>>.Factory.TryRun(
                () =>
                {
                    IEnumerable<IndustrySystem> result;
                    Uri crestEndpoint = new Uri("https://public-crest.eveonline.com/industry/systems/");
                    Dictionary<long, IndustrySystem> systems = new Dictionary<long, IndustrySystem>();

                    if (!isCached(crestEndpoint))
                    {
                        fetchData(crestEndpoint);
                    }

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<CrestResult<IndustrySystem>>(_cache[crestEndpoint].cachedData).items;

                    if (result != null)
                    {
                        foreach (var system in result)
                        {
                            if (!systems.ContainsKey(system.solarSystem.id))
                                systems.Add(system.solarSystem.id, system);
                        }
                    }

                    return systems;
                }
            );
        }
        #endregion

        #region "Market"
        /// <summary>
        /// Fetch market prices (average and adjusted) from CREST
        /// </summary>
        public Task<Dictionary<long, MarketPrice>> fetchMarketPrices()
        {
            return Task<Dictionary<long, MarketPrice>>.Factory.TryRun(
                () =>
                {
                    IEnumerable<MarketPrice> result;
                    Uri crestEndpoint = new Uri("https://public-crest.eveonline.com/market/prices/");
                    Dictionary<long, MarketPrice> prices = new Dictionary<long, MarketPrice>();

                    if (!isCached(crestEndpoint))
                    {
                        fetchData(crestEndpoint);
                    }

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<CrestResult<MarketPrice>>(_cache[crestEndpoint].cachedData).items;

                    if (result != null)
                    {
                        foreach (var price in result)
                        {
                            if (!prices.ContainsKey(price.type.id))
                                prices.Add(price.type.id, price);
                        }
                    }

                    return prices;
                }
            );
        }
        #endregion

        #region "Helpers"
        /// <summary>
        /// Send request to CREST, fetch data in an async process and store the result to the cache
        /// </summary>
        /// <param name="crestEndpoint"></param>
        public void fetchData(Uri crestEndpoint)
        {
            // call data, parse, and so on
            Task<HttpResponseMessage> requestTask = _requestProvider.GetAsync(crestEndpoint);
            requestTask.Wait();

            if (requestTask.IsCompleted && !requestTask.IsCanceled && !requestTask.IsFaulted &&
                requestTask.Exception == null && requestTask.Result != null)
            {
                Task<Stream> contentStreamTask = requestTask.Result.Content.ReadAsStreamAsync();
                contentStreamTask.Wait();

                using (Stream stream = contentStreamTask.Result)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        if (!_cache.ContainsKey(crestEndpoint))
                            _cache.Add(crestEndpoint, new CacheItem(1, reader.ReadToEnd()));
                    }
                }
            }
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
