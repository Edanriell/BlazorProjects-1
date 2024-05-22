using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Pages;

public partial class Index
{
    private string? error;
    private string? newTask;

    private IList<TaskItem>? tasks;
    [Inject] public HttpClient Http { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var requestUri = "api/TaskItems";
            tasks = await Http.GetFromJsonAsync<IList<TaskItem>>(requestUri);
        }
        catch (Exception)
        {
            error = "Error Encountered";
        }
    }

    private async Task CheckboxChecked(TaskItem task)
    {
        task.IsComplete = !task.IsComplete;
        var requestUri = $"api/TaskItems/{task.TaskItemId}";

        var response = await Http.PutAsJsonAsync(requestUri, task);

        if (!response.IsSuccessStatusCode) error = response.ReasonPhrase;
    }

    private async Task DeleteTask(TaskItem taskItem)
    {
        tasks!.Remove(taskItem);
        var requestUri = $"api/TaskItems/{taskItem.TaskItemId}";

        var response = await Http.DeleteAsync(requestUri);

        if (!response.IsSuccessStatusCode) error = response.ReasonPhrase;
    }

    private async Task AddTask()
    {
        if (!string.IsNullOrWhiteSpace(newTask))
        {
            var newTaskItem = new TaskItem
            {
                TaskName = newTask,
                IsComplete = false
            };

            tasks!.Add(newTaskItem);
            var requestUri = "api/TaskItems";

            var response = await Http.PostAsJsonAsync(requestUri, newTaskItem);

            if (response.IsSuccessStatusCode)
                newTask = string.Empty;
            else
                error = response.ReasonPhrase;
        }
    }
}