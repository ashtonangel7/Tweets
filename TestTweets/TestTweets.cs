namespace TestTweets
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestTweets
    {
        [TestMethod]
        [ExpectedException(typeof(TypeInitializationException))]
        public void TestEmptyUserFile()
        {        
            Tweets.App_Start.StaticResourcesConfig.RegisterStaticResources(@"..\..\..\TestFiles\emptyuser.txt", @"..\..\..\TestFiles\tweet.txt");
        }

        [TestMethod]
        public void TestEmptyTweetFile()
        {
            Tweets.App_Start.StaticResourcesConfig.RegisterStaticResources(@"..\..\..\TestFiles\user.txt", @"..\..\..\TestFiles\emptytweet.txt");
            Assert.IsTrue(Tweets.App_Start.StaticResourcesConfig.Tweets.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeInitializationException))]
        public void TestBadFormatUserFile()
        {
            Tweets.App_Start.StaticResourcesConfig.RegisterStaticResources(@"..\..\..\TestFiles\badformatuser.txt", @"..\..\..\TestFiles\tweet.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(TypeInitializationException))]
        public void TestBadFormatUserFile2()
        {
            Tweets.App_Start.StaticResourcesConfig.RegisterStaticResources(@"..\..\..\TestFiles\badformatuser2.txt", @"..\..\..\TestFiles\tweet.txt");
        }
    }
}
