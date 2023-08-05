using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PasswordManager.Core.Helpers.Expressions
{
    public static class ExpressionsHelper
    {

        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {

            // converts a lambda () => some.Property; to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;


            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();


            propertyInfo.SetValue(target, value);
        }
    }
}
