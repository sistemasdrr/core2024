using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ILogger _logger;
        public CompanyRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddAsync(Company obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using (var context = new SqlCoreContext())
                {
                    foreach (var item in Constants.TRADUCTIONS_FORMS)
                    {
                        var exist = obj.Traductions.Where(x => x.Identifier == item).FirstOrDefault();
                        if (exist == null)
                        {
                            obj.Traductions.Add(new Traduction
                            {
                                IdPerson = null,
                                Identifier = item,
                                IdLanguage = 1,
                                LastUpdaterUser=1
                            });
                        }
                    }
                    await context.Companies.AddAsync(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Companies.FindAsync(id) ?? throw new Exception("No existe la empresa solicitada");
                    obj.Enable = false;
                    obj.DeleteDate = DateTime.Now;
                    context.Companies.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public Task<List<Company>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                return await context.Companies.Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new Exception("No existe la empresa solicitada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<Company>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Company obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var existTraduction = obj.Traductions;
                    obj.Traductions=new List<Traduction>();
                    obj.UpdateDate = DateTime.Now;
                    context.Companies.Update(obj);

                    foreach (var item in existTraduction)
                    {
                        var modifierTraduction = await context.Traductions.Where(x => x.IdCompany == obj.Id && x.Identifier == item.Identifier).FirstOrDefaultAsync();
                        if (modifierTraduction != null)
                        {
                            modifierTraduction.ShortValue=item.ShortValue;
                            modifierTraduction.LargeValue=item.LargeValue;  
                            modifierTraduction.LastUpdaterUser=item.LastUpdaterUser;
                            context.Traductions.Update(modifierTraduction);
                        }
                    }                    
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
