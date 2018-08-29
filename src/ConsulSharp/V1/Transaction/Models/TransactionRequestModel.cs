using System.Collections.Generic;

namespace ConsulSharp.V1.Transaction.Models
{
    /// <summary>
    /// The Transaction Request Model.
    /// </summary>
    public class TransactionRequestModel
    {
        /// <summary>
        /// Specifies the datacenter. This will default to the datacenter of the agent being queried.
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>
        /// The Transaction Operations.
        /// </summary>
        public List<ITransactionOperation> Operations { get; set; }

        internal string ToQueryString()
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(DataCenter))
            {
                list.Add("dc=" + DataCenter);
            }

            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }

            return string.Empty;
        }
    }
}