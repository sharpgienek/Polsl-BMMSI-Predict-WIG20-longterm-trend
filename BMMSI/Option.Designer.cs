namespace BMMSI
{
    partial class Option
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxTrainDateStep = new System.Windows.Forms.TextBox();
            this.textBoxPatternStep = new System.Windows.Forms.TextBox();
            this.textBoxMaxPattern = new System.Windows.Forms.TextBox();
            this.textBoxMinPattern = new System.Windows.Forms.TextBox();
            this.textBoxTrainingPeriodStep = new System.Windows.Forms.TextBox();
            this.textBoxTrainingMaxPeriod = new System.Windows.Forms.TextBox();
            this.textBoxTrainingMinPeriod = new System.Windows.Forms.TextBox();
            this.textBoxTrainigPath = new System.Windows.Forms.TextBox();
            this.dateTimePickerTrain = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxTestDateStep = new System.Windows.Forms.TextBox();
            this.textBoxDesiredPatNo = new System.Windows.Forms.TextBox();
            this.textBoxTestPeriodStep = new System.Windows.Forms.TextBox();
            this.textBoxTestMaxPeriodNo = new System.Windows.Forms.TextBox();
            this.textBoxTestMinPeriodNo = new System.Windows.Forms.TextBox();
            this.textBoxTestPath = new System.Windows.Forms.TextBox();
            this.dateTimePickerTest = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxMSE = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxHiddenStep = new System.Windows.Forms.TextBox();
            this.textBoxMaxHidden = new System.Windows.Forms.TextBox();
            this.textBoxMinHidden = new System.Windows.Forms.TextBox();
            this.textBoxEpochsStep = new System.Windows.Forms.TextBox();
            this.textBoxMaxEpochs = new System.Windows.Forms.TextBox();
            this.textBoxMinEpochs = new System.Windows.Forms.TextBox();
            this.textBoxNetPath = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelCreateNetwork = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(162, 266);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 24);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Create network";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(280, 266);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 24);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(281, 241);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxTrainDateStep);
            this.tabPage1.Controls.Add(this.textBoxPatternStep);
            this.tabPage1.Controls.Add(this.textBoxMaxPattern);
            this.tabPage1.Controls.Add(this.textBoxMinPattern);
            this.tabPage1.Controls.Add(this.textBoxTrainingPeriodStep);
            this.tabPage1.Controls.Add(this.textBoxTrainingMaxPeriod);
            this.tabPage1.Controls.Add(this.textBoxTrainingMinPeriod);
            this.tabPage1.Controls.Add(this.textBoxTrainigPath);
            this.tabPage1.Controls.Add(this.dateTimePickerTrain);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(273, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Trainig";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxTrainDateStep
            // 
            this.textBoxTrainDateStep.Location = new System.Drawing.Point(184, 190);
            this.textBoxTrainDateStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTrainDateStep.Name = "textBoxTrainDateStep";
            this.textBoxTrainDateStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxTrainDateStep.TabIndex = 25;
            // 
            // textBoxPatternStep
            // 
            this.textBoxPatternStep.Location = new System.Drawing.Point(184, 145);
            this.textBoxPatternStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPatternStep.Name = "textBoxPatternStep";
            this.textBoxPatternStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxPatternStep.TabIndex = 24;
            this.textBoxPatternStep.TextChanged += new System.EventHandler(this.textBoxPatternStep_TextChanged);
            // 
            // textBoxMaxPattern
            // 
            this.textBoxMaxPattern.Location = new System.Drawing.Point(184, 122);
            this.textBoxMaxPattern.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMaxPattern.Name = "textBoxMaxPattern";
            this.textBoxMaxPattern.Size = new System.Drawing.Size(76, 20);
            this.textBoxMaxPattern.TabIndex = 23;
            this.textBoxMaxPattern.TextChanged += new System.EventHandler(this.textBoxMaxPattern_TextChanged);
            // 
            // textBoxMinPattern
            // 
            this.textBoxMinPattern.Location = new System.Drawing.Point(184, 100);
            this.textBoxMinPattern.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMinPattern.Name = "textBoxMinPattern";
            this.textBoxMinPattern.Size = new System.Drawing.Size(76, 20);
            this.textBoxMinPattern.TabIndex = 22;
            this.textBoxMinPattern.TextChanged += new System.EventHandler(this.textBoxMinPattern_TextChanged);
            // 
            // textBoxTrainingPeriodStep
            // 
            this.textBoxTrainingPeriodStep.Location = new System.Drawing.Point(184, 79);
            this.textBoxTrainingPeriodStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTrainingPeriodStep.Name = "textBoxTrainingPeriodStep";
            this.textBoxTrainingPeriodStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxTrainingPeriodStep.TabIndex = 21;
            this.textBoxTrainingPeriodStep.TextChanged += new System.EventHandler(this.textBoxTrainingPeriodStep_TextChanged);
            // 
            // textBoxTrainingMaxPeriod
            // 
            this.textBoxTrainingMaxPeriod.Location = new System.Drawing.Point(184, 57);
            this.textBoxTrainingMaxPeriod.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTrainingMaxPeriod.Name = "textBoxTrainingMaxPeriod";
            this.textBoxTrainingMaxPeriod.Size = new System.Drawing.Size(76, 20);
            this.textBoxTrainingMaxPeriod.TabIndex = 20;
            this.textBoxTrainingMaxPeriod.TextChanged += new System.EventHandler(this.textBoxTrainingMaxPeriod_TextChanged);
            // 
            // textBoxTrainingMinPeriod
            // 
            this.textBoxTrainingMinPeriod.Location = new System.Drawing.Point(184, 36);
            this.textBoxTrainingMinPeriod.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTrainingMinPeriod.Name = "textBoxTrainingMinPeriod";
            this.textBoxTrainingMinPeriod.Size = new System.Drawing.Size(76, 20);
            this.textBoxTrainingMinPeriod.TabIndex = 19;
            this.textBoxTrainingMinPeriod.TextChanged += new System.EventHandler(this.textBoxTrainingMinPeriod_TextChanged);
            // 
            // textBoxTrainigPath
            // 
            this.textBoxTrainigPath.Location = new System.Drawing.Point(184, 14);
            this.textBoxTrainigPath.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTrainigPath.Name = "textBoxTrainigPath";
            this.textBoxTrainigPath.Size = new System.Drawing.Size(76, 20);
            this.textBoxTrainigPath.TabIndex = 18;
            // 
            // dateTimePickerTrain
            // 
            this.dateTimePickerTrain.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTrain.Location = new System.Drawing.Point(184, 167);
            this.dateTimePickerTrain.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerTrain.Name = "dateTimePickerTrain";
            this.dateTimePickerTrain.Size = new System.Drawing.Size(76, 20);
            this.dateTimePickerTrain.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 194);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Date step";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 172);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Training start date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 149);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Pattern step";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Maksimum training patterns";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Minimum training patterns";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Period step";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maksimum period number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimal period number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder path";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxTestDateStep);
            this.tabPage2.Controls.Add(this.textBoxDesiredPatNo);
            this.tabPage2.Controls.Add(this.textBoxTestPeriodStep);
            this.tabPage2.Controls.Add(this.textBoxTestMaxPeriodNo);
            this.tabPage2.Controls.Add(this.textBoxTestMinPeriodNo);
            this.tabPage2.Controls.Add(this.textBoxTestPath);
            this.tabPage2.Controls.Add(this.dateTimePickerTest);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(273, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Test data ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxTestDateStep
            // 
            this.textBoxTestDateStep.Location = new System.Drawing.Point(179, 146);
            this.textBoxTestDateStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTestDateStep.Name = "textBoxTestDateStep";
            this.textBoxTestDateStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxTestDateStep.TabIndex = 41;
            // 
            // textBoxDesiredPatNo
            // 
            this.textBoxDesiredPatNo.Location = new System.Drawing.Point(179, 98);
            this.textBoxDesiredPatNo.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDesiredPatNo.Name = "textBoxDesiredPatNo";
            this.textBoxDesiredPatNo.Size = new System.Drawing.Size(76, 20);
            this.textBoxDesiredPatNo.TabIndex = 40;
            // 
            // textBoxTestPeriodStep
            // 
            this.textBoxTestPeriodStep.Location = new System.Drawing.Point(179, 77);
            this.textBoxTestPeriodStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTestPeriodStep.Name = "textBoxTestPeriodStep";
            this.textBoxTestPeriodStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxTestPeriodStep.TabIndex = 39;
            // 
            // textBoxTestMaxPeriodNo
            // 
            this.textBoxTestMaxPeriodNo.Location = new System.Drawing.Point(179, 55);
            this.textBoxTestMaxPeriodNo.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTestMaxPeriodNo.Name = "textBoxTestMaxPeriodNo";
            this.textBoxTestMaxPeriodNo.Size = new System.Drawing.Size(76, 20);
            this.textBoxTestMaxPeriodNo.TabIndex = 38;
            // 
            // textBoxTestMinPeriodNo
            // 
            this.textBoxTestMinPeriodNo.Location = new System.Drawing.Point(179, 34);
            this.textBoxTestMinPeriodNo.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTestMinPeriodNo.Name = "textBoxTestMinPeriodNo";
            this.textBoxTestMinPeriodNo.Size = new System.Drawing.Size(76, 20);
            this.textBoxTestMinPeriodNo.TabIndex = 37;
            // 
            // textBoxTestPath
            // 
            this.textBoxTestPath.Location = new System.Drawing.Point(179, 12);
            this.textBoxTestPath.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTestPath.Name = "textBoxTestPath";
            this.textBoxTestPath.Size = new System.Drawing.Size(76, 20);
            this.textBoxTestPath.TabIndex = 36;
            // 
            // dateTimePickerTest
            // 
            this.dateTimePickerTest.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTest.Location = new System.Drawing.Point(179, 124);
            this.dateTimePickerTest.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerTest.Name = "dateTimePickerTest";
            this.dateTimePickerTest.Size = new System.Drawing.Size(76, 20);
            this.dateTimePickerTest.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 150);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Date step";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 128);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Training start date";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 102);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(122, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "Desired patterns number";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 81);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Period step";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(21, 59);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(127, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Maksimum period number";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 38);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "Minimal period number";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 16);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Folder path";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBoxMSE);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.textBoxHiddenStep);
            this.tabPage3.Controls.Add(this.textBoxMaxHidden);
            this.tabPage3.Controls.Add(this.textBoxMinHidden);
            this.tabPage3.Controls.Add(this.textBoxEpochsStep);
            this.tabPage3.Controls.Add(this.textBoxMaxEpochs);
            this.tabPage3.Controls.Add(this.textBoxMinEpochs);
            this.tabPage3.Controls.Add(this.textBoxNetPath);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Controls.Add(this.label25);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(273, 215);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Neural net";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBoxMSE
            // 
            this.textBoxMSE.Location = new System.Drawing.Point(179, 170);
            this.textBoxMSE.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMSE.Name = "textBoxMSE";
            this.textBoxMSE.Size = new System.Drawing.Size(76, 20);
            this.textBoxMSE.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 172);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Desired MSE";
            // 
            // textBoxHiddenStep
            // 
            this.textBoxHiddenStep.Location = new System.Drawing.Point(179, 144);
            this.textBoxHiddenStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxHiddenStep.Name = "textBoxHiddenStep";
            this.textBoxHiddenStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxHiddenStep.TabIndex = 42;
            this.textBoxHiddenStep.TextChanged += new System.EventHandler(this.textBoxHiddenStep_TextChanged);
            // 
            // textBoxMaxHidden
            // 
            this.textBoxMaxHidden.Location = new System.Drawing.Point(179, 120);
            this.textBoxMaxHidden.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMaxHidden.Name = "textBoxMaxHidden";
            this.textBoxMaxHidden.Size = new System.Drawing.Size(76, 20);
            this.textBoxMaxHidden.TabIndex = 41;
            this.textBoxMaxHidden.TextChanged += new System.EventHandler(this.textBoxMaxHidden_TextChanged);
            // 
            // textBoxMinHidden
            // 
            this.textBoxMinHidden.Location = new System.Drawing.Point(179, 99);
            this.textBoxMinHidden.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMinHidden.Name = "textBoxMinHidden";
            this.textBoxMinHidden.Size = new System.Drawing.Size(76, 20);
            this.textBoxMinHidden.TabIndex = 40;
            this.textBoxMinHidden.TextChanged += new System.EventHandler(this.textBoxMinHidden_TextChanged);
            // 
            // textBoxEpochsStep
            // 
            this.textBoxEpochsStep.Location = new System.Drawing.Point(179, 77);
            this.textBoxEpochsStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEpochsStep.Name = "textBoxEpochsStep";
            this.textBoxEpochsStep.Size = new System.Drawing.Size(76, 20);
            this.textBoxEpochsStep.TabIndex = 39;
            this.textBoxEpochsStep.TextChanged += new System.EventHandler(this.textBoxEpochsStep_TextChanged);
            // 
            // textBoxMaxEpochs
            // 
            this.textBoxMaxEpochs.Location = new System.Drawing.Point(179, 56);
            this.textBoxMaxEpochs.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMaxEpochs.Name = "textBoxMaxEpochs";
            this.textBoxMaxEpochs.Size = new System.Drawing.Size(76, 20);
            this.textBoxMaxEpochs.TabIndex = 38;
            this.textBoxMaxEpochs.TextChanged += new System.EventHandler(this.textBoxMaxEpochs_TextChanged);
            // 
            // textBoxMinEpochs
            // 
            this.textBoxMinEpochs.Location = new System.Drawing.Point(179, 34);
            this.textBoxMinEpochs.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMinEpochs.Name = "textBoxMinEpochs";
            this.textBoxMinEpochs.Size = new System.Drawing.Size(76, 20);
            this.textBoxMinEpochs.TabIndex = 37;
            this.textBoxMinEpochs.TextChanged += new System.EventHandler(this.textBoxMinEpochs_TextChanged);
            // 
            // textBoxNetPath
            // 
            this.textBoxNetPath.Location = new System.Drawing.Point(179, 12);
            this.textBoxNetPath.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNetPath.Name = "textBoxNetPath";
            this.textBoxNetPath.Size = new System.Drawing.Size(76, 20);
            this.textBoxNetPath.TabIndex = 36;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 148);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(132, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "Hidden layer multiplier step";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 125);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(163, 13);
            this.label20.TabIndex = 24;
            this.label20.Text = "Maksimum hidden layer multiplier ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(4, 103);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(154, 13);
            this.label21.TabIndex = 23;
            this.label21.Text = "Minimum hidden layer multiplier ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(4, 82);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(109, 13);
            this.label22.TabIndex = 22;
            this.label22.Text = "Epochs step multiplier";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(4, 60);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(133, 13);
            this.label23.TabIndex = 21;
            this.label23.Text = "Maksimum epochs number";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(4, 39);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(118, 13);
            this.label24.TabIndex = 20;
            this.label24.Text = "Minimal epochs number";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 17);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 13);
            this.label25.TabIndex = 19;
            this.label25.Text = "Folder path";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 272);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Network to create";
            // 
            // labelCreateNetwork
            // 
            this.labelCreateNetwork.AutoSize = true;
            this.labelCreateNetwork.Location = new System.Drawing.Point(103, 272);
            this.labelCreateNetwork.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreateNetwork.Name = "labelCreateNetwork";
            this.labelCreateNetwork.Size = new System.Drawing.Size(0, 13);
            this.labelCreateNetwork.TabIndex = 4;
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 308);
            this.Controls.Add(this.labelCreateNetwork);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Option";
            this.Text = "Option";
            this.Shown += new System.EventHandler(this.Option_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTrainigPath;
        private System.Windows.Forms.DateTimePicker dateTimePickerTrain;
        private System.Windows.Forms.DateTimePicker dateTimePickerTest;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxNetPath;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBoxTrainDateStep;
        private System.Windows.Forms.TextBox textBoxPatternStep;
        private System.Windows.Forms.TextBox textBoxMaxPattern;
        private System.Windows.Forms.TextBox textBoxMinPattern;
        private System.Windows.Forms.TextBox textBoxTrainingPeriodStep;
        private System.Windows.Forms.TextBox textBoxTrainingMaxPeriod;
        private System.Windows.Forms.TextBox textBoxTrainingMinPeriod;
        private System.Windows.Forms.TextBox textBoxTestDateStep;
        private System.Windows.Forms.TextBox textBoxDesiredPatNo;
        private System.Windows.Forms.TextBox textBoxTestPeriodStep;
        private System.Windows.Forms.TextBox textBoxTestMaxPeriodNo;
        private System.Windows.Forms.TextBox textBoxTestMinPeriodNo;
        private System.Windows.Forms.TextBox textBoxHiddenStep;
        private System.Windows.Forms.TextBox textBoxMaxHidden;
        private System.Windows.Forms.TextBox textBoxMinHidden;
        private System.Windows.Forms.TextBox textBoxEpochsStep;
        private System.Windows.Forms.TextBox textBoxMaxEpochs;
        private System.Windows.Forms.TextBox textBoxMinEpochs;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxTestPath;
        private System.Windows.Forms.TextBox textBoxMSE;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelCreateNetwork;
    }
}