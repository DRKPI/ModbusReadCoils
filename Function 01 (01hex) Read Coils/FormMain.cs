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

namespace Function_01__01hex__Read_Coils
{
    public partial class FormMain : Form
    {
        private SerialPort sp = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnRequestMsg_Click(object sender, EventArgs e)
        {
            try
            {
                byte slaveAddress = 04;// Slave Address - this can be found on the controller menu under Network
                byte functionCode = 03;// Function code - 03 is for reading coils/registers
                ushort startAddress = 40001;// Starting Address - register 40001 holds the turbidity reading as a float
                ushort numberOfRegisters = 2;// Quantity of Registers to read


                // Build Message(FC03)

                byte[] frame = new byte[8];// total 8 bytes
                frame[0] = slaveAddress;// Slave Address
                frame[1] = functionCode;// Function
                frame[2] = (byte)(startAddress >> 8);// Starting Address High
                frame[3] = (byte)startAddress;// Starting Address Low
                frame[4] = (byte)(numberOfRegisters >> 8);// quantity of Registers High
                frame[5] = (byte)numberOfRegisters;// quantity of Registers Low
                byte[] checkSum = CRC16(frame);// call funtion CRC Calculate
                frame[6] = checkSum[0];// error check Low
                frame[7] = checkSum[1];// error check High
                sp.Write(frame, 0, frame.Length);// send frame
                Thread.Sleep(TimeSpan.FromSeconds(2));
                byte[] buffRec = new byte[sp.BytesToRead];
                int numberOfBytes = sp.Read(buffRec, 0, buffRec.Length);

                string sendMsg = string.Empty;
                string receiveMsg = string.Empty;

                //Send string
                foreach(var item in frame)
                {
                    sendMsg += string.Format("{0:X2} ", item);
                }

                //Receive string
                foreach (var item in buffRec)
                {
                    receiveMsg += string.Format("{0:X2} ", item);
                }

                txtRequestMsg.Text = sendMsg;
                txtReceivedMsg.Text = receiveMsg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //TODO close serial port
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
                sp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //TODO close serial port
            }
        }

        /// <summary>
        /// CRC16
        /// </summary>
        /// <param name="data">byte[]</param>
        /// <returns>byte[] of type checkSum</returns>
        private static byte[] CRC16(byte[] data)
        {
            byte[] checkSum = new byte[2];
            ushort reg_crc = 0Xffff;

            for (int i = 0; i < data.Length - 2; i++)
            {
                reg_crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((reg_crc & 0x01) == 1)
                    {
                        reg_crc = (ushort)((reg_crc >> 1) ^ 0xA001);
                    }
                    else
                    {
                        reg_crc = (ushort)(reg_crc >> 1);
                    }
                }
            }
            checkSum[1] = (byte)((reg_crc >> 8) & 0xFF);
            checkSum[0] = (byte)(reg_crc & 0xFF);

            return checkSum;
        }// end Function checkSum
    }
}
