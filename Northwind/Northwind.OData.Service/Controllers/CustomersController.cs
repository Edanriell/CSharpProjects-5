using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;

namespace OData.Services.Controllers;

public class CustomersController : ODataController
{
    protected readonly NorthwindContext db;

    public CustomersController(NorthwindContext db)
    {
        this.db = db;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(db.Customers);
    }

    [EnableQuery]
    public IActionResult Get(string key)
    {
        return Ok(db.Customers.Where(customer => customer.CustomerId == key));
    }
}
