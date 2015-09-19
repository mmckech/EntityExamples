namespace Animals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSpecies : DbMigration
    {
        public override void Up()
        {
            AddColumn("ANIMAL.Animals", "Species", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("ANIMAL.Animals", "Species");
        }
    }
}
