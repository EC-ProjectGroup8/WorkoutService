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

    [HttpGet("batch")]
    public async Task<IActionResult> GetWorkoutsByIds([FromQuery] string? ids)
    {

        var idList = string.IsNullOrWhiteSpace(ids) ? [] : ids.Split(',')
          .Select(s => s.Trim())
          .Where(s => s.Length > 0)
          .ToList();

        if (idList.Count == 0)
              return BadRequest("ids is required");

        var result = await _workoutService.GetManyWorkoutsByExpressionAsync(x => idList.Contains(x.Id));

        if (!result.Success)
            return StatusCode(500, result.Error ?? "Failed to fetch workouts.");

        return Ok(result.Result ?? []);
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