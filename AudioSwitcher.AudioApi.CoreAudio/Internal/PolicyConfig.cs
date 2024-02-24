using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using System.Runtime.InteropServices;

namespace AudioSwitcher.AudioApi.CoreAudio
{
	internal static class PolicyConfig
	{
		public static void SetDefaultEndpoint(string devId, ERole eRole)
		{
			object policyConfig = null;
			try
			{
				policyConfig = ComObjectFactory.GetPolicyConfig();

				switch (policyConfig)
				{
					case IPolicyConfigRedstone3 config:
						config.SetDefaultEndpoint(devId, eRole);
						break;

					case IPolicyConfigRedstone2 config:
						config.SetDefaultEndpoint(devId, eRole);
						break;

					case IPolicyConfigRedstone config:
						config.SetDefaultEndpoint(devId, eRole);
						break;

					case IPolicyConfigX config:
						config.SetDefaultEndpoint(devId, eRole);
						break;

					case IPolicyConfig config:
						config.SetDefaultEndpoint(devId, eRole);
						break;

					case IPolicyConfigVista config:
						config.SetDefaultEndpoint(devId, eRole);
						break;

					case IPolicyConfigUnknown config:
						config.SetDefaultEndpoint(devId, eRole);
						break;
				}
			}
			finally
			{
				if (policyConfig != null && Marshal.IsComObject(policyConfig))
#pragma warning disable CA1416
					Marshal.FinalReleaseComObject(policyConfig);
#pragma warning restore CA1416
			}
		}
	}
}