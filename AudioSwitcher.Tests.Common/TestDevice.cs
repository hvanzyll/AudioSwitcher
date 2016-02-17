﻿using System;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi;

namespace AudioSwitcher.Tests.Common
{
    public sealed class TestDevice : Device
    {
        private readonly DeviceType _deviceType;
        private readonly TestDeviceController _controller;
        private Guid _id;
        private bool _muted;

        public TestDevice(Guid id, DeviceType dFlow, TestDeviceController controller)
            : base(controller)
        {
            _id = id;
            _deviceType = dFlow;
            _controller = controller;
        }

        public override Guid Id => _id;

        public override string InterfaceName => Id.ToString();

        public override string Name
        {
            get { return Id.ToString(); }
            set { }
        }

        public override string FullName => Id.ToString();

        public override DeviceIcon Icon => DeviceIcon.Unknown;

        public override string IconPath => "";

        public override bool IsDefaultDevice => (Controller.DefaultPlaybackDevice != null && Controller.DefaultPlaybackDevice.Id == Id)
                                                || (Controller.DefaultCaptureDevice != null && Controller.DefaultCaptureDevice.Id == Id);

        public override bool IsDefaultCommunicationsDevice => (Controller.DefaultPlaybackCommunicationsDevice != null && Controller.DefaultPlaybackCommunicationsDevice.Id == Id)
                                                              || (Controller.DefaultCaptureCommunicationsDevice != null && Controller.DefaultCaptureCommunicationsDevice.Id == Id);

        public override DeviceState State => DeviceState.Active;

        public override DeviceType DeviceType => _deviceType;

        public override bool IsMuted => _muted;

        public override double Volume
        {
            get;
            set;
        }

        public override bool SetAsDefault()
        {
            if (IsPlaybackDevice)
                _controller.DefaultPlaybackDevice = this;
            else
                _controller.DefaultCaptureDevice = this;

            return IsDefaultDevice;
        }

        public override Task<bool> SetAsDefaultAsync()
        {
            if (IsPlaybackDevice)
                _controller.DefaultPlaybackDevice = this;
            else
                _controller.DefaultCaptureDevice = this;

            return Task.FromResult(IsDefaultDevice);
        }

        public override bool SetAsDefaultCommunications()
        {
            if (IsPlaybackDevice)
                _controller.DefaultPlaybackCommunicationsDevice = this;
            else
                _controller.DefaultCaptureCommunicationsDevice = this;

            return IsDefaultCommunicationsDevice;
        }

        public override Task<bool> SetAsDefaultCommunicationsAsync()
        {
            if (IsPlaybackDevice)
                _controller.DefaultPlaybackCommunicationsDevice = this;
            else
                _controller.DefaultCaptureCommunicationsDevice = this;

            return Task.FromResult(IsDefaultCommunicationsDevice);
        }

        public override bool Mute(bool mute)
        {
            return _muted = mute;
        }

        public override Task<bool> MuteAsync(bool mute)
        {
            return Task.FromResult(_muted = mute);
        }
    }
}