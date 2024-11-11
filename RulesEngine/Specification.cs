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
    
    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }
    
    public Specification<T> Or(Specification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }
    
    public Specification<T> Not()
    {
        return new NotExpression<T>(this);
    }
    
    public abstract Expression<Func<T, bool>> ToExpression();
    
    public virtual string GetFailureMessage(T entity)
    {
        return $"{Name} was not satisfied.";
    }
    
    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;
        
        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        
        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = left.ToExpression();
            var rightExpression = right.ToExpression();
            
            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
        
        public override string GetFailureMessage(T entity)
        {
            return $"{left.GetFailureMessage(entity)} and {right.GetFailureMessage(entity)}";
        }
    }
    
    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;
        
        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        
        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = left.ToExpression();
            var rightExpression = right.ToExpression();
            
            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
        
        public override string GetFailureMessage(T entity)
        {
            return $"{left.GetFailureMessage(entity)} or {right.GetFailureMessage(entity)}";
        }
    }
    
    internal sealed class NotExpression<T> : Specification<T>
    {
        private readonly Specification<T> specification;
        
        public NotExpression(Specification<T> specification)
        {
            this.specification = specification;
        }
        
        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);
            
            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
        
        public override string GetFailureMessage(T entity)
        {
            return $"Not {specification.GetFailureMessage(entity)}";
        }
    }
}