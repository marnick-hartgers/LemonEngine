namespace LemonEngine.RenderLogic
{
    partial class RenderTools
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
            this.checkBoxLight = new System.Windows.Forms.CheckBox();
            this.checkBoxSmooth = new System.Windows.Forms.CheckBox();
            this.checkBoxColorMaterial = new System.Windows.Forms.CheckBox();
            this.checkBoxTextures = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxLight
            // 
            this.checkBoxLight.AutoSize = true;
            this.checkBoxLight.Checked = true;
            this.checkBoxLight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLight.Location = new System.Drawing.Point(13, 13);
            this.checkBoxLight.Name = "checkBoxLight";
            this.checkBoxLight.Size = new System.Drawing.Size(49, 17);
            this.checkBoxLight.TabIndex = 0;
            this.checkBoxLight.Text = "Light";
            this.checkBoxLight.UseVisualStyleBackColor = true;
            this.checkBoxLight.CheckedChanged += new System.EventHandler(this.checkBoxLight_CheckedChanged);
            // 
            // checkBoxSmooth
            // 
            this.checkBoxSmooth.AutoSize = true;
            this.checkBoxSmooth.Checked = true;
            this.checkBoxSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSmooth.Location = new System.Drawing.Point(12, 59);
            this.checkBoxSmooth.Name = "checkBoxSmooth";
            this.checkBoxSmooth.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSmooth.TabIndex = 1;
            this.checkBoxSmooth.Text = "Smooth";
            this.checkBoxSmooth.UseVisualStyleBackColor = true;
            this.checkBoxSmooth.CheckedChanged += new System.EventHandler(this.checkBoxSmooth_CheckedChanged);
            // 
            // checkBoxColorMaterial
            // 
            this.checkBoxColorMaterial.AutoSize = true;
            this.checkBoxColorMaterial.Location = new System.Drawing.Point(12, 36);
            this.checkBoxColorMaterial.Name = "checkBoxColorMaterial";
            this.checkBoxColorMaterial.Size = new System.Drawing.Size(87, 17);
            this.checkBoxColorMaterial.TabIndex = 2;
            this.checkBoxColorMaterial.Text = "ColorMaterial";
            this.checkBoxColorMaterial.UseVisualStyleBackColor = true;
            this.checkBoxColorMaterial.CheckedChanged += new System.EventHandler(this.checkBoxColorMaterial_CheckedChanged);
            // 
            // checkBoxTextures
            // 
            this.checkBoxTextures.AutoSize = true;
            this.checkBoxTextures.Checked = true;
            this.checkBoxTextures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTextures.Location = new System.Drawing.Point(12, 82);
            this.checkBoxTextures.Name = "checkBoxTextures";
            this.checkBoxTextures.Size = new System.Drawing.Size(67, 17);
            this.checkBoxTextures.TabIndex = 3;
            this.checkBoxTextures.Text = "Textures";
            this.checkBoxTextures.UseVisualStyleBackColor = true;
            this.checkBoxTextures.CheckedChanged += new System.EventHandler(this.checkBoxTextures_CheckedChanged);
            // 
            // RenderTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 112);
            this.Controls.Add(this.checkBoxTextures);
            this.Controls.Add(this.checkBoxColorMaterial);
            this.Controls.Add(this.checkBoxSmooth);
            this.Controls.Add(this.checkBoxLight);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenderTools";
            this.Text = "RenderTools";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxLight;
        private System.Windows.Forms.CheckBox checkBoxSmooth;
        private System.Windows.Forms.CheckBox checkBoxColorMaterial;
        private System.Windows.Forms.CheckBox checkBoxTextures;
    }
}