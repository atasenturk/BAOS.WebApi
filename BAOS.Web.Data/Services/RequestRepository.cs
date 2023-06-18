using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BAOS.Web.Data.Services
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        private readonly BAOSDbContext _context;


        public RequestRepository(BAOSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRequest(int userId, string answers, int protocol)
        {
            try
            {
                var request = new Request
                {
                    Answers = answers,
                    UserId = userId,
                    RequestTime = DateTime.Now
                };

                _context.Requests.Add(request);

                var result = new Result
                {
                    Protocol = protocol,
                    Request = request
                };

                _context.Results.Add(result);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserRequest> GetRequestById(int requestId)
        {
            var userRequest = await _context.Requests
                .Join(_context.Results, req => req.RequestId, res => res.RequestId, (req, res) => new { req, res })
                .Where(r => r.req.RequestId == requestId)
                .Select(r => new UserRequest
                {
                    RequestId = r.req.RequestId,
                    Protocol = r.res.Protocol,
                    Answers = r.req.Answers
                })
                .FirstOrDefaultAsync();

            return userRequest;
        }

    }
}
