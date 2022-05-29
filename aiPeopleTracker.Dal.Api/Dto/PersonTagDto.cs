namespace aiPeopleTracker.Dal.Api.Dto
{
    /// <summary>
    /// ������� ������������� ���������, ������ ����� �������������
    /// � ��������. ������ ���� ����� ��������� ��� ���������� �������
    /// </summary>
    public class PersonTagDto : DtoBase<int>
    {
        public string Name { get; set; }
    }
}