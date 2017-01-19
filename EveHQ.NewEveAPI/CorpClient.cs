﻿// ==============================================================================
// 
// EveHQ - An Eve-Online™ character assistance application
// Copyright © 2005-2015  EveHQ Development Team
//   
// This file is part of EveHQ.
//  
// The source code for EveHQ is free and you may redistribute 
// it and/or modify it under the terms of the MIT License. 
// 
// Refer to the NOTICES file in the root folder of EVEHQ source
// project for details of 3rd party components that are covered
// under their own, separate licenses.
// 
// EveHQ is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the MIT 
// license below for details.
// 
// ------------------------------------------------------------------------------
// 
// The MIT License (MIT)
// 
// Copyright © 2005-2015  EveHQ Development Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// ==============================================================================

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using EveHQ.Caching;
using EveHQ.Common;
using EveHQ.Common.Extensions;
using EveHQ.NewEveApi.Entities;

namespace EveHQ.NewEveApi
{
    /// <summary>The corp client.</summary>
    public sealed class CorpClient : CorpCharBaseClient
    {
        /// <summary>The request prefix.</summary>
        private const string RequestPrefix = "/corp";

        /// <summary>
        ///     Initializes a new instance of the <see cref="CorpClient" /> class. Initializes a new instance of the
        ///     CharacterClient class.
        /// </summary>
        /// <param name="eveServiceLocation">location of the eve API web service</param>
        /// <param name="cacheProvider">root folder used for caching.</param>
        /// <param name="requestProvider">Request provider to use for this instance.</param>
        internal CorpClient(string eveServiceLocation, ICacheProvider cacheProvider,
            IHttpRequestProvider requestProvider)
            : base(eveServiceLocation, cacheProvider, requestProvider, RequestPrefix)
        {
        }


        public EveServiceResponse<CorporateData> CorporationSheet(string keyId, string vCode, long corpId = 0,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(CorporationSheetAsync, keyId, vCode, corpId, responseMode);
        }

        public Task<EveServiceResponse<CorporateData>> CorporationSheetAsync(string keyId, string vCode, long corpId = 0,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());

            const string MethodPath = "{0}/CorporationSheet.xml.aspx";
            const string CacheKeyFormat = "CorporationSheet{0}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId,
                corpId > 0 ? "_{0}".FormatInvariant(corpId) : string.Empty);

            IDictionary<string, string> apiParams = new Dictionary<string, string>();
            if (corpId > 0)
            {
                apiParams["corporationID"] = corpId.ToInvariantString();
            }

            return GetServiceResponseAsync(keyId, vCode, 0, MethodPath.FormatInvariant(PathPrefix), apiParams, cacheKey,
                ApiConstants.SixtyMinuteCache, responseMode, ProcessCorporationSheetResponse);
        }


        private static CorporateData ProcessCorporationSheetResponse(XElement results)
        {
            if (results == null)
            {
                return null; // return null... no data.
            }

            var data = new CorporateData();


            data.CorporationId = results.Element("corporationID").Value.ToInt32();
            data.CorporationName = results.Element("corporationName").Value;
            data.Ticker = results.Element("ticker").Value;
            data.CeoId = results.Element("ceoID").Value.ToInt32();
            data.CeoName = results.Element("ceoName").Value;
            data.StationId = results.Element("stationID").Value.ToInt32();
            data.StationName = results.Element("stationName").Value;
            data.Description = results.Element("description").Value;
            data.Url = results.Element("url").Value;
            data.AllianceId = results.Element("allianceID").Value.ToInt32();
            if (results.Element("allianceName") != null)
            {
                data.AllianceName = results.Element("allianceName").Value;
            }
            data.TaxRate = results.Element("taxRate").Value;
            data.MemberCount = results.Element("memberCount").Value.ToInt32();
            data.MemberLimit = results.Element("memberLimit").Value.ToInt32();
            data.Shares = results.Element("shares").Value.ToInt32();
            data.Divisions = from rowsets in results.Elements("rowset")
                from rows in rowsets.Descendants()
                where rowsets.Attribute("name").Value == "divisions"
                select
                    new CorporateDivision
                    {
                        AccountKey = rows.Attribute("accountKey").Value.ToInt32(),
                        Description = rows.Attribute("description").Value
                    };
            data.WalletDivisions = from rowsets in results.Elements("rowset")
                from rows in rowsets.Descendants()
                where rowsets.Attribute("name").Value == "walletDivisions"
                select
                    new CorporateDivision
                    {
                        AccountKey = rows.Attribute("accountKey").Value.ToInt32(),
                        Description = rows.Attribute("description").Value
                    };

            XElement logoXml = results.Element("logo");
            var logo = new CorporateLogo
            {
                GraphicId = logoXml.Element("graphicID").Value.ToInt32(),
                Color1 = logoXml.Element("color1").Value.ToInt32(),
                Color2 = logoXml.Element("color2").Value.ToInt32(),
                Color3 = logoXml.Element("color3").Value.ToInt32(),
                Shape1 = logoXml.Element("shape1").Value.ToInt32(),
                Shape2 = logoXml.Element("shape2").Value.ToInt32(),
                Shape3 = logoXml.Element("shape3").Value.ToInt32()
            };
            data.Logo = logo;


            return data;
        }
    }
}