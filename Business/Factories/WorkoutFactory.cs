using Data.Entities;
using Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Business.Factories;
public class WorkoutFactory
{
    public static void UpdateWorkoutEntity(WorkoutEntity currentEntity, WorkoutCreationRequest updateRequest)
    {
        currentEntity.Location = updateRequest.Location!;
        currentEntity.Title = updateRequest.Title!;
        currentEntity.StartTime = updateRequest.StartTime!;
        currentEntity.Instructor = updateRequest.Instructor!;
        if (updateRequest.Description != null)
            currentEntity.Description = updateRequest.Description;
    }
}
