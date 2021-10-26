namespace AbraFlexiAspNetCore.Models
{
    public class AbraResponse<T>
    {
        public AbraResponse(bool successful, T? data = default)
        {
            Successful = successful;
            Data = data;
        }

        public bool Successful { get; }
        public T? Data { get; }
    }
}