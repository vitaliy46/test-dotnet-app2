namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>��������� ����</summary>
    public class FloorPlan : EntityBase<int>
    {
        /// <summary>
        /// �������� ����������� ��������������� ������� ��� ���������� �����
        /// </summary>
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        /// <summary>
        /// ��������.
        /// ��� �������� ����� ������������ ������ �����, ����� ����������� ������ ���
        /// ������� ������ ��� ������ ������������ ������������ ���� � ����.
        /// � ������� �������� ���� ��� �������� ������� �� ���������.
        /// </summary>
        private byte[] _picture;
        public byte[] Picture
        {
            get { return _picture; }
            set { SetField(ref _picture, value); }
        }

        /// <summary>
        /// ������� � ����������� ��������� ���������� �����.
        /// ��� �������� �������� ��� �����, ��� �������� ����� ����������� ������.
        /// ���������� �� ������ ������ ������ ������ ��� ��������.
        /// </summary>
        private byte[] _pictureWithCameras;
        public byte[] PictureWithCameras
        {
            get { return _pictureWithCameras; }
            set { SetField(ref _pictureWithCameras, value); }
        }

        /// <summary>
        /// ����� ������������ ��������.
        /// ��� �������� ����� ������������ ������ �����, ����� ����������� ������ ���
        /// ������� ������ ��� ������ ������������ ������������ ���� � ����.
        /// � ������� �������� ���� ��� �������� ������� �� ���������.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// ����� ������������ �������� � ����������� ���������
        /// ���������� �����. ��� �������� �������� ��� �����, ���
        /// �������� ����� ����������� ������.
        /// ���������� �� ������ ������ ������ ������ ��� ��������.
        /// </summary>
        public string UriWithCameras { get; set; }


    }
}