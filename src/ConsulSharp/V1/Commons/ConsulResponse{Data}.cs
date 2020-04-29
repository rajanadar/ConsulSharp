using System;

namespace ConsulSharp.V1.Commons
{
    /// <summary>
    /// Represents a Consul response.
    /// </summary>
    /// <typeparam name="TResponseData">The type of the data contained in the response.</typeparam>
    public class ConsulResponse<TResponseData> : ConsulResponse
    {
        /// <summary>
        /// Gets or sets the response data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TResponseData Data { get; set; }

        internal ConsulResponse<T2> Map<T2>(Func<T2> func)
        {
            return new ConsulResponse<T2>
            {
                Index = this.Index,
                KnownLeader = this.KnownLeader,
                LastContactMilliseconds = this.LastContactMilliseconds,
                Data = func != null ? func() : default(T2),
                CacheAction = CacheAction,
                CacheHitAgeSeconds = CacheHitAgeSeconds,
                ContentHash = ContentHash
            };
        }
    }
}