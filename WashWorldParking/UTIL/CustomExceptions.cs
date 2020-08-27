using System;
namespace WashWorldParking.UTIL
{
    public class LPlateSubscribed : Exception
    {
        public override string Message { get { return "It looks like the license plate is already subscribed!"; } }
    }

    public class OutOfRange : Exception
    {
        public override string Message { get { return "Whoopsie, it looks like you tried to\ninput something that isn't on the list."; } }
    }

    public class BadUser : Exception
    {
        public override string Message { get { return "It looks like you tried to cheat the system ...\nBad user!"; } }
    }
}
