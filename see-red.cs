using System;
using System.Linq;
using RedditSharp;
using System.Net;
using RedditSharp.Things;

namespace reSharp
{
    class Program
    {
        public static void Main()
        {
            string sub = "/r/";
            string saveDir = @"USER_DEFINED_DIR";
            Console.Write("Subreddit >");
            sub += Console.ReadLine();

            Console.Write("Amount >");
            int amount = (int)Console.ReadLine());
            amount += 1;

            Console.WriteLine("Time Period");
            string timePer = Console.ReadLine();

            Reddit reddit = new Reddit();
            var subreddit = reddit.GetSubreddit(sub);
            foreach (var post in subreddit.GetTop(FromTime.All).Take(amount))
            {
                if (post.IsStickied || post.IsSelfPost || post.Url.ToString().Contains("reddituploads")) continue;
                DownloadImages(post.Url.ToString(), saveDir);

            }

        }

        public static void DownloadImages(string imageURL, string userDir)
        {

            if (imageURL.Contains("gfycat.com")) { imageURL = imageURL.Replace("gfycat.com", "zippy.gfycat.com") + ".mp4"; }
            if (imageURL.Contains(".gifv")) { imageURL = imageURL.Replace(".gifv", ".mp4"); }
            
            Console.WriteLine("Downloading {0}", imageURL);
            string fileName = imageURL.Split('/').Last();
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(imageURL, Path.Combine(userDir, fileName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INFO] ERROR DOWNLOADING FILE: {ex.Message}");
            }
        }
    }
}
