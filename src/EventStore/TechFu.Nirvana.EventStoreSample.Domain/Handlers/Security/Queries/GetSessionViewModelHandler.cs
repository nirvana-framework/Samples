﻿using System;
using Nirvana.CQRS;
using Nirvana.Data;
using Nirvana.Mediation;
using TechFu.Nirvana.EventStoreSample.Domain.Handlers.ProductCatalog.Common;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.Security.Command;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.Security.Query;
using TechFu.Nirvana.EventStoreSample.Services.Shared.Services.Security.ViewModels;

namespace TechFu.Nirvana.EventStoreSample.Domain.Handlers.Security.Queries
{
    public class GetSessionViewModelHandler : ViewModelQueryBase<GetNewSessionViewModelQuery, SessionViewModel>
    {
        public GetSessionViewModelHandler(IViewModelRepository repository, IChildMediatorFactory mediator)
            : base(repository, mediator)
        {
        }

        public override QueryResponse<SessionViewModel> Handle(GetNewSessionViewModelQuery query)
        {
            //First Time User
            if (query.SessionId == Guid.Empty)
            {
                var sessionId = Guid.NewGuid();
                Mediator.Command(new CreateNewSessionViewModelCommand {SessionId = sessionId});

                return QueryResponse.Success(new SessionViewModel
                {
                    Id = sessionId,
                    RootEntityKey = sessionId,
                    Name = ""
                });
            }


            //return user w/ cookie
            return GetFromViewModelRepository(query, q => query.SessionId);
        }
    }
}