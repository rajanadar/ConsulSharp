namespace ConsulSharp.V1
{
    /// <summary>
    /// The V1 interface for the Consul Api.
    /// </summary>
    public interface IConsulClientV1
    {
        /// <summary>
        /// The ACL interface.
        /// </summary>
        IACL ACL { get; }
    }
}
