namespace Graph
{
    partial class FormMap
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
            this.btnRemove = new System.Windows.Forms.Button();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.lblStartVertex = new System.Windows.Forms.Label();
            this.lblTargetVertex = new System.Windows.Forms.Label();
            this.cboSearch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1070, 179);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(156, 42);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGraph.Location = new System.Drawing.Point(12, 12);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1048, 648);
            this.panelGraph.TabIndex = 0;
            this.panelGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseClick);
            this.panelGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseMove);
            // 
            // lblStartVertex
            // 
            this.lblStartVertex.AutoSize = true;
            this.lblStartVertex.Location = new System.Drawing.Point(1066, 53);
            this.lblStartVertex.Name = "lblStartVertex";
            this.lblStartVertex.Size = new System.Drawing.Size(98, 20);
            this.lblStartVertex.TabIndex = 8;
            this.lblStartVertex.Text = "Start Vertex:";
            // 
            // lblTargetVertex
            // 
            this.lblTargetVertex.AutoSize = true;
            this.lblTargetVertex.Location = new System.Drawing.Point(1066, 82);
            this.lblTargetVertex.Name = "lblTargetVertex";
            this.lblTargetVertex.Size = new System.Drawing.Size(109, 20);
            this.lblTargetVertex.TabIndex = 9;
            this.lblTargetVertex.Text = "Target Vertex:";
            // 
            // cboSearch
            // 
            this.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch.FormattingEnabled = true;
            this.cboSearch.Items.AddRange(new object[] {
            "Breadth-First Search",
            "Depth-First Search",
            "Greedy Best-First Search",
            "A* Search",
            "CSP Search"});
            this.cboSearch.Location = new System.Drawing.Point(1070, 12);
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(244, 28);
            this.cboSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1070, 131);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(156, 42);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(1070, 275);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(156, 42);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(1070, 323);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(156, 42);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new System.Drawing.Point(1070, 371);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(156, 42);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(1070, 227);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(156, 42);
            this.btnRemoveAll.TabIndex = 16;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.BtnRemoveAll_Click);
            // 
            // FormMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1379, 676);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboSearch);
            this.Controls.Add(this.lblTargetVertex);
            this.Controls.Add(this.lblStartVertex);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.panelGraph);
            this.Name = "FormMap";
            this.Text = "Cebu Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblStartVertex;
        private System.Windows.Forms.Label lblTargetVertex;
        private System.Windows.Forms.ComboBox cboSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnRemoveAll;
    }
}

