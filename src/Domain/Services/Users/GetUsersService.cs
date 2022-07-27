using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Shared.ViewModels;

namespace MasterCraft.Domain.Services.Users
{
    public class GetUsersService : DomainService<string, UserVm>
    {
        readonly IDbContext _dbContext;

        public GetUsersService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
        }

        internal override async Task<UserVm> Handle(string id, CancellationToken token = default)
        {
            User user = await _dbContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync();

            return Map<User, UserVm>(user);

        }

        internal override async Task Validate(string id, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
