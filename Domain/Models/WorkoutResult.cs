namespace Domain.Models;

public class WorkoutResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
}

public class WorkoutResult<T> : WorkoutResult
{
    public T? Result { get; set; }
}