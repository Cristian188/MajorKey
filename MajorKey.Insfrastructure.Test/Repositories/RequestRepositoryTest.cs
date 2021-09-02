using MajorKey.Core.Models.Entities;
using MajorKey.Insfrastructure.DAL;
using MajorKey.Insfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace MajorKey.Insfrastructure.Test.Repositories
{
    public class RequestRepositoryTest
    {
        protected DbContextOptions<ApplicationContext> ContextOptions { get; }

        public RequestRepositoryTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            Seed();
        }

        private void Seed()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Request
                {
                    BuildingCode = "AC12",
                    CreatedBy = "Cristian",
                    CreatedDate = DateTime.Now,
                    CurrentStatus = CurrentStatus.InProgress,
                    Description = "Reparar ascensor",
                    LastModifiedBy = "Cristian",
                    LastModifiedDate = DateTime.Now
                };

                var two = new Request
                {
                    BuildingCode = "AC4",
                    CreatedBy = "Agustin",
                    CreatedDate = DateTime.Now,
                    CurrentStatus = CurrentStatus.Created,
                    Description = "Pintar pared entrada",
                    LastModifiedBy = "Agustin",
                    LastModifiedDate = DateTime.Now
                };

                var three = new Request
                {
                    BuildingCode = "AC1",
                    CreatedBy = "Laura",
                    CreatedDate = DateTime.Now,
                    CurrentStatus = CurrentStatus.NotApplicable,
                    Description = "Cambiar cerradura puerta",
                    LastModifiedBy = "Laura",
                    LastModifiedDate = DateTime.Now
                };

                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAllAsync_ReturnRequests()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);

                var requests = await repository.GetAllAsync();

                Assert.Equal(3, requests.Count());
            }
        }

        [Fact]
        public async Task GetAsync_ValidId_ReturnRequest()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);

                var request = await repository.GetAsync(1);

                Assert.NotNull(request);
                Assert.Equal(1, request.Id);
            }
        }

        [Fact]
        public async Task GetAsync_InValidId_ReturnNull()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);

                var request = await repository.GetAsync(31);

                Assert.Null(request);
            }
        }

        [Fact]
        public async Task CountAsync_ReturnCount()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);

                var request = await repository.CountAsync();

                Assert.Equal(3, request);
            }
        }

        [Fact]
        public async Task DeleteAsync_RemoveRequest()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);

                var requestToDelete = await repository.GetAsync(1);

                await repository.DeleteAsync(requestToDelete);

                var requests = await repository.GetAllAsync();

                Assert.Equal(2, requests.Count());
            }
        }

        [Fact]
        public async Task InsertAsync_AddRequest()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);
                var four = new Request
                {
                    BuildingCode = "AC1",
                    CreatedBy = "Antonella",
                    CreatedDate = DateTime.Now,
                    CurrentStatus = CurrentStatus.Created,
                    Description = "Limpiar piso 3",
                    LastModifiedBy = "Antonella",
                    LastModifiedDate = DateTime.Now
                };

                var request = await repository.InsertAsync(four);

                Assert.Equal(four.BuildingCode, request.BuildingCode);
                Assert.Equal(four.CreatedBy, request.CreatedBy);
                Assert.Equal(four.CreatedDate, request.CreatedDate);
                Assert.Equal(four.CurrentStatus, request.CurrentStatus);
                Assert.Equal(four.Description, request.Description);
                Assert.Equal(four.LastModifiedBy, request.LastModifiedBy);
                Assert.Equal(four.LastModifiedDate, request.LastModifiedDate);
            }
        }
        [Fact]
        public async Task UpdateAsync_UpdateRequest()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                var repository = new RequestRepository(context);
                var requestToUpdate = new Request
                {
                    Id = 1,
                    BuildingCode = "AC14",
                    CreatedBy = "Antonella",
                    CreatedDate = DateTime.Now,
                    CurrentStatus = CurrentStatus.InProgress,
                    Description = "Limpiar piso 3",
                    LastModifiedBy = "Antonella",
                    LastModifiedDate = DateTime.Now
                };

                var request = await repository.UpdateAsync(requestToUpdate);

                Assert.Equal(requestToUpdate.BuildingCode, request.BuildingCode);
                Assert.Equal(requestToUpdate.CreatedBy, request.CreatedBy);
                Assert.Equal(requestToUpdate.CreatedDate, request.CreatedDate);
                Assert.Equal(requestToUpdate.CurrentStatus, request.CurrentStatus);
                Assert.Equal(requestToUpdate.Description, request.Description);
                Assert.Equal(requestToUpdate.LastModifiedBy, request.LastModifiedBy);
                Assert.Equal(requestToUpdate.LastModifiedDate, request.LastModifiedDate);
            }
        }

    }
}
