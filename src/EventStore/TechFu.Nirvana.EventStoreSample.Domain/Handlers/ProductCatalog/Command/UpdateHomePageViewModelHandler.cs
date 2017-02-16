﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nirvana.Configuration;
using Nirvana.CQRS;
using Nirvana.Data;
using Nirvana.Domain;
using Nirvana.Mediation;
using TechFu.Nirvana.EventStoreSample.Domain.Domain.ProductCatalog;
using TechFu.Nirvana.EventStoreSample.Services.Shared;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.ProductCatalog.Commands;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.ProductCatalog.UINotifications;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.ProductCatalog.ViewModels;

namespace TechFu.Nirvana.EventStoreSample.Domain.Handlers.ProductCatalog.Command
{
    public class UpdateHomePageViewModelHandler : BaseNoOpCommandHandler<UpdateHomePageViewModelCommand>
    {
        private readonly IRepository<ProductCatalogRoot> _repository;
        private readonly IViewModelRepository _viewModelRepository;

        public UpdateHomePageViewModelHandler(IViewModelRepository viewModelRepository, IRepository<ProductCatalogRoot> repository,
            IChildMediatorFactory mediator) : base(mediator)
        {
            _viewModelRepository = viewModelRepository;
            _repository = repository;
        }

        public override CommandResponse<Nop> Handle(UpdateHomePageViewModelCommand task)
        {
            var products = _repository.GetAll<Product>().ToArray();

            var homepageModel = new HomePageViewModelBuilder().SetInputs(new Dictionary<string, object>
            {
                {HomePageViewModelBuilder.Keys.Products, products}
            }).Build();
            _viewModelRepository.Save(homepageModel);

            Mediator.Notification(new HomePageUpdatedUiEvent());
            return CommandResponse.Succeeded(Nop.NoValue);
        }
    }

    public class HomePageViewModelBuilder : ViewModelBuilder<HomePageViewModel>
    {
        public override HomePageViewModel Build()
        {
            return new HomePageViewModel
            {
                Id = NirvanaSetup.ApplicationLevelViewModelKey,
                RootEntityKey = NirvanaSetup.ApplicationLevelViewModelKey,
                Products = BuildProductViewModel(GetValue(Keys.Products, new Product[0]))
            };
        }

        private HomePageProductViewModel[] BuildProductViewModel(Product[] products)
        {
            return products.Select(x => new HomePageProductViewModel
            {
                Name = x.Name,
                Id = x.Id,
                ShortDescription = x.ShortDescription,
                Price = x.BasePrice,
                RootEntityKey = Guid.Empty
            }).ToArray();
        }

        public static class Keys
        {
            public static readonly string Products = "Products";
        }
    }
}