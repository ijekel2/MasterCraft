using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Mentors
{
    public class SetupMentorProfileService : DomainService<MentorProfileVm, MentorCreatedVm>
    {
        readonly IDbContext _dbContext;
        readonly IPaymentService _paymentService;

        public SetupMentorProfileService(IDbContext dbContext, IPaymentService paymentService,
            DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        internal override async Task Validate(MentorProfileVm mentor, DomainValidator validator, CancellationToken token = new())
        {
            await Task.CompletedTask;
        }

        internal override async Task<MentorCreatedVm> Handle(MentorProfileVm request, CancellationToken token = new())
        {
            //-- Save the mentor record.
            MentorUserVm mentorUser = Map<MentorProfileVm, MentorUserVm>(request);
            Mentor mentor = Map<MentorUserVm, Mentor>(mentorUser);
            mentor.UserId = Services.CurrentUserService.UserId;
            mentor.ProfileId = request.ProfileId;
            UserVm user = Map<MentorUserVm, UserVm>(mentorUser);
            mentor.StripeAccountId = await _paymentService.CreateConnectedAccount(user, token);

            await _dbContext.AddAsync(mentor, token);
            await _dbContext.SaveChangesAsync(token);

            //-- Save the associated offering
            Offering offering = Map<OfferingVm, Offering>(request.Offerings.FirstOrDefault());
            offering.MentorId = mentor.UserId;

            await _dbContext.AddAsync(offering, token);
            await _dbContext.SaveChangesAsync(token);

            //-- For now we are just storing the image base64 encoded until we have blob storage.
            ////-- Save profile image
            //string imageUrl = string.Empty;
            //foreach (var file in _httpContextAccessor.HttpContext.Request.Form.Files)
            //{
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        await file.CopyToAsync(stream);
            //        Uri uri = await _fileStorage.SaveFileAsync(stream);
            //        imageUrl = uri.AbsoluteUri;
            //    }
            //}

            ////-- Save the profile pic url
            //mentor.ProfileImageUrl = imageUrl;
            //_dbContext.SaveChanges();

            return new MentorCreatedVm()
            {
                UserId = mentor.UserId,
                StripeAccountId = mentor.StripeAccountId,
                ProfileImageUrl = request.ProfileImageUrl,
            };
        }
    }
}
