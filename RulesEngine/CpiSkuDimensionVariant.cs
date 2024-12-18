﻿using System.Data;
using RulesEngine.VariantStates;

namespace RulesEngine;

public class CpiSkuDimensionVariant
{
    public VariantStateBase CurrentState { get; private set; }
    public VariantStateManager StateManager { get; private set; }
    
    public const int MaxWeight = 22679;
    public const int MinWeight = 454;
    
    public const int MaxHeight = 406;
    public const int MinHeight = 51;
    
    public const int MaxWidth = 609;
    public const int MinWidth = 127;
    
    public const int MaxLength = 609;
    public const int MinLength = 163;
    
    public const decimal MaxAspectRatio = 1.80m;
    public const decimal ForcedAspectRatio = 2.2m;
    public const decimal MaxDiagonalRatio = 806.45m;

    public const decimal MinAspectRatio = 1.0m;
    
    public bool IsSymboticEligible { get; set; }
    public TippingState TippingState { get; set; }
    
    public int Weight { get; set; }
    
    public int Height { get; set; }
    
    public int Width { get; set; }
    
    public int Length { get; set; }
    
    public decimal AspectRatio => Length != 0 ? Math.Round((decimal)Width / Length, 2) : 0;

   public decimal DiagonalRatio => Length >= 0 && Width >= 0 ? Math.Round((decimal)Math.Sqrt(Length * Length + Width * Width),2) : 0;
    
    public List<CpiSkuDimensionVariant> ChildVariants { get; set; } = new List<CpiSkuDimensionVariant>();
    public bool IsTippable { get; set; }
    public bool IsDimensionsOutOfBounds { get; set; }
    public string DimensionOutOfBoundsReason { get; set; }
    public bool ShowTipIcon { get; set; }
    public bool ShowUnTipIcon { get; set; }
    public bool ShowUnTippableIcon { get; set; }
    public bool CanEdit { get; set; }
    public bool CanEditSymboticEligible { get; set; }
    public Guid? ParentId { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();

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