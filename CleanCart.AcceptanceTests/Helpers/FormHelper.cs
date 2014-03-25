using System;
using System.Linq.Expressions;

namespace CleanCart.AcceptanceTests.Helpers
{
    class FormHelper<TForm>
    {
        public string FieldName<TField>(Expression<Func<TForm, TField>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("You must use a simple expression like 'x => x.Field' to be able to retrieve the field name");
            }

            return memberExpression.Member.Name;
        }
    }
}
