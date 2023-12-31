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
            divisionEdit.InstituitionId = division.InstituitionId;

            _database.Division.Update(divisionEdit);
            await _database.SaveChangesAsync();

            return divisionEdit;
        }

        public async Task<ICollection<Division>> DivisionsList()
        {
            ICollection<Division> divisions = await _database.Division.ToListAsync();
            return divisions;
        }

        public async Task<ICollection<Division>> GetDivisionsAsync(int instituitionId)
        {
            return await _database.Division.Where(d => d.InstituitionId == instituitionId).ToListAsync();
        }

        public void DeleteDivision(int id)
        {
            Division deleteDivision = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do órgão");

            _database.Division.Remove(deleteDivision);
            _database.SaveChanges();
        }

        public Division SearchForId(int id)
        {
            return _database.Division.FirstOrDefault(x => x.Id == id);
        }
    }
}