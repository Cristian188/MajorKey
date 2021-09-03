using MajorKey.Core.Contracts.Repositories;
using MajorKey.Core.Contracts.Services;
using MajorKey.Core.Models;
using MajorKey.Core.Models.DataTransfer;
using MajorKey.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MajorKey.Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository requestRepository;
        private readonly IMailService mailService;

        public RequestService(IRequestRepository requestRepository, IMailService mailService)
        {
            this.requestRepository = requestRepository;
            this.mailService = mailService;
        }

        public async Task<Request> CreateRequestAsync(CreateRequestDto model)
        {
            var request = new Request()
            {
                Description = model.Description,
                BuildingCode = model.BuildingCode,
                CreatedBy = model.CreatedBy,
                LastModifiedBy = model.CreatedBy
            };

            return await requestRepository.InsertAsync(request);
        }

        public async Task DeleteRequestAsync(Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(Request));
            }

            await requestRepository.DeleteAsync(request);
        }

        public async Task<IEnumerable<Request>> GetAllRequestAsync()
        {
            return await requestRepository.GetAllAsync();
        }

        public async Task<int> CountAsync()
        {
            return await requestRepository.CountAsync();
        }

        public async Task<Request> GetRequestAsync(long requestId)
        {
            return await requestRepository.GetAsync(requestId);
        }

        public async Task<Request> UpdateRequestAsync(UpdateRequestDto model)
        {
            var request = new Request()
            {
                Id = model.Id,
                Description = model.Description,
                BuildingCode = model.BuildingCode,
                LastModifiedBy = model.LastModifiedBy
            };

            return await requestRepository.UpdateAsync(request);
        }

        public async Task<bool> RequestExist(long requestId)
        {
            return await requestRepository.ExistAsync(requestId);
        }

        public async Task SetRequestStatusAsync(Request request, CurrentStatus status)
        {
            var prevRequestStatus = request.CurrentStatus;
            if (prevRequestStatus == status)
            { 
                return;
            }

            request.CurrentStatus = status;
            await requestRepository.UpdateAsync(request);

            // customer notification.
            if (prevRequestStatus != CurrentStatus.Complete &&
                status == CurrentStatus.Complete)
            {
                await NotifyCustomer();
            }
        }

        private async Task NotifyCustomer()
        {
            var mailRequest = new MailRequest()
            {
                Body = "Test body email",
                Subject = "Test subject email",
                ToEmail = "cristiandominutti@gmail.com"
            };

            await this.mailService.SendEmailAsync(mailRequest);
        }
    }
}
