
using Foundation.Patterns;

namespace Gameplay
{
    public class EventGameInit : IEvent { }
    public class EventGameReset : IEvent { }
    public class EventCoinChange : IEvent
    {
    }
    public class EventChangeTime : IEvent
    {
        public int time;
    }
}