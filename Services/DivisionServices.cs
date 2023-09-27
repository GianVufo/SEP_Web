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

        public async Task<Division> RegisterDivision(Division division)
        {
            division.RegisterDate = DateTime.Now;
            await _database.Division.AddAsync(division);
            await _database.SaveChangesAsync();
            return division;
        }

        public async Task<Division> DivisionEdit(Division division)
        {
            Division divisionEdit = SearchForId(division.Id) ?? throw new Exception("Houve um erro na atualização do órgão");

            divisionEdit.Name = division.Name;
            divisionEdit.ModifyDate = DateTime.Now;

            divisionEdit.UserAdministratorId = division.UserAdministratorId;

            _database.Division.Update(divisionEdit);
            await _database.SaveChangesAsync();

            return divisionEdit;
        }

        public async Task<ICollection<Division>> DivisionsList()
        {
            ICollection<Division> divisions = await _database.Division.ToListAsync();
            return divisions;
        }

        public void DeleteDivision(int id)
        {
            throw new NotImplementedException();
        }

        public Division SearchForId(int id)
        {
            return _database.Division.FirstOrDefault(x => x.Id == id);
        }
    }
}