using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Распознанная персона на стопкадре
    /// </summary>
    public class RecognizedPerson : NotifyPropertyChanged
    {
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { SetField(ref _person, value); }
        }

        /// <summary>
        /// Левый низний угол рамки
        /// </summary>
        private Point _pointLeftDown;
        public Point PointLeftDown
        {
            get { return _pointLeftDown; }
            set { SetField(ref _pointLeftDown, value); }
        }

        /// <summary>
        /// Верхний правый угол рамки
        /// </summary>
        private Point _pointRightUp;
        public Point PointRightUp
        {
            get { return _pointRightUp; }
            set { SetField(ref _pointRightUp, value); }
        }
    }
}
