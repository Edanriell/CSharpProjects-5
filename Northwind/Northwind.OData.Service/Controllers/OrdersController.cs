using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;

namespace OData.Services.Controllers;

public class OrdersController : ODataController
{
    protected readonly NorthwindContext db;

    public OrdersController(NorthwindContext db)
    {
        this.db = db;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(db.Orders);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
        return Ok(db.Orders.Where(order => order.OrderId == key));
    }
}
