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
        public void DatabaseTest()
        {
            CreateTest();
            MigrateDatabase();
            AddDataTest();
        }

        public void CreateTest()
        {
            //Arrange
            Database db = new Database();
            db.Database.Delete();

            //Act
            bool created;
            Action act = () => created = db.Database.CreateIfNotExists();

            //Assert
            act.ShouldThrow<InvalidOperationException>("Migrations are enabled so a simple create should fail");
        }

        public void AddDataTest()
        {
            //Arrange
            Database db = new Database();
            Animal expectedAnimal = new Animal {Name = "Allan", Species = "dog", Legs = 2, Edible = true};
            db.Database.ExecuteSqlCommand(@"DELETE FROM ""ANIMAL"".""Animals""");

            //Act
            db.Animals.Add(expectedAnimal);
            Action act = () => db.SaveChanges();

            //Assert
            act.ShouldNotThrow();
            db.Animals.Find(expectedAnimal.Name).Should().NotBeNull();
        }

        public void MigrateDatabase()
        {
            //Arrange

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

            Console.WriteLine("Migrating...");
            foreach (string s in migrator.GetPendingMigrations())
            {
                //Act
                Console.WriteLine("Applying migration {0}", s);
                Action act = () => migrator.Update(s);

                //Assert
                act.ShouldNotThrow();
            }
        }
    }
}
