using CSharpEssentials.Interfaces;
using Deneme2.Services.ProductService.Domain.Products.Fields;

namespace Deneme2.Services.ProductService.Domain.Products.Events;

public sealed record ProductUpdatedDomainEvent(ProductId ProductId) : IDomainEvent;
