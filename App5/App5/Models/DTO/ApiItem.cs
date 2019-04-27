using Newtonsoft.Json;
using System;

namespace App5.Models.DTO
{
    public partial class ApiItem
    {
        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        [JsonProperty("queryResultType")]
        public string QueryResultType { get; set; }

        [JsonProperty("asOf")]
        public DateTimeOffset AsOf { get; set; }

        [JsonProperty("columns")]
        public Column[] Columns { get; set; }

        [JsonProperty("workItems")]
        public WorkItem[] WorkItems { get; set; }
    }

    public partial class Column
    {
        [JsonProperty("referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class WorkItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
