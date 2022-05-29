namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>
    /// ������� ������������� ���������, ������ ����� �������������
    /// � ��������. ������ ���� ����� ��������� ��� ���������� �������
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