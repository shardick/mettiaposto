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
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Data;
using NHibernate.Context;
using System.Web;
using NHibernate;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Framework.Web
{
    /// <summary>
    /// Common Global.asax class
    /// </summary>
    public class Global : BaseApplication
    {
        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                ManagedWebSessionContext.Bind(HttpContext.Current, NHibernateSession.Current.Factory.OpenSession());
            }
            catch (Exception ex)
            {
                LogUtils.Log("Error rewriting url", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Handles the EndRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ISession session = ManagedWebSessionContext.Unbind(
               HttpContext.Current, NHibernateSession.Current.Factory);

            if (session != null)
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                    session.Transaction.Rollback();
                else
                    session.Flush();

                session.Close();
            }
        }
    }
}
