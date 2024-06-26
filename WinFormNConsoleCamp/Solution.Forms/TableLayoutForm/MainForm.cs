﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TableLayoutForm
{
    public partial class Form1 : Form
    {
        public TableLayoutPanel tbPanel;

        private readonly int[,] arr;

        public Form1()
        {
            InitializeComponent();
            arr = new int[,]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
                { 13, 14, 15, 16 }
            };

            tbPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = arr.GetLength(0),
                RowCount = arr.GetLength(1),
            };

            Controls.Add(tbPanel);
            Resize += (s, e) => ReSizeTableLayout();
            Load += Form1_Load;

            Text = @"Table Layout Demo";
            Width = 1600;
            Height = 1000;
            StartPosition = FormStartPosition.CenterScreen;


            var list = new List<int>();

            list.Sort();
            list.Sort();
        }

        /*
         
        // (1) -> List.Sort();
        Sort(0, Count, null);    

        // (2) -> List.Sort
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            if (index < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }

            if (_size - index < count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
            }

            Array.Sort(_items, index, count, comparer);
            _version++;
        }



        // (3) -> Array.Sort
        public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (index < 0 || length < 0)
            {
                throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }

            if (array.Length - index < length)
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
            }

            if (length > 1 && ((comparer != null && comparer != Comparer<T>.Default) || !TrySZSort(array, null, index, index + length - 1)))
            {
                ArraySortHelper<T>.Default.Sort(array, index, length, comparer);
            }
        }
          
         */

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int y = 0; y < arr.GetLength(0); y++)
            {
                for (int x = 0; x < arr.GetLength(1); x++)
                {
                    Button btn = new Button
                    {
                        Text = $"arr[{x}, {y}] = {arr[y, x]}",
                        Dock = DockStyle.Fill,
                        BackColor = Color.Beige,
                        Font = new Font(Font.FontFamily, 20f, FontStyle.Regular)
                    };
                    tbPanel.Controls.Add(btn, x, y);
                }
            }
        }

        private void ReSizeTableLayout()
        {
            tbPanel.RowStyles.Clear();
            tbPanel.ColumnStyles.Clear();
            for (int y = 0; y < arr.GetLength(0); y++)
            {
                tbPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / arr.GetLength(0)));
            }
            for (int x = 0; x < arr.GetLength(1); x++)
            {
                tbPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / arr.GetLength(1)));
            }
        }
    }
}
