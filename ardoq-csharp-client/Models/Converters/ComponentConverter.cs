using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Ardoq.Models.Converters
{
    public class ComponentConverter : CustomCreationConverter<Component>
    {
        public override bool CanWrite
        {
            get { return true; }
        }

        public override Component Create(Type objectType)
        {
            return new Component {Fields = new Dictionary<string, object>(), Children = new List<string>()};
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            // Serializing json fields that are supported by the Component class type
            var component = (Component) base.ReadJson(jsonObject.CreateReader(), objectType, existingValue, serializer);

            // filling non supported fields into Component.Fields property
            IEnumerable<string> availableObjectFields = GetAvailableComponentFields();
            IEnumerable<string> availableJsonFields = jsonObject.Properties().Select(p => p.Name);
            IEnumerable<string> missingFields = availableJsonFields.Except(availableObjectFields);
            IEnumerable<JProperty> missingProperties =
                jsonObject.Properties().Where(p => missingFields.Any(f => f == p.Name));

            foreach (JProperty missingField in missingProperties)
            {
                component.Fields.Add(missingField.Name, missingField.Value.ToObject<Object>());
            }

            return component;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var component = (value as Component);

            // All this logic is because using the serializer would cause an infinite loop because 
            // this converter will be used again and the WriteJson property will be called again and again.
            // The only possile way to deal with this situation is manual conversion.

            writer.WriteStartObject();

            // Serialize Basic Properties
            List<PropertyInfo> availableProperties =
                typeof (Component).GetRuntimeProperties()
                    .Where(p => p.CustomAttributes.Any(ca => ca.AttributeType == typeof (JsonPropertyAttribute)))
                    .ToList();

            foreach (PropertyInfo availableProperty in availableProperties)
            {
                object propertyValue = availableProperty.GetValue(component);
                if (propertyValue != null)
                {
                    string attributeJsonPropertyName =
                        availableProperty.GetCustomAttribute<JsonPropertyAttribute>().PropertyName;
                    writer.WritePropertyName(attributeJsonPropertyName);
                    serializer.Serialize(writer, propertyValue);
                }
            }

            if (component != null)
            {
                Dictionary<string, object> fields = component.Fields;
                // Serialize missing fields missing fields.
                if (fields != null && fields.Any())
                {
                    IEnumerable<string> availableObjectFields = GetAvailableComponentFields();
                    IEnumerable<string> missingFieldsKeys = fields.Keys.Except(availableObjectFields);
                    foreach (string missingFieldKey in missingFieldsKeys)
                    {
                        writer.WritePropertyName(missingFieldKey);
                        serializer.Serialize(writer, fields[missingFieldKey]);
                    }
                }
            }
            writer.WriteEndObject();
        }

        public static IEnumerable<string> GetAvailableComponentFields()
        {
            // returns the json fields' names for each available property
            List<PropertyInfo> availableProperties =
                typeof (Component).GetRuntimeProperties()
                    .Where(p => p.CustomAttributes.Any(ca => ca.AttributeType == typeof (JsonPropertyAttribute)))
                    .ToList();
            return (from availableProperty in availableProperties
                select
                    availableProperty.CustomAttributes.FirstOrDefault(
                        ca => ca.AttributeType == typeof (JsonPropertyAttribute))
                into propertyAttribute
                let arguments = propertyAttribute.NamedArguments
                where arguments != null
                select arguments.FirstOrDefault(na => na.MemberName == "PropertyName")
                into property
                select property.TypedValue.Value.ToString()).ToList();
        }
    }
}