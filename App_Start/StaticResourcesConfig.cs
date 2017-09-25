namespace Tweets.App_Start
{
    using Records;
    using System;
    using System.Collections.Generic;
    using System.IO;
    public class StaticResourcesConfig
    {

        private static readonly List<User> users = new List<User>();
        private static readonly List<Tweet> tweets = new List<Tweet>();

        public static List<User> Users
        {
            get
            {
                return users;
            }
        }

        public static List<Tweet> Tweets
        {
            get
            {
                return tweets;
            }
        }

        public static void RegisterStaticResources(string usersFilePath,
            string tweetsFilePath)
        {

            string usersFile = AppDomain.CurrentDomain.BaseDirectory + usersFilePath;
            string tweetsFile = AppDomain.CurrentDomain.BaseDirectory + tweetsFilePath;

            ReadUsersAndFollowersFromFile(usersFile);
            ReadTweetsFromFile(tweetsFile);
        }

        private static void ReadUsersAndFollowersFromFile(string usersFilePath)
        {
            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(usersFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load the required users and followers file.", ex);
            }

            using (streamReader)
            {

                while (!streamReader.EndOfStream)
                {
                    string line;
                    try
                    {
                        line = streamReader.ReadLine();
                    }
                    catch(OutOfMemoryException ex)
                    {
                        throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                            ex);
                    }
                    catch(IOException ex)
                    {
                        throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                            ex);
                    }

                    string[] usersAndFollowers;

                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        usersAndFollowers = line.Split(' ');

                        AddUserAndFollowers(line, usersAndFollowers);
                    }
                    else
                    {
                        throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                            new Exception("Expected a user file containing at least one user."));
                    }
                }
            }
        }

        private static void AddUserAndFollowers(string line, string[] usersAndFollowers)
        {

            if (usersAndFollowers.Length == 2)
            {
                throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                         new Exception("Loaded Users file has bad formatting in this line = " + line));
            }

            User follower = new User(usersAndFollowers[0]);

            if (!Users.Contains(follower))
            {
                Users.Add(follower);
            }

            for (int loop = usersAndFollowers.Length - 1; loop > 1; loop--)
            {
                string usersName = usersAndFollowers[loop].Replace(",", "");

                if (string.IsNullOrWhiteSpace(usersName))
                {
                    throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                          new Exception("Loaded Users file has bad formatting in this line = "+ line));
                }

                User user = new User(usersName);

                user.AddFollower(follower);

                if (!Users.Contains(user))
                {
                    Users.Add(user);
                }
                else
                {
                    Users[Users.IndexOf(user)].AddFollower(follower);
                }
            }
        }

        private static void ReadTweetsFromFile(string tweetsFilePath)
        {
            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(tweetsFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load tweets file.", ex);
            }

            using (streamReader)
            {

                while (!streamReader.EndOfStream)
                {
                    string line;
                    try
                    {
                        line = streamReader.ReadLine();
                    }
                    catch (OutOfMemoryException ex)
                    {
                        throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                            ex);
                    }
                    catch (IOException ex)
                    {
                        throw new TypeInitializationException("Tweets.App_Start.StaticResourcesConfig",
                            ex);
                    }

                    string[] usersAndTweet;

                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        usersAndTweet = line.Split('>');

                        AddUserAndTweet(line, usersAndTweet);
                    }
                }
            }
        }

        private static void AddUserAndTweet(string line, string[] usersAndTweet)
        {
            if (usersAndTweet.Length == 2)
            {
                User tweetingUser = Users[Users.IndexOf(new User(usersAndTweet[0]))];

                Tweet newTweet = new Tweet(usersAndTweet[1].Trim(), tweetingUser);
                Tweets.Add(newTweet);
            }
        }
    }
}