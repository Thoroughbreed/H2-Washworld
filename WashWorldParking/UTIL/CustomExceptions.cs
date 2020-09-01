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

    public class NoWash : Exception
    {
        public override string Message { get { return "Unfortunately there's no available washes at the moment.\nPlease come back later."; } }
    }

    public class StopWash : Exception
    {
        public override string Message { get { return "Wash is stopped!"; } }
    }

    public class DOOM : Exception
    {
        public override string Message { get { return "You have unlocked godlike-mode ..."; } }
    }
}