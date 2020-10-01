namespace CopsAndRobbers.Game
{
    public class Prisoner
    {
        public Person Person { get; }
        public int Sentence { get; }
        public int ServedTime { get; private set; }
        public bool ToBeReleased { get; private set; }

        public Prisoner(Person person, int sentence)
        {
            Person = person;
            Sentence = sentence;
        }

        public void Tick()
        {
            ServedTime++;
            if (ServedTime >= Sentence) ToBeReleased = true;
        }
    }
}