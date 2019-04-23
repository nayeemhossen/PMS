using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management_Tool.Models
{
    public class BaseClass
    {
        public int CreateId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int UpdateId { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}