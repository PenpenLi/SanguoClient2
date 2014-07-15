// Author : dandanshih
// Date : 2014/7/14

using Newtonsoft.Json;

public class Json
{
	public static string Serialize (object Data)
	{
		return JsonConvert.SerializeObject (Data);
	}

	public static T Deserialize<T> (string Data)
	{
		return JsonConvert.DeserializeObject<T> (Data);
	}
}