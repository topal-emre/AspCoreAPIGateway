using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulpar.Core.Const
{
    #region SearchType

    public enum SearchType : byte
    {
        /// <summary>
        /// Used for equality assignment.
        /// If no search type is assigned, it assigns default value as equal
        /// </summary>
        Equal = 0,
        /// <summary>
        /// Only used in textual fields.
        /// Searches within all value.
        /// </summary>
        Contains = 1,
        /// <summary>
        /// Only used in numeric fields.
        /// Ensures that the value is greater.
        /// </summary>
        GreaterThan = 2,
        /// <summary>
        /// Used in numeric and date fields.
        /// Ensures that the value is greater than or equal to.
        /// </summary>
        GreaterThanOrEqual = 3,
        /// <summary>
        /// Only used in numeric fields.
        /// Ensures that the value is less.
        /// </summary>
        LessThan = 4,
        /// <summary>
        /// Used in numeric and date fields.
        /// Ensures that the value is less than or equal to.
        /// </summary>
        LessThanOrEqual = 5
    }

    #endregion
}
