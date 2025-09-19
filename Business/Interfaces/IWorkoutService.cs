using System.Linq.Expressions;
using Data.Entities;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IWorkoutService
    {
        Task<WorkoutResult> CreateWorkoutAsync(WorkoutCreationRequest request);
        Task<WorkoutResult<IEnumerable<WorkoutResponseModel>>> GetAllWorkoutsAsync();
        Task<WorkoutResult<WorkoutResponseModel>> GetWorkoutByExpressionAsync(Expression<Func<WorkoutEntity, bool>> expression);
        Task<WorkoutResult> UpdateWorkoutAsync(string id, WorkoutCreationRequest updateRequest);
        Task<WorkoutResult> DeleteEventAsync(string id);
    }
}