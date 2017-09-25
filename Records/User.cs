namespace Tweets.Records
{
    using System;
    using System.Collections.Generic;
    public class User
    {

        private readonly List<User> _followers;
        private readonly string _name;

        public User(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name passed into User may not be null or empty.");
            }

            _name = name;
            _followers = new List<User>();
        }


        public string Name
        {
            get
            {
                return _name;
            }
        }

        internal void AddFollower(User follower)
        {
            if (!_followers.Contains(follower))
            {
                _followers.Add(follower);
            }
        }

        internal List<User> GetFollowers()
        {
            return _followers;
        }

        public override bool Equals(object obj)
        {
            User userToCompare;

            if (obj == null)
            {
                return false;
            }

            try
            {
                userToCompare = (User)obj;
            }
            catch
            {
                return false;
            }

            return userToCompare.Name.ToLower() == Name.ToLower();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}