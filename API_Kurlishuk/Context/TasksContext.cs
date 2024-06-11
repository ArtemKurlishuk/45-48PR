using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using API_Kurlishuk.Model;
using System;

namespace API_Kurlishuk.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
        public TasksContext()
        {
            Database.EnsureCreated(); //проверяем, создано ли подключение
            Tasks.Load(); //выполняем загрузку данных
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //подключаемся к серверу майСКЛ, со следующими настройками
            optionsBuilder.UseMySql("server=127.0.0.1; " + "uid=root; " +
                "pwd=;" +
                "database=TaskManager",
                new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
