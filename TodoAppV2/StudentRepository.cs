using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _studentsCollection = database.GetCollection<Student>("Students");
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Student?> GetAsync(string id)
        {
            return await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _studentsCollection.InsertOneAsync(student);
        }

        public async Task UpdateAsync(string id, Student studentIn)
        {
            studentIn.Id = id; // Ensure ID is not changed or properly set
            await _studentsCollection.ReplaceOneAsync(x => x.Id == id, studentIn);
        }

        public async Task DeleteAsync(string id)
        {
            await _studentsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Student>> SearchAsync(string query)
        {
            var filters = new List<FilterDefinition<Student>>();
            
            filters.Add(Builders<Student>.Filter.Regex(x => x.Id, new MongoDB.Bson.BsonRegularExpression(query, "i")));
            filters.Add(Builders<Student>.Filter.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(query, "i")));
            filters.Add(Builders<Student>.Filter.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(query, "i")));
            filters.Add(Builders<Student>.Filter.Regex(x => x.Grade, new MongoDB.Bson.BsonRegularExpression(query, "i")));

            var filter = Builders<Student>.Filter.Or(filters);

            return await _studentsCollection.Find(filter).ToListAsync();
        }
    }
}
