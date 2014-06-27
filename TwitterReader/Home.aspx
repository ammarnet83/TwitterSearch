<%@ Page Title="Hello Twitter Reader" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterReader.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Hello Twitter Reader</h3>
    <table style="width: 100%;">
        <tr>
            <td style="width: 150px;">Find for Hashtag: 
            </td>
            <td>#<asp:TextBox ID="txtHashTag" runat="server"></asp:TextBox>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Width="86px" />&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/TweetsMap.aspx" Target="_blank">View tweets locations on map</asp:HyperLink>
                &nbsp; -&nbsp;
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/TopTwitters.aspx" Target="_blank">View top users</asp:HyperLink>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Literal ID="literalTweets" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
    </table>
</asp:Content>
