using CSharpEssentials.Interfaces;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;

namespace Deneme2.Services.CategoryService.Domain.Categories.Events;

public sealed record CategoryDeletedDomainEvent(CategoryId Id) : IDomainEvent;


