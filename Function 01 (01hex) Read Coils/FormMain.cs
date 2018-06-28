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
        TurbidityPacket turbidity = new TurbidityPacket();

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
            turbidity.BuildMessage();

            turbidity.WriteToSP(turbidity.message);
            //Error message
            if (!String.IsNullOrEmpty(turbidity.errorMessage))
            {
                MessageBox.Show("Error sending message to Turbidity meter. \n See log file for details.", "Error Message", MessageBoxButtons.OK);
            }
            Thread.Sleep(TimeSpan.FromSeconds(2));
            turbidity.ReadFromSP();
            //Error message
            if (!String.IsNullOrEmpty(turbidity.errorMessage))
            {
                MessageBox.Show("Error receiving message from Turbidity meter. \n See log file for details.", "Error Message", MessageBoxButtons.OK);
            }

            //Print to screen and save to a .csv file
            //    turbidity.PrintModbusMessage(turbidity.message, turbidity.buffRec);
            //These are displayed to help with testing and trouble shooting
            //Test
            //    txtRequestMsg.Text = turbidity.sendMsg;
            txtReceivedMsg.Text = turbidity.turbidNum;
            //Test

        }// end Function btnRequestMsg_Click

        /// <summary>
        /// On load of form start a serial port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Open a serial port and handle any error
            turbidity.OpenSerialPort();
            //Error message
            if (!String.IsNullOrEmpty(turbidity.errorMessage))
            {
                MessageBox.Show("Serial Port Open Issue. See log file for details. ", "Error Message", MessageBoxButtons.OK);
            }
        }// end Function FormMain_Load

      
    }
}

//TODO - change error message from pop up to a hidden text box/status message,
//TODO - close and restart app after any errors
//TODO - add edit options to form (include com port, baud rate, sync time (1min - 60min)), hide edit boxes unless edit is chosen
//TODO - decide where to put the edit option (button or menu option)