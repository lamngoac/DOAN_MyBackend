namespace ZTest01
{
	partial class ZTest
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
			this.btnTestMix = new System.Windows.Forms.Button();
			this.btnTestWSBank = new System.Windows.Forms.Button();
			this.btnTVANKQuaGDich = new System.Windows.Forms.Button();
			this.btnMyTest = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnTestMix
			// 
			this.btnTestMix.Location = new System.Drawing.Point(45, 66);
			this.btnTestMix.Margin = new System.Windows.Forms.Padding(4);
			this.btnTestMix.Name = "btnTestMix";
			this.btnTestMix.Size = new System.Drawing.Size(292, 94);
			this.btnTestMix.TabIndex = 0;
			this.btnTestMix.Text = "TestMix";
			this.btnTestMix.UseVisualStyleBackColor = true;
			this.btnTestMix.Click += new System.EventHandler(this.btnTestMix_Click);
			// 
			// btnTestWSBank
			// 
			this.btnTestWSBank.Location = new System.Drawing.Point(435, 66);
			this.btnTestWSBank.Margin = new System.Windows.Forms.Padding(4);
			this.btnTestWSBank.Name = "btnTestWSBank";
			this.btnTestWSBank.Size = new System.Drawing.Size(284, 94);
			this.btnTestWSBank.TabIndex = 1;
			this.btnTestWSBank.Text = "TestWSBank";
			this.btnTestWSBank.UseVisualStyleBackColor = true;
			this.btnTestWSBank.Click += new System.EventHandler(this.btnTestWSBank_Click);
			// 
			// btnTVANKQuaGDich
			// 
			this.btnTVANKQuaGDich.Location = new System.Drawing.Point(45, 202);
			this.btnTVANKQuaGDich.Margin = new System.Windows.Forms.Padding(4);
			this.btnTVANKQuaGDich.Name = "btnTVANKQuaGDich";
			this.btnTVANKQuaGDich.Size = new System.Drawing.Size(284, 94);
			this.btnTVANKQuaGDich.TabIndex = 2;
			this.btnTVANKQuaGDich.Text = "TestTVANKQuaGDich";
			this.btnTVANKQuaGDich.UseVisualStyleBackColor = true;
			this.btnTVANKQuaGDich.Click += new System.EventHandler(this.btnTVANKQuaGDich_Click);
			// 
			// btnMyTest
			// 
			this.btnMyTest.Location = new System.Drawing.Point(435, 202);
			this.btnMyTest.Margin = new System.Windows.Forms.Padding(4);
			this.btnMyTest.Name = "btnMyTest";
			this.btnMyTest.Size = new System.Drawing.Size(292, 94);
			this.btnMyTest.TabIndex = 3;
			this.btnMyTest.Text = "MyTest";
			this.btnMyTest.UseVisualStyleBackColor = true;
			this.btnMyTest.Click += new System.EventHandler(this.btnMyTest_Click);
			// 
			// ZTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(835, 423);
			this.Controls.Add(this.btnMyTest);
			this.Controls.Add(this.btnTVANKQuaGDich);
			this.Controls.Add(this.btnTestWSBank);
			this.Controls.Add(this.btnTestMix);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ZTest";
			this.Text = "ZTest";
			this.Load += new System.EventHandler(this.ZTest_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnTestMix;
		private System.Windows.Forms.Button btnTestWSBank;
		private System.Windows.Forms.Button btnTVANKQuaGDich;
		private System.Windows.Forms.Button btnMyTest;
	}
}

