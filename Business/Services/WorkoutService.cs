using Business.Factories;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using System.Linq.Expressions;

namespace Business.Services;

public class WorkoutService(IWorkoutRepository workoutRepository) : IWorkOutService
{
    private readonly IWorkoutRepository _workoutRepository = workoutRepository;

    public async Task<WorkoutResult> CreateWorkoutAsync(WorkoutCreationRequest request)
    {
		try
		{
			var workoutEntity = request.MapTo<WorkoutEntity>();
			var result = await _workoutRepository.AddAsync(workoutEntity);
			return result.Success
				? new WorkoutResult { Success = true } : throw new Exception("Failed to add workout entity.");
		}
		catch (Exception ex)
		{
			return new WorkoutResult
			{
				Success = false,
				Error = ex.Message
			};
		}
    }

    public async Task<WorkoutResult<IEnumerable<WorkoutResponseModel>>> GetAllWorkoutsAsync()
    {
        try
        {
            var repositoryResult = await _workoutRepository.GetAllAsync();

            if (!repositoryResult.Success || repositoryResult.Result == null)
            {
                return new WorkoutResult<IEnumerable<WorkoutResponseModel>>
                {
                    Success = false,
                    Error = repositoryResult.Error ?? "No data returned from repository."
                };
            }

            var workoutModels = repositoryResult.Result
                .Select(x => x.MapTo<WorkoutResponseModel>())
                .ToList();

            return new WorkoutResult<IEnumerable<WorkoutResponseModel>>
            {
                Success = true,
                Result = workoutModels
            };
        }
        catch (Exception ex)
        {
            return new WorkoutResult<IEnumerable<WorkoutResponseModel>>
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<WorkoutResult<WorkoutResponseModel>> GetWorkoutByExpressionAsync(Expression<Func<WorkoutEntity, bool>> expression)
    {
        var repositoryResult = await _workoutRepository.GetAsync(expression);

        if (!repositoryResult.Success || repositoryResult.Result == null)
        {
            return new WorkoutResult<WorkoutResponseModel>
            {
                Success = false,
                Error = repositoryResult.Error ?? "No data returned from repository."
            };
        }

        var workoutModel = repositoryResult.Result.MapTo<WorkoutResponseModel>();

        return new WorkoutResult<WorkoutResponseModel>
        {
            Success = true,
            Result = workoutModel
        };


    }


    public async Task<WorkoutResult> UpdateWorkoutAsync(string id, WorkoutCreationRequest updateRequest)
    {
        try
        {
            if (updateRequest == null)
                throw new Exception("Update form can not be null.");

            var repositoryResult = await _workoutRepository.GetAsync(x => x.Id == id);
            if (!repositoryResult.Success || repositoryResult.Result == null)
            {
                return new WorkoutResult<WorkoutResponseModel>
                {
                    Success = false,
                    Error = repositoryResult.Error ?? "No data returned from repository."
                };
            }

            var entity = repositoryResult.Result;

            WorkoutFactory.UpdateWorkoutEntity(entity, updateRequest);

            var updatedResult = await _workoutRepository.UpdateAsync(entity);

            if (!updatedResult.Success)
            {
                return new WorkoutResult
                {
                    Success = false,
                };
            }

            return new WorkoutResult { Success = true };
            
        }
        catch (Exception ex)
        {
            return new WorkoutResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

}
