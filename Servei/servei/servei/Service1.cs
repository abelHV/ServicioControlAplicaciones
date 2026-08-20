using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace servei
{
    public partial class Service : ServiceBase
    {
        private Timer _timer;
        private const string PathBase = @"C:\M0490";
        private const string BlacklistFile = "blackList.txt";
        private const string WhitelistFile = "whiteList.txt";

        public Service()
        {
            ServiceName = "ControlServiceDAM";
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(5000); // Verificación cada 5 segundos 
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
            EventLog.WriteEntry("ControlService", "Servicio Iniciado", EventLogEntryType.Information);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ProcesarBlacklist();
            ProcesarWhitelist();
        }

        private void ProcesarBlacklist()
        {
            string path = Path.Combine(PathBase, BlacklistFile);
            if (!File.Exists(path)) return;

            foreach (string line in File.ReadLines(path)) // Uso de StreamReader implícito [cite: 33]
            {
                var parts = line.Split('#'); // nombre#id#descripción [cite: 23]
                if (parts.Length < 3) continue;

                string procName = parts[0];
                int eventId = int.Parse(parts[1]);
                string desc = parts[2];

                var processes = Process.GetProcessesByName(procName);
                if (processes.Length > 0)
                {
                    foreach (var p in processes) p.Kill(); // Cierra todas las instancias [cite: 30]

                    // Registro en el visor de eventos [cite: 31, 32]
                    EventLog.WriteEntry("Blacklist", $"Tancat: {procName} - {desc}", EventLogEntryType.Warning, eventId);
                }
            }
        }

        private void ProcesarWhitelist()
        {
            string path = Path.Combine(PathBase, WhitelistFile);
            if (!File.Exists(path)) return;

            foreach (string line in File.ReadLines(path))
            {
                var parts = line.Split('#');
                if (parts.Length < 3) continue;

                string procName = parts[0];
                int eventId = int.Parse(parts[1]);
                string desc = parts[2];

                if (Process.GetProcessesByName(procName).Length == 0)
                {
                    // Si no está activo, enviamos orden por Socket al Agente [cite: 6, 14]
                    EnviarOrdenActivar(procName);
                    EventLog.WriteEntry("Whitelist", $"Activat: {procName} - {desc}", EventLogEntryType.Information, eventId);
                }
            }
        }

        private void EnviarOrdenActivar(string proceso)
        {
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 9000))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(proceso);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch {  }
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }
    }
}
