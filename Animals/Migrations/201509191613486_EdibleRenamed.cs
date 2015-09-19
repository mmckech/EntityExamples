namespace Animals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EdibleRenamed : DbMigration
    {
        public override void Up()
        {
            AddColumn("ANIMAL.Animals", "Edible", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("ANIMAL.Animals", "Edible");
        }
    }
}
