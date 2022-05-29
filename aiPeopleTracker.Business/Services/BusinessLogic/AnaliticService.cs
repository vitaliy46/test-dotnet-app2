using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Business.Data;
using aiPeopleTracker.Business.Services.Crud;

namespace aiPeopleTracker.Business.Services.BusinessLogic
{
    public class AnaliticService : ServiceBase, IAnaliticService
    {
        private IPersonCrudService _personCrudService;
        private IMultitimelineBuilder _multitimelineBuilder;
        private CameraSettingsCrudService _cameraSettingsCrudService;

        public AnaliticService(IPersonCrudService personCrudService,
            IMultitimelineBuilder multitimelineBuilder, CameraSettingsCrudService cameraSettingsCrudService)
        {
            _cameraSettingsCrudService = cameraSettingsCrudService ?? throw new ArgumentNullException(nameof(cameraSettingsCrudService));
            _multitimelineBuilder = multitimelineBuilder ?? throw new ArgumentNullException(nameof(multitimelineBuilder));
            _personCrudService = personCrudService ?? throw new ArgumentNullException(nameof(personCrudService));
        }

        public RecognizedPersonsScope GetRecognizedPersons(int cameraId, byte[] picture)
        {
            //TODO Спроектировать интерфейс обмена данными с сервисом аналитики
            var recognizedPersonsScope = new RecognizedPersonsScope();

            //Фейковая реализация
            IEnumerable<RecognizedPerson> recognizedPerson = GetFakePresetRecognizedPeople(cameraId);

            recognizedPerson.ForEach(p => recognizedPersonsScope.RecognizedPeople.Add(p));

            return recognizedPersonsScope;
        }

       

        public IMultitimeline GetMultitimelineByPerson(int personId)
        {
            var videoClips = GetFakeVideoClips(clipsCount: 20, camerasCount: 5);

            var multitimeline = _multitimelineBuilder.Build(videoClips);

            return multitimeline;
        }

        public IMultitimeline GetMultitimelineByObject(Point leftBottomPoint, Point rightTopPoint, int cameraId, byte[] picture)
        {
            //TODO Спроектировать интерфейс обмена данными с сервисом аналитики
            var videoClips = GetFakeVideoClips(clipsCount: 20, cameraId: cameraId);

            var multitimeline = _multitimelineBuilder.Build(videoClips);

            return multitimeline;
        }
        /// <summary>
        /// Временно используемый метод получения тестовых данных
        /// TODO после интеграции с внешним сервисом можно удалить
        /// или оставить для тестовых целей
        /// </summary>
        /// <param name="clipsCount"></param>
        /// <param name="camerasCount"></param>
        private IList<IVideoClip> GetFakeVideoClips(int clipsCount, int camerasCount)
        {
            var videoClips = new List<IVideoClip>();

            var cameraSettingsList = new List<CameraSettings>();

            Random rnd = new Random();

            var timeBeginVideoClip = DateTime.Now;

            cameraSettingsList = _cameraSettingsCrudService.GetList(null)
                .OrderBy(x => x.SourceAddress).Take(camerasCount).ToList();

            for (int i = 0; i < clipsCount; i++)
            {
                timeBeginVideoClip += TimeSpan.FromMilliseconds(rnd.Next(-8000, 4000));

                var cameraSettings = cameraSettingsList[rnd.Next(0, camerasCount)];

                if (videoClips.Any()
                    && timeBeginVideoClip < videoClips.Last().EndTime
                    && cameraSettings.CameraId == videoClips.Last().Camera.Id)
                //не допускается перехлест времени у клипов идущих подряд 
                //для одной и той же камеры
                {
                    i--; //чтобы не уменьшилось общее количество клипов
                    continue;
                }

                IVideoClip videoClip = new VideoClip
                {
                    //TODO Заменить этот хардкон с привязкой на ID загрузкой списка камер и выбора 
                    //из него нужного количества
                    Camera = new Camera
                    {
                        Id = cameraSettings.CameraId,
                        CameraSettings = cameraSettings
                    },
                    BeginTime = timeBeginVideoClip,
                    EndTime = timeBeginVideoClip + TimeSpan.FromMilliseconds(rnd.Next(10000, 20000))

                };

                videoClips.Add(videoClip);

                //начало каждого последующего клипа будет варироваться от конца предыдущего
                timeBeginVideoClip = videoClip.EndTime;
            }

            return videoClips;
        }

        /// <summary>
        /// Временно используемый метод получения тестовых данных
        /// TODO после интеграции с внешним сервисом можно удалить
        /// или оставить для тестовых целей
        /// </summary>
        /// <param name="clipsCount"></param>
        private IList<IVideoClip> GetFakeVideoClips(int clipsCount, long cameraId)
        {
            var videoClips = new List<IVideoClip>();

            Random rnd = new Random();

            var timeBeginVideoClip = DateTime.Now;

            var cameraSettings = _cameraSettingsCrudService.GetList(null)
                                    .FirstOrDefault(x => x.Id == cameraId);

            for (int i = 0; i < clipsCount; i++)
            {
                timeBeginVideoClip += TimeSpan.FromMilliseconds(rnd.Next(0, 10000));

                IVideoClip videoClip = new VideoClip
                {
                    Camera = new Camera
                    {
                        Id = cameraSettings.CameraId,
                        CameraSettings = cameraSettings
                    },
                    BeginTime = timeBeginVideoClip,
                    EndTime = timeBeginVideoClip + TimeSpan.FromMilliseconds(rnd.Next(10000, 20000))
                };
                videoClips.Add(videoClip);

                //начало каждого последующего клипа будет варироваться от конца предыдущего
                timeBeginVideoClip = videoClip.EndTime;
            }
            return videoClips;
        }

        /// <summary>
        /// Возвращает фейковый набор распознных персон
        /// </summary>
        /// <returns></returns>
        private IEnumerable<RecognizedPerson> GetFakePresetRecognizedPeople(int cameraId)
        {
            var people = _personCrudService.GetList(null).ToList();

            var countPerson = people.Count;

            Random rnd = new Random();

            //расстояние от центра квадрата до его боковой стороны
            var diametr = 25;

            ///Определяет набор камер и координат центров квадратов, которыми нужно обвести лица
            var rectagleCollection = new Dictionary<int, IList<Point>>();

            rectagleCollection.Add(1, new[] { new Point(615, 115), new Point(670, 157) });
            rectagleCollection.Add(2, new[] { new Point(414, 338), new Point(705, 108), new Point(761, 83) });
            rectagleCollection.Add(3, new[] { new Point(100, 197), new Point(274, 173) });
            rectagleCollection.Add(4, new[] { new Point(177, 467), new Point(267, 229), new Point(428, 163), new Point(750, 283), new Point(845, 124) });
            rectagleCollection.Add(5, new[] { new Point(221, 93),  new Point(303,122), new Point(240,161), new Point(976, 302), new Point(228,205) });
            rectagleCollection.Add(6, new[] { new Point(579, 100), new Point(695, 273) });
            rectagleCollection.Add(7, new[] { new Point(445, 136), new Point(538, 133) });
            rectagleCollection.Add(8, new[] { new Point(530, 143), new Point(771, 209) });
            rectagleCollection.Add(9, new[] { new Point(441, 180) });

            var rectangles = rectagleCollection.FirstOrDefault(x => x.Key == cameraId);
            
            var recognizedPerson = rectangles.Value.Select(point =>
            {
                var person = people[rnd.Next(0, countPerson)];
               
                return new RecognizedPerson
                {
                    Person = person,
                    PointLeftDown = new Point(point.X - diametr, point.Y + diametr),
                    PointRightUp = new Point(point.X + diametr, point.Y - diametr),
                };
            });

            return recognizedPerson;
        }
    }
}
