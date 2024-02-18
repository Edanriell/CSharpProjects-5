using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;

namespace OData.Services.Controllers;

public class SuppliersController : ODataController
{
    protected readonly NorthwindContext db;

    public SuppliersController(NorthwindContext db)
    {
        this.db = db;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(db.Suppliers);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
        return Ok(db.Suppliers.Where(supplier => supplier.SupplierId == key));
    }
}
