namespace RulesEngine;

public class SpecificationResult
{
    public bool IsSatisfied { get; }
    public string Message { get; }

    public SpecificationResult(bool isSatisfied, string message = "")
    {
        IsSatisfied = isSatisfied;
        Message = message;
    }
}