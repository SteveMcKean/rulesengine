namespace RulesEngine;

public class VariantStateInfo
{
    public TipIconStateInfo TipIconStateInfo { get; set; }
    public SymboticEligible SymboticEligible { get; set; }
    public bool CanEdit { get; private set; }

    public static VariantStateInfo Create(TipIconStateInfo tipIconStateInfo, SymboticEligible symboticEligible, bool canEdit) =>
        new VariantStateInfo(tipIconStateInfo, symboticEligible, canEdit);
    
    private VariantStateInfo(TipIconStateInfo tipIconStateInfo, SymboticEligible symboticEligible, bool canEdit) =>
        (TipIconStateInfo, SymboticEligible, CanEdit) = (tipIconStateInfo, symboticEligible, canEdit);
}

public class TipIconStateInfo
{
    public bool ShowTipIcon { get; private set; }
    public bool ShowTipStatus { get; private set; }
    public bool ShowUnTipIcon { get; private set; }
    public bool ShowUnTippableIcon { get; private set; }
    
    public static TipIconStateInfo CreateTipped() =>
        new TipIconStateInfo(showTipIcon:false, true, false, false);
    
    public static TipIconStateInfo CreateAllowed() =>  
        new TipIconStateInfo(showTipIcon: true, true, true, false);
    
    public static TipIconStateInfo CreateNotAllowed() => 
        new TipIconStateInfo(showTipIcon: true, true, false, true);
    
    public static TipIconStateInfo CreateOverridden() => 
        new TipIconStateInfo(showTipIcon: true, true, false, false);
    
    public static TipIconStateInfo CreateUnTippable() => 
        new TipIconStateInfo(showTipIcon: false, true, false, true);
    
    public static TipIconStateInfo CreateForced() => 
        new TipIconStateInfo(showTipIcon: false, true, false, false);
    
    public static TipIconStateInfo CreateChildForced() => 
        new TipIconStateInfo(showTipIcon: false, false, false, false);
    
    public static TipIconStateInfo CreateChildTipped() => 
        new TipIconStateInfo(showTipIcon: false, false, true, false);
    
    private TipIconStateInfo(bool showTipIcon, bool showTipStatus, bool showUnTipIcon, bool showUnTippableIcon) =>
        (ShowTipIcon, ShowTipStatus, ShowUnTipIcon, ShowUnTippableIcon) = (showTipIcon, showTipStatus, showUnTipIcon, showUnTippableIcon);
}

public class SymboticEligible
{
    public bool IsSymboticEligible { get; private set; }
    
    public bool CanEditSymboticEligible { get; private set; }
}