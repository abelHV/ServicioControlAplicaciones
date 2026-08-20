using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;

namespace appServeis
{
    public partial class FrmMain : Form
    {
        private TcpListener _server;
        private Thread _listenThread;
        private bool _isRunning;

        public FrmMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            StartServer();
        }

        private void StartServer()
        {
            _isRunning = true;
            _server = new TcpListener(IPAddress.Any, 9000);
            _listenThread = new Thread(ListenForCommands);
            _listenThread.IsBackground = true; // Para que se cierre al cerrar el Form
            _listenThread.Start();
            LogMessage("Agente iniciado. Esperando órdenes del servicio...");
        }

        private void ListenForCommands()
        {
            try
            {
                _server.Start();
                while (_isRunning)
                {
                    using (TcpClient client = _server.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string processToStart = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        if (!string.IsNullOrEmpty(processToStart))
                        {
                            LogMessage($"Orden recibida: Activar {processToStart}");
                            EjecutarProceso(processToStart);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (_isRunning) LogMessage("Error de socket: " + ex.Message);
            }
        }

        private void EjecutarProceso(string nombreProceso)
        {
            try
            {
                Process.Start(nombreProceso);
                LogMessage($"Éxito: {nombreProceso} se ha puesto en marcha.");
            }
            catch (Exception ex)
            {
                LogMessage($"Error al abrir {nombreProceso}: {ex.Message}");
            }
        }

        // Método auxiliar para escribir en el ListBox desde otro hilo
        private void LogMessage(string message)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action(() => LogMessage(message)));
            }
            else
            {
                lstLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _isRunning = false;
            _server?.Stop();
            base.OnFormClosing(e);
        }

        private void btnInterruptor_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ControlServiceDAM");

            try
            {
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    btnInterruptor.Text = "Activar";
                    btnInterruptor.BackColor = Color.LightGreen;
                }
                else if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                    btnInterruptor.Text = "Parar";
                    btnInterruptor.BackColor = Color.Salmon;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al gestionar el servicio: " + ex.Message +
                    "\n¿Has ejecutado el Agente como Administrador?");
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ServiceController sc = new ServiceController("ControlServiceDAM");

                if (sc.Status == ServiceControllerStatus.Running ||
                    sc.Status == ServiceControllerStatus.StartPending)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("No se pudo detener el servicio: " + ex.Message);
            }
        }
    }
}

