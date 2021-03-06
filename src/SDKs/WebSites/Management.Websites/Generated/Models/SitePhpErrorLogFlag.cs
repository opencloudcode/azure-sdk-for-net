// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.WebSites.Models
{
    using Azure;
    using Management;
    using WebSites;
    using Rest;
    using Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Used for getting PHP error logging flag.
    /// </summary>
    [JsonTransformation]
    public partial class SitePhpErrorLogFlag : Resource
    {
        /// <summary>
        /// Initializes a new instance of the SitePhpErrorLogFlag class.
        /// </summary>
        public SitePhpErrorLogFlag() { }

        /// <summary>
        /// Initializes a new instance of the SitePhpErrorLogFlag class.
        /// </summary>
        /// <param name="location">Resource Location.</param>
        /// <param name="id">Resource Id.</param>
        /// <param name="name">Resource Name.</param>
        /// <param name="kind">Kind of resource.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="tags">Resource tags.</param>
        /// <param name="localLogErrors">Local log_errors setting.</param>
        /// <param name="masterLogErrors">Master log_errors setting.</param>
        /// <param name="localLogErrorsMaxLength">Local log_errors_max_len
        /// setting.</param>
        /// <param name="masterLogErrorsMaxLength">Master log_errors_max_len
        /// setting.</param>
        public SitePhpErrorLogFlag(string location, string id = default(string), string name = default(string), string kind = default(string), string type = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>), string localLogErrors = default(string), string masterLogErrors = default(string), string localLogErrorsMaxLength = default(string), string masterLogErrorsMaxLength = default(string))
            : base(location, id, name, kind, type, tags)
        {
            LocalLogErrors = localLogErrors;
            MasterLogErrors = masterLogErrors;
            LocalLogErrorsMaxLength = localLogErrorsMaxLength;
            MasterLogErrorsMaxLength = masterLogErrorsMaxLength;
        }

        /// <summary>
        /// Gets or sets local log_errors setting.
        /// </summary>
        [JsonProperty(PropertyName = "properties.localLogErrors")]
        public string LocalLogErrors { get; set; }

        /// <summary>
        /// Gets or sets master log_errors setting.
        /// </summary>
        [JsonProperty(PropertyName = "properties.masterLogErrors")]
        public string MasterLogErrors { get; set; }

        /// <summary>
        /// Gets or sets local log_errors_max_len setting.
        /// </summary>
        [JsonProperty(PropertyName = "properties.localLogErrorsMaxLength")]
        public string LocalLogErrorsMaxLength { get; set; }

        /// <summary>
        /// Gets or sets master log_errors_max_len setting.
        /// </summary>
        [JsonProperty(PropertyName = "properties.masterLogErrorsMaxLength")]
        public string MasterLogErrorsMaxLength { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public override void Validate()
        {
            base.Validate();
        }
    }
}

