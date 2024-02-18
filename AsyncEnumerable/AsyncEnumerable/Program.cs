using static System.Console;

await foreach (int number in GetNumbersAsync())
{
    WriteLine($"Number: {number}");
}
