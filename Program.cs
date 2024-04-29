using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;


namespace AudioFixer
{
    internal class Program
    {
        public static CoreAudioController controller;
        public static CoreAudioDevice defaultOutputDevice;
        public static CoreAudioDevice defaultOutputCommDevice;
        public static CoreAudioDevice defaultInputDevice;
        public static CoreAudioDevice defaultInputCommDevice;

        static void Main(string[] args)
        {
            controller = new CoreAudioController();

            // Get current devices
            GetDefaultDevices();

            // Set devices to VoiceMeeter equivalent if not already set
            FixDefaultPlaybackDevice("VoiceMeeter Input");
            FixDefaultCommPlaybackDevice("VoiceMeeter Aux Input");
            FixDefaultCaptureDevice("VoiceMeeter Output");
            FixDefaultCommCaptureDevice("VoiceMeeter Aux Output");

            // Apply changes
            SetDefaultDevices();

            // Confirm Exit
            Console.WriteLine("\nDone, press any key to exit.");
            Console.ReadKey();
        }

        private static void SetDefaultDevices()
        {
            controller.DefaultPlaybackDevice = defaultOutputDevice;
            controller.DefaultPlaybackCommunicationsDevice = defaultOutputCommDevice;
            controller.DefaultCaptureDevice = defaultInputDevice;
            controller.DefaultCaptureCommunicationsDevice = defaultInputCommDevice;
        }

        private static void FixDefaultPlaybackDevice(string desiredDeviceName)
        {
            string deviceType = "Default Playback Device";
            if (controller.GetPlaybackDevices().Any(x => x.Name == desiredDeviceName))
            {
                if (defaultOutputDevice.Name != desiredDeviceName)
                {
                    Console.WriteLine($"Incorrect {deviceType} detected: \"{defaultOutputDevice.Name}\"");
                    defaultOutputDevice = controller.GetPlaybackDevices().First(x => x.Name == desiredDeviceName);
                    Console.WriteLine($"\tSet {deviceType} to: \"{defaultOutputDevice.Name}\"");
                }
                else
                {
                    Console.WriteLine($"{deviceType} is configured correctly.");
                }
            }
            else
                Console.WriteLine($"Error: {deviceType} does not exist: \"{desiredDeviceName}\"");
        }

        private static void FixDefaultCommPlaybackDevice(string desiredDeviceName)
        {
            string deviceType = "Default Communications Playback Device";
            if (controller.GetPlaybackDevices().Any(x => x.Name == desiredDeviceName))
            {
                if (defaultOutputCommDevice.Name != desiredDeviceName)
                {
                    Console.WriteLine($"Incorrect {deviceType} detected: \"{defaultOutputCommDevice.Name}\"");
                    defaultOutputCommDevice = controller.GetPlaybackDevices().First(x => x.Name == desiredDeviceName);
                    Console.WriteLine($"\tSet {deviceType} to: \"{defaultOutputCommDevice.Name}\"");
                }
                else
                {
                    Console.WriteLine($"{deviceType} is configured correctly.");
                }
            }
            else
                Console.WriteLine($"Error: {deviceType} does not exist: \"{desiredDeviceName}\"");
        }

        private static void FixDefaultCaptureDevice(string desiredDeviceName)
        {
            string deviceType = "Default Capture Device";
            if (controller.GetCaptureDevices().Any(x => x.Name == desiredDeviceName))
            {
                if (defaultInputDevice.Name != desiredDeviceName)
                {
                    Console.WriteLine($"Incorrect {deviceType} detected: \"{defaultInputDevice.Name}\"");
                    defaultInputDevice = controller.GetPlaybackDevices().First(x => x.Name == desiredDeviceName);
                    Console.WriteLine($"\tSet {deviceType} to: \"{defaultInputDevice.Name}\"");
                }
                else
                {
                    Console.WriteLine($"{deviceType} is configured correctly.");
                }
            }
            else
                Console.WriteLine($"Error: {deviceType} does not exist: \"{desiredDeviceName}\"");
        }

        private static void FixDefaultCommCaptureDevice(string desiredDeviceName)
        {
            string deviceType = "Default Communications Capture Device";
            if (controller.GetCaptureDevices().Any(x => x.Name == desiredDeviceName))
            {
                if (defaultInputCommDevice.Name != desiredDeviceName)
                {
                    Console.WriteLine($"Incorrect {deviceType} detected: \"{defaultInputCommDevice.Name}\"");
                    defaultInputCommDevice = controller.GetPlaybackDevices().First(x => x.Name == desiredDeviceName);
                    Console.WriteLine($"\tSet {deviceType} to: \"{defaultInputCommDevice.Name}\"");
                }
                else
                {
                    Console.WriteLine($"{deviceType} is configured correctly.");
                }
            }
            else
                Console.WriteLine($"Error: {deviceType} does not exist: \"{desiredDeviceName}\"");
        }

        private static void GetDefaultDevices()
        {
            defaultOutputDevice = controller.DefaultPlaybackDevice;
            defaultOutputCommDevice = controller.DefaultPlaybackCommunicationsDevice;
            defaultInputDevice = controller.DefaultCaptureDevice;
            defaultInputCommDevice = controller.DefaultCaptureCommunicationsDevice;
        }
    }
}
