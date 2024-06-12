using API_Kurlishuk.Modell;
using Microsoft.EntityFrameworkCore;

namespace API_Kurlishuk.Context
{
    public class UsersContext : DbContext
    {
        /// <summary>
        /// Данные из базы данных
        /// </summary>
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// Конструктор контекста
        /// </summary>
        public UsersContext()
        {
            Database.EnsureCreated();
            Users.Load();
        }

        /// <summary>
        /// Переопределяем метод конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql("server=127.0.0.1;uid=root;pwd=;database=Base44", new MySqlServerVersion(new System.Version(8, 0, 11)));
    }
}
