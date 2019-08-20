using HealthIns.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthIns.Tests.Common
{
   public class HealthInsDbContextInMemoryFactory
    {
        public static HealthInsDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<HealthInsDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new HealthInsDbContext(options);
        }
    }
}
