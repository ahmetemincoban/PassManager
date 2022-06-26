using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PassManager.Models
{
    public class GroupModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Group Name")]
        public string groupName { get; set; }
        public bool isPassive { get; set; }

        public List<PassModel> PassList { get; set; }
        public string UserID { get; set; }
    }
}