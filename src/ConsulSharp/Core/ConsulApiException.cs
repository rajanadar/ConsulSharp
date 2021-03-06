﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ConsulSharp.Core
{
    /// <summary>
    /// The consul client exception
    /// </summary>
    public class ConsulApiException : Exception
    {
        /// <summary>
        /// The status code returned by Api.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// The http status code returned by Api.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// The list of api errors.
        /// </summary>
        public IEnumerable<string> ApiErrors { get; }

        /// <summary>
        /// The list of api warnings.
        /// </summary>
        public IEnumerable<string> ApiWarnings { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConsulApiException()
        {
        }

        /// <summary>
        /// Message constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ConsulApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Message constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception</param>
        public ConsulApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Status code based exception.
        /// </summary>
        /// <param name="httpStatusCode">Http status code.</param>
        /// <param name="message">Exception message.</param>
        public ConsulApiException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            StatusCode = (int) HttpStatusCode;

            try
            {
                var structured = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(message);

                if (structured.ContainsKey("errors"))
                {
                    ApiErrors = structured["errors"];
                }

                if (structured.ContainsKey("warnings"))
                {
                    ApiWarnings = structured["warnings"];
                }
            }
            catch
            {
                // nothing to do.
            }
        }
    }
}
