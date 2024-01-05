using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ILogger _logger;
        public PersonRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> ActivateWebAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var existingPerson = await context.People.FindAsync(id);
                if (existingPerson != null)
                {
                    existingPerson.OnWeb = true;
                    context.People.Update(existingPerson);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> AddAsync(Person obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using var context = new SqlCoreContext();
                foreach (var item in Constants.TRADUCTIONS_FORMS_PERSON)
                {
                    var exist = obj.Traductions.Where(x => x.Identifier == item).FirstOrDefault();
                    if (exist == null)
                    {
                        obj.Traductions.Add(new Traduction
                        {
                            IdCompany = null,
                            IdPerson = obj.Id,
                            Identifier = item,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                }
                await context.People.AddAsync(obj);
                await context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<int> AddPersonAsync(Person person)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using var context = new SqlCoreContext();
                await context.People.AddAsync(person);
                foreach (var item in Constants.TRADUCTIONS_FORMS_PERSON)
                {
                    var exist = person.Traductions.Where(x => x.Identifier == item).FirstOrDefault();
                    if (exist == null)
                    {
                        person.Traductions.Add(new Traduction
                        {
                            IdCompany = null,
                            IdPerson = person.Id,
                            Identifier = item,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                }
                await context.SaveChangesAsync();
                return person.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var existingPerson = await context.People.FindAsync(id);
                if(existingPerson != null)
                {
                    existingPerson.DeleteDate = DateTime.Now;
                    existingPerson.Enable = false;
                    context.People.Update(existingPerson);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> DesactivateWebAsync(int id)
        {
            try
            {
                using var context = new SqlCoreContext();
                var existingPerson = await context.People.FindAsync(id);
                if (existingPerson != null)
                {
                    existingPerson.OnWeb = false;
                    context.People.Update(existingPerson);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public Task<List<Person>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Person>> GetAllByAsync(string fullname, string form, int idCountry, bool haveReport)
        {
            List<Person> persons = new List<Person>();
            try
            {
                using var context = new SqlCoreContext();
                if (form == "C")
                {
                    if (idCountry == 0)
                    {
                        persons = await context.People.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).Include(x => x.IdDocumentTypeNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.Fullname.Contains(fullname)).Take(100).ToListAsync();
                    }
                    else
                    {
                        persons = await context.People.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).Include(x => x.IdDocumentTypeNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.IdCountry == idCountry && (x.Fullname.Contains(fullname))).Take(100).ToListAsync();
                    }

                }
                else
                {
                    if (idCountry == 0)
                    {
                        persons = await context.People.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).Include(x => x.IdDocumentTypeNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.Fullname.StartsWith(fullname)).Take(100).ToListAsync();
                    }
                    else
                    {
                        persons = await context.People.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).Include(x => x.IdDocumentTypeNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.IdCountry == idCountry && (x.Fullname.StartsWith(fullname))).Take(100).ToListAsync();
                    }

                }

                return persons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using var context = new SqlCoreContext();
                var company = await context.People.Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new Exception("No existe la persona solicitada");
                traductions.AddRange(await context.Traductions.Where(x => x.IdPerson == id && x.Identifier.Contains("_P_")).ToListAsync());
                company.Traductions = traductions;
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<Person>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Person obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var existTraduction = obj.Traductions;
                    obj.Traductions = new List<Traduction>();
                    obj.UpdateDate = DateTime.Now;
                    context.People.Update(obj);

                    foreach (var item in existTraduction)
                    {
                        var modifierTraduction = await context.Traductions.Where(x => x.IdPerson == obj.Id && x.Identifier == item.Identifier).FirstOrDefaultAsync();
                        if (modifierTraduction != null)
                        {
                            modifierTraduction.ShortValue = item.ShortValue;
                            modifierTraduction.LargeValue = item.LargeValue;
                            modifierTraduction.LastUpdaterUser = item.LastUpdaterUser;
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

        public Task<int> UpdatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
