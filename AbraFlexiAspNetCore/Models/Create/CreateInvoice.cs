using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    public class CreateInvoice
    {
        public CreateInvoice(List<CreateInvoiceInvoice> invoices)
        {
            Invoices = invoices;
        }

        [JsonProperty("faktura-vydana")] public List<CreateInvoiceInvoice> Invoices { get; }
        [JsonProperty("@version")] public string Version { get; } = "1.0";
    }

    public class CreateInvoiceInvoice
    {
        public CreateInvoiceInvoice(DateTime due, int invoiceType, int currency, DateTime dateIssued,
            string companyName, string? @in, string? tin, string? city, string? street, string? zipCode,
            string? country, List<CreateInvoiceInvoiceItem> invoiceItems, bool withoutItems, int paymentTypeId)
        {
            Due = due;
            InvoiceType = invoiceType;
            Currency = currency;
            DateIssued = dateIssued;
            CompanyName = companyName;
            In = @in;
            Tin = tin;
            City = city;
            Street = street;
            ZipCode = zipCode;
            Country = country;
            InvoiceItems = invoiceItems;
            WithoutItems = withoutItems;
            PaymentTypeId = paymentTypeId;
        }

        [JsonProperty("datSplat")] public DateTime Due { get; }
        [JsonProperty("typDokl")] public int InvoiceType { get; }
        [JsonProperty("mena")] public int Currency { get; }
        [JsonProperty("datVyst")] public DateTime DateIssued { get; }
        [JsonProperty("nazFirmy")] public string CompanyName { get; }

        /// <summary>
        /// IČ
        /// </summary>
        [JsonProperty("ic")]
        public string? In { get; }

        /// <summary>
        /// DIČ
        /// </summary>
        [JsonProperty("dic")]
        public string? Tin { get; }

        [JsonProperty("mesto")] public string? City { get; }
        [JsonProperty("ulice")] public string? Street { get; }
        [JsonProperty("psc")] public string? ZipCode { get; }
        [JsonProperty("stat")] public string? Country { get; }
        [JsonProperty("polozkyFaktury")] public List<CreateInvoiceInvoiceItem> InvoiceItems { get; }
        [JsonProperty("bezPolozek")] public bool WithoutItems { get; }
        [JsonProperty("formaUhradyCis")] public int PaymentTypeId { get; }
    }

    public class CreateInvoiceInvoiceItem
    {
        public CreateInvoiceInvoiceItem(int quantity, string code)
        {
            Quantity = quantity;
            Code = code;
        }

        [JsonProperty("mnozMj")] public int Quantity { get; }
        [JsonProperty("kod")] public string Code { get; }
        [JsonProperty("cenik")] public int PriceListId { get; internal set; }
    }
}