using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace TodoAppV2
{
    public class CustomIdSerializer : SerializerBase<string>
    {
        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();
            if (bsonType == BsonType.ObjectId)
            {
                return context.Reader.ReadObjectId().ToString();
            }
            else if (bsonType == BsonType.String)
            {
                return context.Reader.ReadString();
            }
            else
            {
                throw new FormatException($"Cannot deserialize string from BsonType {bsonType}");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
        {
            context.Writer.WriteString(value);
        }
    }

    public class Student
    {
        [BsonId]
        [BsonSerializer(typeof(CustomIdSerializer))]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public string Grade { get; set; } = null!;

        public override string ToString()
        {
            return $"[{Id}] {Name} | {Email} | {Address} | {Age} | {Grade}";
        }
    }
}
