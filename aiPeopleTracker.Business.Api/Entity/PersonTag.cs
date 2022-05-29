namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>
    /// Отметки произвольного характера, котрые могут прикрепляться
    /// к человеку. Список этих тегов админится при настройках системы
    /// </summary>
    public class PersonTag : EntityBase<int>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        public override string ToString()
        {
            return $" {Name}";
        }
    }
}