using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using System;

namespace AudioSwitcher.AudioApi.CoreAudio
{
	internal static class ComObjectFactory
	{
		public static IMultimediaDeviceEnumerator GetDeviceEnumerator()
		{
#pragma warning disable CA1416
			return Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(ComInterfaceIds.DEVICE_ENUMERATOR_CID))) as IMultimediaDeviceEnumerator;
#pragma warning restore CA1416
		}

		public static object GetPolicyConfig()
		{
#pragma warning disable CA1416
			return Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(ComInterfaceIds.POLICY_CONFIG_CID)));
#pragma warning restore CA1416
		}
	}
}