using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>Персона в пределах камеры</summary>
    public class Person : EntityBase<int>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { SetField(ref _surname, value); }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        private string _patronymic;
        public string Patronymic
        {
            get { return _patronymic; }
            set { SetField(ref _patronymic, value); }
        }

        /// <summary>
        /// Фотография
        /// </summary>
        private byte[] _photo;
        public byte[] Photo
        {
            get { return _photo; }
            set { SetField(ref _photo, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        private SortableObservableCollection<PersonTag> _tags;
        public SortableObservableCollection<PersonTag> Tags
        {
            get { return _tags; }
            set { SetField(ref _tags, value); }
        }

        public override string ToString()
        {
            return $"{Surname}, {Name}, {Patronymic}";
        }
    }
}