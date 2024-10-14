// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.CompilerServices;
using RulesEngine;
using RulesEngine.VariantStates;

Console.ForegroundColor = ConsoleColor.Red;
var variant = new CpiSkuDimensionVariant
    {
        Length = GetDoubleFromUser("Enter Length: "),
        Width = GetDoubleFromUser("Enter Width: "),
        Height = GetDoubleFromUser("Enter Height: "),
        Weight = GetDoubleFromUser("Enter Weight: ")
    };

Console.ForegroundColor = ConsoleColor.Green;
var heightSpecification = new ValidHeightSpecification();
var heightResult = heightSpecification.IsSatisfiedBy(variant);
Console.WriteLine($"Height specification satisfied: {heightResult}");

var widthSpecification = new ValidWidthSpecification();
var widthResult = widthSpecification.IsSatisfiedBy(variant);
Console.WriteLine($"Width specification satisfied: {widthResult}");

var lengthSpecification = new ValidLengthSpecification();
var lengthResult = lengthSpecification.IsSatisfiedBy(variant);
Console.WriteLine($"Length specification satisfied: {lengthResult}");

var weightSpecification = new ValidWeightSpecification();
var weightResult = weightSpecification.IsSatisfiedBy(variant);
Console.WriteLine($"Weight specification satisfied: {weightResult}");

var aspectRatioSpecification = new ValidAspectRatioSpecification();
var aspectRatioResult = aspectRatioSpecification.IsSatisfiedBy(variant);
Console.WriteLine($"Aspect Ratio specification satisfied: {aspectRatioResult}");

var diagonalRatioSpecification = new ValidDiagonalRatioSpecification();
var diagonalRatioResult = diagonalRatioSpecification.IsSatisfiedBy(variant);
Console.WriteLine($"Diagonal Ratio specification satisfied: {diagonalRatioResult}");

Console.WriteLine($"Variant dimensions: {variant.AspectRatio} Aspect Ratio, {variant.DiagonalRatio} Diagonal Ratio");

// Reset color
Console.ResetColor();

// Retrieve and apply the first satisfied tipping specification
var specificationType = typeof(Specification<CpiSkuDimensionVariant>);
var specifications = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.BaseType == specificationType)
    .Select(t => Activator.CreateInstance(t) as Specification<CpiSkuDimensionVariant>)
    .ToArray();

var tippingSpecification = Specification<CpiSkuDimensionVariant>.GetFirstSatisfiedBy(variant, specifications);

if (tippingSpecification != null)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Tipping Specification satisfied: {tippingSpecification.Name}");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("No tipping specification satisfied.");
}

// Reset color
Console.ResetColor();
// Reset color and wait for user input
Console.ResetColor();

static int GetDoubleFromUser(string prompt)
{
    int value;
    do
    {
        Console.Write(prompt);
    } while (!int.TryParse(Console.ReadLine(), out value));

    return value;
}

Console.ReadLine();
    
    
    