using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web
{
    public static class RawSignedModelExtensions
    {
        public static string ExportAsString(this RawSignedModel model)
        {
            var rawCardBytes = Bytes.FromString(model.ExportAsJson());
            var rawCardString = Bytes.ToString(rawCardBytes, StringEncoding.BASE64);
            return rawCardString;
        }

        /// <summary>
        /// Exports a RawSignedModel into string. Use this method to transmit the card 
        /// signing request through the network.
        /// </summary>
        public static string ExportAsJson(this RawSignedModel model)
        {
            return Configuration.Serializer.Serialize(model);
        }
    }
}
