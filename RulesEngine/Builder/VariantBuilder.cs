using RulesEngine.VariantStates;

namespace RulesEngine.Builder;

public class VariantBuilder
{
    private readonly CpiSkuDimensionVariant variant;

    // Dictionary mapping TippingState to corresponding configuration actions
    private readonly Dictionary<TippingState, Action> tippingStateConfigurations;

    // Private constructor to restrict direct instantiation
    private VariantBuilder(CpiSkuDimensionVariant existingVariant)
    {
        variant = existingVariant;

        // Initialize the dictionary with actions for each TippingState
        tippingStateConfigurations = new Dictionary<TippingState, Action>
            {
                { TippingState.Tipped, () => AsTipped() },
                { TippingState.UnTippable, () => AsUnTippable() },
                { TippingState.Forced, () => AsForced() },
                { TippingState.Overridden, () => AsAllowed() },
                { TippingState.Undefined, () => AsUndefined() }
            };
    }

    // Static factory method to create a new builder
    public static VariantBuilder Create(CpiSkuDimensionVariant existingVariant)
    {
        return new VariantBuilder(existingVariant);
    }

    // Apply the configuration based on the provided TippingState
    public VariantBuilder WithTippingState(TippingState state, bool applyToChildren = false)
    {
        if (tippingStateConfigurations.TryGetValue(state, out var configure))
        {
            configure();
            variant.TippingState = state;

            // Optionally apply the same state to all children
            if (applyToChildren && variant.ChildVariants.Any())
            {
                foreach (var child in variant.ChildVariants) 
                    new VariantBuilder(child).WithTippingState(child.TippingState);
            }
        }
        else
        {
            throw new InvalidOperationException($"TippingState '{state}' is not recognized.");
        }

        return this;
    }
    
    public VariantBuilder AsUndefined()
    {
        return ApplyConfiguration(v =>
            {
                v.IsTippable = false;
                v.IsSymboticEligible = true;
                v.CanEditSymboticEligible = true;
                v.ShowTipIcon = false;
                v.ShowUnTipIcon = false;
                v.ShowUnTippableIcon = false;
                v.CanEdit = true;

            });
    }
   
    public VariantBuilder AsUnTippable()
    {
        return ApplyConfiguration(v =>
        {
            v.IsTippable = false;
            v.IsSymboticEligible = false;
            v.ShowTipIcon = false;
            v.ShowUnTipIcon = false;
            v.ShowUnTippableIcon = true;
            v.CanEditSymboticEligible = false;
            v.CanEdit = false;
            
        });
    }

    public VariantBuilder AsTippable()
    {
        return ApplyConfiguration(v =>
        {
            v.IsTippable = true;
            v.IsSymboticEligible = true;
            v.CanEditSymboticEligible = true;
            v.ShowTipIcon = true;
            v.ShowUnTipIcon = false;
            v.ShowUnTippableIcon = false;
            v.CanEdit = false;
            
        });
    }

    public VariantBuilder AsTipped()
    {
        return ApplyConfiguration(v =>
        {
            v.IsTippable = false;
            v.IsSymboticEligible = false;
            v.CanEditSymboticEligible = false;
            v.ShowTipIcon = false;
            v.ShowUnTipIcon = true;
            v.ShowUnTippableIcon = false;
            v.CanEdit = false;
            
            // Additional logic for tipped state can be added here
        });
    }

    public VariantBuilder AsForced()
    {
        return ApplyConfiguration(v =>
        {
            v.IsTippable = false;
            v.IsSymboticEligible = false;
            v.CanEditSymboticEligible = false;
            v.ShowTipIcon = false;
            v.ShowUnTipIcon = false;    
            v.ShowUnTippableIcon = true;
            v.CanEdit = false;
            
        });
    }

    public VariantBuilder AsAllowed()
    {
        return ApplyConfiguration(v =>
        {
            v.IsTippable = true;
            v.IsSymboticEligible = true;
            v.CanEditSymboticEligible = true;
            v.ShowTipIcon = true;
            v.ShowUnTipIcon = false;
            v.ShowUnTippableIcon = false;
            v.CanEdit = true;
            
        });
    }

    public CpiSkuDimensionVariant Build()
    {
        ValidateState();
        return variant;
    }
   
    // Method to apply any custom configuration
    public VariantBuilder ApplyConfiguration(Action<CpiSkuDimensionVariant> configuration)
    {
        configuration(variant);
        return this;
    }
    
    private void ValidateState()
    {
        
    }
}



