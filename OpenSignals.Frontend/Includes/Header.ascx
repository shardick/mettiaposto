﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="OpenSignals.Frontend.Includes.Header" %>
<div id="skyline">
    <div id="header">
        <div id="logo">
            <a runat="server" id="linkLogo" href="/"></a>
        </div>
        <div id="menu">
            <ul runat="server" id="citiesMenu" class="cities sf-menu">
                <li runat="server" class="current"><a id="currentCityAnchor" runat="server"></a>
                    <asp:Repeater ID="rptCities" runat="server">
                        <FooterTemplate>
                            </ul></FooterTemplate>
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><a href="<%# ((OpenSignals.Framework.Places.Place)Container.DataItem).Link %>index.aspx">
                                <%# ((OpenSignals.Framework.Places.Place)Container.DataItem).Name %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </li>
            </ul>
            <div id="search" style="background-color: #ffffff; padding: 10px:">
                <input type="text" id="searchCity" name="searchCity" /> <input type="button" value="Cerca" onclick="searchCity(); return false;" />
            </div>
            <ul runat="server" id="tabs" class="tabs">
                <li runat="server" id="link1" class="homeOff"><a title="Torna alla homepage" class="small"
                    id="linkHome" runat="server" href="/index.aspx"><span>Home</span></a></li>
                <li runat="server" runat="server" id="link2" class="tabOff"><a class="tabOff" title="Cerca fra le segnalazioni della tua città"
                    id="linkSearch" runat="server" href="/milano/cerca.aspx">Cerca</a></li>
                <li runat="server" id="link3" class="tabOff"><a class="tabOff" title="Hai qualcosa da segnalare? Inserisci i dati richiesti e invia la tua segnalazione"
                    id="linkSignal" runat="server" href="/milano/invia.aspx">Segnala</a></li>
                <li runat="server" id="link4" class="tabOff"><a class="tabOff" title="Domande, Risposte e altre utili informazioni"
                    href="/pages/info.aspx">FAQ</a></li>
                <li class="rss"><a title="Iscriviti all'RSS della tua città per ricevere le segnalazioni in tempo reale"
                    id="linkRss" runat="server" class="small rss" href="/rss.ashx"><span>Iscriviti all'RSS
                        delle segnalazioni della tua città</span></a></li>
            </ul>
        </div>
    </div>
</div>
<div style="clear: both;">
</div>
<div id="wrapper">
