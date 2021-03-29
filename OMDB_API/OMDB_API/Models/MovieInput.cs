using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMDB_API.Models
{
    public class MovieInput
    {
        public MovieInput() { }
        public MovieInput(string title, int year, PlotSize plotSize)
        {
            Title = title;
            Year = year;
            PlotSize = plotSize;
        }
        public string Title { get; set; }
        public int Year { get; set; }
        public PlotSize PlotSize { get; set; }
    }
    public enum PlotSize
    {
        Short,
        Full
    }
}
