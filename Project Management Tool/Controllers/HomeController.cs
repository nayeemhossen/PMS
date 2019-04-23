using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Project_Management_Tool.CustomizeLayout;
using Project_Management_Tool.Models;

namespace Project_Management_Tool.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ProjectManagementDBContext dbContext = new ProjectManagementDBContext();
        public ActionResult Index()
        {       
            return View();
        }
        [HttpGet]
        public ActionResult UserInfo(bool mail=false)
        {
            
            if (mail)
            {
                ViewBag.Massage = "Alreday exists";
            }
            
            ViewBag.Designations= new SelectList(Dropdownlist.GetDesignations(), "Value", "Text");
            return View();
        } 
        [HttpPost]
        public ActionResult UserInfo(tb_UserInfo user)
        {
            try
            {
                var unique = dbContext.tb_UserInfos.FirstOrDefault(x=>x.Email== user.Email);
                if (unique!=null)
                {
                    return RedirectToAction("UserInfo",new{mail=true});
                }               
                dbContext.tb_UserInfos.Add(user);
                dbContext.SaveChanges();
                //ViewBag.Massage = "Saved";
            }
            catch (Exception )
            {
                //ViewBag.Massage = "Try Again";
                return RedirectToAction("UserInfo");

            }
         
            return RedirectToAction("UserInfo");

        }
        public ActionResult UserList()
        {
            var model = dbContext.tb_UserInfos.ToList();
            return View(model);
        }
        public ActionResult EditUser(int userId)
        {
            var user = dbContext.tb_UserInfos.FirstOrDefault(x => x.UserInfoId == userId);
            ViewBag.Designations = new SelectList(Dropdownlist.GetDesignations(), "Value", "Text");
            if (user!=null)
            {
                return View(user);
            }
            return RedirectToAction("UserList");
        }
        [HttpPost]
        public ActionResult EditUser(tb_UserInfo user)
        {
            tb_UserInfo aUser = dbContext.tb_UserInfos.FirstOrDefault(x => x.UserInfoId == user.UserInfoId);
            try
            {
                if (aUser != null)
                {
                    aUser.UserInfoId = user.UserInfoId;
                }
                var unique = dbContext.tb_UserInfos.FirstOrDefault(x => x.Email == user.Email && x.UserInfoId!=user.UserInfoId);
                if (unique != null)
                {
                    ModelState.AddModelError("","Email Already exsist.");
                    return RedirectToAction("EditUser","Home",new{ userId = user.UserInfoId });
                }
                else
                {
                    // to remove  same key already exists in the ObjectStateManager
                    ((IObjectContextAdapter)dbContext).ObjectContext.Detach(aUser);


                    dbContext.Entry(user).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                
            }
            catch (Exception)

            {
                return RedirectToAction("UserList");
            } 
           
            
            return RedirectToAction("UserList");
        }
        public ActionResult DeleteUser(int userId)
        {
            tb_UserInfo aUser = dbContext.tb_UserInfos.FirstOrDefault(x => x.UserInfoId == userId);
            if (aUser!=null)
            {
                dbContext.tb_UserInfos.Remove(aUser);
                dbContext.SaveChanges();
            }
            return RedirectToAction("UserList");
        }
        [HttpGet]
        public ActionResult SaveProject(bool codeName = false)
        {
            if (codeName)
            {
                ViewBag.Massage = "Alreday exists";
            }

            
            return View();
        }
        [HttpPost]
        public ActionResult SaveProject(tb_ProjectInfo project,HttpPostedFileBase ProjectFile)
        {
            try
            {
                //Upload File
                string file = "";
                if (ProjectFile != null && ProjectFile.ContentLength > 0)
                {
                    //string fileExtension = Path.GetExtension(ProjectFile.FileName).ToLower();                   
                       // var fileName = Guid.NewGuid().ToString("N"); //imagefile.FileName;
                        file = ProjectFile.FileName ;
                        string fileLocation = Server.MapPath("~/Project File/") + file;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                    ProjectFile.SaveAs(fileLocation);                   
                }

                var unique = dbContext.tb_ProjectInfos.FirstOrDefault(x => x.CodeName == project.CodeName);
                if (unique != null)
                {
                    return RedirectToAction("SaveProject",new {codeName = true});
                }
                project.UploadFile = file;
                dbContext.tb_ProjectInfos.Add(project);
                dbContext.SaveChanges();
                //ViewBag.Massage = "Saved";
            }
            catch (Exception )
            {
                //ViewBag.Massage = "Try Again";
                return RedirectToAction("SaveProject");
            }
            return RedirectToAction("SaveProject");
        }
        public ActionResult ProjectList()
        {
            var model = dbContext.tb_ProjectInfos.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult EditProject(int projectId)
        {
            var getProject = dbContext.tb_ProjectInfos.FirstOrDefault(x => x.ProjectInfoId == projectId);
            if (getProject != null)
            {
                return View(getProject);
            }
            return RedirectToAction("ProjectList");
        }
        [HttpPost]
        public ActionResult EditProject(tb_ProjectInfo project, HttpPostedFileBase ProjectFile)
        {
            tb_ProjectInfo aproject = dbContext.tb_ProjectInfos.FirstOrDefault(x => x.ProjectInfoId == project.ProjectInfoId);
            try
            {
                if (aproject != null)
                {
                    aproject.ProjectInfoId = project.ProjectInfoId; 
                }
                string file = "";
                if (ProjectFile != null && ProjectFile.ContentLength > 0)
                {
                   // string fileExtension = Path.GetExtension(ProjectFile.FileName).ToLower();
                    //var fileName = Guid.NewGuid().ToString("N"); //imagefile.FileName;
                    file = ProjectFile.FileName;
                    string fileLocation = Server.MapPath("~/Project File/") + file;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    ProjectFile.SaveAs(fileLocation);
                }
                var unique = dbContext.tb_ProjectInfos.FirstOrDefault(x => x.CodeName == project.CodeName && x.ProjectInfoId != project.ProjectInfoId);
                if (unique != null)
                {
                    ModelState.AddModelError("", "Code Name Already exsist.");
                    return RedirectToAction("EditProject", new { codeName = true });
                }
                else
                {
                    // to remove  same key already exists in the ObjectStateManager
                    ((IObjectContextAdapter)dbContext).ObjectContext.Detach(aproject);
                    project.UploadFile = file;
                    dbContext.Entry(project).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
                ViewBag.Massage = "Saved";
            }
            catch (Exception)
            {
                return RedirectToAction("ProjectList");
            }
            ViewBag.Massage = "Try Again";
            return RedirectToAction("ProjectList");
        }
        public ActionResult ProjectDetails(int projectId)
        {
            var getProject = dbContext.tb_ProjectInfos.FirstOrDefault(x => x.ProjectInfoId == projectId);
            if (getProject != null)
            {
                var assignMember = (from tb_AssignPerson in dbContext.tb_AssignPersons
                            join tb_UserInfo in dbContext.tb_UserInfos on tb_AssignPerson.UserId equals tb_UserInfo.UserInfoId
                            where tb_AssignPerson.ProjectId == projectId
                            select new AssignMemberForIndividualProject
                            {
                                UserId = tb_AssignPerson.UserId,
                                PersonName = tb_UserInfo.Name
                            }).ToList();
                ViewBag.assignMember = assignMember;
                var assignTo = dbContext.tb_ProjectTasks.Where(x=>x.ProjectId== projectId).ToList();
                ViewBag.taskList = assignTo;
                return View(getProject);
            }
            return RedirectToAction("ProjectList");
        }
        public ActionResult DeleteProject(int projectId)
        {
            tb_ProjectInfo aProject = dbContext.tb_ProjectInfos.FirstOrDefault(x=> x.ProjectInfoId== projectId);
            if (aProject != null)
            {
                dbContext.tb_ProjectInfos.Remove(aProject);
                dbContext.SaveChanges();
            }
            return RedirectToAction("ProjectList");
        }
        public ActionResult AssignResourcePerson()
        {
            ViewBag.ProjectName = new SelectList(Dropdownlist.GetAllProjects(), "Value", "Text");
           ViewBag.UserName= new SelectList(Dropdownlist.GetAllUsers(), "Value", "Text");

            var model = (from tb_AssignPerson in dbContext.tb_AssignPersons
                         join tb_UserInfo in dbContext.tb_UserInfos on tb_AssignPerson.UserId equals tb_UserInfo.UserInfoId
                         join tb_ProjectInfo in dbContext.tb_ProjectInfos on tb_AssignPerson.ProjectId equals tb_ProjectInfo.ProjectInfoId
                         select new AssignPersonList
                         {
                             ProjectId = tb_AssignPerson.ProjectId,
                             ProjectName = tb_ProjectInfo.Name,
                             UserId = tb_AssignPerson.UserId,
                             PersonName = tb_UserInfo.Name,
                             PersonDesignation = tb_UserInfo.Designation,
                         }).ToList();
            ViewBag.assignPersonList = model;
            return View();
        }
        [HttpPost]
        public ActionResult AssignResourcePerson(tb_AssignPerson assignPerson)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var unique = dbContext.tb_AssignPersons.FirstOrDefault(x => x.UserId == assignPerson.UserId && x.ProjectId == assignPerson.ProjectId);
                    if (unique != null)
                    {
                        return RedirectToAction("AssignResourcePerson", new { userId = true });
                    }                 
                    dbContext.tb_AssignPersons.Add(assignPerson);
                    dbContext.SaveChanges();
                    var model = (from tb_AssignPerson in dbContext.tb_AssignPersons
                                 join tb_UserInfo in dbContext.tb_UserInfos on tb_AssignPerson.UserId equals tb_UserInfo.UserInfoId
                                 join tb_ProjectInfo in dbContext.tb_ProjectInfos on tb_AssignPerson.ProjectId equals tb_ProjectInfo.ProjectInfoId
                                 select new AssignPersonList
                                 {
                                     ProjectId = tb_AssignPerson.ProjectId,
                                     ProjectName = tb_ProjectInfo.Name,
                                     UserId = tb_AssignPerson.UserId,
                                     PersonName = tb_UserInfo.Name,
                                     PersonDesignation = tb_UserInfo.Designation,
                                 }).ToList();
                    ViewBag.assignPersonList=model;
                }
            }
            catch (Exception  a)
            {
                return RedirectToAction("AssignResourcePerson");
            }
            return RedirectToAction("AssignResourcePerson");
        }
        public ActionResult CreateTask()
        {
            ViewBag.ProjectName = new SelectList(Dropdownlist.GetAllProjects(), "Value", "Text");
            ViewBag.Priority = new SelectList(Dropdownlist.GetPriority(), "Value", "Text");
            return View();
        }
        public JsonResult GetUserByProjectId(int? projectId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var user = (from tb_AssignPerson in dbContext.tb_AssignPersons
                join tb_UserInfo in dbContext.tb_UserInfos on tb_AssignPerson.UserId equals tb_UserInfo.UserInfoId
                where tb_AssignPerson.ProjectId == projectId 
                select new
                {
                    ID = tb_AssignPerson.UserId,
                    Text = tb_UserInfo.Name
                }).ToList();
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateTask(tb_ProjectTask projectTask)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    dbContext.tb_ProjectTasks.Add(projectTask);
                    dbContext.SaveChanges();
                    ViewBag.message = "Save Successfully!";
                }

            }
            catch (Exception ) 
            {
                return RedirectToAction("CreateTask");
            }

            return RedirectToAction("CreateTask");
        }
        public ActionResult AddComment()
        {
            ViewBag.ProjectName = new SelectList(Dropdownlist.GetAllProjects(), "Value", "Text");
            return View();
        }
        public JsonResult GetTaskByProjectId(int? projectId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var task = (from tb_ProjectTask in dbContext.tb_ProjectTasks
                        where tb_ProjectTask.ProjectId == projectId
                        select new
                        {
                            ID = tb_ProjectTask.ProjectTaskId,
                            Text = tb_ProjectTask.Description
                        }).ToList();
            return Json(task, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddComment(tb_TaskComment taskComment)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    dbContext.tb_TaskComments.Add(taskComment);
                    dbContext.SaveChanges();
                    ViewBag.message = "Save Successfully!";
                }

            }
            catch (Exception exception)
            {
                return RedirectToAction("AddComment");
            }

            return RedirectToAction("AddComment");
        }
        public ActionResult ViewTaskComment()
        {
            ViewBag.ProjectName = new SelectList(Dropdownlist.GetAllProjects(), "Value", "Text");
            var taskComents = (from tb_ProjectTask in dbContext.tb_ProjectTasks
                join tb_TaskComment in dbContext.tb_TaskComments on tb_ProjectTask.ProjectTaskId equals tb_TaskComment.TaskId
                join tb_UserInfo in dbContext.tb_UserInfos on tb_ProjectTask.UserId equals tb_UserInfo.UserInfoId
                //where tb_ProjectTask.ProjectTaskId == taskId
                select new ViewComment
                {
                    TaskId = tb_ProjectTask.ProjectTaskId,
                    Cmnt = tb_TaskComment.Comment,
                    UserId = tb_TaskComment.UpdateId,
                    AssignBy = tb_UserInfo.Name,
                    CommentDateTime = tb_ProjectTask.DueDate
                }).ToList();
            ViewBag.ViewTaskComment = taskComents;
            return View();                                       
        }
        public JsonResult GetTaskDropDownByProjectId(int? projectId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var task = (from tb_ProjectTask in dbContext.tb_ProjectTasks
                where tb_ProjectTask.ProjectId == projectId
                select new
                {
                    ID = tb_ProjectTask.ProjectTaskId,
                    Text = tb_ProjectTask.Description
                }).ToList();
            return Json(task, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCommentByTaskId(int? taskId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var viewComments = (from tb_ProjectTask in dbContext.tb_ProjectTasks
                join tb_TaskComment in dbContext.tb_TaskComments on tb_ProjectTask.ProjectTaskId equals tb_TaskComment.TaskId
                join tb_UserInfo in dbContext.tb_UserInfos on tb_ProjectTask.UserId equals tb_UserInfo.UserInfoId
                where tb_ProjectTask.ProjectTaskId == taskId
                select new ViewComment
                {
                    TaskId = tb_ProjectTask.ProjectTaskId,
                    Cmnt = tb_TaskComment.Comment,
                    UserId = tb_TaskComment.UpdateId,
                    AssignBy = tb_UserInfo.Name,
                    CommentDateTime = tb_ProjectTask.DueDate
                }).ToList();
            return Json(viewComments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAllProject()
        {
            //var viewProject = dbContext.tb_ProjectInfos.ToList();
            var viewProject = (from t1 in dbContext.tb_AssignPersons
                               join t2 in dbContext.tb_ProjectInfos on t1.ProjectId equals t2.ProjectInfoId
                group new {t1.ProjectId, t2.Name, t2.CodeName, t2.Status} by new { ProjectId=t1.ProjectId,Name= t2.Name,Code= t2.CodeName, Status = t2.Status} into grp
                select new
                {
                    Id = grp.Key.ProjectId,
                    Name = grp.Key.Name,
                    CodeName = grp.Key.Code,
                    Status = grp.Key.Status,
                    NoOfMember= grp.Select(x=>x.ProjectId).Count()
                    
                }).ToList();
            List<ViewAllProjectList> model=new List<ViewAllProjectList>();
            
            foreach (var x in viewProject)
            {
                ViewAllProjectList obj = new ViewAllProjectList();
                obj.Name = x.Name;
                obj.CodeName = x.CodeName;
                obj.Status = x.Status;
                obj.NoOfMember = x.NoOfMember;
                obj.NoOfTask = dbContext.tb_ProjectTasks.Count(a=>a.ProjectId==x.Id);
                model.Add(obj);
            }
            return View(model);
        }
    }
}