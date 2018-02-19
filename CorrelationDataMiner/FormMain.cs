﻿using CorrelationDataMiner.bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CorrelationDataMiner
{
    public partial class FormMain : Form
    {
        // Global Variables
        string correlationFile, signalOneFile, signalTwoFile;
        List<Frame> framesList;
        List<Interval> intervalsList;

        public FormMain()
        {
            InitializeComponent();

            // Initialize global variables
            correlationFile = "";
            signalOneFile = "";
            signalTwoFile = "";
            framesList = new List<Frame>();
            intervalsList = new List<Interval>();
        }

        private void btnBrowseCorr_Click(object sender, EventArgs e)
        {
            // Create OpenFileDialog object to select correlation signal file
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Select Correlation File...",
                Filter = "TXT Files|*.txt",
                InitialDirectory = @"C:\"
            };

            // Display fileDialog and check if user selected a file
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Save selected file's path
                correlationFile = fileDialog.FileName;

                // Display file path in read-only textbox
                tbCorrPath.Text = correlationFile;
            }
        }

        private void btnBrowseSig1_Click(object sender, EventArgs e)
        {
            // Create OpenFileDialog object to select signal one file
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Select Signal 1 File...",
                Filter = "TXT Files|*.txt",
                InitialDirectory = @"C:\"
            };

            // Display fileDialog and check if user selected a file
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Save selected file's path
                signalOneFile = fileDialog.FileName;

                // Display file path in read-only textbox
                tbSig1Path.Text = signalOneFile;
            }
        }

        private void btnBrowseSig2_Click(object sender, EventArgs e)
        {
            // Create OpenFileDialog object to select signal two file
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Select Signal 2 File...",
                Filter = "TXT Files|*.txt",
                InitialDirectory = @"C:\"
            };

            // Display fileDialog and check if user selected a file
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Save selected file's path
                signalTwoFile = fileDialog.FileName;

                // Display file path in read-only textbox
                tbSig2Path.Text = signalTwoFile;
            }
        }

        private void btnCalculateIntervals_Click(object sender, EventArgs e)
        {
            ReadFiles();
        }

        // Read and store files into global list of Frame objects
        private void ReadFiles()
        {
            ReadCorrelationFile();
            ReadSignalOneFile();
            ReadSignalTwoFile();
        }

        // Read correlation signal file and store into newly created Frame object
        private void ReadCorrelationFile()
        {
            // Open StreamReader object for correlation file
            using (StreamReader reader = new StreamReader(correlationFile))
            {
                string line = "";
                int position = 0;

                // Loop through all lines that are not read as null
                while ((line = reader.ReadLine()) != null)
                {
                    // Increment position variable
                    position++;

                    // Add new frame object to global list, saving the frame's position and correlation signal
                    framesList.Add(new Frame() { Position = position, CorrSignal = Convert.ToDouble(line) });
                }
            }
        }

        // Read signal one file and store into frame object at appropriate position
        private void ReadSignalOneFile()
        {
            // Open StreamReader object for signal one file
            using (StreamReader reader = new StreamReader(signalOneFile))
            {
                string line = "";
                int position = 0;

                // Loop through all lines that are not read as null
                while ((line = reader.ReadLine()) != null)
                {
                    // Use position variable as index to find corresponding frame and store signal one value
                    framesList[position].SigOne = Convert.ToDouble(line);

                    // Increment position variable
                    position++;
                }
            }
        }

        // Read signal two file and store into frame object at appropriate position
        private void ReadSignalTwoFile()
        {
            // Open StreamReader object for signal two file
            using (StreamReader reader = new StreamReader(signalTwoFile))
            {
                string line = "";
                int position = 0;

                // Loop through all lines that are not read as null
                while ((line = reader.ReadLine()) != null)
                {
                    // Use position variable as index to find corresponding frame and store signal two value
                    framesList[position].SigTwo = Convert.ToDouble(line);

                    // Increment position variable
                    position++;
                }
            }
        }

    }
}
