using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

[ApiController]
[Route("/pets")]
public class ExampleController : ControllerBase
{
    [HttpGet(Name = "GetPet")]
    public ActionResult<BaseModel> Get()
    {
        return Ok(new Cat() { CatName = "Mizzy" } as BaseModel);
    }
}

[JsonPolymorphic]
[JsonDerivedType(typeof(Cat), "cat")]
[JsonDerivedType(typeof(Dog), "dog")]
public class BaseModel
{
    public string Name { get; set; }
}

public class Cat : BaseModel
{
    public string CatName { get; set; }
}

class Dog : BaseModel
{
    public string DogName { get; set; }
}
