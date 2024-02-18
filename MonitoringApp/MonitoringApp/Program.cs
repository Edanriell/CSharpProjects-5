using static System.Console;
using Shared;

/*
WriteLine("Processing. Please wait...");
Recorder.Start();

int[] largeArrayOfInts = Enumerable.Range(
  start: 1, count: 10_000).ToArray();

Thread.Sleep(new Random().Next(5, 10) * 1000);
Recorder.Stop();
*/

int[] numbers = Enumerable.Range(start: 1, count: 50_000).ToArray();

SectionTitle("Using StringBuilder");
Recorder.Start();

System.Text.StringBuilder builder = new();

for (int i = 0; i < numbers.Length; i++)
{
    builder.Append(numbers[i]);
    builder.Append(", ");
}

Recorder.Stop();
WriteLine();

SectionTitle("Using string with +");
Recorder.Start();

string s = string.Empty;

for (int i = 0; i < numbers.Length; i++)
{
    s += numbers[i] + ", ";
}

Recorder.Stop();
