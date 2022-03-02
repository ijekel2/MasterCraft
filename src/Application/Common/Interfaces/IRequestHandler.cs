using MasterCraft.Application.Common.Models;
using MasterCraft.Application.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Application.Common.Interfaces
{
    public interface IRequestHandler<TCommand, TResponse>
    {
        Task Validate(TCommand command, Validator validator);
        Task<TResponse> Handle(TCommand command);
    }
}
