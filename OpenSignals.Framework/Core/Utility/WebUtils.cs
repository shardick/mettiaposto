﻿// Copyright (C) 2010-2011 Francesco 'ShArDiCk' Bramato

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace OpenSignals.Framework.Core.Utility
{
    /// <summary>
    /// Utility class for web pages
    /// </summary>
    public class WebUtils
    {
        /// <summary>
        /// Invoke RenderControl method of a generic Control returning the content rendered
        /// </summary>
        /// <param name="c">The control to render</param>
        /// <returns>Control rendered in string</returns>
        public static string RenderControlToString(Control c)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            c.RenderControl(htmlWriter);
            return sb.ToString();
        }

        /// <summary>
        /// Creates the pagination control.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="recordsPerPage">The records per page.</param>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public static HtmlGenericControl CreatePagination(int totalRecords, int recordsPerPage, string func)
        {
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)recordsPerPage));

            if (totalRecords <= recordsPerPage)
                return new HtmlGenericControl("ul");
            else
            {
                HtmlGenericControl ul = new HtmlGenericControl("ul");

                for (int i = 0; i < totalPages; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    HtmlAnchor a = new HtmlAnchor();
                    a.InnerHtml = (i + 1).ToString();
                    a.HRef = "#";
                    a.Attributes.Add("onclick", JsUtils.CreateJsFunction(func, true, (i * recordsPerPage)));
                    li.Controls.Add(a);
                    ul.Controls.Add(li);
                }

                return ul;
            }
        }

        /// <summary>
        /// Resizes the image - Code from http://snippets.dzone.com/posts/show/4336
        /// </summary>
        /// <param name="FullsizeImage">The fullsize image.</param>
        /// <param name="NewWidth">The new width.</param>
        /// <param name="MaxHeight">Height of the max.</param>
        /// <param name="OnlyResizeIfWider">if set to <c>true</c> [only resize if wider].</param>
        /// <returns>Imaged resized</returns>
        public static Image ResizeImage(Image FullsizeImage, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;

            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            //FullsizeImage.Dispose();

            // Save resized picture
            //NewImage.Save(NewFile);
            return NewImage;
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        public static string GetImageUrl(string type, string attachment)
        {
            return ConfigurationOptions.Current.GetString("system_upload_path") + type + attachment;
        }

        /// <summary>
        /// Creates the attachments directories.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void CreateAttachmentsDirectories(string path)
        {
            if (!Directory.Exists(Path.Combine(path, UploadPaths.Original)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Original));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Mobile)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Mobile));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Small)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Small));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Big)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Big));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Comments)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Comments));
        }

        /// <summary>
        /// Formats the HTML.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string FormatHtml(string s)
        {
            return s.Replace("\n", "<br/>");
        }
    }
}
