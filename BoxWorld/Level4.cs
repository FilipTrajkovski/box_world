﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxWorld
{
    [Serializable]
    public partial class Level4 : Form
    {

        List<PictureBox> boxes = new List<PictureBox>();
        List<PictureBox> bricks = new List<PictureBox>();
        List<PictureBox> redOrbs = new List<PictureBox>();

        int points = 0;
        VictoryPopUp victoryPopUp;
        List<PictureBox> scoredRedOrbs = new List<PictureBox>();

        bool didWin = false;

        public Level4()
        {
            InitializeComponent();

            // Init redOrbs list values
            redOrbs.Add(redOrb1);
            redOrbs.Add(redOrb2);
            redOrbs.Add(redOrb3);
            redOrbs.Add(redOrb4);
            redOrbs.Add(redOrb5);

            // Init boxes list values
            boxes.Add(box1);
            boxes.Add(box2);
            boxes.Add(box3);
            boxes.Add(box4);
            boxes.Add(box5);

            // Init bricks list values
            bricks.Add(brick1);
            bricks.Add(brick2);
            bricks.Add(brick3);
            bricks.Add(brick4);
            bricks.Add(brick5);
            bricks.Add(brick6);
            bricks.Add(brick7);
            bricks.Add(brick8);
            bricks.Add(brick9);
            bricks.Add(brick10);
            bricks.Add(brick11);
            bricks.Add(brick12);
            bricks.Add(brick13);
            bricks.Add(brick14);
            bricks.Add(brick15);
            bricks.Add(brick16);
            bricks.Add(brick17);
            bricks.Add(brick18);
            bricks.Add(brick19);
            bricks.Add(brick20);
            bricks.Add(brick21);
            bricks.Add(brick22);
            bricks.Add(brick23);
            bricks.Add(brick24);
            bricks.Add(brick25);
            bricks.Add(brick26);


            
        }

        public void InitState(FormState formState)
        {
            Helper.updatePictureBoxLocation(boxes, formState.boxes);

            wizard.Location = new Point(formState.wizardLocation.X, formState.wizardLocation.Y);

            scoredRedOrbs = Helper.extractPictureBoxesFromNames(formState.scoredRedOrbNames, redOrbs);
            points = scoredRedOrbs.Count;

            for (int i = 0; i < scoredRedOrbs.Count; i++)
            {
                PictureBox redOrb = scoredRedOrbs[i];

                PictureBox box = Helper.getPictureBoxByLocation(boxes, redOrb.Location);

                if (box != null)
                {
                    box.Image = Helper.getBitmapAssetByName("BoxWorld.Assets.crate_green.jpg");
                }
            }
        }

        private void Level4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'W' || e.KeyChar == 'w')
            {
                Helper.wizardMovement(Moving.UP, wizard, bricks, boxes, redOrbs, scoredRedOrbs);
            }
            if (e.KeyChar == 'S' || e.KeyChar == 's')
            {
                Helper.wizardMovement(Moving.DOWN, wizard, bricks, boxes, redOrbs, scoredRedOrbs);
            }
            if (e.KeyChar == 'A' || e.KeyChar == 'a')
            {
                Helper.wizardMovement(Moving.LEFT, wizard, bricks, boxes, redOrbs, scoredRedOrbs);
            }
            if (e.KeyChar == 'D' || e.KeyChar == 'd')
            {
                Helper.wizardMovement(Moving.RIGHT, wizard, bricks, boxes, redOrbs, scoredRedOrbs);
            }

            points = scoredRedOrbs.Count;
            Level4_victoryPopUp();

        }

        private void Level4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Helper.popUp.isExitCalledFromHere && !didWin)
            {
                e.Cancel = true;
                Helper.popUp.FormState.level = "Level4";
                Helper.popUp.FormState.boxes = Helper.mapToPictureBoxLocation(boxes);
                Helper.popUp.FormState.wizardLocation.X = wizard.Location.X;
                Helper.popUp.FormState.wizardLocation.Y = wizard.Location.Y;
                Helper.popUp.FormState.scoredRedOrbNames = Helper.extractNamesFromPictureBoxes(scoredRedOrbs);
                Helper.popUp.exitLabel.Text = "Are you sure you want to exit BoxWorld? (Game will be saved)";
                Helper.popUp.ShowDialog();
            }
        }

        private void Level4_victoryPopUp()
        {
            if (points == 5)
            {
                didWin = true;
                victoryPopUp = new VictoryPopUp(this, new Level5());
                victoryPopUp.ShowDialog();
            }
        }
    }
}
