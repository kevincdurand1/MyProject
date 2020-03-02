using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Infrastructure.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        string CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        string UpdatedBy { get; set; }

        DateTime UpdatedDate { get; set; }

        bool IsDeleted { get; set; }

        string DeletedBy { get; set; }
        
        DateTime DeletedDate { get; set; }

    }
}
