using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    public class CreateBankReceivedPayment
    {
        public CreateBankReceivedPayment(List<CreateBankReceivedPaymentPayment> payments)
        {
            Payments = payments;
        }

        [JsonProperty("banka")] public List<CreateBankReceivedPaymentPayment> Payments { get; }
        [JsonProperty("@version")] public string Version { get; } = "1.0";
    }

    public class CreateBankReceivedPaymentPayment
    {
        public CreateBankReceivedPaymentPayment(float price, int bank, int invoiceType, int currency, string movementType,
            DateTime dateIssued, bool withoutItems)
        {
            Price = price;
            Bank = bank;
            InvoiceType = invoiceType;
            Currency = currency;
            MovementType = movementType;
            DateIssued = dateIssued;
            WithoutItems = withoutItems;
        }

        [JsonProperty("sumOsv")] public float Price { get; }
        [JsonProperty("banka")] public int Bank { get; }
        [JsonProperty("typDokl")] public int InvoiceType { get; }
        [JsonProperty("mena")] public int Currency { get; }
        [JsonProperty("typPohybuK")] public string MovementType { get; }
        [JsonProperty("datVyst")] public DateTime DateIssued { get; }
        [JsonProperty("bezPolozek")] public bool WithoutItems { get; }
    }
}