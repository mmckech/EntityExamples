using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using FluentAssertions;
using NUnit.Framework;

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
            Animal expectedAnimal = new Animal {Name = "Steve", Legs = 2};

            //Act
            db.Animals.Add(expectedAnimal);
            Action act = () => db.SaveChanges();

            //Assert
            act.ShouldNotThrow();
            db.Animals.Find(expectedAnimal.Name).Should().NotBeNull();
        }
    }
}
