using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class MovieDetailBuilder
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Poster { get; set; }
        public int YearRelease { get; set; }
        public bool Status { get; set; }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public MovieDetailBuilder AddWithMovieId(string id)
        {
            Id = id;
            return this;
        }
        public MovieDetailBuilder AddWithDescription(string description)
        {
            Description = description;
            return this;
        }
        public MovieDetailBuilder AddWithTitle(string title)
        {
            Title = title;
            return this;
        }
        public MovieDetailBuilder AddWithStatus(bool status)
        {
            Status = status;
            return this;
        }
        public MovieDetailBuilder AddWithPoster(string poster)
        {
            Poster = poster;
            return this;
        }
        public MovieDetailBuilder AddWithYearRelease(int yearRelease)
        {
            YearRelease = yearRelease;
            return this;
        }
        public MovieDetailBuilder AddWithVote(int voteCount, double voteAverage)
        {
            VoteCount = voteCount;
            VoteAverage = voteAverage;
            return this;
        }
        public MovieDetailResponnse Build()
        {
            return new MovieDetailResponnse(Id, Title, Description, Poster, YearRelease, Status, VoteAverage, VoteCount);
        }
    }
}
