using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Helpers
{
    public class OnlineUsersStore
    {
        public static readonly ConcurrentDictionary<string, int> OnlineUsers = new();
    }
}
