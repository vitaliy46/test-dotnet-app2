using System;

namespace aiPeopleTracker.Dal.Api.Dto
{
    public class DtoBase<K>
    {
        public K Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}