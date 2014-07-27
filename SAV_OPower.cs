﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PKHeX
{
    public partial class SAV_OPower : Form
    {
        public SAV_OPower(Form1 frm1)
        {
            m_parent = frm1;
            InitializeComponent();
            LoadData();
        }
        Form1 m_parent;

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void B_Save_Click(object sender, EventArgs e)
        {
            this.SaveData();
            this.Close();
        }

        private void LoadData()
        {
            int o = 0x1BE00 + m_parent.savindex * 0x7F000; // offset

            // Fill up the 17 o-powers
            ComboBox[] cba = new ComboBox[] {
                CB_1, CB_2, CB_3, CB_4, CB_5, CB_6, CB_7, CB_8, CB_9,
                CB_10, CB_11, CB_12, CB_13, CB_14, CB_15, CB_16, CB_17,
            };
            // 1 2 3 4 5 10 use 4 bytes, everything else uses 3
            o++; // Skip first 0
            CB_1.SelectedIndex = getIndex(o, 4); o += 4; o++; // @ 1
            CB_2.SelectedIndex = getIndex(o, 4); o += 4; o++; // @ 6
            CB_3.SelectedIndex = getIndex(o, 4); o += 4; o++; // @ B
            CB_4.SelectedIndex = getIndex(o, 4); o += 4; o++; // @ 10
            CB_5.SelectedIndex = getIndex(o, 4); o += 4; o++; // @ 15

            CB_6.SelectedIndex = getIndex(o, 3); o += 3; // 1A
            CB_7.SelectedIndex = getIndex(o, 3); o += 3; // 1D
            CB_8.SelectedIndex = getIndex(o, 3); o += 3; // 20
            CB_9.SelectedIndex = getIndex(o, 3); o += 3; // 23

            o++;
            CB_10.SelectedIndex = getIndex(o, 4); o += 4; o++; // @ 27

            CB_11.SelectedIndex = getIndex(o, 3); o += 3; // 2C-2E
            CB_12.SelectedIndex = getIndex(o, 3); o += 3; // 2F-31
            CB_13.SelectedIndex = getIndex(o, 3); o += 3; // 32-34
            CB_14.SelectedIndex = getIndex(o, 3); o += 3; // 35-37
            CB_15.SelectedIndex = getIndex(o, 3); o += 3; // 38-3A
            CB_16.SelectedIndex = getIndex(o, 3); o += 3; // 3B-3D
            CB_17.SelectedIndex = getIndex(o, 3); o += 3; // 3E-40
        }
        private void SaveData()
        {
            ComboBox[] cba = new ComboBox[] {
                CB_1, CB_2, CB_3, CB_4, CB_5, CB_6, CB_7, CB_8, CB_9,
                CB_10, CB_11, CB_12, CB_13, CB_14, CB_15, CB_16, CB_17,
            };
            int[] offsets= new int[] {
                1,6,0xB,0x10,0x15,
                0x1A,0x1D,0x20,0x23,
                0x27,
                0x2C,0x2F,0x32,0x35,0x38,0x3B,0x3E,
            };
            int o = 0x1BE00 + m_parent.savindex * 0x7F000; // offset

            for (int i = 0; i < cba.Length; i++)
            {
                byte[] data = new Byte[cba[i].Items.Count - 1];
                for (int c = 0; c < cba[i].SelectedIndex; c++)
                {
                    data[c] = 1;
                }
                Array.Copy(data, 0, m_parent.savefile, o + offsets[i], data.Length);
            }
        }
        private void setindex(int o, int l, int v)
        {

        }
        private int getIndex(int o, int l)
        {
            byte[] _0 = new byte[] { 00, 00, 00, 00, };
            byte[] _1 = new byte[] { 01, 00, 00, 00, };
            byte[] _2 = new byte[] { 01, 01, 00, 00, };
            byte[] _3 = new byte[] { 01, 01, 01, 00, };
            byte[] _4 = new byte[] { 01, 01, 01, 01, };
            
            byte[] data = new Byte[4];
            Array.Copy(m_parent.savefile, o, data, 0, l);

            if      (data.SequenceEqual(_4)) return 4;
            else if (data.SequenceEqual(_3)) return 3;
            else if (data.SequenceEqual(_2)) return 2;
            else if (data.SequenceEqual(_1)) return 1;
            else if (data.SequenceEqual(_0)) return 0;
            else return 1;
        }

        private void B_AllMax_Click(object sender, EventArgs e)
        {
            max(false);
        }
        private void B_MaxP_Click(object sender, EventArgs e)
        {
            max(true);
        }
        private void max(bool s)
        {
            ComboBox[] cba = new ComboBox[] {
                CB_1, CB_2, CB_3, CB_4, CB_5, CB_6, CB_7, CB_8, CB_9,
                CB_10, CB_11, CB_12, CB_13, CB_14, CB_15, CB_16, CB_17,
            };
            if (s)
            {
                for (int i = 0; i < cba.Length; i++)
                    cba[i].SelectedIndex = cba[i].Items.Count-1;
            }
            else
            {
                for (int i = 0; i < cba.Length; i++)
                    cba[i].SelectedIndex = 3;
            }
        }
    }
}
