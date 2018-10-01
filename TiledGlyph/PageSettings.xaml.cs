﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;


namespace TiledGlyph
{
    /// <summary>
    /// PageSettings.xaml 的交互逻辑
    /// </summary>
    public partial class PageSettings : UserControl
    {
        public PageSettings()
        {
            InitializeComponent();
        }

        private bool checkNumbic(string str){
            Regex r = new Regex(@"^[0-9]*$");
            if (!r.IsMatch(str))
            {
                return false;
            }
            return true;
        }

        private bool checkIntStr(string str)
        {
            Regex r = new Regex(@"^[+-]?([1-9][0-9]*|0)(\.[0-9]+)?%?$");
            if (!r.IsMatch(str))
            {
                return false;
            }
            return true;
        }
        /*
        * 
        * 
        */
        private void textboxTileWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxTileWidth.Text.Trim();
            if (!checkNumbic(tc))
            {
                textboxTileWidth.Text = "16";
                return;
            }
            int tileWidth = Convert.ToInt32(tc);
            if (tileWidth > 128)
            {
                textboxTileWidth.Text = "16";
                return;
            }
            GlobalSettings.iTileWidth = tileWidth;
        }

        private void textboxTileHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxTileHeight.Text.Trim();
            if (!checkNumbic(tc))
            {
                textboxTileHeight.Text = "16";
                return;
            }
            int tileHeight = Convert.ToInt32(tc);
            if (tileHeight > 128)
            {
                textboxTileHeight.Text = "16";
                return;
            }
            GlobalSettings.iTileHeight = tileHeight;
        }

        private void textboxImageWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxImageWidth.Text.Trim();
            if (!checkNumbic(tc) || tc.Length < 1)
            {
                return;
            }

            int imageWidth = Convert.ToInt32(tc);
            if (imageWidth < 16)
            {
                return;
            }
            GlobalSettings.iImageWidth = imageWidth;
        }

        private void textboxImageHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxImageHeight.Text.Trim();
            if (!checkNumbic(tc) || tc.Length < 1)
            {
                textboxImageHeight.Text = "512";
                return;
            }
            int imageHeight = Convert.ToInt32(tc);
            if (imageHeight < 16)
            {
                return;
            }
            GlobalSettings.iImageHeight = imageHeight;
        }



        private void comboboxRenderMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentMode = comboboxRenderMode.SelectedIndex;
            GlobalSettings.iGRenderMode = currentMode;           
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {

            GlobalSettings.iTileWidth = Convert.ToInt32(textboxTileWidth.Text.Trim());
            GlobalSettings.iTileHeight = Convert.ToInt32(textboxTileHeight.Text.Trim());
            GlobalSettings.iFontHeight = (int)(Convert.ToInt32(textboxFontHeight.Text.Trim()) * 0.75);
            GlobalSettings.iGRenderMode = comboboxRenderMode.SelectedIndex;
            GlobalSettings.iImageWidth = Convert.ToInt32(textboxImageWidth.Text.Trim());
            GlobalSettings.iImageHeight = Convert.ToInt32(textboxImageHeight.Text.Trim());
            GlobalSettings.fFontName = textbox_FontName.Text;
            

        }

        private void outlineColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            string currentColor = outlineColorPicker.SelectedColor.ToString();
            if (currentColor != null)
            {
                string c = currentColor;
                textboxOutlineBrushColor.Text = c;
                System.Drawing.ColorConverter colConvert = new System.Drawing.ColorConverter();
                System.Drawing.Color color = (System.Drawing.Color)colConvert.ConvertFromString(c);
                GlobalSettings.cShadowColor = color;
            } 

        }

        private void penColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            string currentColor = penColorPicker.SelectedColor.ToString();
            if (currentColor != null)
            {
                string c = currentColor;
                textboxPenBrushColor.Text = c;
                System.Drawing.ColorConverter colConvert = new System.Drawing.ColorConverter();
                System.Drawing.Color color = (System.Drawing.Color)colConvert.ConvertFromString(c);
                GlobalSettings.cPenColor = color;
            }

        }


        private void bgColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            string currentColor = bgColorPicker.SelectedColor.ToString();
            if (currentColor != null)
            {
                string c = currentColor;
                textboxBgBrushColor.Text = c;
                System.Drawing.ColorConverter colConvert = new System.Drawing.ColorConverter();
                System.Drawing.Color color = (System.Drawing.Color)colConvert.ConvertFromString(c);
                GlobalSettings.cBgColor = color;
            } 
        }

        private void checkboxUseUHeight_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxUseUHeight.IsChecked == true){
                GlobalSettings.bUseUnlimitHeight = true;
            }
        }
        private void checkboxUseUHeight_UnChecked(object sender, RoutedEventArgs e)
        {
            if (checkboxUseUHeight.IsChecked == false)
            {
                GlobalSettings.bUseUnlimitHeight = false;
            }
            
        }

        private void buttonChooseFont_Click(object sender, RoutedEventArgs e)
        {
            string fName;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "";
            openFileDialog.Filter = "True Type Font|*.ttf;*.otf";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog().Value)
            {
                fName = openFileDialog.FileName;
                textbox_FontName.Text = fName;
                GlobalSettings.fFontName = textbox_FontName.Text;
            }

        }

        private void comboboxSaveImageType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentMode = comboboxRenderMode.SelectedIndex;
            if (currentMode == 0)
            {
                GlobalSettings.globalSaveFmt = System.Drawing.Imaging.ImageFormat.Png;
            }
            if (currentMode == 1)
            {
                GlobalSettings.globalSaveFmt = System.Drawing.Imaging.ImageFormat.Bmp;
            }
        }
        private void textboxFontBold_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxFontBold.Text.Trim();
            if (!checkIntStr(tc))
            {
                textboxFontHeight.Text = "0.4";
                return;
            }
            float font_bold = float.Parse(tc);
            if (font_bold > 8.0)
            {
                textboxFontBold.Text = "8";
                return;
            }
            GlobalSettings.iFontBold = font_bold;
        }

        private void textboxRPositionX_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxRPositionX.Text.Trim();
            if (!checkIntStr(tc))
            {
                return;
            }
            int RPositionX = Convert.ToInt32(tc);
            if (RPositionX > GlobalSettings.iFontHeight)
            {
                textboxRPositionX.Text = "0";
                return;
            }
            GlobalSettings.relativePositionX = RPositionX;
        }

        private void textboxRpositionY_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxRpositionY.Text.Trim();
            if (!checkIntStr(tc))
            {
                return;
            }
            int RPositionY = Convert.ToInt32(tc);
            if (RPositionY > GlobalSettings.iFontHeight)
            {
                textboxRpositionY.Text = "0";
                return;
            }
            GlobalSettings.relativePositionY = RPositionY;
        }

        private void checkboxOptmizeAlpha_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxOptmizeAlpha.IsChecked == true)
            {
                GlobalSettings.bOptmizeAlpha = true;
            }
        }

        private void checkboxOptmizeAlpha_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkboxOptmizeAlpha.IsChecked == false)
            {
                GlobalSettings.bOptmizeAlpha = false;
            }
        }

        private void checkboxUseOutlineEffect_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxUseOutlineEffect.IsChecked == true)
            {
                GlobalSettings.bUseOutlineEffect = true;
            }
        }

        private void checkboxUseOutlineEffect_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkboxUseOutlineEffect.IsChecked == false)
            {
                GlobalSettings.bUseOutlineEffect = false;
            }
        }

        private void textboxFontHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            string tc = textboxFontHeight.Text.Trim();
            if (!checkNumbic(tc))
            {
                textboxFontHeight.Text = "16";
                return;
            }
            int fontHeight = Convert.ToInt32(tc);
            if (fontHeight > 128)
            {
                textboxFontHeight.Text = "16";
                return;
            }
            GlobalSettings.iFontHeight = (int)(fontHeight * 0.75);
        }


    }
}
