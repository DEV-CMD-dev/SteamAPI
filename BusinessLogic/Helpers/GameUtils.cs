using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Helpers
{
    public static class GameUtils
    {
        public static bool GetHasRating(int totalReviews)
        {
            return totalReviews >= 10;
        }
    }
}
