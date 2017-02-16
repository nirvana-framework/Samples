﻿using System;
using System.Collections.Generic;
using Nirvana.CQRS;
using Nirvana.Data;
using Nirvana.Mediation;
using TechFu.Nirvana.EventStoreSample.Domain.Domain.ProductCatalog;
using TechFu.Nirvana.EventStoreSample.Domain.Domain.ShoppingCart;
using TechFu.Nirvana.EventStoreSample.Services.Shared;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.ProductCatalog.Commands;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.ProductCatalog.InternalEvents;

namespace TechFu.Nirvana.EventStoreSample.Domain.Handlers.ProductCatalog.Command
{
    public class CreateSampleCatalogHandler : BaseNoOpCommandHandler<CreateSampleCatalogCommand>
    {
        private readonly IRepository<ProductCatalogRoot> _repository;

        public CreateSampleCatalogHandler(IRepository<ProductCatalogRoot> repository, IChildMediatorFactory mediator)
            : base(mediator)
        {
            _repository = repository;
        }

        public override CommandResponse<Nop> Handle(CreateSampleCatalogCommand task)
        {
            var oldCatalog = _repository.GetAll<Product>();
            _repository.DeleteCollection(_repository.GetAll<CartItem>());
            _repository.DeleteCollection(_repository.GetAll<Cart>());
            _repository.DeleteCollection(oldCatalog);

            _repository.SaveOrUpdateCollection(SampleCatalog());
            Mediator.InternalEvent(new CatalogUpdatedEvent());
            return CommandResponse.Succeeded(Nop.NoValue);
        }

        private static List<Product> SampleCatalog()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Test 1",
                    BasePrice = 15.25m,
                    Id = Guid.NewGuid(),
                    LongDescription = "Test test",
                    ShortDescription = "Test"
                },
                new Product
                {
                    Name = "Test 2",
                    BasePrice = 25.25m,
                    Id = Guid.NewGuid(),
                    LongDescription = "Test test",
                    ShortDescription = "Test"
                },
                new Product
                {
                    Name = "Test 3",
                    BasePrice = 35.25m,
                    Id = Guid.NewGuid(),
                    LongDescription = "Test test",
                    ShortDescription = "Test"
                }
            };
        }
    }
}