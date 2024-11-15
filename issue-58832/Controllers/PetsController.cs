using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

[ApiController]
[Route("/pets")]
public class PetsController : ControllerBase
{
    // This works - the response body object contains the $type property
    [HttpGet("mizzy")]
    public ActionResult<BaseModel> GetMizzy()
    {
        return new Cat() { CatName = "Mizzy" };
    }

    // This doesn't work - the response body object does not contain the $type property
    [HttpGet("mizzy2")]
    public ActionResult<BaseModel> GetMizzy2()
    {
        return Ok(new Cat() { CatName = "Mizzy" });
    }

    // Also doesn't work
    [HttpGet("mizzy3")]
    public ActionResult<BaseModel> GetMizzy3()
    {
        return Ok(new Cat() { CatName = "Mizzy" } as BaseModel);
    }

    // This works!
    [HttpGet("fido")]
    public ActionResult<BaseModel> GetFido()
    {
        var result = Ok(new Dog() { DogName = "Fido" });
        result.DeclaredType = typeof(BaseModel);
        return result;
    }

    // This alsom works!
    [HttpGet("lassie")]
    public Ok<BaseModel> GetLassie()
    {
        BaseModel dog = new Dog() { DogName = "Lassie" };
        return TypedResults.Ok(dog);
    }

    // This alsom works!
    [HttpGet("snoopy")]
    public Ok<BaseModel> GetSnoopy()
    {
        return TypedResults.Ok(new Dog() { DogName = "Snoopy" } as BaseModel);
    }
}

[JsonPolymorphic]
[JsonDerivedType(typeof(Cat), "cat")]
[JsonDerivedType(typeof(Dog), "dog")]
public abstract class BaseModel
{
    public string Name { get; set; }
}

public class Cat : BaseModel
{
    public string CatName { get; set; }
}

public class Dog : BaseModel
{
    public string DogName { get; set; }
}
