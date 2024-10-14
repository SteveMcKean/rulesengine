using RulesEngine.VariantStates;

namespace RulesEngine;

public class CpiSkuDimensionVariant
{
    public VariantStateBase CurrentState { get; private set; }
    public VariantStateManager StateManager { get; private set; }
    
    public const int MaxWeight = 23679;
    
    public const int MaxHeight = 500;
    public const int MinHeight = 127;
    
    public const int MaxWidth = 500;
    public const int MinWidth = 127;
    
    public const int MaxLength = 500;
    public const int MinLength = 127;
    
    public const decimal MaxAspectRatio = 1.80m;
    public const decimal ForcedAspectRatio = 2.2m;
    public const decimal MaxDiagonalRatio = 3.70m;

    public const decimal MinAspectRatio = 1.0m;
    
    public bool IsSymboticEligible { get; set; }
    
    public int Weight { get; set; }
    
    public int Height { get; set; }
    
    public int Width { get; set; }
    
    public int Length { get; set; }
    
    public decimal AspectRatio { get; set; }

    public decimal DiagonalRatio { get; set; }
    
    public List<CpiSkuDimensionVariant> ChildVariants { get; set; } = new List<CpiSkuDimensionVariant>();
    public bool IsTippable { get; set; }

    public CpiSkuDimensionVariant()
    {
        StateManager = new VariantStateManager(this);
        IsTippable = true;
        
        CurrentState = new ValidInAllRegardsVariantState();
    }
    
    public void TransitionToState<T>() where T : VariantStateBase, new()
    {
        if (StateManager.CanTransitionTo<T>())
            StateManager.TransitionTo(new T());
        else
            throw new InvalidOperationException("Cannot transition to the requested state.");
    }
    
}