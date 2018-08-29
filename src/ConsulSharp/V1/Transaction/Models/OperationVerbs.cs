namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The Operation Verbs.
    /// </summary>
    public class OperationVerbs
    {
        /// <summary>
        /// Sets the Key to the given Value.
        /// </summary>
        public const string Set = "set";

        /// <summary>
        /// Sets, but with CAS semantics.
        /// </summary>
        public const string CheckAndSet = "cas";

        /// <summary>
        /// Lock with the given Session.
        /// </summary>
        public const string Lock = "lock";

        /// <summary>
        /// Unlock with the given Session.
        /// </summary>
        public const string Unlock = "unlock";

        /// <summary>
        /// Get the key, fails if it does not exist.
        /// </summary>
        public const string Get = "get";

        /// <summary>
        /// Gets all keys with the prefix.
        /// </summary>
        public const string GetTree = "get-tree";

        /// <summary>
        /// Fail if modify index != index.
        /// </summary>
        public const string CheckIndex = "check-index";

        /// <summary>
        /// Fail if not locked by session.
        /// </summary>
        public const string CheckSession = "check-session";

        /// <summary>
        /// Fail if key exists.
        /// </summary>
        public const string CheckNotExists = "check-not-exists";

        /// <summary>
        /// Delete the key.
        /// </summary>
        public const string Delete = "delete";

        /// <summary>
        /// Delete all keys with a prefix.
        /// </summary>
        public const string DeleteTree = "delete-tree";

        /// <summary>
        /// Delete, but with CAS semantics.
        /// </summary>
        public const string DeleteCheckAndSet = "delete-cas";
    }
}