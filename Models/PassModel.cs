using System;
using System.ComponentModel.DataAnnotations;

namespace PassManager.Models
{
    public class PassModel
    {
        [Key]
        public int ID { get; set; }
        public string passName { get; set; }
        public string Pass { get; set; }

        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public DateTime ModifiedDate { get; set; }
        public bool isPassive { get; set; }
        public string URL { get; set; }

        public string UserID { get; set; }
        public int GroupID { get; set; }
        public GroupModel Group { get; set; }
    }
}