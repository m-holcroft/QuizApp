using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuizApp.Common
{
    /// <summary>
    /// A class providing methods that extend the functionality of existing classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a container of type <see cref="IEnumerable{T}"/> to a <see cref="ObservableCollection{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of object in the container.</typeparam>
        /// <param name="col">The name of the object to be converted.</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}
