using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services
{
    public class DivisionServices : IDivisionServices
    {
        private readonly SEP_WebContext _database;

        public DivisionServices(SEP_WebContext database)
        {
            _database = database;
        }
        
        public void DeleteDivision(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Division> DivisionEdit(Division division)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Division>> DivisionsList()
        {
            ICollection<Division> divisions = await _database.Division.ToListAsync();
            return divisions;
        }

        public Task<Division> RegisterDivision(Division division)
        {
            throw new NotImplementedException();
        }

        public Division SearchForId(int id)
        {
            throw new NotImplementedException();
        }
    }
}