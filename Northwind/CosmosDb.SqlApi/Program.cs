using System.Globalization;
using System.Text;
using static System.Console;

OutputEncoding = Encoding.UTF8; // To enable Euro symbol output.

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-IE");

//await CreateCosmosResources();

//await CreateProductItems();

//await ListProductItems();

await ListProductItems(
    "SELECT p.id, p.productName, p.unitPrice FROM Items p WHERE p.category.categoryName = 'Beverages'"
);

//await DeleteProductItems();

//await CreateInsertProductStoredProcedure();

//await ExecuteInsertProductStoredProcedure();

//await ListProductItems("SELECT p.id, p.productName, p.unitPrice FROM Items p WHERE p.productId = '78'");
