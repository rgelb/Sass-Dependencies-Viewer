﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScintillaNET;

namespace SASSFileViewer
{
    public partial class frmMain : Form
    {
        #region Variables
        private Scintilla txtContent;

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private Color BACK_COLOR = Color.White;
        /// <summary>
        /// default text color of the text area
        /// </summary>
        private Color FORE_COLOR = Color.Black;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;
        #endregion

        #region Constructors
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmMain_Load(object sender, EventArgs e)
        {
            txtContent = new Scintilla();
            panelText.Controls.Add(txtContent);

            SetupTextEditor();
        }

        private void toolbtnOpen_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "SASS Files (*.scss)|*.scss"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadFile(ofd.FileName);
            }
        }

        private void treeSass_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtContent.Text = File.ReadAllText(e.Node.Name);
        }
        #endregion

        #region Text Editor Configuration
        private void SetupTextEditor()
        {
            // Basic Config
            txtContent.Dock = DockStyle.Fill;

            // Initial View Config
            txtContent.WrapMode = WrapMode.None;
            txtContent.IndentationGuides = IndentView.LookBoth;

            // Styling
            InitColors();
            InitSyntaxColoring();

            // Numbers margin
            InitNumberMargin();

            // Bookmarks margin
            InitBookmarkMargin();

            // Code folding margin
            InitCodeFolding();
        }

        private void InitNumberMargin()
        {            
            txtContent.Styles[Style.LineNumber].BackColor = BACK_COLOR;
            txtContent.Styles[Style.LineNumber].ForeColor = FORE_COLOR;
            txtContent.Styles[Style.IndentGuide].ForeColor = FORE_COLOR;
            txtContent.Styles[Style.IndentGuide].BackColor = BACK_COLOR;

            var nums = txtContent.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            txtContent.MarginClick += txtContent_MarginClick;
        }

        private void txtContent_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = txtContent.Lines[txtContent.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        private void InitBookmarkMargin()
        {

            //txtContent.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = txtContent.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = txtContent.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {
            txtContent.SetFoldMarginColor(true, BACK_COLOR);
            txtContent.SetFoldMarginHighlightColor(true, BACK_COLOR);

            // Enable code folding
            txtContent.SetProperty("fold", "1");
            txtContent.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            txtContent.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            txtContent.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            txtContent.Margins[FOLDING_MARGIN].Sensitive = true;
            txtContent.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                txtContent.Markers[i].SetForeColor(BACK_COLOR); // styles for [+] and [-]
                txtContent.Markers[i].SetBackColor(FORE_COLOR); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            txtContent.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            txtContent.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            txtContent.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            txtContent.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            txtContent.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            txtContent.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            txtContent.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            txtContent.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void InitColors()
        {
            txtContent.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        private void InitSyntaxColoring()
        {

            // Configure the default style
            txtContent.StyleResetDefault();
            txtContent.Styles[Style.Default].Font = "Consolas";
            txtContent.Styles[Style.Default].Size = 10;
            txtContent.Styles[Style.Default].BackColor = BACK_COLOR;
            txtContent.Styles[Style.Default].ForeColor = FORE_COLOR;
            txtContent.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            txtContent.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            txtContent.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            txtContent.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            txtContent.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            txtContent.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            txtContent.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            txtContent.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            txtContent.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            txtContent.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            txtContent.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            txtContent.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            txtContent.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            txtContent.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            txtContent.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            txtContent.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            txtContent.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            // configure CSS
            txtContent.Styles[Style.Css.Identifier].ForeColor = IntToColor(0x7590FF);
            txtContent.Styles[Style.Css.Comment].ForeColor = Color.Green;
            txtContent.Styles[Style.Css.PseudoClass].ForeColor = Color.Orange;
            txtContent.Styles[Style.Css.UnknownPseudoClass].ForeColor = Color.Orange;
            txtContent.Styles[Style.Css.UnknownIdentifier].ForeColor = Color.Black;
            txtContent.Styles[Style.Css.Value].ForeColor = Color.Black;
            txtContent.Styles[Style.Css.Id].ForeColor = Color.Blue;
            txtContent.Styles[Style.Css.Important].ForeColor = Color.Red;
            txtContent.Styles[Style.Css.Operator].ForeColor = IntToColor(0xAE690C);
            txtContent.Styles[Style.Css.Directive].ForeColor = Color.Green;
            txtContent.Styles[Style.Css.DoubleString].ForeColor = IntToColor(0xA31515);
            txtContent.Styles[Style.Css.SingleString].ForeColor = IntToColor(0xA31515);
            txtContent.Styles[Style.Css.Identifier2].ForeColor = IntToColor(0x7590FF);
            txtContent.Styles[Style.Css.Identifier3].ForeColor = IntToColor(0x7590FF);
            txtContent.Styles[Style.Css.Attribute].ForeColor = Color.Black;
            txtContent.Styles[Style.Css.Tag].ForeColor = Color.Blue;

            txtContent.Lexer = Lexer.Css;

            txtContent.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            txtContent.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }
        #endregion

        #region Helper Methods
        private void LoadFile(string fileName)
        {
            var mainSass = new SassFile() {FileName = fileName};
            ProcessFile(mainSass);
            LoadFileInTree(mainSass);

            treeSass.ExpandAll();   // expand all nodes in tree

            if (treeSass.Nodes.Count > 0)
                treeSass.SelectedNode = treeSass.Nodes[0];
        }

        private void LoadFileInTree(SassFile sassFile, TreeNode node = null)
        {
            var fi = new FileInfo(sassFile.FileName);
            TreeNode tn = node?.Nodes.Add(fi.FullName, fi.Name) ?? treeSass.Nodes.Add(fi.FullName, fi.Name);

            foreach (var childSass in sassFile.Children)
            {
                LoadFileInTree(childSass, tn);
            }
        }

        private void ProcessFile(SassFile sassFile)
        {
            var fi = new FileInfo(sassFile.FileName);
            foreach (var line in File.ReadAllLines(sassFile.FileName))
            {
                if (!line.Trim().ToLower().StartsWith("@import ")) continue;

                foreach (var import in GetimportsFromLine(line.Trim(), fi.DirectoryName))
                {
                    var child = new SassFile() { FileName = import };
                    sassFile.Children.Add(child);

                    ProcessFile(child);
                }
            }
        }

        private List<string> GetimportsFromLine(string importLine, string parentDir)
        {
            var importFiles = new List<string>();

                // remove @import
            importLine = Regex.Replace(importLine, "@import ", "", RegexOptions.IgnoreCase);

            // remove single and double quotes
            importLine = importLine.Replace("\"", "").Replace("'", "");

            // remove semicolons
            importLine = importLine.Replace(";", "");

            // replace forward slashes with backslashes
            importLine = importLine.Replace(@"/", @"\");

            // all that's left are files

            importFiles.Add(Path.IsPathRooted(importLine)
                ? importLine
                : Path.GetFullPath(Path.Combine(parentDir, importLine)));

            return importFiles;
        }
        #endregion
    }

    public class SassFile
    {
        public string FileName { get; set; } = string.Empty;
        public List<SassFile> Children { get; set; } = new List<SassFile>();
    }
}
