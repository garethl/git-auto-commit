namespace GitAutoCommit.Support
{
    public class Interval
    {
        public Interval(int seconds)
        {
            Seconds = seconds;
        }

        public int Seconds { get; set; }

        public override string ToString()
        {
            if (Seconds > 60*60)
                return string.Format("{0} hours", Seconds/60/60);

            if (Seconds == 60*60)
                return string.Format("1 hour");

            if (Seconds == 60)
                return string.Format("1 minute");

            if (Seconds > 60)
                return string.Format("{0} minutes", Seconds/60);

            return string.Format("{0} seconds", Seconds);
        }
    }
}