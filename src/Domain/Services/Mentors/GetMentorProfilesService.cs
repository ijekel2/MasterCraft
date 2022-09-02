using AutoMapper.QueryableExtensions;
using LinqKit;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Parameters;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class GetMentorProfilesService : DomainService<MentorParameters, List<MentorProfileVm>>
    {
        readonly IDbContext _db;

        public GetMentorProfilesService(IDbContext dbContext, DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _db = dbContext;
        }

        internal override async Task<List<MentorProfileVm>> Handle(MentorParameters parameters, CancellationToken token = default)
        {
            IQueryable<Mentor> filteredProfiles = _db.Mentors
                .Include(mentor => mentor.Offerings)
                .Include(Mentor => Mentor.User);

            if (!string.IsNullOrEmpty(parameters.ProfileId))
            {
                filteredProfiles = filteredProfiles.Where(mentor => mentor.ProfileId == parameters.ProfileId);
            }

            if (!string.IsNullOrEmpty(parameters.UserId))
            {
                filteredProfiles = filteredProfiles.Where(mentor => mentor.UserId == parameters.UserId);
            }
            
            var mentorProfiles = filteredProfiles
                .Select(mentor =>
                    new MentorProfileVm()
                    {
                        FirstName = mentor.User.FirstName,
                        LastName = mentor.User.LastName,
                        ProfileImageUrl = mentor.User.ProfileImageUrl,
                        UserId = mentor.UserId,
                        ProfileId = mentor.ProfileId,
                        StripeAccountId = mentor.StripeAccountId,
                        VideoEmbedCode = mentor.VideoEmbedUrl,
                        SampleQuestion1 = mentor.SampleQuestion1,
                        SampleQuestion2 = mentor.SampleQuestion2,
                        SampleQuestion3 = mentor.SampleQuestion3,
                        SampleQuestion4 = mentor.SampleQuestion4,
                        SampleQuestion5 = mentor.SampleQuestion5,
                        Offerings = mentor.Offerings.Select(offering => Map<Offering, OfferingVm>(offering))
                    }
                );

            return await PagedList(mentorProfiles, parameters, token);

        }

        internal override async Task Validate(MentorParameters parameters, DomainValidator validator, CancellationToken pToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
