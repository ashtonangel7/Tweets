# Tweets

Tweets is a demo application to show 3 users tweeting.

The users are loaded from a pre-populated text file.  
The tweets are also loaded from a pre-populated text file.

The application tries to load the files from the Global.asax
as this should be a once off operation for the scope of this application.
If loading of these files fails for whichever reason the load is
 re-attempted on each subsequent request.

 Please note I have used Linq to sort the dictionary, 
 I realize I could use a sorted list or dictionary here, 
 for the scope of this test it seems unnecessary.

 `var orderedModel = tweetModel.UsersTweets.OrderBy(o => o.Key.Name);`

 ----

 #### How To Run

 Open the solution in VS and hit run.  
 There are some basic test which can also be executed in the TestTweets Project.