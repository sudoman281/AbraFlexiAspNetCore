using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    public class AbraPostResponse
    {
        [JsonProperty("winstrom")] public AbraPostResponseBody? Body { get; internal set; }
        [JsonIgnore] public string? Error { get; internal set; }
    }

    public class AbraPostResponseBody
    {
        public AbraPostResponseBody(string version, bool success, AbraPostResponseStats? stats,
            List<AbraPostResponseResult>? results)
        {
            Version = version;
            Success = success;
            Stats = stats;
            Results = results;
        }

        [JsonProperty("@version")] public string Version { get; }
        [JsonProperty("success")] public bool Success { get; }
        [JsonProperty("stats")] public AbraPostResponseStats? Stats { get; }
        [JsonProperty("results")] public List<AbraPostResponseResult>? Results { get; }
    }

    public class AbraPostResponseStats
    {
        public AbraPostResponseStats(int created, int updated, int deleted, int skipped, int failed)
        {
            Created = created;
            Updated = updated;
            Deleted = deleted;
            Skipped = skipped;
            Failed = failed;
        }

        [JsonProperty("created")] public int Created { get; }
        [JsonProperty("updated")] public int Updated { get; }
        [JsonProperty("deleted")] public int Deleted { get; }
        [JsonProperty("skipped")] public int Skipped { get; }
        [JsonProperty("failed")] public int Failed { get; }
    }

    public class AbraPostResponseResult
    {
        public AbraPostResponseResult(int id, string url)
        {
            Id = id;
            Url = url;
        }

        [JsonProperty("id")] public int Id { get; }
        [JsonProperty("ref")] public string Url { get; }
    }
}