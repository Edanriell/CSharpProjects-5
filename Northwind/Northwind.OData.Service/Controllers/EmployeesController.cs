using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;

namespace OData.Services.Controllers;

public class EmployeesController : ODataController
{
    protected readonly NorthwindContext db;

    public EmployeesController(NorthwindContext db)
    {
        this.db = db;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(db.Employees);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
        return Ok(db.Employees.Where(employee => employee.EmployeeId == key));
    }
}
