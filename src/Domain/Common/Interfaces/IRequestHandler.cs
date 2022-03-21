using MasterCraft.Domain.Common.Models;
using MasterCraft.Domain.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.Interfaces
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task Validate(TRequest request, Validator validator);
        Task<TResponse> Handle(TRequest request);
    }
}
