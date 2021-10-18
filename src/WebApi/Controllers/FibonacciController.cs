using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fibonacci;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly ILogger<FibonacciController> _logger;
    private readonly Compute _compute;

    public FibonacciController(ILogger<FibonacciController> logger,
        Compute compute)
    {
        _logger = logger;
        _compute = compute;
    }

    [HttpGet (Name = "/{number}")]
    public async Task<List<long>> Get(string number)
    {
        return await _compute.ExecuteAsync(new []{number});
    }
}
