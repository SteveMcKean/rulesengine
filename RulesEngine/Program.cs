// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngine;
using RulesEngine.VariantStates;
using Serilog;

var services = new ServiceCollection();
ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Tipping tester application started");

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

logger.LogInformation($"Height specification satisfied: {heightResult.IsSatisfied} {heightResult.Message}");

var widthSpecification = new ValidWidthSpecification();
var widthResult = widthSpecification.IsSatisfiedBy(variant);

logger.LogInformation($"Width specification satisfied: {widthResult.IsSatisfied} {widthResult.Message}");

var lengthSpecification = new ValidLengthSpecification();
var lengthResult = lengthSpecification.IsSatisfiedBy(variant);

logger.LogInformation($"Length specification satisfied: {lengthResult.IsSatisfied}  {lengthResult.Message}");

var weightSpecification = new ValidWeightSpecification();
var weightResult = weightSpecification.IsSatisfiedBy(variant);
logger.LogInformation($"Weight specification satisfied: { weightResult.IsSatisfied} {weightResult.Message}");

var aspectRatioSpecification = new ValidAspectRatioSpecification();
var aspectRatioResult = aspectRatioSpecification.IsSatisfiedBy(variant);

logger.LogInformation($"Aspect Ratio specification satisfied: {aspectRatioResult.IsSatisfied} {aspectRatioResult.Message}");

var diagonalRatioSpecification = new ValidDiagonalRatioSpecification();
var diagonalRatioResult = diagonalRatioSpecification.IsSatisfiedBy(variant);

logger.LogInformation($"Diagonal Ratio specification satisfied: {diagonalRatioResult.IsSatisfied} {diagonalRatioResult}");
logger.LogInformation($"Variant dimensions: {variant.AspectRatio} Aspect Ratio, {variant.DiagonalRatio} Diagonal Ratio");

// Reset color
Console.ResetColor();

// Retrieve and apply the first satisfied tipping specification
var specificationType = typeof(Specification<CpiSkuDimensionVariant>);
var specifications = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.BaseType == specificationType && t.Name.Contains("TippingSpecification"))
    .Select(t => Activator.CreateInstance(t) as Specification<CpiSkuDimensionVariant>)
    .ToArray();

var tippingSpecification = Specification<CpiSkuDimensionVariant>.GetFirstSatisfiedBy(variant, specifications);
logger.LogInformation($"Tipping specification satisfied: {tippingSpecification?.Name ?? "None"}");

var dimensionsService = serviceProvider.GetRequiredService<TippedDimensionsService>();

var childVariants = dimensionsService.GetValidTippedDimVars(variant);
if (!childVariants.Any())
{
    logger.LogInformation("Cannot tip variant.");
}
else
{
    foreach (var childVariant in childVariants)
    {
        logger.LogInformation($"Child variant available");        
    }
}

if (tippingSpecification != null)
{
    Console.ForegroundColor = ConsoleColor.Green;
    logger.LogInformation($"Tipping Specification satisfied: {tippingSpecification.Name}");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    logger.LogInformation("No tipping specification satisfied.");
}


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

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIORNMENT") 
                                    ?? "Production"}.json", optional:true)
        .AddEnvironmentVariables();
}

static void ConfigureServices(IServiceCollection services)
{
    var builder = new ConfigurationBuilder();
    BuildConfig(builder);

    var configuration = builder.Build();

    // Configure Serilog
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)  // This reads everything, including Seq and Console sinks
        .Enrich.FromLogContext()
        .CreateLogger();

    // Add Serilog to the LoggerFactory
    services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();  // Clear any pre-existing logging providers
            loggingBuilder.AddSerilog(logger, true); // Register Serilog (without global Log.Logger)
        });

    // Register TippedDimensionsService for DI
    services.AddSingleton<TippedDimensionsService>();
}

Console.ReadLine();
    
    
    