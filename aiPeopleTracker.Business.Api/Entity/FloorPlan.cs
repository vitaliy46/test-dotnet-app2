namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>ѕоэтажный план</summary>
    public class FloorPlan : EntityBase<int>
    {
        /// <summary>
        /// ќписание указываемое одминистратором системы при добавлении плана
        /// </summary>
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        /// <summary>
        ///  артинка.
        /// Ёта картинка будет использована только тогда, когда потребуетс€ первый раз
        /// нанести камеры или заново переотметить расположение всех с нул€.
        /// ¬ рабочих област€х форм эта картинка никогда не выводитс€.
        /// </summary>
        private byte[] _picture;
        public byte[] Picture
        {
            get { return _picture; }
            set { SetField(ref _picture, value); }
        }

        /// <summary>
        ///  артинк с ненесенными отметками размещени€ камер.
        /// ѕри загрузке картинки без камер, это свойство сразу заполн€етс€ планом.
        /// ¬ыводитьс€ на формах всегда должна именно эта картинка.
        /// </summary>
        private byte[] _pictureWithCameras;
        public byte[] PictureWithCameras
        {
            get { return _pictureWithCameras; }
            set { SetField(ref _pictureWithCameras, value); }
        }

        /// <summary>
        /// ћесто расположени€ картинки.
        /// Ёта картинка будет использована только тогда, когда потребуетс€ первый раз
        /// нанести камеры или заново переотметить расположение всех с нул€.
        /// ¬ рабочих област€х форм эта картинка никогда не выводитс€.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// ћесто расположени€ картинки с ненесенными отметками
        /// размещени€ камер. ѕри загрузке картинки без камер, это
        /// свойство сразу заполн€етс€ планом.
        /// ¬ыводитьс€ на формах всегда должна именно эта картинка.
        /// </summary>
        public string UriWithCameras { get; set; }


    }
}