using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;

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
                    UserId = userId
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

    }
}
