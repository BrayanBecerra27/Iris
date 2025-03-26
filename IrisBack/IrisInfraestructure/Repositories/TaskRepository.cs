using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using IrisCore.Entities;
using IrisCore.InterfacesRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisInfraestructure.Repositories
{
    public class TaskRepository(DynamoDBContext dBContext) : GenericRepository<TaskToDo>(dBContext), ITaskRepository
    {
    }
}
