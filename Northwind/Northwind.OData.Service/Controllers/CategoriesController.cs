using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;

namespace OData.Service.Controllers;

public class CategoriesController : ODataController
{
    protected readonly NorthwindContext db;

    public CategoriesController(NorthwindContext db)
    {
        this.db = db;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(db.Categories);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
        return Ok(db.Categories.Where(category => category.CategoryId == key));
    }
}
