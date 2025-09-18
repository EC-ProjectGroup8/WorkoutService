using Data.Entities;
using Domain.Models;
namespace Business.Factories;
public class WorkoutFactory
{
    public static void UpdateWorkoutEntity(WorkoutEntity currentEntity, WorkoutCreationRequest updateRequest)
    {
        currentEntity.Location = updateRequest.Location!;
        currentEntity.Title = updateRequest.Title!;
        currentEntity.StartTime = updateRequest.StartTime!;
        currentEntity.Instructor = updateRequest.Instructor!;
    }
}
