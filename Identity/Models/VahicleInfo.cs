using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Identity.Models
{
    //saving vahicle Info
    public class VahicleInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Reg No is Mandatory Field")]
        [Display(Name = "Reg No")]
        [Remote("IsUniqueRegNo", "VahicleInfoes", ErrorMessage = "Code already exists!")]
        public string RegNo { get; set; }

        [Required(ErrorMessage = "Engine No is Mandatory Field")]
        [Display(Name = "Engine No")]
        [Remote("IsUniqueEngineNo", "VahicleInfoes", ErrorMessage = "Code already exists!")]
        public string EngineNo { get; set; }

    }
}