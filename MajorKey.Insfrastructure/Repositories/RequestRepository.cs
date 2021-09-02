using MajorKey.Core.Contracts.Repositories;
using MajorKey.Core.Models.Entities;
using MajorKey.Insfrastructure.DAL;

namespace MajorKey.Insfrastructure.Repositories
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationContext context)
          : base(context)
        {
        }
    }
}
