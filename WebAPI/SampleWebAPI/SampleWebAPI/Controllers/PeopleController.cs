using System;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Models;
using SampleWebAPI.Services;


namespace SampleWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController: ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _personService.GetAllAysnc();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var people = await _personService.GetByIdAsync(id);
        if (people == null) return NotFound();
        return Ok(people);
    }


    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Person person)
    {
        var People = await _personService.AddAsync(person);
        return Ok(People);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _personService.DeleteAsync(id);
        return NoContent();
    }

}

