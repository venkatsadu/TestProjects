using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LongestWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var totalListOfWords = File.ReadLines(@"C:\VenkatLearning\words1.txt").Select(l => l).ToArray();

            int totalCountofConstructedWords = 0;
            List<string> topTwoConstructedWordsList = FindLongestWords(totalListOfWords, out totalCountofConstructedWords);

            Console.WriteLine("Total count of words            : " + totalListOfWords.Length);
            Console.WriteLine("First longest constructed word  : " + topTwoConstructedWordsList[0]);
            Console.WriteLine("Second longest constructed word : " + topTwoConstructedWordsList[1]);
            Console.WriteLine("Total count of constructed words: " + totalCountofConstructedWords);

            Console.ReadKey(true);
        }

        /// <summary>
        /// Find the finst and second longest constructeds from the list 
        /// </summary>
        /// <param name="totalListOfWords"></param>
        /// <param name="totalCountofConstructedWords"></param>
        /// <returns></returns>
        public static List<string> FindLongestWords(string[] totalListOfWords, out int totalCountofConstructedWords)
        {
            if (totalListOfWords == null) throw new ArgumentNullException("totalListOfWords");
            totalCountofConstructedWords = 0;
            var sortedWordsList = totalListOfWords.OrderByDescending(word => word.Length).ToList();

            var sortedList = new HashSet<String>(sortedWordsList);

            List<string> topTwoWordsList = new List<string>();

            foreach (var word in sortedWordsList)
            {
                if (IsCombinationOfWords(word, sortedList))
                {
                    totalCountofConstructedWords++;
                    if (topTwoWordsList.Count < 2)
                    {
                        if (topTwoWordsList.Count == 0)
                        {
                            topTwoWordsList.Add(word);
                        }
                        else
                        {
                            if (topTwoWordsList[0].Length > word.Length)
                            {
                                topTwoWordsList.Add(word);
                            }
                        }
                    }
                }
            }

            return topTwoWordsList;
        }

        /// <summary>
        /// Check if the word is combination of otherwords in the list
        /// </summary>
        /// <param name="word"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        private static bool IsCombinationOfWords(string word, ICollection<string> sortedList)
        {
            if (String.IsNullOrEmpty(word)) return false;
            if (word.Length == 1)
            {
                return sortedList.Contains(word);
            }
            foreach (var wordCombination in GetWordCombinations(word).Where(wordCombination => sortedList.Contains(wordCombination.Item1)))
            {
                return sortedList.Contains(wordCombination.Item2) || IsCombinationOfWords(wordCombination.Item2, sortedList);
            }
            return false;
        }

        /// <summary>
        /// Split the word into all possible combinations
        /// eg: If the word is apple, split into apple -> a, pple -> ap, ple --> app, le --> apple, e 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static List<Tuple<string, string>> GetWordCombinations(string word)
        {
            var output = new List<Tuple<string, string>>();
            for (int i = 1; i < word.Length; i++                                                                                                                                                                                                                                                                        )
            {
                output.Add(Tuple.Create(word.Substring(0, i), word.Substring(i)));
            }
            return output;
        }


    }




}
