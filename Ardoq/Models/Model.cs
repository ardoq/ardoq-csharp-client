using System;
using System.Collections.Generic;
using Ardoq.Models.Converters;
using Newtonsoft.Json;

namespace Ardoq.Models
{
	[JsonConverter (typeof(ModelConverter))]
	public class Model : IModel
	{
		[JsonProperty (PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? CreatedBy { get; set; }

		[JsonProperty (PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public String Name { get; set; }

		[JsonProperty (PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public String Description { get; set; }

		// TODO CHANGE THE DECLARATIONS OF THE MODEL CLASS TO REFLECT THE NEW MAP

		[JsonIgnore]
		public Dictionary<String, string> ComponentTypes { get; set; }

		[JsonIgnore]
		public Dictionary<String, int> ReferenceTypes { get; set; }

		#region ctor

		public Model ()
		{
			ReferenceTypes = new Dictionary<string, int> ();
			ComponentTypes = new Dictionary<string, string> ();
		}

		public Model (String id, String name, String description, Dictionary<String, string> componentTypes,
		                   Dictionary<String, int> referenceTypes)
		{
			Id = id;
			Name = name;
			Description = description;
			ComponentTypes = componentTypes;
			ReferenceTypes = referenceTypes;
		}

		//    private Dictionary<String, String> getComponentTypes(Dictionary<String, Object> document)
		//    {
		//        HashMap<String, String> componentTypes = new HashMap<String, String>();
		//        for (Map.Entry<String, Object> entries : document.entrySet()) {
		//            Map<String, Object> value = (Map<String, Object>) entries.getValue();
		//            componentTypes.put((String) value.get("name"), entries.getKey());
		//            componentTypes.putAll(getComponentTypes((Map<String, Object>) value.get("children")));
		//        }
		//        return componentTypes;
		//    }

		//    private Map<String, Integer> getReferenceTypes(JsonObject jsonObject) {
		//    Map<String, Integer> references = new HashMap<String, Integer>();
		//    JsonElement referenceTypes = jsonObject.get("referenceTypes");
		//    if (referenceTypes instanceof JsonObject) {
		//        JsonObject document = (JsonObject) referenceTypes;
		//        if (document != null) {
		//            Set<Map.Entry<String, JsonElement>> entries = document.entrySet();
		//            for (Map.Entry<String, JsonElement> entry : entries) {
		//                JsonObject value = (JsonObject) entry.getValue();
		//                references.put(value.get("name").getAsString(), value.get("id").getAsInt());
		//            }
		//        }
		//    }
		//    return references;
		//}

		#endregion

		[JsonProperty (PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }


		[JsonProperty (PropertyName = "_version", NullValueHandling = NullValueHandling.Ignore)]
		public int? VersionCounter { get; set; }

		[JsonProperty (PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty (PropertyName = "last-updated", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? LastUpdated { get; set; }

        [JsonProperty(PropertyName = "useAsTemplate", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? UseAsTemplate { get; }

        public int GetReferenceTypeByName (string name)
		{
			return ReferenceTypes [name];
		}

		public String GetComponentTypeByName (String name)
		{
			return ComponentTypes [name];
		}
	}
}