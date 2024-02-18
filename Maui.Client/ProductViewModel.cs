using CommunityToolkit.Mvvm.ComponentModel;

namespace Northwind.Maui.Client;

internal partial class ProductViewModel : ObservableObject
{
    [ObservableProperty]
    private int productId;

    [ObservableProperty]
    private string productName;

    [ObservableProperty]
    private int supplierId;

    [ObservableProperty]
    private int categoryId;

    [ObservableProperty]
    private string quantityPerUnit;

    [ObservableProperty]
    private decimal unitPrice;

    [ObservableProperty]
    private int unitsInStock;

    [ObservableProperty]
    private int unitsOnOrder;

    [ObservableProperty]
    private int reorderLevel;

    [ObservableProperty]
    private bool discontinued;

    public string Stock
    {
        get =>
            $"Stock: {UnitsInStock} in stock, {UnitsOnOrder} on order, reorder at {ReorderLevel}.";
    }
}
