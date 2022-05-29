using System.Collections.Generic;

namespace aiPeopleTracker.Dal.Api.Dto
{
    /// <summary>Персона в пределах камеры</summary>
    public class PersonDto : DtoBase<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public byte[] Photo { get; set; }

        public string Description { get; set; }

        public virtual IList<PersonTagDto> Tags { get; set; }
       
    }
}