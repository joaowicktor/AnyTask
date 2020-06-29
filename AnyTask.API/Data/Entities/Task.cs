using System.Text.Json.Serialization;

namespace AnyTask.API.Data.Entities
{
    public class Task : BaseEntity
    {
        public string Description { get; private set; }
        public int UserId { get; private set; }
        public bool Concluded { get; private set; }

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
