using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Mudemy.Core.Models;

namespace Mudemy.Data.Configurations
{
    public class UserAppConfiguration : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            
        }
    }
}