using Newtonsoft.Json;

namespace CosmosTaskManager
{
    public class TaskItem
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>Partition key — format: yyyy-MM-dd</summary>
        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>Null until soft-deleted</summary>
        [JsonProperty("deletedDate")]
        public string? DeletedDate { get; set; }
    }
}