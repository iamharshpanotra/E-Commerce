using System;
namespace ECommerceAPI.Core.Entities
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }


        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
