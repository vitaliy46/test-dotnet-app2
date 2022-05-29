using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Core.Common;
using aiPeopleTracker.Wpf.Controls;

namespace aiPeopleTracker.ViewModels
{
    public class RecognizedPersonViewModel : ViewModelBase
    {
        public IMultitimeline Multitimeline { get; set; }

        // Коллекция для учета активных видеоклипов
        IDictionary<IVideoClip, PlayerDataContext> _videoCliPslayerDataContextsDictionary = new Dictionary<IVideoClip, PlayerDataContext>();

        private ObservableCollection<PlayerDataContext> _playersContexts;
        /// <summary>Конексты плееров</summary>
        public ObservableCollection<PlayerDataContext> PlayersContexts
        {
            get { return _playersContexts; }
            set { SetField(ref _playersContexts, value); }
        }

        private int _videoGridColumnCount;
        public int VideoGridColumnCount
        {
            get { return _videoGridColumnCount; }
            set
            {
                SetField(ref _videoGridColumnCount, value);
            }
        }

        private int _videoGridRowCount;
        public int VideoGridRowCount
        {
            get { return _videoGridRowCount; }
            set
            {
                SetField(ref _videoGridRowCount, value);
            }
        }

        private PlayerState _commonState;
        /// <summary>Общее состояние всех плееров шаблона</summary>
        public PlayerState CommonState
        {
            get { return _commonState; }
            set
            {
                SetField(ref _commonState, value);

                // применение состояния ко всем плеерам
                PlayersContexts.ForEach(ctx => ctx.State = value);
            }
        }

        public RecognizedPersonViewModel()
        {
            _playersContexts = new ObservableCollection<PlayerDataContext>();
        }

        public void ResetPlayersContexts()
        {
            PlayersContexts = new ObservableCollection<PlayerDataContext>();
            _videoCliPslayerDataContextsDictionary.Clear();
        }

        public void InitializeModel(IMultitimeline multitimeline)
        {
            Multitimeline = multitimeline;
        }

        /// <summary>
        /// Наполнение списка контекстов плееров
        /// </summary>
        public void FillPlayersContexts(MultitimelinePersonControl multitimelinePersonControl, NotifyCollectionChangedEventArgs e)
        {
            var playableVideoClips = multitimelinePersonControl.Multitimeline.PlayableVideoClips;

            var position = multitimelinePersonControl.SliderValue - multitimelinePersonControl.LeftDate;

            ConfigureGrid(playableVideoClips.Count);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    foreach (var item in e.NewItems)
                    {
                        var newPlayerDataContext = item as IVideoClip;
                        AddClip(newPlayerDataContext, position);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    foreach (var item in e.OldItems)
                    {
                        var oldPlayerDataContext = item as IVideoClip;
                        RemoveClip(oldPlayerDataContext);
                    }
                    break;
            }
        }

        /// <summary>
        /// Настраивает форму сетки в зависимости от 
        /// количества проигрываемых клипов
        /// </summary>
        /// <param name="countClips"></param>
        private void ConfigureGrid(int countClips)
        {
            if (countClips <= 1)
            {
                VideoGridRowCount = VideoGridColumnCount = 1;
            }
            else if (countClips <= 2)
            {
                VideoGridRowCount = 1;
                VideoGridColumnCount = 2;
            }
            else if (countClips <= 4)
            {
                VideoGridRowCount = VideoGridColumnCount = 2;
            }
        }

        private void AddClip(IVideoClip videoClip, TimeSpan position)
        {
            var playerDataContext = new PlayerDataContext(1, 1)
            {
                Stretch = Stretch.Uniform,
                State = CommonState,
                Source = new Uri(videoClip.Camera.CameraSettings.SourceAddress),
                Position = position
            };

            PlayersContexts.Add(playerDataContext);
            _videoCliPslayerDataContextsDictionary.Add(videoClip, playerDataContext);
        }

        private void RemoveClip(IVideoClip videoClip)
        {
            PlayerDataContext playerDataContext;
            _videoCliPslayerDataContextsDictionary.TryGetValue(videoClip, out playerDataContext);

            PlayersContexts.Remove(playerDataContext);
            _videoCliPslayerDataContextsDictionary.Remove(videoClip);
        }

        // Обработка события, изменение позиции проигрываемых клипов при ручном изменения позиции трекера с помощью ползунка
        public void TrackerPositionChangeOnPaused(MultitimelinePersonControl multitimelinePersonControl)
        {
            var position = multitimelinePersonControl.SliderValue - multitimelinePersonControl.LeftDate;

            PlayersContexts.ForEach(ctx => ctx.Position = position);
        }
    }
}