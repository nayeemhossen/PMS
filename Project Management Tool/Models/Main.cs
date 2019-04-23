using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Project_Management_Tool.Models
{
    [Table("tb_UserInfo")]
    public class tb_UserInfo:BaseClass
    {
        [Key]
        public int UserInfoId { get; set; }
        [Required(ErrorMessage = "* A valid Name is required.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Enter Your valid Email Address.")]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Password is required.")]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Default Password")]
        public string DefaultPassword { get; set; }
        [Required(ErrorMessage = "* Please Select  Status.")]
        [Display(Name = "Status")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "* A valid Designation is required.")]
        [StringLength(20)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
    }
    [Table("tb_ProjectInfo")]
    public class tb_ProjectInfo : BaseClass
    {
        [Key]
        public int ProjectInfoId { get; set; }
        [Required(ErrorMessage = "* A valid Project Name is required.")]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* A valid Code Name is required.")]
        [StringLength(15, MinimumLength = 2)]
        [Display(Name = "Code Name")]
        public string CodeName { get; set; }
        [Required(ErrorMessage = "* A valid Description is required.")]
        [StringLength(50, MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "* Select a Start Date is required.")]
        [Display(Name = "Possible Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "* Select a End Date is required.")]
        [Display(Name = "Possible End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        [Display(Name = "Upload File")]
        public string UploadFile { get; set; }
        [Required(ErrorMessage = "* Select Status is required.")]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
    [Table("tb_AssignPerson")]
    public class tb_AssignPerson: BaseClass
    {
        [Key]
        public int AssignPersonId { get; set; }
        [Required(ErrorMessage = "* Select a Project is required.")]
        [ForeignKey("tb_ProjectInfo")]
        public int ProjectId { get; set; }
        public virtual tb_ProjectInfo tb_ProjectInfo { get; set; }
        [Required(ErrorMessage = "* Select a User is required.")]
        [ForeignKey("tb_UserInfo")]
        public int UserId { get; set; }
        public virtual tb_UserInfo tb_UserInfo { get; set; }
    }
    [Table("tb_ProjectTask")]
    public class tb_ProjectTask : BaseClass
    {
        [Key]
        public int ProjectTaskId { get; set; }
        [Required (ErrorMessage = "* Select a Project is required.")]
        [ForeignKey("tb_ProjectInfo")]
        public int ProjectId { get; set; }
        public virtual tb_ProjectInfo tb_ProjectInfo { get; set; }
        [Required(ErrorMessage = "* Select a Person is required.")]
        [ForeignKey("tb_UserInfo")]
        public int UserId { get; set; }
        public virtual tb_UserInfo tb_UserInfo { get; set; }
        [Required (ErrorMessage = "* A valid Description is required.")]
        [StringLength(30, MinimumLength = 2)]
        public string Description { get; set; }
        [Required (ErrorMessage = "* A valid Date is required.")]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "* Priority is required.")]
        public string Priority { get; set; }
    }
    [Table("tb_TaskComment")]
    public class tb_TaskComment : BaseClass
    {
        [Key]
        public int TaskCommentId { get; set; }
        [Required(ErrorMessage = "* Select a Project is required.")]
        [ForeignKey("tb_ProjectInfo")]
        public int ProjectId { get; set; }
        public virtual tb_ProjectInfo tb_ProjectInfo { get; set; }
        [Display(Name = "Project Task")]
        [Required(ErrorMessage = "* Select a Task is required.")]
        [ForeignKey("tb_ProjectTask")]
        public int TaskId { get; set; }
        public virtual tb_ProjectTask tb_ProjectTask { get; set; }
        [Required(ErrorMessage = "* Select a Task is required.")]
        [StringLength(30, MinimumLength = 2)]
        public string Comment { get; set; }
       
    }


    public class AssignPersonList
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UserId { get; set; }
        public string PersonName { get; set; }
        public string PersonDesignation { get; set; }
    }
    public class AssignMemberForIndividualProject
    {
       
        public int UserId { get; set; }
        public string PersonName { get; set; }
      
    }
    public class ViewComment:BaseClass
    {
        public string Cmnt { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string AssignBy { get; set; }
        public DateTime CommentDateTime { get; set; }
    }

    public class ViewAllProjectList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Status { get; set; }        
        public int NoOfMember { get; set; }
        public int NoOfTask { get; set; }
    }
}