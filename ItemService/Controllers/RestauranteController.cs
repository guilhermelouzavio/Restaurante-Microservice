using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Services;

namespace ItemService.Controllers;

[Route("api/item/[controller]")]
[ApiController]
public class RestauranteController : ControllerBase
{
   
    private readonly IMapper _mapper;
    private readonly IRestauranteService _restauranteService;

    public RestauranteController(IMapper mapper, IRestauranteService restauranteService)
    {
     
        _mapper = mapper;
        _restauranteService = restauranteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestauranteReadDto>>> GetRestaurantes()
    {
        var restaurantes = await _restauranteService.GetAllRestaurante();

        return Ok(_mapper.Map<IEnumerable<RestauranteReadDto>>(restaurantes));
    }

    [HttpPost]
    public ActionResult RecebeRestauranteDoRestauranteService(RestauranteReadDto dto)
    {
        Console.WriteLine(dto.Id);
        return Ok(dto);
    }

    //[HttpPost]
    //public IActionResult TestInboundConnection()
    //{
    //    return Ok("Conexão ok!");
    //}
}
