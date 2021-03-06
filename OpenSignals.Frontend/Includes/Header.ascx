﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="OpenSignals.Frontend.Includes.Header" %>
<div id="skyline">
    <div id="header">
        <div id="logo">
            <a runat="server" id="linkLogo" href="/"></a>
        </div>
        <div class="headerlinks">
            <ul>
                <li><a class="facebook" href="http://www.facebook.com/apps/application.php?id=183751108307062"><span>Seguici su Facebook</span></a></li>
                <li><a class="twitter" href="http://twitter.com/mettiaposto"><span>Seguici su Twitter</span></a></li>
				<li><a class="rss" runat="server" id="linkRss" href="#"><span>Sottoscrivi il feed RSS</span></a></li>
                <li><a class="georss" runat="server" id="geoRSSLink" href="#"><span>Sottoscrivi il feed GeoRSS</span></a></li>
            </ul>
        </div>
        <div class="searchBar">
            <div class="currentCity"><asp:Literal ID="ltCurrentCity" runat="server"></asp:Literal></div>
            <div id="search">
                <input type="text" id="searchCity" value="Cambia città..." name="searchCity" />
				<a class="button" title="Cambia città" href="#" onclick="checkPlace(); return false;"><img alt="Cambia città" src="/images/searchicon.png" /></a>
            </div>
        </div>
        <div id="menu">
            <ul runat="server" id="tabs" class="tabs">
                <li runat="server" id="link1" class="homeOff"><a title="Torna alla homepage" class="small"
                    id="linkHome" runat="server" href="/index.aspx"><span>Home</span></a></li>
                <li runat="server" runat="server" id="link2" class="tabOff"><a class="tabOff" title="Cerca fra le segnalazioni della tua città"
                    id="linkSearch" runat="server" href="/milano/cerca.aspx">Cerca</a></li>
                <li runat="server" id="link3" class="tabOff"><a class="tabOff" title="Hai qualcosa da segnalare? Inserisci i dati richiesti e invia la tua segnalazione"
                    id="linkSignal" runat="server" href="/milano/invia.aspx">Segnala</a></li>
                <li runat="server" id="link4" class="tabOff"><a class="tabOff" title="Domande, Risposte e altre utili informazioni"
                    href="/pages/info.aspx">FAQ</a></li>
            </ul>
        </div>
    </div>
</div>
<div style="clear: both;">
</div>
<div id="wrapper">
