Console.WriteLine("Hello, World!");
using System.Net.Http.Json;

var baseUrl = "https://localhost:5001/api/todo";
var httpClient = new HttpClient();

while (true)
{
    Console.WriteLine("1. Show tasks");
    Console.WriteLine("2. Add task");
    Console.WriteLine("3. Mark task as done");
    Console.WriteLine("4. Delete task");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var tasks = await httpClient.GetFromJsonAsync<List<TodoItem>>(baseUrl);
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title} | Done: {task.IsDone}");
                }
            }
            break;

        case "2":
            Console.Write("Task title: ");
            var title = Console.ReadLine();
            await httpClient.PostAsJsonAsync(baseUrl, new TodoItem { Title = title ?? "", IsDone = false });
            Console.WriteLine("Task added.");
            break;

        case "3":
            Console.Write("Task id: ");
            var doneId = Console.ReadLine();
            await httpClient.PutAsync($"{baseUrl}/{doneId}/done", null);
            Console.WriteLine("Task marked as done.");
            break;

        case "4":
            Console.Write("Task id: ");
            var deleteId = Console.ReadLine();
            await httpClient.DeleteAsync($"{baseUrl}/{deleteId}");
            Console.WriteLine("Task deleted.");
            break;

        case "0":
            return;
    }

    Console.WriteLine();
}

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}