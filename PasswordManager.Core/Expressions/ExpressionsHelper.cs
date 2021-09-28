using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PasswordManager.Core {
    /// <summary>
    /// A helper for expressions
    /// </summary>
    public static class ExpressionsHelper {

        /// <summary>
        /// Compiles an expression and gets it property value
        /// </summary>
        /// <typeparam name="T">Type of the return value</typeparam>
        /// <param name="lambda">expression to compile</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda) {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        /// Sets the underlying property value to the given value from an expression that contains the property
        /// </summary>
        /// <typeparam name="T">Type of the value to set</typeparam>
        /// <param name="lambda">Expression</param>
        /// <param name="value">Value to set the property to</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value) {
            
            // converts a lambda () => some.Property; to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            
            propertyInfo.SetValue(target, value);
        }
    }
}
