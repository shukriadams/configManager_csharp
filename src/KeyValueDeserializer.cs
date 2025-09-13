using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace ConfigManager;

public class KeyValueDeserializer : INodeDeserializer
{
    public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
    {
        if (expectedType.IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        {
            Type[] pairArgs = expectedType.GetGenericArguments();
            object key = null;
            object val = null;

            parser.Consume<MappingStart>();

            while (parser.Allow<MappingEnd>() == null)
            {
                Scalar keyName = parser.Consume<Scalar>();
                key = keyName.Value;
                val = nestedObjectDeserializer(parser, pairArgs[1]);
            }

            value = Activator.CreateInstance(expectedType, key, val);
            return true;
        }

        value = null;
        return false;
    }
}
