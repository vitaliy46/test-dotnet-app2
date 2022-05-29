using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Координата позиции на изображении
    /// </summary>
    public class Point
    {
        
        /// <summary>
        /// .ctor with property values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
