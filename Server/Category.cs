using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

 namespace Server
{
    public class Category
    {
        public static List<Category> Db = new List<Category>();
        [JsonPropertyName("cid")]
        public int cid { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }

    }
}
