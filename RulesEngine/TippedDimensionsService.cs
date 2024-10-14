using Microsoft.Extensions.Logging;

namespace RulesEngine;

public class TippedDimensionsService
{
    private readonly ILogger<TippedDimensionsService> logger;
    private readonly DimensionTolerances dimensionTolerances;

    public TippedDimensionsService(ILogger<TippedDimensionsService> logger)
    {
        this.logger = logger;
        dimensionTolerances = new DimensionTolerances();
    }

    public List<CpiSkuDimensionVariant> GetValidTippedDimVars(CpiSkuDimensionVariant inputDimVar)
    {
        logger.LogInformation($"Entering GetValidTippedDimVars()");

        var validTippedDimVars = new List<CpiSkuDimensionVariant>();
        var dimVarsForDuplicateCheck = new List<CpiSkuDimensionVariant>();

        var mW = new Measurement(inputDimVar.Length, inputDimVar.Height, inputDimVar.Width, inputDimVar.Weight, true);
        var mL = new Measurement(inputDimVar.Height, inputDimVar.Width, inputDimVar.Length, inputDimVar.Weight, true);

        // Create variants for tipping both ways
        var dimVarW = new CpiSkuDimensionVariant()
            {
                Length = (int)mW.Length,
                Width = (int)mW.Width,
                Height = (int)mW.Height,
                Weight = (int)mW.Weight
            };

        var dimVarL = new CpiSkuDimensionVariant()
            {
                Length = (int)mL.Length,
                Width = (int)mL.Width,
                Height = (int)mL.Height,
                Weight = (int)mL.Weight
            };

        if (IsTippedVariantValid(dimVarW, dimVarsForDuplicateCheck, out CpiSkuDimensionVariant dupDimVarW,
                out string cannotTipReasonW))
        {
            validTippedDimVars.Add(dimVarW);

            // If swapping width is valid, then check against it for duplicates for validity of other tipped variant
            dimVarsForDuplicateCheck.Add(dimVarW);
        }
        else
        {
            
        }
            /// logger.Debug("SKU {0} cannot be tipped on width due to: {1}.", SkuViewModel?.Sku.SkuId, cannotTipReasonW);

            // Check if length-and-height-swapped variant is valid
        if (IsTippedVariantValid(dimVarL, dimVarsForDuplicateCheck, out CpiSkuDimensionVariant dupDimVarL,
                out string cannotTipReasonL))
            validTippedDimVars.Add(dimVarL);
        else
        {
            
        }
            //logger.Debug("SKU {0} cannot be tipped on length due to: {1}.", SkuViewModel?.Sku.SkuId, cannotTipReasonL);

        if (validTippedDimVars.Count == 0)
        {

        }
        // HandleNoValidTippedVariants(inputDimVar, cannotTipReasonW, cannotTipReasonL);

        return validTippedDimVars;
    }

    private bool IsTippedVariantValid(CpiSkuDimensionVariant dimVar, List<CpiSkuDimensionVariant> dimVarsForDuplicateCheck,
        out CpiSkuDimensionVariant duplicateDimVar, out string cannotTipReason)
    {
        //logger.Debug($"Entering {MethodBase.GetCurrentMethod()}");
        cannotTipReason = string.Empty;

        // check existing dim vars to see if tipped variant is a duplicate, must ALWAYS be done
        duplicateDimVar = FoundExistingDimensionalVariant(dimVarsForDuplicateCheck,
            dimVar.Length,
            dimVar.Width,
            dimVar.Height,
            dimVar.Weight, dimensionTolerances.DimensionVariantLengthTolerance,
            dimensionTolerances.DimensionVariantWidthTolerance,
            dimensionTolerances.DimensionVariantHeightTolerance,
            dimensionTolerances.DimensionVariantWeightTolerance, dimVar, false);

        // found a duplicate - do not accept the possibly-tipped one
        if (duplicateDimVar != null)
        {
            // logger.Debug($"Duplicate dimensional variant was found, so this tipped variant will be discarded: " +
            //              $"L={dimVar.Length} W={dimVar.Width} H={dimVar.Height} Wt={dimVar.Weight}");

            cannotTipReason = "Dimension already exists"; //resourceProvider.DimensionVariantAlreadyExistsMessage;
            return false;
        }

        // If the outputDimVar is not out of bounds and height is smallest dimension
        // then yes, tipping will make it eligible.
        if (!dimVar.IsDimensionsOutOfBounds && TippedDimensionsAreAcceptable(dimVar))
            return true;

        // No can do - make a nice reason string and get out.
        if (dimVar.IsDimensionsOutOfBounds)
            cannotTipReason = dimVar.DimensionOutOfBoundsReason;
        else if (!TippedDimensionsAreAcceptable(dimVar))
            cannotTipReason = string.Format("resourceProvider.CannotTipSkuReason", dimVar.Height);

        return false;
    }

    private CpiSkuDimensionVariant? FoundExistingDimensionalVariant(IEnumerable<CpiSkuDimensionVariant> dimVars,
        int length, int width, int height, int weight, int lengthTolerance, int widthTolerance,
        int heightTolerance, int weightTolerance, CpiSkuDimensionVariant selectedVariant,
        bool ignoreSelectedVariant = false)
    {
        string logSelected;
        if (selectedVariant == null)
            logSelected = string.Format("    FoundExistingDimensionalVariant() No dimensional variant selected");
        else
        {
            logSelected = string.Format(
                "    FoundExistingDimensionalVariant() Selected dimensional variant is L{0} W{1} H{2} ({3})",
                selectedVariant.Length, selectedVariant.Width, selectedVariant.Height, selectedVariant.Weight);
        }

        foreach (var dimVar in dimVars)
        {
            var logCheck = $"    Checking for duplicates of L{dimVar.Length} W{dimVar.Width} H{dimVar.Height} ({dimVar.Weight})";
            //logger.Debug(logCheck);

            if (ignoreSelectedVariant && dimVar == selectedVariant)
                continue;

            if ((Math.Abs(length - dimVar.Length)) <= lengthTolerance &&
                (Math.Abs(width - dimVar.Width)) <= widthTolerance &&
                (Math.Abs(height - dimVar.Height)) <= heightTolerance &&
                (Math.Abs(weight - dimVar.Weight)) <= weightTolerance)
            {
                var logFoundExisting = string.Format(
                    "    * FoundExistingDimensionalVariant() : L{0} W{1} H{2} ({3}) is a duplicate of L{4} W{5} H{6} ({7}). Tolerances are L{8} W{9} H{10} ({11})",
                    length, width, height, weight, dimVar.Length, dimVar.Width, dimVar.Height, dimVar.Weight,
                    lengthTolerance, widthTolerance, heightTolerance, weightTolerance);

                //logger.Debug(logFoundExisting);
                return dimVar;

            }
        }

        return null;
    }

    private bool TippedDimensionsAreAcceptable(CpiSkuDimensionVariant tippedDimVar)
    {
        //logger.Debug($"Entering {MethodBase.GetCurrentMethod()}");

        // check if height is greater than length and width, because that would mean there was a tipping failure
        if (tippedDimVar.Height > tippedDimVar.Length && tippedDimVar.Height > tippedDimVar.Width)
            return false;

        return true;
    }

}