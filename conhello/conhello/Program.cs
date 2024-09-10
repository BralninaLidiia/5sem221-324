// See https://aka.ms/new-console-template for more information
Console.WriteLine("имя?");
string? name = Console.ReadLine();
Console.WriteLine("город?");
string? city = Console.ReadLine();

int? age = null;

Console.WriteLine("Имя = "+name +", город = "+ city);
Console.WriteLine("Имя = {0}, город = {1}", name, city);
Console.WriteLine($"Имя = {name}, Город = {city}");

Console.WriteLine("город?");
