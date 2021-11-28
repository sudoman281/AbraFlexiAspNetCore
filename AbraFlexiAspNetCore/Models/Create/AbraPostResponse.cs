using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    /// <summary>
    /// Default Abra POST response wrapper
    /// </summary>
    public class AbraPostResponse
    {
        /// <summary>
        /// Response body
        /// </summary>
        [JsonProperty("winstrom")] public AbraPostResponseBody? Body { get; internal set; }
        /// <summary>
        /// Error during the request
        /// </summary>
        [JsonIgnore] public string? Error { get; internal set; }
    }

    /// <summary>
    /// Default Abra POST response body
    /// </summary>
    public class AbraPostResponseBody
    {
        /// <summary>
        /// Default Abra POST response body
        /// </summary>
        /// <param name="version">Version of the API</param>
        /// <param name="success">Whether the request was successful or not</param>
        /// <param name="stats">Stats of the request</param>
        /// <param name="results">Ids and urls of created/edited objects</param>
        public AbraPostResponseBody(string version, bool success, AbraPostResponseStats? stats,
            List<AbraPostResponseResult>? results)
        {
            Version = version;
            Success = success;
            Stats = stats;
            Results = results;
        }

        /// <summary>
        /// Version of the API
        /// </summary>
        [JsonProperty("@version")] public string Version { get; }
        /// <summary>
        /// Whether the request was successful or not
        /// </summary>
        [JsonProperty("success")] public bool Success { get; }
        /// <summary>
        /// Stats of the request
        /// </summary>
        [JsonProperty("stats")] public AbraPostResponseStats? Stats { get; }
        /// <summary>
        /// Ids and urls of created/edited objects
        /// </summary>
        [JsonProperty("results")] public List<AbraPostResponseResult>? Results { get; }
    }

    /// <summary>
    /// Stats about how many records were created, updated, etc.
    /// </summary>
    public class AbraPostResponseStats
    {
        /// <summary>
        /// Stats about how many records were created, updated, etc.
        /// </summary>
        /// <param name="created">Created records</param>
        /// <param name="updated">Updated records</param>
        /// <param name="deleted">Deleted records</param>
        /// <param name="skipped">Skipped records</param>
        /// <param name="failed">Failed records</param>
        public AbraPostResponseStats(int created, int updated, int deleted, int skipped, int failed)
        {
            Created = created;
            Updated = updated;
            Deleted = deleted;
            Skipped = skipped;
            Failed = failed;
        }

        /// <summary>
        /// Created records
        /// </summary>
        [JsonProperty("created")] public int Created { get; }
        /// <summary>
        /// Updated records
        /// </summary>
        [JsonProperty("updated")] public int Updated { get; }
        /// <summary>
        /// Deleted records
        /// </summary>
        [JsonProperty("deleted")] public int Deleted { get; }
        /// <summary>
        /// Skipped records
        /// </summary>
        [JsonProperty("skipped")] public int Skipped { get; }
        /// <summary>
        /// Failed records
        /// </summary>
        [JsonProperty("failed")] public int Failed { get; }
    }

    /// <summary>
    /// Created/updated records
    /// </summary>
    public class AbraPostResponseResult
    {
        /// <summary>
        /// Created/updated records
        /// </summary>
        /// <param name="id">Id of the record</param>
        /// <param name="url">Url of the records</param>
        public AbraPostResponseResult(int id, string url)
        {
            Id = id;
            Url = url;
        }

        /// <summary>
        /// Id of the record
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Url of the records
        /// </summary>
        [JsonProperty("ref")] public string Url { get; }
    }
}