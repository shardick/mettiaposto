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
using System.Collections.Generic;
using System.Xml.Serialization;
using OpenSignals.Framework.Comments;

namespace OpenSignals.Framework.Signals
{
    /// <summary>
    /// This class is the signal object
    /// </summary>
    public class Signal
    {
        /// <summary>
        /// Signal status
        /// </summary>
        public struct SignalStatus
        {
            /// <summary>
            /// Not approved, to be approved
            /// </summary>
            public const int NotApproved = 0;
            /// <summary>
            /// Approved and publisheds
            /// </summary>
            public const int Approved = 1;
            /// <summary>
            /// Resolved
            /// </summary>
            public const int Resolved = 2;
            /// <summary>
            /// Expired (after X months of inactivity)
            /// </summary>
            public const int Expired = 3;
            /// <summary>
            /// Reopened after expiration or closed
            /// </summary>
            public const int ReOpened = 4;
        }

        public struct CriticalLevels
        {
            /// <summary>
            /// 
            /// </summary>
            public const int Normal = 1;
            /// <summary>
            /// 
            /// </summary>
            public const int Medium = 2;
            /// <summary>
            /// 
            /// </summary>
            public const int High = 3;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Signal"/> class.
        /// </summary>
        public Signal() { }

        #region Public Properties

        /// <summary>
        /// Gets or sets the author name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets the signal ID.
        /// </summary>
        /// <value>
        /// The signal ID.
        /// </value>
        public virtual int SignalID { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public virtual string Subject { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show author name]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowName { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        public virtual DateTime UpdateDate { get; set; }
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public virtual int CategoryID { get; set; }
        /// <summary>
        /// Gets or sets the author email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public virtual int Status { get; set; }
        /// <summary>
        /// Gets or sets the resolution date.
        /// </summary>
        /// <value>
        /// The resolution date.
        /// </value>
        public virtual DateTime ResolutionDate { get; set; }
        /// <summary>
        /// Gets or sets the resolution description.
        /// </summary>
        /// <value>
        /// The resolution description.
        /// </value>
        public virtual string ResolutionDescription { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public virtual decimal Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public virtual decimal Longitude { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public virtual string Address { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public virtual string City { get; set; }
        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public virtual string Zip { get; set; }
        /// <summary>
        /// Gets or sets the map zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        public virtual int Zoom { get; set; }
        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public virtual string Attachment { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [sent to council].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sent to council]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool SentToCouncil { get; set; }
        /// <summary>
        /// Gets or sets the sent to council date.
        /// </summary>
        /// <value>
        /// The sent to council date.
        /// </value>
        public virtual DateTime SentToCouncilDate { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        [XmlIgnore]
        public virtual IList<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the users subscriptions.
        /// </summary>
        /// <value>
        /// The subscriptions.
        /// </value>
        [XmlIgnore]
        public virtual IList<SignalSubscription> Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public virtual string CategoryName { get; set; }

        /// <summary>
        /// Gets the link to detail page
        /// </summary>
        public virtual string Link
        {
            get
            {
                return "/" + this.City.ToLower() + "/" + this.SignalID.ToString() + "/segnalazione.aspx";
            }
        }

        /// <summary>
        /// Gets or sets the reopen description.
        /// </summary>
        /// <value>
        /// The reopen description.
        /// </value>
        public virtual string ReopenDescription { get; set; }

        /// <summary>
        /// Gets or sets the reopen date.
        /// </summary>
        /// <value>
        /// The reopen date.
        /// </value>
        public virtual DateTime ReopenDate { get; set; }

        /// <summary>
        /// Gets the excerpt.
        /// </summary>
        public virtual string Excerpt
        {
            get
            {
                if (this.Description.Length > 100)
                    return this.Description.Substring(0, 99) + "...";
                else
                    return this.Description;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has image.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasImage
        {
            get { return !this.Attachment.Equals(string.Empty); }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual string Source { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the critical level.
        /// </summary>
        /// <value>
        /// The critical level.
        /// </value>
        public virtual int CriticalLevel { get; set; }
    }

    /// <summary>
    /// This class represent the signal subscription
    /// </summary>
    public class SignalSubscription
    {
        /// <summary>
        /// Gets or sets the signal ID.
        /// </summary>
        /// <value>
        /// The signal ID.
        /// </value>
        public virtual int SignalID { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        ///   </exception>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
