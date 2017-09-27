namespace Tweets.Models
{
    using Records;
    using System.Collections.Generic;

    public class TweetModel
    {
        public Dictionary<User, List<Tweet>> UsersTweets = new Dictionary<User, List<Tweet>>();
    }
}