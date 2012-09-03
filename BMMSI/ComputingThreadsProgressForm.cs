using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMMSI
{
    public partial class ComputingThreadsProgressForm : Form
    {
        public ComputingThreadsProgressForm(int numberOfComputingThreads)
        {
            InitializeComponent();
            for (int i = 1; i <= numberOfComputingThreads; ++i)
            {
                ProgressBar pb = new ProgressBar() 
                {
                    Location = new Point(20, (i * 75) + 25),
                    Size = new Size(this.Size.Width - 60, 20),
                    Minimum = 0,
                    Maximum = 100,
                    
                    Name = "Computing Thread ProgressBar " + i.ToString()
                };

                Label labelProgress = new Label()
                {
                    Location = new Point(20, (i * 75) + 50),
                    Name = "Computing Thread Progress Label " + i.ToString(),
                    Size = new Size(this.Size.Width - 60, 20),
                    Text = "0 %"
                };

                Label labelTime = new Label()
                {
                    Location = new Point(20, (i * 75) + 0),
                    Name = "Computing Thread Time Label " + i.ToString(),
                    Size = new Size(this.Size.Width - 60, 20),
                };

                this.Controls.Add(labelProgress);
                this.Controls.Add(pb);
                this.Controls.Add(labelTime);
                
            }
        }

    }
}
