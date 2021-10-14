using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EvenEndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public int OrganazierId { get; set; }
        public Person Organazier { get; set; }

    }
}
