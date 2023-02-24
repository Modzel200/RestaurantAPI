using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers;
[Route("api/restaurant")]
[ApiController]
[Authorize]
public class RestaurantController:ControllerBase
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }
    [HttpPut("{id}")]
    public ActionResult Update([FromBody] UpdateRestaurantDto dto,[FromRoute]int id)
    {
        _restaurantService.Update(id,dto,User);
        return Ok();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _restaurantService.Delete(id,User);
        return NoContent();
    }
    [HttpPost]
    [Authorize(Roles = "Admin,Menager")]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var id = _restaurantService.Create(dto,userId);
        return Created($"/api/restaurant/{id}", null);
        
    }
    [HttpGet]
    [Authorize(Policy = "HasNationality")]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurantsDtos = _restaurantService.GetAll();
        return Ok(restaurantsDtos);
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<RestaurantDto>> Get([FromRoute] int id)
    {
        var restaurant = _restaurantService.GetById(id);
        return Ok(restaurant);
    }
}