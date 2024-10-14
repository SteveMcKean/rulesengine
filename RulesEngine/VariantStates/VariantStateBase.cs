namespace RulesEngine.VariantStates;

public abstract class VariantStateBase
{
    protected bool CanEdit { get; set; }
    protected bool IsSymboticEligible { get; set; }
    protected TippingRecommendation TippingRecommendation { get; set; }
    protected TippingState TippingState { get; set; }
    protected bool ShowTipping { get; set; }
    protected bool ShowUnTip { get; set; }
    protected bool ShowExcluded { get; set; }
    
    protected List<CpiSkuDimensionVariant> ChildVariants { get; set; } = new List<CpiSkuDimensionVariant>();
    
    public abstract void SetState(CpiSkuDimensionVariant variant);
}