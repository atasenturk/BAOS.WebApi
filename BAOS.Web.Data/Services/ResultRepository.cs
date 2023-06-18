using System.Runtime.InteropServices;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BAOS.Web.Data.Services;

public class ResultRepository : GenericRepository<Result>, IResultRepository
{
    private readonly BAOSDbContext _context;

    public ResultRepository(BAOSDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<UserRequest>> GetAllRequestsById(int userId)
    {
        var results =  _context.Results
            .Join(_context.Requests, res => res.RequestId, req => req.RequestId, (res, req) => new { res, req })
            .Where(r => r.req.UserId == userId)
            .Select(r => new UserRequest
            {
                RequestId = r.req.RequestId,
                Protocol = r.res.Protocol,
                Answers = r.req.Answers
            })
            .ToList();

        return results;
    }
}