﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeUnitOfMeasureSetEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public string UnitOfMeasure { get; }

        public CustomAttributeUnitOfMeasureSetEvent(Guid attributeId, string unitOfMeasure)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            UnitOfMeasure = unitOfMeasure;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} unit of measure set to {UnitOfMeasure}";
        }
    }
}
