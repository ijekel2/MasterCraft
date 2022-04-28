using MasterCraft.Infrastructure.Identity;
using MasterCraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterCraft.Domain.Models;

namespace MasterCraft.Domain.UnitTests
{
    public class TestConstants
    {
        public static readonly ApplicationUser TestMentor = new()
        {
            Id = "23f01adf-26d4-4b36-9a95-f4721354b65f",
            FirstName = "test",
            LastName = "mentor",
            Username = "mastercraftdev@outlook.com",
            Email = "mastercraftdev@outlook.com",
        };
    }
}
