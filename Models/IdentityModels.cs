using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace dndAPI.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<dndAPI.Models.WorldModel> WorldModels { get; set; }

        public System.Data.Entity.DbSet<dndAPI.Models.RoomModel> RoomModels { get; set; }

        public System.Data.Entity.DbSet<dndAPI.Models.UsersInRoomsModel> UsersInRoomsModels { get; set; }

        public System.Data.Entity.DbSet<dndAPI.Models.ItemModel> ItemModels { get; set; }

        public System.Data.Entity.DbSet<dndAPI.Models.CharacterModel> CharacterModels { get; set; }

        public System.Data.Entity.DbSet<dndAPI.Models.LocationModel> LocationModels { get; set; }

        public System.Data.Entity.DbSet<dndAPI.Models.ChatLogsModel> ChatLogsModels { get; set; }
    }
}