using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    /// <summary>
    /// Create a bank income record
    /// </summary>
    public class CreateBankReceivedPayment
    {
        /// <summary>
        /// Create a bank income record
        /// </summary>
        /// <param name="payments">List of the payments to create</param>
        public CreateBankReceivedPayment(List<CreateBankReceivedPaymentPayment> payments)
        {
            Payments = payments;
        }

        /// <summary>
        /// List of the payments to create
        /// </summary>
        [JsonProperty("banka")] public List<CreateBankReceivedPaymentPayment> Payments { get; }
        /// <summary>
        /// Version of the API
        /// </summary>
        [JsonProperty("@version")] public string Version { get; } = "1.0";
    }

    /// <summary>
    /// An income to be created
    /// </summary>
    public class CreateBankReceivedPaymentPayment
    {
        /// <summary>
        /// An income to be created
        /// </summary>
        /// <param name="price">Amount of money received</param>
        /// <param name="bank">Id of the bank</param>
        /// <param name="invoiceType">Id of the invoice type</param>
        /// <param name="currency">Id of the currency</param>
        /// <param name="movementType">Movement type e.g. "typPohybu.prijem"</param>
        /// <param name="dateIssued">Date issued</param>
        /// <param name="withoutItems">Whether the movement is without items or not</param>
        /// <param name="currencyCode">Currency code</param>
        public CreateBankReceivedPaymentPayment(float price, int bank, int invoiceType, int currency, string movementType,
            DateTime dateIssued, bool withoutItems, string currencyCode)
        {
            if (currencyCode == "CZK")
            {
                Price = price;
            }
            else
            {
                PriceOtherCurrency = price;
            }

            Bank = bank;
            InvoiceType = invoiceType;
            Currency = currency;
            MovementType = movementType;
            DateIssued = dateIssued;
            WithoutItems = withoutItems;
        }

        /// <summary>
        /// Amount of money received
        /// </summary>
        [JsonProperty("sumOsv")] public float? Price { get; }
        /// <summary>
        /// Amount of money received (if the currency was foreign)
        /// </summary>
        [JsonProperty("sumOsvMen")] public float? PriceOtherCurrency { get; }
        /// <summary>
        /// Id of the bank
        /// </summary>
        [JsonProperty("banka")] public int Bank { get; }
        /// <summary>
        /// Id of the invoice type
        /// </summary>
        [JsonProperty("typDokl")] public int InvoiceType { get; }
        /// <summary>
        /// Id of the currency
        /// </summary>
        [JsonProperty("mena")] public int Currency { get; }
        /// <summary>
        /// Movement type e.g. "typPohybu.prijem"
        /// </summary>
        [JsonProperty("typPohybuK")] public string MovementType { get; }
        /// <summary>
        /// Date issued
        /// </summary>
        [JsonProperty("datVyst")] public DateTime DateIssued { get; }
        /// <summary>
        /// Whether the movement is without items or not
        /// </summary>
        [JsonProperty("bezPolozek")] public bool WithoutItems { get; }
    }
}