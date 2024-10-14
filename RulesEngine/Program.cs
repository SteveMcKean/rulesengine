// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.CompilerServices;
using RulesEngine;
using RulesEngine.VariantStates;

var variant = new CpiSkuDimensionVariant
    {
        Length = 609,
        Width = 609,
        Height = 406,
        Weight = 6000
    };

var heightSpecification = new ValidHeightSpecification();
var widthSpecification = new ValidWidthSpecification();
var lengthSpecification = new ValidLengthSpecification();
var weightSpecification = new ValidWeightSpecification();

var heightWidthSpecification = new AndSpecification<CpiSkuDimensionVariant>(heightSpecification, widthSpecification);
var lengthWeightSpecification = new AndSpecification<CpiSkuDimensionVariant>(lengthSpecification, weightSpecification);

var dimensionSpecification = new AndSpecification<CpiSkuDimensionVariant>(heightWidthSpecification, lengthWeightSpecification);
var aspectRatioSpecification = new ValidAspectRatioSpecification();
var diagonalRatioSpecification = new ValidDiagonalRatioSpecification();

var aspectsSpecification = new AndSpecification<CpiSkuDimensionVariant>(dimensionSpecification, aspectRatioSpecification);

dimensionSpecification = new AndSpecification<CpiSkuDimensionVariant>(aspectsSpecification, diagonalRatioSpecification);
var result = dimensionSpecification.IsSatisfiedBy(variant);

Console.WriteLine(result);

var specificationType = typeof(Specification<CpiSkuDimensionVariant>);

var specifications = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.BaseType == specificationType)
    .Select(t => Activator.CreateInstance(t) as Specification<CpiSkuDimensionVariant>)
    .ToArray();

var tippingSpecification = Specification<CpiSkuDimensionVariant>.GetFirstSatisfiedBy(variant, specifications);

if(tippingSpecification.GetType().Name == "NotAllowedTippingSpecification")
{
    Console.WriteLine("Not allowed");
}
else if(tippingSpecification.GetType().Name == "ForcedTippingSpecification")
{
    Console.WriteLine("Forced");
}
else if(tippingSpecification.GetType().Name == "AllowedNotRecommendedTippingSpecification")
{
    Console.WriteLine("Allowed not recommended");
}
else if(tippingSpecification.GetType().Name == "RecommendedTippingSpecification")
{
    Console.WriteLine("Recommended");
}

variant = new CpiSkuDimensionVariant
    {
        Length = 500,
        Width = 400,
        Height = 500,
        Weight = 6000
    };

tippingSpecification = Specification<CpiSkuDimensionVariant>.GetFirstSatisfiedBy(variant, specifications);

if(tippingSpecification.GetType().Name == "NotAllowedTippingSpecification")
{
    Console.WriteLine("Not allowed");
}
else if(tippingSpecification.GetType().Name == "ForcedTippingSpecification")
{
    Console.WriteLine("Forced");
}
else if(tippingSpecification.GetType().Name == "AllowedNotRecommendedTippingSpecification")
{
    Console.WriteLine("Allowed not recommended");
}
else if(tippingSpecification.GetType().Name == "RecommendedTippingSpecification")
{
    Console.WriteLine("Recommended");
}

if(variant.StateManager.CanTransitionTo<AllowedNotRecommendedTippingVariantState>())
{
    variant.StateManager.TransitionTo(new AllowedNotRecommendedTippingVariantState());
}

Console.ReadLine();
    
    
    