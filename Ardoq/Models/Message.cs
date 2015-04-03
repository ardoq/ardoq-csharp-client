using System;
using System.Collections.Generic;
using Ardoq.Models.Converters;
using Newtonsoft.Json;

namespace Ardoq
{
	public class Message
	{
		#region Properties

		[JsonProperty (PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]
		public string Subject { get; set; }

		[JsonProperty (PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
		public String Body { get; set; }

		#endregion

		#region ctor

		public Message (String subject, String body)
		{
			Subject = subject;
			Body = body;
		}

		#endregion

	}
}

