namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Generic Abra response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    public class AbraResponse<T>
    {
        /// <summary>
        /// Generic Abra response
        /// </summary>
        /// <param name="successful">Whether the request was successful or not</param>
        /// <param name="data">Response data</param>
        public AbraResponse(bool successful, T? data = default)
        {
            Successful = successful;
            Data = data;
        }

        /// <summary>
        /// Whether the request was successful or not
        /// </summary>
        public bool Successful { get; }
        /// <summary>
        /// Response data
        /// </summary>
        public T? Data { get; }
    }
}