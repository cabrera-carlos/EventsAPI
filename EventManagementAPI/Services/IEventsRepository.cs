using System;
namespace EventManagementAPI.Services
{
    public interface IEventsRepository
    {
        public List<Event> GetAll();

        public Event? GetById(int id);

        public Event Add(Event newEvent);

        public Event Update(Event newEvent);

        public bool Delete(int id);
    }
}

