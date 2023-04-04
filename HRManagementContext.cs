using HRManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRManagement
{
    public class HRManagementContext : DbContext
    {
        public DbSet<Person>? People {get;set;}
        public DbSet<Worker>? Workers {get;set;}
        public DbSet<Salary>? Salaries {get;set;}

        public HRManagementContext(DbContextOptions<HRManagementContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
            // ? Initial Data
            List<Person> peopleInit = new List<Person>();
            peopleInit.Add(new Person() {
                PersonId = 1,
                Name = "Jhon",
                Lastname = "Doe",
                Email = "jhon.doe@gmail.com",
                PersonalAddress = "Black house",
                Phone =  123,
                Picture = File.ReadAllBytes("assets/images/image1.jpg")
            });

            peopleInit.Add(new Person() {
                PersonId = 2,
                Name = "Mike",
                Lastname = "Ross",
                Email = "mike.ross@gmail.com",
                PersonalAddress = "Green house",
                Phone = 456,
                Picture = File.ReadAllBytes("assets/images/image2.jpg")
            });

            peopleInit.Add(new Person() {
                PersonId = 3,
                Name = "Rachel",
                Lastname = "Zane",
                Email = "rachel.zane@gmail.com",
                PersonalAddress = "Yellow house",
                Phone = 789,
                Picture = File.ReadAllBytes("assets/images/image3.png")
            });

            peopleInit.Add(new Person() {
                PersonId = 4,
                Name = "Donna",
                Lastname = "Paulsen",
                Email = "donna.paulsen@gmail.com",
                PersonalAddress = "Blue house",
                Phone = 321,
                Picture = File.ReadAllBytes("assets/images/image4.jpg")
            });

            peopleInit.Add(new Person() {
                PersonId = 5,
                Name = "Louis",
                Lastname = "Litt",
                Email = "louis.litt@gmail.com",
                PersonalAddress = "Red house",
                Phone = 654,
                Picture = File.ReadAllBytes("assets/images/image1.jpg")
            });

            modelBuilder.Entity<Person>(person => {
                person.ToTable("Person");
                person.HasKey(p => p.PersonId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                person.Property(p => p.Name).IsRequired();
                person.Property(p => p.Lastname).IsRequired();
                person.Property(p => p.Email).IsRequired();
                person.Property(p => p.PersonalAddress).IsRequired();
                person.Property(p => p.Phone);
                person.Property(p => p.Picture).IsRequired();
                person.HasMany<Worker>(p => p.Workers).WithOne(p => p.Person);
                person.Ignore(p => p.Workers);

                person.HasData(peopleInit);
            });

            // ? Initial Data
            List<Worker> workersInit = new List<Worker>();
            // workersInit.Add(new Worker() {WorkerId = 1, PersonId = 1, Rol = "worker", SalaryId = 1});
            // workersInit.Add(new Worker() {WorkerId = 2, PersonId = 2, Rol = "worker", SalaryId = 2});
            // workersInit.Add(new Worker() {WorkerId = 3, PersonId = 3, Rol = "worker", SalaryId = 3});
            // workersInit.Add(new Worker() {WorkerId = 4, PersonId = 5, Rol = "worker", SalaryId = 4});
            // workersInit.Add(new Worker() {WorkerId = 5, PersonId = 1, Rol = "specialist", SalaryId = 5});
            // workersInit.Add(new Worker() {WorkerId = 6, PersonId = 2, Rol = "specialist", SalaryId = 6});
            // workersInit.Add(new Worker() {WorkerId = 7, PersonId = 3, Rol = "specialist", SalaryId = 7});
            // workersInit.Add(new Worker() {WorkerId = 8, PersonId = 4, Rol = "specialist", SalaryId = 8});
            // workersInit.Add(new Worker() {WorkerId = 9, PersonId = 5, Rol = "manager", SalaryId = 9});
            // workersInit.Add(new Worker() {WorkerId = 10, PersonId = 4, Rol = "manager", SalaryId = 10});

            workersInit.Add(new Worker() {WorkerId = 1, PersonId = 1, Rol = "worker"});
            workersInit.Add(new Worker() {WorkerId = 2, PersonId = 2, Rol = "worker"});
            workersInit.Add(new Worker() {WorkerId = 3, PersonId = 3, Rol = "worker"});
            workersInit.Add(new Worker() {WorkerId = 4, PersonId = 5, Rol = "worker"});
            workersInit.Add(new Worker() {WorkerId = 5, PersonId = 1, Rol = "specialist"});
            workersInit.Add(new Worker() {WorkerId = 6, PersonId = 2, Rol = "specialist"});
            workersInit.Add(new Worker() {WorkerId = 7, PersonId = 3, Rol = "specialist"});
            workersInit.Add(new Worker() {WorkerId = 8, PersonId = 4, Rol = "specialist"});
            workersInit.Add(new Worker() {WorkerId = 9, PersonId = 5, Rol = "manager"});
            workersInit.Add(new Worker() {WorkerId = 10, PersonId = 4, Rol = "manager"});
            

            modelBuilder.Entity<Worker>(worker => {
                worker.ToTable("Worker");
                worker.HasKey(p => p.WorkerId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                worker.HasOne<Person>(p => p.Person).WithMany(p => p.Workers).HasForeignKey(p => p.PersonId);
                worker.HasMany<Salary>(w => w.Salaries).WithOne(s => s.Worker).HasForeignKey(w => w.WorkerId).OnDelete(DeleteBehavior.Cascade);
                worker.Property(p => p.Rol).IsRequired();
                worker.Property(w => w.Salary);
                worker.Property(p => p.WorkingStartDate).IsRequired().HasDefaultValueSql("GETDATE()");
                worker.Ignore(p => p.Salaries);

                worker.HasData(workersInit);
            });

            // ? Initial Data
            List<Salary> salariesInit = new List<Salary>();
            salariesInit.Add(new Salary() {SalaryId = 1, WorkerId = 1, SalaryValue = 100, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 2, WorkerId = 2, SalaryValue = 100, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 3, WorkerId = 3, SalaryValue = 100, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 4, WorkerId = 4, SalaryValue = 100, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 5, WorkerId = 5, SalaryValue = 200, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 6, WorkerId = 6, SalaryValue = 200, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 7, WorkerId = 7, SalaryValue = 200, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 8, WorkerId = 8, SalaryValue = 200, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 9, WorkerId = 9, SalaryValue = 300, Active = true});
            salariesInit.Add(new Salary() {SalaryId = 10, WorkerId =10, SalaryValue = 300, Active = true});

            modelBuilder.Entity<Salary>(salary => {
                salary.ToTable("Salary");
                salary.HasKey(p => p.SalaryId);
                // salary.HasOne<Worker>(s => s.Worker).WithMany(w => w.Salaries).HasForeignKey(s => s.WorkerId).OnDelete(DeleteBehavior.Cascade);
                salary.Property(p => p.SalaryUpdateDate).IsRequired().HasDefaultValueSql("GETDATE()");
                salary.Property(p => p.SalaryValue).IsRequired();
                salary.Property(p => p.Active);

                salary.HasData(salariesInit);
            });
        }
    }
}