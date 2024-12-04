namespace Plugin.sqlCompiler.UI
{
	partial class ParameterDlg
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
			if(disposing && (components != null))
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label3;
			this.bnCancel = new System.Windows.Forms.Button();
			this.bnOk = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.ddlType = new System.Windows.Forms.ComboBox();
			this.error = new System.Windows.Forms.ErrorProvider(this.components);
			this.cbNullable = new System.Windows.Forms.CheckBox();
			this.txtDefault = new System.Windows.Forms.TextBox();
			this.udSize = new System.Windows.Forms.NumericUpDown();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSize)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(13, 40);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(34, 13);
			label2.TabIndex = 2;
			label2.Text = "&Type:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(13, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(38, 13);
			label1.TabIndex = 0;
			label1.Text = "&Name:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(13, 65);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(44, 13);
			label3.TabIndex = 5;
			label3.Text = "&Default:";
			// 
			// bnCancel
			// 
			this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(182, 88);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(75, 23);
			this.bnCancel.TabIndex = 9;
			this.bnCancel.Text = "&Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			// 
			// bnOk
			// 
			this.bnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bnOk.Location = new System.Drawing.Point(101, 88);
			this.bnOk.Name = "bnOk";
			this.bnOk.Size = new System.Drawing.Size(75, 23);
			this.bnOk.TabIndex = 8;
			this.bnOk.Text = "&OK";
			this.bnOk.UseVisualStyleBackColor = true;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(57, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(200, 20);
			this.txtName.TabIndex = 1;
			// 
			// ddlType
			// 
			this.ddlType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.ddlType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ddlType.DisplayMember = "Text";
			this.ddlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlType.FormattingEnabled = true;
			this.ddlType.Location = new System.Drawing.Point(57, 37);
			this.ddlType.Name = "ddlType";
			this.ddlType.Size = new System.Drawing.Size(137, 21);
			this.ddlType.TabIndex = 3;
			this.ddlType.ValueMember = "Tag";
			// 
			// error
			// 
			this.error.ContainerControl = this;
			// 
			// cbNullable
			// 
			this.cbNullable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbNullable.AutoSize = true;
			this.cbNullable.Location = new System.Drawing.Point(16, 92);
			this.cbNullable.Name = "cbNullable";
			this.cbNullable.Size = new System.Drawing.Size(64, 17);
			this.cbNullable.TabIndex = 7;
			this.cbNullable.Text = "&Nullable";
			this.cbNullable.UseVisualStyleBackColor = true;
			// 
			// txtDefault
			// 
			this.txtDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDefault.Location = new System.Drawing.Point(57, 62);
			this.txtDefault.Name = "txtDefault";
			this.txtDefault.Size = new System.Drawing.Size(200, 20);
			this.txtDefault.TabIndex = 6;
			// 
			// udSize
			// 
			this.udSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.udSize.Location = new System.Drawing.Point(200, 38);
			this.udSize.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
			this.udSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.udSize.Name = "udSize";
			this.udSize.Size = new System.Drawing.Size(57, 20);
			this.udSize.TabIndex = 4;
			this.udSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			// 
			// ParameterDlg
			// 
			this.AcceptButton = this.bnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(269, 119);
			this.Controls.Add(this.udSize);
			this.Controls.Add(this.txtDefault);
			this.Controls.Add(label3);
			this.Controls.Add(this.cbNullable);
			this.Controls.Add(label2);
			this.Controls.Add(label1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.ddlType);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOk);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(190, 134);
			this.Name = "ParameterDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Parameter";
			((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bnCancel;
		private System.Windows.Forms.Button bnOk;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ComboBox ddlType;
		private System.Windows.Forms.ErrorProvider error;
		private System.Windows.Forms.TextBox txtDefault;
		private System.Windows.Forms.CheckBox cbNullable;
		private System.Windows.Forms.NumericUpDown udSize;
	}
}