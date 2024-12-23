namespace WinFormsXml
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGenerar = new Button();
            btnLeer = new Button();
            openFileDialog1 = new OpenFileDialog();
            label1 = new Label();
            btnBuscar = new Button();
            SuspendLayout();
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(262, 352);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(108, 37);
            btnGenerar.TabIndex = 0;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // btnLeer
            // 
            btnLeer.Location = new Point(37, 352);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(101, 37);
            btnLeer.TabIndex = 1;
            btnLeer.Text = "Leér";
            btnLeer.UseVisualStyleBackColor = true;
            btnLeer.Click += btnLeer_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlLightLight;
            label1.Location = new Point(46, 62);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 2;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(144, 352);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(112, 37);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar Dato";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1121, 425);
            Controls.Add(btnBuscar);
            Controls.Add(label1);
            Controls.Add(btnLeer);
            Controls.Add(btnGenerar);
            Name = "Form1";
            Text = "XLM";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGenerar;
        private Button btnLeer;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private Button btnBuscar;
    }
}
