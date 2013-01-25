using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace GitAutoCommit
{
    /// <summary>
    /// Helper for default fonts etc
    /// </summary>
    public static class FontHelper
    {
        private static readonly Font _defaultGuiFont;
        private static readonly Font _headingGuiFont;
        private static readonly Font _subHeadingGuiFont;
        private static readonly Font _monospaceFont;

        /// <summary>
        /// Static constructor
        /// </summary>
        static FontHelper()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _defaultGuiFont = ChooseFont(8.25f, "Segoe UI", "Calibri", "Tahoma", "Arial", "sans-serif");
            }
            else
            {
                _defaultGuiFont = ChooseFont(8.25f, "Tahoma", "Arial", "sans-serif");
            }

            var headingFont = ChooseFont(8.25f, "Segoe UI Light", "Segoe UI", "Calibri", "Tahoma", "Arial", "sans-serif");

            _subHeadingGuiFont = new Font(headingFont.FontFamily, 13f, FontStyle.Regular);
            _headingGuiFont = new Font(headingFont.FontFamily, 20f, FontStyle.Regular);

            _monospaceFont = ChooseFont(9.75f, "Consolas", "Courier New", "monospace");
        }

        /// <summary>
        /// Our default gui font
        /// </summary>
        public static Font DefaultGuiFont
        {
            get { return _defaultGuiFont; }
        }

        /// <summary>
        /// Heading gui font
        /// </summary>
        public static Font HeadingGuiFont
        {
            get { return _headingGuiFont; }
        }

        /// <summary>
        /// Heading gui font
        /// </summary>
        public static Font SubHeadingGuiFont
        {
            get { return _subHeadingGuiFont; }
        }

        /// <summary>
        /// Monospace font
        /// </summary>
        public static Font MonospaceFont
        {
            get { return _monospaceFont; }
        }

        /// <summary>
        /// Given a list of fonts, returns the first one found
        /// </summary>
        /// <param name="size">Size of the font</param>
        /// <param name="requestedFonts">The list of requested fonts</param>
        /// <returns>The font, or the system default font if none of the fonts were found</returns>
        private static Font ChooseFont(float size, params string[] requestedFonts)
        {
            var fonts = new InstalledFontCollection();

            foreach (string name in requestedFonts)
            {
                foreach (FontFamily family in fonts.Families)
                    if (family.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                        return new Font(family, size);
            }

            return SystemFonts.DefaultFont;
        }
    }
}