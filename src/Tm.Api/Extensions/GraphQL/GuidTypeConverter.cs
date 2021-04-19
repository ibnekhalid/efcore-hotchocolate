using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Types;
using System;
using System.Buffers.Text;
using System.Globalization;

namespace Tm.Api.Extensions.GraphQL
{
    public sealed class GuidTypeConverter
     : ScalarType<Guid, StringValueNode>
    {
        private readonly string _format;
        private readonly string _alternateFormat = "D";

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidType"/> class.
        /// </summary>
        public GuidTypeConverter()
            : this('\0')
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidType"/> class.
        /// </summary>
        public GuidTypeConverter(char format)
            : this(ScalarNames.Uuid, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidType"/> class.
        /// </summary>
        public GuidTypeConverter(NameString name, char format = '\0')
            : this(name, null, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidType"/> class.
        /// </summary>
        public GuidTypeConverter(NameString name, string description, char format = '\0')
            : base(name, BindingBehavior.Implicit)
        {
            Description = description;
            _format = CreateFormatString(format);
        }

        protected override bool IsInstanceOfType(StringValueNode valueSyntax)
        {
            if (Utf8Parser.TryParse(
                valueSyntax.AsSpan(), out Guid _, out int _, _format[0]))
            {
                return true;
            }

            return Utf8Parser.TryParse(
                valueSyntax.AsSpan(), out Guid _, out int _, _alternateFormat[0]);
        }

        protected override Guid ParseLiteral(StringValueNode valueSyntax)
        {
            if (Utf8Parser.TryParse(
                valueSyntax.AsSpan(), out Guid formatGuid, out int _, _format[0]))
            {
                return formatGuid;
            }

            if (Utf8Parser.TryParse(
                valueSyntax.AsSpan(), out Guid altFormatGuid, out int _, _alternateFormat[0]))
            {
                return altFormatGuid;
            }

            throw new SerializationException(
                ScalarCannotParseLiteral(Name, valueSyntax.GetType()),
                this);
        }

        protected override StringValueNode ParseValue(Guid runtimeValue)
        {
            return new StringValueNode(runtimeValue
                .ToString(_alternateFormat, CultureInfo.InvariantCulture));
        }

        public override IValueNode ParseResult(object resultValue)
        {
            if (resultValue is null)
            {
                return NullValueNode.Default;
            }

            if (resultValue is string s)
            {
                return new StringValueNode(s);
            }

            if (resultValue is Guid g)
            {
                return ParseValue(g);
            }

            throw new SerializationException(
                ScalarCannotParseLiteral(Name, resultValue.GetType()),
                this);
        }

        public override bool TrySerialize(object runtimeValue, out object resultValue)
        {
            if (runtimeValue is null)
            {
                resultValue = null;
                return true;
            }

            if (runtimeValue is Guid uri)
            {
                resultValue = uri.ToString(_alternateFormat, CultureInfo.InvariantCulture);
                return true;
            }

            resultValue = null;
            return false;
        }

        public override bool TryDeserialize(object resultValue, out object runtimeValue)
        {
            if (resultValue is null)
            {
                runtimeValue = null;
                return true;
            }

            if (resultValue is string s && Guid.TryParse(s, out Guid guid))
            {
                runtimeValue = guid;
                return true;
            }

            if (resultValue is Guid)
            {
                runtimeValue = resultValue;
                return true;
            }

            runtimeValue = null;
            return false;
        }

        private static string CreateFormatString(char format)
        {
            if (format != '\0'
                && format != 'N'
                && format != 'D'
                && format != 'B'
                && format != 'P')
            {
                throw new ArgumentException(
                    "Unknown format. Guid supports the following format chars: " +
                    $"{{ `N`, `D`, `B`, `P` }}.{Environment.NewLine}" +
                    "https://docs.microsoft.com/en-us/dotnet/api/" +
                    "system.buffers.text.utf8parser.tryparse?" +
                    "view=netcore-3.1#System_Buffers_Text_Utf8Parser_" +
                    "TryParse_System_ReadOnlySpan_System_Byte__System_Guid__" +
                    "System_Int32__System_Char_",
                    nameof(format));
            }

            return format == '\0' ? "N" : format.ToString(CultureInfo.InvariantCulture);
        }

        private static string ScalarCannotParseLiteral(
            string typeName, Type literalType)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException(
                    "The typeName mustn't be null or empty.",
                    nameof(typeName));
            }

            if (literalType is null)
            {
                throw new ArgumentNullException(nameof(literalType));
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0} cannot parse the given literal of type `{1}",
                typeName,
                literalType.Name);
        }
    }
}
