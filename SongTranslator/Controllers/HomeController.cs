using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SongTranslator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            if (!String.IsNullOrEmpty(Request.Params["whatDidSheSay"]))
                ViewBag.SheMeant = GenerateWhatSheMeant(Request.Params["whatDidSheSay"]);
            else
                ViewBag.SheMeant = String.Empty;


            ViewBag.SheSaid = Request.Params["whatDidSheSay"];
            ViewBag.ButtonText = "Let's Find Out!";

            return View();
        }

        private String GenerateWhatSheMeant(string whatSheSaid)
        {
            var sentences = whatSheSaid.Split('.').ToList();
            var newSentences = new List<String>();
            foreach(var sentence in sentences)
            {
                newSentences.Add(ScrambleSentence(sentence));
            }
            return String.Join(". ", newSentences);
        }

        private static string ScrambleSentence(string sentence)
        {
            var words = sentence.Split(' ').ToList();
            var newWords = new string[words.Count];
            Random rand = new Random();

            int index = 0;
            while (words.Count > 0)
            {
                int next = rand.Next(0, words.Count);
                newWords[index] = words[next];
                words.RemoveAt(next);
                index++;
            }

            var newSentence = String.Join(" ", newWords).Replace("Geoffrey", "Deoffrey").Replace("Malinda", "Amanda");
            return UppercaseFirst(newSentence);
        }

        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        private string ScrambleWord(string word)
        {
            char[] chars = new char[word.Length];
            Random rand = new Random(10000);

            int index = 0;

            while (word.Length > 0)
            {
                // Get a random number between 0 and the length of the word.
                int next = rand.Next(0, word.Length - 1);

                // Take the character from the random position and add to our char array.
                chars[index] = word[next];

                // Remove the character from the word.
                word = word.Substring(0, next) + word.Substring(next + 1);

                ++index;
            }

            return new String(chars);
        }
    
    }
}
