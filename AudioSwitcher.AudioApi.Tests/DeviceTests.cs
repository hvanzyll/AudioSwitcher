﻿using System.Linq;
using AudioSwitcher.Tests.Common;
using Moq;
using Xunit;

namespace AudioSwitcher.AudioApi.Tests
{
    public partial class DeviceTests
    {
        private IAudioController CreateTestController()
        {
            return new TestDeviceController(2, 2);
        }


        [Fact]
        public void Device_DeviceType_PlaybackIsPlayback()
        {
            var controller = CreateTestController();
            Assert.True(DeviceType.Playback.HasFlag(controller.GetPlaybackDevices().First().DeviceType));
        }

        [Fact]
        public void Device_DeviceType_PlaybackIsAll()
        {
            var controller = CreateTestController();
            Assert.True(DeviceType.All.HasFlag(controller.GetPlaybackDevices().First().DeviceType));
        }

        [Fact]
        public void Device_DeviceType_CaptureIsCapture()
        {
            var controller = CreateTestController();
            Assert.True(DeviceType.Capture.HasFlag(controller.GetCaptureDevices().First().DeviceType));
        }

        [Fact]
        public void Device_DeviceType_CaptureIsAll()
        {
            var controller = CreateTestController();
            Assert.True(DeviceType.All.HasFlag(controller.GetCaptureDevices().First().DeviceType));
        }

        [Fact]
        public void DefaultDeviceChangedArgs_Sets_Device_And_Type_No_Default()
        {
            var device = new Mock<IDevice>();

            device.Setup(x => x.IsDefaultDevice).Returns(false);
            device.Setup(x => x.IsDefaultCommunicationsDevice).Returns(false);

            var args = new DefaultDeviceChangedArgs(device.Object);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.DefaultChanged, args.ChangedType);
            Assert.False(args.IsDefault);
            Assert.False(args.IsDefaultCommunications);
        }

        [Fact]
        public void DefaultDeviceChangedArgs_Sets_Device_And_Type_Default_Only()
        {
            var device = new Mock<IDevice>();

            device.Setup(x => x.IsDefaultDevice).Returns(true);
            device.Setup(x => x.IsDefaultCommunicationsDevice).Returns(false);

            var args = new DefaultDeviceChangedArgs(device.Object);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.DefaultChanged, args.ChangedType);
            Assert.True(args.IsDefault);
            Assert.False(args.IsDefaultCommunications);
        }

        [Fact]
        public void DefaultDeviceChangedArgs_Sets_Device_And_Type_DefaultComm_Only()
        {
            var device = new Mock<IDevice>();

            device.Setup(x => x.IsDefaultDevice).Returns(false);
            device.Setup(x => x.IsDefaultCommunicationsDevice).Returns(true);

            var args = new DefaultDeviceChangedArgs(device.Object);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.DefaultChanged, args.ChangedType);
            Assert.False(args.IsDefault);
            Assert.True(args.IsDefaultCommunications);
        }

        [Fact]
        public void DefaultDeviceChangedArgs_Sets_Device_And_Type_Both_Default()
        {
            var device = new Mock<IDevice>();

            device.Setup(x => x.IsDefaultDevice).Returns(true);
            device.Setup(x => x.IsDefaultCommunicationsDevice).Returns(true);

            var args = new DefaultDeviceChangedArgs(device.Object);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.DefaultChanged, args.ChangedType);
            Assert.True(args.IsDefault);
            Assert.True(args.IsDefaultCommunications);
        }

        [Fact]
        public void DeviceAddedArgs_Sets_Device_And_Type()
        {
            var device = new Mock<IDevice>();
            var args = new DeviceAddedArgs(device.Object);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.DeviceAdded, args.ChangedType);
        }

        [Fact]
        public void DeviceRemovedArgs_Sets_Device_And_Type()
        {
            var device = new Mock<IDevice>();
            var args = new DeviceRemovedArgs(device.Object);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.DeviceRemoved, args.ChangedType);
        }

        [Fact]
        public void DeviceStateChangedArgs_Sets_Device_And_Type()
        {
            const DeviceState state = DeviceState.Active;
            var device = new Mock<IDevice>();
            var args = new DeviceStateChangedArgs(device.Object, state);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.StateChanged, args.ChangedType);
            Assert.Equal(state, args.State);
        }

        [Fact]
        public void DeviceVolumeChangedArgs_Sets_Device_And_Type()
        {
            const int volume = 23;
            var device = new Mock<IDevice>();
            var args = new DeviceVolumeChangedArgs(device.Object, volume);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(volume, args.Volume);
        }

        [Fact]
        public void DevicePropertyChangedArgs_Sets_Device_And_Type()
        {
            const string propertyName = "something";
            var device = new Mock<IDevice>();
            var args = new DevicePropertyChangedArgs(device.Object, propertyName);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.PropertyChanged, args.ChangedType);
            Assert.Equal(propertyName, args.PropertyName);
        }

        [Fact]
        public void DeviceMuteChangedArgs_Sets_Device_And_Type_False()
        {
            const bool isMuted = false;
            var device = new Mock<IDevice>();
            var args = new DeviceMuteChangedArgs(device.Object, isMuted);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.MuteChanged, args.ChangedType);
            Assert.Equal(isMuted, args.IsMuted);
        }

        [Fact]
        public void DeviceMuteChangedArgs_Sets_Device_And_Type_True()
        {
            const bool isMuted = true;
            var device = new Mock<IDevice>();
            var args = new DeviceMuteChangedArgs(device.Object, isMuted);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.MuteChanged, args.ChangedType);
            Assert.Equal(isMuted, args.IsMuted);
        }


        [Fact]
        public void DevicePeakVolumeChangedArgs_Sets_Device_And_Type()
        {
            const double peakValue = 24;
            var device = new Mock<IDevice>();
            var args = new DevicePeakValueChangedArgs(device.Object, peakValue);

            Assert.NotNull(args);
            Assert.NotNull(args.Device);
            Assert.Equal(DeviceChangedType.PeakValueChanged, args.ChangedType);
            Assert.Equal(peakValue, args.PeakValue);
        }

    }
}
