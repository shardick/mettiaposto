﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleSignal.ascx.cs"
    Inherits="FixMi.Frontend.Includes.SingleSignal" %>
<asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
    <ItemTemplate>
        <div class="item">
            <div class="title">
                <a runat="server" id="title"></a>
            </div>
            <div class="status">
                <asp:Image ID="status" runat="server" /></div>
        </div>
        <div class="legend">
            in
            <asp:Label ID="category" runat="server"></asp:Label>, inviato
            <asp:Label ID="timeframe" runat="server"></asp:Label>
        </div>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <div class="alt-item">
            <div class="title">
                <a runat="server" id="title"></a>
            </div>
            <div class="status">
                <asp:Image ID="status" runat="server" /></div>
        </div>
        <div class="legend">
            in
            <asp:Label ID="category" runat="server"></asp:Label>, inviato
            <asp:Label ID="timeframe" runat="server"></asp:Label>
        </div>
    </AlternatingItemTemplate>
    <SeparatorTemplate>
        <div class="clear">
        </div>
    </SeparatorTemplate>
</asp:Repeater>