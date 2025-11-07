namespace ECommerceAPI.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "CreatedBy", c => c.String());
            AddColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "UpdatedBy", c => c.String());
            AddColumn("dbo.Categories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Categories", "DeletedBy", c => c.String());
            AddColumn("dbo.Categories", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Products", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "CreatedBy", c => c.String());
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "UpdatedBy", c => c.String());
            AddColumn("dbo.Products", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Products", "DeletedBy", c => c.String());
            AddColumn("dbo.Products", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DeletedDate");
            DropColumn("dbo.Products", "DeletedBy");
            DropColumn("dbo.Products", "UpdatedDate");
            DropColumn("dbo.Products", "UpdatedBy");
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Products", "CreatedBy");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Products", "IsActive");
            DropColumn("dbo.Categories", "DeletedDate");
            DropColumn("dbo.Categories", "DeletedBy");
            DropColumn("dbo.Categories", "UpdatedDate");
            DropColumn("dbo.Categories", "UpdatedBy");
            DropColumn("dbo.Categories", "CreatedDate");
            DropColumn("dbo.Categories", "CreatedBy");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Categories", "IsActive");
        }
    }
}
