namespace BMMSI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelInfo = new System.Windows.Forms.Label();
            this.dateTimePickerPredictionDate = new System.Windows.Forms.DateTimePicker();
            this.buttonPredict = new System.Windows.Forms.Button();
            this.labelPredictionDate = new System.Windows.Forms.Label();
            this.buttonFindBestNeuralNet = new System.Windows.Forms.Button();
            this.folderBrowserDialogNetwork = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(22, 197);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(312, 23);
            this.progressBar.TabIndex = 6;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(22, 179);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(25, 13);
            this.labelInfo.TabIndex = 7;
            this.labelInfo.Text = "Info";
            // 
            // dateTimePickerPredictionDate
            // 
            this.dateTimePickerPredictionDate.Location = new System.Drawing.Point(106, 62);
            this.dateTimePickerPredictionDate.Name = "dateTimePickerPredictionDate";
            this.dateTimePickerPredictionDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerPredictionDate.TabIndex = 8;
            // 
            // buttonPredict
            // 
            this.buttonPredict.Location = new System.Drawing.Point(230, 86);
            this.buttonPredict.Name = "buttonPredict";
            this.buttonPredict.Size = new System.Drawing.Size(75, 23);
            this.buttonPredict.TabIndex = 9;
            this.buttonPredict.Text = "Predict";
            this.buttonPredict.UseVisualStyleBackColor = true;
            this.buttonPredict.Click += new System.EventHandler(this.buttonPredict_Click);
            // 
            // labelPredictionDate
            // 
            this.labelPredictionDate.AutoSize = true;
            this.labelPredictionDate.Location = new System.Drawing.Point(20, 66);
            this.labelPredictionDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPredictionDate.Name = "labelPredictionDate";
            this.labelPredictionDate.Size = new System.Drawing.Size(81, 13);
            this.labelPredictionDate.TabIndex = 11;
            this.labelPredictionDate.Text = "Preditction date";
            // 
            // buttonFindBestNeuralNet
            // 
            this.buttonFindBestNeuralNet.Location = new System.Drawing.Point(224, 245);
            this.buttonFindBestNeuralNet.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFindBestNeuralNet.Name = "buttonFindBestNeuralNet";
            this.buttonFindBestNeuralNet.Size = new System.Drawing.Size(111, 23);
            this.buttonFindBestNeuralNet.TabIndex = 12;
            this.buttonFindBestNeuralNet.Text = "Find best neural net";
            this.buttonFindBestNeuralNet.UseVisualStyleBackColor = true;
            this.buttonFindBestNeuralNet.Click += new System.EventHandler(this.button1_Click);
            // 
            // folderBrowserDialogNetwork
            // 
            this.folderBrowserDialogNetwork.Description = "Pick up folder with networks";
            this.folderBrowserDialogNetwork.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 349);
            this.Controls.Add(this.buttonFindBestNeuralNet);
            this.Controls.Add(this.labelPredictionDate);
            this.Controls.Add(this.buttonPredict);
            this.Controls.Add(this.dateTimePickerPredictionDate);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.progressBar);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.DateTimePicker dateTimePickerPredictionDate;
        private System.Windows.Forms.Button buttonPredict;
        private System.Windows.Forms.Label labelPredictionDate;
        private System.Windows.Forms.Button buttonFindBestNeuralNet;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogNetwork;
    }
}

