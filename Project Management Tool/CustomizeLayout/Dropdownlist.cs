using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Project_Management_Tool.Models;

namespace Project_Management_Tool.CustomizeLayout
{
    public class Dropdownlist
    {
        public static IList<SelectListItem> GetDesignations()
        {
            IList<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem {Value = "Developer", Text = "Developer"});
            result.Add(new SelectListItem {Value = "Project Manager", Text = "Project Manager"});
            result.Add(new SelectListItem {Value = "Team Leader", Text = "Team Leader"});
            result.Add(new SelectListItem {Value = "QA Engineer", Text = "QA Engineer"});
            result.Add(new SelectListItem {Value = "UX Engineer", Text = "UX Engineer"});
            result.Add(new SelectListItem {Value = "IT Admin", Text = "IT Admin"});
            return result;
        }
        //public static IList<SelectListItem> GetStatus()
        //{
        //    IList<SelectListItem> result = new List<SelectListItem>();
        //    result.Add(new SelectListItem { Value = "Active", Text = "Active" });
        //    result.Add(new SelectListItem { Value = "Inactive", Text = "Inactive" });
        //    return result;
        //}
        public static IList<SelectListItem> GetAllProjects()
        {
            IList<SelectListItem> result = new List<SelectListItem>();
            using (var db = new ProjectManagementDBContext())
            {

                var projectlist = db.tb_ProjectInfos.ToList();
                foreach (var project in projectlist)
                {
                    result.Add(new SelectListItem { Value = project.ProjectInfoId.ToString(), Text = project.Name });
                }
            }
            return result;
        }
        public static IList<SelectListItem> GetAllUsers()
        {
            IList<SelectListItem> result = new List<SelectListItem>();
            using (var db = new ProjectManagementDBContext())
            {
                var userlist = db.tb_UserInfos.ToList();
                foreach (var user in userlist)
                {
                    if (user.Status==true && user.Designation!= "IT Admin")
                    {
                        result.Add(new SelectListItem { Value = user.UserInfoId.ToString(), Text = user.Name });
                    }
                   
                }
            }
            return result;
        }
        public static IList<SelectListItem> GetPriority()
        {
            IList<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem { Value = "High", Text = "High" });
            result.Add(new SelectListItem { Value = "Medium", Text = "Medium" });
            result.Add(new SelectListItem { Value = "Low", Text = "Low" });
            return result;
        }
        //public static IList<SelectListItem> GetItemNameWithSizeItemsSalesTransactionWise(string transid)
        //{
        //    IList<SelectListItem> result = new List<SelectListItem>();
        //    using (var db = new InventoryAccountDbContext())
        //    {
        //        var itemlist = (from item in db.ItemDb
        //            join trans in db.TransactionDetails on item.ItemId equals trans.ItemId
        //            where trans.TransactionId == transid
        //            select new { item.ItemId, item.ItemName, item.SizeName }).Distinct();
        //        foreach (var item in itemlist)
        //        {
        //            result.Add(new SelectListItem { Value = item.ItemId, Text = item.ItemName + "|" + item.SizeName });
        //        }
        //    }
        //    return result;
        //}
    }
    }
   