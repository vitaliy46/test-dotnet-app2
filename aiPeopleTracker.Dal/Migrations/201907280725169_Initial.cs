namespace aiPeopleTracker.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cameras",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        number = c.String(nullable: false),
                        state = c.Int(nullable: false),
                        description = c.String(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.camera_settings",
                c => new
                    {
                        camera_id = c.Int(nullable: false),
                        floor_plan_id = c.Int(nullable: false),
                        source_address = c.String(nullable: false),
                        x = c.Int(),
                        y = c.Int(),
                        is_active = c.Boolean(nullable: false),
                        id = c.Int(nullable: false, identity: true),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.camera_id)
                .ForeignKey("dbo.floor_plans", t => t.floor_plan_id, cascadeDelete: true)
                .ForeignKey("dbo.cameras", t => t.camera_id)
                .Index(t => t.camera_id)
                .Index(t => t.floor_plan_id);
            
            CreateTable(
                "dbo.floor_plans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(nullable: false),
                        uri = c.String(nullable: false),
                        uri_with_cameras = c.String(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.layout_template_camera_links",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        layout_template_id = c.Int(nullable: false),
                        camera_id = c.Int(nullable: false),
                        x = c.Int(nullable: false),
                        y = c.Int(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.layout_templates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        items_x = c.Int(nullable: false),
                        items_y = c.Int(nullable: false),
                        is_active = c.Boolean(nullable: false),
                        usage_index = c.Int(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.persons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        surname = c.String(nullable: false),
                        patronymic = c.String(nullable: false),
                        photo = c.Binary(),
                        description = c.String(),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.person_tags",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        create_date = c.DateTime(nullable: false),
                        update_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.person_person_tag_links",
                c => new
                    {
                        person_id = c.Int(nullable: false),
                        person_tag_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.person_id, t.person_tag_id })
                .ForeignKey("dbo.persons", t => t.person_id, cascadeDelete: true)
                .ForeignKey("dbo.person_tags", t => t.person_tag_id, cascadeDelete: true)
                .Index(t => t.person_id)
                .Index(t => t.person_tag_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.person_person_tag_links", "person_tag_id", "dbo.person_tags");
            DropForeignKey("dbo.person_person_tag_links", "person_id", "dbo.persons");
            DropForeignKey("dbo.camera_settings", "camera_id", "dbo.cameras");
            DropForeignKey("dbo.camera_settings", "floor_plan_id", "dbo.floor_plans");
            DropIndex("dbo.person_person_tag_links", new[] { "person_tag_id" });
            DropIndex("dbo.person_person_tag_links", new[] { "person_id" });
            DropIndex("dbo.camera_settings", new[] { "floor_plan_id" });
            DropIndex("dbo.camera_settings", new[] { "camera_id" });
            DropTable("dbo.person_person_tag_links");
            DropTable("dbo.person_tags");
            DropTable("dbo.persons");
            DropTable("dbo.layout_templates");
            DropTable("dbo.layout_template_camera_links");
            DropTable("dbo.floor_plans");
            DropTable("dbo.camera_settings");
            DropTable("dbo.cameras");
        }
    }
}
