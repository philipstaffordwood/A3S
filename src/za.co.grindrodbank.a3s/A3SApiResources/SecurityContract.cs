/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
/*
 * A3S
 *
 * API Definition for the A3S. This service allows authentication, authorisation and accounting.
 *
 * The version of the OpenAPI document: 1.0.2
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using za.co.grindrodbank.a3s.Converters;

namespace za.co.grindrodbank.a3s.A3SApiResources
{ 
    /// <summary>
    /// Models a security contract definition. 
    /// </summary>
    [DataContract]
    public partial class SecurityContract : IEquatable<SecurityContract>
    { 
        /// <summary>
        /// Gets or Sets Clients
        /// </summary>
        [DataMember(Name="clients", EmitDefaultValue=false)]
        public List<Oauth2ClientSubmit> Clients { get; set; }

        /// <summary>
        /// Gets or Sets Applications
        /// </summary>
        [DataMember(Name="applications", EmitDefaultValue=false)]
        public List<SecurityContractApplication> Applications { get; set; }

        /// <summary>
        /// Gets or Sets DefaultConfigurations
        /// </summary>
        [DataMember(Name="defaultConfigurations", EmitDefaultValue=false)]
        public List<SecurityContractDefaultConfiguration> DefaultConfigurations { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SecurityContract {\n");
            sb.Append("  Clients: ").Append(Clients).Append("\n");
            sb.Append("  Applications: ").Append(Applications).Append("\n");
            sb.Append("  DefaultConfigurations: ").Append(DefaultConfigurations).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SecurityContract)obj);
        }

        /// <summary>
        /// Returns true if SecurityContract instances are equal
        /// </summary>
        /// <param name="other">Instance of SecurityContract to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SecurityContract other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Clients == other.Clients ||
                    Clients != null &&
                    other.Clients != null &&
                    Clients.SequenceEqual(other.Clients)
                ) && 
                (
                    Applications == other.Applications ||
                    Applications != null &&
                    other.Applications != null &&
                    Applications.SequenceEqual(other.Applications)
                ) && 
                (
                    DefaultConfigurations == other.DefaultConfigurations ||
                    DefaultConfigurations != null &&
                    other.DefaultConfigurations != null &&
                    DefaultConfigurations.SequenceEqual(other.DefaultConfigurations)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Clients != null)
                    hashCode = hashCode * 59 + Clients.GetHashCode();
                    if (Applications != null)
                    hashCode = hashCode * 59 + Applications.GetHashCode();
                    if (DefaultConfigurations != null)
                    hashCode = hashCode * 59 + DefaultConfigurations.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(SecurityContract left, SecurityContract right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SecurityContract left, SecurityContract right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
