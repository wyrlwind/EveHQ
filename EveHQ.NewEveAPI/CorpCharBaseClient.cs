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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using EveHQ.Caching;
using EveHQ.Common;
using EveHQ.Common.Extensions;
using EveHQ.NewEveApi.Entities;

namespace EveHQ.NewEveApi
{
    /// <summary>The information client.</summary>
    public abstract class CorpCharBaseClient : BaseApiClient
    {
        /// <summary>The _path prefix.</summary>
        private readonly string _pathPrefix;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CorpCharBaseClient" /> class. Initializes a new instance of the
        ///     CharacterClient class.
        /// </summary>
        /// <param name="eveServiceLocation">location of the eve API web service</param>
        /// <param name="cacheProvider">root folder used for caching.</param>
        /// <param name="requestProvider">Request provider to use for this instance.</param>
        /// <param name="pathPrefix">prefix to add to url paths in requests</param>
        protected internal CorpCharBaseClient(string eveServiceLocation, ICacheProvider cacheProvider,
            IHttpRequestProvider requestProvider, string pathPrefix)
            : base(eveServiceLocation, cacheProvider, requestProvider)
        {
            _pathPrefix = pathPrefix;
        }

        /// <summary>Gets the path prefix.</summary>
        protected string PathPrefix
        {
            get { return _pathPrefix; }
        }

        /// <summary>The account balance.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<AccountBalance>> AccountBalance(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(AccountBalanceAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>Gets the balance of a character.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>Account balances.</returns>
        public Task<EveServiceResponse<IEnumerable<AccountBalance>>> AccountBalanceAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string methodPath = "{0}/AccountBalance.xml.aspx";
            const string cacheKeyFormat = "Character_AccountBalance_{0}_{1}";

            string cacheKey = cacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, methodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseBalanceResponse);
        }

        /// <summary>The asset list.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<AssetItem>> AssetList(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(AssetListAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>Retrieves the given character's asset list.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>An enumerable collection of all items in the characters assets.</returns>
        public Task<EveServiceResponse<IEnumerable<AssetItem>>> AssetListAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string MethodPath = "{0}/AssetList.xml.aspx";
            const string CacheKeyFormat = "Character_AssetList_{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixHourCache, responseMode, ParseAssetListResponse);
        }

        /// <summary>The contact list.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<Contact>> ContactList(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(ContactListAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>Retrieves the character's contact list</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The contact list for the given character</returns>
        public Task<EveServiceResponse<IEnumerable<Contact>>> ContactListAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string MethodPath = "{0}/ContactList.xml.aspx";
            const string CacheKeyFormat = "ContactList{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseContactListResponse);
        }

        /// <summary>Retrieves the collection of notifications from contacts.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The notification list for the given character</returns>
        public Task<EveServiceResponse<IEnumerable<ContactNotification>>> ContactNotifications(string keyId,
            string vCode, long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string MethodPath = "{0}/ContactNotifications.xml.aspx";
            const string CacheKeyFormat = "ContactNotifications{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseContactNotificationResponse);
        }

        /// <summary>The contracts.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="contractId">The contract id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<Contract>> Contracts(string keyId, string vCode, long characterId,
            int contractId = 0, ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(ContractsAsync, keyId, vCode, characterId, contractId, responseMode);
        }

        /// <summary>Retrieves the list of contracts</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="contractId">[OPTIONAL] Only supply a value &gt; 0 if interested in a single contract.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<Contract>>> ContractsAsync(string keyId, string vCode,
            long characterId, int contractId = 0, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string MethodPath = "{0}/Contracts.xml.aspx";
            const string CacheKeyFormat = "CharacterContracts{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            IDictionary<string, string> apiParams = new Dictionary<string, string>();
            if (contractId > 0)
            {
                const string ContractId = "contractID";
                apiParams[ContractId] = contractId.ToInvariantString();
            }

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), apiParams,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseContractResponse);
        }

        /// <summary>The contract items.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="contractId">The contract id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<ContractItem>> ContractItems(string keyId, string vCode, long characterId,
            long contractId, ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(ContractItemsAsync, keyId, vCode, characterId, contractId, responseMode);
        }

        /// <summary>Retrieves the list of contracts</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="contractId">Contract ID to get items for.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<ContractItem>>> ContractItemsAsync(string keyId, string vCode,
            long characterId, long contractId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);
            Guard.Against(contractId == 0);

            const string MethodPath = "{0}/ContractItems.xml.aspx";
            const string CacheKeyFormat = "CharacterContractItems{0}_{1}_{2}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId, contractId);

            IDictionary<string, string> apiParams = new Dictionary<string, string>();

            const string ContractId = "contractID";
            apiParams[ContractId] = contractId.ToInvariantString();

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), apiParams,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseContractItemResponse);
        }

        /// <summary>Gets the character's factional warfare statistics.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>Factional warfare data.</returns>
        public Task<EveServiceResponse<IEnumerable<ContractBid>>> ContractBids(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string MethodPath = "{0}/ContractBids.xml.aspx";
            const string CacheKeyFormat = "CharacterContractBids{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessContractBidsResponse);
        }

        /// <summary>Gets the character's factional warfare statistics.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>Factional warfare data.</returns>
        public Task<EveServiceResponse<FactionalWarfareStats>> FactionalWarfareStatistics(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId == 0);

            const string MethodPath = "{0}/FacWarStats.xml.aspx";
            const string CacheKeyFormat = "CharacterFacWarStats{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseFacWarfareResponse);
        }

        /// <summary>The industry jobs.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<IndustryJob>> IndustryJobs(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(IndustryJobsAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>Gets the Industry Jobs for the given user.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>A collection of Industry Job data.</returns>
        public Task<EveServiceResponse<IEnumerable<IndustryJob>>> IndustryJobsAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Against(keyId.IsNullOrWhiteSpace());
            Guard.Against(vCode.IsNullOrWhiteSpace());
            Guard.Against(characterId <= 0);

            const string MethodPath = "{0}/IndustryJobs.xml.aspx";
            const string CacheKeyFormat = "CharacterIndustryJobs{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ParseIndustryJobsResponse);
        }

        /// <summary>The kill mails</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<Entities.Killmail.KillMail>> KillMails(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(KillMailsAsync, keyId, vCode, characterId, responseMode);
        }

        public Task<EveServiceResponse<IEnumerable<Entities.Killmail.KillMail>>> KillMailsAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/KillMails.xml.aspx";
            const string CacheKeyFormat = "{0}KillMails{1}_{2}";

            string cacheKey = CacheKeyFormat.FormatInvariant(PathPrefix, keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.FiveMinuteCache, responseMode, ProcessKillMailResponse);
        }

        public EveServiceResponse<IEnumerable<Entities.Blueprint>> Blueprints(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(BlueprintsAsync, keyId, vCode, characterId, responseMode);
        }

        public Task<EveServiceResponse<IEnumerable<Entities.Blueprint>>> BlueprintsAsync(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/Blueprints.xml.aspx";
            const string CacheKeyFormat = "{0}Blueprints{1}_{2}";

            string cacheKey = CacheKeyFormat.FormatInvariant(PathPrefix, keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixHourCache, responseMode, ProcessBlueprintsResponse);
        }

        /// <summary>The market orders.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<MarketOrder>> MarketOrders(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(MarketOrdersAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>Retrieves the market orders for the character.</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>a collection of market orders</returns>
        public Task<EveServiceResponse<IEnumerable<MarketOrder>>> MarketOrdersAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/MarketOrders.xml.aspx";
            const string CacheKeyFormat = "CharacterMarketOrders{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessMarketOrdersResponse);
        }

        /// <summary>Retrieves a collection of a character's medals</summary>
        /// <param name="keyId">API Key ID to query</param>
        /// <param name="vCode">The Verification Code for this ID</param>
        /// <param name="characterId">Character to query.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>a collection of medals.</returns>
        public Task<EveServiceResponse<IEnumerable<Medal>>> Medals(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/Medals.xml.aspx";
            const string CacheKeyFormat = "CharacterMedals{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessMedalsResponse);
        }

        /// <summary>The research.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<Research>>> Research(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/Research.xml.aspx";
            const string CacheKeyFormat = "CharacterResearch{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessResearchResponse);
        }

        /// <summary>The skill in training.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<SkillInTraining>> SkillInTraining(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/SkillInTraining.xml.aspx";
            const string CacheKeyFormat = "SkillInTraining{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessSkillInTrainingResponse);
        }

        /// <summary>The skill queue.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public EveServiceResponse<IEnumerable<QueuedSkill>> SkillQueue(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(SkillQueueAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>The skill queue.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<QueuedSkill>>> SkillQueueAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/SkillQueue.xml.aspx";
            const string CacheKeyFormat = "SkillQueue{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessSkillQueueResponse);
        }

        /// <summary>The npc standings.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<NpcStanding>> NPCStandings(string keyId, string vCode, long characterId,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(NPCStandingsAsync, keyId, vCode, characterId, responseMode);
        }

        /// <summary>The standings.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<NpcStanding>>> NPCStandingsAsync(string keyId, string vCode,
            long characterId, ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);

            const string MethodPath = "{0}/Standings.xml.aspx";
            const string CacheKeyFormat = "CharacterNpcStandings{0}_{1}";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), null,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessStandingsResponse);
        }

        // TODO: Wallet methods require support for wallet divisions

        /// <summary>The wallet journal.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="accountKey">The account key.</param>
        /// <param name="fromId">The from id.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<WalletJournalEntry>> WalletJournal(
            string keyId,
            string vCode,
            long characterId,
            int accountKey,
            long? fromId = null,
            int? rowCount = null,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(WalletJournalAsync, keyId, vCode, characterId, accountKey, fromId, rowCount,
                responseMode);
        }

        /// <summary>The wallet journal.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="accountKey"></param>
        /// <param name="fromId">The from id.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<WalletJournalEntry>>> WalletJournalAsync(
            string keyId,
            string vCode,
            long characterId,
            int accountKey,
            long? fromId = null,
            int? rowCount = null,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);
            Guard.Ensure(rowCount == null || rowCount.Value > 0);

            const string MethodPath = "{0}/WalletJournal.xml.aspx";
            const string CacheKeyFormat = "WalletJournal{0}_{1}";

            const string FromId = "fromID";
            const string RowCount = "rowCount";
            const string AccountKey = "accountKey";

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId);
            IDictionary<string, string> apiParams = new Dictionary<string, string>();

            apiParams[AccountKey] = accountKey.ToInvariantString();

            if (fromId != null)
            {
                apiParams[FromId] = fromId.Value.ToInvariantString();
            }

            if (rowCount != null)
            {
                apiParams[RowCount] = rowCount.Value.ToInvariantString();
            }

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), apiParams,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessWalletJournalResponse);
        }

        /// <summary>The wallet transactions.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="accountKey">The account key.</param>
        /// <param name="fromId">The from id.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="responseMode">The response mode.</param>
        /// <returns></returns>
        public EveServiceResponse<IEnumerable<WalletTransaction>> WalletTransactions(
            string keyId,
            string vCode,
            long characterId,
            int accountKey,
            long? fromId = null,
            int? rowCount = null,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            return RunAsyncMethod(WalletTransactionsAsync, keyId, vCode, characterId, accountKey, fromId, rowCount,
                responseMode);
        }

        /// <summary>The wallet transactions.</summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="vCode">The v code.</param>
        /// <param name="characterId">The character id.</param>
        /// <param name="accountKey"></param>
        /// <param name="fromId">The from id.</param>
        /// <param name="rowCount">The row count.</param>
        /// <param name="responseMode">The response Mode.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public Task<EveServiceResponse<IEnumerable<WalletTransaction>>> WalletTransactionsAsync(
            string keyId,
            string vCode,
            long characterId,
            int accountKey,
            long? fromId = null,
            int? rowCount = null,
            ResponseMode responseMode = ResponseMode.Normal)
        {
            Guard.Ensure(!keyId.IsNullOrWhiteSpace());
            Guard.Ensure(!vCode.IsNullOrWhiteSpace());
            Guard.Ensure(characterId > 0);
            Guard.Ensure(rowCount == null || rowCount.Value > 0);

            const string MethodPath = "{0}/WalletTransactions.xml.aspx";
            const string CacheKeyFormat = "WalletTransactions{0}_{1}{2}{3}{4}";

            const string FromId = "fromID";
            const string RowCount = "rowCount";
            const string AccountKey = "accountKey";


            IDictionary<string, string> apiParams = new Dictionary<string, string>();

            apiParams[AccountKey] = accountKey.ToInvariantString();

            if (fromId != null)
            {
                apiParams[FromId] = fromId.Value.ToInvariantString();
            }

            if (rowCount != null)
            {
                apiParams[RowCount] = rowCount.Value.ToInvariantString();
            }

            string cacheKey = CacheKeyFormat.FormatInvariant(keyId, characterId, accountKey,
                fromId.HasValue ? fromId.Value.ToInvariantString() : string.Empty,
                rowCount.HasValue ? rowCount.Value.ToInvariantString() : string.Empty);

            return GetServiceResponseAsync(keyId, vCode, characterId, MethodPath.FormatInvariant(PathPrefix), apiParams,
                cacheKey, ApiConstants.SixtyMinuteCache, responseMode, ProcessWalletTransctionResponse);
        }

        /// <summary>The process wallet journal response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The collection of wallet entries.</returns>
        private static IEnumerable<WalletTransaction> ProcessWalletTransctionResponse(XElement result)
        {
            if (result == null)
            {
                return new WalletTransaction[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let date = row.TryAttribute("transactionDateTime").Value.ToDateTimeOffset(0)
                let transactionId = row.TryAttribute("transactionID").Value.ToInt64()
                let quantity = row.TryAttribute("quantity").Value.ToInt32()
                let typeName = row.TryAttribute("typeName").Value
                let typeId = row.TryAttribute("typeID").Value.ToInt32()
                let price = row.TryAttribute("price").Value.ToDouble()
                let clientId = row.TryAttribute("clientID").Value.ToInt32()
                let clientName = row.TryAttribute("clientName").Value
                let stationId = row.TryAttribute("stationID").Value.ToInt32()
                let stationNane = row.TryAttribute("stationName").Value
                let transactionType = row.TryAttribute("transactionType").Value
                let transactionFor = row.TryAttribute("transactionFor").Value
                let journalEntryId = row.TryAttribute("journalTransactionID").Value.ToInt64()
                select
                    new WalletTransaction
                    {
                        ClientId = clientId,
                        ClientName = clientName,
                        Price = price,
                        Quantity = quantity,
                        StationId = stationId,
                        StationName = stationNane,
                        TransactionDateTime = date,
                        TransactionFor = transactionFor,
                        TransactionId = transactionId,
                        TransactionType = transactionType,
                        TypeId = typeId,
                        TypeName = typeName,
                        WalletJournalEntryId = journalEntryId
                    };
        }

        /// <summary>The process wallet journal response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The collection of wallet entries.</returns>
        private static IEnumerable<WalletJournalEntry> ProcessWalletJournalResponse(XElement result)
        {
            if (result == null)
            {
                return new WalletJournalEntry[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let date = row.TryAttribute("date").Value.ToDateTimeOffset(0)
                let refId = row.TryAttribute("refID").Value.ToInt64()
                let refType = row.TryAttribute("refTypeID").Value.ToInt32()
                let firstName = row.TryAttribute("ownerName1").Value
                let firstId = row.TryAttribute("ownerID1").Value.ToInt32()
                let secondName = row.TryAttribute("ownerName2").Value
                let secondId = row.TryAttribute("ownerID2").Value.ToInt32()
                let argName = row.TryAttribute("argName1").Value
                let argId = row.TryAttribute("argID1").Value.ToInt32()
                let amount = row.TryAttribute("amount").Value.ToDouble()
                let balance = row.TryAttribute("balance").Value.ToDouble()
                let reason = row.TryAttribute("reason").Value
                let taxReceiverId = row.TryAttribute("taxReceiverID").Value.ToInt32()
                let taxAmount = row.TryAttribute("taxAmount").Value.ToDouble()
                select
                    new WalletJournalEntry
                    {
                        Amount = amount,
                        ArgumentId = argId,
                        ArgumentName = argName,
                        Balance = balance,
                        Date = date,
                        FirstPartyId = firstId,
                        FirstPartyName = firstName,
                        Reason = reason,
                        ReferenceType = refType,
                        RefId = refId,
                        SecondPartyId = secondId,
                        SecondPartyName = secondName,
                        TaxAmount = taxAmount,
                        TaxReceiverId = taxReceiverId
                    };
        }

        /// <summary>The process standings response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The collection of NPC standings</returns>
        private static IEnumerable<NpcStanding> ProcessStandingsResponse(XElement result)
        {
            if (result == null)
            {
                return new NpcStanding[0]; // empty collection
            }

            return from standings in result.Elements("characterNPCStandings")
                from rowset in standings.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let type = rowset.Attribute("name").Value.ToEnum<NpcType>()
                let fromId = row.Attribute("fromID").Value.ToInt32()
                let fromName = row.Attribute("fromName").Value
                let standing = row.Attribute("standing").Value.ToDouble()
                select new NpcStanding {FromId = fromId, FromName = fromName, Kind = type, Standing = standing};
        }

        /// <summary>The process skill queue response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The collection of skills in queue</returns>
        public static IEnumerable<QueuedSkill> ProcessSkillQueueResponse(XElement result)
        {
            if (result == null)
            {
                return new QueuedSkill[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let position = row.Attribute("queuePosition").Value.ToInt32()
                let type = row.Attribute("typeID").Value.ToInt32()
                let level = row.Attribute("level").Value.ToInt32()
                let startSP = row.Attribute("startSP").Value.ToInt32()
                let endSP = row.Attribute("endSP").Value.ToInt32()
                let startTime = row.Attribute("startTime").Value.ToDateTimeOffset(0)
                let endTime = row.Attribute("endTime").Value.ToDateTimeOffset(0)
                select
                    new QueuedSkill
                    {
                        EndSP = endSP,
                        EndTime = endTime,
                        Level = level,
                        QueuePosition = position,
                        StartSP = startSP,
                        StartTime = startTime,
                        TypeId = type
                    };
        }

        /// <summary>The process skill in training response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The <see cref="EveApi.SkillInTraining" />.</returns>
        private static SkillInTraining ProcessSkillInTrainingResponse(XElement result)
        {
            if (result == null)
            {
                return null; // empty
            }

            var skill = new SkillInTraining();

            skill.IsTraining = result.Element("skillInTraining").Value.ToInt32() == 1;

            if (skill.IsTraining)
            {
                skill.TrainingEndTime = result.Element("trainingEndTime").Value.ToDateTimeOffset(0);
                skill.TrainingStartTime = result.Element("trainingStartTime").Value.ToDateTimeOffset(0);
                skill.CurrentTQTime = result.Element("currentTQTime").Value.ToDateTimeOffset(0);
                skill.TrainingTypeId = result.Element("trainingTypeID").Value.ToInt32();
                skill.TrainingStartSP = result.Element("trainingStartSP").Value.ToInt32();
                skill.TrainingEndSP = result.Element("trainingDestinationSP").Value.ToInt32();
                skill.TrainingToLevel = result.Element("trainingToLevel").Value.ToInt32();
            }

            return skill;
        }

        /// <summary>The process research response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The collection of research.</returns>
        private static IEnumerable<Research> ProcessResearchResponse(XElement result)
        {
            if (result == null)
            {
                return new Research[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let agent = row.Attribute("agentID").Value.ToInt32()
                let skillType = row.Attribute("skillTypeID").Value.ToInt32()
                let startDate = row.Attribute("researchStartDate").Value.ToDateTimeOffset(0)
                let pointsPerDay = row.Attribute("pointsPerDay").Value.ToDouble()
                let pointsRemaining = row.Attribute("remainderPoints").Value.ToDouble()
                select
                    new Research
                    {
                        AgentId = agent,
                        PointsPerDay = pointsPerDay,
                        RemainingPoints = pointsRemaining,
                        ResearchStartDate = startDate,
                        SkillTypeId = skillType
                    };
        }

        /// <summary>Processes the notifications response.</summary>
        /// <param name="result"></param>
        /// <returns>The collection of notifications.</returns>
        private static IEnumerable<ContractBid> ProcessContractBidsResponse(XElement result)
        {
            if (result == null)
            {
                return new ContractBid[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let bidId = row.Attribute("bidID").Value.ToInt64()
                let contractId = row.Attribute("contractID").Value.ToInt64()
                let bidder = row.Attribute("bidderID").Value.ToInt64()
                let date = row.Attribute("dateBid").Value.ToDateTimeOffset(0)
                let amount = row.Attribute("amount").Value.ToDouble()
                select
                    new ContractBid
                    {
                        Amount = amount,
                        BidDateTime = date,
                        BidderId = bidder,
                        BidId = bidId,
                        ContractId = contractId
                    };
        }

        /// <summary>Processes the medals xml</summary>
        /// <param name="result">xml data to process.</param>
        /// <returns>a collection of Medals</returns>
        private static IEnumerable<Medal> ProcessMedalsResponse(XElement result)
        {
            if (result == null)
            {
                return new Medal[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let medalId = row.Attribute("medalID").Value.ToInt32()
                let reason = row.Attribute("reason").Value
                let status = row.Attribute("status").Value
                let issuerId = row.Attribute("issuerID").Value.ToInt32()
                let issued = row.Attribute("issued").Value.ToDateTimeOffset(0)
                let corpId = row.Attribute("corporationID") != null ? row.Attribute("corporationID").Value.ToInt32() : 0
                let title = row.Attribute("title") != null ? row.Attribute("title").Value : string.Empty
                let description =
                    row.Attribute("description") != null ? row.Attribute("description").Value : string.Empty
                select
                    new Medal
                    {
                        MedalId = medalId,
                        Reason = reason,
                        Status = status,
                        IssuerId = issuerId,
                        DateIssued = issued,
                        CorporationId = corpId,
                        Title = title,
                        Description = description
                    };
        }

        /// <summary>Processes the market order xml into objects.</summary>
        /// <param name="result">xml to process</param>
        /// <returns>Market orders.</returns>
        private static IEnumerable<MarketOrder> ProcessMarketOrdersResponse(XElement result)
        {
            if (result == null)
            {
                return new MarketOrder[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let orderId = row.Attribute("orderID").Value.ToInt32()
                let charId = row.Attribute("charID").Value.ToInt32()
                let stationId = row.Attribute("stationID").Value.ToInt32()
                let quantityEntered = row.Attribute("volEntered").Value.ToInt32()
                let quantityRemaining = row.Attribute("volRemaining").Value.ToInt32()
                let minVolumn = row.Attribute("minVolume").Value.ToInt32()
                let orderState =
                    (MarketOrderState) Enum.Parse(typeof(MarketOrderState), row.Attribute("orderState").Value)
                let typeId = row.Attribute("typeID").Value.ToInt32()
                let range = row.Attribute("range").Value.ToInt32()
                let accountKey = row.Attribute("accountKey").Value.ToInt32()
                let duration = TimeSpan.FromDays(row.Attribute("duration").Value.ToInt32())
                let escrow = row.Attribute("escrow").Value.ToDouble()
                let price = row.Attribute("price").Value.ToDouble()
                let isBuyOrder = row.Attribute("bid").Value.ToBoolean()
                let dateIssued = row.Attribute("issued").Value.ToDateTimeOffset(0)
                select
                    new MarketOrder
                    {
                        OrderId = orderId,
                        CharId = charId,
                        StationId = stationId,
                        QuantityEntered = quantityEntered,
                        QuantityRemaining = quantityRemaining,
                        MinQuantity = minVolumn,
                        OrderState = orderState,
                        TypeId = typeId,
                        Range = range,
                        AccountKey = accountKey,
                        Duration = duration,
                        Escrow = escrow,
                        Price = price,
                        IsBuyOrder = isBuyOrder,
                        DateIssued = dateIssued
                    };
        }

        /// <summary>Processes the xml response from the web service for the industry jobs method.</summary>
        /// <param name="result">xml from the web service.</param>
        /// <returns>a collection of industry jobs</returns>
        private static IEnumerable<IndustryJob> ParseIndustryJobsResponse(XElement result)
        {
            if (result == null)
            {
                return new IndustryJob[0]; // empty collection
            }

            IEnumerable<IndustryJob> jobs = from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let jobId = row.Attribute("jobID").Value.ToInt64()
                let installerId = row.Attribute("installerID").Value.ToInt32()
                let installerName = row.Attribute("installerName").Value
                let facilityId = row.Attribute("facilityID").Value.ToInt32()
                let solarSystemId = row.Attribute("solarSystemID").Value.ToInt32()
                let solarSystemName = row.Attribute("solarSystemName").Value
                let stationId = row.Attribute("stationID").Value.ToInt32()
                let activityId = row.Attribute("activityID").Value.ToInt32()
                let blueprintId = row.Attribute("blueprintID").Value.ToInt64()
                let blueprintTypeId = row.Attribute("blueprintTypeID").Value.ToInt32()
                let blueprintTypeName = row.Attribute("blueprintTypeName").Value
                let blueprintLocationId = row.Attribute("blueprintLocationID").Value.ToInt32()
                let outputLocationId = row.Attribute("outputLocationID").Value.ToInt32()
                let runs = row.Attribute("runs").Value.ToInt32()
                let cost = row.Attribute("cost").Value.ToDouble()
                let teamId = row.Attribute("teamID").Value.ToInt32()
                let licensedRuns = row.Attribute("licensedRuns").Value.ToInt32()
                let probability = row.Attribute("probability").Value.ToDouble()
                let productTypeId = row.Attribute("productTypeID").Value.ToInt32()
                let productTypeName = row.Attribute("productTypeName").Value
                let status = (IndustryJobStatus)row.Attribute("status").Value.ToInt32()
                let timeInSeconds = row.Attribute("timeInSeconds").Value.ToInt32()
                let startDate = row.Attribute("startDate").Value.ToDateTimeOffset(0)
                let endDate = row.Attribute("endDate").Value.ToDateTimeOffset(0)
                let pauseDate = row.Attribute("pauseDate").Value.ToDateTimeOffset(0)
                let completedDate = row.Attribute("completedDate").Value.ToDateTimeOffset(0)
                let completedCharacterId = row.Attribute("completedCharacterID").Value.ToInt32()
                select
                    new IndustryJob
                    {
                        JobId = jobId,
                        InstallerId = installerId,
                        InstallerName = installerName,
                        FacilityId = facilityId,
                        SolarSystemId = solarSystemId,
                        SolarSystemName = solarSystemName,
                        StationId = stationId,
                        ActivityId = activityId,
                        BlueprintId = blueprintId,
                        BlueprintTypeId = blueprintTypeId,
                        BlueprintTypeName = blueprintTypeName,
                        BlueprintLocationId = blueprintLocationId,
                        OutputLocationId = outputLocationId,
                        Runs = runs,
                        Cost = cost, 
                        TeamId = teamId,
                        LicensedRuns = licensedRuns,
                        Probability = probability,
                        ProductTypeId = productTypeId,
                        ProductTypeName = productTypeName,
                        Status = status,
                        TimeInSeconds = timeInSeconds,
                        StartDate = startDate,
                        EndDate = endDate,
                        PauseDate = pauseDate,
                        CompletedDate = completedDate,
                        CompletedCharacterId = completedCharacterId
                    };

            return jobs;
        }

        /// <summary>Parses the factional warfare data.</summary>
        /// <param name="result">xml data</param>
        /// <returns>FactionalWarfareStats object.</returns>
        private static FactionalWarfareStats ParseFacWarfareResponse(XElement result)
        {
            if (result == null)
            {
                return null;
            }

            // Suppressing Null checks for xml parsing as exceptions should be thrown if xml structure is not correct. Base class will catch these.
            // ReSharper disable PossibleNullReferenceException
            int factionId = result.Element("factionID").Value.ToInt32();
            string factionName = result.Element("factionName").Value;
            DateTimeOffset enlisted = result.Element("enlisted").Value.ToDateTimeOffset(0);
            int currentRank = result.Element("currentRank").Value.ToInt32();
            int highestRank = result.Element("highestRank").Value.ToInt32();
            int killsYesterday = result.Element("killsYesterday").Value.ToInt32();
            int killsLastWeek = result.Element("killsLastWeek").Value.ToInt32();
            int killsTotal = result.Element("killsTotal").Value.ToInt32();
            int victoryPointsYesterday = result.Element("victoryPointsYesterday").Value.ToInt32();
            int victoryPointsLastWeek = result.Element("victoryPointsLastWeek").Value.ToInt32();
            int victoryPointsTotal = result.Element("victoryPointsTotal").Value.ToInt32();

            // ReSharper restore PossibleNullReferenceException
            return new FactionalWarfareStats
            {
                FactionId = factionId,
                FactionName = factionName,
                Enlisted = enlisted,
                CurrentRank = currentRank,
                HighestRank = highestRank,
                KillsYesterday = killsYesterday,
                KillsLastWeek = killsLastWeek,
                KillsTotal = killsTotal,
                VictoryPointsYesterday = victoryPointsYesterday,
                VictoryPointsLastWeek = victoryPointsLastWeek,
                VictoryPointsTotal = victoryPointsTotal
            };
        }

        /// <summary>Processes the response for the Contracts method.</summary>
        /// <param name="result">XML data to process.</param>
        /// <returns>A collection of contract objects.</returns>
        private static IEnumerable<ContractItem> ParseContractItemResponse(XElement result)
        {
            if (result == null)
            {
                return new ContractItem[0]; // return empty collection... no data.
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let record = row.Attribute("recordID").Value.ToInt64()
                let type = row.Attribute("typeID").Value.ToInt32()
                let quantity = row.Attribute("quantity").Value.ToInt64()
                let rawQuantity =
                    row.Attribute("rawQuantity") != null ? row.Attribute("rawQuantity").Value.ToInt32() : -1
                let single = row.Attribute("singleton").Value.ToBoolean()
                let included = row.Attribute("included").Value.ToBoolean()
                select
                    new ContractItem
                    {
                        IsIncluded = included,
                        IsSingleton = single,
                        Quantity = quantity,
                        RawQuantity = rawQuantity,
                        RecordId = record,
                        TypeId = type
                    };
        }

        /// <summary>Processes the response for the Contracts method.</summary>
        /// <param name="result">XML data to process.</param>
        /// <returns>A collection of contract objects.</returns>
        private static IEnumerable<Contract> ParseContractResponse(XElement result)
        {
            if (result == null)
            {
                return new Contract[0]; // return empty collection... no data.
            }

            ContractType tempType;
            ContractStatus tempStatus;
            ContractAvailability tempAvail;
            IEnumerable<Contract> contracts = from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let contractId = row.TryAttribute("contractID").Value.ToInt64()
                let issuerId = row.TryAttribute("issuerID").Value.ToInt32()
                let issuerCorpId = row.TryAttribute("issuerCorpID").Value.ToInt32()
                let assigneeId = row.TryAttribute("assigneeID").Value.ToInt32()
                let acceptorId = row.TryAttribute("acceptorID").Value.ToInt32()
                let startStationId = row.TryAttribute("startStationID").Value.ToInt32()
                let endStationId = row.TryAttribute("endStationID").Value.ToInt32()
                let type = Enum.TryParse(row.TryAttribute("type").Value, out tempType) ? tempType : ContractType.Unknown
                let status =
                    Enum.TryParse(row.TryAttribute("status").Value, out tempStatus)
                        ? tempStatus
                        : ContractStatus.Unknown
                let title = row.Attribute("title").Value
                let forCorp = int.Parse(row.TryAttribute("forCorp").Value) == 1
                let availability =
                    Enum.TryParse(row.TryAttribute("availability").Value, out tempAvail)
                        ? tempAvail
                        : ContractAvailability.Unknown
                let dateIssued = row.TryAttribute("dateIssued").Value.ToDateTimeOffset(0)
                let dateExpired = row.TryAttribute("dateExpired").Value.ToDateTimeOffset(0)
                let dateAccepted = row.TryAttribute("dateAccepted").Value.ToDateTimeOffset(0)
                let dateCompleted = row.TryAttribute("dateCompleted").Value.ToDateTimeOffset(0)
                let numDate = row.TryAttribute("numDays").Value.ToInt32()
                let price = row.TryAttribute("price").Value.ToDouble()
                let reward = row.TryAttribute("reward").Value.ToDouble()
                let collateral = row.TryAttribute("collateral").Value.ToDouble()
                let buyout = row.TryAttribute("buyout").Value.ToDouble()
                let volume = row.TryAttribute("volume").Value.ToDouble()
                select
                    new Contract
                    {
                        ContractId = contractId,
                        IssuerId = issuerId,
                        IssuserCorpId = issuerCorpId,
                        AssigneeId = assigneeId,
                        AcceptorId = acceptorId,
                        StartStationId = startStationId,
                        EndStationId = endStationId,
                        Type = type,
                        Status = status,
                        Title = title,
                        ForCorp = forCorp,
                        Availability = availability,
                        DateIssued = dateIssued,
                        DateExpired = dateExpired,
                        DateAccepted = dateAccepted,
                        NumberOfDays = numDate,
                        DateCompleted = dateCompleted,
                        Price = price,
                        Reward = reward,
                        Collateral = collateral,
                        Buyout = buyout,
                        Volume = volume
                    };

            return contracts;
        }

        /// <summary>Parses the xml data response from the Eve web service's ContactNotifications method.</summary>
        /// <param name="result">The xml data to process</param>
        /// <returns>the collection of contact notifications.</returns>
        private static IEnumerable<ContactNotification> ParseContactNotificationResponse(XElement result)
        {
            if (result == null)
            {
                return new ContactNotification[0]; // return empty collection... no data.
            }

            IEnumerable<ContactNotification> notifications = from rowset in result.Elements(ApiConstants.Rowset)
                from row in rowset.Elements(ApiConstants.Row)
                let notificationId = int.Parse(row.Attribute("notificationID").Value)
                let senderId = int.Parse(row.Attribute("senderID").Value)
                let senderName = row.Attribute("senderName").Value
                let sentDate =
                    new DateTimeOffset(DateTime.Parse(row.Attribute("sentDate").Value, CultureInfo.InvariantCulture),
                        TimeSpan.Zero)
                let messageData = row.Attribute("messageData").Value
                select
                    new ContactNotification
                    {
                        NotificationId = notificationId,
                        SenderId = senderId,
                        SenderName = senderName,
                        SentDate = sentDate,
                        MessageData = messageData
                    };
            return notifications;
        }

        /// <summary>Parses the xml data response from the Eve web service's ContactList method.</summary>
        /// <param name="result">The xml data to process</param>
        /// <returns>the collection of contact lists.</returns>
        private static IEnumerable<Contact> ParseContactListResponse(XElement result)
        {
            if (result == null)
            {
                return new Contact[0]; // return empty collection... no data.
            }

            // convert the xml into collections of objects.
            // only the personal list has the "inWatchlist" attribute...others defaulted to false    
            return from rowset in result.Elements(ApiConstants.Rowset)
                where rowset.Attribute("columns").Value.Contains("contactID")
                from row in rowset.Elements(ApiConstants.Row)
                let contactId = int.Parse(row.Attribute("contactID").Value)
                let contactName = row.Attribute("contactName").Value
                let isInWatchList =
                    row.Attributes("inWatchlist").Any() && bool.Parse(row.Attribute("inWatchlist").Value)
                let standing = row.Attribute("standing").Value.ToDouble()
                let kind = rowset.Attribute("name").Value.ToEnum<ContactType>()
                select
                    new Contact
                    {
                        ContactId = contactId,
                        ContactType = kind,
                        ContactName = contactName,
                        IsInWatchList = isInWatchList,
                        Standing = standing
                    };
        }

        /// <summary>Parses the asset list response xml into C# objects.</summary>
        /// <param name="results">the xml result element.</param>
        /// <returns>a collection of items.</returns>
        private static IEnumerable<AssetItem> ParseAssetListResponse(XElement results)
        {
            XElement rowset = results.Element(ApiConstants.Rowset);

            if (rowset == null)
            {
                return new AssetItem[0]; // return empty collection
            }

            return rowset.Elements(ApiConstants.Row).Select(row => CreateItemFromRow(row, 0));
        }

        /// <summary>Parses the response from AccountBalance API.</summary>
        /// <param name="results">XML results from the service</param>
        /// <returns>a collection of balances.</returns>
        private static IEnumerable<AccountBalance> ParseBalanceResponse(XElement results)
        {
            XElement rowset = results.Element(ApiConstants.Rowset);

            if (rowset == null)
            {
                return new AccountBalance[0]; // return empty collection
            }

            return rowset.Elements().Select(
                row =>
                {
                    int accountId = row.Attribute(ApiConstants.AccountId).Value.ToInt32();
                    int accountKey = row.Attribute(ApiConstants.AccountKey).Value.ToInt32();
                    double balance = row.Attribute(ApiConstants.Balance).Value.ToDouble();
                    return new AccountBalance {AccountId = accountId, AccountKey = accountKey, Balance = balance};
                });
        }

        /// <summary>Creates a single AssetItem from xml.</summary>
        /// <param name="row">row describing the item.</param>
        /// <param name="parentId">parent id if applicable, otherwise 0.</param>
        /// <returns>an AssetItem.</returns>
        private static AssetItem CreateItemFromRow(XElement row, long parentId)
        {
            long itemId, quantity, rawQuantity;
            int typeId, locationId, flag;
            bool single;

            XAttribute item = row.Attribute(ApiConstants.ItemId);
            XAttribute location = row.Attribute(ApiConstants.LocationId);
            XAttribute type = row.Attribute(ApiConstants.TypeId);
            XAttribute count = row.Attribute(ApiConstants.Quantity);
            XAttribute rawCount = row.Attribute(ApiConstants.RawQuantity);
            XAttribute flagAttrib = row.Attribute(ApiConstants.Flag);
            XAttribute singleton = row.Attribute(ApiConstants.Singleton);

            itemId = item != null ? item.Value.ToInt64() : 0;
            locationId = location != null ? location.Value.ToInt32() : 0;
            typeId = type != null ? type.Value.ToInt32() : 0;
            quantity = count != null ? count.Value.ToInt64() : 0;
            rawQuantity = rawCount != null ? rawCount.Value.ToInt64() : 0;
            flag = flagAttrib != null ? flagAttrib.Value.ToInt32() : 0;
            single = singleton != null && singleton.Value.ToBoolean();

            // check to see if there are children.
            XElement childRowset = row.Element(ApiConstants.Rowset);
            IEnumerable<AssetItem> children = null;
            if (childRowset != null)
            {
                children = childRowset.Elements(ApiConstants.Row).Select(rowItem => CreateItemFromRow(rowItem, itemId));
            }

            return new AssetItem
            {
                ItemId = itemId,
                LocationId = locationId,
                TypeId = typeId,
                Quantity = quantity,
                RawQuantity = rawQuantity,
                Flag = flag,
                Singleton = single,
                Contents = children,
                ParentItemId = parentId
            };
        }

        private static IEnumerable<Entities.Killmail.KillMail> ProcessKillMailResponse(XElement result)
        {
            if (result == null)
            {
                return new Entities.Killmail.KillMail[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                   from row in rowset.Elements(ApiConstants.Row)
                   let killID = Convert.ToUInt32(row.Attribute("killID").Value)
                   let solarSystemID = Convert.ToUInt32(row.Attribute("solarSystemID").Value)
                   let killTime = DateTimeOffset.Parse(row.Attribute("killTime").Value)
                   let moonID = Convert.ToUInt32(row.Attribute("moonID").Value)
                   let victim = ParseKillMailVictim(row.Element(ApiConstants.Victim))
                   let attackers = ParseKillMailAttackers((from x in row.Elements(ApiConstants.Rowset) where x.Attribute("name").Value == ApiConstants.Attackers select x).First())
                   let items = ParseKillMailItems((from x in row.Elements(ApiConstants.Rowset) where x.Attribute("name").Value == ApiConstants.Items select x).First())
                   select new Entities.Killmail.KillMail
                   { KillID = killID, SolarSystemID = solarSystemID, KillTime = killTime, MoonID = moonID, Victim = victim, Attackers = attackers, Items = items };
                   }

        private static Entities.Killmail.Victim ParseKillMailVictim(XElement result)
        {
            if (result == null)
            {
                return new Entities.Killmail.Victim();
            }

            return new Entities.Killmail.Victim
            {
                CharacterID = Convert.ToUInt32(result.Attribute("characterID").Value),
                CharacterName = result.Attribute("characterName").Value,
                CorporationID = Convert.ToUInt32(result.Attribute("corporationID").Value),
                CorporationName = result.Attribute("corporationName").Value,
                AllianceID = Convert.ToUInt32(result.Attribute("allianceID").Value),
                AllianceName = result.Attribute("allianceName").Value,
                FactionID = Convert.ToUInt32(result.Attribute("factionID").Value),
                FactionName = result.Attribute("factionName").Value,
                DamageTaken = Convert.ToUInt32(result.Attribute("damageTaken").Value),
                ShipTypeID = Convert.ToUInt32(result.Attribute("shipTypeID").Value),
                X = result.Attribute("x").Value.ToDouble(),
                Y = result.Attribute("y").Value.ToDouble(),
                Z = result.Attribute("z").Value.ToDouble()
            };
        }

        private static IEnumerable<Entities.Killmail.Attacker> ParseKillMailAttackers(XElement rowset)
        {
            if (rowset == null)
                return new Entities.Killmail.Attacker[0];

            return from row in rowset.Elements(ApiConstants.Row)
                   let characterId = Convert.ToUInt32(row.Attribute("characterID").Value)
                   let characterName = row.Attribute("characterName").Value
                   let corporationId = Convert.ToUInt32(row.Attribute("corporationID").Value)
                   let corporationName = row.Attribute("corporationName").Value
                   let allianceId = Convert.ToUInt32(row.Attribute("allianceID").Value)
                   let allianceName = row.Attribute("allianceName").Value
                   let factionId = Convert.ToUInt32(row.Attribute("factionID").Value)
                   let factionName = row.Attribute("factionName").Value
                   let securityStatus = row.Attribute("securityStatus").Value.ToDouble()
                   let damageDone = Convert.ToUInt32(row.Attribute("damageDone").Value)
                   let finalBlow = row.Attribute("finalBlow").Value.ToBoolean()
                   let weaponTypeId = Convert.ToUInt32(row.Attribute("weaponTypeID").Value)
                   let shipTypeId = Convert.ToUInt32(row.Attribute("shipTypeID").Value)
                   select new Entities.Killmail.Attacker
                   {
                       CharacterID = characterId,
                       CharacterName = characterName,
                       CorporationID = corporationId,
                       CorporationName = corporationName,
                       AllianceID = allianceId,
                       AllianceName = allianceName,
                       FactionID = factionId,
                       FactionName = factionName,
                       SecurityStatus = securityStatus,
                       DamageDone = damageDone,
                       FinalBlow = finalBlow,
                       WeaponTypeID = weaponTypeId,
                       ShipTypeID = shipTypeId
                   };
        }

        private static IEnumerable<Entities.Killmail.KillItem> ParseKillMailItems(XElement rowset)
        {
            if (rowset == null)
                return new Entities.Killmail.KillItem[0];

            return from row in rowset.Elements(ApiConstants.Row)
                   let typeId = Convert.ToUInt32(row.Attribute("typeID").Value)
                   let flag = Convert.ToUInt32(row.Attribute("flag").Value)
                   let qtyDropped = Convert.ToUInt32(row.Attribute("qtyDropped").Value)
                   let qtyDestroyed = Convert.ToUInt32(row.Attribute("qtyDestroyed").Value)
                   let singleton = row.Attribute("singleton").Value.ToInt32()
                   select new Entities.Killmail.KillItem { TypeID = typeId, Flag = flag, QtyDropped = qtyDropped, QtyDestroyed = qtyDestroyed, Singleton = singleton };
        }

        private static IEnumerable<Entities.Blueprint> ProcessBlueprintsResponse(XElement result)
        {
            if (result == null)
            {
                return new Entities.Blueprint[0]; // empty collection
            }

            return from rowset in result.Elements(ApiConstants.Rowset)
                   from row in rowset.Elements(ApiConstants.Row)
                   let itemId = row.Attribute("itemID").Value.ToInt64()
                   let locationId = row.Attribute("locationID").Value.ToInt64()
                   let typeId = row.Attribute("typeID").Value.ToInt32()
                   let typeName = row.Attribute("typeName").Value
                   let quantity = row.Attribute("quantity").Value.ToInt64()
                   let flagId = row.Attribute("flagID").Value.ToInt64()
                   let timeEfficiency = row.Attribute("timeEfficiency").Value.ToInt64()
                   let materialEfficiency = row.Attribute("materialEfficiency").Value.ToInt64()
                   let runs = row.Attribute("runs").Value.ToInt64()
                   select new Entities.Blueprint { ItemID = itemId, LocationID = locationId, TypeID = typeId, TypeName = typeName, Quantity = quantity,
                       FlagID = flagId, TimeEfficiency = timeEfficiency, MaterialEfficiency = materialEfficiency, Runs = runs };
        }
    }
}