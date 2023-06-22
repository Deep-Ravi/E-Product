using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignment.Core.Filters
{
    public class OperationFilters : Attribute, IActionFilter
    {
        private readonly IUnitOfWork _repository;
        private readonly string _paramOperations;

        public OperationFilters(IUnitOfWork repository, string paramOperations)
        {
            _repository = repository;
            _paramOperations = paramOperations;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var userName = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
                if (userName != null)
                {
                    var userDetail = _repository.User.GetAllUsers().FirstOrDefault(x => x.Username == userName);
                    if (userDetail != null)
                    {
                        if (userDetail.Operation != null)
                        {
                            string[] userOperationsArr = userDetail.Operation.OperationAccess.Split(",");
                            string[] paramOperationsArr = _paramOperations.Split(",");

                            var unMatchedRoles = paramOperationsArr.Except(userOperationsArr);

                            if (unMatchedRoles.Count() < paramOperationsArr.Length)
                            {
                                return;
                            }
                            else
                            {
                                var unMatchedRolesString = string.Join(",", unMatchedRoles);
                                throw new AccessDeniedException($"{userDetail.Username} Do Not Have Priviledge to {unMatchedRolesString.ToLower()} Action");
                            }
                        }
                        else
                        {
                            throw new AccessDeniedException($"{userDetail.Username} Do Not Have Any Priviledge");
                        }
                    }
                }
            }
            catch (AccessDeniedException ex)
            {
                var errorResponse = new ObjectResult(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                }

                );
                errorResponse.StatusCode = StatusCodes.Status403Forbidden;
                context.Result = errorResponse;
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
