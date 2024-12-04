namespace Plugin.sqlCompiler
{
	partial class DocumentSqlCompiler
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.StatusStrip ssMain;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentSqlCompiler));
			this.tsslText = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbnProject = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiProjectNew = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiProjectOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiProjectSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbnCompile = new System.Windows.Forms.ToolStripSplitButton();
			this.tsmiCompile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCompileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tvMethods = new System.Windows.Forms.TreeView();
			this.cmsMethod = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiMethod = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMethodAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMethodRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMethodRename = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiParameter = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiParameterAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiParameterRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiParameterChange = new System.Windows.Forms.ToolStripMenuItem();
			this.ilReflection = new System.Windows.Forms.ImageList(this.components);
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.lvErrors = new System.Windows.Forms.ListView();
			this.colErrLine = new System.Windows.Forms.ColumnHeader();
			this.colErrNumber = new System.Windows.Forms.ColumnHeader();
			this.colErrDescription = new System.Windows.Forms.ColumnHeader();
			ssMain = new System.Windows.Forms.StatusStrip();
			ssMain.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.cmsMethod.SuspendLayout();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssMain
			// 
			ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslText});
			ssMain.Location = new System.Drawing.Point(0, 228);
			ssMain.Name = "ssMain";
			ssMain.Size = new System.Drawing.Size(250, 22);
			ssMain.TabIndex = 0;
			// 
			// tsslText
			// 
			this.tsslText.Name = "tsslText";
			this.tsslText.Size = new System.Drawing.Size(38, 17);
			this.tsslText.Text = "Ready";
			// 
			// tsMain
			// 
			this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbnProject,
            this.tsbnCompile});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(250, 25);
			this.tsMain.TabIndex = 1;
			// 
			// tsbnProject
			// 
			this.tsbnProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProjectNew,
            this.tsmiProjectOpen,
            this.tsmiProjectSave});
			this.tsbnProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbnProject.Image")));
			this.tsbnProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnProject.Name = "tsbnProject";
			this.tsbnProject.Size = new System.Drawing.Size(32, 22);
			this.tsbnProject.Text = "Create/Open project...";
			this.tsbnProject.ButtonClick += new System.EventHandler(this.tsbnProject_ButtonClick);
			this.tsbnProject.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsbnProject_DropDownItemClicked);
			// 
			// tsmiProjectNew
			// 
			this.tsmiProjectNew.Name = "tsmiProjectNew";
			this.tsmiProjectNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tsmiProjectNew.Size = new System.Drawing.Size(163, 22);
			this.tsmiProjectNew.Text = "New...";
			// 
			// tsmiProjectOpen
			// 
			this.tsmiProjectOpen.Name = "tsmiProjectOpen";
			this.tsmiProjectOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsmiProjectOpen.Size = new System.Drawing.Size(163, 22);
			this.tsmiProjectOpen.Text = "Open...";
			// 
			// tsmiProjectSave
			// 
			this.tsmiProjectSave.Enabled = false;
			this.tsmiProjectSave.Name = "tsmiProjectSave";
			this.tsmiProjectSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsmiProjectSave.Size = new System.Drawing.Size(163, 22);
			this.tsmiProjectSave.Text = "Save";
			// 
			// tsbnCompile
			// 
			this.tsbnCompile.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbnCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnCompile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCompile,
            this.tsmiCompileSave});
			this.tsbnCompile.Enabled = false;
			this.tsbnCompile.Image = ((System.Drawing.Image)(resources.GetObject("tsbnCompile.Image")));
			this.tsbnCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnCompile.Name = "tsbnCompile";
			this.tsbnCompile.Size = new System.Drawing.Size(32, 22);
			this.tsbnCompile.Text = "Compile";
			this.tsbnCompile.ButtonClick += new System.EventHandler(this.tsbnCompile_ButtonClick);
			this.tsbnCompile.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsbnCompile_DropDownItemClicked);
			// 
			// tsmiCompile
			// 
			this.tsmiCompile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
			this.tsmiCompile.Name = "tsmiCompile";
			this.tsmiCompile.Size = new System.Drawing.Size(138, 22);
			this.tsmiCompile.Text = "Compile";
			// 
			// tsmiCompileSave
			// 
			this.tsmiCompileSave.Name = "tsmiCompileSave";
			this.tsmiCompileSave.Size = new System.Drawing.Size(138, 22);
			this.tsmiCompileSave.Text = "Save";
			// 
			// tvMethods
			// 
			this.tvMethods.AllowDrop = true;
			this.tvMethods.ContextMenuStrip = this.cmsMethod;
			this.tvMethods.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvMethods.ImageIndex = 0;
			this.tvMethods.ImageList = this.ilReflection;
			this.tvMethods.LabelEdit = true;
			this.tvMethods.Location = new System.Drawing.Point(0, 0);
			this.tvMethods.Name = "tvMethods";
			this.tvMethods.SelectedImageIndex = 0;
			this.tvMethods.Size = new System.Drawing.Size(250, 203);
			this.tvMethods.TabIndex = 2;
			this.tvMethods.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvMethods_AfterLabelEdit);
			this.tvMethods.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvMethods_BeforeExpand);
			this.tvMethods.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvMethods_DragDrop);
			this.tvMethods.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvMethods_DragEnter);
			this.tvMethods.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvMethods_KeyDown);
			// 
			// cmsMethod
			// 
			this.cmsMethod.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMethod,
            this.tsmiParameter});
			this.cmsMethod.Name = "cmsMethod";
			this.cmsMethod.Size = new System.Drawing.Size(136, 48);
			this.cmsMethod.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMethod_Opening);
			// 
			// tsmiMethod
			// 
			this.tsmiMethod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMethodAdd,
            this.tsmiMethodRemove,
            this.tsmiMethodRename});
			this.tsmiMethod.Name = "tsmiMethod";
			this.tsmiMethod.Size = new System.Drawing.Size(135, 22);
			this.tsmiMethod.Text = "&Method";
			this.tsmiMethod.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsmiMethod_DropDownItemClicked);
			// 
			// tsmiMethodAdd
			// 
			this.tsmiMethodAdd.Name = "tsmiMethodAdd";
			this.tsmiMethodAdd.Size = new System.Drawing.Size(124, 22);
			this.tsmiMethodAdd.Text = "&Add";
			// 
			// tsmiMethodRemove
			// 
			this.tsmiMethodRemove.Name = "tsmiMethodRemove";
			this.tsmiMethodRemove.Size = new System.Drawing.Size(124, 22);
			this.tsmiMethodRemove.Text = "&Remove";
			// 
			// tsmiMethodRename
			// 
			this.tsmiMethodRename.Name = "tsmiMethodRename";
			this.tsmiMethodRename.Size = new System.Drawing.Size(124, 22);
			this.tsmiMethodRename.Text = "Re&name";
			// 
			// tsmiParameter
			// 
			this.tsmiParameter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiParameterAdd,
            this.tsmiParameterRemove,
            this.tsmiParameterChange});
			this.tsmiParameter.Name = "tsmiParameter";
			this.tsmiParameter.Size = new System.Drawing.Size(135, 22);
			this.tsmiParameter.Text = "&Parameter";
			this.tsmiParameter.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsmiParameter_DropDownItemClicked);
			// 
			// tsmiParameterAdd
			// 
			this.tsmiParameterAdd.Name = "tsmiParameterAdd";
			this.tsmiParameterAdd.Size = new System.Drawing.Size(124, 22);
			this.tsmiParameterAdd.Text = "&Add";
			// 
			// tsmiParameterRemove
			// 
			this.tsmiParameterRemove.Name = "tsmiParameterRemove";
			this.tsmiParameterRemove.Size = new System.Drawing.Size(124, 22);
			this.tsmiParameterRemove.Text = "&Remove";
			// 
			// tsmiParameterChange
			// 
			this.tsmiParameterChange.Name = "tsmiParameterChange";
			this.tsmiParameterChange.Size = new System.Drawing.Size(124, 22);
			this.tsmiParameterChange.Text = "&Change";
			// 
			// ilReflection
			// 
			this.ilReflection.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilReflection.ImageStream")));
			this.ilReflection.TransparentColor = System.Drawing.Color.Fuchsia;
			this.ilReflection.Images.SetKeyName(0, "Method.Public.bmp");
			this.ilReflection.Images.SetKeyName(1, "Class.Public.bmp");
			this.ilReflection.Images.SetKeyName(2, "Field.Public.bmp");
			// 
			// splitMain
			// 
			this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitMain.Location = new System.Drawing.Point(0, 25);
			this.splitMain.Name = "splitMain";
			this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.tvMethods);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.lvErrors);
			this.splitMain.Panel2Collapsed = true;
			this.splitMain.Size = new System.Drawing.Size(250, 203);
			this.splitMain.SplitterDistance = 118;
			this.splitMain.TabIndex = 3;
			// 
			// lvErrors
			// 
			this.lvErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colErrLine,
            this.colErrNumber,
            this.colErrDescription});
			this.lvErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvErrors.Location = new System.Drawing.Point(0, 0);
			this.lvErrors.Name = "lvErrors";
			this.lvErrors.Size = new System.Drawing.Size(150, 46);
			this.lvErrors.TabIndex = 0;
			this.lvErrors.UseCompatibleStateImageBehavior = false;
			this.lvErrors.View = System.Windows.Forms.View.Details;
			// 
			// colErrLine
			// 
			this.colErrLine.Text = "Ln";
			// 
			// colErrNumber
			// 
			this.colErrNumber.Text = "Number";
			// 
			// colErrDescription
			// 
			this.colErrDescription.Text = "Description";
			// 
			// DocumentSqlCompiler
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitMain);
			this.Controls.Add(this.tsMain);
			this.Controls.Add(ssMain);
			this.Name = "DocumentSqlCompiler";
			this.Size = new System.Drawing.Size(250, 250);
			ssMain.ResumeLayout(false);
			ssMain.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.cmsMethod.ResumeLayout(false);
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			this.splitMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripStatusLabel tsslText;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripSplitButton tsbnProject;
		private System.Windows.Forms.ToolStripMenuItem tsmiProjectNew;
		private System.Windows.Forms.ToolStripMenuItem tsmiProjectOpen;
		private System.Windows.Forms.TreeView tvMethods;
		private System.Windows.Forms.ToolStripSplitButton tsbnCompile;
		private System.Windows.Forms.ToolStripMenuItem tsmiCompile;
		private System.Windows.Forms.ToolStripMenuItem tsmiCompileSave;
		private System.Windows.Forms.ContextMenuStrip cmsMethod;
		private System.Windows.Forms.ToolStripMenuItem tsmiMethod;
		private System.Windows.Forms.ToolStripMenuItem tsmiProjectSave;
		private System.Windows.Forms.ToolStripMenuItem tsmiParameter;
		private System.Windows.Forms.ToolStripMenuItem tsmiMethodAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiParameterAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiParameterRemove;
		private System.Windows.Forms.ToolStripMenuItem tsmiMethodRemove;
		private System.Windows.Forms.ToolStripMenuItem tsmiMethodRename;
		private System.Windows.Forms.ToolStripMenuItem tsmiParameterChange;
		private System.Windows.Forms.ImageList ilReflection;
		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.ListView lvErrors;
		private System.Windows.Forms.ColumnHeader colErrLine;
		private System.Windows.Forms.ColumnHeader colErrNumber;
		private System.Windows.Forms.ColumnHeader colErrDescription;
	}
}
