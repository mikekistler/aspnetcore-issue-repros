using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

[ApiController]
[Route("/pets")]
public class PetsController : ControllerBase
{
    // This doesn't work - the response body object does not contain the $type property
    [HttpGet]
    public ActionResult<BaseModel> Get()
    {
        Cat cat = new Cat() { CatName = "Mizzy" };
        return Ok(cat);
    }

    // This works - the response body object contains the $type property
    [HttpGet("cat")]
    public ActionResult<BaseModel> GetCat()
    {
        Cat cat = new Cat() { CatName = "Mizzy" };
        return cat;
    }

    // This doesn't work - the response body object does not contain the $type property
    [HttpGet("dog")]
    public ActionResult<BaseModel> GetDog()
    {
        Dog dog = new Dog() { DogName = "Fido" };
        var result = Ok(dog);
        result.DeclaredType = typeof(Dog);
        return result;
    }

    // This doesn't work - the response body object does not contain the $type property
    [HttpGet("pooch")]
    public ActionResult<Dog> GetPooch()
    {
        Dog dog = new Dog() { DogName = "Fido" };
        var result = new ActionResult<Dog>(dog);
        return result;
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
