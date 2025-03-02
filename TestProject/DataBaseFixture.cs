using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public ComputersContext Context { get; private set; }

        public DatabaseFixture()
        {
            var uniqueDbName = $"TestDatabase_{Guid.NewGuid()}";
            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<ComputersContext>()
            .UseSqlServer($"Server=srv2\\pupils;Database={uniqueDbName};Trusted_Connection=True;TrustServerCertificate=True").Options;
            Context = new ComputersContext(options);
            Context.Database.EnsureCreated();// create the data base
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
