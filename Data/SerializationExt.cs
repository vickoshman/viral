﻿using Newtonsoft.Json;

namespace Data
{
  public static class SerializationExt
  {
    public static string ToJson<T>(this T obj)
    {
      return JsonConvert.SerializeObject(obj);
    }

    public static T FromJson<T>(this string json)
    {
      return JsonConvert.DeserializeObject<T>(json);
    }
  }
}