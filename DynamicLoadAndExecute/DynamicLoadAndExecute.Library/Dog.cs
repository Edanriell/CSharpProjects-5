using static System.Console;

namespace DynamicLoadAndExecute.Library;

public class Dog
{
    public void Speak(string? name)
    {
        WriteLine($"{name} says Woof!");
    }
}
