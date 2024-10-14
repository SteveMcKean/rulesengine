namespace RulesEngine.VariantStates;

public class VariantStateManager
{
    private CpiSkuDimensionVariant variant;

    public VariantStateBase CurrentState { get; private set; }

    public VariantStateManager(CpiSkuDimensionVariant variant)
    {
        this.variant = variant;
        CurrentState = new ValidInAllRegardsVariantState(); // Default starting state
    }

    public void TransitionTo(VariantStateBase newState)
    {
        CurrentState = newState;
        CurrentState.SetState(variant);
    }
   
    public bool CanTransitionTo<T>() where T : VariantStateBase
    {
        // Logic to determine if transition is allowed.
        // Example: Prevent transition to TippedState if conditions are not met.
        if (typeof(T) == typeof(TippedVariantState) && !variant.IsTippable)
        {
            return false;
        }
        return true;
    }
}
