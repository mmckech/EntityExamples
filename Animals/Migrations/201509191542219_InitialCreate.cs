namespace Animals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ANIMAL.Animals",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Legs = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("ANIMAL.Animals");
        }
    }
}
