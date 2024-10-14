using System.Linq.Expressions;

namespace RulesEngine;

public abstract class Specification<T>
{
    public string Name => GetType().Name;
    
    public SpecificationResult IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        var isSatisfied = predicate(entity);

        return isSatisfied ? new SpecificationResult(true) : 
            new SpecificationResult(false, GetFailureMessage(entity));
    }
    
    public static Specification<T>? GetFirstSatisfiedBy(T entity, IEnumerable<Specification<T>> specifications)
    {
        return specifications.ToList()
            .FirstOrDefault(specification => specification.IsSatisfiedBy(entity).IsSatisfied);
    }
    
    public abstract Expression<Func<T, bool>> ToExpression();
    
    public virtual string GetFailureMessage(T entity)
    {
        return $"{Name} was not satisfied.";
    }
}