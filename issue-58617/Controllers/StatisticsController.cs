using Microsoft.AspNetCore.Mvc;

namespace Issue_58617.Controllers;

[ApiController]
public class StatisticsController : ControllerBase
{

    // TestDto corresponds to component schema "TestDto2" with nullable: true

    [HttpGet("Test")]
    public TestDto Test()
    {
        return default!;
    }

    [HttpGet("Test2")]
    public TestDto? Test2()
    {
        return default;
    }

    // TestDto corresponds to component schema "TestDto" (with nullable: false)

    [HttpGet("Test3")]
    public KeyValuePair<int, TestDto> Test3()
    {
        return default!;
    }
}

public class TestDto
{
    public string x { get; set; } = default!;
}