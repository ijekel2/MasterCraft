﻿namespace MasterCraft.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Email { get; }

    }
}


