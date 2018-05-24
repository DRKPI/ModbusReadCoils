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

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            try
            {
                byte slaveAddress = 32;// Slave Address
                byte functionCode = 03;// Function
                ushort startAddress = 40001;// Starting Address
                ushort numberOfPoints = 2;// Quantity of Registers


                // Build Message(FC01)

                byte[] frame = new byte[8];// total 8 bytes
                frame[0] = slaveAddress;// Slave Address
                frame[1] = functionCode;// Function
                frame[2] = (byte)(startAddress >> 8);// Starting Address High
                frame[3] = (byte)startAddress;// Starting Address Low
                frame[4] = (byte)(numberOfPoints >> 8);// quantity of Registers High
                frame[5] = (byte)numberOfPoints;// quantity of Registers Low
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

                txtSendMsg.Text = sendMsg;
                txtReceiveMsg.Text = receiveMsg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
