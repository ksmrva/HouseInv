using HouseInv.Models.Entities.Notes;
using MongoDB.Bson;
using MongoDB.Driver;
using Task = System.Threading.Tasks.Task;

namespace HouseInv.Repositories
{
    public class MongoDbNotesRepository : IAsyncNotesRepository
    {
        private const string DatabaseName = "houseInventoryDB";
        private const string CollectionName = "notes";
        private readonly IMongoCollection<Note> notesCollection;
        private readonly FilterDefinitionBuilder<Note> noteFilterBuilder = Builders<Note>.Filter;

        public MongoDbNotesRepository(IMongoClient mongoClient) 
        {
            this.notesCollection = mongoClient.GetDatabase(DatabaseName)
                                                  .GetCollection<Note>(CollectionName);
        }
        public async Task CreateNoteAsync(Note note)
        {
            await notesCollection.InsertOneAsync(note);
        }

        public async Task<Note> GetNoteAsync(Guid noteId)
        {
            var noteIdFilter = noteFilterBuilder.Eq(note => note.Id, noteId);
            return await notesCollection.Find(noteIdFilter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesAsync()
        {
            return await notesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateNoteAsync(Note updatedNote)
        {
            var noteIdFilter = noteFilterBuilder.Eq(existingNote => existingNote.Id, updatedNote.Id);
            await notesCollection.ReplaceOneAsync(noteIdFilter, updatedNote);
        }

        public async Task DeleteNoteAsync(Guid noteId)
        {
            var noteIdFilter = noteFilterBuilder.Eq(note => note.Id, noteId);
            await notesCollection.DeleteOneAsync(noteIdFilter);
        }
    }
}