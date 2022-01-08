using Movie.Core.Conversation;
using Movie.Core.Entities;
using Movie.Core.Resources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Movie.Core.Filters.FIlterExtensions
{
    public static class MovieFilterExtension
    {
        public static List<MovieModel> FilterData(this List<MovieModel> movies, string filter)
        {
            if (filter.Contains(","))
            {
                List<string> filters = filter.Split(",").ToList();
                filters.ForEach(filter =>
                {
                    movies = movies.FilterList(filter);
                });
            }
            else
            {
                movies = movies.FilterList(filter);
            }
            return movies;
        }
        public static List<MovieModel> FilterList(this List<MovieModel> movies, string filter)
        {
            string[] filterItem = filter.Split("+").ToArray();
            if(filterItem[1] == "like")
            {
                Regex regex = new Regex(NonAccentVietnamese.Test(filterItem[2]), RegexOptions.IgnoreCase);
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => regex.IsMatch(NonAccentVietnamese.Test(valueData)), filterItem);
                }).ToList();
            }
            if (filterItem[1] == "eq")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => valueData.Contains(valueCheck), filterItem);
                }).ToList();
            }
            if (filterItem[1] == "ne")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => valueData.Contains(valueCheck) == false, filterItem);
                }).ToList();
            }
            if (filterItem[1] == "gt")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => Convert.ToInt64(valueData) > Convert.ToInt64(valueCheck), filterItem);
                }).ToList();
            }
            if (filterItem[1] == "ge")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => Convert.ToInt64(valueData) >= Convert.ToInt64(valueCheck), filterItem);
                }).ToList();
            }
            if (filterItem[1] == "lt")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => Convert.ToInt64(valueData) < Convert.ToInt64(valueCheck), filterItem);
                }).ToList();
            }
            if (filterItem[1] == "le")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => Convert.ToInt64(valueData) <= Convert.ToInt64(valueCheck), filterItem);
                }).ToList();
            }
            if (filterItem[1] == "in")
            {
                movies = movies.Where(x =>
                {
                    return x.WhereExtension((valueData, valueCheck) => valueData.Contains(valueCheck), filterItem);
                }).ToList();
            }
            return movies;
        }
        public static IEnumerable<MovieModel> RandomElement(this List<MovieModel> movie, int random)
        {
            var r = new Random();
            var listRandom = new List<int>();
            for (int i = 0; i < random; i++)
            {
                int elementRandom = r.Next(1, movie.Count());
                if (listRandom.Contains(elementRandom)) continue;
                listRandom.Add(elementRandom);
                yield return movie.ElementAt(elementRandom);
            }
        }

        public static bool WhereExtension(
            this MovieModel movie, 
            Func<string, string, bool> action, 
            string[] filterItem)
        {
            try
            {
                var item = movie.AsDictionary();
                string key = filterItem[0];
                if (item.ContainsKey(key))
                {
                    var valueData = item[key];
                    var valueCheck = filterItem[2];
                    try
                    {
                        bool boll = action(valueData.ToString(), valueCheck);
                        return boll;
                    }
                    catch { return false; }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
