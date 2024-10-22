
public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo()
    {
    }

    public Todo(int id, string title, bool isCompleted)
    {
        Id = id;
        Title = title;
        IsCompleted = isCompleted;
    }
}
