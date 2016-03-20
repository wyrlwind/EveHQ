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

namespace EveHQ.NewEveApi.Entities
{
    /// <summary>The wallet journal entry.</summary>
    public class WalletJournalEntry
    {
        /// <summary>Gets or sets the date.</summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>Gets or sets the ref id.</summary>
        public long RefId { get; set; }

        /// <summary>Gets or sets the reference type.</summary>
        public int ReferenceType { get; set; }

        /// <summary>Gets or sets the first party name.</summary>
        public string FirstPartyName { get; set; }

        /// <summary>Gets or sets the first party id.</summary>
        public int FirstPartyId { get; set; }

        /// <summary>Gets or sets the second party name.</summary>
        public string SecondPartyName { get; set; }

        /// <summary>Gets or sets the second party id.</summary>
        public int SecondPartyId { get; set; }

        /// <summary>Gets or sets the argument name.</summary>
        public string ArgumentName { get; set; }

        /// <summary>Gets or sets the argument id.</summary>
        public int ArgumentId { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        public double Amount { get; set; }

        /// <summary>Gets or sets the balance.</summary>
        public double Balance { get; set; }

        /// <summary>Gets or sets the reason.</summary>
        public string Reason { get; set; }

        /// <summary>Gets or sets the tax receiver id.</summary>
        public int TaxReceiverId { get; set; }

        /// <summary>Gets or sets the tax amount.</summary>
        public double TaxAmount { get; set; }
    }
}