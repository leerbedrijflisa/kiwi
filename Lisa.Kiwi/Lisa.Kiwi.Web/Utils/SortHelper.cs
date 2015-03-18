using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Lisa.Kiwi.Web.Dashboard.Utils
{
    public static class SortHelper
    {
        public static string SortBy(string field, string currentSortBy)
        {
            // No current sort by, so default
            if (currentSortBy == null)
            {
                return field + " DESC";
            }

            var sorting = currentSortBy.Split(' ');

            // Invalid amount of spaces, so default
            if (sorting.Length < 1 || sorting.Length > 2)
            {
                return field + " DESC";
            }

            // Not already this field or lacking order, so default
            if (field != sorting[0] || sorting.Length != 2)
            {
                return field + " DESC";
            }

            // Order other than DESC, so default
            if (sorting[1] != "DESC")
            {
                return field + " DESC";
            }

            // Already this field and with DESC, so flip
            return field;
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda));
            }

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda,
                    type));
            }

            return propInfo;
        }

        public static MvcHtmlString SortLinkFor<TSource, TProperty>(
            this HtmlHelper<IEnumerable<TSource>> helper,
            string linkText, string currentSortBy, Expression<Func<TSource, TProperty>> lambda)
        {
            var property = GetPropertyInfo(lambda);
            var action = helper.ViewContext.RouteData.GetRequiredString("action");

            return helper.ActionLink(linkText, action, new
            {
                sortBy = SortBy(property.Name, currentSortBy)
            });
        }
    }
}