using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace BlazorApp.Shared
{
    public class BingAutoCompleteObject
    {
        [JsonPropertyName("authenticationResultCode")]
        public string AuthenticationResultCode { get; set; } = string.Empty;

        [JsonPropertyName("brandLogoUri")]
        public Uri BrandLogoUri { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; } = string.Empty;

        [JsonPropertyName("resourceSets")]
        public ResourceSet[] ResourceSets { get; set; }

        [JsonPropertyName("statusCode")]
        public long StatusCode { get; set; }

        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; } = string.Empty;

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; } = string.Empty;
    }

    public partial class ResourceSet
    {
        [JsonPropertyName("estimatedTotal")]
        public long EstimatedTotal { get; set; }

        [JsonPropertyName("resources")]
        public Resource[] Resources { get; set; }
    }

    public partial class Resource
    {
        [JsonPropertyName("__type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public Value[] Value { get; set; }
    }

    public partial class Value
    {
        [JsonPropertyName("__type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public partial class Address
    {
        [JsonPropertyName("countryRegion")]
        public string CountryRegion { get; set; } = string.Empty;

        [JsonPropertyName("locality")]
        public string Locality { get; set; } = string.Empty;

        [JsonPropertyName("adminDistrict")]
        public string AdminDistrict { get; set; } = string.Empty;

        [JsonPropertyName("countryRegionIso2")]
        public string CountryRegionIso2 { get; set; } = string.Empty;

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("addressLine")]
        public string AddressLine { get; set; } = string.Empty;

        [JsonPropertyName("formattedAddress")]
        public string FormattedAddress { get; set; } = string.Empty;
    }
    
}
