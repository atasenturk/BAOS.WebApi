using BAOS.Web.Domain.Models;

namespace BAOS.Web.Data.Contracts;

public interface IRequestRepository : IGenericRepository<Request>
{
    Task<bool> AddRequest(int userId, string answers, int protocol);
    Task<UserRequest> GetRequestById(int requestId);
}