// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.DevTestLabs.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Properties for retargeting a virtual machine schedule.
    /// </summary>
    public partial class RetargetScheduleProperties
    {
        /// <summary>
        /// Initializes a new instance of the RetargetScheduleProperties class.
        /// </summary>
        public RetargetScheduleProperties() { }

        /// <summary>
        /// Initializes a new instance of the RetargetScheduleProperties class.
        /// </summary>
        public RetargetScheduleProperties(string currentResourceId = default(string), string targetResourceId = default(string))
        {
            CurrentResourceId = currentResourceId;
            TargetResourceId = targetResourceId;
        }

        /// <summary>
        /// The resource Id of the virtual machine on which the schedule
        /// operates
        /// </summary>
        [JsonProperty(PropertyName = "currentResourceId")]
        public string CurrentResourceId { get; set; }

        /// <summary>
        /// The resource Id of the virtual machine that the schedule should be
        /// retargeted to
        /// </summary>
        [JsonProperty(PropertyName = "targetResourceId")]
        public string TargetResourceId { get; set; }

    }
}