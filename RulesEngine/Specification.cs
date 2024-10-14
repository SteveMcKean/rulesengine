using System.Linq.Expressions;

namespace RulesEngine;

public abstract class Specification<T>
{
    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }
    
    public static Specification<T>? GetFirstSatisfiedBy(T entity, IEnumerable<Specification<T>> specifications)
    {
        return specifications.ToList()
            .FirstOrDefault(specification => specification.IsSatisfiedBy(entity));
    }
    
    public abstract Expression<Func<T, bool>> ToExpression();
}