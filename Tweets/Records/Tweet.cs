namespace Tweets.Records
{
    public class Tweet
    {
        private readonly string _message;
        private readonly User _tweetingUser;

        public Tweet(string message, User tweetingUser)
        {
            _tweetingUser = tweetingUser;
            _message = message;
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public User TweetingUser
        {
            get
            {
                return _tweetingUser;
            }
        }
    }
}