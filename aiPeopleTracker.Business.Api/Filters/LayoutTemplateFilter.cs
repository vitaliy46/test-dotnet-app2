namespace aiPeopleTracker.Business.Api.Filters
{
    public class LayoutTemplateFilter : FilterBase
    {
        /// <summary>
        /// Используется для указания количества отображаемых 
        /// шаблонов в верхней сроке над просмотром камер, где можно
        /// осуществить выстрый переход к шаблонам
        /// </summary>
        public int? CountVisible { get; set; }
    }
}
