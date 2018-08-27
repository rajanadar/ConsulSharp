using System.Collections.Generic;

namespace ConsulSharp.V1.KeyValue.Models
{
    /// <summary>
    /// Model to create/update keys.
    /// </summary>
    public class WriteKeyValueModel
    {
        /// <summary>
        /// Specifies the path of the key to write.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Specifies the datacenter to query. 
        /// This will default to the datacenter of the agent being queried. 
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>
        /// Specifies an unsigned value between 0 and (2^64)-1. 
        /// Clients can choose to use this however makes sense for their application.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// Specifies to use a Check-And-Set operation. 
        /// This is very useful as a building block for more complex synchronization primitives. 
        /// If the index is 0, Consul will only put the key if it does not already exist. 
        /// If the index is non-zero, the key is only set if the index matches the ModifyIndex of that key.
        /// </summary>
        public int CheckAndSet { get; set; }

        /// <summary>
        /// Specifies to use a lock acquisition operation. 
        /// This is useful as it allows leader election to be built on top of Consul. 
        /// If the lock is not held and the session is valid, this increments the LockIndex and sets the Session value 
        /// of the key in addition to updating the key contents. 
        /// A key does not need to exist to be acquired. 
        /// If the lock is already held by the given session, then the LockIndex is not incremented but the key contents are updated. 
        /// This lets the current lock holder update the key contents without having to give up the lock and reacquire it. 
        /// Note that an update that does not include the acquire parameter will proceed normally even if another session has locked the key.
        /// For an example of how to use the lock feature, see the Leader Election Guide.
        /// https://www.consul.io/docs/guides/leader-election.html
        /// </summary>
        public string AcquireSessionId { get; set; }

        /// <summary>
        /// Specifies to use a lock release operation. 
        /// This is useful when paired with <see cref="AcquireSessionId"/> as it allows clients to yield a lock. 
        /// This will leave the LockIndex unmodified but will clear the associated Session of the key. 
        /// The key must be held by this session to be unlocked.
        /// </summary>
        public string ReleaseSessionId { get; set; }

        /// <summary>
        /// Value to write.
        /// </summary>
        public string Value { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(DataCenter))
            {
                list.Add("dc=" + DataCenter);
            }

            if (Flags > 0)
            {
                list.Add("flags=" + Flags);
            }

            if (CheckAndSet > 0)
            {
                list.Add("cas=" + CheckAndSet);
            }

            if (!string.IsNullOrWhiteSpace(AcquireSessionId))
            {
                list.Add("acquire=" + AcquireSessionId);
            }

            if (!string.IsNullOrWhiteSpace(ReleaseSessionId))
            {
                list.Add("release=" + ReleaseSessionId);
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}