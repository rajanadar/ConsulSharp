using System;
using ConsulSharp.V1.Transaction.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsulSharp.V1.Transaction
{
    /// <summary>
    /// Converts the <see cref="ITransactionResult" /> object from JSON.
    /// </summary>
    internal class TransactionResultJsonConverter : JsonConverter
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite => false;

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="NotImplementedException">Unnecessary because CanWrite is false. The type will skip the converter.</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jtoken = JToken.Load(reader);

            if (jtoken != null && jtoken.HasValues)
            {
                var key = jtoken.First.Path;

                if (key.ToLowerInvariant().Equals("kv"))
                {
                    var kv = new KeyValueTransactionResult { KeyValueModel = new KeyValue.Models.KeyValueModel() };
                    serializer.Populate(jtoken.First.First.CreateReader(), kv.KeyValueModel);

                    return kv;
                }
                else
                {
                    var gen = new GenericTransactionResult();
                    serializer.Populate(jtoken.CreateReader(), gen);

                    return gen;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ITransactionResult);
        }

    }
}