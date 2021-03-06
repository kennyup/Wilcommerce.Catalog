﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductEanCodeChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string EanCode { get; }

        public ProductEanCodeChangedEvent(Guid productId, string ean)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            EanCode = ean;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} EAN changed to {EanCode}";
        }
    }
}
