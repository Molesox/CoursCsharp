using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.API
{
    #region APIEntityResponse class

    /// <summary>
    /// This class represents a standard response for an API call with an entity of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type returned by the API.</typeparam>
    public class APIEntityResponse<TEntity> where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the API call was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error messages, if any, resulting from the API call.
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the data resulting from a successful API call.
        /// </summary>
        public TEntity? Data { get; set; }

        #endregion
    }

    #endregion

}
