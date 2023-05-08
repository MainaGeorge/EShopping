using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses
{
    public class TypesResponse
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
