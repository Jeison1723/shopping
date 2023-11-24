

using Shooping.Data.Entities;
using Shopping.Controllers.Data.Entities;
using Shopping.Controllers.Data.TipoUsuario;
using Shopping.Helpers;

namespace Shopping.Controllers.Data
{
    public class Seedb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public Seedb(DataContext context, IUserHelper userHelper) 

        {
            _context = context;
            _userHelper = userHelper;
        } 
        
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckedCategoriesAsync();
            await checkedContriesAync();
            await chekedRolesAsync();
        }

        private async Task chekedRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await CheckUserAsync("1010", "Juan", "Zuluaga", "Jei@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(

     string document,

     string firstName,

     string lastName,

     string email,

     string phone,

     string address,

     UserType userType)

        {

            User user = await _userHelper.GetUserAsync(email);

            if (user == null)

            {

                user = new User

                {

                    FirstName = firstName,

                    LastName = lastName,

                    Email = email,

                    UserName = email,

                    PhoneNumber = phone,

                    Address = address,

                    Document = document,

                   

                    UserType = userType,

                };


                await _userHelper.AddUserAsync(user, "123456");

                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

            }


            return user;

        }

        private async Task checkedContriesAync()
        {
           if (!_context.countries.Any())
            {
                _context.countries.Add(new Country
                {
                    Name = "Belgica",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Rumania"
                        }
                    }
                });
            }
        }

        private async Task CheckedCategoriesAsync()
        {
           if(!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnologia" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
