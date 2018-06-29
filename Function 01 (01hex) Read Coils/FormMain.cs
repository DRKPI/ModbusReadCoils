using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turbidity
{
    public partial class FormMain : Form
    {
        TurbidityCommunication turbidity = new TurbidityCommunication();

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On button click build message, send and receive message, print out message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestMsg_Click(object sender, EventArgs e)
        {
            while (true)
            {
                turbidity.WriteToSP(turbidity.message);
                //Error message
                if (!String.IsNullOrEmpty(turbidity.errorMessage))
                {
                    MessageBox.Show("Error sending message to Turbidity meter." + Environment.NewLine + "See log file for details.", "Error Message", MessageBoxButtons.OK);

                }
                Thread.Sleep(TimeSpan.FromSeconds(2));
                turbidity.ReadFromSP();
                //Error message
                if (!String.IsNullOrEmpty(turbidity.errorMessage))
                {
                    MessageBox.Show("Error receiving message from Turbidity meter." + Environment.NewLine + "See log file for details.", "Error Message", MessageBoxButtons.OK);

                }

                //Print turbidity number to screen
                txtReceivedMsg.Text = turbidity.turbidNum;

                //Wait specified time span before getting turbidity reading again
                //Thread.Sleep(TimeSpan.FromMinutes(turbidity.timeInterval));
            }
        }// end Function btnRequestMsg_Click

        /// <summary>
        /// On load of form starts a serial port and 
        /// builds message to send to turbidity device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            //Call Function to build Modbus request message
            turbidity.BuildMessage();

            // Open a serial port and handle any error
            turbidity.OpenSerialPort();
            //Error message
            if (!String.IsNullOrEmpty(turbidity.errorMessage))
            {
                MessageBox.Show("Serial Port could not be opended." + Environment.NewLine +
                    "Verify the config file is written in the correct format." + Environment.NewLine +
                    " See log file for details. ", "Error Message", MessageBoxButtons.OK);
            }
        }// end Function FormMain_Load
    }
}

//TODO - change error message from pop up to a hidden text box/status message,
//TODO - close and restart app after any errors
//TODO - add edit options to form (include com port, baud rate, sync time (1min - 60min)), hide edit boxes unless edit is chosen
//TODO - decide where to put the edit option (button or menu option)