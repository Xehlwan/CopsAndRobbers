using System.Collections.Generic;

namespace CopsAndRobbers.Game
{
    public class PlayField
    {
        private readonly List<Person> people = new List<Person>();
        private readonly List<Prisoner> prisoners = new List<Prisoner>();

        public int Width { get; }
        public int Height { get; }
        
        public int Robberies { get; set; }
        public int Arrests { get; set; }
        public int PeopleInPrison => prisoners.Count;

        public int NextRelease
        {
            get
            {
                if (prisoners.Count == 0) return 0;
                int value = int.MaxValue;
                foreach (var prisoner in prisoners)
                {
                    var release = prisoner.Sentence - prisoner.ServedTime;
                    value = release < value ? release : value;
                }

                return value;
            }
        }

        public PlayField(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void PutInPrison(Person person, int time)
        {
            prisoners.Add(new Prisoner(person, time));
            people.Remove(person);
        }

        public void ReleasePrisoners()
        {
            var release = new List<Prisoner>();
            foreach (var prisoner in prisoners)
            {
                if (prisoner.ToBeReleased)
                    release.Add(prisoner);

                prisoner.Tick();
            }

            foreach (var prisoner in release)
            {
                people.Add(prisoner.Person);
                prisoners.Remove(prisoner);
            }
        }

        public void MovePeople()
        {
            foreach (Person person in people)
            {
                person.Move();
                if (person.Position.X >= Width) {person.Position = (0, person.Position.Y);}
                if (person.Position.X < 0) {person.Position = (Width - 1, person.Position.Y);}
                if (person.Position.Y >= Height) {person.Position = (person.Position.X, 0);}
                if (person.Position.Y < 0) {person.Position = (person.Position.X, Height - 1);}
            }
        }

        public void AddPerson(Person person)
        {
            person.RandomizePosition(Width, Height);
            while (people.Exists(x => x.Position == person.Position))
            {
                person.RandomizePosition(Width, Height);
            }
            people.Add(person);
        }

        public void RemovePerson(Person person) => people.Remove(person);

        public List<HashSet<Person>> GetCollisions()
        {
            people.Sort((a, b) => a.Position.CompareTo(b.Position));

            var collisions = new List<HashSet<Person>>();
            var collision = new HashSet<Person>();
            for (int i = 1; i < people.Count; i++)
            {
                if (people[i].Position == people[i - 1].Position)
                {
                    collision.Add(people[i]);
                    collision.Add(people[i - 1]);
                }
                else if (collision.Count > 0)
                {
                    collisions.Add(collision);
                    collision = new HashSet<Person>();
                }
            }
            if(collision.Count > 0) collisions.Add(collision);

            return collisions;
        }

        public IEnumerable<Person> GetPeople() => people;
    }
}
