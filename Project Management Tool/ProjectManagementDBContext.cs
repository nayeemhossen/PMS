using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Project_Management_Tool.Models;

namespace Project_Management_Tool
{
    public class ProjectManagementDBContext:DbContext
    {
        public ProjectManagementDBContext()
            : base("name=ProjectManagementToolDBConnection")
        {
            Database.SetInitializer<ProjectManagementDBContext>(new CreateDatabaseIfNotExists<ProjectManagementDBContext>());
        }
        public virtual DbSet<tb_ProjectInfo> tb_ProjectInfos { get; set; }
        public virtual DbSet<tb_UserInfo> tb_UserInfos { get; set; }
        public virtual DbSet<tb_AssignPerson> tb_AssignPersons { get; set; }
        public virtual DbSet<tb_ProjectTask> tb_ProjectTasks { get; set; }
        public virtual DbSet<tb_TaskComment> tb_TaskComments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ProjectManagementDBContext>(new CreateDatabaseIfNotExists<ProjectManagementDBContext>());
            
            //modelBuilder.Entity<ItemDb>().Property(p => p.SalePrice).HasPrecision(18, 2); // or whatever your schema specifies
            
        }
    }

}