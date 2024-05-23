namespace BLL.Hepler
{
    public static class CompareChangesHelper
    {
        /// <summary>
        /// Get all properties name of 2 object with the same type which are have different value.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<string> GetDiffPropertiesFrom<TEntity>(TEntity from, TEntity to, params string[]? exceptedProps) where TEntity : class
        {
            var listDiffProps = new List<string>();
            var properties = from.GetType().GetProperties();
            foreach (var property in properties)
            {
                object? valueFrom = property.GetValue(from);
                object? valueTo = property.GetValue(to);
                if (!CompareObject(valueFrom, valueTo) && exceptedProps?.Contains(property.Name) == false)
                {
                    listDiffProps.Add(property.Name);
                }
            }

            return listDiffProps;
        }

        /// <summary>
        /// True is two objects are the same.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        private static bool CompareObject(object? obj1, object? obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return true;
            }
            else if ((obj1 == null && obj2 != null) || (obj1 != null && obj2 == null))
            {
                return false;
            }
            else
            {
                return obj1!.ToString() == obj2!.ToString() && obj1!.GetHashCode() == obj2!.GetHashCode();
            }
        }
    }
}
