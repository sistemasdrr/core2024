using AutoMapper;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using DRRCore.Infraestructure.Interfaces.Repository;

namespace DRRCore.Infraestructure.Repository.SQLRepository
{
    public class EmailUserRepository : IEmailUserRepository
    {      
        private readonly IMySqlUserRepository _mySqlUserRepository;
        private IMapper Mapper { get; }
       
        public EmailUserRepository(IMySqlUserRepository mySqlUserRepository, IMapper mapper) {
             _mySqlUserRepository = mySqlUserRepository;
            this.Mapper = mapper;
        }    

        public async void Add()
        {
            try
            {
            //    var listUser = await _mySqlUserRepository.GetAll();
                using (var context = new SqlContext())            
                {
                    await context.EmailUsers.AddRangeAsync(Mapper.Map<List<EmailUser>>(new object()));                   
                    await context.SaveChangesAsync(); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<EmailUser> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
