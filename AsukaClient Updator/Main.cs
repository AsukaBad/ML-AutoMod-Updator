using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MelonLoader;

namespace AsukaClient
{
    public class Updator : MelonPlugin
    {
        public bool NoDl = false;


        public override void OnApplicationStart()
        {
            NoDl = Environment.CommandLine.Contains("--no-download");

            if (NoDl) return;

            if (!File.Exists("Mods/AsukaClient.dll"))
            {
                MelonLogger.Msg("Hi ! welcome to AsukaClient");
            }
            if (!File.Exists("AsukaClient"))
            {
                Directory.CreateDirectory("AsukaClient");
            }
            if (!File.Exists("AsukaClient/Dependencies"))
            {
                Directory.CreateDirectory("AsukaClient/Dependencies");
            }
            if (!File.Exists("AsukaClient/License"))
            {
                Directory.CreateDirectory("AsukaClient/License");
            }

            DownloadFromStorage("AsukaClient", "Storage", true, true);

        }

        private void DownloadFromStorage(string fileName, string version, bool n, bool nowrite)
        {
            using var sha256 = SHA256.Create();

            byte[] bytes = null;

            byte[] bytestrage = null;

            if (File.Exists($"Mods/{fileName}.dll"))
            {
                bytestrage = File.ReadAllBytes($"Mods/{fileName}.dll");

            }

            using var wc = new WebClient
            {
                Headers =
                {
                    ["User-Agent"] =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0"
                }
            };

            try
            {
                bytes = wc.DownloadData($"https://storage.googleapis.com/asuka-website/{fileName}.dll");
                File.WriteAllBytes($"Mods/{fileName}.dll", bytes);

            }
            catch (WebException e)
            {
                MelonLogger.Error($"Unable to download latest version of {fileName}: {e}");
            }
        }
    }
}
