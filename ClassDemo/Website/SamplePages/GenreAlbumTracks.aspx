<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GenreAlbumTracks.aspx.cs" Inherits="SamplePages_GenreAlbumTracks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Genre Album Tracks</h1>
    <!-- 
        inside a repeater you need a minimum of one ItemTemplate
        other templates include HeaderTemplate, FooterTemplate,
        AlternatingItemTemplate, SperatorTemplate

        outer repeater will display the first fields from the DTO class
            which do not repeat (not in a collection)
        outer repeater will get its data from an ODS

        nested repeater will display the collection of the previous repeater
        nested repeater will get its datasource from the collection of the
            previous DTO level (either from a POCO or another DTO)
        
        -->
    <asp:Repeater ID="GenreAlbumTrackList" runat="server" 
        DataSourceID="GenreAlbumTrackListODS" 
        ItemType="Chinook.Data.DTOs.GenreDTO">
        <ItemTemplate>
            <h2>Genre: <%# Eval("genre") %></h2>
            <asp:Repeater ID="AlbumTrackList" runat="server"
               DataSource='<%# Eval("albums") %>' ItemType="Chinook.Data.DTOs.AlbumDTO">
                <ItemTemplate>
                    <h4>Album: 
                        <%--    First way of doing this
                            <%# string.Format("{0} ({1}) Tracks: {2}",
                            Eval("title"), Eval("releaseyear"),
                            Eval("numberoftracks")) %>
                            Second way of doing this below
                            --%>
                        <%# string.Format("{0} ({1}) Tracks: {2}",
                            Item.title, Item.releaseyear,
                            Item.numberoftracks) %>
                    </h4>
                    <asp:Repeater ID="TrackList" runat="server"
                        ItemType="Chinook.Data.POCOs.TrackPOCO"
                        DataSource="<%# Item.tracks %>">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th>Song</th>
                                    <th>Length</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width:600px"><%# Item.song %></td>
                                <td> <%# Item.length %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr style="height:3px;border:none;color:#000;background-color:#000;" />
                </SeparatorTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
    
    <asp:ObjectDataSource ID="GenreAlbumTrackListODS" runat="server" OldValuesParameterFormatString="original_{0}"
         SelectMethod="Genre_GenreAlbumTracks" TypeName="ChinookSystem.BLL.GenreController">
    </asp:ObjectDataSource>
</asp:Content>

