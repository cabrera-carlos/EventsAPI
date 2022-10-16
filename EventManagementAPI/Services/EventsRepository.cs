using System;
namespace EventManagementAPI.Services
{
    public record Event(int Id, DateTime Date, string Location, string Description);

    public class EventsRepository : IEventsRepository
    {
        private List<Event> Events { get; }

        public EventsRepository()
        {
            Events= new() {
                new Event(1, DateTime.Today, " NY", "Baby Shower"),
                new Event(2, DateTime.Today, "LA", "Birthday"),
                new Event(3, DateTime.Today, "Chicago", "Soccer Game"),
            };
        }

        public List<Event> GetAll() => Events;

        public Event? GetById(int id) => Events.FirstOrDefault(e => e.Id == id);

        public Event Add(Event newEvent)
        {
            Events.Add(newEvent);
            return newEvent;
        }

        public Event Update(Event newEvent)
        {
            int index = Events.FindIndex(0, 1, e => e.Id == newEvent.Id);
            Events[index] = newEvent;
            return newEvent;
        }

        public bool Delete(int id)
        {
            var target = GetById(id);

            if (id < 0 || target == null)
                throw new ArgumentException("No event exists with the given id", nameof(id));

            return Events.Remove(target);

        }


    }
}

