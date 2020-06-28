using System.Text.Json.Serialization;

namespace AnyTask.API.Data.Entities
{
    public class Task : BaseEntity
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public bool Concluded { get; set; }

        public Task() { }

        public Task(string description, int userId)
        {
            Description = description;
            Concluded = false;
            UserId = userId;
        }

        public void Update(string description)
        {
            Description = description;
        }

        public void Conclude()
        {
            Concluded = true;
        }
    }
}
