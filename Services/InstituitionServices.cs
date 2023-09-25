using Microsoft.EntityFrameworkCore;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services
{
    public class InstituitionServices : IInstituitionServices
    {
        private readonly SEP_WebContext _database;

        public InstituitionServices(SEP_WebContext database)
        {
            _database = database;
        }

        public async Task<Instituition> RegisterInstituition(Instituition instituition)
        {
            await _database.Instituition.AddAsync(instituition);
            await _database.SaveChangesAsync();
            return instituition;
        }

        public async Task<Instituition> InstituitionEdit(Instituition instituition)
        {
            Instituition instituitionEdit = SearchForId(instituition.Id) ?? throw new Exception("Houve um erro na atualização do órgão");

            instituitionEdit.Name = instituition.Name;
            instituitionEdit.UserAdministratorId = instituition.UserAdministratorId;

            _database.Instituition.Update(instituitionEdit);
            await _database.SaveChangesAsync();

            return instituitionEdit;
        }

        public async Task<ICollection<Instituition>> InstituitionsList()
        {
            ICollection<Instituition> instituition = await _database.Instituition.ToListAsync();
            return instituition;
        }

        public void DeleteInstituition(int id)
        {
            Instituition deleteInstituition = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do órgão");

            _database.Instituition.Remove(deleteInstituition);
            _database.SaveChanges();
        }

        public Instituition SearchForId(int id)
        {
            return _database.Instituition.FirstOrDefault(x => x.Id == id);
        }
    }
}