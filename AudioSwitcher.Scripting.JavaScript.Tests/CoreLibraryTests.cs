﻿using AudioSwitcher.AudioApi;
using AudioSwitcher.Scripting.JavaScript.Internal;
using AudioSwitcher.Tests.Common;
using Jint.Native;
using Xunit;

namespace AudioSwitcher.Scripting.JavaScript.Tests
{
    public class CoreLibraryTests
    {
        public static IAudioController GetAudioController()
        {
            return new TestDeviceController(2, 2);
        }

        [Fact]
        public void Engine_AddLibrary_Core()
        {
            using (var engine = new JsEngine())
            {
                engine.AddCoreLibrary();
                engine.Execute("Core = require('AudioSwitcher');");
                Assert.NotEqual(JsValue.Undefined, engine.InternalEngine.GetValue("Core"));
            }
        }

        [Fact]
        public void Core_sleep_Exists()
        {
            using (var engine = new JsEngine())
            {
                engine.AddCoreLibrary();
                engine.Execute("Core = require('Core');");
                engine.Execute("Core.sleep(100)");
            }
        }

    }
}