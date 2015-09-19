using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using Animals.Migrations;
using FluentAssertions;
using NUnit.Framework;
using Database = Animals.Database;

namespace AnimalsTests
{
    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        public void CreateTest()
        {
            //Arrange
            Database db = new Database();

            //Act
            bool created = db.Database.CreateIfNotExists();

            //Assert
            created.Should().BeTrue();
        }

        [Test]
        public void AddDataTest()
        {
            //Arrange
            Database db = new Database();
            Animal expectedAnimal = new Animal {Name = "Allan", Species = "dog", Legs = 2, Edible = true};

            //Act
            db.Animals.Add(expectedAnimal);
            Action act = () => db.SaveChanges();

            //Assert
            act.ShouldNotThrow();
            db.Animals.Find(expectedAnimal.Name).Should().NotBeNull();
        }

        [Test]
        public void MigrateDatabase()
        {
            //Arrange
            //MigrateDatabaseToLatestVersion<Database, Configuration> initializer = 
            //    new MigrateDatabaseToLatestVersion<Database, Configuration>("Database");
            //System.Data.Entity.Database.SetInitializer<Database>(initializer);

            Animals.Migrations.Configuration config = new Configuration();
            DbMigrator migrator = new DbMigrator(config);

            Console.WriteLine("Past migrations:");
            foreach (string s in migrator.GetDatabaseMigrations())
                Console.WriteLine(s);

            Console.WriteLine("Local migrations:");
            foreach (string s in migrator.GetLocalMigrations())
                Console.WriteLine(s);

            Console.WriteLine("Pending migrations:");
            foreach (string s in migrator.GetPendingMigrations())
                Console.WriteLine(s);

            //Act
            Console.WriteLine("Migrating...");
            foreach (string s in migrator.GetPendingMigrations())
            {
                Console.WriteLine("Applying migration {0}", s);
                migrator.Update(s);
            }

            //Assert
            Animal expectedAnimal = new Animal { Name = "Allan", Species = "dog", Legs = 2, Edible = true };
            Database db = new Database();
            db.Animals.Add(expectedAnimal);
            Action act = () => db.SaveChanges();
            act.ShouldNotThrow();
            db.Animals.Find(expectedAnimal.Name).Should().NotBeNull();

        }
    }
}
