namespace aiPeopleTracker.Dal.Api.Dto
{
    /// <summary>
    /// Отметки произвольного характера, котрые могут прикрепляться
    /// к человеку. Список этих тегов админится при настройках системы
    /// </summary>
    public class PersonTagDto : DtoBase<int>
    {
        public string Name { get; set; }
    }
}