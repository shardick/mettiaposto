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

using System.Collections.Generic;
namespace OpenSignals.Framework.Categories
{
    /// <summary>
    /// This class represents signal category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category() { }

        #region Private variables

        private int _categoryID;
        private string _name;
        private bool _status;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public virtual int CategoryID { get { return _categoryID; } set { _categoryID = value; } }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get { return _name; } set { _name = value; } }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Category"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Status { get { return _status; } set { _status = value; } }
        /// <summary>
        /// Gets or sets the associations with Places
        /// </summary>
        /// <value>
        /// The associations.
        /// </value>
        public virtual IList<CategoryAssociation> Associations { get; set; }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class CategoryAssociation
    {
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public virtual int CategoryID { get; set; }
        /// <summary>
        /// Gets or sets the place ID.
        /// </summary>
        /// <value>
        /// The place ID.
        /// </value>
        public virtual int PlaceID { get; set; }
        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        /// <value>
        /// The contact email.
        /// </value>
        public virtual string ContactEmail { get; set; }
        /// <summary>
        /// Gets or sets the open311 service code.
        /// </summary>
        /// <value>
        /// The open311 service code.
        /// </value>
        public virtual string Open311ServiceCode { get; set; }
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
