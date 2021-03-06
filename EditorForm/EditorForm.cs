﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Story_Crafter {
    // TODO: add analyze tab: wallswim, unsmooth collision surfaces, ...
    // TODO: cutscenes tab
    // TODO: music tab
    // TODO: ambiance tab
    // TODO: custom objects tab
    public partial class EditorForm: Form {
        private ICSharpCode.AvalonEdit.TextEditor worldIni_avEdit;

        OverviewTab overview;
        ScreenTab screen;
        MapTab map;
        CutscenesTab cutscenes;
        TilesetsTab tilesets;
        GradientsTab gradients;
        MusicTab music;
        AmbianceTab ambiance;
        ObjectsTab objects;
        WorldIniTab worldIni;

        public EditorForm() {
            InitializeComponent();

            this.translucentPanel1.BackColor = Color.FromArgb(192, 255, 255, 255);

            this.worldIni_avEdit = new ICSharpCode.AvalonEdit.TextEditor();
            this.worldIni_avEdit.FontFamily = new System.Windows.Media.FontFamily("Courier New");
            this.worldIni_avEdit.BorderThickness = new System.Windows.Thickness(1);
            this.elementHost1.Child = worldIni_avEdit;
        }
        public void StoryChanged() {
            Program.ChangingStory = true;
            this.Text = Program.OpenStory.Title + " - Story Crafter";

            this.overview.StoryChanged();
            this.screen.StoryChanged();
            this.map.StoryChanged();
            this.cutscenes.StoryChanged();
            this.tilesets.StoryChanged();
            this.gradients.StoryChanged();
            this.music.StoryChanged();
            this.ambiance.StoryChanged();
            this.objects.StoryChanged();
            this.worldIni.StoryChanged();

            GC.Collect();
            Program.ChangingStory = false;
        }

        private void EditorForm_Load(object sender, EventArgs e) {
            Program.Debug.Show();
            if(Program.Start.ShowDialog(this) != DialogResult.OK) {
                this.Close();
                return;
            }

            this.overview = new OverviewTab(this);
            this.screen = new ScreenTab(this);
            this.map = new MapTab(this);
            this.cutscenes = new CutscenesTab(this);
            this.tilesets = new TilesetsTab(this);
            this.gradients = new GradientsTab(this);
            this.music = new MusicTab(this);
            this.ambiance = new AmbianceTab(this);
            this.objects = new ObjectsTab(this);
            this.worldIni = new WorldIniTab(this);

            StoryChanged();
        }

        private void menuItem2_Click(object sender, EventArgs e) {
            if(Program.Preferences.ShowDialog() == DialogResult.OK) {
                this.screen.ProfileChanged();
            }
        }

        private void editWithGIMPToolStripMenuItem_Click(object sender, EventArgs e) {
            //System.Diagnostics.Process.Start(@"D:\Program Files (x86)\GIMP\bin\gimp-2.6.exe", '"' + (string)((ContextMenuStrip)((ToolStripDropDownItem)sender).Owner).SourceControl.Tag + '"');
        }

        private void menuTools_Click(object sender, EventArgs e) {
            MenuItem m = (MenuItem)sender;
            menuTools.MenuItems[screen.CurrentToolIndex].Checked = false;
            m.Checked = true;
            screen.CurrentToolIndex = m.Index;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            switch(tabControl1.SelectedIndex) {
                case 0: this.overview.TabOpened(); break;
                case 1: this.screen.TabOpened(); break;
                case 2: this.map.TabOpened(); break;
                case 3: this.cutscenes.TabOpened(); break;
                case 4: this.tilesets.TabOpened(); break;
                case 5: this.gradients.TabOpened(); break;
                case 6: this.music.TabOpened(); break;
                case 7: this.ambiance.TabOpened(); break;
                case 8: this.objects.TabOpened(); break;
                case 9: this.worldIni.TabOpened(); break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch(tabControl1.SelectedIndex) {
                case 0: this.overview.ProcessCmdKey(ref msg, keyData); break;
                case 1: this.screen.ProcessCmdKey(ref msg, keyData); break;
                case 2: this.map.ProcessCmdKey(ref msg, keyData); break;
                case 3: this.cutscenes.ProcessCmdKey(ref msg, keyData); break;
                case 4: this.tilesets.ProcessCmdKey(ref msg, keyData); break;
                case 5: this.gradients.ProcessCmdKey(ref msg, keyData); break;
                case 6: this.music.ProcessCmdKey(ref msg, keyData); break;
                case 7: this.ambiance.ProcessCmdKey(ref msg, keyData); break;
                case 8: this.objects.ProcessCmdKey(ref msg, keyData); break;
                case 9: this.worldIni.ProcessCmdKey(ref msg, keyData); break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void menuItem8_Click(object sender, EventArgs e) {
            Program.OpenStory.Save(this.worldIni_avEdit.Text);
        }

        private void menuItem5_Click(object sender, EventArgs e) {
            if(Program.Start.ShowDialog() == DialogResult.OK) {
                StoryChanged();
            }
        }

        public string GetWorldINIText() {
            return this.worldIni_avEdit.Text;
        }
    }
}