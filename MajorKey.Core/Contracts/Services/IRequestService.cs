using MajorKey.Core.Models.DataTransfer;
using MajorKey.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MajorKey.Core.Contracts.Services
{
    public interface IRequestService
    {
        Task<Request> GetRequestAsync(long requestId);
        Task<IEnumerable<Request>> GetAllRequestAsync();

        Task<int> CountAsync();

        Task<Request> CreateRequestAsync(CreateRequestDto model);

        Task<Request> UpdateRequestAsync(UpdateRequestDto model);

        Task DeleteRequestAsync(Request request);

        Task<bool> RequestExist(long id);
    }
}
