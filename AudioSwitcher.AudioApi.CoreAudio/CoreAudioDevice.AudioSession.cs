﻿using System;
using System.Runtime.InteropServices;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;
using AudioSwitcher.AudioApi.Session;

namespace AudioSwitcher.AudioApi.CoreAudio
{
    public partial class CoreAudioDevice : IAudioSessionEndpoint
    {

        public bool IsSupported
        {
            get;
            private set;
        }

        public IAudioSessionController SessionController
        {
            get;
            private set;
        }

        private void ClearAudioSession()
        {
            if (SessionController != null)
            {
                SessionController.Dispose();
                SessionController = null;
            }
        }

        private void LoadAudioSessionController(IMultimediaDevice device)
        {
            //This should be all on the COM thread to avoid any
            //weird lookups on the result COM object not on an STA Thread
            ComThread.Assert();

            //Need to catch here, as there is a chance that unauthorized is thrown.
            //It's not an HR exception, but bubbles up through the .net call stack
            try
            {
                var clsGuid = new Guid(ComIIds.AUDIO_SESSION_MANAGER2_IID);
                object result;
                Marshal.GetExceptionForHR(device.Activate(ref clsGuid, ClsCtx.Inproc, IntPtr.Zero, out result));

                //This is scoped into the managed object, so disposal is taken care of there.
                var audioSessionManager = result as IAudioSessionManager2;

                if (audioSessionManager != null)
                {
                    SessionController = new CoreAudioSessionController(audioSessionManager);
                    IsSupported = true;
                }
            }
            catch (Exception)
            {
                IsSupported = false;
            }
        }
    }
}