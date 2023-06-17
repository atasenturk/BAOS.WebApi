using BAOS.Web.Domain.Models;

namespace BAOS.Web.Data.Contracts;

public interface IResultRepository : IGenericRepository<Result>
{
    Task<List<UserRequest>> GetRequestAndAnswers(int UserId);
}