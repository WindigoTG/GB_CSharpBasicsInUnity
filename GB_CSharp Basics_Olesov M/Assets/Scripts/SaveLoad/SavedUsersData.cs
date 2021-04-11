using System.Collections.Generic;
using System;

namespace BallGame
{
    [Serializable]
    public sealed class SavedUsersData
    { 
        public List<UserData> Users;
    }
}