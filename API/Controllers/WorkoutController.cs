using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.Logging;
using Domain.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutController(IWorkoutService workoutService) : ControllerBase
{

    private readonly IWorkoutService _workoutService = workoutService;

    [HttpGet]
    public async Task<IActionResult> GetAllWorkouts()
    {
        var workouts = await _workoutService.GetAllWorkoutsAsync();
        return workouts.Success
            ? Ok(workouts.Result)
            : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkoutById(string id)
    {
        var workout = await _workoutService.GetWorkoutByExpressionAsync(x => x.Id == id);
        return workout.Success
            ? Ok(workout.Result)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout([FromBody] WorkoutCreationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var createdWorkout = await _workoutService.CreateWorkoutAsync(request);

        return createdWorkout.Success
            ? Ok(createdWorkout.Success)
            : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(string id, [FromBody] WorkoutCreationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updateResult = await _workoutService.UpdateWorkoutAsync(id, request);

        return updateResult.Success
          ? Ok(updateResult.Success)
          : BadRequest();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(string id)
    {
        var deleteResult = await _workoutService.DeleteEventAsync(id);

        return deleteResult.Success
            ? Ok(deleteResult.Success)
            : BadRequest();
    }
}