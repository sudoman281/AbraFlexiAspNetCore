using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    /// <summary>
    /// Pair an income with an invoice
    /// </summary>
    public class CreateInvoiceReceivedPaymentPair
    {
        /// <summary>
        /// Pair an income with an invoice
        /// </summary>
        /// <param name="paymentId">Income id</param>
        /// <param name="invoiceId">Invoice id</param>
        public CreateInvoiceReceivedPaymentPair(int paymentId, int invoiceId)
        {
            Pair = new CreateInvoiceReceivedPaymentPairPair(paymentId,
                new CreateInvoiceReceivedPaymentPairPairData(new CreateInvoiceReceivedPaymentPairInvoice(invoiceId)));
        }

        /// <summary>
        /// The pair
        /// </summary>
        [JsonProperty("banka")] public CreateInvoiceReceivedPaymentPairPair Pair { get; }
        /// <summary>
        /// Version of the API
        /// </summary>
        [JsonProperty("@version")] public string Version { get; } = "1.0";
    }

    /// <summary>
    /// Invoice-Income pair
    /// </summary>
    public class CreateInvoiceReceivedPaymentPairPair
    {
        /// <summary>
        /// Invoice-Income pair
        /// </summary>
        /// <param name="paymentId">Payment id</param>
        /// <param name="data">Pair data</param>
        public CreateInvoiceReceivedPaymentPairPair(int paymentId, CreateInvoiceReceivedPaymentPairPairData data)
        {
            PaymentId = paymentId;
            Data = data;
        }

        /// <summary>
        /// Payment id
        /// </summary>
        [JsonProperty("id")] public int PaymentId { get; }
        /// <summary>
        /// Pair data
        /// </summary>
        [JsonProperty("sparovani")] public CreateInvoiceReceivedPaymentPairPairData Data { get; }
    }

    /// <summary>
    /// Pair data
    /// </summary>
    public class CreateInvoiceReceivedPaymentPairPairData
    {
        /// <summary>
        /// Pair data
        /// </summary>
        /// <param name="pairInvoice">The pair invoice</param>
        public CreateInvoiceReceivedPaymentPairPairData(CreateInvoiceReceivedPaymentPairInvoice pairInvoice)
        {
            PairInvoice = pairInvoice;
        }

        /// <summary>
        /// The pair invoice
        /// </summary>
        [JsonProperty("uhrazovanaFak")] private CreateInvoiceReceivedPaymentPairInvoice PairInvoice { get; }
        /// <summary>
        /// Rest
        /// </summary>
        [JsonProperty("zbytek")] public string Rest { get; } = "ne";
    }

    /// <summary>
    /// The pair invoice
    /// </summary>
    public class CreateInvoiceReceivedPaymentPairInvoice
    {
        /// <summary>
        /// The pair invoice
        /// </summary>
        /// <param name="id">Invoice id</param>
        public CreateInvoiceReceivedPaymentPairInvoice(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Id of the invoice
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
    }
}