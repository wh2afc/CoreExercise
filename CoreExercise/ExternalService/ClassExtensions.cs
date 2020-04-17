using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Returns string representation of a HttpResponseMessage.
        /// </summary>
        /// <param name="httpResponse">Response object to format.</param>
        /// <returns>The string, formatted into curly braces.</returns>
        public static string AsFormattedString(this HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
                throw new ArgumentNullException(nameof(httpResponse));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(httpResponse.ToString());
            if (httpResponse.Content != null)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Body:");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine(httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult());
                stringBuilder.AppendLine("}");
            }
            return stringBuilder.ToString();
        }
    }
}
