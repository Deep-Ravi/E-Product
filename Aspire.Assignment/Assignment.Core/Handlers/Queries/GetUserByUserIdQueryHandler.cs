using Assignment.Contracts;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Core.Filters;
using AutoMapper;
using MediatR;
using System.Globalization;

namespace Assignment.Providers.Handlers.Queries
{
    public class GetUserByUserIdQuery : IRequest<UserDTO>
    {
        public string UserName { get; }
        public GetUserByUserIdQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, UserDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetUserByUserIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            string errorMessage = string.Empty;
            var userDetail = GetEncryptedData(request.UserName);
            string userName = userDetail.Item1;
            var app = await Task.FromResult(_repository.User.GetAllUsers().FirstOrDefault(x=>x.Username==userName));
            if (userDetail.Item2 && app != null)
                throw new EntityNotFoundException(Constants.PassowrdLinkExpired);

            if (string.IsNullOrEmpty(errorMessage) && !userName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase) && app == null)
                throw new EntityNotFoundException($"No User found for  {request.UserName}");

            if (string.IsNullOrEmpty(errorMessage) && (app.LastPasswordChange > userDetail.Item3))
                throw new EntityNotFoundException(Constants.PassowrdLinkUsed);

            return _mapper.Map<UserDTO>(app);
        }
        private System.Tuple<string, bool, DateTime> GetEncryptedData(string userDetail)
        {
            try
            {
                var decrytedInfo = CryptFilter.Decrypt(userDetail);
                DateTime CreatedDate = DateTime.ParseExact(decrytedInfo.Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                bool isExpire = (DateTime.Now - CreatedDate).TotalMinutes > 150;
                return System.Tuple.Create(decrytedInfo.Substring(14), isExpire, CreatedDate);
            }
            catch (Exception ex)
            {
                return System.Tuple.Create(userDetail, true, DateTime.Now);
            }
        }
    }

}