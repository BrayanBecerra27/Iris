using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace IrisCore.Entities
{
    [DynamoDBTable("TaskToDo")]
    public class TaskToDo
    {
        [DynamoDBHashKey]
        [Required]
        public string Id { get; set; }
        [DynamoDBProperty]
        [Required]
        public string Description { get; set; }
        [DynamoDBProperty]
        public bool IsCompleted { get; set; }
        [DynamoDBProperty]
        public bool IsFavorite { get; set; }
        [DynamoDBProperty]
        public DateTime CreationDate { get; set; }
    }
}
