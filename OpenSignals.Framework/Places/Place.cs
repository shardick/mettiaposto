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

using OpenSignals.Framework.API.Google;
namespace OpenSignals.Framework.Places
{
    /// <summary>
    /// This class represent a Place (= City)
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public virtual int PlaceID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Place"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Status { get; set; }
        /// <summary>
        /// Gets or sets the open311 URL.
        /// </summary>
        /// <value>
        /// The open311 URL.
        /// </value>
        public virtual string Open311URL { get; set; }
        /// <summary>
        /// Gets or sets the open311 API key.
        /// </summary>
        /// <value>
        /// The open311 API key.
        /// </value>
        public virtual string Open311ApiKey { get; set; }
        /// <summary>
        /// Gets or sets the open311 city ID.
        /// </summary>
        /// <value>
        /// The open311 city ID.
        /// </value>
        public virtual string Open311CityID { get; set; }

        /// <summary>
        /// Gets the link.
        /// </summary>
        public virtual string Link
        {
            get { return "/" + this.RewritedName.ToLower() + "/"; }
        }

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
        /// Gets or sets the map zoom.
        /// </summary>
        /// <value>
        /// The map zoom.
        /// </value>
        public virtual int MapZoom { get; set; }

        /// <summary>
        /// Gets or sets the geolocation info.
        /// </summary>
        /// <value>
        /// The geolocation info.
        /// </value>
        public virtual Geometry GeolocationInfo { get; set; }

        /// <summary>
        /// Gets or sets the name of the rewrited.
        /// </summary>
        /// <value>
        /// The name of the rewrited.
        /// </value>
        public virtual string RewritedName { get; set; }
    }
}
