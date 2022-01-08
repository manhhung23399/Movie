using Movie.Core.Conversation;
using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movie.Core.Filters.FIlterExtensions
{
    public static class MovieOrderbyExtension
    {
        public static List<MovieModel> OrderByExtension(this List<MovieModel> movies, string orderBy)
        {
            try
            {
                IOrderedEnumerable<MovieModel> dataOrderBy;
                if (orderBy.Contains(","))
                {
                    var orderByArray = orderBy.Split(',').ToArray();
                    int count = 1;
                    if (orderByArray[0].Contains(" "))
                    {
                        string[] check = orderByArray[0].Split(' ');
                        if (check[1] == "desc")
                        {
                            dataOrderBy = movies.OrderByDescending(x =>
                            {
                                var checkData = x.GetType().GetProperty(check[0]).GetValue(x, null);
                                return checkData;
                            });
                        }
                        else
                        {
                            dataOrderBy = movies.OrderBy(x =>
                            {
                                var checkData = x.GetType().GetProperty(check[0]).GetValue(x, null);
                                return checkData;
                            });
                        }
                    }
                    else
                    {
                        dataOrderBy = movies.OrderBy(x =>
                        {
                            var checkData = x.GetType().GetProperty(orderByArray[0]).GetValue(x, null);
                            return checkData;
                        });
                    }
                    while (count < orderByArray.Length)
                    {
                        if (orderByArray[count].Contains(" "))
                        {
                            string[] check = orderByArray[count].Split(' ');
                            if (check[1] == "desc")
                            {
                                dataOrderBy = dataOrderBy.ThenByDescending(x =>
                                {
                                    var checkData = x.GetType().GetProperty(check[0]).GetValue(x, null);
                                    return checkData;
                                });
                            }
                            else
                            {
                                dataOrderBy = dataOrderBy.ThenBy(x =>
                                {
                                    var checkData = x.GetType().GetProperty(check[0]).GetValue(x, null);
                                    return checkData;
                                });
                            }
                        }
                        else
                        {
                            dataOrderBy = dataOrderBy.ThenBy(x =>
                            {
                                var checkData = x.GetType().GetProperty(orderByArray[count]).GetValue(x, null);
                                return checkData;
                            });
                        }
                    }
                }
                else
                {
                    if (orderBy.Contains(" "))
                    {
                        string[] check = orderBy.Split(' ');
                        if (check[1] == "desc")
                        {
                            dataOrderBy = movies.OrderByDescending(x =>
                            {
                                var checkData = x.GetType().GetProperty(check[0]).GetValue(x, null);
                                return checkData;
                            });
                        }
                        else
                        {
                            dataOrderBy = movies.OrderBy(x =>
                            {
                                var checkData = x.GetType().GetProperty(check[0]).GetValue(x, null);
                                return checkData;
                            });
                        }
                    }
                    else
                    {
                        dataOrderBy = movies.OrderBy(x =>
                        {
                            var checkData = x.GetType().GetProperty(orderBy).GetValue(x, null);
                            return checkData;
                        });
                    }
                }
                if(dataOrderBy.ToList().Count() > 0) return dataOrderBy.ToList();
                return movies;
            }
            catch
            {
                return movies;
            }
        }

    }
}
