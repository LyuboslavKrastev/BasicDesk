using BasicDesk.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BasicDesk.Tests.Utilities
{
    public static class TestsDbContext
    {
        public static BasicDeskDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<BasicDeskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new BasicDeskDbContext(options);
        }
    }
}
