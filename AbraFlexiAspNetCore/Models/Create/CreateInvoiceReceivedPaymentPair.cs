using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    public class CreateInvoiceReceivedPaymentPair
    {
        public CreateInvoiceReceivedPaymentPair(int paymentId, int invoiceId)
        {
            Pair = new CreateInvoiceReceivedPaymentPairPair(paymentId,
                new CreateInvoiceReceivedPaymentPairPairData(new CreateInvoiceReceivedPaymentPairInvoice(invoiceId)));
        }

        [JsonProperty("banka")] public CreateInvoiceReceivedPaymentPairPair Pair { get; }
        [JsonProperty("@version")] public string Version { get; } = "1.0";
    }

    public class CreateInvoiceReceivedPaymentPairPair
    {
        public CreateInvoiceReceivedPaymentPairPair(int paymentId, CreateInvoiceReceivedPaymentPairPairData data)
        {
            PaymentId = paymentId;
            Data = data;
        }

        [JsonProperty("id")] public int PaymentId { get; }
        [JsonProperty("sparovani")] public CreateInvoiceReceivedPaymentPairPairData Data { get; }
    }

    public class CreateInvoiceReceivedPaymentPairPairData
    {
        public CreateInvoiceReceivedPaymentPairPairData(CreateInvoiceReceivedPaymentPairInvoice pair)
        {
            Pair = pair;
        }

        [JsonProperty("uhrazovanaFak")] private CreateInvoiceReceivedPaymentPairInvoice Pair { get; }
        [JsonProperty("zbytek")] public string Rest { get; } = "ne";
    }

    public class CreateInvoiceReceivedPaymentPairInvoice
    {
        public CreateInvoiceReceivedPaymentPairInvoice(int id)
        {
            Id = id;
        }

        [JsonProperty("id")] public int Id { get; }
    }
}