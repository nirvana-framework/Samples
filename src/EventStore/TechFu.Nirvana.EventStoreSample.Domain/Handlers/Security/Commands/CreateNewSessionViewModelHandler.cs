﻿using Nirvana.CQRS;
using Nirvana.Data;
using Nirvana.Mediation;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.Security.Command;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.Security.Events;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.Security.ViewModels;

namespace TechFu.Nirvana.EventStoreSample.Domain.Handlers.Security.Commands
{
    public class CreateNewSessionViewModelHandler : BaseNoOpCommandHandler<CreateNewSessionViewModelCommand>
    {
        private readonly IViewModelRepository _repository;

        public CreateNewSessionViewModelHandler(IViewModelRepository repository, IChildMediatorFactory mediator)
            : base(mediator)
        {
            _repository = repository;
        }

        public override CommandResponse<Nop> Handle(CreateNewSessionViewModelCommand task)
        {
            _repository.Save(new SessionViewModel
            {
                Id = task.SessionId,
                Name = "",
                RootEntityKey = task.SessionId
            });

            Mediator.InternalEvent(new SessionViewModelCreatedEvent {SessionId = task.SessionId});


            return CommandResponse.Succeeded(Nop.NoValue);
        }
    }
}