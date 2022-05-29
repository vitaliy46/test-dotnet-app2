using System;
using System.Collections.ObjectModel;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Информация о распознанных персон на стопкадре
    /// </summary>
    public class RecognizedPersonsScope : NotifyPropertyChanged
    {
        private IVideoClip _videoClip;
        private ObservableCollection<RecognizedPerson> _recognizedPeople;

        /// <summary>
        /// Список распознанных людей
        /// </summary>
        public ObservableCollection<RecognizedPerson> RecognizedPeople
        {
            get => _recognizedPeople;
            set => SetField(ref _recognizedPeople, value);
        }

        public IVideoClip VideoClip
        {
            get => _videoClip;
            set => SetField(ref _videoClip, value);
        }

        private DateTime _currentDate;

        /// <summary>
        /// Текущая дата стопкадра
        /// </summary>
        public DateTime CurrentDate
        {
            get => _currentDate;
            set => SetField(ref _currentDate, value);
        }

        public RecognizedPersonsScope()
        {
            RecognizedPeople = new ObservableCollection<RecognizedPerson>();
        }
       
    }
}
