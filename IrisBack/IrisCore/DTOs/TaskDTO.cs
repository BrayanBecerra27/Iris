using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisCore.DTOs
{
    public class TaskDTO
    {
        public string Id { get; set; }
        public required string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsFavorite { get; set; }
    }
}
