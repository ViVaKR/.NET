using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ToDoGrpc.Services;

public class ToDoService(AppDbContext Db) : ToDoIt.ToDoItBase
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GetAllResponse> ListToDo(GetAllRequest request, ServerCallContext context)
    {
        var res = new GetAllResponse();
        var toDoItems = await Db.ToDoItems.ToListAsync();
        foreach (var toDoItem in toDoItems)
        {
            res.ToDo.Add(new ReadTodoResponse
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                ToDoStatus = toDoItem.ToDoStatus
            });
        }
        return await Task.FromResult(res);
    }

    public override async Task<ReadTodoResponse> ReadToDo(ReadToDoRequest request, ServerCallContext context)
    {
        if (request.Id <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Resouce index must be greater than 0"));

        ToDoItem? toDoItem = await Db.ToDoItems.FindAsync(request.Id);

        if (toDoItem != null)
            return await Task.FromResult(new ReadTodoResponse
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                ToDoStatus = toDoItem.ToDoStatus
            });

        throw new RpcException(new Status(StatusCode.NotFound, $"No Task with Id: {request.Id}"));
    }

    public override async Task<CreateToDoResponse> CreateToDo(CreateToDoRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply a valid object"));

        var toDoItem = new ToDoItem
        {
            Title = request.Title,
            Description = request.Description
        };

        await Db.AddAsync(toDoItem);
        await Db.SaveChangesAsync();

        return await Task.FromResult(new CreateToDoResponse { Id = toDoItem.Id });
    }



    public override async Task<UpdateToDoResponse> UpdateToDo(UpdateToDoRequest request, ServerCallContext context)
    {
        if (request.Id <= 0 || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Resouce index must be greater than 0"));

        var toDoItem = await Db.ToDoItems.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"No Task with Id {request.Id}"));

        toDoItem.Title = request.Title;
        toDoItem.Description = request.Description;
        toDoItem.ToDoStatus = request.ToDoStatus;
        await Db.SaveChangesAsync();
        return await Task.FromResult(new UpdateToDoResponse { Id = toDoItem.Id });
    }

    public override async Task<DeleteToDoRespoonse> DeleteToDo(DeleteToDoRequest request, ServerCallContext context)
    {
        if (request.Id <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Resouce index must be greater than 0"));

        var toDoItem = await Db.ToDoItems.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"No Task with Id {request.Id}"));

        Db.ToDoItems.Remove(toDoItem);
        await Db.SaveChangesAsync();
        return await Task.FromResult(new DeleteToDoRespoonse { Id = request.Id });
    }
}
