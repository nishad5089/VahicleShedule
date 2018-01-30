using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace Identity.Models
{
    public class SheduleVahicle
    {
        public int Id { get; set; }
       
        public virtual VahicleInfo VahicleInfo { get; set; }
        [Required(ErrorMessage = "Vahicle Info Must Not be Empty")]
        public int VahicleInfoId { get; set; }

        [Required(ErrorMessage = "Date Must Not be Empty")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime? SheduleDate { get; set;}

       
        public virtual VahicleShift Shift { get; set; }
        [Required(ErrorMessage = "Vahicle Shift Must Not be Empty")]
        [Display(Name = "Select Shift")]
        public int ShiftId { get; set; }
        [Display(Name = "Booked By")]
        [Required]
        public string BookedBy { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
    }
}