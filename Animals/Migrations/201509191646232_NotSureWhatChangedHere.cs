namespace Animals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotSureWhatChangedHere : DbMigration
    {
        public override void Up()
        {
            AddColumn("ANIMAL.Animals", "Edible", c => c.Boolean(nullable: true, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("ANIMAL.Animals", "Edible");
        }
    }
}
