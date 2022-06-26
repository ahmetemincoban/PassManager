using System;
using System.ComponentModel.DataAnnotations;

namespace PassManager.Models
{
    public class PassModel
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Password Name")]
        [Required(ErrorMessage ="{0} field is required")]
        public string passName { get; set; }
        [Display(Name ="Password")]
        [Required(ErrorMessage ="{0} field is required")]
        public string Pass { get; set; }

        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public DateTime ModifiedDate { get; set; }
        public bool isPassive { get; set; }
        public string URL { get; set; }

        public string UserID { get; set; }
        [Display(Name ="Group")]
        [Required(ErrorMessage ="{0} field is required")]
        public int GroupID { get; set; }
        public GroupModel Group { get; set; }
    }
}