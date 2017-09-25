namespace Tweets.Controllers
{
    using App_Start;
    using Models;
    using Records;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class TweetController : Controller
    {
        public ActionResult Index()
        {
            TweetModel tweetModel = new TweetModel();

            foreach (Tweet tweet in StaticResourcesConfig.Tweets)
            {

                if (!tweetModel.UsersTweets.ContainsKey(tweet.TweetingUser))
                {
                    List<Tweet> tweets = new List<Tweet>();
                    tweets.Add(tweet);
                    tweetModel.UsersTweets.Add(tweet.TweetingUser, tweets);
                }
                else
                {
                    tweetModel.UsersTweets[tweet.TweetingUser].Add(tweet);
                }

                List<User> usersFollowers = tweet.TweetingUser.GetFollowers();

                foreach (User follower in usersFollowers)
                {
                    if (!tweetModel.UsersTweets.ContainsKey(follower))
                    {
                        List<Tweet> tweets = new List<Tweet>();
                        tweets.Add(tweet);
                        tweetModel.UsersTweets.Add(follower, tweets);
                    }
                    else
                    {
                        tweetModel.UsersTweets[follower].Add(tweet);
                    }
                }
            }

            foreach (User user in StaticResourcesConfig.Users)
            {
                if (!tweetModel.UsersTweets.ContainsKey(user))
                {
                    tweetModel.UsersTweets.Add(user, null);
                }
            }

            Dictionary<User, List<Tweet>> outputModel = new Dictionary<User, List<Tweet>>();

            var orderedModel = tweetModel.UsersTweets.OrderBy(o => o.Key.Name);

            foreach(var userTweet in orderedModel)
            {
                outputModel.Add(userTweet.Key, userTweet.Value);
            }

            tweetModel.UsersTweets = outputModel;


            return View(tweetModel);
        }
    }
}