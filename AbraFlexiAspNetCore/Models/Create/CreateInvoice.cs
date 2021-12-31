using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    /// <summary>
    /// Create an invoice
    /// </summary>
    public class CreateInvoice
    {
        /// <summary>
        /// Create an invoice
        /// </summary>
        /// <param name="invoices">List of the invoices to be created</param>
        public CreateInvoice(List<CreateInvoiceInvoice> invoices)
        {
            Invoices = invoices;
        }

        /// <summary>
        /// List of the invoices to be created
        /// </summary>
        [JsonProperty("faktura-vydana")] public List<CreateInvoiceInvoice> Invoices { get; }
        /// <summary>
        /// Version of the API
        /// </summary>
        [JsonProperty("@version")] public string Version { get; } = "1.0";
    }

    /// <summary>
    /// Invoice to be created
    /// </summary>
    public class CreateInvoiceInvoice
    {
        /// <summary>
        /// Invoice to be created
        /// </summary>
        /// <param name="due">Due date of the invoice</param>
        /// <param name="invoiceType">Invoice type id</param>
        /// <param name="currency">Currency id</param>
        /// <param name="dateIssued">Date issued</param>
        /// <param name="companyName">Name of the company</param>
        /// <param name="in">IN of the company (IČ)</param>
        /// <param name="tin">TIN of the company (DIČ)</param>
        /// <param name="city">City of the company</param>
        /// <param name="street">Street of the company</param>
        /// <param name="zipCode">ZIP code of the company</param>
        /// <param name="countryId">Country of the company</param>
        /// <param name="invoiceItems">List of the price list items</param>
        /// <param name="withoutItems">Whether the invoice should be without any price list items or not</param>
        /// <param name="paymentTypeId">Payment type id</param>
        public CreateInvoiceInvoice(DateTime due, int invoiceType, int currency, DateTime dateIssued,
            string companyName, string? @in, string? tin, string? city, string? street, string? zipCode,
            int? countryId, List<CreateInvoiceInvoiceItem> invoiceItems, bool withoutItems, int paymentTypeId)
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
            CountryId = countryId;
            InvoiceItems = invoiceItems;
            WithoutItems = withoutItems;
            PaymentTypeId = paymentTypeId;
        }

        /// <summary>
        /// Due date of the invoice
        /// </summary>
        [JsonProperty("datSplat")] public DateTime Due { get; }
        /// <summary>
        /// Invoice type id
        /// </summary>
        [JsonProperty("typDokl")] public int InvoiceType { get; }
        /// <summary>
        /// Currency id
        /// </summary>
        [JsonProperty("mena")] public int Currency { get; }
        /// <summary>
        /// Date issued
        /// </summary>
        [JsonProperty("datVyst")] public DateTime DateIssued { get; }
        /// <summary>
        /// Name of the company
        /// </summary>
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

        /// <summary>
        /// City of the company
        /// </summary>
        [JsonProperty("mesto")] public string? City { get; }
        /// <summary>
        /// Street of the company
        /// </summary>
        [JsonProperty("ulice")] public string? Street { get; }
        /// <summary>
        /// ZIP code of the company
        /// </summary>
        [JsonProperty("psc")] public string? ZipCode { get; }
        /// <summary>
        /// Country of the company
        /// </summary>
        [JsonProperty("stat")] public int? CountryId { get; }
        /// <summary>
        /// List of the price list items
        /// </summary>
        [JsonProperty("polozkyFaktury")] public List<CreateInvoiceInvoiceItem> InvoiceItems { get; }
        /// <summary>
        /// Whether the invoice should be without any price list items or not
        /// </summary>
        [JsonProperty("bezPolozek")] public bool WithoutItems { get; }
        /// <summary>
        /// Payment type id
        /// </summary>
        [JsonProperty("formaUhradyCis")] public int PaymentTypeId { get; }
    }

    /// <summary>
    /// Invoice price list item
    /// </summary>
    public class CreateInvoiceInvoiceItem
    {
        /// <summary>
        /// Invoice price list item
        /// </summary>
        /// <param name="quantity">Quantity</param>
        /// <param name="code">Code of the price list item</param>
        public CreateInvoiceInvoiceItem(int quantity, string code)
        {
            Quantity = quantity;
            Code = code;
        }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("mnozMj")] public int Quantity { get; }
        /// <summary>
        /// Code of the price list item
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Id of the price list item
        /// </summary>
        [JsonProperty("cenik")] public int PriceListId { get; internal set; }
    }
}