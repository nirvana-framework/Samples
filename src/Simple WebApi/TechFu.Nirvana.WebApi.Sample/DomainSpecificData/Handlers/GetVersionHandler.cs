﻿using Nirvana.CQRS;
using Nirvana.Mediation;
using TechFu.Nirvana.WebApi.Sample.DomainSpecificData.Queries;

namespace TechFu.Nirvana.WebApi.Sample.DomainSpecificData.Handlers
{
    public class GetVersionHandler : QueryHandlerBase<GetVersionQuery, string>
    {
        public GetVersionHandler(IChildMediatorFactory mediator) : base(mediator)
        {
        }

        public override QueryResponse<string> Handle(GetVersionQuery query)
        {
            return QueryResponse.Success("V 1.0");
        }
    }
}