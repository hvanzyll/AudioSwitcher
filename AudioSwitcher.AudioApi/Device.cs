﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.Observables;

namespace AudioSwitcher.AudioApi
{
    /// <summary>
    ///     Provides a basic implementation of IDevice so that developers need not implement very common functionality.
    ///     Should be used in place of IDevice where possible
    /// </summary>
    public abstract class Device : IDevice
    {
        private readonly Broadcaster<DefaultDeviceChangedArgs> _defaultChanged;
        private readonly Broadcaster<DeviceMuteChangedArgs> _muteChanged;
        private readonly Broadcaster<DevicePeakValueChangedArgs> _peakValueChanged;
        private readonly Broadcaster<DevicePropertyChangedArgs> _propertyChanged;
        private readonly Broadcaster<DeviceStateChangedArgs> _stateChanged;
        private readonly Broadcaster<DeviceVolumeChangedArgs> _volumeChanged;

        protected Device(IAudioController controller)
        {
            Controller = controller;
            _muteChanged = new Broadcaster<DeviceMuteChangedArgs>();
            _stateChanged = new Broadcaster<DeviceStateChangedArgs>();
            _volumeChanged = new Broadcaster<DeviceVolumeChangedArgs>();
            _defaultChanged = new Broadcaster<DefaultDeviceChangedArgs>();
            _propertyChanged = new Broadcaster<DevicePropertyChangedArgs>();
            _peakValueChanged = new Broadcaster<DevicePeakValueChangedArgs>();
        }
        ~Device()
        {
            Dispose(false);
        }

        public IAudioController Controller { get; private set; }

        public abstract Guid Id { get; }

        public abstract string Name { get; set; }

        public abstract string InterfaceName { get; }

        public abstract string FullName { get; }

        public abstract DeviceIcon Icon { get; }
        public abstract string IconPath { get; }

        public abstract bool IsDefaultDevice { get; }

        public abstract bool IsDefaultCommunicationsDevice { get; }

        public abstract DeviceState State { get; }

        public abstract DeviceType DeviceType { get; }

        public virtual bool IsPlaybackDevice
        {
            get
            {
                return DeviceType == DeviceType.Playback || DeviceType == DeviceType.All;
            }
        }

        public virtual bool IsCaptureDevice
        {
            get
            {
                return DeviceType == DeviceType.Capture || DeviceType == DeviceType.All;
            }
        }

        public abstract bool IsMuted { get; }

        public abstract double Volume { get; set; }

        public IObservable<DeviceVolumeChangedArgs> VolumeChanged
        {
            get { return _volumeChanged.AsObservable(); }
        }

        public IObservable<DeviceMuteChangedArgs> MuteChanged
        {
            get { return _muteChanged.AsObservable(); }
        }

        public IObservable<DevicePropertyChangedArgs> PropertyChanged
        {
            get { return _propertyChanged.AsObservable(); }
        }

        public IObservable<DefaultDeviceChangedArgs> DefaultChanged
        {
            get { return _defaultChanged.AsObservable(); }
        }

        public IObservable<DeviceStateChangedArgs> StateChanged
        {
            get { return _stateChanged.AsObservable(); }
        }

        public IObservable<DevicePeakValueChangedArgs> PeakValueChanged
        {
            get { return _peakValueChanged.AsObservable(); }
        }

        public abstract bool SetAsDefault();
        public abstract Task<bool> SetAsDefaultAsync();
        public abstract bool SetAsDefaultCommunications();
        public abstract Task<bool> SetAsDefaultCommunicationsAsync();

        public abstract bool Mute(bool mute);

        public abstract Task<bool> MuteAsync(bool mute);

        public virtual bool ToggleMute()
        {
            return Mute(!IsMuted);
        }

        public virtual Task<bool> ToggleMuteAsync()
        {
            return MuteAsync(!IsMuted);
        }

        public virtual Task<double> SetVolumeAsync(double volume)
        {
            return Task.Run(() => Volume = volume);
        }


        protected virtual void OnMuteChanged(bool isMuted)
        {
            _muteChanged.OnNext(new DeviceMuteChangedArgs(this, isMuted));
        }

        protected virtual void OnVolumeChanged(double volume)
        {
            _volumeChanged.OnNext(new DeviceVolumeChangedArgs(this, volume));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            _propertyChanged.OnNext(new DevicePropertyChangedArgs(this, propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<IDevice, object>> expression)
        {
            _propertyChanged.OnNext(DevicePropertyChangedArgs.FromExpression(this, expression));
        }

        protected virtual void OnDefaultChanged()
        {
            _defaultChanged.OnNext(new DefaultDeviceChangedArgs(this));
        }

        protected virtual void OnStateChanged(DeviceState state)
        {
            _stateChanged.OnNext(new DeviceStateChangedArgs(this, state));
        }

        protected virtual void OnPeakValueChanged(double peakValue)
        {
            _peakValueChanged.OnNext(new DevicePeakValueChangedArgs(this, peakValue));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _muteChanged.Dispose();
            _stateChanged.Dispose();
            _volumeChanged.Dispose();
            _defaultChanged.Dispose();
            _propertyChanged.Dispose();
            _peakValueChanged.Dispose();
        }
    }
}