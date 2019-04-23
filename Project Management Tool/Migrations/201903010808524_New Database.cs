namespace Project_Management_Tool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_TaskComment", "UserId", "dbo.tb_UserInfo");
            DropForeignKey("dbo.tb_TaskComment", "ProjectTaskId", "dbo.tb_ProjectTask");
            DropIndex("dbo.tb_TaskComment", new[] { "ProjectTaskId" });
            DropIndex("dbo.tb_TaskComment", new[] { "UserId" });
            RenameColumn(table: "dbo.tb_TaskComment", name: "ProjectTaskId", newName: "TaskId");
            AlterColumn("dbo.tb_TaskComment", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_TaskComment", "TaskId");
            AddForeignKey("dbo.tb_TaskComment", "TaskId", "dbo.tb_ProjectTask", "ProjectTaskId", cascadeDelete: false);
            DropColumn("dbo.tb_TaskComment", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_TaskComment", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_TaskComment", "TaskId", "dbo.tb_ProjectTask");
            DropIndex("dbo.tb_TaskComment", new[] { "TaskId" });
            AlterColumn("dbo.tb_TaskComment", "TaskId", c => c.Int());
            RenameColumn(table: "dbo.tb_TaskComment", name: "TaskId", newName: "ProjectTaskId");
            CreateIndex("dbo.tb_TaskComment", "UserId");
            CreateIndex("dbo.tb_TaskComment", "ProjectTaskId");
            AddForeignKey("dbo.tb_TaskComment", "ProjectTaskId", "dbo.tb_ProjectTask", "ProjectTaskId");
            AddForeignKey("dbo.tb_TaskComment", "UserId", "dbo.tb_UserInfo", "UserInfoId", cascadeDelete: false);
        }
    }
}
